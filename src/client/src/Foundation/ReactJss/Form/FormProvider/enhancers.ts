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

import { compose, withHandlers, withReducer } from 'recompose';

import * as handlers from './handlers';
import { FormAction, FormDispatchProp, FormEnhancedProps, FormEnhancerProps, FormProps, FormStateProp } from './models';

import { FormState } from '../models';
import formReducer, { initialState } from './reducer';

export const withFormState = withReducer<
  FormEnhancerProps,
  FormState,
  FormAction,
  keyof FormStateProp,
  keyof FormDispatchProp
>('formState', 'dispatch', formReducer, initialState);

export const withFormHandlers = withHandlers({
  FormChange: handlers.FormChange,
  RegisterField: handlers.RegisterField,
  UnregisterField: handlers.UnregisterField,
});

export const formEnhancer = compose<FormEnhancedProps, FormProps>(withFormState, withFormHandlers);
