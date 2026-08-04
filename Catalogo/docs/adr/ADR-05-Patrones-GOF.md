# ADR-05: Incorporación de patrones GOF (Strategy + Decorator)

## Estado

Aceptado

## Fecha

2026-06-23

## Autor

David Morales Guerrero

---

## Contexto

MotoTrack cuenta con una arquitectura por capas documentada que incluye persistencia JSON, API REST con Swagger y un dashboard que muestra el estado de mantenimiento de cada motocicleta.

Actualmente, la lógica que determina si un mantenimiento está "VENCIDO", "PRÓXIMO" o "AL DÍA" utiliza un umbral fijo de 500 kilómetros hardcodeado dentro del helper `CalculadorEstadoMantenimiento`. Este valor no puede modificarse sin editar el código fuente.

Adicionalmente, las operaciones del repositorio de motocicletas (`MotocicletaRepository`) no registran trazabilidad. No es posible auditar cuándo se consultan, agregan, actualizan o eliminan motocicletas, lo que dificulta la depuración de errores de persistencia.

La Actividad #28 requiere incorporar dos patrones GOF de categorías distintas que resuelvan problemas reales del proyecto sin modificar la arquitectura existente.

El proyecto ha evolucionado de forma incremental: ADR-01 definió la arquitectura inicial por capas, ADR-03 formalizó dicho estilo arquitectónico y ADR-04 incorporó la API REST con Swagger. El presente ADR complementa estas decisiones incorporando patrones GOF que resuelven problemas específicos sin alterar la estructura base del sistema.

---

## Decisión

Se incorporan dos patrones GOF de categorías diferentes:

### Strategy (Behavioral) — Determinación de estado de mantenimiento

Se define la interfaz `IEstadoMantenimientoStrategy` con un método `DeterminarEstado(int kilometrajeActual, int kilometrajeProximo)` que encapsula la lógica de comparación entre el kilometraje actual de la motocicleta y el kilometraje estimado del próximo servicio.

| Estrategia | Umbral | Descripción |
|---|---|---|
| `DefaultEstadoStrategy` | 500 km | Comportamiento actual del sistema |
| `ConservadoraEstadoStrategy` | 1000 km | Alertas tempranas para motocicletas de alto uso |
| `EstrictaEstadoStrategy` | 200 km | Alertas solo cuando el servicio está muy próximo |

`DefaultEstadoStrategy` es la estrategia activa mediante inyección de dependencias. Las otras implementaciones existen para demostrar la extensibilidad del patrón — pueden activarse cambiando una línea en `Program.cs`.

El `CalculadorEstadoMantenimiento` ahora recibe `IEstadoMantenimientoStrategy` por constructor y delega la determinación de estado a la estrategia inyectada.

### Decorator (Structural) — Logging del repositorio de motocicletas

Se implementa `LoggingMotocicletaRepository` que envuelve cualquier implementación de `IMotocicletaRepository` y agrega mensajes de log antes y después de cada operación mediante `Console.WriteLine`.

| Capa | Función |
|---|---|
| `IMotocicletaRepository` (interfaz) | Define el contrato del repositorio |
| `MotocicletaRepository` (concreto) | Implementación JSON existente |
| `LoggingMotocicletaRepository` (decorador) | Envuelve el concreto y agrega trazabilidad |

El decorador se aplica en el Composition Root (`Program.cs`) sin modificar `MotocicletaService` ni los controladores. El servicio recibe el decorador como si fuera el repositorio real, y tanto el flujo MVC como la API REST quedan trazados.

---

## ¿Por qué?

**Strategy** porque el umbral de kilometraje para determinar el estado de mantenimiento es una regla de negocio que debe poder configurarse sin modificar el código del helper. Extraerla a una interfaz con implementaciones intercambiables permite cambiar el comportamiento del dashboard completo modificando únicamente la línea de registro en DI.

