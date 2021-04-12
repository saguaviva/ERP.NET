Imports CoreLab.MySql
Public Class clsComposicionTejido
    Inherits clsADO
#Region "---CAMPOS---"
    Private mTEIXIT As String
    Public Property TEIXIT() As String
        Get
            Try
                mTEIXIT = nz(dvForm(pa).Row("TEIXIT"), "")
            Catch ex As Exception : End Try
            Return nz(mTEIXIT, "")
        End Get
        Set(ByVal Value As String)
            mTEIXIT = nz(Value, "")
            dvForm(pa).Row("TEIXIT") = nz(Value, "") : GuardarDV()
        End Set
    End Property

    Private mCOMP As String
    Public Property COMP() As String
        Get
            Try
                mCOMP = nz(dvForm(pa).Row("COMP"), "")
            Catch ex As Exception : End Try
            Return nz(mCOMP, "")
        End Get
        Set(ByVal Value As String)
            mCOMP = nz(Value, "")
            dvForm(pa).Row("COMP") = nz(Value, "") : GuardarDV()
        End Set
    End Property

    Private mPER As Integer
    Public Property PER() As Integer
        Get
            Try
                mPER = nzn(dvForm(pa).Row("PER"), 0)
            Catch ex As Exception : End Try
            Return nzn(mPER, 0)
        End Get
        Set(ByVal Value As Integer)
            mPER = nzn(Value, 0)
            dvForm(pa).Row("PER") = nzn(Value, 0) : GuardarDV()
        End Set
    End Property

    Private mPROVE As Integer
    Public Property PROVE() As Integer
        Get
            Try
                mPROVE = nzn(dvForm(pa).Row("PROVE"), 0)
            Catch ex As Exception : End Try
            Return nzn(mPROVE, 0)
        End Get
        Set(ByVal Value As Integer)
            If esCodigoExistente(dtProve, CCProve, Value) Then
                mPROVE = nzn(Value, 0)
                dvForm(pa).Row("NOMPROVE") = OBN(Value, dtProve, CNProve)
                dvForm(pa).Row("PROVE") = nzn(Value, 0) : GuardarDV()
            Else
                dvForm(pa).Row("PROVE") = DBNull.Value

                dvForm(pa).Row("NOMPROVE") = "" : GuardarDV()
                MessageBox.Show(RM.GetString("NOEXISTEPROVE"))
            End If
        End Set
    End Property

    Private mNOMPROVE As String
    Public Property NOMPROVE() As String
        Get
            Try
                mNOMPROVE = nz(dvForm(pa).Row("NOMPROVE"), "")
            Catch ex As Exception : End Try
            Return nz(mNOMPROVE, "")
        End Get
        Set(ByVal Value As String)
            mNOMPROVE = nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(pa).Row("NOMPROVE") = nz(Value, "") : GuardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(pa).Row("NOMPROVE") = nz(Value, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mPREU As Double
    Public Property PREU() As Double
        Get
            Try
                mPREU = nzn(dvForm(pa).Row("PREU"), 0)
            Catch ex As Exception : End Try
            Return nzn(mPREU, 0)
        End Get
        Set(ByVal Value As Double)
            mPREU = nzn(Value, 0)
            dvForm(pa).Row("PREU") = nzn(Value, 0) : GuardarDV()
        End Set
    End Property

    Private mIMPORT As Double
    Public Property IMPORT() As Double
        Get
            Try
                mIMPORT = nzn(dvForm(pa).Row("IMPORT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mIMPORT, 0)
        End Get
        Set(ByVal Value As Double)
            mIMPORT = nzn(Value, 0)
            dvForm(pa).Row("IMPORT") = nzn(Value, 0) : GuardarDV()
        End Set
    End Property

#End Region
    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, _
                ByVal conexion As MySqlConnection, _
                ByRef bindingcontext As BindingContext)

        MyBase.New(tabla, centro, conexion, bindingcontext, "ESDETALLE")
        Try

            sqlSinWhere = "SELECT mattei.*, " & _
                " prove.NOM as NOMPROVE,  " & _
                " filiales.DESCRI AS NOMCENTRO " & _
                " FROM mattei  " & _
                " LEFT JOIN prove ON (mattei.prove = prove.codi) " & _
                " LEFT JOIN filiales ON (filiales.CODI = mattei.CENTRO) "

            sqlSel = sqlSinWhere & _
                        " ORDER BY mattei.teixit"
            '" LIMIT 1"
            cmdSel.CommandText = sqlSel
            dvForm.Sort = "TEIXIT"
            da.SelectCommand = cmdSel
            da.Fill(tabla)

            tabla.Columns.Add("ELEGIRCOMP")

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Friend Overrides Function genWhere(byval ID as object, byval esCambio as boolean) As String
        Try
            Dim ret As String

            ret = "WHERE " & tabla.TableName & ".CENTRO = """ & centro & """"

            Return ret
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function
    Friend Overrides Function GenOrder() As String
        Try
            Return ""
            'Return " ORDER BY TEMPORADA, CLIENT, SERIE, CODI "
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        Dim cmd As New MySqlCommand(" SELECT " & _
            " (SELECT COUNT(*) " & _
            " FROM " & tabla.TableName & " AS M2 WHERE " & _
            " M2.CODI <= M1.CODI) AS rownum FROM " & tabla.TableName & " AS M1 WHERE CODI = """ & id & """ " & WC() & GenOrder(), cn)
        Try
            Dim idx As Integer = cmd.ExecuteScalar()


            Return idx - 1

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function
    Friend Overrides Function genWhereNumeroRegistros() As String
        Try
            Return genWhere(nothing, false)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function

    Public Overrides Sub ActualizarOrigen(Optional ByVal NOCERRAR As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            Try : guardarDV() : Catch ex As Exception : End Try

            If Not tabla.GetChanges Is Nothing Then
                ACN()
                da.Update(tabla)
                If Not NOCERRAR Then : CCN() : End If
                tabla.AcceptChanges()
            End If


        Catch ex As Exception
            If ex.ToString.Substring(0, 9) = "Duplicate" Then
                MessageBox.Show(RM.GetString("REGISTRODUPLICADO"))
                Throw ex
            Else
                MessageBox.Show(ex.ToString)
            End If
            cn.Close()
        End Try
    End Sub
End Class
