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

import classnames from 'classnames';
import * as React from 'react';
import Swiper, { SwiperOptions } from 'swiper';

import { Slide } from './components';
import { CarouselProps, CarouselState } from './models';

import './styles';

/**
 * Base carousel component with generic logic for the carousel
 */
export class Carousel<TProps extends CarouselProps, TState extends CarouselState> extends React.Component<
  CarouselProps,
  CarouselState
> {
  public static Slide = Slide;

  private readonly SWIPER_UPDATE_DELAY = 1000;
  private swiper: Swiper;
  private container: HTMLDivElement | null;
  private readonly swiperOptions: SwiperOptions;

  /**
   * @param props The Props.
   * @param swiperOptions Swiper Options, for details see http://idangero.us/swiper/api/#parameters
   */
  constructor(props: TProps) {
    super(props);

    this.swiperOptions = props.options;

    this.state = {
      currentIndex: 0,
      nextButtonDisabled: true,
      prevButtonDisabled: true,
    };

    this.slideTo = this.slideTo.bind(this);
    this.slidePrev = this.slidePrev.bind(this);
  }

  protected setSwiperContainer(container: HTMLDivElement) {
    this.container = container;
  }

  /**
   * Executes swiper's slide next
   */
  protected slideNext() {
    if (this.swiper) {
      const { onSlideNext } = this.props;

      if (onSlideNext) {
        onSlideNext(this.swiper);
      }

      this.swiper.slideNext();
    }
  }

  /**
   * Executes swiper's slide prev
   */
  protected slidePrev() {
    if (this.swiper) {
      const { onSlidePrev } = this.props;

      if (onSlidePrev) {
        onSlidePrev(this.swiper);
      }

      this.swiper.slidePrev();
    }
  }

  /**
   * Executes swiper's slideTo
   * @param index index of the item to slide
   */
  protected slideTo(index: number) {
    if (this.swiper) {
      this.swiper.slideTo(index);
    }
  }

  /**
   * Init swiper if it was not initialized before
   */
  protected initSwiper(options?: SwiperOptions) {
    if (this.swiper || !this.container || !this.swiperOptions) {
      return;
    }

    this.swiper = new Swiper(this.container, options || this.swiperOptions);

    // subscribe to slideChange event in order to change component's state properly
    this.swiper.on('slideChange', () => {
      const { onSlideChange } = this.props;

      if (onSlideChange) {
        onSlideChange(this.swiper);
      }

      this.updateCarouselState();
    });

    // subscribe to window resize event in order to recalculate swiper's parameters
    this.swiper.on('resize', () => {
      setTimeout(() => {
        if (this.container && this.swiper) {
          // we need to extend the existing update logic of swiper with cleanup of style attribute
          // it allows swiper to set proper width of the swiper slide on container size change
          const swiperSlideNodes = this.container.getElementsByClassName('swiper-slide');
          const swiperSlides: Element[] = Array.prototype.slice.call(swiperSlideNodes);

          swiperSlides.forEach((swiperSlide) => {
            if (swiperSlide.getAttribute('style')) {
              swiperSlide.removeAttribute('style');
            }
          });

          // execute the swiper update
          this.swiper.update();
        }
        // tslint:disable-next-line:align
      }, this.SWIPER_UPDATE_DELAY);
    });

    // finally call the state update in order to properly  render current state of carousel
    this.updateCarouselState();
  }

  /**
   * Destroys swiper
   */
  protected destroySwiper() {
    if (this.swiper) {
      this.swiper.destroy(true, true);
    }
  }

  /**
   * Subscribed to 'slideChange' event of the swiper and update state accordingly
   */
  protected updateCarouselState() {
    const { isBeginning, isEnd, activeIndex, realIndex } = this.swiper;

    // in case loop option is enabled just set the realIndex as a currentIndex of carousel
    if (this.swiperOptions.loop) {
      return this.setState({
        currentIndex: realIndex,
      });
    }

    // don't allow swipe prev if swiper is in the beginning
    this.swiper.allowSlidePrev = !isBeginning;
    // don't allow swipe next if swiper is in the end
    this.swiper.allowSlideNext = !isEnd;

    this.setState({
      currentIndex: activeIndex,
      nextButtonDisabled: isEnd,
      prevButtonDisabled: isBeginning,
    });
  }

  /**
   * Returns the instance of swiper
   */
  protected getSwiperInstance() {
    return this.swiper;
  }

  /**
   * Calls the update method of swiper
   */
  protected updateSwiper() {
    if (this.swiper) {
      this.swiper.update();
    }
  }

  public componentDidMount() {
    this.initSwiper();
  }

  /**
   * Renders the carousel
   */
  public render() {
    const { children, className, buttonNextText, buttonPreviousText } = this.props;
    const { prevButtonDisabled, nextButtonDisabled } = this.state;

    const noSlide = prevButtonDisabled && nextButtonDisabled;
    return (
      <div className={classnames('carousel', { 'no-slide': noSlide })}>
        {buttonPreviousText &&
          !noSlide && (
            <div className="carousel-button-container">
              <button
                disabled={prevButtonDisabled}
                className="carousel-button-container_button"
                onClick={(e) => this.slidePrev()}
              >
                {buttonPreviousText}
              </button>
            </div>
          )}
        <div
          ref={(el) => {
            this.setSwiperContainer(el);
          }}
          className={classnames('swiper-container', { [className]: !!className })}
        >
          <div className="swiper-wrapper">{children}</div>
        </div>
        {buttonPreviousText &&
          !noSlide && (
            <div className="carousel-button-container">
              <button
                disabled={nextButtonDisabled}
                className="carousel-button-container_button"
                onClick={(e) => this.slideNext()}
              >
                {buttonNextText}
              </button>
            </div>
          )}
      </div>
    );
  }
}
