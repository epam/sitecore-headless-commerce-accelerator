//    Copyright 2020 EPAM Systems, Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

import * as React from 'react';

import { graphql } from '@apollo/client/react/hoc';
import { resetExperienceEditorChromes, withSitecoreContext } from '@sitecore-jss/sitecore-jss-react';
import { DefinitionNode, DocumentNode, OperationDefinitionNode, VariableDefinitionNode } from 'graphql';

/**
 * Higher order component that abstracts common JSS + Apollo integration needs.
 *
 * This component works similar to react-apollo's graphql() HOC, but:
 * * Automatically injects $contextItem and $datasource GraphQL variable values, if the GraphQL declares usage of them
 * * Automatically disables execution of subscription queries when doing SSR
 * * Passes through any other props to its wrapped component
 *
 * @param {*} query The GraphQL AST to execute (should go through graphql-tag, no strings)
 * @param {*} configuration Values passed in are shipped to react-apollo configuration (https://www.apollographql.com/docs/react/basics/setup.html#graphql-config)
 */
function GraphQLData(
  query: DocumentNode,
  configuration: any,
): (Component: React.ComponentClass<any, React.ComponentState> | React.StatelessComponent<any>) => any {
  return function wrapComponent(
    Component: React.ComponentClass<any, React.ComponentState> | React.StatelessComponent<any>,
  ): any {
    interface SitecoreRenderingWrapperProps {
      sitecoreContext: {
        pageEditing?: boolean;
        language?: string;
        pageState?: 'preview' | 'edit' | 'normal';
        visitorIdentificationTimestamp?: number;
        site?: {
          name?: string;
        };
        itemId: string;
      };
      rendering: any;
    }

    class SitecoreRenderingWrapper extends React.Component<SitecoreRenderingWrapperProps> {
      public static displayName: string = `JSSGraphQLComponent(${
        Component.displayName || Component.name || 'Component'
      })`;

      public render() {
        if (!query) {
          throw new Error(
            'query was falsy in GraphQLData. It should be a GraphQL query from graphql-tag. Perhaps missing graphql-tag/loader?',
          );
        }

        const newConfiguration: any = { ...configuration };

        if (!newConfiguration.name) {
          newConfiguration.name = 'data';
        }

        // ensure variables object exists
        newConfiguration.options = newConfiguration.options || {};
        newConfiguration.options.variables = newConfiguration.options.variables || {};

        // if we're in experience editor or preview we need to disable SSR of GraphQL queries
        // because SSR queries are made unauthenticated, so they would have normal mode data = bad
        if (this.props.sitecoreContext && this.props.sitecoreContext.pageState !== 'normal') {
          newConfiguration.options.ssr = false;
        } else if (
          query.definitions.some(
            (def: DefinitionNode) => def.kind === 'OperationDefinition' && def.operation === 'subscription',
          )
        ) {
          // if the document includes any subscriptions, we also disable SSR as this hangs the SSR process
          // not to mention being quite silly to SSR when they're reactive
          newConfiguration.options.ssr = false;
        }

        // find all variable definitions in the GraphQL query, so we can send only ones we're using
        const variableNames: { [s: string]: boolean } = extractVariableNames(query);

        // set the datasource variable, if we're using it
        if (variableNames.datasource && this.props.rendering && this.props.rendering.dataSource) {
          newConfiguration.options.variables.datasource = this.props.rendering.dataSource;
        }

        // set the contextItem variable, if we're using it
        if (variableNames.contextItem && this.props.sitecoreContext && this.props.sitecoreContext.itemId) {
          newConfiguration.options.variables.contextItem = this.props.sitecoreContext.itemId;
        }

        // build the props processing function that will set the result object to the name
        newConfiguration.props = (props: any) => {
          const innerQuery = props[newConfiguration.name];

          let resultProps: any = {};

          resultProps[newConfiguration.name] = innerQuery;

          // run a user-specified props function too if one exists
          if (configuration.props) {
            resultProps = { ...resultProps, ...configuration.props(props) };
          }
          return resultProps;
        };

        const GQL = graphql(query, newConfiguration)(Component);

        return <GQL {...this.props} />;
      }

      // eslint-disable-next-line class-methods-use-this
      public componentDidUpdate(): void {
        resetExperienceEditorChromes();
      }
    }

    return withSitecoreContext()(SitecoreRenderingWrapper);
  };
}

function extractVariableNames(query: any): { [s: string]: boolean } {
  const variableNames: { [s: string]: boolean } = {};

  query.definitions
    .map((def: OperationDefinitionNode) => def.variableDefinitions)
    .filter((def: VariableDefinitionNode) => def)
    .forEach((defs: ReadonlyArray<VariableDefinitionNode>) =>
      defs.forEach((def: VariableDefinitionNode) => {
        if (def.kind && def.kind === 'VariableDefinition') {
          variableNames[def.variable.name.value] = true;
        }
      }),
    );

  return variableNames;
}

export default GraphQLData;
