# MotoTrack — Brand Guidelines

> Version 1.0 — Última actualización: 2026-07-28
>
> Estas guías constituyen la única fuente de verdad para la identidad visual de MotoTrack.
> Cualquier desviación debe ser aprobada por el Architecture Governance Board.

---

## 1. Filosofía Visual

MotoTrack es una herramienta de gestión de mantenimiento para motocicletas. La identidad visual se inspira en tres pilares conceptuales:

- **Ruta**: el camino recorrido por cada motocicleta, reflejado en el seguimiento de mantenimientos y kilometraje.
- **Precisión**: el mantenimiento no es opcional; cada servicio tiene su momento exacto.
- **Progreso**: la evolución constante del vehículo y del conductor, reflejada en datos y decisiones informadas.

El logotipo representa una montaña o camino ascendente — el viaje, el reto, la meta. La línea quebrada evoca tanto una ruta topográfica como la inicial "M" de MotoTrack.

---

## 2. Personalidad de la Marca

| Atributo | Descripción |
|---|---|
| **Profesional** | Comunicación clara, precisa, sin ambigüedades |
| **Enérgica** | El naranja vibrante transmite acción y movimiento |
| **Técnica** | Lenguaje y estética orientados a ingeniería y datos |
| **Confinable** | Accesible para entusiastas y profesionales por igual |

---

## 3. Valores

- **Mantenibilidad**: no solo del código, también de la identidad visual.
- **Precisión**: cada detalle importa, desde el tracking de kilometraje hasta el kerning del logo.
- **Evolución**: el diseño crece con el producto, sin revoluciones innecesarias.
- **Sobriedad**: la interfaz no compite con el contenido; lo organiza.

---

## 4. Colores Oficiales

### Paleta primaria

| Token | Color | Hex | RGB | HSL | Uso |
|---|---|---|---|---|---|
| Primary | MotoTrack Orange | `#ff7a00` | `rgb(255, 122, 0)` | `hsl(29, 100%, 50%)` | Acciones principales, logo, enlaces, acentos |
| Primary Hover | Orange Light | `#ffa64d` | `rgb(255, 166, 77)` | `hsl(29, 100%, 65%)` | Hover de elementos primarios |
| Primary Glow | Orange Glow | `rgba(255, 122, 0, 0.15)` | — | — | Sombras y brillos |

### Paleta de fondo

| Token | Color | Hex | RGB | Uso |
|---|---|---|---|---|
| Background | Dark | `#121212` | `rgb(18, 18, 18)` | Fondo principal de la UI |
| Card | Card Dark | `#1e1e1e` | `rgb(30, 30, 30)` | Tarjetas, paneles, contenedores |
| Card Hover | Card Hover | `#222222` | `rgb(34, 34, 34)` | Hover de tarjetas |
| Card Border | Card Border | `rgba(255, 255, 255, 0.06)` | — | Bordes sutiles |

### Paleta de texto

| Token | Hex | Uso |
|---|---|---|
| Text Primary | `#f5f5f5` | Texto principal |
| Text Secondary | `#aaaaaa` | Texto secundario, metadatos |
| Text Muted | `#777777` | Texto deshabilitado, placeholders |
| Text Dim | `#666666` | Texto de baja prioridad |

### Paleta semántica

| Token | Color | Hex | Uso |
|---|---|---|---|
| Success | Verde | `#2e7d32` | Mantenimiento al día |
| Success Text | Verde claro | `#a5d6a7` | Texto de éxito |
| Warning | Naranja oscuro | `#ef6c00` | Mantenimiento próximo |
| Warning Text | Naranja claro | `#ffcc80` | Texto de advertencia |
| Danger | Rojo | `#c62828` | Mantenimiento vencido |
| Danger Text | Rojo claro | `#ef9a9a` | Texto de error |
| Info | Azul | `#4a9eff` | Información general |

### Modo claro (no implementado aún)

Cuando se implemente un tema claro, los valores invertirán: fondos claros (`#f5f5f5` → `#121212`) y texto oscuro (`#1a1a1a` → `#f5f5f5`). Los colores primarios y semánticos se mantienen idénticos.

