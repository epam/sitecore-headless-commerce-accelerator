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

import * as JSS from 'Foundation/ReactJss';

import { StoreLocatorOwnState, StoreLocatorProps } from './models';

class StoreLocatorComponent extends JSS.SafePureComponent<StoreLocatorProps, StoreLocatorOwnState> {
  protected safeRender() {
    const { title, description } = this.props.fields.data.datasource;
    return (
      <div className="col-md-8 order-md-1">
        <h4 className="mb-3">{title.jss.value}</h4>
        <p>{description.jss.value}</p>
        <form className="needs-validation">
          <div className="row">
            <div className="col-md-3 mb-3">
              <label>Zip Code</label>
              <input type="text" className="form-control" id="zipCode" placeholder="Enter zip code..." value="" />
            </div>
            <div className="col-md-3 mb-3">
              <label>Radius</label>
              <select className="custom-select d-block w-100" id="radius">
                <option value="">Choose...</option>
                <option>50</option>
                <option>100</option>
              </select>
            </div>
            <div className="col-md-8 mb-3">
              <button type="submit">Search</button>
            </div>
          </div>
        </form>
      </div>
    );
  }
}

export const StoreLocator = JSS.rendering(StoreLocatorComponent);
