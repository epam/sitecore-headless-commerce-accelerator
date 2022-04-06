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

import { Placeholder } from '@sitecore-jss/sitecore-jss-react';
import * as React from 'react';

import * as JSS from 'Foundation/ReactJss';

import { Icon } from 'components';

import { ProductOverviewProps, ProductOverviewState } from './models';
import './styles.scss';

import { ProductRating } from 'ui/ProductRating';
import { RelatedProducts } from 'ui/RelatedProducts';

import { Variant } from 'services/commerce';
import GlassesImg from 'static/images/glasses-for-slider.png';

import { ProductGallery } from './components/ProductGallery';
import { ProductActions } from './ProductActions';

import { eventHub, events } from 'services/eventHub';
import { ProductCardContext } from 'ui/ProductCard/context';

export default class ProductOverviewComponent extends JSS.SafePureComponent<
  ProductOverviewProps,
  ProductOverviewState
> {
  constructor(props: ProductOverviewProps) {
    super(props);
  }

  public componentDidUpdate() {
    const { selectedVariant } = this.props;
    const { product } = this.props.sitecoreContext;
    const selectedCatalogItem = selectedVariant || product;

    if (selectedCatalogItem) {
      eventHub.publish(events.PRODUCT_DETAILS.PRODUCT_DETAILS_VIEWED, {
        ...selectedCatalogItem,
        pageUri: window.location.pathname,
        price: selectedCatalogItem.adjustedPrice,
        variant: selectedCatalogItem.displayName,
      });
    }
  }

  protected safeRender() {
    const { selectedVariant } = this.props;
    const { product, fallbackImageUrl, productColors } = this.props.sitecoreContext;
    const selectedCatalogItem = selectedVariant || product;
    const selectedCatalogItemId =
      selectedCatalogItem &&
      (selectedCatalogItem.hasOwnProperty('variantId')
        ? (selectedCatalogItem as Variant).variantId
        : selectedCatalogItem.productId);

    const context = {
      fallbackImageUrl,
      onChangeVariant: (variantId: string) => {
        return;
      },
      product,
      productColors,
      selectedVariant,
    };

    return (
      (selectedCatalogItem && (
        <section className="product-overview">
          <div className="panel-overview">
            <div className="row">
              <div className="col-md-6">
                {!!selectedCatalogItem.imageUrls && selectedCatalogItem.imageUrls.length > 0 ? (
                  <ProductGallery key={selectedCatalogItemId} images={selectedCatalogItem.imageUrls} />
                ) : (
                  <img src={fallbackImageUrl} alt="No image" className="fallback-image" />
                )}
              </div>
              <div className="col-md-6">
                <header className="product-header">
                  <h1 className="product-title">{selectedCatalogItem.displayName}</h1>
                  <span className="product-code">{`Product code:  ${selectedCatalogItem.productId}`}</span>
                  {/* <div
                className={classNames('product-stock-status', {
                  'back-orderable': selectedCatalogItem.stockStatusName === Common.StockStatus.BackOrderable,
                  'in-stock': selectedCatalogItem.stockStatusName === Common.StockStatus.InStock,
                  'out-of-stock': selectedCatalogItem.stockStatusName === Common.StockStatus.OutOfStock,
                  'pre-orderable': selectedCatalogItem.stockStatusName === Common.StockStatus.PreOrderable,
                })}
              >
                <div>{this.getStockStatusLabel(selectedCatalogItem.stockStatusName)}</div>
              </div> */}
                </header>
                <div className="product-info">
                  <p className="product-price">
                    <span className="price-value">
                      {selectedCatalogItem.currencySymbol}
                      {selectedCatalogItem.adjustedPrice.toFixed(2)}
                    </span>
                    {selectedCatalogItem.adjustedPrice !== selectedCatalogItem.listPrice && (
                      <span className="price-value-adjusted">
                        {selectedCatalogItem.currencySymbol}
                        {selectedCatalogItem.listPrice.toFixed(2)}
                      </span>
                    )}
                  </p>
                  <ProductRating rating={selectedCatalogItem.customerAverageRating} />
                  <div className="product-description">{selectedCatalogItem.description}</div>
                  <div className="product-params">
                    <Placeholder name="product-properties" rendering={this.props.rendering} />
                    <div className="actions">
                      <ProductCardContext.Provider value={context}>
                        <ProductActions />
                      </ProductCardContext.Provider>
                    </div>
                  </div>
                  <div className="product-tags">
                    <span className="product-tags_title">Brand :</span>
                    {selectedCatalogItem.brand && (
                      <span className="product-tags_item">{selectedCatalogItem.brand}</span>
                    )}
                  </div>
                  <div className="product-tags">
                    <span className="product-tags_title">Tags :</span>
                    {selectedCatalogItem.tags &&
                      selectedCatalogItem.tags.map((tag, index) => (
                        <span key={index} className="product-tags_item">
                          {tag}
                        </span>
                      ))}
                  </div>
                  <div className="pro-details-social">
                    <ul>
                      <li>
                        <a href="//facebook.com">
                          <Icon icon="icon-facebook-f-brands" size="s" />
                        </a>
                      </li>
                      <li>
                        <a href="//dribbble.com">
                          <Icon icon="icon-dribbble-brands" size="s" />
                        </a>
                      </li>
                      <li>
                        <a href="//pinterest.com">
                          <Icon icon="icon-pinterest-brands" size="s" />
                        </a>
                      </li>
                      <li>
                        <a href="//twitter.com">
                          <Icon icon="icon-twitter-brands" size="s" />
                        </a>
                      </li>
                      <li>
                        <a href="//linkedin.com">
                          <Icon icon="icon-linkedin-brands" size="s" />
                        </a>
                      </li>
                    </ul>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div className="panel-productAdded">
            <div className="productAdded">
              <a href="" title="Close modal window" className="close">
                <Icon icon="icon-close" />
              </a>
              <div className="row">
                <div className="col-sm-6 col-lg-4 col-lg-offset">
                  <div className="productAdded_overview">
                    <p className="productAdded_columnTitle">Added to Cart</p>
                    <div className="product">
                      <img src={GlassesImg} alt="Product image" className="product_img" />
                      <div className="product_brand">Google</div>
                      <h2 className="product_title">New Google Glass V3.0 2GB Explorer Edition</h2>
                      <div className="product_id">Product #1234567890</div>
                    </div>
                  </div>
                </div>
                <div className="col-sm-6 col-lg-4">
                  <div className="productAdded_subtotal">
                    <p className="productAdded_columnTitle">
                      Subtotal
                      <span className="price">
                        <span className="price_currency">$</span>
                        <span className="price_value">1,089.00</span>
                      </span>
                    </p>
                    <div className="product_param product_param-colors">
                      <p className="param_label">Color:</p>
                      <div className="param_wrap">
                        <ul className="colors-list">
                          <li className="colors-listitem">
                            <span style={{ background: '#dadad8' }} className="colors-option" />
                          </li>
                          <li className="colors-listitem">
                            <span style={{ background: '#3d3e40' }} className="colors-option" />
                          </li>
                          <li className="colors-listitem">
                            <span style={{ background: '#706762' }} className="colors-option" />
                          </li>
                          <li className="colors-listitem">
                            <span style={{ background: '#5b9abd' }} className="colors-option" />
                          </li>
                        </ul>
                      </div>
                    </div>
                    <div className="product_param">
                      <span className="param_label">Size:</span>
                      <span className="param_value">1</span>
                    </div>
                    <div className="product_param">
                      <span className="param_label">Length:</span>
                      <span className="param_value">2</span>
                    </div>
                    <div className="product_param">
                      <span className="param_label">Quantity:</span>
                      <span className="param_value">3</span>
                    </div>
                    <div className="product_actions">
                      <a className="btn btn-viewCart">View Cart (1)</a>
                      <a className="btn btn-checkout">Checkout</a>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div>
            <RelatedProducts fallbackImageUrl={fallbackImageUrl} productColors={productColors} />
          </div>
        </section>
      )) ||
      null
    );
  }
  // private getStockStatusLabel(stockStatus: string) {
  //   switch (stockStatus) {
  //     case Common.StockStatus.BackOrderable:
  //       return 'Back Orderable';
  //     case Common.StockStatus.InStock:
  //       return 'In Stock';
  //     case Common.StockStatus.OutOfStock:
  //       return 'Out Of Stock';
  //     case Common.StockStatus.PreOrderable:
  //       return 'Pre Orderable';
  //     default:
  //       return '';
  //   }
  // }
}
