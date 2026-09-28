export interface ProductResponseFormDTO {
  id: string;
  name: string;
  description?: string;
  order: number;
  urlImage?: string;
  price: number;
  categoryId: string;
}
