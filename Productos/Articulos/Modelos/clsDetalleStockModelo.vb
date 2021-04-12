Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Public Class clsDetalleStockModelo
    Inherits clsADO

#Region "CAMPOS"
    Private mMODEL As String
    Public Property MODEL() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mMODEL = general.nz(dvForm(pa).Row("MODEL"), "")
            Catch ex As Exception : End Try
            Return general.nz(mMODEL, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(MODEL, "") Then
                mMODEL = general.nz(Value, "")
                dvForm(pa).Row("MODEL") = general.nz(mMODEL, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mCLIENT As Integer
    Public Property CLIENT() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mCLIENT = nzn(dvForm(pa).Row("CLIENT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCLIENT, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If esCodigoExistente(dtCLIENTS, CCCLIENTS, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mCLIENT = nzn(Value, 0)
                    dvForm(pa).Row("NOMCLIENT") = general.OBN(Value, dtCLIENTS, CNCLIENTS)
                    dvForm(pa).Row("CLIENT") = nzn(Value, 0) : GuardarDV()
                End If
            Else
                dvForm(pa).Row("CLIENT") = 0

                dvForm(pa).Row("NOMCLIENT") = "" : GuardarDV()
                MessageBox.Show(rm.GetString("NOEXISTECLIENTS"))
            End If
        End Set
    End Property

    Private mNOMCLIENT As String
    Public Property NOMCLIENT() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mNOMCLIENT = general.nz(dvForm(pa).Row("NOMCLIENT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMCLIENT, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mNOMCLIENT = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(pa).Row("NOMCLIENT") = general.nz(Value, "") : GuardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(pa).Row("NOMCLIENT") = general.nz(Value, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mSERIE As String
    Public Property SERIE() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mSERIE = general.nz(dvForm(pa).Row("SERIE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mSERIE, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(SERIE, "") Then
                mSERIE = general.nz(Value, "")
                dvForm(pa).Row("SERIE") = general.nz(mSERIE, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mTEMPORADA As String
    Public Property TEMPORADA() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mTEMPORADA = general.nz(dvForm(pa).Row("TEMPORADA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTEMPORADA, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TEMPORADA, "") Then
                mTEMPORADA = general.nz(Value, "")
                dvForm(pa).Row("TEMPORADA") = general.nz(mTEMPORADA, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mCOLOR As String
    Public Property COLOR() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCOLOR = general.nz(dvForm(pa).Row("COLOR"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCOLOR, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(COLOR, "") Then
                mCOLOR = general.nz(Value, "")
                dvForm(pa).Row("COLOR") = general.nz(mCOLOR, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mTALLA As String
    Public Property TALLA() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mTALLA = general.nz(dvForm(pa).Row("TALLA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLA, "") Then
                mTALLA = general.nz(Value, "")
                dvForm(pa).Row("TALLA") = general.nz(mTALLA, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mTALLA01 As Double
    Public Property TALLA01() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mTALLA01 = nzn(dvForm(pa).Row("TALLA01"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA01, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA01, 0) Then
                mTALLA01 = nzn(Value, 0)
                dvForm(pa).Row("TALLA01") = nzn(mTALLA01, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mTALLA02 As Double
    Public Property TALLA02() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mTALLA02 = nzn(dvForm(pa).Row("TALLA02"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA02, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA02, 0) Then
                mTALLA02 = nzn(Value, 0)
                dvForm(pa).Row("TALLA02") = nzn(mTALLA02, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mTALLA03 As Double
    Public Property TALLA03() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mTALLA03 = nzn(dvForm(pa).Row("TALLA03"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA03, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA03, 0) Then
                mTALLA03 = nzn(Value, 0)
                dvForm(pa).Row("TALLA03") = nzn(mTALLA03, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mTALLA04 As Double
    Public Property TALLA04() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mTALLA04 = nzn(dvForm(pa).Row("TALLA04"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA04, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA04, 0) Then
                mTALLA04 = nzn(Value, 0)
                dvForm(pa).Row("TALLA04") = nzn(mTALLA04, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mTALLA05 As Double
    Public Property TALLA05() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mTALLA05 = nzn(dvForm(pa).Row("TALLA05"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA05, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA05, 0) Then
                mTALLA05 = nzn(Value, 0)
                dvForm(pa).Row("TALLA05") = nzn(mTALLA05, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mTALLA06 As Double
    Public Property TALLA06() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mTALLA06 = nzn(dvForm(pa).Row("TALLA06"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA06, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA06, 0) Then
                mTALLA06 = nzn(Value, 0)
                dvForm(pa).Row("TALLA06") = nzn(mTALLA06, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mTALLA07 As Double
    Public Property TALLA07() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mTALLA07 = nzn(dvForm(pa).Row("TALLA07"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA07, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA07, 0) Then
                mTALLA07 = nzn(Value, 0)
                dvForm(pa).Row("TALLA07") = nzn(mTALLA07, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mTALLA08 As Double
    Public Property TALLA08() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mTALLA08 = nzn(dvForm(pa).Row("TALLA08"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA08, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA08, 0) Then
                mTALLA08 = nzn(Value, 0)
                dvForm(pa).Row("TALLA08") = nzn(mTALLA08, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mTALLA09 As Double
    Public Property TALLA09() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mTALLA09 = nzn(dvForm(pa).Row("TALLA09"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA09, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA09, 0) Then
                mTALLA09 = nzn(Value, 0)
                dvForm(pa).Row("TALLA09") = nzn(mTALLA09, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mTALLA10 As Double
    Public Property TALLA10() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mTALLA10 = nzn(dvForm(pa).Row("TALLA10"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLA10, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TALLA10, 0) Then
                mTALLA10 = nzn(Value, 0)
                dvForm(pa).Row("TALLA10") = nzn(mTALLA10, 0) : GuardarDV()
            End If
        End Set
    End Property
#End Region

#Region "VARIABLES"

    Friend dtClients As New DataTable("CLIENTS")
    Private modelo As clsModelo

#End Region

    Public Sub New(ByVal tabla As DataTable, _
            ByVal centro As String, ByRef bindingcontext As BindingContext, ByVal mode As clsModelo)

        MyBase.New(tabla, centro, bindingcontext)
        Dim sqlSel As String
        Try
            modelo = mode
            sqlSinWhere = "SELECT * FROM MODSTK "


            sqlSel = sqlSinWhere & " WHERE MODEL = """ & modelo.CODI & """ AND " & _
                            " TEMPORADA = """ & modelo.TEMPORADA & """ AND " & _
                            " SERIE = """ & modelo.SERIE & """ AND " & _
                            " CLIENT = """ & modelo.CLIENT & """ AND " & _
                            " CENTRO = """ & modelo.centro & """ "

            cmdSel.CommandText = sqlSel

            da.SelectCommand = cmdSel
            da.Fill(tabla)
            PonerDefaults()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Sub CambioDetalle(ByVal centro As String, ByVal mode As clsModelo)
        Try
            Dim sqlSel As String
            modelo = mode
            Me.centro = centro
            sqlSel = sqlSinWhere & _
                            " WHERE MODEL = """ & modelo.CODI & """ AND " & _
                            " TEMPORADA = """ & modelo.TEMPORADA & """ AND " & _
                            " SERIE = """ & modelo.SERIE & """ AND " & _
                            " CLIENT = """ & modelo.CLIENT & """ AND " & _
                            " CENTRO = """ & modelo.centro & """ "

            cmdSel.CommandText = sqlSel
            'dvForm.Sort = "NLINEA"
            da.SelectCommand = cmdSel
            tabla.Clear()
            da.Fill(tabla)
            PonerDefaults()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerDefaults()
        Try
            With dvForm
                .Table.Columns("MODEL").DefaultValue = modelo.CODI
                .Table.Columns("CLIENT").DefaultValue = modelo.CLIENT
                .Table.Columns("SERIE").DefaultValue = modelo.SERIE
                .Table.Columns("TEMPORADA").DefaultValue = modelo.TEMPORADA
                .Table.Columns("CENTRO").DefaultValue = modelo.centro
                .Table.Columns("COLOR").DefaultValue = ""
                .Table.Columns("TALLA").DefaultValue = ""
            End With

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

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
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function GenOrder() As String
        Try
            Return ""
            'Return " ORDER BY TEMPORADA, CLIENT, SERIE, CODI "
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        If id Is Nothing Then
            id = "MODEL"
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
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function genWhereNumeroRegistros() As String
        Try
            Return genWhere()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function

#End Region

    Public Sub ActualizarDetalle()
        Dim i As Integer
        Dim cambio As Boolean = False
        Try
            For i = 0 To dvForm.Count - 1
                If general.nz(dvForm(i).Item("MODEL"), "") <> general.nz(modelo.CODI, 0) Then dvForm(i).Item("MODEL") = general.nz(modelo.CODI, 0) : cambio = True
                If general.nz(dvForm(i).Item("SERIE"), "") <> general.nz(modelo.SERIE, "") Then dvForm(i).Item("SERIE") = general.nz(modelo.SERIE, "") : cambio = True
                If general.nz(dvForm(i).Item("TEMPORADA"), "") <> general.nz(modelo.TEMPORADA, "") Then dvForm(i).Item("TEMPORADA") = general.nz(modelo.TEMPORADA, "") : cambio = True
                If general.nz(dvForm(i).Item("CENTRO"), "") <> general.nz(modelo.centro, "") Then dvForm(i).Item("CENTRO") = general.nz(modelo.centro, "") : cambio = True
            Next
            If cambio Then guardarDV()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            ActualizarDetalle()
            MyBase.ActualizarOrigen(True, True)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Public Overrides Sub borrar()
        Try
            'Este borra todas las lineas
            BorrarActualDVDetalle()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

End Class
