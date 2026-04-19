# Mapa Legacy -> SaaS

## Idea base

En el ERP WinForms antiguo, el concepto operativo dominante era `CENTRO`, cargado desde la tabla `filiales`.

En la solución SaaS nueva lo hemos separado en tres niveles:

- `PlatformAdmin`: administrador global de toda la solución SaaS.
- `Tenant`: grupo empresarial cliente de la plataforma.
- `Company`: empresa operativa dentro de un tenant.

Además, cada `Company` guarda un `LegacyCenterCode`, que es el puente con la base de datos actual.

## Equivalencias

### Legacy

- `filiales`
  - en la práctica era el catálogo de centros/empresas seleccionables.
- `CENTRO`
  - era el filtro operativo real en la mayoría de formularios.
- `empresaPorDefecto`
  - actuaba como centro por defecto.
- usuarios
  - no estaban modelados como SaaS multiempresa/multitenant; el filtrado fuerte solía hacerse por `CENTRO`.

### SaaS

- `PlatformAdmin`
  - ve todos los tenants, companies y usuarios de la solución.
- `Tenant`
  - representa un grupo empresarial o cliente SaaS.
- `Company`
  - representa una empresa operativa dentro del tenant.
  - se enlaza al legado mediante `LegacyCenterCode`.
- `ActiveCompany`
  - empresa actualmente seleccionada por el usuario.
- `User`
  - pertenece a un tenant y puede tener acceso a una o varias companies de ese tenant.

## Regla práctica de migración

- Un `tenant` agrupa varias empresas reales del mismo grupo.
- Cada `company` del tenant apunta a un `CENTRO` legacy distinto.
- Los datos legacy no se duplican al principio.
- La app web filtra por `tenant + active company`, y la `company` resuelve qué `LegacyCenterCode` usar en tablas como `clients`, `prove`, `factur`, `cactur`, etc.

## Tu caso actual

Si hoy tienes algo así:

- `COESPUNT` -> centro legacy `C`
- `COMPLETEX` -> centro legacy `M`

Entonces la estructura recomendada es:

- `Tenant`
  - `Grupo Completex`
- `Companies`
  - `Coespunt` con `LegacyCenterCode = C`
  - `Completex` con `LegacyCenterCode = M`

## Qué consigue esto

- Un solo tenant para todo el grupo actual.
- Varias companies dentro de ese tenant.
- Un usuario puede tener acceso solo a `Coespunt`, solo a `Completex`, o a ambas.
- Al cambiar la empresa activa en la web, cambiamos de `company`, y por tanto de `LegacyCenterCode`.

## Administradores y usuarios

### Global

- `PlatformAdmin`
  - administra toda la solución.
  - puede crear tenants.
  - puede crear companies dentro de cualquier tenant.
  - puede crear usuarios y asignarlos a companies.
  - puede ver usuarios de todos los tenants.

Esto ya encaja con el modelo actual de la app.

### Tenant

- `TenantAdmin`
  - administra solo su tenant.
  - no ve datos de otro tenant.
  - puede dar acceso de un usuario a una o varias companies de su tenant.

- `TenantReader`
  - entra en su tenant y trabaja en modo solo lectura sobre las companies asignadas.

## Qué no debemos hacer

No conviene mapear:

- `tenant = centro`
- `company = centro`

si sabemos que `C` y `M` pertenecen al mismo grupo empresarial.

Eso rompería la visión futura SaaS, porque convertiría cada centro histórico en un cliente separado, cuando en realidad son empresas del mismo grupo.

## Modelo recomendado para el arranque

Primera migración:

1. crear `Tenant = Grupo Completex`
2. crear `Company = Coespunt` con centro `C`
3. crear `Company = Completex` con centro `M`
4. dar acceso al usuario administrador del grupo a ambas companies
5. dar a cada usuario acceso solo a las companies que realmente use

## Cómo se comportan los datos

Ejemplo:

- si el usuario entra con empresa activa `Coespunt`, el CRM y Compras leen `CENTRO = 'C'`
- si cambia a `Completex`, las mismas pantallas leen `CENTRO = 'M'`

Eso permite convivir con la base de datos actual mientras migramos módulo a módulo.

## Resultado

La traducción correcta es:

- legacy `filial/centro` -> SaaS `company`
- conjunto de companies relacionadas -> SaaS `tenant`
- administrador de toda la plataforma -> `PlatformAdmin`

Para el grupo actual, `COESPUNT` y `COMPLETEX` deberían vivir dentro de un mismo `tenant`, cada una como `company` distinta enlazada a su centro legacy.
