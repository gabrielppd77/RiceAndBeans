export interface CreateProductRequest {
  categoryId?: string | null;
  name: string;
  description?: string | null;
  price: number;
}
