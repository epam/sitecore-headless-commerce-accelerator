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

const SLIDES_PER_VIEW = {
  640: 2,
  992: 4,
  1024: 2,
  1200: 3,
};

export const calculateNumberOfSlidesToShow = (images: string[]) => {
  const breakpointsRules = {};

  for (const breakpoint of Object.keys(SLIDES_PER_VIEW)) {
    breakpointsRules[breakpoint] = {
      slidesPerView:
        images && images.length < SLIDES_PER_VIEW[breakpoint] ? images.length : SLIDES_PER_VIEW[breakpoint],
    };
  }

  return breakpointsRules;
};
