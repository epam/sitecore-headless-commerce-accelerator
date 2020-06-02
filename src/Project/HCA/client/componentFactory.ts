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

import * as Account from 'Feature/Account/client';
import * as Catalog from 'Feature/Catalog/client';
import * as Checkout from 'Feature/Checkout/client';
import * as PageContent from 'Feature/PageContent/client';

import Copyright from 'Feature/Navigation/client/Copyright';
import GlobalFooter from 'Feature/Navigation/client/GlobalFooter';
import GlobalHeader from 'Feature/Navigation/client/GlobalHeader';

import FooterLinks from 'Feature/Navigation/client/FooterLinks';
import SocialNetworksLinks from 'Feature/Navigation/client/SocialNetworksLinks';

const components = new Map<string, any>();

// add components to the map
// components.set('Component', <Component />);

// Page Scaffolding
components.set('One Column', PageContent.OneColumn);
components.set('Two Column', PageContent.TwoColumn);
components.set('Two Column Row', PageContent.TwoColumnRow);
components.set('Three Column Row', PageContent.ThreeColumnRow);

// Page Content
components.set('Header', GlobalHeader);

// Home renderings
components.set('Recommended Products', PageContent.RecommendedProducts);
components.set('Banner Grid', PageContent.BannerGrid);
components.set('Banner', PageContent.Banner);
components.set('Clear Banner', PageContent.ClearBanner);
components.set('Light Banner', PageContent.LightBanner);
components.set('Modern Banner', PageContent.ModernBanner);

// Footer renderings
components.set('Footer', GlobalFooter);
components.set('Copyright', Copyright);
components.set('Footer Links', FooterLinks);
components.set('Social Networks Links', SocialNetworksLinks);

// Catalog renderings
components.set('Find In Store', Catalog.FindInStore);
components.set('Product Overview', Catalog.ProductOverview);
components.set('Product Information', Catalog.ProductInformation);
components.set('Product List', Catalog.ProductList);
components.set('Product Filters', Catalog.ProductFilters);
components.set('Product Variants', Catalog.ProductVariants);
components.set('Products Search', Catalog.ProductsSearch);

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

// Account renderings
components.set('Sign-up Form', Account.SignUpForm);
components.set('Change Password Form', Account.ChangePassword);
components.set('Address Manager', Account.AddressManager);
components.set('Change User Information Form', Account.ChangeUserInformationForm);

export default (componentName: string) => components.get(componentName);
