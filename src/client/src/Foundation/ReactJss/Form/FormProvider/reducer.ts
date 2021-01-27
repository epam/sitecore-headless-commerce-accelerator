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

import { FormState } from './../models';
import { actionTypes } from './constants';
import { DispatchPayload, FieldRegisterPayload, FieldUnregisterPayload, FormAction, FormChangePayload } from './models';

export const initialState: FormState = {
  fields: {},
  status: {
    valid: false,
  },
  values: {},
};

export default (state: FormState = { ...initialState }, action: FormAction<DispatchPayload>) => {
  switch (action.type) {
    case actionTypes.FIELD_REGISTER: {
      const { fieldKey, field } = action.payload as FieldRegisterPayload;
      const { fields, values } = state;

      const mergedFields = {
        ...fields,
        [fieldKey]: field,
      };

      const { defaultValue } = field;

      const mergedValues = defaultValue
        ? {
            ...values,
            [fieldKey]: defaultValue,
          }
        : values;

      return {
        ...state,
        fields: {
          ...mergedFields,
        },
        values: {
          ...mergedValues,
        },
      };
    }

    case actionTypes.FIELD_UNREGISTER: {
      const { fieldKey } = action.payload as FieldUnregisterPayload;

      const { fields, values } = state;

      const newFields = {
        ...fields,
      };

      if (newFields[fieldKey]) {
        delete newFields[fieldKey];
      }

      const newValues = {
        ...values,
      };

      if (newValues[fieldKey]) {
        delete newValues[fieldKey];
      }

      return {
        ...state,
        fields: newFields,
        values: newValues,
      };
    }
    case actionTypes.UPDATE_STATUS: {
      const { fields } = state;
      const invalidFields = Object.keys(fields).filter((key) => !fields[key].validity.valid);
      return {
        ...state,
        status: {
          valid: invalidFields.length === 0,
        },
      };
    }

    case actionTypes.FORM_CHANGE: {
      const { values, fields } = action.payload as FormChangePayload;

      return {
        ...state,
        fields,
        values,
      };
    }
    default: {
      return state;
    }
  }
};
