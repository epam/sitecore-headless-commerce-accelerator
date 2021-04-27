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

import React, { ChangeEvent } from 'react';

import { Checkbox } from 'components';

import BaseField from '../BaseField';

export default class CheckboxField extends BaseField<HTMLInputElement> {
  public componentDidMount() {
    const { validity } = this.currentFieldRef;
    const defaultValue = this.props.defaultChecked;
    this.registerField(validity, defaultValue);
  }

  public render() {
    const { ...pureProps } = this.getPureProps();

    return (
      <Checkbox
        ref={this.getFieldRef}
        controlSize={(pureProps as any).controlSize}
        {...pureProps}
        onChange={(e: any) => this.handleOnChange(e)}
      />
    );
  }

  private handleOnChange(e: ChangeEvent<HTMLInputElement>) {
    if (this.props.onChange) {
      this.props.onChange(e);
    }

    this.handleChange(e.target.checked);
  }
}
