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
import { AboutMissionProps, AboutMissionState } from './models';

import '../styles.scss';

export class AboutMission extends JSS.SafePureComponent<AboutMissionProps, AboutMissionState> {
  protected safeRender() {
    return (
      <div className="about-mission-area">
   <div className="container">
      <div className="row">
         <div className="col-lg-4 col-md-4">
            <div className="single-mission">
               <h3>Our Vision</h3>
               <p>Enables a JSS-based eCommerce site that is headless, meaning it is decoupled from Sitecore servers</p>
            </div>
         </div>
         <div className="col-lg-4 col-md-4">
            <div className="single-mission">
               <h3>Our Mission</h3>
               <p>Allows front-end JavaScript developers to quickly develop and adjust eCommerce website features independently from Sitecore back-end developers</p>
            </div>
         </div>
         <div className="col-lg-4 col-md-4">
            <div className="single-mission">
               <h3>Our Goal</h3>
               <p>Offers a proven alternative to the Sitecore Experience Accelerator (SXA)-based storefront</p>
            </div>
         </div>
      </div>
   </div>
</div>
    );
  }
}
