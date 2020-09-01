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

import { connect } from 'react-redux';
import { compose } from 'recompose';
import { bindActionCreators, Dispatch } from 'redux';

import * as JSS from 'Foundation/ReactJss';

import { GetWishlist, selector } from '../Integration/Wishlist';

import { WishlistComponent } from './Component';
import { AppState, WishlistDispatchProps, WishlistStoreProps } from './models';

const mapStateToProps = (state: AppState): WishlistStoreProps => ({
  items: selector.wishlist(state),
});

const mapDispatchToProps = (dispatch: Dispatch): WishlistDispatchProps =>
  bindActionCreators(
    {
      GetWishlist,
    },
    dispatch,
  );

export const Wishlist = compose(JSS.rendering, connect(mapStateToProps, mapDispatchToProps))(WishlistComponent);
