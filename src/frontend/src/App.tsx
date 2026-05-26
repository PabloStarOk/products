import { useEffect, useState } from "react";
import "@/App.css";
import ProductsList from "@/components/ProductsList";
import type { Product } from "@/models/product";
import { productService } from "@/services/product-service";
import SearchBar from "@/components/SearchBar";
import { Button } from "@/components/ui/button";
import AddForm from "@/components/AddForm";

function App() {
  const [products, setProducts] = useState<Product[]>([]);
  const [search, setSearch] = useState("");
  const [addFormOpen, setAddFormOpen] = useState(false);

  function loadProducts() {
    productService.getAll().then(setProducts);
  }

  useEffect(() => {
    loadProducts();
  }, []);

  const filteredProducts = products.filter((p) => {
    const query = search.trim().toLowerCase();
    return (
      p.name?.toLowerCase().includes(query) ||
      p.sku?.toLowerCase().includes(query)
    );
  });

  return (
    <>
      <header>
        <h1 className="text-3xl font-bold text-center my-4">
          Inventario de Productos
        </h1>
      </header>
      <main className="flex flex-col gap-4 p-4 md:p-12">
        <div className="flex justify-center items-center gap-4 bg-background p-4 border border-border rounded-md shadow-2xs">
          <SearchBar onSearch={setSearch} />
          <Button onClick={() => setAddFormOpen(true)}>Añadir</Button>
        </div>
        <AddForm
          open={addFormOpen}
          onOpenChange={setAddFormOpen}
          onSubmit={async (product) => {
            await productService.create(product);
            loadProducts();
          }}
        />
        <ProductsList products={filteredProducts} />
      </main>
    </>
  );
}

export default App;
