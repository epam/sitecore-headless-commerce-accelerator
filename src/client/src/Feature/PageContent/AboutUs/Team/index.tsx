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

import { Image, Link, Text } from '@sitecore-jss/sitecore-jss-react';

import * as JSS from 'Foundation/ReactJss';
import * as React from 'react';

import { TeamProps, TeamState } from './models';

import '../styles.scss';

export class Team extends JSS.SafePureComponent<TeamProps, TeamState> {
  protected safeRender() {
    const { datasource } = this.props.fields.data;

    return (
      <div className="team-area">
        <div className="container">
          <div className="team-title">
            <Text tag="h2" field={datasource.title.jss} className="about-us-heading " />
            <Text tag="p" field={datasource.text.jss} />
          </div>
          <div className="row row-flexible">
            {datasource &&
              datasource.items &&
              datasource.items.map((data, key) => {
                return (
                  <div className="col-lg-3 col-md-6 col-sm-6" key={key}>
                    <div className="team-wrapper">
                      <div className="team-img">
                        <Image media={data.image.jss} className="img-fluid" />
                        <div className="team-action">
                          {data.items &&
                            data.items.map((link, index) => (
                              <Link field={link.uri.jss} rel="noopener noreferrer" key={index}>
                                <i className={link.iconClass.jss.value} />
                              </Link>
                            ))}
                        </div>
                      </div>
                      <div className="team-content text-center">
                        <Text tag="h4" field={data.fullName.jss} />
                        <Text tag="span" field={data.position.jss} />
                      </div>
                    </div>
                  </div>
                );
              })}
          </div>
        </div>
      </div>
    );
  }
}
