# ERP SaaS local

La soluci\u00f3n est\u00e1 preparada para leer secretos desde `User Secrets` o variables de entorno.

## User Secrets

Los proyectos `Erp.App` y `Erp.Site` comparten el mismo `UserSecretsId`, as\u00ed que basta con configurar una vez:

```powershell
dotnet user-secrets set "ErpDatabase:Password" "TU_PASSWORD_MYSQL" --project .\src\Erp.SaaS\Erp.App
dotnet user-secrets set "BootstrapSeed:PlatformAdminPassword" "TU_PASSWORD_ADMIN_TEMPORAL" --project .\src\Erp.SaaS\Erp.App
```

Si necesitas cambiar m\u00e1s valores:

```powershell
dotnet user-secrets set "ErpDatabase:Host" "localhost" --project .\src\Erp.SaaS\Erp.App
dotnet user-secrets set "ErpDatabase:Database" "completex" --project .\src\Erp.SaaS\Erp.App
dotnet user-secrets set "ErpDatabase:Username" "root" --project .\src\Erp.SaaS\Erp.App
```

## Variables de entorno

Tambi\u00e9n puedes usar:

```powershell
$env:ErpDatabase__Password="TU_PASSWORD_MYSQL"
$env:BootstrapSeed__PlatformAdminPassword="TU_PASSWORD_ADMIN_TEMPORAL"
```

## Nota de bootstrap

`BootstrapOnStartup` est\u00e1 en `true` para que el esquema siga evolucionando autom\u00e1ticamente en desarrollo. Cuando quieras endurecer el arranque, podemos moverlo a `Development` o dejarlo solo en un script de migraci\u00f3n.

## Acceso privado para demos

La app privada puede ponerse detr\u00e1s de una puerta de pruebas antes del login real. Se activa con la secci\u00f3n `PreviewAccess`.

Ejemplo con variables de entorno:

```powershell
$env:PreviewAccess__Enabled="true"
$env:PreviewAccess__RequireApprovedEmail="true"
$env:PreviewAccess__SharedPassword="DEMO-2026"
```

Comportamiento:

- si configuras `SharedPassword`, la demo pide una clave compartida;
- si activas `RequireApprovedEmail`, la demo solo deja pasar a emails aprobados desde base de datos;
- si configuras `AllowedEmails`, esa lista también se admite como lista blanca fija;
- si configuras email aprobado y clave compartida, exige ambos;
- despu\u00e9s de pasar esa puerta, el usuario entra en el login normal del ERP.

Flujo recomendado:

- la landing p\u00fablica recoge solicitudes en [DemoAccess.cshtml](/C:/Users/sagua/source/repos/ERP.NET/src/Erp.SaaS/Erp.Site/Pages/DemoAccess.cshtml);
- `PlatformAdmin` revisa esas solicitudes y crea invitaciones desde [PlatformAdmin.razor](/C:/Users/sagua/source/repos/ERP.NET/src/Erp.SaaS/Erp.App/Components/Pages/PlatformAdmin.razor);
- la puerta privada de [PreviewAccess.razor](/C:/Users/sagua/source/repos/ERP.NET/src/Erp.SaaS/Erp.App/Components/Pages/PreviewAccess.razor) valida el email aprobado antes del login real.

## Azure App Service

He dejado una gu\u00eda corta para publicar la demo privada con el dominio temporal de Azure y la puerta de pruebas activada:

- [deploy-azure-app-service.md](/C:/Users/sagua/source/repos/ERP.NET/src/Erp.SaaS/docs/deploy-azure-app-service.md)

## Mapeo legacy

La equivalencia entre `filiales`, `CENTRO`, `tenant` y `company` est\u00e1 documentada en [legacy-to-saas-mapping.md](/C:/Users/sagua/source/repos/ERP.NET/src/Erp.SaaS/docs/legacy-to-saas-mapping.md).
