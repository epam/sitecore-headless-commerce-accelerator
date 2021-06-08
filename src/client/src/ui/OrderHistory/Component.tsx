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

import React, { MouseEvent } from 'react';

import { SafePureComponent } from 'Foundation/ReactJss';

import { Text } from '@sitecore-jss/sitecore-jss-react';
import { Icon, Spinner } from 'components';
import { NavigationLink } from 'Foundation/UI';

import { OrderHistoryItem } from './components';
import { OrderHistoryProps, OrderHistoryState } from './models';

import './styles.scss';

export class OrderHistoryComponent extends SafePureComponent<OrderHistoryProps, OrderHistoryState> {
  constructor(props: OrderHistoryProps) {
    super(props);
    this.state = {
      isFirstLoad: true,
    };
  }
  public componentDidMount() {
    this.props.GetOrderHistory();
  }

  public safeRender() {
    const { orders, isLastPage, isLoading, sitecoreContext } = this.props;
    const { isFirstLoad } = this.state;
    return (
      <div className="order-history">
        {!isLoading || !isFirstLoad ? (
          orders && orders.length > 0 ? (
            <div>
              <div className="order-history-color-title">
                <Text tag="h1" field={{ value: 'Order History' }} className="title" />
              </div>
              <div className="order-list">
                {orders.map((order, index) => (
                  <div className="row order-list-item" key={index}>
                    <OrderHistoryItem order={order} fallbackImageUrl={sitecoreContext.fallbackImageUrl} />
                  </div>
                ))}
              </div>
              {!isLastPage && !isLoading && (
                <div className="order-history-loadMore">
                  <a className="btn-load-more" href="#" onClick={(e) => this.loadMoreHandler(e)}>
                    View more orders
                  </a>
                </div>
              )}
              {isLoading && !isFirstLoad && <Spinner />}
            </div>
          ) : (
            <div className="order-history_container-empty">
              <div className="order-history-icon">
                <Icon icon="icon-note2" />
              </div>
              <label className="order-history_label-empty">Order history is empty</label>
              <NavigationLink to={'/'}>
                <button className="order-history_btn-empty">Shop Now</button>
              </NavigationLink>
            </div>
          )
        ) : (
          <Spinner />
        )}
      </div>
    );
  }

  private loadMoreHandler(e: MouseEvent<HTMLAnchorElement>) {
    e.preventDefault();
    this.setState({ isFirstLoad: false });
    this.props.LoadMore();
  }
}