---

## 5. Tipografía Oficial

### Jerarquía tipográfica

| Nivel | Fuente | Peso | Tamaño | Uso |
|---|---|---|---|---|
| Display | Montserrat | 700, 800, 900 | 2rem+ | Títulos de sección, hero, dashboard |
| Heading | Montserrat | 600, 700 | 1.25–1.75rem | Encabezados de tarjetas y paneles |
| Body | Inter | 400, 600 | 0.875–1rem | Texto general, párrafos, tablas |
| Small | Inter | 400 | 0.6–0.75rem | Metadatos, etiquetas, timestamps |
| Monospace | JetBrains Mono (futuro) | 400, 600 | 0.875rem | Código, datos técnicos (futura integración) |

### Fuentes

| Fuente | Rol | Tipo | Carga |
|---|---|---|---|
| [Montserrat](https://fonts.google.com/specimen/Montserrat) | Títulos, display | Sans-serif, geométrica | Google Fonts |
| [Inter](https://fonts.google.com/specimen/Inter) | Cuerpo, UI | Sans-serif, humanista | Google Fonts |

### Reglas tipográficas

- El texto body nunca debe ser menor a `0.875rem` en interfaces principales.
- Los títulos en Montserrat usan letter-spacing reducido (`-0.02em`) para mejorar legibilidad.
- Inter es la fuente predeterminada del sistema. Montserrat es solo para display y headings.
- No usar Montserrat para párrafos largos. Su diseño compacto reduce legibilidad en texto corrido.

---

## 6. Uso del Logotipo

### Descripción

El logotipo de MotoTrack consiste en un símbolo (una línea quebrada que forma un camino ascendente / "M") acompañado del nombre "MotoTrack" en tipografía Montserrat Bold.

### Versiones

| Archivo | Ruta | Variante | Uso recomendado |
|---|---|---|---|
| `mototrack-logo-primary.svg` | `logo/primary/` | Logo completo, primary | Fondos oscuros (#121212, #1e1e1e). Uso principal. |
| `mototrack-logo-primary.png` | `logo/primary/` | Logo completo, primary | Rasterizado para contextos que requieran PNG. |
| `mototrack-logo-light.svg` | `logo/primary/` | Logo completo, light | Fondos claros (#ffffff, #f5f5f5). Temas claro. |
| `mototrack-logo-light.png` | `logo/primary/` | Logo completo, light | Rasterizado para contextos que requieran PNG. |
| `mototrack-logo-monochrome.png` | `logo/primary/` | Logo completo, monochrome | Uso monocromático (solo PNG oficial). |
| `mototrack-isotype.svg` | `logo/isotype/` | Solo símbolo, primary | Avatar, navbar, espacios reducidos, fondos oscuros. |
| `mototrack-isotype.png` | `logo/isotype/` | Solo símbolo, primary | Rasterizado para contextos que requieran PNG. |
| `mototrack-isotype-circle.png` | `logo/isotype/` | Solo símbolo, círculo | Avatares e iconos circulares. |
| `mototrack-isotype-monochrome.png` | `logo/isotype/` | Solo símbolo, monochrome | Uso monocromático (solo PNG oficial). |
| `mototrack-isotype-orange.png` | `logo/isotype/` | Solo símbolo, orange | Uso en marca naranja. |
| `favicon.svg` | `logo/favicon/` | Símbolo optimizado | Favicon de navegador (viewBox reducido). |
| `mototrack-github-banner.svg` | `logo/social/` | Logo completo, banner | GitHub profile, README banner. |
| `mototrack-social-preview.png` | `logo/social/` | Logo + nombre, preview | Open Graph, Twitter Cards. |
| `mototrack-app-icon-*.png` | `logo/app-icon/` | Símbolo cuadrado HD | PWA manifest, mobile home screen (128/256/512/1024). |
| `mototrack-splash.png` | `logo/mobile/` | Símbolo cuadrado | Pantalla de bienvenida (splash). |

### Tamaño mínimo

| Contexto | Ancho mínimo |
|---|---|
| Logo completo en pantalla | 120px |
| Logo completo en impresión | 1.5in / 38mm |
| Isotipo | 24px |
| Favicon | 16px |

### Familia de favicon

Todos los iconos deben derivarse del archivo maestro `logo/master/mototrack-brand-assets.ai`. Las variantes disponibles actualmente son:

| Tamaño | Archivo | Ruta | Formato | Destino |
|--------|---------|------|---------|---------|
| 16×16 | `favicon-16.png` | `logo/favicon/` | PNG | Navegador (pestaña) |
| 32×32 | `favicon.ico` / `favicon.svg` / `favicon-32.png` | `logo/favicon/` | ICO + SVG + PNG | Favicon estándar |
| 48×48 | `favicon-48.png` | `logo/favicon/` | PNG | Navegador (alta densidad) |
| 64×64 | `favicon.svg` | `logo/favicon/` | SVG (escalable) | Navegadores modernos |
| 128×128 | `mototrack-app-icon-128.png` | `logo/app-icon/` | PNG | PWA manifest |
| 256×256 | `mototrack-app-icon-256.png` | `logo/app-icon/` | PNG | Apple Touch Icon / PWA |
| 512×512 | `mototrack-app-icon-512.png` | `logo/app-icon/` | PNG | PWA manifest |
| 1024×1024 | `mototrack-app-icon-1024.png` | `logo/app-icon/` | PNG | PWA manifest / store |

Todos los archivos ICO, SVG y PNG se generan desde el isotipo oficial.

### Área de resguardo

El área de resguardo (clear space) alrededor del logotipo debe ser igual al 25% del ancho del isotipo. No colocar texto, bordes ni otros elementos gráficos dentro de esta área.

---

## 7. Uso Incorrecto

| Práctica | Incorrecta | Correcta |
|---|---|---|
| Color | Cambiar el color naranja del logo por otro color | Usar exclusivamente `#ff7a00` para el símbolo |
| Deformación | Escalar el logo de forma no proporcional (stretch) | Mantener aspect ratio original siempre |
| Rotación | Rotar el logo 90°, 180° o cualquier ángulo | El logo siempre en orientación horizontal original |
| Efectos | Agregar sombras, gradientes, brillos, outlines | Usar las versiones planas proporcionadas |
| Fondo inapropiado | Colocar el logo sobre fondos que compitan con su visibilidad | Ver sección de Fondos Permitidos |
| Modificación | Agregar texto, iconos o elementos al logo | No modificar la composición original |
| Recoloración | Usar el logo en blanco y negro o escala de grises | Usar la versión dark/light correspondiente |
| Bordes | Agregar bordes, marcos o fondos alrededor del logo | Respetar el área de resguardo |

---

## 8. Márgenes de Seguridad

Para el logo completo: el margen mínimo alrededor del logo es igual a la altura de la letra "M" de "MotoTrack".

Para el isotipo: el margen mínimo es igual al 25% del ancho del isotipo.

Ningún otro elemento visual (texto, iconos, bordes) debe invadir esta zona.

---

## 9. Fondos Permitidos

| Fondo | Versión del logo |
|---|---|
| `#121212` (Dark primary) | Light |
| `#1e1e1e` (Card) | Light |
| `#222222` (Card hover) | Light |
| `#ffffff` (White) | Dark |
| `#f5f5f5` (Light gray) | Dark |
| Imágenes con overlay oscuro (>60% opacidad) | Light |
| Imágenes con overlay claro (>60% opacidad) | Dark |

---

## 10. Fondos Prohibidos

- Fondos cuyo contraste con el logo sea menor a 3:1 (WCAG AA para elementos gráficos).
- Fondos con patrones que interfieran con la legibilidad del isotipo.
- Fondos fotográficos sin overlay.
- El logo nunca debe colocarse sobre el color naranja `#ff7a00` o variaciones cercanas.

---

## 11. Convención de Nombres

```
mototrack-{tipo}-{variante}.{ext}
```

| Segmento | Valores | Descripción |
|---|---|---|
| `{tipo}` | `logo`, `isotype`, `icon`, `favicon`, `github-banner`, `social-preview`, `app-icon` | Tipo de asset |
| `{variante}` | `dark`, `light` (opcional) | Variante de color. Ausente = light por defecto |
| `{ext}` | `svg`, `png`, `ico`, `ai` | Formato de archivo |

### Ejemplos

```
mototrack-logo.svg           → Logo completo, light
mototrack-logo-dark.svg      → Logo completo, dark
mototrack-isotype.svg        → Isotipo, light
mototrack-isotype-dark.svg   → Isotipo, dark
mototrack-icon.svg           → Icono cuadrado
mototrack-favicon.svg        → Favicon
mototrack-github-banner.svg  → Banner para GitHub
mototrack-social-preview.png → Social preview
mototrack-app-icon.png       → App icon para PWA
mototrack-logo.png           → Logo rasterizado
```

### Archivos fuente editables

Los archivos fuente editables se almacenan en `logo/master/` con el siguiente formato:

```
mototrack-brand-assets.{ext}
```

Donde `{ext}` puede ser `ai` (Adobe Illustrator), `fig` (Figma), o `sketch` (Sketch). Solo un archivo fuente maestro debe existir en todo momento.

---

## 12. Voice & Tone

### Principios de comunicación

| Principio | Aplicación |
|---|---|
| **Claro** | Frases directas. Sin jargon innecesario. |
| **Técnico cuando corresponda** | Usar terminología de mantenimiento automotriz con precisión. |
| **No infantilizar** | El usuario es un adulto gestionando activos valiosos. El tono debe reflejarlo. |
| **Consistente** | Misma voz en UI, documentación, y comunicaciones. |

### Ejemplos de tono

| Contexto | Correcto | Incorrecto |
|---|---|---|
| Error de sistema | "No pudimos cargar los datos de mantenimiento. Verifica tu conexión e inténtalo de nuevo." | "Oops! Algo salió mal :(" |
| Mantenimiento vencido | "Cambio de aceite vencido desde el 15/03/2026. Programa este servicio." | "Tu moto necesita cariño!" |
| Confirmación de acción | "Motocicleta registrada correctamente." | "Listo! 🏍️" |

---

## 13. Futuras Integraciones

Esta sección documenta los puntos de contacto donde el branding debe aplicarse en BUILD futuros.

| Prioridad | Punto de contacto | BUILD responsable | Estado |
|---|---|---|---|
| Alta | Navbar (logo + enlace) | Brand Integration — UI | Pendiente |
| Alta | Login screen (logo + fondo) | Brand Integration — UI | Pendiente |
| Alta | Dashboard header (logo + título) | Brand Integration — UI | Pendiente |
| Alta | Favicon en layout | Brand Integration — UI | Pendiente |
| Media | PWA manifest + App Icon | Brand Integration — PWA | Pendiente |
| Media | Open Graph tags en layout | Brand Integration — SEO | Pendiente |
| Media | GitHub repository social preview | Brand Integration — GitHub | Pendiente |
| Baja | Slide decks en `assets/presentations/` | Brand Integration — Docs | Pendiente |
| Baja | Screenshots con branding en `assets/screenshots/` | Brand Integration — Docs | Pendiente |
| Futura | Modo claro (temas alternos) | Brand Integration — Theming | Documentado — Pendiente de implementación |

---

## 14. Mantenimiento de estas Guías

| Acción | Frecuencia | Responsable |
|---|---|---|
| Revisar coherencia con site.css | Cada BUILD que modifique design tokens | Desarrollador |
| Actualizar ejemplos de tono | Cuando se agreguen nuevos patrones de UI | UX / Frontend |
| Agregar nuevas variantes de logo | Cuando se requiera un nuevo formato | Diseñador |
| Revisión completa de guías | Anual | Architecture Governance Board |

---

*MotoTrack Brand Guidelines v1.0 — Este documento es la única fuente de verdad para la identidad visual del proyecto. Toda integración visual debe referenciar este documento.*
