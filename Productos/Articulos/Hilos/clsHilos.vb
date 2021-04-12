Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes

Public Class clsHilo

#Region "VARIABLES"

    Inherits clsADO
    Public dtProveConHilos As DataTable
    Public cartaColores As clsCartaColoresHilo
    Public mProveSeleccionado As Integer = -1
    Friend dtProve As New DataTable("PROVE")
    Friend dtValoracion As DataTable

#End Region

#Region "CAMPOS"

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
                dvForm(pa).Row("CODI") = general.nz(mCODI, "") : GuardarDV()
            End If
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
            If general.nz(Value, "") <> general.nz(DESCRI, "") Then
                mDESCRI = general.nz(Value, "")
                dvForm(pa).Row("DESCRI") = general.nz(mDESCRI, "") : GuardarDV()
            End If
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
            If nzn(Value, 0) <> nzn(PREU, 0) Then
                mPREU = nzn(Value, 0)
                dvForm(pa).Row("PREU") = nzn(mPREU, 0) : GuardarDV()
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
            If esCodigoExistente(dtPROVE, CCPROVE, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mPROVE = nzn(Value, 0)
                    dvForm(pa).Row("NOMPROVE") = general.OBN(Value, dtPROVE, CNPROVE)
                    dvForm(pa).Row(tablaProveedores) = nzn(Value, 0) : GuardarDV()
                End If
            Else
                dvForm(pa).Row("PROVE") = 0

                dvForm(pa).Row("NOMPROVE") = "" : GuardarDV()
                MessageBox.Show(rm.GetString("NOEXISTEPROVE"))
            End If
        End Set
    End Property

    Private mNOMPROVE As String
    Public Property NOMPROVE() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMPROVE = general.nz(dvForm(PA).Row("NOMPROVE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMPROVE, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMPROVE = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMPROVE") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMPROVE") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCOST As Double
    Public Property COST() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mCOST = nzn(dvForm(PA).Row("COST"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCOST, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(COST, 0) Then
                mCOST = nzn(Value, 0)
                dvForm(PA).Row("COST") = nzn(mCOST, 0) : guardarDV()
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

    Private mIVA As String
    Public Property IVA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mIVA = general.nz(dvForm(PA).Row("IVA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mIVA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(IVA, "") Then
                mIVA = general.nz(Value, "")
                dvForm(PA).Row("IVA") = general.nz(mIVA, "") : guardarDV()
            End If
        End Set
    End Property

#End Region

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext)

        MyBase.New(tabla, centro, bindingcontext, "CODI")
        Dim sqlSel As String
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

            CargaTabla(tablaProveedores, CCProve, CNProve, dtProve, True)
            CargarIdentificadores()
            IniciarDetalle()
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Sub

    Public Overrides Sub CargarIdentificadores()
        Dim a As New ArrayList
        Dim i As Integer
        Try
            a.Add(CCFil)
            a.Add(CNFil)
            a.Add("PROVE")
            CargaTabla(tablaHilos, a, dtIdentificadores, True)
            If Not dtIdentificadores.Columns.Contains("NOMPROVE") Then
                dtIdentificadores.Columns.Add("NOMPROVE")
            End If
            'Los strings que se vean correctamte en el cboid
            dtIdentificadores.Columns("NOMPROVE").Caption = rm.GetString("NOMPROVE")
            dtIdentificadores.Columns("CODI").Caption = rm.GetString("CODIGO")
            dtIdentificadores.Columns("DESCRI").Caption = rm.GetString("DESCRIPCION")

            'Le ponemos los nombres a los proveedores
            For i = 0 To dtIdentificadores.Rows.Count - 1
                dtIdentificadores.Rows(i).Item("NOMPROVE") = general.OBN(dtIdentificadores.Rows(i).Item("PROVE"), dtProve)
            Next

            dvIdentificadores.Sort = CCFil
            dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#Region "DESPLAZAMIENTO"

    Private Sub IniciarDetalle()
        Try
            cartaColores = New clsCartaColoresHilo(New aura2k3.ds11.filcolDataTable, centro, bc, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Sub
    Public Overrides Sub SiguienteReg()
        Try
            CargarRegistro(siguiente, ProveSeleccionado)
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Sub
    Public Overrides Sub AnteriorReg()
        Try
            CargarRegistro(anterior, ProveSeleccionado)
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Sub
    Public Overrides Sub UltimoReg()
        Try
            CargarRegistro(ultimo)
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Sub
    Public Overrides Sub PrimeroReg()
        Try
            CargarRegistro(primero)
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Sub
    'Le paso un object para conservar la compatibilidad con la anterior
    Public Overloads Sub CambiarAReg(ByVal dr As Object, ByVal accion As Integer)
        Try
            If accion = iraRegistroFila Then
                MyBase.CambiarAReg(dr, "SELECT * FROM FIL WHERE " & _
                    " FIL.CENTRO =  """ & dr("centro") & """ AND " & _
                    " CODI = """ & dr("CODI") & """  AND " & _
                    " PROVE = " & dr("PROVE"), accion)
            Else
                MyBase.CambiarAReg(dr, "", accion)
            End If
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ORGANIZAR"

    Private Sub CanviarCartaColores()
        Try
            If Not pa = -1 Then
                cartaColores.CambioDetalle(centro, Me)
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub NuevoRegistro()

        Dim drNew As DataRow
        Try
            ActualizarOrigen()
            tabla.Clear()
            drNew = tabla.NewRow()

            drNew.Item("CODI") = ""
            drNew.Item("CENTRO") = centro
            drNew.Item(tablaProveedores) = 0

            tabla.Rows.Add(drNew)
            numeroRegistros = numeroRegistros + 1
            Try
                guardarDV()
            Catch ex As Exception
            End Try
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            MyBase.ActualizarOrigen(True)
            cartaColores.ActualizarOrigen(True, True)
            MoverActual()

        Catch ex As Exception
            Throw ex
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub borrar()
        Try
            MyBase.borrar()
            cartaColores.borrar() : ActualizarOrigen()
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub MoverActual()
        Try
            CanviarCartaColores()
            PonerNombres()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub PonerNombres()
        Dim aceptarCambiosAlFinal As Boolean = False
        Try
            If tabla.GetChanges Is Nothing Then aceptarCambiosAlFinal = True
            Try : NOMPROVE = general.OBN(PROVE, dtProve) : Catch EX As Exception : End Try
            If aceptarCambiosAlFinal AndAlso (Not tabla.GetChanges Is Nothing) Then tabla.AcceptChanges()

        Catch ex As Exception
            LOG(ex.ToString, True) : cargando = False
        End Try
    End Sub
    Public Property ProveSeleccionado() As Integer
        Get
            Return mProveSeleccionado
        End Get
        Set(ByVal Value As Integer)
            mProveSeleccionado = Value
            numeroRegistros = obtenerNumeroReg(t, "")
            If CargarRegistro(primero, esCambioSeleccion) Then
                MoverActual()
                mProveSeleccionado = Value
            End If
            If Value = -1 Then
                dvIdentificadores.RowFilter = ""
            Else
                dvIdentificadores.RowFilter = "PROVE = '" & PROVE & "'"
            End If
            tabla.AcceptChanges()
        End Set
    End Property

#End Region

#Region "OVERRIDES"

    Friend Overrides Function TieneCambios() As Boolean
        Try
            guardarDV()
            If Not tabla.GetChanges Is Nothing OrElse cartaColores.TieneCambios Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
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
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function genWhere() As String
        Dim ret As String
        Try
            If HaySeleccion() Then
                ret = " WHERE  " & WC() & " AND " & t() & ".PROVE = """ & ProveSeleccionado & """ "
            Else
                ret = " WHERE " & WC() & " "
            End If

            Return ret

            'If Not HaySeleccion() Then
            'Else
            '    If esCambio Then
            '        ret = " WHERE " & WC() & " "
            '    Else
            '    End If
            'End If


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function GenOrder() As String
        Try
            Return " ORDER BY CODI, PROVE "

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        Dim idx As Object
        Dim cmd As MySqlCommand
        Dim esCambio As Boolean = False
        Try
            If id Is Nothing Then : id = CODI : Else
                If id.GetType Is GetType(Integer) Then
                    If id = esCambioSeleccion Then : esCambio = True : End If
                End If
            End If

            ACN()
            cmd = New MySqlCommand(" SELECT (SELECT COUNT(*) " & _
               " FROM " & t() & " AS M2 WHERE " & genWhereSubSelect(id, esCambio) & " " & _
               " M2.CODI < M1.CODI " & _
               genWhereSubSeleccion() & ") AS rownum FROM " & t() & " AS M1 " & _
               " " & GENWHERECONTODO(id, esCambio) & "  AND " & WCNoTabla() & GenOrder(), cnn)

            idx = cmd.ExecuteScalar()
            If idx Is Nothing Then Return -1
            Return idx

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function HaySeleccion() As Boolean
        Return Not (mProveSeleccionado = -1 OrElse mProveSeleccionado = 0)
    End Function
    Private Function GENWHERECONTODO(ByVal id As Object, ByVal esCambio As Boolean) As String
        Dim ret As String
        Try
            If esUnaDatarow(id) Then
                If Not HaySeleccion() Then
                    ret = " WHERE CODI = """ & id("CODI") & """ AND " & WCNoTabla() & " "
                Else
                    If id Is Nothing Or esCambio Then
                        ret = " WHERE  " & WCNoTabla() & " AND PROVE = """ & id("PROVE") & """ "
                    Else
                        ret = " WHERE  " & WCNoTabla() & " AND CODI = """ & id("CODI") & """ AND PROVE = """ & id("PROVE") & """ "
                    End If
                End If

                Return ret

            Else
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
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Function genWhereSubSelect(ByVal id As Object, ByVal esCambio As Boolean) As String
        Try
            If esUnaDatarow(id) Then
                If esCambio Then
                    Return " M1.PROVE = " & id("PROVE") & " AND "
                End If

                If HaySeleccion() Then
                    Return " PROVE = " & id("PROVE") & " AND "
                Else
                    Return " "
                End If
            Else
                If esCambio Then
                    Return " M1.PROVE = " & ProveSeleccionado & " AND "
                End If

                If HaySeleccion() Then
                    Return " PROVE = " & ProveSeleccionado & " AND "
                Else
                    Return " "
                End If
            End If


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
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

#Region "CAMBIO CENTRO"

    Public Overloads Function cambioCentro(ByVal centro As Char, ByVal iralregistro As Integer) As Boolean
        Try
            MyBase.cambioCentro(centro, iralregistro)
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function

#End Region

    Public Sub GenerarValoracionStock()
        Dim strSQL As String
        Dim cmdSelect As MySqlCommand

        strSQL = "SELECT FILCOL.FIL, " &
                        " FIL.DESCRI, " &
                        " FIL.PROVE, " &
                        " PROVE.NOM AS NOMPROVE, " &
                        " FIL.COST, " &
                        " SUM(ACTUAL) AS STOCKTOTAL, " &
                        " round(SUM(ACTUAL) * FIL.COST, 2) AS VALORACION " &
                        " FROM FILCOL " &
                        " LEFT JOIN FIL ON FIL.CENTRO = FILCOL.CENTRO AND " &
                        " FIL.PROVE = FILCOL.PROVE AND FIL.CODI = FILCOL.FIL " &
                        " LEFT JOIN PROVE ON FIL.PROVE = PROVE.CODI " &
                        " WHERE TIPUS = ""F"" AND FIL.CENTRO = """ & centro & """ GROUP BY FIL, PROVE"
        cmdSelect = New MySqlCommand(strSQL, cnn)
        ACN()
        Dim da As New MySqlDataAdapter(cmdSelect)

        dtValoracion = New DataTable("dtValoracion")
        'dtValoracion.Fill()
        da.Fill(dtValoracion)

        CCN()
    End Sub

End Class
