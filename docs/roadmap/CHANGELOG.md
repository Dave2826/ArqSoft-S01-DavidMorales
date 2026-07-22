# Changelog

## Objetivo

Registrar cronológicamente todas las versiones y cambios significativos del proyecto MotoTrack, permitiendo rastrear la evolución del proyecto.

## Alcance

Este documento cubre todas las versiones publicadas de MotoTrack, incluyendo funcionalidades, correcciones, cambios arquitectónicos y documentales.

## Formato

Cada versión sigue la estructura:

```markdown
## [Versión] — Fecha

### Added
- Nuevas funcionalidades.

### Changed
- Cambios en funcionalidades existentes.

### Fixed
- Correcciones de errores.

### Docs
- Cambios en documentación.

### Chore
- Tareas de mantenimiento.
```

## Versiones

### [0.1.0] — 2026-07-07

#### Added
- Estructura base de documentación del proyecto.
- Estándares de desarrollo, documentación y Git.
- Roadmap, backlog y changelog.
- Modelo C4 con niveles 1, 2 y 3.

#### Docs
- README principal con descripción del proyecto.
- ADR 01-05 con decisiones arquitectónicas.
- Diagramas de vistas arquitectónicas (4+1).

## Buenas prácticas

- Cada merge a `main` debe producir una entrada en el changelog.
- Las versiones siguen [SemVer](https://semver.org/).
- La fecha debe usar formato ISO 8601.

## Consideraciones finales

El changelog es el historial oficial del proyecto. Debe mantenerse actualizado y reflejar fielmente cada cambio integrado.