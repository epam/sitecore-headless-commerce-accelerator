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

import React, { FC, useCallback, useState } from 'react';

import { Common } from 'Feature/Catalog/Integration';
import { notify, notifySubscribed } from 'Foundation/services/notificationsService';

import { Button, QuantityPicker } from 'components';

import { SubmitEmailDialog } from '../SubmitEmailDialog';
import { ProductActionsProps } from './models';

import { cnProductActions } from './cn';
import './ProductActions.scss';

export const ProductActionsComponent: FC<ProductActionsProps> = ({
  isLoading,
  variant,
  productId,
  AddToCart,
  sitecoreContext,
  commerceUser,
}) => {
  const [quantity, setQuantity] = useState(1);
  const [dialogOpen, setDialogOpen] = useState(false);
  const outOfStock = variant && variant.stockStatusName === Common.StockStatus.OutOfStock;

  const handleAddToCartClick = useCallback(() => {
    AddToCart({ productId, quantity, variantId: variant.variantId });
  }, [productId, quantity, variant]);

  const subscribeToOOSProduct = (email: string) => {
    // TO-DO:: handle subscribe to oos product action

    notifySubscribed(sitecoreContext.product);
  };

  const handleInformMeButtonClick = useCallback(() => {
    if (commerceUser && commerceUser.customerId) {
      subscribeToOOSProduct('');
    } else {
      setDialogOpen(true);
    }
  }, [commerceUser]);

  const handleQuantityChange = useCallback(
    (newQuantity) => {
      setQuantity(newQuantity);
    },
    [setQuantity],
  );

  return (
    <>
      <div className={cnProductActions('Row')}>
        {!outOfStock && (
          <QuantityPicker min={1} max={100} size="l" theme="grey" value={quantity} onChange={handleQuantityChange} />
        )}
        <Button
          className={cnProductActions('ContainedButton', { outOfStock })}
          disabled={isLoading || outOfStock}
          title={outOfStock ? 'Out of Stock' : 'Add to Cart'}
          onClick={handleAddToCartClick}
          buttonTheme="defaultSlide"
        >
          {isLoading && <i className="fa fa-spinner fa-spin" />}
          {outOfStock ? 'Out of Stock' : 'Add to Cart'}
        </Button>
        <a href="javascript:if(window.print)window.print()" title="Print button" className="btn btn-main btn-print">
          <i className="pe-7s-print" />
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
