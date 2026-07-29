# Branding — MotoTrack

Este directorio es la **única fuente de verdad** para toda la identidad visual del proyecto MotoTrack.

## Estructura

```
branding/
├── README.md                 ← Este archivo
├── BRAND_GUIDELINES.md       ← Guías completas de identidad visual
├── logo/                     ← Assets del logotipo (SVG, PNG, ICO)
│   ├── mototrack-logo.svg        ← Logo completo, light
│   ├── mototrack-logo-dark.svg   ← Logo completo, dark
│   ├── mototrack-logo.png        ← Logo rasterizado 600×150
│   ├── mototrack-isotype.svg     ← Isotipo, light
│   ├── mototrack-isotype-dark.svg← Isotipo, dark
│   ├── mototrack-isotype.png     ← Isotipo rasterizado 256×256
│   ├── mototrack-icon.svg        ← Icono cuadrado vectorial
│   ├── mototrack-icon.png        ← Icono cuadrado 512×512
│   ├── mototrack-favicon.svg     ← Favicon vectorial
│   ├── mototrack-favicon.ico     ← Favicon 32×32
│   ├── mototrack-app-icon.png    ← App icon 1024×1024 (PWA)
│   └── master/                   ← Archivo fuente editable (único)
│       └── README.md             ← Instrucciones de exportación
├── social/                   ← Redes sociales y previews
│   ├── mototrack-github-banner.svg   ← Banner 1280×640 para GitHub
│   └── mototrack-social-preview.png  ← Open Graph / Twitter Card
└── presentations/            ← Presentaciones con identidad visual
```

## Fuente de Verdad

- **Branding**: el contenido de `branding/` es la autoridad.
- **Documentación**: `BRAND_GUIDELINES.md` es la referencia única para colores, tipografía y usos del logo.
- **Código**: los assets en `Catalogo/wwwroot/` son copias. Si hay discrepancia, `branding/` tiene razón.

## Flujo de Trabajo

1. **Editar**: modificar únicamente el archivo maestro en `logo/master/`.
2. **Exportar**: regenerar los SVGs/PNGs/ICOs derivados desde el archivo maestro.
3. **Nunca editar derivados directamente**: todos los archivos en `logo/` y `social/` se generan desde `master/`.
4. **Actualizar guías**: si el cambio afecta colores, tipografía o usos del logo, reflejarlo en `BRAND_GUIDELINES.md`.

## Convención de Nombres

```
mototrack-{tipo}-{variante}.{ext}
```

| Segmento | Valores | Ejemplo |
|---|---|---|
| `{tipo}` | `logo`, `isotype`, `icon`, `favicon`, `app-icon`, `github-banner`, `social-preview` | Tipo de asset |
| `{variante}` | `dark` (opcional) | Variante de color. Ausente = light |
| `{ext}` | `svg`, `png`, `ico` | Formato de archivo |

## Referencias

- [Brand Guidelines](BRAND_GUIDELINES.md) — reglas completas de identidad visual
- [Master Source](logo/master/README.md) — instrucciones de exportación desde archivo editable
- [README principal](../README.md) — visión general del proyecto
