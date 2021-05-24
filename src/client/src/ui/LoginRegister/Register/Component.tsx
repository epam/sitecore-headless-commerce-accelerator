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

import React, { FC, FormEvent, useCallback, useEffect, useState } from 'react';

import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

import { Button } from 'components';

import { LoadingStatus } from 'Foundation/Integration';
import { Checkbox, Form, FormValues, Input, Submit } from 'Foundation/ReactJss/Form';
import { NavigationLink } from 'Foundation/UI';
import { validateEmail as validateEmailUtils } from 'Foundation/utils/validation';

import { FORM_FIELDS } from './constants';
import { SignUpProps } from './models';

import { cnRegister } from './cn';
import './styles.scss';

// tslint:disable-next-line:no-big-function
export const RegisterComponent: FC<SignUpProps> = ({
  accountValidation,
  returnUrl,
  AccountValidation,
  loading,
  createAccount,
  CreateAccount,
  ResetValidation,
  // tslint:disable-next-line:no-big-function
}) => {
  const [formValues, setFormValues] = useState({});

  const [isConfirmPasswordEmpty, setIsConfirmPasswordEmpty] = useState(false);
  const [isEmailEmpty, setIsEmailEmpty] = useState(false);
  const [isEmailValid, setIsEmailValid] = useState(true);
  const [isFirstNameEmpty, setIsFirstNameEmpty] = useState(false);
  const [isLastNameEmpty, setIsLastNameEmpty] = useState(false);
  const [isPasswordEmpty, setIsPasswordEmpty] = useState(false);
  const [isPasswordsValid, setIsPasswordsValid] = useState(true);
  const [termsAndConditionsAccepted, setTermsAndConditionsAccepted] = useState(false);
  const [termsAndConditionsValid, setTermsAndConditionsValid] = useState(true);

  const [showPasswordValue, setShowPasswordValue] = useState(false);
  const [showConfirmPasswordValue, setShowConfirmPasswordValue] = useState(false);

  const toggleShowPasswordValue = useCallback(() => {
    setShowPasswordValue((value) => !value);
  }, [setShowPasswordValue, showPasswordValue]);

  const toggleShowConfirmPasswordValue = useCallback(() => {
    setShowConfirmPasswordValue((value) => !value);
  }, [setShowConfirmPasswordValue, showConfirmPasswordValue]);

  const toggleTermsAndConditions = useCallback(
    (e: FormEvent<HTMLInputElement>) => {
      setTermsAndConditionsAccepted(e.currentTarget.checked);
      setTermsAndConditionsValid(e.currentTarget.checked);
    },
    [setTermsAndConditionsAccepted],
  );

  useEffect(() => {
    return () => {
      ResetValidation();
    };
  }, []);

  const checkEmailValidation = () => {
    if (
      accountValidation.inUse &&
      accountValidation.status !== LoadingStatus.Loading &&
      !isEmailEmpty &&
      isEmailValid
    ) {
      return 'Email is already in use';
    }

    if (isEmailEmpty) {
      return 'Email field is required';
    }

    if (!isEmailValid) {
      return 'Email is invalid';
    }

    return '';
  };

  const passwordValidator = (form: FormValues) => {
    if (form[FORM_FIELDS.PASSWORD] === form[FORM_FIELDS.CONFIRM_PASSWORD]) {
      setIsPasswordsValid(true);
      return true;
    } else {
      setIsPasswordsValid(false);
      return false;
    }
  };

  const validateFirstName = (form: FormValues) => {
    const fName = form[FORM_FIELDS.FIRST_NAME] as string;
    if (form && fName) {
      setIsFirstNameEmpty(false);
      return true;
    } else {
      setIsFirstNameEmpty(true);
      return false;
    }
  };

  const validateLastName = (form: FormValues) => {
    const lName = form[FORM_FIELDS.LAST_NAME] as string;
    if (form && lName) {
      setIsLastNameEmpty(false);
      return true;
    } else {
      setIsLastNameEmpty(true);
      return false;
    }
  };

  const validateEmail = (form: FormValues) => {
    const email = form[FORM_FIELDS.EMAIL] as string;

    if (email === '') {
      setIsEmailEmpty(true);
      setIsEmailValid(false);
      return false;
    }

    const isValid = validateEmailUtils(email);

    if (isValid) {
      setIsEmailEmpty(false);
      setIsEmailValid(true);
      return true;
    }

    setIsEmailEmpty(false);
    setIsEmailValid(false);

    return false;
  };

  const validatePassword = (form: FormValues) => {
    const password = form[FORM_FIELDS.PASSWORD] as string;
    if (form && password) {
      setIsPasswordEmpty(false);
      return true;
    } else {
      setIsPasswordEmpty(true);
      return false;
    }
  };

  const validateConfirmPassword = (form: FormValues) => {
    const confirmPassword = form[FORM_FIELDS.CONFIRM_PASSWORD] as string;
    if (form && confirmPassword) {
      setIsConfirmPasswordEmpty(false);
      return true;
    } else {
      setIsConfirmPasswordEmpty(true);
      return false;
    }
  };

  const validate = (form: FormValues) => {
    const isFirstNameValid = validateFirstName(form);
    const isLastNameValid = validateLastName(form);
    const isPasswordValid = validatePassword(form);
    const isConfirmPasswordValid = validateConfirmPassword(form);
    let isPasswordsMatch = false;
    setTermsAndConditionsValid(termsAndConditionsAccepted);

    if (isPasswordValid && isConfirmPasswordValid) {
      isPasswordsMatch = passwordValidator(form);
    }

    if (isFirstNameValid && isLastNameValid && isPasswordsMatch) {
      return true;
    }

    return false;
  };

  useEffect(() => {
    const { status, inUse, invalid } = accountValidation;

    if (status === LoadingStatus.Loaded && !inUse && !invalid) {
      const isValid = validate(formValues);

      if (isValid) {
        const createAccountDto = {
          email: formValues[FORM_FIELDS.EMAIL] as string,
          firstName: formValues[FORM_FIELDS.FIRST_NAME] as string,
          lastName: formValues[FORM_FIELDS.LAST_NAME] as string,
          password: formValues[FORM_FIELDS.PASSWORD] as string,
        };

        CreateAccount(createAccountDto, returnUrl);
      }
    }
  }, [accountValidation]);

  const handleFormSubmit = (form: FormValues) => {
    validate(form);

    if (validateEmail(form) && termsAndConditionsAccepted) {
      const email = form[FORM_FIELDS.EMAIL] as string;
      setFormValues(form);
      AccountValidation(email);
    }
  };

  const formDisabled =
    createAccount.status === LoadingStatus.Loading || accountValidation.status === LoadingStatus.Loading;

  return (
    <Form className={cnRegister()}>
      <div className={cnRegister('SignUpOptions')}>
        <p>Sign up with</p>
        <div className={cnRegister('SocialButtons')}>
          <Button
            className={cnRegister('SocialButton', { first: true })}
            buttonTheme="transparentSlide"
            fullWidth={true}
          >
            Facebook
          </Button>
          <Button className={cnRegister('SocialButton')} buttonTheme="transparentSlide" fullWidth={true}>
            Google
          </Button>
          <Button
            className={cnRegister('SocialButton', { last: true })}
            buttonTheme="transparentSlide"
            fullWidth={true}
          >
            Apple
          </Button>
        </div>
        <p className={cnRegister('SignUpOptionsDescription')}>
          Signing up with social is super quick. Don't worry, we'd never share any of your data or post anything on your
          behalf
        </p>
      </div>
      <h5 className={cnRegister('Divider')}>
        <span>or</span>
      </h5>
      <div className={cnRegister('FormField')}>
        <label htmlFor="first-name">First name</label>
        <Input
          id="first-name"
          type="text"
          name={FORM_FIELDS.FIRST_NAME}
          disabled={formDisabled}
          fullWidth={true}
          error={isFirstNameEmpty}
          helperText={isFirstNameEmpty && 'First name field is required'}
        />
      </div>
      <div className={cnRegister('FormField')}>
        <label htmlFor="last-name">Last name</label>
        <Input
          id="last-name"
          type="text"
          name={FORM_FIELDS.LAST_NAME}
          disabled={formDisabled}
          fullWidth={true}
          error={isLastNameEmpty}
          helperText={isLastNameEmpty && 'Last name field is required'}
        />
      </div>
      <div className={cnRegister('FormField')}>
        <label htmlFor="email-address">Email address</label>
        <Input
          id="email-address"
          type="text"
          name={FORM_FIELDS.EMAIL}
          disabled={formDisabled}
          fullWidth={true}
          error={!isEmailValid || accountValidation.inUse || accountValidation.invalid || isEmailEmpty}
          helperText={
            (!isEmailValid || accountValidation.inUse || accountValidation.invalid || isEmailEmpty) &&
            checkEmailValidation()
          }
        />
      </div>
      <div className={cnRegister('FormField')}>
        <label htmlFor="password">Password</label>
        <Input
          id="password"
          type={showPasswordValue ? 'text' : 'password'}
          minLength={6}
          name={FORM_FIELDS.PASSWORD}
          disabled={formDisabled}
          fullWidth={true}
          error={isPasswordEmpty}
          helperText={(isPasswordEmpty && 'Password field is required') || 'Must be at least 6 characters'}
          adornment={
            <FontAwesomeIcon
              icon={showPasswordValue ? faEyeSlash : faEye}
              className={cnRegister('FaEyeIcon')}
              size="lg"
              onClick={toggleShowPasswordValue}
            />
          }
        />
      </div>
      <div className={cnRegister('FormField')}>
        <label htmlFor="confirm-password">Confirm new password</label>
        <Input
          id="confirm-password"
          type={showConfirmPasswordValue ? 'text' : 'password'}
          minLength={6}
          name={FORM_FIELDS.CONFIRM_PASSWORD}
          disabled={formDisabled}
          fullWidth={true}
          error={isConfirmPasswordEmpty || !isPasswordsValid}
          helperText={
            (isConfirmPasswordEmpty && 'Confirm password field is required') ||
            (!isPasswordsValid && 'Passwords do not match')
          }
          adornment={
            <FontAwesomeIcon
              icon={showConfirmPasswordValue ? faEyeSlash : faEye}
              className={cnRegister('FaEyeIcon')}
              size="lg"
              onClick={toggleShowConfirmPasswordValue}
            />
          }
        />
      </div>
      <div className={cnRegister('Actions')}>
        <Submit
          className={cnRegister('Button')}
          buttonTheme="grey"
          buttonSize="s"
          disabled={formDisabled}
          onSubmitHandler={(form) => handleFormSubmit(form)}
        >
          {(loading || formDisabled) && <i className="fa fa-spinner fa-spin" />}
          <span>Register</span>
        </Submit>
        <div className={cnRegister('TermsAndConditions')}>
          <label className={cnRegister('TermsAndConditionsLabel')}>
            <Checkbox
              id="terms-and-conditions"
              name={FORM_FIELDS.TERMS_CONDITIONS}
              checked={termsAndConditionsAccepted}
              onChange={toggleTermsAndConditions}
              error={!termsAndConditionsValid}
            />
            <label htmlFor="terms-and-conditions" className={cnRegister('TermsAndConditionsText')}>
              I have read and agree to the <NavigationLink to="/">Terms of Use</NavigationLink> and{' '}
              <NavigationLink to="/">Customer Privacy Policy.</NavigationLink>
            </label>
          </label>
        </div>
      </div>
    </Form>
  );
};
