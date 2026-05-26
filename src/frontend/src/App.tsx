import { useEffect, useState } from "react";
import "@/App.css";
import ProductsList from "@/components/ProductsList";
import type { Product } from "@/models/product";
import { productService } from "@/services/product-service";
import SearchBar from "./components/SearchBar";

function App() {
  const [products, setProducts] = useState<Product[]>([]);
  const [search, setSearch] = useState("");

  useEffect(() => {
    productService.getAll().then(setProducts);
  }, []);

  const filteredProducts = products.filter((p) => {
    const query = search.trim().toLowerCase();
    return (
      p.name?.toLowerCase().includes(query) ||
      p.sku?.toLowerCase().includes(query)
    );
  });

  return (
    <main>
      <SearchBar onSearch={setSearch} />
      <ProductsList products={filteredProducts} />
    </main>
  );
}

export default App;
