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

import * as Jss from 'Foundation/ReactJss';

export interface TeamMemberSocialLinkDataSource extends Jss.BaseDataSourceItem {
  uri: Jss.GraphQLField<Jss.TextField>;
  iconClass: Jss.GraphQLField<Jss.TextField>;
}

export interface TeamMemberDataSource
  extends Jss.BaseDataSourceItem,
    Jss.GraphQLListField<TeamMemberSocialLinkDataSource> {
  fullName: Jss.GraphQLField<Jss.TextField>;
  position: Jss.GraphQLField<Jss.TextField>;
  image: Jss.GraphQLField<Jss.TextField>;
}

export interface TeamDataSource extends Jss.BaseDataSourceItem, Jss.GraphQLListField<TeamMemberDataSource> {
  title: Jss.GraphQLField<Jss.TextField>;
  text: Jss.GraphQLField<Jss.TextField>;
}

export interface TeamProps extends Jss.GraphQLRendering<TeamDataSource> {}

export interface TeamState extends Jss.SafePureComponentState {}
