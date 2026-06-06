Vista Lógica

## Descripción

La vista lógica muestra los principales componentes funcionales de MotoTrack y las relaciones entre la interfaz de usuario, los controladores, los servicios, los repositorios y la persistencia de datos.

```mermaid
flowchart TD

    U[Usuario]

    U --> C1[AuthController]
    U --> C2[MotocicletaController]
    U --> C3[MantenimientoController]
    U --> C4[LecturaKilometrajeController]
    U --> C5[PerfilController]
    U --> C6[CatalogoController]
    U --> C7[HomeController]

    C1 --> S1[UsuarioService]

    C2 --> S2[MotocicletaService]
    C2 --> S5[ConfiguracionMantenimientoService]

    C3 --> S3[MantenimientoService]
    C3 --> S2

    C4 --> S4[LecturaKilometrajeService]
    C4 --> S2

    C5 --> S1

    S1 --> R1[UsuarioRepository]
    S2 --> R2[MotocicletaRepository]
    S3 --> R3[MantenimientoRepository]
    S4 --> R4[LecturaKilometrajeRepository]
    S5 --> R5[ConfiguracionMantenimientoRepository]

    R1 --> D1[usuarios.json]
    R2 --> D2[motocicletas.json]
    R3 --> D3[mantenimientos.json]
    R4 --> D4[lecturasKilometraje.json]
    R5 --> D5[configuracionesMantenimiento.json]
```

## Interpretación

MotoTrack utiliza una arquitectura por capas donde los usuarios interactúan con controladores MVC. Los controladores delegan la lógica de negocio a los servicios correspondientes y estos utilizan repositorios para acceder a la información almacenada en archivos JSON.