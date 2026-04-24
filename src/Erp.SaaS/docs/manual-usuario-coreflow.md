# Manual de usuario CoreFlow ERP SaaS

Versión v1.0 - 24/04/2026

## Índice
1. **Primeros pasos**: Preparar el entorno de trabajo antes de entrar en procesos operativos.
2. **Guía rápida para usuarios**: Accesos directos a las tareas más habituales y dónde resolverlas.
3. **Rutinas por rol**: Qué debería revisar cada perfil al empezar, durante el día y antes de cerrar.
4. **Cómo leer una pantalla**: Partes comunes de una pantalla: cabecera, filtros, KPIs, tabla, acciones y documentos.
5. **Capturas recomendadas**: Pantallas que conviene capturar para convertir el manual en una guía visual de formación.
6. **Estados, señales y datos críticos**: Interpretar estados, avisos y campos que no conviene dejar sin revisar.
7. **Conceptos comunes**: Empresa activa, idioma, idioma documental, tema visual, navegación, filtros y documentos.
8. **Mapa funcional**: Resumen de las áreas disponibles y pantallas principales.
9. **Workflows transversales**: Operaciones repetidas en toda la aplicación: buscar, crear, editar, imprimir y exportar.
10. **CRM**: Clientes, proveedores, duplicados, representantes, transportistas y talleres.
11. **Artículos**: Fornituras, hilos, tejidos, models, muestras, complementos y disposiciones.
12. **Base**: Maestros y parámetros que alimentan compras, ventas, producción y almacén.
13. **Producción**: Partes de acabado y órdenes de fabricación asociadas a muestras, models y disposiciones.
14. **Ventas**: Pedidos, albaranes, pre-facturación, facturas, remesas e Intrastat.
15. **Compras**: Pedidos, recepciones, facturas de proveedor y circuitos específicos de hilos y fabricación.
16. **Almacén**: Stock, traspasos, movimientos, inventarios, conteo ciego y ajustes.
17. **Workflows operativos principales**: Procedimientos concretos para resolver tareas habituales de trabajo.
18. **Procesos punta a punta**: Secuencias completas desde el dato maestro hasta documento, producción, stock o reporting.
19. **Criterios de calidad**: Qué comprobar antes de guardar, imprimir, exportar o cerrar un proceso.
20. **Resolución de incidencias**: Síntomas habituales, causa probable y primera acción recomendada.
21. **Checklists de cierre**: Rutinas de cierre diario, semanal y mensual para evitar trabajo pendiente oculto.
22. **Listados y estadísticas**: Consultas ejecutivas, reporting por áreas, exportaciones y lectura de KPIs.
23. **Alertas operativas**: Avisos de trabajo pendiente y señales de datos que conviene revisar.
24. **Administración**: Empresas, usuarios, tenant, plataforma y configuración de acceso.
25. **Buenas prácticas**: Recomendaciones para trabajar con seguridad, consistencia y trazabilidad.
26. **Preguntas frecuentes**: Respuestas rápidas a incidencias habituales.
27. **Glosario**: Términos frecuentes usados en el ERP.

## 1. Primeros pasos
CoreFlow ERP SaaS centraliza el trabajo diario que antes estaba repartido en pantallas legacy: CRM, artículos, producción, ventas, compras, almacén, listados, estadísticas y administración.

### Antes de empezar
- Entra con tu usuario autorizado.
- Comprueba que la empresa activa es la correcta.
- Selecciona el idioma de pantalla.
- Selecciona el idioma de impresiones/exportaciones.
- Elige tema visual.
- Despliega solo la categoría de menú que vas a usar.

## 2. Guía rápida para usuarios
- **Quiero encontrar un cliente**: ir a `CRM > Clientes`. Comprobar: Buscar por nombre, NIF, ciudad, email o teléfono. Revisar duplicados si hay varias coincidencias.
- **Quiero crear un cliente o proveedor**: ir a `CRM > Clientes / Proveedores`. Comprobar: Buscar antes de crear. Completar datos fiscales y de contacto mínimos antes de usarlo en documentos.
- **Quiero consultar una ficha de tejido**: ir a `Artículos > Tejidos`. Comprobar: Buscar por código o descripción. Abrir ficha, revisar datos técnicos, colores, escandallo, stock e impresiones.
- **Quiero lanzar un trabajo a planta**: ir a `Producción > Acabados`. Comprobar: Crear parte manual o desde muestra/disposición. Revisar acabador, máquina, cantidades, estado e imprimir parte.
- **Quiero preparar un inventario**: ir a `Almacén > Inventarios`. Comprobar: Crear inventario, filtrar alcance, imprimir hoja de conteo ciego, introducir recuentos y validar diferencias.
- **Quiero revisar una factura de cliente**: ir a `Ventas > Facturas`. Comprobar: Buscar factura, abrir detalle, revisar cliente, albaranes, cobro, pendiente, contabilidad, total y PDF.
- **Quiero exportar información**: ir a `Listados / Estadísticas / Intrastat`. Comprobar: Aplicar filtros, comprobar idioma documental, exportar y validar totales antes de compartir.
- **Quiero saber qué está pendiente**: ir a `Alertas / Dashboard / Estadísticas`. Comprobar: Empezar por alertas, luego KPIs y finalmente listados concretos del área afectada.

