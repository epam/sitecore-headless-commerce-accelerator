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

import { Link, Text } from '@sitecore-jss/sitecore-jss-react';
import * as React from 'react';

import * as JSS from 'Foundation/ReactJss';

import { SocialLinksProps, SocialLinksState } from './models';

import './styles.scss';

export default class SocialNetworksLinksComponent extends JSS.SafePureComponent<SocialLinksProps, SocialLinksState> {
  protected safeRender() {
    const { datasource } = this.props.fields.data;
    const { params } = this.props;

    return (
      <div className={params.containerClass}>
        {params.showTitle && (
          <Text tag={params.titleTag} field={datasource.sectionTitle.jss} className={params.titleClass} />
        )}
        <ul className="social-list">
          {datasource &&
            datasource.links &&
            datasource.links.items &&
            datasource.links.items.map((link, index) => {
              const { uri } = link;
              return (
                <li key={index} className="social-item">
                  {this.props.isPageEditingMode ? (
                    <Link field={uri.jss} className="social-link" />
                  ) : (
                    <Link field={uri.jss} className="social-link">
                      <i className={uri.jss.value.class} />
                    </Link>
                  )}
                </li>
              );
            })}
        </ul>
      </div>
    );
  }
}
