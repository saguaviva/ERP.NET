Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class clsEmpresa
    Inherits clsADO

#Region "CAMPOS"

    Private mLOGO As Byte()
    Public Property LOGO() As Byte()
        Get
            If PA = -1 Then Exit Property
            Try
                mLOGO = dvForm(pa).Row("LOGO")
            Catch ex As Exception : End Try
            Return mLOGO
        End Get
        Set(ByVal Value As Byte())
            If PA = -1 Then Exit Property
            If Not Value Is LOGO Then
                mLOGO = Value
                dvForm(pa).Row("LOGO") = mLOGO : guardarDV()
            End If
        End Set
    End Property
    Private mCODI As String
    Public Property CODI() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mCODI = general.nz(dvForm(PA).Row("CODI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCODI, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CODI, "") Then
                mCODI = general.nz(Value, "")
                dvForm(PA).Row("CODI") = general.nz(mCODI, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDESCRI As String
    Public Property DESCRI() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mDESCRI = general.nz(dvForm(PA).Row("DESCRI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDESCRI, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DESCRI, "") Then
                mDESCRI = general.nz(Value, "")
                dvForm(PA).Row("DESCRI") = general.nz(mDESCRI, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDOM As String
    Public Property DOM() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mDOM = general.nz(dvForm(PA).Row("DOM"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDOM, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DOM, "") Then
                mDOM = general.nz(Value, "")
                dvForm(PA).Row("DOM") = general.nz(mDOM, "") : guardarDV()
            End If
        End Set
    End Property

    Private mPROV As String
    Public Property PROV() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mPROV = general.nz(dvForm(PA).Row("PROV"), "")
            Catch ex As Exception : End Try
            Return general.nz(mPROV, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(PROV, "") Then
                mPROV = general.nz(Value, "")
                dvForm(PA).Row("PROV") = general.nz(mPROV, "") : guardarDV()
            End If
        End Set
    End Property

    Private mPOB As String
    Public Property POB() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mPOB = general.nz(dvForm(PA).Row("POB"), "")
            Catch ex As Exception : End Try
            Return general.nz(mPOB, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(POB, "") Then
                mPOB = general.nz(Value, "")
                dvForm(PA).Row("POB") = general.nz(mPOB, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTEL1 As String
    Public Property TEL1() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTEL1 = general.nz(dvForm(PA).Row("TEL1"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTEL1, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TEL1, "") Then
                mTEL1 = general.nz(Value, "")
                dvForm(PA).Row("TEL1") = general.nz(mTEL1, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTEL2 As String
    Public Property TEL2() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTEL2 = general.nz(dvForm(PA).Row("TEL2"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTEL2, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TEL2, "") Then
                mTEL2 = general.nz(Value, "")
                dvForm(PA).Row("TEL2") = general.nz(mTEL2, "") : guardarDV()
            End If
        End Set
    End Property

    Private mFAX As String
    Public Property FAX() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mFAX = general.nz(dvForm(PA).Row("FAX"), "")
            Catch ex As Exception : End Try
            Return general.nz(mFAX, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(FAX, "") Then
                mFAX = general.nz(Value, "")
                dvForm(PA).Row("FAX") = general.nz(mFAX, "") : guardarDV()
            End If
        End Set
    End Property

    Private mOBSERV As String
    Public Property OBSERV() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mOBSERV = general.nz(dvForm(PA).Row("OBSERV"), "")
            Catch ex As Exception : End Try
            Return general.nz(mOBSERV, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(OBSERV, "") Then
                mOBSERV = general.nz(Value, "")
                dvForm(PA).Row("OBSERV") = general.nz(mOBSERV, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCP As String
    Public Property CP() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mCP = general.nz(dvForm(PA).Row("CP"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCP, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CP, "") Then
                mCP = general.nz(Value, "")
                dvForm(PA).Row("CP") = general.nz(mCP, "") : guardarDV()
            End If
        End Set
    End Property

#End Region

    Private dtNum As New DataTable
    Private cmdSelect As MySql.Data.MySqlClient.MySqlCommand
    Private cmdBuilder As MySql.Data.MySqlClient.MySqlCommandBuilder
    Private daMYSQL As MySql.Data.MySqlClient.MySqlDataAdapter

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext)

        MyBase.New(tabla, centro, bindingcontext)
        Dim sqlSel As String
        Try

            sqlSinWhere = "SELECT * FROM filiales "

            sqlSel = sqlSinWhere & " LIMIT 1"

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            numeroRegistros = obtenerNumeroReg(tabla.TableName, "")
            da.Fill(tabla)

            CargarIdentificadores()


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub CargarIdentificadores()
        Try
            CargaTabla(tablaEmpresas, "CODI", "DESCRI", dtIdentificadores, True)
            'dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "
            'dvIdentificadores.Sort = CNForpag

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#Region "DESPLAZAMIENTO"

    Public Overloads Sub SiguienteReg()
        Try
            MyBase.SiguienteReg()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub AnteriorReg()
        Try
            MyBase.AnteriorReg()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub UltimoReg()
        Try
            MyBase.UltimoReg()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub PrimeroReg()
        Try
            MyBase.PrimeroReg()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub CambiarAReg(ByVal id As String, ByVal accion As Integer)
        Try
            MyBase.CambiarAReg(id, "", accion)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

    Public Sub crearNumeraciones()
        Dim strSQL As String
        Dim dr As DataRow
        Try
            strSQL = "SELECT * FROM NUMERACIONES"

            cmdSelect = New MySql.Data.MySqlClient.MySqlCommand(strSQL, cnn)
            daMYSQL = New MySql.Data.MySqlClient.MySqlDataAdapter(cmdSelect)
            cmdBuilder = New MySql.Data.MySqlClient.MySqlCommandBuilder(daMYSQL)
            ACN()
            daMYSQL.Fill(dtNum)
            CCN()

            dtNum.Columns("CENTRO").DefaultValue = CODI
            dtNum.Columns("TIPUS").DefaultValue = "M"

            dr = dtNum.NewRow

            dr("TABLA") = "FACTUR"
            dr("DOCUMENT") = Factura
            dr("CODI") = GetNumeroUltimoPAF(Factura, "C")
            dtNum.Rows.Add(dr)

            dr = dtNum.NewRow
            dr("TABLA") = "FACTUR"
            dr("DOCUMENT") = Albaran
            dr("CODI") = GetNumeroUltimoPAF(Albaran, "C")
            dtNum.Rows.Add(dr)

            dr = dtNum.NewRow
            dr("TABLA") = "FACTUR"
            dr("DOCUMENT") = Pedido
            dr("CODI") = GetNumeroUltimoPAF(Pedido, "C")
            dtNum.Rows.Add(dr)

            dr = dtNum.NewRow
            dr("TABLA") = "FACTUR"
            dr("DOCUMENT") = Proforma
            dr("CODI") = GetNumeroUltimoPAF(Proforma, "C")
            dtNum.Rows.Add(dr)

            'ORDENES MODELOS
            dr = dtNum.NewRow
            dr("TABLA") = "ORDRE"
            dr("DOCUMENT") = OrdenModelos
            dr("CODI") = 0 'GetNumeroUltimoPAFCompra(Orden, "M", "C")
            dtNum.Rows.Add(dr)

            'ORDENES MUESTRAS
            dr = dtNum.NewRow
            dr("TABLA") = "CACTUR"
            dr("DOCUMENT") = OrdenFabComplementos
            dr("CODI") = GetNumeroUltimoPAFCompra(OrdenFabComplementos, "M", "C")
            dtNum.Rows.Add(dr)

            'ALBARANES ENTREGA HILOS
            dr = dtNum.NewRow
            dr("TABLA") = "CACTUR"
            dr("DOCUMENT") = Albaran
            dr("CODI") = GetNumeroUltimoPAFCompra(Albaran, "M", "C")
            dtNum.Rows.Add(dr)

            'PEDIDOS COMPRA HILOS
            dr = dtNum.NewRow
            dr("TABLA") = "CACTUR"
            dr("DOCUMENT") = Pedido
            dr("CODI") = GetNumeroUltimoPAFCompra(Pedido, "M", "C")
            dtNum.Rows.Add(dr)

            'PEDIDOS COMPRA TEJIDOS
            dr = dtNum.NewRow
            dr("TABLA") = "CACTUR"
            dr("TIPUS") = "T"
            dr("DOCUMENT") = Pedido
            dr("CODI") = GetNumeroUltimoPAFCompra(Pedido, "T", "C")
            dtNum.Rows.Add(dr)

            'dr("TABLA") = "CACTUR"
            'dr("DOCUMENT") = Proforma
            'dtNum.Rows.Add(dr)

            daMYSQL.Update(dtNum)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
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
    Public Overloads Sub NuevoRegistro()
        Try
            MyBase.NuevoRegistro()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Function genWhere() As String
        Try
            Dim ret As String = ""
            'ret = "WHERE " & tabla.TableName & ".CENTRO = """ & centro & """"
            Return ret

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function GenOrder() As String
        Try
            Return " ORDER BY CODI  "
            'Return " ORDER BY TEMPORADA, CLIENT, SERIE, CODI "

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        If id Is Nothing Then
            id = CODI
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
