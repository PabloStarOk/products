import type { ProductRequest } from "@/models/product-request";

const BASE_URL = import.meta.env.VITE_API_BASE_URL;
const PRODUCTS_URL = `${BASE_URL}/products`;

export const productService = {
  getAll: () => fetch(PRODUCTS_URL).then((r) => r.json()),
  getById: (id: number) => fetch(`${PRODUCTS_URL}/${id}`).then((r) => r.json()),
  create: (request: ProductRequest) =>
    fetch(PRODUCTS_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(request),
    }),
  update: (id: number, data: ProductRequest) =>
    fetch(`${PRODUCTS_URL}/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    }),
  delete: (id: number) => fetch(`${PRODUCTS_URL}/${id}`, { method: "DELETE" }),
};
