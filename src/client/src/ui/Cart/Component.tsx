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

import * as React from 'react';

import * as Jss from 'Foundation/ReactJss';

import { Button, Icon, Spinner, Dialog } from 'components';

import { NavigationLink } from 'ui/NavigationLink';

import { CartSummary, OrderSummary } from './components';
import { CartProps, CartState } from './models';

import './styles.scss';

export default class Cart extends Jss.SafePureComponent<CartProps, CartState> {
  public constructor(props: CartProps) {
    super(props);
    this.state = {
      isFirstInitLoadPage: true,
      dialogOpen: false,
    };
  }

  public componentDidMount() {
    this.props.LoadCart();
  }

  public componentDidUpdate(prevProps: any) {
    const { isLoading } = this.props;
    const { isFirstInitLoadPage } = this.state;
    if (prevProps.isLoading !== isLoading && isLoading === false && isFirstInitLoadPage === true) {
      this.setState({ isFirstInitLoadPage: false });
    }
  }

  public handleToggleDialogClick() {
    this.setState({ dialogOpen: !this.state.dialogOpen });
  }

  public safeRender() {
    const { shoppingCartData, sitecoreContext } = this.props;
    const { isFirstInitLoadPage } = this.state;

    if (
      !isFirstInitLoadPage &&
      shoppingCartData &&
      shoppingCartData.cartLines &&
      shoppingCartData.cartLines.length === 0
    ) {
      return (
        <div className="empty-cart">
          <Icon icon="icon-cart" />
          <div className="empty-cart-not-found-text">No items found in cart</div>
          <div className="empty-cart-button">
            <NavigationLink to="/search?q=">Shop Now</NavigationLink>
          </div>
        </div>
      );
    }

    if (isFirstInitLoadPage) {
      return <Spinner data-autotests="loading_spinner" />;
    }

    return (
      <div className="CartWrap">
        <div className="row">
          <div className="action_container">
            <NavigationLink to="/search?q=">Continue Shopping</NavigationLink>
          </div>
          <div className="col-md-8 col-xs-12">
            <header className="title-cart-item">Cart</header>
            <div className="DeleteAllWrap">
              <Button className="DeleteALL" buttonTheme="link" onClick={() => this.handleToggleDialogClick()}>
                Delete All
              </Button>
            </div>
          </div>
        </div>
        <div className="col-md-8 col-xs-12 CartSummaryWrap">
          {shoppingCartData && shoppingCartData.cartLines && (
            <CartSummary
              cartLines={shoppingCartData.cartLines}
              productColors={sitecoreContext.productColors}
              fallbackImageUrl={sitecoreContext.fallbackImageUrl}
            />
          )}
        </div>
        <div className="col-md-4 col-xs-12 OrderSummaryWrap">
          {shoppingCartData && shoppingCartData.price && (
            <OrderSummary
              price={shoppingCartData.price}
              rendering={this.props.rendering}
              countries={this.props.fields.countries}
            />
          )}
        </div>
        <Dialog isOpen={this.state.dialogOpen} toggleDialog={() => this.handleToggleDialogClick()}>
          <div className="CartDialog ContentDialog">
            <div className="CartDialog HeaderDialog">
              <h4 className="CartDialog TitleDialog">Are you sure you want to remove all products from the cart?</h4>
            </div>
            <form>
              <div className="CartDialog SubmitContainerDialog">
                <Button
                  className="CartDialog ButtonDialog"
                  buttonTheme="default"
                  onClick={() => {
                    this.handleClearCart();
                  }}
                >
                  Clear Cart
                </Button>
                <Button
                  className="CartDialog ButtonDialog"
                  buttonTheme="default"
                  onClick={() => this.handleToggleDialogClick()}
                >
                  CANCEL
                </Button>
              </div>
            </form>
          </div>
        </Dialog>
      </div>
    );
  }

  private handleClearCart = () => {
    const { CleanCart } = this.props;
    CleanCart();
    this.handleToggleDialogClick();
  };
}
