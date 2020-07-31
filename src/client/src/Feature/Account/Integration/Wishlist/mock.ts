import * as Commerse from 'Foundation/Commerce';

// commerse entities

export interface Wishlist {
  lines: WishlistLine[];
  shopName: string;
  name: string;
  customerId: string;
  customerIdFacet: number;
  userId: string;
  userIdFacet: number;
  isFavorite: boolean;
}

export interface WishlistLine {
  product: Commerse.Product;
  quantity: number;
  total: number;
}

export interface RemoveWishlistLineRequest {
  productId: Commerse.Product;
  variantId: number;
}
