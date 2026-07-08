# Enterprise Hub

Aplicacion web de gestion empresarial con frontend Angular y API ASP.NET Core.

## Estructura

- `EnterpriseHub.Api/`: API REST en ASP.NET Core con Entity Framework Core.
- `enterprise-hub-web/`: frontend Angular.

## Requisitos

- .NET SDK compatible con `net10.0`.
- Node.js y npm.

## Ejecutar la API

```bash
cd EnterpriseHub.Api
dotnet run
```

La API queda disponible por defecto en `http://localhost:5156`.

## Ejecutar el frontend

```bash
cd enterprise-hub-web
npm install
npm start
```

El frontend queda disponible en `http://localhost:4200`.

## Compilar

```bash
cd EnterpriseHub.Api
dotnet build
```

```bash
cd enterprise-hub-web
npm run build
```

## Base de datos

Por defecto la API usa base de datos en memoria (`DatabaseProvider: InMemory`). Para SQL Server se puede ajustar `DatabaseProvider` y `ConnectionStrings:EnterpriseHubDb` en `EnterpriseHub.Api/appsettings.json`.
