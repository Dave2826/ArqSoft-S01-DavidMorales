# Sistema Inteligente de Alertas — Lógica de Cálculo

## Propósito

El sistema de alertas de MotoTrack determina el estado de cada tipo de mantenimiento para una motocicleta en función de su kilometraje actual y el historial de servicios registrados. Los estados posibles son AL DÍA, PRÓXIMO y VENCIDO.

## Fuente de los intervalos

Los intervalos de mantenimiento ya no están hardcodeados ni dependen de una configuración fija. Desde la ETAPA 2.1, todos los valores de referencia se obtienen del catálogo interno `MaintenanceCatalog`, que contiene recomendaciones técnicas basadas en manuales de fabricantes y talleres especializados.

Cada tipo de mantenimiento tiene su propio intervalo `RecommendedIntervalKm` y su propio umbral de alerta `WarningThresholdKm`.

## Cálculo del próximo servicio

El sistema busca el último mantenimiento registrado del tipo correspondiente para la motocicleta. Si existe, el próximo servicio se calcula como:

```
ProximoKilometraje = UltimoKilometrajeServicio + RecommendedIntervalKm
```

Si no existe ningún mantenimiento registrado de ese tipo, pero la motocicleta tiene un kilometraje de compra, se utiliza ese valor como referencia inicial:

```
ProximoKilometraje = KilometrajeCompra + RecommendedIntervalKm
```

## Determinación del estado

Con el kilometraje actual de la motocicleta y el próximo servicio calculado, se determina cuántos kilómetros faltan:

```
Faltan = ProximoKilometraje - KilometrajeActual
```

El estado se asigna de la siguiente forma:

- **VENCIDO**: cuando `Faltan < 0` (el kilometraje actual ya superó el próximo servicio recomendado).
- **PRÓXIMO**: cuando `0 <= Faltan <= WarningThresholdKm` (el kilometraje actual está dentro del umbral de alerta).
- **AL DÍA**: cuando `Faltan > WarningThresholdKm` (aún hay suficiente margen antes del próximo servicio).

## Significado del umbral (WarningThresholdKm)

Cada tipo de mantenimiento tiene un `WarningThresholdKm` propio, que define la ventana de kilometraje antes del servicio en la que el sistema comienza a mostrar alertas. Este valor varía según la criticidad y la naturaleza del mantenimiento:

| Mantenimiento | Intervalo | Umbral de alerta |
|---|---|---|
| Aceite | 3000 km | 500 km |
| Cadena | 20000 km | 1000 km |
| Balatas | 15000 km | 1000 km |
| Llantas | 25000 km | 2000 km |
| Filtro de aire | 12000 km | 1000 km |
| Bujías | 20000 km | 1000 km |
| Válvulas | 24000 km | 2000 km |

## Recomendaciones conservadoras

MotoTrack utiliza valores conservadores dentro de los rangos documentados por los fabricantes. Esto significa que los intervalos recomendados están en el lado seguro del espectro, priorizando la prevención sobre el desgaste máximo. El usuario podrá personalizar estos intervalos en una etapa posterior.

## Componentes involucrados

- `MaintenanceCatalog`: catálogo interno con los valores de referencia por tipo.
- `CalculadorEstadoMantenimiento`: clase que orquesta el cálculo por tipo.
- `IEstadoMantenimientoStrategy`: interfaz que define cómo se compara el kilometraje actual contra el umbral.
- `MaintenanceStatusResult`: objeto de resultado con estado, color, mensaje descriptivo, kilómetros restantes y próximo servicio.
