# Vista de Despliegue

## Descripción

La vista de despliegue muestra cómo se ejecuta actualmente MotoTrack y los elementos físicos involucrados en su funcionamiento.

```mermaid
flowchart TD

    U[Usuario]

    U --> B[Navegador Web<br/>o Cliente HTTP]

    B --> S[Kestrel - http://localhost:5141<br/>ASP.NET Core]

    S --> M1[Controladores MVC]
    S --> M2[MotocicletasApiController<br/>/api/motocicletas]
    S --> M3[Swagger UI<br/>/swagger]

    S --> J1[Data/usuarios.json]
    S --> J2[Data/motocicletas.json]
    S --> J3[Data/mantenimientos.json]
    S --> J4[Data/lecturasKilometraje.json]
    S --> J5[Data/configuracionesMantenimiento.json]
```

## Interpretación

MotoTrack se ejecuta en un entorno local de desarrollo utilizando Kestrel (`dotnet run`, perfil `http` en puerto 5141 o `https` en puerto 7116). Los usuarios acceden mediante un navegador web (interfaz MVC) o mediante clientes HTTP (API REST). Swagger UI está disponible en `/swagger` para explorar y probar los endpoints. Toda la información se almacena en archivos JSON dentro del directorio `Catalogo/Data/`. También existe un perfil `IIS Express` opcional en `launchSettings.json`.
