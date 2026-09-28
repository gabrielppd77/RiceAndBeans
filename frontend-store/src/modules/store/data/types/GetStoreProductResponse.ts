export interface GetStoreProductResponse {
  id: string;
  name: string;
  description?: string | null;
  urlImage?: string | null;
  price: number;
  categoryName: string;
}
