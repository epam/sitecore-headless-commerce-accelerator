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

import { Field, FormFields, FormValues } from './../models';

import { actionTypes } from './constants';

export const FormChange = (values: FormValues, fields: FormFields) => ({
  payload: {
    fields,
    values,
  },
  type: actionTypes.FORM_CHANGE,
});

export const UpdateStatus = () => ({
  type: actionTypes.UPDATE_STATUS,
});

export const RegisterField = (fieldKey: string, field: Field) => ({
  payload: {
    field,
    fieldKey,
  },
  type: actionTypes.FIELD_REGISTER,
});

export const UnregisterField = (fieldKey: string) => ({
  payload: {
    fieldKey,
  },
  type: actionTypes.FIELD_UNREGISTER,
});
