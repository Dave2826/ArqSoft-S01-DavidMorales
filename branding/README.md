# Branding — MotoTrack

Este directorio es la **única fuente de verdad** para toda la identidad visual del proyecto MotoTrack.

## Estructura

```
branding/
├── README.md                    ← Este archivo
├── BRAND_GUIDELINES.md          ← Guías completas de identidad visual
├── logo/
│   ├── primary/                 ← Logo completo con nombre
│   │   ├── mototrack-logo-primary.svg    ← Principal (fondos oscuros)
│   │   ├── mototrack-logo-primary.png    ← Rasterizado
│   │   ├── mototrack-logo-light.svg      ← Para fondos claros
│   │   ├── mototrack-logo-light.png      ← Rasterizado
│   │   └── mototrack-logo-monochrome.png ← Monocromático (PNG oficial)
│   ├── isotype/                 ← Isotipo (símbolo sin texto)
│   │   ├── mototrack-isotype.svg         ← Principal
│   │   ├── mototrack-isotype.png         ← Rasterizado
│   │   ├── mototrack-isotype-circle.png  ← Círculo
│   │   ├── mototrack-isotype-monochrome.png ← Monocromático
│   │   └── mototrack-isotype-orange.png  ← Naranja
│   ├── favicon/                 ← Favicon e iconos
│   │   ├── favicon.ico                  ← Favicon 32×32
│   │   ├── favicon.svg                  ← Favicon vectorial
│   │   ├── favicon-16.png               ← 16×16
│   │   ├── favicon-32.png               ← 32×32
│   │   ├── favicon-48.png               ← 48×48
│   │   └── favicon-256.png              ← 256×256 (trabajo)
│   ├── app-icon/                ← Iconos de aplicación
│   │   ├── mototrack-app-icon-128.png   ← PWA
│   │   ├── mototrack-app-icon-256.png   ← Apple Touch Icon / PWA
│   │   ├── mototrack-app-icon-512.png   ← PWA manifest
│   │   └── mototrack-app-icon-1024.png  ← Store / HD
│   ├── social/                  ← Redes sociales y previews
│   │   ├── mototrack-github-banner.svg  ← Banner 1280×640 para GitHub
│   │   └── mototrack-social-preview.png ← Open Graph / Twitter Card
│   ├── mobile/                  ← Recursos móviles
│   │   └── mototrack-splash.png         ← Splash screen
│   ├── documentation/           ← Documentación visual del branding
│   │   ├── mototrack-brand-concept.png
│   │   ├── mototrack-brand-usage.png
│   │   ├── mototrack-color-palette.png
│   │   ├── mototrack-symbol-construction.png
│   │   └── mototrack-typography.png
│   ├── marketing/               ← Piezas de marketing
│   │   ├── mototrack-business-card-front.png
│   │   └── mototrack-business-card-back.png
│   ├── previews/                ← Previews de producto
│   │   └── mototrack-dashboard-preview.png
│   ├── wallpapers/              ← Fondos de pantalla
│   │   └── mototrack-wallpaper-4k.png
│   └── master/                  ← Archivo fuente editable (único)
│       └── README.md            ← Instrucciones de exportación
├── presentations/               ← Plantillas para presentaciones institucionales
│   ├── .gitkeep
│   └── README.md
```

## Fuente de Verdad

- **Branding**: el contenido de `branding/` es la autoridad única.
- **Documentación**: `BRAND_GUIDELINES.md` es la referencia única para colores, tipografía y usos del logo.
- **Código**: los assets en `Catalogo/wwwroot/assets/branding/` son copias. Si hay discrepancia, `branding/` tiene razón.

## Flujo de Trabajo

1. **Editar**: modificar únicamente el archivo maestro en `logo/master/`.
2. **Exportar**: regenerar los assets derivados desde el archivo maestro a sus subdirectorios correspondientes.
3. **Nunca editar derivados directamente**: todos los archivos en `logo/` se generan desde `master/`.
4. **Sincronizar wwwroot**: copiar los assets consumidos por la aplicación a `Catalogo/wwwroot/assets/branding/`.
5. **Actualizar guías**: si el cambio afecta colores, tipografía o usos del logo, reflejarlo en `BRAND_GUIDELINES.md`.

