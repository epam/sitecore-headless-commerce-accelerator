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

export interface GetInTouchFormDataSource extends JSS.BaseDataSourceItem {
  formTitle: JSS.GraphQLField<JSS.TextField>;
  namePlaceholder: JSS.GraphQLField<JSS.TextField>;
  emailPlaceholder: JSS.GraphQLField<JSS.TextField>;
  subjectPlaceholder: JSS.GraphQLField<JSS.TextField>;
  messagePlaceholder: JSS.GraphQLField<JSS.TextField>;
  submitButtonText: JSS.GraphQLField<JSS.TextField>;
}

export interface GetInTouchFormProps extends JSS.GraphQLRendering<GetInTouchFormDataSource> {}
