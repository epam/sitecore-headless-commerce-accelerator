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

export interface Banner extends JSS.SafePureComponentState {
  image: JSS.GraphQLField<JSS.ImageFieldValue>;
  link: JSS.GraphQLField<JSS.LinkFieldValue>;
  subtitle: JSS.GraphQLField<string>;
  title: JSS.GraphQLField<string>;
  text: JSS.GraphQLField<string>;
}

interface CarouselBannerDataSource extends JSS.BaseDataSourceItem {
  banners: JSS.GraphQLListField<Banner>;
}

export interface CarouselBannerProps extends JSS.GraphQLRendering<CarouselBannerDataSource> {}

export interface CarouselBannerState extends JSS.SafePureComponentState {}
