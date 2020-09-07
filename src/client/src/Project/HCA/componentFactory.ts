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
import * as Navigation from 'Feature/Navigation';
import * as PageContent from 'Feature/PageContent';
import * as Scaffolding from 'Project/HCA/App/components/Scaffolding/components';

const components = new Map<string, any>();

// add components to the map
// components.set('Component', <Component />);

// Page Scaffolding
components.set('One Column', Scaffolding.OneColumn);
components.set('Two Column', Scaffolding.TwoColumn);
components.set('Two Column Row', Scaffolding.TwoColumnRow);
components.set('Three Column Row', Scaffolding.ThreeColumnRow);

// Home renderings
components.set('Recommended Products', PageContent.RecommendedProducts);
components.set('Recommended Products 2', PageContent.RecommendedProducts2);
components.set('Banner Grid', PageContent.BannerGrid);
components.set('Banner', PageContent.Banner);
components.set('Clear Banner', PageContent.ClearBanner);
components.set('Light Banner', PageContent.LightBanner);
components.set('Modern Banner', PageContent.ModernBanner);

components.set('Banner Grid 2', PageContent.BannerGrid2);
components.set('Banner 2', PageContent.Banner2);
components.set('Clear Banner 2', PageContent.ClearBanner2);
components.set('Light Banner 2', PageContent.LightBanner2);
components.set('Modern Banner 2', PageContent.ModernBanner2);

// Header renderings
components.set('Header', Navigation.Header);
components.set('Header Content', Navigation.HeaderContent);
components.set('Logo', Navigation.Logo);
components.set('Navigation Menu', Navigation.NavigationMenu);
components.set('Search Box', Navigation.NavigationSearch);
components.set('Quick Navigation', Navigation.QuickNavigation);
components.set('User Navigation', Navigation.UserNavigation);

// Header2 renderings
components.set('Header 2', Navigation.Header2);
components.set('Header Content 2', Navigation.HeaderContent2);
components.set('Logo 2', Navigation.Logo2);
components.set('Navigation Menu 2', Navigation.NavigationMenu2);
components.set('Search Box 2', Navigation.NavigationSearch2);
components.set('Quick Navigation 2', Navigation.QuickNavigation2);
components.set('User Navigation 2', Navigation.UserNavigation2);

// Footer renderings
components.set('Footer', Navigation.GlobalFooter);
components.set('Copyright', Navigation.Copyright);
components.set('Footer Links', Navigation.FooterLinks);
components.set('Social Networks Links', Navigation.SocialNetworksLinks);

// Footer2 renderings
components.set('Footer 2', Navigation.GlobalFooter2);
components.set('Copyright 2', Navigation.Copyright2);
components.set('Footer Links 2', Navigation.FooterLinks2);
components.set('Social Networks Links 2', Navigation.SocialNetworksLinks2);

// Catalog renderings
components.set('Find In Store', Catalog.FindInStore);
components.set('Product Overview', Catalog.ProductOverview);
components.set('Product Information', Catalog.ProductInformation);
components.set('Product List', Catalog.ProductList);
components.set('Product List 2', Catalog.ProductList2);
components.set('Product Filters', Catalog.ProductFilters);
components.set('Product Variants', Catalog.ProductVariants);
components.set('Products Search', Catalog.ProductsSearch);

// Catalog 2 renderings
components.set('Product Overview 2', Catalog.ProductOverview2);
components.set('Product Information 2', Catalog.ProductInformation2);
components.set('Product Variants 2', Catalog.ProductVariants);

// Checkout renderings
components.set('Add To Cart', Checkout.AddToCart);
components.set('Shopping Cart', Checkout.Cart);
components.set('Billing', Checkout.Billing);
components.set('Shipping', Checkout.Shipping);
components.set('Order Summary', Checkout.OrderInformation);
components.set('Order Confirmation', Checkout.OrderConfirmation);
components.set('Order History', Checkout.OrderHistory);
components.set('Payment', Checkout.Payment);
components.set('Checkout Navigation', Checkout.CheckoutNavigation);
components.set('Go To Checkout', Checkout.GoToCheckout);

// Checkout renderings 2
components.set('Add To Cart 2', Checkout.AddToCart);

// Account renderings
components.set('Sign-up Form', Account.SignUpForm);
components.set('Change Password Form', Account.ChangePassword);
components.set('Address Manager', Account.AddressManager);
components.set('Change User Information Form', Account.ChangeUserInformationForm);
components.set('Login Register', Account.LoginRegister);
export default (componentName: string) => components.get(componentName);

// Account renderings 2
components.set('Add To Wishlist 2', Account.AddToWishlist2);
