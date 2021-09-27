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

import React, { FC, useCallback, useState } from 'react';

import axios, { AxiosError } from 'axios';

import { notify, notifySubscribed } from 'services/notifications';
import { StockStatus } from 'services/catalog';

import { AddToCart } from 'ui/ProductCard';

import { Button, Icon } from 'components';

import { SubmitEmailDialog } from '../SubmitEmailDialog';
import { ProductActionsProps } from './models';

import { cnProductActions } from './cn';
import './ProductActions.scss';

export const ProductActionsComponent: FC<ProductActionsProps> = ({
  productId,
  sitecoreContext,
  commerceUser,
  variants,
}) => {
  const [dialogOpen, setDialogOpen] = useState(false);

  const [variant] = variants;

  const outOfStock = variant && variant.stockStatusName === StockStatus.OutOfStock;

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

  return (
    <>
      <div className={cnProductActions('Row')}>
        <AddToCart />
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
