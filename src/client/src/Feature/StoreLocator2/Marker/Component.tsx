import * as React from 'react';

import * as JSS from 'Foundation/ReactJss';

import { InfoWindow, Marker as GoogleMapMarker } from 'react-google-maps';
import { MarkerProps, MarkerState } from './models';

export class Marker extends JSS.SafePureComponent<MarkerProps, MarkerState> {

  private divRef = React.createRef<HTMLInputElement>();

  public constructor(props: MarkerProps) {
    super(props);

    this.state = {
      isOpen: false,
    };

    this.onToggleOpen = this.onToggleOpen.bind(this);
  }

  public handleClick = (e: MouseEvent) => {
    const currentRef = this.divRef.current;
    if (!this.state.isOpen || !currentRef || currentRef.contains(e.target as Element)) {
      return;
    }

    this.setState({
      isOpen: false,
    });
  };

  public componentDidMount() {
    document.addEventListener('mousedown', this.handleClick);
    document.addEventListener('touchstart', this.handleClick);
  }

  public componentWillUnmount() {
    document.removeEventListener('mousedown', this.handleClick);
    document.removeEventListener('touchend', this.handleClick);
  }

  protected safeRender() {
    return (
      <GoogleMapMarker
        position={{ lat: this.props.position.latitude, lng: this.props.position.longitude }}
        onClick={this.onToggleOpen}
      >
        {this.state.isOpen && (
          <InfoWindow onCloseClick={this.onToggleOpen}>
            <div ref={this.divRef}>
              <h4>{this.props.information.title}</h4>
              <p>{this.props.information.description}</p>
            </div>
          </InfoWindow>
        )}
      </GoogleMapMarker>
    );
  }
  private onToggleOpen() {
    this.setState({
      isOpen: !this.state.isOpen,
    });
  }
}
