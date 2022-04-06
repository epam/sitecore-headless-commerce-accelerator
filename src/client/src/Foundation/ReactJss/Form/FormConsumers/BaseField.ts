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

import invariant from 'invariant';
import * as React from 'react';

import { FieldValue, FormValues } from './../models';
import { BaseFieldEnhancedProps, FieldHTMLElement } from './models';

export default class BaseField<THtmlElement extends FieldHTMLElement> extends React.Component<
  BaseFieldEnhancedProps<THtmlElement>
> {
  private fieldRef: React.RefObject<THtmlElement> = React.createRef();

  protected get getFieldRef() {
    return this.fieldRef;
  }

  protected get currentFieldRef() {
    return this.getFieldRef.current;
  }

  public constructor(props: BaseFieldEnhancedProps<THtmlElement>) {
    super(props);

    this.fieldChangeHandler = this.fieldChangeHandler.bind(this);

    const { formContext } = this.props;
    const key = this.getFieldKey();

    formContext.RegisterFieldChangeHandler(key, this.fieldChangeHandler);
  }

  public componentWillUnmount() {
    const { UnregisterField } = this.props.formContext;

    UnregisterField(this.getFieldKey());
  }

  protected registerField(validity: ValidityState, defaultValue?: FieldValue) {
    const key = this.getFieldKey();
    this.props.formContext.RegisterField(key, { validity, touched: false, defaultValue });
  }

  protected handleChange(value: FieldValue) {
    const { formContext } = this.props;

    const fieldKey = this.getFieldKey();

    // Dispatch is async function, and we want to run validation against latest field value
    formContext.EmitFieldChange(fieldKey, value);
  }

  protected handleBlur() {
    const { formContext } = this.props;

    const { fields } = formContext.form;

    const field = fields[this.getFieldKey()];

    if (!field.touched) {
      // touch it
    }
  }

  protected getPureProps() {
    const pureProps = {
      ...this.props,
    };

    if (pureProps.formContext) {
      delete pureProps.formContext;
    }
    if (pureProps.customValidator) {
      delete pureProps.customValidator;
    }

    return pureProps;
  }

  protected getFieldKey() {
    const { name } = this.props;

    invariant(!!name, 'Field key is not defined');
    return name;
  }

  private fieldChangeHandler(values: FormValues) {
    // the field was destroyed
    if (!this.currentFieldRef) {
      return null;
    }

    const { customValidator } = this.props;

    if (customValidator) {
      const validationResult = customValidator(values);

      if (!validationResult) {
        this.currentFieldRef.setCustomValidity('invalid');
      } else {
        this.currentFieldRef.setCustomValidity('');
      }
    }

    return {
      validity: this.currentFieldRef.validity,
    };
  }
}
