Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes



Public Class clsAcabadosTejidos
    Inherits clsADO

#Region "CAMPOS"

    Private mACABAT As String
    Public Property ACABAT() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mACABAT = general.nz(dvForm(pa).Row("ACABAT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mACABAT, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(ACABAT, "") Then
                mACABAT = general.nz(Value, "")
                dvForm(pa).Row("ACABAT") = general.nz(mACABAT, "") : guardarDV()
            End If
        End Set
    End Property

    Private mPROVE As Integer
    Public Property PROVE() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mPROVE = nzn(dvForm(pa).Row(tablaProveedores), 0)
            Catch ex As Exception : End Try
            Return nzn(mPROVE, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If esCodigoExistente(dtPROVE, CCProve, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mPROVE = nzn(Value, 0)
                    dvForm(pa).Row("NOMPROVE") = general.OBN(Value, dtPROVE, CNProve)
                    dvForm(pa).Row(tablaProveedores) = nzn(Value, 0) : guardarDV()
                End If
            Else
                dvForm(pa).Row("PROVE") = 0

                dvForm(pa).Row("NOMPROVE") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTEPROVE"))
            End If
        End Set
    End Property

    Private mNOMPROVE As String
    Public Property NOMPROVE() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mNOMPROVE = general.nz(dvForm(pa).Row("NOMPROVE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMPROVE, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mNOMPROVE = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(pa).Row("NOMPROVE") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(pa).Row("NOMPROVE") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTEIXIT As String
    Public Property TEIXIT() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mTEIXIT = general.nz(dvForm(pa).Row("TEIXIT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTEIXIT, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TEIXIT, "") Then
                mTEIXIT = general.nz(Value, "")
                dvForm(pa).Row("TEIXIT") = general.nz(mTEIXIT, "") : guardarDV()
            End If
        End Set
    End Property

    Private mORDEN As Integer
    Public Property ORDEN() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mORDEN = nzn(dvForm(PA).Row("ORDEN"), 0)
            Catch ex As Exception : End Try
            Return nzn(mORDEN, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(ORDEN, 0) Then
                mORDEN = nzn(Value, 0)
                dvForm(PA).Row("ORDEN") = nzn(mORDEN, 0) : guardarDV()
            End If
        End Set
    End Property

#End Region

#Region "VARIABLES"

    Friend m_Tejido As clsTejido
    Private dtProve As New DataTable
    Friend dvTodosLosAcabados As New DataView

#End Region

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext, ByVal tejid As clsTejido)

        MyBase.New(tabla, centro, bindingcontext, "ESDETALLE")
        Dim sqlSel As String
        Try
            m_Tejido = tejid

            sqlSinWhere = "SELECT acabatsteixits.*, " &
                                      " filiales.DESCRI AS NOMCENTRO, " &
                                      " dacabats.PREUM, dacabats.PREUK " &
                                      " FROM acabatsteixits " &
                                      " INNER JOIN filiales ON (filiales.CODI = acabatsteixits.CENTRO) " &
                                      " INNER JOIN dacabats ON (dacabats.CODI = acabatsteixits.ACABAT AND dacabats.PROVE = acabatsteixits.PROVE) "
            sqlSel = sqlSinWhere & _
                        " WHERE acabatsteixits.CENTRO = """ & m_Tejido.centro & """ AND  acabatsteixits.TEIXIT = """ & m_Tejido.CODI & """ AND  " & _
                                      " ACABATSTEIXITS.PROVE = " & m_Tejido.ACABADOR & " ORDER BY dacabats.CODI "

            '" LIMIT 1"
            cmdSel.CommandText = sqlSel
            dvForm.Sort = "ORDEN"
            da.SelectCommand = cmdSel
            da.Fill(tabla)

            PonerDefaults()
            tabla.AcceptChanges()
            CargarTodosLosAcabados()
            AddHandler tabla.RowChanged, AddressOf RowChanged

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#Region "ORGANIZAR"

    Private Sub PonerDefaults()

        Try
            With dvForm
                .Table.Columns("ACABAT").DefaultValue = ""
                .Table.Columns("PROVE").DefaultValue = m_Tejido.ACABADOR
                .Table.Columns("TEIXIT").DefaultValue = m_Tejido.CODI
                .Table.Columns("ORDEN").DefaultValue = 10000
                .Table.Columns("CENTRO").DefaultValue = centro
            End With

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Sub CambioDetalle(ByVal centro As String, ByVal tejid As clsTejido)
        Dim sqlSel As String
        Try
            m_Tejido = tejid
            Me.centro = centro
            '        cmdSelectAcabados.CommandText = "SELECT acabatsteixits.*, dacabats.PREUM, dacabats.PREUK FROM acabatsteixits LEFT JOIN dacabats ON (dacabats.PROVE = " & tejidoActual.ACABADOR & " AND dacabats.CODI = acabatsteixits.ACABAT)"

            sqlSinWhere = "SELECT acabatsteixits.*, " &
                                      " filiales.DESCRI AS NOMCENTRO, " &
                                      " dacabats.PREUM, dacabats.PREUK " &
                                      " FROM acabatsteixits " &
                                      " INNER JOIN filiales ON (filiales.CODI = acabatsteixits.CENTRO) " &
                                      " INNER JOIN dacabats ON (dacabats.CODI = acabatsteixits.ACABAT AND dacabats.PROVE = acabatsteixits.PROVE) "
            sqlSel = sqlSinWhere & _
                        " WHERE acabatsteixits.CENTRO = """ & m_Tejido.centro & """ AND  acabatsteixits.TEIXIT = """ & m_Tejido.CODI & """ AND  " & _
                                      " ACABATSTEIXITS.PROVE = " & m_Tejido.ACABADOR & " ORDER BY dacabats.CODI "
            cmdSel.CommandText = sqlSel
            dvForm.Sort = "ACABAT"

            da.SelectCommand = cmdSel
            tabla.Clear()
            da.Fill(tabla)
            dvForm.Sort = "ORDEN"

            PonerDefaults()
            tabla.AcceptChanges()
            CargarTodosLosAcabados()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub ActualizarDetalle()
        Dim i As Integer
        Dim cambio As Boolean = False
        Try
            For i = 0 To dvForm.Count - 1
                If general.nz(dvForm(i).Item("TEIXIT"), "") <> general.nz(m_Tejido.CODI, "") Then dvForm(i).Item("TEIXIT") = general.nz(m_Tejido.CODI, "") : cambio = True
                'If general.nz(dvForm(i).Item("CENTRO"), "") <> general.nz(m_Tejido.centro, "") Then dvForm(i).Item("CENTRO") = general.nz(m_Tejido.centro, "") : cambio = True
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
        BorrarActualDVDetalle()
        'ActualizarOrigen()
    End Sub
    Friend Function EstaSeleccionado(ByVal acabado As String) As Boolean
        Try
            If dvForm.Find(acabado) = -1 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Sub RowChanged(ByVal sender As Object, ByVal e As System.data.DataRowChangeEventArgs)
        Try
            If ORDEN = 10000 Then
                Try
                    If dvForm(dvForm.Count - 2).Item("ORDEN") = -1 Then
                        ORDEN = 1
                    Else
                        ORDEN = dvForm(dvForm.Count - 2).Item("ORDEN") + 1
                    End If

                Catch ex As Exception
                    ORDEN = 1
                End Try
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Sub AñadirAcabado(ByVal acabado As String)
        Dim dr As DataRow
        Try
            dr = tabla.NewRow
            dr("ACABAT") = acabado
            dr.Item("PROVE") = m_Tejido.ACABADOR
            dr.Item("TEIXIT") = m_Tejido.CODI
            dr.Item("CENTRO") = m_Tejido.centro
            Try
                tabla.Rows.Add(dr)
            Catch ex As Exception
                Throw ex
            End Try

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Sub QuitarAcabado(ByVal acabado As String)
        Dim key(3) As String
        Try
            key(0) = acabado
            key(1) = m_Tejido.ACABADOR
            key(2) = m_Tejido.CODI
            key(3) = m_Tejido.centro
            tabla.Rows.Find(key).Delete()
            'dvForm(dvForm.Find(acabado)).Delete()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Sub CargarTodosLosAcabados()
        'Aqui cargamos todos los acabados de todos los talleres y lo tenemos en dvTodosLosAcabados
        Dim cmdSel As New MySqlCommand("SELECT * FROM dacabats WHERE PROVE = " & m_Tejido.ACABADOR, cnn)
        Dim daSelAcabados As New MySqlDataAdapter
        Try
            ACN()
            ds.Tables("dacabats").Clear()
            daSelAcabados.SelectCommand = cmdSel
            daSelAcabados.Fill(ds.Tables("dacabats"))
            dvTodosLosAcabados = ds.Tables("dacabats").DefaultView
            dvTodosLosAcabados.Sort = "CODI"
            CCN()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Friend Sub PonerPrecioAcabados()
        Dim idx, i As Integer
        Dim PrecioAcabado As Double = 0
        Try
            'En dvAcabado tenemos que elegir el acabador, tejido y operacion actual
            For i = 0 To dvForm.Count - 1
                idx = dvTodosLosAcabados.Find(dvForm(i).Item("ACABAT"))
                If Not idx = -1 Then
                    'Si el preu en metres es 0 vol dir que no hem de multiplicar pel rendiment
                    If dvTodosLosAcabados(idx).Item("PREUM") = 0 Then
                        PrecioAcabado = PrecioAcabado + dvTodosLosAcabados(idx).Item("PREUK")
                    Else
                        PrecioAcabado = PrecioAcabado + roundnum(dvTodosLosAcabados(idx).Item("PREUM") * m_Tejido.RENDIMENT, 2)
                    End If
                End If
            Next
            If PrecioAcabado <> 0 Then m_Tejido.PACA = PrecioAcabado

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub PonerAcabadosEnTejido()
        Dim i As Integer
        Dim str As String
        Try
            str = ""
            For i = 0 To dvForm.Count - 1
                If str = "" Then
                    str = dvForm(i).Item("ACABAT")
                Else
                    str = str & " - " & dvForm(i).Item("ACABAT")
                End If
            Next
            If m_Tejido.ACABAT <> str Then
                m_Tejido.ACABAT = str
            End If

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
            Return " ORDER BY filcol.color "
            'Return " ORDER BY TEMPORADA, CLIENT, SERIE, CODI "
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        If id Is Nothing Then
            id = ACABAT
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

    Friend Sub borraracabados()
        'Dim cmdDel As New MySqlCommand("delete FROM acabatsteixits WHERE PROVE = " & m_Tejido.ACABADOR & " and teixit = '" & m_Tejido.CODI & "' and centro = '" & m_Tejido.centro & "' ", cnn)
        Try
            'tabla.Rows.Clear()
            'tabla.AcceptChanges()
            'dvForm.Table.Rows.Clear()
            'PonerAcabadosEnTejido()
            For i As Integer = dvForm.Table.Rows.Count - 1 To 0 Step -1
                dvForm.Delete(i)
            Next

            'ACN()
            'cmdDel.ExecuteNonQuery()
            'CCN()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

End Class
