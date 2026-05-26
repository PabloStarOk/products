import { useEffect, useState } from "react";
import "@/App.css";
import ProductsList from "@/components/ProductsList";
import type { Product } from "@/models/product";
import { productService } from "@/services/product-service";

function App() {
  const [products, setProducts] = useState<Product[]>([]);
  useEffect(() => {
    productService.getAll().then(setProducts);
  }, []);

  return <ProductsList products={products} />;
}

export default App;
