# Vista Física

## Descripción

La vista física representa la organización de los proyectos que conforman la solución MotoTrack y la ubicación de los archivos de persistencia utilizados por el sistema.

```mermaid
flowchart TB

    A[MotoTrack]

    A --> B[Catalogo]
    A --> C[MotoTrack.Application]
    A --> D[MotoTrack.Domain]
    A --> E[MotoTrack.Infrastructure]

    B --> F[Controllers]
    B --> F1[Controllers/Api]
    B --> G[Views]
    B --> H[Helpers]
    B --> I[Data]
    B --> J[docs]

    I --> J1[usuarios.json]
    I --> J2[motocicletas.json]
    I --> J3[mantenimientos.json]
    I --> J4[lecturasKilometraje.json]
    I --> J5[configuracionesMantenimiento.json]
    I --> J6[MotoTrack.db]

    C --> K[Services]

    D --> L[Models]
    D --> M[Interfaces]

    E --> N1[Repositories]
    E --> N2[Persistence]
    E --> N3[Decorators]

    N2 --> O1[MotoTrackDbContext]
    N2 --> O2[Configurations]
    N2 --> O3[EF Repositories]
    N2 --> O4[Migrations]
```

## Interpretación

MotoTrack se encuentra dividido en múltiples proyectos que permiten separar responsabilidades. La capa web (`Catalogo`) contiene los controladores MVC y API, vistas, helpers, la documentación técnica y los archivos de persistencia (JSON y SQLite). La capa de aplicación (`MotoTrack.Application`) contiene la lógica de negocio mediante servicios. La capa de dominio (`MotoTrack.Domain`) define los modelos e interfaces del sistema. La capa de infraestructura (`MotoTrack.Infrastructure`) implementa los repositorios JSON, los repositorios Entity Framework Core con sus configuraciones Fluent API, el DbContext, las migraciones y el decorador de trazabilidad. El proveedor de persistencia activo se selecciona mediante la clave `Persistence:Provider` en `appsettings.json`.
