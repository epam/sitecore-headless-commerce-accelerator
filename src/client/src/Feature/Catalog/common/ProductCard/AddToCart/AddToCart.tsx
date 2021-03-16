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

import React, { FC, useCallback, useContext, useEffect, useMemo, useRef, useState } from 'react';

import { debounce, get } from 'lodash';
import { useDispatch, useSelector } from 'react-redux';

import { Common } from 'Feature/Catalog/Integration';
import { shoppingCart } from 'Feature/Checkout/Integration/ShoppingCart';
import { updateCartItemRequest } from 'Feature/Checkout/Integration/ShoppingCart/actions';
import { CartLine } from 'Foundation/Commerce';
import { LoadingStatus } from 'Foundation/Integration';

import { Button } from 'Foundation/UI/components/Button';
import { QuantityPicker } from 'Foundation/UI/components/QuantityPicker';

import { ProductCardContext } from '../context';

import { cnProductCard } from '../cn';
import './AddToCart.scss';

const DEFAULT_QUANTITY_FOR_ADD_TO_CART = 1;
const MAX_QUANTITY = 100;
const DEBOUNCE_DELAY = 1000;

export type AddToCartProps = {
  className?: string;
};

export const AddToCart: FC<AddToCartProps> = ({ className }) => {
  const dispatch = useDispatch();
  const contextData = useContext(ProductCardContext);
  const cartState = useSelector(shoppingCart);
  const [quantities, setQuantities] = useState<Record<string, number>>({});
  const initializedRef = useRef(false);

  const { product, selectedVariant } = contextData;
  const { data: cartData, cartItemsState } = cartState;
  const statusId = `${product.productId}-${selectedVariant.variantId}`;
  const loadingStatus = get(cartItemsState, [statusId, 'status']);
  const quantity = quantities[selectedVariant.variantId] || 0;
  const cartLinesState: CartLine[] | null = get(cartData, 'cartLines', null);

  const inCart = useMemo(() => {
    if (!cartLinesState) {
      return false;
    }

    const cartLine = cartLinesState.find(
      (item) => item.product.productId === product.productId && item.variant.variantId === selectedVariant.variantId,
    );

    return Boolean(cartLine);
  }, [cartLinesState]);

  useEffect(() => {
    if (!initializedRef.current && cartLinesState) {
      const res = cartLinesState.reduce((acc, x) => {
        if (x.product.productId === product.productId) {
          acc[x.variant.variantId] = x.quantity;
        }

        return acc;
      }, {});
      setQuantities(res);

      initializedRef.current = true;
    }
  }, [cartLinesState, product]);

  const debouncedRequestUpdate = useMemo(
    () =>
      debounce(({ productId, productQuantity, variantId }) => {
        dispatch(
          updateCartItemRequest({
            productId,
            quantity: productQuantity,
            variantId,
          }),
        );
      }, DEBOUNCE_DELAY),
    [],
  );

  const handleChangeQuantity = useCallback(
    (value) => {
      const { variantId, productId } = selectedVariant;

      setQuantities((currentQuantities) => ({
        ...currentQuantities,
        [variantId]: value,
      }));

      if (value === 0) {
        debouncedRequestUpdate.cancel();
        dispatch(updateCartItemRequest({ productId, quantity: value, variantId }));
      } else {
        debouncedRequestUpdate({ productId, productQuantity: value, variantId });
      }
    },
    [cartData, selectedVariant, dispatch],
  );

  const handleClickAddToCart = useCallback(() => {
    const { variantId, productId } = selectedVariant;

    setQuantities((currentQuantities) => ({
      ...currentQuantities,
      [variantId]: DEFAULT_QUANTITY_FOR_ADD_TO_CART,
    }));

    dispatch(updateCartItemRequest({ productId, quantity: DEFAULT_QUANTITY_FOR_ADD_TO_CART, variantId }));
  }, [selectedVariant, dispatch]);

  const outOfStock = selectedVariant.stockStatusName === Common.StockStatus.OutOfStock;

  return (
    <div className={cnProductCard('AddToCart', [className])}>
      {quantity === 0 || !inCart ? (
        <Button
          className={cnProductCard('AddToCartButton')}
          buttonTheme={'defaultSlide'}
          fullWidth={true}
          disabled={outOfStock || loadingStatus === LoadingStatus.Loading || !cartLinesState}
          onClick={handleClickAddToCart}
        >
          {/* {isLoading && <i className={cnProductCard('Spinner', ['fa fa-spinner fa-spin'])} />} */}
          {outOfStock ? 'out of stock' : 'add to cart'}
        </Button>
      ) : (
        <div className={cnProductCard('QuantityPickerContainer')}>
          <QuantityPicker value={quantity} max={MAX_QUANTITY} onChange={handleChangeQuantity} />
          <span className={cnProductCard('ExtraInfo')}>In your cart</span>
        </div>
      )}
    </div>
  );
};
