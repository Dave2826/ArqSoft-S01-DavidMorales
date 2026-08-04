# ADR-07 — DashboardViewModel Technical Debt

## Estado

Accepted

## Fecha

2026-07-14

## Contexto

El Dashboard forma parte de la Web Application de MotoTrack. Durante la auditoría arquitectónica del proyecto se identificó una deuda técnica relacionada con la organización del DashboardViewModel. Esta deuda fue documentada para planificar una futura refactorización.

---

# Deuda técnica

## Nombre

DashboardViewModel definido dentro del HomeController.

## Qué es

El archivo `HomeController.cs` contiene dos responsabilidades completamente distintas: la lógica de presentación del Dashboard y la definición completa de `DashboardViewModel`, que está anidada dentro de la misma clase del controller. Esto impide su reutilización en otros controladores — `MotocicletaController` construye un `Dictionary<Guid, EstadoMantenimientoResult>` manualmente porque no puede usar el mismo ViewModel.

Además, tanto `DashboardViewModel` como `EstadoMantenimientoResult` definen numerosas propiedades repetitivas que siguen exactamente el mismo patrón para cada tipo de mantenimiento:

- `Ultimo{Type}` (string)
- `Proximo{Type}` (string)
- `Estado{Type}` (string)
- `{Type}EsEstimado` (bool)

Esto significa que agregar un nuevo tipo de mantenimiento requiere modificar múltiples clases y archivos relacionados (`CalculadorEstadoMantenimiento`, `EstadoMantenimientoResult`, `DashboardViewModel`, y la Vista Razor).

**Evidencia concreta:**
- `HomeController.cs:243-360`: clase `DashboardViewModel` anidada en el mismo archivo que `HomeController`.
- `HomeController.cs:76-175`: asignación manual de cada propiedad desde el resultado del cálculo.
- `MotocicletaController.cs:53-60`: construcción manual de `Dictionary<Guid, EstadoMantenimientoResult>` porque `DashboardViewModel` no es reutilizable.

## ¿Por qué existe?

Durante el desarrollo inicial, la prioridad era tener un Dashboard funcional lo antes posible. Definir el ViewModel cerca del controller aceleró la implementación porque evitaba navegar entre archivos. Las propiedades repetitivas surgieron naturalmente al modelar cada tipo de mantenimiento como una propiedad individual con nombre — una decisión que priorizó la claridad inmediata sobre la mantenibilidad a largo plazo.

## Costo de no pagarla

- Agregar un nuevo tipo de mantenimiento requiere modificar 4 archivos: el enum `MaintenanceType`, el `CalculadorEstadoMantenimiento`, el `EstadoMantenimientoResult`, y el `DashboardViewModel`, además de la vista Razor. Cualquier omisión produce un bug silencioso.
- El ViewModel no es reutilizable: `MotocicletaController.Index()` tiene que construir manualmente un `Dictionary<Guid, EstadoMantenimientoResult>` porque no puede usar `DashboardViewModel` directamente.
- El archivo `HomeController.cs` mezcla responsabilidades: lógica de presentación, construcción de ViewModel, y definición del tipo de datos. Violación directa del principio de responsabilidad única.
- Pruebas unitarias dificultadas: no se puede instanciar `DashboardViewModel` sin referenciar `HomeController`.

## Impacto

| Dimensión | Impacto |
|---|---|
| Mantenibilidad | Cada nuevo tipo de mantenimiento multiplica los puntos de modificación. Alta probabilidad de errores de copia/pega. |
| Reutilización | ViewModel encapsulado dentro del controller. No disponible para otros componentes (`MotocicletaController`, API). |
| Escalabilidad | La estructura explota linealmente con cada nuevo tipo de mantenimiento. |
| Separación de responsabilidades | Controller conoce y define la estructura de datos de la Vista. El ViewModel debería ser un tipo independiente. |

## Propuesta de solución

Aplicar **Extract Class** para mover `DashboardViewModel` a un archivo independiente dentro de la capa correspondiente. Esto desacopla el controller del ViewModel, mejora la separación de responsabilidades, aumenta la reutilización y facilita el mantenimiento futuro.

Como posible evolución futura, las propiedades repetitivas podrían reemplazarse por una colección tipada que permita agregar nuevos tipos de mantenimiento sin modificar la estructura del ViewModel.

## Estado actual

Esta deuda técnica ha sido identificada y documentada. No representa un error funcional: el Dashboard funciona correctamente con la estructura actual. Su resolución queda planificada para una futura refactorización.

---

*Para la identidad visual oficial de MotoTrack (colores, tipografía, logotipo), consultar [`BRAND_GUIDELINES.md`](../../branding/BRAND_GUIDELINES.md) — única fuente de verdad para los recursos gráficos del proyecto.*