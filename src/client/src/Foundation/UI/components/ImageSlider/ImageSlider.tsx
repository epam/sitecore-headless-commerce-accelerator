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

import React, { Children, FC, ReactElement, useCallback, useMemo, useRef, useState } from 'react';

import { get } from 'lodash';
import Swiper from 'react-id-swiper';
import { useInterval } from 'react-use';

import { cnImageSlider } from './cn';
import './ImageSlider.scss';

const MIN_SLIDES_FOR_DOTS = 2;

export type ImageSliderProps = {
  children?: ReactElement | ReactElement[];
  delay?: number;
};

export const ImageSlider: FC<ImageSliderProps> = ({ children, delay = 1000 }) => {
  const ref = useRef(null);
  const [swiperState, setSwiperState] = useState<'stop' | 'forward' | 'backward'>('stop');
  const defaultParams = useMemo(() => {
    const params = {
      lazy: true,
    };

    if (Children.count(children) >= MIN_SLIDES_FOR_DOTS) {
      params['pagination'] = {
        bulletActiveClass: 'ImageSlider-SwiperBullets_active',
        el: '.swiper-pagination',
      };
    }

    return params;
  }, [children]);

  useInterval(
    () => {
      const swiper = get(ref, ['current', 'swiper']);

      if (swiperState === 'forward' && swiper) {
        swiper.slideNext();
      }

      if (swiperState === 'backward' && swiper) {
        swiper.slidePrev();
      }
    },
    swiperState === 'stop' ? null : delay,
  );

  const goNext = useCallback(() => {
    setSwiperState('forward');
  }, [setSwiperState]);

  const goPrev = useCallback(() => {
    setSwiperState('backward');
  }, [setSwiperState]);

  const clear = useCallback(() => {
    setSwiperState('stop');
  }, [setSwiperState]);

  return (
    <div className={cnImageSlider()}>
      <div className={cnImageSlider('SwiperContainer')}>
        <Swiper {...defaultParams} ref={ref}>
          {children}
        </Swiper>
      </div>
      <div className={cnImageSlider('SwiperOverlay')} onMouseLeave={clear}>
        <div className={cnImageSlider('ButtonLeft')} onMouseEnter={goPrev} />
        <div className={cnImageSlider('ButtonRight')} onMouseEnter={goNext} />
      </div>
    </div>
  );
};
