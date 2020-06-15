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

import { SubmitEnhancedProps } from './models';

export default class Submit extends React.Component<SubmitEnhancedProps> {
  public render() {
    const { formContext, disabled } = this.props;

    const isDisabled = !formContext.form.status.valid || disabled;
    const pureProps = this.getPureProps();

    return (
      <button {...pureProps} type="submit" disabled={isDisabled} onClick={(e) => this.handleFormSubmit(e)}>
        {this.props.children}
      </button>
    );
  }

  protected getPureProps() {
    const pureProps = {
      ...this.props,
    };

    if (pureProps.formContext) {
      delete pureProps.formContext;
    }

    if (pureProps.onSubmitHandler) {
      delete pureProps.onSubmitHandler;
    }

    return pureProps;
  }

  private handleFormSubmit(e: React.MouseEvent<HTMLButtonElement>) {
    e.preventDefault();

    const { onSubmitHandler, formContext } = this.props;

    onSubmitHandler(formContext.form.values, formContext.form.fields);
  }
}
