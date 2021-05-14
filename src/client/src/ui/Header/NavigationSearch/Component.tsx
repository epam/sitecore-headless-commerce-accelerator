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

import React, { ChangeEvent } from 'react';

import * as JSS from 'Foundation/ReactJss';
import { debounce } from 'lodash';

import { LoadingStatus } from 'Foundation/Integration';

import { Input } from 'components';

import { NavigationSearchProps, NavigationSearchState } from './models';
import { SuggestionList } from './SuggestionList';

import './styles.scss';

const SEARCH_INPUT_NAME = 'q';
const TIME_ASYNC_FOCUS = 50;
const DEBOUNCE_DELAY = 400;
const MIN_CHARACTERS_TO_SEARCH = 3;

import classnames from 'classnames';
export class NavigationSearchComponent extends JSS.SafePureComponent<NavigationSearchProps, NavigationSearchState> {
  private form: HTMLFormElement;
  private searchInput: HTMLInputElement | null;
  private wrapperRef: React.MutableRefObject<HTMLDivElement>;
  private timeOutFocus: ReturnType<typeof setTimeout>;

  private debouncedRequestSuggestions = debounce((value: string) => {
    this.props.requestSuggestions(value);
  }, DEBOUNCE_DELAY);

  constructor(props: NavigationSearchProps) {
    super(props);
    this.state = {
      isOpen: false,
    };
    this.wrapperRef = React.createRef<HTMLDivElement>();
  }

  public setTimeOutFocus() {
    this.timeOutFocus = setTimeout(() => {
      this.searchInput.focus();
    }, TIME_ASYNC_FOCUS);
  }

  public clearTimeOutFocus() {
    clearTimeout(this.timeOutFocus);
  }

  public componentDidMount() {
    this.setTimeOutFocus();
    document.addEventListener('click', this.handleOutsidePopupClick.bind(this), false);
  }

  public componentDidUpdate() {
    this.setTimeOutFocus();
  }
  public componentDidUnMount() {
    this.clearTimeOutFocus();
    document.removeEventListener('click', this.handleOutsidePopupClick.bind(this), false);
  }
  public handleClick = () => {
    this.setState({ isOpen: !this.state.isOpen });
  };
  protected safeRender() {
    return (
      <div className="navigation-buttons_item search" ref={this.wrapperRef}>
        <a className="navigation-search_link" onClick={this.handleClick}>
          <i className="pe-7s-search" />
        </a>
        <div className={classnames('search_popup', { 'search_popup--visible': this.state.isOpen })}>
          <form
            className="search_form"
            action="#"
            ref={(el) => (this.form = el)}
            onSubmit={(e) => this.handleFormSubmit(e)}
          >
            <Input
              className="search_field"
              ref={(el) => (this.searchInput = el)}
              type="search"
              name={SEARCH_INPUT_NAME}
              placeholder="Search"
              autoComplete="off"
              onChange={this.handleSearchChange}
              fullWidth={true}
            />
            <button className="search_button">
              <i className="pe-7s-search" />
              <i className="fa fa-search" />
            </button>
          </form>
          <SuggestionList onItemClick={this.handleItemClick} />
        </div>
      </div>
    );
  }
  private handleFormSubmit(e: React.FormEvent<HTMLFormElement>) {
    this.resetSuggestions();
    e.preventDefault();
    const formData = new FormData(this.form);
    const q = formData.get(SEARCH_INPUT_NAME);
    if (q) {
      this.searchInput.value = '';
      this.props.ChangeRoute(`/search?q=${q}`);
    }
    this.setState({
      isOpen: !this.state.isOpen,
    });
  }
  private handleOutsidePopupClick(e: MouseEvent) {
    if (this.wrapperRef.current && !this.wrapperRef.current.contains(e.target as Node) && this.state.isOpen) {
      this.resetSuggestions();
      this.searchInput.value = '';
      this.setState({
        isOpen: !this.state.isOpen,
      });
    }
  }

  private resetSuggestions = () => {
    this.debouncedRequestSuggestions.cancel();
    this.props.resetSuggestionsState();
  };

  private handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { searchSuggestionsStatus } = this.props;
    const value = e.target.value.trim();

    if (value.length >= MIN_CHARACTERS_TO_SEARCH) {
      this.debouncedRequestSuggestions(value);
    } else if (searchSuggestionsStatus === LoadingStatus.Loaded) {
      this.resetSuggestions();
    }
  };

  private handleItemClick = () => {
    const { closeHamburgerMenu } = this.props;

    this.resetSuggestions();
    this.searchInput.value = '';
    this.setState({
      isOpen: false,
    });

    closeHamburgerMenu();
  };
}
