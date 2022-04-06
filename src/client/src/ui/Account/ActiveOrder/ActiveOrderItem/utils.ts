//    Copyright 2022 EPAM Systems, Inc.
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

// Returns a date in a format MM/DD/YYYY
export const getFormattedDate = (date: Date) => {
  const parsedDate = new Date(Date.parse(date.toString()));

  const year = parsedDate.getFullYear();

  // getMonth() returns a number from 0 to 11
  const monthNumber = parsedDate.getMonth() + 1;
  const month = monthNumber < 10 ? '0' + monthNumber : monthNumber;

  const day = parsedDate.getDate();

  return [month, day, year].join('/');
};

export const getFormattedStatus = (status: string) => {
  return status
    .split(/(?=[A-Z])/)
    .join(' ')
    .toLowerCase();
};
