Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Imports System.Xml

Public Class clsOtrasFunciones
    Public general As New clsFuncionesUtiles

#Region "TRASPASOS"

    Public Shared Function ObtenerDocNuevo(ByVal documento As String, Optional ByVal afacturaproforma As Boolean = False) As Object
        '! Changed ObtenerDocNuevo to Object
        Try
            If afacturaproforma = True Then
                Return Proforma
            End If
            Select Case documento
                Case Pedido : Return Albaran
                Case Albaran : Return Factura
            End Select
            Return ""
            MessageBox.Show("ERROR ObtenerDocNuevo")
        Catch ex As Exception
            LOG(ex.ToString) : CCN() : Throw ex
        End Try
    End Function
    '*****************************************************************************
    'A esta funcion no hace falta pasarle el tipo pq los paf no influye para la nuemeracion
    Public Function TraspasarNLineas(ByVal aAF As Integer, ByVal tejido As String, ByVal cantidad As Integer, ByVal color As String, ByVal documento As String, ByVal tipo As String, ByVal dvLineas As DataView, ByVal dePA As Integer, ByVal nlinea As Integer, ByVal CENTRO As Char)

        Dim cmdInsertar As New MySqlCommand
        Dim documentonuevo As String = ObtenerDocNuevo(documento)
        Dim cmdSel As MySqlCommand
        Dim nlineas As Integer
        Dim key(3) As String
        Try
            'TIPUS=M y DOCUMENT=A y introduciremos en DFACTU las lineas q tenemos
            'en el dgActual
            'Primero seleccionar 
            'NO BORRAR
            'Hay que buscar las lineas de AF de de aAF y mirar si tiene algun tejido
            'q cumpla lo de color, tejido, documentonuevo y tipo
            'Si existe le incrementamos la cantidad y recalculamos todos lo campos!! 
            'Esto ultimo parece que el programa no lo hace actualmente preguntar
            'End If
            'Miramos el numero de lineas del AF y insertamos una nueva con lo que nos han pasado
            cmdSel = New MySqlCommand("SELECT COUNT(*) FROM " & tablaLineasVenta & " WHERE (TIPUS = """ & tipo & """ AND DOCUMENT = """ & documentonuevo & """ AND FRA = """ & aAF & """ AND CENTRO = """ & CENTRO & """)", cnn)
            ACN()
            nlineas = cmdSel.ExecuteScalar + 1
            CCN()
            cmdInsertar.Connection = cnn
            'Solo hay una PAF a traspasar en este caso pq son las lineas
            'Obtenemos la linea a traspasar

            key(0) = nlinea
            key(1) = dePA
            key(2) = documento
            key(3) = CENTRO
            cmdInsertar.Connection = cnn
            'MessageBox.Show(dvLineas.Sort)
            dvLineas.Sort = "NLINEA, FRA, DOCUMENT, CENTRO"
            'MessageBox.Show(dvLineas.Sort)
            With dvLineas.Item(dvLineas.Find(key))
                cmdInsertar.CommandText = "INSERT INTO " & tablaLineasVenta & "( " & _
                " TIPUS, " & _
                " DOCUMENT,  " & _
                " FRA,  " & _
                " NLINEA,  " & _
                " MOSTRA,  " & _
                " DESCRI,  " & _
                " COLOR,  " & _
                " TALLA,  " & _
                " UNITATS,  " & _
                " PREU,  " & _
                " DTE,  " & _
                " IMPORT,  " & _
                " COMAN,  " & _
                " ALBAR,  " & _
                " FACTURA,  " & _
                " ESTOC,  " & _
                " KM,  " & _
                " NTRAS,  " & _
                " PERREBRE,  " & _
                " REBUT,  " & _
                " CENTRO " & _
                " ) VALUES ( " & _
                " """ & tipo & """,  " & _
                " """ & documentonuevo & """,  " & _
                " " & aAF & ",  " & _
                " " & nlineas & ",  " & _
                " """ & general.NS(.Item("MOSTRA")) & """,  " & _
                " """ & general.NS(.Item("DESCRI")) & """,  " & _
                " """ & general.NS(.Item("COLOR")) & """, " & _
                "  """ & general.NS(.Item("TALLA")) & """,  " & _
                " """ & general.NS(cantidad, True) & """,  " & _
                " """ & general.NS(.Item("PREU"), True) & """, " & _
                " """ & general.NS(.Item("DTE"), True) & """,  " & _
                " """ & general.NS(.Item("IMPORT"), True) & """,  " & _
                " " & .Item("FRA") & ",  " & _
                " ""0"", " & _
                " ""0"", " & _
                " ""0"", " & _
                " """ & .Item("KM") & """,  " & _
                " '', " & _
                " """ & general.NS(.Item("PERREBRE"), True) & """, " & _
                " """ & general.NS(.Item("REBUT"), True) & """, " & _
                " """ & general.NS(.Item("CENTRO"), True) & """)"
            End With
            dvLineas.Sort = "NLINEA"
            'Ahora hay que insertarlo en dcfactutrasp
            ACN()
            cmdInsertar.ExecuteNonQuery()
            CCN()
            Return nlineas
        Catch ex As Exception
            clsFuncionesLOG.LOG(ex.ToString) : CCN() : Return Nothing
        End Try
    End Function

    ''TIPUS=M y DOCUMENT=A y introduciremos en DFACTU las lineas q tenemos
    '''en el dgActual
    '''Primero seleccionar 
    '''NO BORRAR
    '''Hay que buscar las lineas de AF de de aAF y mirar si tiene algun tejido
    '''q cumpla lo de color, tejido, documentonuevo y tipo
    '''Si existe le incrementamos la cantidad y recalculamos todos lo campos!! 
    '''Esto ultimo parece que el programa no lo hace actualmente preguntar
    '''End If
    '''Miramos el numero de lineas del AF y insertamos una nueva con lo que nos han pasado


    Public Function TraspasarNLineasCompra(ByVal aAF As Integer, ByVal tejido As String, ByVal cantidad As Integer, ByVal color As String, ByVal documento As String, ByVal tipo As String, ByVal dvLineas As DataView, ByVal dePA As Integer, ByVal nlinea As Integer, ByVal CENTRO As Char)
        Dim cmdInsertar As New MySqlCommand
        Dim documentonuevo As String = ObtenerDocNuevo(documento)
        Dim cmdSel As MySqlCommand
        Dim nlineas As Integer
        Dim key(4) As String
        Try
            cmdSel = New MySqlCommand("SELECT COUNT(*) FROM " & tablaLineasCompras & " WHERE (TIPUS = """ & tipo & """ AND " & _
                            " DOCUMENT = """ & documentonuevo & """ AND FRA = """ & aAF & """ AND CENTRO = """ & CENTRO & """)", cnn)
            ACN()
            nlineas = cmdSel.ExecuteScalar + 1
            CCN()
            cmdInsertar.Connection = cnn
            'Solo hay una PAF a traspasar en este caso pq son las lineas
            'Obtenemos la linea a traspasar
            dvLineas.Sort = "NLINEA, FRA, TIPUS, DOCUMENT, CENTRO"
            key(0) = nlinea
            key(1) = dePA
            key(2) = tipo
            key(3) = documento
            key(4) = CENTRO
            cmdInsertar.Connection = cnn
            With dvLineas.Item(dvLineas.Find(key))
                cmdInsertar.CommandText = "INSERT INTO " & tablaLineasCompras & "( " & _
                    " TIPUS,  " & _
                    " DOCUMENT,  " & _
                    " FRA,  " & _
                    " NLINEA,  " & _
                    " ARTICLE, " & _
                    " DESCRI,  " & _
                    " TALLA,  " & _
                    " UNITATS,  " & _
                    " PREU,  " & _
                    " DTE, " & _
                    " IMPORT,  " & _
                    " COMAN,  " & _
                    " ALBAR,  " & _
                    " FACTURA,  " & _
                    " ESTOC,  " & _
                    " COLOR,  " & _
                    " KM,  " & _
                    " PERREBRE,  " & _
                    " REBUT,  " & _
                    " CENTRO)  " & _
                    " VALUES (" & _
                    " """ & tipo & """, " & _
                    " """ & documentonuevo & """, " & _
                    " " & aAF & ", " & _
                    " " & nlineas & ", " & _
                    " """ & general.NS(.Item("ARTICLE")) & """,  " & _
                    " """ & general.NS(.Item("DESCRI")) & """, " & _
                    " """ & general.NS(.Item("TALLA")) & """, " & _
                    " """ & general.NS(cantidad, True) & """,  " & _
                    " """ & general.NS(.Item("PREU"), True) & """, " & _
                    " """ & general.NS(.Item("DTE"), True) & """, " & _
                    " """ & general.NS(.Item("IMPORT"), True) & """,  " & _
                    " " & .Item("FRA") & ", " & _
                    " ""0"", " & _
                    " ""0"",'',""" & .Item("COLOR") & """, """ & .Item("KM") & """,  " & _
                    " """ & general.NS(.Item("PERREBRE"), True) & """,""" & general.NS(.Item("REBUT"), True) & """,""" & general.NS(.Item("CENTRO"), True) & """)"
            End With
            dvLineas.Sort = "NLINEA"
            'Ahora hay que insertarlo en dcfactutrasp
            ACN()
            cmdInsertar.ExecuteNonQuery()
            CCN()
            Return nlineas
        Catch ex As Exception
            clsFuncionesLOG.LOG(ex.ToString) : CCN() : Throw ex
        End Try
    End Function
    Public Function CrearPAFCompra(ByVal fecha As Date, ByVal AF As Integer, ByVal tipo As String, ByVal documento As String, _
                        ByVal dv As DataView, ByVal centro As String, Optional ByVal adcfijo As Integer = -1) As Integer
        Dim documentonuevo As String
        Dim UltimoPAF As Double
        Dim cmdInsertar As New MySqlCommand
        Dim fecha2 As String
        Try
            documentonuevo = ObtenerDocNuevo(documento)
            If adcfijo = -1 Then : UltimoPAF = GetNumeroUltimoPAFCompra(documentonuevo, tipo, centro) + 1
            Else : UltimoPAF = adcfijo : End If

            cmdInsertar.Connection = cnn
            dv.RowFilter = "TIPUS = '" & tipo & "' AND DOCUMENT = '" & documento & "' AND FRA = '" & AF & "' AND CENTRO = '" & centro & "'"
            ACN()


            With dv.Item(0)
                fecha2 = fecha.Year & "-" & fecha.Month & "-" & fecha.Day
                cmdInsertar.CommandText = "INSERT INTO " & tablaCompras & " " & _
                    " (TIPUS, DOCUMENT, FRA, " & _
                    " PROVE, NRO, DATA,  " & _
                    " DATAENTREGA,  " & _
                    " BASE1, " & _
                    " P_IVA1, " & _
                    " IVA1, " & _
                    " TOTAL, P_DTE, " & _
                    " DTE1, " & _
                    " BRUT1, TRASPAS,  " & _
                    " P_RE1, RE1, VENCIM, " & _
                    " TRASPASF, OBSERV, CENTRO)  " & _
                    " VALUES( " & _
                    " """ & tipo & """, """ & documentonuevo & """, """ & UltimoPAF & """,  " & _
                    " """ & .Item("PROVE") & """, """ & .Item("NRO") & """, """ & fecha2 & """,  " & _
                    " """ & .Item("DATAENTREGA") & """, " & _
                    " """ & general.NS(.Item("BASE1"), True) & """, " & _
                    " """ & general.NS(.Item("P_IVA1"), True) & """,  " & _
                    " """ & general.NS(.Item("IVA1"), True) & """, " & _
                    " """ & general.NS(.Item("TOTAL"), True) & """, """ & general.NS(.Item("P_DTE"), True) & """, " & _
                    " """ & general.NS(.Item("DTE1"), True) & """, " & _
                    " """ & general.NS(.Item("BRUT1"), True) & """, """ & .Item("TRASPAS") & """, " & _
                    " """ & general.NS(.Item("P_RE1"), True) & """, " & _
                    " """ & general.NS(.Item("RE1"), True) & """, """ & .Item("VENCIM") & """, " & _
                    " """ & .Item("TRASPASF") & """, """ & general.NS(.Item("OBSERV")) & """, """ & centro & """) "

                cmdInsertar.ExecuteNonQuery()
                CCN()
            End With
            ActualizarNum("FACTUR", documentonuevo, centro, UltimoPAF, tipo)
            Return UltimoPAF
            'Crear un PAF nuevo segun sea el documento actual un P o A o F

        Catch ex As Exception
            clsFuncionesLOG.LOG(ex.ToString) : CCN()
        End Try
    End Function
    '************************************************************************
    ' Si adocfijo es un -1 entonces hay que poner el siguiente en cambio si es un numero concrteto
    ' hay que traspasarlo a ese documento
    '************************************************************************
    Public Function CrearPAF(ByVal fecha As Date, ByVal AF As Integer, ByVal documento As String, ByVal tipo As String, ByVal dv As DataView, ByVal CENTRO As String, Optional ByVal adcfijo As Integer = -1) As Integer
        Dim documentonuevo As String
        Dim UltimoPAF As Double
        Dim cmdInsertar As New MySqlCommand
        Try
            documentonuevo = ObtenerDocNuevo(documento)
            If adcfijo = -1 Then
                UltimoPAF = GetNumeroUltimoPAF(documentonuevo, CENTRO) + 1
            Else
                UltimoPAF = adcfijo
            End If
            cmdInsertar.Connection = cnn
            dv.RowFilter = "DOCUMENT = '" & documento & "' AND FRA = '" & AF & "' AND CENTRO = '" & CENTRO & "'"
            ACN()
            With dv.Item(0)
                Dim fecha2 As String
                fecha2 = fecha.Year & "-" & fecha.Month & "-" & fecha.Day
                cmdInsertar.CommandText = "INSERT INTO " & tablaVentas & " (DOCUMENT, TIPUS, FRA, CLIENT, DATA, " & _
                        " BASE1, P_IVA1, IVA1, TOTAL, P_DTE, " & _
                        " DTE1, BRUT1, TRASPAS, P_RE1, RE1, " & _
                        " VENCIM, TRASPASF, OBSERV, REPRES, COMIS, CENTRO) VALUES " & _
                        " (""" & documentonuevo & """, """ & tipo & """, """ & UltimoPAF & """, """ & .Item("CLIENT") & """, """ & fecha2 & """, " & _
                        " """ & general.NS(.Item("BASE1"), True) & """, " & _
                        " """ & general.NS(.Item("P_IVA1"), True) & """, " & _
                        " """ & general.NS(.Item("IVA1"), True) & """, " & _
                        " """ & general.NS(.Item("TOTAL"), True) & """, """ & general.NS(.Item("P_DTE"), True) & """, """ & general.NS(.Item("DTE1"), True) & """, " & _
                        " """ & general.NS(.Item("BRUT1"), True) & """, " & _
                        " " & .Item("TRASPAS") & ", " & _
                        " """ & general.NS(.Item("P_RE1"), True) & """, " & _
                        " """ & general.NS(.Item("RE1"), True) & """, " & _
                        " """ & .Item("VENCIM") & """, " & nzn(.Item("TRASPASF"), 0) & ", """ & general.NS(.Item("OBSERV")) & """, " & _
                        " """ & nzn(.Item("REPRES"), 0) & """, """ & general.NS(.Item("COMIS"), True) & """, """ & .Item("CENTRO") & """)"
                cmdInsertar.ExecuteNonQuery()
                CCN()
            End With
            CrearPAF = UltimoPAF
            ActualizarNum("FACTUR", documentonuevo, CENTRO, UltimoPAF, "T")
            'Crear un PAF nuevo segun sea el documento actual un P o A o F
        Catch ex As Exception
            clsFuncionesLOG.LOG(ex.ToString) : CCN()
        End Try
    End Function

#End Region

#Region "NUMERACIONES BASADA EN TABLAS"

    '*****************************************************************************
    'A esta funcion no hace falta pasarle el tipo pq los paf no influye para la nuemeracion
    '*****************************************************************************
    Public Shared Function GetNumeroUltimoPAF2(ByVal doc As String, ByVal CENTRO As Char, Optional ByVal cliente As Integer = -1) As Double
        Dim fra As Double
        Dim fra2 As Double
        Dim cmdSelectPAF As New MySqlCommand

        Dim strCliente As String = ""
        Try
            If cliente <> -1 Then
                strCliente = " client = " & cliente & " AND "
            End If
            Select Case doc
                Case OrdenModelos
                    cmdSelectPAF.Connection = cnn
                    cmdSelectPAF.CommandText = "select IFNULL(max(ORDRE),0) from CORDRE WHERE CENTRO = """ & CENTRO & """ "
                    ACN()
                    fra = nzn(cmdSelectPAF.ExecuteScalar, 0)
                    CCN()
                    cmdSelectPAF = Nothing
                    Return fra
                Case Factura
                    cmdSelectPAF.Connection = cnn
                    ACN()
                    cmdSelectPAF.CommandText = "SELECT IFNULL(max(FRA),0) FROM FACTUR WHERE " & strCliente & " DOCUMENT = ""F"" AND FACTUR.CENTRO = """ & CENTRO & """"
                    fra2 = nzn(cmdSelectPAF.ExecuteScalar, 0)
                    CCN()
                    Return fra2
                Case Albaran
                    cmdSelectPAF.Connection = cnn
                    ACN()
                    cmdSelectPAF.CommandText = "SELECT IFNULL(max(FRA),0) FROM FACTUR WHERE " & strCliente & " DOCUMENT = ""A"" AND FACTUR.CENTRO = """ & CENTRO & """"
                    fra2 = nzn(cmdSelectPAF.ExecuteScalar, 0)
                    CCN()
                    Return fra2
                Case Pedido
                    cmdSelectPAF.Connection = cnn
                    cmdSelectPAF.CommandText = "SELECT IFNULL(max(FRA),0) FROM FACTUR WHERE " & strCliente & " FACTUR.DOCUMENT = ""C"" AND FACTUR.CENTRO = """ & CENTRO & """"
                    ACN()
                    fra = nzn(cmdSelectPAF.ExecuteScalar, 0)
                    CCN()
                    Return fra
                    'Hay que comprobar a ver si habra proforma para modelos
                Case Proforma
                    cmdSelectPAF.Connection = cnn
                    cmdSelectPAF.CommandText = "SELECT IFNULL(max(FRA),0) FROM FACTUR WHERE " & strCliente & " FACTUR.DOCUMENT = ""M"" AND FACTUR.CENTRO = """ & CENTRO & """"
                    ACN()
                    fra = nzn(cmdSelectPAF.ExecuteScalar, 0)
                    CCN()
                    Return fra
            End Select

        Catch ex As Exception
            clsFuncionesLOG.LOG(ex.ToString) : CCN()
        End Try
    End Function
    Public Shared Function GetNumeroUltimoPAFCompra2(ByVal doc As String, ByVal tipo As String, ByVal CENTRO As Char) As Double
        Dim fra As Double
        Dim fra2 As Double
        Dim cmdSelectPAF As New MySqlCommand
        Try
            cmdSelectPAF.Connection = cnn
            Select Case doc
                Case OrdenModelos

                    cmdSelectPAF.CommandText = "SELECT IFNULL(max(ORDRE),0) FROM CORDRE WHERE CENTRO = """ & CENTRO & """ "
                    ACN()
                    fra = nzn(cmdSelectPAF.ExecuteScalar, 0)
                    CCN()
                    cmdSelectPAF = Nothing
                    Return fra
                Case OrdenFabComplementos
                    cmdSelectPAF.CommandText = "select IFNULL(max(FRA),0) FROM CACTUR WHERE DOCUMENT = ""O"" AND CENTRO = """ & CENTRO & """ "
                    ACN()
                    fra = nzn(cmdSelectPAF.ExecuteScalar, 0)
                    CCN()
                    cmdSelectPAF = Nothing
                    Return fra
                    'FACTURAS COMPRA
                Case Albaran

                    ACN()
                    fra = 0
                    'fra = nzn(cmdSelectPAF.ExecuteScalar, 0)
                    cmdSelectPAF.CommandText = "SELECT IFNULL(max(FRA),0) FROM CACTUR WHERE DOCUMENT = ""A"" AND CENTRO = """ & CENTRO & """ "
                    fra2 = nzn(cmdSelectPAF.ExecuteScalar, 0)
                    CCN()
                    Return Math.Max(fra, fra2)
                    'PEDIDOS COMPRA
                Case Pedido
                    Select Case tipo
                        Case Muestra
                            cmdSelectPAF.CommandText = "SELECT IFNULL(max(FRA),0) FROM CACTUR WHERE DOCUMENT = ""C"" AND TIPUS = ""M"" AND CENTRO = """ & CENTRO & """ "
                        Case Tejido, Fornitura
                            cmdSelectPAF.CommandText = "SELECT IFNULL(max(FRA),0) FROM CACTUR WHERE DOCUMENT = ""C"" AND (TIPUS = ""T"" OR TIPUS = ""F"") AND CENTRO = """ & CENTRO & """ "
                    End Select

                    ACN()
                    fra = nzn(cmdSelectPAF.ExecuteScalar, 0)
                    CCN()
                    Return fra
            End Select

        Catch ex As Exception
            clsFuncionesLOG.LOG(ex.ToString) : CCN()
        End Try
    End Function

#End Region

    Public Shared Function GetNumeroUltimoPAF(ByVal doc As String, ByVal CENTRO As Char, Optional ByVal cliente As Integer = -1) As Double
        Dim fra As Double
        Dim cmdSelectPAF As New MySqlCommand
        Dim strCliente As String = ""
        Try
            If cliente <> -1 Then strCliente = " client = " & cliente & " AND "

            Select Case doc
                Case OrdenModelos
                    'Ordenes fabricacion modelos
                    cmdSelectPAF.Connection = cnn
                    cmdSelectPAF.CommandText = "SELECT CODI FROM NUMERACIONES WHERE TABLA = ""ORDRE"" AND CENTRO = """ & CENTRO & """ "
                    ACN()
                    fra = nzn(cmdSelectPAF.ExecuteScalar, 0)
                    CCN()

                    'Numero de facturas, albaranes, pedidos y proforma son todos seguidos
                Case Factura, Albaran, Pedido, Proforma
                    cmdSelectPAF.Connection = cnn
                    ACN()
                    cmdSelectPAF.CommandText = "SELECT CODI FROM NUMERACIONES WHERE TABLA = ""FACTUR"" AND CENTRO = """ & CENTRO & """ AND DOCUMENT = """ & doc & """ "
                    fra = nzn(cmdSelectPAF.ExecuteScalar, 0)
                    CCN()
            End Select

            Return fra
        Catch ex As Exception
            clsFuncionesLOG.LOG(ex.ToString) : CCN()
        End Try
    End Function
    Public Shared Function GetNumeroUltimoPAFCompra(ByVal doc As String, ByVal tipo As String, ByVal CENTRO As Char) As Integer
        Dim fra As Integer
        Dim cmdSelectPAF As New MySqlCommand
        Try
            If tipo = Fornitura OrElse tipo = Tejido Then
                tipo = "T"
            Else
                tipo = "M"
            End If

            cmdSelectPAF.Connection = cnn
            cmdSelectPAF.CommandText = "SELECT CODI FROM NUMERACIONES " & _
                        " WHERE " & _
                        " DOCUMENT = """ & doc & """ AND " & _
                        " TABLA = ""CACTUR"" AND " & _
                        " TIPUS = """ & tipo & """ AND " & _
                        " CENTRO = """ & CENTRO & """ "
            ACN()
            fra = nzn(cmdSelectPAF.ExecuteScalar, 0)
            CCN()
            Return fra

        Catch ex As Exception
            clsFuncionesLOG.LOG(ex.ToString) : CCN()
        End Try
    End Function
    Public Shared Function ExisteID(ByVal tabla As String, ByVal ID As Integer, ByVal centro As Char) As Boolean
        Dim cmdSel As New MySqlCommand("SELECT CODI FROM " & tabla & _
                " WHERE CENTRO = """ & centro & """ AND " & _
                " CODI = " & ID & " ", cnn)
        Dim yaexiste As Boolean = False
        Try
            ACN()
            If cmdSel.ExecuteScalar Is Nothing Then
                yaexiste = False
            Else
                yaexiste = True
            End If
            CCN()
            Return yaexiste
        Catch ex As Exception
            clsFuncionesLOG.LOG(ex.ToString) : CCN()
        End Try
    End Function
    Public Shared Function esTeclaValida(ByVal tecla As String) As Boolean
        Try
            If (tecla = Keys.PageUp.ToString Or _
                tecla = Keys.PageDown.ToString Or _
                tecla = Keys.Home.ToString Or _
                tecla = Keys.End.ToString Or _
                tecla = Keys.F3.ToString Or _
                tecla = Keys.F4.ToString Or _
                tecla = Keys.F5.ToString Or _
                tecla = Keys.F2.ToString Or _
                tecla = Keys.F6.ToString) Then
                Return True
            End If
            Return False
        Catch ex As Exception
            clsFuncionesLOG.LOG(ex.ToString) : CCN()
        End Try
    End Function
End Class
