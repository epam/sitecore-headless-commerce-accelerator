import * as commonModels from 'Feature/Catalog/Integration/common/models';
import { GlobalProductsSearchSuggestionsState, Product } from 'Feature/Catalog/Integration/ProductsSearchSuggestions';

export type SuggestionListStateProps = {
  status: string;
  products: Product[];
};

export type SuggestionListDispatchProps = {
  resetState: () => void;
};

export type SuggestionListOwnProps = {
  onItemClick?: (product: Product) => void;
};

export type SuggestionListProps = SuggestionListStateProps & SuggestionListOwnProps & SuggestionListDispatchProps;

export type AppState = GlobalProductsSearchSuggestionsState & commonModels.AppState;
