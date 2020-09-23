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

import * as JSS from 'Foundation/ReactJss';
import pictureOne from 'Foundation/UI/common/media/images/testimonial/1.jpg';
import pictureTwo from 'Foundation/UI/common/media/images/testimonial/2.jpg';

import { ProductInformationProps, ProductInformationState } from './models';

import './styles.scss';

class ProductInformationComponent extends JSS.SafePureComponent<ProductInformationProps, ProductInformationState> {
  public constructor(props: ProductInformationProps) {
    super(props);

    this.state = {
      selectedTab: 'description',
    };
  }

  protected safeRender() {
    const { product } = this.props.sitecoreContext;
    const { selectedTab } = this.state;
    return (
      <section className="product-description-2">
        <div className="tab">
          <button
            className={'tab-links ' + (selectedTab === 'description' ? 'active' : '')}
            onClick={() => this.setState({ selectedTab: 'description' })}
          >
            Product Description
          </button>
          <button
            className={'tab-links ' + (selectedTab === 'features' ? 'active' : '')}
            onClick={() => this.setState({ selectedTab: 'features' })}
          >
            Features
          </button>
          <button
            className={'tab-links ' + (selectedTab === 'rating' ? 'active' : '')}
            onClick={() => this.setState({ selectedTab: 'rating' })}
          >
            Rating
          </button>
        </div>
        <div className="product-content">
          {selectedTab === 'description' && (
            <div className="description-text">
              <p>{product && product.description}</p>
            </div>
          )}
          {selectedTab === 'features' && (
            <div className="description-features">
              <ul className="feature-list">
                <li>2 GB</li>
                <li>Model XE-C</li>
                <li>Charcoal</li>
                <li>Explorer Edition</li>
              </ul>
            </div>
          )}
          {selectedTab === 'rating' && (
            <div className="row">
              <div className="col-lg-7">
                <div className="review-wrapper">
                  <div className="single-review">
                    <div className="review-img">
                      <img src={pictureOne} alt="" />
                    </div>
                    <div className="review-content">
                      <div className="review-top-wrap">
                        <div className="review-left">
                          <div className="review-name">
                            <h4>White Lewis</h4>
                          </div>
                          <div className="review-rating">
                            <i className="fa fa-star" />
                            <i className="fa fa-star" />
                            <i className="fa fa-star" />
                            <i className="fa fa-star" />
                            <i className="fa fa-star" />
                          </div>
                        </div>
                        <div className="review-left">
                          <button>Reply</button>
                        </div>
                      </div>
                      <div className="review-bottom">
                        <p>
                          Vestibulum ante ipsum primis aucibus orci luctustrices posuere cubilia Curae Suspendisse
                          viverra ed viverra. Mauris ullarper euismod vehicula. Phasellus quam nisi, congue id nulla.
                        </p>
                      </div>
                    </div>
                  </div>
                  <div className="single-review child-review">
                    <div className="review-img">
                      <img src={pictureTwo} alt="" />
                    </div>
                    <div className="review-content">
                      <div className="review-top-wrap">
                        <div className="review-left">
                          <div className="review-name">
                            <h4>White Lewis</h4>
                          </div>
                          <div className="review-rating">
                            <i className="fa fa-star" />
                            <i className="fa fa-star" />
                            <i className="fa fa-star" />
                            <i className="fa fa-star" />
                            <i className="fa fa-star" />
                          </div>
                        </div>
                        <div className="review-left">
                          <button>Reply</button>
                        </div>
                      </div>
                      <div className="review-bottom">
                        <p>
                          Vestibulum ante ipsum primis aucibus orci luctustrices posuere cubilia Curae Suspendisse
                          viverra ed viverra. Mauris ullarper euismod vehicula. Phasellus quam nisi, congue id nulla.
                        </p>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div className="col-lg-5">
                <div className="ratting-form-wrapper pl-50">
                  <h3>Add a Review</h3>
                  <div className="ratting-form">
                    <form action="#">
                      <div className="star-box">
                        <span>Your rating:</span>
                        <div className="ratting-star">
                          <i className="fa fa-star" />
                          <i className="fa fa-star" />
                          <i className="fa fa-star" />
                          <i className="fa fa-star" />
                          <i className="fa fa-star" />
                        </div>
                      </div>
                      <div className="row">
                        <div className="col-md-6">
                          <div className="rating-form-style mb-10">
                            <input placeholder="Name" type="text" />
                          </div>
                        </div>
                        <div className="col-md-6">
                          <div className="rating-form-style mb-10">
                            <input placeholder="Email" type="email" />
                          </div>
                        </div>
                        <div className="col-md-12">
                          <div className="rating-form-style form-submit">
                            <textarea name="Your Review" placeholder="Message" defaultValue={''} />
                            <input type="submit" defaultValue="Submit" />
                          </div>
                        </div>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
            </div>
          )}
        </div>
      </section>
    );
  }
}

export const ProductInformation2 = JSS.renderingWithContext(ProductInformationComponent);