## 3. Rutinas por rol
- **Administración**: Revisar alertas, facturas pendientes, remesas, formas de pago, cobros/pagos y documentos con datos fiscales incompletos.
- **Comercial**: Revisar clientes, pedidos, albaranes, facturas, duplicados y trazabilidad de documentos por cliente.
- **Compras**: Revisar proveedores, pedidos, recepciones, facturas proveedor, diferencias recepción-factura y stock de hilos.
- **Producción / Planta**: Revisar partes vivos, disposiciones, acabadores, máquinas, impresiones de parte y carga por estado/semana.
- **Almacén**: Revisar stock actual, movimientos, traspasos, inventarios abiertos, conteos ciegos y diferencias pendientes de validar.
- **Dirección**: Revisar estadísticas por área: ventas, compras, producción, almacén, evolución, concentración y alertas principales.

## 4. Cómo leer una pantalla
- **Cabecera**: Indica módulo, título, descripción y acciones principales como Nuevo, Volver, PDF o Imprimir.
- **Empresa activa**: Determina los datos visibles. Si no ves registros esperados, comprueba primero la empresa.
- **Filtros**: Permiten acotar por texto, estado, fechas, almacén, proveedor, cliente, artículo o color según pantalla.
- **KPIs / tarjetas**: Resumen rápido de volumen, importes, diferencias, líneas, países, stock o actividad.
- **Tabla**: Lista los registros. Normalmente permite ordenar columnas y abrir detalle.
- **Acciones de fila**: Abrir, editar, imprimir, crear parte, PDF o acciones específicas del documento.
- **Paginación**: Controla página y tamaño. Si faltan resultados, revisa filtros antes de aumentar página.
- **Mensajes**: Avisos de validación, errores o notas de sincronización. No conviene ignorarlos.

## 5. Capturas recomendadas
- **Inicio / Dashboard**: debe verse Mostrar acceso inicial, tarjetas por área y navegación principal. Sirve para: Usuario nuevo entiende por dónde empezar.
- **Menu lateral plegado/desplegado**: debe verse Mostrar categorías CRM, Artículos, Base, Producción, Ventas, Compras, Almacén y Administración. Sirve para: Explicar que no faltan opciones: pueden estar plegadas.
- **Selector de empresa e idioma documental**: debe verse Mostrar empresa activa, idioma web e idioma de impresiones/exportaciones. Sirve para: Evitar errores por empresa o idioma antes de crear documentos.
- **CRM / Clientes**: debe verse Mostrar búsqueda, filtros, tabla, estado y acceso a duplicados. Sirve para: Formación de usuarios comerciales y administración.
- **Artículos / Tejidos ficha**: debe verse Mostrar ficha técnica, colores, costes, stock e impresiones. Sirve para: Explicar la ficha más rica del catálogo textil.
- **Artículos / Muestras o Complementos**: debe verse Mostrar desglose técnico y acciones de impresión/parte. Sirve para: Explicar el puente hacia producción.
- **Producción / Acabados**: debe verse Mostrar listado, estado, filtros e impresión de parte. Sirve para: Formación de planta y responsable de producción.
- **Ventas / Facturas**: debe verse Mostrar cliente, estado, pendiente, contabilidad, total y PDF. Sirve para: Explicar revisión antes de enviar o contabilizar.
- **Almacén / Inventarios**: debe verse Mostrar creación, conteo ciego, diferencias y cierre. Sirve para: Formación de almacén y recuentos de planta.
- **Estadísticas**: debe verse Mostrar rango de fechas, KPIs, comparativa y exportación. Sirve para: Formación para dirección y seguimiento semanal.
- **Alertas**: debe verse Mostrar lista de avisos, prioridad y enlace a origen. Sirve para: Explicar rutina diaria de revisión.

