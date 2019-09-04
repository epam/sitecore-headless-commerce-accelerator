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

import FormContext from './../FormContext';
import { FieldChangeHandler, FieldValue } from './../models';
import { FieldChangeHandlers, FormEnhancedProps } from './models';

export default class Form extends React.Component<FormEnhancedProps> {
  private fieldChangeHandlers: FieldChangeHandlers = {};

  public constructor(props: FormEnhancedProps) {
    super(props);

    this.registerFieldChangeHandler = this.registerFieldChangeHandler.bind(this);
    this.emitFieldChange = this.emitFieldChange.bind(this);
  }

  public render() {
    const pureHtmlFormProps = this.getPureHtmlFormProps();
    const contextProps = this.getContextProps();

    return (
      <form {...pureHtmlFormProps}>
        <FormContext.Provider value={contextProps}>{this.props.children}</FormContext.Provider>
      </form>
    );
  }

  private getContextProps() {
    const { formState, RegisterField, UnregisterField } = this.props;
    return {
      EmitFieldChange: this.emitFieldChange,
      RegisterField,
      RegisterFieldChangeHandler: this.registerFieldChangeHandler,
      UnregisterField,
      form: formState,
    };
  }

  private getPureHtmlFormProps() {
    const pureHtmlFormProps = {
      ...this.props,
    };

    // TODO: rework with copying
    if (pureHtmlFormProps.dispatch) {
      delete pureHtmlFormProps.dispatch;
    }

    if (pureHtmlFormProps.formState) {
      delete pureHtmlFormProps.formState;
    }

    if (pureHtmlFormProps.RegisterField) {
      delete pureHtmlFormProps.RegisterField;
    }

    if (pureHtmlFormProps.FormChange) {
      delete pureHtmlFormProps.FormChange;
    }

    if (pureHtmlFormProps.UnregisterField) {
      delete pureHtmlFormProps.UnregisterField;
    }

    return {
      ...pureHtmlFormProps,
    };
  }

  private registerFieldChangeHandler(fieldKey: string, fieldChangeHandler: FieldChangeHandler) {
    this.fieldChangeHandlers[fieldKey] = fieldChangeHandler;
  }

  private emitFieldChange(currentFieldKey: string, currentFieldValue: FieldValue) {
    const { values } = this.props.formState;
    const { FormChange } = this.props;

    const newValues = {
      ...values,
      [currentFieldKey]: currentFieldValue,
    };

    const newFields = Object.keys(this.fieldChangeHandlers).reduce((result, validatorKey) => {
      const handler = this.fieldChangeHandlers[validatorKey];

      const handlerResult = handler(newValues);

      if (handlerResult) {
        result[validatorKey] = handlerResult;
      }

      return result;
      // tslint:disable-next-line:align
    }, {});

    FormChange(newValues, newFields);
  }
}