**Decorator** porque la trazabilidad de las operaciones de persistencia es una responsabilidad transversal (cross-cutting concern) que no debe mezclarse con la lógica de lectura y escritura de archivos JSON. Aplicarlo como decorador mantiene el repositorio original limpio y permite activar/desactivar el logging sin cambiar su código.

---

## Alternativas consideradas

| Alternativa | Descripción | Motivo de descarte |
|---|---|---|
| **Hardcodear umbral en configuración** | Leer el umbral desde `appsettings.json` | No es un patrón GOF. No cumple el requisito académico de la Actividad #28. |
| **ILogger en cada repositorio** | Inyectar `ILogger<T>` en los 7 repositorios existentes | Viola SRP. Mezcla persistencia con logging. Requiere modificar todos los repositorios. |
| **Proxy dinámico (DispatchProxy)** | Decorador genérico con Reflection | Viola la restricción de no usar Reflection. Menos claro académicamente. |
| **Strategy + Decorator (seleccionado)** | Strategy para umbral de mantenimiento. Decorator para logging del repositorio. | Sin Reflection. Sin librerías externas. Sin modificar arquitectura existente. |

---

## Consecuencias

### Positivas

- El umbral de 500 km ahora es configurable sin editar código fuente.
- Las 3 estrategias demuestran el patrón GOF Strategy con implementaciones reales.
- Todas las operaciones del repositorio de motocicletas quedan trazadas en consola.
- El decorador se aplica sin modificar `MotocicletaService`, `MotocicletaController` ni `MotocicletasApiController`.
- Si se requiere logging en otros repositorios, se aplica el mismo patrón.
- Cero cambios en modelos de dominio, persistencia JSON, API REST o Swagger.

### Negativas

- El decorador usa `Console.WriteLine` en lugar de `ILogger<T>` (intencional para evitar dependencias externas).
- `ConservadoraEstadoStrategy` y `EstrictaEstadoStrategy` existen como demostración pero no se usan activamente.
- El decorador agrega overhead mínimo de escritura en consola por cada operación de repositorio.

---

## Diagrama

```mermaid
flowchart TD
    subgraph "Strategy Pattern"
        S[IEstadoMantenimientoStrategy]
        S1[DefaultEstadoStrategy<br/>500 km]
        S2[ConservadoraEstadoStrategy<br/>1000 km]
        S3[EstrictaEstadoStrategy<br/>200 km]
        C[CalculadorEstadoMantenimiento]
        D[Dashboard<br/>HomeController + MotocicletaController]
    end

    subgraph "Decorator Pattern"
        I[IMotocicletaRepository]
        R[MotocicletaRepository<br/>JSON]
        L[LoggingMotocicletaRepository]
        SVC[MotocicletaService]
        CT[MotocicletaController<br/>+ MotocicletasApiController]
    end

    S --> S1
    S --> S2
    S --> S3
    S1 --> C
    C --> D

    I --> R
    L --> I
    R --> L
    SVC --> L
    CT --> SVC

    style S fill:#2a2a2a,stroke:#66bb6a,color:#f5f5f5
    style S1 fill:#2a2a2a,stroke:#66bb6a,color:#f5f5f5
    style S2 fill:#2a2a2a,stroke:#66bb6a,color:#f5f5f5
    style S3 fill:#2a2a2a,stroke:#66bb6a,color:#f5f5f5
    style C fill:#2a2a2a,stroke:#66bb6a,color:#f5f5f5
    style L fill:#2a2a2a,stroke:#ff7a00,color:#f5f5f5
    style R fill:#2a2a2a,stroke:#ab47bc,color:#f5f5f5
    style SVC fill:#2a2a2a,stroke:#4a9eff,color:#f5f5f5
    style CT fill:#2a2a2a,stroke:#4a9eff,color:#f5f5f5

---

*Para la identidad visual oficial de MotoTrack (colores, tipografía, logotipo), consultar [`BRAND_GUIDELINES.md`](../../branding/BRAND_GUIDELINES.md) — única fuente de verdad para los recursos gráficos del proyecto.*
```
