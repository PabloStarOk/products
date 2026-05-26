export interface ProductRequest {
  name: string;
  sku: string;
  price: number;
  stock: number;
  category: string | null;
}
