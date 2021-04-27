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

import React from 'react';

import { ImageSlider, Slide } from 'components/ImageSlider';

export default {
  title: 'ImageSlider',
};

const items = [
  'https://int-cd.hca.azure.epmc-stc.projects.epam.com/-/media/Images/Habitat/7042081_01.ashx',
  'https://images.unsplash.com/photo-1614486760304-3377622946b9?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=3334&q=80',
  'https://images.unsplash.com/photo-1614341433341-d3c9062a7830?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=906&q=80',
];

export const ImageSliderStory = () => {
  return (
    <div
      style={{
        height: '300px',
        width: '300px',
      }}
    >
      <ImageSlider>
        {items.map((url, i) => (
          <Slide key={i} url={url} />
        ))}
      </ImageSlider>
    </div>
  );
};
