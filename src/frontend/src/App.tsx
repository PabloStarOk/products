import { useEffect, useState } from "react";
import "./App.css";
import ProductsList from "./components/ProductsList";
import type { Product } from "./models/product";
import { productService } from "./services/product-service";
import AddForm from "./components/AddForm";
import type { ProductRequest } from "./models/product-request";

function App() {
  const [products, setProducts] = useState<Product[]>([]);
  const [searchTerm, setSearchTerm] = useState("");
  const filteredProducts = products.filter(
    (p) =>
      p.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
      p.sku.toLowerCase().includes(searchTerm.toLowerCase()),
  );
  const [showAddForm, setShowAddForm] = useState(false);

  useEffect(() => {
    productService.getAll().then(setProducts);
  }, []);

  return (
    <>
      <input
        type="text"
        placeholder="Búsqueda por nombre o SKU"
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
      />
      <ProductsList products={filteredProducts} />
      <button onClick={() => setShowAddForm(true)}>Agregar producto</button>
      {showAddForm && <AddForm onSubmit={handleAddProduct} />}
    </>
  );

  async function handleAddProduct(product: ProductRequest) {
    const response = await productService.create(product);
    if (!response.ok) {
      alert("Error al crear el producto");
      return;
    }
    const updatedProducts = await productService.getAll();
    setProducts(updatedProducts);
    setShowAddForm(false);
  }
}

export default App;
