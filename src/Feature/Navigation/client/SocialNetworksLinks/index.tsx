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

import * as JSS from 'Foundation/ReactJss/client';
import * as React from 'react';

import { Link, Text, withExperienceEditorChromes } from '@sitecore-jss/sitecore-jss-react';
import { SocialLinksProps, SocialLinksState } from './models';
import './styles.scss';

class SocialNetworksLinks extends JSS.SafePureComponent<SocialLinksProps, SocialLinksState> {
  protected safeRender() {
    const { fields } = this.props;
    const { datasource } = fields.data;

    return (
      <div>
        <Text tag="h2" field={datasource.sectionTitle.jss} className="social-title" />
        <ul className="social-list">
          {datasource.links && datasource.links.items && datasource.links.items.map((link, index) => {
              const { uri, cssClass } = link;

              return (
                <li key={index} className="social-item">
                  <Link field={uri.jss} title=" " target="_blank" className="social-link">
                    <i className={cssClass.jss.value}/>
                  </Link>
                </li>
              );
          })}
        </ul>
      </div>
    );
  }
}
export default withExperienceEditorChromes(SocialNetworksLinks);