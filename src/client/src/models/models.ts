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

export enum LoadingStatus {
  NotLoaded = 'Not Loaded',
  Loading = 'Loading',
  Loaded = 'Loaded',
  Failure = 'Failure',
}

export interface Status {
  status: LoadingStatus;
  error?: string;
  stack?: string;
}

export type StatusType = () => Action<StatusPayload>;
export type FailureType = (error: string, stack?: string) => Action<FailurePayload>;

export interface Action<T = {}> {
  type: string;
  payload?: T;
}

export class Action<T = {}> implements Action<T> {
  public type: string;
  public payload?: T;
}

export interface StatusPayload extends Status {}

export interface FailurePayload extends Status {
  error: string;
  stack?: string;
}

export interface JsonResult {
  status: string;
  tempData: object;
}

export interface OkJsonResultModel<TData> extends JsonResult {
  data: TData;
}

export interface ErrorJsonResultModel extends JsonResult {
  error: string;
  exceptionMessage: string;
}

export interface Result<T> {
  data?: T;
  error?: Error;
}
