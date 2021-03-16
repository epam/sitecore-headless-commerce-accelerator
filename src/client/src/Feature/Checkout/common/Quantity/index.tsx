import React, { FC } from 'react';

import { Input } from 'Foundation/UI/components/Input';

type Props = {
  cartLineId?: string;
  inc: () => void;
  dec: () => void;
  quantity: number;
};

export const QuantityProductCommon: FC<Props> = ({ cartLineId, inc, dec, quantity }) => {
  return (
    <div className="quantity" data-autotests="productQty">
      <input type="button" value="-" className="qty-button qty-minus" onClick={dec} />
      <Input
        fullWidth={true}
        type="number"
        id={`qty-${cartLineId}`}
        className="cart-plus-minus-box"
        value={quantity}
        disabled={true}
      />
      <input type="button" value="+" className="qty-button qty-plus" onClick={inc} />
    </div>
  );
};
