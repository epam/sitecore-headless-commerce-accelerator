//    Copyright 2021 EPAM Systems, Inc.
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

import React, { FC, useMemo } from 'react';

import { Placeholder, VisitorIdentification } from '@sitecore-jss/sitecore-jss-react';
import Helmet from 'react-helmet';

import { useSitecoreContext } from 'Foundation/hooks';
import { getRenderingSitecoreProps } from 'Foundation/utils';
import { LayoutProps } from 'Project/HCA/models';

export const Default: FC<LayoutProps> = (props) => {
  const rendering = getRenderingSitecoreProps(props);
  const sitecoreContext = useSitecoreContext;
  const title = useMemo(() => {
    const { category, product } = sitecoreContext;
    const globalTitle = 'HCA';
    const pageTitle =
      (category && category.displayName) ||
      (product && product.displayName) ||
      (rendering && rendering.displayName) ||
      '';

    return pageTitle ? `${pageTitle} | ${globalTitle} ` : globalTitle;
  }, [sitecoreContext, rendering]);

  return (
    <>
      <Helmet>
        <title>{title}</title>
      </Helmet>
      <VisitorIdentification />
      <Placeholder name="main-content" rendering={rendering} {...props} />
    </>
  );
};
