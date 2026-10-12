# Proceso de publicación en el servidor Ubuntu (Mono + Apache)

Aplica a cambios de fotos de quejas por lote y EXPO (octubre 2026). Sirve como guía general para publicar cambios del sistema de quejas.

Reemplaza `<RUTA_APP>` por la carpeta de la aplicación en el servidor (la que contiene `bin/queja.dll` y `fotos_ant/`).

## Archivos que cambian

| Archivo | Qué es | Cómo llega al servidor |
|---|---|---|
| `bin/queja.dll` | Contiene el code-behind compilado (`SubirArchivos.ashx.cs`, `consulta.aspx.cs`, `investigadores.aspx.cs`) | Recompilar y copiar |
| `principalnew.aspx` | Alta de queja por lote | Copiar tal cual |
| `principalexpnew.aspx` | Alta de queja EXPO | Copiar tal cual |

Los archivos `.cs` NO se copian al servidor: hay que recompilar la DLL.

## 1. Compilar en la PC

En Visual Studio, configuración Release: Compilar > Recompilar solución. La DLL queda en `queja\bin\queja.dll`. Verificar que su fecha sea nueva.

## 2. Encontrar la ruta de la app en el servidor

```bash
sudo find / -name "principalnew.aspx" -not -path "*/proc/*" 2>/dev/null
```

Esa carpeta es `<RUTA_APP>`. Confirmar que existan ahí `bin/queja.dll` y `fotos_ant/`.

## 3. Respaldo en el servidor

```bash
cd <RUTA_APP>
sudo mkdir -p /root/respaldo_$(date +%F)
sudo cp -p bin/queja.dll principalnew.aspx principalexpnew.aspx /root/respaldo_$(date +%F)/
```

## 4. Copiar solo los 3 archivos

Desde PowerShell en la PC:

```powershell
cd \\gabira1\RespSistemas1\Proyectos_Angel\proyectos\queja\queja
scp bin\queja.dll principalnew.aspx principalexpnew.aspx administrator@192.168.123.4:/tmp/
```

En el servidor:

```bash
cd /tmp
sudo cp queja.dll <RUTA_APP>/bin/
sudo cp principalnew.aspx principalexpnew.aspx <RUTA_APP>/
```

No sincronizar la carpeta completa ni usar opciones que borren archivos (por ejemplo `rsync --delete`): se perdería el `Web.config` de producción y el contenido de `fotos_ant/`. La publicación de Visual Studio (`FolderProfile.pubxml`) tiene `DeleteExistingFiles=true`, así que tampoco se debe publicar directo sobre la carpeta de producción.

## 5. Mover las fotos viejas de lote a la carpeta correcta

Antes del cambio, el alta por lote guardaba las fotos en `imagenes/quejas/{folio}/`. Las pantallas las leen de `fotos_ant/`.

Revisar qué hay:

```bash
cd <RUTA_APP>
ls -R imagenes/quejas | head -50
```

Copiar sin sobrescribir:

```bash
sudo cp -n imagenes/quejas/*/* fotos_ant/
sudo chown www-data:www-data fotos_ant/*
ls -l fotos_ant | grep 13545
ls -ld fotos_ant
```

`fotos_ant/` debe ser escribible por el usuario de Apache (normalmente `www-data`). Conservar `imagenes/quejas` unos días como respaldo.

## 6. Reiniciar y probar

```bash
sudo systemctl restart apache2
```

Pruebas:

1. Abrir la queja 13545 en consulta o investigadores: debe verse la foto.
2. Registrar una queja por lote nueva con una foto `.png`: debe verse en consulta.
3. Intentar guardar sin archivo y con un archivo de más de 4 MB: debe bloquear ambos casos.
4. Repetir las pruebas 2 y 3 en EXPO.

## Rollback

Restaurar desde `/root/respaldo_<fecha>/` los 3 archivos a `<RUTA_APP>` y reiniciar Apache.

## Referencia: correo y DNS del servidor

Si los correos dejan de salir, revisar en este orden:

1. DNS: `getent hosts mail1.mrlucky.com.mx` debe devolver una IP (`198.72.117.168` a octubre 2026). El DNS interno `192.168.123.3` estuvo caído; `/etc/netplan/50-cloud-init.yaml` tiene `8.8.8.8` como respaldo. En YAML no usar tabuladores, solo espacios.
2. Red: `nc -zv mail1.mrlucky.com.mx 587`.
3. Certificados de Mono: `sudo update-ca-certificates`. El certificado de `mail1` lo emite SSL.com y vence el 29 de enero de 2027.
4. Prueba de envío con `mono /tmp/test.exe` (usar la contraseña SMTP vía variable `SMTP_PASS`, sin escribirla en el historial).
5. Reiniciar `apache2` y `mono-xsp4`.

Pendientes conocidos: el DNS `192.168.123.3` sigue sin responder; `nginx` aparece en estado `failed`; la contraseña SMTP está en texto plano en el código fuente.
