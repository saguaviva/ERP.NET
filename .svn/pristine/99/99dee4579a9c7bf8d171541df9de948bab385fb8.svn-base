Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones
Imports C1.Win.C1TrueDBGrid
Imports System.Collections.Generic

Namespace aura2k3

    Public Class frmVentaPAFPlantilla
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

        Friend WithEvents btnImprimir As C1.Win.C1Input.C1Button
        Friend WithEvents btnImprimirEtiquetas As C1.Win.C1Input.C1Button
        Friend WithEvents dtpFechaPAFC As DateTimePicker
        Friend WithEvents dgVencim As C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Friend WithEvents txtAPAF As C1.Win.C1Input.C1TextBox
        Friend WithEvents txtDetalleImpresion As C1.Win.C1Input.C1TextBox
        Friend WithEvents txtAFecha As C1.Win.C1Input.C1DateEdit
        Friend WithEvents date1 As C1.Win.C1Input.C1DateEdit
        Friend WithEvents date2 As C1.Win.C1Input.C1DateEdit
        Friend WithEvents dtvDATA As C1.Win.C1Input.C1DateEdit
        Friend WithEvents lblDeCliente As System.Windows.Forms.Label
        Friend WithEvents lblDePAF As System.Windows.Forms.Label
        Friend WithEvents lblDeFecha As System.Windows.Forms.Label
        Friend WithEvents lblHastaCliente As System.Windows.Forms.Label
        Friend WithEvents lblHastaFecha As System.Windows.Forms.Label
        Friend WithEvents lblHastaPAF As System.Windows.Forms.Label
        Friend WithEvents lblAPAF As System.Windows.Forms.Label
        Friend WithEvents btnTraspasarAPAF As C1.Win.C1Input.C1Button
        Friend WithEvents lblConFecha As System.Windows.Forms.Label
        Friend WithEvents cmbCliente2 As C1.Win.C1List.C1Combo
        Friend WithEvents cmbCliente1 As C1.Win.C1List.C1Combo
        Friend WithEvents cmbPAF2 As C1.Win.C1List.C1Combo
        Friend WithEvents cmbPAF1 As C1.Win.C1List.C1Combo
        Friend WithEvents btnGenerarPedidoCompraHilosDesdeMuestra As C1.Win.C1Input.C1Button
        Friend WithEvents lblNumeracion As System.Windows.Forms.Label
        Friend WithEvents lblFacturaV As System.Windows.Forms.Label
        Friend WithEvents btnTraspasarAProforma As C1.Win.C1Input.C1Button
        Friend WithEvents dgTraspas As C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Friend WithEvents lblClientePAFV As System.Windows.Forms.Label
        Friend WithEvents lblFechaPAFV As System.Windows.Forms.Label
        Friend WithEvents btnHacerTraspaso As C1.Win.C1Input.C1Button
        Friend WithEvents lblFormaParte As System.Windows.Forms.Label
        Friend WithEvents lblVieneDe As System.Windows.Forms.Label
        Friend WithEvents btnAbrirAF As C1.Win.C1Input.C1Button
        Friend WithEvents lstFormaParte As System.Windows.Forms.ListBox
        Friend WithEvents btnAbrirPAF As C1.Win.C1Input.C1Button
        Friend WithEvents lstVieneDePedido As System.Windows.Forms.ListBox
        Friend WithEvents txvFRA As C1.Win.C1Input.C1TextBox
        Friend WithEvents txvNOMCLIENT As C1.Win.C1Input.C1TextBox
        Friend WithEvents btnGenerarVencimientos As C1.Win.C1Input.C1Button
        Friend WithEvents lblComandaMostra As System.Windows.Forms.Label
        Friend WithEvents txtORDRE As C1.Win.C1Input.C1TextBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents dtrDATA As C1.Win.C1Input.C1DateEdit
        Friend WithEvents txrNOMCLIENT As C1.Win.C1Input.C1TextBox
        Friend WithEvents txrFRA As C1.Win.C1Input.C1TextBox
        Friend WithEvents lbrFRA As System.Windows.Forms.Label
        Friend WithEvents lblPedidCliente As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents cboAPAFIMPRESION As C1.Win.C1List.C1Combo
        Friend WithEvents cboDEPAFIMPRESION As C1.Win.C1List.C1Combo
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents lblDia3 As System.Windows.Forms.Label
        Friend WithEvents lblDia2 As System.Windows.Forms.Label
        Friend WithEvents lblDia1 As System.Windows.Forms.Label
        Friend WithEvents lblDia_3 As System.Windows.Forms.Label
        Friend WithEvents lblDia_2 As System.Windows.Forms.Label
        Friend WithEvents lblDia_1 As System.Windows.Forms.Label
        Friend WithEvents lblRE2 As System.Windows.Forms.Label
        Friend WithEvents lblRE_ As System.Windows.Forms.Label
        Friend WithEvents lblIVA2 As System.Windows.Forms.Label
        Friend WithEvents lblIVA_ As System.Windows.Forms.Label
        Friend WithEvents txvCLIENT As C1.Win.C1Input.C1TextBox
        Friend WithEvents lblFORMA As System.Windows.Forms.Label
        Friend WithEvents lblFormaDePago As System.Windows.Forms.Label
        Protected WithEvents tabPageTraspasoPAF As System.Windows.Forms.TabPage
        Protected WithEvents tabPageVencimientos As System.Windows.Forms.TabPage
        Protected WithEvents tabPageImpresionPAF As System.Windows.Forms.TabPage
        Protected WithEvents tabPageRecibos As System.Windows.Forms.TabPage
        Protected WithEvents tabPagePAF As System.Windows.Forms.TabPage
        Protected WithEvents tabPageConsumo As System.Windows.Forms.TabPage
        Protected WithEvents gbVencimPAF As System.Windows.Forms.GroupBox
        Protected WithEvents gbVencim As System.Windows.Forms.GroupBox
        Protected WithEvents GroupBox3 As System.Windows.Forms.GroupBox
        Protected WithEvents gbCentro As System.Windows.Forms.GroupBox
        Friend WithEvents gbPAF As System.Windows.Forms.GroupBox
        Friend WithEvents gbTraspasarLista As System.Windows.Forms.GroupBox
        Friend WithEvents dateDesdeFecha As C1.Win.C1Input.C1DateEdit
        Friend WithEvents lblDesdeFecha As System.Windows.Forms.Label
        Friend WithEvents gbTraspasarIntervalo As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Protected Friend WithEvents dgConsumos As C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Protected Friend WithEvents txCFRA As C1.Win.C1Input.C1TextBox
        Protected Friend WithEvents txtCLIENT As C1.Win.C1Input.C1TextBox
        Protected Friend WithEvents txCNOMCLIENT As C1.Win.C1Input.C1TextBox
        Protected Friend WithEvents txtP_DTE As C1.Win.C1Input.C1TextBox
        Protected Friend WithEvents txtCOMIS As C1.Win.C1Input.C1TextBox
        Protected Friend WithEvents txtREPRES As C1.Win.C1Input.C1TextBox
        Protected Friend WithEvents dtpDATA As C1.Win.C1Input.C1DateEdit
        Protected Friend WithEvents rdoVerTodasPAF As System.Windows.Forms.RadioButton
        Protected Friend WithEvents rdoVerPAFDeCliente As System.Windows.Forms.RadioButton
        Protected Friend WithEvents cboSeleccionClienteParaPAF As C1.Win.C1List.C1Combo
        Protected Friend WithEvents lblClienteConsumos As System.Windows.Forms.Label
        Protected Friend WithEvents lblNumeroPAF As System.Windows.Forms.Label
        Protected Friend WithEvents lblFechaPAF As System.Windows.Forms.Label
        Protected Friend WithEvents lblFecha As System.Windows.Forms.Label
        Protected Friend WithEvents lblTantoPorCientoDescuento As System.Windows.Forms.Label
        Protected Friend WithEvents lblCliente As System.Windows.Forms.Label
        Protected Friend WithEvents lblRepresentante As System.Windows.Forms.Label
        Protected Friend WithEvents btnElegirCliente As C1.Win.C1Input.C1Button
        Protected Friend WithEvents btnElegirRepresentate As C1.Win.C1Input.C1Button
        Protected Friend WithEvents lblComisio As System.Windows.Forms.Label
        Protected Friend WithEvents cboNOMCLIENT As C1.Win.C1List.C1Combo
        Protected Friend WithEvents cboFRA As C1.Win.C1List.C1Combo
        Protected Friend WithEvents cboNOMREPRES As C1.Win.C1List.C1Combo
        Protected Friend WithEvents lblImpReciboAFecha As System.Windows.Forms.Label
        Protected Friend WithEvents lblImpReciboDeFecha As System.Windows.Forms.Label
        Protected Friend WithEvents lblDeFactura As System.Windows.Forms.Label
        Protected Friend WithEvents lblAFactura As System.Windows.Forms.Label
        Protected Friend WithEvents cboDeFacuraRecibo As C1.Win.C1List.C1Combo
        Protected Friend WithEvents cboAFacturaRecibo As C1.Win.C1List.C1Combo
        Protected Friend WithEvents dtpAFechaRecibo As C1.Win.C1Input.C1DateEdit
        Protected Friend WithEvents dtpDeFechaRecibo As C1.Win.C1Input.C1DateEdit
        Protected Friend WithEvents btnGenerarRecibo As C1.Win.C1Input.C1Button
        Protected Friend WithEvents dtcDATA As C1.Win.C1Input.C1DateEdit
        Protected Friend WithEvents txtALBCLI As C1.Win.C1Input.C1TextBox
        Protected Friend WithEvents lblPedidoCliente As System.Windows.Forms.Label
        Protected Friend WithEvents chkCondicionFecha As System.Windows.Forms.CheckBox
        Protected Friend WithEvents cboDOM As C1.Win.C1List.C1Combo
        Protected Friend WithEvents lblDireccionDeEnvio As System.Windows.Forms.Label
        Protected Friend WithEvents txcCLIENT As C1.Win.C1Input.C1TextBox
        Protected Friend WithEvents lblTotalBruto As System.Windows.Forms.Label
        Protected Friend WithEvents lblObservaciones As System.Windows.Forms.Label
        Protected Friend WithEvents chkTRASPAS As System.Windows.Forms.CheckBox
        Protected Friend WithEvents gb As System.Windows.Forms.GroupBox
        Protected Friend WithEvents gbEleccionClientesPAF As System.Windows.Forms.GroupBox
        Protected Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
        Protected Friend WithEvents tabControlPAFVentas As System.Windows.Forms.TabControl
        Protected Friend WithEvents txtOBSERV As C1.Win.C1Input.C1TextBox
        Protected Friend WithEvents txtBASE1 As C1.Win.C1Input.C1TextBox
        Protected Friend WithEvents txtIVA1 As C1.Win.C1Input.C1TextBox
        Protected Friend WithEvents txtRE1 As C1.Win.C1Input.C1TextBox
        Protected WithEvents txtDTE1 As C1.Win.C1Input.C1TextBox
        Protected Friend WithEvents lblDescuento As System.Windows.Forms.Label
        Protected Friend WithEvents lblBase As System.Windows.Forms.Label
        Protected Friend WithEvents chkSumadoEstoc As System.Windows.Forms.CheckBox
        Protected Friend WithEvents lblRE As System.Windows.Forms.Label
        Protected Friend WithEvents lblIVA As System.Windows.Forms.Label
        Protected Friend WithEvents dgLineasPAFVenta As C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Protected Friend WithEvents cboMuestra As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Protected Friend WithEvents cboMuestra2 As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Protected Friend WithEvents cboKM As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Protected Friend WithEvents cboTipo As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Protected Friend WithEvents cboTalla As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Protected Friend WithEvents cboColor As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Protected Friend WithEvents lblPAF As System.Windows.Forms.Label
        Protected Friend WithEvents lblPedido As System.Windows.Forms.Label
        Protected Friend WithEvents txvTOTAL As C1.Win.C1Input.C1TextBox
        Protected Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents lstOrdenes As C1.Win.C1List.C1List
        Protected Friend WithEvents txtBRUT1 As C1.Win.C1Input.C1TextBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents chkFacturaInternacional As System.Windows.Forms.CheckBox
        Protected Friend WithEvents dgImpresion As C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Friend WithEvents lblInformacion As System.Windows.Forms.Label
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents cboSemanas As C1.Win.C1List.C1Combo
        Protected Friend WithEvents dgImpresion2 As C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Protected Friend WithEvents btnEliminarFilasSeleccionadas As C1.Win.C1Input.C1Button
        Friend WithEvents lblDeDocumento As System.Windows.Forms.Label
        Protected Friend WithEvents lblPOB As System.Windows.Forms.Label
        Protected Friend WithEvents lblPROV As System.Windows.Forms.Label
        Protected Friend WithEvents lblPAIS As System.Windows.Forms.Label
        Protected Friend WithEvents lblDOM As System.Windows.Forms.Label
        Protected Friend WithEvents cboBANCO As C1.Win.C1List.C1Combo
        Protected Friend WithEvents cboImpresion As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Friend WithEvents C1Report1 As C1.C1Report.C1Report
        Friend WithEvents Button1 As System.Windows.Forms.Button
        Friend WithEvents lblIncoterm As System.Windows.Forms.Label

        Friend WithEvents c1PreviewControl_leftSplitter As System.Windows.Forms.Splitter
        Friend WithEvents c1PreviewControl_rightSplitter As System.Windows.Forms.Splitter
        Friend WithEvents c1PreviewControl_fileStrip As System.Windows.Forms.ToolStrip
        Friend WithEvents c1PreviewControl_btnFileOpen As System.Windows.Forms.ToolStripSplitButton
        Friend WithEvents c1PreviewControl_btnFileSave As System.Windows.Forms.ToolStripButton
        Friend WithEvents c1PreviewControl_btnPageSetup As System.Windows.Forms.ToolStripButton
        Friend WithEvents c1PreviewControl_btnPrint As System.Windows.Forms.ToolStripButton
        Friend WithEvents c1PreviewControl_btnReflow As System.Windows.Forms.ToolStripButton
        Friend WithEvents ppfitxa2 As C1.Win.C1Preview.C1PrintPreviewControl
        Protected Friend WithEvents lblOBSERVOCULTA As Label
        Protected Friend WithEvents txtOBSERVOCULTA As C1.Win.C1Input.C1TextBox
        Friend WithEvents tlpPrincipal As TableLayoutPanel
        Friend WithEvents pnl1 As Panel
        Friend WithEvents tlpsuperior As TableLayoutPanel
        Friend WithEvents pnl11 As Panel
        Friend WithEvents pnl12 As Panel
        Friend WithEvents pnl2 As Panel
        Protected Friend WithEvents txtTOTAL As C1.Win.C1Input.C1TextBox
        Protected Friend WithEvents lblTotal As Label
        Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
        Friend WithEvents pnl21 As Panel
        Friend WithEvents pnl22 As Panel
        Friend WithEvents pnl23 As Panel
        Friend WithEvents tlpTab3 As TableLayoutPanel
        Friend WithEvents pnlTab31 As Panel
        Friend WithEvents pnlTab32 As Panel
        Friend WithEvents tlpTab6 As TableLayoutPanel
        Friend WithEvents pnlTab61 As Panel
        Friend WithEvents pnlTab62 As Panel
        Friend WithEvents tlpTab2 As TableLayoutPanel
        Friend WithEvents pnlTab21 As Panel
        Friend WithEvents pnlTAb22 As Panel
        Friend WithEvents cboIncoterm As C1.Win.C1List.C1Combo

        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVentaPAFPlantilla))
        Dim Style71 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style72 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style73 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style74 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style75 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style76 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style77 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style78 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style79 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style80 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style81 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style82 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style83 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style84 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style85 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style86 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style87 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style88 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style89 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style90 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style91 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style92 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style93 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style94 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style95 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style96 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style97 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style98 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style99 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style100 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style101 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style102 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style103 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style104 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style105 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style106 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style107 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style108 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style109 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style110 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style111 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style112 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style113 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style114 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style115 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style116 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style117 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style118 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style119 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style120 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style121 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style122 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style123 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style124 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style125 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style126 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style127 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style128 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style129 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style130 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style131 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style132 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style133 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style134 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style135 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style136 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style137 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style138 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style139 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style140 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.gbPAF = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblPROV = New System.Windows.Forms.Label()
        Me.cboNOMCLIENT = New C1.Win.C1List.C1Combo()
        Me.lblPAIS = New System.Windows.Forms.Label()
        Me.lblPOB = New System.Windows.Forms.Label()
        Me.lblDOM = New System.Windows.Forms.Label()
        Me.lblDireccionDeEnvio = New System.Windows.Forms.Label()
        Me.cboDOM = New C1.Win.C1List.C1Combo()
        Me.txtALBCLI = New C1.Win.C1Input.C1TextBox()
        Me.lblPedidoCliente = New System.Windows.Forms.Label()
        Me.lblPedidCliente = New System.Windows.Forms.Label()
        Me.dtpDATA = New C1.Win.C1Input.C1DateEdit()
        Me.txtCOMIS = New C1.Win.C1Input.C1TextBox()
        Me.txtP_DTE = New C1.Win.C1Input.C1TextBox()
        Me.cboFRA = New C1.Win.C1List.C1Combo()
        Me.lblPedido = New System.Windows.Forms.Label()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.lblRepresentante = New System.Windows.Forms.Label()
        Me.btnElegirCliente = New C1.Win.C1Input.C1Button()
        Me.lblTantoPorCientoDescuento = New System.Windows.Forms.Label()
        Me.cboNOMREPRES = New C1.Win.C1List.C1Combo()
        Me.lblComisio = New System.Windows.Forms.Label()
        Me.btnElegirRepresentate = New C1.Win.C1Input.C1Button()
        Me.txtREPRES = New C1.Win.C1Input.C1TextBox()
        Me.txtCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.lblObservaciones = New System.Windows.Forms.Label()
        Me.tabControlPAFVentas = New System.Windows.Forms.TabControl()
        Me.tabPagePAF = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.pnl21 = New System.Windows.Forms.Panel()
        Me.chkSumadoEstoc = New System.Windows.Forms.CheckBox()
        Me.chkTRASPAS = New System.Windows.Forms.CheckBox()
        Me.txtOBSERVOCULTA = New C1.Win.C1Input.C1TextBox()
        Me.txtOBSERV = New C1.Win.C1Input.C1TextBox()
        Me.lblOBSERVOCULTA = New System.Windows.Forms.Label()
        Me.pnl22 = New System.Windows.Forms.Panel()
        Me.cboTalla = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.cboMuestra = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.cboMuestra2 = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.cboKM = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.cboTipo = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.cboColor = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.dgLineasPAFVenta = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.pnl23 = New System.Windows.Forms.Panel()
        Me.txtTOTAL = New C1.Win.C1Input.C1TextBox()
        Me.txtBRUT1 = New C1.Win.C1Input.C1TextBox()
        Me.txtIVA1 = New C1.Win.C1Input.C1TextBox()
        Me.txtRE1 = New C1.Win.C1Input.C1TextBox()
        Me.txtBASE1 = New C1.Win.C1Input.C1TextBox()
        Me.txtDTE1 = New C1.Win.C1Input.C1TextBox()
        Me.btnEliminarFilasSeleccionadas = New C1.Win.C1Input.C1Button()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblTotalBruto = New System.Windows.Forms.Label()
        Me.lblBase = New System.Windows.Forms.Label()
        Me.lblDescuento = New System.Windows.Forms.Label()
        Me.lblIVA = New System.Windows.Forms.Label()
        Me.lblRE = New System.Windows.Forms.Label()
        Me.tabPageImpresionPAF = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.tlpTab2 = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlTab21 = New System.Windows.Forms.Panel()
        Me.cboImpresion = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.dgImpresion = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.pnlTAb22 = New System.Windows.Forms.Panel()
        Me.dgImpresion2 = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboSemanas = New C1.Win.C1List.C1Combo()
        Me.lblIncoterm = New System.Windows.Forms.Label()
        Me.cboIncoterm = New C1.Win.C1List.C1Combo()
        Me.lblInformacion = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblDeDocumento = New System.Windows.Forms.Label()
        Me.cboAPAFIMPRESION = New C1.Win.C1List.C1Combo()
        Me.cboDEPAFIMPRESION = New C1.Win.C1List.C1Combo()
        Me.btnImprimir = New C1.Win.C1Input.C1Button()
        Me.chkFacturaInternacional = New System.Windows.Forms.CheckBox()
        Me.tabPageRecibos = New System.Windows.Forms.TabPage()
        Me.ppfitxa2 = New C1.Win.C1Preview.C1PrintPreviewControl()
        Me.lstOrdenes = New C1.Win.C1List.C1List()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cboDeFacuraRecibo = New C1.Win.C1List.C1Combo()
        Me.chkCondicionFecha = New System.Windows.Forms.CheckBox()
        Me.lblImpReciboAFecha = New System.Windows.Forms.Label()
        Me.lblImpReciboDeFecha = New System.Windows.Forms.Label()
        Me.dtpAFechaRecibo = New C1.Win.C1Input.C1DateEdit()
        Me.dtpDeFechaRecibo = New C1.Win.C1Input.C1DateEdit()
        Me.lblAFactura = New System.Windows.Forms.Label()
        Me.lblDeFactura = New System.Windows.Forms.Label()
        Me.cboAFacturaRecibo = New C1.Win.C1List.C1Combo()
        Me.btnGenerarRecibo = New C1.Win.C1Input.C1Button()
        Me.tabPageTraspasoPAF = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtrDATA = New C1.Win.C1Input.C1DateEdit()
        Me.txrNOMCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbrFRA = New System.Windows.Forms.Label()
        Me.txrFRA = New C1.Win.C1Input.C1TextBox()
        Me.dateDesdeFecha = New C1.Win.C1Input.C1DateEdit()
        Me.gbTraspasarLista = New System.Windows.Forms.GroupBox()
        Me.tlpTab3 = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlTab31 = New System.Windows.Forms.Panel()
        Me.lblVieneDe = New System.Windows.Forms.Label()
        Me.dgTraspas = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.btnHacerTraspaso = New C1.Win.C1Input.C1Button()
        Me.btnAbrirPAF = New C1.Win.C1Input.C1Button()
        Me.lstVieneDePedido = New System.Windows.Forms.ListBox()
        Me.pnlTab32 = New System.Windows.Forms.Panel()
        Me.lstFormaParte = New System.Windows.Forms.ListBox()
        Me.lblFormaParte = New System.Windows.Forms.Label()
        Me.btnAbrirAF = New C1.Win.C1Input.C1Button()
        Me.gbTraspasarIntervalo = New System.Windows.Forms.GroupBox()
        Me.txtAFecha = New C1.Win.C1Input.C1DateEdit()
        Me.date2 = New C1.Win.C1Input.C1DateEdit()
        Me.date1 = New C1.Win.C1Input.C1DateEdit()
        Me.txtAPAF = New C1.Win.C1Input.C1TextBox()
        Me.cmbPAF1 = New C1.Win.C1List.C1Combo()
        Me.lblAPAF = New System.Windows.Forms.Label()
        Me.lblConFecha = New System.Windows.Forms.Label()
        Me.cmbPAF2 = New C1.Win.C1List.C1Combo()
        Me.btnTraspasarAPAF = New C1.Win.C1Input.C1Button()
        Me.cmbCliente1 = New C1.Win.C1List.C1Combo()
        Me.cmbCliente2 = New C1.Win.C1List.C1Combo()
        Me.btnTraspasarAProforma = New C1.Win.C1Input.C1Button()
        Me.lblDeFecha = New System.Windows.Forms.Label()
        Me.lblHastaCliente = New System.Windows.Forms.Label()
        Me.lblHastaFecha = New System.Windows.Forms.Label()
        Me.lblHastaPAF = New System.Windows.Forms.Label()
        Me.lblDePAF = New System.Windows.Forms.Label()
        Me.lblDeCliente = New System.Windows.Forms.Label()
        Me.lblDesdeFecha = New System.Windows.Forms.Label()
        Me.tabPageConsumo = New System.Windows.Forms.TabPage()
        Me.lblComandaMostra = New System.Windows.Forms.Label()
        Me.txtORDRE = New C1.Win.C1Input.C1TextBox()
        Me.dgConsumos = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.gb = New System.Windows.Forms.GroupBox()
        Me.txcCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.dtcDATA = New C1.Win.C1Input.C1DateEdit()
        Me.lblClienteConsumos = New System.Windows.Forms.Label()
        Me.lblFechaPAF = New System.Windows.Forms.Label()
        Me.lblPAF = New System.Windows.Forms.Label()
        Me.txCNOMCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.txCFRA = New C1.Win.C1Input.C1TextBox()
        Me.btnGenerarPedidoCompraHilosDesdeMuestra = New C1.Win.C1Input.C1Button()
        Me.tabPageVencimientos = New System.Windows.Forms.TabPage()
        Me.tlpTab6 = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlTab61 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblFormaDePago = New System.Windows.Forms.Label()
        Me.lblFORMA = New System.Windows.Forms.Label()
        Me.lblDia3 = New System.Windows.Forms.Label()
        Me.lblDia_3 = New System.Windows.Forms.Label()
        Me.lblDia1 = New System.Windows.Forms.Label()
        Me.lblDia_2 = New System.Windows.Forms.Label()
        Me.lblRE2 = New System.Windows.Forms.Label()
        Me.lblRE_ = New System.Windows.Forms.Label()
        Me.lblIVA2 = New System.Windows.Forms.Label()
        Me.lblIVA_ = New System.Windows.Forms.Label()
        Me.lblDia_1 = New System.Windows.Forms.Label()
        Me.lblDia2 = New System.Windows.Forms.Label()
        Me.cboBANCO = New C1.Win.C1List.C1Combo()
        Me.gbVencimPAF = New System.Windows.Forms.GroupBox()
        Me.txvCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.dtvDATA = New C1.Win.C1Input.C1DateEdit()
        Me.txvNOMCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.lblClientePAFV = New System.Windows.Forms.Label()
        Me.lblFechaPAFV = New System.Windows.Forms.Label()
        Me.lblFacturaV = New System.Windows.Forms.Label()
        Me.txvFRA = New C1.Win.C1Input.C1TextBox()
        Me.pnlTab62 = New System.Windows.Forms.Panel()
        Me.gbVencim = New System.Windows.Forms.GroupBox()
        Me.txvTOTAL = New C1.Win.C1Input.C1TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnGenerarVencimientos = New C1.Win.C1Input.C1Button()
        Me.dgVencim = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.c1PreviewControl_leftSplitter = New System.Windows.Forms.Splitter()
        Me.C1Report1 = New C1.C1Report.C1Report()
        Me.c1PreviewControl_rightSplitter = New System.Windows.Forms.Splitter()
        Me.c1PreviewControl_fileStrip = New System.Windows.Forms.ToolStrip()
        Me.c1PreviewControl_btnFileOpen = New System.Windows.Forms.ToolStripSplitButton()
        Me.c1PreviewControl_btnFileSave = New System.Windows.Forms.ToolStripButton()
        Me.c1PreviewControl_btnPageSetup = New System.Windows.Forms.ToolStripButton()
        Me.c1PreviewControl_btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.c1PreviewControl_btnReflow = New System.Windows.Forms.ToolStripButton()
        Me.gbCentro = New System.Windows.Forms.GroupBox()
        Me.lblNumeracion = New System.Windows.Forms.Label()
        Me.gbEleccionClientesPAF = New System.Windows.Forms.GroupBox()
        Me.cboSeleccionClienteParaPAF = New C1.Win.C1List.C1Combo()
        Me.lblNumeroPAF = New System.Windows.Forms.Label()
        Me.rdoVerTodasPAF = New System.Windows.Forms.RadioButton()
        Me.rdoVerPAFDeCliente = New System.Windows.Forms.RadioButton()
        Me.btnImprimirEtiquetas = New C1.Win.C1Input.C1Button()
        Me.tlpPrincipal = New System.Windows.Forms.TableLayoutPanel()
        Me.pnl1 = New System.Windows.Forms.Panel()
        Me.tlpsuperior = New System.Windows.Forms.TableLayoutPanel()
        Me.pnl11 = New System.Windows.Forms.Panel()
        Me.pnl12 = New System.Windows.Forms.Panel()
        Me.pnl2 = New System.Windows.Forms.Panel()
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
        Me.gbPAF.SuspendLayout
        CType(Me.cboNOMCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboDOM,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtALBCLI,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dtpDATA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtCOMIS,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtP_DTE,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboFRA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnElegirCliente,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboNOMREPRES,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnElegirRepresentate,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtREPRES,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tabControlPAFVentas.SuspendLayout
        Me.tabPagePAF.SuspendLayout
        Me.TableLayoutPanel1.SuspendLayout
        Me.pnl21.SuspendLayout
        CType(Me.txtOBSERVOCULTA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtOBSERV,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnl22.SuspendLayout
        CType(Me.cboTalla,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboMuestra,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboMuestra2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboKM,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboTipo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboColor,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dgLineasPAFVenta,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnl23.SuspendLayout
        CType(Me.txtTOTAL,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtBRUT1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtIVA1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtRE1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtBASE1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtDTE1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnEliminarFilasSeleccionadas,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tabPageImpresionPAF.SuspendLayout
        Me.GroupBox2.SuspendLayout
        Me.tlpTab2.SuspendLayout
        Me.pnlTab21.SuspendLayout
        CType(Me.cboImpresion,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dgImpresion,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlTAb22.SuspendLayout
        CType(Me.dgImpresion2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboSemanas,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboIncoterm,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboAPAFIMPRESION,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboDEPAFIMPRESION,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnImprimir,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tabPageRecibos.SuspendLayout
        CType(Me.ppfitxa2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ppfitxa2.PreviewPane,System.ComponentModel.ISupportInitialize).BeginInit
        Me.ppfitxa2.SuspendLayout
        CType(Me.lstOrdenes,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox4.SuspendLayout
        CType(Me.cboDeFacuraRecibo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dtpAFechaRecibo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dtpDeFechaRecibo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboAFacturaRecibo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnGenerarRecibo,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tabPageTraspasoPAF.SuspendLayout
        Me.GroupBox1.SuspendLayout
        CType(Me.dtrDATA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txrNOMCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txrFRA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dateDesdeFecha,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbTraspasarLista.SuspendLayout
        Me.tlpTab3.SuspendLayout
        Me.pnlTab31.SuspendLayout
        CType(Me.dgTraspas,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnHacerTraspaso,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnAbrirPAF,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlTab32.SuspendLayout
        CType(Me.btnAbrirAF,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbTraspasarIntervalo.SuspendLayout
        CType(Me.txtAFecha,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.date2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.date1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtAPAF,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cmbPAF1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cmbPAF2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnTraspasarAPAF,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cmbCliente1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cmbCliente2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnTraspasarAProforma,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tabPageConsumo.SuspendLayout
        CType(Me.txtORDRE,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dgConsumos,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gb.SuspendLayout
        CType(Me.txcCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dtcDATA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txCNOMCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txCFRA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnGenerarPedidoCompraHilosDesdeMuestra,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tabPageVencimientos.SuspendLayout
        Me.tlpTab6.SuspendLayout
        Me.pnlTab61.SuspendLayout
        Me.GroupBox3.SuspendLayout
        CType(Me.cboBANCO,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbVencimPAF.SuspendLayout
        CType(Me.txvCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dtvDATA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txvNOMCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txvFRA,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlTab62.SuspendLayout
        Me.gbVencim.SuspendLayout
        CType(Me.txvTOTAL,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnGenerarVencimientos,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dgVencim,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.C1Report1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.c1PreviewControl_fileStrip.SuspendLayout
        Me.gbCentro.SuspendLayout
        Me.gbEleccionClientesPAF.SuspendLayout
        CType(Me.cboSeleccionClienteParaPAF,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnImprimirEtiquetas,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tlpPrincipal.SuspendLayout
        Me.pnl1.SuspendLayout
        Me.tlpsuperior.SuspendLayout
        Me.pnl11.SuspendLayout
        Me.pnl12.SuspendLayout
        Me.pnl2.SuspendLayout
        Me.SuspendLayout
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(7, 295)
        Me.sbtipo.Size = New System.Drawing.Size(123, 20)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(60, 374)
        Me.btnRecargar.Size = New System.Drawing.Size(140, 28)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(407, 399)
        Me.btnSiguiente.Size = New System.Drawing.Size(45, 28)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(15, 399)
        Me.btnAnterior.Size = New System.Drawing.Size(45, 28)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(15, 374)
        Me.btnPrimero.Size = New System.Drawing.Size(45, 28)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(407, 374)
        Me.btnUltimo.Size = New System.Drawing.Size(45, 28)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(183, 399)
        Me.btnModificar.Size = New System.Drawing.Size(140, 28)
        '
        'btnTancar
        '
        Me.btnTancar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnTancar.Location = New System.Drawing.Point(735, 399)
        Me.btnTancar.Size = New System.Drawing.Size(112, 28)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(295, 776)
        Me.btnBorrar.Size = New System.Drawing.Size(140, 29)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(183, 752)
        Me.btnNuevo.Size = New System.Drawing.Size(115, 56)
        Me.btnNuevo.TextAlign = System.Drawing.ContentAlignment.TopLeft
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(60, 776)
        Me.btnActualizar.Size = New System.Drawing.Size(140, 29)
        '
        'btnVerLista
        '
        Me.btnVerLista.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnVerLista.Location = New System.Drawing.Point(735, 752)
        Me.btnVerLista.Size = New System.Drawing.Size(112, 28)
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.ItemHeight = 17
        Me.cboSeleccionCentro.Location = New System.Drawing.Point(2699, 30)
        Me.cboSeleccionCentro.Size = New System.Drawing.Size(218, 25)
        '
        'lblCliente
        '
        Me.lblCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCliente.Location = New System.Drawing.Point(11, 44)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(126, 25)
        Me.lblCliente.TabIndex = 3
        Me.lblCliente.Text = "Cliente"
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gbPAF
        '
        Me.gbPAF.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.gbPAF.BackColor = System.Drawing.SystemColors.Control
        Me.gbPAF.Controls.Add(Me.Button1)
        Me.gbPAF.Controls.Add(Me.lblPROV)
        Me.gbPAF.Controls.Add(Me.cboNOMCLIENT)
        Me.gbPAF.Controls.Add(Me.lblPAIS)
        Me.gbPAF.Controls.Add(Me.lblPOB)
        Me.gbPAF.Controls.Add(Me.lblDOM)
        Me.gbPAF.Controls.Add(Me.lblDireccionDeEnvio)
        Me.gbPAF.Controls.Add(Me.cboDOM)
        Me.gbPAF.Controls.Add(Me.txtALBCLI)
        Me.gbPAF.Controls.Add(Me.lblPedidoCliente)
        Me.gbPAF.Controls.Add(Me.lblPedidCliente)
        Me.gbPAF.Controls.Add(Me.dtpDATA)
        Me.gbPAF.Controls.Add(Me.txtCOMIS)
        Me.gbPAF.Controls.Add(Me.txtP_DTE)
        Me.gbPAF.Controls.Add(Me.cboFRA)
        Me.gbPAF.Controls.Add(Me.lblPedido)
        Me.gbPAF.Controls.Add(Me.lblFecha)
        Me.gbPAF.Controls.Add(Me.lblRepresentante)
        Me.gbPAF.Controls.Add(Me.lblCliente)
        Me.gbPAF.Controls.Add(Me.btnElegirCliente)
        Me.gbPAF.Controls.Add(Me.lblTantoPorCientoDescuento)
        Me.gbPAF.Controls.Add(Me.cboNOMREPRES)
        Me.gbPAF.Controls.Add(Me.lblComisio)
        Me.gbPAF.Controls.Add(Me.btnElegirRepresentate)
        Me.gbPAF.Controls.Add(Me.txtREPRES)
        Me.gbPAF.Controls.Add(Me.txtCLIENT)
        Me.gbPAF.Location = New System.Drawing.Point(4, 4)
        Me.gbPAF.Name = "gbPAF"
        Me.gbPAF.Size = New System.Drawing.Size(576, 204)
        Me.gbPAF.TabIndex = 4
        Me.gbPAF.TabStop = false
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(622, 123)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 30)
        Me.Button1.TabIndex = 391
        Me.Button1.Text = "Button1"
        Me.Button1.Visible = false
        '
        'lblPROV
        '
        Me.lblPROV.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPROV.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPROV.Location = New System.Drawing.Point(462, 71)
        Me.lblPROV.Name = "lblPROV"
        Me.lblPROV.Size = New System.Drawing.Size(112, 30)
        Me.lblPROV.TabIndex = 390
        Me.lblPROV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboNOMCLIENT
        '
        Me.cboNOMCLIENT.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cboNOMCLIENT.AutoSelect = true
        Me.cboNOMCLIENT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMCLIENT.Caption = ""
        Me.cboNOMCLIENT.CaptionHeight = 17
        Me.cboNOMCLIENT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboNOMCLIENT.ColumnCaptionHeight = 17
        Me.cboNOMCLIENT.ColumnFooterHeight = 17
        Me.cboNOMCLIENT.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMCLIENT.DeadAreaBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.cboNOMCLIENT.EditorBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.cboNOMCLIENT.Images.Add(CType(resources.GetObject("cboNOMCLIENT.Images"),System.Drawing.Image))
        Me.cboNOMCLIENT.IntegralHeight = true
        Me.cboNOMCLIENT.Location = New System.Drawing.Point(245, 46)
        Me.cboNOMCLIENT.MatchEntryTimeout = CType(100,Long)
        Me.cboNOMCLIENT.MaxDropDownItems = CType(8,Short)
        Me.cboNOMCLIENT.MaxLength = 0
        Me.cboNOMCLIENT.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMCLIENT.Name = "cboNOMCLIENT"
        Me.cboNOMCLIENT.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMCLIENT.Size = New System.Drawing.Size(322, 19)
        Me.cboNOMCLIENT.TabIndex = 3
        Me.cboNOMCLIENT.PropBag = resources.GetString("cboNOMCLIENT.PropBag")
        '
        'lblPAIS
        '
        Me.lblPAIS.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPAIS.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPAIS.Location = New System.Drawing.Point(605, 71)
        Me.lblPAIS.Name = "lblPAIS"
        Me.lblPAIS.Size = New System.Drawing.Size(126, 30)
        Me.lblPAIS.TabIndex = 389
        Me.lblPAIS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPOB
        '
        Me.lblPOB.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPOB.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPOB.Location = New System.Drawing.Point(303, 71)
        Me.lblPOB.Name = "lblPOB"
        Me.lblPOB.Size = New System.Drawing.Size(140, 30)
        Me.lblPOB.TabIndex = 388
        Me.lblPOB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDOM
        '
        Me.lblDOM.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblDOM.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDOM.Location = New System.Drawing.Point(11, 71)
        Me.lblDOM.Name = "lblDOM"
        Me.lblDOM.Size = New System.Drawing.Size(266, 30)
        Me.lblDOM.TabIndex = 386
        Me.lblDOM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDireccionDeEnvio
        '
        Me.lblDireccionDeEnvio.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDireccionDeEnvio.Location = New System.Drawing.Point(11, 106)
        Me.lblDireccionDeEnvio.Name = "lblDireccionDeEnvio"
        Me.lblDireccionDeEnvio.Size = New System.Drawing.Size(230, 24)
        Me.lblDireccionDeEnvio.TabIndex = 385
        Me.lblDireccionDeEnvio.Text = "Direcció enviament"
        Me.lblDireccionDeEnvio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDireccionDeEnvio.Visible = false
        '
        'cboDOM
        '
        Me.cboDOM.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cboDOM.AutoSelect = true
        Me.cboDOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboDOM.Caption = ""
        Me.cboDOM.CaptionHeight = 17
        Me.cboDOM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboDOM.ColumnCaptionHeight = 17
        Me.cboDOM.ColumnFooterHeight = 17
        Me.cboDOM.ContentHeight = 18
        Me.cboDOM.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDOM.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboDOM.EditorBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.cboDOM.Images.Add(CType(resources.GetObject("cboDOM.Images"),System.Drawing.Image))
        Me.cboDOM.IntegralHeight = true
        Me.cboDOM.Location = New System.Drawing.Point(252, 106)
        Me.cboDOM.MatchEntryTimeout = CType(100,Long)
        Me.cboDOM.MaxDropDownItems = CType(8,Short)
        Me.cboDOM.MaxLength = 0
        Me.cboDOM.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboDOM.Name = "cboDOM"
        Me.cboDOM.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboDOM.Size = New System.Drawing.Size(315, 19)
        Me.cboDOM.TabIndex = 384
        Me.cboDOM.Visible = false
        Me.cboDOM.PropBag = resources.GetString("cboDOM.PropBag")
        '
        'txtALBCLI
        '
        Me.txtALBCLI.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtALBCLI.Location = New System.Drawing.Point(459, 162)
        Me.txtALBCLI.Name = "txtALBCLI"
        Me.txtALBCLI.Size = New System.Drawing.Size(108, 21)
        Me.txtALBCLI.TabIndex = 383
        Me.txtALBCLI.Tag = Nothing
        '
        'lblPedidoCliente
        '
        Me.lblPedidoCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPedidoCliente.Location = New System.Drawing.Point(337, 162)
        Me.lblPedidoCliente.Name = "lblPedidoCliente"
        Me.lblPedidoCliente.Size = New System.Drawing.Size(117, 21)
        Me.lblPedidoCliente.TabIndex = 382
        Me.lblPedidoCliente.Text = "Comanda Client"
        Me.lblPedidoCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPedidCliente
        '
        Me.lblPedidCliente.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPedidCliente.BackColor = System.Drawing.SystemColors.Control
        Me.lblPedidCliente.Enabled = false
        Me.lblPedidCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPedidCliente.Location = New System.Drawing.Point(-85, 16)
        Me.lblPedidCliente.Name = "lblPedidCliente"
        Me.lblPedidCliente.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPedidCliente.Size = New System.Drawing.Size(0, 0)
        Me.lblPedidCliente.TabIndex = 0
        Me.lblPedidCliente.Visible = false
        '
        'dtpDATA
        '
        Me.dtpDATA.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        '
        '
        '
        Me.dtpDATA.Calendar.DayNameLength = 1
        Me.dtpDATA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpDATA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.dtpDATA.DisableOnNoData = false
        Me.dtpDATA.EmptyAsNull = true
        Me.dtpDATA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpDATA.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dtpDATA.Location = New System.Drawing.Point(392, 15)
        Me.dtpDATA.Name = "dtpDATA"
        Me.dtpDATA.Size = New System.Drawing.Size(151, 21)
        Me.dtpDATA.TabIndex = 381
        Me.dtpDATA.Tag = Nothing
        '
        'txtCOMIS
        '
        Me.txtCOMIS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCOMIS.Location = New System.Drawing.Point(257, 162)
        Me.txtCOMIS.Name = "txtCOMIS"
        Me.txtCOMIS.Size = New System.Drawing.Size(90, 21)
        Me.txtCOMIS.TabIndex = 277
        Me.txtCOMIS.Tag = Nothing
        '
        'txtP_DTE
        '
        Me.txtP_DTE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtP_DTE.Location = New System.Drawing.Point(96, 162)
        Me.txtP_DTE.Name = "txtP_DTE"
        Me.txtP_DTE.Size = New System.Drawing.Size(73, 21)
        Me.txtP_DTE.TabIndex = 276
        Me.txtP_DTE.Tag = Nothing
        '
        'cboFRA
        '
        Me.cboFRA.AutoSelect = true
        Me.cboFRA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboFRA.Caption = ""
        Me.cboFRA.CaptionHeight = 17
        Me.cboFRA.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboFRA.ColumnCaptionHeight = 17
        Me.cboFRA.ColumnFooterHeight = 17
        Me.cboFRA.ContentHeight = 18
        Me.cboFRA.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboFRA.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboFRA.Images.Add(CType(resources.GetObject("cboFRA.Images"),System.Drawing.Image))
        Me.cboFRA.IntegralHeight = true
        Me.cboFRA.Location = New System.Drawing.Point(140, 15)
        Me.cboFRA.MatchEntryTimeout = CType(100,Long)
        Me.cboFRA.MaxDropDownItems = CType(8,Short)
        Me.cboFRA.MaxLength = 0
        Me.cboFRA.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboFRA.Name = "cboFRA"
        Me.cboFRA.ReadOnly = true
        Me.cboFRA.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboFRA.Size = New System.Drawing.Size(137, 19)
        Me.cboFRA.TabIndex = 218
        Me.cboFRA.PropBag = resources.GetString("cboFRA.PropBag")
        '
        'lblPedido
        '
        Me.lblPedido.BackColor = System.Drawing.SystemColors.Control
        Me.lblPedido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPedido.Location = New System.Drawing.Point(11, 15)
        Me.lblPedido.Name = "lblPedido"
        Me.lblPedido.Size = New System.Drawing.Size(126, 24)
        Me.lblPedido.TabIndex = 4
        Me.lblPedido.Text = "Comanda"
        Me.lblPedido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFecha
        '
        Me.lblFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFecha.Location = New System.Drawing.Point(291, 15)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(95, 24)
        Me.lblFecha.TabIndex = 7
        Me.lblFecha.Text = "Data"
        Me.lblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRepresentante
        '
        Me.lblRepresentante.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRepresentante.Location = New System.Drawing.Point(11, 135)
        Me.lblRepresentante.Name = "lblRepresentante"
        Me.lblRepresentante.Size = New System.Drawing.Size(126, 25)
        Me.lblRepresentante.TabIndex = 178
        Me.lblRepresentante.Text = "Representant"
        Me.lblRepresentante.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnElegirCliente
        '
        Me.btnElegirCliente.Enabled = false
        Me.btnElegirCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirCliente.Location = New System.Drawing.Point(213, 44)
        Me.btnElegirCliente.Name = "btnElegirCliente"
        Me.btnElegirCliente.Size = New System.Drawing.Size(33, 25)
        Me.btnElegirCliente.TabIndex = 3
        Me.btnElegirCliente.TabStop = false
        Me.btnElegirCliente.Text = "..."
        Me.btnElegirCliente.UseVisualStyleBackColor = true
        '
        'lblTantoPorCientoDescuento
        '
        Me.lblTantoPorCientoDescuento.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTantoPorCientoDescuento.Location = New System.Drawing.Point(-33, 162)
        Me.lblTantoPorCientoDescuento.Name = "lblTantoPorCientoDescuento"
        Me.lblTantoPorCientoDescuento.Size = New System.Drawing.Size(126, 25)
        Me.lblTantoPorCientoDescuento.TabIndex = 10
        Me.lblTantoPorCientoDescuento.Text = "% Descompte"
        Me.lblTantoPorCientoDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboNOMREPRES
        '
        Me.cboNOMREPRES.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cboNOMREPRES.AutoSelect = true
        Me.cboNOMREPRES.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMREPRES.Caption = ""
        Me.cboNOMREPRES.CaptionHeight = 17
        Me.cboNOMREPRES.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboNOMREPRES.ColumnCaptionHeight = 17
        Me.cboNOMREPRES.ColumnFooterHeight = 17
        Me.cboNOMREPRES.ContentHeight = 18
        Me.cboNOMREPRES.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMREPRES.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMREPRES.EditorBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.cboNOMREPRES.Images.Add(CType(resources.GetObject("cboNOMREPRES.Images"),System.Drawing.Image))
        Me.cboNOMREPRES.IntegralHeight = true
        Me.cboNOMREPRES.Location = New System.Drawing.Point(252, 135)
        Me.cboNOMREPRES.MatchEntryTimeout = CType(100,Long)
        Me.cboNOMREPRES.MaxDropDownItems = CType(8,Short)
        Me.cboNOMREPRES.MaxLength = 255
        Me.cboNOMREPRES.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMREPRES.Name = "cboNOMREPRES"
        Me.cboNOMREPRES.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMREPRES.Size = New System.Drawing.Size(315, 19)
        Me.cboNOMREPRES.TabIndex = 6
        Me.cboNOMREPRES.PropBag = resources.GetString("cboNOMREPRES.PropBag")
        '
        'lblComisio
        '
        Me.lblComisio.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblComisio.Location = New System.Drawing.Point(171, 162)
        Me.lblComisio.Name = "lblComisio"
        Me.lblComisio.Size = New System.Drawing.Size(86, 21)
        Me.lblComisio.TabIndex = 12
        Me.lblComisio.Text = "% Comissió"
        Me.lblComisio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnElegirRepresentate
        '
        Me.btnElegirRepresentate.Enabled = false
        Me.btnElegirRepresentate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirRepresentate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirRepresentate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirRepresentate.Location = New System.Drawing.Point(213, 135)
        Me.btnElegirRepresentate.Name = "btnElegirRepresentate"
        Me.btnElegirRepresentate.Size = New System.Drawing.Size(33, 24)
        Me.btnElegirRepresentate.TabIndex = 7
        Me.btnElegirRepresentate.TabStop = false
        Me.btnElegirRepresentate.Text = "..."
        Me.btnElegirRepresentate.UseVisualStyleBackColor = true
        '
        'txtREPRES
        '
        Me.txtREPRES.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtREPRES.Location = New System.Drawing.Point(140, 135)
        Me.txtREPRES.Name = "txtREPRES"
        Me.txtREPRES.Size = New System.Drawing.Size(67, 21)
        Me.txtREPRES.TabIndex = 242
        Me.txtREPRES.Tag = Nothing
        '
        'txtCLIENT
        '
        Me.txtCLIENT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCLIENT.Location = New System.Drawing.Point(140, 44)
        Me.txtCLIENT.Name = "txtCLIENT"
        Me.txtCLIENT.Size = New System.Drawing.Size(67, 21)
        Me.txtCLIENT.TabIndex = 243
        Me.txtCLIENT.Tag = Nothing
        '
        'lblObservaciones
        '
        Me.lblObservaciones.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblObservaciones.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblObservaciones.Location = New System.Drawing.Point(582, 1)
        Me.lblObservaciones.Name = "lblObservaciones"
        Me.lblObservaciones.Size = New System.Drawing.Size(107, 30)
        Me.lblObservaciones.TabIndex = 176
        Me.lblObservaciones.Text = "Observacions"
        Me.lblObservaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tabControlPAFVentas
        '
        Me.tabControlPAFVentas.Controls.Add(Me.tabPagePAF)
        Me.tabControlPAFVentas.Controls.Add(Me.tabPageImpresionPAF)
        Me.tabControlPAFVentas.Controls.Add(Me.tabPageRecibos)
        Me.tabControlPAFVentas.Controls.Add(Me.tabPageTraspasoPAF)
        Me.tabControlPAFVentas.Controls.Add(Me.tabPageConsumo)
        Me.tabControlPAFVentas.Controls.Add(Me.tabPageVencimientos)
        Me.tabControlPAFVentas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControlPAFVentas.ItemSize = New System.Drawing.Size(42, 18)
        Me.tabControlPAFVentas.Location = New System.Drawing.Point(0, 0)
        Me.tabControlPAFVentas.Name = "tabControlPAFVentas"
        Me.tabControlPAFVentas.Padding = New System.Drawing.Point(0, 0)
        Me.tabControlPAFVentas.SelectedIndex = 0
        Me.tabControlPAFVentas.Size = New System.Drawing.Size(1002, 555)
        Me.tabControlPAFVentas.TabIndex = 0
        '
        'tabPagePAF
        '
        Me.tabPagePAF.Controls.Add(Me.TableLayoutPanel1)
        Me.tabPagePAF.Location = New System.Drawing.Point(4, 22)
        Me.tabPagePAF.Name = "tabPagePAF"
        Me.tabPagePAF.Size = New System.Drawing.Size(994, 529)
        Me.tabPagePAF.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel1.Controls.Add(Me.pnl21, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pnl22, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.pnl23, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 217!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(994, 529)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'pnl21
        '
        Me.pnl21.Controls.Add(Me.gbPAF)
        Me.pnl21.Controls.Add(Me.chkSumadoEstoc)
        Me.pnl21.Controls.Add(Me.chkTRASPAS)
        Me.pnl21.Controls.Add(Me.txtOBSERVOCULTA)
        Me.pnl21.Controls.Add(Me.lblObservaciones)
        Me.pnl21.Controls.Add(Me.txtOBSERV)
        Me.pnl21.Controls.Add(Me.lblOBSERVOCULTA)
        Me.pnl21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl21.Location = New System.Drawing.Point(3, 3)
        Me.pnl21.Name = "pnl21"
        Me.pnl21.Size = New System.Drawing.Size(988, 211)
        Me.pnl21.TabIndex = 0
        '
        'chkSumadoEstoc
        '
        Me.chkSumadoEstoc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chkSumadoEstoc.BackColor = System.Drawing.Color.Transparent
        Me.chkSumadoEstoc.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkSumadoEstoc.Location = New System.Drawing.Point(713, 7)
        Me.chkSumadoEstoc.Name = "chkSumadoEstoc"
        Me.chkSumadoEstoc.Size = New System.Drawing.Size(146, 26)
        Me.chkSumadoEstoc.TabIndex = 206
        Me.chkSumadoEstoc.Text = "Restat a l'Stock"
        Me.chkSumadoEstoc.UseVisualStyleBackColor = false
        Me.chkSumadoEstoc.Visible = false
        '
        'chkTRASPAS
        '
        Me.chkTRASPAS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chkTRASPAS.BackColor = System.Drawing.Color.Transparent
        Me.chkTRASPAS.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkTRASPAS.Location = New System.Drawing.Point(861, 7)
        Me.chkTRASPAS.Name = "chkTRASPAS"
        Me.chkTRASPAS.Size = New System.Drawing.Size(111, 25)
        Me.chkTRASPAS.TabIndex = 230
        Me.chkTRASPAS.Text = "Traspassat"
        Me.chkTRASPAS.UseVisualStyleBackColor = false
        Me.chkTRASPAS.Visible = false
        '
        'txtOBSERVOCULTA
        '
        Me.txtOBSERVOCULTA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtOBSERVOCULTA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOBSERVOCULTA.Location = New System.Drawing.Point(586, 135)
        Me.txtOBSERVOCULTA.Multiline = true
        Me.txtOBSERVOCULTA.Name = "txtOBSERVOCULTA"
        Me.txtOBSERVOCULTA.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOBSERVOCULTA.Size = New System.Drawing.Size(379, 71)
        Me.txtOBSERVOCULTA.TabIndex = 251
        Me.txtOBSERVOCULTA.Tag = Nothing
        '
        'txtOBSERV
        '
        Me.txtOBSERV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtOBSERV.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOBSERV.Location = New System.Drawing.Point(586, 33)
        Me.txtOBSERV.Multiline = true
        Me.txtOBSERV.Name = "txtOBSERV"
        Me.txtOBSERV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOBSERV.Size = New System.Drawing.Size(379, 80)
        Me.txtOBSERV.TabIndex = 240
        Me.txtOBSERV.Tag = Nothing
        '
        'lblOBSERVOCULTA
        '
        Me.lblOBSERVOCULTA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblOBSERVOCULTA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblOBSERVOCULTA.Location = New System.Drawing.Point(583, 110)
        Me.lblOBSERVOCULTA.Name = "lblOBSERVOCULTA"
        Me.lblOBSERVOCULTA.Size = New System.Drawing.Size(252, 29)
        Me.lblOBSERVOCULTA.TabIndex = 252
        Me.lblOBSERVOCULTA.Text = "Observacions ocultes"
        Me.lblOBSERVOCULTA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnl22
        '
        Me.pnl22.Controls.Add(Me.cboTalla)
        Me.pnl22.Controls.Add(Me.cboMuestra)
        Me.pnl22.Controls.Add(Me.cboMuestra2)
        Me.pnl22.Controls.Add(Me.cboKM)
        Me.pnl22.Controls.Add(Me.cboTipo)
        Me.pnl22.Controls.Add(Me.cboColor)
        Me.pnl22.Controls.Add(Me.dgLineasPAFVenta)
        Me.pnl22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl22.Location = New System.Drawing.Point(3, 220)
        Me.pnl22.Name = "pnl22"
        Me.pnl22.Size = New System.Drawing.Size(988, 238)
        Me.pnl22.TabIndex = 1
        '
        'cboTalla
        '
        Me.cboTalla.AllowColMove = true
        Me.cboTalla.AllowColSelect = true
        Me.cboTalla.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboTalla.AlternatingRows = true
        Me.cboTalla.CaptionHeight = 19
        Me.cboTalla.CaptionStyle = Style71
        Me.cboTalla.ColumnCaptionHeight = 17
        Me.cboTalla.ColumnFooterHeight = 17
        Me.cboTalla.ColumnSelectorStyle = Style72
        Me.cboTalla.EvenRowStyle = Style73
        Me.cboTalla.FetchRowStyles = false
        Me.cboTalla.FooterStyle = Style74
        Me.cboTalla.HeadingStyle = Style75
        Me.cboTalla.HighLightRowStyle = Style76
        Me.cboTalla.Images.Add(CType(resources.GetObject("cboTalla.Images"),System.Drawing.Image))
        Me.cboTalla.Location = New System.Drawing.Point(487, 53)
        Me.cboTalla.Name = "cboTalla"
        Me.cboTalla.OddRowStyle = Style77
        Me.cboTalla.RecordSelectorStyle = Style78
        Me.cboTalla.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboTalla.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboTalla.RowHeight = 17
        Me.cboTalla.RowSelectorStyle = Style79
        Me.cboTalla.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboTalla.ScrollTips = false
        Me.cboTalla.Size = New System.Drawing.Size(84, 231)
        Me.cboTalla.Style = Style80
        Me.cboTalla.TabIndex = 238
        Me.cboTalla.TabStop = false
        Me.cboTalla.Text = "C1TrueDBDropdown1"
        Me.cboTalla.UseCompatibleTextRendering = false
        Me.cboTalla.ValueTranslate = true
        Me.cboTalla.Visible = false
        Me.cboTalla.PropBag = resources.GetString("cboTalla.PropBag")
        '
        'cboMuestra
        '
        Me.cboMuestra.AllowColMove = false
        Me.cboMuestra.AllowColSelect = false
        Me.cboMuestra.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboMuestra.AlternatingRows = true
        Me.cboMuestra.CaptionHeight = 19
        Me.cboMuestra.CaptionStyle = Style81
        Me.cboMuestra.ColumnCaptionHeight = 17
        Me.cboMuestra.ColumnFooterHeight = 17
        Me.cboMuestra.ColumnSelectorStyle = Style82
        Me.cboMuestra.EvenRowStyle = Style83
        Me.cboMuestra.FetchRowStyles = false
        Me.cboMuestra.FooterStyle = Style84
        Me.cboMuestra.HeadingStyle = Style85
        Me.cboMuestra.HighLightRowStyle = Style86
        Me.cboMuestra.Images.Add(CType(resources.GetObject("cboMuestra.Images"),System.Drawing.Image))
        Me.cboMuestra.Location = New System.Drawing.Point(39, 53)
        Me.cboMuestra.Name = "cboMuestra"
        Me.cboMuestra.OddRowStyle = Style87
        Me.cboMuestra.RecordSelectorStyle = Style88
        Me.cboMuestra.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboMuestra.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboMuestra.RowHeight = 17
        Me.cboMuestra.RowSelectorStyle = Style89
        Me.cboMuestra.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboMuestra.ScrollTips = false
        Me.cboMuestra.Size = New System.Drawing.Size(84, 231)
        Me.cboMuestra.Style = Style90
        Me.cboMuestra.TabIndex = 232
        Me.cboMuestra.TabStop = false
        Me.cboMuestra.Text = "C1TrueDBDropdown1"
        Me.cboMuestra.UseCompatibleTextRendering = false
        Me.cboMuestra.ValueTranslate = true
        Me.cboMuestra.Visible = false
        Me.cboMuestra.PropBag = resources.GetString("cboMuestra.PropBag")
        '
        'cboMuestra2
        '
        Me.cboMuestra2.AllowColMove = false
        Me.cboMuestra2.AllowColSelect = false
        Me.cboMuestra2.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboMuestra2.AlternatingRows = true
        Me.cboMuestra2.CaptionHeight = 19
        Me.cboMuestra2.CaptionStyle = Style91
        Me.cboMuestra2.ColumnCaptionHeight = 17
        Me.cboMuestra2.ColumnFooterHeight = 17
        Me.cboMuestra2.ColumnSelectorStyle = Style92
        Me.cboMuestra2.EvenRowStyle = Style93
        Me.cboMuestra2.ExtendRightColumn = true
        Me.cboMuestra2.FetchRowStyles = true
        Me.cboMuestra2.FooterStyle = Style94
        Me.cboMuestra2.HeadingStyle = Style95
        Me.cboMuestra2.HighLightRowStyle = Style96
        Me.cboMuestra2.Images.Add(CType(resources.GetObject("cboMuestra2.Images"),System.Drawing.Image))
        Me.cboMuestra2.Location = New System.Drawing.Point(151, 53)
        Me.cboMuestra2.Name = "cboMuestra2"
        Me.cboMuestra2.OddRowStyle = Style97
        Me.cboMuestra2.RecordSelectorStyle = Style98
        Me.cboMuestra2.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboMuestra2.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboMuestra2.RowHeight = 17
        Me.cboMuestra2.RowSelectorStyle = Style99
        Me.cboMuestra2.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboMuestra2.ScrollTips = false
        Me.cboMuestra2.Size = New System.Drawing.Size(84, 231)
        Me.cboMuestra2.Style = Style100
        Me.cboMuestra2.TabIndex = 233
        Me.cboMuestra2.TabStop = false
        Me.cboMuestra2.Text = "C1TrueDBDropdown1"
        Me.cboMuestra2.UseCompatibleTextRendering = false
        Me.cboMuestra2.ValueTranslate = true
        Me.cboMuestra2.Visible = false
        Me.cboMuestra2.PropBag = resources.GetString("cboMuestra2.PropBag")
        '
        'cboKM
        '
        Me.cboKM.AllowColMove = true
        Me.cboKM.AllowColSelect = true
        Me.cboKM.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboKM.AlternatingRows = true
        Me.cboKM.CaptionHeight = 19
        Me.cboKM.CaptionStyle = Style101
        Me.cboKM.ColumnCaptionHeight = 17
        Me.cboKM.ColumnFooterHeight = 17
        Me.cboKM.ColumnSelectorStyle = Style102
        Me.cboKM.EvenRowStyle = Style103
        Me.cboKM.FetchRowStyles = false
        Me.cboKM.FooterStyle = Style104
        Me.cboKM.HeadingStyle = Style105
        Me.cboKM.HighLightRowStyle = Style106
        Me.cboKM.Images.Add(CType(resources.GetObject("cboKM.Images"),System.Drawing.Image))
        Me.cboKM.Location = New System.Drawing.Point(375, 53)
        Me.cboKM.Name = "cboKM"
        Me.cboKM.OddRowStyle = Style107
        Me.cboKM.RecordSelectorStyle = Style108
        Me.cboKM.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboKM.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboKM.RowHeight = 17
        Me.cboKM.RowSelectorStyle = Style109
        Me.cboKM.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboKM.ScrollTips = false
        Me.cboKM.Size = New System.Drawing.Size(84, 74)
        Me.cboKM.Style = Style110
        Me.cboKM.TabIndex = 236
        Me.cboKM.TabStop = false
        Me.cboKM.Text = "C1TrueDBDropdown1"
        Me.cboKM.UseCompatibleTextRendering = false
        Me.cboKM.ValueTranslate = true
        Me.cboKM.Visible = false
        Me.cboKM.PropBag = resources.GetString("cboKM.PropBag")
        '
        'cboTipo
        '
        Me.cboTipo.AllowColMove = true
        Me.cboTipo.AllowColSelect = true
        Me.cboTipo.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboTipo.AlternatingRows = true
        Me.cboTipo.CaptionHeight = 19
        Me.cboTipo.CaptionStyle = Style111
        Me.cboTipo.ColumnCaptionHeight = 17
        Me.cboTipo.ColumnFooterHeight = 17
        Me.cboTipo.ColumnSelectorStyle = Style112
        Me.cboTipo.EvenRowStyle = Style113
        Me.cboTipo.FetchRowStyles = false
        Me.cboTipo.FooterStyle = Style114
        Me.cboTipo.HeadingStyle = Style115
        Me.cboTipo.HighLightRowStyle = Style116
        Me.cboTipo.Images.Add(CType(resources.GetObject("cboTipo.Images"),System.Drawing.Image))
        Me.cboTipo.Location = New System.Drawing.Point(599, 53)
        Me.cboTipo.Name = "cboTipo"
        Me.cboTipo.OddRowStyle = Style117
        Me.cboTipo.RecordSelectorStyle = Style118
        Me.cboTipo.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboTipo.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboTipo.RowHeight = 17
        Me.cboTipo.RowSelectorStyle = Style119
        Me.cboTipo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboTipo.ScrollTips = false
        Me.cboTipo.Size = New System.Drawing.Size(84, 74)
        Me.cboTipo.Style = Style120
        Me.cboTipo.TabIndex = 237
        Me.cboTipo.TabStop = false
        Me.cboTipo.Text = "C1TrueDBDropdown1"
        Me.cboTipo.UseCompatibleTextRendering = false
        Me.cboTipo.ValueTranslate = true
        Me.cboTipo.Visible = false
        Me.cboTipo.PropBag = resources.GetString("cboTipo.PropBag")
        '
        'cboColor
        '
        Me.cboColor.AllowColMove = true
        Me.cboColor.AllowColSelect = true
        Me.cboColor.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboColor.AlternatingRows = true
        Me.cboColor.CaptionHeight = 19
        Me.cboColor.CaptionStyle = Style121
        Me.cboColor.ColumnCaptionHeight = 17
        Me.cboColor.ColumnFooterHeight = 17
        Me.cboColor.ColumnSelectorStyle = Style122
        Me.cboColor.EvenRowStyle = Style123
        Me.cboColor.FetchRowStyles = false
        Me.cboColor.FooterStyle = Style124
        Me.cboColor.HeadingStyle = Style125
        Me.cboColor.HighLightRowStyle = Style126
        Me.cboColor.Images.Add(CType(resources.GetObject("cboColor.Images"),System.Drawing.Image))
        Me.cboColor.Location = New System.Drawing.Point(263, 53)
        Me.cboColor.Name = "cboColor"
        Me.cboColor.OddRowStyle = Style127
        Me.cboColor.RecordSelectorStyle = Style128
        Me.cboColor.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboColor.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboColor.RowHeight = 17
        Me.cboColor.RowSelectorStyle = Style129
        Me.cboColor.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboColor.ScrollTips = false
        Me.cboColor.Size = New System.Drawing.Size(84, 231)
        Me.cboColor.Style = Style130
        Me.cboColor.TabIndex = 239
        Me.cboColor.TabStop = false
        Me.cboColor.Text = "C1TrueDBDropdown1"
        Me.cboColor.UseCompatibleTextRendering = false
        Me.cboColor.ValueTranslate = true
        Me.cboColor.Visible = false
        Me.cboColor.PropBag = resources.GetString("cboColor.PropBag")
        '
        'dgLineasPAFVenta
        '
        Me.dgLineasPAFVenta.AllowAddNew = true
        Me.dgLineasPAFVenta.AllowColMove = false
        Me.dgLineasPAFVenta.AllowColSelect = false
        Me.dgLineasPAFVenta.AllowDelete = true
        Me.dgLineasPAFVenta.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgLineasPAFVenta.AllowSort = false
        Me.dgLineasPAFVenta.CaptionHeight = 17
        Me.dgLineasPAFVenta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgLineasPAFVenta.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.dgLineasPAFVenta.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgLineasPAFVenta.Images.Add(CType(resources.GetObject("dgLineasPAFVenta.Images"),System.Drawing.Image))
        Me.dgLineasPAFVenta.Location = New System.Drawing.Point(0, 0)
        Me.dgLineasPAFVenta.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder
        Me.dgLineasPAFVenta.Name = "dgLineasPAFVenta"
        Me.dgLineasPAFVenta.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgLineasPAFVenta.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgLineasPAFVenta.PreviewInfo.ZoomFactor = 75R
        Me.dgLineasPAFVenta.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgLineasPAFVenta.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgLineasPAFVenta.RowHeight = 15
        Me.dgLineasPAFVenta.Size = New System.Drawing.Size(988, 238)
        Me.dgLineasPAFVenta.SpringMode = true
        Me.dgLineasPAFVenta.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgLineasPAFVenta.TabIndex = 231
        Me.dgLineasPAFVenta.Text = "C1TrueDBGrid1"
        Me.dgLineasPAFVenta.UseCompatibleTextRendering = false
        Me.dgLineasPAFVenta.WrapCellPointer = true
        Me.dgLineasPAFVenta.PropBag = resources.GetString("dgLineasPAFVenta.PropBag")
        '
        'pnl23
        '
        Me.pnl23.Controls.Add(Me.txtTOTAL)
        Me.pnl23.Controls.Add(Me.txtBRUT1)
        Me.pnl23.Controls.Add(Me.txtIVA1)
        Me.pnl23.Controls.Add(Me.txtRE1)
        Me.pnl23.Controls.Add(Me.txtBASE1)
        Me.pnl23.Controls.Add(Me.txtDTE1)
        Me.pnl23.Controls.Add(Me.btnEliminarFilasSeleccionadas)
        Me.pnl23.Controls.Add(Me.lblTotal)
        Me.pnl23.Controls.Add(Me.lblTotalBruto)
        Me.pnl23.Controls.Add(Me.lblBase)
        Me.pnl23.Controls.Add(Me.lblDescuento)
        Me.pnl23.Controls.Add(Me.lblIVA)
        Me.pnl23.Controls.Add(Me.lblRE)
        Me.pnl23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl23.Location = New System.Drawing.Point(3, 464)
        Me.pnl23.Name = "pnl23"
        Me.pnl23.Size = New System.Drawing.Size(988, 62)
        Me.pnl23.TabIndex = 2
        '
        'txtTOTAL
        '
        Me.txtTOTAL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtTOTAL.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.txtTOTAL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTOTAL.CustomFormat = "#,##0.00"
        Me.txtTOTAL.DataType = GetType(Double)
        Me.txtTOTAL.DisplayFormat.CustomFormat = "#,##0.00"
        Me.txtTOTAL.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtTOTAL.DisplayFormat.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None
        Me.txtTOTAL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTOTAL.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtTOTAL.Location = New System.Drawing.Point(894, 32)
        Me.txtTOTAL.Name = "txtTOTAL"
        Me.txtTOTAL.Size = New System.Drawing.Size(84, 21)
        Me.txtTOTAL.TabIndex = 254
        Me.txtTOTAL.Tag = Nothing
        Me.txtTOTAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBRUT1
        '
        Me.txtBRUT1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtBRUT1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.txtBRUT1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBRUT1.CustomFormat = "#,##0.00"
        Me.txtBRUT1.DataType = GetType(Double)
        Me.txtBRUT1.DisplayFormat.CustomFormat = "#,##0.00"
        Me.txtBRUT1.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtBRUT1.DisplayFormat.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None
        Me.txtBRUT1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtBRUT1.Location = New System.Drawing.Point(802, 33)
        Me.txtBRUT1.Name = "txtBRUT1"
        Me.txtBRUT1.Size = New System.Drawing.Size(84, 21)
        Me.txtBRUT1.TabIndex = 249
        Me.txtBRUT1.Tag = Nothing
        Me.txtBRUT1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIVA1
        '
        Me.txtIVA1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtIVA1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.txtIVA1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIVA1.CustomFormat = "#,##0.00"
        Me.txtIVA1.DataType = GetType(Double)
        Me.txtIVA1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtIVA1.Location = New System.Drawing.Point(684, 33)
        Me.txtIVA1.Name = "txtIVA1"
        Me.txtIVA1.Size = New System.Drawing.Size(84, 21)
        Me.txtIVA1.TabIndex = 248
        Me.txtIVA1.Tag = Nothing
        Me.txtIVA1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRE1
        '
        Me.txtRE1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtRE1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.txtRE1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRE1.CustomFormat = "#,##0.00"
        Me.txtRE1.DataType = GetType(Double)
        Me.txtRE1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtRE1.Location = New System.Drawing.Point(566, 33)
        Me.txtRE1.Name = "txtRE1"
        Me.txtRE1.Size = New System.Drawing.Size(84, 21)
        Me.txtRE1.TabIndex = 247
        Me.txtRE1.Tag = Nothing
        Me.txtRE1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBASE1
        '
        Me.txtBASE1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtBASE1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.txtBASE1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBASE1.CustomFormat = "#,##0.00"
        Me.txtBASE1.DataType = GetType(Double)
        Me.txtBASE1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtBASE1.Location = New System.Drawing.Point(438, 33)
        Me.txtBASE1.Name = "txtBASE1"
        Me.txtBASE1.Size = New System.Drawing.Size(92, 21)
        Me.txtBASE1.TabIndex = 246
        Me.txtBASE1.Tag = Nothing
        Me.txtBASE1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDTE1
        '
        Me.txtDTE1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtDTE1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.txtDTE1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDTE1.CustomFormat = "#,##0.00"
        Me.txtDTE1.DataType = GetType(Double)
        Me.txtDTE1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtDTE1.Location = New System.Drawing.Point(320, 33)
        Me.txtDTE1.Name = "txtDTE1"
        Me.txtDTE1.Size = New System.Drawing.Size(84, 21)
        Me.txtDTE1.TabIndex = 245
        Me.txtDTE1.Tag = Nothing
        Me.txtDTE1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnEliminarFilasSeleccionadas
        '
        Me.btnEliminarFilasSeleccionadas.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnEliminarFilasSeleccionadas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEliminarFilasSeleccionadas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminarFilasSeleccionadas.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnEliminarFilasSeleccionadas.Location = New System.Drawing.Point(8, 24)
        Me.btnEliminarFilasSeleccionadas.Name = "btnEliminarFilasSeleccionadas"
        Me.btnEliminarFilasSeleccionadas.Size = New System.Drawing.Size(227, 30)
        Me.btnEliminarFilasSeleccionadas.TabIndex = 250
        Me.btnEliminarFilasSeleccionadas.TabStop = false
        Me.btnEliminarFilasSeleccionadas.Text = "Eliminar linies sel·leccionades"
        Me.btnEliminarFilasSeleccionadas.UseVisualStyleBackColor = true
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTotal.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTotal.Location = New System.Drawing.Point(897, 11)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(81, 24)
        Me.lblTotal.TabIndex = 253
        Me.lblTotal.Text = "TOTAL"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTotalBruto
        '
        Me.lblTotalBruto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblTotalBruto.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTotalBruto.Location = New System.Drawing.Point(807, 11)
        Me.lblTotalBruto.Name = "lblTotalBruto"
        Me.lblTotalBruto.Size = New System.Drawing.Size(81, 24)
        Me.lblTotalBruto.TabIndex = 201
        Me.lblTotalBruto.Text = "Total Brut"
        Me.lblTotalBruto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBase
        '
        Me.lblBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblBase.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblBase.Location = New System.Drawing.Point(433, 11)
        Me.lblBase.Name = "lblBase"
        Me.lblBase.Size = New System.Drawing.Size(105, 24)
        Me.lblBase.TabIndex = 202
        Me.lblBase.Text = "Base Imposable"
        Me.lblBase.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDescuento
        '
        Me.lblDescuento.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblDescuento.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDescuento.Location = New System.Drawing.Point(323, 11)
        Me.lblDescuento.Name = "lblDescuento"
        Me.lblDescuento.Size = New System.Drawing.Size(81, 24)
        Me.lblDescuento.TabIndex = 203
        Me.lblDescuento.Text = "Dte.:"
        Me.lblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIVA
        '
        Me.lblIVA.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblIVA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblIVA.Location = New System.Drawing.Point(690, 11)
        Me.lblIVA.Name = "lblIVA"
        Me.lblIVA.Size = New System.Drawing.Size(81, 24)
        Me.lblIVA.TabIndex = 204
        Me.lblIVA.Text = "IVA"
        Me.lblIVA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRE
        '
        Me.lblRE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblRE.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRE.Location = New System.Drawing.Point(572, 11)
        Me.lblRE.Name = "lblRE"
        Me.lblRE.Size = New System.Drawing.Size(76, 24)
        Me.lblRE.TabIndex = 218
        Me.lblRE.Text = "Recàrrec"
        Me.lblRE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tabPageImpresionPAF
        '
        Me.tabPageImpresionPAF.Controls.Add(Me.GroupBox2)
        Me.tabPageImpresionPAF.Location = New System.Drawing.Point(4, 22)
        Me.tabPageImpresionPAF.Name = "tabPageImpresionPAF"
        Me.tabPageImpresionPAF.Size = New System.Drawing.Size(994, 529)
        Me.tabPageImpresionPAF.TabIndex = 4
        Me.tabPageImpresionPAF.Text = "Impresió"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.tlpTab2)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.cboSemanas)
        Me.GroupBox2.Controls.Add(Me.lblIncoterm)
        Me.GroupBox2.Controls.Add(Me.cboIncoterm)
        Me.GroupBox2.Controls.Add(Me.lblInformacion)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.lblDeDocumento)
        Me.GroupBox2.Controls.Add(Me.cboAPAFIMPRESION)
        Me.GroupBox2.Controls.Add(Me.cboDEPAFIMPRESION)
        Me.GroupBox2.Controls.Add(Me.btnImprimir)
        Me.GroupBox2.Controls.Add(Me.chkFacturaInternacional)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 25)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(978, 522)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = false
        '
        'tlpTab2
        '
        Me.tlpTab2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.tlpTab2.ColumnCount = 2
        Me.tlpTab2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.tlpTab2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.tlpTab2.Controls.Add(Me.pnlTab21, 0, 0)
        Me.tlpTab2.Controls.Add(Me.pnlTAb22, 1, 0)
        Me.tlpTab2.Location = New System.Drawing.Point(8, 223)
        Me.tlpTab2.Name = "tlpTab2"
        Me.tlpTab2.RowCount = 1
        Me.tlpTab2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tlpTab2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 246!))
        Me.tlpTab2.Size = New System.Drawing.Size(962, 246)
        Me.tlpTab2.TabIndex = 240
        '
        'pnlTab21
        '
        Me.pnlTab21.Controls.Add(Me.cboImpresion)
        Me.pnlTab21.Controls.Add(Me.dgImpresion)
        Me.pnlTab21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTab21.Location = New System.Drawing.Point(3, 3)
        Me.pnlTab21.Name = "pnlTab21"
        Me.pnlTab21.Size = New System.Drawing.Size(475, 240)
        Me.pnlTab21.TabIndex = 0
        '
        'cboImpresion
        '
        Me.cboImpresion.AllowColMove = true
        Me.cboImpresion.AllowColSelect = true
        Me.cboImpresion.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboImpresion.AlternatingRows = true
        Me.cboImpresion.CaptionHeight = 19
        Me.cboImpresion.CaptionStyle = Style131
        Me.cboImpresion.ColumnCaptionHeight = 17
        Me.cboImpresion.ColumnFooterHeight = 17
        Me.cboImpresion.ColumnSelectorStyle = Style132
        Me.cboImpresion.EvenRowStyle = Style133
        Me.cboImpresion.FetchRowStyles = false
        Me.cboImpresion.FooterStyle = Style134
        Me.cboImpresion.HeadingStyle = Style135
        Me.cboImpresion.HighLightRowStyle = Style136
        Me.cboImpresion.Images.Add(CType(resources.GetObject("cboImpresion.Images"),System.Drawing.Image))
        Me.cboImpresion.Location = New System.Drawing.Point(4, 20)
        Me.cboImpresion.Name = "cboImpresion"
        Me.cboImpresion.OddRowStyle = Style137
        Me.cboImpresion.RecordSelectorStyle = Style138
        Me.cboImpresion.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboImpresion.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboImpresion.RowHeight = 17
        Me.cboImpresion.RowSelectorStyle = Style139
        Me.cboImpresion.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboImpresion.ScrollTips = false
        Me.cboImpresion.Size = New System.Drawing.Size(84, 74)
        Me.cboImpresion.Style = Style140
        Me.cboImpresion.TabIndex = 239
        Me.cboImpresion.TabStop = false
        Me.cboImpresion.Text = "cboImpresion"
        Me.cboImpresion.UseCompatibleTextRendering = false
        Me.cboImpresion.ValueTranslate = true
        Me.cboImpresion.Visible = false
        Me.cboImpresion.PropBag = resources.GetString("cboImpresion.PropBag")
        '
        'dgImpresion
        '
        Me.dgImpresion.AllowAddNew = true
        Me.dgImpresion.AllowColMove = false
        Me.dgImpresion.AllowColSelect = false
        Me.dgImpresion.AllowDelete = true
        Me.dgImpresion.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgImpresion.AllowSort = false
        Me.dgImpresion.CaptionHeight = 17
        Me.dgImpresion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgImpresion.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.dgImpresion.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgImpresion.Images.Add(CType(resources.GetObject("dgImpresion.Images"),System.Drawing.Image))
        Me.dgImpresion.Location = New System.Drawing.Point(0, 0)
        Me.dgImpresion.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder
        Me.dgImpresion.Name = "dgImpresion"
        Me.dgImpresion.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgImpresion.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgImpresion.PreviewInfo.ZoomFactor = 75R
        Me.dgImpresion.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgImpresion.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgImpresion.RowHeight = 15
        Me.dgImpresion.Size = New System.Drawing.Size(475, 240)
        Me.dgImpresion.SpringMode = true
        Me.dgImpresion.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgImpresion.TabIndex = 232
        Me.dgImpresion.Text = "C1TrueDBGrid1"
        Me.dgImpresion.UseCompatibleTextRendering = false
        Me.dgImpresion.WrapCellPointer = true
        Me.dgImpresion.PropBag = resources.GetString("dgImpresion.PropBag")
        '
        'pnlTAb22
        '
        Me.pnlTAb22.Controls.Add(Me.dgImpresion2)
        Me.pnlTAb22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTAb22.Location = New System.Drawing.Point(484, 3)
        Me.pnlTAb22.Name = "pnlTAb22"
        Me.pnlTAb22.Size = New System.Drawing.Size(475, 240)
        Me.pnlTAb22.TabIndex = 1
        '
        'dgImpresion2
        '
        Me.dgImpresion2.AllowAddNew = true
        Me.dgImpresion2.AllowColMove = false
        Me.dgImpresion2.AllowColSelect = false
        Me.dgImpresion2.AllowDelete = true
        Me.dgImpresion2.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgImpresion2.AllowSort = false
        Me.dgImpresion2.CaptionHeight = 17
        Me.dgImpresion2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgImpresion2.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.dgImpresion2.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgImpresion2.Images.Add(CType(resources.GetObject("dgImpresion2.Images"),System.Drawing.Image))
        Me.dgImpresion2.Location = New System.Drawing.Point(0, 0)
        Me.dgImpresion2.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder
        Me.dgImpresion2.Name = "dgImpresion2"
        Me.dgImpresion2.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgImpresion2.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgImpresion2.PreviewInfo.ZoomFactor = 75R
        Me.dgImpresion2.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgImpresion2.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgImpresion2.RowHeight = 15
        Me.dgImpresion2.Size = New System.Drawing.Size(475, 240)
        Me.dgImpresion2.SpringMode = true
        Me.dgImpresion2.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgImpresion2.TabIndex = 238
        Me.dgImpresion2.Text = "C1TrueDBGrid1"
        Me.dgImpresion2.UseCompatibleTextRendering = false
        Me.dgImpresion2.WrapCellPointer = true
        Me.dgImpresion2.PropBag = resources.GetString("dgImpresion2.PropBag")
        '
        'Label8
        '
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(22, 192)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(247, 25)
        Me.Label8.TabIndex = 237
        Me.Label8.Text = "Tepms d'entrega (en setmanes)"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboSemanas
        '
        Me.cboSemanas.AutoSelect = true
        Me.cboSemanas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboSemanas.Caption = ""
        Me.cboSemanas.CaptionHeight = 17
        Me.cboSemanas.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboSemanas.ColumnCaptionHeight = 17
        Me.cboSemanas.ColumnFooterHeight = 17
        Me.cboSemanas.ContentHeight = 18
        Me.cboSemanas.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSemanas.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cboSemanas.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboSemanas.EditorBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.cboSemanas.Images.Add(CType(resources.GetObject("cboSemanas.Images"),System.Drawing.Image))
        Me.cboSemanas.IntegralHeight = true
        Me.cboSemanas.Location = New System.Drawing.Point(274, 192)
        Me.cboSemanas.MatchEntryTimeout = CType(100,Long)
        Me.cboSemanas.MaxDropDownItems = CType(8,Short)
        Me.cboSemanas.MaxLength = 0
        Me.cboSemanas.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboSemanas.Name = "cboSemanas"
        Me.cboSemanas.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboSemanas.Size = New System.Drawing.Size(73, 19)
        Me.cboSemanas.TabIndex = 236
        Me.cboSemanas.PropBag = resources.GetString("cboSemanas.PropBag")
        '
        'lblIncoterm
        '
        Me.lblIncoterm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblIncoterm.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblIncoterm.Location = New System.Drawing.Point(213, 158)
        Me.lblIncoterm.Name = "lblIncoterm"
        Me.lblIncoterm.Size = New System.Drawing.Size(241, 24)
        Me.lblIncoterm.TabIndex = 235
        Me.lblIncoterm.Text = "Incoterm"
        Me.lblIncoterm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboIncoterm
        '
        Me.cboIncoterm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cboIncoterm.AutoSelect = true
        Me.cboIncoterm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboIncoterm.Caption = ""
        Me.cboIncoterm.CaptionHeight = 17
        Me.cboIncoterm.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboIncoterm.ColumnCaptionHeight = 17
        Me.cboIncoterm.ColumnFooterHeight = 17
        Me.cboIncoterm.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboIncoterm.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cboIncoterm.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboIncoterm.EditorBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.cboIncoterm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboIncoterm.Images.Add(CType(resources.GetObject("cboIncoterm.Images"),System.Drawing.Image))
        Me.cboIncoterm.IntegralHeight = true
        Me.cboIncoterm.ItemHeight = 13
        Me.cboIncoterm.Location = New System.Drawing.Point(465, 158)
        Me.cboIncoterm.MatchEntryTimeout = CType(100,Long)
        Me.cboIncoterm.MaxDropDownItems = CType(8,Short)
        Me.cboIncoterm.MaxLength = 0
        Me.cboIncoterm.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboIncoterm.Name = "cboIncoterm"
        Me.cboIncoterm.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboIncoterm.Size = New System.Drawing.Size(435, 22)
        Me.cboIncoterm.TabIndex = 234
        Me.cboIncoterm.PropBag = resources.GetString("cboIncoterm.PropBag")
        '
        'lblInformacion
        '
        Me.lblInformacion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblInformacion.Location = New System.Drawing.Point(11, 473)
        Me.lblInformacion.Name = "lblInformacion"
        Me.lblInformacion.Size = New System.Drawing.Size(571, 30)
        Me.lblInformacion.TabIndex = 233
        Me.lblInformacion.Text = "Per seleccionar l'idioma ves a Opcions-> Idioma Impresió en el menú principal"
        '
        'Label3
        '
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(17, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(123, 24)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "A Document"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDeDocumento
        '
        Me.lblDeDocumento.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDeDocumento.Location = New System.Drawing.Point(17, 34)
        Me.lblDeDocumento.Name = "lblDeDocumento"
        Me.lblDeDocumento.Size = New System.Drawing.Size(123, 25)
        Me.lblDeDocumento.TabIndex = 15
        Me.lblDeDocumento.Text = "De Document"
        Me.lblDeDocumento.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboAPAFIMPRESION
        '
        Me.cboAPAFIMPRESION.AutoSelect = true
        Me.cboAPAFIMPRESION.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboAPAFIMPRESION.Caption = ""
        Me.cboAPAFIMPRESION.CaptionHeight = 17
        Me.cboAPAFIMPRESION.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboAPAFIMPRESION.ColumnCaptionHeight = 17
        Me.cboAPAFIMPRESION.ColumnFooterHeight = 17
        Me.cboAPAFIMPRESION.ContentHeight = 18
        Me.cboAPAFIMPRESION.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAPAFIMPRESION.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboAPAFIMPRESION.EditorBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.cboAPAFIMPRESION.Images.Add(CType(resources.GetObject("cboAPAFIMPRESION.Images"),System.Drawing.Image))
        Me.cboAPAFIMPRESION.IntegralHeight = true
        Me.cboAPAFIMPRESION.Location = New System.Drawing.Point(146, 74)
        Me.cboAPAFIMPRESION.MatchEntryTimeout = CType(100,Long)
        Me.cboAPAFIMPRESION.MaxDropDownItems = CType(8,Short)
        Me.cboAPAFIMPRESION.MaxLength = 0
        Me.cboAPAFIMPRESION.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboAPAFIMPRESION.Name = "cboAPAFIMPRESION"
        Me.cboAPAFIMPRESION.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboAPAFIMPRESION.Size = New System.Drawing.Size(156, 19)
        Me.cboAPAFIMPRESION.TabIndex = 18
        Me.cboAPAFIMPRESION.PropBag = resources.GetString("cboAPAFIMPRESION.PropBag")
        '
        'cboDEPAFIMPRESION
        '
        Me.cboDEPAFIMPRESION.AutoSelect = true
        Me.cboDEPAFIMPRESION.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboDEPAFIMPRESION.Caption = ""
        Me.cboDEPAFIMPRESION.CaptionHeight = 17
        Me.cboDEPAFIMPRESION.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboDEPAFIMPRESION.ColumnCaptionHeight = 17
        Me.cboDEPAFIMPRESION.ColumnFooterHeight = 17
        Me.cboDEPAFIMPRESION.ContentHeight = 18
        Me.cboDEPAFIMPRESION.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDEPAFIMPRESION.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboDEPAFIMPRESION.EditorBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.cboDEPAFIMPRESION.Images.Add(CType(resources.GetObject("cboDEPAFIMPRESION.Images"),System.Drawing.Image))
        Me.cboDEPAFIMPRESION.IntegralHeight = true
        Me.cboDEPAFIMPRESION.Location = New System.Drawing.Point(146, 34)
        Me.cboDEPAFIMPRESION.MatchEntryTimeout = CType(100,Long)
        Me.cboDEPAFIMPRESION.MaxDropDownItems = CType(8,Short)
        Me.cboDEPAFIMPRESION.MaxLength = 0
        Me.cboDEPAFIMPRESION.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboDEPAFIMPRESION.Name = "cboDEPAFIMPRESION"
        Me.cboDEPAFIMPRESION.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboDEPAFIMPRESION.Size = New System.Drawing.Size(156, 19)
        Me.cboDEPAFIMPRESION.TabIndex = 17
        Me.cboDEPAFIMPRESION.PropBag = resources.GetString("cboDEPAFIMPRESION.PropBag")
        '
        'btnImprimir
        '
        Me.btnImprimir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimir.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnImprimir.Location = New System.Drawing.Point(844, 473)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(100, 30)
        Me.btnImprimir.TabIndex = 14
        Me.btnImprimir.TabStop = false
        Me.btnImprimir.Text = "Imprimir"
        Me.btnImprimir.UseVisualStyleBackColor = true
        '
        'chkFacturaInternacional
        '
        Me.chkFacturaInternacional.Location = New System.Drawing.Point(22, 113)
        Me.chkFacturaInternacional.Name = "chkFacturaInternacional"
        Me.chkFacturaInternacional.Size = New System.Drawing.Size(185, 40)
        Me.chkFacturaInternacional.TabIndex = 2
        Me.chkFacturaInternacional.Text = "Factura Internacional"
        '
        'tabPageRecibos
        '
        Me.tabPageRecibos.Controls.Add(Me.ppfitxa2)
        Me.tabPageRecibos.Controls.Add(Me.lstOrdenes)
        Me.tabPageRecibos.Controls.Add(Me.GroupBox4)
        Me.tabPageRecibos.Controls.Add(Me.btnGenerarRecibo)
        Me.tabPageRecibos.Location = New System.Drawing.Point(4, 22)
        Me.tabPageRecibos.Name = "tabPageRecibos"
        Me.tabPageRecibos.Size = New System.Drawing.Size(994, 529)
        Me.tabPageRecibos.TabIndex = 5
        Me.tabPageRecibos.Text = "Rebuts"
        '
        'ppfitxa2
        '
        Me.ppfitxa2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.ppfitxa2.AvailablePreviewActions = CType(((((C1.Win.C1Preview.C1PreviewActionFlags.FileOpen Or C1.Win.C1Preview.C1PreviewActionFlags.FileSave)  _
            Or C1.Win.C1Preview.C1PreviewActionFlags.PageSetup)  _
            Or C1.Win.C1Preview.C1PreviewActionFlags.Print)  _
            Or C1.Win.C1Preview.C1PreviewActionFlags.Reflow),C1.Win.C1Preview.C1PreviewActionFlags)
        Me.ppfitxa2.Location = New System.Drawing.Point(400, 15)
        Me.ppfitxa2.Name = "ppfitxa2"
        Me.ppfitxa2.NavigationPanelVisible = false
        '
        'ppfitxa2.PreviewPane
        '
        Me.ppfitxa2.PreviewPane.IntegrateExternalTools = true
        Me.ppfitxa2.PreviewPane.ShowToolTips = false
        Me.ppfitxa2.PreviewPane.TabIndex = 0
        Me.ppfitxa2.Size = New System.Drawing.Size(708, 537)
        Me.ppfitxa2.StatusBarVisible = false
        Me.ppfitxa2.TabIndex = 247
        Me.ppfitxa2.Text = "C1PrintPreviewControl1"
        '
        '
        '
        Me.ppfitxa2.ToolBars.File.Parameters.Image = CType(resources.GetObject("ppfitxa2.ToolBars.File.Parameters.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.File.Parameters.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.File.Parameters.Name = "btnParameters"
        Me.ppfitxa2.ToolBars.File.Parameters.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.File.Parameters.Tag = "C1PreviewActionEnum.Parameters"
        Me.ppfitxa2.ToolBars.File.Parameters.ToolTipText = "Report Parameters"
        Me.ppfitxa2.ToolBars.File.Parameters.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.File.PrintLayout.Image = CType(resources.GetObject("ppfitxa2.ToolBars.File.PrintLayout.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.File.PrintLayout.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.File.PrintLayout.Name = "btnPrintLayout"
        Me.ppfitxa2.ToolBars.File.PrintLayout.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.File.PrintLayout.Tag = "C1PreviewActionEnum.PrintLayout"
        Me.ppfitxa2.ToolBars.File.PrintLayout.ToolTipText = "Print Layout"
        Me.ppfitxa2.ToolBars.File.PrintLayout.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.File.Stop.Image = CType(resources.GetObject("ppfitxa2.ToolBars.File.Stop.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.File.Stop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.File.Stop.Name = "btnStop"
        Me.ppfitxa2.ToolBars.File.Stop.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.File.Stop.Tag = "C1PreviewActionEnum.Stop"
        Me.ppfitxa2.ToolBars.File.Stop.ToolTipText = "Stop"
        Me.ppfitxa2.ToolBars.File.Stop.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.GoFirst.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Navigation.GoFirst.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Navigation.GoFirst.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Navigation.GoFirst.Name = "btnGoFirst"
        Me.ppfitxa2.ToolBars.Navigation.GoFirst.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Navigation.GoFirst.Tag = "C1PreviewActionEnum.GoFirst"
        Me.ppfitxa2.ToolBars.Navigation.GoFirst.ToolTipText = "Go To First Page"
        Me.ppfitxa2.ToolBars.Navigation.GoFirst.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.GoLast.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Navigation.GoLast.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Navigation.GoLast.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Navigation.GoLast.Name = "btnGoLast"
        Me.ppfitxa2.ToolBars.Navigation.GoLast.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Navigation.GoLast.Tag = "C1PreviewActionEnum.GoLast"
        Me.ppfitxa2.ToolBars.Navigation.GoLast.ToolTipText = "Go To Last Page"
        Me.ppfitxa2.ToolBars.Navigation.GoLast.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.GoNext.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Navigation.GoNext.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Navigation.GoNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Navigation.GoNext.Name = "btnGoNext"
        Me.ppfitxa2.ToolBars.Navigation.GoNext.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Navigation.GoNext.Tag = "C1PreviewActionEnum.GoNext"
        Me.ppfitxa2.ToolBars.Navigation.GoNext.ToolTipText = "Go To Next Page"
        Me.ppfitxa2.ToolBars.Navigation.GoNext.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.GoPrev.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Navigation.GoPrev.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Navigation.GoPrev.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Navigation.GoPrev.Name = "btnGoPrev"
        Me.ppfitxa2.ToolBars.Navigation.GoPrev.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Navigation.GoPrev.Tag = "C1PreviewActionEnum.GoPrev"
        Me.ppfitxa2.ToolBars.Navigation.GoPrev.ToolTipText = "Go To Previous Page"
        Me.ppfitxa2.ToolBars.Navigation.GoPrev.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.HistoryNext.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Navigation.HistoryNext.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Navigation.HistoryNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Navigation.HistoryNext.Name = "btnHistoryNext"
        Me.ppfitxa2.ToolBars.Navigation.HistoryNext.Size = New System.Drawing.Size(32, 22)
        Me.ppfitxa2.ToolBars.Navigation.HistoryNext.Tag = "C1PreviewActionEnum.HistoryNext"
        Me.ppfitxa2.ToolBars.Navigation.HistoryNext.ToolTipText = "Next View"
        Me.ppfitxa2.ToolBars.Navigation.HistoryNext.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.HistoryPrev.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Navigation.HistoryPrev.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Navigation.HistoryPrev.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Navigation.HistoryPrev.Name = "btnHistoryPrev"
        Me.ppfitxa2.ToolBars.Navigation.HistoryPrev.Size = New System.Drawing.Size(32, 22)
        Me.ppfitxa2.ToolBars.Navigation.HistoryPrev.Tag = "C1PreviewActionEnum.HistoryPrev"
        Me.ppfitxa2.ToolBars.Navigation.HistoryPrev.ToolTipText = "Previous View"
        Me.ppfitxa2.ToolBars.Navigation.HistoryPrev.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.LblOfPages.Name = "lblOfPages"
        Me.ppfitxa2.ToolBars.Navigation.LblOfPages.Size = New System.Drawing.Size(27, 22)
        Me.ppfitxa2.ToolBars.Navigation.LblOfPages.Tag = "C1PreviewActionEnum.GoPageCount"
        Me.ppfitxa2.ToolBars.Navigation.LblOfPages.Text = "of 0"
        Me.ppfitxa2.ToolBars.Navigation.LblOfPages.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.LblPage.Name = "lblPage"
        Me.ppfitxa2.ToolBars.Navigation.LblPage.Size = New System.Drawing.Size(33, 22)
        Me.ppfitxa2.ToolBars.Navigation.LblPage.Tag = "C1PreviewActionEnum.GoPageLabel"
        Me.ppfitxa2.ToolBars.Navigation.LblPage.Text = "Page"
        Me.ppfitxa2.ToolBars.Navigation.LblPage.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.NavigationPane.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Navigation.NavigationPane.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Navigation.NavigationPane.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Navigation.NavigationPane.Name = "btnNavigationPane"
        Me.ppfitxa2.ToolBars.Navigation.NavigationPane.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Navigation.NavigationPane.Tag = "C1PreviewActionEnum.NavigationPane"
        Me.ppfitxa2.ToolBars.Navigation.NavigationPane.ToolTipText = "Navigation Pane"
        Me.ppfitxa2.ToolBars.Navigation.NavigationPane.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.PageNo.Name = "txtPageNo"
        Me.ppfitxa2.ToolBars.Navigation.PageNo.Size = New System.Drawing.Size(34, 25)
        Me.ppfitxa2.ToolBars.Navigation.PageNo.Tag = "C1PreviewActionEnum.GoPageNumber"
        Me.ppfitxa2.ToolBars.Navigation.PageNo.Text = "1"
        Me.ppfitxa2.ToolBars.Navigation.PageNo.Visible = false
        Me.ppfitxa2.ToolBars.Navigation.ToolTipPageNo = Nothing
        '
        '
        '
        Me.ppfitxa2.ToolBars.Page.Continuous.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Page.Continuous.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Page.Continuous.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Page.Continuous.Name = "btnPageContinuous"
        Me.ppfitxa2.ToolBars.Page.Continuous.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Page.Continuous.Tag = "C1PreviewActionEnum.PageContinuous"
        Me.ppfitxa2.ToolBars.Page.Continuous.ToolTipText = "Continuous View"
        Me.ppfitxa2.ToolBars.Page.Continuous.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Page.Facing.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Page.Facing.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Page.Facing.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Page.Facing.Name = "btnPageFacing"
        Me.ppfitxa2.ToolBars.Page.Facing.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Page.Facing.Tag = "C1PreviewActionEnum.PageFacing"
        Me.ppfitxa2.ToolBars.Page.Facing.ToolTipText = "Pages Facing View"
        Me.ppfitxa2.ToolBars.Page.Facing.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Page.FacingContinuous.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Page.FacingContinuous.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Page.FacingContinuous.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Page.FacingContinuous.Name = "btnPageFacingContinuous"
        Me.ppfitxa2.ToolBars.Page.FacingContinuous.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Page.FacingContinuous.Tag = "C1PreviewActionEnum.PageFacingContinuous"
        Me.ppfitxa2.ToolBars.Page.FacingContinuous.ToolTipText = "Pages Facing Continuous View"
        Me.ppfitxa2.ToolBars.Page.FacingContinuous.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Page.Single.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Page.Single.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Page.Single.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Page.Single.Name = "btnPageSingle"
        Me.ppfitxa2.ToolBars.Page.Single.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Page.Single.Tag = "C1PreviewActionEnum.PageSingle"
        Me.ppfitxa2.ToolBars.Page.Single.ToolTipText = "Single Page View"
        Me.ppfitxa2.ToolBars.Page.Single.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Search.CloseSearch.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Search.CloseSearch.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Search.CloseSearch.Name = "btnCloseSearch"
        Me.ppfitxa2.ToolBars.Search.CloseSearch.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Search.CloseSearch.Tag = "C1PreviewActionEnum.CloseSearch"
        Me.ppfitxa2.ToolBars.Search.CloseSearch.ToolTipText = "Close"
        Me.ppfitxa2.ToolBars.Search.CloseSearch.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Search.MatchCase.Name = "btnMatchCase"
        Me.ppfitxa2.ToolBars.Search.MatchCase.Size = New System.Drawing.Size(73, 22)
        Me.ppfitxa2.ToolBars.Search.MatchCase.Tag = "C1PreviewActionEnum.MatchCase"
        Me.ppfitxa2.ToolBars.Search.MatchCase.Text = "Match Case"
        Me.ppfitxa2.ToolBars.Search.MatchCase.ToolTipText = "Search with case sensitivity"
        Me.ppfitxa2.ToolBars.Search.MatchCase.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Search.MatchWholeWord.Name = "btnMatchWholeWord"
        Me.ppfitxa2.ToolBars.Search.MatchWholeWord.Size = New System.Drawing.Size(77, 22)
        Me.ppfitxa2.ToolBars.Search.MatchWholeWord.Tag = "C1PreviewActionEnum.MatchWholeWord"
        Me.ppfitxa2.ToolBars.Search.MatchWholeWord.Text = "Whole Word"
        Me.ppfitxa2.ToolBars.Search.MatchWholeWord.ToolTipText = "Match whole word only"
        Me.ppfitxa2.ToolBars.Search.MatchWholeWord.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Search.SearchLabel.Name = "lblSearch"
        Me.ppfitxa2.ToolBars.Search.SearchLabel.Size = New System.Drawing.Size(33, 22)
        Me.ppfitxa2.ToolBars.Search.SearchLabel.Tag = "C1PreviewActionEnum.SearchLabel"
        Me.ppfitxa2.ToolBars.Search.SearchLabel.Text = "Find:"
        Me.ppfitxa2.ToolBars.Search.SearchLabel.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Search.SearchNext.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Search.SearchNext.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Search.SearchNext.Name = "btnSearchNext"
        Me.ppfitxa2.ToolBars.Search.SearchNext.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Search.SearchNext.Tag = "C1PreviewActionEnum.SearchNext"
        Me.ppfitxa2.ToolBars.Search.SearchNext.ToolTipText = "Search Next"
        Me.ppfitxa2.ToolBars.Search.SearchNext.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Search.SearchPrevious.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Search.SearchPrevious.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Search.SearchPrevious.Name = "btnSearchPrevious"
        Me.ppfitxa2.ToolBars.Search.SearchPrevious.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Search.SearchPrevious.Tag = "C1PreviewActionEnum.SearchPrevious"
        Me.ppfitxa2.ToolBars.Search.SearchPrevious.ToolTipText = "Search Previous"
        Me.ppfitxa2.ToolBars.Search.SearchPrevious.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Search.SearchText.Name = "txtSearchText"
        Me.ppfitxa2.ToolBars.Search.SearchText.Size = New System.Drawing.Size(280, 27)
        Me.ppfitxa2.ToolBars.Search.SearchText.Tag = "C1PreviewActionEnum.SearchText"
        Me.ppfitxa2.ToolBars.Search.SearchText.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Text.Find.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Text.Find.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Text.Find.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Text.Find.Name = "btnFind"
        Me.ppfitxa2.ToolBars.Text.Find.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Text.Find.Tag = "C1PreviewActionEnum.Find"
        Me.ppfitxa2.ToolBars.Text.Find.ToolTipText = "Find Text"
        Me.ppfitxa2.ToolBars.Text.Find.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Text.Hand.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Text.Hand.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Text.Hand.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Text.Hand.Name = "btnHandTool"
        Me.ppfitxa2.ToolBars.Text.Hand.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Text.Hand.Tag = "C1PreviewActionEnum.HandTool"
        Me.ppfitxa2.ToolBars.Text.Hand.ToolTipText = "Hand Tool"
        Me.ppfitxa2.ToolBars.Text.Hand.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Text.SelectText.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Text.SelectText.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Text.SelectText.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Text.SelectText.Name = "btnSelectTextTool"
        Me.ppfitxa2.ToolBars.Text.SelectText.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Text.SelectText.Tag = "C1PreviewActionEnum.SelectTextTool"
        Me.ppfitxa2.ToolBars.Text.SelectText.ToolTipText = "Text Select Tool"
        Me.ppfitxa2.ToolBars.Text.SelectText.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Zoom.DropZoomFactor.Name = "dropZoomFactor"
        Me.ppfitxa2.ToolBars.Zoom.DropZoomFactor.Size = New System.Drawing.Size(13, 22)
        Me.ppfitxa2.ToolBars.Zoom.DropZoomFactor.Tag = "C1PreviewActionEnum.ZoomFactor"
        Me.ppfitxa2.ToolBars.Zoom.DropZoomFactor.Visible = false
        Me.ppfitxa2.ToolBars.Zoom.ToolTipZoomFactor = Nothing
        '
        '
        '
        Me.ppfitxa2.ToolBars.Zoom.ZoomFactor.Name = "txtZoomFactor"
        Me.ppfitxa2.ToolBars.Zoom.ZoomFactor.Size = New System.Drawing.Size(47, 27)
        Me.ppfitxa2.ToolBars.Zoom.ZoomFactor.Tag = "C1PreviewActionEnum.ZoomFactor"
        Me.ppfitxa2.ToolBars.Zoom.ZoomFactor.Text = "100%"
        Me.ppfitxa2.ToolBars.Zoom.ZoomFactor.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Zoom.ZoomIn.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Zoom.ZoomIn.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Zoom.ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Zoom.ZoomIn.Name = "btnZoomIn"
        Me.ppfitxa2.ToolBars.Zoom.ZoomIn.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Zoom.ZoomIn.Tag = "C1PreviewActionEnum.ZoomIn"
        Me.ppfitxa2.ToolBars.Zoom.ZoomIn.ToolTipText = "Zoom In"
        Me.ppfitxa2.ToolBars.Zoom.ZoomIn.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Zoom.ZoomInTool.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Zoom.ZoomInTool.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Zoom.ZoomInTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Zoom.ZoomInTool.Name = "itemZoomInTool"
        Me.ppfitxa2.ToolBars.Zoom.ZoomInTool.Size = New System.Drawing.Size(193, 26)
        Me.ppfitxa2.ToolBars.Zoom.ZoomInTool.Tag = "C1PreviewActionEnum.ZoomInTool"
        Me.ppfitxa2.ToolBars.Zoom.ZoomInTool.Text = "Zoom In Tool"
        Me.ppfitxa2.ToolBars.Zoom.ZoomInTool.ToolTipText = "Zoom In Tool"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Zoom.ZoomOut.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Zoom.ZoomOut.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Zoom.ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Zoom.ZoomOut.Name = "btnZoomOut"
        Me.ppfitxa2.ToolBars.Zoom.ZoomOut.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Zoom.ZoomOut.Tag = "C1PreviewActionEnum.ZoomOut"
        Me.ppfitxa2.ToolBars.Zoom.ZoomOut.ToolTipText = "Zoom Out"
        Me.ppfitxa2.ToolBars.Zoom.ZoomOut.Visible = false
        '
        '
        '
        Me.ppfitxa2.ToolBars.Zoom.ZoomOutTool.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Zoom.ZoomOutTool.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Zoom.ZoomOutTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Zoom.ZoomOutTool.Name = "itemZoomOutTool"
        Me.ppfitxa2.ToolBars.Zoom.ZoomOutTool.Size = New System.Drawing.Size(193, 26)
        Me.ppfitxa2.ToolBars.Zoom.ZoomOutTool.Tag = "C1PreviewActionEnum.ZoomOutTool"
        Me.ppfitxa2.ToolBars.Zoom.ZoomOutTool.Text = "Zoom Out Tool"
        Me.ppfitxa2.ToolBars.Zoom.ZoomOutTool.ToolTipText = "Zoom Out Tool"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Zoom.ZoomTool.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ppfitxa2.ToolBars.Zoom.ZoomInTool, Me.ppfitxa2.ToolBars.Zoom.ZoomOutTool})
        Me.ppfitxa2.ToolBars.Zoom.ZoomTool.Image = CType(resources.GetObject("ppfitxa2.ToolBars.Zoom.ZoomTool.Image"),System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Zoom.ZoomTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Zoom.ZoomTool.Name = "btnZoomTool"
        Me.ppfitxa2.ToolBars.Zoom.ZoomTool.Size = New System.Drawing.Size(32, 22)
        Me.ppfitxa2.ToolBars.Zoom.ZoomTool.Tag = "C1PreviewActionEnum.ZoomInTool"
        Me.ppfitxa2.ToolBars.Zoom.ZoomTool.ToolTipText = "Zoom In Tool"
        Me.ppfitxa2.ToolBars.Zoom.ZoomTool.Visible = false
        '
        'lstOrdenes
        '
        Me.lstOrdenes.AllowColMove = false
        Me.lstOrdenes.AllowColSelect = false
        Me.lstOrdenes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lstOrdenes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstOrdenes.CaptionHeight = 17
        Me.lstOrdenes.ColumnCaptionHeight = 17
        Me.lstOrdenes.ColumnFooterHeight = 17
        Me.lstOrdenes.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark
        Me.lstOrdenes.Images.Add(CType(resources.GetObject("lstOrdenes.Images"),System.Drawing.Image))
        Me.lstOrdenes.Location = New System.Drawing.Point(22, 366)
        Me.lstOrdenes.MatchEntryTimeout = CType(2000,Long)
        Me.lstOrdenes.Name = "lstOrdenes"
        Me.lstOrdenes.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.lstOrdenes.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.lstOrdenes.PreviewInfo.ZoomFactor = 75R
        Me.lstOrdenes.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.lstOrdenes.SelectionMode = C1.Win.C1List.SelectionModeEnum.CheckBox
        Me.lstOrdenes.Size = New System.Drawing.Size(140, 108)
        Me.lstOrdenes.TabIndex = 244
        Me.lstOrdenes.Visible = false
        Me.lstOrdenes.PropBag = resources.GetString("lstOrdenes.PropBag")
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.cboDeFacuraRecibo)
        Me.GroupBox4.Controls.Add(Me.chkCondicionFecha)
        Me.GroupBox4.Controls.Add(Me.lblImpReciboAFecha)
        Me.GroupBox4.Controls.Add(Me.lblImpReciboDeFecha)
        Me.GroupBox4.Controls.Add(Me.dtpAFechaRecibo)
        Me.GroupBox4.Controls.Add(Me.dtpDeFechaRecibo)
        Me.GroupBox4.Controls.Add(Me.lblAFactura)
        Me.GroupBox4.Controls.Add(Me.lblDeFactura)
        Me.GroupBox4.Controls.Add(Me.cboAFacturaRecibo)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 15)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(380, 282)
        Me.GroupBox4.TabIndex = 243
        Me.GroupBox4.TabStop = false
        '
        'cboDeFacuraRecibo
        '
        Me.cboDeFacuraRecibo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cboDeFacuraRecibo.AutoSelect = true
        Me.cboDeFacuraRecibo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboDeFacuraRecibo.Caption = ""
        Me.cboDeFacuraRecibo.CaptionHeight = 17
        Me.cboDeFacuraRecibo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboDeFacuraRecibo.ColumnCaptionHeight = 17
        Me.cboDeFacuraRecibo.ColumnFooterHeight = 17
        Me.cboDeFacuraRecibo.ContentHeight = 18
        Me.cboDeFacuraRecibo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDeFacuraRecibo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboDeFacuraRecibo.EditorBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.cboDeFacuraRecibo.Images.Add(CType(resources.GetObject("cboDeFacuraRecibo.Images"),System.Drawing.Image))
        Me.cboDeFacuraRecibo.IntegralHeight = true
        Me.cboDeFacuraRecibo.Location = New System.Drawing.Point(146, 34)
        Me.cboDeFacuraRecibo.MatchEntryTimeout = CType(100,Long)
        Me.cboDeFacuraRecibo.MaxDropDownItems = CType(8,Short)
        Me.cboDeFacuraRecibo.MaxLength = 0
        Me.cboDeFacuraRecibo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboDeFacuraRecibo.Name = "cboDeFacuraRecibo"
        Me.cboDeFacuraRecibo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboDeFacuraRecibo.Size = New System.Drawing.Size(156, 19)
        Me.cboDeFacuraRecibo.TabIndex = 9
        Me.cboDeFacuraRecibo.PropBag = resources.GetString("cboDeFacuraRecibo.PropBag")
        '
        'chkCondicionFecha
        '
        Me.chkCondicionFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkCondicionFecha.Location = New System.Drawing.Point(17, 128)
        Me.chkCondicionFecha.Name = "chkCondicionFecha"
        Me.chkCondicionFecha.Size = New System.Drawing.Size(229, 30)
        Me.chkCondicionFecha.TabIndex = 11
        Me.chkCondicionFecha.Text = "Per interval de dates"
        '
        'lblImpReciboAFecha
        '
        Me.lblImpReciboAFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblImpReciboAFecha.Location = New System.Drawing.Point(17, 207)
        Me.lblImpReciboAFecha.Name = "lblImpReciboAFecha"
        Me.lblImpReciboAFecha.Size = New System.Drawing.Size(123, 24)
        Me.lblImpReciboAFecha.TabIndex = 4
        Me.lblImpReciboAFecha.Text = "A Data"
        '
        'lblImpReciboDeFecha
        '
        Me.lblImpReciboDeFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblImpReciboDeFecha.Location = New System.Drawing.Point(17, 172)
        Me.lblImpReciboDeFecha.Name = "lblImpReciboDeFecha"
        Me.lblImpReciboDeFecha.Size = New System.Drawing.Size(123, 25)
        Me.lblImpReciboDeFecha.TabIndex = 3
        Me.lblImpReciboDeFecha.Text = "De Data"
        '
        'dtpAFechaRecibo
        '
        Me.dtpAFechaRecibo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.dtpAFechaRecibo.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        '
        '
        '
        Me.dtpAFechaRecibo.Calendar.DayNameLength = 1
        Me.dtpAFechaRecibo.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpAFechaRecibo.Enabled = false
        Me.dtpAFechaRecibo.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpAFechaRecibo.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dtpAFechaRecibo.Location = New System.Drawing.Point(151, 207)
        Me.dtpAFechaRecibo.Name = "dtpAFechaRecibo"
        Me.dtpAFechaRecibo.Size = New System.Drawing.Size(207, 21)
        Me.dtpAFechaRecibo.TabIndex = 2
        Me.dtpAFechaRecibo.Tag = Nothing
        '
        'dtpDeFechaRecibo
        '
        Me.dtpDeFechaRecibo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.dtpDeFechaRecibo.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        '
        '
        '
        Me.dtpDeFechaRecibo.Calendar.DayNameLength = 1
        Me.dtpDeFechaRecibo.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpDeFechaRecibo.Enabled = false
        Me.dtpDeFechaRecibo.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpDeFechaRecibo.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dtpDeFechaRecibo.Location = New System.Drawing.Point(151, 172)
        Me.dtpDeFechaRecibo.Name = "dtpDeFechaRecibo"
        Me.dtpDeFechaRecibo.Size = New System.Drawing.Size(207, 21)
        Me.dtpDeFechaRecibo.TabIndex = 1
        Me.dtpDeFechaRecibo.Tag = Nothing
        '
        'lblAFactura
        '
        Me.lblAFactura.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAFactura.Location = New System.Drawing.Point(17, 74)
        Me.lblAFactura.Name = "lblAFactura"
        Me.lblAFactura.Size = New System.Drawing.Size(123, 24)
        Me.lblAFactura.TabIndex = 8
        Me.lblAFactura.Text = "A Factura"
        '
        'lblDeFactura
        '
        Me.lblDeFactura.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDeFactura.Location = New System.Drawing.Point(17, 34)
        Me.lblDeFactura.Name = "lblDeFactura"
        Me.lblDeFactura.Size = New System.Drawing.Size(123, 25)
        Me.lblDeFactura.TabIndex = 7
        Me.lblDeFactura.Text = "De Factura"
        '
        'cboAFacturaRecibo
        '
        Me.cboAFacturaRecibo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cboAFacturaRecibo.AutoSelect = true
        Me.cboAFacturaRecibo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboAFacturaRecibo.Caption = ""
        Me.cboAFacturaRecibo.CaptionHeight = 17
        Me.cboAFacturaRecibo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboAFacturaRecibo.ColumnCaptionHeight = 17
        Me.cboAFacturaRecibo.ColumnFooterHeight = 17
        Me.cboAFacturaRecibo.ContentHeight = 18
        Me.cboAFacturaRecibo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAFacturaRecibo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboAFacturaRecibo.EditorBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.cboAFacturaRecibo.Images.Add(CType(resources.GetObject("cboAFacturaRecibo.Images"),System.Drawing.Image))
        Me.cboAFacturaRecibo.IntegralHeight = true
        Me.cboAFacturaRecibo.Location = New System.Drawing.Point(146, 74)
        Me.cboAFacturaRecibo.MatchEntryTimeout = CType(100,Long)
        Me.cboAFacturaRecibo.MaxDropDownItems = CType(8,Short)
        Me.cboAFacturaRecibo.MaxLength = 0
        Me.cboAFacturaRecibo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboAFacturaRecibo.Name = "cboAFacturaRecibo"
        Me.cboAFacturaRecibo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboAFacturaRecibo.Size = New System.Drawing.Size(156, 19)
        Me.cboAFacturaRecibo.TabIndex = 10
        Me.cboAFacturaRecibo.PropBag = resources.GetString("cboAFacturaRecibo.PropBag")
        '
        'btnGenerarRecibo
        '
        Me.btnGenerarRecibo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnGenerarRecibo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerarRecibo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerarRecibo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnGenerarRecibo.Location = New System.Drawing.Point(259, 302)
        Me.btnGenerarRecibo.Name = "btnGenerarRecibo"
        Me.btnGenerarRecibo.Size = New System.Drawing.Size(134, 29)
        Me.btnGenerarRecibo.TabIndex = 11
        Me.btnGenerarRecibo.TabStop = false
        Me.btnGenerarRecibo.Text = "Generar Rebuts"
        Me.btnGenerarRecibo.UseVisualStyleBackColor = true
        '
        'tabPageTraspasoPAF
        '
        Me.tabPageTraspasoPAF.Controls.Add(Me.GroupBox1)
        Me.tabPageTraspasoPAF.Controls.Add(Me.dateDesdeFecha)
        Me.tabPageTraspasoPAF.Controls.Add(Me.gbTraspasarLista)
        Me.tabPageTraspasoPAF.Controls.Add(Me.gbTraspasarIntervalo)
        Me.tabPageTraspasoPAF.Controls.Add(Me.lblDesdeFecha)
        Me.tabPageTraspasoPAF.Location = New System.Drawing.Point(4, 22)
        Me.tabPageTraspasoPAF.Name = "tabPageTraspasoPAF"
        Me.tabPageTraspasoPAF.Size = New System.Drawing.Size(994, 529)
        Me.tabPageTraspasoPAF.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.dtrDATA)
        Me.GroupBox1.Controls.Add(Me.txrNOMCLIENT)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lbrFRA)
        Me.GroupBox1.Controls.Add(Me.txrFRA)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(616, 123)
        Me.GroupBox1.TabIndex = 386
        Me.GroupBox1.TabStop = false
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(92, 79)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 24)
        Me.Label5.TabIndex = 385
        Me.Label5.Text = "Data"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtrDATA
        '
        '
        '
        '
        Me.dtrDATA.Calendar.DayNameLength = 1
        Me.dtrDATA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtrDATA.DisableOnNoData = false
        Me.dtrDATA.EmptyAsNull = true
        Me.dtrDATA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtrDATA.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dtrDATA.Location = New System.Drawing.Point(129, 79)
        Me.dtrDATA.Name = "dtrDATA"
        Me.dtrDATA.ReadOnly = true
        Me.dtrDATA.Size = New System.Drawing.Size(168, 21)
        Me.dtrDATA.TabIndex = 384
        Me.dtrDATA.Tag = Nothing
        '
        'txrNOMCLIENT
        '
        Me.txrNOMCLIENT.Location = New System.Drawing.Point(129, 49)
        Me.txrNOMCLIENT.Name = "txrNOMCLIENT"
        Me.txrNOMCLIENT.ReadOnly = true
        Me.txrNOMCLIENT.Size = New System.Drawing.Size(330, 21)
        Me.txrNOMCLIENT.TabIndex = 189
        Me.txrNOMCLIENT.Tag = Nothing
        '
        'Label1
        '
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(22, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 24)
        Me.Label1.TabIndex = 178
        Me.Label1.Text = "Client"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbrFRA
        '
        Me.lbrFRA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbrFRA.Location = New System.Drawing.Point(22, 20)
        Me.lbrFRA.Name = "lbrFRA"
        Me.lbrFRA.Size = New System.Drawing.Size(98, 24)
        Me.lbrFRA.TabIndex = 180
        Me.lbrFRA.Text = "Factura"
        Me.lbrFRA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txrFRA
        '
        Me.txrFRA.Location = New System.Drawing.Point(129, 20)
        Me.txrFRA.Name = "txrFRA"
        Me.txrFRA.ReadOnly = true
        Me.txrFRA.Size = New System.Drawing.Size(140, 21)
        Me.txrFRA.TabIndex = 188
        Me.txrFRA.Tag = Nothing
        '
        'dateDesdeFecha
        '
        '
        '
        '
        Me.dateDesdeFecha.Calendar.DayNameLength = 1
        Me.dateDesdeFecha.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dateDesdeFecha.DisableOnNoData = false
        Me.dateDesdeFecha.EmptyAsNull = true
        Me.dateDesdeFecha.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dateDesdeFecha.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dateDesdeFecha.Location = New System.Drawing.Point(314, 148)
        Me.dateDesdeFecha.Name = "dateDesdeFecha"
        Me.dateDesdeFecha.Size = New System.Drawing.Size(184, 21)
        Me.dateDesdeFecha.TabIndex = 385
        Me.dateDesdeFecha.Tag = Nothing
        Me.dateDesdeFecha.Visible = false
        '
        'gbTraspasarLista
        '
        Me.gbTraspasarLista.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.gbTraspasarLista.BackColor = System.Drawing.SystemColors.Control
        Me.gbTraspasarLista.Controls.Add(Me.tlpTab3)
        Me.gbTraspasarLista.Controls.Add(Me.btnAbrirAF)
        Me.gbTraspasarLista.Location = New System.Drawing.Point(648, 10)
        Me.gbTraspasarLista.Name = "gbTraspasarLista"
        Me.gbTraspasarLista.Size = New System.Drawing.Size(346, 516)
        Me.gbTraspasarLista.TabIndex = 26
        Me.gbTraspasarLista.TabStop = false
        '
        'tlpTab3
        '
        Me.tlpTab3.ColumnCount = 2
        Me.tlpTab3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.67327!))
        Me.tlpTab3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.32673!))
        Me.tlpTab3.Controls.Add(Me.pnlTab31, 0, 0)
        Me.tlpTab3.Controls.Add(Me.pnlTab32, 1, 0)
        Me.tlpTab3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpTab3.Location = New System.Drawing.Point(3, 19)
        Me.tlpTab3.Name = "tlpTab3"
        Me.tlpTab3.RowCount = 1
        Me.tlpTab3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tlpTab3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 494!))
        Me.tlpTab3.Size = New System.Drawing.Size(340, 494)
        Me.tlpTab3.TabIndex = 222
        '
        'pnlTab31
        '
        Me.pnlTab31.Controls.Add(Me.lblVieneDe)
        Me.pnlTab31.Controls.Add(Me.dgTraspas)
        Me.pnlTab31.Controls.Add(Me.btnHacerTraspaso)
        Me.pnlTab31.Controls.Add(Me.btnAbrirPAF)
        Me.pnlTab31.Controls.Add(Me.lstVieneDePedido)
        Me.pnlTab31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTab31.Location = New System.Drawing.Point(3, 3)
        Me.pnlTab31.Name = "pnlTab31"
        Me.pnlTab31.Size = New System.Drawing.Size(173, 488)
        Me.pnlTab31.TabIndex = 0
        '
        'lblVieneDe
        '
        Me.lblVieneDe.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblVieneDe.Location = New System.Drawing.Point(4, 62)
        Me.lblVieneDe.Name = "lblVieneDe"
        Me.lblVieneDe.Size = New System.Drawing.Size(65, 78)
        Me.lblVieneDe.TabIndex = 221
        Me.lblVieneDe.Text = "Label4"
        Me.lblVieneDe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dgTraspas
        '
        Me.dgTraspas.AllowColMove = false
        Me.dgTraspas.AllowColSelect = false
        Me.dgTraspas.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgTraspas.AllowUpdate = false
        Me.dgTraspas.AllowUpdateOnBlur = false
        Me.dgTraspas.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.dgTraspas.CaptionHeight = 19
        Me.dgTraspas.FilterBar = true
        Me.dgTraspas.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgTraspas.Images.Add(CType(resources.GetObject("dgTraspas.Images"),System.Drawing.Image))
        Me.dgTraspas.Location = New System.Drawing.Point(8, 394)
        Me.dgTraspas.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgTraspas.Name = "dgTraspas"
        Me.dgTraspas.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgTraspas.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgTraspas.PreviewInfo.ZoomFactor = 75R
        Me.dgTraspas.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgTraspas.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgTraspas.RowHeight = 17
        Me.dgTraspas.Size = New System.Drawing.Size(101, 29)
        Me.dgTraspas.SpringMode = true
        Me.dgTraspas.TabIndex = 25
        Me.dgTraspas.Text = "C1TrueDBGrid1"
        Me.dgTraspas.UseCompatibleTextRendering = false
        Me.dgTraspas.Visible = false
        Me.dgTraspas.WrapCellPointer = true
        Me.dgTraspas.PropBag = resources.GetString("dgTraspas.PropBag")
        '
        'btnHacerTraspaso
        '
        Me.btnHacerTraspaso.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnHacerTraspaso.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHacerTraspaso.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnHacerTraspaso.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnHacerTraspaso.Location = New System.Drawing.Point(4, 443)
        Me.btnHacerTraspaso.Name = "btnHacerTraspaso"
        Me.btnHacerTraspaso.Size = New System.Drawing.Size(151, 39)
        Me.btnHacerTraspaso.TabIndex = 20
        Me.btnHacerTraspaso.TabStop = false
        Me.btnHacerTraspaso.Text = "Fer Traspàs"
        Me.btnHacerTraspaso.UseVisualStyleBackColor = true
        Me.btnHacerTraspaso.Visible = false
        '
        'btnAbrirPAF
        '
        Me.btnAbrirPAF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAbrirPAF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAbrirPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAbrirPAF.Location = New System.Drawing.Point(91, 396)
        Me.btnAbrirPAF.Name = "btnAbrirPAF"
        Me.btnAbrirPAF.Size = New System.Drawing.Size(79, 27)
        Me.btnAbrirPAF.TabIndex = 219
        Me.btnAbrirPAF.TabStop = false
        Me.btnAbrirPAF.Text = "Obrir"
        Me.btnAbrirPAF.UseVisualStyleBackColor = true
        Me.btnAbrirPAF.Visible = false
        '
        'lstVieneDePedido
        '
        Me.lstVieneDePedido.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lstVieneDePedido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstVieneDePedido.ItemHeight = 17
        Me.lstVieneDePedido.Location = New System.Drawing.Point(75, 22)
        Me.lstVieneDePedido.Name = "lstVieneDePedido"
        Me.lstVieneDePedido.Size = New System.Drawing.Size(91, 342)
        Me.lstVieneDePedido.TabIndex = 217
        '
        'pnlTab32
        '
        Me.pnlTab32.Controls.Add(Me.lstFormaParte)
        Me.pnlTab32.Controls.Add(Me.lblFormaParte)
        Me.pnlTab32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTab32.Location = New System.Drawing.Point(182, 3)
        Me.pnlTab32.Name = "pnlTab32"
        Me.pnlTab32.Size = New System.Drawing.Size(155, 488)
        Me.pnlTab32.TabIndex = 1
        '
        'lstFormaParte
        '
        Me.lstFormaParte.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lstFormaParte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstFormaParte.ItemHeight = 17
        Me.lstFormaParte.Location = New System.Drawing.Point(68, 22)
        Me.lstFormaParte.Name = "lstFormaParte"
        Me.lstFormaParte.Size = New System.Drawing.Size(84, 257)
        Me.lstFormaParte.TabIndex = 216
        '
        'lblFormaParte
        '
        Me.lblFormaParte.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFormaParte.Location = New System.Drawing.Point(8, 58)
        Me.lblFormaParte.Name = "lblFormaParte"
        Me.lblFormaParte.Size = New System.Drawing.Size(118, 79)
        Me.lblFormaParte.TabIndex = 220
        Me.lblFormaParte.Text = "Label4"
        Me.lblFormaParte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnAbrirAF
        '
        Me.btnAbrirAF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnAbrirAF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAbrirAF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAbrirAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAbrirAF.Location = New System.Drawing.Point(60, 133)
        Me.btnAbrirAF.Name = "btnAbrirAF"
        Me.btnAbrirAF.Size = New System.Drawing.Size(78, 27)
        Me.btnAbrirAF.TabIndex = 218
        Me.btnAbrirAF.TabStop = false
        Me.btnAbrirAF.Text = "Obrir"
        Me.btnAbrirAF.UseVisualStyleBackColor = true
        Me.btnAbrirAF.Visible = false
        '
        'gbTraspasarIntervalo
        '
        Me.gbTraspasarIntervalo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.gbTraspasarIntervalo.BackColor = System.Drawing.SystemColors.Control
        Me.gbTraspasarIntervalo.Controls.Add(Me.txtAFecha)
        Me.gbTraspasarIntervalo.Controls.Add(Me.date2)
        Me.gbTraspasarIntervalo.Controls.Add(Me.date1)
        Me.gbTraspasarIntervalo.Controls.Add(Me.txtAPAF)
        Me.gbTraspasarIntervalo.Controls.Add(Me.cmbPAF1)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblAPAF)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblConFecha)
        Me.gbTraspasarIntervalo.Controls.Add(Me.cmbPAF2)
        Me.gbTraspasarIntervalo.Controls.Add(Me.btnTraspasarAPAF)
        Me.gbTraspasarIntervalo.Controls.Add(Me.cmbCliente1)
        Me.gbTraspasarIntervalo.Controls.Add(Me.cmbCliente2)
        Me.gbTraspasarIntervalo.Controls.Add(Me.btnTraspasarAProforma)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblDeFecha)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblHastaCliente)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblHastaFecha)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblHastaPAF)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblDePAF)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblDeCliente)
        Me.gbTraspasarIntervalo.Location = New System.Drawing.Point(3, 180)
        Me.gbTraspasarIntervalo.Name = "gbTraspasarIntervalo"
        Me.gbTraspasarIntervalo.Size = New System.Drawing.Size(616, 269)
        Me.gbTraspasarIntervalo.TabIndex = 25
        Me.gbTraspasarIntervalo.TabStop = false
        '
        'txtAFecha
        '
        '
        '
        '
        Me.txtAFecha.Calendar.DayNameLength = 1
        Me.txtAFecha.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtAFecha.DisableOnNoData = false
        Me.txtAFecha.EmptyAsNull = true
        Me.txtAFecha.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.txtAFecha.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.txtAFecha.Location = New System.Drawing.Point(431, 167)
        Me.txtAFecha.Name = "txtAFecha"
        Me.txtAFecha.Size = New System.Drawing.Size(151, 21)
        Me.txtAFecha.TabIndex = 384
        Me.txtAFecha.Tag = Nothing
        '
        'date2
        '
        '
        '
        '
        Me.date2.Calendar.DayNameLength = 1
        Me.date2.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.date2.DisableOnNoData = false
        Me.date2.EmptyAsNull = true
        Me.date2.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.date2.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.date2.Location = New System.Drawing.Point(431, 69)
        Me.date2.Name = "date2"
        Me.date2.Size = New System.Drawing.Size(174, 21)
        Me.date2.TabIndex = 383
        Me.date2.Tag = Nothing
        '
        'date1
        '
        '
        '
        '
        Me.date1.Calendar.DayNameLength = 1
        Me.date1.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.date1.DisableOnNoData = false
        Me.date1.EmptyAsNull = true
        Me.date1.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.date1.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.date1.Location = New System.Drawing.Point(106, 69)
        Me.date1.Name = "date1"
        Me.date1.Size = New System.Drawing.Size(174, 21)
        Me.date1.TabIndex = 382
        Me.date1.Tag = Nothing
        '
        'txtAPAF
        '
        Me.txtAPAF.Location = New System.Drawing.Point(146, 167)
        Me.txtAPAF.Name = "txtAPAF"
        Me.txtAPAF.Size = New System.Drawing.Size(140, 21)
        Me.txtAPAF.TabIndex = 268
        Me.txtAPAF.Tag = Nothing
        '
        'cmbPAF1
        '
        Me.cmbPAF1.AutoSelect = true
        Me.cmbPAF1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPAF1.Caption = ""
        Me.cmbPAF1.CaptionHeight = 17
        Me.cmbPAF1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbPAF1.ColumnCaptionHeight = 17
        Me.cmbPAF1.ColumnFooterHeight = 17
        Me.cmbPAF1.ContentHeight = 18
        Me.cmbPAF1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPAF1.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cmbPAF1.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbPAF1.Images.Add(CType(resources.GetObject("cmbPAF1.Images"),System.Drawing.Image))
        Me.cmbPAF1.IntegralHeight = true
        Me.cmbPAF1.Location = New System.Drawing.Point(106, 39)
        Me.cmbPAF1.MatchEntryTimeout = CType(100,Long)
        Me.cmbPAF1.MaxDropDownItems = CType(8,Short)
        Me.cmbPAF1.MaxLength = 0
        Me.cmbPAF1.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbPAF1.Name = "cmbPAF1"
        Me.cmbPAF1.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbPAF1.Size = New System.Drawing.Size(174, 19)
        Me.cmbPAF1.TabIndex = 0
        Me.cmbPAF1.PropBag = resources.GetString("cmbPAF1.PropBag")
        '
        'lblAPAF
        '
        Me.lblAPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAPAF.Location = New System.Drawing.Point(62, 167)
        Me.lblAPAF.Name = "lblAPAF"
        Me.lblAPAF.Size = New System.Drawing.Size(140, 21)
        Me.lblAPAF.TabIndex = 16
        Me.lblAPAF.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblConFecha
        '
        Me.lblConFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblConFecha.Location = New System.Drawing.Point(308, 167)
        Me.lblConFecha.Name = "lblConFecha"
        Me.lblConFecha.Size = New System.Drawing.Size(118, 21)
        Me.lblConFecha.TabIndex = 18
        Me.lblConFecha.Text = "Amb Data"
        Me.lblConFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbPAF2
        '
        Me.cmbPAF2.AutoSelect = true
        Me.cmbPAF2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPAF2.Caption = ""
        Me.cmbPAF2.CaptionHeight = 17
        Me.cmbPAF2.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbPAF2.ColumnCaptionHeight = 17
        Me.cmbPAF2.ColumnFooterHeight = 17
        Me.cmbPAF2.ContentHeight = 18
        Me.cmbPAF2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPAF2.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cmbPAF2.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbPAF2.Images.Add(CType(resources.GetObject("cmbPAF2.Images"),System.Drawing.Image))
        Me.cmbPAF2.IntegralHeight = true
        Me.cmbPAF2.Location = New System.Drawing.Point(431, 39)
        Me.cmbPAF2.MatchEntryTimeout = CType(100,Long)
        Me.cmbPAF2.MaxDropDownItems = CType(8,Short)
        Me.cmbPAF2.MaxLength = 0
        Me.cmbPAF2.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbPAF2.Name = "cmbPAF2"
        Me.cmbPAF2.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbPAF2.Size = New System.Drawing.Size(174, 19)
        Me.cmbPAF2.TabIndex = 1
        Me.cmbPAF2.PropBag = resources.GetString("cmbPAF2.PropBag")
        '
        'btnTraspasarAPAF
        '
        Me.btnTraspasarAPAF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTraspasarAPAF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTraspasarAPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnTraspasarAPAF.Location = New System.Drawing.Point(336, 211)
        Me.btnTraspasarAPAF.Name = "btnTraspasarAPAF"
        Me.btnTraspasarAPAF.Size = New System.Drawing.Size(269, 44)
        Me.btnTraspasarAPAF.TabIndex = 9
        Me.btnTraspasarAPAF.TabStop = false
        Me.btnTraspasarAPAF.Text = "Fer Traspàs a factura definitiva"
        Me.btnTraspasarAPAF.UseVisualStyleBackColor = true
        '
        'cmbCliente1
        '
        Me.cmbCliente1.AutoSelect = true
        Me.cmbCliente1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbCliente1.Caption = ""
        Me.cmbCliente1.CaptionHeight = 17
        Me.cmbCliente1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbCliente1.ColumnCaptionHeight = 17
        Me.cmbCliente1.ColumnFooterHeight = 17
        Me.cmbCliente1.ContentHeight = 18
        Me.cmbCliente1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbCliente1.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cmbCliente1.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbCliente1.Images.Add(CType(resources.GetObject("cmbCliente1.Images"),System.Drawing.Image))
        Me.cmbCliente1.IntegralHeight = true
        Me.cmbCliente1.Location = New System.Drawing.Point(106, 103)
        Me.cmbCliente1.MatchEntryTimeout = CType(100,Long)
        Me.cmbCliente1.MaxDropDownItems = CType(8,Short)
        Me.cmbCliente1.MaxLength = 0
        Me.cmbCliente1.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbCliente1.Name = "cmbCliente1"
        Me.cmbCliente1.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbCliente1.Size = New System.Drawing.Size(174, 19)
        Me.cmbCliente1.TabIndex = 4
        Me.cmbCliente1.Visible = false
        Me.cmbCliente1.PropBag = resources.GetString("cmbCliente1.PropBag")
        '
        'cmbCliente2
        '
        Me.cmbCliente2.AutoSelect = true
        Me.cmbCliente2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbCliente2.Caption = ""
        Me.cmbCliente2.CaptionHeight = 17
        Me.cmbCliente2.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbCliente2.ColumnCaptionHeight = 17
        Me.cmbCliente2.ColumnFooterHeight = 17
        Me.cmbCliente2.ContentHeight = 18
        Me.cmbCliente2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbCliente2.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cmbCliente2.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbCliente2.Images.Add(CType(resources.GetObject("cmbCliente2.Images"),System.Drawing.Image))
        Me.cmbCliente2.IntegralHeight = true
        Me.cmbCliente2.Location = New System.Drawing.Point(431, 103)
        Me.cmbCliente2.MatchEntryTimeout = CType(100,Long)
        Me.cmbCliente2.MaxDropDownItems = CType(8,Short)
        Me.cmbCliente2.MaxLength = 0
        Me.cmbCliente2.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbCliente2.Name = "cmbCliente2"
        Me.cmbCliente2.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbCliente2.Size = New System.Drawing.Size(174, 19)
        Me.cmbCliente2.TabIndex = 5
        Me.cmbCliente2.Visible = false
        Me.cmbCliente2.PropBag = resources.GetString("cmbCliente2.PropBag")
        '
        'btnTraspasarAProforma
        '
        Me.btnTraspasarAProforma.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTraspasarAProforma.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTraspasarAProforma.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnTraspasarAProforma.Location = New System.Drawing.Point(25, 211)
        Me.btnTraspasarAProforma.Name = "btnTraspasarAProforma"
        Me.btnTraspasarAProforma.Size = New System.Drawing.Size(269, 44)
        Me.btnTraspasarAProforma.TabIndex = 8
        Me.btnTraspasarAProforma.TabStop = false
        Me.btnTraspasarAProforma.Text = "Fer Traspàs a factura PROFORMA"
        Me.btnTraspasarAProforma.UseVisualStyleBackColor = true
        Me.btnTraspasarAProforma.Visible = false
        '
        'lblDeFecha
        '
        Me.lblDeFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDeFecha.Location = New System.Drawing.Point(17, 69)
        Me.lblDeFecha.Name = "lblDeFecha"
        Me.lblDeFecha.Size = New System.Drawing.Size(83, 21)
        Me.lblDeFecha.TabIndex = 13
        Me.lblDeFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHastaCliente
        '
        Me.lblHastaCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblHastaCliente.Location = New System.Drawing.Point(311, 103)
        Me.lblHastaCliente.Name = "lblHastaCliente"
        Me.lblHastaCliente.Size = New System.Drawing.Size(114, 24)
        Me.lblHastaCliente.TabIndex = 12
        Me.lblHastaCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblHastaCliente.Visible = false
        '
        'lblHastaFecha
        '
        Me.lblHastaFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblHastaFecha.Location = New System.Drawing.Point(308, 70)
        Me.lblHastaFecha.Name = "lblHastaFecha"
        Me.lblHastaFecha.Size = New System.Drawing.Size(107, 20)
        Me.lblHastaFecha.TabIndex = 11
        Me.lblHastaFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHastaPAF
        '
        Me.lblHastaPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblHastaPAF.Location = New System.Drawing.Point(308, 39)
        Me.lblHastaPAF.Name = "lblHastaPAF"
        Me.lblHastaPAF.Size = New System.Drawing.Size(118, 24)
        Me.lblHastaPAF.TabIndex = 10
        Me.lblHastaPAF.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDePAF
        '
        Me.lblDePAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDePAF.Location = New System.Drawing.Point(17, 39)
        Me.lblDePAF.Name = "lblDePAF"
        Me.lblDePAF.Size = New System.Drawing.Size(83, 24)
        Me.lblDePAF.TabIndex = 9
        Me.lblDePAF.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDeCliente
        '
        Me.lblDeCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDeCliente.Location = New System.Drawing.Point(17, 103)
        Me.lblDeCliente.Name = "lblDeCliente"
        Me.lblDeCliente.Size = New System.Drawing.Size(83, 21)
        Me.lblDeCliente.TabIndex = 8
        Me.lblDeCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDeCliente.Visible = false
        '
        'lblDesdeFecha
        '
        Me.lblDesdeFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDesdeFecha.Location = New System.Drawing.Point(17, 143)
        Me.lblDesdeFecha.Name = "lblDesdeFecha"
        Me.lblDesdeFecha.Size = New System.Drawing.Size(280, 34)
        Me.lblDesdeFecha.TabIndex = 24
        Me.lblDesdeFecha.Text = "Seleccionar documents desde la data:"
        Me.lblDesdeFecha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDesdeFecha.Visible = false
        '
        'tabPageConsumo
        '
        Me.tabPageConsumo.Controls.Add(Me.lblComandaMostra)
        Me.tabPageConsumo.Controls.Add(Me.txtORDRE)
        Me.tabPageConsumo.Controls.Add(Me.dgConsumos)
        Me.tabPageConsumo.Controls.Add(Me.gb)
        Me.tabPageConsumo.Controls.Add(Me.btnGenerarPedidoCompraHilosDesdeMuestra)
        Me.tabPageConsumo.Location = New System.Drawing.Point(4, 22)
        Me.tabPageConsumo.Name = "tabPageConsumo"
        Me.tabPageConsumo.Size = New System.Drawing.Size(994, 529)
        Me.tabPageConsumo.TabIndex = 2
        '
        'lblComandaMostra
        '
        Me.lblComandaMostra.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblComandaMostra.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblComandaMostra.Location = New System.Drawing.Point(1602, 20)
        Me.lblComandaMostra.Name = "lblComandaMostra"
        Me.lblComandaMostra.Size = New System.Drawing.Size(140, 28)
        Me.lblComandaMostra.TabIndex = 188
        Me.lblComandaMostra.Text = "OrdreFabricacio"
        '
        'txtORDRE
        '
        Me.txtORDRE.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtORDRE.Location = New System.Drawing.Point(1747, 20)
        Me.txtORDRE.Name = "txtORDRE"
        Me.txtORDRE.ReadOnly = true
        Me.txtORDRE.Size = New System.Drawing.Size(140, 21)
        Me.txtORDRE.TabIndex = 187
        Me.txtORDRE.Tag = Nothing
        '
        'dgConsumos
        '
        Me.dgConsumos.AllowAddNew = true
        Me.dgConsumos.AllowDelete = true
        Me.dgConsumos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.dgConsumos.CaptionHeight = 19
        Me.dgConsumos.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgConsumos.Images.Add(CType(resources.GetObject("dgConsumos.Images"),System.Drawing.Image))
        Me.dgConsumos.Location = New System.Drawing.Point(17, 153)
        Me.dgConsumos.Name = "dgConsumos"
        Me.dgConsumos.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgConsumos.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgConsumos.PreviewInfo.ZoomFactor = 75R
        Me.dgConsumos.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgConsumos.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgConsumos.RowHeight = 17
        Me.dgConsumos.Size = New System.Drawing.Size(1876, 282)
        Me.dgConsumos.TabIndex = 186
        Me.dgConsumos.Text = "C1TrueDBGrid1"
        Me.dgConsumos.UseCompatibleTextRendering = false
        Me.dgConsumos.WrapCellPointer = true
        Me.dgConsumos.PropBag = resources.GetString("dgConsumos.PropBag")
        '
        'gb
        '
        Me.gb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.gb.BackColor = System.Drawing.SystemColors.Control
        Me.gb.Controls.Add(Me.txcCLIENT)
        Me.gb.Controls.Add(Me.dtcDATA)
        Me.gb.Controls.Add(Me.lblClienteConsumos)
        Me.gb.Controls.Add(Me.lblFechaPAF)
        Me.gb.Controls.Add(Me.lblPAF)
        Me.gb.Controls.Add(Me.txCNOMCLIENT)
        Me.gb.Controls.Add(Me.txCFRA)
        Me.gb.Location = New System.Drawing.Point(28, 18)
        Me.gb.Name = "gb"
        Me.gb.Size = New System.Drawing.Size(1142, 115)
        Me.gb.TabIndex = 185
        Me.gb.TabStop = false
        '
        'txcCLIENT
        '
        Me.txcCLIENT.Location = New System.Drawing.Point(129, 49)
        Me.txcCLIENT.Name = "txcCLIENT"
        Me.txcCLIENT.ReadOnly = true
        Me.txcCLIENT.Size = New System.Drawing.Size(73, 21)
        Me.txcCLIENT.TabIndex = 386
        Me.txcCLIENT.Tag = Nothing
        '
        'dtcDATA
        '
        '
        '
        '
        Me.dtcDATA.Calendar.DayNameLength = 1
        Me.dtcDATA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtcDATA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtcDATA.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dtcDATA.Location = New System.Drawing.Point(129, 79)
        Me.dtcDATA.Name = "dtcDATA"
        Me.dtcDATA.ReadOnly = true
        Me.dtcDATA.Size = New System.Drawing.Size(185, 21)
        Me.dtcDATA.TabIndex = 189
        Me.dtcDATA.Tag = Nothing
        '
        'lblClienteConsumos
        '
        Me.lblClienteConsumos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblClienteConsumos.Location = New System.Drawing.Point(17, 50)
        Me.lblClienteConsumos.Name = "lblClienteConsumos"
        Me.lblClienteConsumos.Size = New System.Drawing.Size(105, 25)
        Me.lblClienteConsumos.TabIndex = 178
        Me.lblClienteConsumos.Text = "Client"
        Me.lblClienteConsumos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFechaPAF
        '
        Me.lblFechaPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFechaPAF.Location = New System.Drawing.Point(17, 84)
        Me.lblFechaPAF.Name = "lblFechaPAF"
        Me.lblFechaPAF.Size = New System.Drawing.Size(105, 24)
        Me.lblFechaPAF.TabIndex = 181
        Me.lblFechaPAF.Text = "Data"
        Me.lblFechaPAF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPAF
        '
        Me.lblPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPAF.Location = New System.Drawing.Point(17, 23)
        Me.lblPAF.Name = "lblPAF"
        Me.lblPAF.Size = New System.Drawing.Size(105, 25)
        Me.lblPAF.TabIndex = 180
        Me.lblPAF.Text = "Comanda"
        Me.lblPAF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txCNOMCLIENT
        '
        Me.txCNOMCLIENT.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txCNOMCLIENT.Location = New System.Drawing.Point(213, 49)
        Me.txCNOMCLIENT.Name = "txCNOMCLIENT"
        Me.txCNOMCLIENT.ReadOnly = true
        Me.txCNOMCLIENT.Size = New System.Drawing.Size(918, 21)
        Me.txCNOMCLIENT.TabIndex = 188
        Me.txCNOMCLIENT.Tag = Nothing
        '
        'txCFRA
        '
        Me.txCFRA.Location = New System.Drawing.Point(129, 20)
        Me.txCFRA.Name = "txCFRA"
        Me.txCFRA.ReadOnly = true
        Me.txCFRA.Size = New System.Drawing.Size(112, 21)
        Me.txCFRA.TabIndex = 187
        Me.txCFRA.Tag = Nothing
        '
        'btnGenerarPedidoCompraHilosDesdeMuestra
        '
        Me.btnGenerarPedidoCompraHilosDesdeMuestra.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnGenerarPedidoCompraHilosDesdeMuestra.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerarPedidoCompraHilosDesdeMuestra.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerarPedidoCompraHilosDesdeMuestra.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnGenerarPedidoCompraHilosDesdeMuestra.Location = New System.Drawing.Point(1642, 444)
        Me.btnGenerarPedidoCompraHilosDesdeMuestra.Name = "btnGenerarPedidoCompraHilosDesdeMuestra"
        Me.btnGenerarPedidoCompraHilosDesdeMuestra.Size = New System.Drawing.Size(252, 30)
        Me.btnGenerarPedidoCompraHilosDesdeMuestra.TabIndex = 1
        Me.btnGenerarPedidoCompraHilosDesdeMuestra.TabStop = false
        Me.btnGenerarPedidoCompraHilosDesdeMuestra.Text = "Generar Comanda/es de compra"
        Me.btnGenerarPedidoCompraHilosDesdeMuestra.UseVisualStyleBackColor = true
        '
        'tabPageVencimientos
        '
        Me.tabPageVencimientos.Controls.Add(Me.tlpTab6)
        Me.tabPageVencimientos.Location = New System.Drawing.Point(4, 22)
        Me.tabPageVencimientos.Name = "tabPageVencimientos"
        Me.tabPageVencimientos.Size = New System.Drawing.Size(994, 529)
        Me.tabPageVencimientos.TabIndex = 3
        Me.tabPageVencimientos.Text = "Venciments"
        '
        'tlpTab6
        '
        Me.tlpTab6.ColumnCount = 2
        Me.tlpTab6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.06749!))
        Me.tlpTab6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.93251!))
        Me.tlpTab6.Controls.Add(Me.pnlTab61, 0, 0)
        Me.tlpTab6.Controls.Add(Me.pnlTab62, 1, 0)
        Me.tlpTab6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpTab6.Location = New System.Drawing.Point(0, 0)
        Me.tlpTab6.Name = "tlpTab6"
        Me.tlpTab6.RowCount = 1
        Me.tlpTab6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tlpTab6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 529!))
        Me.tlpTab6.Size = New System.Drawing.Size(994, 529)
        Me.tlpTab6.TabIndex = 387
        '
        'pnlTab61
        '
        Me.pnlTab61.Controls.Add(Me.GroupBox3)
        Me.pnlTab61.Controls.Add(Me.gbVencimPAF)
        Me.pnlTab61.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTab61.Location = New System.Drawing.Point(3, 3)
        Me.pnlTab61.Name = "pnlTab61"
        Me.pnlTab61.Size = New System.Drawing.Size(521, 523)
        Me.pnlTab61.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.lblFormaDePago)
        Me.GroupBox3.Controls.Add(Me.lblFORMA)
        Me.GroupBox3.Controls.Add(Me.lblDia3)
        Me.GroupBox3.Controls.Add(Me.lblDia_3)
        Me.GroupBox3.Controls.Add(Me.lblDia1)
        Me.GroupBox3.Controls.Add(Me.lblDia_2)
        Me.GroupBox3.Controls.Add(Me.lblRE2)
        Me.GroupBox3.Controls.Add(Me.lblRE_)
        Me.GroupBox3.Controls.Add(Me.lblIVA2)
        Me.GroupBox3.Controls.Add(Me.lblIVA_)
        Me.GroupBox3.Controls.Add(Me.lblDia_1)
        Me.GroupBox3.Controls.Add(Me.lblDia2)
        Me.GroupBox3.Controls.Add(Me.cboBANCO)
        Me.GroupBox3.Location = New System.Drawing.Point(4, 190)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(513, 177)
        Me.GroupBox3.TabIndex = 386
        Me.GroupBox3.TabStop = false
        Me.GroupBox3.Text = "Dades Facturació Client"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(11, 87)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(163, 25)
        Me.Label7.TabIndex = 388
        Me.Label7.Text = "Banc"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFormaDePago
        '
        Me.lblFormaDePago.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFormaDePago.Location = New System.Drawing.Point(13, 59)
        Me.lblFormaDePago.Name = "lblFormaDePago"
        Me.lblFormaDePago.Size = New System.Drawing.Size(162, 20)
        Me.lblFormaDePago.TabIndex = 287
        Me.lblFormaDePago.Text = "Forma de pagament"
        Me.lblFormaDePago.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFORMA
        '
        Me.lblFORMA.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblFORMA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFORMA.Location = New System.Drawing.Point(98, 59)
        Me.lblFORMA.Name = "lblFORMA"
        Me.lblFORMA.Size = New System.Drawing.Size(476, 20)
        Me.lblFORMA.TabIndex = 286
        Me.lblFORMA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDia3
        '
        Me.lblDia3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDia3.Location = New System.Drawing.Point(601, 30)
        Me.lblDia3.Name = "lblDia3"
        Me.lblDia3.Size = New System.Drawing.Size(61, 19)
        Me.lblDia3.TabIndex = 285
        Me.lblDia3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDia_3
        '
        Me.lblDia_3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDia_3.Location = New System.Drawing.Point(545, 30)
        Me.lblDia_3.Name = "lblDia_3"
        Me.lblDia_3.Size = New System.Drawing.Size(50, 19)
        Me.lblDia_3.TabIndex = 282
        Me.lblDia_3.Text = "Dia 3"
        Me.lblDia_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDia1
        '
        Me.lblDia1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDia1.Location = New System.Drawing.Point(354, 30)
        Me.lblDia1.Name = "lblDia1"
        Me.lblDia1.Size = New System.Drawing.Size(62, 19)
        Me.lblDia1.TabIndex = 283
        Me.lblDia1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDia_2
        '
        Me.lblDia_2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDia_2.Location = New System.Drawing.Point(421, 30)
        Me.lblDia_2.Name = "lblDia_2"
        Me.lblDia_2.Size = New System.Drawing.Size(51, 19)
        Me.lblDia_2.TabIndex = 281
        Me.lblDia_2.Text = "Dia 2"
        Me.lblDia_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRE2
        '
        Me.lblRE2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRE2.Location = New System.Drawing.Point(197, 30)
        Me.lblRE2.Name = "lblRE2"
        Me.lblRE2.Size = New System.Drawing.Size(62, 19)
        Me.lblRE2.TabIndex = 279
        Me.lblRE2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRE_
        '
        Me.lblRE_.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRE_.Location = New System.Drawing.Point(147, 30)
        Me.lblRE_.Name = "lblRE_"
        Me.lblRE_.Size = New System.Drawing.Size(45, 19)
        Me.lblRE_.TabIndex = 278
        Me.lblRE_.Text = "RE"
        Me.lblRE_.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIVA2
        '
        Me.lblIVA2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblIVA2.Location = New System.Drawing.Point(63, 30)
        Me.lblIVA2.Name = "lblIVA2"
        Me.lblIVA2.Size = New System.Drawing.Size(78, 19)
        Me.lblIVA2.TabIndex = 277
        Me.lblIVA2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIVA_
        '
        Me.lblIVA_.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblIVA_.Location = New System.Drawing.Point(13, 30)
        Me.lblIVA_.Name = "lblIVA_"
        Me.lblIVA_.Size = New System.Drawing.Size(44, 19)
        Me.lblIVA_.TabIndex = 276
        Me.lblIVA_.Text = "IVA"
        Me.lblIVA_.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDia_1
        '
        Me.lblDia_1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDia_1.Location = New System.Drawing.Point(298, 30)
        Me.lblDia_1.Name = "lblDia_1"
        Me.lblDia_1.Size = New System.Drawing.Size(51, 19)
        Me.lblDia_1.TabIndex = 280
        Me.lblDia_1.Text = "Dia 1"
        Me.lblDia_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDia2
        '
        Me.lblDia2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDia2.Location = New System.Drawing.Point(477, 30)
        Me.lblDia2.Name = "lblDia2"
        Me.lblDia2.Size = New System.Drawing.Size(62, 19)
        Me.lblDia2.TabIndex = 284
        Me.lblDia2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboBANCO
        '
        Me.cboBANCO.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cboBANCO.AutoSelect = true
        Me.cboBANCO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboBANCO.Caption = ""
        Me.cboBANCO.CaptionHeight = 17
        Me.cboBANCO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cboBANCO.ColumnCaptionHeight = 17
        Me.cboBANCO.ColumnFooterHeight = 17
        Me.cboBANCO.ContentHeight = 18
        Me.cboBANCO.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboBANCO.DeadAreaBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.cboBANCO.EditorBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.cboBANCO.Images.Add(CType(resources.GetObject("cboBANCO.Images"),System.Drawing.Image))
        Me.cboBANCO.IntegralHeight = true
        Me.cboBANCO.Location = New System.Drawing.Point(181, 89)
        Me.cboBANCO.MatchEntryTimeout = CType(100,Long)
        Me.cboBANCO.MaxDropDownItems = CType(8,Short)
        Me.cboBANCO.MaxLength = 0
        Me.cboBANCO.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboBANCO.Name = "cboBANCO"
        Me.cboBANCO.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboBANCO.Size = New System.Drawing.Size(308, 19)
        Me.cboBANCO.TabIndex = 387
        Me.cboBANCO.PropBag = resources.GetString("cboBANCO.PropBag")
        '
        'gbVencimPAF
        '
        Me.gbVencimPAF.BackColor = System.Drawing.SystemColors.Control
        Me.gbVencimPAF.Controls.Add(Me.txvCLIENT)
        Me.gbVencimPAF.Controls.Add(Me.dtvDATA)
        Me.gbVencimPAF.Controls.Add(Me.txvNOMCLIENT)
        Me.gbVencimPAF.Controls.Add(Me.lblClientePAFV)
        Me.gbVencimPAF.Controls.Add(Me.lblFechaPAFV)
        Me.gbVencimPAF.Controls.Add(Me.lblFacturaV)
        Me.gbVencimPAF.Controls.Add(Me.txvFRA)
        Me.gbVencimPAF.Location = New System.Drawing.Point(4, 15)
        Me.gbVencimPAF.Name = "gbVencimPAF"
        Me.gbVencimPAF.Size = New System.Drawing.Size(710, 134)
        Me.gbVencimPAF.TabIndex = 186
        Me.gbVencimPAF.TabStop = false
        '
        'txvCLIENT
        '
        Me.txvCLIENT.Location = New System.Drawing.Point(129, 54)
        Me.txvCLIENT.Name = "txvCLIENT"
        Me.txvCLIENT.ReadOnly = true
        Me.txvCLIENT.Size = New System.Drawing.Size(67, 21)
        Me.txvCLIENT.TabIndex = 385
        Me.txvCLIENT.Tag = Nothing
        '
        'dtvDATA
        '
        '
        '
        '
        Me.dtvDATA.Calendar.DayNameLength = 1
        Me.dtvDATA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtvDATA.DisableOnNoData = false
        Me.dtvDATA.EmptyAsNull = true
        Me.dtvDATA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtvDATA.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dtvDATA.Location = New System.Drawing.Point(129, 89)
        Me.dtvDATA.Name = "dtvDATA"
        Me.dtvDATA.ReadOnly = true
        Me.dtvDATA.Size = New System.Drawing.Size(168, 21)
        Me.dtvDATA.TabIndex = 384
        Me.dtvDATA.Tag = Nothing
        '
        'txvNOMCLIENT
        '
        Me.txvNOMCLIENT.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txvNOMCLIENT.Location = New System.Drawing.Point(202, 54)
        Me.txvNOMCLIENT.Name = "txvNOMCLIENT"
        Me.txvNOMCLIENT.ReadOnly = true
        Me.txvNOMCLIENT.Size = New System.Drawing.Size(484, 21)
        Me.txvNOMCLIENT.TabIndex = 189
        Me.txvNOMCLIENT.Tag = Nothing
        '
        'lblClientePAFV
        '
        Me.lblClientePAFV.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblClientePAFV.Location = New System.Drawing.Point(17, 49)
        Me.lblClientePAFV.Name = "lblClientePAFV"
        Me.lblClientePAFV.Size = New System.Drawing.Size(98, 25)
        Me.lblClientePAFV.TabIndex = 178
        Me.lblClientePAFV.Text = "Client"
        Me.lblClientePAFV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFechaPAFV
        '
        Me.lblFechaPAFV.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFechaPAFV.Location = New System.Drawing.Point(17, 89)
        Me.lblFechaPAFV.Name = "lblFechaPAFV"
        Me.lblFechaPAFV.Size = New System.Drawing.Size(98, 24)
        Me.lblFechaPAFV.TabIndex = 181
        Me.lblFechaPAFV.Text = "Data"
        Me.lblFechaPAFV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFacturaV
        '
        Me.lblFacturaV.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFacturaV.Location = New System.Drawing.Point(17, 20)
        Me.lblFacturaV.Name = "lblFacturaV"
        Me.lblFacturaV.Size = New System.Drawing.Size(98, 24)
        Me.lblFacturaV.TabIndex = 180
        Me.lblFacturaV.Text = "Factura"
        Me.lblFacturaV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txvFRA
        '
        Me.txvFRA.Location = New System.Drawing.Point(129, 20)
        Me.txvFRA.Name = "txvFRA"
        Me.txvFRA.ReadOnly = true
        Me.txvFRA.Size = New System.Drawing.Size(140, 21)
        Me.txvFRA.TabIndex = 188
        Me.txvFRA.Tag = Nothing
        '
        'pnlTab62
        '
        Me.pnlTab62.Controls.Add(Me.gbVencim)
        Me.pnlTab62.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTab62.Location = New System.Drawing.Point(530, 3)
        Me.pnlTab62.Name = "pnlTab62"
        Me.pnlTab62.Size = New System.Drawing.Size(461, 523)
        Me.pnlTab62.TabIndex = 1
        '
        'gbVencim
        '
        Me.gbVencim.BackColor = System.Drawing.SystemColors.Control
        Me.gbVencim.Controls.Add(Me.txvTOTAL)
        Me.gbVencim.Controls.Add(Me.Label6)
        Me.gbVencim.Controls.Add(Me.btnGenerarVencimientos)
        Me.gbVencim.Controls.Add(Me.dgVencim)
        Me.gbVencim.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbVencim.Location = New System.Drawing.Point(0, 0)
        Me.gbVencim.Name = "gbVencim"
        Me.gbVencim.Size = New System.Drawing.Size(461, 523)
        Me.gbVencim.TabIndex = 1
        Me.gbVencim.TabStop = false
        '
        'txvTOTAL
        '
        Me.txvTOTAL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txvTOTAL.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(211,Byte),Integer), CType(CType(120,Byte),Integer))
        Me.txvTOTAL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txvTOTAL.CustomFormat = "#,##0.00"
        Me.txvTOTAL.DataType = GetType(Double)
        Me.txvTOTAL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txvTOTAL.Location = New System.Drawing.Point(180, 432)
        Me.txvTOTAL.Name = "txvTOTAL"
        Me.txvTOTAL.Size = New System.Drawing.Size(98, 21)
        Me.txvTOTAL.TabIndex = 243
        Me.txvTOTAL.Tag = Nothing
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(282, 430)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(179, 24)
        Me.Label6.TabIndex = 242
        Me.Label6.Text = "Total Factura"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnGenerarVencimientos
        '
        Me.btnGenerarVencimientos.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnGenerarVencimientos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerarVencimientos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerarVencimientos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnGenerarVencimientos.Location = New System.Drawing.Point(199, 476)
        Me.btnGenerarVencimientos.Name = "btnGenerarVencimientos"
        Me.btnGenerarVencimientos.Size = New System.Drawing.Size(163, 40)
        Me.btnGenerarVencimientos.TabIndex = 188
        Me.btnGenerarVencimientos.TabStop = false
        Me.btnGenerarVencimientos.Text = "Generar Venciments"
        Me.btnGenerarVencimientos.UseVisualStyleBackColor = true
        '
        'dgVencim
        '
        Me.dgVencim.AllowAddNew = true
        Me.dgVencim.AllowDelete = true
        Me.dgVencim.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.dgVencim.CaptionHeight = 18
        Me.dgVencim.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgVencim.Images.Add(CType(resources.GetObject("dgVencim.Images"),System.Drawing.Image))
        Me.dgVencim.Location = New System.Drawing.Point(11, 20)
        Me.dgVencim.Name = "dgVencim"
        Me.dgVencim.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgVencim.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgVencim.PreviewInfo.ZoomFactor = 75R
        Me.dgVencim.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgVencim.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgVencim.RowHeight = 17
        Me.dgVencim.Size = New System.Drawing.Size(442, 387)
        Me.dgVencim.TabIndex = 187
        Me.dgVencim.Text = "C1TrueDBGrid1"
        Me.dgVencim.UseCompatibleTextRendering = false
        Me.dgVencim.PropBag = resources.GetString("dgVencim.PropBag")
        '
        'c1PreviewControl_leftSplitter
        '
        Me.c1PreviewControl_leftSplitter.Location = New System.Drawing.Point(0, 0)
        Me.c1PreviewControl_leftSplitter.Name = "c1PreviewControl_leftSplitter"
        Me.c1PreviewControl_leftSplitter.Size = New System.Drawing.Size(3, 475)
        Me.c1PreviewControl_leftSplitter.TabIndex = 247
        Me.c1PreviewControl_leftSplitter.TabStop = false
        '
        'C1Report1
        '
        Me.C1Report1.ReportDefinition = resources.GetString("C1Report1.ReportDefinition")
        Me.C1Report1.ReportName = "C1Report1"
        '
        'c1PreviewControl_rightSplitter
        '
        Me.c1PreviewControl_rightSplitter.Dock = System.Windows.Forms.DockStyle.Right
        Me.c1PreviewControl_rightSplitter.Location = New System.Drawing.Point(586, 0)
        Me.c1PreviewControl_rightSplitter.Name = "c1PreviewControl_rightSplitter"
        Me.c1PreviewControl_rightSplitter.Size = New System.Drawing.Size(3, 475)
        Me.c1PreviewControl_rightSplitter.TabIndex = 248
        Me.c1PreviewControl_rightSplitter.TabStop = false
        Me.c1PreviewControl_rightSplitter.Visible = false
        '
        'c1PreviewControl_fileStrip
        '
        Me.c1PreviewControl_fileStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.c1PreviewControl_fileStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.c1PreviewControl_fileStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.c1PreviewControl_btnFileOpen, Me.c1PreviewControl_btnFileSave, Me.c1PreviewControl_btnPageSetup, Me.c1PreviewControl_btnPrint, Me.c1PreviewControl_btnReflow})
        Me.c1PreviewControl_fileStrip.Location = New System.Drawing.Point(3, 0)
        Me.c1PreviewControl_fileStrip.Name = "c1PreviewControl_fileStrip"
        Me.c1PreviewControl_fileStrip.Size = New System.Drawing.Size(136, 25)
        Me.c1PreviewControl_fileStrip.TabIndex = 1
        '
        'c1PreviewControl_btnFileOpen
        '
        Me.c1PreviewControl_btnFileOpen.Image = CType(resources.GetObject("c1PreviewControl_btnFileOpen.Image"),System.Drawing.Image)
        Me.c1PreviewControl_btnFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.c1PreviewControl_btnFileOpen.Name = "c1PreviewControl_btnFileOpen"
        Me.c1PreviewControl_btnFileOpen.Size = New System.Drawing.Size(39, 22)
        Me.c1PreviewControl_btnFileOpen.Tag = "C1PreviewActionEnum.FileOpen"
        Me.c1PreviewControl_btnFileOpen.ToolTipText = "Open File"
        '
        'c1PreviewControl_btnFileSave
        '
        Me.c1PreviewControl_btnFileSave.Image = CType(resources.GetObject("c1PreviewControl_btnFileSave.Image"),System.Drawing.Image)
        Me.c1PreviewControl_btnFileSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.c1PreviewControl_btnFileSave.Name = "c1PreviewControl_btnFileSave"
        Me.c1PreviewControl_btnFileSave.Size = New System.Drawing.Size(29, 22)
        Me.c1PreviewControl_btnFileSave.Tag = "C1PreviewActionEnum.FileSave"
        Me.c1PreviewControl_btnFileSave.ToolTipText = "Save File"
        '
        'c1PreviewControl_btnPageSetup
        '
        Me.c1PreviewControl_btnPageSetup.Image = CType(resources.GetObject("c1PreviewControl_btnPageSetup.Image"),System.Drawing.Image)
        Me.c1PreviewControl_btnPageSetup.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.c1PreviewControl_btnPageSetup.Name = "c1PreviewControl_btnPageSetup"
        Me.c1PreviewControl_btnPageSetup.Size = New System.Drawing.Size(29, 22)
        Me.c1PreviewControl_btnPageSetup.Tag = "C1PreviewActionEnum.PageSetup"
        Me.c1PreviewControl_btnPageSetup.ToolTipText = "Page Setup"
        '
        'c1PreviewControl_btnPrint
        '
        Me.c1PreviewControl_btnPrint.Image = CType(resources.GetObject("c1PreviewControl_btnPrint.Image"),System.Drawing.Image)
        Me.c1PreviewControl_btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.c1PreviewControl_btnPrint.Name = "c1PreviewControl_btnPrint"
        Me.c1PreviewControl_btnPrint.Size = New System.Drawing.Size(29, 24)
        Me.c1PreviewControl_btnPrint.Tag = "C1PreviewActionEnum.Print"
        Me.c1PreviewControl_btnPrint.ToolTipText = "Print"
        '
        'c1PreviewControl_btnReflow
        '
        Me.c1PreviewControl_btnReflow.Image = CType(resources.GetObject("c1PreviewControl_btnReflow.Image"),System.Drawing.Image)
        Me.c1PreviewControl_btnReflow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.c1PreviewControl_btnReflow.Name = "c1PreviewControl_btnReflow"
        Me.c1PreviewControl_btnReflow.Size = New System.Drawing.Size(29, 24)
        Me.c1PreviewControl_btnReflow.Tag = "C1PreviewActionEnum.Reflow"
        Me.c1PreviewControl_btnReflow.ToolTipText = "Reflow"
        '
        'gbCentro
        '
        Me.gbCentro.BackColor = System.Drawing.SystemColors.Control
        Me.gbCentro.Controls.Add(Me.lblNumeracion)
        Me.gbCentro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCentro.Location = New System.Drawing.Point(0, 0)
        Me.gbCentro.Name = "gbCentro"
        Me.gbCentro.Size = New System.Drawing.Size(266, 58)
        Me.gbCentro.TabIndex = 221
        Me.gbCentro.TabStop = false
        '
        'lblNumeracion
        '
        Me.lblNumeracion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblNumeracion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNumeracion.Location = New System.Drawing.Point(-145, 25)
        Me.lblNumeracion.Name = "lblNumeracion"
        Me.lblNumeracion.Size = New System.Drawing.Size(133, 29)
        Me.lblNumeracion.TabIndex = 220
        Me.lblNumeracion.Text = "Numeració de:"
        Me.lblNumeracion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gbEleccionClientesPAF
        '
        Me.gbEleccionClientesPAF.BackColor = System.Drawing.SystemColors.Control
        Me.gbEleccionClientesPAF.Controls.Add(Me.cboSeleccionClienteParaPAF)
        Me.gbEleccionClientesPAF.Controls.Add(Me.lblNumeroPAF)
        Me.gbEleccionClientesPAF.Controls.Add(Me.rdoVerTodasPAF)
        Me.gbEleccionClientesPAF.Controls.Add(Me.rdoVerPAFDeCliente)
        Me.gbEleccionClientesPAF.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbEleccionClientesPAF.Location = New System.Drawing.Point(0, 0)
        Me.gbEleccionClientesPAF.Name = "gbEleccionClientesPAF"
        Me.gbEleccionClientesPAF.Size = New System.Drawing.Size(724, 58)
        Me.gbEleccionClientesPAF.TabIndex = 209
        Me.gbEleccionClientesPAF.TabStop = false
        Me.gbEleccionClientesPAF.Text = "El·leccions"
        '
        'cboSeleccionClienteParaPAF
        '
        Me.cboSeleccionClienteParaPAF.AutoCompletion = true
        Me.cboSeleccionClienteParaPAF.AutoSelect = true
        Me.cboSeleccionClienteParaPAF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboSeleccionClienteParaPAF.Caption = ""
        Me.cboSeleccionClienteParaPAF.CaptionHeight = 17
        Me.cboSeleccionClienteParaPAF.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboSeleccionClienteParaPAF.ColumnCaptionHeight = 17
        Me.cboSeleccionClienteParaPAF.ColumnFooterHeight = 17
        Me.cboSeleccionClienteParaPAF.ContentHeight = 18
        Me.cboSeleccionClienteParaPAF.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSeleccionClienteParaPAF.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboSeleccionClienteParaPAF.EditorBackColor = System.Drawing.Color.Empty
        Me.cboSeleccionClienteParaPAF.Images.Add(CType(resources.GetObject("cboSeleccionClienteParaPAF.Images"),System.Drawing.Image))
        Me.cboSeleccionClienteParaPAF.IntegralHeight = true
        Me.cboSeleccionClienteParaPAF.LimitToList = true
        Me.cboSeleccionClienteParaPAF.Location = New System.Drawing.Point(242, 30)
        Me.cboSeleccionClienteParaPAF.MatchEntryTimeout = CType(100,Long)
        Me.cboSeleccionClienteParaPAF.MaxDropDownItems = CType(8,Short)
        Me.cboSeleccionClienteParaPAF.MaxLength = 255
        Me.cboSeleccionClienteParaPAF.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboSeleccionClienteParaPAF.Name = "cboSeleccionClienteParaPAF"
        Me.cboSeleccionClienteParaPAF.RowSubDividerColor = System.Drawing.Color.Empty
        Me.cboSeleccionClienteParaPAF.Size = New System.Drawing.Size(317, 23)
        Me.cboSeleccionClienteParaPAF.TabIndex = 2
        Me.cboSeleccionClienteParaPAF.Visible = false
        Me.cboSeleccionClienteParaPAF.PropBag = resources.GetString("cboSeleccionClienteParaPAF.PropBag")
        '
        'lblNumeroPAF
        '
        Me.lblNumeroPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNumeroPAF.Location = New System.Drawing.Point(756, 25)
        Me.lblNumeroPAF.Name = "lblNumeroPAF"
        Me.lblNumeroPAF.Size = New System.Drawing.Size(241, 28)
        Me.lblNumeroPAF.TabIndex = 3
        Me.lblNumeroPAF.Text = "Num:"
        '
        'rdoVerTodasPAF
        '
        Me.rdoVerTodasPAF.Checked = true
        Me.rdoVerTodasPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoVerTodasPAF.Location = New System.Drawing.Point(6, 27)
        Me.rdoVerTodasPAF.Name = "rdoVerTodasPAF"
        Me.rdoVerTodasPAF.Size = New System.Drawing.Size(136, 26)
        Me.rdoVerTodasPAF.TabIndex = 0
        Me.rdoVerTodasPAF.TabStop = true
        Me.rdoVerTodasPAF.Text = "Tots els clients"
        '
        'rdoVerPAFDeCliente
        '
        Me.rdoVerPAFDeCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoVerPAFDeCliente.Location = New System.Drawing.Point(148, 28)
        Me.rdoVerPAFDeCliente.Name = "rdoVerPAFDeCliente"
        Me.rdoVerPAFDeCliente.Size = New System.Drawing.Size(106, 26)
        Me.rdoVerPAFDeCliente.TabIndex = 1
        Me.rdoVerPAFDeCliente.Text = "Del Client"
        '
        'btnImprimirEtiquetas
        '
        Me.btnImprimirEtiquetas.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnImprimirEtiquetas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImprimirEtiquetas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimirEtiquetas.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnImprimirEtiquetas.Location = New System.Drawing.Point(865, 752)
        Me.btnImprimirEtiquetas.Name = "btnImprimirEtiquetas"
        Me.btnImprimirEtiquetas.Size = New System.Drawing.Size(143, 24)
        Me.btnImprimirEtiquetas.TabIndex = 16
        Me.btnImprimirEtiquetas.TabStop = false
        Me.btnImprimirEtiquetas.Text = "Imprimir Etiquetas"
        Me.btnImprimirEtiquetas.UseVisualStyleBackColor = true
        Me.btnImprimirEtiquetas.Visible = false
        '
        'tlpPrincipal
        '
        Me.tlpPrincipal.ColumnCount = 1
        Me.tlpPrincipal.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tlpPrincipal.Controls.Add(Me.pnl1, 0, 0)
        Me.tlpPrincipal.Controls.Add(Me.pnl2, 0, 1)
        Me.tlpPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpPrincipal.Location = New System.Drawing.Point(0, 0)
        Me.tlpPrincipal.Name = "tlpPrincipal"
        Me.tlpPrincipal.RowCount = 3
        Me.tlpPrincipal.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70!))
        Me.tlpPrincipal.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tlpPrincipal.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69!))
        Me.tlpPrincipal.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25!))
        Me.tlpPrincipal.Size = New System.Drawing.Size(1008, 700)
        Me.tlpPrincipal.TabIndex = 253
        '
        'pnl1
        '
        Me.pnl1.Controls.Add(Me.tlpsuperior)
        Me.pnl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl1.Location = New System.Drawing.Point(3, 3)
        Me.pnl1.Name = "pnl1"
        Me.pnl1.Size = New System.Drawing.Size(1002, 64)
        Me.pnl1.TabIndex = 0
        '
        'tlpsuperior
        '
        Me.tlpsuperior.ColumnCount = 2
        Me.tlpsuperior.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.92089!))
        Me.tlpsuperior.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.07911!))
        Me.tlpsuperior.Controls.Add(Me.pnl11, 0, 0)
        Me.tlpsuperior.Controls.Add(Me.pnl12, 1, 0)
        Me.tlpsuperior.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpsuperior.Location = New System.Drawing.Point(0, 0)
        Me.tlpsuperior.Name = "tlpsuperior"
        Me.tlpsuperior.RowCount = 1
        Me.tlpsuperior.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tlpsuperior.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64!))
        Me.tlpsuperior.Size = New System.Drawing.Size(1002, 64)
        Me.tlpsuperior.TabIndex = 0
        '
        'pnl11
        '
        Me.pnl11.Controls.Add(Me.gbEleccionClientesPAF)
        Me.pnl11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl11.Location = New System.Drawing.Point(3, 3)
        Me.pnl11.Name = "pnl11"
        Me.pnl11.Size = New System.Drawing.Size(724, 58)
        Me.pnl11.TabIndex = 0
        '
        'pnl12
        '
        Me.pnl12.Controls.Add(Me.gbCentro)
        Me.pnl12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl12.Location = New System.Drawing.Point(733, 3)
        Me.pnl12.Name = "pnl12"
        Me.pnl12.Size = New System.Drawing.Size(266, 58)
        Me.pnl12.TabIndex = 1
        '
        'pnl2
        '
        Me.pnl2.Controls.Add(Me.tabControlPAFVentas)
        Me.pnl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl2.Location = New System.Drawing.Point(3, 73)
        Me.pnl2.Name = "pnl2"
        Me.pnl2.Size = New System.Drawing.Size(1002, 555)
        Me.pnl2.TabIndex = 1
        '
        'frmVentaPAFPlantilla
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 16)
        Me.ClientSize = New System.Drawing.Size(1006, 721)
        Me.Controls.Add(Me.btnImprimirEtiquetas)
        Me.Controls.Add(Me.tlpPrincipal)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.KeyPreview = true
        Me.Name = "frmVentaPAFPlantilla"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Controls.SetChildIndex(Me.tlpPrincipal, 0)
        Me.Controls.SetChildIndex(Me.btnImprimirEtiquetas, 0)
        Me.Controls.SetChildIndex(Me.sbtipo, 0)
        Me.Controls.SetChildIndex(Me.cboSeleccionCentro, 0)
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
        Me.gbPAF.ResumeLayout(false)
        Me.gbPAF.PerformLayout
        CType(Me.cboNOMCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboDOM,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtALBCLI,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtpDATA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtCOMIS,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtP_DTE,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboFRA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnElegirCliente,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboNOMREPRES,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnElegirRepresentate,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtREPRES,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabControlPAFVentas.ResumeLayout(false)
        Me.tabPagePAF.ResumeLayout(false)
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.pnl21.ResumeLayout(false)
        CType(Me.txtOBSERVOCULTA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtOBSERV,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnl22.ResumeLayout(false)
        CType(Me.cboTalla,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboMuestra,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboMuestra2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboKM,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboTipo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboColor,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgLineasPAFVenta,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnl23.ResumeLayout(false)
        CType(Me.txtTOTAL,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtBRUT1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtIVA1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtRE1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtBASE1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtDTE1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnEliminarFilasSeleccionadas,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabPageImpresionPAF.ResumeLayout(false)
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.tlpTab2.ResumeLayout(false)
        Me.pnlTab21.ResumeLayout(false)
        CType(Me.cboImpresion,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgImpresion,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlTAb22.ResumeLayout(false)
        CType(Me.dgImpresion2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboSemanas,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboIncoterm,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboAPAFIMPRESION,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboDEPAFIMPRESION,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnImprimir,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabPageRecibos.ResumeLayout(false)
        CType(Me.ppfitxa2.PreviewPane,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ppfitxa2,System.ComponentModel.ISupportInitialize).EndInit
        Me.ppfitxa2.ResumeLayout(false)
        Me.ppfitxa2.PerformLayout
        CType(Me.lstOrdenes,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox4.ResumeLayout(false)
        Me.GroupBox4.PerformLayout
        CType(Me.cboDeFacuraRecibo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtpAFechaRecibo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtpDeFechaRecibo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboAFacturaRecibo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnGenerarRecibo,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabPageTraspasoPAF.ResumeLayout(false)
        Me.GroupBox1.ResumeLayout(false)
        CType(Me.dtrDATA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txrNOMCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txrFRA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dateDesdeFecha,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbTraspasarLista.ResumeLayout(false)
        Me.tlpTab3.ResumeLayout(false)
        Me.pnlTab31.ResumeLayout(false)
        CType(Me.dgTraspas,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnHacerTraspaso,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnAbrirPAF,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlTab32.ResumeLayout(false)
        CType(Me.btnAbrirAF,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbTraspasarIntervalo.ResumeLayout(false)
        Me.gbTraspasarIntervalo.PerformLayout
        CType(Me.txtAFecha,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.date2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.date1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtAPAF,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cmbPAF1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cmbPAF2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnTraspasarAPAF,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cmbCliente1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cmbCliente2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnTraspasarAProforma,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabPageConsumo.ResumeLayout(false)
        CType(Me.txtORDRE,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgConsumos,System.ComponentModel.ISupportInitialize).EndInit
        Me.gb.ResumeLayout(false)
        CType(Me.txcCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtcDATA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txCNOMCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txCFRA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnGenerarPedidoCompraHilosDesdeMuestra,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabPageVencimientos.ResumeLayout(false)
        Me.tlpTab6.ResumeLayout(false)
        Me.pnlTab61.ResumeLayout(false)
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        CType(Me.cboBANCO,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbVencimPAF.ResumeLayout(false)
        CType(Me.txvCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtvDATA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txvNOMCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txvFRA,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlTab62.ResumeLayout(false)
        Me.gbVencim.ResumeLayout(false)
        CType(Me.txvTOTAL,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnGenerarVencimientos,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgVencim,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.C1Report1,System.ComponentModel.ISupportInitialize).EndInit
        Me.c1PreviewControl_fileStrip.ResumeLayout(false)
        Me.c1PreviewControl_fileStrip.PerformLayout
        Me.gbCentro.ResumeLayout(false)
        Me.gbEleccionClientesPAF.ResumeLayout(false)
        Me.gbEleccionClientesPAF.PerformLayout
        CType(Me.cboSeleccionClienteParaPAF,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnImprimirEtiquetas,System.ComponentModel.ISupportInitialize).EndInit
        Me.tlpPrincipal.ResumeLayout(false)
        Me.pnl1.ResumeLayout(false)
        Me.tlpsuperior.ResumeLayout(false)
        Me.pnl11.ResumeLayout(false)
        Me.pnl12.ResumeLayout(false)
        Me.pnl2.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub

#End Region

#Region "VARIABLES"

        Dim fcargando As frCargando
        Dim posicionUltimoClickBotonRaton As Point
        Public PAFActual As clsPAFVenta
        Private dtConsumos As New DataTable("CONSUMOS")

#End Region

#Region "INICIAR"

        Private Sub QuitarTabPages()
            Try
                Try : tabControlPAFVentas.TabPages.Remove(tabPageTraspasoPAF)
                Catch ex As Exception
                End Try

                Try : tabControlPAFVentas.TabPages.Remove(tabPageConsumo)
                Catch ex As Exception
                End Try

                Try : tabControlPAFVentas.TabPages.Remove(tabPageVencimientos)
                Catch ex As Exception
                End Try

                Try : tabControlPAFVentas.TabPages.Remove(tabPageImpresionPAF)
                Catch ex As Exception
                End Try

                Try : tabControlPAFVentas.TabPages.Remove(tabPageRecibos)
                Catch ex As Exception
                End Try

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        '************************************************************
        'Pone los tabpages necesarios segun el tipo de PAFActual.documento
        '************************************************************
        Private Sub PonerTabPages()
            Try
                If EsRegistroNuevo Then
                    tabControlPAFVentas.TabPages.Add(tabPageTraspasoPAF)
                    tabControlPAFVentas.TabPages.Add(tabPageConsumo)
                    tabControlPAFVentas.TabPages.Add(tabPageVencimientos)
                    tabControlPAFVentas.TabPages.Add(tabPageImpresionPAF)
                    tabControlPAFVentas.TabPages.Add(tabPageRecibos)
                End If

                Select Case PAFActual.DOCUMENT
                    Case Factura
                        tabControlPAFVentas.TabPages.Remove(tabPageConsumo)
                        tabControlPAFVentas.TabPages.Remove(tabPageTraspasoPAF)
                    Case Proforma
                        tabControlPAFVentas.TabPages.Remove(tabPageConsumo)
                        tabControlPAFVentas.TabPages.Remove(tabPageVencimientos)
                        tabControlPAFVentas.TabPages.Remove(tabPageTraspasoPAF)
                        tabControlPAFVentas.TabPages.Remove(tabPageRecibos)
                    Case Albaran
                        tabControlPAFVentas.TabPages.Remove(tabPageConsumo)
                        tabControlPAFVentas.TabPages.Remove(tabPageVencimientos)
                        tabControlPAFVentas.TabPages.Remove(tabPageRecibos)
                        dgImpresion2.Visible = False
                    Case Pedido
                        tabControlPAFVentas.TabPages.Remove(tabPageVencimientos)
                        tabControlPAFVentas.TabPages.Remove(tabPageRecibos)
                End Select

                Try
                    'tabControlPAFVentas.TabPages.Remove(tabPageImpresionPAF)
                Catch ex As Exception
                End Try

                tabControlPAFVentas.SelectedIndex = 0

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        '************************************************************
        'Inicia las cosas propias de las facturas
        '************************************************************
        Private Sub INCFactura()
        End Sub

        '************************************************************
        'Preparamos el dg de detalle. Y le ponermos las columnas de eleccion
        '
        '************************************************************
        Private Sub PrepararLineas()

            Try
                With dgLineasPAFVenta
                    .AllowAddNew = True
                    .AllowColMove = False
                    .AllowColSelect = False
                    .AllowDelete = True
                    .AllowRowSelect = True
                    .AllowSort = False
                    .AllowUpdate = True
                    .BorderStyle = BorderStyle.FixedSingle
                    '.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation
                    .TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation
                End With

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        '*****************************************************************
        'Funciones para crear las tablas que se usan para elegir en los dg
        '*****************************************************************

        '*****************************************************************
        'Creamos la tabla de traspasos
        '*****************************************************************
        Private Sub CrearListaTraspas()
            Try
                Dim dc1 As New DataColumn
                Dim dc2 As New DataColumn
                Dim dc3 As New DataColumn
                Dim dc4 As New DataColumn("TOTAL", GetType(Double))
                Dim dc5 As New DataColumn("CODICLIENT")


                dc1.ColumnName = "FRA"
                dc2.ColumnName = "TRASPAS"
                dc3.ColumnName = "CLIENT"

                dc2.Caption = "Traspasada?"
                dc3.Caption = "Client"
                dc4.Caption = "Import"

                Select Case PAFActual.DOCUMENT
                    Case "C"
                        dc1.Caption = "COMANDA"
                        PAFActual.dtTraspaso.Columns.Add(dc1)

                    Case "A"
                        dc1.Caption = "Albarà"
                        PAFActual.dtTraspaso.Columns.Add(dc1)
                    Case "F"

                    Case "P"
                End Select
                PAFActual.dtTraspaso.Columns.Add(dc2)
                PAFActual.dtTraspaso.Columns.Add(dc3)
                PAFActual.dtTraspaso.Columns.Add(dc4)
                PAFActual.dtTraspaso.Columns.Add(dc5)

                PAFActual.dvTraspaso = PAFActual.dtTraspaso.DefaultView
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        '*****************************************************************
        'Añade el estilo para la fila seleccionada (en este caso con negrita)
        '*****************************************************************
        Private Sub AñadirEstilos()
            Dim s As New Style
            Try

                s.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
                dgLineasPAFVenta.AddCellStyle(CellStyleFlag.SelectedRow, s)

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub CreaTablas()
            CrearListaTraspas()
            CrearTablaConsumosMuestras()
        End Sub
        Private Sub IniciarDGs(ByVal seleccion As Boolean)
            Try
                If PAFActual.DOCUMENT = Factura Then IniciarDGVencim(seleccion)
                'IniciarDGTraspas(seleccion)
                IniciarDGPAF(seleccion)

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub IniciarSegunDocumento()
            Select Case PAFActual.DOCUMENT
                Case Pedido
                    chkTRASPAS.Visible = True
                    tabPageConsumo.Text = "Consum mostres"
                    lblFacturaV.Text = rm.GetString("PEDIDO")
                    lbrFRA.Text = rm.GetString("PEDIDO")
                    lblAPAF.Text = rm.GetString("AALBARAN")
                    lblDeCliente.Text = rm.GetString("DECLIENTE")
                    lblDeFecha.Text = rm.GetString("DEFECHA")
                    lblDePAF.Text = rm.GetString("DEPEDIDO")
                    lblFecha.Text = rm.GetString("CONFECHA")
                    lblHastaCliente.Text = rm.GetString("HASTACLIENTE")
                    lblHastaFecha.Text = rm.GetString("HASTAFECHA")
                    lblHastaPAF.Text = rm.GetString("HASTAPEDIDO")
                    tabPageTraspasoPAF.Text = rm.GetString("TRASPASOAALBARAN")
                    tabPagePAF.Text = rm.GetString("PEDIDO")
                    gbEleccionClientesPAF.Text = "El·lecció comandes"
                    lblFormaParte.Text = "Traspasat a Albarans:"
                    btnTraspasarAPAF.Text = "Traspassar a Albarà"
                    lblFormaParte.Visible = True
                    lstFormaParte.Visible = True
                    'btnAbrirAF.Visible = True
                    lblVieneDe.Visible = False
                    lstVieneDePedido.Visible = False
                    'btnAbrirPAF.Visible = False
                    lblFormaParte.Text = "Forma part dels Albarans:"
                    'Me.BackColor = System.Drawing.Color.SteelBlue
                    Me.Text = rm.GetString("PEDIDOVENTATEJIDOCOMPLEMENTO")
                    PonerColores(System.Drawing.Color.Silver.ToArgb)
                    chkFacturaInternacional.Text = "Comanda Internacional"

                Case Albaran

                    lblFacturaV.Text = rm.GetString("ALBARAN")
                    lbrFRA.Text = rm.GetString("ALBARAN")
                    lblFormaParte.Text = "Forma part de les factures:"
                    lblVieneDe.Text = "Ve de les comandes:"
                    lblVieneDe.Visible = True
                    lstVieneDePedido.Visible = True
                    'btnAbrirPAF.Visible = True

                    lblPedido.Text = "Albarà"
                    tabPagePAF.Text = "Albarà"
                    lblFormaParte.Text = "Albarà inclòs en aquesta/es Factura/es"
                    'tabControlPAFVentas.TabPages.Remove(tabPageConsumo)
                    tabPageConsumo.Visible = False
                    tabControlPAFVentas.SelectedIndex = 0
                    btnNuevo.Text = "Nou Albarà"
                    btnBorrar.Text = "Borrar Albarà "
                    lblAPAF.Text = "A Factura"
                    lblDeCliente.Text = "De Client"
                    lblDeFecha.Text = "De Data"
                    lblDePAF.Text = "D'Albarà"
                    lblFecha.Text = "Amb Data"
                    lblHastaCliente.Text = "Fins a Client"
                    lblHastaFecha.Text = "Fins a Data"
                    lblHastaPAF.Text = "Fins Albarà"
                    tabPageTraspasoPAF.Text = "Traspàs a factura"
                    gbEleccionClientesPAF.Text = "El·lecció Albarans"
                    chkFacturaInternacional.Text = "Albarà internacional"
                    'chkFacturaInternacional.Visible = False
                    btnTraspasarAProforma.Visible = True
                    'Me.BackColor = System.Drawing.Color.SeaGreen
                    chkTRASPAS.Visible = True
                    Me.Text = rm.GetString("ALBARANTEJIDO")
                    PonerColores(CType(176, Byte), CType(206, Byte), CType(172, Byte))

                Case Proforma
                    lblFacturaV.Text = rm.GetString("FACTURA")
                    lbrFRA.Text = rm.GetString("FACTURA")
                    chkTRASPAS.Visible = True
                    'btnNuevo.Text = "Nova Factura"
                    'btnBorrar.Text = "Borrar Factura"
                    lblAPAF.Text = "A Albarà"
                    lblDeCliente.Text = "De Client"
                    lblDeFecha.Text = "De Data"
                    lblDePAF.Text = "De Comanda"
                    lblFecha.Text = "Amb Data"
                    lblHastaCliente.Text = "Fins a Client"
                    lblHastaFecha.Text = "Fins a Data"
                    lblHastaPAF.Text = "Fins a Comanda"
                    tabPageTraspasoPAF.Text = rm.GetString("TRASPASOAALBARAN")
                    tabPagePAF.Text = "Factura"
                    tabPageConsumo.Text = "Consum Factura"
                    gbEleccionClientesPAF.Text = "El·lecció factures"
                    lstFormaParte.Visible = False
                    lblFormaParte.Visible = False
                    'btnAbrirAF.Visible = False

                    lstVieneDePedido.Visible = True
                    lblVieneDe.Visible = True
                    'btnAbrirPAF.Visible = True
                    lblVieneDe.Text = "Ve d'Albarans:"
                    'Me.BackColor = System.Drawing.Color.Gold
                    Me.Text = rm.GetString("PROFORMAVENTATEJIDOCOMPLEMENTO")
                    cboDOM.Visible = True
                    lblDireccionDeEnvio.Visible = True

                Case Factura
                    cboDOM.Visible = True
                    lblDireccionDeEnvio.Visible = True
                    lblFacturaV.Text = rm.GetString("FACTURA")
                    lbrFRA.Text = rm.GetString("FACTURA")
                    btnNuevo.Text = "Nova Factura"
                    lblPedido.Text = rm.GetString("FACTURA")
                    btnBorrar.Text = "Borrar Factura"
                    lblAPAF.Text = "A Albarà"
                    lblDeCliente.Text = "De Client"
                    lblDeFecha.Text = "De Data"
                    lblDePAF.Text = "De Comanda"
                    lblFecha.Text = "Amb Data"
                    lblHastaCliente.Text = "Fins a Client"
                    lblHastaFecha.Text = "Fins a Data"
                    lblHastaPAF.Text = "Fins a Comanda"
                    tabPageTraspasoPAF.Text = rm.GetString("TRASPASOAALBARAN")
                    tabPagePAF.Text = "Factura"
                    tabPageConsumo.Text = "Consum Factura"
                    gbEleccionClientesPAF.Text = "El·lecció factures"
                    lstFormaParte.Visible = False
                    lblFormaParte.Visible = False
                    'btnAbrirAF.Visible = False
                    tabPageTraspasoPAF.Visible = False

                    lstVieneDePedido.Visible = True
                    lblVieneDe.Visible = True
                    'btnAbrirPAF.Visible = True
                    lblVieneDe.Text = "Ve d'Albarans:"
                    INCFactura()
                    'Me.BackColor = System.Drawing.Color.IndianRed
                    Me.Text = rm.GetString("FACTURATEJIDO")
                    PonerColores(CType(221, Byte), CType(202, Byte), CType(217, Byte))
            End Select

            PonerTabPages()

        End Sub
        Private Sub PonerColores(ByVal r As Byte, ByVal g As Byte, ByVal b As Byte)

            Me.tabPageTraspasoPAF.BackColor = System.Drawing.Color.FromArgb(CType(176, Byte), CType(206, Byte), CType(172, Byte))
            Me.tabPageVencimientos.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.tabPageImpresionPAF.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.tabPageRecibos.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.tabPageConsumo.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.tabPageVencimientos.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.tabPagePAF.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.tabPageTraspasoPAF.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))


            Me.gbTraspasarIntervalo.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.gbTraspasarLista.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.gbPAF.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.gb.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.gbVencimPAF.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.gbVencim.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.gbEleccionClientesPAF.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(b, Byte), CType(b, Byte))
            Me.gbCentro.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(b, Byte), CType(b, Byte))
            Me.gbEleccionClientesPAF.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))

            'Me.ppFitxa.Splitter.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))

            Me.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))

            Me.lblPedido.BackColor = System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))

            Me.dgLineasPAFVenta.BackColor = System.Drawing.Color.Silver

        End Sub
        Private Sub PonerColores(ByVal rgb As Integer)

            Me.tabPageTraspasoPAF.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb((CType(176, Byte), CType(206, Byte), CType(172, Byte))
            Me.tabPageVencimientos.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.tabPageImpresionPAF.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.tabPageRecibos.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.tabPageConsumo.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.tabPageVencimientos.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.tabPagePAF.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.tabPageTraspasoPAF.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))


            Me.gbTraspasarIntervalo.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.gbTraspasarLista.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.gbPAF.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.gb.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.gbVencimPAF.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.gbVencim.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))
            Me.gbEleccionClientesPAF.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(b, Byte), CType(b, Byte))
            Me.gbCentro.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(b, Byte), CType(b, Byte))
            Me.gbEleccionClientesPAF.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))

            'Me.ppFitxa.Splitter.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))

            Me.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))

            Me.lblPedido.BackColor = System.Drawing.Color.FromArgb(rgb)
            Me.dgLineasPAFVenta.BackColor = System.Drawing.Color.FromArgb(rgb) 'System.Drawing.Color.Silver

        End Sub
        Friend Sub IniciarDocumento(ByVal documento As String, Optional ByVal seleccion As Boolean = False)
            Try
                Me.arrayGrids = New System.Windows.Forms.Control() {Me.dgConsumos, Me.dgLineasPAFVenta, Me.dgTraspas, Me.dgVencim, Me.dgImpresion, Me.dgImpresion2}
                Me.arrayTextBox = New System.Windows.Forms.Control() {Me.txvFRA, Me.txrFRA, Me.txrNOMCLIENT, Me.txvNOMCLIENT, Me.txCFRA, Me.txCNOMCLIENT, Me.txtBASE1, Me.txtBRUT1, Me.txtCLIENT, Me.txtCOMIS, Me.txtDTE1, Me.txtIVA1, Me.txtOBSERV, Me.txtORDRE, Me.txtP_DTE, Me.txtRE1, Me.txtREPRES, Me.txtTOTAL, Me.txtALBCLI, Me.txvCLIENT, Me.txcCLIENT, Me.txvTOTAL, txtOBSERVOCULTA}
                Me.arrayCombos = New System.Windows.Forms.Control() {Me.cboAFacturaRecibo, Me.cboDeFacuraRecibo, Me.cboNOMCLIENT, Me.cboNOMREPRES, Me.cboDOM, Me.cboIncoterm, Me.cboSemanas}
                Me.arrayBotones = New System.Windows.Forms.Control() {Me.btnElegirCliente, Me.btnElegirRepresentate, Me.btnGenerarPedidoCompraHilosDesdeMuestra, Me.btnGenerarVencimientos, Me.btnHacerTraspaso, Me.btnEliminarFilasSeleccionadas}
                Me.arrayEtiquetas = New System.Windows.Forms.Control() {Me.lblAPAF, Me.lblBase, Me.lblCliente, Me.lblClienteConsumos, Me.lblClientePAFV, Me.lblComisio, Me.lblConFecha, Me.lblDeCliente, Me.lblDeFecha, Me.lblDePAF, Me.lblDescuento, Me.lblDesdeFecha, Me.lblDia1, Me.lblDia2, Me.lblDia3, Me.lblDia_1, Me.lblDia_2, Me.lblDia_3, Me.lblFacturaV, Me.lblFecha, Me.lblFechaPAF, Me.lblFechaPAFV, Me.lblHastaCliente, Me.lblHastaFecha, Me.lblHastaPAF, Me.lblIVA, Me.lblIVA2, Me.lblIVA_, Me.lblNumeracion, Me.lblNumeroPAF, Me.lblObservaciones, Me.lblPAF, Me.lblPedido, Me.lblRE, Me.lblRE2, Me.lblRE_, Me.lblRepresentante, Me.lblTantoPorCientoDescuento, Me.lblTotal, Me.lblTotalBruto}
                Me.arrayTabPages = New System.Windows.Forms.Control() {Me.tabPageImpresionPAF, Me.tabPagePAF, Me.tabPageTraspasoPAF, Me.tabPageVencimientos, Me.tabPageConsumo}
                Me.arrayCheckBox = New System.Windows.Forms.Control() {Me.chkSumadoEstoc, Me.chkTRASPAS, Me.chkCondicionFecha}
                Me.arrayDateTime = New System.Windows.Forms.Control() {Me.dtrDATA, Me.dateDesdeFecha, Me.dtpAFechaRecibo, Me.dtpDATA, Me.dtpDeFechaRecibo, Me.dtvDATA, Me.dtcDATA}

                AjustarLabels()

                ACN()
                PAFActual = New clsPAFVenta(ds.Tables("factur"), empresaPorDefecto, BindingContext, documento, Tejido)

                HacerBindings()

                CreaTablas()

                AñadirEstilos()

                IniciarSegunDocumento()

                PrepararLineas()

                IniciarDGs(seleccion)

                PonerModificables(soloLectura)

                MoverActual()

                'Dim p As Point
                'p.X = gbCentro.Location.X + lblNumeracion.Location.X + lblNumeracion.Size.Width
                'p.Y = lblNumeroPAF.Location.Y
                'cboSeleccionCentro.Location = p

                PSBTIPO(PAFActual.centro)

                CCN()

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

        Private Sub AjustarLabels()
            'txtDTE1.Location = New System.Drawing.Point(495, 25)
            'txtBASE1.Location = New System.Drawing.Point(579, 25)
            'txtRE1.Location = New System.Drawing.Point(671, 25)
            'txtIVA1.Location = New System.Drawing.Point(755, 25)
            'txtBRUT1.Location = New System.Drawing.Point(839, 25)
            'txtTOTAL.Location = New System.Drawing.Point(905, 25)

            'lblTotal.Location = New System.Drawing.Point(907, 7)
            'lblTotalBruto.Location = New System.Drawing.Point(843, 7)
            'lblIVA.Location = New System.Drawing.Point(759, 7)
            'lblDescuento.Location = New System.Drawing.Point(497, 7)
            'lblBase.Location = New System.Drawing.Point(576, 7)
            'lblRE.Location = New System.Drawing.Point(675, 7)
        End Sub

