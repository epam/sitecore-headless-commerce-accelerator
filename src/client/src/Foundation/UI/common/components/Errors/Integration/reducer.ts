
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

import { Action } from 'Foundation/Integration';
import { GlobalErrorState } from './models';

export const ADD_ERROR = `ADD`;
export const REMOVE_ERROR = `REMOVE`;

// Reducer
export const GlobalErrorInitialState: any = {
    errors: [
        // { id: 1, message: '' }
    ],
};

export default (state: GlobalErrorState = {...GlobalErrorInitialState}, action: Action) => {
    switch (action.type) {
        case ADD_ERROR: {
            const newErrId = state.errors.length ? state.errors[state.errors.length - 1].id + 1 : 1;
            return {
                errors: [
                    ...state.errors,
                    {
                        id: newErrId,
                        message: action.payload || null
                    },
                ],
            };
        }

        case REMOVE_ERROR: {
            if (action.payload.hasOwnProperty('id')) {
                const removedId = action.payload['id'];
                return {
                    errors: state.errors.filter((err: any) => err.id !== removedId),
                };
            }
            return state;
        }
        default:
            return state;
    }
};

// Selector
export const selectLastError = (state: GlobalErrorState) => state.errors[0];