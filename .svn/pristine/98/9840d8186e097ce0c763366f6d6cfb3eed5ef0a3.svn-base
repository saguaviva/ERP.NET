Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Public Class clsCuentasBancos
    Inherits clsADO

#Region "CAMPOS"

    '+--------+--------------+------+-----+---------+-------+
    '| Field  | Type         | Null | Key | Default | Extra |
    '+--------+--------------+------+-----+---------+-------+
    '| BANC   | int(11)      | NO   | PRI | 0       |       |
    '| CUENTA | varchar(255) | NO   | PRI |         |       |
    '| CENTRO | char(1)      | NO   | PRI | C       |       |
    '| TIPO   | varchar(1)   | NO   |     |         |       |
    '| DOM    | varchar(255) | YES  |     | NULL    |       |
    '| POB    | varchar(45)  | YES  |     | NULL    |       |
    '| CP     | varchar(45)  | YES  |     | NULL    |       |
    '| PROV   | varchar(45)  | YES  |     | NULL    |       |
    '+--------+--------------+------+-----+---------+-------+
    '8 rows

    'Private mBANC As Integer
    'Public Property BANC() As Integer
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mBANC = nzn(dvForm(pa).Row("BANC"), 0)
    '        Catch ex As Exception : End Try
    '        Return nzn(mBANC, 0)
    '    End Get
    '    Set(ByVal Value As Integer)
    '        If PA = -1 Then Exit Property
    '        If esCodigoExistente(dtBANCS, CCBANCS, Value) Then
    '            If nzn(Value, 0) <> 0 Then
    '                mBANC = nzn(Value, 0)
    '                dvForm(pa).Row("NOMBANC") = general.OBN(Value, dtBANCS, CNBANCS)
    '                dvForm(pa).Row("BANC") = nzn(Value, 0) : GuardarDV()
    '            Else
    '                dvForm(pa).Row("BANC") = DBNull.value

    '                dvForm(pa).Row("NOMBANC") = "" : GuardarDV()
    '            End If
    '        Else
    '            dvForm(pa).Row("BANC") = DBNull.value

    '            dvForm(pa).Row("NOMBANC") = "" : GuardarDV()
    '            MessageBox.Show(rm.GetString("NOEXISTEBANCS"))
    '        End If
    '    End Set
    'End Property

    Private mNOMBANC As String
    Public Property NOMBANC() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mNOMBANC = general.nz(dvForm(pa).Row("NOMBANC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMBANC, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mNOMBANC = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(pa).Row("NOMBANC") = general.nz(Value, "") : GuardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(pa).Row("NOMBANC") = general.nz(Value, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mCUENTA As String
    Public Property CUENTA() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCUENTA = general.nz(dvForm(pa).Row("CUENTA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCUENTA, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CUENTA, "") Then
                mCUENTA = general.nz(Value, "")
                dvForm(pa).Row("CUENTA") = general.nz(mCUENTA, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mTIPO As String
    Public Property TIPO() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mTIPO = general.nz(dvForm(pa).Row("TIPO"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTIPO, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TIPO, "") Then
                mTIPO = general.nz(Value, "")
                dvForm(pa).Row("TIPO") = general.nz(mTIPO, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mDOM As String
    Public Property DOM() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mDOM = general.nz(dvForm(pa).Row("DOM"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDOM, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DOM, "") Then
                mDOM = general.nz(Value, "")
                dvForm(pa).Row("DOM") = general.nz(mDOM, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mPOB As String
    Public Property POB() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mPOB = general.nz(dvForm(pa).Row("POB"), "")
            Catch ex As Exception : End Try
            Return general.nz(mPOB, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(POB, "") Then
                mPOB = general.nz(Value, "")
                dvForm(pa).Row("POB") = general.nz(mPOB, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mCP As String
    Public Property CP() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCP = general.nz(dvForm(pa).Row("CP"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCP, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CP, "") Then
                mCP = general.nz(Value, "")
                dvForm(pa).Row("CP") = general.nz(mCP, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mPROV As String
    Public Property PROV() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mPROV = general.nz(dvForm(pa).Row("PROV"), "")
            Catch ex As Exception : End Try
            Return general.nz(mPROV, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(PROV, "") Then
                mPROV = general.nz(Value, "")
                dvForm(pa).Row("PROV") = general.nz(mPROV, "") : GuardarDV()
            End If
        End Set
    End Property

#End Region

#Region "VARIABLES"

    Private banco As clsBanco
    Public dtTipo As New DataTable
    Public dtFiliales As New DataTable

#End Region

#Region "INICIARLIZAR"

    Public Sub New(ByVal tabla As DataTable, _
                    ByVal centro As String, ByRef bindingcontext As BindingContext, ByVal banc As clsBanco)
        MyBase.New(tabla, centro, bindingcontext, "ESDETALLE")
        Dim sqlSel As String
        Try
            banco = banc
            Me.centro = banco.centro
            sqlSinWhere = "SELECT bancscuentas.*, filiales.DESCRI AS NOMCENTRO FROM bancscuentas " & _
                                   " LEFT JOIN filiales ON (filiales.CODI = bancscuentas.CENTRO) "

            sqlSel = sqlSinWhere & " WHERE BANC = """ & banco.CODI & """"
            DormirHandlers()

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            CargaTablas()

            PonerDefaults()
            dvForm.Sort = "TIPO"
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CargaTablas()
        Try
            CrearTablaTIPO()
            'CargaTabla(tablaEmpresas, CCFiliales, CNFiliales, dtFiliales, True)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Friend Sub DespertarHandlers()

        Try : AddHandler tabla.RowChanged, AddressOf RowChanged : Catch : End Try

    End Sub
    Friend Sub DormirHandlers()

        Try : RemoveHandler tabla.RowChanged, AddressOf RowChanged : Catch : End Try

    End Sub
    Friend Sub CambioDetalle(ByVal centro As String, ByVal banc As clsBanco)
        Dim sqlSel As String
        Try
            banco = banc
            Me.centro = banco.centro
            sqlSel = sqlSinWhere & " WHERE BANC = """ & banco.CODI & """ "

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            dvForm.Sort = "TIPO"
            tabla.Clear()
            da.Fill(tabla)
            'dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub ActualizarDetalle()
        Dim i As Integer
        Dim cambio As Boolean = False
        Try
            For i = 0 To dvForm.Count - 1
                If nzn(dvForm(i).Item("BANC"), "") <> nzn(banco.CODI, 0) Then dvForm(i).Item("BANC") = nzn(banco.CODI, 0) : cambio = True
            Next
            If cambio Then guardarDV()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Friend Sub RowChanged(ByVal sender As Object, ByVal e As System.data.DataRowChangeEventArgs)
        Try
            DormirHandlers()

            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerDefaults()
        Try
            dvForm.Table.Columns("CUENTA").DefaultValue = "D"
            dvForm.Table.Columns("BANC").DefaultValue = banco.CODI
            dvForm.Table.Columns("CENTRO").DefaultValue = banco.centro

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub CrearTablaTIPO()
        Dim dc1 As New DataColumn("TIPO")
        Dim dc2 As New DataColumn("NOMTIPO")
        Dim dr As DataRow
        Try
            dtTipo.Columns.Add(dc1)
            dtTipo.Columns.Add(dc2)
            dr = dtTipo.NewRow
            dr("TIPO") = "C"
            dr("NOMTIPO") = rm.GetString("CREDITO")
            dtTipo.Rows.Add(dr)
            dr = dtTipo.NewRow
            dr("TIPO") = "D"
            dr("NOMTIPO") = rm.GetString("DEBITO")
            dtTipo.Rows.Add(dr)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
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
    Public Overrides Sub borrar()
        Try
            BorrarActualDV()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Function genWhere() As String
        Try
            Dim ret As String

            ret = "WHERE " & tabla.TableName & ".CENTRO = """ & banco.centro & """"

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
            'id = CODI
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
    'Friend Sub CambioDetalle(ByVal centro As String, ByVal PA As clsPAFVenta)
    '    Dim sqlSel As String
    '    Try
    '        PAF = PA
    '        Me.centro = PAF.centro
    '        sqlSel = sqlSinWhere & _
    '                    " WHERE DFACTU.FRA = """ & PAF.FRA & """ " & _
    '                    " AND DFACTU.DOCUMENT = """ & PAF.DOCUMENT & """ AND " & _
    '                    " DFACTU.CENTRO = """ & PAF.centro & """ ORDER BY DFACTU.FRA "

    '        cmdSel.CommandText = sqlSel
    '        da.SelectCommand = cmdSel
    '        dvForm.Sort = "NLINEA ASC"
    '        tabla.Clear()
    '        da.Fill(tabla)
    '        PonerDefaults()
    '        ADMuestras(PAF.CLIENT)
    '        'dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "

    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN()
    '    End Try
    'End Sub
    'Public Sub ActualizarDetalle()
    '    Dim i As Integer
    '    Dim cambio As Boolean = False
    '    Try
    '        For i = 0 To dvForm.Count - 1
    '            If nzn(dvForm(i).Item("FRA"), "") <> nzn(PAF.FRA, 0) Then dvForm(i).Item("FRA") = nzn(PAF.FRA, 0) : cambio = True
    '            If general.nz(dvForm(i).Item("DOCUMENT"), "") <>general.nz(PAF.DOCUMENT, "") Then dvForm(i).Item("DOCUMENT") =general.nz(PAF.DOCUMENT, "") : cambio = True
    '            If general.nz(dvForm(i).Item("CENTRO"), "") <>general.nz(PAF.centro, "") Then dvForm(i).Item("CENTRO") =general.nz(PAF.centro, "") : cambio = True
    '        Next
    '        If cambio Then guardarDV()

    '    Catch ex As Exception
    '        LOG(ex.ToString)
    '    End Try
    'End Sub
    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            ActualizarDetalle()
            MyBase.ActualizarOrigen(True, True)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#End Region

End Class