export interface GetProductResponse {
  id: string;
  name: string;
  description?: string | null;
  urlImage?: string | null;
  price: number;
  categoryId?: string | null;
}
