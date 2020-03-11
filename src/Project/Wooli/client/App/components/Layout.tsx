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

import { Placeholder, VisitorIdentification } from '@sitecore-jss/sitecore-jss-react';
import * as React from 'react';
import Helmet from 'react-helmet';

import { LayoutProps } from '../../models';

export default class Layout extends React.Component<LayoutProps> {
  public render() {
    const title = this.getPageTitle();
    return (
      <React.Fragment>
        <Helmet>
          <title>{title}</title>
        </Helmet>
        <VisitorIdentification />
        <Placeholder name="wooli-content" {...this.props} />
      </React.Fragment>
    );
  }

  private getPageTitle(): string {
    const rendering = this.props.rendering as any;
    const { category, product } = this.props.sitecoreContext;
    const globalTitle = 'Wooli';
    const pageTitle =
      (category && category.displayName) ||
      (product && product.displayName) ||
      (rendering && rendering.displayName) ||
      '';

    return pageTitle ? `${pageTitle} | ${globalTitle} ` : globalTitle;
  }
}
