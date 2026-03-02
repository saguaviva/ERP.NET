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
