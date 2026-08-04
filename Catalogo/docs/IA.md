# Declaración de Uso de Inteligencia Artificial

## Proyecto

MotoTrack – Sistema de gestión de motocicletas y mantenimientos.

---

# Actividad #20 - Estilo Arquitectónico

## IA utilizada

ChatGPT (OpenAI)

## Propósito

Apoyar en el análisis de alternativas arquitectónicas para el proyecto MotoTrack, particularmente en la comparación entre Arquitectura por Capas y Arquitectura Hexagonal, con el fin de evaluar ventajas, desventajas, costos de implementación y consecuencias para el desarrollo futuro del sistema.

## Prompt utilizado

Analiza la arquitectura actual de MotoTrack y compara la Arquitectura por Capas con la Arquitectura Hexagonal. Identifica ventajas, desventajas, impacto en mantenimiento, escalabilidad y complejidad, para determinar cuál se adapta mejor al estado actual del proyecto.

## Archivos utilizados

* Estructura actual del repositorio MotoTrack.
* ADRs previamente documentados.
* Diagramas arquitectónicos existentes.

## Resultado

La IA fue utilizada como apoyo para analizar distintas alternativas arquitectónicas y comprender las implicaciones de una posible migración hacia Arquitectura Hexagonal. La decisión final fue tomada por el autor después de revisar la estructura real del proyecto, concluyendo que la Arquitectura por Capas representa de manera más adecuada la implementación actual de MotoTrack y sus necesidades presentes.

---

# Actividad #24 - Incorporación de API REST

## IA utilizada

ChatGPT (OpenAI)

## Propósito

Apoyar en el diseño e implementación de una API REST para MotoTrack utilizando ASP.NET Core Web API y Swagger/OpenAPI, así como en la documentación de la decisión arquitectónica mediante un ADR.

## Prompt utilizado

Analiza la arquitectura actual de MotoTrack y propone una implementación mínima de API REST que cumpla con los requisitos de la actividad. Incluye Swagger para documentación, define los endpoints necesarios y genera una propuesta de ADR justificando la decisión.

## Archivos utilizados

* Program.cs
* MotoTrack.csproj
* Controllers/Api/MotocicletasApiController.cs
* docs/adr/ADR-04-Incorporacion-API-REST.md
* Repositorio MotoTrack
* Servicios existentes de motocicletas

## Resultado

La IA fue utilizada como apoyo técnico para planificar la incorporación de una API REST, la configuración de Swagger/OpenAPI y la elaboración de la documentación arquitectónica correspondiente.

Las recomendaciones generadas por la IA fueron revisadas, adaptadas e implementadas por el autor dentro del proyecto MotoTrack, validando manualmente la compilación, el funcionamiento de los endpoints REST y la documentación generada mediante Swagger UI.

La decisión final sobre la arquitectura, los endpoints expuestos y la documentación incluida en el repositorio fue tomada por el autor del proyecto.

---

# Actividad #28 — Patrones GOF

## IA utilizada

ChatGPT (OpenAI)

## Propósito

Apoyar en el análisis de patrones GOF aplicables a problemas reales del proyecto MotoTrack, evaluando alternativas como Factory Method + Decorator frente a Strategy + Decorator, así como en la revisión de consistencia arquitectónica y validación de riesgos antes de la implementación.

## Prompt utilizado

Identifica los problemas actuales de MotoTrack relacionados con el umbral de kilometraje hardcodeado en el cálculo de estado de mantenimiento y la falta de trazabilidad en las operaciones del repositorio de motocicletas. Evalúa qué patrones GOF de categorías distintas (behavioral y structural) podrían aplicarse para resolver estos problemas sin modificar la arquitectura por capas existente ni los modelos de dominio.

## Archivos utilizados

* CalculadorEstadoMantenimiento.cs
* MotocicletaRepository.cs
* IMotocicletaRepository.cs
* docs/adr/ADR-05-Patrones-GOF.md
* Repositorio MotoTrack

## Resultado

La IA fue utilizada como apoyo para evaluar patrones candidatos, comparar alternativas de implementación y validar la consistencia arquitectónica de la solución propuesta. La decisión final de adoptar Strategy + Decorator, la implementación concreta de cada patrón y las pruebas fueron realizadas por el autor del proyecto.

---

## Consideraciones

La inteligencia artificial fue utilizada como herramienta de apoyo para:

- Analizar alternativas arquitectónicas.
- Revisar consistencia entre documentación y código.
- Identificar posibles riesgos técnicos.
- Resolver dudas específicas durante la implementación.

La validación final de las decisiones arquitectónicas, la implementación del código, las pruebas y la documentación fueron realizadas por el autor del proyecto.

---

*Para la identidad visual oficial de MotoTrack (colores, tipografía, logotipo), consultar [`BRAND_GUIDELINES.md`](../branding/BRAND_GUIDELINES.md) — única fuente de verdad para los recursos gráficos del proyecto.*
