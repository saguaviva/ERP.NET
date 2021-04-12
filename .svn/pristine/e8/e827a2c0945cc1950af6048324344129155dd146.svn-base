'!!!!!!!! HAY QUE HACERLO CON EL CAMBIO DETALLE

Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Public Class clsDetalleDisposicion
    Inherits clsADO

#Region "CAMPOS"

    Private mDESCRIPCIO As String
    Public Property DESCRIPCIO() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mDESCRIPCIO = general.nz(dvForm(pa).Row("DESCRIPCIO"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDESCRIPCIO, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DESCRIPCIO, "") Then
                mDESCRIPCIO = general.nz(Value, "")
                dvForm(pa).Row("DESCRIPCIO") = general.nz(mDESCRIPCIO, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mLINEA As Integer
    Public Property LINEA() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mLINEA = nzn(dvForm(pa).Row("LINEA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mLINEA, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(LINEA, 0) Then
                mLINEA = nzn(Value, 0)
                dvForm(pa).Row("LINEA") = nzn(mLINEA, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mDISPOS As Integer
    Public Property DISPOS() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mDISPOS = nzn(dvForm(pa).Row("DISPOS"), 0)
            Catch ex As Exception : End Try
            Return nzn(mDISPOS, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(DISPOS, 0) Then
                mDISPOS = nzn(Value, 0)
                dvForm(pa).Row("DISPOS") = nzn(mDISPOS, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mTEJEDOR As Integer
    Public Property TEJEDOR() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mTEJEDOR = nzn(dvForm(pa).Row("TEJEDOR"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTEJEDOR, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If esCodigoExistente(dispo.dtTejedores, CCTallers, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mTEJEDOR = nzn(Value, 0)
                    dvForm(PA).Row("NOMTEJEDOR") = general.OBN(Value, dispo.dtTejedores, CNTallers)
                    dvForm(PA).Row("TEJEDOR") = nzn(Value, 0) : guardarDV()
                End If
            Else
                dvForm(PA).Row("TEJEDOR") = DBNull.Value

                dvForm(PA).Row("NOMTEJEDOR") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTETALLERS"))
            End If
        End Set
    End Property

    Private mNOMTEJEDOR As String
    Public Property NOMTEJEDOR() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mNOMTEJEDOR = general.nz(dvForm(pa).Row("NOMTEJEDOR"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMTEJEDOR, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mNOMTEJEDOR = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(pa).Row("NOMTEJEDOR") = general.nz(Value, "") : GuardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(pa).Row("NOMTEJEDOR") = general.nz(Value, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mNALBARAN As String
    Public Property NALBARAN() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mNALBARAN = general.nz(dvForm(pa).Row("NALBARAN"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNALBARAN, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(NALBARAN, "") Then
                mNALBARAN = general.nz(Value, "")
                dvForm(pa).Row("NALBARAN") = general.nz(mNALBARAN, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mTEJIDO As String
    Public Property TEJIDO() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mTEJIDO = general.nz(dvForm(pa).Row("TEJIDO"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTEJIDO, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TEJIDO, "") Then
                mTEJIDO = general.nz(Value, "")
                dvForm(pa).Row("TEJIDO") = general.nz(mTEJIDO, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mCOMPOS As String
    Public Property COMPOS() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCOMPOS = general.nz(dvForm(pa).Row("COMPOS"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCOMPOS, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(COMPOS, "") Then
                mCOMPOS = general.nz(Value, "")
                dvForm(pa).Row("COMPOS") = general.nz(mCOMPOS, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mNPIEZAS As String
    Public Property NPIEZAS() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mNPIEZAS = general.nz(dvForm(pa).Row("NPIEZAS"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNPIEZAS, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(NPIEZAS, "") Then
                mNPIEZAS = general.nz(Value, "")
                dvForm(pa).Row("NPIEZAS") = general.nz(mNPIEZAS, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mTOTALPIEZAS As Double
    Public Property TOTALPIEZAS() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mTOTALPIEZAS = nzn(dvForm(pa).Row("TOTALPIEZAS"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTOTALPIEZAS, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TOTALPIEZAS, 0) Then
                mTOTALPIEZAS = nzn(Value, 0)
                dvForm(pa).Row("TOTALPIEZAS") = nzn(mTOTALPIEZAS, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mTOTALKG As Double
    Public Property TOTALKG() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mTOTALKG = nzn(dvForm(pa).Row("TOTALKG"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTOTALKG, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TOTALKG, 0) Then
                mTOTALKG = nzn(Value, 0)
                dvForm(pa).Row("TOTALKG") = nzn(mTOTALKG, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mACABADO As String
    Public Property ACABADO() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mACABADO = general.nz(dvForm(pa).Row("ACABADO"), "")
            Catch ex As Exception : End Try
            Return general.nz(mACABADO, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(ACABADO, "") Then
                mACABADO = general.nz(Value, "")
                dvForm(pa).Row("ACABADO") = general.nz(mACABADO, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mANCHO As String
    Public Property ANCHO() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mANCHO = general.nz(dvForm(pa).Row("ANCHO"), "")
            Catch ex As Exception : End Try
            Return general.nz(mANCHO, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(ANCHO, "") Then
                mANCHO = general.nz(Value, "")
                dvForm(pa).Row("ANCHO") = general.nz(mANCHO, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mGRAMAJE As Double
    Public Property GRAMAJE() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mGRAMAJE = nzn(dvForm(pa).Row("GRAMAJE"), 0)
            Catch ex As Exception : End Try
            Return nzn(mGRAMAJE, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(GRAMAJE, 0) Then
                mGRAMAJE = nzn(Value, 0)
                dvForm(pa).Row("GRAMAJE") = nzn(mGRAMAJE, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mRENDIMIENTO As Double
    Public Property RENDIMIENTO() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mRENDIMIENTO = nzn(dvForm(pa).Row("RENDIMIENTO"), 0)
            Catch ex As Exception : End Try
            Return nzn(mRENDIMIENTO, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(RENDIMIENTO, 0) Then
                mRENDIMIENTO = nzn(Value, 0)
                dvForm(pa).Row("RENDIMIENTO") = nzn(mRENDIMIENTO, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mSERVIDO As Boolean
    Public Property SERVIDO() As Boolean
        Get
            If PA = -1 Then Exit Property
            Try
                mSERVIDO = dvForm(pa).Row("SERVIDO")
            Catch ex As Exception : End Try
            Return mSERVIDO
        End Get
        Set(ByVal Value As Boolean)
            If PA = -1 Then Exit Property
            If Value <> SERVIDO Then
                mSERVIDO = Value
                dvForm(pa).Row("SERVIDO") = mSERVIDO : GuardarDV()
            End If
        End Set
    End Property

    Private mDISPUESTO As Boolean
    Public Property DISPUESTO() As Boolean
        Get
            If PA = -1 Then Exit Property
            Try
                mDISPUESTO = dvForm(pa).Row("DISPUESTO")
            Catch ex As Exception : End Try
            Return mDISPUESTO
        End Get
        Set(ByVal Value As Boolean)
            If PA = -1 Then Exit Property
            If Value <> DISPUESTO Then
                mDISPUESTO = Value
                dvForm(pa).Row("DISPUESTO") = mDISPUESTO : GuardarDV()
            End If
        End Set
    End Property

#End Region

#Region "VARIABLES"

    Private bm As BindingManagerBase = bc.Item(dvForm)
    Friend dispo As clsDisposicion
    Private Const quitar As Short = 1
    Private Const poner As Short = 2
    Friend dtTallers As New DataTable("TALLERS")

#End Region


    Public Sub New(ByVal tabla As DataTable, _
                    ByVal centro As String, ByRef bindingcontext As BindingContext)

        MyBase.New(tabla, centro, bindingcontext, "FIL")
        Dim sqlSel As String
        Try


            sqlSinWhere = " SELECT ddispos.*, " & _
                            " filiales.DESCRI AS NOMCENTRO, " & _
                            " tallers.NOM AS NOMTEJEDOR " & _
                            " FROM ddispos " & _
                            " LEFT JOIN filiales ON (filiales.CODI = ddispos.CENTRO) " & _
                            " LEFT JOIN tallers ON (tallers.CODI = ddispos.TEJEDOR)  "

            sqlSel = sqlSinWhere & _
                        " WHERE ddispos.CENTRO = """ & centro & """ ORDER BY ddispos.DISPOS"
            '" LIMIT 1"

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            dvForm = tabla.DefaultView
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Sub

#Region "ORGANIZAR"

    Private Function SumarQuitarColor(ByVal MASMENOS As String)
        Dim METROS As Double

        Dim REND As Double
        Dim MASMENOSDISPUESTO As String
        Try
            '.STCRUK = roundnum(.STCRUM / .RENDIMENT, 2)
            '.STCRUM = roundnum(.STCRUK * .RENDIMENT, 2)
            'Hay que sumar al color del tejido que tiene esta disposicion los kg de la linea
            If RENDIMIENTO = 0 Then
                REND = 1
            Else
                REND = RENDIMIENTO
            End If
            METROS = roundnum(TOTALKG * REND, 2)

            Dim cmd As New MySqlCommand("UPDATE FILCOL SET " & _
                                " ACTUAL = ACTUAL " & MASMENOS & " " & METROS.ToString.Replace(",", ".") & _
                                " WHERE (FIL= """ & TEJIDO & """ AND  " & _
                                " COLOR = """ & dispo.COLOR & """ AND CENTRO = """ & dispo.centro & """ AND " & _
                                " TIPUS = ""T"")", cnn)

            If MessageBox.Show("Vols afegir aquest stock servit al color del teixit?", "Pregunta", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                cmd.ExecuteNonQuery()
            End If

            'SI es menos quiere decir que estamos quitandole del color y añadiendolo al stock dispuesto

            If MASMENOS = "+" Then
                MASMENOSDISPUESTO = "-"
            Else
                MASMENOSDISPUESTO = "+"

            End If
            cmd.CommandText = "UPDATE TEIXITS " & _
                            " SET STDISPK = STDISPK " & MASMENOSDISPUESTO & """" & TOTALKG.ToString.Replace(",", ".") & """, " & _
                            " STDISPM = STDISPM " & MASMENOSDISPUESTO & """" & METROS.ToString.Replace(",", ".") & """ " & _
                            "  WHERE CODI = """ & TEJIDO & """ AND CENTRO = """ & dispo.centro & """ "
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Function
    Private Function existeColorEnTejido(ByVal color As String, ByVal tejido As String) As Boolean
        Dim cmd As MySqlCommand
        Dim numeroColores As Integer
        Try
            cmd = New MySqlCommand("SELECT COUNT(COLOR) FROM FILCOL " & _
                    " WHERE PROVE = 0 AND " & _
                    " TIPUS = ""T"" AND " & _
                    " FIL = """ & tejido & """ AND " & _
                    " COLOR = """ & general.NS(color) & """ AND CENTRO = """ & dispo.centro & """ ", cnn)
            ACN()
            numeroColores = nzn(cmd.ExecuteScalar(), 0)
            CCN()
            If numeroColores > 0 Then
                Return True
            End If
            Return False

        Catch ex As Exception
            LOG(ex.ToString)
            Return False
        End Try
    End Function
    '********************
    'Hay que leer los precios antes
    Private Sub InsertarColor()
        Try
            If Not existeColorEnTejido(dispo.COLOR, Me.TEJIDO) Then
                Dim tejido As New clsTejido(ds.Tables(tablaTejidos), centro, bc)
                tejido.CambiarAReg(general.NS(Me.TEJIDO), iraregistro)
                tejido.cartaColores.AñadirColor(dispo.COLOR)
                tejido = Nothing
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
            tejido = Nothing
            CCN()
        End Try
    End Sub
    '********************************
    'Si el color no existe del tejido hay que insertarlo
    Public Function PonerQuitarServido(ByVal PONER As Boolean) As Boolean
        Try
            ACN()
            If PONER = True Then
                InsertarColor()
                ACN()
                If SumarQuitarColor("+") Then
                    Return True
                Else
                    Return False
                End If
            Else
                If SumarQuitarColor("-") Then
                    Return True
                Else
                    Return False
                End If
            End If
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Public Function PonerQuitarDispuesta(ByVal poner As Boolean) As Boolean
        Try
            ACN()
            If poner = True Then
                If MessageBox.Show(rm.GetString("DISPOSDISPONER") & " " & TOTALKG & rm.GetString("DISPOSDISPTEJIDO") & " " & TEJIDO, rm.GetString("INFO"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    If SumarQuitarDispuesto("-", "+") Then
                        Return True
                    Else
                        Return False
                    End If

                Else
                    Return False
                End If
            Else
                'Hay que restar de dispuesto
                SumarQuitarDispuesto("+", "-")
            End If
            CCN()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function suficienteCrudoODispuesto(ByVal CRUDOODISPUESTO As String) As Boolean
        Dim cmd As New MySqlCommand
        Dim cantidad As Double
        Try
            cmd.Connection = cnn

            cmd.CommandText = "SELECT " & CRUDOODISPUESTO & " FROM TEIXITS WHERE CENTRO = """ & general.nz(centro, empresaPorDefecto) & """ AND CODI = """ & TEJIDO & """"
            cantidad = nzn(cmd.ExecuteScalar, 0)

            If cantidad >= TOTALKG Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function SumarQuitarDispuesto(ByVal MASMENOSCRUDO As String, ByVal MASMENOSDISPUESTO As String) As Boolean
        Dim cmd As MySqlCommand
        Dim KILOS As Double
        Dim METROS As Double
        Dim rend As Double
        Try
            If MASMENOSCRUDO = "-" Then
                If Not suficienteCrudoODispuesto("STCRUK") Then
                    MessageBox.Show(rm.GetString("NOHAYSUFSTKCRU")) 'NO HI HA SUFICIENT STOCK CRU D'AQUEST TEIXIT
                    Return False
                End If
            Else
                If Not suficienteCrudoODispuesto("STDISPK") Then
                    MessageBox.Show(rm.GetString("NOHAYSUFSTKDISP")) '"NO HI HA SUFICIENT STOCK DISPOSAT D'AQUEST TEIXIT")
                    Return False
                End If
            End If


            KILOS = TOTALKG
            If RENDIMIENTO = 0 Then
                rend = 1
            Else
                rend = RENDIMIENTO
            End If
            '.STCRUK = roundnum(.STCRUM / .RENDIMENT, 2)
            '.STCRUM = roundnum(.STCRUK * .RENDIMENT, 2)
            METROS = roundnum(TOTALKG * rend, 2)
            cmd = New MySqlCommand("UPDATE TEIXITS " & _
                            " SET STDISPK = STDISPK " & MASMENOSDISPUESTO & """" & KILOS.ToString.Replace(",", ".") & """, " & _
                            " STCRUK = STCRUK " & MASMENOSCRUDO & """" & KILOS.ToString.Replace(",", ".") & """, " & _
                            " STDISPM = STDISPM " & MASMENOSDISPUESTO & """" & METROS.ToString.Replace(",", ".") & """, " & _
                            " STCRUM = STCRUM " & MASMENOSCRUDO & """" & METROS.ToString.Replace(",", ".") & """ WHERE CODI = """ & TEJIDO & """  AND CENTRO = """ & dispo.centro & """ ", cnn)

            cmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Public Function TodasRecibidas() As Boolean
        Try
            Dim i As Integer
            For i = 0 To dvForm.Count - 1
                If dvForm(i).Item("SERVIDO") = False Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Public Sub PonerAcabadoLineaActual(ByVal aAcabados As ArrayList)
        Dim i As Integer
        Dim s As String = ""
        Try

            If Not TEJIDO = "" Then

                Dim cmdSelectAcabados As New MySqlCommand("SELECT ACABAT " & _
                                        " FROM acabatsteixits WHERE " & _
                                        " CENTRO = """ & general.nz(centro, empresaPorDefecto) & """ AND TEIXIT = """ & TEJIDO & """ AND " & _
                                        " PROVE = " & dispo.ACABADOR & "", cnn)

                Dim dt As New DataTable '(cmdSelectAcabados, cnn)
                ACN()
                Dim da As New MySqlDataAdapter(cmdSelectAcabados)
                da.Fill(dt)
                'dt.Fill()
                ACABADO = ""

                If aAcabados.Count = 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        s = s & "-" & dt.Rows(i).Item("ACABAT")
                    Next
                    If Not s.Length = 0 Then
                        ACABADO = s.Substring(0, s.Length - 1)
                    End If
                Else

                    For i = 0 To aAcabados.Count - 1
                        s = s & aAcabados(i) & " - "
                    Next
                    ACABADO = s.Substring(0, s.Length - 3)
                End If

                COMPOS = getcomposicion()
                CCN()

            End If


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Function getcomposicion() As String
        Dim str As String = ""
        Dim i As Integer
        Try

            Dim cmd As New MySqlCommand("SELECT mattei.PER, mattei.COMP, " & _
                                            " filiales.DESCRI AS NOMCENTRO " & _
                                            " FROM mattei  " & _
                                            " LEFT JOIN filiales ON (filiales.CODI = mattei.CENTRO) " & _
                                            " WHERE MATTEI.CENTRO = """ & centro & """ AND " & _
                                            " TEIXIT = """ & TEJIDO & """ " & _
                                            " ORDER BY mattei.teixit", cnn)
            Dim dt As New DataTable ' MySqlDataTable(cmd, cnn)
            Dim da As New MySqlDataAdapter(cmd.CommandText, cnn)

            ACN()
            da.Fill(dt)

            CCN()

            For i = 0 To dt.Rows.Count - 1
                If i = dt.Rows.Count - 1 Then
                    str = str & dt.Rows(i).Item("PER") & "% :" & dt.Rows(i).Item("COMP")
                Else
                    str = str & dt.Rows(i).Item("PER") & "% :" & dt.Rows(i).Item("COMP") & " - "
                End If
            Next

            Return str

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Function

#End Region

#Region "HANDLERS"

    Private Sub DespertarHandlers()
        AddHandler tabla.ColumnChanged, AddressOf LineasDisposicion_ColumnChanged
        AddHandler tabla.RowChanged, AddressOf rowchanged
    End Sub
    Private Sub DormirHandlers()
        RemoveHandler tabla.ColumnChanged, AddressOf LineasDisposicion_ColumnChanged
        RemoveHandler tabla.RowChanged, AddressOf rowchanged
    End Sub
    Private Sub rowchanged(ByVal sender As Object, ByVal e As System.Data.DataRowChangeEventArgs)
        Try
            DormirHandlers()
            If LINEA = 10000 Then
                Try
                    If dvForm(dvForm.Count - 2).Item("LINEA") = 10000 Then
                        LINEA = 1
                    Else
                        LINEA = dvForm(dvForm.Count - 2).Item("LINEA") + 1
                    End If

                Catch ex As Exception
                    LINEA = 1
                End Try
            End If
            DespertarHandlers()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Sub
    Private Sub LineasDisposicion_ColumnChanged(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs)

        Try
            Dim sumaPiezas As Double = sumaTotal("TOTALPIEZAS", dvForm)
            Dim sumaKGPiezas As Double = sumaTotal("TOTALKG", dvForm)
            dispo.TOTALKG = sumaKGPiezas
            dispo.TOTALPIEZAS = sumaPiezas

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Sub

#End Region

#Region "OVERRIDES"

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
    Friend Overrides Function genWhere() As String
        Try
            Dim ret As String

            ret = "WHERE " & tabla.TableName & ".CENTRO = """ & centro & """"

            Return ret
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Function
    Friend Overrides Function GenOrder() As String
        Try
            Return ""
            'Return " ORDER BY TEMPORADA, CLIENT, SERIE, CODI "
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        If id Is Nothing Then
            id = DISPOS
        End If

        Dim cmd As New MySqlCommand(" SELECT " & _
          " (SELECT COUNT(*) " & _
          " FROM " & tabla.TableName & " AS M2 WHERE " & _
          " M2.CODI < M1.CODI AND  " & WCNoTabla() & " ) AS rownum FROM " & tabla.TableName & " AS M1  WHERE CODI = """ & id & """ AND " & WCNoTabla() & GenOrder(), cnn)
        Try
            Dim idx As Object = cmd.ExecuteScalar()

            If idx Is Nothing Then Return -1
            Return idx '- 1

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Function
    Friend Overrides Function genWhereNumeroRegistros() As String
        Try
            Return genWhere()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Function
    Public Overrides Sub borrar()
        BorrarActualDVDetalle()
        'ActualizarOrigen()
    End Sub

#End Region

End Class
