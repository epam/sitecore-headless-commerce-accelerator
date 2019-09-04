//    Copyright 2019 EPAM Systems, Inc.
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

import * as Models from './models';

export class SafePureComponent<P, S extends Models.SafePureComponentState> extends React.PureComponent<P, S> {
  private hasError: boolean = false;

  constructor(props: P, context?: any) {
    super(props, context);
  }

  public componentDidMount() {
    this.setState({ hasError: this.hasError });
  }

  public render() {
    // componentDidCatch doesn't work in React 16's renderToString
    // https://github.com/facebook/react/issues/10442
    // Due to streaming nature of renderToString in React 16, need to wrap each render method in try/catch
    try {
      // Using error boundaries for client-side rendering
      if (this.state && this.state.hasError) {
        return this.renderErrorView();
      }

      return this.safeRender();
    } catch (error) {
      // this allows to differentiate server vs client rendering
      if (typeof window !== 'undefined') {
        // used to avoid side-effects via setState()
        this.hasError = true;
        console.error('Error inside', this.getComponentName(), 'is', error);
      }

      return this.renderErrorView();
    }
  }

  // this method should be overridden in nested classes
  protected safeRender(): React.ReactNode {
    return (
      <span dangerouslySetInnerHTML={{ __html: `<!-- Component ${this.getComponentName()} is not implemented. -->` }} />
    );
  }

  protected renderErrorView() {
    return (
      <span
        dangerouslySetInnerHTML={{
          __html: `<!-- Error inside ${this.getComponentName()}. For more info, please, check the console."-->`,
        }}
      />
    );
  }

  protected getComponentName() {
    return !!this.constructor.name ? this.constructor.name : 'component';
  }
}
