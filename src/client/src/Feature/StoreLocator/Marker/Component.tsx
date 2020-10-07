import * as React from 'react';

import * as JSS from 'Foundation/ReactJss';

import { InfoWindow, Marker as GoogleMapMarker } from 'react-google-maps';
import { MarkerProps, MarkerState } from './models';

const deltaPixels = 6;

export class Marker extends JSS.SafePureComponent<MarkerProps, MarkerState> {
  private divRef = React.createRef<HTMLInputElement>();

  public constructor(props: MarkerProps) {
    super(props);

    this.state = {
      isOpen: false,
      startX: undefined,
      startY: undefined,
    };
  }

  public componentDidMount() {
    document.addEventListener('mousedown', this.handleClick);
    document.addEventListener('mouseup', this.onMouseUp);
    document.addEventListener('touchstart', this.handleClick);
    document.addEventListener('touchend', this.onMouseUp);
  }

  public componentWillUnmount() {
    document.removeEventListener('mousedown', this.handleClick);
    document.removeEventListener('mouseup', this.onMouseUp);
    document.removeEventListener('touchend', this.handleClick);
    document.removeEventListener('touchend', this.onMouseUp);
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
  private onToggleOpen = () => {
    this.setState({
      isOpen: !this.state.isOpen,
    });
  };

  private handleClick = (e: MouseEvent) => {
    const currentRef = this.divRef.current;
    const targetElement = e.target as Element;

    if (!this.state.isOpen || !currentRef || currentRef.contains(targetElement)) {
      return;
    }
    this.setState({
      startX: e.pageX,
      startY: e.pageY,
    });
  };

  private onMouseUp = (e: MouseEvent) => {
    const diffX = Math.abs(e.pageX - this.state.startX);
    const diffY = Math.abs(e.pageY - this.state.startY);

    if (diffX > deltaPixels || diffY > deltaPixels) {
      return;
    }
  };
}
