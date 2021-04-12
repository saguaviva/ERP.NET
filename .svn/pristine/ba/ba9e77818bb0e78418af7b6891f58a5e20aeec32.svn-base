Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Imports C1.Win.C1TrueDBGrid


Namespace aura2k3

    Public Class frmCompraPAFPlantilla
        Inherits aura2k3.frmBase

#Region " Código generado por el Diseñador de Windows Forms "

        Public Sub New()
            MyBase.New()

            'El Diseñador de Windows Forms requiere esta llamada.
            InitializeComponent() : Dim tom As SMcMaster.TabOrderManager = New SMcMaster.TabOrderManager(Me) : tom.SetTabOrder(SMcMaster.TabOrderManager.TabScheme.AcrossFirst)

            'Agregar cualquier inicialización después de la llamada a InitializeComponent()

        End Sub

        'Form reemplaza a Dispose para limpiar la lista de componentes.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
           MyBase.Dispose(disposing)
        End Sub

        'Requerido por el Diseñador de Windows Forms
        Private components As System.ComponentModel.IContainer

        'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
        'Puede modificarse utilizando el Diseñador de Windows Forms. 
        'No lo modifique con el editor de código.
        Protected WithEvents lblPedido As Label
        Protected WithEvents lblObservaciones As Label
        Protected WithEvents lblTraspasado As Label
        Protected WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Protected WithEvents lblDescuento As Label
        Protected WithEvents lblBase As Label
        Protected WithEvents lblTotalBruto As Label
        Protected WithEvents txtTOTAL As C1.Win.C1Input.C1TextBox
        Protected WithEvents btnImprimir As C1.Win.C1Input.C1Button
        Protected WithEvents gbEleccionClientesPAF As System.Windows.Forms.GroupBox
        Protected WithEvents lblTotal As Label
        Protected WithEvents btnElegirProveedor As C1.Win.C1Input.C1Button
        Protected WithEvents lblFecha As Label
        Protected WithEvents lblNumeroPedido As Label
        Protected WithEvents lblProveedor As Label
        Protected WithEvents tabPagePAF As System.Windows.Forms.TabPage
        Protected WithEvents rdoVerTodasPAF As System.Windows.Forms.RadioButton
        Protected WithEvents rdoVerPAFDeCliente As System.Windows.Forms.RadioButton
        Protected WithEvents lblRE As Label
        Protected WithEvents lblIVA As Label
        Protected WithEvents lblFechaEntrega As Label
        Protected WithEvents cboSeleccionProveParaPAF As C1.Win.C1List.C1Combo
        Protected WithEvents dgDetalle As C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Protected WithEvents cboKM As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Protected WithEvents cboColor As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Protected WithEvents cboArticle As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Protected WithEvents lblNumeroPAF As System.Windows.Forms.Label
        Protected WithEvents cboNOMPROVE As C1.Win.C1List.C1Combo
        Protected WithEvents txtPROVE As C1.Win.C1Input.C1TextBox
        Protected WithEvents txtOBSERV As C1.Win.C1Input.C1TextBox
        Friend WithEvents dtpDATA As C1.Win.C1Input.C1DateEdit
        Protected WithEvents txtNRO As C1.Win.C1Input.C1TextBox
        Protected WithEvents txtP_DTE As C1.Win.C1Input.C1TextBox
        Protected WithEvents txtBASE1 As C1.Win.C1Input.C1TextBox
        Protected WithEvents txtBRUT1 As C1.Win.C1Input.C1TextBox
        Protected WithEvents txtIVA1 As C1.Win.C1Input.C1TextBox
        Protected WithEvents txtDTE1 As C1.Win.C1Input.C1TextBox
        Protected WithEvents txtRE1 As C1.Win.C1Input.C1TextBox
        Friend WithEvents dtpDATAENTREGA As C1.Win.C1Input.C1DateEdit
        Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
        Protected WithEvents TabControlPAF As System.Windows.Forms.TabControl
        Protected WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents cboFRA As C1.Win.C1List.C1Combo
        Protected WithEvents chkESTOC As System.Windows.Forms.CheckBox
        Protected WithEvents btnCancelar As C1.Win.C1Input.C1Button
        Protected WithEvents lblPedidoMuestra As System.Windows.Forms.Label
        Protected WithEvents txtCOMANDAMOSTRA As C1.Win.C1Input.C1TextBox
        Friend WithEvents cboNOMTALLER As C1.Win.C1List.C1Combo
        Friend WithEvents btnElegirTaller As C1.Win.C1Input.C1Button
        Friend WithEvents txtTALLER As C1.Win.C1Input.C1TextBox
        Friend WithEvents lblTaller As System.Windows.Forms.Label
        Protected WithEvents lblOrdenFabComplemento As System.Windows.Forms.Label
        Friend WithEvents cboORDENFABCOMP As C1.Win.C1List.C1Combo
        Protected WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents ToolBarButton1 As System.Windows.Forms.ToolBarButton
        Friend WithEvents ToolBarButton2 As System.Windows.Forms.ToolBarButton
        Friend WithEvents ToolBarButton3 As System.Windows.Forms.ToolBarButton
        Friend WithEvents ToolBarButton4 As System.Windows.Forms.ToolBarButton
        Friend WithEvents ToolBarButton5 As System.Windows.Forms.ToolBarButton
        Friend WithEvents ToolBarButton6 As System.Windows.Forms.ToolBarButton
        Protected WithEvents cboMedida As C1.Win.C1TrueDBGrid.C1TrueDBDropdown

        Friend WithEvents ToolBarButton7 As System.Windows.Forms.ToolBarButton

        Friend WithEvents ToolBarButton8 As System.Windows.Forms.ToolBarButton

        Friend WithEvents ToolBarButton9 As System.Windows.Forms.ToolBarButton

        Friend WithEvents ToolBarButton10 As System.Windows.Forms.ToolBarButton

        Friend WithEvents ToolBarButton11 As System.Windows.Forms.ToolBarButton

        Friend WithEvents ToolBarButton12 As System.Windows.Forms.ToolBarButton

        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Style1 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style2 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style3 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style4 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style5 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style6 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCompraPAFPlantilla))
        Dim Style7 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style8 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style9 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style10 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style11 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style12 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style13 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style14 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style15 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style16 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style17 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style18 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style19 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style20 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style21 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style22 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style23 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style24 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style25 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style26 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style27 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style28 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style29 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style30 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style31 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style32 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style33 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style34 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style35 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style36 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style37 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style38 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style39 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style40 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Me.tabPagePAF = New System.Windows.Forms.TabPage()
        Me.cboMedida = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboArticle = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.cboColor = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.cboKM = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.dgDetalle = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.lblRE = New System.Windows.Forms.Label()
        Me.txtRE1 = New C1.Win.C1Input.C1TextBox()
        Me.lblIVA = New System.Windows.Forms.Label()
        Me.lblDescuento = New System.Windows.Forms.Label()
        Me.lblBase = New System.Windows.Forms.Label()
        Me.lblTotalBruto = New System.Windows.Forms.Label()
        Me.txtIVA1 = New C1.Win.C1Input.C1TextBox()
        Me.txtTOTAL = New C1.Win.C1Input.C1TextBox()
        Me.txtBASE1 = New C1.Win.C1Input.C1TextBox()
        Me.txtDTE1 = New C1.Win.C1Input.C1TextBox()
        Me.txtBRUT1 = New C1.Win.C1Input.C1TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboORDENFABCOMP = New C1.Win.C1List.C1Combo()
        Me.lblOrdenFabComplemento = New System.Windows.Forms.Label()
        Me.cboNOMTALLER = New C1.Win.C1List.C1Combo()
        Me.txtTALLER = New C1.Win.C1Input.C1TextBox()
        Me.btnElegirTaller = New C1.Win.C1Input.C1Button()
        Me.lblTaller = New System.Windows.Forms.Label()
        Me.txtCOMANDAMOSTRA = New C1.Win.C1Input.C1TextBox()
        Me.lblPedidoMuestra = New System.Windows.Forms.Label()
        Me.cboFRA = New C1.Win.C1List.C1Combo()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtP_DTE = New C1.Win.C1Input.C1TextBox()
        Me.dtpDATA = New C1.Win.C1Input.C1DateEdit()
        Me.dtpDATAENTREGA = New C1.Win.C1Input.C1DateEdit()
        Me.txtNRO = New C1.Win.C1Input.C1TextBox()
        Me.cboNOMPROVE = New C1.Win.C1List.C1Combo()
        Me.lblFechaEntrega = New System.Windows.Forms.Label()
        Me.lblPedido = New System.Windows.Forms.Label()
        Me.lblProveedor = New System.Windows.Forms.Label()
        Me.btnElegirProveedor = New C1.Win.C1Input.C1Button()
        Me.txtPROVE = New C1.Win.C1Input.C1TextBox()
        Me.lblNumeroPedido = New System.Windows.Forms.Label()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.chkESTOC = New System.Windows.Forms.CheckBox()
        Me.txtOBSERV = New C1.Win.C1Input.C1TextBox()
        Me.lblObservaciones = New System.Windows.Forms.Label()
        Me.lblTraspasado = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.gbEleccionClientesPAF = New System.Windows.Forms.GroupBox()
        Me.lblNumeroPAF = New System.Windows.Forms.Label()
        Me.cboSeleccionProveParaPAF = New C1.Win.C1List.C1Combo()
        Me.rdoVerTodasPAF = New System.Windows.Forms.RadioButton()
        Me.rdoVerPAFDeCliente = New System.Windows.Forms.RadioButton()
        Me.btnImprimir = New C1.Win.C1Input.C1Button()
        Me.TabControlPAF = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.btnCancelar = New C1.Win.C1Input.C1Button()
        Me.ToolBarButton1 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton2 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton3 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton4 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton5 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton6 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton7 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton8 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton9 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton10 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton11 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton12 = New System.Windows.Forms.ToolBarButton()
        CType(Me.btnRecargar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnSiguiente,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnAnterior,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnPrimero,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnUltimo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnModificar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnTancar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnBorrar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnNuevo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnActualizar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnVerLista,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tabPagePAF.SuspendLayout
        CType(Me.cboMedida,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboArticle,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboColor,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboKM,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dgDetalle,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtRE1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtIVA1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtTOTAL,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtBASE1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtDTE1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtBRUT1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox1.SuspendLayout
        CType(Me.cboORDENFABCOMP,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboNOMTALLER,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtTALLER,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnElegirTaller,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtCOMANDAMOSTRA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboFRA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtP_DTE,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dtpDATA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dtpDATAENTREGA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtNRO,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboNOMPROVE,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnElegirProveedor,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtPROVE,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtOBSERV,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbEleccionClientesPAF.SuspendLayout
        CType(Me.cboSeleccionProveParaPAF,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnImprimir,System.ComponentModel.ISupportInitialize).BeginInit
        Me.TabControlPAF.SuspendLayout
        CType(Me.btnCancelar,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(7, 106)
        Me.sbtipo.Size = New System.Drawing.Size(123, 20)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(49, 660)
        Me.btnRecargar.Size = New System.Drawing.Size(140, 28)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(444, 661)
        Me.btnSiguiente.Size = New System.Drawing.Size(45, 28)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(4, 660)
        Me.btnAnterior.Size = New System.Drawing.Size(45, 28)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(4, 631)
        Me.btnPrimero.Size = New System.Drawing.Size(45, 29)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(444, 631)
        Me.btnUltimo.Size = New System.Drawing.Size(45, 29)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(189, 631)
        Me.btnModificar.Size = New System.Drawing.Size(140, 29)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(957, 660)
        Me.btnTancar.Size = New System.Drawing.Size(112, 28)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(189, 660)
        Me.btnBorrar.Size = New System.Drawing.Size(140, 28)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(329, 631)
        Me.btnNuevo.Size = New System.Drawing.Size(115, 57)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(49, 631)
        Me.btnActualizar.Size = New System.Drawing.Size(140, 29)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(957, 631)
        Me.btnVerLista.Size = New System.Drawing.Size(112, 29)
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.ItemHeight = 17
        Me.cboSeleccionCentro.Location = New System.Drawing.Point(642, 7)
        Me.cboSeleccionCentro.Size = New System.Drawing.Size(218, 25)
        '
        'tabPagePAF
        '
        Me.tabPagePAF.Controls.Add(Me.cboMedida)
        Me.tabPagePAF.Controls.Add(Me.Label2)
        Me.tabPagePAF.Controls.Add(Me.cboArticle)
        Me.tabPagePAF.Controls.Add(Me.cboColor)
        Me.tabPagePAF.Controls.Add(Me.cboKM)
        Me.tabPagePAF.Controls.Add(Me.dgDetalle)
        Me.tabPagePAF.Controls.Add(Me.lblRE)
        Me.tabPagePAF.Controls.Add(Me.txtRE1)
        Me.tabPagePAF.Controls.Add(Me.lblIVA)
        Me.tabPagePAF.Controls.Add(Me.lblDescuento)
        Me.tabPagePAF.Controls.Add(Me.lblBase)
        Me.tabPagePAF.Controls.Add(Me.lblTotalBruto)
        Me.tabPagePAF.Controls.Add(Me.txtIVA1)
        Me.tabPagePAF.Controls.Add(Me.txtTOTAL)
        Me.tabPagePAF.Controls.Add(Me.txtBASE1)
        Me.tabPagePAF.Controls.Add(Me.txtDTE1)
        Me.tabPagePAF.Controls.Add(Me.txtBRUT1)
        Me.tabPagePAF.Controls.Add(Me.GroupBox1)
        Me.tabPagePAF.Controls.Add(Me.txtOBSERV)
        Me.tabPagePAF.Controls.Add(Me.lblObservaciones)
        Me.tabPagePAF.Controls.Add(Me.lblTraspasado)
        Me.tabPagePAF.Location = New System.Drawing.Point(4, 22)
        Me.tabPagePAF.Name = "tabPagePAF"
        Me.tabPagePAF.Size = New System.Drawing.Size(1143, 517)
        Me.tabPagePAF.TabIndex = 0
        '
        'cboMedida
        '
        Me.cboMedida.AllowColMove = true
        Me.cboMedida.AllowColSelect = true
        Me.cboMedida.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboMedida.AlternatingRows = true
        Me.cboMedida.CaptionHeight = 17
        Me.cboMedida.CaptionStyle = Style1
        Me.cboMedida.ColumnCaptionHeight = 17
        Me.cboMedida.ColumnFooterHeight = 17
        Me.cboMedida.ColumnSelectorStyle = Style2
        Me.cboMedida.EvenRowStyle = Style3
        Me.cboMedida.FetchRowStyles = false
        Me.cboMedida.FooterStyle = Style4
        Me.cboMedida.HeadingStyle = Style5
        Me.cboMedida.HighLightRowStyle = Style6
        Me.cboMedida.Images.Add(CType(resources.GetObject("cboMedida.Images"),System.Drawing.Image))
        Me.cboMedida.Location = New System.Drawing.Point(288, 272)
        Me.cboMedida.Name = "cboMedida"
        Me.cboMedida.OddRowStyle = Style7
        Me.cboMedida.RecordSelectorStyle = Style8
        Me.cboMedida.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboMedida.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboMedida.RowHeight = 15
        Me.cboMedida.RowSelectorStyle = Style9
        Me.cboMedida.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboMedida.ScrollTips = false
        Me.cboMedida.Size = New System.Drawing.Size(296, 162)
        Me.cboMedida.Style = Style10
        Me.cboMedida.TabIndex = 237
        Me.cboMedida.TabStop = false
        Me.cboMedida.Text = "C1TrueDBDropdown1"
        Me.cboMedida.UseCompatibleTextRendering = false
        Me.cboMedida.Visible = false
        Me.cboMedida.PropBag = resources.GetString("cboMedida.PropBag")
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(944, 479)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 25)
        Me.Label2.TabIndex = 236
        Me.Label2.Text = "Total"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboArticle
        '
        Me.cboArticle.AllowColMove = true
        Me.cboArticle.AllowColSelect = true
        Me.cboArticle.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboArticle.AlternatingRows = true
        Me.cboArticle.CaptionHeight = 17
        Me.cboArticle.CaptionStyle = Style11
        Me.cboArticle.ColumnCaptionHeight = 17
        Me.cboArticle.ColumnFooterHeight = 17
        Me.cboArticle.ColumnSelectorStyle = Style12
        Me.cboArticle.EvenRowStyle = Style13
        Me.cboArticle.FetchRowStyles = false
        Me.cboArticle.FooterStyle = Style14
        Me.cboArticle.HeadingStyle = Style15
        Me.cboArticle.HighLightRowStyle = Style16
        Me.cboArticle.Images.Add(CType(resources.GetObject("cboArticle.Images"),System.Drawing.Image))
        Me.cboArticle.Location = New System.Drawing.Point(221, 261)
        Me.cboArticle.Name = "cboArticle"
        Me.cboArticle.OddRowStyle = Style17
        Me.cboArticle.RecordSelectorStyle = Style18
        Me.cboArticle.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboArticle.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboArticle.RowHeight = 15
        Me.cboArticle.RowSelectorStyle = Style19
        Me.cboArticle.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboArticle.ScrollTips = false
        Me.cboArticle.Size = New System.Drawing.Size(258, 162)
        Me.cboArticle.Style = Style20
        Me.cboArticle.TabIndex = 235
        Me.cboArticle.TabStop = false
        Me.cboArticle.Text = "C1TrueDBDropdown1"
        Me.cboArticle.UseCompatibleTextRendering = false
        Me.cboArticle.Visible = false
        Me.cboArticle.PropBag = resources.GetString("cboArticle.PropBag")
        '
        'cboColor
        '
        Me.cboColor.AllowColMove = true
        Me.cboColor.AllowColSelect = true
        Me.cboColor.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboColor.AlternatingRows = true
        Me.cboColor.CaptionHeight = 17
        Me.cboColor.CaptionStyle = Style21
        Me.cboColor.ColumnCaptionHeight = 17
        Me.cboColor.ColumnFooterHeight = 17
        Me.cboColor.ColumnSelectorStyle = Style22
        Me.cboColor.EvenRowStyle = Style23
        Me.cboColor.FetchRowStyles = false
        Me.cboColor.FooterStyle = Style24
        Me.cboColor.HeadingStyle = Style25
        Me.cboColor.HighLightRowStyle = Style26
        Me.cboColor.Images.Add(CType(resources.GetObject("cboColor.Images"),System.Drawing.Image))
        Me.cboColor.Location = New System.Drawing.Point(39, 261)
        Me.cboColor.Name = "cboColor"
        Me.cboColor.OddRowStyle = Style27
        Me.cboColor.RecordSelectorStyle = Style28
        Me.cboColor.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboColor.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboColor.RowHeight = 15
        Me.cboColor.RowSelectorStyle = Style29
        Me.cboColor.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboColor.ScrollTips = false
        Me.cboColor.Size = New System.Drawing.Size(297, 162)
        Me.cboColor.Style = Style30
        Me.cboColor.TabIndex = 234
        Me.cboColor.TabStop = false
        Me.cboColor.Text = "C1TrueDBDropdown1"
        Me.cboColor.UseCompatibleTextRendering = false
        Me.cboColor.Visible = false
        Me.cboColor.PropBag = resources.GetString("cboColor.PropBag")
        '
        'cboKM
        '
        Me.cboKM.AllowColMove = true
        Me.cboKM.AllowColSelect = true
        Me.cboKM.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboKM.AlternatingRows = true
        Me.cboKM.CaptionHeight = 17
        Me.cboKM.CaptionStyle = Style31
        Me.cboKM.ColumnCaptionHeight = 17
        Me.cboKM.ColumnFooterHeight = 17
        Me.cboKM.ColumnSelectorStyle = Style32
        Me.cboKM.EvenRowStyle = Style33
        Me.cboKM.FetchRowStyles = false
        Me.cboKM.FooterStyle = Style34
        Me.cboKM.HeadingStyle = Style35
        Me.cboKM.HighLightRowStyle = Style36
        Me.cboKM.Images.Add(CType(resources.GetObject("cboKM.Images"),System.Drawing.Image))
        Me.cboKM.Location = New System.Drawing.Point(35, 286)
        Me.cboKM.Name = "cboKM"
        Me.cboKM.OddRowStyle = Style37
        Me.cboKM.RecordSelectorStyle = Style38
        Me.cboKM.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboKM.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboKM.RowHeight = 15
        Me.cboKM.RowSelectorStyle = Style39
        Me.cboKM.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboKM.ScrollTips = false
        Me.cboKM.Size = New System.Drawing.Size(330, 162)
        Me.cboKM.Style = Style40
        Me.cboKM.TabIndex = 233
        Me.cboKM.TabStop = false
        Me.cboKM.Text = "C1TrueDBDropdown1"
        Me.cboKM.UseCompatibleTextRendering = false
        Me.cboKM.ValueTranslate = true
        Me.cboKM.Visible = false
        Me.cboKM.PropBag = resources.GetString("cboKM.PropBag")
        '
        'dgDetalle
        '
        Me.dgDetalle.AllowAddNew = true
        Me.dgDetalle.AllowDelete = true
        Me.dgDetalle.AllowSort = false
        Me.dgDetalle.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.dgDetalle.Caption = "LINIES COMPRA"
        Me.dgDetalle.CaptionHeight = 17
        Me.dgDetalle.CellTipsDelay = 250
        Me.dgDetalle.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgDetalle.Images.Add(CType(resources.GetObject("dgDetalle.Images"),System.Drawing.Image))
        Me.dgDetalle.Location = New System.Drawing.Point(11, 226)
        Me.dgDetalle.Name = "dgDetalle"
        Me.dgDetalle.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgDetalle.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgDetalle.PreviewInfo.ZoomFactor = 75R
        Me.dgDetalle.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgDetalle.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgDetalle.RowHeight = 15
        Me.dgDetalle.Size = New System.Drawing.Size(1118, 247)
        Me.dgDetalle.SpringMode = true
        Me.dgDetalle.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgDetalle.TabIndex = 213
        Me.dgDetalle.Text = "C1TrueDBGrid1"
        Me.dgDetalle.UseCompatibleTextRendering = false
        Me.dgDetalle.WrapCellPointer = true
        Me.dgDetalle.PropBag = resources.GetString("dgDetalle.PropBag")
        '
        'lblRE
        '
        Me.lblRE.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRE.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRE.Location = New System.Drawing.Point(423, 479)
        Me.lblRE.Name = "lblRE"
        Me.lblRE.Size = New System.Drawing.Size(45, 25)
        Me.lblRE.TabIndex = 212
        Me.lblRE.Text = "RE"
        Me.lblRE.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRE1
        '
        Me.txtRE1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtRE1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.txtRE1.CustomFormat = "#,##0.00"
        Me.txtRE1.DataType = GetType(Double)
        Me.txtRE1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtRE1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtRE1.Location = New System.Drawing.Point(474, 479)
        Me.txtRE1.Name = "txtRE1"
        Me.txtRE1.Size = New System.Drawing.Size(101, 21)
        Me.txtRE1.TabIndex = 211
        Me.txtRE1.Tag = Nothing
        Me.txtRE1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblIVA
        '
        Me.lblIVA.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblIVA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblIVA.Location = New System.Drawing.Point(567, 479)
        Me.lblIVA.Name = "lblIVA"
        Me.lblIVA.Size = New System.Drawing.Size(56, 25)
        Me.lblIVA.TabIndex = 194
        Me.lblIVA.Text = "IVA"
        Me.lblIVA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDescuento
        '
        Me.lblDescuento.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblDescuento.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDescuento.Location = New System.Drawing.Point(17, 479)
        Me.lblDescuento.Name = "lblDescuento"
        Me.lblDescuento.Size = New System.Drawing.Size(56, 25)
        Me.lblDescuento.TabIndex = 193
        Me.lblDescuento.Text = "Dte.:"
        Me.lblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBase
        '
        Me.lblBase.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblBase.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblBase.Location = New System.Drawing.Point(208, 479)
        Me.lblBase.Name = "lblBase"
        Me.lblBase.Size = New System.Drawing.Size(108, 25)
        Me.lblBase.TabIndex = 192
        Me.lblBase.Text = "Base Imposable"
        Me.lblBase.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalBruto
        '
        Me.lblTotalBruto.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTotalBruto.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTotalBruto.Location = New System.Drawing.Point(754, 479)
        Me.lblTotalBruto.Name = "lblTotalBruto"
        Me.lblTotalBruto.Size = New System.Drawing.Size(72, 25)
        Me.lblTotalBruto.TabIndex = 191
        Me.lblTotalBruto.Text = "Total Brut"
        Me.lblTotalBruto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtIVA1
        '
        Me.txtIVA1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtIVA1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.txtIVA1.CustomFormat = "#,##0.00"
        Me.txtIVA1.DataType = GetType(Double)
        Me.txtIVA1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtIVA1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtIVA1.Location = New System.Drawing.Point(629, 479)
        Me.txtIVA1.Name = "txtIVA1"
        Me.txtIVA1.Size = New System.Drawing.Size(101, 21)
        Me.txtIVA1.TabIndex = 190
        Me.txtIVA1.Tag = Nothing
        Me.txtIVA1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTOTAL
        '
        Me.txtTOTAL.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtTOTAL.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.txtTOTAL.CustomFormat = "#,##0.00"
        Me.txtTOTAL.DataType = GetType(Double)
        Me.txtTOTAL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtTOTAL.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtTOTAL.Location = New System.Drawing.Point(992, 479)
        Me.txtTOTAL.Name = "txtTOTAL"
        Me.txtTOTAL.Size = New System.Drawing.Size(101, 21)
        Me.txtTOTAL.TabIndex = 189
        Me.txtTOTAL.Tag = Nothing
        Me.txtTOTAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBASE1
        '
        Me.txtBASE1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtBASE1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.txtBASE1.CustomFormat = "#,##0.00"
        Me.txtBASE1.DataType = GetType(Double)
        Me.txtBASE1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtBASE1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtBASE1.Location = New System.Drawing.Point(316, 479)
        Me.txtBASE1.Name = "txtBASE1"
        Me.txtBASE1.Size = New System.Drawing.Size(101, 21)
        Me.txtBASE1.TabIndex = 188
        Me.txtBASE1.Tag = Nothing
        Me.txtBASE1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDTE1
        '
        Me.txtDTE1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtDTE1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.txtDTE1.CustomFormat = "#,##0.00"
        Me.txtDTE1.DataType = GetType(Double)
        Me.txtDTE1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtDTE1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtDTE1.Location = New System.Drawing.Point(78, 479)
        Me.txtDTE1.Name = "txtDTE1"
        Me.txtDTE1.Size = New System.Drawing.Size(101, 21)
        Me.txtDTE1.TabIndex = 187
        Me.txtDTE1.Tag = Nothing
        Me.txtDTE1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBRUT1
        '
        Me.txtBRUT1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtBRUT1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.txtBRUT1.CustomFormat = "#,##0.00"
        Me.txtBRUT1.DataType = GetType(Double)
        Me.txtBRUT1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtBRUT1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtBRUT1.Location = New System.Drawing.Point(831, 479)
        Me.txtBRUT1.Name = "txtBRUT1"
        Me.txtBRUT1.Size = New System.Drawing.Size(101, 21)
        Me.txtBRUT1.TabIndex = 186
        Me.txtBRUT1.Tag = Nothing
        Me.txtBRUT1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cboORDENFABCOMP)
        Me.GroupBox1.Controls.Add(Me.lblOrdenFabComplemento)
        Me.GroupBox1.Controls.Add(Me.cboNOMTALLER)
        Me.GroupBox1.Controls.Add(Me.txtTALLER)
        Me.GroupBox1.Controls.Add(Me.btnElegirTaller)
        Me.GroupBox1.Controls.Add(Me.lblTaller)
        Me.GroupBox1.Controls.Add(Me.txtCOMANDAMOSTRA)
        Me.GroupBox1.Controls.Add(Me.lblPedidoMuestra)
        Me.GroupBox1.Controls.Add(Me.cboFRA)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtP_DTE)
        Me.GroupBox1.Controls.Add(Me.dtpDATA)
        Me.GroupBox1.Controls.Add(Me.dtpDATAENTREGA)
        Me.GroupBox1.Controls.Add(Me.txtNRO)
        Me.GroupBox1.Controls.Add(Me.cboNOMPROVE)
        Me.GroupBox1.Controls.Add(Me.lblFechaEntrega)
        Me.GroupBox1.Controls.Add(Me.lblPedido)
        Me.GroupBox1.Controls.Add(Me.lblProveedor)
        Me.GroupBox1.Controls.Add(Me.btnElegirProveedor)
        Me.GroupBox1.Controls.Add(Me.txtPROVE)
        Me.GroupBox1.Controls.Add(Me.lblNumeroPedido)
        Me.GroupBox1.Controls.Add(Me.lblFecha)
        Me.GroupBox1.Controls.Add(Me.chkESTOC)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 15)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(767, 207)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = false
        '
        'cboORDENFABCOMP
        '
        Me.cboORDENFABCOMP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboORDENFABCOMP.Caption = ""
        Me.cboORDENFABCOMP.CaptionHeight = 17
        Me.cboORDENFABCOMP.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboORDENFABCOMP.ColumnCaptionHeight = 17
        Me.cboORDENFABCOMP.ColumnFooterHeight = 17
        Me.cboORDENFABCOMP.ContentHeight = 18
        Me.cboORDENFABCOMP.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboORDENFABCOMP.Images.Add(CType(resources.GetObject("cboORDENFABCOMP.Images"),System.Drawing.Image))
        Me.cboORDENFABCOMP.Location = New System.Drawing.Point(454, 110)
        Me.cboORDENFABCOMP.MatchEntryTimeout = CType(2000,Long)
        Me.cboORDENFABCOMP.MaxDropDownItems = CType(5,Short)
        Me.cboORDENFABCOMP.MaxLength = 32767
        Me.cboORDENFABCOMP.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboORDENFABCOMP.Name = "cboORDENFABCOMP"
        Me.cboORDENFABCOMP.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboORDENFABCOMP.Size = New System.Drawing.Size(168, 21)
        Me.cboORDENFABCOMP.TabIndex = 285
        Me.cboORDENFABCOMP.Text = "C1Combo1"
        Me.cboORDENFABCOMP.Visible = false
        Me.cboORDENFABCOMP.PropBag = resources.GetString("cboORDENFABCOMP.PropBag")
        '
        'lblOrdenFabComplemento
        '
        Me.lblOrdenFabComplemento.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblOrdenFabComplemento.Location = New System.Drawing.Point(291, 108)
        Me.lblOrdenFabComplemento.Name = "lblOrdenFabComplemento"
        Me.lblOrdenFabComplemento.Size = New System.Drawing.Size(157, 25)
        Me.lblOrdenFabComplemento.TabIndex = 284
        Me.lblOrdenFabComplemento.Text = "Ordre Complement"
        Me.lblOrdenFabComplemento.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblOrdenFabComplemento.Visible = false
        '
        'cboNOMTALLER
        '
        Me.cboNOMTALLER.AutoCompletion = true
        Me.cboNOMTALLER.AutoSelect = true
        Me.cboNOMTALLER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMTALLER.Caption = ""
        Me.cboNOMTALLER.CaptionHeight = 17
        Me.cboNOMTALLER.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMTALLER.ColumnCaptionHeight = 17
        Me.cboNOMTALLER.ColumnFooterHeight = 17
        Me.cboNOMTALLER.ContentHeight = 18
        Me.cboNOMTALLER.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMTALLER.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMTALLER.Images.Add(CType(resources.GetObject("cboNOMTALLER.Images"),System.Drawing.Image))
        Me.cboNOMTALLER.IntegralHeight = true
        Me.cboNOMTALLER.Location = New System.Drawing.Point(230, 79)
        Me.cboNOMTALLER.MatchEntryTimeout = CType(100,Long)
        Me.cboNOMTALLER.MaxDropDownItems = CType(8,Short)
        Me.cboNOMTALLER.MaxLength = 0
        Me.cboNOMTALLER.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMTALLER.Name = "cboNOMTALLER"
        Me.cboNOMTALLER.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMTALLER.Size = New System.Drawing.Size(520, 21)
        Me.cboNOMTALLER.TabIndex = 281
        Me.cboNOMTALLER.Visible = false
        Me.cboNOMTALLER.PropBag = resources.GetString("cboNOMTALLER.PropBag")
        '
        'txtTALLER
        '
        Me.txtTALLER.Location = New System.Drawing.Point(123, 79)
        Me.txtTALLER.Name = "txtTALLER"
        Me.txtTALLER.Size = New System.Drawing.Size(67, 21)
        Me.txtTALLER.TabIndex = 280
        Me.txtTALLER.Tag = Nothing
        Me.txtTALLER.Visible = false
        '
        'btnElegirTaller
        '
        Me.btnElegirTaller.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirTaller.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirTaller.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirTaller.Location = New System.Drawing.Point(190, 79)
        Me.btnElegirTaller.Name = "btnElegirTaller"
        Me.btnElegirTaller.Size = New System.Drawing.Size(34, 24)
        Me.btnElegirTaller.TabIndex = 283
        Me.btnElegirTaller.Text = "..."
        Me.btnElegirTaller.UseVisualStyleBackColor = true
        Me.btnElegirTaller.Visible = false
        '
        'lblTaller
        '
        Me.lblTaller.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTaller.Location = New System.Drawing.Point(11, 79)
        Me.lblTaller.Name = "lblTaller"
        Me.lblTaller.Size = New System.Drawing.Size(107, 24)
        Me.lblTaller.TabIndex = 282
        Me.lblTaller.Text = "Taller"
        Me.lblTaller.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTaller.Visible = false
        '
        'txtCOMANDAMOSTRA
        '
        Me.txtCOMANDAMOSTRA.Location = New System.Drawing.Point(442, 20)
        Me.txtCOMANDAMOSTRA.Name = "txtCOMANDAMOSTRA"
        Me.txtCOMANDAMOSTRA.Size = New System.Drawing.Size(157, 21)
        Me.txtCOMANDAMOSTRA.TabIndex = 274
        Me.txtCOMANDAMOSTRA.Tag = Nothing
        Me.txtCOMANDAMOSTRA.Visible = false
        '
        'lblPedidoMuestra
        '
        Me.lblPedidoMuestra.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPedidoMuestra.Location = New System.Drawing.Point(308, 15)
        Me.lblPedidoMuestra.Name = "lblPedidoMuestra"
        Me.lblPedidoMuestra.Size = New System.Drawing.Size(129, 29)
        Me.lblPedidoMuestra.TabIndex = 275
        Me.lblPedidoMuestra.Text = "Numero Comanda Venda"
        Me.lblPedidoMuestra.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblPedidoMuestra.Visible = false
        '
        'cboFRA
        '
        Me.cboFRA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboFRA.Caption = ""
        Me.cboFRA.CaptionHeight = 17
        Me.cboFRA.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboFRA.ColumnCaptionHeight = 17
        Me.cboFRA.ColumnFooterHeight = 17
        Me.cboFRA.ContentHeight = 18
        Me.cboFRA.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboFRA.DropDownWidth = 8
        Me.cboFRA.Images.Add(CType(resources.GetObject("cboFRA.Images"),System.Drawing.Image))
        Me.cboFRA.Location = New System.Drawing.Point(123, 20)
        Me.cboFRA.MatchEntryTimeout = CType(2000,Long)
        Me.cboFRA.MaxDropDownItems = CType(5,Short)
        Me.cboFRA.MaxLength = 32767
        Me.cboFRA.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboFRA.Name = "cboFRA"
        Me.cboFRA.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboFRA.Size = New System.Drawing.Size(179, 21)
        Me.cboFRA.TabIndex = 273
        Me.cboFRA.Text = "C1Combo1"
        Me.cboFRA.PropBag = resources.GetString("cboFRA.PropBag")
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(11, 167)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 25)
        Me.Label1.TabIndex = 272
        Me.Label1.Text = "Dte.:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtP_DTE
        '
        Me.txtP_DTE.AcceptsReturn = true
        Me.txtP_DTE.CustomFormat = "#,##0.00"
        Me.txtP_DTE.DataType = GetType(Double)
        Me.txtP_DTE.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtP_DTE.Location = New System.Drawing.Point(123, 167)
        Me.txtP_DTE.Name = "txtP_DTE"
        Me.txtP_DTE.Size = New System.Drawing.Size(157, 21)
        Me.txtP_DTE.TabIndex = 271
        Me.txtP_DTE.Tag = Nothing
        '
        'dtpDATA
        '
        '
        '
        '
        Me.dtpDATA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpDATA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpDATA.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dtpDATA.Location = New System.Drawing.Point(123, 138)
        Me.dtpDATA.Name = "dtpDATA"
        Me.dtpDATA.Size = New System.Drawing.Size(163, 21)
        Me.dtpDATA.TabIndex = 270
        Me.dtpDATA.Tag = Nothing
        '
        'dtpDATAENTREGA
        '
        '
        '
        '
        Me.dtpDATAENTREGA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpDATAENTREGA.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dtpDATAENTREGA.Location = New System.Drawing.Point(454, 138)
        Me.dtpDATAENTREGA.Name = "dtpDATAENTREGA"
        Me.dtpDATAENTREGA.Size = New System.Drawing.Size(168, 21)
        Me.dtpDATAENTREGA.TabIndex = 269
        Me.dtpDATAENTREGA.Tag = Nothing
        Me.dtpDATAENTREGA.Visible = false
        '
        'txtNRO
        '
        Me.txtNRO.Location = New System.Drawing.Point(123, 108)
        Me.txtNRO.Name = "txtNRO"
        Me.txtNRO.Size = New System.Drawing.Size(157, 21)
        Me.txtNRO.TabIndex = 3
        Me.txtNRO.Tag = Nothing
        '
        'cboNOMPROVE
        '
        Me.cboNOMPROVE.AutoCompletion = true
        Me.cboNOMPROVE.AutoSelect = true
        Me.cboNOMPROVE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMPROVE.Caption = ""
        Me.cboNOMPROVE.CaptionHeight = 17
        Me.cboNOMPROVE.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMPROVE.ColumnCaptionHeight = 17
        Me.cboNOMPROVE.ColumnFooterHeight = 17
        Me.cboNOMPROVE.ContentHeight = 18
        Me.cboNOMPROVE.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMPROVE.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMPROVE.Images.Add(CType(resources.GetObject("cboNOMPROVE.Images"),System.Drawing.Image))
        Me.cboNOMPROVE.Location = New System.Drawing.Point(230, 49)
        Me.cboNOMPROVE.MatchEntryTimeout = CType(100,Long)
        Me.cboNOMPROVE.MaxDropDownItems = CType(5,Short)
        Me.cboNOMPROVE.MaxLength = 255
        Me.cboNOMPROVE.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMPROVE.Name = "cboNOMPROVE"
        Me.cboNOMPROVE.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMPROVE.Size = New System.Drawing.Size(520, 21)
        Me.cboNOMPROVE.TabIndex = 2
        Me.cboNOMPROVE.PropBag = resources.GetString("cboNOMPROVE.PropBag")
        '
        'lblFechaEntrega
        '
        Me.lblFechaEntrega.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFechaEntrega.Location = New System.Drawing.Point(291, 138)
        Me.lblFechaEntrega.Name = "lblFechaEntrega"
        Me.lblFechaEntrega.Size = New System.Drawing.Size(157, 24)
        Me.lblFechaEntrega.TabIndex = 12
        Me.lblFechaEntrega.Text = "Data Entrega"
        Me.lblFechaEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFechaEntrega.Visible = false
        '
        'lblPedido
        '
        Me.lblPedido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPedido.Location = New System.Drawing.Point(11, 20)
        Me.lblPedido.Name = "lblPedido"
        Me.lblPedido.Size = New System.Drawing.Size(112, 24)
        Me.lblPedido.TabIndex = 4
        Me.lblPedido.Text = "Comanda"
        Me.lblPedido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblProveedor
        '
        Me.lblProveedor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblProveedor.Location = New System.Drawing.Point(11, 49)
        Me.lblProveedor.Name = "lblProveedor"
        Me.lblProveedor.Size = New System.Drawing.Size(112, 25)
        Me.lblProveedor.TabIndex = 3
        Me.lblProveedor.Text = "Proveedor"
        Me.lblProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnElegirProveedor
        '
        Me.btnElegirProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirProveedor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirProveedor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirProveedor.Location = New System.Drawing.Point(190, 49)
        Me.btnElegirProveedor.Name = "btnElegirProveedor"
        Me.btnElegirProveedor.Size = New System.Drawing.Size(34, 25)
        Me.btnElegirProveedor.TabIndex = 2
        Me.btnElegirProveedor.Text = "..."
        Me.btnElegirProveedor.UseVisualStyleBackColor = true
        '
        'txtPROVE
        '
        Me.txtPROVE.Location = New System.Drawing.Point(123, 49)
        Me.txtPROVE.Name = "txtPROVE"
        Me.txtPROVE.Size = New System.Drawing.Size(67, 21)
        Me.txtPROVE.TabIndex = 1
        Me.txtPROVE.Tag = Nothing
        '
        'lblNumeroPedido
        '
        Me.lblNumeroPedido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNumeroPedido.Location = New System.Drawing.Point(11, 108)
        Me.lblNumeroPedido.Name = "lblNumeroPedido"
        Me.lblNumeroPedido.Size = New System.Drawing.Size(112, 25)
        Me.lblNumeroPedido.TabIndex = 6
        Me.lblNumeroPedido.Text = "Numero"
        Me.lblNumeroPedido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFecha
        '
        Me.lblFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFecha.Location = New System.Drawing.Point(11, 138)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(112, 24)
        Me.lblFecha.TabIndex = 7
        Me.lblFecha.Text = "Data"
        Me.lblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkESTOC
        '
        Me.chkESTOC.BackColor = System.Drawing.Color.Transparent
        Me.chkESTOC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkESTOC.Location = New System.Drawing.Point(605, 20)
        Me.chkESTOC.Name = "chkESTOC"
        Me.chkESTOC.Size = New System.Drawing.Size(145, 27)
        Me.chkESTOC.TabIndex = 0
        Me.chkESTOC.Text = "Sumat a l'Stock"
        Me.chkESTOC.UseVisualStyleBackColor = false
        Me.chkESTOC.Visible = false
        '
        'txtOBSERV
        '
        Me.txtOBSERV.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtOBSERV.Location = New System.Drawing.Point(784, 30)
        Me.txtOBSERV.Multiline = true
        Me.txtOBSERV.Name = "txtOBSERV"
        Me.txtOBSERV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOBSERV.Size = New System.Drawing.Size(345, 192)
        Me.txtOBSERV.TabIndex = 0
        Me.txtOBSERV.Tag = Nothing
        '
        'lblObservaciones
        '
        Me.lblObservaciones.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblObservaciones.Location = New System.Drawing.Point(784, 10)
        Me.lblObservaciones.Name = "lblObservaciones"
        Me.lblObservaciones.Size = New System.Drawing.Size(207, 18)
        Me.lblObservaciones.TabIndex = 176
        Me.lblObservaciones.Text = "Observacions"
        Me.lblObservaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTraspasado
        '
        Me.lblTraspasado.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTraspasado.Location = New System.Drawing.Point(1098, 10)
        Me.lblTraspasado.Name = "lblTraspasado"
        Me.lblTraspasado.Size = New System.Drawing.Size(184, 18)
        Me.lblTraspasado.TabIndex = 179
        Me.lblTraspasado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotal
        '
        Me.lblTotal.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTotal.Location = New System.Drawing.Point(0, 0)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(100, 23)
        Me.lblTotal.TabIndex = 0
        '
        'gbEleccionClientesPAF
        '
        Me.gbEleccionClientesPAF.Controls.Add(Me.lblNumeroPAF)
        Me.gbEleccionClientesPAF.Controls.Add(Me.cboSeleccionProveParaPAF)
        Me.gbEleccionClientesPAF.Controls.Add(Me.rdoVerTodasPAF)
        Me.gbEleccionClientesPAF.Controls.Add(Me.rdoVerPAFDeCliente)
        Me.gbEleccionClientesPAF.Location = New System.Drawing.Point(11, 10)
        Me.gbEleccionClientesPAF.Name = "gbEleccionClientesPAF"
        Me.gbEleccionClientesPAF.Size = New System.Drawing.Size(976, 59)
        Me.gbEleccionClientesPAF.TabIndex = 210
        Me.gbEleccionClientesPAF.TabStop = false
        Me.gbEleccionClientesPAF.Text = "Escollir Proveïdors PAF"
        '
        'lblNumeroPAF
        '
        Me.lblNumeroPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNumeroPAF.Location = New System.Drawing.Point(747, 25)
        Me.lblNumeroPAF.Name = "lblNumeroPAF"
        Me.lblNumeroPAF.Size = New System.Drawing.Size(213, 28)
        Me.lblNumeroPAF.TabIndex = 2
        '
        'cboSeleccionProveParaPAF
        '
        Me.cboSeleccionProveParaPAF.AutoCompletion = true
        Me.cboSeleccionProveParaPAF.AutoSelect = true
        Me.cboSeleccionProveParaPAF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboSeleccionProveParaPAF.Caption = ""
        Me.cboSeleccionProveParaPAF.CaptionHeight = 17
        Me.cboSeleccionProveParaPAF.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboSeleccionProveParaPAF.ColumnCaptionHeight = 17
        Me.cboSeleccionProveParaPAF.ColumnFooterHeight = 17
        Me.cboSeleccionProveParaPAF.ContentHeight = 18
        Me.cboSeleccionProveParaPAF.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSeleccionProveParaPAF.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboSeleccionProveParaPAF.Images.Add(CType(resources.GetObject("cboSeleccionProveParaPAF.Images"),System.Drawing.Image))
        Me.cboSeleccionProveParaPAF.LimitToList = true
        Me.cboSeleccionProveParaPAF.Location = New System.Drawing.Point(356, 25)
        Me.cboSeleccionProveParaPAF.MatchEntryTimeout = CType(100,Long)
        Me.cboSeleccionProveParaPAF.MaxDropDownItems = CType(5,Short)
        Me.cboSeleccionProveParaPAF.MaxLength = 32767
        Me.cboSeleccionProveParaPAF.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboSeleccionProveParaPAF.Name = "cboSeleccionProveParaPAF"
        Me.cboSeleccionProveParaPAF.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboSeleccionProveParaPAF.Size = New System.Drawing.Size(363, 23)
        Me.cboSeleccionProveParaPAF.TabIndex = 0
        Me.cboSeleccionProveParaPAF.PropBag = resources.GetString("cboSeleccionProveParaPAF.PropBag")
        '
        'rdoVerTodasPAF
        '
        Me.rdoVerTodasPAF.Checked = true
        Me.rdoVerTodasPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoVerTodasPAF.Location = New System.Drawing.Point(6, 25)
        Me.rdoVerTodasPAF.Name = "rdoVerTodasPAF"
        Me.rdoVerTodasPAF.Size = New System.Drawing.Size(174, 24)
        Me.rdoVerTodasPAF.TabIndex = 0
        Me.rdoVerTodasPAF.TabStop = true
        Me.rdoVerTodasPAF.Text = "Tots els proveïdors"
        '
        'rdoVerPAFDeCliente
        '
        Me.rdoVerPAFDeCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.rdoVerPAFDeCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoVerPAFDeCliente.Location = New System.Drawing.Point(202, 25)
        Me.rdoVerPAFDeCliente.Name = "rdoVerPAFDeCliente"
        Me.rdoVerPAFDeCliente.Size = New System.Drawing.Size(162, 24)
        Me.rdoVerPAFDeCliente.TabIndex = 1
        Me.rdoVerPAFDeCliente.Text = "Del proveïdor"
        '
        'btnImprimir
        '
        Me.btnImprimir.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimir.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnImprimir.Location = New System.Drawing.Point(842, 633)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(100, 26)
        Me.btnImprimir.TabIndex = 0
        Me.btnImprimir.Text = "Imprimir"
        Me.btnImprimir.UseVisualStyleBackColor = true
        '
        'TabControlPAF
        '
        Me.TabControlPAF.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TabControlPAF.Controls.Add(Me.tabPagePAF)
        Me.TabControlPAF.Controls.Add(Me.TabPage1)
        Me.TabControlPAF.ItemSize = New System.Drawing.Size(42, 18)
        Me.TabControlPAF.Location = New System.Drawing.Point(6, 69)
        Me.TabControlPAF.Name = "TabControlPAF"
        Me.TabControlPAF.SelectedIndex = 0
        Me.TabControlPAF.Size = New System.Drawing.Size(1151, 543)
        Me.TabControlPAF.TabIndex = 178
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(1143, 517)
        Me.TabPage1.TabIndex = 1
        Me.TabPage1.Text = "Impresió"
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCancelar.Location = New System.Drawing.Point(842, 660)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(100, 26)
        Me.btnCancelar.TabIndex = 211
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = true
        '
        'ToolBarButton1
        '
        Me.ToolBarButton1.Name = "ToolBarButton1"
        Me.ToolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'ToolBarButton2
        '
        Me.ToolBarButton2.Name = "ToolBarButton2"
        Me.ToolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'ToolBarButton3
        '
        Me.ToolBarButton3.Name = "ToolBarButton3"
        Me.ToolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'ToolBarButton4
        '
        Me.ToolBarButton4.Name = "ToolBarButton4"
        Me.ToolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'ToolBarButton5
        '
        Me.ToolBarButton5.Name = "ToolBarButton5"
        Me.ToolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'ToolBarButton6
        '
        Me.ToolBarButton6.Name = "ToolBarButton6"
        Me.ToolBarButton6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'ToolBarButton7
        '
        Me.ToolBarButton7.Name = "ToolBarButton7"
        Me.ToolBarButton7.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'ToolBarButton8
        '
        Me.ToolBarButton8.Name = "ToolBarButton8"
        Me.ToolBarButton8.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'ToolBarButton9
        '
        Me.ToolBarButton9.Name = "ToolBarButton9"
        Me.ToolBarButton9.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'ToolBarButton10
        '
        Me.ToolBarButton10.Name = "ToolBarButton10"
        Me.ToolBarButton10.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'ToolBarButton11
        '
        Me.ToolBarButton11.Name = "ToolBarButton11"
        Me.ToolBarButton11.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'ToolBarButton12
        '
        Me.ToolBarButton12.Name = "ToolBarButton12"
        Me.ToolBarButton12.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'frmCompraPAFPlantilla
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 16)
        Me.ClientSize = New System.Drawing.Size(1159, 711)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.TabControlPAF)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.gbEleccionClientesPAF)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.KeyPreview = true
        Me.Name = "frmCompraPAFPlantilla"
        Me.Controls.SetChildIndex(Me.gbEleccionClientesPAF, 0)
        Me.Controls.SetChildIndex(Me.btnImprimir, 0)
        Me.Controls.SetChildIndex(Me.TabControlPAF, 0)
        Me.Controls.SetChildIndex(Me.btnCancelar, 0)
        Me.Controls.SetChildIndex(Me.cboSeleccionCentro, 0)
        Me.Controls.SetChildIndex(Me.sbtipo, 0)
        Me.Controls.SetChildIndex(Me.btnActualizar, 0)
        Me.Controls.SetChildIndex(Me.btnNuevo, 0)
        Me.Controls.SetChildIndex(Me.btnBorrar, 0)
        Me.Controls.SetChildIndex(Me.btnTancar, 0)
        Me.Controls.SetChildIndex(Me.btnUltimo, 0)
        Me.Controls.SetChildIndex(Me.btnPrimero, 0)
        Me.Controls.SetChildIndex(Me.btnAnterior, 0)
        Me.Controls.SetChildIndex(Me.btnSiguiente, 0)
        Me.Controls.SetChildIndex(Me.btnRecargar, 0)
        Me.Controls.SetChildIndex(Me.btnVerLista, 0)
        Me.Controls.SetChildIndex(Me.btnModificar, 0)
        CType(Me.btnRecargar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnSiguiente,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnAnterior,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnPrimero,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnUltimo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnModificar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnTancar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnBorrar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnNuevo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnActualizar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnVerLista,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabPagePAF.ResumeLayout(false)
        CType(Me.cboMedida,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboArticle,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboColor,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboKM,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgDetalle,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtRE1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtIVA1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtTOTAL,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtBASE1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtDTE1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtBRUT1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        CType(Me.cboORDENFABCOMP,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboNOMTALLER,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtTALLER,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnElegirTaller,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtCOMANDAMOSTRA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboFRA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtP_DTE,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtpDATA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtpDATAENTREGA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtNRO,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboNOMPROVE,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnElegirProveedor,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtPROVE,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtOBSERV,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbEleccionClientesPAF.ResumeLayout(false)
        Me.gbEleccionClientesPAF.PerformLayout
        CType(Me.cboSeleccionProveParaPAF,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnImprimir,System.ComponentModel.ISupportInitialize).EndInit
        Me.TabControlPAF.ResumeLayout(false)
        CType(Me.btnCancelar,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

#End Region

#Region "VARIABLES"

        Public PAFActual As clsPAFCompra
        Dim fcargando As frCargando

#End Region

#Region "INICIALIZACION"

        Private Sub INCPedidoTejido()
            Try
                'btnNuevo.Text = "Nova Comanda"
                'btnBorrar.Text = "Borrar Comanda"
                tabPagePAF.Text = rm.GetString("PEDIDO")
                gbEleccionClientesPAF.Text = "Escollir Comandes"
                Me.Text = rm.GetString("PEDIDOCOMPRATEJIDO")
                PonerColores(System.Drawing.Color.Silver.ToArgb)

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub INCOrdenMuestras()
            Try
                Me.Text = rm.GetString("ORDENCOMPLEMENTO")
                'btnNuevo.Text = "Nou ordre fabricació"
                'btnBorrar.Text = "Borrar ordre"
                tabPagePAF.Text = "Ordre"
                ' rdoVerPAFDeCliente.Text = "Veure Comandes Fils de Proveïdor:"
                lblProveedor.Text = "Taller"
                Me.Text = "Ordres fabricació"
                gbEleccionClientesPAF.Text = "El·lecció ordres"
                lblProveedor.Text = rm.GetString("TALLER")
                dgDetalle.Caption = rm.GetString("LINEASORDENFAB")
                'Nos muesta de que pedido se ha generado esta orden de fabricacion si es que la hay
                txtCOMANDAMOSTRA.Visible = True
                lblPedidoMuestra.Visible = True
                'txtBRUT1.Visible = False
                'txtBASE1.Visible = False
                'txtIVA1.Visible = False
                'txtRE1.Visible = False
                'txtDTE1.Visible = False
                'txtNRO.Visible = False
                'txtTOTAL.Visible = False

                'lblBase.Visible = False
                'lblTotalBruto.Visible = False
                'lblDescuento.Visible = False
                'lblNumeroPedido.Visible = False
                'lblRE.Visible = False
                'lblIVA.Visible = False
                'lblTotal.Visible = False
                'txtP_DTE.Visible = False

                dtpDATAENTREGA.Visible = True
                lblFechaEntrega.Visible = True

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub INCPedidoMuestras()
            Try
                Me.Text = rm.GetString("PEDIDOCOMPRAHILO")
                'btnNuevo.Text = "Nova Comanda"
                'btnBorrar.Text = rm.GetString("BORRARPEDIDO")
                tabPagePAF.Text = rm.GetString("PEDIDO")
                'rdoVerPAFDeCliente.Text = "Veure Comandes Fils de Proveïdor:"
                gbEleccionClientesPAF.Text = "El·lecció comandes"
                PonerColores(CType(221, Byte), CType(202, Byte), CType(217, Byte))

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub INCPedidoFornituras()
            Try
                Me.Text = rm.GetString("PEDIDOFORNITURA")
                'btnNuevo.Text = "Nova Comanda"
                'btnBorrar.Text = "Borrar"
                tabPagePAF.Text = rm.GetString("PEDIDO")
                gbEleccionClientesPAF.Text = "El·lecció Comandes"

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub INCAlbaranEntregaHilos()
            Try
                Me.Text = rm.GetString("ALBARANESENTREGAHILOS")
                'btnNuevo.Text = "Nou Albarà"
                'btnBorrar.Text = "Borrar Albarà"
                tabPagePAF.Text = "Albarà Entrega Fil"
                Text = "Albarans d'Entrega fils"
                gbEleccionClientesPAF.Text = "El·lecció Albarans"
                cboNOMTALLER.Visible = True
                lblTaller.Visible = True
                txtTALLER.Visible = True
                lblOrdenFabComplemento.Visible = True
                cboORDENFABCOMP.Visible = True
                PonerColores(CType(176, Byte), CType(206, Byte), CType(172, Byte))

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub IniciarSegunTipoDocumento()
            Try
                Select Case PAFActual.DOCUMENT
                    Case Pedido
                        Select Case PAFActual.TIPUS
                            Case Tejido : INCPedidoTejido()
                            Case Muestra : INCPedidoMuestras()
                            Case Fornitura : INCPedidoFornituras()
                        End Select
                    Case Albaran
                        Select Case PAFActual.TIPUS
                            Case "M" : INCAlbaranEntregaHilos()
                                'Case "T" : IniciarNumeroColumnasTejidoAlbaran()
                        End Select
                    Case OrdenFabComplementos
                        'Es un orden de fabricacion de momento solo muestras
                        Select Case PAFActual.TIPUS
                            Case Muestra : INCOrdenMuestras()
                        End Select
                End Select

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub

        Friend Sub IniciarDocumento(ByVal tipo as string, ByVal documento as string)
            Try
                'En el caso de los ordenes de fabricacion va hacia un taller
                TabControlPAF.TabPages.Remove(TabPage1)
                cargando = True

                ACN()
                PAFActual = New clsPAFCompra(ds.Tables("cactur"), empresaPorDefecto, BindingContext, DOCUMENTO, TIPO)
                'Dim d As New aura2k3.ds22
                'd.Tables.Add(PAFActual.dvForm.Table.Copy)
                'd.Tables.Add(PAFActual.lineasCompra.dvForm.Table.Copy)

                IniciarSegunTipoDocumento()
                HacerBindings()

                'CargarNumerosPAF()
                cboSeleccionProveParaPAF.Visible = False
                'lblNumeroPAF.Visible = False

                PonerModificables(soloLectura)
                MoverActual()
                CCN()

                PSBTIPO(PAFActual.centro)
                If PAFActual.TIPUS = Muestra Then
                    PAFActual.lineasCompra.IniciarDesplegableArticulo()
                End If

                AñadirColumnas()

                OcultarTodasColumnaCbo(cboNOMPROVE)
                PonerVisibleColumnaCBO(cboNOMPROVE, "NOM", 1)
                AutoSizeCC(cboNOMPROVE)
                cargando = False

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

#End Region

#Region "ESPECIFICO"

#Region "PAFCOMPRA"

        Private Sub PonerControlesNuevo(ByVal b As Boolean)
            Try
                If b Then : btnActualizar.Text = rm.GetString("ACTUALIZAR")
                Else : btnActualizar.Text = rm.GetString("CONFIRMAR") : End If

                ModiNuev(b)

                cboFRA.AutoCompletion = Not editando

                txtBASE1.Visible = b
                lblBase.Visible = b

                txtBRUT1.Visible = b
                lblTotalBruto.Visible = b

                txtDTE1.Visible = b
                lblDescuento.Visible = b

                txtIVA1.Visible = b
                lblIVA.Visible = b

                txtRE1.Visible = b
                lblRE.Visible = b

                txtTOTAL.Visible = b
                lblTotal.Visible = b

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnNuevo.Click
            Try
                cargando = True
                EsRegistroNuevo = True

                If PAFActual.mProveSel <> -1 Then CambioEleccionTodosPAF()
                cboSeleccionProveParaPAF.Visible = False
                rdoVerPAFDeCliente.Checked = False
                rdoVerTodasPAF.Checked = True

                dgDetalle.Visible = False
                gbEleccionClientesPAF.Visible = False

                PonerControlesNuevo(False)
                PonerModificables(modificable)

                PAFActual.NuevoRegistro()

                chkESTOC.Checked = False

                MoverActual(True)

                PSBTIPO(PAFActual.centro) : cargando = False

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub
        Private Sub HacerBindings()
            Try
                HacerBindingsTodos(PAFActual.dvForm)

                AñadirBinding(cboFRA, PAFActual.dvForm, "FRA")
                AñadirBindingCombo(cboFRA, PAFActual.dvIdentificadores, "FRA", "FRA")
                'OcultarMostrarColumnaCbo(cboFRA, "CENTRO", False)

                PAFActual.dtProve.DefaultView.Sort = "NOM"
                AñadirBindingCombo(cboNOMPROVE, PAFActual.dtProve, CCProve, CNProve)
                If PAFActual.DOCUMENT = Albaran AndAlso PAFActual.TIPUS = Hilos Then
                    'Estamos ante un albaran de venta
                    AñadirBindingCombo(cboNOMTALLER, PAFActual.dtTallers, CCTallers, CNTallers)
                    AñadirBindingCombo(cboORDENFABCOMP, PAFActual.dtOrdeneFabComplementos, "FRA", "FRA")
                End If

                dgDetalle.SetDataBinding(PAFActual.lineasCompra.dvForm, "")

                'chkSumadoEstoc.DataBindings.Add(New Binding("Checked", ds, "dcactu.ESTOC"))

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub

#End Region

#Region "DETALLE PAF"

        Private Sub dgDetalle_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgDetalle.AfterColUpdate
            Dim columna As String = e.Column.DataColumn.DataField
            Try
                If PuedoModificar() Then
                    cargando = True
                    dgDetalle.UpdateData()
                    PAFActual.lineasCompra.HacerCalculosLineasPAF(columna)
                    cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False
            End Try
        End Sub
        Private Sub dgDetalle_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgDetalle.RowColChange
            Try
                If PuedoModificar() Then
                    cargando = True
                    If e.LastRow <> dgDetalle.Row Then
                        PAFActual.lineasCompra.CambioFila()
                        TratarCambioFila()
                    End If

                    AutosizeColumnasTrueDBDropdown(cboArticle)
                    AutosizeColumnasTrueDBDropdown(cboColor)
                    AutosizeColumnasTrueDBDropdown(cboKM)

                    AutoSizeCC(dgDetalle)

                    cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub dgDetalle_FetchCellTips(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.FetchCellTipsEventArgs) Handles dgDetalle.FetchCellTips
            Try
                Select Case e.Column.DataColumn.DataField

                    Case "ELEGIRTEJIDO"
                        Select Case PAFActual.TIPUS
                            Case Tejido
                                e.CellTip = rm.GetString("TTESCOJERTEJIDO")
                            Case Muestra
                                e.CellTip = rm.GetString("TTESCOJERHILO")
                        End Select
                    Case "RECEPCION"
                        e.CellTip = rm.GetString("TTTRASPASORECEPCION")

                End Select

            Catch ex As Exception
                LOG(ex.ToString, True) : cargando = False
            End Try
        End Sub
        Private Sub dgDetalle_ButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgDetalle.ButtonClick
            Dim cambios As Boolean = False
            Try
                If PuedoModificar() Then
                    cargando = True
                    'If dgDetalle(dgDetalle.Row, "TIPUS") <> "" Then 'AndAlso PAFActual.lineasCompra.dvForm.Count <> 0 Then
                    Select Case e.Column.DataColumn.DataField

                        Case "ELEGIRTEJIDO"
                            dgDetalle.EditActive = True
                            Select Case PAFActual.TIPUS
                                Case "T" : cambios = EscogerTejido()
                                Case "M" : cambios = EscogerHilo()
                                Case "F" : cambios = EscogerFornitura()
                            End Select
                            If cambios Then PAFActual.lineasCompra.HacerCalculosLineasPAF(e.Column.DataColumn.DataField)

                        Case "RECEPCION"
                            'dgDetalle.EditActive = True
                            PAFActual.TraspasarLinea()
                    End Select

                    cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub dgDetalle_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgDetalle.GotFocus
            Try
                If PuedoModificar() Then
                    cargando = True
                    If PAFActual.TRASPAS = True And dgDetalle.Enabled = True Then
                        MessageBox.Show(rm.GetString("NOTRASPLINIASDOCTRAS"))
                    End If
                    cargando = False
                End If
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub AñadirColumnas()
            Dim i As Integer
            Try
                OcultarColumnasDG(dgDetalle)
                i = 0
                Select Case PAFActual.TIPUS
                    Case Tejido
                        PPCol2("ARTICLE", dgDetalle, rm.GetString("TEJIDO"), "", True, 100, False, PresentationEnum.ComboBox, False, 100, i, False, cboArticle)
                    Case Muestra
                        If PAFActual.DOCUMENT = OrdenFabComplementos Then
                            PPCol2("ARTICLE", dgDetalle, rm.GetString("COMPLEMENTO"), "", True, 100, False, PresentationEnum.ComboBox, False, 100, i, False, cboArticle)
                        Else
                            PPCol2("ARTICLE", dgDetalle, rm.GetString("HILO"), "", True, 100, False, PresentationEnum.ComboBox, False, 100, i, False, cboArticle)
                        End If
                    Case Fornitura
                        PPCol2("ARTICLE", dgDetalle, rm.GetString("FORNITURA"), "", True, 100, False, PresentationEnum.ComboBox, False, 100, i, False, cboArticle)
                End Select

                AñadirBindingCombo(cboArticle, PAFActual.lineasCompra.dtArticulos, "CODI", "")

                i = i + 1
                PPCol2("ELEGIRTEJIDO", dgDetalle, "", "", True, 20, False, PresentationEnum.Normal, True, 20, i, False)
                PAFActual.lineasCompra.tabla.Columns("ELEGIRTEJIDO").DefaultValue = ""

                i = i + 1
                PPCol2("DESCRI", dgDetalle, rm.GetString("DESCRIPCION"), "", True, 300, False, PresentationEnum.Normal, False, 300, i, False)

                i = i + 1
                PPCol2("COLOR", dgDetalle, rm.GetString("COLOR"), "", True, 100, False, PresentationEnum.ComboBox, False, 100, i, False, cboColor)
                
                AñadirBindingCombo(cboColor, PAFActual.lineasCompra.dtColores, "COLOR", "COLOR")

                If PAFActual.TIPUS = Tejido Then
                    i = i + 1
                    PPCol2("KM", dgDetalle, "K/M", "", True, 50, False, PresentationEnum.ComboBox, False, 50, i, False, cboKM)
                    AñadirBindingCombo(cboKM, PAFActual.lineasCompra.dtKM, "NOMKM", "KM")
                End If

                'Ordenes de fabricacion de muestras
                If PAFActual.DOCUMENT = "O" AndAlso PAFActual.TIPUS = "M" Then
                    i = i + 1
                    PPCol2("TALLA", dgDetalle, rm.GetString("TALLA"), "", True, 45, False, PresentationEnum.Normal, False, 45, i, False)
                End If

                If PAFActual.TIPUS = Fornitura Then
                    i = i + 1
                    'En talla tenemos las medias cuando hablamos de fornituras 
                    PPCol2("TALLA", dgDetalle, rm.GetString("MEDIDA"), "", True, 45, False, PresentationEnum.Normal, False, 45, i, False, cboMedida)
                    AñadirBindingCombo(cboMedida, PAFActual.lineasCompra.dtMedidas, "MEDIDA", "MEDIDA")
                End If

                i = i + 1
                PPCol2("UNITATS", dgDetalle, rm.GetString("UNIDADES"), "#,##0.00", True, 45, False, PresentationEnum.Normal, False, 45, i, False, Nothing, True, 0, AlignHorzEnum.Far)

                If PAFActual.DOCUMENT = "C" AndAlso (PAFActual.TIPUS = "M" OrElse PAFActual.TIPUS = Tejido OrElse PAFActual.TIPUS = Fornitura) Then
                    i = i + 1
                    Dim S As New C1.Win.C1TrueDBGrid.Style

                    S.BackColor = Color.PaleGreen : S.Font = New Font(dgDetalle.Font, FontStyle.Bold)
                    PPCol2("PERREBRE", dgDetalle, rm.GetString("PORRECIBIR"), "#,##0.00", True, 50, False, PresentationEnum.Normal, False, 50, i, False, Nothing, True, 0, AlignHorzEnum.Far, S, "^[1-9\-]")
                    i = i + 1

                    S = New C1.Win.C1TrueDBGrid.Style
                    S.BackColor = Color.PaleVioletRed : S.Font = New Font(dgDetalle.Font, FontStyle.Bold)
                    '-[0\.[1-9]
                    PPCol2("REBUT", dgDetalle, rm.GetString("RECIBIDO"), "#,##0.00", True, 50, False, PresentationEnum.Normal, False, 50, i, False, Nothing, True, 0, AlignHorzEnum.Far, S, "^[1-9\-]")

                    i = i + 1
                    PPCol2("RECEPCION", dgDetalle, rm.GetString("RECEPCION"), "", True, 35, False, PresentationEnum.Normal, True, 35, i, False)
                End If

                If PAFActual.DOCUMENT <> OrdenFabComplementos Then
                    i = i + 1
                    PPCol2("PREU", dgDetalle, rm.GetString("PRECIO"), "#,##0.00", True, 50, False, PresentationEnum.Normal, False, 50, i, False, Nothing, True, 0, AlignHorzEnum.Far)

                    i = i + 1
                    PPCol2("DTE", dgDetalle, rm.GetString("DTE"), "#,##0.00", True, 50, False, PresentationEnum.Normal, False, 50, i, False, Nothing, True, 0, AlignHorzEnum.Far)

                End If
                i = i + 1
                PPCol2("IMPORT", dgDetalle, rm.GetString("IMPORTE"), "#,##0.00", True, 50, False, PresentationEnum.Normal, False, 50, i, False, Nothing, True, 0, AlignHorzEnum.Far)

                i = i + 1

                'PPCol2("NLINEA", dgDetalle, rm.GetString("LINEA"), "", True, 55, False, PresentationEnum.Normal, False, 55, i, False)
                'PPCol2("CENTRO", dgDetalle, "CENT", "", True, 55, False, PresentationEnum.Normal, False, 55, i, False)

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub TratarCambioFila()
            Try
                'Asociamos las tablas adecuadas y ajustamos el tamaño

                AñadirBindingCombo(cboColor, PAFActual.lineasCompra.dtColores, "COLOR", "COLOR")
                AñadirBindingCombo(cboKM, PAFActual.lineasCompra.dtKM, "NOMKM", "KM")

                AutoSizeCC(dgDetalle)

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False
            End Try
        End Sub

#End Region

#Region "ESTOCS"

        '!!! ESTO SE DEBERÍA HACER EN LA CLASE
        'En cantidad tenemos lo que tenemos que sumar de estoc. En paf.lineascompra.KG tenemos si son kilos o metros
        'El rendimiento del tejido se saca con el dlookup
        Private Function SumarQuitarDispuesto(ByVal MASMENOS As String, ByVal cantidad As Double, ByVal articulo As String, ByVal km As String)
            Dim METROS As Double
            Dim KILOS As Double
            Dim REND As Double
            Dim cmd As New MySqlCommand
            Try
                '.STCRUK = roundnum(.STCRUM / .RENDIMENT, 2)
                '.STCRUM = roundnum(.STCRUK * .RENDIMENT, 2)
                'Hay que sumar al color del tejido que tiene esta disposicion los kg de la linea
                Dim rendimientoTejido As Double = DLookUp("RENDIMENT", "TEIXITS", " CODI = """ & articulo & """ ")
                If rendimientoTejido = 0 Then : REND = 1
                Else : REND = rendimientoTejido : End If
                'Aquí disponemos del rendimiento del tejido

                Select Case km.ToUpper
                    Case "K"
                        'Esta representado en kilos
                        METROS = roundnum(cantidad * REND, 2)
                        KILOS = cantidad
                    Case "M"
                        METROS = cantidad
                        KILOS = roundnum(cantidad / REND, 2)
                        'Esta representado en metros
                End Select


                cmd = New MySqlCommand("UPDATE TEIXITS " & _
                            " SET STCRUK = STCRUK " & MASMENOS & """" & general.NS(KILOS, True) & """, " & _
                            " STCRUM = STCRUM " & MASMENOS & """" & general.NS(METROS, True) & """ " & _
                            " WHERE CODI = """ & articulo & """ ", cnn)


                cmd.ExecuteNonQuery()

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Function
        Private Sub ActualizarStockTejidos()
            Dim i As Integer
            Dim cmdSel As New MySqlCommand
            Dim cmdUpd As New MySqlCommand
            Dim strSQL As String
            Dim stock As Double
            Try
                cmdSel.Connection = cnn
                cmdUpd.Connection = cnn
                ACN()
                For i = 0 To PAFActual.lineasCompra.dvForm.Count - 1
                    '!!!!!!!!!!!!!!!!!!!!!!!!!!
                    If PAFActual.TIPUS = Tejido Then
                        If PAFActual.lineasCompra.dvForm.Item(i).Item("COLOR") = "**CRU**" Then
                            If chkESTOC.Checked = False Then
                                SumarQuitarDispuesto("-", PAFActual.lineasCompra.dvForm.Item(i).Item("UNITATS"), PAFActual.lineasCompra.dvForm.Item(i).Item("ARTICLE"), PAFActual.lineasCompra.dvForm.Item(i).Item("KM"))
                            Else
                                SumarQuitarDispuesto("+", PAFActual.lineasCompra.dvForm.Item(i).Item("UNITATS"), PAFActual.lineasCompra.dvForm.Item(i).Item("ARTICLE"), PAFActual.lineasCompra.dvForm.Item(i).Item("KM"))
                            End If
                            GoTo JUER
                        Else
                            strSQL = "SELECT ACTUAL FROM " & tablaHiloColores & " WHERE(FIL = """ & PAFActual.lineasCompra.dvForm(i).Item("ARTICLE") & """ AND TIPUS = 'T' AND COLOR = """ & PAFActual.lineasCompra.dvForm(i).Item("COLOR") & """)"
                        End If
                    Else
                        strSQL = "SELECT ACTUAL FROM " & tablaHiloColores & " WHERE(FIL = """ & PAFActual.lineasCompra.dvForm(i).Item("ARTICLE") & """ AND TIPUS = 'F' AND COLOR = """ & PAFActual.lineasCompra.dvForm(i).Item("COLOR") & """)"
                    End If
                    cmdSel.CommandText = strSQL

                    If chkESTOC.Checked = False Then : stock = cmdSel.ExecuteScalar() - PAFActual.lineasCompra.dvForm.Item(i).Item("UNITATS")
                    Else : stock = cmdSel.ExecuteScalar() + PAFActual.lineasCompra.dvForm.Item(i).Item("UNITATS") : End If

                    If PAFActual.TIPUS = Tejido Then
                        strSQL = "UPDATE " & tablaHiloColores & " SET ACTUAL = """ & general.NS(stock, True) & """ WHERE(FIL = """ & general.NS(PAFActual.lineasCompra.dvForm(i).Item("ARTICLE")) & """ AND TIPUS = 'T' AND COLOR = """ & general.NS(PAFActual.lineasCompra.dvForm(i).Item("COLOR")) & """)"
                    Else
                        strSQL = "UPDATE " & tablaHiloColores & " SET ACTUAL = """ & general.NS(stock, True) & """ WHERE(FIL = """ & general.NS(PAFActual.lineasCompra.dvForm(i).Item("ARTICLE")) & """ AND TIPUS = 'F' AND COLOR = """ & general.NS(PAFActual.lineasCompra.dvForm(i).Item("COLOR")) & """)"
                    End If
                    cmdUpd.CommandText = strSQL
                    cmdUpd.ExecuteNonQuery()
JUER:           Next
                CCN()

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub chkSumadoEstoc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkESTOC.CheckedChanged
            Try
                actualizarchecked()
                'ActualizarOrigen(tablaLineasCompras, daLineasPedido)
                ActualizarStockTejidos()

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub
        Private Sub ActualitzarEstocs()
            Try

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

#End Region

#End Region

#Region "DESPLAZAMIENTO"

        Private Sub MoverActual(Optional ByVal nuevo As Boolean = False)
            Try
                cargando = True
                ActualizarTraspasado(nzn(cboFRA.Text, 0), "TRASPAS")
                PAFActual.lineasCompra.HacerCalculosLineasPAF("")
                AñadirBindingCombo(cboArticle, PAFActual.lineasCompra.dtArticulos, "CODI", "CODI")
                PSBTIPO(PAFActual.centro) : cargando = False

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) ''Handles btnSiguiente.Click
            Try
                cargando = True
                desplazando = True
                cargando = True
                PAFActual.SiguienteReg() : ActualizarOrigen()
                MoverActual()
                desplazando = False
                PSBTIPO(PAFActual.centro) : cargando = False

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUltimo.Click
            Try
                cargando = True
                desplazando = True
                PAFActual.UltimoReg() : ActualizarOrigen()
                MoverActual()
                desplazando = False
                PSBTIPO(PAFActual.centro) : cargando = False

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub
        Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAnterior.Click
            Try
                cargando = True
                desplazando = True
                PAFActual.AnteriorReg() : ActualizarOrigen()
                MoverActual()
                desplazando = False
                PSBTIPO(PAFActual.centro) : cargando = False

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnPrimero.Click
            Try
                cargando = True
                desplazando = True
                PAFActual.PrimeroReg() : ActualizarOrigen()
                MoverActual()
                desplazando = False
                PSBTIPO(PAFActual.centro) : cargando = False
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

#End Region

#Region "MODIFICAR ORIGEN"

        Private Sub PonerModificables(Optional ByVal b As Boolean = False)
            Try
                PonerMod(b)
                gbEleccionClientesPAF.Visible = b
                If Not b Then : cboFRA.DataSource = Nothing
                Else : AñadirBindingCombo(cboFRA, PAFActual.dvIdentificadores, "FRA", "FRA") : PAFActual.tabla.AcceptChanges() : End If
                cboFRA.LimitToList = b
                cboFRA.SuperBack = b
                cboFRA.ReadOnly = editando Or EsRegistroNuevo
                ModiNuev(b)

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub ActualizarOrigen()
            Try
                If editando Then
                    editando = False
                    PonerModificables(soloLectura)
                End If
                If EsRegistroNuevo Then
                    PonerControlesNuevo(True)
                    MoverActual()
                    dgDetalle.Visible = True
                    gbEleccionClientesPAF.Visible = True
                    editando = True
                    PSBTIPO(PAFActual.centro)
                    PAFActual.actualizarNumeraciones()
                    EsRegistroNuevo = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Friend Overrides Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnActualizar.Click
            Try
                cargando = True
                If PAFActual.PROVE = 0 Then
                    MessageBox.Show(rm.GetString("FALTAPROVE"), rm.GetString("INFORMACION"), MessageBoxButtons.OK)
                    cargando = False
                    Exit Sub
                End If
                If PAFActual.TieneCambios Then
                    Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                        Case DialogResult.Cancel
                            cargando = False
                            Exit Sub
                        Case DialogResult.No
                            PAFActual.tabla.RejectChanges()
                            PAFActual.lineasCompra.tabla.RejectChanges()
                            cargando = False
                            Exit Sub
                    End Select
                End If
                PAFActual.ActualizarOrigen() : ActualizarOrigen()
                PSBTIPO(PAFActual.centro)
                cargando = False

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub
        Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnBorrar.Click
            Try
                If MessageBox.Show(rm.GetString("BorrarActual"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    editando = True
                    cargando = True
                    'Primero borramos el tejido de la tabla tejido
                    PAFActual.borrar()
                    MoverActual()

                    AñadirBindingCombo(cboFRA, PAFActual.dvIdentificadores, "FRA", "FRA")
                    PAFActual.tabla.AcceptChanges()

                    editando = False
                    cargando = False
                    PSBTIPO(PAFActual.centro)
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

#End Region

#Region "IMPRIMIR"

        Private Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
            Try
                PAFActual.ImprimirPAF()

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

#End Region

#Region "SELECCION REGISTRO"

        Private Sub VerPAFDeProveedor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoVerPAFDeCliente.CheckedChanged
            Try
                'Hay que rellenar el combo donde podemos elegir al cliente
                If consulta() Then
                    cargando = True
                    If rdoVerPAFDeCliente.Checked = True Then
                        cboSeleccionProveParaPAF.Visible = True
                        AñadirBindingCombo(cboSeleccionProveParaPAF, PAFActual.dtProveConPAF, CCProve, CNProve)
                        cboSeleccionProveParaPAF.Focus()
                        lblNumeroPAF.Visible = True
                    Else
                        Select Case PAFActual.DOCUMENT
                            Case Pedido
                                lblNumeroPAF.Text = rm.GetString("NUMPEDIDOS") & " " & PAFActual.dvIdentificadores.Count
                            Case Albaran
                                lblNumeroPAF.Text = rm.GetString("NUMALBARANES") & " " & PAFActual.dvIdentificadores.Count
                            Case Factura
                                lblNumeroPAF.Text = rm.GetString("NUMFACTURAS") & " " & PAFActual.dvIdentificadores.Count
                            Case Proforma
                                lblNumeroPAF.Text = rm.GetString("NUMPROFORMA") & " " & PAFActual.dvIdentificadores.Count
                        End Select
                        cboSeleccionProveParaPAF.Visible = False
                        lblNumeroPAF.Visible = False
                    End If
                    cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub cboSeleccionProveParaPAF_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboSeleccionProveParaPAF.Validating
            Try
                If consulta() Then
                    cargando = True
                    PAFActual.ElegirProve(nzn(cboSeleccionProveParaPAF.WillChangeToValue, 0))
                    AñadirBindingCombo(cboFRA, PAFActual.dvIdentificadores, "FRA", "FRA")
                    PAFActual.tabla.AcceptChanges()

                    Select Case PAFActual.DOCUMENT
                        Case Pedido
                            lblNumeroPAF.Text = rm.GetString("NUMPEDIDOS") & " " & PAFActual.dvIdentificadores.Count
                        Case Albaran
                            lblNumeroPAF.Text = rm.GetString("NUMALBARANES") & " " & PAFActual.dvIdentificadores.Count
                        Case Factura
                            lblNumeroPAF.Text = rm.GetString("NUMFACTURAS") & " " & PAFActual.dvIdentificadores.Count
                        Case Proforma
                            lblNumeroPAF.Text = rm.GetString("NUMPROFORMA") & " " & PAFActual.dvIdentificadores.Count
                    End Select
                    MoverActual()
                    cargando = False

                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Function obtenercodigoProveedor(ByVal nombrecliente As String) As Integer
            Try
                Dim cmdSel As New MySqlCommand("SELECT codi FROM prove WHERE (NOM = """ & general.ponercontrabarrasreal(nombrecliente) & """)", cnn)
                ACN()
                Dim i As Integer = cmdSel.ExecuteScalar
                CCN()
                Return i
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Function
        Private Sub CambioEleccionTodosPAF()
            Try
                PAFActual.ElegirProve(-1)
                PAFActual.numeroRegistros = PAFActual.obtenerNumeroReg(PAFActual.tabla.TableName, "")
                PAFActual.tabla.AcceptChanges()

                AutoSizeCC(cboFRA)

                Select Case PAFActual.DOCUMENT
                    Case Pedido
                        lblNumeroPAF.Text = rm.GetString("NUMPEDIDOS") & " " & PAFActual.dvIdentificadores.Count
                    Case Albaran
                        lblNumeroPAF.Text = rm.GetString("NUMALBARANES") & " " & PAFActual.dvIdentificadores.Count
                    Case Factura
                        lblNumeroPAF.Text = rm.GetString("NUMFACTURAS") & " " & PAFActual.dvIdentificadores.Count
                    Case Proforma
                        lblNumeroPAF.Text = rm.GetString("NUMPROFORMA") & " " & PAFActual.dvIdentificadores.Count
                End Select

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Private Sub rdoVerTodasPAF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoVerTodasPAF.CheckedChanged
            Try
                If consulta() Then
                    cargando = True
                    If rdoVerTodasPAF.Checked = True Then
                        CambioEleccionTodosPAF()
                        Select Case PAFActual.DOCUMENT
                            Case Pedido
                                lblNumeroPAF.Text = rm.GetString("NUMPEDIDOS") & " " & PAFActual.dvIdentificadores.Count
                            Case Albaran
                                lblNumeroPAF.Text = rm.GetString("NUMALBARANES") & " " & PAFActual.dvIdentificadores.Count
                            Case Factura
                                lblNumeroPAF.Text = rm.GetString("NUMFACTURAS") & " " & PAFActual.dvIdentificadores.Count
                            Case Proforma
                                lblNumeroPAF.Text = rm.GetString("NUMPROFORMA") & " " & PAFActual.dvIdentificadores.Count
                        End Select
                    End If
                    cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

#End Region

#Region "ELEGIR REGISTRO"

        Private Sub cboID_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboFRA.KeyPress
            Try
                If editando Then : cboFRA.AutoCompletion = False
                Else : cboFRA.AutoCompletion = True : End If

            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        End Sub
        Private Sub cboCodigoPAF_ItemChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboFRA.Validating
            Try
                If consulta() Then
                    cargando = True
                    CargandoFormulario()
                    PAFActual.CambiarAReg(nzn(cboFRA.WillChangeToValue, 0), iraregistro)

                    MoverActual()
                    PSBTIPO(PAFActual.centro) : cargando = False
                    fcargando.Close()
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

#End Region

#Region "ELECCIONES"

        Private Sub txtTALLER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTALLER.Validated
            Try
                If PuedoModificar() Then
                    cargando = True
                    PAFActual.TALLER = nzn(txtTALLER.Text, 0)
                    PSBTIPO(PAFActual.centro) : cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub txtCodigoProveedor_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPROVE.Validated
            Try
                If PuedoModificar() Then
                    cargando = True
                    PAFActual.PROVE = nzn(txtPROVE.Text, 0)
                    cboNOMPROVE.SelectedValue = PAFActual.PROVE
                    PAFActual.PonerDatosProve()
                    If PAFActual.TIPUS = Muestra Then
                        PAFActual.lineasCompra.IniciarDesplegableArticulo()
                        AñadirBindingCombo(cboArticle, PAFActual.lineasCompra.dtArticulos, "CODI", "CODI")
                    End If
                    PSBTIPO(PAFActual.centro) : cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub btnElegirProveedor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirProveedor.Click

            Try
                If PAFActual.DOCUMENT = OrdenFabComplementos Then
                    Cursor = Cursors.WaitCursor
                    Dim f As frmTalleresLista = frmTalleresLista.GetInstance(esEleccion)
                    f.ShowDialog()
                    If Not f.dr Is Nothing Then
                        PAFActual.PROVE = f.dr("CODI")
                        PAFActual.PonerDatosProve()
                    End If
                    Cursor = Cursors.Default
                    f = Nothing
                Else
                    Cursor = Cursors.WaitCursor
                    Dim f As frmProveedoresLista = frmProveedoresLista.GetInstance(esEleccion)
                    f.ShowDialog()
                    If Not f.dr Is Nothing Then
                        PAFActual.PROVE = f.dr("CODI")
                        PAFActual.PonerDatosProve()
                    End If
                    If PAFActual.TIPUS = Muestra Then
                        PAFActual.lineasCompra.IniciarDesplegableArticulo()
                        AñadirBindingCombo(cboArticle, PAFActual.lineasCompra.dtArticulos, "CODI", "CODI")
                    End If
                    f = Nothing
                    Cursor = Cursors.Default
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Function EscogerTejido() As Boolean
            Try
                Cursor = Cursors.WaitCursor
                Dim f As frmTejidosLista = frmTejidosLista.GetInstance(esEleccion, PAFActual.centro)
                f.ShowDialog()
                Cursor = Cursors.Default
                If Not f.dr Is Nothing Then
                    PAFActual.lineasCompra.ARTICLE = f.dr("CODI")
                    PAFActual.lineasCompra.DESCRI = f.dr("DESCRI")
                    f = Nothing
                    Return True
                Else
                    f = Nothing
                    Return False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Function
        Private Function EscogerHilo() As Boolean
            Try
                Cursor = Cursors.WaitCursor
                Dim f As frmHilosLista = frmHilosLista.GetInstance(esEleccion, PAFActual.centro)
                f.prove = PAFActual.PROVE
                f.ShowDialog()
                Cursor = Cursors.Default
                If Not f.dr Is Nothing Then
                    PAFActual.lineasCompra.ARTICLE = f.dr("CODI")
                    PAFActual.lineasCompra.DESCRI = f.dr("DESCRI")
                    f = Nothing
                    Return True
                Else
                    f = Nothing
                    Return False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Function
        Private Function EscogerFornitura() As Boolean
            Try
                Cursor = Cursors.WaitCursor
                Dim f As frmForniturasLista = frmForniturasLista.GetInstance(esEleccion, PAFActual.centro)
                f.ShowDialog()
                Cursor = Cursors.Default
                If Not f.dr Is Nothing Then
                    PAFActual.lineasCompra.ARTICLE = f.dr("CODI")
                    PAFActual.lineasCompra.DESCRI = f.dr("DESCRI")
                    f = Nothing
                    Return True
                Else
                    f = Nothing
                    Return False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Function
        Private Sub cboNOMPROVE_Cambio(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMPROVE.SelectedValueChanged
            Try
                If PuedoModificar() Then
                    cargando = True
                    With cboNOMPROVE
                        PAFActual.PROVE = nzn(cboNOMPROVE.WillChangeToValue, 0)
                        PAFActual.PonerDatosProve()
                        If PAFActual.TIPUS = "M" Then
                            PAFActual.lineasCompra.IniciarDesplegableArticulo()
                            AñadirBindingCombo(cboArticle, PAFActual.lineasCompra.dtArticulos, "CODI", "CODI")
                        End If
                        PSBTIPO(PAFActual.centro)
                    End With
                    cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub
        'El taller al que se van a llevar los hilos para hacer muestras o modelos.
        Private Sub cboNOMTALLER_Cambio(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMTALLER.SelectedValueChanged
            Try
                If PuedoModificar() Then
                    With cboNOMTALLER
                        If (EsRegistroNuevo Or editando) And Not cargando Then
                            cargando = True
                            PAFActual.TALLER = nzn(cboNOMTALLER.WillChangeToValue, 0)
                            PSBTIPO(PAFActual.centro)
                            cargando = False
                        End If
                    End With
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
#End Region

#Region "ORGANIZAR"

        Private Sub ActualizarTraspasado(ByVal codigoabuscar As Integer, ByVal campo As String)

            Try
                If PAFActual.DOCUMENT = "F" Then

                    Dim drNew As System.Data.DataRow
                    Dim cm As CurrencyManager = Me.BindingContext(ds, tabla)
                    Dim dsrc As DataView
                    dsrc = CType(cm.List, DataView)
                    dsrc.Sort = "FRA"
                    Dim idx As Integer = dsrc.Find(codigoabuscar)
                    If Not idx = -1 Then
                        If Not IsDBNull(PAFActual.tabla.Rows(idx).Item(campo)) Then
                            If (PAFActual.tabla.Rows(idx).Item(campo) = "S") Then : lblTraspasado.Text = "TRASPASAT"
                            Else : lblTraspasado.Text = "NO TRASPASAT"
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub
        Private Sub actualizarchecked()
            Try
                Dim i As Integer
                If chkESTOC.Checked = False Then
                    For i = 0 To PAFActual.lineasCompra.dvForm.Count - 1
                        PAFActual.lineasCompra.dvForm(i).Item("ESTOC") = 0
                        'MessageBox.Show(i)
                    Next
                Else
                    For i = 0 To PAFActual.lineasCompra.dvForm.Count - 1
                        PAFActual.lineasCompra.dvForm(i).Item("ESTOC") = 1
                        'MessageBox.Show(i)
                    Next
                End If
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub
        Private Sub Validando_TantoPorCiento(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtP_DTE.Validating
            Try
                If (txtP_DTE.Text > 100 Or txtP_DTE.Text < 0) Then
                    MessageBox.Show(rm.GetString("TANTOPORCIENTOINVALIDO"))
                    e.Cancel = True
                Else
                    'PAFActual.ActualizarTotalesPAF()
                End If
                'AceptarCambios(tabla)
            Catch ex As Exception

            End Try

        End Sub
        Private Sub btnRecargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecargar.Click
            Try
                cargando = True
                Dim id As Object
                id = cboFRA.Text
                PAFActual.ActualizarOrigen()
                PAFActual.CambiarAReg(id, iraregistro)
                MoverActual()
                cargando = False

            Catch ex As Exception
                LOG(ex.ToString) : CCN() : cargando = False
            End Try
        End Sub
        Friend Overrides Sub btnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnModificar.Click
            Try
                If Not editando Then
                    cargando = True
                    editando = True
                    CambioEleccionTodosPAF()
                    cboSeleccionProveParaPAF.Visible = False
                    rdoVerPAFDeCliente.Checked = False
                    rdoVerTodasPAF.Checked = True

                    PonerModificables(modificable)
                    cargando = False
                    PSBTIPO(PAFActual.centro)
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
            Try
                If PAFActual.tabla.GetChanges Is Nothing Then
                    Close()
                Else
                    Close()
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False
            End Try
        End Sub
        Private Sub dgdetalle_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgDetalle.BeforeDelete
            Try
                If MessageBox.Show(rm.GetString("BORRARLINEA"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Private Sub frmCompraPAFPlantilla_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
            Try
                GrabaParametro(PAFActual.FRA, "" & PAFActual.DOCUMENT & PAFActual.TIPUS & "C" & PAFActual.centro & "")

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Friend Sub CargandoFormulario()
            Try

                fcargando = New frCargando
                fcargando.Show()
                fcargando.Refresh()
                fcargando.BringToFront()

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN() : cnn.Close()
            End Try
        End Sub
        Private Sub PonerColores(ByVal rgb As Integer)
            Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(rgb)
            Me.tabPagePAF.BackColor = System.Drawing.Color.FromArgb(rgb)
            Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(rgb)
            Me.gbEleccionClientesPAF.BackColor = System.Drawing.Color.FromArgb(rgb)
            Me.gbEleccionClientesPAF.BackColor = System.Drawing.Color.FromArgb(rgb)
            Me.BackColor = System.Drawing.Color.FromArgb(rgb)
            Me.lblPedido.BackColor = System.Drawing.Color.FromArgb(rgb)

        End Sub
        Private Sub PonerColores(ByVal r As Byte, ByVal g As Byte, ByVal b As Byte)

            Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(176, Byte), CType(206, Byte), CType(172, Byte))
            Me.tabPagePAF.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.gbEleccionClientesPAF.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(b, Byte), CType(b, Byte))
            Me.gbEleccionClientesPAF.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.lblPedido.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))

        End Sub
        Private Sub TabControlPAF_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControlPAF.SelectedIndexChanged
            Try
                If Not Me.DesignMode Then
                    If Not cargando And Not EsRegistroNuevo Then
                        If PAFActual.TieneCambios Then
                            Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                                Case DialogResult.Cancel
                                    cargando = False
                                Case DialogResult.No
                                    PAFActual.tabla.RejectChanges()
                                    PAFActual.lineasCompra.tabla.RejectChanges()
                                    cargando = False
                                Case DialogResult.Yes
                                    PAFActual.ActualizarOrigen()
                            End Select
                        End If
                    End If
                End If
                'If TabControlPAF.SelectedTab Is TabPage1 Then
                '    Dim Report As New rptCompraPedidoFornituras(PAFActual.FRA, PAFActual.FRA, PAFActual.PROVE, PAFActual.PROVE, PAFActual.TIPUS, PAFActual.DOCUMENT, PAFActual.centro)
                '    ' assigning a report to the print control
                '    PrintControl1.PrintingSystem = Report.PrintingSystem
                '    ' creating the report document
                '    Report.CreateDocument()
                'End If

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub

#End Region

#Region "LISTADO"

        Private Sub btnVerLista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerLista.Click
            Try
                Cursor = Cursors.WaitCursor
                Dim f As frmPAFLista = frmPAFLista.GetInstance(PAFActual.TIPUS, PAFActual.DOCUMENT, PAFActual.centro, tablaCompras)

                f.MdiParent = Me.MdiParent
                f.Show()
                f.BringToFront()
                Cursor = Cursors.Default

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

#End Region

#Region "SELECCION EMPRESA"
        Private Sub cboSeleccion_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
                                                          Handles cboSeleccionCentro.SelectionChangeCommitted
            Try
                If consulta() Then
                    cargando = True
                    PAFActual.cambioCentro(GENERAL.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto), ultimo)
                    'AñadirBindingCombo(cboFRA, PAFActual.dvIdentificadores, "FRA", "FRA")
                    'Aqui hay que hacer un moveractual
                    'CargarNumerosPAF()
                    cboSeleccionCentro.SelectedValue = PAFActual.centro
                    cargando = False
                Else
                    If EsRegistroNuevo And Not cargando Then
                        cargando = True
                        PAFActual.centro = general.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto)
                        PAFActual.tabla.Clear()
                        PAFActual.NuevoRegistro()
                        PSBTIPO(PAFActual.centro)
                    End If
                    cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : CCN() : cargando = False
            End Try
        End Sub

#End Region

        Private Sub frmCompraPAFPlantilla_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.arrayCombos = New System.Windows.Forms.Control() {Me.cboNOMPROVE, Me.cboNOMTALLER, Me.cboORDENFABCOMP}
            Me.arrayTextBox = New System.Windows.Forms.Control() {Me.txtBASE1, Me.txtBRUT1, Me.txtCOMANDAMOSTRA, Me.txtDTE1, Me.txtIVA1, Me.txtNRO, Me.txtOBSERV, Me.txtPROVE, Me.txtP_DTE, Me.txtRE1, Me.txtTALLER, Me.txtTOTAL}
            Me.arrayGrids = New System.Windows.Forms.Control() {Me.dgDetalle}
            Me.arrayBotones = New System.Windows.Forms.Control() {Me.btnElegirProveedor, Me.btnElegirTaller}
            Me.arrayCheckBox = New System.Windows.Forms.Control() {Me.chkESTOC}
            Me.arrayDateTime = New System.Windows.Forms.Control() {Me.dtpDATA, Me.dtpDATAENTREGA}
            Me.arrayTabPages = New System.Windows.Forms.Control() {Me.tabPagePAF}
           Me.arrayGrids = New System.Windows.Forms.Control() {Me.dgDetalle}

        End Sub
    End Class

End Namespace