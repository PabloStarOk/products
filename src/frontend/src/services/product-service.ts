import type { ProductRequest } from "@/models/product-request";

const BASE_URL = import.meta.env.VITE_API_BASE_URL;
const PRODUCTS_URL = `${BASE_URL}/products`;

type ProblemDetails = {
  title?: string;
  status?: number;
  detail?: string;
  errors?: Record<string, string[]>;
};

async function httpRequest(input: RequestInfo | URL, init?: RequestInit) {
  const response = await fetch(input, init);

  if (!response.ok) {
    const problem = (await response
      .json()
      .catch(() => null)) as ProblemDetails | null;

    const skuError =
      problem?.errors &&
      Object.entries(problem.errors).find(
        ([key]) => key.toLowerCase() === "sku",
      )?.[1]?.[0];

    const message =
      skuError ??
      problem?.detail ??
      problem?.title ??
      `Request failed with status ${response.status}`;

    console.log("API error:", { status: response.status, problem });

    throw new Error(message);
  }

  return response;
}

export const productService = {
  getAll: async () => {
    const response = await httpRequest(PRODUCTS_URL, {
      method: "GET",
    });
    return response.json();
  },
  getById: (id: number) => fetch(`${PRODUCTS_URL}/${id}`).then((r) => r.json()),
  create: async (request: ProductRequest) => {
    const response = await httpRequest(PRODUCTS_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(request),
    });

    return response.json().catch(() => null);
  },
  update: (id: number, data: ProductRequest) =>
    fetch(`${PRODUCTS_URL}/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    }),
  delete: (id: number) => fetch(`${PRODUCTS_URL}/${id}`, { method: "DELETE" }),
};
