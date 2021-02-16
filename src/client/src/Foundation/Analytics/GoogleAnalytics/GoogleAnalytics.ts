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

import ReactGA from 'react-ga';

import { AnalyticsEventArgs, CartLine, Product, ProductDetailsView, ProductImpression } from './models';

class GoogleAnalytics {
  public initialize(debug: boolean = false): void {
    ReactGA.initialize(process.env.TRACKING_ID, { debug });

    ReactGA.plugin.require('ec');
  }

  public viewProductDetails(productDetailsView: ProductDetailsView, pageUri: string) {
    ReactGA.plugin.execute('ec', 'addProduct', productDetailsView);
    ReactGA.plugin.execute('ec', 'setAction', 'detail');
    ReactGA.pageview(pageUri);
  }

  public addProductImpression(productImpression: ProductImpression): void {
    ReactGA.plugin.execute('ec', 'addImpression', { ...productImpression });
    ReactGA.pageview(productImpression.list);
  }

  public raiseEmailValidatedEvent(): void {
    this.event({ action: 'Email validated', category: 'Account' });
  }

  public raiseAccountCreatedEvent(): void {
    this.event({ action: 'Account created', category: 'Account' });
  }

  public raiseAccountUpdatedEvent(): void {
    this.event({ action: 'Account updated', category: 'Account' });
  }

  public raisePasswordChangedEvent(): void {
    this.event({ action: 'Password changed', category: 'Account' });
  }

  public raiseAddressAddedEvent(): void {
    this.event({ action: 'Address added', category: 'Account' });
  }

  public raiseAddressUpdatedEvent(): void {
    this.event({ action: 'Address updated', category: 'Account' });
  }

  public raiseAddressRemovedEvent(): void {
    this.event({ action: 'Address removed', category: 'Account' });
  }

  public raiseLoginEvent(): void {
    this.event({ action: 'Login', category: 'Authentification' });
  }

  public raiseLogoutEvent(): void {
    this.event({ action: 'Logout', category: 'Authentification' });
  }

  public raiseCartLineAddedEvent(cartLine: CartLine): void {
    ReactGA.plugin.execute('ec', 'addProduct', cartLine);
    ReactGA.plugin.execute('ec', 'setAction', 'add', cartLine);
    this.event({ action: 'Click', category: 'Cart', label: 'CartLine added' });
  }

  public raiseCartLineUpdatedEvent(cartLine: CartLine): void {
    this.event({ action: 'Click', category: 'Cart', label: 'CartLine updated' });
  }

  public raiseCartLineRemovedEvent(cartLine: CartLine): void {
    ReactGA.plugin.execute('ec', 'addProduct', cartLine);
    ReactGA.plugin.execute('ec', 'setAction', 'remove');
    this.event({ action: 'Click', category: 'Cart', label: 'CartLine removed' });
  }

  public raiseProductClickedEvent(product: Product): void {
    ReactGA.plugin.execute('ec', 'addProduct', product);
    ReactGA.plugin.execute('ec', 'setAction', 'click', {list: product.list || 'not set'});
    this.event({ action: 'Click', category: 'ProductList', label: 'Product clicked'});
  }

  private event(args: AnalyticsEventArgs): void {
    ReactGA.event(args);
  }
}

export const googleAnalytics = new GoogleAnalytics();
