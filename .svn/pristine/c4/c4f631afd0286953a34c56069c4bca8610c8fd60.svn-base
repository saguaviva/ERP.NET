Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Public Class clsCuentasRemesa
    Inherits clsADO

#Region "CAMPOS"

    '+----------+--------------+------+-----+---------+-------+
    '| Field    | Type         | Null | Key | Default | Extra |
    '+----------+--------------+------+-----+---------+-------+
    '| CODI     | int(11)      | NO   | PRI | 0       |       |
    '| CENTRO   | char(1)      | NO   | PRI | C       |       |
    '| CUENTA   | varchar(255) | NO   |     |         |       |
    '| TIPO     | varchar(1)   | NO   |     |         |       |
    '+----------+--------------+------+-----+---------+-------+
    '4 rows

    Private mREMESA As Integer
    Public Property REMESA() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mREMESA = nzn(dvForm(PA).Row("REMESA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mREMESA, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(REMESA, 0) Then
                mREMESA = nzn(Value, 0)
                dvForm(PA).Row("REMESA") = nzn(mREMESA, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mCUENTA As String
    Public Property CUENTA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mCUENTA = general.nz(dvForm(PA).Row("CUENTA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCUENTA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CUENTA, "") Then
                mCUENTA = general.nz(Value, "")
                dvForm(PA).Row("CUENTA") = general.nz(mCUENTA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTIPO As String
    Public Property TIPO() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTIPO = general.nz(dvForm(PA).Row("TIPO"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTIPO, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TIPO, "") Then
                mTIPO = general.nz(Value, "")
                dvForm(PA).Row("TIPO") = general.nz(mTIPO, "") : guardarDV()
            End If
        End Set
    End Property

#End Region

#Region "VARIABLES"

    Private remesaa As clsRemesa

#End Region

#Region "INICIARLIZAR"

    Public Sub New(ByVal tabla As DataTable, _
                    ByVal centro As String, ByRef bindingcontext As BindingContext, ByVal reme As clsRemesa)
        MyBase.New(tabla, centro, bindingcontext, "ESDETALLE")
        Dim sqlSel As String
        Try
            remesaa = reme
            Me.centro = remesaa.centro
            sqlSinWhere = "SELECT REMESASCUENTAS.* FROM REMESASCUENTAS "

            sqlSel = sqlSinWhere & " WHERE REMESA = " & remesaa.CODI & " AND REMESASCUENTAS.CENTRO = """ & remesaa.centro & """ "

            DormirHandlers()

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            'CreaTablas()
            'PonerDefaults()
            dvForm.Sort = "CUENTA"
            'DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Sub DespertarHandlers()

        'Try : AddHandler tabla.RowChanged, AddressOf RowChanged : Catch : End Try

    End Sub
    Friend Sub DormirHandlers()

        'Try : RemoveHandler tabla.RowChanged, AddressOf RowChanged : Catch : End Try

    End Sub
    Friend Sub CambioDetalle(ByVal centro As String, ByVal reme As clsRemesa)
        Dim sqlSel As String
        Try
            remesaa = reme
            Me.centro = remesaa.centro
            sqlSel = sqlSinWhere & " WHERE REMESA = " & remesaa.CODI & ""

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            dvForm.Sort = "CUENTA"
            tabla.Clear()
            PonerDefaults()
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
                If nzn(dvForm(i).Item("REMESA"), "") <> nzn(remesaa.CODI, 0) Then dvForm(i).Item("REMESA") = nzn(remesaa.CODI, 0) : cambio = True
            Next
            'hAY QUE ACTUALIZAR EL TIPO??
            If cambio Then guardarDV()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

    Private Sub PonerDefaults()
        Try
            dvForm.Table.Columns("REMESA").DefaultValue = remesaa.CODI
            If remesaa.DESCUENTO = True Then
                dvForm.Table.Columns("TIPO").DefaultValue = "C"
            Else
                dvForm.Table.Columns("TIPO").DefaultValue = "D"
            End If
            dvForm.Table.Columns("CENTRO").DefaultValue = remesaa.centro

        Catch ex As Exception
            LOG(ex.ToString)
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

            ret = "WHERE " & tabla.TableName & ".CENTRO = """ & remesaa.centro & """"

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
            id = REMESA
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