# Publicar ERP SaaS en Hetzner Cloud VPS

## Objetivo

Publicar `Erp.App` en un VPS barato y estable de Hetzner Cloud con:

- Ubuntu 24.04 LTS;
- MariaDB local;
- Nginx como proxy inverso;
- HTTPS con Let's Encrypt;
- `systemd` para mantener la app arrancada;
- despliegue repetible desde Windows con PowerShell.

La app se publica como binario `self-contained`, por lo que el servidor no necesita tener instalado el runtime de .NET. Esto reduce problemas de versiones mientras el proyecto usa `net10.0`.

## Arquitectura recomendada

- VPS Hetzner Cloud `CX22` o superior.
- Sistema operativo `Ubuntu 24.04`.
- Arquitectura `x86_64`.
- Dominio tipo `erp.tudominio.com`.
- Base de datos local `erp_saas`.
- Aplicacion en `/opt/erp-saas/current`.
- Variables secretas en `/etc/erp-saas/erp-saas.env`.

Si se usa un servidor ARM, el publish debe cambiar de `linux-x64` a `linux-arm64`.

## 1. Preparar clave SSH en Windows

Comprueba si ya existe una clave:

```powershell
Test-Path "$env:USERPROFILE\.ssh\id_ed25519.pub"
```

Si devuelve `False`, crea una:

```powershell
ssh-keygen -t ed25519 -C "tu-email@dominio.com"
```

Copia la clave publica:

```powershell
Get-Content "$env:USERPROFILE\.ssh\id_ed25519.pub" | Set-Clipboard
```

En Hetzner se pega la clave que termina en `.pub`. Para conectar desde Windows se usa la clave privada, sin `.pub`.

Ejemplo correcto:

```powershell
ssh -i "$env:USERPROFILE\.ssh\id_ed25519" root@IP_DEL_SERVIDOR
```

Ejemplo incorrecto:

```powershell
ssh -i "C:\ssh2.pub" root@IP_DEL_SERVIDOR
```

Si Windows avisa de permisos demasiado abiertos en la clave privada:

```powershell
icacls "$env:USERPROFILE\.ssh\id_ed25519" /inheritance:r
icacls "$env:USERPROFILE\.ssh\id_ed25519" /remove:g "*S-1-1-0" "*S-1-5-32-545" "*S-1-5-11"
icacls "$env:USERPROFILE\.ssh\id_ed25519" /grant:r "$env:USERNAME:R"
```

## 2. Crear VPS en Hetzner

En Hetzner Cloud:

1. crea un proyecto;
2. crea un servidor;
3. selecciona `Ubuntu 24.04`;
4. selecciona `CX22` o superior;
5. anade tu SSH key;
6. activa firewall con puertos `22`, `80` y `443`;
7. opcionalmente activa backups si habra datos reales;
8. pega el siguiente `cloud-init` en el campo `Cloud config`.

Cambia `PEGA_AQUI_TU_SSH_PUBLIC_KEY` por tu clave publica.

