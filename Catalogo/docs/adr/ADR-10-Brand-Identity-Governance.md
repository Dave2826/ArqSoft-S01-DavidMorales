# ADR-10: Brand Identity Governance

**Estado:** Aceptado  
**Fecha:** 2026-07-28  
**Decidido por:** Architecture Governance Board  
**Referencias:** ADR-03 (Estilo Arquitectónico), [`BRAND_GUIDELINES.md`](../../branding/BRAND_GUIDELINES.md)

---

## Contexto

MotoTrack incorporó una identidad visual oficial definida en el directorio `branding/`, que incluye logotipo, isotipo, favicon, iconos de plataforma, paleta de colores, tipografía, guías de uso y recursos para redes sociales. A medida que la aplicación y su documentación crecieron, se identificaron los siguientes problemas:

- La documentación técnica (ADR, diagramas, knowledge base) no referenciaba formalmente las guías de marca.
- Los assets visuales se consumían desde distintas ubicaciones sin una fuente única de verdad claramente establecida.
- No existía una política formal que definiera cómo la documentación y la aplicación deben relacionarse con la identidad visual.

## Problema identificado

Ausencia de una decisión arquitectónica documentada que establezca la gobernanza de la identidad visual, dejando sin respaldo formal las reglas de consumo, referencia y actualización de los recursos gráficos del proyecto.

## Decisión

Se establece la siguiente política de gobernanza para la identidad visual de MotoTrack:

### Principios

1. **Fuente única de verdad.** El directorio `branding/` es la única fuente autorizada para todos los recursos gráficos del proyecto. Cualquier discrepancia entre `branding/` y otras ubicaciones se resuelve a favor de `branding/`.

2. **Documento normativo.** `BRAND_GUIDELINES.md` es el documento normativo que define colores, tipografía, uso del logotipo, variantes, tamaños mínimos, áreas de resguardo y tratamiento en distintos fondos. Ningún otro documento duplica estas reglas.

3. **Consumo desde la aplicación.** La aplicación web (`Catalogo/wwwroot/`) puede contener copias derivadas de los assets de `branding/` cuando sean necesarias para su ejecución. Estas copias deben sincronizarse desde `branding/` y nunca modificarse directamente.

4. **Referencia desde la documentación.** Toda la documentación técnica (ADR, diagramas, knowledge base, IA statement) debe referenciar `BRAND_GUIDELINES.md` como fuente de verdad para la identidad visual. No debe duplicar contenido definido en las guías.

5. **Actualización controlada.** Cualquier modificación a la identidad visual debe realizarse sobre el archivo maestro en `branding/logo/master/`, exportando posteriormente los derivados. Los cambios en las guías deben reflejarse en `BRAND_GUIDELINES.md` y notificarse en los ADR correspondientes.

### Ámbito de aplicación

Esta política aplica a:

- El directorio `branding/` y todos sus subdirectorios.
- Todos los archivos de documentación en `Catalogo/docs/`.
- Los assets estáticos en `Catalogo/wwwroot/` derivados de `branding/`.
- El código de la aplicación que haga referencia a colores, tipografía o recursos gráficos.

## Alternativas consideradas

| Alternativa | Motivo de descarte |
|---|---|
| **Distribuir assets sin fuente única** | Riesgo de divergencia entre assets y pérdida de coherencia visual. |
| **Duplicar reglas visuales en cada ADR** | Viola el principio de fuente única de verdad. Incrementa el costo de mantenimiento. |
| **No documentar la gobernanza** | La ausencia de una política formal deja la identidad visual sin respaldo arquitectónico, dificultando su mantenimiento futuro y la incorporación de nuevos contribuyentes. |
| **Política exclusivamente técnica sin ADR** | Una decisión de esta magnitud (afecta documentación, código, assets y proceso) debe registrarse como ADR para mantener la trazabilidad arquitectónica del proyecto. |

## Consecuencias

### Positivas

- La identidad visual cuenta con respaldo arquitectónico formal.
- Se elimina la ambigüedad sobre la fuente autorizada de recursos gráficos.
- La documentación técnica mantiene coherencia visual sin duplicar reglas.
- Nuevos contribuyentes pueden identificar rápidamente cómo y dónde aplicar la identidad visual.
- La trazabilidad de cambios en la marca queda registrada mediante ADR.

### Negativas

- Requiere que todo cambio visual futuro se documente en ADR además de en `BRAND_GUIDELINES.md`.
- La sincronización entre `branding/` y `wwwroot/` debe mantenerse manualmente hasta que se automatice en un BUILD futuro.

---

*Para la identidad visual oficial de MotoTrack (colores, tipografía, logotipo), consultar [`BRAND_GUIDELINES.md`](../../branding/BRAND_GUIDELINES.md) — única fuente de verdad para los recursos gráficos del proyecto.*
