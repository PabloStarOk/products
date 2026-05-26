# Catalogo de Productos

Aplicación de catalogo de productos.

## Tecnologías

- .NET 10
- React 19

### Sistema Back-end Endpoints

- URL base: http://host:port/api
- Documentación: http://host:port/api/docs
- Endpoint para obtener todos los productos: GET http://host:port/api/products
- Endpoint para obtener un producto por ID: GET http://host:port/api/products/{id}
- Endpoint para crear un producto: POST http://host:port/api/products
- Endpoint para actualizar un producto: PUT http://host:port/api/products/{id}
- Endpoint para eliminar un producto: DELETE http://host:port/api/products/{id}

## Uso

### Requerimientos

1. Tener instalado .NET 10.
2. Tener instalado Bun o Nodejs (Preferiblemente Bun ya que el proyecto fue gestionado con este).

### Pasos para Ejecutar el Sistema Back-end

1. Clonar el repositorio.

```bash
git clone git@github.com:PabloStarOk/products.git
```

2. Acceder a la repositorio clonado y navegar hasta `src/backend/`

```bash
cd products # Repositorio
cd src/backend # Código del sistema back-end.
```

3. Construir el sistema back-end.

```bash
dotnet build Products/Products.csproj -c Release
```

4. Navegue a la carpeta del binario generado.

```bash
cd Products/bin/Release/net10.0/
```

5. Cree un archivo llamado `appsettings.json` en esta carpeta e indique la cadena de conexión para la base de datos SQLite de prueba.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=products.db"
  }
}
```

6. Estando en la carpeta del binario `Products/bin/Release/net10.0`, ejecute la aplicación según su sistema operativo:

```bash
# Linux
./Products

# Windows
./Products.exe
```

### Pasos para Ejecutar el Sistema Front-end

1. Clonar el repositorio.

```bash
git clone git@github.com:PabloStarOk/products.git
```

2. Acceder a la repositorio clonado y navegar hasta `src/frontend/`.

```bash
cd products # Repositorio
cd src/frontend # Código del sistema front-end.
```

3. Crear un archivo `.env` en la carpeta actual y establecer la variable `VITE_API_BASE_URL` indicando la URL donde se esta ejecutando el sistema back-end. Como alternativa, se puede establecer la misma variable de entorno pero a nivel del sistema en lugar del archivo.

```env
VITE_API_BASE_URL="http://localhost:5187/api"
```

4. Instalar las dependencias del proyecto según su gestor de paquetes.

```bash
# Bun
bun install

# NodeJS - npm
npm install

# NodeJS - pnpm
pnpm install
```

5. Realizar la construcción del código optimizado para producción.

```bash
# Bun
bun run build

# NodeJS - npm
npm run build

# NodeJS - pnpm
pnpm run build
```

6. Ejecutar la `dist`.

```bash
# Bun
bun run preview

# NodeJS - npm
npm run preview

# NodeJS - pnpm
pnpm run preview
```

### Pasos para Ejecutar el Script para Detectar SKUs duplicados

1. Clonar el repositorio.

```bash
git clone git@github.com:PabloStarOk/products.git
```

2. Acceder a la repositorio clonado y navegar hasta `scripts`.

```bash
cd products # Repositorio
cd scripts # Carpeta con script de python.
```

3. Ejecutar el script utilizando Python 3, pasando como argumento la ruta del archivo `products.json` o `products.csv` presentes en la carpeta actual.

```bash
# JSON
python detect_duplicates.py products.json

# CSV
python detect_duplicates.py products.csv

# Archivo personalizado (asegurar se incluir los campos id, name, sku,  price, stock y category)
python detect_duplicates.py /path/to/custom-file
```
