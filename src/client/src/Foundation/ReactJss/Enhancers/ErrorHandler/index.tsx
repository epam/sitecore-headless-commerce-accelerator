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

interface ErrorHandlerWrapperState {
  hasError: boolean;
}

export const ErrorHandler = <TProps extends {}>(
  WrappedComponent: React.ComponentClass<TProps> | React.Factory<TProps>,
): React.ComponentClass<TProps> => {
  return class ErrorHandlerWrapper extends React.Component<TProps, ErrorHandlerWrapperState> {
    public componentDidCatch(error: Error, errorInfo: React.ErrorInfo) {
      this.setState({ hasError: true });
      console.error('Error inside', this.getWrappedComponentName(), 'is', error);
    }
    public render() {
      if (this.state && this.state.hasError) {
        // You can render any custom fallback UI
        const componentName = this.getWrappedComponentName();
        return (
          <span
            dangerouslySetInnerHTML={{
              __html: `<!-- Error inside ${componentName}. For more info please check the console -->`,
            }}
          />
        );
      }
      return <WrappedComponent {...this.props} />;
    }
    private getWrappedComponentName() {
      const WrappedComponentAsComponentClass = WrappedComponent as React.ComponentClass<TProps>;
      return WrappedComponentAsComponentClass.displayName || WrappedComponent.name || 'Component';
    }
  };
};
