//    Copyright 2022 EPAM Systems, Inc.
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

import { FormValues } from 'Foundation/ReactJss/Form';

export interface ChangePasswordDialogProps {
  dialogOpen: boolean;
  showOldPassword: boolean;
  showNewPassword: boolean;
  showConfirmNewPassword: boolean;
  isLoading: boolean;
  stateFormFields: object;
  handleToggleDialogClick: () => void;
  handlerFocusField: (field: string) => void;
  handleToggleShowOldPassword: () => void;
  handleToggleShowNewPassword: () => void;
  handleToggleShowConfirmNewPassword: () => void;
  handleResetPasswordLinkClick: () => void;
  handleChangePasswordSubmit: (formValues: FormValues) => void;
}
