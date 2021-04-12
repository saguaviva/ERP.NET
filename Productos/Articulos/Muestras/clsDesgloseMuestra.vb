Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Imports Excel = Microsoft.Office.Interop.Excel

Public Class clsDesgloseMuestra
    Inherits clsADO

#Region "VARIABLES"

    Public dvHilosElegibles As New DataView
    'Public dvColores As New DataView
    Public dvProveedores As New DataView
    Public dvHilos As New DataView

    Public m_Muestra As clsMuestra

    Public m_Maquina As clsMaquinasMuestra
    Public dtColores As New DataTable

    Friend dtProve As New DataTable("PROVE")
    Friend dtClients As New DataTable("CLIENTS")
    Friend dtTeixits As New DataTable("TEIXITS")

#End Region

#Region "CAMPOS"

    Private mMOSTRA As String
    Public Property MOSTRA() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mMOSTRA = general.nz(dvForm(pa).Row("MOSTRA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mMOSTRA, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(MOSTRA, "") Then
                mMOSTRA = general.nz(Value, "")
                dvForm(pa).Row("MOSTRA") = general.nz(mMOSTRA, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mGRADUACION As Integer
    Public Property GRADUACION() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mGRADUACION = nzn(dvForm(pa).Row("GRADUACION"), 0)
            Catch ex As Exception : End Try
            Return nzn(mGRADUACION, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(GRADUACION, 0) Then
                mGRADUACION = nzn(Value, 0)
                dvForm(pa).Row("GRADUACION") = nzn(mGRADUACION, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mTALLA As String
    Public Property TALLA() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mTALLA = general.nz(dvForm(pa).Row("TALLA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLA, "") Then
                mTALLA = general.nz(Value, "")
                dvForm(pa).Row("TALLA") = general.nz(mTALLA, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mLINEA As Integer
    Public Property LINEA() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mLINEA = nzn(dvForm(pa).Row("LINEA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mLINEA, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(LINEA, 0) Then
                mLINEA = nzn(Value, 0)
                dvForm(pa).Row("LINEA") = nzn(mLINEA, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mCOLOR As String
    Public Property COLOR() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCOLOR = general.nz(dvForm(pa).Row("COLOR"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCOLOR, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(COLOR, "") Then
                mCOLOR = general.nz(Value, "")
                dvForm(pa).Row("COLOR") = general.nz(mCOLOR, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mFIL As Double
    Public Property FIL() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mFIL = nzn(dvForm(pa).Row("FIL"), 0)
            Catch ex As Exception : End Try
            Return nzn(mFIL, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(FIL, 0) Then
                mFIL = nzn(Value, 0)
                dvForm(pa).Row("FIL") = nzn(mFIL, 0) : GuardarDV()
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
                dvForm(pa).Row("TEIXIT") = general.nz(mTEIXIT, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mPROVE As Double
    Public Property PROVE() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mPROVE = nzn(dvForm(pa).Row(tablaProveedores), 0)
            Catch ex As Exception : End Try
            Return nzn(mPROVE, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If esCodigoExistente(dtProve, CCProve, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mPROVE = nzn(Value, 0)
                    dvForm(pa).Row("NOMPROVE") = general.OBN(Value, dtProve, CNProve)
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

    Private mCAPS As Double
    Public Property CAPS() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mCAPS = nzn(dvForm(pa).Row("CAPS"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCAPS, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(CAPS, 0) Then
                mCAPS = nzn(Value, 0)
                dvForm(pa).Row("CAPS") = nzn(mCAPS, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mPASSADES As Double
    Public Property PASSADES() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mPASSADES = nzn(dvForm(pa).Row("PASSADES"), 0)
            Catch ex As Exception : End Try
            Return nzn(mPASSADES, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(PASSADES, 0) Then
                mPASSADES = nzn(Value, 0)
                dvForm(pa).Row("PASSADES") = nzn(mPASSADES, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mCONSUM As Double
    Public Property CONSUM() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mCONSUM = nzn(dvForm(pa).Row("CONSUM"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCONSUM, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(CONSUM, 0) Then
                mCONSUM = nzn(Value, 0)
                dvForm(pa).Row("CONSUM") = nzn(mCONSUM, 0) : guardarDV()
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

    Private mIMPORT As Double
    Public Property IMPORT() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mIMPORT = nzn(dvForm(pa).Row("IMPORT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mIMPORT, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(IMPORT, 0) Then
                mIMPORT = nzn(Value, 0)
                dvForm(pa).Row("IMPORT") = nzn(mIMPORT, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mCOLORM As String
    Public Property COLORM() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCOLORM = general.nz(dvForm(pa).Row("COLORM"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCOLORM, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(COLORM, "") Then
                mCOLORM = general.nz(Value, "")
                dvForm(pa).Row("COLORM") = general.nz(mCOLORM, "") : guardarDV()
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
                MessageBox.Show(rm.GetString("NOEXISTECLIENTS"))
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

#End Region

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext, ByVal muestra As clsMuestra, _
                ByVal maqui As clsMaquinasMuestra)

        MyBase.New(tabla, centro, bindingcontext, "CODI")
        Dim sqlSel As String
        Try
            m_Muestra = muestra
            m_Maquina = maqui

            sqlSinWhere = "SELECT color.*, prove.NOM AS NOMPROVE , filiales.DESCRI AS NOMCENTRO " & _
                            " FROM " & tablaColores & " " & _
                            " LEFT JOIN prove ON (prove.CODI = color.PROVE) " & _
                            " LEFT JOIN filiales ON (filiales.CODI = color.CENTRO) "

            sqlSel = sqlSinWhere & " WHERE (" & _
                            " CLIENT = " & m_Muestra.CLIENT & " AND " & _
                            " MOSTRA = """ & m_Muestra.CODI & """ AND " & _
                            " TALLA = """ & m_Muestra.cartaColores.TALLA & """ AND " & _
                            " COLORM = """ & m_Muestra.cartaColores.COLOR & """) " & _
                            " ORDER BY color"

            tabla.Columns.Add("ELEGIRHILO")

            ACN()
            tabla.AcceptChanges()

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            dvForm = tabla.DefaultView
            dvForm.Sort = "LINEA ASC"

            PonerDefaults()
            IniciarTablas()
            CargaTabla(tablaProveedores, CCProve, CNProve, dtProve, True)
            CargaTabla(tablaClientes, CCClients, CNClients, dtClients, True)

            DesperatarHandlers()
            AddHandler tabla.RowChanged, AddressOf RowChanged

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#Region "ORGANIZAR"

    Private Sub RowChanged(ByVal sender As Object, ByVal e As System.data.DataRowChangeEventArgs)
        Try
            If LINEA = 10000 Then
                Try
                    If dvForm(dvForm.Count - 2).Item("LINEA") = 10000 Then
                        LINEA = 1
                    Else
                        LINEA = dvForm(dvForm.Count - 2).Item("LINEA") + 1
                    End If

                Catch ex As Exception
                    LINEA = 1
                End Try
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub IniciarTablas()
        Try
            IniciarDesplegableArticulo()
            iniciartablaProveedores()
            IniciarTablaColores()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub IniciarTablaColores()
        Dim dc1 As New DataColumn("COLOR")
        dc1.Caption = rm.GetString("COLOR")
        Dim dc2 As New DataColumn("COST")
        dc2.Caption = rm.GetString("COSTE")
        Try
            dtColores.Columns.Add(dc1)
            dtColores.Columns.Add(dc2)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub iniciartablaProveedores()
        Dim dt As New DataTable(tablaProveedores)
        Dim DC As New DataColumn("PROVE", GetType(Integer))
        DC.Caption = rm.GetString("PROVEEDOR")
        Dim DC2 As New DataColumn("NOMPROVE", GetType(String))
        DC2.Caption = rm.GetString("NOMPROVE")
        Try
            dt.Columns.Add(DC)
            dt.Columns.Add(DC2)
            dvProveedores = dt.DefaultView
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub ponerProveedores()
        Dim dr() As DataRow
        Dim dr1 As DataRow
        Try
            dr = dvHilosElegibles.Table.Select("CODI = '" & TEIXIT & "'")
            dvProveedores.Table.Rows.Clear()
            For Each dr1 In dr
                Dim d As DataRow
                d = dvProveedores.Table.NewRow
                d(tablaProveedores) = dr1(tablaProveedores)
                d("NOMPROVE") = dr1("NOMPROVE")
                dvProveedores.Table.Rows.Add(d)
            Next

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Private Sub DesperatarHandlers()
        Try
            AddHandler tabla.ColumnChanged, AddressOf CanvioColumna
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub DormirHandlers()
        Try
            RemoveHandler tabla.ColumnChanged, AddressOf CanvioColumna
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerDefaults()
        Try
            With dvForm

                .Table.Columns("TEIXIT").DefaultValue = ""
                .Table.Columns("ELEGIRHILO").DefaultValue = ""

                .Table.Columns("COLOR").DefaultValue = ""
                .Table.Columns("FIL").DefaultValue = 0
                .Table.Columns("GRADUACION").DefaultValue = 0
                .Table.Columns("CAPS").DefaultValue = 0
                .Table.Columns("PASSADES").DefaultValue = 0
                .Table.Columns("CONSUM").DefaultValue = 0
                .Table.Columns("PREU").DefaultValue = 0

                'Esto identifica una tupla de la tabla color
                .Table.Columns(tablaProveedores).DefaultValue = 0
                .Table.Columns("MOSTRA").DefaultValue = m_Muestra.CODI
                .Table.Columns("CLIENT").DefaultValue = m_Muestra.CLIENT
                .Table.Columns("CENTRO").DefaultValue = m_Muestra.centro
                .Table.Columns("TALLA").DefaultValue = m_Muestra.cartaColores.TALLA
                .Table.Columns("COLORM").DefaultValue = m_Muestra.cartaColores.COLOR
                'Hack mu feo
                .Table.Columns("LINEA").DefaultValue = 10000

            End With

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub IniciarDesplegableArticulo()

        Dim i As Integer
        Dim cmdSelect As New MySqlCommand
        Dim strSQL As String
        Dim dr As DataRow
        Dim dc As New DataColumn("CODI")
        Dim dc2 As New DataColumn("DESCRI")
        Dim dc3 As New DataColumn(tablaProveedores)
        Dim dc4 As New DataColumn("NOMPROVE")
        Dim daReader As MySqlDataReader

        Try
            dc.Caption = rm.GetString("CODIGO")
            dc2.Caption = rm.GetString("DESCRIPCION")
            dc3.Caption = rm.GetString("PROVEEDOR")
            dc4.Caption = rm.GetString("NOMPROVE")

            dc.DefaultValue = ""
            dc2.DefaultValue = ""
            dc3.DefaultValue = 0
            dc4.DefaultValue = ""

            cmdSelect.Connection = cnn
            cmdSelect.CommandText = "SELECT fil.CODI, fil.DESCRI, " & _
                " fil.PROVE, prove.NOM AS NOMPROVE FROM fil LEFT JOIN prove ON (prove.CODI = fil.PROVE)  " & _
                " ORDER BY CODI"
            ACN()

            daReader = cmdSelect.ExecuteReader

            dtTeixits.Clear()
            dtTeixits.Columns.Clear()
            dtTeixits.Columns.Add(dc)
            dtTeixits.Columns.Add(dc2)
            dtTeixits.Columns.Add(dc3)
            dtTeixits.Columns.Add(dc4)
            While daReader.Read
                dr = dtTeixits.NewRow
                dr("CODI") = daReader("CODI")
                dr("DESCRI") = daReader("DESCRI")
                dr(tablaProveedores) = daReader(tablaProveedores)
                dr("NOMPROVE") = daReader("NOMPROVE")
                dtTeixits.Rows.Add(dr)
            End While
            daReader.Close()

            dvHilosElegibles = dtTeixits.DefaultView
            dvHilosElegibles.Sort = "CODI, PROVE"

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            MyBase.ActualizarOrigen(True, True)

        Catch ex As Exception
            Throw ex
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub borrar()
        Try
            MyBase.borrar()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub PonerNombres()
        Dim aceptarCambiosAlFinal As Boolean = False
        Try
            If tabla.GetChanges Is Nothing Then aceptarCambiosAlFinal = True
            NOMPROVE = general.OBN(PROVE, dtProve)
            If aceptarCambiosAlFinal AndAlso (Not tabla.GetChanges Is Nothing) Then tabla.AcceptChanges()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub
    Public Shadows Sub CargarColores()
        Try

            If Not TEIXIT = "" AndAlso Not PROVE = 0 Then

                Dim i As Integer
                Dim strSQL As String
                Dim cmdSelect As New MySqlCommand
                Dim dr As DataRow
                Dim daReader As MySqlDataReader


                cmdSelect.Connection = cnn
                strSQL = "SELECT COLOR, FILCOL.PREUCOST AS COST FROM FILCOL " & _
                        " LEFT JOIN FIL ON (FIL.CODI = FILCOL.FIL AND FIL.PROVE = FILCOL.PROVE AND FIL.PROVE = """ & PROVE & """) " & _
                        " WHERE (" & _
                        " TIPUS =""F"" AND " & _
                        " FILCOL.fil = """ & TEIXIT & """ AND " & _
                        " FILCOL.PROVE = """ & PROVE & """) " & _
                        " ORDER BY FILCOL.color"

                cmdSelect.CommandText = strSQL

                ACN()
                daReader = cmdSelect.ExecuteReader
                dtColores.Rows.Clear()
                While daReader.Read
                    dr = dtColores.NewRow
                    dr("COLOR") = daReader("COLOR")
                    dr("COST") = daReader("COST")
                    dtColores.Rows.Add(dr)
                End While

                daReader.Close()
                CCN()
                PonerPrecio()
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerPrecio()
        Dim dr() As DataRow
        Try
            If PROVE <> 0 And TEIXIT <> "" And COLOR <> "" Then

                dr = dtColores.Select("COLOR = '" & COLOR & "'")

                Try : PREU = dr(0)("COST") : Catch ex As Exception : End Try

            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub CanvioColumna(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs)
        Try
            DormirHandlers()
            guardarDV()

            If Not IsDBNull(CONSUM) Then
                Me.IMPORT = roundnum(Me.CONSUM * Me.PREU, 2)
                m_Maquina.IMPORT = m_Maquina.PREU * m_Maquina.TEMPS
                m_Muestra.cartaColores.COST = m_Maquina.IMPORT + sumaTotal("IMPORT", dvForm)
                m_Muestra.cartaColores.VENDA = roundnum(100 * m_Muestra.cartaColores.COST / (100 - m_Muestra.MARGE), 2)

            End If
            DesperatarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN() : DesperatarHandlers()
        End Try
    End Sub
    Public Sub ValidarTiempoMaq()
        Try
            'Esto quiere decir que todavía no se ha introducido ninguna maquina
            'como desglose
            IMPORT = roundnum(nzn(m_Maquina.TEMPS, 0) * nzn(m_Maquina.PREU, 0), 2)
            'actualizarCostePVP(nzn(txtImporte.Text, 0))

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
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
    Friend Overrides Function GenOrder() As String

    End Function
    Friend Overrides Function genWhere() As String

    End Function
    Friend Overrides Function genWhereNumeroRegistros() As String

    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer

    End Function

#End Region

#Region "IMPRIMIR"

    Private Function PonerCabecera(ByVal xls As ExcelDirecto.ExcelDirecto)
        Try
            xls.PonerBordeARango(xls.objHojaExcel.Range("A1:D1"), Excel.XlBordersIndex.xlEdgeTop, Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, RGB(0, 0, 0))
            xls.PonerBordeARango(xls.objHojaExcel.Range("A3:D3"), Excel.XlBordersIndex.xlEdgeTop, Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, RGB(0, 0, 0))

            xls.PonerBordeARango(xls.objHojaExcel.Range("A1:A2"), Excel.XlBordersIndex.xlEdgeLeft, Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, RGB(0, 0, 0))
            xls.PonerBordeARango(xls.objHojaExcel.Range("D1:D2"), Excel.XlBordersIndex.xlEdgeRight, Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, RGB(0, 0, 0))

            With xls.objHojaExcel
                .Range("A1:A1").Value = rm.GetString("CLIENTE")
                .Range("A2:A2").Value = rm.GetString("FECHA")
                .Range("A2:A2").Value = m_Muestra.NOMCLIENT
                .Range("B2:B2").Value = m_Maquina.DATA.ToShortDateString

                .Range("A5:A5").Value = "1- " & rm.GetString("COMPLEMENTO")
                .Range("B5:B5").Value = MOSTRA
                .Range("A6:A6").Value = "2- " & rm.GetString("TALLA")
                .Range("B6:B6").Value = m_Muestra.cartaColores.TALLA
                .Range("A7:A7").Value = "3- " & rm.GetString("MAQUINA")
                .Range("B7:B7").Value = m_Muestra.NOMMAQUI
                .Range("A8:A8").Value = "4- " & rm.GetString("DISCO")
                .Range("B8:B8").Value = "4- " & rm.GetString("DISCO")


                .Range("C5:C5").Value = "5- " & rm.GetString("TIEMPOBAJADA")
                .Range("D5:D5").Value = m_Maquina.TEMPS & " min."

                .Range("C6:C6").Value = "6- " & rm.GetString("VELOCIDADCARRO")
                .Range("D6:D6").Value = m_Maquina.VELOSITAT
                .Range("C7:C7").Value = "7- " & rm.GetString("CORTES")
                .Range("D7:D7").Value = ""
                .Range("C8:C8").Value = "8- " & rm.GetString("PESOTOTAL")
                .Range("D8:D8").Value = ""

                'xls.PonerBordeARango(.Range("E1: K8"), Excel.XlBordersIndex.xlInsideHorizontal, Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, RGB(0, 0, 0))
                xls.PonerBordeARango(.Range("E1: K1"), Excel.XlBordersIndex.xlEdgeTop, Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, RGB(0, 0, 0))
                xls.PonerBordeARango(.Range("E8: K8"), Excel.XlBordersIndex.xlEdgeBottom, Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, RGB(0, 0, 0))
                xls.PonerBordeARango(.Range("E1: E8"), Excel.XlBordersIndex.xlEdgeLeft, Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, RGB(0, 0, 0))
                xls.PonerBordeARango(.Range("K1: K8"), Excel.XlBordersIndex.xlEdgeRight, Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, RGB(0, 0, 0))

                Return 9
            End With
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Public Function PonerDetalle(ByVal xls As ExcelDirecto.ExcelDirecto, ByVal fila1 As Integer) As Integer
        Dim i As Integer
        Try
            With xls.objHojaExcel

                .Range("A" & fila1 & ":A" & fila1).Value = rm.GetString("MATERIA")
                .Range("B" & fila1 & ":B" & fila1).Value = rm.GetString("PROVEEDOR")
                .Range("C" & fila1 & ":C" & fila1).Value = rm.GetString("COLOR")
                .Range("D" & fila1 & ":D" & fila1).Value = rm.GetString("GH")
                .Range("E" & fila1 & ":E" & fila1).Value = rm.GetString("CABOS")
                .Range("F" & fila1 & ":F" & fila1).Value = rm.GetString("CICLOS")
                .Range("G" & fila1 & ":G" & fila1).Value = rm.GetString("CONSUM")
                .Range("H" & fila1 & ":H" & fila1).Value = rm.GetString("GRADUACION")

            End With
            fila1 = fila1 + 1
            For i = 0 To dvForm.Count - 1
                With xls.objHojaExcel
                    fila1 = fila1 + i

                    .Range("A" & fila1 & ":A" & fila1).Value = dvForm(i).Item("TEIXIT")
                    .Range("B" & fila1 & ":B" & fila1).Value = dvForm(i).Item("NOMPROVE")
                    .Range("C" & fila1 & ":C" & fila1).Value = dvForm(i).Item("COLOR")
                    .Range("D" & fila1 & ":D" & fila1).Value = dvForm(i).Item("FIL")
                    .Range("E" & fila1 & ":E" & fila1).Value = dvForm(i).Item("CAPS")
                    .Range("F" & fila1 & ":F" & fila1).Value = dvForm(i).Item("CICLES")
                    .Range("G" & fila1 & ":G" & fila1).Value = dvForm(i).Item("CONSUM")
                    .Range("H" & fila1 & ":H" & fila1).Value = dvForm(i).Item("GRADUACION")

                End With
            Next

            Return fila1 + 1
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function PonerRecuadro(ByVal xls As ExcelDirecto.ExcelDirecto, ByVal fila1 As Integer)
        Try

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function CrearTablaFichaTecnica() As DataTable
        Try
            'Ahora con direcciones de envio
            Dim dt As New DataTable("FICHATECNICA")
            Dim dr As DataRow
            Dim i As Integer
            dt.Columns.Add(New DataColumn("NOMCLIENT"))
            dt.Columns("NOMCLIENT").DefaultValue = m_Muestra.NOMCLIENT

            dt.Columns.Add(New DataColumn("DATA"))
            dt.Columns("DATA").DefaultValue = m_Maquina.DATA.ToShortDateString

            dt.Columns.Add(New DataColumn("MUESTRA"))
            dt.Columns("MUESTRA").DefaultValue = m_Muestra.CODI

            dt.Columns.Add(New DataColumn("NOMMAQUINA"))
            dt.Columns("NOMMAQUINA").DefaultValue = m_Muestra.NOMMAQUI

            dt.Columns.Add(New DataColumn("DISCO"))
            dt.Columns("DISCO").DefaultValue = m_Maquina.DISCO

            dt.Columns.Add(New DataColumn("TIEMPOBAJADA"))
            dt.Columns("TIEMPOBAJADA").DefaultValue = m_Maquina.TEMPS

            dt.Columns.Add(New DataColumn("VELOCIDAD"))
            dt.Columns("VELOCIDAD").DefaultValue = m_Maquina.VELOSITAT

            dt.Columns.Add(New DataColumn("NOTAS"))
            dt.Columns("NOTAS").DefaultValue = m_Maquina.NOTAS

            dt.Columns.Add(New DataColumn("TALLA"))
            dt.Columns("TALLA").DefaultValue = m_Maquina.TALLA

            dt.Columns.Add(New DataColumn("CORTES"))
            dt.Columns("CORTES").DefaultValue = m_Maquina.CORTES

            dt.Columns.Add(New DataColumn("PESOTOTAL"))
            dt.Columns("PESOTOTAL").DefaultValue = sumaTotal("CONSUM", dvForm)

            dt.Columns.Add(New DataColumn("MATERIA"))
            dt.Columns.Add(New DataColumn("PROVEEDOR"))
            dt.Columns.Add(New DataColumn("COLOR"))
            dt.Columns.Add(New DataColumn("GH"))
            dt.Columns.Add(New DataColumn("CABOS"))
            dt.Columns.Add(New DataColumn("CICLOS"))
            dt.Columns.Add(New DataColumn("CONSUMO"))
            dt.Columns.Add(New DataColumn("GRADUACION"))


            For i = 0 To dvForm.Count - 1
                dr = dt.NewRow
                dr("MATERIA") = dvForm(i).Item("TEIXIT")
                dr("PROVEEDOR") = dvForm(i).Item("NOMPROVE")
                dr("COLOR") = dvForm(i).Item("COLOR")
                dr("GH") = dvForm(i).Item("FIL")
                dr("CABOS") = dvForm(i).Item("CAPS")
                dr("CONSUMO") = dvForm(i).Item("CONSUM")
                dr("GRADUACION") = dvForm(i).Item("GRADUACION")
                'dr("CLICLOS") = dvForm(i).Item("CICLES")'!!!!
                dt.Rows.Add(dr)
            Next
            'Dim dsTEMPORAL As New DataSet
            'dsTEMPORAL.Tables.Add(dt)
            'dsTEMPORAL.WriteXml(curdir & "\esquema.xml")
            Return dt

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Public Function ImprimirFichaTecnica() As System.Drawing.Printing.PrintDocument
        Try
            Dim c1r As New C1.C1Report.C1Report
            c1r.Load(CurDir() & "\informes\informes.xml", "FICHATECNICAMUESTRAS")
            c1r.DataSource.Recordset = CrearTablaFichaTecnica()
            Return (c1r.Document)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function

#End Region

End Class

#Region "BORRAR"

'Dim xls As New ExcelDirecto.ExcelDirecto(True, 45, 0, 0, 0, 0)
'Dim fila As Integer
'Dim margenes As ExcelDirecto.ExcelDirecto.TamMargenes
'Try
'Dim dsTEMPORAL As New DataSet
'dsTEMPORAL.Tables.Add(dvForm.Table.Copy)
'dsTEMPORAL.Tables.Add(m_Maquina.tabla.Copy)
'dsTEMPORAL.Tables.Add(m_Muestra.tabla.Copy)
'dsTEMPORAL.Tables.Add(m_Muestra.cartaColores.tabla.Copy)
'dsTEMPORAL.WriteXml(directorio&"esquema.xml")


'margenes.margen_der = 0.4
'margenes.margen_inf = 0.4
'margenes.margen_izq = 0.4
'margenes.margen_sup = 0.4

'xls.AñadirHoja(rm.GetString("FICHATECNICA"), "Tahoma")
'xls.ConfigurarPagina(ExcelDirecto.ExcelDirecto.Orientacion.Horizontal, margenes)

'fila = PonerCabecera(xls)

'fila = PonerDetalle(xls, 9)

'fila = PonerRecuadro(xls, fila)

'xls.LiberarExcel()

#End Region

