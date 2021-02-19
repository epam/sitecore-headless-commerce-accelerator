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

import BaseField from '../BaseField';

import { Select } from 'Foundation/UI/components/Select';

export default class SelectField extends BaseField<HTMLSelectElement> {
  public componentDidMount() {
    const { defaultValue } = this.props;
    const { validity } = this.currentFieldRef;

    this.registerField(validity, defaultValue);
  }

  public render() {
    const pureProps = this.getPureProps();
    const { current } = this.getFieldRef;
    const hasError = current ? !current.validity.valid : true;

    return (
      <Select error={hasError} ref={this.getFieldRef} {...pureProps} onChange={(e: any) => this.handleOnChange(e)}>
        {this.props.children}
      </Select>
    );
  }

  private handleOnChange(e: React.ChangeEvent<HTMLSelectElement>) {
    this.handleChange(e.target.value);
  }
}
