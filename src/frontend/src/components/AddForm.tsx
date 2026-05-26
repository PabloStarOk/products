import { useState } from "react";
import type { ProductRequest } from "../models/product-request";

export default function AddForm({
  onSubmit,
}: {
  onSubmit: (product: ProductRequest) => void;
}) {
  const [name, setName] = useState("");
  const [sku, setSku] = useState("");
  const [price, setPrice] = useState(0);
  const [stock, setStock] = useState(0);
  const [category, setCategory] = useState("");

  return (
    <dialog open>
      <header>
        <h2>Crear producto</h2>
      </header>
      <form>
        <label htmlFor="productName">Nombre</label>
        <input
          id="productName"
          type="text"
          name="name"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <label htmlFor="sku">SKU</label>
        <input
          id="sku"
          type="text"
          name="sku"
          value={sku}
          onChange={(e) => setSku(e.target.value)}
        />
        <label htmlFor="price">Precio</label>
        <input
          id="price"
          type="number"
          name="price"
          step="0.01"
          value={price}
          onChange={(e) => setPrice(parseFloat(e.target.value) || 0)}
        />
        <label htmlFor="stock">Stock</label>
        <input
          id="stock"
          type="number"
          name="stock"
          step="1"
          value={stock}
          onChange={(e) => setStock(parseInt(e.target.value) || 0)}
        />
        <label htmlFor="category">Categoría</label>
        <input
          id="category"
          type="text"
          name="category"
          value={category}
          onChange={(e) => setCategory(e.target.value)}
        />
      </form>
      <button onClick={() => onSubmit({ name, sku, price, stock, category })}>
        Crear
      </button>
    </dialog>
  );
}