## 6. Estados, señales y datos críticos
- **Borrador**: Documento editable que aún no debería considerarse definitivo.
- **Emitida / Preparada**: Documento ya generado o listo para contabilidad/gestión según circuito.
- **Pendiente**: Queda acción por realizar: cobrar, pagar, recibir, validar o cerrar.
- **Cerrado**: Proceso finalizado. Solo modificar si hay autorización o corrección justificada.
- **Anulado**: Documento sin efecto operativo, pero mantenido por trazabilidad.
- **Con diferencia**: Hay desviación entre esperado y real. Revisar antes de cerrar inventario, recepción o ajuste.
- **Origen legacy**: Registro importado del sistema histórico. Modificar con especial cuidado.
- **Origen local**: Registro creado o modificado en la SaaS.

### Datos que conviene validar siempre
- **Cliente / proveedor**: Debe mostrar nombre reconocible, no solo código genérico, antes de enviar documentos.
- **Empresa y centro**: Validar siempre antes de crear documentos o sincronizar datos.
- **Fechas**: Afectan a facturación, Intrastat, estadísticas, recepciones e inventarios.
- **Código artículo / color**: Clave para stock, producción, escandallos y trazabilidad.
- **Cantidades e importes**: Revisar separadores, decimales, signos negativos y totales.
- **Estado**: Define si algo está vivo, pendiente, cerrado, anulado o requiere acción.
- **Idioma documental**: Validar antes de generar PDF, impresión o exportación para terceros.

## 7. Conceptos comunes
- **Empresa activa**: Todos los listados y documentos se consultan dentro de la empresa seleccionada. Cambiar empresa debe mantener la pantalla y recargar datos.
- **Idioma de la web**: Afecta a menús, botones, etiquetas y textos de pantalla.
- **Idioma de documentos**: Afecta a PDFs, impresiones y exportaciones. Puede ser distinto del idioma de la web.
- **Tema visual**: Cambia densidad y aspecto. Ultracompacto aprovecha casi toda la pantalla dejando margen de lectura.
- **Menu lateral**: Organizado por categorías plegables: CRM, Artículos, Base, Producción, Ventas, Compras, Almacén y Administración.
- **Origen legacy**: Indica que el registro procede del sistema histórico. La web guarda cambios locales salvo procesos de sincronización definidos.
- **Listados**: Normalmente permiten búsqueda, filtros, ordenación, paginación y acceso a ficha o impresión.
- **Documentos**: Pedidos, albaranes, facturas, partes, inventarios y remesas conservan trazabilidad entre pasos.

## 8. Mapa funcional
- **Dashboard**: Vision inicial por areas y accesos rapidos. Pantallas: `Inicio`
- **CRM**: Clientes, duplicados, proveedores, representantes, transportistas y talleres. Pantallas: `/crm/clientes, /crm/proveedores, /crm/talleres`
- **Articulos**: Catalogo textil y de producto: fornituras, hilos, tejidos, models, muestras, complementos y disposiciones. Pantallas: `/articulos/tejidos, /articulos/muestras, /articulos/models`
- **Base**: Maestros comunes: numeraciones, bancos, formas de pago, operaciones, maquinas, temporadas, IVA e incoterms. Pantallas: `/base-datos/...`
- **Produccion**: Partes de acabado, ordenes de fabricacion y seguimiento de estado. Pantallas: `/produccion/acabados`
- **Ventas**: Pedidos, albaranes, pre-facturacion, facturas, remesas e Intrastat. Pantallas: `/ventas/...`
- **Compras**: Proveedores, pedidos, recepciones, facturas y órdenes específicas. Pantallas: `/compras/...`
- **Almacen**: Stock actual, hilos, tejidos, models, movimientos, traspasos, inventarios y ajustes. Pantallas: `/almacen/...`
- **Listados**: Cuadros y consultas operativas por ventas, compras, produccion y almacen. Pantallas: `/listados`
- **Estadisticas**: KPIs, comparativas, evolucion y analitica por areas. Pantallas: `/estadisticas`
- **Alertas**: Panel de avisos operativos y revision de incidencias. Pantallas: `/alertas`
- **Administracion**: Gestion de tenant, plataforma, empresas y usuarios con permiso. Pantallas: `/administracion, /plataforma`

## 9. Workflows transversales
### Cambiar empresa activa
1. Abrir el selector Empresa activa del menú lateral.
2. Seleccionar la empresa de trabajo.
3. La pantalla actual se mantiene y se recarga con datos de esa empresa.
4. Verificar el nombre de empresa antes de crear documentos o movimientos.

### Cambiar idioma de la web
1. Abrir el selector Idioma.
2. Elegir Castellano, Català o English.
3. La navegación se mantiene en la pantalla actual.
4. Recordar que el idioma de pantalla no cambia necesariamente el idioma de PDFs o exportaciones.

