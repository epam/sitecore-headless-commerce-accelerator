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

import * as AboutUs from 'Feature/PageContent/AboutUs';

import * as Account from 'Feature/Account';
import * as Catalog from 'Feature/Catalog';

import * as PageContent from 'Feature/PageContent';
import { NotFound } from 'Foundation/UI/common/components/Errors/NotFound';
import { FullWidth, OneColumn, ThreeColumnRow, TwoColumn, TwoColumnRow } from 'layouts';

import { AddressManager } from 'ui/AddressManager';
import { ChangePassword } from 'ui/ChangePassword';
import { ChangeUserInformationForm } from 'ui/ChangeUserInformationForm';
import { LoginRegister } from 'ui/LoginRegister';
import { ResetRequestForm, PasswordResetForm } from 'ui/ResetPassword';
import { Billing } from 'ui/Billing';
import { Cart } from 'ui/Cart';
import { CheckoutNavigation } from 'ui/CheckoutNavigation';
import { GoToCheckout } from 'ui/GoToCheckout';
import { OrderConfirmation } from 'ui/OrderConfirmation';
import { OrderHistory } from 'ui/OrderHistory';
import { OrderInformation } from 'ui/OrderInformation';
import { Payment } from 'ui/Payment';
import { Shipping } from 'ui/Shipping';

import { Breadcrumb } from 'ui/Breadcrumb';
import { Copyright } from 'ui/Copyright';
import { FooterLinks } from 'ui/FooterLinks';
import { GlobalFooter } from 'ui/GlobalFooter';
import * as Navigation from 'ui/Header';
import { SocialNetworksLinks } from 'ui/SocialNetworksLinks';

const components = new Map<string, any>();

// add components to the map
// components.set('Component', <Component />);

// Page Scaffolding
components.set('One Column', OneColumn);
components.set('Two Column', TwoColumn);
components.set('Two Column Row', TwoColumnRow);
components.set('Three Column Row', ThreeColumnRow);
components.set('Full Width', FullWidth);

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
components.set('Footer', GlobalFooter);
components.set('Copyright', Copyright);
components.set('Footer Links', FooterLinks);
components.set('Social Networks Links', SocialNetworksLinks);

// Catalog renderings
components.set('Product Overview', Catalog.ProductOverview);
components.set('Product Information', Catalog.ProductInformation);
components.set('Product List', Catalog.ProductList);
components.set('Product Filters', Catalog.ProductFilters);
components.set('Product Variants', Catalog.ProductVariants);
components.set('Products Search', Catalog.ProductsSearch);
components.set('Product Shop', Catalog.ProductShop);

// Checkout renderings
components.set('Shopping Cart', Cart);
components.set('Billing', Billing);
components.set('Shipping', Shipping);
components.set('Order Summary', OrderInformation);
components.set('Order Confirmation', OrderConfirmation);
components.set('Order History', OrderHistory);
components.set('Payment', Payment);
components.set('Checkout Navigation', CheckoutNavigation);
components.set('Go To Checkout', GoToCheckout);

// Account renderings
components.set('Change Password Form', ChangePassword);
components.set('Address Manager', AddressManager);
components.set('Change User Information Form', ChangeUserInformationForm);
components.set('Login Register', LoginRegister);
components.set('Reset Request Form', ResetRequestForm);
components.set('Password Reset Form', PasswordResetForm);

// Navigation
components.set('Breadcrumb', Breadcrumb);

// Not Found
components.set('Not Found', NotFound);

// About Us
components.set('Welcome', AboutUs.Welcome);
components.set('Banner', AboutUs.AboutUsBanner);
components.set('About Mission', AboutUs.AboutMission);
components.set('Fun Facts', AboutUs.FunFacts);
components.set('Team', AboutUs.Team);
components.set('Brand Logos', AboutUs.BrandLogos);

// Contact Us
components.set('Contact Us', PageContent.ContactUs);
components.set('Get In Touch Form', PageContent.GetInTouchForm);

export default (componentName: string) => components.get(componentName);
