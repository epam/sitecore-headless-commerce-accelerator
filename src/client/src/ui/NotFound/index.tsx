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

import * as JSS from 'Foundation/ReactJss';
import * as React from 'react';
import { Placeholder } from '@sitecore-jss/sitecore-jss-react';

import { NavigationLink } from 'ui/NavigationLink';
import { getRenderingSitecoreProps } from 'utils';
import { NotFoundProps, NotFoundState } from './models';
import { cnNotFound } from './cn';
import './styles.scss';
import { BackToTop } from '../../components/BackToTop';

class NotFoundComponent extends JSS.SafePureComponent<NotFoundProps, NotFoundState> {
  protected safeRender() {
    const rendering = getRenderingSitecoreProps(this.props);
    const { isLoaded } = this.props;
    return (
      isLoaded && (
        <div className={cnNotFound()}>
          <div className={cnNotFound('Title')}>404</div>
          <div className={cnNotFound('Description')}>
            Unfortunately, the page you are looking for is not found.
            <br />
            You might want to go to the
            <NavigationLink to="/" className={cnNotFound('Link')}>
              {' '}
              Home page{' '}
            </NavigationLink>
            or browse shop categories from main menu
          </div>
          <Placeholder name="main-content" rendering={rendering} />
          <BackToTop disabled={false} />
        </div>
      )
    );
  }
}

export const NotFound = JSS.rendering(NotFoundComponent);
