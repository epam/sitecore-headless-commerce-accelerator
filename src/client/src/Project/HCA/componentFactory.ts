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
import { AboutMission } from 'Feature/PageContent/AboutUs/AboutMission';
import { AboutUsBanner } from 'Feature/PageContent/AboutUs/Banner';
import { BrandLogos } from 'Feature/PageContent/AboutUs/BrandLogos';
import { FunFacts } from 'Feature/PageContent/AboutUs/FunFacts';
import { Team } from 'Feature/PageContent/AboutUs/Team';
import { Welcome } from 'Feature/PageContent/AboutUs/Welcome';

import * as Account from 'Feature/Account';
import * as Catalog from 'Feature/Catalog';
import * as Checkout from 'Feature/Checkout';
import * as Navigation from 'Feature/Navigation';
import * as PageContent from 'Feature/PageContent';
import { NotFound } from 'Foundation/UI/common/components/Errors/NotFound';
import * as Scaffolding from 'Project/HCA/App/components/Scaffolding/components';

const components = new Map<string, any>();

// add components to the map
// components.set('Component', <Component />);

// Page Scaffolding
components.set('One Column', Scaffolding.OneColumn);
components.set('Two Column', Scaffolding.TwoColumn);
components.set('Two Column Row', Scaffolding.TwoColumnRow);
components.set('Three Column Row', Scaffolding.ThreeColumnRow);
components.set('Full Width', Scaffolding.FullWidth);

// Home renderings
components.set('Carousel Banner', PageContent.CarouselBanner);
components.set('Recommended Products', PageContent.RecommendedProducts);

// Header renderings
components.set('Header', Navigation.Header);
components.set('Header Content', Navigation.HeaderContent);
components.set('Logo', Navigation.Logo);
components.set('Navigation Menu', Navigation.NavigationMenu);
components.set('Quick Navigation', Navigation.QuickNavigation);
components.set('User Navigation', Navigation.UserNavigation);

// Footer renderings
components.set('Footer', Navigation.GlobalFooter);
components.set('Copyright', Navigation.Copyright);
components.set('Footer Links', Navigation.FooterLinks);
components.set('Social Networks Links', Navigation.SocialNetworksLinks);

// Catalog renderings
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
components.set('Change Password Form', Account.ChangePassword);
components.set('Address Manager', Account.AddressManager);
components.set('Change User Information Form', Account.ChangeUserInformationForm);
components.set('Login Register', Account.LoginRegister);

// Not Found
components.set('Not Found', NotFound);

// About Us
components.set('Welcome', Welcome);
components.set('Banner', AboutUsBanner);
components.set('About Mission', AboutMission);
components.set('Fun Facts', FunFacts);
components.set('Team', Team);
components.set('Brand Logos', BrandLogos);

export default (componentName: string) => components.get(componentName);
