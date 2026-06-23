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

## Consideraciones

La inteligencia artificial fue utilizada como herramienta de apoyo para:

- Analizar alternativas arquitectónicas.
- Revisar consistencia entre documentación y código.
- Identificar posibles riesgos técnicos.
- Resolver dudas específicas durante la implementación.

La validación final de las decisiones arquitectónicas, la implementación del código, las pruebas y la documentación fueron realizadas por el autor del proyecto.
