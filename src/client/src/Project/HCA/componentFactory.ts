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

import * as Account from 'Feature/Account';
import * as Catalog from 'Feature/Catalog';
import * as Checkout from 'Feature/Checkout';
import { NotFound2 } from 'Feature/Errors/NotFound2';
import * as Navigation from 'Feature/Navigation';
import * as PageContent from 'Feature/PageContent';
import * as StoreLocator2 from 'Feature/StoreLocator2';
import * as Scaffolding from 'Project/HCA/App/components/Scaffolding/components';

const components = new Map<string, any>();

// add components to the map
// components.set('Component', <Component />);

// Store Locator
components.set('Store Locator', StoreLocator2.StoreLocator);

// Page Scaffolding
components.set('One Column', Scaffolding.OneColumn);
components.set('Two Column', Scaffolding.TwoColumn);
components.set('Two Column Row', Scaffolding.TwoColumnRow);
components.set('Three Column Row', Scaffolding.ThreeColumnRow);

// Home renderings
components.set('Carousel Banner', PageContent.CarouselBanner);
components.set('Recommended Products', PageContent.RecommendedProducts2);

// Header renderings
components.set('Header', Navigation.Header2);
components.set('Header Content', Navigation.HeaderContent2);
components.set('Logo', Navigation.Logo2);
components.set('Navigation Menu', Navigation.NavigationMenu2);
components.set('Quick Navigation', Navigation.QuickNavigation2);
components.set('User Navigation', Navigation.UserNavigation2);

// Footer renderings
components.set('Footer', Navigation.GlobalFooter2);
components.set('Copyright', Navigation.Copyright2);
components.set('Footer Links', Navigation.FooterLinks2);
components.set('Social Networks Links', Navigation.SocialNetworksLinks2);

// Catalog renderings
components.set('Product Overview', Catalog.ProductOverview2);
components.set('Product Information', Catalog.ProductInformation2);
components.set('Product List', Catalog.ProductList2);
components.set('Product Filters', Catalog.ProductFilters2);
components.set('Product Variants', Catalog.ProductVariants2);
components.set('Products Search', Catalog.ProductsSearch);

// Checkout renderings
components.set('Add To Cart', Checkout.AddToCart);
components.set('Shopping Cart', Checkout.Cart2);
components.set('Billing', Checkout.Billing2);
components.set('Shipping', Checkout.Shipping2);
components.set('Order Summary', Checkout.OrderInformation2);
components.set('Order Confirmation', Checkout.OrderConfirmation2);
components.set('Order History', Checkout.OrderHistory);
components.set('Payment', Checkout.Payment2);
components.set('Checkout Navigation', Checkout.CheckoutNavigation2);
components.set('Go To Checkout', Checkout.GoToCheckout);

// Account renderings
components.set('Change Password Form', Account.ChangePassword);
components.set('Address Manager', Account.AddressManager);
components.set('Change User Information Form', Account.ChangeUserInformationForm);
components.set('Wishlist', Account.Wishlist2);
components.set('Add To Wishlist', Account.AddToWishlist2);
components.set('Login Register', Account.LoginRegister);

// Not Found
components.set('Not Found', NotFound2);

export default (componentName: string) => components.get(componentName);
