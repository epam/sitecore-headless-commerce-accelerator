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

import { combineReducers } from 'redux';

import { Action, LoadingStatus } from 'Foundation/Integration/client';

import { reducerActionTypes } from './constants';
import {
  AccountState,
  AddressPayload,
  ChangePasswordState,
  SavedAddressListState,
  SignUpState,
  UpdateAccountState,
} from './models';

export const initialSignUpState: SignUpState = {
  accountValidation: {
    email: '',
    inUse: false,
    invalid: false,
    status: LoadingStatus.NotLoaded,
  },
  create: {
    status: LoadingStatus.NotLoaded,
  },
};

export const signUpReducer = (state: SignUpState = { ...initialSignUpState }, action: Action) => {
  switch (action.type) {
    case reducerActionTypes.CREATE_FAILURE:
    case reducerActionTypes.CREATE_REQUEST:
    case reducerActionTypes.CREATE_SUCCESS: {
      const { create } = state;
      return {
        ...state,
        create: {
          ...create,
          ...action.payload,
        },
      };
    }

    case reducerActionTypes.ACCOUNT_VALIDATION_FAILURE:
    case reducerActionTypes.ACCOUNT_VALIDATION_REQUEST:
    case reducerActionTypes.ACCOUNT_VALIDATION_SUCCESS: {
      const { accountValidation } = state;

      return {
        ...state,
        accountValidation: {
          ...accountValidation,
          ...action.payload,
        },
      };
    }
    case reducerActionTypes.RESET_EMAIL_VALIDATION: {
      return {
        ...state,
        ...initialSignUpState,
      };
    }
    default: {
      return state;
    }
  }
};

export const initialChangePasswordState: ChangePasswordState = {
  status: LoadingStatus.NotLoaded,
};

export const changePasswordReducer = (state: ChangePasswordState = initialChangePasswordState, action: Action) => {
  switch (action.type) {
    case reducerActionTypes.CHANGE_PASSWORD_FAILURE:
    case reducerActionTypes.CHANGE_PASSWORD_REQUEST:
    case reducerActionTypes.CHANGE_PASSWORD_SUCCESS: {
      return {
        ...state,
        ...action.payload,
      };
    }
    default: {
      return state;
    }
  }
};

export const addressInitialState: SavedAddressListState = {
  items: {},
  status: LoadingStatus.NotLoaded,
};

export const saveAddressListReducer = (state: SavedAddressListState = { ...addressInitialState }, action: Action) => {
  switch (action.type) {
    case reducerActionTypes.ADDRESS_ADD_FAILURE:
    case reducerActionTypes.ADDRESS_ADD_REQUEST:
    case reducerActionTypes.ADDRESS_GET_LIST_FAILURE:
    case reducerActionTypes.ADDRESS_GET_LIST_REQUEST:
    case reducerActionTypes.ADDRESS_GET_LIST_SUCCESS:
    case reducerActionTypes.ADDRESS_REMOVE_FAILURE:
    case reducerActionTypes.ADDRESS_REMOVE_REQUEST:
    case reducerActionTypes.ADDRESS_REMOVE_SUCCESS:
    case reducerActionTypes.ADDRESS_UPDATE_FAILURE:
    case reducerActionTypes.ADDRESS_UPDATE_REQUEST:
    case reducerActionTypes.ADDRESS_UPDATE_SUCCESS: {
      return {
        ...state,
        ...action.payload,
      };
    }
    case reducerActionTypes.ADDRESS_ADD_SUCCESS: {
      const payload = action.payload as AddressPayload;

      return {
        items: {
          ...state.items,
          ...payload.items,
        },
        status: payload.status,
      };
    }
    default: {
      return {
        ...state,
      };
    }
  }
};

export const updateAccountInitialState: UpdateAccountState = {
  status: LoadingStatus.NotLoaded,
};

export const updateAccountReducer = (state: UpdateAccountState = { ...updateAccountInitialState }, action: Action) => {
  switch (action.type) {
    case reducerActionTypes.UPDATE_FAILURE:
    case reducerActionTypes.UPDATE_REQUEST:
    case reducerActionTypes.UPDATE_SUCCESS: {
      return {
        ...state,
        ...action.payload,
      };
    }
    default: {
      return { ...state };
    }
  }
};

export default combineReducers<AccountState>({
  changePassword: changePasswordReducer,
  savedAddressList: saveAddressListReducer,
  signUp: signUpReducer,
  update: updateAccountReducer,
});