### Cambiar idioma de impresiones y exportaciones
1. Abrir Idioma impresiones / exportaciones.
2. Elegir el idioma documental deseado.
3. Generar de nuevo el PDF, impresión o exportación.
4. Usar este selector cuando el usuario trabaja en un idioma pero el cliente necesita documentos en otro.

### Buscar y filtrar un listado
1. Entrar en el área correspondiente desde el menú lateral.
2. Escribir el criterio de búsqueda: código, cliente, artículo, documento, color o notas según pantalla.
3. Ajustar filtros adicionales como estado, fechas, almacén o solo clasificados.
4. Pulsar Buscar y revisar el contador de resultados.
5. Ordenar columnas pulsando sobre el encabezado cuando esté disponible.

### Crear un registro maestro
1. Entrar en el listado del maestro.
2. Pulsar Nuevo, Nueva o el boton equivalente.
3. Rellenar los campos obligatorios y los datos operativos.
4. Guardar y volver al listado para comprobar que aparece.
5. Si el registro viene de legacy, revisar que el origen y centro sean correctos antes de modificarlo.

### Editar un registro existente
1. Buscar el registro en el listado.
2. Pulsar Abrir o Editar.
3. Modificar únicamente los campos necesarios.
4. Guardar y revisar mensajes de validación.
5. En registros legacy, recordar que los cambios son locales en la SaaS salvo que se indique lo contrario.

### Imprimir o generar PDF
1. Abrir el documento o ficha.
2. Comprobar primero el idioma documental.
3. Pulsar Imprimir, PDF o Abrir ficha imprimible.
4. Revisar la previsualización antes de enviarlo al cliente o a planta.
5. Guardar el PDF con un nombre que incluya documento, cliente y fecha.

### Exportar datos
1. Aplicar filtros antes de exportar.
2. Comprobar el idioma de impresiones / exportaciones.
3. Usar Exportar CSV, Excel o el boton específico de la pantalla.
4. Abrir el archivo y revisar columnas, separadores de miles y decimales antes de compartir.

## 10. CRM
- **Clientes**: Buscar, crear, editar y revisar clientes. La bandeja de duplicados ayuda a detectar registros repetidos o dudosos.
- **Duplicados**: Muestra candidatos a duplicado para limpiar el maestro sin perder trazabilidad.
- **Proveedores**: Aunque aparece dentro del circuito de compras, forma parte del dato maestro comercial.
- **Representantes**: Mantiene la red comercial asociable a clientes y documentos.
- **Transportistas**: Mantiene agencias y transportistas usados en albaranes y expediciones.
- **Talleres**: Maestro operativo que puede alimentar producción, artículos y compras.

### Buenas prácticas del área
- Buscar por NIF, email o teléfono antes de crear un nuevo cliente o proveedor.
- Revisar la bandeja de duplicados cuando aparezcan nombres parecidos.
- Mantener datos fiscales y de contacto claros para evitar PDFs incompletos.
- No bloquear ni reactivar clientes sin confirmar el motivo con administración.

## 11. Artículos
- **Fornituras**: Consulta y mantenimiento de complementos de producto usados en fichas y costes.
- **Hilos**: Maestro de hilos con proveedor, coste, precio, IVA, notas y carta de colores.
- **Tejidos**: Ficha técnica con composición, gramaje, ancho, costes, stock, colores, escandallo, etiquetas, reposición y valoración.
- **Models**: Catalogo específico de models del legacy, con edición cuidadosa porque el origen histórico puede estar incompleto.
- **Muestras**: Ficha de muestra con desglose técnico, colores/tallas, impresión y conexion con producción.
- **Complementos**: Circuito paralelo a muestras para artículos complementarios y desglose técnico.
- **Disposiciones**: Documentos de preparacion/trabajo con cliente, acabador, color, piezas, kilos, impresión y creación de parte.

### Buenas prácticas del área
- Buscar por código legacy y descripción antes de crear una ficha nueva.
- Completar datos técnicos mínimos antes de usar el artículo en ventas, compras o producción.
- Revisar colores, composición, costes y stock antes de imprimir fichas o etiquetas.
- En models, ser especialmente prudente: el origen histórico puede estar incompleto.

## 12. Base
- **Empresas**: Gestion de empresas disponibles para el tenant cuando el usuario tiene permisos.
- **Numeraciones**: Control de series y contadores de documentos.
- **Mailing**: Campañas y comunicaciones imprimibles.
- **Bancos / cajas**: Maestro financiero usado en cobros, pagos y remesas.
- **Formas de pago**: Condiciones reutilizables en clientes, proveedores y documentos.
- **Operaciones**: Catalogo operativo para procesos productivos.
- **Maquinas**: Maestro de máquinas utilizado en producción, muestras y acabados.
- **Temporadas**: Clasificacion comercial o de coleccion.
- **Tipos de IVA**: Fiscalidad aplicable a documentos y maestros.
- **Incoterms**: Condiciones internacionales para operaciones comerciales.

