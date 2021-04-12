Public Class clsConstantes

#Region "Tamaños"

    Public Const AltoEtiquetaEnvio As Integer = 77
    Public Const AltoEtiquetaTejido As Integer = 79
    Public Const twins2mm As Integer = 55

#End Region

    Public Shared dtFiliales As New DataTable

    Public Shared mHost As String

    Public Const pubAño As Integer = 2005
    Public Const siguiente As Short = 1
    Public Const anterior As Short = 2
    Public Const ultimo As Short = 3
    Public Const primero As Short = 4
    Public Const iraregistro As Short = 5
    Public Const iraRegistroNombre As Short = 6
    Public Const iraRegistroFila As Short = 7
    Public Const elMismo As Short = 8
    Public Const esCambioSeleccion As Short = -2
    Public Const noBoton As Short = 100
    Public Const siBoton As Short = 99
    Public Const n1 As Object = Nothing
    Public Shared idiomaImpresion As String = "es-ES"

    Public Const Compra = "C"
    Public Const Venta = "V"

    Public Const PAF = "D"
    Public Const Recibo = "R"

    Public Const Proforma As Char = "M"
    Public Const Albaran As Char = "A"
    Public Const Pedido As Char = "C"
    Public Const Factura As Char = "F"
    Public Const OrdenFabComplementos As Char = "O"
    Public Const OrdenModelos As Char = "P"
    Public Const Prenda As Char = "P"

    Public Const Modelo As Char = "M"
    Public Const Hilos As Char = "M"
    Public Const Muestra As Char = "M"
    Public Const Tejido As Char = "T"
    Public Const Fornitura As Char = "F"

    Public Const poner As Short = 110
    Public Const quitar As Short = 111

    Public Const soloLectura As Boolean = True
    Public Const modificable As Boolean = False

    Public Const esRegistroExistente As Boolean = True

    Public Const esListado As Short = 0
    Public Const esEleccion As Short = 1

    Public Shared empresaPorDefecto As Char = "C"
    Public Shared modoDebug As Boolean = False
    Public Shared directorio As String

    Public Shared HaciendoFormatBinding As Boolean = False

#Region "NOMBRES DE TABLAS"


    Public Const tablaPAISES As String = "paises"
    Public Const tablaMuestras As String = "MOSTRES"
    Public Const tablaProveedores As String = "prove"
    Public Const tablaLineasDisposiciones As String = "ddispos"
    Public Const tablaClientes As String = "clients"
    Public Const tablaMaq As String = "MAQ"
    Public Const tablaTemporadas As String = "temporadas"
    Public Const tablaModelos As String = "models"
    Public Const tablaForniturasModelos As String = "mforni"
    Public Const tablaDisposiciones As String = "dispos"
    Public Const tablaColores As String = "color"
    Public Const tablaTallas As String = "TALLA"
    Public Const tablaFornituras As String = "forni"
    Public Const tablaTransportistas As String = "trans"
    Public Const tablaTrabajos As String = "treballs"
    Public Const tablaFormaPago As String = "forpag"
    Public Const tablaAcabados As String = "dacabats"
    Public Const tablaMaquinas As String = "maqui"
    Public Const tablaBancos As String = "bancs"
    Public Const tablaTejidos As String = "teixits"
    Public Const tablaHiloColores As String = "filcol"
    Public Const tablaComposicionTejidos As String = "mattei"
    Public Const tablaCompras As String = "cactur"
    Public Const tablaLineasCompras As String = "dcactu"
    Public Const tablaVentas As String = "factur"
    Public Const tablaLineasVenta As String = "dfactu"
    Public Const tablaRepresentantes As String = "repres"
    Public Const tablaLineasRemesas As String = "dremesas"
    Public Const tablaRemesas As String = "remesas"
    Public Const tablaEmpresas As String = "filiales"
    Public Const tablaIVA As String = "IVA"
    Public Const tablaHilos As String = "fil"
    Public Const tablaTalleres As String = "tallers"
    Public Const tablaOrdenesModelos As String = "cordre"

#End Region

#Region "NOMBRES DE CAMPOS"

    Public Const CCPAISES As String = "CODI"
    Public Const CNPAISES As String = "NOM"

    Public Const CCDisposiciones As String = "IDDispos"

    Public Const CCClients As String = "CODI"
    Public Const CNClients As String = "NOM"

    Public Const CCProve As String = "CODI"
    Public Const CNProve As String = "NOM"

    Public Const CCRemesor As String = "CODI"
    Public Const CNRemesor As String = "NOM"

    Public Const CCForni As String = "CODI"
    Public Const CNForni As String = "DESCRI"

    Public Const CCTrans As String = "CODI"
    Public Const CNTrans As String = "NOM"

    Public Const CCTallers As String = "CODI"
    Public Const CNTallers As String = "NOM"

    Public Const CCRepres As String = "CODI"
    Public Const CNRepres As String = "NOM"

    Public Const CCTreballs As String = "CODI"
    Public Const CNTreballs As String = "DESCRI"

    Public Const CCForpag As String = "CODI"
    Public Const CNForpag As String = "DESCRIPCIO"

    Public Const CCBancs As String = "CODI"
    Public Const CNBancs As String = "DESCRIPCIO"

    Public Const CCMaqui As String = "CODI"
    Public Const CNMaqui As String = "DESCRI"

    Public Const CCTeixits As String = "CODI"
    Public Const CNTeixits As String = "DESCRI"

    Public Const CCFil As String = "CODI"
    Public Const CNFil As String = "DESCRI"

    Public Const CCMuestra As String = "CODI"
    Public Const CNMuestra As String = "DESCRI"

    Public Const CCModelo As String = "CODI"
    Public Const CNModelo As String = "DESCRI"

    Public Const CCFiliales As String = "CODI"
    Public Const CNFiliales As String = "DESCRI"

    Public Const CNIVA As String = "DESCRIPCIO"
    Public Const CCIVA As String = "CODI"

#End Region

End Class
