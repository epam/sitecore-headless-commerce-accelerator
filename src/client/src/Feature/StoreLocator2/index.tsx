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

import { Map } from './Map/Component';
import { Store, StoreLocatorOwnState, StoreLocatorProps } from './models';

import { Marker } from './Marker/models';

import './styles.scss';

class StoreLocatorComponent extends JSS.SafePureComponent<StoreLocatorProps, StoreLocatorOwnState> {
  protected safeRender() {
    const { title, description, defaultLatitude, defaultLongitude, stores } = this.props.fields.data.datasource;

    return (
      <div className="col-md-12 order-md-1 locator_content" style={{marginTop: 30, marginBottom: 20}}>
        <h2 className="mb-3 locator_header">{title.jss.value}</h2>
        <p>{description.jss.value}</p>
        <form className="needs-validation">
          <div style={{display: 'flex', flexDirection: 'row', marginBottom: 30}}>
            <div className="form_item">
              <label>Zip Code</label>
              <input type="text" id="zipCode" className="form_item_input" placeholder="Enter zip code..." />
            </div>
            <div className="form_item">
              <label>Radius</label>
              <select className="d-block w-100 form_item_input" id="radius">
                <option value="">Choose...</option>
                <option>50</option>
                <option>100</option>
              </select>
            </div>
            <div className="form_item">
              <label>Country</label>
              <select className="d-block w-100 form_item_input" id="country">
                <option value="">Choose...</option>
                <option>United States</option>
                <option>Canada</option>
              </select>
            </div>
            <div className="form_item">
              <button type="submit" className="form_item_input">Search</button>
            </div>
          </div>
          <div className="row">
            <div className="col-md-12">
              <Map
                markers={stores.items.map(this.mapStoreToMarker)}
                defaultCenter={{ latitude: +defaultLatitude.jss.value, longitude: +defaultLongitude.jss.value }}
                googleMapURL="https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=geometry,drawing,places"
                loadingElement={<div style={{ height: `100%` }} />}
                containerElement={<div style={{ height: `500px` }} />}
                mapElement={<div style={{ height: `100%` }} />}
              />
            </div>
          </div>
        </form>
      </div>
    );
  }

  private mapStoreToMarker(store: Store): Marker {
    return {
      information: {
        description: store.description.jss.value,
        title: store.title.jss.value,
      },
      position: {
        latitude: +store.latitude.jss.value,
        longitude: +store.longitude.jss.value,
      },
    };
  }
}

export const StoreLocator = JSS.rendering(StoreLocatorComponent);
