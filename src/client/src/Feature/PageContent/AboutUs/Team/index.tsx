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

import * as JSS from 'Foundation/ReactJss';
import * as React from 'react';
import { AboutUsTeamMockData } from '../mocks';
import { TeamProps, TeamState } from './models';

import TeamPlaceholder from 'Foundation/UI/common/media/images/team-placeholder.jpg';

import '../styles.scss';

export class Team extends JSS.SafePureComponent<TeamProps, TeamState> {
  protected safeRender() {
    return (
      <div className="team-area">
        <div className="container">
          <div className="team-title">
            <h2>Team Members</h2>
            <p>Lorem ipsum dolor sit amet conse ctetu.</p>
          </div>
          <div className="row">
            {
              AboutUsTeamMockData.map((data, key) => {
                return (
                  <div className="col-lg-3 col-md-6 col-sm-6" key={key}>
                  <div className="team-wrapper">
                    <div className="team-img">
                      <img
                        src={data.img ? data.img : TeamPlaceholder}
                        alt=""
                        className="img-fluid"
                      />
                      <div className="team-action">
                        <a
                          className="facebook"
                          href={data.fbLink}
                          target="_blank"
                          rel="noopener noreferrer"
                        >
                          <i className="fa fa-facebook" />
                        </a>
                        <a
                          className="twitter"
                          href={data.twitterLink}
                          target="_blank"
                          rel="noopener noreferrer"
                        >
                          <i className="fa fa-twitter" />
                        </a>
                        <a
                          className="instagram"
                          href={data.instagramLink}
                          target="_blank"
                          rel="noopener noreferrer"
                        >
                          <i className="fa fa-instagram" />
                        </a>
                      </div>
                    </div>
                    <div className="team-content text-center">
                      <h4>{data.name}</h4>
                      <span>{data.position} </span>
                    </div>
                  </div>
                </div>
                );
              })
            }
            </div>
          </div>
        </div>
    );
  }
}
