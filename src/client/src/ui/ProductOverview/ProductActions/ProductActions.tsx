//    Copyright 2021 EPAM Systems, Inc.
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

import React, { FC, useCallback, useEffect, useState, useMemo } from 'react';

import axios, { AxiosError } from 'axios';

import { LoadingStatus } from 'models';
import { notify, notifySubscribed } from 'services/notifications';
import { StockStatus } from 'services/catalog';
import { Variant } from 'services/commerce';

import { AddToCart } from 'ui/ProductCard';

import { Button, Icon } from 'components';

import { useWishlistState } from 'ui/Wishlist/hooks';
import { SubmitEmailDialog } from '../SubmitEmailDialog';
import { ProductActionsProps } from './models';

import classnames from 'classnames';

import { cnProductActions } from './cn';
import './ProductActions.scss';

export const ProductActionsComponent: FC<ProductActionsProps> = ({
  productId,
  sitecoreContext,
  commerceUser,
  variants,
  wishlistStatus,
}) => {
  const [dialogOpen, setDialogOpen] = useState(false);
  const [displayNotifications, setDisplayNotifications] = useState(false);

  const [wishlistItems, setWishlistItems] = useWishlistState([]);
  const [variant] = variants;

  const outOfStock = variant && variant.stockStatusName === StockStatus.OutOfStock;

  useEffect(() => {
    if (displayNotifications && wishlistStatus === LoadingStatus.Loaded) {
      notify('success', 'Product added!');
    }
    if (displayNotifications && wishlistStatus === LoadingStatus.Failure) {
      notify('success', 'Sorry, something went wrong');
    }
  }, [wishlistStatus]);

  const subscribeToOOSProduct = (email: string) => {
    const getApi = process.env.OUT_OF_STOCK_SUBSCRIPTION_API_URL || 'http://localhost:7071/api';
    const outOfStockSubscriptionApiURL = getApi + '/Subscribe';
    const data = {
      email,
      productId,
    };

    axios
      .post(outOfStockSubscriptionApiURL, data)
      .then(() => notifySubscribed(sitecoreContext.product))
      .catch((error: AxiosError) => notify('error', error.message));
  };

  const handleInformMeButtonClick = useCallback(() => {
    if (commerceUser && commerceUser.customerId) {
      subscribeToOOSProduct(commerceUser.email);
    } else {
      setDialogOpen(true);
    }
  }, [commerceUser]);

  const isAddedItem = useMemo(() => {
    return wishlistItems.some((x) => x.variantId === variant.variantId);
  }, [wishlistItems]);

  const handleAddToWishlistClick = () => {
    if (!isAddedItem) {
      const addItem: Variant[] = wishlistItems.concat(variant);
      setWishlistItems(addItem);
      setDisplayNotifications(true);
    } else {
      const removeItem: Variant[] = wishlistItems.filter((product) => product.variantId !== variant.variantId);
      setWishlistItems(removeItem);
    }
  };

  return (
    <>
      <div className={cnProductActions('Row')}>
        <AddToCart />
        <button
          title={isAddedItem ? 'Remove from Wishlist' : 'Add to Wishlist'}
          className={classnames('btn btn-main btn-wishlist', {
            'add-wishlist-active': isAddedItem,
          })}
          onClick={handleAddToWishlistClick}
        >
          <Icon icon="icon-heart" />
        </button>
        <a href="javascript:if(window.print)window.print()" title="Print button" className="btn btn-main btn-print">
          <Icon icon="icon-print" />
        </a>
      </div>
      {outOfStock && (
        <>
          <Button buttonTheme="text" className={cnProductActions('InformMeButton')} onClick={handleInformMeButtonClick}>
            Inform me when it back in
          </Button>
          <SubmitEmailDialog
            dialogOpen={dialogOpen}
            toggleDialog={setDialogOpen}
            submitDialogData={subscribeToOOSProduct}
          />
        </>
      )}
    </>
  );
};
