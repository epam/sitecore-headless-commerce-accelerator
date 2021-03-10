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
import {
  AddToCart as addToCart,
  RemoveCartLine as removeFromCart,
  UpdateCartLine as updateToCart,
} from 'Feature/Checkout/Integration/ShoppingCart/actions';
import { CartLine } from 'Foundation/Commerce';

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
  const { data: cartData } = cartState;
  const quantity = quantities[selectedVariant.variantId] || 0;

  const cartLinesState: CartLine[] | null = get(cartData, 'cartLines', null);

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
          updateToCart({
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
      const { id: cartId, price: cartPrice } = cartData;

      setQuantities((currentQuantities) => ({
        ...currentQuantities,
        [variantId]: value,
      }));

      if (value === 0) {
        dispatch(
          removeFromCart({
            id: cartId,
            price: cartPrice,
            product,
            quantity: value,
            variant: selectedVariant,
          }),
        );
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

    dispatch(
      addToCart({
        productId,
        quantity: DEFAULT_QUANTITY_FOR_ADD_TO_CART,
        variantId,
      }),
    );
  }, [selectedVariant, dispatch]);

  const outOfStock = selectedVariant.stockStatusName === Common.StockStatus.OutOfStock;

  return (
    <div className={cnProductCard('AddToCart', [className])}>
      {quantity > 0 ? (
        <div className={cnProductCard('QuantityPickerContainer')}>
          <QuantityPicker value={quantity} max={MAX_QUANTITY} onChange={handleChangeQuantity} />
          <span className={cnProductCard('ExtraInfo')}>In your cart</span>
        </div>
      ) : (
        <Button
          className={cnProductCard('AddToCartButton')}
          buttonTheme={outOfStock ? 'default' : 'defaultSlide'}
          fullWidth={true}
          disabled={outOfStock || !cartLinesState}
          onClick={handleClickAddToCart}
        >
          {/* {isLoading && <i className={cnProductCard('Spinner', ['fa fa-spinner fa-spin'])} />} */}
          {outOfStock ? 'out of stock' : 'add to cart'}
        </Button>
      )}
    </div>
  );
};
