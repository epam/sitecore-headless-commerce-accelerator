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

import * as JSS from 'Foundation/ReactJss/client';
import { connect } from 'react-redux';
import { compose } from 'recompose';
import { bindActionCreators, Dispatch } from 'redux';

import * as Authentication from 'Feature/Account/client/Integration/Authentication';
import * as ShoppingCart from 'Feature/Checkout/client/Integration/ShoppingCart';

import { UserNavigationComponent } from './Component';
import { AppState } from './models';

const mapStateToProps = (state: AppState) => {
  const authProcess = Authentication.authenticationProcess(state);
  const cartData = ShoppingCart.shoppingCartData(state);
  const cartQuantity = cartData && cartData.cartLines ? cartData.cartLines.length : 0;
  const commerceUser = JSS.sitecoreContext(state).commerceUser;
  const returnUrl = JSS.routingLocationPathname(state);

  return {
    authProcess,
    cartQuantity,
    commerceUser,
    returnUrl,
  };
};

const mapDispatchToProps = (dispatch: Dispatch) =>
  bindActionCreators(
    {
      StartAuth: Authentication.StartAuthentication,
    },
    dispatch,
  );

export const UserNavigation = compose(
  JSS.rendering,
  connect(mapStateToProps, mapDispatchToProps),
)(UserNavigationComponent);
