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

import { storiesOf } from '@storybook/react';

import { Spoiler } from 'components/Spoiler';

const handleClick = () => {
  const spoiler = document.querySelector('.Spoiler');
  spoiler.classList.toggle('Spoiler_expanded');
};

storiesOf('Spoiler', module)
  .add('Caption with text', () => (
    <Spoiler caption="I'm a spoiler's caption. Click me please" onClick={handleClick}>
      <div style={{ padding: '50px 40px' }}>
        <h1>Hidden text</h1>
        <img src="https://picsum.photos/790/400?grayscale" alt="random" />
      </div>
    </Spoiler>
  ))
  .add('Caption with React element', () => (
    <Spoiler
      caption={
        <span>
          I'm also a spoiler's caption but with image <img src="https://picsum.photos/790/400?grayscale&random=1" />
        </span>
      }
      onClick={handleClick}
    >
      <div style={{ padding: '50px 40px' }}>
        <h1>Hidden text</h1>
        <img src="https://picsum.photos/790/400?grayscale&random=2" alt="random" />
      </div>
    </Spoiler>
  ));
