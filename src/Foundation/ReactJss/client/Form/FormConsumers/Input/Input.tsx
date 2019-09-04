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

import BaseField from '../BaseField';

export default class InputField extends BaseField<HTMLInputElement> {
  public componentDidMount() {
    const { validity } = this.currentFieldRef;

    const defaultValue = this.getDefaultValue();
    this.registerField(validity, defaultValue);
  }

  public render() {
    const pureProps = this.getPureProps();

    return (
      <input
        ref={this.getFieldRef}
        {...pureProps}
        onChange={(e) => this.handleOnChange(e)}
        onBlur={(e) => this.handleOnBlur(e)}
      />
    );
  }

  private handleOnChange(e: React.ChangeEvent<HTMLInputElement>) {
    const { onChange } = this.props;
    if (onChange) {
      onChange(e);
    }

    const value = this.getValue(e);
    this.handleChange(value);
  }

  private handleOnBlur(e: React.FocusEvent<HTMLInputElement>) {
    const { onBlur } = this.props;

    if (onBlur) {
      onBlur(e);
    }

    this.handleBlur();
  }

  private getDefaultValue() {
    const { defaultValue, type } = this.props;

    switch (type) {
      case 'checkbox':
      case 'radio': {
        const { defaultChecked } = this.props;

        return defaultChecked ? defaultValue : null;
      }
      default: {
        return defaultValue;
      }
    }
  }

  private getValue(e: React.ChangeEvent<HTMLInputElement>) {
    const { type } = this.props;

    switch (type) {
      case 'checkbox': {
        return e.target.checked;
      }
      case 'radio':
      default: {
        return e.target.value;
      }
    }
  }
}
