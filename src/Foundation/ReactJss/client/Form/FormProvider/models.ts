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

import { Field, FieldChangeHandler, FieldValue, FormFields, FormState, FormValues } from './../models';

export interface FieldChangeHandlers {
  [fieldKey: string]: FieldChangeHandler;
}

export interface FormAction<T = {}> {
  type: string;
  payload?: T;
}

export interface FormStateProp {
  formState: FormState;
}

export interface FormDispatchProp<TActionPayload = {}> {
  dispatch: (action: FormAction<TActionPayload>, callback?: () => void) => void;
}

export interface FormHandlerProps {
  RegisterField: (fieldKey: string, field: Field, value?: FieldValue) => void;
  FormChange: (values: FormValues, fields: FormFields) => void;
  UnregisterField: (fieldKey: string) => void;
}

export interface FormEnhancerProps extends FormStateProp, FormDispatchProp, FormHandlerProps {}

export interface FormProps extends React.HTMLProps<HTMLFormElement> {}
export interface FormEnhancedProps extends FormProps, Partial<FormEnhancerProps> {}

export interface FieldRegisterPayload {
  field: Field;
  fieldKey: string;
}
export interface FieldUnregisterPayload {
  fieldKey: string;
}
export interface FormChangePayload {
  values: FormValues;
  fields: FormFields;
}

export type DispatchPayload = FieldRegisterPayload | FormChangePayload | FieldUnregisterPayload;
