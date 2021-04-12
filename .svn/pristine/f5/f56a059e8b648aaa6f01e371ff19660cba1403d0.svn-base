Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes

Public Class clsForma2
    Inherits clsADO

#Region "CAMPOS"

    Private mCODI As String
    Public Property CODI() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCODI = CType(general.nz(dvForm(PA).Row("CODI"), ""), String)
            Catch ex As Exception : End Try
            Return CType(general.nz(mCODI, ""), String)
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CODI, "") Then
                mCODI = CType(general.nz(Value, ""), String)
                dvForm(pa).Row("CODI") = general.nz(mCODI, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mDESCRIPCIO As String
    Public Property DESCRIPCIO() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mDESCRIPCIO = CType(general.nz(dvForm(PA).Row("DESCRIPCIO"), ""), String)
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

    Private mNro As Integer
    Public Property Nro() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mNro = nzn(dvForm(pa).Row("Nro"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNro, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(Nro, 0) Then
                mNro = nzn(Value, 0)
                dvForm(pa).Row("Nro") = nzn(mNro, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mV_1 As Integer
    Public Property V_1() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mV_1 = CInt(nzn(dvForm(PA).Row("V_1"), 0))
            Catch ex As Exception : End Try
            Return nzn(mV_1, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(V_1, 0) Then
                mV_1 = CInt(nzn(Value, 0))
                dvForm(pa).Row("V_1") = nzn(mV_1, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mDies As Integer
    Public Property Dies() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mDies = CInt(nzn(dvForm(PA).Row("Dies"), 0))
            Catch ex As Exception : End Try
            Return nzn(mDies, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(Dies, 0) Then
                mDies = CInt(nzn(Value, 0))
                dvForm(pa).Row("Dies") = nzn(mDies, 0) : GuardarDV()
            End If
        End Set
    End Property

    'Private mESTRANSFERENCIA As Boolean
    'Public Property ESTRANSFERENCIA() As Boolean
    '    Get
    '        If PA() = -1 Then Exit Property
    '        Try
    '            mESTRANSFERENCIA = dvForm(PA).Row("ESTRANSFERENCIA")
    '        Catch ex As Exception : End Try
    '        Return mESTRANSFERENCIA
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        If PA() = -1 Then Exit Property
    '        mESTRANSFERENCIA = Value
    '        dvForm(PA).Row("ESTRANSFERENCIA") = Value : guardarDV()
    '    End Set
    'End Property

#End Region

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext)

        MyBase.New(tabla, centro, bindingcontext)
        Dim sqlSel As String
        Try

            sqlSinWhere = "SELECT forpag.*, " & _
                     " filiales.DESCRI AS NOMCENTRO " & _
                     " FROM forpag " & _
                     " LEFT JOIN filiales ON (filiales.CODI = forpag.CENTRO) "
            sqlSel = sqlSinWhere & _
                        " WHERE forpag.CENTRO = """ & centro & """ ORDER BY forpag.CODI "
            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)


        Catch ex As Exception
            LOG(ex.ToString) : Cargando = False : ccn()
        End Try
    End Sub


#Region "---DESPLAZAMIENTO---"


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
End Class