### Buenas prácticas del área
- Cambiar maestros solo cuando se entienda donde se usan.
- Evitar duplicar formas de pago, máquinas u operaciones con nombres ligeramente distintos.
- Revisar numeraciones antes de empezar una serie documental nueva.
- Mantener incoterms, IVA y bancos/cajas con nombres comprensibles para usuario final.

## 13. Producción
- **Partes de acabado**: Registrar trabajo productivo, acabador, máquina, estado, origen, cantidades y notas.
- **Alta guiada**: Puede iniciarse desde una muestra, complemento o disposición cuando el flujo lo permite.
- **Impresion**: Genera un documento de planta con la información necesaria para ejecutar el trabajo.
- **Seguimiento**: El listado permite filtrar trabajo vivo y revisar estados para priorizar planta.

### Buenas prácticas del área
- No lanzar partes sin acabador, máquina, cantidades y notas suficientes para planta.
- Usar alta guiada desde muestra o disposición cuando exista, porque conserva trazabilidad.
- Imprimir el parte antes de pasar trabajo a planta.
- Revisar partes antiguos o bloqueados antes de crear nuevos.

## 14. Ventas
- **Pedidos Teixits-Mostres**: Circuito de pedidos específico para tejidos y muestras.
- **Albaranes**: Documentos de entrega y expedicion, con variantes para tejidos/muestras y models.
- **Pre-facturación**: Borradores previos a factura para revisar importes y trazabilidad.
- **Facturas**: Facturas emitidas con PDF, estado de cobro, contabilidad, albaranes asociados e importe pendiente.
- **Remesas**: Agrupacion de cobros para gestión bancaria.
- **Intrastat**: Generacion y revisión de datos comunitarios por mes, año, país y código NC.

### Buenas prácticas del área
- Antes de facturar, revisar cliente, albaranes asociados, importes y estado de cobro.
- Usar el idioma documental correcto antes de generar PDF.
- En Teixits-Mostres, seguir el circuito pedido > albarán > pre-facturación > factura.
- En Intrastat, comprobar empresa, mes, año, país y código NC antes de exportar.

## 15. Compras
- **Pedidos**: Pedidos generales de compra.
- **Comandes fils**: Pedidos específicos de hilos.
- **Ordenes de fabricación muestras/models**: Ordenes productivas ubicadas historicamente en compras dentro del legacy.
- **Recepciones**: Entrada de mercancía y control frente a pedido.
- **Albaranes entrega fils**: Recepciones específicas del circuito de hilos.
- **Facturas proveedor**: Registro y revisión de facturas recibidas.

### Buenas prácticas del área
- Cruzar pedido, recepción y factura antes de cerrar la revisión del proveedor.
- En hilos, revisar proveedor, color y cantidad recibida contra stock.
- No registrar factura de proveedor sin validar diferencias relevantes de recepción.
- Mantener proveedores completos para evitar incidencias fiscales o de trazabilidad.

## 16. Almacén
- **Stock actual**: Vista general de existencias por artículo, color, almacén y posicion.
- **Stock / Fils**: Consulta operativa de hilos con filtros por proveedor, color y tipo de movimiento.
- **Stock / Tejidos**: Vista específica de tejidos para cerrar simetria con el legacy.
- **Stock / Models**: Vista específica de stock asociado a models.
- **Inventarios**: Conteos por almacén, familia, artículo o color; incluye conteo ciego e impresión para planta.
- **Traspasos**: Movimientos entre almacenes o ubicaciones.
- **Movimientos**: Historico de entradas, salidas, ajustes y actividad por posicion.
- **Ajustes**: Regularizaciones controladas cuando hay diferencias justificadas.

### Buenas prácticas del área
- Consultar movimientos antes de hacer ajustes manuales.
- Usar traspasos para mover stock entre almacenes o posiciones, no ajustes si hay traslado real.
- En inventarios, usar conteo ciego para planta y mostrar diferencias solo al validar.
- Cerrar inventarios solo cuando las diferencias esten revisadas por responsable.

## 17. Workflows operativos principales
### Resolver duplicados de cliente
1. Entrar en CRM > Clientes > Bandeja de duplicados.
2. Filtrar por estado o buscar por nombre, NIF, ciudad, email o teléfono.
3. Comparar candidatos antes de actuar: nombre fiscal, NIF, email, teléfono, ciudad y origen.
4. Marcar para revisar cuando no haya seguridad suficiente.
5. Evitar eliminar datos sin validación comercial o administrativa.

