# MotoTrack

## Descripción

MotoTrack es una aplicación web desarrollada en ASP.NET Core MVC para ayudar a los motociclistas a llevar el control de sus motocicletas, registrar mantenimientos y dar seguimiento al kilometraje de cada unidad.

El proyecto fue desarrollado como parte de la materia de Arquitectura de Software y se encuentra en evolución continua mediante entregas incrementales y mejoras por sprint.

---

## Funcionalidades actuales

Actualmente MotoTrack permite:

* Registro de usuarios.
* Inicio y cierre de sesión.
* Registro de motocicletas.
* Edición de motocicletas.
* Eliminación de motocicletas.
* Registro de lecturas de kilometraje.
* Registro de mantenimientos.
* Consulta de historial de mantenimientos.
* Almacenamiento de información mediante archivos JSON.

---

## Arquitectura del proyecto

El sistema está organizado utilizando una arquitectura por capas para separar responsabilidades y facilitar el mantenimiento del código.

### Estructura principal

* **Catalogo**
  Contiene controladores, vistas y configuración general de la aplicación.

* **Catalogo.Application**
  Contiene los servicios que implementan la lógica de negocio.

* **Catalogo.Domain**
  Contiene los modelos e interfaces principales del sistema.

* **Catalogo.Infrastructure**
  Contiene los repositorios y la persistencia de datos.

---

## Tecnologías utilizadas

* ASP.NET Core MVC
* C#
* Razor Views
* Bootstrap
* JSON para persistencia local
* Git y GitHub para control de versiones

---

## Estado actual

MotoTrack se encuentra en desarrollo activo.

Las funcionalidades principales para la administración de motocicletas ya se encuentran operativas y actualmente se trabaja en nuevas mejoras relacionadas con visualización de información, experiencia de usuario y gestión de mantenimientos.

---

## Autor

David Morales Guerrero

Tecnológico del Software

Materia: Arquitectura de Software

---

## Uso de Inteligencia Artificial

Se utilizó ChatGPT como herramienta de apoyo para analizar problemas específicos de implementación, revisar decisiones arquitectónicas y apoyar en la elaboración de documentación técnica del proyecto.

Las decisiones de diseño, implementación y validación final fueron realizadas y verificadas por el autor.
