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

import React from 'react';

import { RadioButton } from 'Foundation/UI/components/RadioButton';

import BaseField from '../BaseField';

export default class RadioField extends BaseField<HTMLInputElement> {
  public componentDidMount() {
    const { validity } = this.currentFieldRef;
    const defaultValue = this.getDefaultValue();
    this.registerField(validity, defaultValue);
  }

  public render() {
    const { ...pureProps } = this.getPureProps();

    return (
      <RadioButton
        ref={this.getFieldRef}
        {...pureProps}
        controlSize={(pureProps as any).controlSize}
        onChange={(e: any) => this.handleOnChange(e)}
        onBlur={(e: any) => this.handleOnBlur(e)}
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
    const { defaultValue } = this.props;

    return defaultValue;
  }

  private getValue(e: React.ChangeEvent<HTMLInputElement>) {
    return e.target.value;
  }
}
