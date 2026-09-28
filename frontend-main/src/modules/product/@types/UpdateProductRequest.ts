export interface UpdateProductRequest {
  productId: string;
  categoryId?: string | null;
  name: string;
  description?: string | null;
  price: number;
}
