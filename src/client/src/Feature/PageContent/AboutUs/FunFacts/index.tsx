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

import CountUp from 'react-countup';
import VisibilitySensor from 'react-visibility-sensor';
import { FunFactsProps, FunFactsState } from './models';

import { AboutUsFactsMockData } from '../mocks';

export class FunFacts extends JSS.SafePureComponent<FunFactsProps, FunFactsState> {
  public constructor(props: any) {
    super(props);
    this.state = {
      countUp: false,
    };
  }

  protected safeRender() {
    const { countUp } = this.state;
    return (
    <div className="funfact-area">
      <div className="container">
        <div className="row">
          {AboutUsFactsMockData &&
            AboutUsFactsMockData.map((data, key) => {
              const endNumber = countUp ? data.countNum : 0;
              return (
                <div className="col-lg-3 col-md-6 col-sm-6" key={key}>
                  <div className="single-count">
                    <div className="count-icon">
                      <i className={data.iconClass} />
                    </div>
                    <h2 className="count">
                      <VisibilitySensor
                        onChange={this.onVisibilityChange}
                        offset={{ top: 10 }}
                        delayedCall={true}
                      >
                        <CountUp end={endNumber} />
                      </VisibilitySensor>
                    </h2>
                    <span>{data.title}</span>
                  </div>
                </div>
              );
            })}
        </div>
      </div>
    </div>
    );
  }
  private onVisibilityChange = (isVisible: boolean) => {
    if (isVisible) {
      this.setState({
        countUp: true
      });
    }
  };
}
