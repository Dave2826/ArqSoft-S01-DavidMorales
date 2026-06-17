# ADR-03: Selección del Estilo Arquitectónico de MotoTrack

## Estado

Aceptado

## Fecha

2026-06-12

## Autor

David Morales Guerrero

---

## Contexto

MotoTrack es una aplicación web desarrollada en ASP.NET Core MVC cuyo propósito es ayudar a los motociclistas a administrar sus vehículos, registrar lecturas de kilometraje y llevar un control de los mantenimientos realizados.

Conforme el proyecto ha evolucionado, se han incorporado nuevas funcionalidades y se ha estructurado el sistema en diferentes proyectos que separan responsabilidades específicas.

Actualmente la solución se encuentra organizada mediante las siguientes capas:

- Presentación (ASP.NET Core MVC)
- Aplicación (Servicios de negocio)
- Dominio (Modelos e interfaces)
- Infraestructura (Persistencia y repositorios)

Dado que el proyecto continúa creciendo, resulta necesario definir formalmente el estilo arquitectónico utilizado para documentar la decisión y facilitar futuras modificaciones.

---

## Decisión

Se adopta una **Arquitectura por Capas (Layered Architecture)** como estilo arquitectónico principal para MotoTrack.

La arquitectura por capas divide el sistema en niveles con responsabilidades claramente definidas, permitiendo mantener una adecuada separación de intereses entre la interfaz de usuario, la lógica de negocio, el dominio del problema y la persistencia de datos.

La implementación actual de MotoTrack sigue este modelo mediante la siguiente estructura:

### Capa de Presentación

Responsable de la interacción con el usuario.

**Componentes:**

- Controllers
- Views
- ViewModels

### Capa de Aplicación

Responsable de coordinar los casos de uso y reglas de negocio.

**Componentes:**

- MotocicletaService
- MantenimientoService
- LecturaKilometrajeService
- ConfiguracionMantenimientoService

### Capa de Dominio

Responsable de representar las entidades y contratos principales del sistema.

**Componentes:**

- Models
- Interfaces

### Capa de Infraestructura

Responsable de la persistencia y acceso a datos.

**Componentes:**

- Repositories
- Archivos JSON de almacenamiento

---

## Justificación

La arquitectura por capas fue seleccionada porque proporciona una estructura clara y adecuada para el tamaño y complejidad actual de MotoTrack.

Las principales razones son:

- Facilita la separación de responsabilidades.
- Permite mantener una organización clara del código.
- Reduce el acoplamiento entre componentes.
- Facilita el mantenimiento y evolución del sistema.
- Es adecuada para un proyecto académico desarrollado por un solo integrante.
- Permite incorporar nuevas funcionalidades sin afectar significativamente otras partes del sistema.

Además, la estructura actual del proyecto ya implementa de forma natural este estilo arquitectónico, evitando refactorizaciones innecesarias y manteniendo la estabilidad del sistema.

---

## Alternativas Consideradas

### Alternativa 1: Arquitectura Hexagonal (Ports and Adapters)

La arquitectura hexagonal busca aislar completamente el dominio mediante puertos y adaptadores, permitiendo una mayor independencia tecnológica.

#### Motivo de descarte

Aunque ofrece beneficios importantes en proyectos grandes o con múltiples integraciones, su implementación incrementa significativamente la complejidad del sistema.

Para el alcance actual de MotoTrack, los beneficios obtenidos no justifican el costo de migración ni el aumento de complejidad de desarrollo y mantenimiento.

No obstante, la estructura actual del proyecto facilita una futura evolución hacia este estilo si el crecimiento del sistema lo requiere.

---

### Alternativa 2: Arquitectura de Microservicios

La arquitectura de microservicios divide el sistema en múltiples servicios independientes desplegados por separado.

#### Motivo de descarte

El tamaño actual de MotoTrack no requiere una separación distribuida de servicios.

Su adopción implicaría un aumento considerable en complejidad operativa, despliegue, monitoreo y mantenimiento.

---

### Alternativa 3: Arquitectura Cliente-Servidor Tradicional

La arquitectura cliente-servidor concentra la lógica principal en una única aplicación con poca separación interna.

#### Motivo de descarte

A medida que el sistema crece, este enfoque dificulta el mantenimiento y la evolución del software debido al incremento del acoplamiento entre componentes.

---

## Consecuencias

### Positivas

- Organización clara del proyecto.
- Separación adecuada de responsabilidades.
- Menor complejidad de desarrollo.
- Facilidad de mantenimiento.
- Curva de aprendizaje reducida.
- Escalabilidad suficiente para el alcance actual del sistema.
- Facilita futuras mejoras y refactorizaciones.

### Negativas

- Menor desacoplamiento que una arquitectura hexagonal.
- Dependencia moderada entre capas.
- Posible necesidad de evolución arquitectónica si el proyecto crece significativamente.

---

## Relación con la Implementación Actual

La arquitectura por capas se encuentra reflejada en la estructura real del proyecto:

```text
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
```

Esta organización soporta todas las funcionalidades actualmente implementadas en el sistema y mantiene una separación clara de responsabilidades.

---

## Diagramas Asociados

Esta decisión arquitectónica se complementa con el diagrama:

- Arquitectura por Capas de MotoTrack.