#End Region

#Region "MODIFICAR ORIGEN"

        Private Sub PonerControlesNuevo(ByVal sonControlesParaNuevo As Boolean)
            Dim i As Integer
            Dim B As Boolean
            Try
                'HACK FEO
                B = Not sonControlesParaNuevo

                If B Then
                    btnActualizar.Text = rm.GetString("ACTUALIZAR")
                Else
                    btnActualizar.Text = rm.GetString("CONFIRMAR")
                End If

                btnSiguiente.Enabled = B : btnModificar.Enabled = B
                btnAnterior.Enabled = B
                btnUltimo.Enabled = B
                btnPrimero.Enabled = B
                btnNuevo.Enabled = B
                btnBorrar.Enabled = B
                btnRecargar.Enabled = B
                btnVerLista.Enabled = B
                btnTancar.Enabled = B
                cboFRA.AutoCompletion = Not editando

                Select Case PAFActual.documento
                    Case "C"
                        lblFormaParte.Visible = B
                        lstFormaParte.Visible = B
                        'btnAbrirAF.Visible = B
                    Case "A"
                        lblFormaParte.Visible = B
                        'btnAbrirPAF.Visible = B
                        lblVieneDe.Visible = B
                        'btnAbrirAF.Visible = B
                        lstFormaParte.Visible = B
                        lstVieneDePedido.Visible = B
                    Case "F"
                        lblVieneDe.Visible = B
                        lstVieneDePedido.Visible = B
                        'btnAbrirAF.Visible = B

                    Case "M"
                        lblVieneDe.Visible = B
                        lstVieneDePedido.Visible = B
                        'btnAbrirAF.Visible = B

                End Select
                txtBASE1.Visible = B
                lblBase.Visible = B

                txtBRUT1.Visible = B
                lblTotalBruto.Visible = B

                txtDTE1.Visible = B
                lblDescuento.Visible = B

                txtIVA1.Visible = B
                lblIVA.Visible = B

                txtRE1.Visible = B
                lblRE.Visible = B

                txtTOTAL.Visible = B
                lblTotal.Visible = B

                For i = 1 To tabControlPAFVentas.TabPages.Count - 1
                    tabControlPAFVentas.TabPages(i).Enabled = B
                Next

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
                    editando = True
                    PonerControlesNuevo(False)
                    PonerModificables(modificable)
                    PonerTabPages()
                    EsRegistroNuevo = False
                    dgConsumos.Visible = True
                    dgLineasPAFVenta.Visible = True
                    gbEleccionClientesPAF.Visible = True
                    PAFActual.actualizarNumeraciones()
                    MoverActual()
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Friend Overrides Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Try
                cargando = True
                If EsRegistroNuevo AndAlso (PAFActual.DOCUMENT = Albaran OrElse PAFActual.DOCUMENT = Pedido) Then
                    If PAFActual.ALBCLI = "" Then
                        MessageBox.Show(rm.GetString("FALTAPEDIDOCLIENTE"), rm.GetString("INFORMACION"), MessageBoxButtons.OK)
                        cargando = False
                        Exit Sub
                    End If
                End If
                If PAFActual.CLIENT = 0 Then
                    MessageBox.Show(rm.GetString("FALTACLIENTE"), rm.GetString("INFORMACION"), MessageBoxButtons.OK)
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
                            PAFActual.lineasVenta.tabla.RejectChanges()
                            If PAFActual.DOCUMENT = Factura Then PAFActual.vencimientos.tabla.RejectChanges()
                            cargando = False
                            Exit Sub
                    End Select
                End If

                'If PAFActual.mClienteSel <> -1 Then CambioEleccionTodosPAF()
                'cboSeleccionClienteParaPAF.Visible = False
                'rdoVerPAFDeCliente.Checked = False
                'rdoVerTodasPAF.Checked = True

                PAFActual.ActualizarOrigen() : ActualizarOrigen()
                If Not desplazando Then PSBTIPO(PAFActual.centro) : cargando = False

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Try
                If MessageBox.Show(rm.GetString("BorrarActual"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    'Despues borramos todos sus "hilos" de la tabla filcol
                    cargando = True
                    editando = True
                    'Primero borramos el tejido de la tabla tejido
                    PAFActual.borrar() : ActualizarOrigen()

                    AñadirBindingCombo(cboFRA, PAFActual.dvIdentificadores, "FRA", "FRA")
                    PAFActual.tabla.AcceptChanges()

                    PSBTIPO(PAFActual.centro) : cargando = False
                    editando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub

#End Region

#Region "ESPECÍFICO"

#Region "PAF"

        Private Sub PonerBindingLabels()
            Try
                AñadirBinding(lblIVA2, PAFActual.dvForm, "P_IVA1")
                AñadirBinding(lblRE2, PAFActual.dvForm, "P_RE1")
                AñadirBinding(lblDia1, PAFActual.dvForm, "DIA1")
                AñadirBinding(lblDia2, PAFActual.dvForm, "DIA2")
                AñadirBinding(lblDia3, PAFActual.dvForm, "DIA3")
                AñadirBinding(lblFORMA, PAFActual.dvForm, "NOMFORMA")
                AñadirBinding(lblPOB, PAFActual.dvForm, "POB")
                AñadirBinding(lblDOM, PAFActual.dvForm, "DOM2")
                AñadirBinding(lblPROV, PAFActual.dvForm, "PROV")
                AñadirBinding(lblPAIS, PAFActual.dvForm, "PAIS")

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        '**************************************************
        'Crea los bindings del PAF y de los dg
        '**************************************************
        Private Sub HacerBindings()
            Try
                PonerBindingLabels()
                HacerBindingsTodos(PAFActual.dvForm)

                AñadirBinding(cboFRA, PAFActual.dvForm, "FRA")
                AñadirBindingCombo(cboFRA, PAFActual.dvIdentificadores, "FRA", "FRA")

                AñadirBindingCombo(cboNOMCLIENT, PAFActual.dtclients, CCClients, CNClients)
                OcultarTodasColumnaCbo(cboNOMCLIENT)

                AñadirBindingCombo(cboNOMREPRES, PAFActual.dtRepres, CCRepres, CNRepres)
                OcultarTodasColumnaCbo(cboNOMREPRES)

                dgLineasPAFVenta.SetDataBinding(PAFActual.lineasVenta.dvForm, "")

                If PAFActual.DOCUMENT = Factura Then
                    AñadirBindingCombo(cboDeFacuraRecibo, PAFActual.dtIdentificadores.Copy, "FRA", "FRA")
                    AñadirBindingCombo(cboAFacturaRecibo, PAFActual.dtIdentificadores.Copy, "FRA", "FRA")
                    AñadirBindingCombo(cboDEPAFIMPRESION, PAFActual.dtIdentificadores.Copy, "FRA", "FRA")
                    AñadirBindingCombo(cboAPAFIMPRESION, PAFActual.dtIdentificadores.Copy, "FRA", "FRA")
                    AñadirBindingCombo(cboDOM, PAFActual.dtDireccionesEnvio, "DOM", "DOM")
                    dgVencim.SetDataBinding(PAFActual.vencimientos.dvForm, "")
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        '****************************************
        'Pone editables las cosas para poder modificar un PAF
        '************************************************
        'Le decimos si es sololectura
        Private Sub PonerModificables(ByVal sololecura As Boolean)
            Try
                PonerMod(sololecura)
                gbEleccionClientesPAF.Visible = sololecura
                If Not sololecura Then
                    cboFRA.DataSource = Nothing
                Else
                    AñadirBindingCombo(cboFRA, PAFActual.dvIdentificadores, "FRA", "FRA") : PAFActual.tabla.AcceptChanges()
                End If
                cboFRA.LimitToList = sololecura
                cboFRA.SuperBack = sololecura
                cboFRA.ReadOnly = Not sololecura

                ModiNuev(sololecura)

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        '************************************************
        'Genera un nuevo PAF
        '**************************************************
        Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Dim drNew As System.Data.DataRow
            Dim cm As CurrencyManager
            Dim i As Integer
            Try
                If consulta() Then
                    cargando = True
                    EsRegistroNuevo = True

                    If PAFActual.mClienteSel <> -1 Then CambioEleccionTodosPAF()
                    cboSeleccionClienteParaPAF.Visible = False
                    rdoVerPAFDeCliente.Checked = False
                    rdoVerTodasPAF.Checked = True

                    dgConsumos.Visible = False
                    dgLineasPAFVenta.Visible = False
                    gbEleccionClientesPAF.Visible = False
                    QuitarTabPages()
                    PonerModificables(modificable)
                    PonerControlesNuevo(True)
                    PAFActual.NuevoRegistro()

                    'lblTrasapaso.ForeColor = System.Drawing.SystemColors.ControlText.Green
                    'Select Case PAFActual.DOCUMENT
                    '    Case Pedido : lblTrasapaso.Text = rm.GetString("PEDIDONOTRASP")
                    '    Case Albaran : lblTrasapaso.Text = rm.GetString("ALBARANOTRASP")
                    'End Select

                    lstFormaParte.Items.Clear()
                    lstVieneDePedido.Items.Clear()

                    'se le pasa esto pq todavia no sabemos que hilo es el que se va a introducir
                    MoverActual(True)

                    PSBTIPO(PAFActual.centro) : cargando = False

                End If
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

#End Region

#Region "FORMA PARTE"

        Private Sub btnAbrirAF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbrirAF.Click
            Try
                Select Case PAFActual.documento
                    Case "C" : CargarAlbaran(lstFormaParte)
                    Case "A" : CargarFactura(lstFormaParte)
                End Select

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub btnAbrirPAF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbrirPAF.Click
            Try
                Select Case PAFActual.documento
                    Case "C" : CargarAlbaran(lstVieneDePedido)
                    Case "A" : CargarPedido(lstVieneDePedido)
                    Case "F" : CargarAlbaran(lstVieneDePedido)
                End Select

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub CargarPedido(ByVal lst As ListBox)
            Try
                If lst.SelectedItems.Count = 0 Then
                    MessageBox.Show("Selecciona una factura")
                Else
                    Dim f As frmTejidoVentaPedido = frmTejidoVentaPedido.GetInstance()
                    AddHandler f.Closed, AddressOf CType(Me.MdiParent, frmPrincipal).childCerrado
                    AddHandler f.Load, AddressOf CType(Me.MdiParent, frmPrincipal).childAbierto : AddHandler f.Activated, AddressOf CType(Me.MdiParent, frmPrincipal).childOcultandoMostrando
                    f.MdiParent = Me.MdiParent
                    Me.Visible = False
                    f.cargando = True
                    f.Show()
                    f.BringToFront()
                    f.PAFActual.CambiarAReg(lst.SelectedItem, iraregistro)
                    f.MoverActual()
                    f.cargando = False
                    cargando = False
                    Me.Close()
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub CargarAlbaran(ByVal lst As ListBox)
            Try
                If lst.SelectedItems.Count = 0 Then
                    MessageBox.Show("Selecciona un Albarà")
                Else
                    Dim f As frmTejidoVentaAlbaran = frmTejidoVentaAlbaran.GetInstance()
                    AddHandler f.Closed, AddressOf CType(Me.MdiParent, frmPrincipal).childCerrado
                    AddHandler f.Load, AddressOf CType(Me.MdiParent, frmPrincipal).childAbierto : AddHandler f.Activated, AddressOf CType(Me.MdiParent, frmPrincipal).childOcultandoMostrando
                    f.MdiParent = Me.MdiParent
                    Me.Visible = False
                    f.cargando = True
                    f.Show()
                    f.BringToFront()
                    f.PAFActual.CambiarAReg(lst.SelectedItem, iraregistro)
                    f.MoverActual()
                    f.cargando = False
                    cargando = False
                    Me.Close()
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub CargarFactura(ByVal lst As ListBox)
            Try

                If lst.SelectedItems.Count = 0 Then
                    MessageBox.Show("Selecciona una factura")
                Else
                    Dim f As frmVentaFactura = frmVentaFactura.GetInstance()

                    AddHandler f.Closed, AddressOf CType(Me.MdiParent, frmPrincipal).childCerrado
                    AddHandler f.Load, AddressOf CType(Me.MdiParent, frmPrincipal).childAbierto : AddHandler f.Activated, AddressOf CType(Me.MdiParent, frmPrincipal).childOcultandoMostrando
                    f.MdiParent = Me.MdiParent
                    Me.Visible = False
                    f.cargando = True
                    f.Show()
                    f.BringToFront()
                    f.PAFActual.CambiarAReg(lst.SelectedItem, iraregistro)
                    f.MoverActual()
                    f.cargando = False
                    cargando = False
                    Me.Close()
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub CargarListaFPPedido(ByRef ArrayFormaParte As ArrayList)
            Try
                Dim cmdSelTemp As New MySqlCommand("SELECT FRA FROM " & tablaLineasVenta & " WHERE (DOCUMENT = ""A"" AND COMAN = " & PAFActual.FRA & "  AND CENTRO = """ & PAFActual.centro & """)", cnn)

                ACN()
                Dim daread As MySqlDataReader = cmdSelTemp.ExecuteReader

                While daread.Read
                    If ArrayFormaParte.Contains(daread.Item("FRA")) Then
                    Else
                        'Si no esta lo ponemos
                        ArrayFormaParte.Add(daread.Item("FRA"))
                    End If
                End While
                daread.Close()
                CCN()

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub CargarListaFPAlbaran(ByRef ArrayFormaParte As ArrayList, ByRef ArrayVD As ArrayList)
            Dim i As Integer
            Try

                'Aqui miraremos de que pedidos forma parte este albarán
                For i = 0 To PAFActual.lineasVenta.dvForm.Count - 1
                    If ArrayVD.Contains(PAFActual.lineasVenta.dvForm(i).Item("COMAN")) Then
                    Else
                        'Si no esta lo ponemos
                        ArrayVD.Add(PAFActual.lineasVenta.dvForm(i).Item("COMAN"))
                    End If
                Next

                'Esto de antes valdria para saber los pedidos pero queremos saber las facturas que vienen
                'de este albaran. Esta informacion esta en "ALBAR" de las facuras. Hay que hacer un select
                'de todas las facturas que sean del mismo tipo que este y que tenga el ALBAR
                'lo mismo que el PAFActual.documento actual
                Dim cmdSelTemp As New MySqlCommand("SELECT FRA FROM " & tablaLineasVenta & " WHERE (DOCUMENT = ""F"" AND ALBAR = " & PAFActual.FRA & " AND CENTRO = """ & PAFActual.centro & """)", cnn)
                ACN()
                Dim daread As MySqlDataReader = cmdSelTemp.ExecuteReader

                While daread.Read
                    If ArrayFormaParte.Contains(daread.Item("FRA")) Then
                    Else
                        'Si no esta lo ponemos
                        ArrayFormaParte.Add(daread.Item("FRA"))
                    End If
                End While
                daread.Close()
                CCN()

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub CargarListaFPFactura(ByRef ArrayFormaParte As ArrayList)
            Dim i As Integer
            Try
                'En cada linea de la factura tenemos de que albarán viene
                For i = 0 To PAFActual.lineasVenta.dvForm.Count - 1
                    If ArrayFormaParte.Contains(PAFActual.lineasVenta.dvForm(i).Item("ALBAR")) Then
                    Else
                        'Si no esta lo ponemos
                        ArrayFormaParte.Add(PAFActual.lineasVenta.dvForm(i).Item("ALBAR"))
                    End If
                Next
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub CargarListaFormaParte()
            Try
                'La abreviacion FP es FORMA PARTE
                'La abreviacion VD es VIENE DE
                Dim ArrayFP As New ArrayList
                Dim ArrayVD As New ArrayList
                Select Case PAFActual.DOCUMENT
                    Case "C" : CargarListaFPPedido(ArrayFP)
                    Case "A" : CargarListaFPAlbaran(ArrayFP, ArrayVD)
                    Case "F" : CargarListaFPFactura(ArrayVD)
                    Case "M" : CargarListaFPFactura(ArrayVD)
                End Select
                RellenarListaDesdeArrayList(lstFormaParte, ArrayFP)
                RellenarListaDesdeArrayList(lstVieneDePedido, ArrayVD)

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

#End Region

#Region "VENCIMIENTOS"

        Private Sub IniciarDGVencim(ByVal seleccion As Boolean)
            Dim i As Integer
            Try
                'TODO Es un calendario
                OcultarColumnasDG(dgVencim)
                i = 0
                PPCol2("EMISSIO", dgVencim, rm.GetString("EMISION"), "Short Date", True,
                        80, False, PresentationEnum.Normal, False, 80, i, False)
                i = i + 1
                PPCol2("VENCIM", dgVencim, rm.GetString("VENCIMIENTO"), "Short Date", True,
                        80, False, PresentationEnum.Normal, False, 80, i, False)
                i = i + 1
                PPCol2("IMPORT", dgVencim, rm.GetString("IMPORTE"), "#,##0.00", True,
                        80, False, PresentationEnum.Normal, False, 80, i, False, Nothing, True, 0, AlignHorzEnum.Far)
                i = i + 1
                PPCol2("REMES", dgVencim, rm.GetString("REMESADO"), "", True,
                        50, False, PresentationEnum.CheckBox, False, 50, i, False, Nothing, False, 0, AlignHorzEnum.Center)

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub btnGenerarVencimientos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerarVencimientos.Click
            Try
                PAFActual.vencimientos.GenerarVencimientos()
                PAFActual.ActualizarOrigen()

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

#End Region

#Region "DETALLE PAF"

        Private Sub IniciarDGPAF(ByVal seleccion As Boolean)
            Dim i As Integer
            Try

                OcultarColumnasDG(dgLineasPAFVenta)
                i = 0
                Select Case PAFActual.documento
                    Case "C"
                    Case "A"
                        PPCol2("COMAN", dgLineasPAFVenta, rm.GetString("PEDIDO"), "", True, 55, False,
                                PresentationEnum.Normal, False, 55, i, False)
                        i = i + 1
                    Case "F"
                        PPCol2("ALBAR", dgLineasPAFVenta, rm.GetString("ALBARAN"), "", True, 55, False,
                                PresentationEnum.Normal, False, 55, i, False)
                        i = i + 1
                    Case "M"
                        PPCol2("ALBAR", dgLineasPAFVenta, rm.GetString("ALBARAN"), "", True, 55, False,
                                PresentationEnum.Normal, False, 55, i, False)
                        i = i + 1
                End Select


                PPCol2("TIPUS", dgLineasPAFVenta, rm.GetString("TIPO"), "", True, 35, False,
                                PresentationEnum.ComboBox, False, 35, i, False, cboTipo)
                i = i + 1
                'cboTipo.ReBind(True)
                If seleccion Then
                    cboTipo.Rebind(True)
                Else
                    AñadirBindingCombo(cboTipo, PAFActual.lineasVenta.dtTipo, "NOMTIPUS", "TIPUS")
                End If

                AutosizeColumnasTrueDBDropdown(cboTipo)

                PPCol2("MOSTRA", dgLineasPAFVenta, rm.GetString("ARTICULO"), "", True, 100, False,
                        PresentationEnum.ComboBox, False, 100, i, False, cboMuestra)
                i = i + 1
                If seleccion Then
                    cboMuestra.Rebind(True)
                Else
                    AñadirBindingCombo(cboMuestra, PAFActual.lineasVenta.dtTeixits.DefaultView, "", "CODI")
                End If

                If seleccion Then
                    cboMuestra2.Rebind(True)
                Else
                    AñadirBindingCombo(cboMuestra2, PAFActual.lineasVenta.dvMuestras, "", "CODI")
                End If

                PPCol2("ELEGIRTEJIDO", dgLineasPAFVenta, "", "", True, 20, False,
                        PresentationEnum.Normal, True, 20, i, False)
                i = i + 1

                PPCol2("DESCRI", dgLineasPAFVenta, rm.GetString("DESCRIPCION"), "", True, 220, False,
                        PresentationEnum.Normal, False, 220, i, False)
                i = i + 1

                PPCol2("COLOR", dgLineasPAFVenta, rm.GetString("COLOR"), "", True, 100, False,
                        PresentationEnum.ComboBox, False, 100, i, False, cboColor)
                i = i + 1
                If seleccion Then
                    cboColor.Rebind(True)
                Else
                    AñadirBindingCombo(cboColor, PAFActual.lineasVenta.dtColor, "COLOR", "COLOR")
                End If

                i = i + 1
                PPCol2("TALLA", dgLineasPAFVenta, rm.GetString("TALLA"), "", True, 40, False,
                        PresentationEnum.ComboBox, False, 40, i, False, cboTalla)
                i = i + 1
                If seleccion Then
                    cboTalla.Rebind(True)
                Else
                    AñadirBindingCombo(cboTalla, PAFActual.lineasVenta.dtTalla, "TALLA", "TALLA")
                End If
                PPCol2("TALLAL", dgLineasPAFVenta, rm.GetString("TALLAL"), "", True, 80, False,
                        PresentationEnum.Normal, False, 80, i, False)
                i = i + 1
                PPCol2("TALLAH", dgLineasPAFVenta, rm.GetString("TALLAH"), "", True, 80, False,
                        PresentationEnum.Normal, False, 80, i, False)
                i = i + 1

                PPCol2("KM", dgLineasPAFVenta, "K/M", "", True, 40, False,
                        PresentationEnum.ComboBox, False, 40, i, False, cboKM)
                i = i + 1
                If seleccion Then
                    cboKM.Rebind(True)
                Else
                    AñadirBindingCombo(cboKM, PAFActual.lineasVenta.dtKM, "NOMKM", "KM")
                End If

                PPCol2("UNITATS", dgLineasPAFVenta, rm.GetString("UNIDADES"), "#,##0.00", True, 45, False, PresentationEnum.Normal, False, 45, i, False, Nothing, True, 0, AlignHorzEnum.Far)
                i = i + 1
                If PAFActual.documento = Pedido Then
                    PPCol2("PERREBRE", dgLineasPAFVenta, "Per Enviar", "#,##0.00", True, 40, False, PresentationEnum.Normal, False, 40, i, False, Nothing, True, 0, AlignHorzEnum.Far)
                    i = i + 1
                    PPCol2("REBUT", dgLineasPAFVenta, "Enviat", "#,##0.00", True, 40, False, PresentationEnum.Normal, False, 40, i, False, Nothing, True, 0, AlignHorzEnum.Far)
                    i = i + 1
                End If
                PPCol2("PREU", dgLineasPAFVenta, rm.GetString("PRECIO"), "#,##0.00", True, 40, False, PresentationEnum.Normal, False, 40, i, False, Nothing, True, 0, AlignHorzEnum.Far)
                i = i + 1
                PPCol2("DTE", dgLineasPAFVenta, "Dte.", "#,##0.00", True, 40, False, PresentationEnum.Normal, False, 40, i, False, Nothing, True, 0, AlignHorzEnum.Far)
                i = i + 1
                PPCol2("IMPORT", dgLineasPAFVenta, rm.GetString("IMPORTE"), "#,##0.00", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, False, Nothing, True, 0, AlignHorzEnum.Far)
                i = i + 1

                'JP: 02/08/2017
                PPCol2("COMPOSICIO", dgLineasPAFVenta, rm.GetString("COMPOSICIO"), "", True, 120, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 120, i, False, Nothing, True, 0, AlignHorzEnum.Far)
                i = i + 1
                PPCol2("PESO", dgLineasPAFVenta, rm.GetString("PESO"), "#,##0.00", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, False, Nothing, True, 0, AlignHorzEnum.Far)
                i = i + 1
                PPCol2("NCCODE", dgLineasPAFVenta, rm.GetString("NCCODE"), "", True, 120, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 120, i, False, Nothing, True, 0, AlignHorzEnum.Far)
                i = i + 1

                If PAFActual.documento = Pedido Then
                    PPCol2("TRASPASARLINEA", dgLineasPAFVenta, "Traspàs linea", "", True, 20, False, PresentationEnum.Normal, True, 20, i, False)
                    i = i + 1
                    PPCol2("TRASPASARLINEAORDEN", dgLineasPAFVenta, "Traspas a ordre", "", True, 20, False, PresentationEnum.Normal, True, 20, i, False)
                End If

                'i = i + 1
                'PPCol2("NLINEA", dgLineasPAFVenta, rm.GetString("LINEA"), "", True, 55, False, PresentationEnum.Normal, False, 55, i, False)

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

        Private Function EscogerMuestra() As Boolean
            Try
                Cursor = Cursors.WaitCursor
                Dim f As frmMuestrasLista = frmMuestrasLista.GetInstance(esEleccion, PAFActual.centro)
                f.clienteSeleccionado = PAFActual.CLIENT
                f.ShowDialog()
                Cursor = Cursors.Default
                If Not f.dr Is Nothing Then
                    PAFActual.lineasVenta.MOSTRA = f.dr("CODI")
                    PAFActual.lineasVenta.DESCRI = f.dr("DESCRI")
                    PAFActual.lineasVenta.HacerCalculosLineasPAF("MOSTRA")
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
        Private Function EscogerTejido() As Boolean
            Try
                Cursor = Cursors.WaitCursor
                Dim f As frmTejidosLista = frmTejidosLista.GetInstance(esEleccion, PAFActual.centro)
                f.ShowDialog()
                Cursor = Cursors.Default
                If Not f.dr Is Nothing Then
                    PAFActual.lineasVenta.MOSTRA = f.dr("CODI")
                    PAFActual.lineasVenta.DESCRI = general.nz(f.dr("DESCRI"), "")
                    PAFActual.lineasVenta.HacerCalculosLineasPAF("MOSTRA")
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
                    PAFActual.lineasVenta.MOSTRA = f.dr("CODI")
                    PAFActual.lineasVenta.DESCRI = f.dr("DESCRI")
                    PAFActual.lineasVenta.HacerCalculosLineasPAF("MOSTRA")
                    f = Nothing
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Function

        Private Function todosServidos() As Boolean
            Dim unidadesTotales As Double
            Dim unidadesEnviadas As Double
            Try
                unidadesTotales = sumaTotal("UNITATS", PAFActual.lineasVenta.dvForm)
                unidadesEnviadas = sumaTotal("REBUT", PAFActual.lineasVenta.dvForm)

                If unidadesTotales - unidadesEnviadas <= 0 Then
                    Return True
                End If
                Return False

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Function
        Private Sub dgLineasPAFVenta_ButtonClick(ByVal sender As System.Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgLineasPAFVenta.ButtonClick
            Dim cambios As Boolean = False
            Try
                If PuedoModificar() Then
                    cargando = True
                    If dgLineasPAFVenta(dgLineasPAFVenta.Row, "TIPUS") <> "" AndAlso PAFActual.lineasVenta.dvForm.Count <> 0 Then
                        Select Case e.Column.DataColumn.DataField
                            Case "ELEGIRTEJIDO"
                                Select Case PAFActual.lineasVenta.TIPUS
                                    Case Muestra : cambios = EscogerMuestra()
                                    Case Tejido : cambios = EscogerTejido()
                                    Case Fornitura : cambios = EscogerFornitura()
                                End Select
                                If cambios Then PAFActual.lineasVenta.HacerCalculosLineasPAF(e.Column.DataColumn.DataField)

                            Case "TRASPASARLINEA"
                                Dim p As Point
                                p.X = System.Windows.Forms.Cursor.Position.X - dgLineasPAFVenta.Width + 50
                                p.Y = System.Windows.Forms.Cursor.Position.Y + 10
                                PAFActual.ActualizarOrigen()
                                PAFActual.TraspasarLinea(p)
                                If todosServidos() Then
                                    If MessageBox.Show("Totes les linies han estat servides vols marcar el document com a traspassat?", rm.GetObject("INFORMACION"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                        PAFActual.TRASPAS = True
                                    End If
                                End If

                            Case "TRASPASARLINEAORDEN"
                                If PAFActual.lineasVenta.TIPUS = Muestra Then
                                    Dim p As Point
                                    p.X = System.Windows.Forms.Cursor.Position.X - dgLineasPAFVenta.Width + 50
                                    p.Y = System.Windows.Forms.Cursor.Position.Y + 10
                                    PAFActual.GenerarOrden(p)
                                End If
                        End Select
                    End If
                    cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub dgLineasPAFVenta_AfterColUpdate(ByVal sender As System.Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgLineasPAFVenta.AfterColUpdate
            Dim col As C1DataColumn = dgLineasPAFVenta.Splits(0).DisplayColumns(dgLineasPAFVenta.Col).DataColumn
            Try
                If PuedoModificar() Then
                    cargando = True
                    dgLineasPAFVenta.UpdateData()
                    Select Case col.DataField'columna
                        Case "TIPUS" : TratarCambioFilaTipo()
                    End Select
                    PAFActual.lineasVenta.HacerCalculosLineasPAF(col.DataField) '(columna)
                    cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False
            End Try
        End Sub
        Private Sub dgLineasPAFVenta_RowColChange(ByVal sender As System.Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgLineasPAFVenta.RowColChange
            'Dim columna As String = e.Column.DataColumn.DataField


            Try

                If PuedoModificar() Then
                    cargando = True
                    'Miramos si hay un cambio de columna
                    If e.LastRow <> dgLineasPAFVenta.Row Then
                        PAFActual.lineasVenta.CambioFila()
                        TratarCambioFilaTipo()
                    End If
                    SaltarSegunTipo()
                    AutosizeColumnasTrueDBDropdown(cboColor)
                    AutosizeColumnasTrueDBDropdown(cboTalla)
                    cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub dgLineasPAFVenta_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgLineasPAFVenta.GotFocus
            Try
                If PuedoModificar() Then
                    cargando = True
                    If PAFActual.TRASPAS = True And dgLineasPAFVenta.Enabled = True Then
                        MessageBox.Show(rm.GetString("NOTRASPLINIASDOCTRAS"))
                    End If
                    cargando = False
                End If
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub dgLineasPAFVenta_OwnerDrawCell(ByVal sender As System.Object, ByVal e As C1.Win.C1TrueDBGrid.OwnerDrawCellEventArgs) Handles dgLineasPAFVenta.OwnerDrawCell
            Try
                If dgLineasPAFVenta.Row = e.Row Then
                    e.Style.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
                    If dgLineasPAFVenta(e.Row, "TIPUS") = "T" Then
                        e.Style.BackColor = Color.LightGreen
                    Else
                        If dgLineasPAFVenta(e.Row, "TIPUS") = "M" Then
                            e.Style.BackColor = Color.LightBlue
                        End If
                    End If
                Else
                    e.Style.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular)
                    If dgLineasPAFVenta(e.Row, "TIPUS") = "T" Then
                        e.Style.BackColor = Color.LightGreen
                    Else
                        If dgLineasPAFVenta(e.Row, "TIPUS") = "M" Then
                            e.Style.BackColor = Color.LightBlue
                        End If
                    End If
                End If

                If dgLineasPAFVenta(e.Row, "TIPUS") = "T" AndAlso editando = False Then
                    e.Style.BackColor = Color.LightGreen
                Else
                    If dgLineasPAFVenta(e.Row, "TIPUS") = "M" AndAlso editando = False Then
                        e.Style.BackColor = Color.LightBlue
                    End If
                End If
                'e.Handled = True
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

        Private Sub SaltarSegunTipo()
            Dim columna As String
            Try
                columna = dgLineasPAFVenta.Splits(0).DisplayColumns(dgLineasPAFVenta.Col).DataColumn.DataField

                If PAFActual.lineasVenta.TIPUS = "M" Then
                    If columna = "KM" Then dgLineasPAFVenta.Col = dgLineasPAFVenta.Col + 1
                Else
                    If columna = "TALLA" Then dgLineasPAFVenta.Col = dgLineasPAFVenta.Col + 1
                End If
                If dgLineasPAFVenta(dgLineasPAFVenta.Row, "TIPUS") = "" Then dgLineasPAFVenta.Col = dgLineasPAFVenta.Splits(0).DisplayColumns.IndexOf(dgLineasPAFVenta.Splits(0).DisplayColumns("TIPUS").DataColumn)

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False
            End Try
        End Sub
        Private Sub TratarCambioFilaTipo()
            Try
                CambioTipo()
                'Asociamos las tablas adecuadas y ajustamos el tamaño
                With PAFActual.lineasVenta
                    If .TIPUS = "T" Then
                        dgLineasPAFVenta.Splits(0).DisplayColumns("TALLA").AllowFocus = False
                        dgLineasPAFVenta.Splits(0).DisplayColumns("KM").AllowFocus = True
                        AñadirBindingCombo(cboMuestra, PAFActual.lineasVenta.dtTeixits.DefaultView, "", "CODI")

                    End If

                    If .TIPUS = "M" Then
                        dgLineasPAFVenta.Splits(0).DisplayColumns("TALLA").AllowFocus = True
                        dgLineasPAFVenta.Splits(0).DisplayColumns("KM").AllowFocus = False
                        AñadirBindingCombo(cboMuestra2, PAFActual.lineasVenta.dvMuestras, "", "CODI")
                    End If

                    'If .TIPUS = "" OrElse dgLineasPAFVenta(dgLineasPAFVenta.Row, "TIPUS") = "" Then
                    '    dgLineasPAFVenta.Splits(0).DisplayColumns("TALLA").AllowFocus = False
                    '    dgLineasPAFVenta.Splits(0).DisplayColumns("KM").AllowFocus = False
                    '    dgLineasPAFVenta.Splits(0).DisplayColumns("DESCRI").AllowFocus = False
                    '    dgLineasPAFVenta.Splits(0).DisplayColumns("COLOR").AllowFocus = False
                    '    dgLineasPAFVenta.Splits(0).DisplayColumns("PREU").AllowFocus = False
                    '    dgLineasPAFVenta.Splits(0).DisplayColumns("IMPORT").AllowFocus = False
                    '    dgLineasPAFVenta.Splits(0).DisplayColumns("MOSTRA").AllowFocus = False
                    'Else
                    '    dgLineasPAFVenta.Splits(0).DisplayColumns("DESCRI").AllowFocus = True
                    '    dgLineasPAFVenta.Splits(0).DisplayColumns("COLOR").AllowFocus = True
                    '    dgLineasPAFVenta.Splits(0).DisplayColumns("PREU").AllowFocus = True
                    '    dgLineasPAFVenta.Splits(0).DisplayColumns("IMPORT").AllowFocus = True
                    '    dgLineasPAFVenta.Splits(0).DisplayColumns("MOSTRA").AllowFocus = True
                    'End If
                End With
                AñadirBindingCombo(cboColor, PAFActual.lineasVenta.dtColor, "COLOR", "COLOR")
                AñadirBindingCombo(cboKM, PAFActual.lineasVenta.dtKM, "NOMKM", "KM")
                AñadirBindingCombo(cboTalla, PAFActual.lineasVenta.dtTalla, "TALLA", "TALLA")

                AutoSizeCC(dgLineasPAFVenta)

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False
            End Try
        End Sub
        Private Sub CambioTipo()
            Try
                With PAFActual.lineasVenta
                    If .TIPUS = "T" Then
                        If Not dgLineasPAFVenta.Columns("MOSTRA").DropDown Is cboMuestra Then
                            dgLineasPAFVenta.Columns("MOSTRA").DropDown = cboMuestra
                            '.DESCRI = ""
                        End If
                    Else
                        If .TIPUS = "M" Then
                            If Not dgLineasPAFVenta.Columns("MOSTRA").DropDown Is cboMuestra2 Then _
                                                        dgLineasPAFVenta.Columns("MOSTRA").DropDown = cboMuestra2
                            '.DESCRI = ""
                        End If
                    End If
                End With

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

#End Region

#Region "CONSUMO MUESTRAS"

        'Tiene que mirar el numero de unidades que se necesitan de cada muestra y mirando
        'en el desglose de cada color de las muestras mirar que hilos necesita
        'mirar el stock de ese hilo y mirar la diferencia si no tiene suficiente hacer un
        'pedido para esos hilos o los que se necesite si son hilos diferentes
        'Multiplicar las unidades necesaria de una muestra por el consumo de la muestra para obtener los
        'hilos neceasarios
        Private Sub CrearTablaConsumosMuestras()
            Dim i As Integer
            Try
                dtConsumos.Columns.Clear()
                dtConsumos.Clear()

                Dim ts As New DataGridTableStyle
                ts.MappingName = "CONSUMOS"

                Dim dcMOSTRA As New DataColumn("CHILO")
                dcMOSTRA.ColumnName = "CHILO"
                dcMOSTRA.DefaultValue = ""
                dtConsumos.Columns.Add(dcMOSTRA)

                Dim dcPROVEEDOR As New DataColumn("PROVEEDOR")
                dcPROVEEDOR.ColumnName = "CPROVEEDOR"
                dcPROVEEDOR.DefaultValue = ""
                dtConsumos.Columns.Add(dcPROVEEDOR)

                Dim dcCOLOR As New DataColumn
                dcCOLOR.ColumnName = "CCOLOR"
                dcCOLOR.DefaultValue = ""
                dtConsumos.Columns.Add(dcCOLOR)

                Dim dcCANTIDAD As New DataColumn
                dcCANTIDAD.ColumnName = "CCANTIDAD"
                dcCANTIDAD.DefaultValue = 0
                dtConsumos.Columns.Add(dcCANTIDAD)

                Dim dcACTUAL As New DataColumn
                dcACTUAL.ColumnName = "CACTUAL"
                dcACTUAL.DefaultValue = 0
                dtConsumos.Columns.Add(dcACTUAL)

                Dim dcMINIMO As New DataColumn
                dcMINIMO.ColumnName = "CMINIMO"
                dcMINIMO.DefaultValue = 0
                dtConsumos.Columns.Add(dcMINIMO)


                Dim dcDIFERENCIA As New DataColumn
                dcDIFERENCIA.ColumnName = "CDIFERENCIA"
                dcDIFERENCIA.DefaultValue = 0
                dtConsumos.Columns.Add(dcDIFERENCIA)
                dgConsumos.SetDataBinding(dtConsumos, "")
                OcultarColumnasDG(dgConsumos)
                i = 0
                PPCol2("CHILO", dgConsumos, rm.GetString("HILO"), "", True, 150,
                        False, PresentationEnum.Normal, False, 150, i, False)
                i = i + 1
                PPCol2("CPROVEEDOR", dgConsumos, rm.GetString("PROVEEDOR"), "", True, 150,
                        False, PresentationEnum.Normal, False, 70, i, False)
                i = i + 1
                PPCol2("CCOLOR", dgConsumos, rm.GetString("COLOR"), "", True, 150,
                        False, PresentationEnum.Normal, False, 150, i, False)
                i = i + 1
                PPCol2("CCANTIDAD", dgConsumos, rm.GetString("CANTIDAD"), "#,##0.00", True, 70,
                    False, PresentationEnum.Normal, False, 70, i, False)
                i = i + 1
                PPCol2("CACTUAL", dgConsumos, rm.GetString("ACTUAL"), "#,##0.00", True, 70,
                        False, PresentationEnum.Normal, False, 70, i, False)
                i = i + 1
                PPCol2("CMINIMO", dgConsumos, rm.GetString("MINIMO"), "#,##0.00", True, 70,
                        False, PresentationEnum.Normal, False, 70, i, False)
                i = i + 1
                PPCol2("CDIFERENCIA", dgConsumos, rm.GetString("DIFERENCIA"), "", True, 70,
                        False, PresentationEnum.Normal, False, 70, i, False)




            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub
        Private Function CrearConsumos() As Object
            '! Changed CrearConsumos to Object
            'Para cada uno de las lineas del PAF (las muestras) hay que buscar
            'si para esa muestra, color, talla tiene algun hilo asociado
            'Si lo tiene por cada uno de los asociados mirar el consumo
            'Multiplicar ese consumo por la cantidad que tenemos de la muestra
            'Despues mirar de ese hilo / color / proveedor la cantidad que tenemos
            'Restar el consumo - actual

            Dim hilo As String
            Dim color As String
            Dim prove As String
            Dim diferencia As String
            Dim consumo As String
            Dim cmdSel As New MySqlCommand
            Dim strSQL As String
            Dim minim As Double
            Dim actual As Double
            Dim daRead2 As MySqlDataReader
            '! Dead variable: j
            Dim i, j As Integer
            Dim dr As DataRow

            Try

                cmdSel.Connection = cnn
                dtConsumos.Clear()

                For i = 0 To PAFActual.lineasVenta.dvForm.Count - 1
                    If CStr(PAFActual.lineasVenta.dvForm(i).Item("TIPUS")) = "M" Then
                        'En tabla colores miramos si esta la muestra / color / talla
                        'Por cada uno de los hilos que este aqui será una linea del consumo
                        'j = BuscarHilosConsumo(txtCodigoCliente.Text, dgLineasPAFVenta(i, "MOSTRA"), dgLineasPAFVenta(i, "TALLA"))
                        strSQL = CType("SELECT " & tablaColores & ".teixit, " &
                            " " & tablaColores & ".color, " &
                            " " & tablaColores & ".consum, " &
                            " " & tablaColores & ".prove, " &
                            " " & tablaHiloColores & ".actual, " &
                            " " & tablaHiloColores & ".minim FROM " &
                            " " & tablaColores & " " &
                            " LEFT JOIN " & tablaHiloColores & " ON " &
                            " (" & tablaHiloColores & ".FIL = " & tablaColores & ".teixit AND " &
                            " " & tablaHiloColores & ".PROVE = " & tablaColores & ".prove AND " &
                            " " & tablaHiloColores & ".COLOR = " & tablaColores & ".COLOR )" &
                            " WHERE MOSTRA = """ & dgLineasPAFVenta(i, "MOSTRA") & """ AND " &
                            " TALLA = """ & dgLineasPAFVenta(i, "TALLA") & """ AND " &
                            " TALLAL = """ & dgLineasPAFVenta(i, "TALLAL") & """ AND " &
                            " COLOR.COLORM = """ & dgLineasPAFVenta(i, "COLOR") & """ AND " &
                            " COLOR.CLIENT = """ & PAFActual.CLIENT & """ AND " &
                            " CLIENT = """ & txtCLIENT.Text & """ AND " &
                            " TIPUS = ""F"" ", String)

                        cmdSel.CommandText = strSQL
                        ACN()
                        daRead2 = cmdSel.ExecuteReader
                        While daRead2.Read
                            'En daRead.item(0) tengo el hilo y en (1) tengo el color
                            'Hay que por cada uno de los hilos obtener el color
                            'Despues si no está en el datagrid de los consumos añadirlo
                            'En caso de que este sumarlo a la cantidad
                            hilo = daRead2.Item("TEIXIT")
                            color = daRead2.Item("COLOR")
                            consumo = daRead2.Item("CONSUM")
                            prove = daRead2.Item("PROVE")
                            actual = daRead2.Item("ACTUAL")
                            minim = daRead2.Item("MINIM")

                            If posicionHiloDG(hilo, color) = -1 Then
                                'Quiere decir que todavia no hemos introducido este hilo/color
                                dr = dtConsumos.NewRow
                                dr("CHILO") = hilo
                                dr("CCOLOR") = color
                                dr("CACTUAL") = actual
                                dr("CMINIMO") = minim
                                dr("CCANTIDAD") = consumo * dgLineasPAFVenta(i, "UNITATS")
                                dr("CPROVEEDOR") = prove
                                diferencia = dr("CCANTIDAD") - dr("CACTUAL")
                                If diferencia > 0 Then dr("CDIFERENCIA") = diferencia
                                dtConsumos.Rows.Add(dr)
                                dgConsumos.Refresh()
                            Else
                                dgConsumos(posicionHiloDG(hilo, color), "CCANTIDAD") = dgConsumos(posicionHiloDG(hilo, color), "CCANTIDAD") + (dgLineasPAFVenta(i, "UNITATS") * consumo)
                                diferencia = dgConsumos(posicionHiloDG(hilo, color), "CCANTIDAD") - dgConsumos(posicionHiloDG(hilo, color), "CACTUAL")
                                If diferencia > 0 Then dgConsumos(posicionHiloDG(hilo, color), "CDIFERENCIA") = diferencia
                                'Ya lo hemos introducido solo sumar
                            End If
                        End While
                        daRead2.Close()
                        CCN()
                    End If
                Next

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN() : daRead2.Close()
            End Try
        End Function
        Private Function posicionHiloDG(ByVal hilo As String, ByVal color As String) As Object
            '! Changed posicionHiloDG to Object
            Try
                Dim i As Integer
                For i = 0 To NumeroFilas2(dgConsumos) - 1
                    'If NumeroFilas(dgConsumos) = 0 Then
                    If dgConsumos(i, 0) = hilo AndAlso dgConsumos(i, 2) = color Then
                        Return i
                    End If
                    'End If
                Next
                Return -1

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Function
        Private Function CrearPedido(ByVal prove As Integer) As Integer
            Dim cmdIns As MySqlCommand
            Dim fra As Integer
            Try
                'Crea un pedido de hilo TIPO = M y DOCUMENT = C y devuelve el numero de ese
                'pedido nuevo
                cmdIns = New MySqlCommand
                cmdIns.Connection = cnn

                fra = GetNumeroUltimoPAFCompra(Pedido, "M", PAFActual.centro) + 1
                cmdIns.CommandText = "INSERT INTO CACTUR (DOCUMENT, FRA, PROVE, NRO, DATA, BASE1, P_IVA1, IVA1, TOTAL, P_DTE, DTE1, BRUT1, TRASPAS, P_RE1, RE1, VENCIM, TRASPASF, OBSERV, CENTRO,OBSERVOCULTA) VALUES (""C"", " & fra & ", " & prove & ", 0, """ & ConvertirAfechaMysql(Date.Now) & """, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, """", 0, 0, 0, 0, 0, 0, """", """", """", """ & PAFActual.centro & ""","""")"
                ACN()
                cmdIns.ExecuteNonQuery()
                CCN()

                Return fra
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Function
        Private Sub btnGenerarPedidoCompraHilosDesdeMuestra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerarPedidoCompraHilosDesdeMuestra.Click
            Dim dv As DataView
            Dim prove As Integer = -1
            Dim anteriorprove As Integer = -1
            Dim nlinea As Integer = 1
            Dim i As Integer
            Dim fra As Integer
            Dim s As String
            Try
                'Hay que generar un pedido de compra de muestra por cada uno de las lineas, si alguna de las lineas tiene algun
                'proveedor igual se ponen en el mismo pedido
                s = "S'han creat les comandes de compra de fils: "

                dv = dtConsumos.DefaultView
                dv.Sort = "CPROVEEDOR"
                For i = 0 To dv.Count - 1
                    'Recorremos las lineas de la consumicion
                    prove = dv(i).Item("CPROVEEDOR")
                    If anteriorprove = prove Then
                        'nlinea += nlinea
                        PAFActual.PCompraHilo.lineasCompra.AñadirLinea(dgConsumos(i, "CHILO"), dgConsumos(i, "CCOLOR"), dgConsumos(i, "CCANTIDAD"))
                    Else
                        nlinea = 1
                        fra = PAFActual.CrearPediDo(prove, dgConsumos(i, "CHILO"), dgConsumos(i, "CCOLOR"), dgConsumos(i, "CCANTIDAD"))
                        s = s & " " & fra & ","
                        anteriorprove = prove
                    End If
                Next
                MessageBox.Show(s.Substring(0, s.Length - 1))
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub

#End Region

#Region "ESTOCS"

        Private Sub chkSumadoEstoc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSumadoEstoc.CheckedChanged
            Try
                PAFActual.ActualizarOrigen()
                ActualizarStockTejidos()

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub
        '********************************************************
        'Esto ahora no vale pq lo controlamos con los traspasos parciales
        '********************************************************
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
                For i = 0 To PAFActual.lineasVenta.dvForm.Count - 1
                    strSQL = "SELECT ACTUAL FROM " & tablaHiloColores & " WHERE " &
                                " (FIL = """ & general.NS(PAFActual.lineasVenta.dvForm(i).Item("MOSTRA")) & """ AND  " &
                                " TIPUS = ""T"" AND COLOR = """ & general.NS(PAFActual.lineasVenta.dvForm(i).Item("COLOR")) & """)"

                    cmdSel.CommandText = strSQL
                    If chkSumadoEstoc.Checked = False Then
                        stock = cmdSel.ExecuteScalar() + PAFActual.lineasVenta.dvForm.Item(i).Item("UNITATS")
                    Else
                        stock = cmdSel.ExecuteScalar() - PAFActual.lineasVenta.dvForm.Item(i).Item("UNITATS")
                    End If
                    strSQL = "UPDATE " & tablaHiloColores & " SET ACTUAL = """ & general.NS(stock, True) & """ WHERE (FIL = """ & general.NS(PAFActual.lineasVenta.dvForm(i).Item("MOSTRA")) & """ AND TIPUS = 'T' AND COLOR = """ & general.NS(PAFActual.lineasVenta.dvForm(i).Item("COLOR")) & """)"
                    cmdUpd.CommandText = strSQL
                    cmdUpd.ExecuteNonQuery()
                Next
                CCN()

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub

#End Region

#Region "TRASPASOS"

        Private Sub btnTraspasarAPAF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTraspasarAPAF.Click
            Try
                If Not cargando Then
                    Dim seTraspassa As Boolean
                    cargando = True
                    seTraspassa = RecalcularLieasStock()
                    If seTraspassa Then
                        PAFActual.Traspasar(nzn(txtAPAF.Text, 0), txtAFecha.Value, cmbPAF1.Text, cmbPAF2.Text,
                                        date1.Value, date2.Value, nzn(cmbCliente1.Text, 0), nzn(cmbCliente2.Text, 0), False)
                        'ActualizarLblTraspasado()

                        CargarListaFormaParte()

                        PSBTIPO(PAFActual.centro) : cargando = False
                    End If
                End If
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub

        Private Function RecalcularLieasStock() As Boolean
            Try
                'PAFActual
                Dim strSQL As String
                Dim stock As Double
                Dim cmdSel As New MySqlCommand
                Dim cmdUpd As New MySqlCommand
                Dim actualizarStock As Boolean = True
                Dim listaStocks As New LinkedList(Of Double)
                Dim insuficienteStock As String = rm.GetString("STOCKINSUFICIENTE") & vbLf
                Dim i As Integer = 0
                cmdSel.Connection = cnn
                cmdUpd.Connection = cnn

                ACN()

                For Each row As DataRow In PAFActual.lineasVenta.tabla.Rows
                    If general.NS(row("TIPUS")) = Tejido Then
                        strSQL = "SELECT ACTUAL FROM " & tablaHiloColores & " WHERE(CENTRO = """ & PAFActual.centro & """ AND FIL = """ & general.NS(row("MOSTRA")) & """ AND TIPUS = ""T"" AND COLOR = """ & general.NS(row("COLOR")) & """)"
                        cmdSel.CommandText = strSQL
                        stock = general.nz(cmdSel.ExecuteScalar(), 0) - Math.Abs(row("PERREBRE"))
                        listaStocks.AddLast(stock)
                        If stock < 0 Then
                            actualizarStock = False
                            insuficienteStock = insuficienteStock & "    - " & general.NS(row("MOSTRA")) & "  Color:  " & general.NS(row("COLOR")) & " ,es necesiten: " & Math.Abs(stock) & vbLf
                        End If
                    End If
                Next

                If Not actualizarStock Then
                    MessageBox.Show(insuficienteStock)
                    If MessageBox.Show(rm.GetString("CONTINUARIGUALMENTE"), "", MessageBoxButtons.YesNo) = DialogResult.No Then
                        Return False
                    End If
                End If

                strSQL = ""
                For Each row As DataRow In PAFActual.lineasVenta.tabla.Rows
                    If general.NS(row("TIPUS")) = Tejido Then
                        strSQL = strSQL & "UPDATE " & tablaHiloColores & " SET ACTUAL = " & general.NS(listaStocks(i), True) & " WHERE(CENTRO = """ & PAFActual.centro & """ AND FIL = """ & general.NS(row("MOSTRA")) & """ AND TIPUS = ""T"" AND COLOR = """ & general.NS(row("COLOR")) & """) ; "
                        i = i + 1
                    End If
                Next
                cmdUpd.CommandText = strSQL
                If strSQL <> "" Then
                    cmdUpd.ExecuteNonQuery()
                End If
                CCN()

                For i = 0 To PAFActual.lineasVenta.dvForm.Count - 1
                    PAFActual.lineasVenta.dvForm(i).Item("PERREBRE") = 0
                    PAFActual.lineasVenta.dvForm(i).Item("REBUT") = PAFActual.lineasVenta.dvForm(i).Item("UNITATS")
                Next
                PAFActual.ActualizarOrigen(True)

                Return True

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Function

        Private Sub btnTraspasarAProforma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTraspasarAProforma.Click
            Try
                If Not cargando Then
                    cargando = True
                    PAFActual.Traspasar(nzn(txtAPAF.Text, 0), txtAFecha.Value, nzn(cmbPAF1.Text, 0), nzn(cmbPAF2.Text, 0),
                                       date1.Value, date2.Value, nzn(cmbCliente1.Text, 0), nzn(cmbCliente2.Text, 0), True)
                    cargando = False

                End If
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub


#End Region

#Region "FACTURAS"

        Private Sub btnGenerarRecibos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerarRecibo.Click
            Try
                'ppfitxa2.Document=
                ppfitxa2.Document = PAFActual.ImprimirRecibos(nzn(cboDeFacuraRecibo.SelectedValue, 0),
                                        nzn(cboAFacturaRecibo.SelectedValue, 0),
                                        dtpDeFechaRecibo.Value,
                                        dtpAFechaRecibo.Value, chkCondicionFecha.Checked)
                'ppfitxa2.Document.Print() ' .Print(True, 1, ppFitxa.PageCount)

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub

#End Region

#End Region

#Region "IMPRESIONES"

#Region "IMPRIMIR PAF"

        Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
            Dim arrayDatosFactura As New ArrayList
            Try

                arrayDatosFactura.Add(general.nz(cboIncoterm.Text, ""))
                'Añadir la direccion evnvio
                Try
                    arrayDatosFactura.Add(general.nz(PAFActual.obtenerStringDireccionEnvio(), ""))

                Catch ex As Exception

                End Try
                'Las semanas de plazo de entrega
                arrayDatosFactura.Add(general.nz(cboSemanas.Text, ""))
                PAFActual.ActualizarOrigen()
                PAFActual.ImprimirPAF(cboDEPAFIMPRESION.Text, cboAPAFIMPRESION.Text, chkFacturaInternacional.Checked, arrayDatosFactura)

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub

#End Region

#End Region

#Region "COMUNES"

        Friend Sub btnRecargar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecargar.Click
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

#End Region

#Region "DESPLAZARSE"

        Private Sub MoverActual(Optional ByVal nuevo As Boolean = False)
            Try
                ACN()

                CargarListaFormaParte()
                If PAFActual.TRASPAS = True Then
                    gbTraspasarIntervalo.Visible = False
                Else
                    gbTraspasarIntervalo.Visible = True
                End If
                If Not PAFActual.documento = Factura Then
                    cmbCliente1.Text = PAFActual.CLIENT
                    cmbCliente2.Text = PAFActual.CLIENT
                    cmbPAF1.Text = PAFActual.FRA
                    cmbPAF2.Text = PAFActual.FRA
                    date1.Value = PAFActual.DATA
                    date2.Value = PAFActual.DATA
                    txtAFecha.Value = Date.Today
                    'txtAPAF.Text = 0
                    'CargarListaTraspaso()
                    'CargarNumeroPedidos()
                    'CargarNumeroClientes()
                Else
                    cboDeFacuraRecibo.Text = PAFActual.FRA
                    cboAFacturaRecibo.Text = PAFActual.FRA
                    dtpAFechaRecibo.Value = PAFActual.DATA
                    dtpDeFechaRecibo.Value = PAFActual.DATA
                End If
                cboDEPAFIMPRESION.Text = PAFActual.FRA
                cboAPAFIMPRESION.Text = PAFActual.FRA

                AutoSizeCC(dgLineasPAFVenta)

                AñadirBindingCombo(cboDOM, PAFActual.dtDireccionesEnvio, "DOM", "DOM")
                If PAFActual.DOM = "" Then
                    cboDOM.Text = ""
                End If
                'PAFActual.tabla.AcceptChanges()


                AñadirBindingCombo(cboMuestra, PAFActual.lineasVenta.dtTeixits.DefaultView, "", "CODI")

                AñadirBindingCombo(cboMuestra2, PAFActual.lineasVenta.dvMuestras, "", "CODI")
                cboMuestra2.Rebind()
                PSBTIPO(PAFActual.centro)
                CCN()


            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Try

                cargando = True
                PAFActual.SiguienteReg() : ActualizarOrigen()
                MoverActual()

                PSBTIPO(PAFActual.centro) : cargando = False

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub
        Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Try
                desplazando = True
                cargando = True
                PAFActual.UltimoReg() : ActualizarOrigen()
                MoverActual()
                PSBTIPO(PAFActual.centro) : cargando = False
                desplazando = False

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub
        Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
        Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            cargando = True
            desplazando = True
            PAFActual.PrimeroReg() : ActualizarOrigen()
            MoverActual()
            desplazando = False
            PSBTIPO(PAFActual.centro) : cargando = False
        End Sub

#End Region

#Region "SELECCION REGISTRO"

        Private Sub VerPAFDeCliente_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoVerPAFDeCliente.CheckedChanged
            Try
                'Hay que rellenar el combo donde podemos elegir al cliente
                If Not cargando And consulta() Then
                    cargando = True
                    If rdoVerPAFDeCliente.Checked = True Then
                        cboSeleccionClienteParaPAF.Visible = True
                        lblNumeroPAF.Visible = True
                        AñadirBindingCombo(cboSeleccionClienteParaPAF, PAFActual.dtClientConPAF, CCClients, CNClients)
                        cboSeleccionClienteParaPAF.Focus()
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
                        cboSeleccionClienteParaPAF.Visible = False
                        lblNumeroPAF.Visible = False
                    End If
                    PSBTIPO(PAFActual.centro) : cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub CambioEleccionTodosPAF()
            Try
                PAFActual.ElegirCliente(-1)
                PAFActual.numeroRegistros = PAFActual.obtenerNumeroReg(PAFActual.tabla.TableName, "")
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

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Private Sub rdoVerTodasPAF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoVerTodasPAF.CheckedChanged
            Try
                If Not cargando And consulta() Then
                    cargando = True
                    If rdoVerTodasPAF.Checked = True Then
                        CambioEleccionTodosPAF()
                    End If
                    cargando = False
                End If
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try

        End Sub
        Private Sub cboSeleccionClienteParaPAF_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboSeleccionClienteParaPAF.Validating
            Try
                If consulta() Then
                    cargando = True
                    PAFActual.ElegirCliente(nzn(cboSeleccionClienteParaPAF.WillChangeToValue, 0))
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
        Private Sub cboCodigoPAF_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboFRA.KeyDown
            Try
                If Not cargando Then
                    If editando Or EsRegistroNuevo Then : cboFRA.AutoCompletion = False
                    Else : cboFRA.AutoCompletion = True : End If
                End If

            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        End Sub

#End Region

#Region "ELECCIONES"

        Private Sub txtCodigoCliente_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCLIENT.Validated
            Try
                If PuedoModificar() Then
                    cargando = True
                    PAFActual.CLIENT = nzn(txtCLIENT.Text, 0)
                    PAFActual.PonerDatosCliente() : AñadirBindingCombo(cboDOM, PAFActual.dtDireccionesEnvio, "DOM", "DOM")
                    cboDOM.Text = ""
                    PSBTIPO(PAFActual.centro) : cargando = False
                End If
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub txtCodigoRepresentante2_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtREPRES.Validated
            Try
                If PuedoModificar() Then
                    cargando = True
                    PAFActual.REPRES = nzn(txtREPRES.Text, 0)
                    PAFActual.PonerDatosRepre()
                    cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub btnElegirRepresentate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirRepresentate.Click
            Try
                If PuedoModificar() Then
                    cargando = True
                    Dim f As frmRepresentantesLista = frmRepresentantesLista.GetInstance(esEleccion)
                    'f.Show()
                    f.ShowDialog()
                    If Not f.dr Is Nothing Then
                        PAFActual.REPRES = f.dr("CODI")
                        PAFActual.PonerDatosRepre()
                    End If
                    f = Nothing
                    PSBTIPO(PAFActual.centro) : cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub btnElegirCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirCliente.Click
            Try
                Cursor = Cursors.WaitCursor
                Dim f As frmClientesLista = frmClientesLista.GetInstance(esEleccion)
                f.ShowDialog()
                If Not f.dr Is Nothing Then
                    If Not PAFActual.ClienteBloqueado() Then
                        PAFActual.CLIENT = f.dr("CODI")
                    Else
                        MsgBox(rm.GetString("CLIENTEBLOQUEADO"))
                    End If
                    PAFActual.PonerDatosCliente() : AñadirBindingCombo(cboDOM, PAFActual.dtDireccionesEnvio, "DOM", "DOM")
                    cboDOM.Text = ""
                End If
                Cursor = Cursors.Default
                f = Nothing
                PSBTIPO(PAFActual.centro) : cargando = False

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub cboNombreCliente_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMCLIENT.SelectedValueChanged
            Try
                With cboNOMCLIENT
                    If PuedoModificar() Then
                        cargando = True
                        If Not PAFActual.ClienteBloqueado() Then
                            PAFActual.CLIENT = nzn(.WillChangeToValue, 0)
                        Else
                            MsgBox(rm.GetString("CLIENTEBLOQUEADO"))
                        End If
                        PAFActual.PonerDatosCliente() : AñadirBindingCombo(cboDOM, PAFActual.dtDireccionesEnvio, "DOM", "DOM")
                        PSBTIPO(PAFActual.centro) : cargando = False
                    End If
                End With

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub cboBANCO_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBANCO.SelectedValueChanged
            Try
                With cboBANCO
                    If PuedoModificar() Then
                        cargando = True
                        PAFActual.BANCO = nzn(.WillChangeToValue, 0)
                        PAFActual.PonerDatosCliente() : AñadirBindingCombo(cboDOM, PAFActual.dtDireccionesEnvio, "DOM", "DOM")
                        PSBTIPO(PAFActual.centro) : cargando = False
                    End If
                End With

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub cboNombreRepresentante_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMREPRES.SelectedValueChanged
            Try
                With cboNOMREPRES
                    If PuedoModificar() Then
                        cargando = True
                        PAFActual.REPRES = nzn(.WillChangeToValue, 0)
                        PAFActual.PonerDatosRepre()
                        PSBTIPO(PAFActual.centro) : cargando = False
                    End If
                End With

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub cboSeleccion_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
                                                   Handles cboSeleccionCentro.SelectionChangeCommitted
            Try
                If consulta() Then
                    cargando = True
                    PAFActual.cambioCentro(general.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto), ultimo)


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

                    cboSeleccionCentro.SelectedValue = PAFActual.centro
                    AñadirBindingCombo(cboFRA, PAFActual.dvIdentificadores, "FRA", "FRA")
                    PAFActual.tabla.AcceptChanges()
                    MoverActual()

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

#Region "ORGANIZAR"

        Private Sub RellenarListaDesdeArrayList(ByRef lstFormaParte As ListBox, ByVal ArrayFormaParte As ArrayList)
            Dim i As Integer
            Try
                lstFormaParte.Items.Clear()
                For i = 0 To ArrayFormaParte.Count - 1
                    If Not IsDBNull(ArrayFormaParte.Item(i)) Then
                        If Not ArrayFormaParte.Item(i) = 0 Then
                            lstFormaParte.Items.Add(ArrayFormaParte(i))
                        End If
                    End If
                Next
                lstFormaParte.Sorted = True
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub CargarNumeroPedidos()
            Dim i As Integer
            Try
                For i = 0 To PAFActual.dvTraspaso.Count - 1
                    If PAFActual.dvTraspaso(i).Item("TRASPAS") = "No" Then
                        cmbPAF1.AddItem(PAFActual.dvTraspaso(i).Item("FRA"))
                        cmbPAF2.AddItem(PAFActual.dvTraspaso(i).Item("FRA"))
                        cmbCliente1.AddItem(PAFActual.dvTraspaso(i).Item("CODICLIENT"))
                        cmbCliente2.AddItem(PAFActual.dvTraspaso(i).Item("CODICLIENT"))
                    End If
                Next

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub CargarNumeroClientes()
            Try

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub tabControlPAFVentas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabControlPAFVentas.SelectedIndexChanged
            Try
                If Not Me.DesignMode Then
                    If Not cargando And Not EsRegistroNuevo Then
                        If PAFActual.TieneCambios Then
                            Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                                Case DialogResult.Cancel
                                    cargando = False
                                Case DialogResult.No
                                    PAFActual.tabla.RejectChanges()
                                    PAFActual.lineasVenta.tabla.RejectChanges()
                                    If PAFActual.DOCUMENT = Factura Then PAFActual.vencimientos.tabla.RejectChanges()
                                    cargando = False
                                Case DialogResult.Yes
                                    PAFActual.ActualizarOrigen()
                            End Select
                        End If
                        If tabControlPAFVentas.SelectedTab Is tabPageConsumo Then
                            CrearConsumos()
                        End If
                        If tabControlPAFVentas.SelectedTab Is tabPageImpresionPAF Then
                            PAFActual.CargarImpresiones()
                            If PAFActual.detalleImpresion.dvForm.Count = 0 Then
                                PAFActual.InicializarImpresion()
                            End If
                            IniciarIncoterm()
                            IniciarDGImpresiones()
                        End If
                    End If
                End If
            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub IniciarIncoterm()
            Dim dar As MySqlDataReader
            Dim cmd As MySqlCommand
            Try
                'cboIncoterm.Text = DLookUp("INCOTERM", "CLIENTS", "CODI = " & PAFActual.CLIENT, False)
                If cboIncoterm.ListCount <> 0 Then Exit Sub

                'Borramos los elementos del cboIncoterm
                cmd = New MySqlCommand("SELECT NOMBRE FROM INCOTERM ", cnn)
                ACN()
                dar = cmd.ExecuteReader

                While dar.Read
                    'AñadirBindingCombo(cboTalla, PAFActual.lineasVenta.dtTalla, "TALLA", "TALLA")
                    cboIncoterm.AddItem(dar!NOMBRE)
                End While
                CCN()
                dar.Close()
                cboSemanas.AddItem("1")
                cboSemanas.AddItem("2")
                cboSemanas.AddItem("3")
                cboSemanas.AddItem("4")
                cboSemanas.AddItem("5")
                cboSemanas.AddItem("6")
                cboSemanas.AddItem("7")
                cboSemanas.AddItem("8")
                AutoSizeCC(cboIncoterm)
                AutoSizeCC(cboSemanas)

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Private Sub IniciarDGImpresiones()
            Dim i As Integer
            Try
                'TODO Es un calendario
                dgImpresion.SetDataBinding(PAFActual.detalleImpresion.dvForm, "")
                OcultarColumnasDG(dgImpresion)
                i = 0
                PPCol2("NOMBREDETALLE", dgImpresion, rm.GetString("CONCEPTO"), "", True,
                        150, False, PresentationEnum.Normal, False, 150, i, True)
                i = i + 1
                'PPCol2("DESCRIDETALLE", dgLineasPAFVenta, rm.GetString("VALOR"), "", True, 100, False, _
                '    PresentationEnum.ComboBox, False, 80, i, False, cboImpresion)
                PPCol2("DESCRIDETALLE", dgImpresion, rm.GetString("VALOR"), "", True,
                        100, False, PresentationEnum.Normal, False, 100, i, True)

                dgImpresion2.SetDataBinding(PAFActual.detalleImpresion2.dvForm, "")
                OcultarColumnasDG(dgImpresion2)
                i = 0
                PPCol2("TEXTO1", dgImpresion2, rm.GetString("TEXTO1"), "", True,
                        200, False, PresentationEnum.Normal, False, 200, i, True)

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Friend Overrides Sub btnModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                If Not editando Then
                    cargando = True
                    editando = True

                    CambioEleccionTodosPAF()
                    cboSeleccionClienteParaPAF.Visible = False
                    rdoVerPAFDeCliente.Checked = False
                    rdoVerTodasPAF.Checked = True

                    PonerModificables(modificable)
                    PSBTIPO(PAFActual.centro) : cargando = False
                End If

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False
            End Try
        End Sub
        Private Sub btnVerLista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerLista.Click
            Try
                Cursor = Cursors.WaitCursor
                Dim f As frmPAFLista = frmPAFLista.GetInstance(PAFActual.TIPUS, PAFActual.DOCUMENT, PAFActual.centro, tablaVentas)

                f.MdiParent = Me.MdiParent
                f.Show()
                f.BringToFront()
                Cursor = Cursors.Default

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
        End Sub
        Private Sub chkCondicionFecha_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCondicionFecha.CheckedChanged
            dtpDeFechaRecibo.Enabled = chkCondicionFecha.Checked
            dtpAFechaRecibo.Enabled = chkCondicionFecha.Checked
        End Sub
        Private Sub dgLineasPAFVenta_AfterDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgLineasPAFVenta.AfterDelete
            Try
                PAFActual.lineasVenta.HacerCalculosLineasPAF("PREU")

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Private Sub dgLineasPAFVenta_BeforeDelete(ByVal sender As System.Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgLineasPAFVenta.BeforeDelete
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
        Private Sub dgTraspaso_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgTraspas.BeforeDelete
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
        Private Sub dgConsumos_BeforeDelete(ByVal sender As System.Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgConsumos.BeforeDelete
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
        Private Sub dgVencim_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgVencim.BeforeDelete
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
        Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

        End Sub
        Private Sub frmVentaPAFPlantilla_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
            Try
                GrabaParametro(PAFActual.FRA, "" & PAFActual.DOCUMENT & PAFActual.TIPUS & "V" & PAFActual.centro & "")

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

#End Region

        Private Sub btnEliminarFilasSeleccionadas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarFilasSeleccionadas.Click
            Dim i As Integer
            If MessageBox.Show("Vols eliminar les linies sel·leccionades") = DialogResult.Yes Then
                For i = 0 To dgLineasPAFVenta.SelectedRows.Count - 1
                    dgLineasPAFVenta.Bookmark = dgLineasPAFVenta.SelectedRows(0)
                    dgLineasPAFVenta.Delete()
                Next i
            End If
        End Sub

    End Class

End Namespace

#Region "BORRAR"

'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHacerTraspaso.Click
'    Dim i As Integer
'    Try
'        If PuedoModificar() Then
'            cargando = True
'            'Recorregmos la lista de las paf seleccionadas
'            For i = 0 To dgTraspas.SelectedRows.Count - 1
'                If dgTraspas(i, "TRASPAS") = "Si" Then
'                    MessageBox.Show("Només es poden seleccionar documents no traspassats")
'                    Exit Sub
'                End If
'            Next
'            cargando = False
'        End If
'    Catch ex As Exception

'        If (ex.Message = "IPedidoNoValido") Then MessageBox.Show("FALLO Pedido")
'        If (ex.Message = "IFechaNoValido") Then MessageBox.Show("FALLO Fecha")
'        If (ex.Message = "IClienteNoValido") Then MessageBox.Show("FALLO Cliente")

'    End Try
'End Sub
'Private Sub dateDesdeFecha_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
'    'CargarListaTraspaso()
'End Sub
'Private Function obtenercodigocliente(ByVal nombrecliente As String) As Integer
'    Dim i As Integer
'    Dim cmdSel As New MySqlCommand
'    Try
'        cmdSel = New MySqlCommand("SELECT codi FROM clients WHERE (NOM = """ & ponercontrabarrasreal(nombrecliente) & """)", cnn)
'        ACN()
'        i = cmdSel.ExecuteScalar
'        CCN()
'        Return i
'    Catch ex As Exception
'        LOG(ex.ToString) : cargando = False : CCN()
'    End Try
'End Function
'If PAFActual.documento = Factura OrElse PAFActual.documento = "M" Then
'    tabControlPAFVentas.TabPages.Remove(tabPageTraspasoPAF)
'Else
'    If PAFActual.DOCUMENT = Factura Then
'        tabControlPAFVentas.TabPages.Remove(tabPageImpresionPAF)
'        tabControlPAFVentas.TabPages.Remove(tabPageVencimientos)
'    End If
'End If
'If PAFActual.documento <> "C" Then
'    tabControlPAFVentas.TabPages.Remove(tabPageConsumo)
'End If
'************************************************************
'Cargamos los numeros de los PAF
'TODO: Meterlo dentro de la clase y llamarla.
'************************************************************
'Private Sub CargarNumerosPAF(Optional ByVal IDcliente As Integer = -1)
'    Dim dt1 As DataTable
'    Dim dc1 As DataColumn
'    Dim dr As DataRow
'    Dim dv As DataView
'    Dim c As MySqlCommand
'    Dim d As MySqlDataReader
'    Try
'        dc1 = New DataColumn("FRA")
'        dt1 = New DataTable

'        dt1.Columns.Add(dc1)

'        c = New MySqlCommand("SELECT FRA  FROM FACTUR  WHERE  DOCUMENT = """ & PAFActual.documento & """ AND  CENTRO  = """ &general.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto) & """", cnn)

'        If Not IDcliente = -1 Then
'            c.CommandText = c.CommandText & "   AND CLIENT = """ & IDcliente & """"
'        End If


'        If cnn.State = ConnectionState.Closed Then cnn.Open()
'        d = c.ExecuteReader
'        While d.Read
'            dr = dt1.NewRow
'            dr("FRA") = d("FRA")
'            dt1.Rows.Add(dr)
'        End While

'        d.Close()
'        dv = dt1.DefaultView
'        Select Case PAFActual.DOCUMENT
'            Case "C"
'                lblNumeroPAF.Text = rm.GetString("NUMPEDIDOS") & " " & dv.Count
'            Case "A"
'                lblNumeroPAF.Text = rm.GetString("NUMALBARANES") & " " & dv.Count
'            Case "F"
'                lblNumeroPAF.Text = rm.GetString("NUMFACTURAS") & " " & dv.Count
'            Case "P"
'                lblNumeroPAF.Text = rm.GetString("NUMPROFORMA") & " " & dv.Count
'        End Select

'    Catch ex As Exception
'        LOG(ex.ToString) : cargando = False : CCN()
'    End Try
'End Sub

#End Region