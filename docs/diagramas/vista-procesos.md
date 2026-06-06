# Vista de Procesos

## Descripción

La vista de procesos representa el flujo principal de interacción de un usuario dentro de MotoTrack, mostrando las actividades más importantes que puede realizar dentro del sistema.

```mermaid
flowchart TD

    A[Usuario] --> B[Iniciar Sesión]

    B --> C[Mis Motocicletas]

    C --> D[Registrar Motocicleta]

    C --> E[Editar Motocicleta]

    C --> F[Actualizar Kilometraje]

    C --> G[Registrar Mantenimiento]

    G --> H[Guardar Mantenimiento]

    H --> I[Consultar Historial]

    E --> C
    F --> C
    I --> C
```

## Interpretación

El proceso principal de MotoTrack inicia cuando el usuario accede al sistema mediante el inicio de sesión. Una vez autenticado, puede administrar sus motocicletas registradas, actualizar kilometraje, registrar mantenimientos y consultar el historial de servicios realizados.

Este flujo representa los escenarios de uso más importantes del sistema y sirve como elemento integrador de las demás vistas arquitectónicas.
```