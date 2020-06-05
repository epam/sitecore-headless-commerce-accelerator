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

import * as JSS from 'Foundation/ReactJss/client';
import * as React from 'react';

import { NavigationSearchProps, NavigationSearchState } from './models';
import './styles.scss';

const SEARCH_INPUT_NAME = 'q';

export class NavigationSearchComponent extends JSS.SafePureComponent<NavigationSearchProps, NavigationSearchState> {
  private form: HTMLFormElement;
  private searchInput: HTMLInputElement | null;

  protected safeRender() {
    return (
      <form ref={(el) => (this.form = el)} className="navigation-search" onSubmit={(e) => this.handleFormSubmit(e)}>
        <span className="fa fa-search" />
        <input
          ref={(el) => (this.searchInput = el)}
          type="search"
          required={true}
          name={SEARCH_INPUT_NAME}
          placeholder="I'M LOOKING FOR..."
        />
      </form>
    );
  }

  private handleFormSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    const formData = new FormData(this.form);
    const q = formData.get(SEARCH_INPUT_NAME);
    if (q) {
      this.searchInput.value = '';
      this.props.ChangeRoute(`/search?q=${q}`);
    }
  }
}