```yaml
#cloud-config
package_update: true
package_upgrade: true

packages:
  - nginx
  - mariadb-server
  - unzip
  - curl
  - ufw
  - fail2ban
  - certbot
  - python3-certbot-nginx
  - unattended-upgrades

users:
  - name: deploy
    groups: [sudo]
    shell: /bin/bash
    sudo: ["ALL=(ALL) NOPASSWD:ALL"]
    ssh_authorized_keys:
      - PEGA_AQUI_TU_SSH_PUBLIC_KEY

write_files:
  - path: /etc/erp-saas/erp-saas.env
    permissions: "0600"
    owner: root:root
    content: |
      ASPNETCORE_ENVIRONMENT=Production
      ASPNETCORE_URLS=http://127.0.0.1:5000

      SaasDatabase__Host=127.0.0.1
      SaasDatabase__Port=3306
      SaasDatabase__Database=erp_saas
      SaasDatabase__Username=erp_app
      SaasDatabase__Password=CAMBIAR_PASSWORD_DB
      SaasDatabase__BootstrapOnStartup=true

      ErpDatabase__Host=127.0.0.1
      ErpDatabase__Port=3306
      ErpDatabase__Database=erp_saas
      ErpDatabase__Username=erp_app
      ErpDatabase__Password=CAMBIAR_PASSWORD_DB
      ErpDatabase__BootstrapOnStartup=true

      BootstrapSeed__PlatformAdminEmail=admin@tudominio.com
      BootstrapSeed__PlatformAdminPassword=CAMBIAR_PASSWORD_ADMIN

      LegacySync__NightlyEnabled=false

  - path: /etc/systemd/system/erp-saas.service
    permissions: "0644"
    content: |
      [Unit]
      Description=ERP SaaS CoreFlow
      After=network.target mariadb.service

      [Service]
      WorkingDirectory=/opt/erp-saas/current
      ExecStart=/opt/erp-saas/current/Erp.App
      EnvironmentFile=/etc/erp-saas/erp-saas.env
      Restart=always
      RestartSec=10
      KillSignal=SIGINT
      SyslogIdentifier=erp-saas
      User=www-data
      NoNewPrivileges=true

      [Install]
      WantedBy=multi-user.target

  - path: /etc/nginx/sites-available/erp-saas
    permissions: "0644"
    content: |
      server {
          listen 80;
          server_name _;

          client_max_body_size 100M;

          location / {
              proxy_pass http://127.0.0.1:5000;
              proxy_http_version 1.1;
              proxy_set_header Upgrade $http_upgrade;
              proxy_set_header Connection "Upgrade";
              proxy_set_header Host $host;
              proxy_set_header X-Real-IP $remote_addr;
              proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
              proxy_set_header X-Forwarded-Proto $scheme;
              proxy_cache_bypass $http_upgrade;
          }
      }

runcmd:
  - mkdir -p /opt/erp-saas/releases /opt/erp-saas/shared /etc/erp-saas
  - chown -R www-data:www-data /opt/erp-saas
  - ln -s /etc/nginx/sites-available/erp-saas /etc/nginx/sites-enabled/erp-saas
  - rm -f /etc/nginx/sites-enabled/default
  - nginx -t
  - systemctl reload nginx
  - ufw allow OpenSSH
  - ufw allow "Nginx Full"
  - ufw --force enable
  - systemctl enable erp-saas
```

## 3. Entrar al servidor

Si has usado el `cloud-init` anterior:

```powershell
ssh deploy@IP_DEL_SERVIDOR
```

Si no has creado el usuario `deploy`, entra como `root`:

```powershell
ssh root@IP_DEL_SERVIDOR
```

Si pide password, normalmente la SSH key no se ha anadido al servidor o estas usando una clave privada equivocada.

## 4. Crear passwords seguros

En el servidor:

```bash
openssl rand -base64 32
openssl rand -base64 32
```

Guarda uno para la base de datos y otro para el admin inicial.

## 5. Crear la base de datos

En el servidor:

```bash
sudo mariadb
```

Dentro de MariaDB:

```sql
CREATE DATABASE erp_saas CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
CREATE USER 'erp_app'@'localhost' IDENTIFIED BY 'PASSWORD_DB_FUERTE';
GRANT ALL PRIVILEGES ON erp_saas.* TO 'erp_app'@'localhost';
FLUSH PRIVILEGES;
EXIT;
```

Edita las variables reales:

```bash
sudo nano /etc/erp-saas/erp-saas.env
```

Cambia:

- `CAMBIAR_PASSWORD_DB`;
- `CAMBIAR_PASSWORD_ADMIN`;
- `admin@tudominio.com`.

No subas este archivo al repositorio.

## 6. Configurar dominio y HTTPS

Crea en tu DNS:

```text
A  erp.tudominio.com  IP_DEL_SERVIDOR
```

En el servidor:

```bash
sudo nano /etc/nginx/sites-available/erp-saas
```

Cambia:

```nginx
server_name _;
```

por:

```nginx
server_name erp.tudominio.com;
```

Valida Nginx:

```bash
sudo nginx -t
sudo systemctl reload nginx
```

Activa HTTPS:

```bash
sudo certbot --nginx -d erp.tudominio.com
```

## 7. Publicar desde Windows

Desde PowerShell:

