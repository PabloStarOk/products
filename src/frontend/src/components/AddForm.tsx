import { useState } from "react";
import type { ProductRequest } from "../models/product-request";
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "./ui/dialog";
import { Button } from "./ui/button";
import { Field, FieldGroup } from "./ui/field";
import { Label } from "./ui/label";
import { Input } from "./ui/input";

export default function AddForm({
  open,
  onOpenChange,
  onSubmit,
}: {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  onSubmit: (product: ProductRequest) => Promise<void>;
}) {
  const [name, setName] = useState("");
  const [sku, setSku] = useState("");
  const [price, setPrice] = useState(0);
  const [stock, setStock] = useState(0);
  const [category, setCategory] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(false);
  const [statusMsg, setStatusMsg] = useState("");

  function resetForm() {
    setName("");
    setSku("");
    setPrice(0);
    setStock(0);
    setCategory("");
    setLoading(false);
    setError(false);
    setStatusMsg("");
  }

  function handleOpenChange(nextOpen: boolean) {
    if (!nextOpen) {
      resetForm();
    }

    onOpenChange(nextOpen);
  }

  async function submit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setError(false);
    setLoading(true);
    setStatusMsg("");

    try {
      await onSubmit({
        name: name.trim(),
        sku: sku.trim(),
        price,
        stock,
        category: category.trim() || null,
      });

      resetForm();
      setStatusMsg("Producto añadido correctamente.");
    } catch (error: unknown) {
      const message =
        error instanceof Error
          ? error.message
          : "Error al añadir el producto, intentalo de nuevo.";

      setStatusMsg(message);
      setError(true);
    } finally {
      setLoading(false);
    }
  }

  return (
    <Dialog open={open} onOpenChange={handleOpenChange}>
      <DialogContent>
        <form className="flex flex-col gap-8" onSubmit={submit}>
          <DialogHeader>
            <DialogTitle className="text-2xl">Añadir Producto</DialogTitle>
          </DialogHeader>
          <FieldGroup>
            <Field>
              <Label htmlFor="name">Nombre</Label>
              <Input
                id="name"
                value={name}
                onChange={(e) => setName(e.target.value)}
                required
              />
            </Field>
            <Field>
              <Label htmlFor="sku">SKU</Label>
              <Input
                id="sku"
                value={sku}
                onChange={(e) => setSku(e.target.value)}
                required
              />
            </Field>
            <Field>
              <Label htmlFor="price">Precio</Label>
              <Input
                id="price"
                type="number"
                value={price}
                onChange={(e) => setPrice(Number(e.target.value))}
                min={0}
              />
            </Field>
            <Field>
              <Label htmlFor="stock">Stock</Label>
              <Input
                id="stock"
                type="number"
                value={stock}
                onChange={(e) => setStock(Number(e.target.value))}
                min={0}
              />
            </Field>
            <Field>
              <Label htmlFor="category">Categoría</Label>
              <Input
                id="category"
                value={category}
                onChange={(e) => setCategory(e.target.value)}
              />
            </Field>
          </FieldGroup>
          <DialogFooter>
            <div className="flex flex-col w-full gap-2">
              <p className={error ? "text-destructive" : "text-emerald-600"}>
                {statusMsg}
              </p>
              <div className="flex justify-end gap-2">
                <Button
                  variant="outline"
                  onClick={() => onOpenChange(false)}
                  disabled={loading}
                >
                  Cancelar
                </Button>
                <Button type="submit" disabled={loading}>
                  {loading ? "Enviando..." : "Añadir"}
                </Button>
              </div>
            </div>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
