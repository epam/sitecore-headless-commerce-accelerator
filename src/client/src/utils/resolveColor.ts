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

export type ProductColors = {
  [key: string]: string;
};

export const colorValues: { [key: string]: string } = {
  Blue: '#3387d4',
  Mint: '#78E1C9',
  Orange: '#FFA500',
  Pink: '#E34D67',
  Purple: '#C126C1',
  Red: '#FF0000',
  Spring: '#7EE17E',
  Teal: '#008080',
  Violet: '#5C2AAB',
  Yellow: '#FFFF00',
};

export const resolveColor = (colorName: string, productColors: ProductColors) => {
  if (colorName) {
    const colorFromContext = productColors[colorName];
    if (colorFromContext) {
      return colorFromContext;
    }

    const colorPredifined = colorValues[colorName];
    if (colorPredifined) {
      return colorPredifined;
    }
  }

  console.info(`Color '${colorName}' is not set or not found in settings and in predefined values.`);
  return '#BBBBBB';
};