```powershell
$server = "deploy@IP_DEL_SERVIDOR"
$version = Get-Date -Format "yyyyMMddHHmmss"
$repo = "C:\Users\sagua\source\repos\ERP.NET"
$project = "$repo\src\Erp.SaaS\Erp.App\Erp.App.csproj"
$out = "$repo\.publish\erp-saas-$version"
$zip = "$repo\.publish\erp-saas-$version.zip"

dotnet publish $project -c Release -r linux-x64 --self-contained true -o $out

if (Test-Path $zip) {
    Remove-Item $zip -Force
}

Compress-Archive -Path "$out\*" -DestinationPath $zip -Force
scp $zip "${server}:/tmp/erp-saas-$version.zip"

ssh $server "sudo mkdir -p /opt/erp-saas/releases/$version && sudo unzip -q /tmp/erp-saas-$version.zip -d /opt/erp-saas/releases/$version && sudo chmod +x /opt/erp-saas/releases/$version/Erp.App && sudo chown -R www-data:www-data /opt/erp-saas/releases/$version && sudo ln -sfn /opt/erp-saas/releases/$version /opt/erp-saas/current && sudo systemctl restart erp-saas && sudo systemctl status erp-saas --no-pager"
```

Si el servidor no tiene usuario `deploy`, cambia:

```powershell
$server = "root@IP_DEL_SERVIDOR"
```

## 8. Validar el despliegue

En el servidor:

```bash
sudo systemctl status erp-saas --no-pager
sudo journalctl -u erp-saas -f
```

Pruebas rapidas:

```bash
curl -I http://127.0.0.1:5000
curl -I https://erp.tudominio.com
```

Si la app arranca por primera vez con `BootstrapOnStartup=true`, creara o actualizara el esquema de base de datos al inicio.

## 9. Configurar sincronizacion legacy

Empieza con la sincronizacion nocturna apagada:

```text
LegacySync__NightlyEnabled=false
```

Cuando el acceso a la base legacy este verificado, anade al archivo `/etc/erp-saas/erp-saas.env`:

```text
LegacySourceDatabase__Host=HOST_LEGACY
LegacySourceDatabase__Port=3306
LegacySourceDatabase__Database=completex
LegacySourceDatabase__Username=USUARIO_LECTURA
LegacySourceDatabase__Password=PASSWORD_LECTURA
LegacySourceDatabase__BootstrapOnStartup=false
LegacySync__NightlyEnabled=true
```

El servidor legacy debe permitir conexiones desde la IP publica del VPS de Hetzner.

Reinicia:

```bash
sudo systemctl restart erp-saas
```

## 10. Rollback rapido

Lista releases:

```bash
ls -1 /opt/erp-saas/releases
```

Vuelve a una version anterior:

```bash
sudo ln -sfn /opt/erp-saas/releases/VERSION_ANTERIOR /opt/erp-saas/current
sudo systemctl restart erp-saas
```

## 11. Backup basico de MariaDB

Backup manual:

```bash
mysqldump -u erp_app -p erp_saas > ~/erp_saas_$(date +%F_%H%M).sql
```

Restaurar:

```bash
mysql -u erp_app -p erp_saas < ~/erp_saas_YYYY-MM-DD_HHMM.sql
```

Para produccion real, activa backups de Hetzner o snapshots periodicos ademas de los dumps.

## 12. Seguridad minima

- usa SSH keys, no password;
- limita el puerto `22` a tu IP si puedes;
- deja MariaDB solo en local;
- no subas `/etc/erp-saas/erp-saas.env` al repo;
- usa passwords generados con `openssl rand -base64 32`;
- activa backups antes de sincronizar datos reales;
- revisa logs despues de cada despliegue.

## 13. Referencias oficiales

- Hetzner Cloud, crear servidor: <https://docs.hetzner.com/cloud/servers/getting-started/creating-a-server/>
- Hetzner Cloud, firewalls: <https://docs.hetzner.com/cloud/firewalls/overview/>
- Microsoft, ASP.NET Core con Nginx y Linux: <https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx>
- Microsoft, instalar .NET en Ubuntu: <https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu-decision>
