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

import { EventEmitter } from 'events';

import { handler } from './models';

class EventHub extends EventEmitter {
  public publish(event: string, data?: {}): boolean {
    return super.emit(event, data);
  }

  public subscribe(event: string, listener: handler): () => void {
    super.on(event, listener);

    return () => {
      return removeEventListener(event, listener);
    };
  }

  public subscribeOnce(event: string, listener: handler): void {
    super.once(event, listener);
  }
}

export const eventHub = new EventHub();
