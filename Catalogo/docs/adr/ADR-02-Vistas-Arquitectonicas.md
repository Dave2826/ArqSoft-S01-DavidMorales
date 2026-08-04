# ADR-02: Documentación arquitectónica mediante vistas para MotoTrack

## Estado

Aceptado

## Fecha

2026-06-05

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

## ¿Por qué?

La documentación arquitectónica mediante vistas permite comprender un sistema desde múltiples perspectivas complementarias, evitando la sobrecarga cognitiva de un solo diagrama monolítico. El modelo 4+1 de Kruchten separa la estructura lógica, la distribución física, el despliegue y los procesos del sistema, manteniendo los escenarios de uso como hilo conductor.

En MotoTrack, este enfoque facilita:
- Distinguir qué componentes son responsabilidad de cada capa arquitectónica.
- Visualizar cómo los controladores MVC y API se relacionan con los servicios y repositorios.
- Entender el flujo de ejecución desde el navegador hasta la persistencia JSON.
- Mantener la documentación sincronizada con la evolución del proyecto mediante diagramas Mermaid versionados junto al código.

---

## Alternativas consideradas

| Alternativa | Descripción | Motivo de descarte |
|---|---|---|
| **Documentación únicamente textual** | Describir la arquitectura solo con texto sin diagramas. | Dificulta la comprensión visual de la estructura y las relaciones entre componentes. Sin diagramas, la arquitectura es ambigua. |
| **Solo diagramas UML tradicionales** | Usar exclusivamente diagramas UML (clases, secuencia, paquetes). | UML muestra estructura estática pero no captura las múltiples perspectivas (física, despliegue, procesos) que requiere un sistema web completo como MotoTrack. |
| **Vistas arquitectónicas (seleccionado)** | Documentar el sistema con 4 vistas complementarias (lógica, física, despliegue, procesos) usando Mermaid. | Proporciona una visión integral: cada perspectiva responde preguntas distintas sobre la arquitectura. Los diagramas se mantienen en el repositorio y evolucionan con el código. |

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
* Consulta de gastos por tipo de mantenimiento.
* Explorador de catálogo de motocicletas (adopción).
* Dashboard con indicadores de estado de mantenimiento (6 tipos).
* Centro de alertas (vencidos/próximos).
* API REST con Swagger (`/api/motocicletas`, 5 endpoints).
* Landing page para usuarios no autenticados.
* Perfil de usuario con estadísticas.
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

---

## Diagrama

El siguiente diagrama muestra cómo las 4 vistas arquitectónicas cubren diferentes aspectos de MotoTrack y cómo se relacionan entre sí:

```mermaid
flowchart TD
    subgraph "Vista Lógica"
        C[Controladores MVC + API]
        S[Servicios de Aplicación]
        R[Repositorios]
    end

    subgraph "Vista Física"
        P1[Catalogo - MVC]
        P2[MotoTrack.Application]
        P3[MotoTrack.Domain]
        P4[MotoTrack.Infrastructure]
    end

    subgraph "Vista de Despliegue"
        K[Kestrel / dotnet run]
        N[Navegador / Cliente HTTP]
    end

    subgraph "Vista de Procesos"
        F1[Login]
        F2[Mis Motos / Dashboard]
        F3[Mantenimientos / API REST]
    end

    C --> S
    S --> R
    R --> P4
    P4 --> P2
    P2 --> P1
    N --> K
    K --> C
    F1 --> F2
    F2 --> F3

    style C fill:#2a2a2a,stroke:#ff7a00,color:#f5f5f5
    style S fill:#2a2a2a,stroke:#4a9eff,color:#f5f5f5
    style R fill:#2a2a2a,stroke:#66bb6a,color:#f5f5f5
    style K fill:#2a2a2a,stroke:#ab47bc,color:#f5f5f5
    style N fill:#2a2a2a,stroke:#ef5350,color:#f5f5f5

---

*Para la identidad visual oficial de MotoTrack (colores, tipografía, logotipo), consultar [`BRAND_GUIDELINES.md`](../../branding/BRAND_GUIDELINES.md) — única fuente de verdad para los recursos gráficos del proyecto.*
```
