Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1
Imports clsConstantes
Imports clsFuncionesUtiles
Public Class clsMuestra
    Inherits clsADO

#Region "VARIABLES"

    Public cartaColores As clsCartaColoresMuestra
    Public mClienteSeleccionado As Integer = -1
    Public dtClienteConMuestras As DataTable
    Friend dtClients As New DataTable("CLIENTS")
    Friend dtMaqui As New DataTable("MAQ")

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
                dvForm(pa).Row("CODI") = general.nz(mCODI, "") : guardarDV()
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
                dvForm(pa).Row("DESCRI") = general.nz(mDESCRI, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCLIENT As Integer
    Public Property CLIENT() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mCLIENT = nzn(dvForm(pa).Row("CLIENT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCLIENT, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If esCodigoExistente(dtClients, CCClients, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mCLIENT = nzn(Value, 0)
                    dvForm(pa).Row("NOMCLIENT") = general.OBN(Value, dtClients, CNClients)
                    dvForm(pa).Row("CLIENT") = nzn(Value, 0) : guardarDV()
                End If
            Else
                dvForm(pa).Row("CLIENT") = 0

                dvForm(pa).Row("NOMCLIENT") = "" : guardarDV()
                MessageBox.Show(RM.GetString("NOEXISTECLIENTS"))
            End If
        End Set
    End Property

    Private mNOMCLIENT As String
    Public Property NOMCLIENT() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mNOMCLIENT = general.nz(dvForm(pa).Row("NOMCLIENT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMCLIENT, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mNOMCLIENT = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(pa).Row("NOMCLIENT") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(pa).Row("NOMCLIENT") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mREFE As String
    Public Property REFE() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mREFE = general.nz(dvForm(pa).Row("REFE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mREFE, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(REFE, "") Then
                mREFE = general.nz(Value, "")
                dvForm(pa).Row("REFE") = general.nz(mREFE, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTEMP As String
    Public Property TEMP() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mTEMP = general.nz(dvForm(pa).Row("TEMP"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTEMP, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TEMP, "") Then
                mTEMP = general.nz(Value, "")
                dvForm(pa).Row("TEMP") = general.nz(mTEMP, "") : guardarDV()
            End If
        End Set
    End Property

    Private mMAQUINA As Double
    Public Property MAQUINA() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mMAQUINA = nzn(dvForm(pa).Row("MAQUINA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mMAQUINA, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If esCodigoExistente(dtMaqui, CCMaqui, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mMAQUINA = nzn(Value, 0)
                    dvForm(pa).Row("NOMMAQUI") = general.OBN(Value, dtMaqui, CNMaqui)
                    dvForm(PA).Row("MAQUINA") = nzn(Value, 0) : guardarDV()
                Else
                    dvForm(PA).Row("MAQUINA") = DBNull.Value

                    dvForm(PA).Row("NOMMAQUI") = "" : guardarDV()

                End If
            Else
                dvForm(pa).Row("MAQUINA") = DBNull.Value

                dvForm(pa).Row("NOMMAQUI") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTEMAQUI"))
            End If
        End Set
    End Property

    Private mNOMMAQUI As String
    Public Property NOMMAQUI() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mNOMMAQUI = general.nz(dvForm(pa).Row("NOMMAQUI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMMAQUI, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mNOMMAQUI = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(pa).Row("NOMMAQUI") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(pa).Row("NOMMAQUI") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mMARGE As Double
    Public Property MARGE() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mMARGE = nzn(dvForm(pa).Row("MARGE"), 0)
            Catch ex As Exception : End Try
            Return nzn(mMARGE, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(MARGE, 0) Then
                mMARGE = nzn(Value, 0)
                dvForm(pa).Row("MARGE") = nzn(mMARGE, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mIVA As String
    Public Property IVA() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mIVA = general.nz(dvForm(pa).Row("IVA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mIVA, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(IVA, "") Then
                mIVA = general.nz(Value, "")
                dvForm(pa).Row("IVA") = general.nz(mIVA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mOBSERV As String
    Public Property OBSERV() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mOBSERV = general.nz(dvForm(pa).Row("OBSERV"), "")
            Catch ex As Exception : End Try
            Return general.nz(mOBSERV, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(OBSERV, "") Then
                mOBSERV = general.nz(Value, "")
                dvForm(pa).Row("OBSERV") = general.nz(mOBSERV, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCOMPO As String
    Public Property COMPO() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCOMPO = general.nz(dvForm(pa).Row("COMPO"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCOMPO, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(COMPO, "") Then
                mCOMPO = general.nz(Value, "")
                dvForm(pa).Row("COMPO") = general.nz(mCOMPO, "") : guardarDV()
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
                dvForm(pa).Row("PREU") = nzn(mPREU, 0) : guardarDV()
            End If
        End Set
    End Property

#End Region

#Region "INICIALIZAR"

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext)

        MyBase.New(tabla, centro, bindingcontext, "CODI")
        Dim sqlSel As String
        Try
            sqlSinWhere = "SELECT * FROM MOSTRES "

            sqlSel = sqlSinWhere &
                        " WHERE MOSTRES.CENTRO = """ & centro & """ " & GenOrder() & " " '&
            '" LIMIT 1"

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel

            da.Fill(tabla)

            CargaTabla(tablaClientes, "CODI", "NOM", dtClients, True)
            CargaTabla(tablaMaquinas, "CODI", "DESCRI", dtMaqui, True)
            CargarIdentificadores()
            IniciarDetalle()
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub CargarIdentificadores()
        Dim a As New ArrayList
        Dim i As Integer
        Try
            a.Add(CCMuestra)
            a.Add(CNMuestra)
            a.Add("CLIENT")
            CargaTabla(tablaMuestras, a, dtIdentificadores, True)

            If Not dtIdentificadores.Columns.Contains("NOMCLIENT") Then
                dtIdentificadores.Columns.Add("NOMCLIENT")
            End If

            'Los strings que se vean correctamte en el cboid
            dtIdentificadores.Columns("NOMCLIENT").Caption = rm.GetString("NOMCLIENT")
            dtIdentificadores.Columns("CODI").Caption = rm.GetString("CODIGO")
            dtIdentificadores.Columns("DESCRI").Caption = rm.GetString("DESCRIPCION")

            'Le ponemos los nombres a los proveedores
            For i = 0 To dtIdentificadores.Rows.Count - 1
                dtIdentificadores.Rows(i).Item("NOMCLIENT") = general.OBN(dtIdentificadores.Rows(i).Item("CLIENT"), dtClients)
            Next

            'Orden de los identificadores
            dvIdentificadores.Sort = CCMuestra
            dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Friend Overrides Sub PonerNombres()
        Dim aceptarCambiosAlFinal As Boolean = False
        Try
            If tabla.GetChanges Is Nothing Then aceptarCambiosAlFinal = True
            NOMCLIENT = general.OBN(CLIENT, dtClients)
            NOMMAQUI = general.OBN(MAQUINA, dtMaqui, "DESCRI")
            If aceptarCambiosAlFinal AndAlso (Not tabla.GetChanges Is Nothing) Then tabla.AcceptChanges()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub
    Private Sub IniciarDetalle()
        Try
            cartaColores = New clsCartaColoresMuestra(New aura2k3.ds11.tallaDataTable, centro, bc, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "DESPLAZAMIENTO"

    Public Overrides Sub SiguienteReg()
        Try
            CargarRegistro(siguiente, ClienteSeleccionado)
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub AnteriorReg()
        Try
            CargarRegistro(anterior, ClienteSeleccionado)
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub UltimoReg()
        Try
            CargarRegistro(ultimo)
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub PrimeroReg()
        Try
            CargarRegistro(primero)
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub CambiarAReg(ByVal dr As Object, ByVal accion As Object)
        Dim SQL As String
        Try
            If accion = iraRegistroFila Then
                MyBase.CambiarAReg(dr, _
                    "SELECT * FROM MOSTRES WHERE " & _
                    "MOSTRES.CENTRO =  """ & dr("centro") & """ AND " & _
                    "CODI = """ & dr("codi") & """  AND " & _
                    "CLIENT = " & dr("CLIENT"), iraRegistroFila)
            Else
                MyBase.CambiarAReg(dr, "", accion)
            End If
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "MODIFICAR ORIGEN"

    Public Overrides Sub NuevoRegistro()
        Dim drNew As DataRow

        Try
            ActualizarOrigen()
            tabla.Clear()
            drNew = tabla.NewRow()

            drNew.Item("CODI") = "MOSTRA"
            drNew.Item("CLIENT") = 0
            drNew.Item("CENTRO") = centro
            tabla.Rows.Add(drNew)
            CopiarDetalleANuevaMuestra(drNew.Item("CODI"))

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
    Private Sub CopiarDetalleANuevaMuestra(ByVal codigo As String)
        Dim dr As DataRow
        Dim i As Integer
        Try

            For i = 0 To cartaColores.dvForm.Count - 1
                dr = cartaColores.tabla.NewRow
                CopiarFila(cartaColores.dvForm(i).Row, dr)
                dr("MOSTRA") = "MOSTRA"
                dr("CLIENT") = 0
                dr("centro") = centro
                dr("Tallah") = 0
                dr("Tallal") = 0
                cartaColores.tabla.Rows.Add(dr)
            Next

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            MyBase.ActualizarOrigen(True)
            cartaColores.ActualizarOrigen(True, True)
            MoverActual()

        Catch ex As Exception
            If ex.ToString.Substring(0, 9) = "Duplicate" Then
                'La única posibilidad de que este dupliacado es que alguien haya insertado otro antes
                Try
                    MessageBox.Show("Ja existeix una mostra amb aquest codi per al client " & NOMCLIENT, rm.GetString("INFORMACION"))
                Catch ex2 As Exception
                    Throw ex
                End Try
            End If
            Throw ex
            LOG(ex.ToString)
            cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub borrar()
        Try
            BorrarDesgloseMaquina()
            cartaColores.borrar() : ActualizarOrigen()
            BorrarDesgloseColores()
            MyBase.borrar()
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub BorrarDesgloseMaquina()
        Dim cmdDel As New MySqlCommand("DELETE FROM " & tablaMaq & " WHERE " & _
                    " (mostra = """ & CODI & """  AND " & _
                    " client = """ & CLIENT & """)", cnn)

        Try
            ACN()
            cmdDel.ExecuteNonQuery()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub BorrarDesgloseColores()
        Dim cmdDel As New MySqlCommand("DELETE FROM " & tablaColores & " WHERE " & _
                    " (mostra = """ & CODI & """ AND " & _
                    " client = """ & CLIENT & """) ", cnn)
        Try
            ACN()
            cmdDel.ExecuteNonQuery()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "DESPLAZAMIENTO"

    Private Sub MoverActual()
        Try
            If Not PA() = -1 Then cartaColores.CambioDetalle(centro, Me)
            PonerNombres()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "SELECCION REGISTRO"

    Public Property ClienteSeleccionado() As Integer
        Get
            Return mClienteSeleccionado
        End Get
        Set(ByVal Value As Integer)
            mClienteSeleccionado = Value
            numeroRegistros = obtenerNumeroReg(t, "")
            If numeroRegistros = 0 Then
                Exit Property
            End If
            'ActualizarOrigen(True)
            If CargarRegistro(primero, esCambioSeleccion) Then
                MoverActual()
                mClienteSeleccionado = Value
            End If
            If Value = -1 Then
                dvIdentificadores.RowFilter = ""
            Else
                dvIdentificadores.RowFilter = "CLIENT = '" & CLIENT & "'"
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
                ret = " where  " & WCNoTabla() & " and CLIENT = """ & ClienteSeleccionado & """ "
            End If

            Return ret
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function genWhere() As String
        Dim ret As String
        Try
            If Not HaySeleccion() Then
                ret = " WHERE " & WC() & " "
            Else
                ' If esCambio Then
                ' ret = " WHERE " & WC() & " "
                ' Else
                ret = " WHERE  " & WC() & " AND " & t() & ".CLIENT = """ & ClienteSeleccionado & """ "
                'End If
            End If
            Return ret

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function GenOrder() As String
        Try
            Return " ORDER BY CLIENT, CODI "

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function GENWHERECONTODO(ByVal id As Object, ByVal esCambio As Boolean) As String
        Dim ret As String
        Try
            If esUnaDatarow(id) Then
                If Not HaySeleccion() Then
                    ret = " WHERE CODI = """ & id("CODI") & """ AND CLIENT = " & id("CLIENT") & " AND " & WCNoTabla() & " "
                Else
                    If id Is Nothing Or esCambio Then
                        ret = " WHERE  " & WCNoTabla() & " AND CLIENT = """ & id("CLIENT") & """ "
                    Else
                        ret = " WHERE  " & WCNoTabla() & " AND CODI = """ & id("CODI") & """ AND CLIENT = """ & id("CLIENT") & """ "
                    End If
                End If
                Return ret
            Else
                'lo de siempre
                If Not HaySeleccion() Then
                    ret = " WHERE CODI = """ & id & """ AND CLIENT = " & CLIENT & " AND " & WCNoTabla() & " "
                Else
                    If id Is Nothing Or esCambio Then
                        ret = " WHERE  " & WCNoTabla() & " AND CLIENT = """ & ClienteSeleccionado & """ "
                    Else
                        ret = " WHERE  " & WCNoTabla() & " AND CODI = """ & id & """ AND CLIENT = """ & ClienteSeleccionado & """ "
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
                    Return " M1.CLIENT = " & id("CLIENT") & " AND "
                End If

                If HaySeleccion() Then
                    Return " CLIENT = " & id("CLIENT") & " AND "
                Else
                    Return " "
                End If
            Else
                If esCambio Then
                    Return " M1.CLIENT = " & ClienteSeleccionado & " AND "
                End If

                If HaySeleccion() Then
                    Return " CLIENT = " & ClienteSeleccionado & " AND "
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
            Return " OR M2.CLIENT = M1.CLIENT And M2.CODI < M1.CODI "
        Else
            Return " OR M2.CLIENT = M1.CLIENT And M2.CODI < M1.CODI "
        End If
    End Function
    Private Function HaySeleccion() As Boolean
        Return Not (mClienteSeleccionado = -1 OrElse mClienteSeleccionado = 0)
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        Dim idx As Object
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
              " FROM " & t() & " AS M2 WHERE  " & genWhereSubSelect(id, esCambio) & " " & _
              " M2.CLIENT < M1.CLIENT " & _
              genWhereSubSeleccion() & ") AS rownum FROM " & t() & " AS M1 " & _
              " " & GENWHERECONTODO(id, esCambio) & "  AND " & WCNoTabla() & GenOrder(), cnn)

            idx = cmd.ExecuteScalar()
            If idx Is Nothing Then Return -1
            Return idx

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Public Sub cambioMargen()
        Try
            guardarDV()
            cartaColores.VENDA = roundnum(100 * cartaColores.COST / (100 - MARGE), 2)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#End Region

End Class
