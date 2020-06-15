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

export const keyMirror = <T extends {}>(keys: T, namespace?: string): { [K in keyof T]: string } => {
  const mirror = {};

  Object.keys(keys).forEach((key) => {
    mirror[key] = namespace ? `@${namespace}/${key}` : key;
  });

  return mirror as { [K in keyof T]: string };
};

const SAGA_NAMESPACE = 'SAGA';

export const keyMirrorSaga = <T extends {}>(keys: T, namespace?: string): { [K in keyof T]: string } => {
  const sagaNameSpace = namespace ? `${namespace}/${SAGA_NAMESPACE}` : SAGA_NAMESPACE;
  return keyMirror(keys, sagaNameSpace);
};

const REDUCER_NAMESPACE = 'REDUCER';

export const keyMirrorReducer = <T extends {}>(keys: T, namespace?: string): { [K in keyof T]: string } => {
  const sagaNameSpace = namespace ? `${namespace}/${REDUCER_NAMESPACE}` : REDUCER_NAMESPACE;
  return keyMirror(keys, sagaNameSpace);
};
