# ADR-04: Incorporación de API REST

## Estado

Aceptado

## Fecha

2026-06-19

## Autor

David Morales Guerrero

---

## Contexto

Hasta ahora toda la interacción con MotoTrack ha sido exclusivamente a través del navegador mediante formularios Razor y controladores MVC tradicionales.

La Actividad #24 requiere exponer endpoints HTTP públicos que permitan la integración programática con el sistema, posibilitando que aplicaciones externas o scripts puedan consultar y manipular los datos de motocicletas sin necesidad de una interfaz gráfica.

---

## Decisión

Se implementa una API REST utilizando controladores Web API de ASP.NET Core, documentada con Swagger/OpenAPI mediante el paquete Swashbuckle.AspNetCore v10.2.2.

La API se despliega dentro del mismo proyecto web existente, compartiendo el mismo proceso y puerto, sin necesidad de un proyecto separado. Esta API se integra dentro de la Capa de Presentación de la Arquitectura por Capas definida y formalizada en ADR-03.

### Detalles técnicos

| Aspecto | Decisión |
|---|---|
| Formato | JSON (`application/json`) |
| Ruta base | `/api/motocicletas` |
| Controlador | `MotocicletasApiController` en `Controllers/Api/` |
| Recurso inicial | Motocicletas (único recurso en esta fase) |
| Endpoints | 5 (GET all, GET by id, POST, PUT, DELETE) |
| PATCH | No implementado — PUT cubre las necesidades actuales |
| DTOs | No se utilizan — se expone el modelo `Motocicleta` directamente |
| Documentación | Swagger UI en `/swagger` (documento OpenAPI en `/swagger/v1/swagger.json`) |
| Paquete NuGet | `Swashbuckle.AspNetCore` v10.2.2 |
| Autenticación | No implementada en esta fase |
| Versionado | No implementado en esta fase |

---

## ¿Por qué?

La implementación de una API REST permite que aplicaciones externas, scripts de automatización o futuros clientes móviles puedan interactuar con MotoTrack de forma programática. La exposición de endpoints estandarizados facilita la integración con otros sistemas y sienta las bases para una posible evolución del proyecto hacia una arquitectura más distribuida.

Swagger UI proporciona una interfaz de navegación y prueba interactiva que simplifica el desarrollo y depuración de los endpoints sin requerir herramientas externas.

---

## Alternativas consideradas

| Alternativa | Descripción | Motivo de descarte |
|---|---|---|
| **SOAP** | Protocolo XML sobre HTTP con WSDL para contratos estrictos. Soporte nativo en .NET Framework (WCF). | No se alinea con el stack actual basado en JSON y HTTP REST utilizado por MotoTrack. Sobredimensionado para un proyecto con persistencia JSON. |
| **Minimal APIs** | API sobre HTTP con menos ceremonia (sin controllers, lambdas directas en Program.cs). | Escalabilidad limitada al crecer los endpoints. Difícil de testear unitariamente. Pierde organización por controlador. MotoTrack puede crecer en número de endpoints. |
| **GraphQL** | Consultas sobre un solo endpoint con schema tipado (SDL). Cliente solicita exactamente los datos que necesita. | Complejidad de implementación (resolvers, DataLoader, N+1). Caché HTTP difícil. Sobrecarga cognitiva. Los recursos de MotoTrack son planos con relaciones simples. |
| **REST (seleccionado)** | JSON/HTTP con semántica de verbos (GET/POST/PUT/DELETE). Stateless, cacheable, ampliamente soportado. | — |

---

## Consecuencias

### Positivas

- Los endpoints API son accesibles desde cualquier cliente HTTP (curl, Postman, aplicaciones web).
- Swagger UI permite explorar y probar los endpoints sin configuración adicional.
- No se modificaron los controladores MVC existentes ni la lógica de negocio.
- El recurso API comparte el mismo servicio (`MotocicletaService`) que la aplicación web.
- La estructura `Controllers/Api/` permite agregar nuevos recursos siguiendo el mismo patrón.

### Negativas

- Al no usar DTOs, los campos internos del modelo (`Id`, `FechaRegistro`, `UsuarioId`) son editables vía PUT/POST.
- Swagger UI queda accesible en producción (riesgo controlado, sin datos sensibles).
- No hay autenticación ni autorización en los endpoints.
- No hay versionado — cambios futuros en los endpoints podrían romper clientes existentes.

### Deuda técnica y expansiones futuras

- `GastoService` e `IGastoRepository` existen en el proyecto pero no participan en la API actual. Queda como posible expansión agregar endpoints de gastos siguiendo el mismo patrón.
- La incorporación de la API requiere reflejar el nuevo componente en las vistas arquitectónicas documentadas en ADR-02 (especialmente vista lógica y vista de despliegue).

---

## Diagrama

El siguiente diagrama representa el pipeline completo de una solicitud HTTP a la API REST de MotoTrack:

```mermaid
flowchart LR
    Client["Cliente HTTP<br/>(curl, Postman, app)"]
    Swagger["Swagger UI<br/>/swagger"]
    ASP["Middleware ASP.NET Core<br/>Kestrel + Routing"]
    API["MotocicletasApiController<br/>/api/motocicletas"]
    Service["MotocicletaService"]
    Repo["MotocicletaRepository"]
    JSON["Archivo JSON<br/>Catalogo/Data/motocicletas.json"]

    Client --> ASP
    Swagger --> ASP
    ASP --> API
    API --> Service
    Service --> Repo
    Repo --> JSON

    style ASP fill:#2a2a2a,stroke:#ab47bc,color:#f5f5f5
    style API fill:#2a2a2a,stroke:#ff7a00,color:#f5f5f5
    style Swagger fill:#2a2a2a,stroke:#4a9eff,color:#f5f5f5
    style Service fill:#2a2a2a,stroke:#66bb6a,color:#f5f5f5
    style Repo fill:#2a2a2a,stroke:#ef5350,color:#f5f5f5
    style JSON fill:#2a2a2a,stroke:#ef5350,color:#f5f5f5
```

El diagrama anterior muestra el flujo completo de una solicitud HTTP: el cliente o Swagger UI llegan al middleware ASP.NET Core (Kestrel + Routing), que direcciona la petición al controlador API. Este delega en el servicio existente, el cual utiliza el repositorio para leer o escribir en el archivo JSON. La API se integra sin modificar la arquitectura existente.

---

## Uso de Inteligencia Artificial

El uso de IA relacionado con esta actividad se encuentra documentado en `IA.md`.

---

*Para la identidad visual oficial de MotoTrack (colores, tipografía, logotipo), consultar [`BRAND_GUIDELINES.md`](../../branding/BRAND_GUIDELINES.md) — única fuente de verdad para los recursos gráficos del proyecto.*
