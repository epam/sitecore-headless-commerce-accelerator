import * as commonModels from 'Feature/Catalog/Integration/common/models';
import { GlobalSearchState, SuggestionProduct } from 'services/search';

export type SuggestionListStateProps = {
  status: string;
  products: SuggestionProduct[];
};

export type SuggestionListDispatchProps = {
  resetState: () => void;
};

export type SuggestionListOwnProps = {
  onItemClick?: (product: SuggestionProduct) => void;
};

export type SuggestionListProps = SuggestionListStateProps & SuggestionListOwnProps & SuggestionListDispatchProps;

export type AppState = GlobalSearchState & commonModels.AppState;
