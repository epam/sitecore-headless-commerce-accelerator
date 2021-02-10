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
};
