import * as React from 'react';

import * as JSS from 'Foundation/ReactJss';

import { GoogleMap, withGoogleMap, withScriptjs } from 'react-google-maps';
import { MapProps, MapState } from './models';

import { Marker } from '../Marker/Component';

class MapComponent extends JSS.SafePureComponent<MapProps, MapState> {
  protected safeRender() {
    const { defaultCenter, markers } = this.props;
    return (
      <GoogleMap defaultZoom={4} defaultCenter={{ lat: defaultCenter.latitude, lng: defaultCenter.longitude }}>
        {markers.map((marker, index) => (
          <Marker key={index} {...marker} />
        ))}
      </GoogleMap>
    );
  }
}

export const Map = withScriptjs(withGoogleMap(MapComponent));