## Convención de Nombres

```
mototrack-{tipo}[-{variante}].{ext}
```

| Segmento | Valores | Descripción |
|---|---|---|
| `{tipo}` | `logo`, `isotype`, `favicon`, `app-icon`, `icon-square`, `github-banner`, `social-preview` | Tipo de asset |
| `{variante}` | `primary`, `light`, `preview` (opcional) | Variante de uso o color |
| `{ext}` | `svg`, `png`, `ico` | Formato de archivo |

## Referencias

- [Brand Guidelines](BRAND_GUIDELINES.md) — reglas completas de identidad visual
- [Master Source](logo/master/README.md) — instrucciones de exportación desde archivo editable
- [Presentations](presentations/README.md) — plantillas para presentaciones institucionales
- [README principal](../README.md) — visión general del proyecto

## Assets de plataforma

Los siguientes assets en `Catalogo/wwwroot/assets/branding/` se derivan de `branding/`:

| Ruta en wwwroot | Fuente en branding/ | Propósito |
|-----------------|---------------------|-----------|
| `favicon/favicon.ico` | `logo/favicon/favicon.ico` | Favicon de navegador |
| `favicon/favicon.svg` | `logo/favicon/favicon.svg` | Favicon vectorial |
| `favicon/favicon-16.png` | `logo/favicon/favicon-16.png` | Favicon 16×16 |
| `favicon/favicon-32.png` | `logo/favicon/favicon-32.png` | Favicon 32×32 |
| `favicon/favicon-48.png` | `logo/favicon/favicon-48.png` | Favicon 48×48 |
| `isotype/mototrack-isotype.svg` | `logo/isotype/mototrack-isotype.svg` | Isotipo en interfaz |
| `isotype/mototrack-isotype.png` | `logo/isotype/mototrack-isotype.png` | Isotipo rasterizado |
| `isotype/mototrack-isotype-circle.png` | `logo/isotype/mototrack-isotype-circle.png` | Isotipo circular |
| `isotype/mototrack-isotype-monochrome.png` | `logo/isotype/mototrack-isotype-monochrome.png` | Isotipo monocromático |
| `isotype/mototrack-isotype-orange.png` | `logo/isotype/mototrack-isotype-orange.png` | Isotipo naranja |
| `logo/mototrack-logo-primary.svg` | `logo/primary/mototrack-logo-primary.svg` | Logo en landing, login, footer |
| `logo/mototrack-logo-primary.png` | `logo/primary/mototrack-logo-primary.png` | Logo rasterizado |
| `logo/mototrack-logo-light.svg` | `logo/primary/mototrack-logo-light.svg` | Logo para fondos claros |
| `logo/mototrack-logo-light.png` | `logo/primary/mototrack-logo-light.png` | Logo light rasterizado |
| `logo/mototrack-logo-monochrome.png` | `logo/primary/mototrack-logo-monochrome.png` | Logo monocromático |
| `app-icon/mototrack-app-icon-128.png` | `logo/app-icon/mototrack-app-icon-128.png` | PWA |
| `app-icon/mototrack-app-icon-256.png` | `logo/app-icon/mototrack-app-icon-256.png` | Apple Touch Icon / PWA |
| `app-icon/mototrack-app-icon-512.png` | `logo/app-icon/mototrack-app-icon-512.png` | PWA manifest 512×512 |
| `app-icon/mototrack-app-icon-1024.png` | `logo/app-icon/mototrack-app-icon-1024.png` | PWA manifest / store |
| `splash/mototrack-splash.png` | `logo/mobile/mototrack-splash.png` | Splash screen |
| `site.webmanifest` | — | PWA manifest (brand-aligned) |
