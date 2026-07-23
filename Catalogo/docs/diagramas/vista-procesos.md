# Vista de Procesos

## Descripción

La vista de procesos representa los flujos principales de interacción de un usuario dentro de MotoTrack, mostrando las actividades más importantes que puede realizar dentro del sistema.

```mermaid
flowchart TD

    A[Usuario] --> B[Iniciar Sesión]

    B --> C[Dashboard / Mis Motocicletas]

    C --> D[Registrar Motocicleta]
    C --> E[Editar Motocicleta]
    C --> F[Actualizar Kilometraje]
    C --> G[Registrar Mantenimiento]

    G --> H[Guardar Mantenimiento]
    H --> I[Consultar Historial]

    C --> J[Ver Gastos por Tipo]
    C --> K[Consultar Alertas]

    A --> L[Explorar Catálogo]
    L --> M[Iniciar Sesión / Adoptar Moto]

    A --> N[Ver Perfil]
    N --> O[Estadísticas de Cuenta]

    E --> C
    F --> C
    I --> C
    J --> C
    K --> C
    M --> C
    O --> C

    P[Cliente HTTP / curl] --> Q[MotocicletasApiController]
    Q --> R[GET /api/motocicletas]
    Q --> S[GET /api/motocicletas/{id}]
    Q --> T[POST /api/motocicletas]
    Q --> U[PUT /api/motocicletas/{id}]
    Q --> V[DELETE /api/motocicletas/{id}]
```

## Interpretación

MotoTrack ofrece dos vías de interacción: la interfaz web (navegador) y la API REST. El flujo principal inicia con el inicio de sesión, desde donde el usuario accede al dashboard, administra motocicletas, registra mantenimientos y consulta gastos y alertas. Los usuarios no autenticados pueden explorar el catálogo público de motocicletas. La API REST expone los datos de motocicletas para integración programática.

Este flujo representa los escenarios de uso más importantes del sistema y sirve como elemento integrador de las demás vistas arquitectónicas (modelo 4+1 de Kruchten).
