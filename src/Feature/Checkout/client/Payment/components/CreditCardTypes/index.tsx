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

import AmericanExpress from 'Foundation/UI/client/common/media/images/cc-american-express-straight-32px.png';
import Discover from 'Foundation/UI/client/common/media/images/cc-discover-straight-32px.png';
import MasterCard from 'Foundation/UI/client/common/media/images/cc-mastercard-straight-32px.png';
import Visa from 'Foundation/UI/client/common/media/images/cc-visa-straight-32px.png';

export default () => (
  <div className="credit-cards-col">
    <ul className="card-types">
      <li>
        <img src={Visa} alt="Visa" />
      </li>
      <li>
        <img src={MasterCard} alt="MasterCard" />
      </li>
      <li>
        <img src={AmericanExpress} alt="American Express" />
      </li>
      <li>
        <img src={Discover} alt="Discover" />
      </li>
    </ul>
  </div>
);
