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

import { AnalyticsEventArgs } from './models';

class GoogleAnalytics {
  public initialize(debug: boolean = false): void {
    ReactGA.initialize(process.env.TRACKING_ID, { debug });
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

  private event(args: AnalyticsEventArgs): void {
    ReactGA.event(args);
  }
}

export const googleAnalytics = new GoogleAnalytics();
