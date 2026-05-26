import { Field, FieldLabel } from "./ui/field";
import { Input } from "./ui/input";

export default function SearchBar({
  onSearch,
}: {
  onSearch: (query: string) => void;
}) {
  return (
    <Field>
      <FieldLabel htmlFor="searchBar">Barra de Búsqueda</FieldLabel>
      <Input
        id="searchBar"
        placeholder="Ingresa el nombre de un producto o su SKU"
        onChange={(e) => onSearch(e.target.value)}
      />
    </Field>
  );
}
