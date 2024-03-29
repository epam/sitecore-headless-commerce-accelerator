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

import { Button, Icon } from 'components';

import 'styles/typography';

storiesOf('Button', module)
  .add('Default Color', () => <Button onClick={() => console.log('clicked')}>Click me!</Button>)
  .add('Default Color Reversed', () => (
    <Button buttonTheme="defaultReversed" onClick={() => console.log('clicked')}>
      Click me!
    </Button>
  ))
  .add('Default Color Slide', () => (
    <Button buttonTheme="defaultSlide" onClick={() => console.log('clicked')}>
      Click me!
    </Button>
  ))
  .add('Transparent Color Slide', () => (
    <Button buttonTheme="transparentSlide" onClick={() => console.log('clicked')}>
      Click me!
    </Button>
  ))
  .add('Grey Color', () => (
    <Button buttonTheme="grey" onClick={() => console.log('clicked')}>
      Click me!
    </Button>
  ))
  .add('Grey Color Reversed', () => (
    <Button buttonTheme="greyReversed" onClick={() => console.log('clicked')}>
      Click me!
    </Button>
  ))
  .add('Orange Color', () => (
    <Button buttonTheme="orange" onClick={() => console.log('clicked')}>
      Click me!
    </Button>
  ))
  .add('Black', () => (
    <Button buttonTheme="black" onClick={() => console.log('clicked')}>
      Click me!
    </Button>
  ))
  .add('Link Type', () => (
    <Button buttonType="link" href="/test">
      Click me!
    </Button>
  ))
  .add('Default Color with Rounded Borders', () => (
    <Button rounded={true} onClick={() => console.log('clicked')}>
      Click me!
    </Button>
  ))
  .add('Default Color with Full Width', () => (
    <Button fullWidth={true} onClick={() => console.log('clicked')}>
      Click me!
    </Button>
  ))
  .add('Clear', () => (
    <Button buttonTheme="clear" onClick={() => console.log('clicked')}>
      <div style={{ color: 'red' }}>
        <Icon icon="icon-star" />
      </div>
    </Button>
  ));
