# ADR-02: Documentación arquitectónica mediante vistas para MotoTrack

## Estado

Aceptado

## Fecha

Junio 2026

## Autor

David Morales Guerrero

---

## Contexto

MotoTrack es una aplicación web desarrollada en ASP.NET Core MVC cuyo objetivo es ayudar a los motociclistas a administrar sus motocicletas, registrar lecturas de kilometraje y llevar un historial de mantenimientos.

A medida que el proyecto ha evolucionado y se han incorporado nuevas funcionalidades, se ha vuelto necesario documentar formalmente la arquitectura del sistema para facilitar su comprensión, mantenimiento y crecimiento futuro.

Actualmente, MotoTrack utiliza una arquitectura por capas que separa la interfaz de usuario, la lógica de negocio, el dominio y la persistencia de datos.

---

## Decisión

Se adopta el uso de vistas arquitectónicas para documentar formalmente la arquitectura de MotoTrack.

Este enfoque se basa en el modelo 4+1 de Philippe Kruchten, el cual permite analizar y documentar un sistema desde diferentes perspectivas complementarias.

La documentación arquitectónica del proyecto estará compuesta por las siguientes vistas:

* Vista lógica.
* Vista física.
* Vista de despliegue.
* Vista de procesos.

Adicionalmente, se consideran los escenarios de uso principales del sistema como elemento integrador de las distintas vistas arquitectónicas.

---

## Alternativas consideradas

### Alternativa 1: Documentación únicamente textual

Consistía en documentar la arquitectura exclusivamente mediante descripciones escritas.

**Motivo de descarte:**

Dificulta la comprensión visual de la estructura del sistema y de las relaciones existentes entre sus componentes.

---

### Alternativa 2: Utilizar únicamente diagramas UML tradicionales

Consistía en representar toda la arquitectura únicamente mediante diagramas UML.

**Motivo de descarte:**

Aunque UML proporciona información útil, no permite mostrar con suficiente claridad las distintas perspectivas arquitectónicas requeridas para el proyecto.

---

### Alternativa 3: Utilizar vistas arquitectónicas complementarias

Consiste en documentar el sistema mediante diferentes vistas enfocadas en estructura, componentes, despliegue y comportamiento.

**Resultado:**

Alternativa seleccionada.

---

## Consecuencias

### Consecuencias positivas

* Facilita la comprensión general del sistema.
* Permite identificar claramente los componentes principales de MotoTrack.
* Mejora la comunicación técnica entre los participantes del proyecto.
* Facilita futuras modificaciones y ampliaciones del sistema.
* Proporciona documentación organizada y reutilizable para futuras etapas del desarrollo.

### Consecuencias negativas

* Requiere mantener actualizada la documentación conforme evolucione el proyecto.
* Incrementa ligeramente el esfuerzo de documentación y mantenimiento.

---

## Relación con MotoTrack

Las vistas arquitectónicas documentan la implementación actual del sistema, incluyendo:

* Gestión de usuarios.
* Registro e inicio de sesión.
* Administración de motocicletas.
* Registro de lecturas de kilometraje.
* Registro de mantenimientos.
* Consulta de historial de servicios.
* Persistencia de información mediante archivos JSON.
* Arquitectura en capas implementada en la solución.

La documentación generada servirá como referencia para futuras mejoras y para comprender la estructura actual del proyecto.

---

## Diagramas asociados

La presente decisión arquitectónica se complementa con los siguientes diagramas:

1. Vista lógica.
2. Vista física.
3. Vista de despliegue.
4. Vista de procesos.

Todos los diagramas se encuentran documentados mediante Mermaid y forman parte de la documentación técnica de MotoTrack.
