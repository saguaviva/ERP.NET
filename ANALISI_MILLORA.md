# Análisis de optimizaciones de velocidad (ERP.NET)

Este documento prioriza mejoras de **rendimiento** observadas en el código actual.

## 1) Reducir I/O de base de datos: evitar `SELECT *` y traer solo columnas usadas

**Evidencia**
- En `CargarTodosLosAcabados` se usa `SELECT * FROM dacabats` y luego solo se consumen `CODI`, `PREUM` y `PREUK`.

**Impacto**
- Más bytes por consulta, más parsing, más memoria en `DataTable`.

**Optimización propuesta**
- Sustituir por: `SELECT CODI, PREUM, PREUK FROM dacabats WHERE PROVE = @prove`.
- Mantener índices en `(PROVE, CODI)`.

## 2) Evitar consultas repetidas al cambiar detalle (cache por proveedor)

**Evidencia**
- `New(...)` y `CambioDetalle(...)` llaman a `CargarTodosLosAcabados()` y recargan el mismo catálogo para el mismo `ACABADOR`.

**Impacto**
- Repetición de round-trips a BD cuando el usuario navega por tejidos del mismo proveedor.

**Optimización propuesta**
- Cache en memoria por clave `PROVE` (DataTable/DataView) con invalidación simple al guardar maestros.

## 3) Evitar coste O(n log n) extra por sort reiterado en DataView

**Evidencia**
- Se cambia `dvForm.Sort` varias veces en el flujo de carga (`ORDEN`, luego `ACABAT`, después otra vez `ORDEN`).

**Impacto**
- Cada cambio de sort puede reordenar el DataView completo.

**Optimización propuesta**
- Ordenar una sola vez al final del flujo o mantener una estrategia de sort estable por caso de uso.

## 4) Evitar búsquedas lineales repetidas en bucles críticos

**Evidencia**
- `PonerPrecioAcabados()` recorre `dvForm` y por cada fila hace `dvTodosLosAcabados.Find(...)`.

**Impacto**
- Con muchos acabados, el coste de búsqueda acumulado crece y penaliza la UI.

**Optimización propuesta**
- Crear diccionario en memoria `Dictionary(Of String, (PREUM, PREUK))` una vez por carga.
- Calcular el precio sobre acceso O(1) medio por acabado.

## 5) Reducir trabajo innecesario al guardar detalles

**Evidencia**
- `ActualizarOrigen()` siempre llama `ActualizarDetalle()` y luego `MyBase.ActualizarOrigen(...)`.

**Impacto**
- Puede disparar comprobaciones/guardados incluso sin cambios efectivos.

**Optimización propuesta**
- Cortocircuito temprano si `tabla.GetChanges Is Nothing`.
- Guardado diferido (batch) cuando se realizan múltiples cambios en secuencia.

## 6) Evitar re-render y eventos durante cargas masivas

**Evidencia**
- Se hace `da.Fill(tabla)` + `PonerDefaults()` + `AcceptChanges()` con `RowChanged` suscrito.

**Impacto**
- Más eventos y potencial coste de UI si hay enlazados activos.

**Optimización propuesta**
- Suspender eventos/binding durante carga (`cargando=True`, `SuspendBinding` si aplica) y reactivar al final.

## 7) Índices SQL recomendados para consultas frecuentes

**Evidencia**
- Filtros frecuentes detectados:
  - `acabatsteixits` por `CENTRO`, `TEIXIT`, `PROVE`.
  - join con `dacabats` por `CODI`, `PROVE`.

**Optimización propuesta**
- Índice compuesto en `acabatsteixits (CENTRO, TEIXIT, PROVE, ACABAT)`.
- Índice compuesto en `dacabats (PROVE, CODI)`.

## Quick wins (orden sugerido)

1. Reemplazar `SELECT *` por columnas necesarias + SQL parametrizada.
2. Añadir índices compuestos en las tablas más consultadas.
3. Cache por `PROVE` en catálogo de acabados.
4. Consolidar `Sort` y reducir eventos durante cargas.
5. Introducir diccionario para cálculo de precios en memoria.

## Métricas para validar mejora

- Tiempo de `CambioDetalle()` (p50/p95).
- Número de consultas SQL por cambio de tejido.
- Tiempo de render/refresco del formulario al cambiar registro.
- Filas leídas y bytes transferidos por consulta de acabados.
# Anàlisi de punts de millora (ERP.NET)

Aquest document recull millores tècniques detectades en una revisió ràpida de codi.

## 1) SQL concatenat (risc de SQL injection i errors de format)

**Evidència**
- Construcció de consultes SQL concatenant valors directament (incloent cadenes):
  - `clsAcabadosTejidos`: filtrat per `CENTRO`, `TEIXIT`, `PROVE`. 
  - `frmModelos`: `SELECT codi FROM clients WHERE (NOM = "..." )`.

**Risc**
- Exposició a injecció SQL i a errors per escapament de cometes/caràcters especials.

**Millora recomanada**
- Migrar a consultes parametritzades (`@centro`, `@tejit`, `@prove`, `@nom`, ...).
- Centralitzar helper de creació de comandes SQL per reduir regressions.

## 2) Gestió d'errors silenciosa en getters

**Evidència**
- Patró repetit: `Catch ex As Exception : End Try` sense log ni re-llançar.

**Risc**
- Es perden errors de dades/mapeig i després apareixen símptomes difícils de diagnosticar.

**Millora recomanada**
- Substituir captures buides per:
  - logging contextual (nom de propietat + claus del registre), i
  - retorn controlat només en excepcions esperables.

## 3) Accessos a DataRow sense validacions prèvies

**Evidència**
- `tabla.Rows.Find(key).Delete()` s'executa sense comprovar si `Find` retorna `Nothing`.

**Risc**
- Excepcions en temps d'execució (NullReference) quan la clau no existeix.

**Millora recomanada**
- Patró segur:
  - `Dim row = tabla.Rows.Find(key)`
  - `If row IsNot Nothing Then row.Delete()`

## 4) Duplicació de SQL i lògica de càrrega

**Evidència**
- Mateix bloc SQL base i filtre repetit a `New(...)` i `CambioDetalle(...)` a `clsAcabadosTejidos`.

**Risc**
- Divergències funcionals en manteniment i més cost de canvi.

**Millora recomanada**
- Extreure mètode privat (p.ex. `BuildSelectAcabadosQuery`) i reutilitzar-lo.

## 5) Problemes de codificació de caràcters

**Evidència**
- Identificadors/llamades com `A�adirAcabado` o `A�adirBindingCombo` mostren caràcters mal codificats.

**Risc**
- Dificulta manteniment, cerca de símbols i pot provocar errors en tooling o generació.

**Millora recomanada**
- Estandarditzar codificació a UTF-8 (sense BOM o amb BOM segons política del projecte) i normalitzar noms de mètodes/fitxers.

## Priorització proposada

1. **Seguretat i robustesa BD**: parametrització SQL (punt 1).
2. **Diagnòstic d'errors**: eliminar catches silenciosos (punt 2).
3. **Estabilitat runtime**: validacions abans de `Delete` i accés a files (punt 3).
4. **Mantenibilitat**: refactor de duplicacions (punt 4).
5. **Qualitat de codi**: normalització de codificació (punt 5).
