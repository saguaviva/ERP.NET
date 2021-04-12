Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Public Class clsDetalleEscandalloModelo
    Inherits clsADO

#Region "CAMPOS"

    Private mMODEL As String
    Public Property MODEL() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mMODEL = general.nz(dvForm(PA).Row("MODEL"), "")
            Catch ex As Exception : End Try
            Return general.nz(mMODEL, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(MODEL, "") Then
                mMODEL = general.nz(Value, "")
                dvForm(PA).Row("MODEL") = general.nz(mMODEL, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCLIENT As Integer
    Public Property CLIENT() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mCLIENT = nzn(dvForm(PA).Row("CLIENT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCLIENT, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtClients, CCClients, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mCLIENT = nzn(Value, 0)
                    dvForm(PA).Row("NOMCLIENT") = general.OBN(Value, dtClients, CNClients)
                    dvForm(PA).Row("CLIENT") = nzn(Value, 0) : guardarDV()
                End If
            Else
                dvForm(PA).Row("CLIENT") = 0

                dvForm(PA).Row("NOMCLIENT") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTECLIENTS"))
            End If
        End Set
    End Property

    Private mNOMCLIENT As String
    Public Property NOMCLIENT() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMCLIENT = general.nz(dvForm(PA).Row("NOMCLIENT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMCLIENT, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMCLIENT = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMCLIENT") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMCLIENT") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTEMPORADA As String
    Public Property TEMPORADA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTEMPORADA = general.nz(dvForm(PA).Row("TEMPORADA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTEMPORADA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TEMPORADA, "") Then
                mTEMPORADA = general.nz(Value, "")
                dvForm(PA).Row("TEMPORADA") = general.nz(mTEMPORADA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mSERIE As String
    Public Property SERIE() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mSERIE = general.nz(dvForm(PA).Row("SERIE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mSERIE, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(SERIE, "") Then
                mSERIE = general.nz(Value, "")
                dvForm(PA).Row("SERIE") = general.nz(mSERIE, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTITULO As String
    Public Property TITULO() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTITULO = general.nz(dvForm(PA).Row("TITULO"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTITULO, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TITULO, "") Then
                mTITULO = general.nz(Value, "")
                dvForm(PA).Row("TITULO") = general.nz(mTITULO, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTEIXIT As String
    Public Property TEIXIT() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTEIXIT = general.nz(dvForm(PA).Row("TEIXIT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTEIXIT, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TEIXIT, "") Then
                mTEIXIT = general.nz(Value, "")
                dvForm(PA).Row("TEIXIT") = general.nz(mTEIXIT, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCONSUM As Double
    Public Property CONSUM() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mCONSUM = nzn(dvForm(PA).Row("CONSUM"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCONSUM, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(CONSUM, 0) Then
                mCONSUM = nzn(Value, 0)
                dvForm(PA).Row("CONSUM") = nzn(mCONSUM, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mPREU As Double
    Public Property PREU() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mPREU = nzn(dvForm(PA).Row("PREU"), 0)
            Catch ex As Exception : End Try
            Return nzn(mPREU, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(PREU, 0) Then
                mPREU = nzn(Value, 0)
                dvForm(PA).Row("PREU") = nzn(mPREU, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mCOST As Double
    Public Property COST() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mCOST = nzn(dvForm(PA).Row("COST"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCOST, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(COST, 0) Then
                mCOST = nzn(Value, 0)
                dvForm(PA).Row("COST") = nzn(mCOST, 0) : guardarDV()
            End If
        End Set
    End Property

#End Region

#Region "VARIABLES"

    Friend dtClients As New DataTable("CLIENTS")
    Private modelo As clsModelo

#End Region

#Region "INICIALIZAR"

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext, ByVal mode As clsModelo)

        MyBase.New(tabla, centro, bindingcontext)
        Dim sqlSel As String
        Try
            modelo = mode
            sqlSinWhere = "SELECT * FROM MODELSESCANDALLO"

            sqlSel = sqlSinWhere & " WHERE MODEL = """ & modelo.CODI & """ AND " & _
                            " TEMPORADA = """ & modelo.TEMPORADA & """ AND " & _
                            " SERIE = """ & modelo.SERIE & """ AND " & _
                            " CLIENT = """ & modelo.CLIENT & """ AND " & _
                            " CENTRO = """ & modelo.centro & """ "

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            tabla.Columns.Add(New DataColumn("ELEGIRTEJIDO"))
            guardarDV()
            PonerDefaults()
            tabla.AcceptChanges()


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
                .Table.Columns("TEIXIT").DefaultValue = ""
                .Table.Columns("TITULO").DefaultValue = ""
                .Table.Columns("CONSUM").DefaultValue = 0
                .Table.Columns("PREU").DefaultValue = 0
                .Table.Columns("COST").DefaultValue = 0
                .Table.Columns("CENTRO").DefaultValue = modelo.centro
                .Table.Columns("ELEGIRTEJIDO").DefaultValue = ""
            End With

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

    Public Function obtenerTejidoTitulo(ByVal titulo As String) As String
        Dim idx As Integer
        Dim dvsort As String
        Dim ret As String
        Try
            dvsort = dvForm.Sort
            dvForm.Sort = "TITULO"
            idx = dvForm.Find(titulo)

            If Not idx = -1 Then
                ret = dvForm(idx).Item("TEIXIT")
                dvForm.Sort = dvsort
                Return ret
            Else
                dvForm.Sort = dvsort
                Return ""
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function

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
            id = MODEL
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
    Public Overrides Sub borrar()
        Try
            'Este borra todas las lineas
            BorrarActualDVDetalle()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

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

End Class
