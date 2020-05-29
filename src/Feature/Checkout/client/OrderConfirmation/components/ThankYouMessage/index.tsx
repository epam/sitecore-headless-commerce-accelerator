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

import * as Jss from 'Foundation/ReactJss/client';

import './styles.scss';

import { ThankYouMessageProps, ThankYouMessageState } from './models';

export class ThankYouMessage extends Jss.SafePureComponent<ThankYouMessageProps, ThankYouMessageState> {
  public safeRender() {
    const { order } = this.props;
    return (
      <div className="thank-you-bg-wrapper">
        <section className="container">
          <div className="row">
            <div className="col-md-12 nopadding">
              <div className="thank-you-content">
                <div className="col-md-9">
                  <div className="thank-you-col1">
                    <span className="thank-you-text1">Thank You For Your Order.</span>
                    <span className="thank-you-text2">
                      Order #{order.trackingNumber} has been received. Check <strong>{order.email}</strong> for status
                      updates.
                    </span>
                  </div>
                </div>
                <div className="col-md-3">
                  <div className="thank-you-col2">
                    <a className="btn btn-main btn-order-print">
                      <i className="fa fa-print" /> <span>Print Receipt</span>
                    </a>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </section>
      </div>
    );
  }
}