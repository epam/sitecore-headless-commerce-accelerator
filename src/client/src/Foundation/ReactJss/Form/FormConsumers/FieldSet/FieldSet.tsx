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

import { FieldSetEnhancedProps } from './models';

export default class FieldSet extends React.Component<FieldSetEnhancedProps> {
  public render() {
    const { customVisibility, formContext } = this.props;

    if (customVisibility) {
      const { values, fields } = formContext.form;
      const customVisibilityResult = customVisibility(values, fields);

      if (!customVisibilityResult) {
        return null;
      }
    }

    const pureProps = this.getPureProps();

    return <fieldset {...pureProps}>{this.props.children}</fieldset>;
  }

  protected getPureProps() {
    const pureProps = {
      ...this.props,
    };

    if (pureProps.formContext) {
      delete pureProps.formContext;
    }
    if (pureProps.customVisibility) {
      delete pureProps.customVisibility;
    }

    return pureProps;
  }
}
