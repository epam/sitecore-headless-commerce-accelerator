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

import { Image } from '@sitecore-jss/sitecore-jss-react';
import * as React from 'react';

import * as JSS from 'Foundation/ReactJss/client';
import { NavigationLink } from 'Foundation/UI/client';

import NavMain from './components/NavMain';
import NavUser from './components/NavUser';
import NavUtility from './components/NavUtility';
import SearchMain from './components/SearchMain';

import { GlobalHeaderProps, GlobalHeaderState } from './models';
import './styles.scss';

export default class GlobalHeaderControl extends JSS.SafePureComponent<GlobalHeaderProps, GlobalHeaderState> {
  public componentDidMount() {
    this.props.LoadCart();
  }

  protected openMenu(e: React.MouseEvent<HTMLAnchorElement>) {
    e.preventDefault();
    document.getElementById('header-main').classList.add('panel-open');
    document.getElementById('closeOverlay').classList.add('overlay-open');
  }

  protected closeMenu(e: React.MouseEvent<HTMLDivElement>) {
    e.preventDefault();
    document.getElementById('header-main').classList.remove('panel-open');
    document.getElementById('closeOverlay').classList.remove('overlay-open');
  }

  protected safeRender() {
    const { ChangeRoute, returnUrl, StartAuthentication, authProcess, cartQuantity, commerceUser, fields } = this.props;
    const { datasource } = fields.data;

    return (
      <>
        <header id="header-main">
          <div className="wrap">
            <NavUtility />
          </div>
          <div className="nav-wrap">
            <div className="wrap">
              <SearchMain ChangeRoute={ChangeRoute} />
              <NavigationLink to="/" className="logo">
                <Image media={datasource.logo.jss} />
              </NavigationLink>
              <NavMain menuItems={datasource.menuItems} />
            </div>
          </div>
          <div className="wrap">
            <NavUser
              cartQuantity={cartQuantity}
              authProcess={authProcess}
              returnUrl={returnUrl}
              StartAuth={StartAuthentication}
              commerceUser={commerceUser}
            />
          </div>
        </header>
        <header id="header-mobile" scroll-header="scroll-header">
          <a href="" panel-toggle="panel-toggle" className="toggle" onClick={(e) => this.openMenu(e)}>
            <i className="fa fa-bars" />
          </a>
          <NavigationLink to="/" className="logo">
            <Image media={datasource.mobileLogo.jss} />
          </NavigationLink>
        </header>
        <div id="closeOverlay" onClick={(e) => this.closeMenu(e)}>
          <div className="closeOverlay_header" />
          <div className="closeOverlay_body" />
        </div>
      </>
    );
  }
}
