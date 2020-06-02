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
import classNames from 'classnames';
import * as React from 'react';

import * as JSS from 'Foundation/ReactJss/client';

import { Common } from 'Feature/Catalog/client/Integration';

import { ProductOverviewProps, ProductOverviewState } from './models';
import './styles.scss';

import GlassesImg from 'Foundation/UI/client/common/media/images/glasses-for-slider.png';
import { ProductGallery } from './components/ProductGallery';
import { ProductRating } from './components/ProductRating';

export default class ProductOverviewComponent extends JSS.SafePureComponent<
  ProductOverviewProps,
  ProductOverviewState
> {
  constructor(props: ProductOverviewProps) {
    super(props);
  }
  public closeAddedModal(e: any) {
    console.log(e);
  }
  // tslint:disable-next-line:no-big-function
  protected safeRender() {
    const { product, fallbackImageUrl } = this.props.sitecoreContext;
    const selectedVariant = this.props.selectedVariant;
    const selectedCatalogItem = selectedVariant || product;

    return (
      <section className="product-overview">
        <div className="panel-overview">
          <header className="product-header">
            <a href="javascript:if(window.print)window.print()" title="Print button" className="product-print">
              <i className="fa fa-print" />
            </a>
            <p className="product-brand">{selectedCatalogItem.brand}</p>
            <h1 className="product-title">{selectedCatalogItem.displayName}</h1>
            <div
              className={classNames('product-stock-status', {
                'back-orderable': selectedCatalogItem.stockStatusName === Common.StockStatus.BackOrderable,
                'in-stock': selectedCatalogItem.stockStatusName === Common.StockStatus.InStock,
                'out-of-stock': selectedCatalogItem.stockStatusName === Common.StockStatus.OutOfStock,
                'pre-orderable': selectedCatalogItem.stockStatusName === Common.StockStatus.PreOrderable,
              })}
            >
              <h4>{this.getStockStatusLabel(selectedCatalogItem.stockStatusName)}</h4>
            </div>
          </header>
          <div className="row">
            <div className="col-md-6">
              {!!selectedCatalogItem.imageUrls && selectedCatalogItem.imageUrls.length > 0 ? (
                <ProductGallery images={selectedCatalogItem.imageUrls} />
              ) : (
                <img src={fallbackImageUrl} alt="No image" className="fallback-image" />
              )}
            </div>
            <div className="col-md-6">
              <div className="product-info">
                <ProductRating rating={selectedCatalogItem.customerAverageRating} />
                <p className="product-price">
                  <span className="price-label">Sale</span>
                  <span className="price-value">
                    {selectedCatalogItem.currencySymbol} {selectedCatalogItem.adjustedPrice.toFixed(2)}
                  </span>
                </p>
                <div className="product-params">
                  <Placeholder name="product-properties" rendering={this.props.rendering} />
                  <div className="property-selectors" />
                  <div className="links">
                    <Placeholder name="product-actions" rendering={this.props.rendering} />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div className="panel-productAdded">
          <div className="productAdded">
            <a href="" title="Close modal window" onClick={(e) => this.closeAddedModal(e)} className="close">
              <i className="fa fa-close" />
            </a>
            <div className="row">
              <div className="col-sm-6 col-lg-4 col-lg-offset-2">
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
      </section>
    );
  }
  private getStockStatusLabel(stockStatus: string) {
    switch (stockStatus) {
      case Common.StockStatus.BackOrderable:
        return 'Back Orderable';
      case Common.StockStatus.InStock:
        return 'In Stock';
      case Common.StockStatus.OutOfStock:
        return 'Out Of Stock';
      case Common.StockStatus.PreOrderable:
        return 'Pre Orderable';
      default:
        return '';
    }
  }
}
