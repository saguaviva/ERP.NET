Imports CoreLab.MySql
Public Class clsCartaColoresHilo
    Inherits clsADO
#Region "---CAMPOS----"

    Private mTIPUS As String
    Public Property TIPUS() As String
        Get
            Try
                mTIPUS = nz(dvForm(pa).Row("TIPUS"), "")
            Catch ex As Exception : End Try
            Return nz(mTIPUS, "")
        End Get
        Set(ByVal Value As String)
            mTIPUS = nz(Value, "")
            dvForm(pa).Row("TIPUS") = nz(Value, "") : GuardarDV()
        End Set
    End Property

    Private mFIL As String
    Public Property FIL() As String
        Get
            Try
                mFIL = nz(dvForm(pa).Row("FIL"), "")
            Catch ex As Exception : End Try
            Return nz(mFIL, "")
        End Get
        Set(ByVal Value As String)
            mFIL = nz(Value, "")
            dvForm(pa).Row("FIL") = nz(Value, "") : GuardarDV()
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

    Private mCOLOR As String
    Public Property COLOR() As String
        Get
            Try
                mCOLOR = nz(dvForm(pa).Row("COLOR"), "")
            Catch ex As Exception : End Try
            Return nz(mCOLOR, "")
        End Get
        Set(ByVal Value As String)
            mCOLOR = nz(Value, "")
            dvForm(pa).Row("COLOR") = nz(Value, "") : GuardarDV()
        End Set
    End Property

    Private mACTUAL As Double
    Public Property ACTUAL() As Double
        Get
            Try
                mACTUAL = nzn(dvForm(pa).Row("ACTUAL"), 0)
            Catch ex As Exception : End Try
            Return nzn(mACTUAL, 0)
        End Get
        Set(ByVal Value As Double)
            mACTUAL = nzn(Value, 0)
            dvForm(pa).Row("ACTUAL") = nzn(Value, 0) : GuardarDV()
        End Set
    End Property

    Private mMINIM As Double
    Public Property MINIM() As Double
        Get
            Try
                mMINIM = nzn(dvForm(pa).Row("MINIM"), 0)
            Catch ex As Exception : End Try
            Return nzn(mMINIM, 0)
        End Get
        Set(ByVal Value As Double)
            mMINIM = nzn(Value, 0)
            dvForm(pa).Row("MINIM") = nzn(Value, 0) : GuardarDV()
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

    Private mTINTAR As Double
    Public Property TINTAR() As Double
        Get
            Try
                mTINTAR = nzn(dvForm(pa).Row("TINTAR"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTINTAR, 0)
        End Get
        Set(ByVal Value As Double)
            mTINTAR = nzn(Value, 0)
            dvForm(pa).Row("TINTAR") = nzn(Value, 0) : GuardarDV()
        End Set
    End Property

    Private mMETRES As Double
    Public Property METRES() As Double
        Get
            Try
                mMETRES = nzn(dvForm(pa).Row("METRES"), 0)
            Catch ex As Exception : End Try
            Return nzn(mMETRES, 0)
        End Get
        Set(ByVal Value As Double)
            mMETRES = nzn(Value, 0)
            dvForm(pa).Row("METRES") = nzn(Value, 0) : GuardarDV()
        End Set
    End Property

    Private mKG As Double
    Public Property KG() As Double
        Get
            Try
                mKG = nzn(dvForm(pa).Row("KG"), 0)
            Catch ex As Exception : End Try
            Return nzn(mKG, 0)
        End Get
        Set(ByVal Value As Double)
            mKG = nzn(Value, 0)
            dvForm(pa).Row("KG") = nzn(Value, 0) : GuardarDV()
        End Set
    End Property

    Private mOBSERV As String
    Public Property OBSERV() As String
        Get
            Try
                mOBSERV = nz(dvForm(pa).Row("OBSERV"), "")
            Catch ex As Exception : End Try
            Return nz(mOBSERV, "")
        End Get
        Set(ByVal Value As String)
            mOBSERV = nz(Value, "")
            dvForm(pa).Row("OBSERV") = nz(Value, "") : GuardarDV()
        End Set
    End Property

#End Region

    Friend hilo As clsHilo

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, _
                ByVal conexion As MySqlConnection, _
                ByRef bindingcontext As BindingContext, ByVal hil As clsHilo)

        MyBase.New(tabla, centro, conexion, bindingcontext, "ESDETALLE")
        Try
            hilo = hil
            sqlSinWhere = " SELECT filcol.*, " & _
                            " filiales.DESCRI AS NOMCENTRO " & _
                            " FROM filcol " & _
                            " LEFT JOIN filiales ON (filiales.CODI = filcol.CENTRO) "
            sqlSel = sqlSinWhere & _
                        " WHERE filcol.TIPUS = ""F"" AND " & _
                        " filcol.FIL = """ & hilo.CODI & """ AND " & _
                        " filcol.PROVE = """ & hilo.PROVE & """ AND " & _
                        " filcol.CENTRO = """ & centro & """ ORDER BY filcol.color"

            cmdSel.CommandText = sqlSel
            'dvForm.Sort = "COLOR"
            da.SelectCommand = cmdSel
            da.Fill(tabla)

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Friend Sub CambioDetalle(ByVal centro As String, ByVal hil As clsHilo)
        Try
            hilo = hil
            sqlSel = sqlSinWhere & _
                       " WHERE filcol.TIPUS = ""F"" AND " & _
                       " filcol.FIL = """ & hilo.CODI & """ AND " & _
                       " filcol.PROVE = """ & hilo.PROVE & """ AND " & _
                       " filcol.CENTRO = """ & centro & """ ORDER BY filcol.color"

            cmdSel.CommandText = sqlSel
            'dvForm.Sort = "COLOR"
            da.SelectCommand = cmdSel
            tabla.Clear()
            da.Fill(tabla)

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub


    Public Overrides Sub borrar()
        Try
            'Este borra todas las lineas
            BorrarActualDVDetalle()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Friend Overrides Function genWhere(ByVal ID As Object, ByVal esCambio As Boolean) As String
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
        If id Is Nothing Then
            id = FIL
        End If

        Dim cmd As New MySqlCommand(" SELECT " & _
            " (SELECT COUNT(*) " & _
            " FROM " & tabla.TableName & " AS M2 WHERE " & _
            " M2.CODI <= M1.CODI) AS rownum FROM " & tabla.TableName & " AS M1 WHERE CODI = """ & id & """ AND " & WCNoTabla() & GenOrder(), cn)
        Try
            Dim idx As Integer = cmd.ExecuteScalar()


            Return idx - 1

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function
    Friend Overrides Function genWhereNumeroRegistros() As String
        Try
            Return genWhere(Nothing, False)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function

End Class
