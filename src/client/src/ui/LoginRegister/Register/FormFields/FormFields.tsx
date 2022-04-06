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

import React, { FC, useCallback, useContext, useEffect, useState } from 'react';

import { useDispatch, useSelector } from 'react-redux';
import { get, isEmpty } from 'lodash';

import { routingLocationPathname as routingLocationPathnameSelector } from 'Foundation/ReactJss';
import FormContext from 'Foundation/ReactJss/Form/FormContext';
import {
  AccountValidation as onAccountValidation,
  CreateAccount as onCreateAccount,
  ResetValidation as onResetValidation,
  signUp as signUpSelector,
} from 'services/account';
import { LoadingStatus } from 'models';

import { Checkbox, Input, Submit } from 'Foundation/ReactJss/Form';
import { NavigationLink } from 'ui/NavigationLink';

import { Icon } from 'components';

import { FORM_FIELDS } from '../constants';
import { validate } from '../utils';

import { RegisterOptions } from '../RegisterOptions';
import { PasswordField } from '../PasswordField';

import { cnRegister } from '../cn';
import '../Register.scss';

export const FormFields: FC = () => {
  const dispatch = useDispatch();

  const returnUrl = useSelector(routingLocationPathnameSelector);
  const signUpState = useSelector(signUpSelector);
  const { accountValidation, create: createAccount } = signUpState;
  const loading = createAccount.status === LoadingStatus.Loading;

  const formContext = useContext(FormContext);
  const formValues = formContext.form.values;

  const [stateFormFields, setStateFormFields] = useState({});
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  const handleToggleShowPassword = useCallback(() => {
    setShowPassword((state) => !state);
  }, [showPassword]);

  const handleToggleShowConfirmPassword = useCallback(() => {
    setShowConfirmPassword((state) => !state);
  }, [showConfirmPassword]);

  useEffect(() => {
    const { email, inUse, status, errorMessage, invalid } = accountValidation;

    if (status === LoadingStatus.Loaded && (inUse || invalid) && email === formValues[FORM_FIELDS.EMAIL]) {
      setStateFormFields((value) => ({
        ...value,
        email: {
          hasError: true,
          message: inUse ? 'Email is already in use' : errorMessage,
        },
      }));
    }
  }, [accountValidation, formValues[FORM_FIELDS.EMAIL]]);

  useEffect(() => {
    if (
      accountValidation.status === LoadingStatus.Loaded &&
      !accountValidation.inUse &&
      !accountValidation.invalid &&
      isEmpty(stateFormFields) &&
      !isEmpty(formValues)
    ) {
      dispatch(
        onCreateAccount(
          {
            email: formValues[FORM_FIELDS.EMAIL] as string,
            firstName: formValues[FORM_FIELDS.FIRST_NAME] as string,
            lastName: formValues[FORM_FIELDS.LAST_NAME] as string,
            password: formValues[FORM_FIELDS.PASSWORD] as string,
            termsAndPolicyAgreement: formValues[FORM_FIELDS.TERMS_CONDITIONS] as boolean,
          },
          returnUrl,
        ),
      );
    }
  }, [accountValidation.status]);

  useEffect(() => {
    dispatch(onResetValidation());
  }, []);

  const handleFormSubmit = useCallback(() => {
    if (accountValidation.status === LoadingStatus.Loaded) {
      dispatch(onResetValidation());
    }

    const formFields = validate(formValues);

    if (!get(formFields, ['email'])) {
      const email = formValues[FORM_FIELDS.EMAIL] as string;

      dispatch(onAccountValidation(email));
    }

    setStateFormFields(formFields);
  }, [formValues, setStateFormFields]);

  const formDisabled =
    createAccount.status === LoadingStatus.Loading ||
    accountValidation.status === LoadingStatus.Loading ||
    createAccount.status === LoadingStatus.Loaded;

  const handlerFocusField = (field: string) => {
    setStateFormFields((value) => ({
      ...value,
      [field]: {
        hasError: false,
      },
    }));
  };

  return (
    <>
      <RegisterOptions />
      <h5 className={cnRegister('Divider')}>
        <span>or</span>
      </h5>
      <div className={cnRegister('FormField')}>
        <label htmlFor="first-name">First name</label>
        <Input
          id="first-name"
          name={FORM_FIELDS.FIRST_NAME}
          disabled={formDisabled}
          fullWidth={true}
          error={get(stateFormFields, ['firstName', 'hasError'], false)}
          helperText={get(stateFormFields, ['firstName', 'message'])}
          handlerFocusField={() => handlerFocusField('firstName')}
        />
      </div>
      <div className={cnRegister('FormField')}>
        <label htmlFor="last-name">Last name</label>
        <Input
          id="last-name"
          name={FORM_FIELDS.LAST_NAME}
          disabled={formDisabled}
          fullWidth={true}
          error={get(stateFormFields, ['lastName', 'hasError'], false)}
          helperText={get(stateFormFields, ['lastName', 'message'])}
          handlerFocusField={() => handlerFocusField('lastName')}
        />
      </div>
      <div className={cnRegister('FormField')}>
        <label htmlFor="email-address">Email address</label>
        <Input
          id="email-address"
          name={FORM_FIELDS.EMAIL}
          disabled={formDisabled}
          fullWidth={true}
          error={get(stateFormFields, ['email', 'hasError'], false)}
          helperText={get(stateFormFields, ['email', 'message'])}
          handlerFocusField={() => handlerFocusField('email')}
        />
      </div>
      <div className={cnRegister('FormField')}>
        <PasswordField
          label="Password"
          id="password"
          name={FORM_FIELDS.PASSWORD}
          disabled={formDisabled}
          showPassword={showPassword}
          error={get(stateFormFields, ['password', 'hasError'], false)}
          helperText={get(stateFormFields, ['password', 'message'])}
          onClickAdornment={handleToggleShowPassword}
          handlerFocusField={() => handlerFocusField('password')}
        />
      </div>
      <div className={cnRegister('FormField')}>
        <PasswordField
          label="Confirm new password"
          id="confirm-password"
          name={FORM_FIELDS.CONFIRM_PASSWORD}
          disabled={formDisabled}
          showPassword={showConfirmPassword}
          error={get(stateFormFields, ['confirmPassword', 'hasError'], false)}
          helperText={get(stateFormFields, ['confirmPassword', 'message'])}
          onClickAdornment={handleToggleShowConfirmPassword}
          handlerFocusField={() => handlerFocusField('confirmPassword')}
        />
      </div>
      <div className={cnRegister('Actions')}>
        <Submit
          className={cnRegister('Button')}
          buttonTheme="default"
          buttonSize="m"
          disabled={formDisabled}
          onSubmitHandler={handleFormSubmit}
        >
          {(loading || formDisabled) && <Icon icon="icon-spinner-solid" />}
          <span>Register</span>
        </Submit>
        <div className={cnRegister('TermsAndConditions')}>
          <label className={cnRegister('TermsAndConditionsLabel')}>
            <Checkbox
              id="terms-and-conditions"
              name={FORM_FIELDS.TERMS_CONDITIONS}
              checked={get(formValues, [FORM_FIELDS.TERMS_CONDITIONS], false) as boolean}
              error={get(stateFormFields, ['termsConditions', 'hasError'], false)}
              disabled={formDisabled}
            />
            <label htmlFor="terms-and-conditions" className={cnRegister('TermsAndConditionsText')}>
              I have read and agree to the <NavigationLink to="/">Terms of Use</NavigationLink> and{' '}
              <NavigationLink to="/">Customer Privacy Policy.</NavigationLink>
            </label>
          </label>
        </div>
      </div>
    </>
  );
};
