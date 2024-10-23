export enum SortOrder {
  Ascending,
  Descending,
}

export interface PaginatedRequest<T> {
  page?: number;
  pageSize?: number;
  sortBy?: string;
  sortDirection?: SortOrder;
  searchValue?: string;
  filter?: any;
}
