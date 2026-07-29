# ADR-06: Technical Debt

## Estado

Accepted

## Fecha

2026-07-14

## Contexto

MotoTrack utiliza actualmente persistencia basada en archivos JSON. Durante la auditoría arquitectónica del proyecto se identificó una deuda técnica relacionada con la configuración utilizada por los repositorios de infraestructura. Esta deuda fue documentada para facilitar su futura refactorización.

---

# Deuda Técnica 1

## Nombre

Rutas de archivos JSON hardcodeadas.

## Qué es

Actualmente, los repositorios de infraestructura definen la ubicación de los archivos JSON directamente en el código fuente mediante rutas físicas construidas a partir del directorio de trabajo actual. Cada repositorio contiene su propia copia de esta ruta, lo que significa que la información de configuración de persistencia está distribuida en lugar de centralizada.

## ¿Por qué existe?

Esta decisión fue consciente durante las primeras etapas del proyecto. La prioridad inicial era establecer un ciclo de desarrollo rápido utilizando persistencia basada en archivos JSON, sin introducir la complejidad de un sistema de configuración externo. Mantener las rutas en el código simplificó el arranque del proyecto y permitió cumplir los objetivos funcionales iniciales sin sobrecarga de infraestructura.

## Costo de no pagarla

- Cambiar la ubicación de los archivos JSON requiere modificar múltiples clases en lugar de un solo punto de configuración.
- Cada nuevo repositorio debe replicar el mismo patrón de ruta hardcodeada, perpetuando la duplicación.
- Migrar el proyecto a un entorno con estructura de directorios diferente (Docker, contenedores, servidores remotos) exige recompilar en lugar de reconfigurar.
- El proyecto queda atado a la estructura física del directorio de trabajo, lo que reduce su portabilidad.

## Impacto

Esta deuda afecta principalmente la mantenibilidad, portabilidad, escalabilidad y facilidad de configuración del proyecto. El costo de mantener las rutas hardcodeadas aumentará conforme el proyecto crezca y se agreguen nuevos repositorios o entornos de despliegue.

## Propuesta de solución

La solución recomendada consiste en centralizar la configuración de rutas moviéndola hacia `appsettings.json` y utilizando el sistema de configuración de ASP.NET Core. Los repositorios recibirán la ruta mediante el patrón Options (`IOptions<T>`), eliminando cualquier referencia directa al sistema de archivos en el código de infraestructura. La configuración dejará de depender del código fuente y pasará a formar parte de la infraestructura de la aplicación, favoreciendo una arquitectura más flexible, mantenible y preparada para futuras migraciones.

## Estado actual

Esta deuda técnica ha sido identificada y documentada. Su resolución queda planificada para una futura refactorización. La deuda no representa un riesgo funcional inmediato. Su implementación fue pospuesta conscientemente para priorizar el desarrollo de funcionalidades del proyecto durante las primeras etapas.

---

*Para la identidad visual oficial de MotoTrack (colores, tipografía, logotipo), consultar [`BRAND_GUIDELINES.md`](../../branding/BRAND_GUIDELINES.md) — única fuente de verdad para los recursos gráficos del proyecto.*