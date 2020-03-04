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

import { withExperienceEditorChromes } from '@sitecore-jss/sitecore-jss-react';
import { connect } from 'react-redux';
import { bindActionCreators, Dispatch } from 'redux';

import * as Jss from 'Foundation/ReactJss/client';
import { ChangeRoute } from 'Foundation/ReactJss/client/SitecoreContext';

import * as Authentication from 'Feature/Account/client/Integration/Authentication';
import * as ShoppingCart from 'Feature/Checkout/client/Integration/ShoppingCart';

import GlobalHeaderComponent from './Component';

import { AppState, GlobalHeaderDispatchProps, GlobalHeaderOwnProps, GlobalHeaderStateProps } from './models';

const mapStateToProps = (state: AppState, ownProps: GlobalHeaderOwnProps) => {
  const authProcess = Authentication.authenticationProcess(state);
  const cartData = ShoppingCart.shoppingCartData(state);
  const cartQuantity = cartData && cartData.cartLines ? cartData.cartLines.length : 0;
  const sitecoreContext = Jss.sitecoreContext(state);

  return {
    authProcess,
    cartQuantity,
    commerceUser: sitecoreContext.commerceUser,
    returnUrl: ownProps.history.location.pathname,
  };
};
const mapDispatchToProps = (dispatch: Dispatch) =>
  bindActionCreators(
    {
      ChangeRoute,
      LoadCart: ShoppingCart.LoadCart,
      StartAuthentication: Authentication.StartAuthentication,
    },
    dispatch
  );

const connectedToStore = connect<GlobalHeaderStateProps, GlobalHeaderDispatchProps, GlobalHeaderOwnProps>(
  mapStateToProps,
  mapDispatchToProps
)(GlobalHeaderComponent);

export default withExperienceEditorChromes(connectedToStore);
