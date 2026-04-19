# Publicar demo privada en Azure App Service

## Objetivo

Publicar `Erp.App` en una URL de pruebas tipo `https://tu-demo.azurewebsites.net` y exigir una puerta de acceso previa antes del login normal del ERP.

## Recomendaci\u00f3n

- usar `Azure App Service` con el dominio temporal `azurewebsites.net`;
- activar `PreviewAccess`;
- repartir a testers:
  - la URL de la demo;
  - la clave de acceso de pruebas;
  - y su usuario/contrase\u00f1a real del ERP.

## Variables de entorno m\u00ednimas

En App Service, dentro de `Configuration`, define al menos:

```text
ErpDatabase__Host=TU_HOST_MYSQL
ErpDatabase__Port=3306
ErpDatabase__Database=completex
ErpDatabase__Username=TU_USUARIO
ErpDatabase__Password=TU_PASSWORD
BootstrapSeed__PlatformAdminPassword=TU_PASSWORD_ADMIN_TEMPORAL
PreviewAccess__Enabled=true
PreviewAccess__RequireApprovedEmail=true
PreviewAccess__SharedPassword=DEMO-2026
```

Opcionales:

```text
PreviewAccess__CookieDays=14
BootstrapSeed__InitialTenantName=Grupo Completex
BootstrapSeed__InitialTenantSlug=grupo-completex
```

## Flujo esperado

1. el tester abre la URL de Azure;
2. ve la pantalla `Acceso privado de pruebas`;
3. introduce un email previamente aprobado y/o la clave compartida;
4. entra despu\u00e9s al login normal del ERP;
5. accede con su usuario real del sistema.

## Aprobar testers sin tocar variables

1. el tester entra en la landing p\u00fablica y usa `Acceso de pruebas`;
2. la solicitud queda guardada en `demo_access_requests`;
3. desde `PlatformAdmin`, en la app privada, puedes pulsar `Invitar emails solicitados`;
4. la app crea o reactiva invitaciones en `preview_access_invites`;
5. la puerta privada de la demo ya reconoce esos emails.

## Publicaci\u00f3n desde Visual Studio

1. clic derecho en `Erp.App`
2. `Publish`
3. `Azure`
4. `Azure App Service (Linux)` o `Windows`
5. seleccionar la suscripci\u00f3n y crear el recurso
6. publicar
7. configurar las variables anteriores en `Configuration`
8. reiniciar la app

## Nota operativa

Para una demo corta, puedes dejar `BootstrapOnStartup=true` si todav\u00eda est\u00e1s evolucionando el esquema. Para algo m\u00e1s serio, mejor apagarlo cuando la base ya est\u00e9 preparada.
