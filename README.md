<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="branding/logo/social/mototrack-github-banner.svg">
    <img src="branding/logo/social/mototrack-github-banner.svg" alt="MotoTrack Banner" width="100%">
  </picture>
</p>

<br>

<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="branding/logo/primary/mototrack-logo-primary.svg">
    <img src="branding/logo/primary/mototrack-logo-primary.svg" alt="MotoTrack Logo" height="48">
  </picture>
</p>

# MotoTrack

**Smart motorcycle maintenance tracking.**

<p align="center">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white" alt=".NET 10">
  <img src="https://img.shields.io/badge/ASP.NET_Core-MVC-512BD4?logo=dotnet&logoColor=white" alt="ASP.NET Core MVC">
  <img src="https://img.shields.io/badge/EF_Core-SQLite-0769AD?logo=sqlite&logoColor=white" alt="EF Core SQLite">
  <img src="https://img.shields.io/badge/license-MIT-green" alt="License MIT">
</p>

---

## Descripción

MotoTrack es una aplicación web **ASP.NET Core MVC** para la gestión integral del mantenimiento de motocicletas. Permite registrar vehículos, controlar el kilometraje, programar servicios, visualizar alertas inteligentes de mantenimiento, consultar el historial y gestionar gastos. Incluye una **API REST** documentada con **Swagger/OpenAPI**.

Técnicamente es un proyecto por capas con 5 proyectos independientes, dos proveedores de persistencia seleccionables por configuración (JSON y Entity Framework Core + SQLite) y patrones de diseño aplicados sobre una base organizada por ADR y diagramas.

---

## Vista del sistema

![Dashboard principal](Catalogo/screenshots/dashboard-principal.png)

Panel principal: resume el estado de mantenimiento de la motocicleta, muestra alertas activas y los servicios recientes.

---

## Funcionalidades

- Registro e inicio de sesión (sessions).
- Gestión de motocicletas e imágenes.
- Registro de kilometraje.
- Mantenimientos e historial.
- Alertas de mantenimiento inteligentes.
- Gestión de gastos.
- Dashboard de estado general.
- Perfil de usuario.
- API REST documentada con Swagger/OpenAPI.

---

## Capturas

#### Mis motocicletas

Vista de los vehículos registrados con su estado y accesos rápidos a historial, gastos, kilometraje y mantenimientos.

![Mis motocicletas](Catalogo/screenshots/mis-motocicletas.png)

#### Historial de mantenimientos

Línea de tiempo con los servicios realizados: fecha, kilometraje, categoría, proveedor y observaciones.

![Historial de mantenimientos](Catalogo/screenshots/historial-mantenimientos.png)

#### Swagger UI

Documentación interactiva de la API REST.

![Swagger UI](Catalogo/screenshots/swagger-ui.png)

---

## Arquitectura

MotoTrack está organizado siguiendo una **arquitectura por capas** alineada con principios de *Clean Architecture*:

| Capa | Proyecto | Responsabilidad |
|------|----------|-----------------|
| Presentación | `Catalogo/` (web) | Controladores MVC/API, vistas Razor, Swagger |
| Aplicación | `MotoTrack.Application/` | Servicios de aplicación y strategies |
| Dominio | `MotoTrack.Domain/` | Entidades, interfaces e contratos de alta nivel |
| Infraestructura | `MotoTrack.Infrastructure/` | Repositories JSON, EF Core y decorators |

Patrones aplicados: **Repository Pattern** (abstrae el almacenamiento), **Strategy** (cálculo del estado de mantenimiento), **Decorator** (trazabilidad del repositorio de motos) y **Dependency Injection** nativa.

La profundidad técnica (motivo de cada decisión) queda delegada a los [ADR](Catalogo/docs/adr/) y los [diagramas](Catalogo/docs/diagramas/).

---

## Stack tecnológico

- ASP.NET Core MVC (.NET 10) y C#
- Razor Views + Bootstrap
- Entity Framework Core + SQLite
- JSON (proveedor de persistencia alternativo)
- Swagger / OpenAPI
- xUnit + Moq (pruebas)
- GitHub Actions (CI)

---

## API REST y persistencia

API REST documentada con Swagger/OpenAPI, disponible en `/swagger`, con los endpoints `api/motocicletas`:

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/motocicletas` | Listar motocicletas |
| GET | `/api/motocicletas/{id}` | Obtener por id |
| POST | `/api/motocicletas` | Crear motocicleta |
| PUT | `/api/motocicletas/{id}` | Actualizar motocicleta |
| DELETE | `/api/motocicletas/{id}` | Eliminar motocicleta |

El almacenamiento se abstrae con **Repository Pattern** tras interfaces en la capa de Dominio y se selecciona por configuración (`Persistence:Provider` en `appsettings.json`):

- `Json` — proveedor alternativo (predeterminado).
- `EntityFramework` — base de datos relacional con **EF Core + SQLite**.

La lógica de negocio es independiente del proveedor activo. Detalle en [ADR-08](Catalogo/docs/adr/ADR-08-Migracion-EntityFrameworkCore.md).

---

## Ejecución

### Requisitos

- .NET SDK 10

### Compilar y ejecutar (desde la raíz del repositorio)

```
dotnet restore Catalogo/MotoTrack.slnx
dotnet build Catalogo/MotoTrack.slnx
dotnet run --project Catalogo/MotoTrack.csproj
```

Instrucciones más detalladas (setup inicial, datos de ejemplo) en [LOCAL_DEMO.md](Catalogo/docs/demo/LOCAL_DEMO.md).

---

## Documentación

| Categoría | Documento |
|-----------|-----------|
| Decisiones arquitectónicas (ADR) | [`Catalogo/docs/adr/`](Catalogo/docs/adr/) |
| Diagramas (4+1 / C4) | [`Catalogo/docs/diagramas/`](Catalogo/docs/diagramas/) |
| Despliegue AWS (EC2) | [`AWS_DEPLOYMENT.md`](Catalogo/docs/deployment/AWS_DEPLOYMENT.md) |
| Demo local | [`LOCAL_DEMO.md`](Catalogo/docs/demo/LOCAL_DEMO.md) |
| Knowledge Base mantenimiento | [`Catalogo/docs/knowledge/`](Catalogo/docs/knowledge/) |
| Uso de IA en el desarrollo | [`IA.md`](Catalogo/docs/IA.md) |
| Branding / identidad visual | [`BRAND_GUIDELINES.md`](branding/BRAND_GUIDELINES.md) · [`README.md`](branding/README.md) |

---

## Estado del proyecto

- Arquitectura por capas establecida y documentada mediante ADR y diagramas.
- Implementación de Repository, Strategy, Decorator y Dependency Injection.
- API REST con Swagger/OpenAPI funcional sobre el proveedor JSON predeterminado y soporte de EF Core + SQLite seleccionable por configuración.
- Pruebas unitarias (xUnit + Moq) e integración continua (GitHub Actions) sobre `main`.
- Despliegue público en AWS EC2, realizado manualmente desde `main` (CI ≠ CD).

---

## Autor

**David Morales Guerrero**