### Crear o revisar un tejido
1. Entrar en Artículos > Tejidos.
2. Buscar por código, descripción, ancho, máquina, tejedor o acabador.
3. Abrir la ficha o crear un nuevo tejido.
4. Completar datos técnicos: composición, gramaje, ancho, tubular, costes y notas.
5. Revisar carta de colores, escandallo, acabados y stock.
6. Usar las impresiones de ficha, etiquetas, reposición o valoración cuando proceda.

### Pasar de muestra a producción
1. Entrar en Artículos > Muestras o Complementos.
2. Abrir la ficha y revisar el desglose técnico concreto.
3. Seleccionar la acción de crear parte u orden de fabricación cuando esté disponible.
4. Comprobar color, máquina, cantidades, acabador y notas antes de guardar.
5. Imprimir el parte para planta si el trabajo ya puede lanzarse.

### Trabajar con disposiciones
1. Entrar en Artículos > Disposiciones.
2. Filtrar por número, cliente, acabador, color, comanda o notas.
3. Abrir la disposición para revisar piezas, kilos, estado y origen.
4. Imprimir el documento o crear parte de acabado cuando proceda.
5. Evitar crear parte si faltan datos críticos de cliente, color o acabador.

### Circuito de ventas Teixits-Mostres
1. Entrar en Ventas > Pedidos Teixits-Mostres.
2. Registrar o revisar pedido y líneas.
3. Generar o consultar albaranes asociados cuando hay entrega.
4. Revisar pre-facturación antes de emitir factura.
5. Abrir la factura, comprobar pendiente, cobro, contabilidad y total.
6. Generar PDF en el idioma documental correcto.

### Circuito de compras de hilos
1. Entrar en Compras > Comandes fils.
2. Registrar pedido al proveedor con artículo/color y cantidades.
3. Al recibir mercancía, consultar o registrar albarán de entrega fils.
4. Revisar recepción frente a factura para detectar diferencias.
5. Consultar Stock / Fils para validar impacto en almacén.

### Revisar factura antes de enviarla o contabilizarla
1. Abrir Ventas > Facturas o Compras > Facturas según corresponda.
2. Buscar por número, cliente/proveedor, fecha o notas.
3. Comprobar nombre fiscal, fecha, vencimiento, líneas, base, IVA, total y estado.
4. Verificar si hay albaranes o recepciones asociados.
5. Generar PDF solo después de comprobar el idioma documental.

### Preparar Intrastat mensual
1. Entrar en Ventas > Intrastat.
2. Seleccionar empresa, mes y año.
3. Revisar países UE, líneas clasificadas, líneas sin NC code y base Intrastat.
4. Abrir detalle si hay importes inesperados, negativos o países no previstos.
5. Exportar Excel/CSV y validar totales antes de presentar o compartir.

### Hacer un traspaso de stock
1. Entrar en Almacén > Traspasos.
2. Crear nuevo traspaso.
3. Indicar origen, destino, artículo, color y cantidad.
4. Comprobar que el movimiento representa un traslado real y no una regularización.
5. Guardar y revisar Stock actual o Movimientos para confirmar el impacto.

### Inventario con conteo ciego
1. Entrar en Almacén > Inventarios.
2. Crear inventario nuevo o nuevo desde stock.
3. Filtrar por almacén, familia, artículo o color si el conteo es parcial.
4. Imprimir hoja de recuento ciego para planta: artículo, color y casilla de conteo, sin stock esperado ni importes.
5. Introducir conteos reales.
6. Validar diferencias de forma masiva por familia o almacén.
7. Cerrar el inventario solo cuando las diferencias esten revisadas.

### Consultar estadísticas por área
1. Entrar en Estadísticas.
2. Seleccionar rango de fechas.
3. Abrir Ventas, Compras, Producción o Almacén.
4. Leer primero KPIs principales, luego comparativa y evolución.
5. Exportar CSV si se necesita análisis externo.

### Revisar alertas operativas
1. Entrar en Alertas.
2. Priorizar avisos críticos o con impacto en cliente, stock o producción.
3. Abrir el enlace asociado al documento o maestro.
4. Corregir el dato origen si procede.
5. Volver a alertas para comprobar si el aviso desaparece o queda pendiente.

### Cierre operativo semanal
1. Revisar Alertas y resolver incidencias críticas.
2. Consultar Estadísticas por área para detectar desviaciones.
3. Revisar facturas pendientes, recepciones sin factura y partes de producción vivos.
4. Comprobar inventarios abiertos y diferencias no validadas.
5. Exportar listados necesarios y guardar evidencias antes de cerrar el periodo.

