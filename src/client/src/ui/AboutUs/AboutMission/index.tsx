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

import { Text } from '@sitecore-jss/sitecore-jss-react';

import * as JSS from 'Foundation/ReactJss';
import * as React from 'react';
import { AboutMissionProps, AboutMissionState } from './models';

import '../styles.scss';

export class AboutMission extends JSS.SafePureComponent<AboutMissionProps, AboutMissionState> {
  protected safeRender() {
    const { items } = this.props.fields;

    return (
      <div className="about-mission-area">
        <div className="container">
          <div className="row">
            {items &&
              items.map((paragraph, index) => (
                <div className="col-lg-4 col-md-4" key={index}>
                  <div className="single-mission">
                    <Text tag="h3" field={paragraph.fields.title} />
                    <Text tag="p" field={paragraph.fields.text} />
                  </div>
                </div>
              ))}
          </div>
        </div>
      </div>
    );
  }
}
