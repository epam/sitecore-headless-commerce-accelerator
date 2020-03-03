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

export const signIn = (email: string, password: string) => {
  const form = document.createElement('form');
  form.style.visibility = 'hidden';
  form.method = 'POST';
  form.action = `/apix/client/commerce/auth/signIn?returnUrl=${location.pathname}`;

  const emailInput = document.createElement('input');
  emailInput.name = 'email';
  emailInput.value = email;
  form.appendChild(emailInput);

  const passwordInput = document.createElement('input');
  // tslint:disable-next-line:no-hardcoded-credentials
  passwordInput.name = 'password';
  passwordInput.value = password;
  form.appendChild(passwordInput);

  document.body.appendChild(form);
  form.submit();
};
