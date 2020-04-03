// tslint:disable:indent array-type

    export interface FacetDto {
        name: string;
        values: string[];
    }
    export const enum SortDirection {
        Asc = 0,
        Desc = 1
    }
    export interface ProductsSearchRequest {
        categoryId: string;
        facets: FacetDto[];
        pageNumber: number;
        pageSize: number;
        searchKeyword: string;
        sortDirection: SortDirection;
        sortField: string;
    }
