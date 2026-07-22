# ADR-09: Pruebas Unitarias e Integración Continua

**Estado:** Aprobado  
**Fecha:** 2026-07-22  
**Decidido por:** Equipo de desarrollo  
**Referencias:** ADR-03 (Estilo Arquitectónico), ADR-08 (Migración EF Core)

---

## Contexto

El proyecto MotoTrack incorporó un conjunto de pruebas unitarias y automatización del proceso de compilación y ejecución de pruebas. Hasta este punto, la validación del código dependía exclusivamente de la compilación correcta. No existía un mecanismo automatizado para detectar regresiones en la lógica de negocio ni un proceso estandarizado de verificación previo a integración.

## Decisiones

1. **Se adopta xUnit como framework de pruebas unitarias.**  
   xUnit es el framework estándar para proyectos .NET modernos. Se integra con `dotnet test` sin configuración adicional y con el ecosistema de Visual Studio y CI. Las pruebas se organizan siguiendo el patrón Arrange / Act / Assert.

2. **Se adopta Moq para aislamiento de dependencias mediante mocks.**  
   Los servicios que dependen de repositorios (interfaces del dominio) se prueban inyectando mocks de `Mock<T>` para aislar la lógica de negocio de la capa de persistencia. Las estrategias sin dependencias externas se prueban sin mocks, directamente con instancias concretas.

3. **Se incorpora GitHub Actions como mecanismo de Integración Continua.**  
   El flujo de trabajo ejecuta `dotnet restore`, `dotnet build` y `dotnet test` en cada push y pull request sobre la rama `main`. El pipeline falla automáticamente si falla la compilación o alguna prueba.

4. **Se mantiene la estrategia de commits pequeños con una sola responsabilidad por BUILD.**  
   Cada BUILD aborda un único objetivo: infraestructura de pruebas, pruebas de una clase específica, actualización de CI, etc. La granularidad facilita la revisión y el aislamiento de problemas.

5. **Se identifica como deuda técnica futura la reorganización de la raíz del repositorio.**  
   Actualmente la raíz del repositorio Git está en `Catalogo/`, mientras que los proyectos `MotoTrack.Application`, `MotoTrack.Domain`, `MotoTrack.Infrastructure` y `MotoTrack.Tests` residen fuera de ella. Esto impide que el workflow de GitHub Actions acceda a todos los proyectos necesarios durante el checkout. La corrección de esta estructura se difiere a un BUILD futuro.

## Consecuencias

### Positivas

- **Validación automática de reglas de negocio.** Las pruebas unitarias cubren las estrategias de estado (`DefaultEstadoStrategy`, `ConservadoraEstadoStrategy`) y los servicios con lógica condicional (`UsuarioService`).
- **Prevención de regresiones.** Cualquier cambio que altere el comportamiento esperado de las clases probadas se detecta en el pipeline antes de integrarse a `main`.
- **Build reproducible.** El workflow de CI ejecuta los mismos comandos en un entorno limpio, eliminando dependencias del entorno local del desarrollador.
- **Integración Continua.** Cada push y pull request a `main` dispara automáticamente la compilación y ejecución de pruebas.
- **Mejor mantenibilidad.** Las pruebas documentan el comportamiento esperado de cada clase y sirven como especificación ejecutable.

### Negativas

- **Incremento del mantenimiento de pruebas.** Cada cambio en la lógica de negocio puede requerir actualizar las pruebas correspondientes.
- **La estructura actual del repositorio limita la ejecución del workflow en GitHub.** Hasta que se reorganice la raíz del repositorio para incluir todos los proyectos, el pipeline no podrá completarse exitosamente en GitHub Actions.
