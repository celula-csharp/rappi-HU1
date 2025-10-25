# Rappi API

## Instalación

1. Clonar repositorio:

```bash
git clone https://github.com/celula-csharp/rappi-HU1.git
cd rappi-HU1
```

2. Restaura dependencias

```bash
dotnet restore
```

3. Configura la cadena de conexión en [appsettings.json](rappi.Api/appsettings.json)

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=rappi_db;user=root;password=;"
}
```

4. Aplica migraciones

```bash
dotnet ef database update Initial --project rappi.Infrastructure --startup-project rappi.Api
```

5. Ejecutar proyecto

```bash
dotnet run --project rappi.Api
```

---

## Ejecución y pruebas

### Swagger

Se pueden probar los endpoints directamente desde:

```
http://localhost:5239/swagger
```

### Postman

1. Abrir postman
2. Importar la colección desde [rappi.Api.postman_collection.json](docs/postman/rappi.Api.postman_collection.json)
3. Importar los test de postman desde [rappi.Api.postman_test_run.json](docs/postman/rappi.Api.postman_test_run.json)
4. (Si es necesario) Configura la variable `{{base_url}}` por el localhost en el que este corriendo la aplicación. Por ejemplo, `http://localhost:5239`