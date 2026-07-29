# Branding — MotoTrack

Este directorio es la **única fuente de verdad** para toda la identidad visual del proyecto MotoTrack.

## Estructura

```
branding/
├── README.md                 ← Este archivo
├── BRAND_GUIDELINES.md       ← Guías completas de identidad visual
├── logo/                     ← Activos del logotipo (SVG, PNG, fuente)
│   ├── mototrack-logo.svg
│   ├── mototrack-logo-dark.svg
│   ├── mototrack-isotype.svg
│   ├── mototrack-isotype-dark.svg
│   ├── mototrack-icon.svg
│   ├── mototrack-favicon.svg
│   ├── mototrack-github-banner.svg
│   ├── mototrack-social-preview.png
│   ├── mototrack-app-icon.png
│   └── master/               ← Archivos fuente editables (.ai, .fig, etc.)
│       └── mototrack-brand-assets.ai
└── assets/
    ├── screenshots/           ← Capturas de pantalla con branding aplicado
    └── presentations/         ← Presentaciones con identidad visual
```

## Fuente de Verdad

- **Branding**: el contenido de `branding/` es la autoridad.
- **Documentación**: `BRAND_GUIDELINES.md` es la referencia única para colores, tipografía, y usos del logo.
- **Código**: los assets en `Catalogo/wwwroot/` son copias de los originales en `branding/logo/`. Si hay discrepancia, `branding/` tiene razón.

## Flujo de Trabajo

1. **Agregar un nuevo asset**: colocar el archivo en `logo/` siguiendo la nomenclatura `mototrack-{tipo}-{variante}.{ext}`.
2. **Actualizar un asset existente**: modificar el archivo en `logo/`. Si el asset está referenciado desde `wwwroot/`, actualizar también la copia allí.
3. **Actualizar las guías**: si el cambio afecta colores, tipografía, o usos del logo, reflejarlo en `BRAND_GUIDELINES.md`.

## Convención de Nombres

```
mototrack-{tipo}-{variante}.{ext}
```

Ver `BRAND_GUIDELINES.md` — Sección 11 para la especificación completa.

## Referencias

- [Brand Guidelines](BRAND_GUIDELINES.md) — reglas completas de identidad visual
- [README principal](../README.md) — visión general del proyecto
- [ADR-10](../Catalogo/docs/adr/) — Architecture Governance Standard
