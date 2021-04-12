Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Public Class clsImpresionVenta
    Inherits clsADO

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
            If nzn(Value, 0) <> nzn(FRA, 0) Then
                mFRA = nzn(Value, 0)
                dvForm(PA).Row("FRA") = nzn(mFRA, 0) : guardarDV()
            End If
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
            If nzn(Value, 0) <> nzn(NLINEA, 0) Then
                mNLINEA = nzn(Value, 0)
                dvForm(PA).Row("NLINEA") = nzn(mNLINEA, 0) : guardarDV()
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
            If general.nz(Value, "") <> general.nz(TIPUS, "") Then
                mTIPUS = general.nz(Value, "")
                dvForm(PA).Row("TIPUS") = general.nz(mTIPUS, "") : guardarDV()
            End If
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
            If general.nz(Value, "") <> general.nz(DOCUMENT, "") Then
                mDOCUMENT = general.nz(Value, "")
                dvForm(PA).Row("DOCUMENT") = general.nz(mDOCUMENT, "") : guardarDV()
            End If
        End Set
    End Property

    Private mNOMBREDETALLE As String
    Public Property NOMBREDETALLE() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMBREDETALLE = general.nz(dvForm(PA).Row("NOMBREDETALLE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMBREDETALLE, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(NOMBREDETALLE, "") Then
                mNOMBREDETALLE = general.nz(Value, "")
                dvForm(PA).Row("NOMBREDETALLE") = general.nz(NOMBREDETALLE, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDESCRIDETALLE As String
    Public Property DESCRIDETALLE() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mDESCRIDETALLE = general.nz(dvForm(PA).Row("DESCRIDETALLE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDESCRIDETALLE, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DESCRIDETALLE, "") Then
                mDESCRIDETALLE = general.nz(Value, "")
                dvForm(PA).Row("DESCRIDETALLE") = general.nz(DESCRIDETALLE, "") : guardarDV()
            End If
        End Set
    End Property
#End Region

#Region "VARIABLES"

    Friend paf As clsPAFVenta

#End Region

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext, ByVal p As clsPAFVenta)

        MyBase.New(tabla, centro, bindingcontext, "ESDETALLE")
        Dim sqlSel As String
        Try
            PAF = p
            sqlSinWhere = " SELECT * FROM dfaturimpresion  "

            sqlSel = sqlSinWhere & _
                    " WHERE FRA = " & paf.FRA & " AND " & _
                    " DOCUMENT = """ & paf.documento & """ AND " & _
                    " TIPUS = """ & paf.tipo & """ AND " & _
                    " CENTRO = """ & paf.centro & """ "
            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            dvForm.Sort = "NLINEA ASC"
            PonerDefaults()
            Try : AddHandler tabla.RowChanged, AddressOf RowChanged : Catch ex As Exception : End Try

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Private Sub PonerDefaults()
        Try
            dvForm.Table.Columns("TIPUS").DefaultValue = paf.TIPUS
            dvForm.Table.Columns("DOCUMENT").DefaultValue = paf.documento
            dvForm.Table.Columns("FRA").DefaultValue = paf.FRA
            dvForm.Table.Columns("CENTRO").DefaultValue = paf.centro
            dvForm.Table.Columns("NLINEA").DefaultValue = 10000

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Sub RowChanged(ByVal sender As Object, ByVal e As System.data.DataRowChangeEventArgs)
        If e.Action = DataRowAction.Change AndAlso Not (e.Row Is Nothing) AndAlso Not (e.Row.RowState = DataRowState.Unchanged) Then

        End If
        Try
            If Not PAF.cargando Then
                PAF.DormirHandlers()
                If NLINEA = 10000 Then
                    Try
                        If dvForm(dvForm.Count - 2).Item("NLINEA") = -1 Then
                            NLINEA = 1
                        Else
                            NLINEA = dvForm(dvForm.Count - 2).Item("NLINEA") + 1
                        End If

                    Catch ex As Exception
                        NLINEA = 1
                    End Try
                End If
                PAF.DespertarHandlers()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Sub CambioDetalle(ByVal centro As String, ByVal p As clsPAFVenta)
        Dim sqlSel As String
        Try
            paf = p
            Me.centro = centro
            sqlSel = sqlSinWhere & _
                        " WHERE FRA = " & paf.FRA & " AND " & _
                        " DOCUMENT = """ & paf.documento & """ AND " & _
                        " TIPUS = """ & paf.tipo & """ AND " & _
                        " CENTRO = """ & paf.centro & """ "

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel

            tabla.Clear()
            da.Fill(tabla)
            dvForm.Sort = "NLINEA ASC"
            PonerDefaults()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub ActualizarDetalle()
        Dim i As Integer
        Dim cambio As Boolean = False
        Try
            For i = 0 To dvForm.Count - 1
                If nzn(dvForm(i).Item("FRA"), "") <> nzn(paf.FRA, 0) Then dvForm(i).Item("FRA") = nzn(paf.FRA, 0) : cambio = True
                If general.nz(dvForm(i).Item("DOCUMENT"), "") <> general.nz(paf.DOCUMENT, "") Then dvForm(i).Item("DOCUMENT") = general.nz(paf.DOCUMENT, "") : cambio = True
                If general.nz(dvForm(i).Item("CENTRO"), "") <> general.nz(paf.centro, "") Then dvForm(i).Item("CENTRO") = general.nz(paf.centro, "") : cambio = True
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
    Public Sub AñadirLineaDetalleImpresion(ByVal concepto As String, ByVal valor As String)
        Dim dr As DataRow
        Try
            dr = tabla.NewRow
            dr("NOMBREDETALLE") = concepto
            dr.Item("CENTRO") = paf.centro
            dr.Item("NLINEA") = dvForm.Count
            dr.Item("DESCRIDETALLE") = valor

            Try
                tabla.Rows.Add(dr)
                MyBase.ActualizarOrigen(True, True)
            Catch ex As Exception
                'Throw ex
                LOG(ex.ToString, True)
            End Try


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
    Public Overrides Sub borrar()
        Try
            'Este borra todas las lineas
            BorrarActualDVDetalle()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
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

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        If id Is Nothing Then
            ' id = FIL
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

End Class
