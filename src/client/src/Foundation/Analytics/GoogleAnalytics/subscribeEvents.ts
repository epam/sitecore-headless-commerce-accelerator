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

import { eventHub, events } from 'Foundation/EventHub';

import { googleAnalytics } from './GoogleAnalytics';
import { CartLine } from './models';

export const subscribeGoogleAnalyticsEvents = () => {
  eventHub.subscribe(events.ANALYTICS.INITIALIZE, (params) => googleAnalytics.initialize(params && params.debug));
  eventHub.subscribe(events.ACCOUNT.EMAIL_VALIDATED, () => googleAnalytics.raiseEmailValidatedEvent());
  eventHub.subscribe(events.ACCOUNT.CREATED, () => googleAnalytics.raiseAccountCreatedEvent());
  eventHub.subscribe(events.ACCOUNT.UPDATED, () => googleAnalytics.raiseAccountUpdatedEvent());
  eventHub.subscribe(events.ACCOUNT.PASSWORD_CHANGED, () => googleAnalytics.raisePasswordChangedEvent());
  eventHub.subscribe(events.ACCOUNT.ADDRESS_ADDED, () => googleAnalytics.raiseAddressAddedEvent());
  eventHub.subscribe(events.ACCOUNT.ADDRESS_UPDATED, () => googleAnalytics.raiseAddressUpdatedEvent());
  eventHub.subscribe(events.ACCOUNT.ADDRESS_REMOVED, () => googleAnalytics.raiseAddressRemovedEvent());
  eventHub.subscribe(events.AUTHENTICATION.LOGIN, () => googleAnalytics.raiseLoginEvent());
  eventHub.subscribe(events.AUTHENTICATION.LOGOUT, () => googleAnalytics.raiseLogoutEvent());
  eventHub.subscribe(
    events.PRODUCT_LIST.PRODUCT_SHOWN,
    ({ productId: id, displayName: name, adjustedPrice: price = null, variants = null, ...params }) =>
      googleAnalytics.addProductImpression({
        ...params,
        id,
        name,
        price,
        variant: variants && variants[0] && variants[0].displayName,
      }),
  );
  eventHub.subscribe(events.CART.CARTLINE_ADDED, (cartLine: CartLine) => {
    googleAnalytics.raiseCartLineAddedEvent(cartLine);
  });
  eventHub.subscribe(events.CART.CARTLINE_UPDATED, (cartLine: CartLine) => {
    googleAnalytics.raiseCartLineUpdatedEvent(cartLine);
  });
  eventHub.subscribe(events.CART.CARTLINE_REMOVED, (cartLine: CartLine) => {
    googleAnalytics.raiseCartLineRemovedEvent(cartLine);
  });
  eventHub.subscribe(
    events.PRODUCT_DETAILS.PRODUCT_DETAILS_VIEWED,
    ({ productId: id, displayName: name, adjustedPrice: price = null, pageUri, ...params }) =>
      googleAnalytics.viewProductDetails(
        {
          ...params,
          id,
          name,
          price,
        },
        pageUri,
      ),
  );
  eventHub.subscribe(
    events.PRODUCT_LIST.PRODUCT_CLICKED,
    ({ productId: id, displayName: name, adjustedPrice: price = null, variants = null, ...params }) => {
      googleAnalytics.raiseProductClickedEvent({
        ...params,
        id,
        name,
        price,
        variant: variants && variants[0] && variants[0].displayName || name,
      });
    }
  );
};
