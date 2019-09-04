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

import { RouterProps } from 'react-router';

import { AuthProcessState, GlobalAuthenticationState } from 'Feature/Account/client/Integration/Authentication';
import * as Commerce from 'Foundation/Commerce/client';
import * as Jss from 'Foundation/ReactJss/client';

import { GlobalShoppingCartState } from 'Feature/Checkout/client/Integration/ShoppingCart';

export interface CommerceCategory extends Jss.BaseDataSourceItem {
  name: string;
}

export interface MenuItem extends Jss.BaseDataSourceItem {
  title: Jss.GraphQLField<Jss.TextField>;
  image:  Jss.GraphQLField<Jss.ImageField>;
  commerceCategories: {
    items: [CommerceCategory]
  };
}

export interface GlobalHeaderDataSource extends Jss.BaseDataSourceItem {
  data: {
    datasource: {
      menuItems: {
        items: [MenuItem]
      };
    }
  };
}

export interface GlobalHeaderOwnProps extends RouterProps, Jss.Rendering<GlobalHeaderDataSource> {}

export interface GlobalHeaderStateProps {
  commerceUser: Commerce.CommerceUserModel;
  returnUrl: string;
  authProcess: AuthProcessState;
  cartQuantity: number;
}
export interface GlobalHeaderDispatchProps {
  StartAuthentication: (email: string, password: string) => void;
  ChangeRoute: (newRoute: string) => void;
  LoadCart: () => void;
}

export interface GlobalHeaderProps extends GlobalHeaderOwnProps, GlobalHeaderStateProps, GlobalHeaderDispatchProps {}

export interface GlobalHeaderState extends Jss.SafePureComponentState {}

export interface AppState
  extends GlobalShoppingCartState,
    GlobalAuthenticationState,
    Jss.SitecoreState<Commerce.CommerceUserContext> {}
