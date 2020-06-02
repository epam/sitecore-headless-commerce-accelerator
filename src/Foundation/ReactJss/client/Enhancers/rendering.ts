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

import { withExperienceEditorChromes, withSitecoreContext } from '@sitecore-jss/sitecore-jss-react';
import { branch, compose, renderComponent, renderNothing } from 'recompose';

import { DatasourceRequiredComponent } from '../Components/ExperienceEditorWarning';

import { RenderingWithContextProps } from './models';

export const rendering = <P, S = {}>(component: React.ComponentClass<P, S>) =>
  withExperienceEditorChromes(component) as React.ComponentClass<P, S>;

export const renderingWithContext = <P extends { sitecoreContext: any }, S = {}>(
  component: React.ComponentClass<P, S>,
  options?: { updatable?: boolean },
) => compose<P, P>(withSitecoreContext(options), rendering)(component);

const withDatasourceMissingErrorInExperienceMode = branch<RenderingWithContextProps>(
  (props) => props && props.sitecoreContext && props.sitecoreContext.pageEditing && !props.rendering.dataSource,
  renderComponent(DatasourceRequiredComponent),
);

const withRenderedComponentInNormalMode = (renderInNormalModeWithoutDatasource: boolean) =>
  branch<RenderingWithContextProps>(
    (props) =>
      props &&
      props.sitecoreContext &&
      !props.sitecoreContext.pageEditing &&
      !props.rendering.dataSource &&
      !renderInNormalModeWithoutDatasource,
    renderNothing,
  );

export const renderingWithContextAndDatasource = <P extends { sitecoreContext: any }, S = {}>(
  component: React.ComponentClass<P, S>,
  options?: { updatable?: boolean },
  renderInNormalModeWithoutDatasource: boolean = false,
) =>
  renderingWithContext(
    compose<P, P>(
      withDatasourceMissingErrorInExperienceMode,
      withRenderedComponentInNormalMode(renderInNormalModeWithoutDatasource),
    )(component),
    options,
  );
