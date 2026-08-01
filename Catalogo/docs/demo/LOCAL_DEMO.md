# Demo Local

Guía para ejecutar MotoTrack como servidor local y acceder desde cualquier dispositivo conectado a la misma red Wi-Fi durante una exposición presencial.

---

## Cómo obtener la IP

1. Abrir una terminal (PowerShell o CMD).
2. Ejecutar:

```
ipconfig
```

3. Buscar el adaptador de red activo (Wi-Fi o Ethernet).
4. Copiar la dirección **IPv4**, por ejemplo `192.168.1.80`.

---

## Cómo ejecutar

Desde la carpeta `Catalogo/` del proyecto:

```
dotnet run
```

La aplicación quedará escuchando en todas las interfaces de red:

```
http://0.0.0.0:5000
https://0.0.0.0:5001
```

Si se prefiere únicamente HTTP (recomendado para la demostración desde un celular):

```
dotnet run --launch-profile http
```

---

## Cómo acceder desde otro dispositivo

1. Conectar el celular a la **misma red Wi-Fi** que la laptop.
2. Abrir el navegador del celular y escribir:

```
http://192.168.X.X:5000
```

Reemplazar `192.168.X.X` por la IP obtenida con `ipconfig`.

Ejemplo:

```
http://192.168.1.80:5000
```

> **Nota:** Desde la propia laptop también se puede abrir `http://localhost:5000`.

---

## Problemas comunes

### Firewall de Windows

Si el dispositivo no puede abrir la página, Windows Defender Firewall puede estar bloqueando el puerto.

- Al ejecutar la primera vez, permitir el acceso cuando Windows solicite confirmación.
- O abrir manualmente el puerto:
  1. Abrir "Firewall de Windows con seguridad avanzada".
  2. "Reglas de entrada" → "Nueva regla" → "Puerto".
  3. Protocolo **TCP** y puertos `5000` (HTTP) y, si aplica, `5001` (HTTPS).
  4. Seleccionar "Permitir la conexión" y aplicar a los perfiles "Privado" y "Público".

### Wi-Fi distinta

El celular y la laptop deben estar en la **misma red**.

- Verificar que ambos estén conectados al mismo router o SSID.
- Algunas redes de invitados, hoteles o campus bloquean la comunicación entre dispositivos (aislamiento de AP) y no permiten la conexión.
- Verificar que la IP mostrada en `ipconfig` corresponda a la red actual (por ejemplo `192.168.x.x` o `10.x.x.x`).

### Puerto ocupado

Si `dotnet run` falla indicando que la dirección o el puerto ya están en uso:

- Cerrar la otra aplicación que usa el puerto 5000.
- O identificar el proceso que lo ocupa:

```
netstat -ano | findstr :5000
```

y detener el proceso correspondiente.

---

## Cómo detener la aplicación

En la terminal donde se ejecuta MotoTrack, presionar:

```
Ctrl + C
```

O simplemente cerrar la ventana de la terminal.
