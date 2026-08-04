# Base de Conocimiento de Mantenimiento

## Propósito

El catálogo de mantenimiento (`MaintenanceCatalog`) es un componente interno de MotoTrack que centraliza las recomendaciones técnicas para los intervalos de servicio de cada tipo de mantenimiento. Su objetivo es proporcionar una fuente única de referencia para los cálculos de alertas, el estado del semáforo de mantenimiento y futuras funcionalidades como tooltips informativos o configuración personalizada.

## Recomendaciones, no valores absolutos

Los intervalos registrados en el catálogo representan valores conservadores basados en manuales de servicio de fabricantes y talleres especializados. No deben interpretarse como valores absolutos ni como sustitutos de las recomendaciones específicas de cada fabricante para un modelo particular de motocicleta.

Cada entrada del catálogo incluye un campo `RangeFound` que documenta el rango típico encontrado en las fuentes consultadas. El valor `RecommendedIntervalKm` es un punto dentro de ese rango seleccionado por MotoTrack como equilibrio entre seguridad y utilidad práctica.

## Personalización futura

En una etapa posterior, los usuarios podrán ajustar los intervalos de mantenimiento de acuerdo con sus propias necesidades y condiciones de uso. El catálogo actual sirve como base técnica y valores predeterminados para esa funcionalidad.

## Respaldo técnico

El catálogo proporciona los valores de referencia que utiliza el sistema para:

- Calcular el estado de mantenimiento de cada motocicleta.
- Determinar umbrales de alerta temprana (`WarningThresholdKm`).
- Respaldar las recomendaciones mostradas en el dashboard y otras secciones informativas.

Toda la información contenida en el catálogo es interna y no se expone directamente al usuario.

## Tipos de mantenimiento y sus intervalos

| Tipo | Intervalo recomendado | Rango típico | Umbral de alerta |
|------|----------------------|--------------|------------------|
| Aceite | 3 000 km | 2500–5000 km | 500 km |
| Cadena | 20 000 km | 15000–25000 km | 1000 km |
| Balatas | 15 000 km | 10000–20000 km | 1000 km |
| Llantas | 25 000 km | 20000–30000 km | 2000 km |
| Filtro de aire | 12 000 km | 10000–15000 km | 1000 km |
| Bujías | 20 000 km | 15000–24000 km | 1000 km |
| Válvulas | 24 000 km | 20000–26000 km | 2000 km |
| Batería | 24 000 km | 20000–30000 km | 2000 km |
| Suspensión | 30 000 km | 25000–35000 km | 3000 km |
| Líquido de frenos | 20 000 km | 15000–25000 km | 2000 km |
| Anticongelante | 40 000 km | 30000–50000 km | 3000 km |

---

*Para la identidad visual oficial de MotoTrack (colores, tipografía, logotipo), consultar [`BRAND_GUIDELINES.md`](../../branding/BRAND_GUIDELINES.md) — única fuente de verdad para los recursos gráficos del proyecto.*
