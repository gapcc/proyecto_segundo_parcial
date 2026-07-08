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
## Despliegue en Vercel

Este repositorio esta preparado para desplegar el frontend Angular en Vercel desde la raiz del proyecto usando `vercel.json`.

> La API esta hecha en ASP.NET Core. Vercel no ejecuta este backend .NET de forma nativa, por lo que debe publicarse aparte, por ejemplo en Azure App Service, Render, Railway, IIS o un VPS.

### Configuracion en Vercel

1. Sube el repositorio a GitHub.
2. En Vercel, importa el repositorio.
3. Deja la raiz del proyecto como root del repo. Vercel usara `vercel.json`.
4. Agrega la variable de entorno `API_BASE_URL` con la URL publica de la API, incluyendo `/api` al final.

Ejemplo:

```text
API_BASE_URL=https://tu-api.com/api
```

### Build usado por Vercel

```bash
cd enterprise-hub-web
npm run build:vercel
```

Ese comando genera `public/config.js` con la URL de la API y luego compila Angular.