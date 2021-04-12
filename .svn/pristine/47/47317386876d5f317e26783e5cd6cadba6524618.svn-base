Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes

Public Class clsBanco
    Inherits clsADO

#Region "VARIABLES"

    Public cuentas As clsCuentasBancos

#End Region

#Region "CAMPOS"

    '+------------+------------+------+-----+---------+-------+
    '| Field      | Type       | Null | Key | Default | Extra |
    '+------------+------------+------+-----+---------+-------+
    '| CODI       | varchar(4) | NO   | PRI | 0000    |       |
    '| DESCRIPCIO | tinytext   | YES  |     | NULL    |       |
    '| LOGO       | blob       | YES  |     | NULL    |       |
    '| CENTRO     | char(1)    | NO   | PRI | C       |       |
    '+------------+------------+------+-----+---------+-------+
    '4 rows

    Private mCODI As String
    Public Property CODI() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCODI = general.nz(dvForm(pa).Row("CODI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCODI, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CODI, "") Then
                mCODI = general.nz(Value, "")
                dvForm(pa).Row("CODI") = general.nz(mCODI, "") : guardarDV()
            End If
        End Set
    End Property

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
                dvForm(pa).Row("DESCRIPCIO") = general.nz(mDESCRIPCIO, "") : guardarDV()
            End If
        End Set
    End Property
    Private mSWIFT As String
    Public Property SWIFT() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mSWIFT = general.nz(dvForm(PA).Row("SWIFT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mSWIFT, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(SWIFT, "") Then
                mSWIFT = general.nz(Value, "")
                dvForm(PA).Row("SWIFT") = general.nz(mSWIFT, "") : guardarDV()
            End If
        End Set
    End Property
    Private mIBAN As String
    Public Property IBAN() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mIBAN = general.nz(dvForm(PA).Row("IBAN"), "")
            Catch ex As Exception : End Try
            Return general.nz(mIBAN, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(IBAN, "") Then
                mIBAN = general.nz(Value, "")
                dvForm(PA).Row("IBAN") = general.nz(mIBAN, "") : guardarDV()
            End If
        End Set
    End Property
    Private mNUMCUENTA As String
    Public Property NUMCUENTA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNUMCUENTA = general.nz(dvForm(PA).Row("NUMCUENTA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNUMCUENTA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(NUMCUENTA, "") Then
                mNUMCUENTA = general.nz(Value, "")
                dvForm(PA).Row("NUMCUENTA") = general.nz(mNUMCUENTA, "") : guardarDV()
            End If
        End Set
    End Property
    Private mLOGO As Byte()
    Public Property LOGO() As Byte()
        Get
            If PA() = -1 Then Exit Property
            Try
                mLOGO = dvForm(PA).Row("LOGO")
            Catch ex As Exception : End Try
            Return mLOGO
        End Get
        Set(ByVal Value As Byte())
            If PA() = -1 Then Exit Property
            If Not Value Is LOGO Then
                mLOGO = Value
                dvForm(PA).Row("LOGO") = mLOGO : guardarDV()
            End If
        End Set
    End Property


#End Region

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext)
        MyBase.New(tabla, centro, bindingcontext)
        Dim sqlSel As String
        Try

            sqlSinWhere = "SELECT bancs.*, " & _
                     " filiales.DESCRI AS NOMCENTRO " & _
                     " FROM bancs " & _
                     " LEFT JOIN filiales ON (filiales.CODI = bancs.CENTRO) "

            sqlSel = sqlSinWhere & _
                        " WHERE bancs.CENTRO = """ & centro & """ ORDER BY bancs.CODI " & _
                        " LIMIT 1"

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            IniciarLineas()
            CargarIdentificadores()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub CargarIdentificadores()
        Try
            CargaTabla(tablaBancos, CCBancs, CNBancs, dtIdentificadores, True)

            dtIdentificadores.Columns(CCBancs).Caption = rm.GetString("CODIGO")
            dtIdentificadores.Columns(CNBancs).Caption = rm.GetString("NOM")
            dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "
            dvIdentificadores.Sort = CNBancs

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub IniciarLineas()
        Try
            Dim dt As New aura2k3.ds11.bancscuentasDataTable
            cuentas = New clsCuentasBancos(dt, Me.centro, Me.bc, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            ACN()
            MyBase.ActualizarOrigen(True)
            cuentas.ActualizarOrigen(True, True)
            CCN()

        Catch ex As Exception
            Throw ex
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#Region "DESPLAZAMIENTO"

    Public Overloads Sub SiguienteReg()
        Try
            MyBase.SiguienteReg()
            If Not PA() = -1 Then cuentas.CambioDetalle(centro, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub AnteriorReg()
        Try
            MyBase.AnteriorReg()
            If Not PA() = -1 Then cuentas.CambioDetalle(centro, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub UltimoReg()
        Try
            MyBase.UltimoReg()
            If Not PA() = -1 Then cuentas.CambioDetalle(centro, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub PrimeroReg()
        Try
            MyBase.PrimeroReg()
            If Not PA() = -1 Then cuentas.CambioDetalle(centro, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub CambiarAReg(ByVal id As String, ByVal accion As Integer)
        Dim sql As String
        Try
            MyBase.CambiarAReg(id, "", accion)
            If Not PA() = -1 Then cuentas.CambioDetalle(centro, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "OVERRIDES"

    Friend Overrides Function TieneCambios() As Boolean
        Try
            guardarDV()
            If Not tabla.GetChanges Is Nothing OrElse cuentas.TieneCambios Then
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
            Dim ret As String

            ret = "WHERE " & tabla.TableName & ".CENTRO = """ & centro & """"

            Return ret
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function GenOrder() As String
        Try
            Return " ORDER BY CODI "
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
