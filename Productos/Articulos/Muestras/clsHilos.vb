Imports CoreLab.MySql

Public Class clsHilo
    Inherits clsADO
    Public dtProveConHilos As DataTable
    Public cartaColores As clsCartaColoresHilo
    Public mProveSeleccionado As Integer = -1

#Region "---CAMPOS"

    Private mCODI As String
    Public Property CODI() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCODI = nz(dvForm(pa).Row("CODI"), "")
            Catch ex As Exception : End Try
            Return nz(mCODI, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mCODI = nz(Value, "")
            dvForm(pa).Row("CODI") = nz(Value, "") : GuardarDV()
        End Set
    End Property

    Private mDESCRI As String
    Public Property DESCRI() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mDESCRI = nz(dvForm(pa).Row("DESCRI"), "")
            Catch ex As Exception : End Try
            Return nz(mDESCRI, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mDESCRI = nz(Value, "")
            dvForm(pa).Row("DESCRI") = nz(Value, "") : GuardarDV()
        End Set
    End Property

    Private mPREU As Double
    Public Property PREU() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mPREU = nzn(dvForm(pa).Row("PREU"), 0)
            Catch ex As Exception : End Try
            Return nzn(mPREU, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            mPREU = nzn(Value, 0)
            dvForm(pa).Row("PREU") = nzn(Value, 0) : GuardarDV()
        End Set
    End Property

    Private mPROVE As Integer
    Public Property PROVE() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mPROVE = nzn(dvForm(pa).Row("PROVE"), 0)
            Catch ex As Exception : End Try
            Return nzn(mPROVE, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
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
            If PA = -1 Then Exit Property
            Try
                mNOMPROVE = nz(dvForm(pa).Row("NOMPROVE"), "")
            Catch ex As Exception : End Try
            Return nz(mNOMPROVE, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mNOMPROVE = nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(pa).Row("NOMPROVE") = nz(Value, "") : GuardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(pa).Row("NOMPROVE") = nz(Value, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mCOST As Double
    Public Property COST() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mCOST = nzn(dvForm(pa).Row("COST"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCOST, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            mCOST = nzn(Value, 0)
            dvForm(pa).Row("COST") = nzn(Value, 0) : GuardarDV()
        End Set
    End Property

    Private mOBSERV As String
    Public Property OBSERV() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mOBSERV = nz(dvForm(pa).Row("OBSERV"), "")
            Catch ex As Exception : End Try
            Return nz(mOBSERV, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mOBSERV = nz(Value, "")
            dvForm(pa).Row("OBSERV") = nz(Value, "") : GuardarDV()
        End Set
    End Property

    Private mIVA As String
    Public Property IVA() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mIVA = nz(dvForm(pa).Row("IVA"), "")
            Catch ex As Exception : End Try
            Return nz(mIVA, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mIVA = nz(Value, "")
            dvForm(pa).Row("IVA") = nz(Value, "") : GuardarDV()
        End Set
    End Property
#End Region

    
    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, _
                ByVal conexion As MySqlConnection, _
                    ByRef bindingcontext As BindingContext)

        MyBase.New(tabla, centro, conexion, bindingcontext, "CODI")
        Try

            sqlSinWhere = "SELECT fil.*, " & _
                    " filiales.DESCRI AS NOMCENTRO " & _
                    " FROM fil " & _
                    " LEFT JOIN filiales ON (filiales.CODI = fil.CENTRO) "

            sqlSel = sqlSinWhere & _
                        " WHERE fil.CENTRO = """ & centro & """ " & GenOrder() & "" & _
                        " LIMIT 1"

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            dvForm.Sort = "CODI, PROVE"

            CargaTabla(tablaProveedores, CCProve, CNProve, dtProve, True)
            IniciarDetalle()
            MoverActual()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

#Region "---DESPLAZAMIENTO---"
    Private Sub IniciarDetalle()
        Try
            cartaColores = New clsCartaColoresHilo(New aura2k3.ds11.filcolDataTable, centro, cn, bc, Me)

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Public Overrides Sub SiguienteReg()
        Try
            CargarRegistro(siguiente, ProveSeleccionado)
            MoverActual()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Public Overrides Sub AnteriorReg()
        Try
            CargarRegistro(anterior, ProveSeleccionado)
            MoverActual()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Public Overrides Sub UltimoReg()
        Try

            CargarRegistro(ultimo)

            MoverActual()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Public Overrides Sub PrimeroReg()
        Try
            CargarRegistro(primero)
            MoverActual()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Public Overloads Sub CambiarAReg(ByVal id As String, ByVal accion As Integer)
        Dim sql As String
        Try
            sql = "SELECT CODI FROM fil WHERE (CENTRO = """ & centro & """ AND NOM = """ & id & """)"

            MyBase.CambiarAReg(id, sql, accion)
            MoverActual()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub


#End Region

    Private Sub CanviarCartaColores()
        Try
            If Not pa = -1 Then
                cartaColores.CambioDetalle(centro, Me)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString) : cn.Close()
        End Try
    End Sub

    Public Overrides Sub NuevoRegistro()

        Dim drNew As DataRow
        Try
            tabla.Clear()
            drNew = tabla.NewRow()

            drNew.Item("CODI") = ""
            drNew.Item("CENTRO") = centro
            drNew.Item("PROVE") = 0

            tabla.Rows.Add(drNew)
            numeroRegistros = numeroRegistros + 1
            Try
                guardarDV()
            Catch ex As Exception
            End Try
            MoverActual()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            MyBase.ActualizarOrigen()
            'MoverActual()
            cartaColores.ActualizarOrigen()
        Catch ex As Exception
            Throw ex
            'MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Public Overloads Sub borrar()
        Try
            MyBase.borrar()
            cartaColores.borrar()
            MoverActual()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub MoverActual()
        Try
            PonerNombres()
            CanviarCartaColores()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub PonerNombres()

        NOMPROVE = OBN(PROVE, dtProve)

    End Sub
    Public Property ProveSeleccionado() As Integer
        Get
            Return mProveSeleccionado
        End Get
        Set(ByVal Value As Integer)
            mProveSeleccionado = Value
            numeroRegistros = obtenerNumeroReg(t, "")
            If CargarRegistro(primero, -2) Then
                MoverActual()
                mProveSeleccionado = Value
            End If

            tabla.AcceptChanges()
        End Set
    End Property

#Region "----OVERRIDES----"

    Friend Overrides Function genWhereNumeroRegistros() As String
        Dim ret As String
        Try
            If Not HaySeleccion() Then
                ret = " where " & WCNoTabla()
            Else
                ret = " where  " & WCNoTabla() & " and PROVE = """ & ProveSeleccionado & """ "
            End If

            Return ret
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function
    Friend Overrides Function genWhere(ByVal ID As Object, ByVal esCambio As Boolean) As String
        Dim ret As String
        Try

            If Not HaySeleccion() Then
                ret = " WHERE " & WC() & " "
            Else
                If esCambio Then
                    ret = " WHERE " & WC() & " "
                Else
                    ret = " WHERE  " & WC() & " AND " & t() & ".PROVE = """ & ProveSeleccionado & """ "
                End If

            End If

            Return ret
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function
    Friend Overrides Function GenOrder() As String
        Try
            Return " ORDER BY CODI, PROVE "

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        Dim idx As Integer
        Dim cmd As MySqlCommand
        Dim esCambio As Boolean = False
        Try
            If id Is Nothing Then : id = CODI : Else
                If id.GetType Is GetType(Integer) Then
                    If id = -2 Then : esCambio = True : End If
                End If
            End If

            ACN()
            cmd = New MySqlCommand(" SELECT (SELECT COUNT(*) " & _
               " FROM " & t() & " AS M2 WHERE " & genWhereSubSelect(id, esCambio) & " " & _
               " M2.CODI < M1.CODI " & _
               genWhereSubSeleccion() & ") AS rownum FROM " & t() & " AS M1 " & _
               " " & GENWHERECONTODO(id, esCambio) & "  AND " & WCNoTabla() & GenOrder(), cn)
            idx = cmd.ExecuteScalar()
            Return idx

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function
    Private Function HaySeleccion() As Boolean
        Return Not (mProveSeleccionado = -1 OrElse mProveSeleccionado = 0)
    End Function
    Private Function GENWHERECONTODO(ByVal id As Object, ByVal esCambio As Boolean) As String
        Dim ret As String
        Try
            If Not HaySeleccion() Then
                ret = " WHERE CODI = """ & id & """ AND " & WCNoTabla() & " "
            Else
                If id Is Nothing Or esCambio Then
                    ret = " WHERE  " & WCNoTabla() & " AND PROVE = """ & ProveSeleccionado & """ "
                Else
                    ret = " WHERE  " & WCNoTabla() & " AND CODI = """ & id & """ AND PROVE = """ & ProveSeleccionado & """ "
                End If
            End If

            Return ret
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function
    Friend Function genWhereSubSelect(ByVal id As Object, ByVal esCambio As Boolean) As String
        Try
            If esCambio Then
                Return " M1.PROVE = " & ProveSeleccionado & " AND "
            End If

            If HaySeleccion() Then
                Return " PROVE = " & ProveSeleccionado & " AND "
            Else
                Return " "
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function
    Friend Function genWhereSubSeleccion()
        If HaySeleccion() Then
            Return ""
        Else
            Return " OR M2.CODI = M1.CODI And M2.PROVE < M1.PROVE "

        End If
    End Function

#End Region





End Class
