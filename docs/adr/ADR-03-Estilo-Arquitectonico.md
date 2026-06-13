ADR-03: Selección del estilo arquitectónico para MotoTrack
Estado

Aceptado

Fecha

Junio 2026

Autor

David Morales Guerrero

Contexto

MotoTrack es una aplicación web desarrollada en ASP.NET Core MVC cuyo propósito es ayudar a los motociclistas a administrar sus vehículos, registrar lecturas de kilometraje y llevar un control de los mantenimientos realizados.

Conforme el proyecto ha evolucionado, se han incorporado nuevas funcionalidades y se ha estructurado el sistema en diferentes proyectos que separan responsabilidades específicas.

Actualmente la solución se encuentra organizada mediante las siguientes capas:

Presentación (ASP.NET Core MVC)
Aplicación (Servicios de negocio)
Dominio (Modelos e interfaces)
Infraestructura (Persistencia y repositorios)

Dado que el proyecto continúa creciendo, resulta necesario definir formalmente el estilo arquitectónico utilizado para documentar la decisión y facilitar futuras modificaciones.

Decisión

Se adopta una Arquitectura por Capas (Layered Architecture) como estilo arquitectónico principal para MotoTrack.

La arquitectura por capas divide el sistema en niveles con responsabilidades claramente definidas, permitiendo mantener una adecuada separación de intereses entre la interfaz de usuario, la lógica de negocio, el dominio del problema y la persistencia de datos.

La implementación actual de MotoTrack sigue este modelo mediante la siguiente estructura:

Capa de Presentación

Responsable de la interacción con el usuario.

Componentes:

Controllers
Views
ViewModels
Capa de Aplicación

Responsable de coordinar los casos de uso y reglas de negocio.

Componentes:

MotocicletaService
MantenimientoService
LecturaKilometrajeService
ConfiguracionMantenimientoService
Capa de Dominio

Responsable de representar las entidades y contratos principales del sistema.

Componentes:

Models
Interfaces
Capa de Infraestructura

Responsable de la persistencia y acceso a datos.

Componentes:

Repositories
Archivos JSON de almacenamiento
Justificación

La arquitectura por capas fue seleccionada porque proporciona una estructura clara y adecuada para el tamaño y complejidad actual de MotoTrack.

Las principales razones son:

Facilita la separación de responsabilidades.
Permite mantener una organización clara del código.
Reduce el acoplamiento entre componentes.
Facilita el mantenimiento y evolución del sistema.
Es adecuada para un proyecto académico desarrollado por un solo equipo.
Permite incorporar nuevas funcionalidades sin afectar significativamente otras partes del sistema.

Además, la estructura actual del proyecto ya implementa de forma natural este estilo arquitectónico, evitando refactorizaciones innecesarias.

Alternativas consideradas
Alternativa 1: Arquitectura Hexagonal (Ports and Adapters)

Consiste en aislar completamente el dominio mediante puertos y adaptadores que desacoplan la lógica de negocio de tecnologías externas.

Motivo de descarte:

Aunque ofrece mayor independencia tecnológica y escalabilidad, incrementa la complejidad del proyecto y agrega capas adicionales que actualmente no son necesarias para los objetivos de MotoTrack.

La arquitectura actual satisface adecuadamente los requisitos funcionales sin requerir dicho nivel de abstracción.

Alternativa 2: Arquitectura de Microservicios

Consiste en dividir el sistema en múltiples servicios independientes.

Motivo de descarte:

El tamaño actual del proyecto no justifica la complejidad operativa asociada a la gestión de múltiples servicios, despliegues independientes y comunicación distribuida.

Alternativa 3: Arquitectura Cliente-Servidor Tradicional

Consiste en concentrar la mayor parte de la lógica en una única aplicación monolítica con mínima separación interna.

Motivo de descarte:

Dificulta el mantenimiento y crecimiento del sistema conforme aumenta el número de funcionalidades.

Consecuencias
Consecuencias positivas
Organización clara del proyecto.
Separación adecuada de responsabilidades.
Menor complejidad de desarrollo.
Facilidad de mantenimiento.
Curva de aprendizaje reducida.
Escalabilidad suficiente para el alcance actual del sistema.
Facilita futuras mejoras y refactorizaciones.
Consecuencias negativas
Menor desacoplamiento que una arquitectura hexagonal.
Dependencia moderada entre capas.
Posible necesidad de evolución arquitectónica si el proyecto crece significativamente.
Relación con MotoTrack

La arquitectura por capas se encuentra implementada actualmente mediante la siguiente estructura:

MotoTrack
│
├── Presentación (MVC)
│   ├── Controllers
│   ├── Views
│   └── ViewModels
│
├── MotoTrack.Application
│   └── Services
│
├── MotoTrack.Domain
│   ├── Models
│   └── Interfaces
│
└── MotoTrack.Infrastructure
    ├── Repositories
    └── Persistencia JSON