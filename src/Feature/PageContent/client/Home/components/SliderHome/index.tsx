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

import * as React from 'react';

import { Text, withExperienceEditorChromes } from '@sitecore-jss/sitecore-jss-react';
import { Carousel } from 'Foundation/UI/client';
import { SliderHomeControlProps, SliderHomeControlState } from './models';

import * as Jss from 'Foundation/ReactJss/client';
import './styles.scss';

import GlassesImage from 'Foundation/UI/client/common/media/images/glasses-for-slider.png';

class SliderHomeControl extends Jss.SafePureComponent<SliderHomeControlProps, SliderHomeControlState> {
    public safeRender() {
        const { fields } = this.props;
        const images: string[] = new Array(8)
            .fill(undefined)
            .map(() => '');

        return (
            <div className="slider sub-categories top-picks">
                <div className="slider-header">
                    <div className="rainbow-title">
                    <Text
                        field={fields.sliderHeader}
                        tag="h2"
                        className="title"
                    />
                    <div className="rainbow-bar rainbow-bar-4" />
                    </div>
                </div>

                <Carousel
                    className="gallery-thumbs"
                    buttonPreviousText={<i className="fa fa-angle-left" />}
                    buttonNextText={<i className="fa fa-angle-right" />}
                    options={{
                        breakpoints: {
                            480: {
                                slidesPerView: 1,
                                spaceBetween: 25
                            },
                            1024: {
                                slidesPerView: 2,
                                spaceBetween: 25
                            },
                            1310: {
                                slidesPerView: 3,
                                spaceBetween: 25
                            },
                        },
                        slidesPerView: 4,
                        spaceBetween: 0,
                    }}
                >
                    {images &&
                    images.map((item, index) => (
                        <figure key={index} className="swiper-slide item">
                            <div className="image-wrap">
                                <img src={GlassesImage} alt={index.toString()}/>
                            </div>
                            <figcaption className="item-cap">
                                <Text
                                    field={{ value: 'Google' }}
                                    tag="p"
                                    className="item-brand"
                                />
                                <h2 className="item-title">
                                    <Text
                                        tag="a"
                                        href="#"
                                        title="Glasses"
                                        field={{ value: 'Glasses' }}
                                    />
                                </h2>
                                <div className="item-rating">
                                    <span className="star">
                                        <i className="fa fa-star"/>
                                    </span>
                                    <span className="star">
                                        <i className="fa fa-star"/>
                                    </span>
                                    <span className="star">
                                        <i className="fa fa-star"/>
                                    </span>
                                    <span className="star">
                                        <i className="fa fa-star"/>
                                    </span>
                                </div>
                            </figcaption>
                        </figure>
                    ))}
                </Carousel>
            </div>
        );
    }
}

export const SliderHome = withExperienceEditorChromes(SliderHomeControl);