## 18. Procesos punta a punta
- **Alta de cliente hasta factura**: entrada: Cliente o pedido recibido. Recorrido: CRM > Cliente; Ventas > Pedido; Albarán; Pre-facturación; Factura. Resultado: Factura emitida con cliente, líneas, total y PDF revisado. Validación: Nombre fiscal correcto, importes coherentes, idioma documental adecuado y estado de cobro claro.
- **Muestra hasta parte de acabado**: entrada: Muestra o complemento con desglose técnico. Recorrido: Abrir ficha; revisar desglose; crear parte; asignar acabador/máquina; imprimir. Resultado: Parte listo para planta con trazabilidad a origen. Validación: Color, cantidad, máquina, acabador y notas de planta completos.
- **Compra de hilos hasta stock**: entrada: Necesidad de hilo o pedido a proveedor. Recorrido: Compras > Comandes fils; recepción; factura proveedor; Almacén > Stock / Fils. Resultado: Stock de hilo actualizado y recepción trazable. Validación: Proveedor, color, cantidad recibida y factura cruzados sin diferencias relevantes.
- **Disposicion hasta producción**: entrada: Disposicion preparada para trabajar. Recorrido: Artículos > Disposiciones; abrir; imprimir o crear parte de acabado. Resultado: Trabajo preparado para planta/acabador. Validación: Cliente, color, piezas, kilos, estado y acabador revisados.
- **Inventario hasta ajuste**: entrada: Necesidad de validar stock real. Recorrido: Almacén > Inventarios; conteo ciego; introducir recuento; validar diferencias; cerrar. Resultado: Stock regularizado con diferencias revisadas. Validación: Diferencias justificadas por familia/almacén y cierre autorizado.
- **Intrastat mensual**: entrada: Periodo mensual cerrado o en revisión. Recorrido: Ventas > Intrastat; seleccionar empresa/mes/año; revisar detalle; exportar. Resultado: Archivo de Intrastat preparado para revisión externa. Validación: Paises UE, NC code, importes y negativos revisados antes de presentar.
## 19. Criterios de calidad
- **Antes de crear**: revisar Buscar duplicados y confirmar empresa activa. Resultado esperado: Evitar registros repetidos y datos en empresa equivocada.
- **Antes de guardar maestro**: revisar Código, nombre, descripción, origen, notas y campos obligatorios. Resultado esperado: Registro localizable y reutilizable en otros módulos.
- **Antes de guardar documento**: revisar Cliente/proveedor, fecha, líneas, cantidades, precios, estado y observaciones. Resultado esperado: Documento coherente y trazable.
- **Antes de imprimir/PDF**: revisar Idioma documental, destinatario, importes, datos fiscales y logo/encabezado. Resultado esperado: Documento listo para cliente, proveedor o planta.
- **Antes de exportar**: revisar Filtros, rango de fechas, empresa, totales y formato numerico. Resultado esperado: Archivo consistente para Excel o reporting externo.
- **Antes de cerrar inventario**: revisar Diferencias, responsable, familias afectadas y movimientos pendientes. Resultado esperado: Cierre sin regularizaciones ocultas.
- **Antes de cerrar semana**: revisar Alertas, facturas pendientes, recepciones sin factura, partes vivos e inventarios abiertos. Resultado esperado: Periodo operativo controlado.
## 20. Resolución de incidencias
- **No aparecen datos**: causa probable: Empresa, filtros, fechas o permisos no coinciden. Primera acción: Limpiar filtros, comprobar empresa activa, ampliar fechas y revisar rol.
- **Aparece Cliente 123 en vez del nombre**: causa probable: La consulta no ha resuelto el maestro de cliente. Primera acción: Abrir cliente por código y revisar que exista nombre; si se repite, apuntar pantalla para corrección.
- **El PDF sale en otro idioma**: causa probable: Se cambió idioma web pero no idioma documental. Primera acción: Cambiar Idioma impresiones / exportaciones y generar de nuevo.
- **Un total parece incorrecto**: causa probable: Filtro parcial, linea negativa, abono o formato de decimales. Primera acción: Abrir detalle, revisar líneas, signos, base, IVA y total.
- **No veo una opción del menú**: causa probable: Categoria plegada o falta de permisos. Primera acción: Desplegar categoría; si no aparece, consultar rol con administración.
- **No se puede cerrar inventario**: causa probable: Diferencias sin validar o líneas incompletas. Primera acción: Filtrar por diferencias, revisar conteos y validar masivamente solo cuando proceda.
- **El listado va muy cargado**: causa probable: Rango o filtro demasiado amplio. Primera acción: Usar búsqueda, fechas y tamaño de página menor; en ultracompacto ocultar menú lateral.
- **Intrastat no cuadra**: causa probable: Mes/empresa incorrectos, NC code faltante o país no UE. Primera acción: Revisar detalle por factura y agrupacion por país/NC code antes de exportar.
## 21. Checklists de cierre
### Cierre diario
- Revisar Alertas y resolver las críticas.
- Comprobar documentos creados durante el día: cliente/proveedor, fecha, importes y estado.
- Revisar partes de producción vivos que deban imprimirse o asignarse.
- Comprobar movimientos de almacén relevantes y traspasos pendientes.
- Anotar incidencias que no puedan resolverse en el día.
### Cierre semanal
- Revisar Estadísticas por área y comparar con el periodo anterior.
- Cruzar recepciones de compras con facturas proveedor.
- Revisar facturas de venta pendientes de cobro o contabilizacion.
- Revisar inventarios abiertos y diferencias sin validar.
- Exportar listados necesarios para seguimiento interno.
### Cierre mensual
- Revisar Intrastat del mes por empresa antes de exportar.
- Comprobar remesas, cobros, pagos y facturas pendientes.
- Revisar stock parado, rotación y diferencias de inventario.
- Validar que no queden partes productivos antiguos sin estado claro.
- Guardar exportaciones y evidencias en la carpeta de cierre correspondiente.
## 22. Listados y estadísticas
Listados y Estadísticas permiten leer el negocio por bloques: ventas, compras, producción y almacén.
## 23. Alertas operativas
- Revisar alertas al iniciar la jornada.
- Resolver primero alertas con impacto en cliente, stock, facturación o producción.
- Abrir el documento origen antes de corregir.
## 24. Administración
Administración aparece para usuarios con permisos y permite gestionar tenant, plataforma, empresas, usuarios y acceso privado.
## 25. Buenas prácticas
- Comprueba siempre la empresa activa antes de crear documentos, movimientos o fichas.
- Usa búsquedas y filtros antes de crear un nuevo registro para evitar duplicados.
- Distingue entre idioma de pantalla e idioma de documentos: pueden ser distintos.
- No modifiques datos legacy sin revisar primero origen, centro y trazabilidad.
- En listados grandes, guarda o comparte la URL cuando quieras conservar filtros y paginación.
- Antes de cerrar inventarios, revisa diferencias por familia o almacén y documenta incidencias.
- Antes de enviar PDFs a cliente o proveedor, abre la previsualización y valida importes, idioma y datos fiscales.
- Usa alertas y estadísticas como rutina diaria: primero avisos, después trabajo operativo.
- En producción, no lances partes sin acabador, máquina, cantidades y notas mínimas de planta.
- En exportaciones, verifica separadores, totales y filtros aplicados antes de enviar a terceros.
## 26. Preguntas frecuentes
### Cambio de idioma y vuelvo a otra pantalla
El comportamiento esperado es permanecer en la pantalla actual. Si ocurre, revisar si la URL tiene parámetros raros o si la sesión ha caducado.
### Cambio de empresa y no veo datos
Puede que la empresa activa no tenga registros para ese módulo o que el usuario no tenga permisos sobre esa empresa.
### Veo 'Cliente 123' en vez del nombre
Significa que el documento tiene código de cliente pero no se ha resuelto el nombre en esa consulta. Conviene revisar el maestro de clientes y la consulta de esa pantalla.
### Un listado parece vacío
Revisar filtros activos, empresa, fechas, estado y tamaño de página. En Intrastat, comprobar mes/año y empresa.
### No aparece una opción de menú
Puede depender del rol del usuario o estar dentro de una categoría plegada del menú lateral.
### El PDF sale en otro idioma
Revisar el selector Idioma impresiones / exportaciones, no solo el idioma de la web.
## 27. Glosario
- **Tenant**: Grupo o entorno SaaS que contiene empresas, usuarios y configuración compartida.
- **Empresa activa**: Empresa sobre la que se filtran listados, documentos y maestros operativos.
- **Legacy**: Sistema histórico original desde el que se importan o sincronizan datos.
- **Origen legacy**: Registro procedente del sistema histórico; normalmente se conserva trazabilidad y centro.
- **Teixits-Mostres**: Circuito específico de tejidos y muestras heredado del funcionamiento legacy.
- **Model**: Tipo de artículo/circuito histórico del legacy, independiente de tejido o muestra.
- **Disposicion**: Documento operativo de preparacion o trabajo con cliente, color, piezas, kilos y acabador.
- **Parte de acabado**: Orden o parte productivo para planta/acabador.
- **Conteo ciego**: Inventario donde planta cuenta sin ver el stock esperado ni importes.
- **Intrastat**: Declaracion/agrupacion de operaciones intracomunitarias por país y código arancelario.
