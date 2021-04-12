Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes

Public Class clsError
    Inherits clsADO

#Region "---CAMPOS---"
    Private mERROR As String

    Public Property ERRORR() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mERROR = general.nz(dvForm(pa).Row("ERROR"), "")
            Catch ex As Exception : End Try
            Return general.nz(mERROR, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mERROR = general.nz(Value, "")
            dvForm(pa).Row("ERROR") = general.nz(Value, "") : GuardarDV()
        End Set
    End Property

    Private mFECHA As Date
    Public Property FECHA() As Date
        Get
            If PA = -1 Then Exit Property
            Try
                mFECHA = dvForm(pa).Row("FECHA")
            Catch ex As Exception : End Try
            Return mFECHA
        End Get
        Set(ByVal Value As Date)
            If PA = -1 Then Exit Property
            mFECHA = Value
            dvForm(pa).Row("FECHA") = Value : guardarDV()
        End Set
    End Property

    Private mSOLUCIONADO As Boolean
    Public Property SOLUCIONADO() As Boolean
        Get
            If PA = -1 Then Exit Property
            Try
                mSOLUCIONADO = dvForm(pa).Row("SOLUCIONADO")
            Catch ex As Exception : End Try
            Return mSOLUCIONADO
        End Get
        Set(ByVal Value As Boolean)
            If PA = -1 Then Exit Property
            mSOLUCIONADO = Value
            dvForm(pa).Row("SOLUCIONADO") = Value : guardarDV()
        End Set
    End Property

    Private mFORMULARIO As String
    Public Property FORMULARIO() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mFORMULARIO = general.nz(dvForm(pa).Row("FORMULARIO"), "")
            Catch ex As Exception : End Try
            Return general.nz(mFORMULARIO, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mFORMULARIO = general.nz(Value, "")
            dvForm(pa).Row("FORMULARIO") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mDESCRI As String
    Public Property DESCRI() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mDESCRI = general.nz(dvForm(pa).Row("DESCRI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDESCRI, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mDESCRI = general.nz(Value, "")
            dvForm(pa).Row("DESCRI") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mCODI As Integer
    Public Property CODI() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mCODI = nzn(dvForm(pa).Row("CODI"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCODI, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            mCODI = nzn(Value, 0)
            dvForm(pa).Row("CODI") = nzn(Value, 0) : guardarDV()
        End Set
    End Property
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

    Public Sub New(ByVal tabla As DataTable, _
               ByVal centro As String, _
                                  ByRef bindingcontext As BindingContext)

        MyBase.New(tabla, centro, bindingcontext)
        Dim sqlSel As String
        Try

            sqlSinWhere = "SELECT errores.*, " & _
                     " filiales.DESCRI AS NOMCENTRO " & _
                     " FROM errores " & _
                     " LEFT JOIN filiales ON (filiales.CODI = errores.CENTRO) "
            sqlSel = sqlSinWhere & _
                        " WHERE errores.CENTRO = """ & centro & """ ORDER BY errores.ERROR " & _
                        " LIMIT 1"

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)

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
        Dim ret As String

        ret = " ORDER BY CODI  "

        Return ret
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

End Class
