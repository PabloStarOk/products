import csv
import json
import os
import sys

ID_FIELD = "id"
NAME_FIELD = "name"
SKU_FIELD = "sku"
PRICE_FIELD = "price"
STOCK_FIELD = "stock"
CATEGORY_FIELD = "category"
ALLOWED_EXTENSIONS = ['.csv', '.json']

class Product:
    def __init__(self, id, name, sku, price, stock, category):
        self.id = id
        self.name = name
        self.sku = sku
        self.price = price
        self.stock = stock
        self.category = category
        self.normalized_sku = self.normalize_sku(sku)

    def normalize_sku(self, sku):
        if not sku:
            return None
        return sku.upper().replace(" ", "").replace("-", "").replace("_", "")

    def __str__(self):
        return f"{ self.id, self.name, self.sku, self.price, self.stock, self.category }"


def load_csv_products(filepath):
    with open(filepath, mode='r', encoding='utf-8') as file:
        csv_reader = csv.DictReader(file)
        products = []
        for row in csv_reader:
            product = Product(
                id=row.get(ID_FIELD),
                name=row.get(NAME_FIELD),
                sku=row.get(SKU_FIELD),
                price=float(row.get(PRICE_FIELD, 0)),
                stock=int(row.get(STOCK_FIELD, 0)),
                category=row.get(CATEGORY_FIELD)
            )
            products.append(product)
    return products

def load_json_products(filepath):
    with open(filepath, mode='r', encoding='utf-8') as file:
        data = json.load(file)
        products = []
        for item in data:
            product = Product(
                id=item.get(ID_FIELD),
                name=item.get(NAME_FIELD),
                sku=item.get(SKU_FIELD),
                price=float(item.get(PRICE_FIELD, 0)),
                stock=int(item.get(STOCK_FIELD, 0)),
                category=item.get(CATEGORY_FIELD)
            )
            products.append(product)
    return products

def detect_duplicated_skus(products: list[Product]):
    print("Analizando SKUs potencialmente duplicados en el catálogo\n")
    sku_map: dict[str, list[Product]] = {}
    for product in products:
        if not product.sku:
            continue
        
        if product.normalized_sku not in sku_map:
            sku_map[product.normalized_sku] = []
        sku_map[product.normalized_sku].append(product)
    
    found = False
    for product.normalized_sku, products_list in sku_map.items():
        if len(products_list) < 2:
                return
        
        found = True
        formatted_products = list(map(lambda p: p.__str__(), products_list))
        joined_products = "\n".join(formatted_products)
        print(f"Potencial duplicado:")
        print(f"- Patrón base detectado: '{product.normalized_sku}'")
        print(f"- Productos en conflicto en el archivo:\n{joined_products}\n")
            
    if not found:
        print("No se detectaron anomalías en los SKUs.")

def main():
    if (len(sys.argv) <= 1):
        print("Error: No se proporcionó un archivo de entrada. Por favor, especifique la ruta de un archivo de entrada.")
        return

    filepath = sys.argv[1]
    if (not os.path.exists(filepath) or os.path.isdir(filepath)):
        print("Error: El archivo proporcionado no existe o es un directorio.")
        return

    if (not filepath.endswith('.csv') and not filepath.endswith('.json')):
        print("Error: El archivo proporcionado no tiene una extensión válida, solamente se soportan archivos con extensión .csv o .json.")
        return
    
    if filepath.endswith('.csv'):
        print(f"Cargando datos desde '{filepath}'")
        products = load_csv_products(filepath)
        detect_duplicated_skus(products)
    elif filepath.endswith('.json'):
        print(f"Cargando datos desde '{filepath}'")
        products = load_json_products(filepath)
        detect_duplicated_skus(products)

if __name__ == "__main__":
    main()