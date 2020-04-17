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

import { Field, FormFields, FormValues } from '../models';
import * as actions from './actions';
import { DispatchPayload, FormDispatchProp } from './models';

export const FormChange = ({ dispatch }: FormDispatchProp<DispatchPayload>) => (
  values: FormValues,
  fields: FormFields
) => {
  dispatch(actions.FormChange(values, fields), () => {
    dispatch(actions.UpdateStatus());
  });
};

export const RegisterField = ({ dispatch }: FormDispatchProp<DispatchPayload>) => (fieldKey: string, field: Field) => {
  dispatch(actions.RegisterField(fieldKey, field), () => {
    dispatch(actions.UpdateStatus());
  });
};

export const UnregisterField = ({ dispatch }: FormDispatchProp<DispatchPayload>) => (fieldKey: string) => {
  dispatch(actions.UnregisterField(fieldKey), () => {
    dispatch(actions.UpdateStatus());
  });
};
