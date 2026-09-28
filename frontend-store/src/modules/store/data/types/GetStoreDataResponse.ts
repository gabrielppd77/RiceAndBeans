import type { GetStoreProductResponse } from "./GetStoreProductResponse";

export interface GetStoreDataResponse {
  id: string;
  name: string;
  description?: string | null;
  urlImage?: string | null;
  products: GetStoreProductResponse[];
}
