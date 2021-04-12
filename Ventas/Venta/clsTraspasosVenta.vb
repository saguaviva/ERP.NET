Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class clsTraspasosVenta
    Inherits clsADO

#Region "VARIABLES"

    Public PAF As clsPAFVenta
    Private Nuevodocumento As String
    Private OCV As Char = "V"
    Public dtTalleres As New DataTable
    Private funcionesOtras As New clsOtrasFunciones
#End Region

#Region "CAMPOS"

    Private mFRA As Integer
    Public Property FRA() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mFRA = nzn(dvForm(PA).Row("FRA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mFRA, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            mFRA = nzn(Value, 0)
            dvForm(PA).Row("FRA") = nzn(Value, 0) : guardarDV()
        End Set
    End Property
    Private mTALLER As Integer
    Public Property TALLER() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLER = nzn(dvForm(PA).Row("TALLER"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLER, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtTalleres, CCTallers, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mTALLER = nzn(Value, 0)
                    dvForm(PA).Row("NOMTALLER") = general.OBN(Value, dtTalleres, CNTallers)
                    dvForm(PA).Row("TALLER") = nzn(Value, 0) : guardarDV()
                End If
            Else
                dvForm(PA).Row("TALLER") = 0

                dvForm(PA).Row("NOMTALLER") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTETALLERS"))
            End If
        End Set
    End Property


    Private mNOMTALLER As String
    Public Property NOMTALLER() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMTALLER = general.nz(dvForm(PA).Row("NOMTALLER"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMTALLER, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMTALLER = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMTALLER") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMTALLER") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property
    Private mTIPUS As String
    Public Property TIPUS() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTIPUS = general.nz(dvForm(PA).Row("TIPUS"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTIPUS, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mTIPUS = general.nz(Value, "")
            dvForm(PA).Row("TIPUS") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mDOCUMENT As String
    Public Property DOCUMENT() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mDOCUMENT = general.nz(dvForm(PA).Row("DOCUMENT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDOCUMENT, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mDOCUMENT = general.nz(Value, "")
            dvForm(PA).Row("DOCUMENT") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mDATA As Date
    Public Property DATA() As Date
        Get
            If PA() = -1 Then Exit Property
            Try
                mDATA = dvForm(PA).Row("DATA")
            Catch ex As Exception : End Try
            Return mDATA
        End Get
        Set(ByVal Value As Date)
            If PA() = -1 Then Exit Property
            mDATA = Value
            dvForm(PA).Row("DATA") = Value : guardarDV()
        End Set
    End Property

    Private mQUANTITAT As Double
    Public Property QUANTITAT() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mQUANTITAT = nzn(dvForm(PA).Row("QUANTITAT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mQUANTITAT, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            mQUANTITAT = nzn(Value, 0)
            dvForm(PA).Row("QUANTITAT") = nzn(Value, 0) : guardarDV()
        End Set
    End Property

    Private mNLINEA As Integer
    Public Property NLINEA() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mNLINEA = nzn(dvForm(PA).Row("NLINEA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNLINEA, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            mNLINEA = nzn(Value, 0)
            dvForm(PA).Row("NLINEA") = nzn(Value, 0) : guardarDV()
        End Set
    End Property

    Private mALINEA As Integer
    Public Property ALINEA() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mALINEA = nzn(dvForm(PA).Row("ALINEA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mALINEA, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            mALINEA = nzn(Value, 0)
            dvForm(PA).Row("ALINEA") = nzn(Value, 0) : guardarDV()
        End Set
    End Property

    Private mTRASPASADA As Boolean
    Public Property TRASPASADA() As Boolean
        Get
            If PA() = -1 Then Exit Property
            Try
                mTRASPASADA = dvForm(PA).Row("TRASPASADA")
            Catch ex As Exception : End Try
            Return mTRASPASADA
        End Get
        Set(ByVal Value As Boolean)
            If PA() = -1 Then Exit Property
            mTRASPASADA = Value
            dvForm(PA).Row("TRASPASADA") = Value : guardarDV()
        End Set
    End Property

    Private mVC As String
    Public Property VC() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mVC = general.nz(dvForm(PA).Row("VC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mVC, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mVC = general.nz(Value, "")
            dvForm(PA).Row("VC") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mCOMAN As Integer
    Public Property COMAN() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mCOMAN = nzn(dvForm(PA).Row("COMAN"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCOMAN, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            mCOMAN = nzn(Value, 0)
            dvForm(PA).Row("COMAN") = nzn(Value, 0) : guardarDV()
        End Set
    End Property

    Private mALBAR As Integer
    Public Property ALBAR() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mALBAR = nzn(dvForm(PA).Row("ALBAR"), 0)
            Catch ex As Exception : End Try
            Return nzn(mALBAR, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            mALBAR = nzn(Value, 0)
            dvForm(PA).Row("ALBAR") = nzn(Value, 0) : guardarDV()
        End Set
    End Property
    Private mTALLA01 As Double
    Public Property TALLA01() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA01 = nzn(dvForm(PA).Row("TALLA01"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA01, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA01, 0) Then
                mTALLA01 = nzn(Value, 0)
                dvForm(PA).Row("TALLA01") = nzn(mTALLA01, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA02 As Double
    Public Property TALLA02() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA02 = nzn(dvForm(PA).Row("TALLA02"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA02, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA02, 0) Then
                mTALLA02 = nzn(Value, 0)
                dvForm(PA).Row("TALLA02") = nzn(mTALLA02, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA04 As Double
    Public Property TALLA04() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA04 = nzn(dvForm(PA).Row("TALLA04"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA04, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA04, 0) Then
                mTALLA04 = nzn(Value, 0)
                dvForm(PA).Row("TALLA04") = nzn(mTALLA04, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA05 As Double
    Public Property TALLA05() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA05 = nzn(dvForm(PA).Row("TALLA05"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA05, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA05, 0) Then
                mTALLA05 = nzn(Value, 0)
                dvForm(PA).Row("TALLA05") = nzn(mTALLA05, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA06 As Double
    Public Property TALLA06() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA06 = nzn(dvForm(PA).Row("TALLA06"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA06, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA06, 0) Then
                mTALLA06 = nzn(Value, 0)
                dvForm(PA).Row("TALLA06") = nzn(mTALLA06, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA07 As Double
    Public Property TALLA07() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA07 = nzn(dvForm(PA).Row("TALLA07"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA07, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA07, 0) Then
                mTALLA07 = nzn(Value, 0)
                dvForm(PA).Row("TALLA07") = nzn(mTALLA07, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA08 As Double
    Public Property TALLA08() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA08 = nzn(dvForm(PA).Row("TALLA08"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA08, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA08, 0) Then
                mTALLA08 = nzn(Value, 0)
                dvForm(PA).Row("TALLA08") = nzn(mTALLA08, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA09 As Double
    Public Property TALLA09() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA09 = nzn(dvForm(PA).Row("TALLA09"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA09, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA09, 0) Then
                mTALLA09 = nzn(Value, 0)
                dvForm(PA).Row("TALLA09") = nzn(mTALLA09, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA10 As Double
    Public Property TALLA10() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA10 = nzn(dvForm(PA).Row("TALLA10"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA10, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA10, 0) Then
                mTALLA10 = nzn(Value, 0)
                dvForm(PA).Row("TALLA10") = nzn(mTALLA10, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA03 As Double
    Public Property TALLA03() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA03 = nzn(dvForm(PA).Row("TALLA03"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA03, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA03, 0) Then
                mTALLA03 = nzn(Value, 0)
                dvForm(PA).Row("TALLA03") = nzn(mTALLA03, 0) : guardarDV()
            End If
        End Set
    End Property

#End Region


    Public Sub New(ByVal tabla As DataTable, _
                   ByVal centro As String, ByRef bindingcontext As BindingContext, ByVal p As clsPAFVenta, ByVal OrdenCompraVenta As Char)

        MyBase.New(tabla, centro, bindingcontext, "ESDETALLE")
        Dim sqlSel As String
        Try
            PAF = p
            OCV = OrdenCompraVenta
            If OCV = OrdenFabComplementos Then
                CargaTabla(tablaTalleres, CCTallers, CNTallers, dtTalleres)
            End If
            Me.centro = PAF.centro
            If OCV = OrdenFabComplementos Then
                NuevoDocumento = OrdenFabComplementos
            Else
                NuevoDocumento = ObtenerDocNuevo(p.DOCUMENT)
            End If

            sqlSinWhere = "SELECT dcfactutrasp.*, " & _
                                " filiales.DESCRI AS NOMCENTRO " & _
                                " FROM dcfactutrasp " & _
                                " LEFT JOIN filiales ON (filiales.CODI = dcfactutrasp.CENTRO) "

            Select Case PAF.DOCUMENT
                Case Pedido
                    Select Case OCV
                        Case OrdenFabComplementos
                            sqlSinWhere = "SELECT dcfactutrasp.*, " &
                                " TALLERS.NOM AS NOMTALLER, " &
                                " filiales.DESCRI AS NOMCENTRO " &
                                " FROM dcfactutrasp " &
                                " LEFT JOIN filiales ON (filiales.CODI = dcfactutrasp.CENTRO) " &
                                " LEFT JOIN TALLERS ON (TALLERS.CODI = dcfactutrasp.TALLER) "

                            sqlSel = sqlSinWhere &
                                " WHERE dcfactutrasp.COMAN = """ & PAF.FRA & """ AND " &
                                " dcfactutrasp.DOCUMENT = """ & OrdenFabComplementos & """ AND " &
                                " dcfactutrasp.VC = ""O"" AND " &
                                " dcfactutrasp.NLINEA = """ & PAF.lineasVenta.NLINEA & """ AND " &
                                " dcfactutrasp.CENTRO = """ & PAF.centro & """ ORDER BY dcfactutrasp.FRA "
                        Case Else
                            sqlSel = sqlSinWhere &
                                " WHERE dcfactutrasp.COMAN = """ & PAF.FRA & """ AND " &
                                " dcfactutrasp.DOCUMENT = """ & Nuevodocumento & """ AND " &
                                " dcfactutrasp.VC = ""V"" AND " &
                                " dcfactutrasp.NLINEA = """ & PAF.lineasVenta.NLINEA & """ AND " &
                                " dcfactutrasp.CENTRO = """ & PAF.centro & """ ORDER BY dcfactutrasp.FRA desc "
                    End Select
                Case Albaran
                    sqlSel = sqlSinWhere & _
                                " WHERE dcfactutrasp.ALBAR = """ & PAF.FRA & """ AND " & _
                                " dcfactutrasp.DOCUMENT = """ & NuevoDocumento & """ AND " & _
                                " dcfactutrasp.VC = ""V"" AND " & _
                                " dcfactutrasp.NLINEA = """ & PAF.lineasVenta.NLINEA & """ AND " & _
                                " dcfactutrasp.CENTRO = """ & PAF.centro & """ ORDER BY dcfactutrasp.FRA "
            End Select

            cmdSel.CommandText = sqlSel
            If OCV = OrdenFabComplementos AndAlso tabla.Columns("NOMTALLER") Is Nothing Then
                tabla.Columns.Add("NOMTALLER")
            End If
            dvForm.Sort = "FRA"
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            PonerDefaults()

            'CreaTablas()
            'PonerDefaults()
            'DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
#Region "TRASPASO"

    Private Function HacerTraspasoLinea(ByVal aFecha As Date) As Boolean
        Dim aAF As Integer
        Dim quehacersiponenumerodestino As Short
        Dim nuevoDoc As clsPAFVenta
        Dim documentoNuevo As String
        Try
            If FRA = 0 Then
                'FRA = CrearPAF(DATA, PAF.FRA, PAF.DOCUMENT, PAF.TIPUS, PAF.dvForm, centro)

                documentoNuevo = PAF.ObtenerDocNuevo(False)
                FRA = GetNumeroUltimoPAF(documentoNuevo, centro) + 1
                nuevoDoc = PAF.ObtenerDocumentoNuevo(PAF, documentoNuevo, FRA)
                nuevoDoc.DATA = aFecha
                nuevoDoc.ActualizarOrigen(True, True)

                ActualizarNum(tablaVentas, documentoNuevo, centro, FRA, nuevoDoc.TIPUS)

                'Ponemos el docuemnto al que ha sido traspasado
                ALINEA = TraspasarNLineas2(FRA, QUANTITAT, PAF.lineasVenta)  ', QUANTITAT, PAF.lineasVenta.COLOR, PAF.lineasVenta.DOCUMENT, PAF.lineasVenta.TIPUS, PAF.lineasVenta.dvForm, FRA, NLINEA, centro)
            Else
                'Hay que controlora que el albbarán de destino sea correcto
                quehacersiponenumerodestino = DocDestinoCorrecto(FRA, Nuevodocumento, TIPUS, centro)
                Select Case quehacersiponenumerodestino
                    Case 0
                        'No hacer nada mensaje de error mostrado anteriormente pq el doc no es valido
                        Return False
                    Case 1
                        FRA = funcionesOtras.CrearPAF(DATA, PAF.FRA, PAF.DOCUMENT, PAF.TIPUS, PAF.dvForm, centro, FRA)
                        ALINEA = TraspasarNLineas2(FRA, QUANTITAT, PAF.lineasVenta)
                    Case 2
                        'Ya existe
                        ALINEA = TraspasarNLineas2(FRA, QUANTITAT, PAF.lineasVenta)
                End Select
            End If

            'Habria que hacer un recalcular de lo nuevo
            Dim pafp As New clsPAFVenta(ds.Tables(tablaVentas).Copy, PAF.centro, Me.bc, Nuevodocumento, PAF.TIPUS, False, FRA, False)
            pafp.lineasVenta.HacerCalculosLineasPAF("PREU", True)
            pafp.ActualizarOrigen()
            pafp = Nothing
            Return True

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Function TraspasarNLineas2(ByVal aAF As Integer, ByVal cantidad As Double, ByVal linea As clsLineasVenta) ' ByVal tejido As String, ByVal cantidad As Integer, ByVal color As String, ByVal documento as string, ByVal tipo as string, ByVal dvLineas As DataView, ByVal dePA As Integer, ByVal nlinea As Integer, ByVal CENTRO As Char)
        Dim i As Integer
        Dim cmdInsertar As New MySqlCommand
        Dim documentonuevo As String = ObtenerDocNuevo(linea.DOCUMENT)
        Dim cmdSel As MySqlCommand
        Dim nlineas As Integer
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
            cmdSel = New MySqlCommand("SELECT MAX(NLINEA) FROM " & tablaLineasVenta & " WHERE " & _
                    " (DOCUMENT = """ & documentonuevo & """ AND " & _
                    " FRA = """ & aAF & """ AND " & _
                    " CENTRO = """ & linea.centro & """)", cnn)
            ACN()
            nlineas = nzn(cmdSel.ExecuteScalar, 0) + 1
            CCN()
            cmdInsertar.Connection = cnn
            'Solo hay una PAF a traspasar en este caso pq son las lineas
            'Obtenemos la linea a traspasar

            cmdInsertar.Connection = cnn
            cmdInsertar.CommandText = "INSERT INTO " & tablaLineasVenta & "(TIPUS, DOCUMENT, FRA, NLINEA, MOSTRA, " &
                    " DESCRI, COLOR, TALLA, TALLAL, TALLAH, UNITATS, PREU, DTE, IMPORT, COMAN, ALBAR, FACTURA, " &
                    " ESTOC, KM, PERREBRE, REBUT, CENTRO) VALUES " &
                    " (""" & linea.TIPUS & """, " &
                    " """ & documentonuevo & """, " &
                    " " & aAF & ", " &
                    " " & nlineas & ", " &
                    " """ & general.NS(linea.MOSTRA) & """, " &
                    " """ & general.NS(linea.DESCRI) & """, " &
                    " """ & general.NS(linea.COLOR) & """, " &
                    " """ & general.NS(linea.TALLA) & """, " &
                    " " & linea.TALLAL.ToString().Replace(","c, "."c) & " ," &
                    " " & linea.TALLAH.ToString().Replace(","c, "."c) & " ," &
                    " """ & general.NS(cantidad, True) & """, " &
                    " """ & general.NS(linea.PREU, True) & """, " &
                    " """ & general.NS(linea.DTE, True) & """, " &
                    " """ & general.NS(roundnum((linea.PREU * cantidad) * (1 - linea.DTE / 100), 2), True) & """, " &
                    " " & linea.FRA & ", " &
                    " ""0"", " &
                    " ""0"", " &
                    " ""0"", " &
                    " """ & linea.KM & """, " &
                    " """ & general.NS(linea.PERREBRE, True) & """, " &
                    " """ & general.NS(linea.REBUT, True) & """, " &
                    " """ & linea.centro & """)"

            'Ahora hay que insertarlo en dcfactutrasp
            ACN()
            cmdInsertar.ExecuteNonQuery()
            CCN()
            Return nlineas
        Catch ex As Exception
            LOG(ex.ToString) : CCN()
        End Try
    End Function

#End Region

#Region "ORDENES FABRICACION MUESTRAS"

    Private Function CrearOrden() As Double
        Dim cmdIns As MySqlCommand
        Dim ultimaorden As Double
        Try
            ultimaorden = GetNumeroUltimoPAFCompra(OrdenFabComplementos, "M", centro) + 1
            ActualizarNum(tablaCompras, OrdenFabComplementos, centro, ultimaorden, Muestra)
            cmdIns = New MySqlCommand("INSERT INTO cactur " & _
                " (TIPUS, " & _
                " DOCUMENT, " & _
                " FRA, " & _
                " PROVE, " & _
                " NRO, " & _
                " DATA, " & _
                " BASE1, " & _
                " P_IVA1, " & _
                " IVA1, " & _
                " TOTAL, P_DTE, DTE1, " & _
                " BRUT1, " & _
                " TRASPAS, P_RE1, RE1, " & _
                " VENCIM, TRASPASF, OBSERV, COMANDAMOSTRA" & _
                " ) VALUES (" & _
                " ""M"", " & _
                " ""O"", " & _
                " """ & ultimaorden & """, " & _
                " """ & TALLER & """, ""0""," & _
                " """ & ConvertirAfechaMysql(DATA) & """, " & _
                " ""0"", " & _
                " ""0"",  " & _
                " ""0"",  " & _
                " ""0"", ""0"", ""0"",  " & _
                " ""0"",  " & _
                " false, ""0"", ""0"",  " & _
                " """", false, """", """ & PAF.FRA & """)", cnn)
            ACN()
            cmdIns.ExecuteNonQuery()
            Return ultimaorden

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function
    Private Function TraspasarOrdenLinea() As Double
        Dim cmdIns As MySqlCommand
        Dim cmdSel As MySqlCommand
        Dim nlineas As Double
        Try
            cmdSel = New MySqlCommand("SELECT COUNT(*) FROM " & tablaLineasCompras & " WHERE " & _
                 " (TIPUS = ""M"" AND " & _
                 " DOCUMENT = ""O"" AND " & _
                 " FRA = """ & FRA & """ AND " & _
                 " CENTRO = """ & PAF.centro & """)", cnn)
            ACN()
            nlineas = cmdSel.ExecuteScalar + 1
            CCN()

            cmdIns = New MySqlCommand("INSERT INTO DCACTU " & _
                   " (TIPUS, CENTRO,DOCUMENT,FRA,NLINEA,ARTICLE,COLOR,TALLA,DESCRI,UNITATS)  " & _
                   " VALUES ( ""M"",  """ & PAF.lineasVenta.centro & """, " & _
                   " ""O"", """ & FRA & """, """ & nlineas & """, " & _
                   " """ & PAF.lineasVenta.MOSTRA & """, " & _
                   " """ & PAF.lineasVenta.COLOR & """, " & _
                   " """ & PAF.lineasVenta.TALLA & """, " & _
                   " """ & PAF.lineasVenta.DESCRI & """, " & _
                   " """ & QUANTITAT & """)", cnn)
            ACN()
            cmdIns.ExecuteNonQuery()
            CCN()
            Return nlineas

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function

    Public Sub GenerarOrden()
        Dim aAF As Integer
        Dim quehacersiponenumerodestino As Short
        Try

            Try
                If FRA = 0 Then
                    FRA = CrearOrden()
                    ALINEA = TraspasarOrdenLinea()

                Else
                    'Hay que controlora que el albbarán de destino sea correcto
                    quehacersiponenumerodestino = DocDestinoCorrecto(FRA, Nuevodocumento, TIPUS, centro)
                    Select Case quehacersiponenumerodestino
                        Case 0
                            'No hacer nada mensaje de error mostrado anteriormente pq el doc no es valido
                        Case 1
                            FRA = CrearOrden()
                            ALINEA = TraspasarOrdenLinea()
                        Case 2
                            'Ya existe
                            ALINEA = TraspasarOrdenLinea()
                    End Select
                End If
                'Habria que hacer un recalcular de lo nuevo
                'Dim pafp As New clsPAFVenta(ds.Tables(tablaVentas).Copy, PAF.centro, Me.bc, PAF.DOCUMENT, PAF.TIPUS)
                'pafp.lineasVenta.HacerCalculosLineasPAF("PREU")
                'pafp.ActualizarOrigen()

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

            CCN()
            'ORDRE = ultimaOrden
            ActualizarOrigen()


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "CALCULOS"

    Public Sub CantidadTraspasada()
        Dim i As Integer
        Dim ret As Double = 0
        Try
            For i = 0 To dvForm.Count - 1
                If dvForm(i).Item("TRASPASADA") = True Then
                    ret = dvForm(i).Item("QUANTITAT") + ret
                End If
            Next

            PAF.lineasVenta.PERREBRE = PAF.lineasVenta.UNITATS - ret
            PAF.lineasVenta.REBUT = ret
            PAF.ActualizarOrigen(True)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub

#End Region

#Region "ORGANIZAR"

    Private Sub PonerDefaults()
        Try
            '+------------+---------------------+------+-----+------------+-------+
            '| Field      | Type                | Null | Key | Default    | Extra |
            '+------------+---------------------+------+-----+------------+-------+
            '| FRA        | int(11)             | NO   |     | 0          |       |
            '| TIPUS      | char(1)             | NO   | PRI |            |       |
            '| DOCUMENT   | char(1)             | NO   | PRI |            |       |
            '| DATA       | date                | NO   |     | 0000-00-00 |       |
            '| QUANTITAT  | double(15,3)        | YES  |     | NULL       |       |
            '| NLINEA     | int(11)             | NO   | PRI | 0          |       |
            '| ALINEA     | int(11)             | NO   | PRI | 0          |       |
            '| TRASPASADA | tinyint(1) unsigned | YES  |     | NULL       |       |
            '| VC         | char(1)             | NO   | PRI |            |       |
            '| COMAN      | int(11)             | NO   | PRI | 0          |       |
            '| ALBAR      | int(11)             | NO   | PRI | 0          |       |
            '| CENTRO     | char(1)             | NO   | PRI | C          |       |
            '+------------+---------------------+------+-----+------------+-------+

            'Ponemos 0 todavía no se sabe a donde va a ir
            dvForm.Table.Columns("FRA").DefaultValue = 0
            dvForm.Table.Columns("DOCUMENT").DefaultValue = NuevoDocumento
            dvForm.Table.Columns("TIPUS").DefaultValue = PAF.lineasVenta.TIPUS
            dvForm.Table.Columns("QUANTITAT").DefaultValue = 0
            dvForm.Table.Columns("NLINEA").DefaultValue = PAF.lineasVenta.NLINEA
            dvForm.Table.Columns("DATA").DefaultValue = Date.Today.ToShortDateString
            dvForm.Table.Columns("ALINEA").DefaultValue = 0
            dvForm.Table.Columns("TALLER").DefaultValue = 0
            dvForm.Table.Columns("TRASPASADA").DefaultValue = False
            dvForm.Table.Columns("VC").DefaultValue = OCV
            Select Case PAF.DOCUMENT
                Case Pedido
                    dvForm.Table.Columns("COMAN").DefaultValue = PAF.lineasVenta.FRA
                Case Albaran
                    dvForm.Table.Columns("ALBAR").DefaultValue = PAF.lineasVenta.FRA
            End Select
            dvForm.Table.Columns("CENTRO").DefaultValue = PAF.centro

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Function DocDestinoCorrecto(ByVal af As Integer, ByVal doc As String, ByVal tipo As String, ByVal CENTRO As String) As Short
        Dim cmd As New MySqlCommand
        Dim dar As MySqlDataReader
        Try
            cmd.Connection = cnn

            If OCV = OrdenFabComplementos Then
                cmd.CommandText = "SELECT count(TRASPAS) as cantidad, TRASPAS " & _
                    " FROM CACTUR WHERE DOCUMENT = """ & doc & """ AND  " & _
                    " CENTRO = """ & CENTRO & """ AND  " & _
                    " FRA = """ & af & """ GROUP BY FRA"
            Else
                cmd.CommandText = "SELECT count(TRASPAS) as cantidad, TRASPAS " & _
                    " FROM FACTUR WHERE DOCUMENT = """ & doc & """ AND  " & _
                    " CENTRO = """ & CENTRO & """ AND  " & _
                    " FRA = """ & af & """ GROUP BY FRA"
            End If
            ACN()
            'cnn.Open()
            dar = cmd.ExecuteReader
            dar.Read()
            If dar("cantidad") = 0 Then
                'Hay que crearlo
                dar.Close()
                cnn.Close()
                Return 1
            End If
            If Not dar("TRASPAS") = 1 Then
                dar.Close()
                cnn.Close()
                Return 2
            Else
                MessageBox.Show("El document  " & af & " ja ha sigut trasapassat")
                dar.Close()
                cnn.Close()
                Return 0
            End If

            dar.Close()
            cnn.Close()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
            dar.Close()
            cnn.Close()
            Return 1
        End Try
    End Function
    Friend Sub HacerCalculos(ByVal aFecha As Date)
        Dim b As Boolean
        Try
            Select Case OCV
                Case OrdenFabComplementos
                    'Hay que generar una orden para el taller seleccionado o añadir a la orden
                    'Comprobar que sea el mismo taller si existe la orden introducida
                    GenerarOrden()

                Case Else
                    If TRASPASADA = True Then
                        Select Case PAF.lineasVenta.TIPUS
                            Case Tejido
                                b = ActualizarStockTejidos(-QUANTITAT)
                            Case Muestra
                                'En este caso no hay controles de stock porque estamos en muestras
                                b = True
                        End Select
                        'En este caso se quiere hacer el traspaso
                        If b Then
                            If HacerTraspasoLinea(aFecha) = False Then
                                'No se ha podido hacer el traspaso
                                TRASPASADA = False
                                Exit Sub
                            End If
                        End If
                    Else
                        'En este caso se quiere quitar el traspaso
                        Select Case PAF.lineasVenta.TIPUS
                            Case Tejido
                                ActualizarStockTejidos(QUANTITAT)
                            Case Muestra

                        End Select
                    End If
                    CantidadTraspasada()

            End Select

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Try
            guardarDV()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Function ActualizarStockTejidos(ByVal cantidad As Double) As Boolean
        Dim strSQL As String
        Dim stock As Double
        Dim cmdSel As New MySqlCommand
        Dim cmdUpd As New MySqlCommand
        Try
            'HACK FEISIMO
            ' Return True
            cmdSel.Connection = cnn
            cmdUpd.Connection = cnn

            ACN()
            If Not cantidad < 0 Then
                strSQL = "UPDATE " & tablaHiloColores & " SET ACTUAL = ACTUAL + " & general.NS(cantidad, True) & " WHERE(CENTRO = """ & PAF.centro & """ AND FIL = """ & general.NS(PAF.lineasVenta.MOSTRA) & """ AND TIPUS = ""T"" AND COLOR = """ & general.NS(PAF.lineasVenta.COLOR) & """)"
                cmdUpd.CommandText = strSQL
                cmdUpd.ExecuteNonQuery()
                CCN()
                Return True
            Else
                strSQL = "SELECT ACTUAL FROM " & tablaHiloColores & " WHERE(CENTRO = """ & PAF.centro & """ AND FIL = """ & general.NS(PAF.lineasVenta.MOSTRA) & """ AND TIPUS = ""T"" AND COLOR = """ & general.NS(PAF.lineasVenta.COLOR) & """)"
                cmdSel.CommandText = strSQL
                stock = cmdSel.ExecuteScalar() - Math.Abs(cantidad)
                If stock < 0 Then
                    MessageBox.Show("No hi suficient stock d'aquest teixit es necesiten: " & Math.Abs(stock))
                    If MessageBox.Show(rm.GetString("CONTINUARIGUALMENTE"), "", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        strSQL = "UPDATE " & tablaHiloColores & " SET ACTUAL = ACTUAL + " & general.NS(cantidad, True) & " WHERE(CENTRO = """ & PAF.centro & """ AND FIL = """ & general.NS(PAF.lineasVenta.MOSTRA) & """ AND TIPUS = ""T"" AND COLOR = """ & general.NS(PAF.lineasVenta.COLOR) & """)"
                    Else
                        CCN()
                        TRASPASADA = False
                        Return False
                    End If
                Else
                    strSQL = "UPDATE " & tablaHiloColores & " SET ACTUAL = ACTUAL + " & general.NS(cantidad, True) & " WHERE(CENTRO = """ & PAF.centro & """ AND FIL = """ & general.NS(PAF.lineasVenta.MOSTRA) & """ AND TIPUS = ""T"" AND COLOR = """ & general.NS(PAF.lineasVenta.COLOR) & """)"
                End If
                cmdUpd.CommandText = strSQL
                cmdUpd.ExecuteNonQuery()
                CCN()
                Return True
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function

#End Region

#Region "OVERRIDES"

    Friend Overrides Function GenOrder() As String

    End Function
    Friend Overrides Function genWhere() As String

    End Function
    Friend Overrides Function genWhereNumeroRegistros() As String

    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer

    End Function
    Friend Overrides Function TieneCambios() As Boolean
        Try
            guardarDV()
            If Not tabla.GetChanges Is Nothing Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function

#End Region

End Class