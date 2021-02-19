import React from 'react';
interface Quantity {
  cartLineId?: string;
  setQuantity: (quantity: boolean) => void;
  quantityString: string;
}

const QuantityProductCommon = ({ cartLineId, setQuantity, quantityString }: Quantity) => {
  return (
    <div className="quantity" data-autotests="productQty">
      <input type="button" value="-" className="qty-button qty-minus" onClick={() => setQuantity(false)} />
      <input type="text" id={`qty-${cartLineId}`} className="cart-plus-minus-box" value={quantityString} disabled={true} />
      <input type="button" value="+" className="qty-button qty-plus" onClick={() => setQuantity(true)} />
    </div>
  );
};

export {
  QuantityProductCommon
};