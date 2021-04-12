Imports C1.Win.C1TrueDBGrid
Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class frmOrdenesFabricacion
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
        '! Seal for less overhead - Can declare NotInheritable Class
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
       MyBase.Dispose(disposing)
        frmChildForm = Nothing
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.

    Friend WithEvents lblComision As Label
    Friend WithEvents lblPrecio As Label
    Friend WithEvents btnElegirRepresentante As C1.Win.C1Input.C1Button
    Friend WithEvents lblRepresentante As Label
    Friend WithEvents btnElegirCliente As C1.Win.C1Input.C1Button
    Friend WithEvents lblCliente As Label
    Friend WithEvents lblPedido As Label
    Friend WithEvents lblSerie As Label
    Friend WithEvents lblModelo As Label
    Friend WithEvents lblExpedicion As Label
    Friend WithEvents lblOrden As Label
    Friend WithEvents lblFechaFinal As Label
    Friend WithEvents lblFechaPrevista As Label
    Friend WithEvents lblFechaEntrada As Label
    Friend WithEvents btnImprimir As C1.Win.C1Input.C1Button
    Friend WithEvents cboID As C1.Win.C1List.C1Combo
    Friend WithEvents lblTemporda As Label
    Friend WithEvents tabProcesosOrden As System.Windows.Forms.TabPage
    Friend WithEvents lblExpedicionP As Label
    Friend WithEvents lblSerieP As Label
    Friend WithEvents gpModeloT As System.Windows.Forms.GroupBox
    Friend WithEvents tabPageOrden As System.Windows.Forms.TabPage
    Friend WithEvents tabPageAAlbaran As System.Windows.Forms.TabPage
    Friend WithEvents txtAAlbaran As C1.Win.C1Input.C1TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnTraspasarAAlbaran As C1.Win.C1Input.C1Button
    Friend WithEvents tabPageConsumos As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblObsercaciones As Label
    Friend WithEvents lblFechaPrevistaT As Label
    Friend WithEvents lblFechaEntradaT As Label
    Friend WithEvents lblClienteT As Label
    Friend WithEvents lblSerieT As Label
    Friend WithEvents lblModeloT As Label
    Friend WithEvents lblExpedicionT As Label
    Friend WithEvents lblOrdenT As Label
    Friend WithEvents lblFechaPrevistaP As Label
    Friend WithEvents lblFechaEntradaP As Label
    Friend WithEvents lblClienteP As Label
    Friend WithEvents lblModeloP As Label
    Friend WithEvents lblOrdenP As Label
    Friend WithEvents lblFechaPrevistaC As Label
    Friend WithEvents lblFechaEntradaC As Label
    Friend WithEvents lblClienteC As Label
    Friend WithEvents lblSerieC As Label
    Friend WithEvents lblModeloC As Label
    Friend WithEvents lblExpedicionC As Label
    Friend WithEvents lblCodigoOrdenC As Label
    Friend WithEvents gbProcesos As System.Windows.Forms.GroupBox
    Friend WithEvents dgOrdenes As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents dgAnulaciones As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents dgProcesos As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents dgConsumosModelo As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents cboColores As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents cboTallas As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents tabControlOrdenes As System.Windows.Forms.TabControl
    Friend WithEvents btnElegirModelo As C1.Win.C1Input.C1Button
    Friend WithEvents cboTalleres As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents cboOperaciones As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents tabPageTaras As System.Windows.Forms.TabPage
    Friend WithEvents txtTALLA10 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA09 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA08 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA07 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA06 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA05 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA04 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA03 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA02 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA01 As C1.Win.C1Input.C1TextBox
    Friend WithEvents dtpENTRADA As C1.Win.C1Input.C1DateEdit
    Friend WithEvents dtpFINAL As C1.Win.C1Input.C1DateEdit
    Friend WithEvents txtOBSERV As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCOMIS As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtPREU As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboNOMREPRES As C1.Win.C1List.C1Combo
    Friend WithEvents txtREPRES As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboNOMCLIENT As C1.Win.C1List.C1Combo
    Friend WithEvents txtCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtEXPEDICIO As C1.Win.C1Input.C1TextBox
    Friend WithEvents dtpPREVISTA As C1.Win.C1Input.C1DateEdit
    Friend WithEvents txtALBARA As C1.Win.C1Input.C1TextBox
    Friend WithEvents dt2ENTRADA As C1.Win.C1Input.C1DateEdit
    Friend WithEvents dt2PREVISTA As C1.Win.C1Input.C1DateEdit
    Friend WithEvents tx2NOMCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx2CLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx2MODEL As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx2EXPEDICIO As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx2ORDRE As C1.Win.C1Input.C1TextBox
    Friend WithEvents dt3ENTRADA As C1.Win.C1Input.C1DateEdit
    Friend WithEvents dt3PREVISTA As C1.Win.C1Input.C1DateEdit
    Friend WithEvents tx3NOMCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx3CLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx3SERIE As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx3MODEL As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx3EXPEDICIO As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx3ORDRE As C1.Win.C1Input.C1TextBox
    Friend WithEvents dt4ENTRADA As C1.Win.C1Input.C1DateEdit
    Friend WithEvents dt4PREVISTA As C1.Win.C1Input.C1DateEdit
    Friend WithEvents tx4NOMCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx4CLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx4ORDRE As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx4SERIE As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx4MODEL As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx4EXPEDICIO As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtMODEL As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtSERIE As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTEMPORADA As C1.Win.C1Input.C1TextBox
    Friend WithEvents gbEleccionClientesPAF As System.Windows.Forms.GroupBox
    Friend WithEvents lblNumeroPAF As System.Windows.Forms.Label
    Friend WithEvents cboSeleccionClienteParaPAF As C1.Win.C1List.C1Combo
    Friend WithEvents rdoVerTodasPAF As System.Windows.Forms.RadioButton
    Friend WithEvents rdoVerPAFDeCliente As System.Windows.Forms.RadioButton
    Friend WithEvents gbCentro As System.Windows.Forms.GroupBox
    Friend WithEvents lblNumeracion As System.Windows.Forms.Label
    Friend WithEvents tx2SERIE As C1.Win.C1Input.C1TextBox
    Friend WithEvents dtpAAlbaran As C1.Win.C1Input.C1DateEdit
    Friend WithEvents chkALB As System.Windows.Forms.CheckBox
    Friend WithEvents lstOrdenes As C1.Win.C1List.C1List
    Friend WithEvents btnGenerarConsumos As C1.Win.C1Input.C1Button
    Friend WithEvents btnGenerarComandesCompra As C1.Win.C1Input.C1Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboDeOrden As C1.Win.C1List.C1Combo
    Friend WithEvents cboAOrden As C1.Win.C1List.C1Combo

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Style1 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style2 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style3 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style4 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style5 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style6 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOrdenesFabricacion))
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
        Me.tabControlOrdenes = New System.Windows.Forms.TabControl()
        Me.tabPageOrden = New System.Windows.Forms.TabPage()
        Me.chkALB = New System.Windows.Forms.CheckBox()
        Me.txtTEMPORADA = New C1.Win.C1Input.C1TextBox()
        Me.txtSERIE = New C1.Win.C1Input.C1TextBox()
        Me.txtMODEL = New C1.Win.C1Input.C1TextBox()
        Me.dtpPREVISTA = New C1.Win.C1Input.C1DateEdit()
        Me.btnElegirModelo = New C1.Win.C1Input.C1Button()
        Me.dtpENTRADA = New C1.Win.C1Input.C1DateEdit()
        Me.dtpFINAL = New C1.Win.C1Input.C1DateEdit()
        Me.cboColores = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.txtTALLA10 = New C1.Win.C1Input.C1TextBox()
        Me.txtTALLA09 = New C1.Win.C1Input.C1TextBox()
        Me.txtTALLA08 = New C1.Win.C1Input.C1TextBox()
        Me.txtTALLA07 = New C1.Win.C1Input.C1TextBox()
        Me.txtTALLA06 = New C1.Win.C1Input.C1TextBox()
        Me.txtTALLA05 = New C1.Win.C1Input.C1TextBox()
        Me.txtTALLA04 = New C1.Win.C1Input.C1TextBox()
        Me.txtTALLA03 = New C1.Win.C1Input.C1TextBox()
        Me.txtTALLA02 = New C1.Win.C1Input.C1TextBox()
        Me.txtTALLA01 = New C1.Win.C1Input.C1TextBox()
        Me.dgOrdenes = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.lblObsercaciones = New System.Windows.Forms.Label()
        Me.txtALBARA = New C1.Win.C1Input.C1TextBox()
        Me.lblTemporda = New System.Windows.Forms.Label()
        Me.txtOBSERV = New C1.Win.C1Input.C1TextBox()
        Me.cboID = New C1.Win.C1List.C1Combo()
        Me.lblComision = New System.Windows.Forms.Label()
        Me.txtCOMIS = New C1.Win.C1Input.C1TextBox()
        Me.lblPrecio = New System.Windows.Forms.Label()
        Me.txtPREU = New C1.Win.C1Input.C1TextBox()
        Me.cboNOMREPRES = New C1.Win.C1List.C1Combo()
        Me.btnElegirRepresentante = New C1.Win.C1Input.C1Button()
        Me.lblRepresentante = New System.Windows.Forms.Label()
        Me.txtREPRES = New C1.Win.C1Input.C1TextBox()
        Me.cboNOMCLIENT = New C1.Win.C1List.C1Combo()
        Me.btnElegirCliente = New C1.Win.C1Input.C1Button()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.txtCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.lblPedido = New System.Windows.Forms.Label()
        Me.lblSerie = New System.Windows.Forms.Label()
        Me.lblModelo = New System.Windows.Forms.Label()
        Me.lblExpedicion = New System.Windows.Forms.Label()
        Me.lblOrden = New System.Windows.Forms.Label()
        Me.lblFechaFinal = New System.Windows.Forms.Label()
        Me.lblFechaPrevista = New System.Windows.Forms.Label()
        Me.lblFechaEntrada = New System.Windows.Forms.Label()
        Me.txtEXPEDICIO = New C1.Win.C1Input.C1TextBox()
        Me.tabPageAAlbaran = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dtpAAlbaran = New C1.Win.C1Input.C1DateEdit()
        Me.btnTraspasarAAlbaran = New C1.Win.C1Input.C1Button()
        Me.txtAAlbaran = New C1.Win.C1Input.C1TextBox()
        Me.tabProcesosOrden = New System.Windows.Forms.TabPage()
        Me.cboOperaciones = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.cboTalleres = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.dgProcesos = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.gbProcesos = New System.Windows.Forms.GroupBox()
        Me.dt3ENTRADA = New C1.Win.C1Input.C1DateEdit()
        Me.dt3PREVISTA = New C1.Win.C1Input.C1DateEdit()
        Me.tx3NOMCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.tx3CLIENT = New C1.Win.C1Input.C1TextBox()
        Me.lblClienteP = New System.Windows.Forms.Label()
        Me.tx3SERIE = New C1.Win.C1Input.C1TextBox()
        Me.lblSerieP = New System.Windows.Forms.Label()
        Me.tx3MODEL = New C1.Win.C1Input.C1TextBox()
        Me.lblModeloP = New System.Windows.Forms.Label()
        Me.tx3EXPEDICIO = New C1.Win.C1Input.C1TextBox()
        Me.lblOrdenP = New System.Windows.Forms.Label()
        Me.lblExpedicionP = New System.Windows.Forms.Label()
        Me.tx3ORDRE = New C1.Win.C1Input.C1TextBox()
        Me.lblFechaPrevistaP = New System.Windows.Forms.Label()
        Me.lblFechaEntradaP = New System.Windows.Forms.Label()
        Me.tabPageTaras = New System.Windows.Forms.TabPage()
        Me.cboTallas = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.dgAnulaciones = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.gpModeloT = New System.Windows.Forms.GroupBox()
        Me.dt2ENTRADA = New C1.Win.C1Input.C1DateEdit()
        Me.dt2PREVISTA = New C1.Win.C1Input.C1DateEdit()
        Me.lblFechaPrevistaT = New System.Windows.Forms.Label()
        Me.lblFechaEntradaT = New System.Windows.Forms.Label()
        Me.tx2NOMCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.tx2CLIENT = New C1.Win.C1Input.C1TextBox()
        Me.lblClienteT = New System.Windows.Forms.Label()
        Me.tx2SERIE = New C1.Win.C1Input.C1TextBox()
        Me.lblSerieT = New System.Windows.Forms.Label()
        Me.tx2MODEL = New C1.Win.C1Input.C1TextBox()
        Me.lblModeloT = New System.Windows.Forms.Label()
        Me.tx2EXPEDICIO = New C1.Win.C1Input.C1TextBox()
        Me.lblExpedicionT = New System.Windows.Forms.Label()
        Me.tx2ORDRE = New C1.Win.C1Input.C1TextBox()
        Me.lblOrdenT = New System.Windows.Forms.Label()
        Me.tabPageConsumos = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboAOrden = New C1.Win.C1List.C1Combo()
        Me.cboDeOrden = New C1.Win.C1List.C1Combo()
        Me.btnGenerarComandesCompra = New C1.Win.C1Input.C1Button()
        Me.btnGenerarConsumos = New C1.Win.C1Input.C1Button()
        Me.lstOrdenes = New C1.Win.C1List.C1List()
        Me.dgConsumosModelo = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dt4ENTRADA = New C1.Win.C1Input.C1DateEdit()
        Me.dt4PREVISTA = New C1.Win.C1Input.C1DateEdit()
        Me.lblFechaPrevistaC = New System.Windows.Forms.Label()
        Me.lblFechaEntradaC = New System.Windows.Forms.Label()
        Me.tx4NOMCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.tx4CLIENT = New C1.Win.C1Input.C1TextBox()
        Me.lblClienteC = New System.Windows.Forms.Label()
        Me.tx4ORDRE = New C1.Win.C1Input.C1TextBox()
        Me.lblSerieC = New System.Windows.Forms.Label()
        Me.tx4SERIE = New C1.Win.C1Input.C1TextBox()
        Me.lblModeloC = New System.Windows.Forms.Label()
        Me.tx4MODEL = New C1.Win.C1Input.C1TextBox()
        Me.lblExpedicionC = New System.Windows.Forms.Label()
        Me.tx4EXPEDICIO = New C1.Win.C1Input.C1TextBox()
        Me.lblCodigoOrdenC = New System.Windows.Forms.Label()
        Me.btnImprimir = New C1.Win.C1Input.C1Button()
        Me.gbEleccionClientesPAF = New System.Windows.Forms.GroupBox()
        Me.lblNumeroPAF = New System.Windows.Forms.Label()
        Me.cboSeleccionClienteParaPAF = New C1.Win.C1List.C1Combo()
        Me.rdoVerTodasPAF = New System.Windows.Forms.RadioButton()
        Me.rdoVerPAFDeCliente = New System.Windows.Forms.RadioButton()
        Me.gbCentro = New System.Windows.Forms.GroupBox()
        Me.lblNumeracion = New System.Windows.Forms.Label()
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
        Me.tabControlOrdenes.SuspendLayout
        Me.tabPageOrden.SuspendLayout
        CType(Me.txtTEMPORADA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtSERIE,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtMODEL,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dtpPREVISTA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnElegirModelo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dtpENTRADA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dtpFINAL,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboColores,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtTALLA10,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtTALLA09,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtTALLA08,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtTALLA07,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtTALLA06,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtTALLA05,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtTALLA04,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtTALLA03,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtTALLA02,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtTALLA01,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dgOrdenes,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtALBARA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtOBSERV,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboID,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtCOMIS,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtPREU,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboNOMREPRES,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnElegirRepresentante,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtREPRES,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboNOMCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnElegirCliente,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtEXPEDICIO,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tabPageAAlbaran.SuspendLayout
        Me.GroupBox1.SuspendLayout
        CType(Me.dtpAAlbaran,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnTraspasarAAlbaran,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtAAlbaran,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tabProcesosOrden.SuspendLayout
        CType(Me.cboOperaciones,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboTalleres,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dgProcesos,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbProcesos.SuspendLayout
        CType(Me.dt3ENTRADA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dt3PREVISTA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx3NOMCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx3CLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx3SERIE,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx3MODEL,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx3EXPEDICIO,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx3ORDRE,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tabPageTaras.SuspendLayout
        CType(Me.cboTallas,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dgAnulaciones,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gpModeloT.SuspendLayout
        CType(Me.dt2ENTRADA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dt2PREVISTA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx2NOMCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx2CLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx2SERIE,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx2MODEL,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx2EXPEDICIO,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx2ORDRE,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tabPageConsumos.SuspendLayout
        CType(Me.cboAOrden,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboDeOrden,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnGenerarComandesCompra,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnGenerarConsumos,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.lstOrdenes,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dgConsumosModelo,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox2.SuspendLayout
        CType(Me.dt4ENTRADA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dt4PREVISTA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx4NOMCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx4CLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx4ORDRE,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx4SERIE,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx4MODEL,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tx4EXPEDICIO,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnImprimir,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbEleccionClientesPAF.SuspendLayout
        CType(Me.cboSeleccionClienteParaPAF,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbCentro.SuspendLayout
        Me.SuspendLayout
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.sbtipo.Location = New System.Drawing.Point(7, 22)
        Me.sbtipo.Size = New System.Drawing.Size(123, 20)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(49, -6)
        Me.btnRecargar.Size = New System.Drawing.Size(140, 28)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(444, -6)
        Me.btnSiguiente.Size = New System.Drawing.Size(45, 28)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(4, -6)
        Me.btnAnterior.Size = New System.Drawing.Size(45, 28)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(4, -35)
        Me.btnPrimero.Size = New System.Drawing.Size(45, 29)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(444, -35)
        Me.btnUltimo.Size = New System.Drawing.Size(45, 29)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(189, -35)
        Me.btnModificar.Size = New System.Drawing.Size(140, 29)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(982, -6)
        Me.btnTancar.Size = New System.Drawing.Size(112, 28)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(189, -6)
        Me.btnBorrar.Size = New System.Drawing.Size(140, 28)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(329, -35)
        Me.btnNuevo.Size = New System.Drawing.Size(115, 57)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(49, -35)
        Me.btnActualizar.Size = New System.Drawing.Size(140, 29)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(982, -35)
        Me.btnVerLista.Size = New System.Drawing.Size(112, 29)
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.ItemHeight = 17
        Me.cboSeleccionCentro.Location = New System.Drawing.Point(876, 7)
        Me.cboSeleccionCentro.Size = New System.Drawing.Size(218, 25)
        '
        'tabControlOrdenes
        '
        Me.tabControlOrdenes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.tabControlOrdenes.Controls.Add(Me.tabPageOrden)
        Me.tabControlOrdenes.Controls.Add(Me.tabPageAAlbaran)
        Me.tabControlOrdenes.Controls.Add(Me.tabProcesosOrden)
        Me.tabControlOrdenes.Controls.Add(Me.tabPageTaras)
        Me.tabControlOrdenes.Controls.Add(Me.tabPageConsumos)
        Me.tabControlOrdenes.ItemSize = New System.Drawing.Size(42, 18)
        Me.tabControlOrdenes.Location = New System.Drawing.Point(6, 70)
        Me.tabControlOrdenes.Name = "tabControlOrdenes"
        Me.tabControlOrdenes.SelectedIndex = 0
        Me.tabControlOrdenes.Size = New System.Drawing.Size(1141, 531)
        Me.tabControlOrdenes.TabIndex = 0
        '
        'tabPageOrden
        '
        Me.tabPageOrden.Controls.Add(Me.chkALB)
        Me.tabPageOrden.Controls.Add(Me.txtTEMPORADA)
        Me.tabPageOrden.Controls.Add(Me.txtSERIE)
        Me.tabPageOrden.Controls.Add(Me.txtMODEL)
        Me.tabPageOrden.Controls.Add(Me.dtpPREVISTA)
        Me.tabPageOrden.Controls.Add(Me.btnElegirModelo)
        Me.tabPageOrden.Controls.Add(Me.dtpENTRADA)
        Me.tabPageOrden.Controls.Add(Me.dtpFINAL)
        Me.tabPageOrden.Controls.Add(Me.cboColores)
        Me.tabPageOrden.Controls.Add(Me.txtTALLA10)
        Me.tabPageOrden.Controls.Add(Me.txtTALLA09)
        Me.tabPageOrden.Controls.Add(Me.txtTALLA08)
        Me.tabPageOrden.Controls.Add(Me.txtTALLA07)
        Me.tabPageOrden.Controls.Add(Me.txtTALLA06)
        Me.tabPageOrden.Controls.Add(Me.txtTALLA05)
        Me.tabPageOrden.Controls.Add(Me.txtTALLA04)
        Me.tabPageOrden.Controls.Add(Me.txtTALLA03)
        Me.tabPageOrden.Controls.Add(Me.txtTALLA02)
        Me.tabPageOrden.Controls.Add(Me.txtTALLA01)
        Me.tabPageOrden.Controls.Add(Me.dgOrdenes)
        Me.tabPageOrden.Controls.Add(Me.lblObsercaciones)
        Me.tabPageOrden.Controls.Add(Me.txtALBARA)
        Me.tabPageOrden.Controls.Add(Me.lblTemporda)
        Me.tabPageOrden.Controls.Add(Me.txtOBSERV)
        Me.tabPageOrden.Controls.Add(Me.cboID)
        Me.tabPageOrden.Controls.Add(Me.lblComision)
        Me.tabPageOrden.Controls.Add(Me.txtCOMIS)
        Me.tabPageOrden.Controls.Add(Me.lblPrecio)
        Me.tabPageOrden.Controls.Add(Me.txtPREU)
        Me.tabPageOrden.Controls.Add(Me.cboNOMREPRES)
        Me.tabPageOrden.Controls.Add(Me.btnElegirRepresentante)
        Me.tabPageOrden.Controls.Add(Me.lblRepresentante)
        Me.tabPageOrden.Controls.Add(Me.txtREPRES)
        Me.tabPageOrden.Controls.Add(Me.cboNOMCLIENT)
        Me.tabPageOrden.Controls.Add(Me.btnElegirCliente)
        Me.tabPageOrden.Controls.Add(Me.lblCliente)
        Me.tabPageOrden.Controls.Add(Me.txtCLIENT)
        Me.tabPageOrden.Controls.Add(Me.lblPedido)
        Me.tabPageOrden.Controls.Add(Me.lblSerie)
        Me.tabPageOrden.Controls.Add(Me.lblModelo)
        Me.tabPageOrden.Controls.Add(Me.lblExpedicion)
        Me.tabPageOrden.Controls.Add(Me.lblOrden)
        Me.tabPageOrden.Controls.Add(Me.lblFechaFinal)
        Me.tabPageOrden.Controls.Add(Me.lblFechaPrevista)
        Me.tabPageOrden.Controls.Add(Me.lblFechaEntrada)
        Me.tabPageOrden.Controls.Add(Me.txtEXPEDICIO)
        Me.tabPageOrden.Location = New System.Drawing.Point(4, 22)
        Me.tabPageOrden.Name = "tabPageOrden"
        Me.tabPageOrden.Size = New System.Drawing.Size(1133, 505)
        Me.tabPageOrden.TabIndex = 0
        Me.tabPageOrden.Text = "Ordre"
        '
        'chkALB
        '
        Me.chkALB.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkALB.Location = New System.Drawing.Point(1033, 2)
        Me.chkALB.Name = "chkALB"
        Me.chkALB.Size = New System.Drawing.Size(289, 30)
        Me.chkALB.TabIndex = 253
        Me.chkALB.Text = "Traspassat"
        '
        'txtTEMPORADA
        '
        Me.txtTEMPORADA.AutoSize = false
        Me.txtTEMPORADA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtTEMPORADA.Location = New System.Drawing.Point(123, 130)
        Me.txtTEMPORADA.MaxLength = 10
        Me.txtTEMPORADA.Name = "txtTEMPORADA"
        Me.txtTEMPORADA.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTEMPORADA.Size = New System.Drawing.Size(163, 25)
        Me.txtTEMPORADA.TabIndex = 252
        Me.txtTEMPORADA.Tag = Nothing
        Me.txtTEMPORADA.WordWrap = false
        '
        'txtSERIE
        '
        Me.txtSERIE.AutoSize = false
        Me.txtSERIE.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtSERIE.Location = New System.Drawing.Point(123, 98)
        Me.txtSERIE.MaxLength = 10
        Me.txtSERIE.Name = "txtSERIE"
        Me.txtSERIE.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSERIE.Size = New System.Drawing.Size(163, 25)
        Me.txtSERIE.TabIndex = 251
        Me.txtSERIE.Tag = Nothing
        Me.txtSERIE.WordWrap = false
        '
        'txtMODEL
        '
        Me.txtMODEL.AutoSize = false
        Me.txtMODEL.Location = New System.Drawing.Point(123, 69)
        Me.txtMODEL.Name = "txtMODEL"
        Me.txtMODEL.Size = New System.Drawing.Size(163, 25)
        Me.txtMODEL.TabIndex = 254
        Me.txtMODEL.Tag = Nothing
        '
        'dtpPREVISTA
        '
        '
        '
        '
        Me.dtpPREVISTA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpPREVISTA.Culture = 3082
        Me.dtpPREVISTA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpPREVISTA.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dtpPREVISTA.Location = New System.Drawing.Point(557, 103)
        Me.dtpPREVISTA.Name = "dtpPREVISTA"
        Me.dtpPREVISTA.Size = New System.Drawing.Size(196, 21)
        Me.dtpPREVISTA.TabIndex = 249
        Me.dtpPREVISTA.Tag = Nothing
        '
        'btnElegirModelo
        '
        Me.btnElegirModelo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirModelo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirModelo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirModelo.Location = New System.Drawing.Point(291, 69)
        Me.btnElegirModelo.Name = "btnElegirModelo"
        Me.btnElegirModelo.Size = New System.Drawing.Size(90, 89)
        Me.btnElegirModelo.TabIndex = 248
        Me.btnElegirModelo.Text = "Escollir Model"
        Me.btnElegirModelo.UseVisualStyleBackColor = true
        '
        'dtpENTRADA
        '
        '
        '
        '
        Me.dtpENTRADA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpENTRADA.Culture = 3082
        Me.dtpENTRADA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpENTRADA.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dtpENTRADA.Location = New System.Drawing.Point(557, 71)
        Me.dtpENTRADA.Name = "dtpENTRADA"
        Me.dtpENTRADA.Size = New System.Drawing.Size(196, 21)
        Me.dtpENTRADA.TabIndex = 247
        Me.dtpENTRADA.Tag = Nothing
        '
        'dtpFINAL
        '
        '
        '
        '
        Me.dtpFINAL.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpFINAL.Culture = 3082
        Me.dtpFINAL.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpFINAL.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dtpFINAL.Location = New System.Drawing.Point(557, 133)
        Me.dtpFINAL.Name = "dtpFINAL"
        Me.dtpFINAL.Size = New System.Drawing.Size(196, 21)
        Me.dtpFINAL.TabIndex = 246
        Me.dtpFINAL.Tag = Nothing
        '
        'cboColores
        '
        Me.cboColores.AllowColMove = true
        Me.cboColores.AllowColSelect = true
        Me.cboColores.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboColores.AlternatingRows = false
        Me.cboColores.CaptionHeight = 17
        Me.cboColores.CaptionStyle = Style1
        Me.cboColores.ColumnCaptionHeight = 17
        Me.cboColores.ColumnFooterHeight = 17
        Me.cboColores.ColumnSelectorStyle = Style2
        Me.cboColores.EvenRowStyle = Style3
        Me.cboColores.FetchRowStyles = false
        Me.cboColores.FooterStyle = Style4
        Me.cboColores.HeadingStyle = Style5
        Me.cboColores.HighLightRowStyle = Style6
        Me.cboColores.Images.Add(CType(resources.GetObject("cboColores.Images"),System.Drawing.Image))
        Me.cboColores.Location = New System.Drawing.Point(90, 352)
        Me.cboColores.Name = "cboColores"
        Me.cboColores.OddRowStyle = Style7
        Me.cboColores.RecordSelectorStyle = Style8
        Me.cboColores.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboColores.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboColores.RowHeight = 15
        Me.cboColores.RowSelectorStyle = Style9
        Me.cboColores.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboColores.ScrollTips = false
        Me.cboColores.Size = New System.Drawing.Size(72, 160)
        Me.cboColores.Style = Style10
        Me.cboColores.TabIndex = 244
        Me.cboColores.Text = "C1TrueDBDropdown1"
        Me.cboColores.UseCompatibleTextRendering = false
        Me.cboColores.ValueTranslate = true
        Me.cboColores.Visible = false
        Me.cboColores.PropBag = resources.GetString("cboColores.PropBag")
        '
        'txtTALLA10
        '
        Me.txtTALLA10.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA10.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA10.Location = New System.Drawing.Point(785, 315)
        Me.txtTALLA10.MaxLength = 3
        Me.txtTALLA10.Name = "txtTALLA10"
        Me.txtTALLA10.Size = New System.Drawing.Size(49, 19)
        Me.txtTALLA10.TabIndex = 236
        Me.txtTALLA10.Tag = Nothing
        '
        'txtTALLA09
        '
        Me.txtTALLA09.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA09.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA09.Location = New System.Drawing.Point(722, 315)
        Me.txtTALLA09.MaxLength = 3
        Me.txtTALLA09.Name = "txtTALLA09"
        Me.txtTALLA09.Size = New System.Drawing.Size(47, 19)
        Me.txtTALLA09.TabIndex = 235
        Me.txtTALLA09.Tag = Nothing
        '
        'txtTALLA08
        '
        Me.txtTALLA08.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA08.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA08.Location = New System.Drawing.Point(660, 315)
        Me.txtTALLA08.MaxLength = 3
        Me.txtTALLA08.Name = "txtTALLA08"
        Me.txtTALLA08.Size = New System.Drawing.Size(46, 19)
        Me.txtTALLA08.TabIndex = 234
        Me.txtTALLA08.Tag = Nothing
        '
        'txtTALLA07
        '
        Me.txtTALLA07.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA07.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA07.Location = New System.Drawing.Point(598, 315)
        Me.txtTALLA07.MaxLength = 3
        Me.txtTALLA07.Name = "txtTALLA07"
        Me.txtTALLA07.Size = New System.Drawing.Size(43, 19)
        Me.txtTALLA07.TabIndex = 233
        Me.txtTALLA07.Tag = Nothing
        '
        'txtTALLA06
        '
        Me.txtTALLA06.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA06.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA06.Location = New System.Drawing.Point(537, 315)
        Me.txtTALLA06.MaxLength = 3
        Me.txtTALLA06.Name = "txtTALLA06"
        Me.txtTALLA06.Size = New System.Drawing.Size(43, 19)
        Me.txtTALLA06.TabIndex = 232
        Me.txtTALLA06.Tag = Nothing
        '
        'txtTALLA05
        '
        Me.txtTALLA05.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA05.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA05.Location = New System.Drawing.Point(470, 315)
        Me.txtTALLA05.MaxLength = 3
        Me.txtTALLA05.Name = "txtTALLA05"
        Me.txtTALLA05.Size = New System.Drawing.Size(46, 19)
        Me.txtTALLA05.TabIndex = 231
        Me.txtTALLA05.Tag = Nothing
        '
        'txtTALLA04
        '
        Me.txtTALLA04.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA04.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA04.Location = New System.Drawing.Point(411, 315)
        Me.txtTALLA04.MaxLength = 3
        Me.txtTALLA04.Name = "txtTALLA04"
        Me.txtTALLA04.Size = New System.Drawing.Size(43, 19)
        Me.txtTALLA04.TabIndex = 230
        Me.txtTALLA04.Tag = Nothing
        '
        'txtTALLA03
        '
        Me.txtTALLA03.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA03.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA03.Location = New System.Drawing.Point(346, 315)
        Me.txtTALLA03.MaxLength = 3
        Me.txtTALLA03.Name = "txtTALLA03"
        Me.txtTALLA03.Size = New System.Drawing.Size(49, 19)
        Me.txtTALLA03.TabIndex = 229
        Me.txtTALLA03.Tag = Nothing
        '
        'txtTALLA02
        '
        Me.txtTALLA02.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA02.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA02.Location = New System.Drawing.Point(284, 315)
        Me.txtTALLA02.MaxLength = 3
        Me.txtTALLA02.Name = "txtTALLA02"
        Me.txtTALLA02.Size = New System.Drawing.Size(46, 19)
        Me.txtTALLA02.TabIndex = 228
        Me.txtTALLA02.Tag = Nothing
        '
        'txtTALLA01
        '
        Me.txtTALLA01.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA01.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA01.Location = New System.Drawing.Point(222, 315)
        Me.txtTALLA01.MaxLength = 3
        Me.txtTALLA01.Name = "txtTALLA01"
        Me.txtTALLA01.Size = New System.Drawing.Size(46, 19)
        Me.txtTALLA01.TabIndex = 227
        Me.txtTALLA01.Tag = Nothing
        '
        'dgOrdenes
        '
        Me.dgOrdenes.AllowAddNew = true
        Me.dgOrdenes.AllowDelete = true
        Me.dgOrdenes.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dgOrdenes.CaptionHeight = 17
        Me.dgOrdenes.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgOrdenes.Images.Add(CType(resources.GetObject("dgOrdenes.Images"),System.Drawing.Image))
        Me.dgOrdenes.Location = New System.Drawing.Point(-1, 315)
        Me.dgOrdenes.Name = "dgOrdenes"
        Me.dgOrdenes.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgOrdenes.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgOrdenes.PreviewInfo.ZoomFactor = 75R
        Me.dgOrdenes.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgOrdenes.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgOrdenes.RowHeight = 15
        Me.dgOrdenes.Size = New System.Drawing.Size(1127, 187)
        Me.dgOrdenes.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgOrdenes.TabIndex = 243
        Me.dgOrdenes.Text = "C1TrueDBGrid1"
        Me.dgOrdenes.UseCompatibleTextRendering = false
        Me.dgOrdenes.WrapCellPointer = true
        Me.dgOrdenes.PropBag = resources.GetString("dgOrdenes.PropBag")
        '
        'lblObsercaciones
        '
        Me.lblObsercaciones.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblObsercaciones.Location = New System.Drawing.Point(759, 5)
        Me.lblObsercaciones.Name = "lblObsercaciones"
        Me.lblObsercaciones.Size = New System.Drawing.Size(269, 25)
        Me.lblObsercaciones.TabIndex = 242
        Me.lblObsercaciones.Text = "Observacions"
        Me.lblObsercaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtALBARA
        '
        Me.txtALBARA.AutoSize = false
        Me.txtALBARA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtALBARA.Location = New System.Drawing.Point(123, 251)
        Me.txtALBARA.MaxLength = 10
        Me.txtALBARA.Name = "txtALBARA"
        Me.txtALBARA.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtALBARA.Size = New System.Drawing.Size(163, 25)
        Me.txtALBARA.TabIndex = 8
        Me.txtALBARA.Tag = Nothing
        Me.txtALBARA.WordWrap = false
        '
        'lblTemporda
        '
        Me.lblTemporda.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTemporda.Location = New System.Drawing.Point(3, 130)
        Me.lblTemporda.Name = "lblTemporda"
        Me.lblTemporda.Size = New System.Drawing.Size(117, 25)
        Me.lblTemporda.TabIndex = 240
        Me.lblTemporda.Text = "Temporada"
        Me.lblTemporda.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOBSERV
        '
        Me.txtOBSERV.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtOBSERV.Location = New System.Drawing.Point(759, 34)
        Me.txtOBSERV.Multiline = true
        Me.txtOBSERV.Name = "txtOBSERV"
        Me.txtOBSERV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOBSERV.Size = New System.Drawing.Size(351, 266)
        Me.txtOBSERV.TabIndex = 15
        Me.txtOBSERV.Tag = Nothing
        '
        'cboID
        '
        Me.cboID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboID.Caption = ""
        Me.cboID.CaptionHeight = 17
        Me.cboID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboID.ColumnCaptionHeight = 17
        Me.cboID.ColumnFooterHeight = 17
        Me.cboID.ContentHeight = 18
        Me.cboID.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboID.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboID.Images.Add(CType(resources.GetObject("cboID.Images"),System.Drawing.Image))
        Me.cboID.IntegralHeight = true
        Me.cboID.Location = New System.Drawing.Point(123, 9)
        Me.cboID.MatchEntryTimeout = CType(100,Long)
        Me.cboID.MaxDropDownItems = CType(15,Short)
        Me.cboID.MaxLength = 0
        Me.cboID.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboID.Name = "cboID"
        Me.cboID.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboID.Size = New System.Drawing.Size(255, 21)
        Me.cboID.TabIndex = 0
        Me.cboID.PropBag = resources.GetString("cboID.PropBag")
        '
        'lblComision
        '
        Me.lblComision.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblComision.Location = New System.Drawing.Point(3, 222)
        Me.lblComision.Name = "lblComision"
        Me.lblComision.Size = New System.Drawing.Size(117, 24)
        Me.lblComision.TabIndex = 57
        Me.lblComision.Text = "% Comissió"
        Me.lblComision.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCOMIS
        '
        Me.txtCOMIS.Location = New System.Drawing.Point(123, 222)
        Me.txtCOMIS.Name = "txtCOMIS"
        Me.txtCOMIS.Size = New System.Drawing.Size(67, 21)
        Me.txtCOMIS.TabIndex = 14
        Me.txtCOMIS.Tag = Nothing
        '
        'lblPrecio
        '
        Me.lblPrecio.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPrecio.Location = New System.Drawing.Point(308, 251)
        Me.lblPrecio.Name = "lblPrecio"
        Me.lblPrecio.Size = New System.Drawing.Size(129, 25)
        Me.lblPrecio.TabIndex = 55
        Me.lblPrecio.Text = "Preu"
        Me.lblPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPREU
        '
        Me.txtPREU.Location = New System.Drawing.Point(440, 251)
        Me.txtPREU.Name = "txtPREU"
        Me.txtPREU.Size = New System.Drawing.Size(67, 21)
        Me.txtPREU.TabIndex = 9
        Me.txtPREU.Tag = Nothing
        '
        'cboNOMREPRES
        '
        Me.cboNOMREPRES.AutoSelect = true
        Me.cboNOMREPRES.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMREPRES.Caption = ""
        Me.cboNOMREPRES.CaptionHeight = 17
        Me.cboNOMREPRES.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMREPRES.ColumnCaptionHeight = 17
        Me.cboNOMREPRES.ColumnFooterHeight = 17
        Me.cboNOMREPRES.ContentHeight = 18
        Me.cboNOMREPRES.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMREPRES.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMREPRES.Images.Add(CType(resources.GetObject("cboNOMREPRES.Images"),System.Drawing.Image))
        Me.cboNOMREPRES.IntegralHeight = true
        Me.cboNOMREPRES.Location = New System.Drawing.Point(230, 192)
        Me.cboNOMREPRES.MatchEntryTimeout = CType(100,Long)
        Me.cboNOMREPRES.MaxDropDownItems = CType(8,Short)
        Me.cboNOMREPRES.MaxLength = 255
        Me.cboNOMREPRES.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMREPRES.Name = "cboNOMREPRES"
        Me.cboNOMREPRES.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMREPRES.Size = New System.Drawing.Size(515, 21)
        Me.cboNOMREPRES.TabIndex = 13
        Me.cboNOMREPRES.PropBag = resources.GetString("cboNOMREPRES.PropBag")
        '
        'btnElegirRepresentante
        '
        Me.btnElegirRepresentante.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirRepresentante.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirRepresentante.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirRepresentante.Location = New System.Drawing.Point(190, 192)
        Me.btnElegirRepresentante.Name = "btnElegirRepresentante"
        Me.btnElegirRepresentante.Size = New System.Drawing.Size(34, 25)
        Me.btnElegirRepresentante.TabIndex = 52
        Me.btnElegirRepresentante.Text = "..."
        Me.btnElegirRepresentante.UseVisualStyleBackColor = true
        '
        'lblRepresentante
        '
        Me.lblRepresentante.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRepresentante.Location = New System.Drawing.Point(3, 192)
        Me.lblRepresentante.Name = "lblRepresentante"
        Me.lblRepresentante.Size = New System.Drawing.Size(117, 25)
        Me.lblRepresentante.TabIndex = 51
        Me.lblRepresentante.Text = "Representant"
        Me.lblRepresentante.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtREPRES
        '
        Me.txtREPRES.Location = New System.Drawing.Point(123, 192)
        Me.txtREPRES.Name = "txtREPRES"
        Me.txtREPRES.Size = New System.Drawing.Size(67, 21)
        Me.txtREPRES.TabIndex = 12
        Me.txtREPRES.Tag = Nothing
        '
        'cboNOMCLIENT
        '
        Me.cboNOMCLIENT.AutoSelect = true
        Me.cboNOMCLIENT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMCLIENT.Caption = ""
        Me.cboNOMCLIENT.CaptionHeight = 17
        Me.cboNOMCLIENT.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMCLIENT.ColumnCaptionHeight = 17
        Me.cboNOMCLIENT.ColumnFooterHeight = 17
        Me.cboNOMCLIENT.ContentHeight = 18
        Me.cboNOMCLIENT.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMCLIENT.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMCLIENT.Images.Add(CType(resources.GetObject("cboNOMCLIENT.Images"),System.Drawing.Image))
        Me.cboNOMCLIENT.IntegralHeight = true
        Me.cboNOMCLIENT.Location = New System.Drawing.Point(232, 39)
        Me.cboNOMCLIENT.MatchEntryTimeout = CType(100,Long)
        Me.cboNOMCLIENT.MaxDropDownItems = CType(8,Short)
        Me.cboNOMCLIENT.MaxLength = 0
        Me.cboNOMCLIENT.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMCLIENT.Name = "cboNOMCLIENT"
        Me.cboNOMCLIENT.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMCLIENT.Size = New System.Drawing.Size(516, 21)
        Me.cboNOMCLIENT.TabIndex = 11
        Me.cboNOMCLIENT.PropBag = resources.GetString("cboNOMCLIENT.PropBag")
        '
        'btnElegirCliente
        '
        Me.btnElegirCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirCliente.Location = New System.Drawing.Point(193, 39)
        Me.btnElegirCliente.Name = "btnElegirCliente"
        Me.btnElegirCliente.Size = New System.Drawing.Size(34, 25)
        Me.btnElegirCliente.TabIndex = 48
        Me.btnElegirCliente.Text = "..."
        Me.btnElegirCliente.UseVisualStyleBackColor = true
        '
        'lblCliente
        '
        Me.lblCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCliente.Location = New System.Drawing.Point(3, 39)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(117, 25)
        Me.lblCliente.TabIndex = 47
        Me.lblCliente.Text = "Client"
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCLIENT
        '
        Me.txtCLIENT.Location = New System.Drawing.Point(123, 39)
        Me.txtCLIENT.Name = "txtCLIENT"
        Me.txtCLIENT.Size = New System.Drawing.Size(67, 21)
        Me.txtCLIENT.TabIndex = 10
        Me.txtCLIENT.Tag = Nothing
        '
        'lblPedido
        '
        Me.lblPedido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPedido.Location = New System.Drawing.Point(3, 251)
        Me.lblPedido.Name = "lblPedido"
        Me.lblPedido.Size = New System.Drawing.Size(117, 25)
        Me.lblPedido.TabIndex = 45
        Me.lblPedido.Text = "Comanda"
        Me.lblPedido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSerie
        '
        Me.lblSerie.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblSerie.Location = New System.Drawing.Point(3, 98)
        Me.lblSerie.Name = "lblSerie"
        Me.lblSerie.Size = New System.Drawing.Size(117, 25)
        Me.lblSerie.TabIndex = 43
        Me.lblSerie.Text = "Serie"
        Me.lblSerie.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblModelo
        '
        Me.lblModelo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblModelo.Location = New System.Drawing.Point(3, 69)
        Me.lblModelo.Name = "lblModelo"
        Me.lblModelo.Size = New System.Drawing.Size(117, 25)
        Me.lblModelo.TabIndex = 41
        Me.lblModelo.Text = "Model"
        Me.lblModelo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblExpedicion
        '
        Me.lblExpedicion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblExpedicion.Location = New System.Drawing.Point(6, 162)
        Me.lblExpedicion.Name = "lblExpedicion"
        Me.lblExpedicion.Size = New System.Drawing.Size(114, 25)
        Me.lblExpedicion.TabIndex = 39
        Me.lblExpedicion.Text = "Expedició"
        Me.lblExpedicion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblOrden
        '
        Me.lblOrden.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblOrden.Location = New System.Drawing.Point(3, 9)
        Me.lblOrden.Name = "lblOrden"
        Me.lblOrden.Size = New System.Drawing.Size(117, 24)
        Me.lblOrden.TabIndex = 38
        Me.lblOrden.Text = "Ordre"
        Me.lblOrden.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFechaFinal
        '
        Me.lblFechaFinal.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFechaFinal.Location = New System.Drawing.Point(412, 133)
        Me.lblFechaFinal.Name = "lblFechaFinal"
        Me.lblFechaFinal.Size = New System.Drawing.Size(128, 25)
        Me.lblFechaFinal.TabIndex = 37
        Me.lblFechaFinal.Text = "Data Final"
        Me.lblFechaFinal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFechaPrevista
        '
        Me.lblFechaPrevista.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFechaPrevista.Location = New System.Drawing.Point(412, 103)
        Me.lblFechaPrevista.Name = "lblFechaPrevista"
        Me.lblFechaPrevista.Size = New System.Drawing.Size(128, 25)
        Me.lblFechaPrevista.TabIndex = 36
        Me.lblFechaPrevista.Text = "Data Prevista"
        Me.lblFechaPrevista.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFechaEntrada
        '
        Me.lblFechaEntrada.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFechaEntrada.Location = New System.Drawing.Point(412, 71)
        Me.lblFechaEntrada.Name = "lblFechaEntrada"
        Me.lblFechaEntrada.Size = New System.Drawing.Size(128, 25)
        Me.lblFechaEntrada.TabIndex = 35
        Me.lblFechaEntrada.Text = "Data Entrada"
        Me.lblFechaEntrada.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtEXPEDICIO
        '
        Me.txtEXPEDICIO.Location = New System.Drawing.Point(123, 162)
        Me.txtEXPEDICIO.Name = "txtEXPEDICIO"
        Me.txtEXPEDICIO.Size = New System.Drawing.Size(140, 21)
        Me.txtEXPEDICIO.TabIndex = 7
        Me.txtEXPEDICIO.Tag = Nothing
        '
        'tabPageAAlbaran
        '
        Me.tabPageAAlbaran.Controls.Add(Me.GroupBox1)
        Me.tabPageAAlbaran.Location = New System.Drawing.Point(4, 22)
        Me.tabPageAAlbaran.Name = "tabPageAAlbaran"
        Me.tabPageAAlbaran.Size = New System.Drawing.Size(1133, 505)
        Me.tabPageAAlbaran.TabIndex = 3
        Me.tabPageAAlbaran.Text = "Traspàs a Albarà"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dtpAAlbaran)
        Me.GroupBox1.Controls.Add(Me.btnTraspasarAAlbaran)
        Me.GroupBox1.Controls.Add(Me.txtAAlbaran)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(370, 118)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = false
        '
        'dtpAAlbaran
        '
        '
        '
        '
        Me.dtpAAlbaran.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpAAlbaran.Culture = 3082
        Me.dtpAAlbaran.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpAAlbaran.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dtpAAlbaran.Location = New System.Drawing.Point(157, 34)
        Me.dtpAAlbaran.Name = "dtpAAlbaran"
        Me.dtpAAlbaran.Size = New System.Drawing.Size(196, 21)
        Me.dtpAAlbaran.TabIndex = 246
        Me.dtpAAlbaran.Tag = Nothing
        '
        'btnTraspasarAAlbaran
        '
        Me.btnTraspasarAAlbaran.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTraspasarAAlbaran.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTraspasarAAlbaran.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnTraspasarAAlbaran.Location = New System.Drawing.Point(207, 74)
        Me.btnTraspasarAAlbaran.Name = "btnTraspasarAAlbaran"
        Me.btnTraspasarAAlbaran.Size = New System.Drawing.Size(143, 28)
        Me.btnTraspasarAAlbaran.TabIndex = 2
        Me.btnTraspasarAAlbaran.Text = "Traspasar Albarà"
        Me.btnTraspasarAAlbaran.UseVisualStyleBackColor = true
        '
        'txtAAlbaran
        '
        Me.txtAAlbaran.Location = New System.Drawing.Point(17, 34)
        Me.txtAAlbaran.Name = "txtAAlbaran"
        Me.txtAAlbaran.Size = New System.Drawing.Size(129, 21)
        Me.txtAAlbaran.TabIndex = 0
        Me.txtAAlbaran.Tag = Nothing
        '
        'tabProcesosOrden
        '
        Me.tabProcesosOrden.Controls.Add(Me.cboOperaciones)
        Me.tabProcesosOrden.Controls.Add(Me.cboTalleres)
        Me.tabProcesosOrden.Controls.Add(Me.dgProcesos)
        Me.tabProcesosOrden.Controls.Add(Me.gbProcesos)
        Me.tabProcesosOrden.Location = New System.Drawing.Point(4, 22)
        Me.tabProcesosOrden.Name = "tabProcesosOrden"
        Me.tabProcesosOrden.Size = New System.Drawing.Size(1133, 505)
        Me.tabProcesosOrden.TabIndex = 1
        Me.tabProcesosOrden.Text = "Processos Ordre"
        '
        'cboOperaciones
        '
        Me.cboOperaciones.AllowColMove = true
        Me.cboOperaciones.AllowColSelect = true
        Me.cboOperaciones.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboOperaciones.AlternatingRows = false
        Me.cboOperaciones.CaptionHeight = 17
        Me.cboOperaciones.CaptionStyle = Style11
        Me.cboOperaciones.ColumnCaptionHeight = 17
        Me.cboOperaciones.ColumnFooterHeight = 17
        Me.cboOperaciones.ColumnSelectorStyle = Style12
        Me.cboOperaciones.EvenRowStyle = Style13
        Me.cboOperaciones.FetchRowStyles = false
        Me.cboOperaciones.FooterStyle = Style14
        Me.cboOperaciones.HeadingStyle = Style15
        Me.cboOperaciones.HighLightRowStyle = Style16
        Me.cboOperaciones.Images.Add(CType(resources.GetObject("cboOperaciones.Images"),System.Drawing.Image))
        Me.cboOperaciones.Location = New System.Drawing.Point(179, 241)
        Me.cboOperaciones.Name = "cboOperaciones"
        Me.cboOperaciones.OddRowStyle = Style17
        Me.cboOperaciones.RecordSelectorStyle = Style18
        Me.cboOperaciones.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboOperaciones.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboOperaciones.RowHeight = 15
        Me.cboOperaciones.RowSelectorStyle = Style19
        Me.cboOperaciones.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboOperaciones.ScrollTips = false
        Me.cboOperaciones.Size = New System.Drawing.Size(112, 163)
        Me.cboOperaciones.Style = Style20
        Me.cboOperaciones.TabIndex = 64
        Me.cboOperaciones.Text = "C1TrueDBDropdown1"
        Me.cboOperaciones.UseCompatibleTextRendering = false
        Me.cboOperaciones.ValueTranslate = true
        Me.cboOperaciones.Visible = false
        Me.cboOperaciones.PropBag = resources.GetString("cboOperaciones.PropBag")
        '
        'cboTalleres
        '
        Me.cboTalleres.AllowColMove = true
        Me.cboTalleres.AllowColSelect = true
        Me.cboTalleres.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboTalleres.AlternatingRows = false
        Me.cboTalleres.CaptionHeight = 17
        Me.cboTalleres.CaptionStyle = Style21
        Me.cboTalleres.ColumnCaptionHeight = 17
        Me.cboTalleres.ColumnFooterHeight = 17
        Me.cboTalleres.ColumnSelectorStyle = Style22
        Me.cboTalleres.EvenRowStyle = Style23
        Me.cboTalleres.FetchRowStyles = false
        Me.cboTalleres.FooterStyle = Style24
        Me.cboTalleres.HeadingStyle = Style25
        Me.cboTalleres.HighLightRowStyle = Style26
        Me.cboTalleres.Images.Add(CType(resources.GetObject("cboTalleres.Images"),System.Drawing.Image))
        Me.cboTalleres.Location = New System.Drawing.Point(50, 241)
        Me.cboTalleres.Name = "cboTalleres"
        Me.cboTalleres.OddRowStyle = Style27
        Me.cboTalleres.RecordSelectorStyle = Style28
        Me.cboTalleres.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboTalleres.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboTalleres.RowHeight = 15
        Me.cboTalleres.RowSelectorStyle = Style29
        Me.cboTalleres.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboTalleres.ScrollTips = false
        Me.cboTalleres.Size = New System.Drawing.Size(112, 163)
        Me.cboTalleres.Style = Style30
        Me.cboTalleres.TabIndex = 63
        Me.cboTalleres.Text = "C1TrueDBDropdown1"
        Me.cboTalleres.UseCompatibleTextRendering = false
        Me.cboTalleres.ValueTranslate = true
        Me.cboTalleres.Visible = false
        Me.cboTalleres.PropBag = resources.GetString("cboTalleres.PropBag")
        '
        'dgProcesos
        '
        Me.dgProcesos.AllowAddNew = true
        Me.dgProcesos.AllowDelete = true
        Me.dgProcesos.CaptionHeight = 17
        Me.dgProcesos.ExtendRightColumn = true
        Me.dgProcesos.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgProcesos.Images.Add(CType(resources.GetObject("dgProcesos.Images"),System.Drawing.Image))
        Me.dgProcesos.Location = New System.Drawing.Point(39, 222)
        Me.dgProcesos.Name = "dgProcesos"
        Me.dgProcesos.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgProcesos.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgProcesos.PreviewInfo.ZoomFactor = 75R
        Me.dgProcesos.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgProcesos.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgProcesos.RowHeight = 15
        Me.dgProcesos.Size = New System.Drawing.Size(1249, 315)
        Me.dgProcesos.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgProcesos.TabIndex = 62
        Me.dgProcesos.Text = "C1TrueDBGrid1"
        Me.dgProcesos.UseCompatibleTextRendering = false
        Me.dgProcesos.WrapCellPointer = true
        Me.dgProcesos.PropBag = resources.GetString("dgProcesos.PropBag")
        '
        'gbProcesos
        '
        Me.gbProcesos.Controls.Add(Me.dt3ENTRADA)
        Me.gbProcesos.Controls.Add(Me.dt3PREVISTA)
        Me.gbProcesos.Controls.Add(Me.tx3NOMCLIENT)
        Me.gbProcesos.Controls.Add(Me.tx3CLIENT)
        Me.gbProcesos.Controls.Add(Me.lblClienteP)
        Me.gbProcesos.Controls.Add(Me.tx3SERIE)
        Me.gbProcesos.Controls.Add(Me.lblSerieP)
        Me.gbProcesos.Controls.Add(Me.tx3MODEL)
        Me.gbProcesos.Controls.Add(Me.lblModeloP)
        Me.gbProcesos.Controls.Add(Me.tx3EXPEDICIO)
        Me.gbProcesos.Controls.Add(Me.lblOrdenP)
        Me.gbProcesos.Controls.Add(Me.lblExpedicionP)
        Me.gbProcesos.Controls.Add(Me.tx3ORDRE)
        Me.gbProcesos.Controls.Add(Me.lblFechaPrevistaP)
        Me.gbProcesos.Controls.Add(Me.lblFechaEntradaP)
        Me.gbProcesos.Location = New System.Drawing.Point(11, 10)
        Me.gbProcesos.Name = "gbProcesos"
        Me.gbProcesos.Size = New System.Drawing.Size(678, 182)
        Me.gbProcesos.TabIndex = 60
        Me.gbProcesos.TabStop = false
        '
        'dt3ENTRADA
        '
        '
        '
        '
        Me.dt3ENTRADA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dt3ENTRADA.Culture = 3082
        Me.dt3ENTRADA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dt3ENTRADA.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dt3ENTRADA.Location = New System.Drawing.Point(482, 23)
        Me.dt3ENTRADA.Name = "dt3ENTRADA"
        Me.dt3ENTRADA.Size = New System.Drawing.Size(190, 21)
        Me.dt3ENTRADA.TabIndex = 253
        Me.dt3ENTRADA.Tag = Nothing
        '
        'dt3PREVISTA
        '
        '
        '
        '
        Me.dt3PREVISTA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dt3PREVISTA.Culture = 3082
        Me.dt3PREVISTA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dt3PREVISTA.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dt3PREVISTA.Location = New System.Drawing.Point(482, 54)
        Me.dt3PREVISTA.Name = "dt3PREVISTA"
        Me.dt3PREVISTA.Size = New System.Drawing.Size(190, 21)
        Me.dt3PREVISTA.TabIndex = 252
        Me.dt3PREVISTA.Tag = Nothing
        '
        'tx3NOMCLIENT
        '
        Me.tx3NOMCLIENT.Location = New System.Drawing.Point(224, 143)
        Me.tx3NOMCLIENT.Name = "tx3NOMCLIENT"
        Me.tx3NOMCLIENT.Size = New System.Drawing.Size(442, 21)
        Me.tx3NOMCLIENT.TabIndex = 5
        Me.tx3NOMCLIENT.Tag = Nothing
        '
        'tx3CLIENT
        '
        Me.tx3CLIENT.Location = New System.Drawing.Point(151, 143)
        Me.tx3CLIENT.Name = "tx3CLIENT"
        Me.tx3CLIENT.Size = New System.Drawing.Size(62, 21)
        Me.tx3CLIENT.TabIndex = 4
        Me.tx3CLIENT.Tag = Nothing
        '
        'lblClienteP
        '
        Me.lblClienteP.Enabled = false
        Me.lblClienteP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblClienteP.Location = New System.Drawing.Point(11, 148)
        Me.lblClienteP.Name = "lblClienteP"
        Me.lblClienteP.Size = New System.Drawing.Size(129, 19)
        Me.lblClienteP.TabIndex = 53
        Me.lblClienteP.Text = "Client"
        Me.lblClienteP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx3SERIE
        '
        Me.tx3SERIE.Location = New System.Drawing.Point(151, 113)
        Me.tx3SERIE.Name = "tx3SERIE"
        Me.tx3SERIE.Size = New System.Drawing.Size(163, 21)
        Me.tx3SERIE.TabIndex = 3
        Me.tx3SERIE.Tag = Nothing
        '
        'lblSerieP
        '
        Me.lblSerieP.Enabled = false
        Me.lblSerieP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblSerieP.Location = New System.Drawing.Point(11, 118)
        Me.lblSerieP.Name = "lblSerieP"
        Me.lblSerieP.Size = New System.Drawing.Size(129, 20)
        Me.lblSerieP.TabIndex = 51
        Me.lblSerieP.Text = "Serie"
        Me.lblSerieP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx3MODEL
        '
        Me.tx3MODEL.Location = New System.Drawing.Point(151, 84)
        Me.tx3MODEL.Name = "tx3MODEL"
        Me.tx3MODEL.Size = New System.Drawing.Size(163, 21)
        Me.tx3MODEL.TabIndex = 2
        Me.tx3MODEL.Tag = Nothing
        '
        'lblModeloP
        '
        Me.lblModeloP.Enabled = false
        Me.lblModeloP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblModeloP.Location = New System.Drawing.Point(11, 89)
        Me.lblModeloP.Name = "lblModeloP"
        Me.lblModeloP.Size = New System.Drawing.Size(129, 19)
        Me.lblModeloP.TabIndex = 49
        Me.lblModeloP.Text = "Model"
        Me.lblModeloP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx3EXPEDICIO
        '
        Me.tx3EXPEDICIO.Location = New System.Drawing.Point(151, 54)
        Me.tx3EXPEDICIO.Name = "tx3EXPEDICIO"
        Me.tx3EXPEDICIO.Size = New System.Drawing.Size(163, 21)
        Me.tx3EXPEDICIO.TabIndex = 1
        Me.tx3EXPEDICIO.Tag = Nothing
        '
        'lblOrdenP
        '
        Me.lblOrdenP.Enabled = false
        Me.lblOrdenP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblOrdenP.Location = New System.Drawing.Point(11, 28)
        Me.lblOrdenP.Name = "lblOrdenP"
        Me.lblOrdenP.Size = New System.Drawing.Size(129, 20)
        Me.lblOrdenP.TabIndex = 45
        Me.lblOrdenP.Text = "Ordre"
        Me.lblOrdenP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblExpedicionP
        '
        Me.lblExpedicionP.Enabled = false
        Me.lblExpedicionP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblExpedicionP.Location = New System.Drawing.Point(11, 59)
        Me.lblExpedicionP.Name = "lblExpedicionP"
        Me.lblExpedicionP.Size = New System.Drawing.Size(129, 20)
        Me.lblExpedicionP.TabIndex = 47
        Me.lblExpedicionP.Text = "Expedició"
        Me.lblExpedicionP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx3ORDRE
        '
        Me.tx3ORDRE.Location = New System.Drawing.Point(151, 25)
        Me.tx3ORDRE.Name = "tx3ORDRE"
        Me.tx3ORDRE.Size = New System.Drawing.Size(163, 21)
        Me.tx3ORDRE.TabIndex = 0
        Me.tx3ORDRE.Tag = Nothing
        '
        'lblFechaPrevistaP
        '
        Me.lblFechaPrevistaP.Enabled = false
        Me.lblFechaPrevistaP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFechaPrevistaP.Location = New System.Drawing.Point(325, 59)
        Me.lblFechaPrevistaP.Name = "lblFechaPrevistaP"
        Me.lblFechaPrevistaP.Size = New System.Drawing.Size(151, 20)
        Me.lblFechaPrevistaP.TabIndex = 59
        Me.lblFechaPrevistaP.Text = "Data Prevista"
        Me.lblFechaPrevistaP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFechaEntradaP
        '
        Me.lblFechaEntradaP.Enabled = false
        Me.lblFechaEntradaP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFechaEntradaP.Location = New System.Drawing.Point(325, 28)
        Me.lblFechaEntradaP.Name = "lblFechaEntradaP"
        Me.lblFechaEntradaP.Size = New System.Drawing.Size(151, 20)
        Me.lblFechaEntradaP.TabIndex = 58
        Me.lblFechaEntradaP.Text = "Data Entrada"
        Me.lblFechaEntradaP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tabPageTaras
        '
        Me.tabPageTaras.Controls.Add(Me.cboTallas)
        Me.tabPageTaras.Controls.Add(Me.dgAnulaciones)
        Me.tabPageTaras.Controls.Add(Me.gpModeloT)
        Me.tabPageTaras.Location = New System.Drawing.Point(4, 22)
        Me.tabPageTaras.Name = "tabPageTaras"
        Me.tabPageTaras.Size = New System.Drawing.Size(1133, 505)
        Me.tabPageTaras.TabIndex = 2
        Me.tabPageTaras.Text = "Taras"
        '
        'cboTallas
        '
        Me.cboTallas.AllowColMove = true
        Me.cboTallas.AllowColSelect = true
        Me.cboTallas.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboTallas.AlternatingRows = false
        Me.cboTallas.CaptionHeight = 17
        Me.cboTallas.CaptionStyle = Style31
        Me.cboTallas.ColumnCaptionHeight = 17
        Me.cboTallas.ColumnFooterHeight = 17
        Me.cboTallas.ColumnSelectorStyle = Style32
        Me.cboTallas.EvenRowStyle = Style33
        Me.cboTallas.FetchRowStyles = false
        Me.cboTallas.FooterStyle = Style34
        Me.cboTallas.HeadingStyle = Style35
        Me.cboTallas.HighLightRowStyle = Style36
        Me.cboTallas.Images.Add(CType(resources.GetObject("cboTallas.Images"),System.Drawing.Image))
        Me.cboTallas.Location = New System.Drawing.Point(73, 226)
        Me.cboTallas.Name = "cboTallas"
        Me.cboTallas.OddRowStyle = Style37
        Me.cboTallas.RecordSelectorStyle = Style38
        Me.cboTallas.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboTallas.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboTallas.RowHeight = 15
        Me.cboTallas.RowSelectorStyle = Style39
        Me.cboTallas.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboTallas.ScrollTips = false
        Me.cboTallas.Size = New System.Drawing.Size(168, 158)
        Me.cboTallas.Style = Style40
        Me.cboTallas.TabIndex = 3
        Me.cboTallas.Text = "C1TrueDBDropdown1"
        Me.cboTallas.UseCompatibleTextRendering = false
        Me.cboTallas.ValueTranslate = true
        Me.cboTallas.Visible = false
        Me.cboTallas.PropBag = resources.GetString("cboTallas.PropBag")
        '
        'dgAnulaciones
        '
        Me.dgAnulaciones.AllowAddNew = true
        Me.dgAnulaciones.AllowDelete = true
        Me.dgAnulaciones.CaptionHeight = 17
        Me.dgAnulaciones.ExtendRightColumn = true
        Me.dgAnulaciones.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgAnulaciones.Images.Add(CType(resources.GetObject("dgAnulaciones.Images"),System.Drawing.Image))
        Me.dgAnulaciones.Location = New System.Drawing.Point(17, 212)
        Me.dgAnulaciones.Name = "dgAnulaciones"
        Me.dgAnulaciones.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgAnulaciones.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgAnulaciones.PreviewInfo.ZoomFactor = 75R
        Me.dgAnulaciones.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgAnulaciones.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgAnulaciones.RowHeight = 15
        Me.dgAnulaciones.Size = New System.Drawing.Size(1305, 325)
        Me.dgAnulaciones.SpringMode = true
        Me.dgAnulaciones.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgAnulaciones.TabIndex = 2
        Me.dgAnulaciones.Text = "C1TrueDBGrid1"
        Me.dgAnulaciones.UseCompatibleTextRendering = false
        Me.dgAnulaciones.WrapCellPointer = true
        Me.dgAnulaciones.PropBag = resources.GetString("dgAnulaciones.PropBag")
        '
        'gpModeloT
        '
        Me.gpModeloT.Controls.Add(Me.dt2ENTRADA)
        Me.gpModeloT.Controls.Add(Me.dt2PREVISTA)
        Me.gpModeloT.Controls.Add(Me.lblFechaPrevistaT)
        Me.gpModeloT.Controls.Add(Me.lblFechaEntradaT)
        Me.gpModeloT.Controls.Add(Me.tx2NOMCLIENT)
        Me.gpModeloT.Controls.Add(Me.tx2CLIENT)
        Me.gpModeloT.Controls.Add(Me.lblClienteT)
        Me.gpModeloT.Controls.Add(Me.tx2SERIE)
        Me.gpModeloT.Controls.Add(Me.lblSerieT)
        Me.gpModeloT.Controls.Add(Me.tx2MODEL)
        Me.gpModeloT.Controls.Add(Me.lblModeloT)
        Me.gpModeloT.Controls.Add(Me.tx2EXPEDICIO)
        Me.gpModeloT.Controls.Add(Me.lblExpedicionT)
        Me.gpModeloT.Controls.Add(Me.tx2ORDRE)
        Me.gpModeloT.Controls.Add(Me.lblOrdenT)
        Me.gpModeloT.Location = New System.Drawing.Point(11, 10)
        Me.gpModeloT.Name = "gpModeloT"
        Me.gpModeloT.Size = New System.Drawing.Size(678, 187)
        Me.gpModeloT.TabIndex = 1
        Me.gpModeloT.TabStop = false
        '
        'dt2ENTRADA
        '
        '
        '
        '
        Me.dt2ENTRADA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dt2ENTRADA.Culture = 3082
        Me.dt2ENTRADA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dt2ENTRADA.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dt2ENTRADA.Location = New System.Drawing.Point(482, 25)
        Me.dt2ENTRADA.Name = "dt2ENTRADA"
        Me.dt2ENTRADA.Size = New System.Drawing.Size(190, 21)
        Me.dt2ENTRADA.TabIndex = 251
        Me.dt2ENTRADA.Tag = Nothing
        '
        'dt2PREVISTA
        '
        '
        '
        '
        Me.dt2PREVISTA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dt2PREVISTA.Culture = 3082
        Me.dt2PREVISTA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dt2PREVISTA.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dt2PREVISTA.Location = New System.Drawing.Point(482, 54)
        Me.dt2PREVISTA.Name = "dt2PREVISTA"
        Me.dt2PREVISTA.Size = New System.Drawing.Size(190, 21)
        Me.dt2PREVISTA.TabIndex = 250
        Me.dt2PREVISTA.Tag = Nothing
        '
        'lblFechaPrevistaT
        '
        Me.lblFechaPrevistaT.Enabled = false
        Me.lblFechaPrevistaT.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFechaPrevistaT.Location = New System.Drawing.Point(325, 59)
        Me.lblFechaPrevistaT.Name = "lblFechaPrevistaT"
        Me.lblFechaPrevistaT.Size = New System.Drawing.Size(151, 20)
        Me.lblFechaPrevistaT.TabIndex = 74
        Me.lblFechaPrevistaT.Text = "Data Prevista"
        Me.lblFechaPrevistaT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFechaEntradaT
        '
        Me.lblFechaEntradaT.Enabled = false
        Me.lblFechaEntradaT.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFechaEntradaT.Location = New System.Drawing.Point(325, 30)
        Me.lblFechaEntradaT.Name = "lblFechaEntradaT"
        Me.lblFechaEntradaT.Size = New System.Drawing.Size(151, 19)
        Me.lblFechaEntradaT.TabIndex = 73
        Me.lblFechaEntradaT.Text = "Data Entrada"
        Me.lblFechaEntradaT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx2NOMCLIENT
        '
        Me.tx2NOMCLIENT.Location = New System.Drawing.Point(224, 143)
        Me.tx2NOMCLIENT.Name = "tx2NOMCLIENT"
        Me.tx2NOMCLIENT.Size = New System.Drawing.Size(314, 21)
        Me.tx2NOMCLIENT.TabIndex = 7
        Me.tx2NOMCLIENT.Tag = Nothing
        '
        'tx2CLIENT
        '
        Me.tx2CLIENT.Location = New System.Drawing.Point(151, 143)
        Me.tx2CLIENT.Name = "tx2CLIENT"
        Me.tx2CLIENT.Size = New System.Drawing.Size(62, 21)
        Me.tx2CLIENT.TabIndex = 6
        Me.tx2CLIENT.Tag = Nothing
        '
        'lblClienteT
        '
        Me.lblClienteT.Enabled = false
        Me.lblClienteT.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblClienteT.Location = New System.Drawing.Point(11, 148)
        Me.lblClienteT.Name = "lblClienteT"
        Me.lblClienteT.Size = New System.Drawing.Size(129, 19)
        Me.lblClienteT.TabIndex = 68
        Me.lblClienteT.Text = "Client"
        Me.lblClienteT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx2SERIE
        '
        Me.tx2SERIE.Location = New System.Drawing.Point(151, 113)
        Me.tx2SERIE.Name = "tx2SERIE"
        Me.tx2SERIE.Size = New System.Drawing.Size(163, 21)
        Me.tx2SERIE.TabIndex = 5
        Me.tx2SERIE.Tag = Nothing
        '
        'lblSerieT
        '
        Me.lblSerieT.Enabled = false
        Me.lblSerieT.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblSerieT.Location = New System.Drawing.Point(11, 118)
        Me.lblSerieT.Name = "lblSerieT"
        Me.lblSerieT.Size = New System.Drawing.Size(129, 20)
        Me.lblSerieT.TabIndex = 66
        Me.lblSerieT.Text = "Serie"
        Me.lblSerieT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx2MODEL
        '
        Me.tx2MODEL.Location = New System.Drawing.Point(151, 84)
        Me.tx2MODEL.Name = "tx2MODEL"
        Me.tx2MODEL.Size = New System.Drawing.Size(163, 21)
        Me.tx2MODEL.TabIndex = 4
        Me.tx2MODEL.Tag = Nothing
        '
        'lblModeloT
        '
        Me.lblModeloT.Enabled = false
        Me.lblModeloT.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblModeloT.Location = New System.Drawing.Point(11, 89)
        Me.lblModeloT.Name = "lblModeloT"
        Me.lblModeloT.Size = New System.Drawing.Size(129, 19)
        Me.lblModeloT.TabIndex = 64
        Me.lblModeloT.Text = "Modelo"
        Me.lblModeloT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx2EXPEDICIO
        '
        Me.tx2EXPEDICIO.Location = New System.Drawing.Point(151, 54)
        Me.tx2EXPEDICIO.Name = "tx2EXPEDICIO"
        Me.tx2EXPEDICIO.Size = New System.Drawing.Size(163, 21)
        Me.tx2EXPEDICIO.TabIndex = 1
        Me.tx2EXPEDICIO.Tag = Nothing
        '
        'lblExpedicionT
        '
        Me.lblExpedicionT.Enabled = false
        Me.lblExpedicionT.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblExpedicionT.Location = New System.Drawing.Point(11, 59)
        Me.lblExpedicionT.Name = "lblExpedicionT"
        Me.lblExpedicionT.Size = New System.Drawing.Size(129, 20)
        Me.lblExpedicionT.TabIndex = 62
        Me.lblExpedicionT.Text = "Expedició"
        Me.lblExpedicionT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx2ORDRE
        '
        Me.tx2ORDRE.Location = New System.Drawing.Point(151, 25)
        Me.tx2ORDRE.Name = "tx2ORDRE"
        Me.tx2ORDRE.Size = New System.Drawing.Size(163, 21)
        Me.tx2ORDRE.TabIndex = 0
        Me.tx2ORDRE.Tag = Nothing
        '
        'lblOrdenT
        '
        Me.lblOrdenT.Enabled = false
        Me.lblOrdenT.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblOrdenT.Location = New System.Drawing.Point(11, 30)
        Me.lblOrdenT.Name = "lblOrdenT"
        Me.lblOrdenT.Size = New System.Drawing.Size(129, 19)
        Me.lblOrdenT.TabIndex = 60
        Me.lblOrdenT.Text = "Ordre"
        Me.lblOrdenT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tabPageConsumos
        '
        Me.tabPageConsumos.Controls.Add(Me.Label2)
        Me.tabPageConsumos.Controls.Add(Me.Label1)
        Me.tabPageConsumos.Controls.Add(Me.cboAOrden)
        Me.tabPageConsumos.Controls.Add(Me.cboDeOrden)
        Me.tabPageConsumos.Controls.Add(Me.btnGenerarComandesCompra)
        Me.tabPageConsumos.Controls.Add(Me.btnGenerarConsumos)
        Me.tabPageConsumos.Controls.Add(Me.lstOrdenes)
        Me.tabPageConsumos.Controls.Add(Me.dgConsumosModelo)
        Me.tabPageConsumos.Controls.Add(Me.GroupBox2)
        Me.tabPageConsumos.Location = New System.Drawing.Point(4, 22)
        Me.tabPageConsumos.Name = "tabPageConsumos"
        Me.tabPageConsumos.Size = New System.Drawing.Size(1133, 505)
        Me.tabPageConsumos.TabIndex = 4
        Me.tabPageConsumos.Text = "Consums"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(336, 170)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 28)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "a Ordre"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(17, 170)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 28)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Veure d'Ordre"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboAOrden
        '
        Me.cboAOrden.Caption = ""
        Me.cboAOrden.CaptionHeight = 17
        Me.cboAOrden.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboAOrden.ColumnCaptionHeight = 17
        Me.cboAOrden.ColumnFooterHeight = 17
        Me.cboAOrden.ContentHeight = 18
        Me.cboAOrden.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboAOrden.Images.Add(CType(resources.GetObject("cboAOrden.Images"),System.Drawing.Image))
        Me.cboAOrden.Location = New System.Drawing.Point(414, 172)
        Me.cboAOrden.MatchEntryTimeout = CType(2000,Long)
        Me.cboAOrden.MaxDropDownItems = CType(5,Short)
        Me.cboAOrden.MaxLength = 32767
        Me.cboAOrden.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboAOrden.Name = "cboAOrden"
        Me.cboAOrden.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboAOrden.Size = New System.Drawing.Size(170, 23)
        Me.cboAOrden.TabIndex = 29
        Me.cboAOrden.PropBag = resources.GetString("cboAOrden.PropBag")
        '
        'cboDeOrden
        '
        Me.cboDeOrden.Caption = ""
        Me.cboDeOrden.CaptionHeight = 17
        Me.cboDeOrden.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboDeOrden.ColumnCaptionHeight = 17
        Me.cboDeOrden.ColumnFooterHeight = 17
        Me.cboDeOrden.ContentHeight = 18
        Me.cboDeOrden.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboDeOrden.Images.Add(CType(resources.GetObject("cboDeOrden.Images"),System.Drawing.Image))
        Me.cboDeOrden.Location = New System.Drawing.Point(157, 172)
        Me.cboDeOrden.MatchEntryTimeout = CType(2000,Long)
        Me.cboDeOrden.MaxDropDownItems = CType(5,Short)
        Me.cboDeOrden.MaxLength = 32767
        Me.cboDeOrden.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboDeOrden.Name = "cboDeOrden"
        Me.cboDeOrden.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboDeOrden.Size = New System.Drawing.Size(169, 23)
        Me.cboDeOrden.TabIndex = 28
        Me.cboDeOrden.PropBag = resources.GetString("cboDeOrden.PropBag")
        '
        'btnGenerarComandesCompra
        '
        Me.btnGenerarComandesCompra.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnGenerarComandesCompra.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerarComandesCompra.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerarComandesCompra.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnGenerarComandesCompra.Location = New System.Drawing.Point(1098, 177)
        Me.btnGenerarComandesCompra.Name = "btnGenerarComandesCompra"
        Me.btnGenerarComandesCompra.Size = New System.Drawing.Size(224, 25)
        Me.btnGenerarComandesCompra.TabIndex = 27
        Me.btnGenerarComandesCompra.Text = "Generar Comandes Teixits"
        Me.btnGenerarComandesCompra.UseVisualStyleBackColor = true
        '
        'btnGenerarConsumos
        '
        Me.btnGenerarConsumos.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnGenerarConsumos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerarConsumos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerarConsumos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnGenerarConsumos.Location = New System.Drawing.Point(924, 177)
        Me.btnGenerarConsumos.Name = "btnGenerarConsumos"
        Me.btnGenerarConsumos.Size = New System.Drawing.Size(168, 25)
        Me.btnGenerarConsumos.TabIndex = 26
        Me.btnGenerarConsumos.Text = "Generar Consums"
        Me.btnGenerarConsumos.UseVisualStyleBackColor = true
        '
        'lstOrdenes
        '
        Me.lstOrdenes.AllowColMove = false
        Me.lstOrdenes.AllowColSelect = false
        Me.lstOrdenes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstOrdenes.Caption = ""
        Me.lstOrdenes.CaptionHeight = 17
        Me.lstOrdenes.ColumnCaptionHeight = 17
        Me.lstOrdenes.ColumnFooterHeight = 17
        Me.lstOrdenes.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark
        Me.lstOrdenes.Images.Add(CType(resources.GetObject("lstOrdenes.Images"),System.Drawing.Image))
        Me.lstOrdenes.Location = New System.Drawing.Point(17, 212)
        Me.lstOrdenes.MatchEntryTimeout = CType(2000,Long)
        Me.lstOrdenes.Name = "lstOrdenes"
        Me.lstOrdenes.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.lstOrdenes.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.lstOrdenes.PreviewInfo.ZoomFactor = 75R
        Me.lstOrdenes.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.lstOrdenes.SelectionMode = C1.Win.C1List.SelectionModeEnum.CheckBox
        Me.lstOrdenes.Size = New System.Drawing.Size(605, 344)
        Me.lstOrdenes.TabIndex = 4
        Me.lstOrdenes.PropBag = resources.GetString("lstOrdenes.PropBag")
        '
        'dgConsumosModelo
        '
        Me.dgConsumosModelo.AllowAddNew = true
        Me.dgConsumosModelo.AllowDelete = true
        Me.dgConsumosModelo.CaptionHeight = 17
        Me.dgConsumosModelo.ExtendRightColumn = true
        Me.dgConsumosModelo.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgConsumosModelo.Images.Add(CType(resources.GetObject("dgConsumosModelo.Images"),System.Drawing.Image))
        Me.dgConsumosModelo.Location = New System.Drawing.Point(633, 212)
        Me.dgConsumosModelo.Name = "dgConsumosModelo"
        Me.dgConsumosModelo.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgConsumosModelo.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgConsumosModelo.PreviewInfo.ZoomFactor = 75R
        Me.dgConsumosModelo.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgConsumosModelo.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgConsumosModelo.RowHeight = 15
        Me.dgConsumosModelo.Size = New System.Drawing.Size(689, 344)
        Me.dgConsumosModelo.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgConsumosModelo.TabIndex = 3
        Me.dgConsumosModelo.Text = "C1TrueDBGrid1"
        Me.dgConsumosModelo.UseCompatibleTextRendering = false
        Me.dgConsumosModelo.WrapCellPointer = true
        Me.dgConsumosModelo.PropBag = resources.GetString("dgConsumosModelo.PropBag")
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dt4ENTRADA)
        Me.GroupBox2.Controls.Add(Me.dt4PREVISTA)
        Me.GroupBox2.Controls.Add(Me.lblFechaPrevistaC)
        Me.GroupBox2.Controls.Add(Me.lblFechaEntradaC)
        Me.GroupBox2.Controls.Add(Me.tx4NOMCLIENT)
        Me.GroupBox2.Controls.Add(Me.tx4CLIENT)
        Me.GroupBox2.Controls.Add(Me.lblClienteC)
        Me.GroupBox2.Controls.Add(Me.tx4ORDRE)
        Me.GroupBox2.Controls.Add(Me.lblSerieC)
        Me.GroupBox2.Controls.Add(Me.tx4SERIE)
        Me.GroupBox2.Controls.Add(Me.lblModeloC)
        Me.GroupBox2.Controls.Add(Me.tx4MODEL)
        Me.GroupBox2.Controls.Add(Me.lblExpedicionC)
        Me.GroupBox2.Controls.Add(Me.tx4EXPEDICIO)
        Me.GroupBox2.Controls.Add(Me.lblCodigoOrdenC)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 10)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1014, 123)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = false
        '
        'dt4ENTRADA
        '
        '
        '
        '
        Me.dt4ENTRADA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dt4ENTRADA.Culture = 3082
        Me.dt4ENTRADA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dt4ENTRADA.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dt4ENTRADA.Location = New System.Drawing.Point(806, 25)
        Me.dt4ENTRADA.Name = "dt4ENTRADA"
        Me.dt4ENTRADA.Size = New System.Drawing.Size(191, 21)
        Me.dt4ENTRADA.TabIndex = 255
        Me.dt4ENTRADA.Tag = Nothing
        '
        'dt4PREVISTA
        '
        '
        '
        '
        Me.dt4PREVISTA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dt4PREVISTA.Culture = 3082
        Me.dt4PREVISTA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dt4PREVISTA.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dt4PREVISTA.Location = New System.Drawing.Point(806, 54)
        Me.dt4PREVISTA.Name = "dt4PREVISTA"
        Me.dt4PREVISTA.Size = New System.Drawing.Size(191, 21)
        Me.dt4PREVISTA.TabIndex = 254
        Me.dt4PREVISTA.Tag = Nothing
        '
        'lblFechaPrevistaC
        '
        Me.lblFechaPrevistaC.Enabled = false
        Me.lblFechaPrevistaC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFechaPrevistaC.Location = New System.Drawing.Point(666, 59)
        Me.lblFechaPrevistaC.Name = "lblFechaPrevistaC"
        Me.lblFechaPrevistaC.Size = New System.Drawing.Size(140, 20)
        Me.lblFechaPrevistaC.TabIndex = 74
        Me.lblFechaPrevistaC.Text = "Data Prevista"
        Me.lblFechaPrevistaC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFechaEntradaC
        '
        Me.lblFechaEntradaC.Enabled = false
        Me.lblFechaEntradaC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFechaEntradaC.Location = New System.Drawing.Point(666, 30)
        Me.lblFechaEntradaC.Name = "lblFechaEntradaC"
        Me.lblFechaEntradaC.Size = New System.Drawing.Size(140, 19)
        Me.lblFechaEntradaC.TabIndex = 73
        Me.lblFechaEntradaC.Text = "Data Entrada"
        Me.lblFechaEntradaC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx4NOMCLIENT
        '
        Me.tx4NOMCLIENT.Location = New System.Drawing.Point(224, 84)
        Me.tx4NOMCLIENT.Name = "tx4NOMCLIENT"
        Me.tx4NOMCLIENT.Size = New System.Drawing.Size(437, 21)
        Me.tx4NOMCLIENT.TabIndex = 70
        Me.tx4NOMCLIENT.Tag = Nothing
        '
        'tx4CLIENT
        '
        Me.tx4CLIENT.Location = New System.Drawing.Point(151, 84)
        Me.tx4CLIENT.Name = "tx4CLIENT"
        Me.tx4CLIENT.Size = New System.Drawing.Size(62, 21)
        Me.tx4CLIENT.TabIndex = 69
        Me.tx4CLIENT.Tag = Nothing
        '
        'lblClienteC
        '
        Me.lblClienteC.Enabled = false
        Me.lblClienteC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblClienteC.Location = New System.Drawing.Point(11, 89)
        Me.lblClienteC.Name = "lblClienteC"
        Me.lblClienteC.Size = New System.Drawing.Size(129, 19)
        Me.lblClienteC.TabIndex = 68
        Me.lblClienteC.Text = "Client"
        Me.lblClienteC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx4ORDRE
        '
        Me.tx4ORDRE.Location = New System.Drawing.Point(151, 25)
        Me.tx4ORDRE.Name = "tx4ORDRE"
        Me.tx4ORDRE.Size = New System.Drawing.Size(163, 21)
        Me.tx4ORDRE.TabIndex = 69
        Me.tx4ORDRE.Tag = Nothing
        '
        'lblSerieC
        '
        Me.lblSerieC.Enabled = false
        Me.lblSerieC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblSerieC.Location = New System.Drawing.Point(325, 59)
        Me.lblSerieC.Name = "lblSerieC"
        Me.lblSerieC.Size = New System.Drawing.Size(129, 20)
        Me.lblSerieC.TabIndex = 66
        Me.lblSerieC.Text = "Sèrie"
        Me.lblSerieC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx4SERIE
        '
        Me.tx4SERIE.Location = New System.Drawing.Point(465, 54)
        Me.tx4SERIE.Name = "tx4SERIE"
        Me.tx4SERIE.Size = New System.Drawing.Size(162, 21)
        Me.tx4SERIE.TabIndex = 67
        Me.tx4SERIE.Tag = Nothing
        '
        'lblModeloC
        '
        Me.lblModeloC.Enabled = false
        Me.lblModeloC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblModeloC.Location = New System.Drawing.Point(325, 30)
        Me.lblModeloC.Name = "lblModeloC"
        Me.lblModeloC.Size = New System.Drawing.Size(129, 19)
        Me.lblModeloC.TabIndex = 64
        Me.lblModeloC.Text = "Model"
        Me.lblModeloC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx4MODEL
        '
        Me.tx4MODEL.Location = New System.Drawing.Point(465, 25)
        Me.tx4MODEL.Name = "tx4MODEL"
        Me.tx4MODEL.Size = New System.Drawing.Size(162, 21)
        Me.tx4MODEL.TabIndex = 65
        Me.tx4MODEL.Tag = Nothing
        '
        'lblExpedicionC
        '
        Me.lblExpedicionC.Enabled = false
        Me.lblExpedicionC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblExpedicionC.Location = New System.Drawing.Point(11, 59)
        Me.lblExpedicionC.Name = "lblExpedicionC"
        Me.lblExpedicionC.Size = New System.Drawing.Size(129, 20)
        Me.lblExpedicionC.TabIndex = 62
        Me.lblExpedicionC.Text = "Expedició"
        Me.lblExpedicionC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx4EXPEDICIO
        '
        Me.tx4EXPEDICIO.Location = New System.Drawing.Point(151, 54)
        Me.tx4EXPEDICIO.Name = "tx4EXPEDICIO"
        Me.tx4EXPEDICIO.Size = New System.Drawing.Size(163, 21)
        Me.tx4EXPEDICIO.TabIndex = 63
        Me.tx4EXPEDICIO.Tag = Nothing
        '
        'lblCodigoOrdenC
        '
        Me.lblCodigoOrdenC.Enabled = false
        Me.lblCodigoOrdenC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCodigoOrdenC.Location = New System.Drawing.Point(11, 30)
        Me.lblCodigoOrdenC.Name = "lblCodigoOrdenC"
        Me.lblCodigoOrdenC.Size = New System.Drawing.Size(129, 19)
        Me.lblCodigoOrdenC.TabIndex = 60
        Me.lblCodigoOrdenC.Text = "Ordre"
        Me.lblCodigoOrdenC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnImprimir
        '
        Me.btnImprimir.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimir.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnImprimir.Location = New System.Drawing.Point(773, 634)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(101, 25)
        Me.btnImprimir.TabIndex = 25
        Me.btnImprimir.Text = "Imprimir"
        Me.btnImprimir.UseVisualStyleBackColor = true
        '
        'gbEleccionClientesPAF
        '
        Me.gbEleccionClientesPAF.BackColor = System.Drawing.SystemColors.Control
        Me.gbEleccionClientesPAF.Controls.Add(Me.lblNumeroPAF)
        Me.gbEleccionClientesPAF.Controls.Add(Me.cboSeleccionClienteParaPAF)
        Me.gbEleccionClientesPAF.Controls.Add(Me.rdoVerTodasPAF)
        Me.gbEleccionClientesPAF.Controls.Add(Me.rdoVerPAFDeCliente)
        Me.gbEleccionClientesPAF.Location = New System.Drawing.Point(6, 5)
        Me.gbEleccionClientesPAF.Name = "gbEleccionClientesPAF"
        Me.gbEleccionClientesPAF.Size = New System.Drawing.Size(752, 59)
        Me.gbEleccionClientesPAF.TabIndex = 222
        Me.gbEleccionClientesPAF.TabStop = false
        Me.gbEleccionClientesPAF.Text = "El·leccions"
        '
        'lblNumeroPAF
        '
        Me.lblNumeroPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNumeroPAF.Location = New System.Drawing.Point(528, 25)
        Me.lblNumeroPAF.Name = "lblNumeroPAF"
        Me.lblNumeroPAF.Size = New System.Drawing.Size(202, 28)
        Me.lblNumeroPAF.TabIndex = 3
        '
        'cboSeleccionClienteParaPAF
        '
        Me.cboSeleccionClienteParaPAF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboSeleccionClienteParaPAF.Caption = ""
        Me.cboSeleccionClienteParaPAF.CaptionHeight = 17
        Me.cboSeleccionClienteParaPAF.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboSeleccionClienteParaPAF.ColumnCaptionHeight = 17
        Me.cboSeleccionClienteParaPAF.ColumnFooterHeight = 17
        Me.cboSeleccionClienteParaPAF.ContentHeight = 18
        Me.cboSeleccionClienteParaPAF.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSeleccionClienteParaPAF.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboSeleccionClienteParaPAF.Images.Add(CType(resources.GetObject("cboSeleccionClienteParaPAF.Images"),System.Drawing.Image))
        Me.cboSeleccionClienteParaPAF.IntegralHeight = true
        Me.cboSeleccionClienteParaPAF.LimitToList = true
        Me.cboSeleccionClienteParaPAF.Location = New System.Drawing.Point(245, 27)
        Me.cboSeleccionClienteParaPAF.MatchEntryTimeout = CType(100,Long)
        Me.cboSeleccionClienteParaPAF.MaxDropDownItems = CType(8,Short)
        Me.cboSeleccionClienteParaPAF.MaxLength = 255
        Me.cboSeleccionClienteParaPAF.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboSeleccionClienteParaPAF.Name = "cboSeleccionClienteParaPAF"
        Me.cboSeleccionClienteParaPAF.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboSeleccionClienteParaPAF.Size = New System.Drawing.Size(277, 23)
        Me.cboSeleccionClienteParaPAF.TabIndex = 2
        Me.cboSeleccionClienteParaPAF.Visible = false
        Me.cboSeleccionClienteParaPAF.PropBag = resources.GetString("cboSeleccionClienteParaPAF.PropBag")
        '
        'rdoVerTodasPAF
        '
        Me.rdoVerTodasPAF.Checked = true
        Me.rdoVerTodasPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoVerTodasPAF.Location = New System.Drawing.Point(6, 27)
        Me.rdoVerTodasPAF.Name = "rdoVerTodasPAF"
        Me.rdoVerTodasPAF.Size = New System.Drawing.Size(129, 26)
        Me.rdoVerTodasPAF.TabIndex = 0
        Me.rdoVerTodasPAF.TabStop = true
        Me.rdoVerTodasPAF.Text = "Tots els clients"
        '
        'rdoVerPAFDeCliente
        '
        Me.rdoVerPAFDeCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoVerPAFDeCliente.Location = New System.Drawing.Point(138, 27)
        Me.rdoVerPAFDeCliente.Name = "rdoVerPAFDeCliente"
        Me.rdoVerPAFDeCliente.Size = New System.Drawing.Size(101, 26)
        Me.rdoVerPAFDeCliente.TabIndex = 1
        Me.rdoVerPAFDeCliente.Text = "Del Client"
        '
        'gbCentro
        '
        Me.gbCentro.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.gbCentro.BackColor = System.Drawing.SystemColors.Control
        Me.gbCentro.Controls.Add(Me.lblNumeracion)
        Me.gbCentro.Location = New System.Drawing.Point(772, 5)
        Me.gbCentro.Name = "gbCentro"
        Me.gbCentro.Size = New System.Drawing.Size(370, 59)
        Me.gbCentro.TabIndex = 223
        Me.gbCentro.TabStop = false
        '
        'lblNumeracion
        '
        Me.lblNumeracion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNumeracion.Location = New System.Drawing.Point(11, 30)
        Me.lblNumeracion.Name = "lblNumeracion"
        Me.lblNumeracion.Size = New System.Drawing.Size(123, 17)
        Me.lblNumeracion.TabIndex = 220
        Me.lblNumeracion.Text = "Numeració de:"
        Me.lblNumeracion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmOrdenesFabricacion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 16)
        Me.ClientSize = New System.Drawing.Size(1157, 708)
        Me.Controls.Add(Me.gbEleccionClientesPAF)
        Me.Controls.Add(Me.gbCentro)
        Me.Controls.Add(Me.tabControlOrdenes)
        Me.Controls.Add(Me.btnImprimir)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "frmOrdenesFabricacion"
        Me.Text = "Ordres de Fabricació"
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
        Me.Controls.SetChildIndex(Me.cboSeleccionCentro, 0)
        Me.Controls.SetChildIndex(Me.btnImprimir, 0)
        Me.Controls.SetChildIndex(Me.tabControlOrdenes, 0)
        Me.Controls.SetChildIndex(Me.gbCentro, 0)
        Me.Controls.SetChildIndex(Me.gbEleccionClientesPAF, 0)
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
        Me.tabControlOrdenes.ResumeLayout(false)
        Me.tabPageOrden.ResumeLayout(false)
        Me.tabPageOrden.PerformLayout
        CType(Me.txtTEMPORADA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtSERIE,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtMODEL,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtpPREVISTA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnElegirModelo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtpENTRADA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtpFINAL,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboColores,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtTALLA10,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtTALLA09,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtTALLA08,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtTALLA07,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtTALLA06,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtTALLA05,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtTALLA04,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtTALLA03,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtTALLA02,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtTALLA01,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgOrdenes,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtALBARA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtOBSERV,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboID,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtCOMIS,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtPREU,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboNOMREPRES,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnElegirRepresentante,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtREPRES,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboNOMCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnElegirCliente,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtEXPEDICIO,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabPageAAlbaran.ResumeLayout(false)
        Me.GroupBox1.ResumeLayout(false)
        CType(Me.dtpAAlbaran,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnTraspasarAAlbaran,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtAAlbaran,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabProcesosOrden.ResumeLayout(false)
        CType(Me.cboOperaciones,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboTalleres,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgProcesos,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbProcesos.ResumeLayout(false)
        CType(Me.dt3ENTRADA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dt3PREVISTA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx3NOMCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx3CLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx3SERIE,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx3MODEL,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx3EXPEDICIO,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx3ORDRE,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabPageTaras.ResumeLayout(false)
        CType(Me.cboTallas,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgAnulaciones,System.ComponentModel.ISupportInitialize).EndInit
        Me.gpModeloT.ResumeLayout(false)
        CType(Me.dt2ENTRADA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dt2PREVISTA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx2NOMCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx2CLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx2SERIE,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx2MODEL,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx2EXPEDICIO,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx2ORDRE,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabPageConsumos.ResumeLayout(false)
        Me.tabPageConsumos.PerformLayout
        CType(Me.cboAOrden,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboDeOrden,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnGenerarComandesCompra,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnGenerarConsumos,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.lstOrdenes,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgConsumosModelo,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox2.ResumeLayout(false)
        CType(Me.dt4ENTRADA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dt4PREVISTA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx4NOMCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx4CLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx4ORDRE,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx4SERIE,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx4MODEL,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tx4EXPEDICIO,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnImprimir,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbEleccionClientesPAF.ResumeLayout(false)
        Me.gbEleccionClientesPAF.PerformLayout
        CType(Me.cboSeleccionClienteParaPAF,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbCentro.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub

#End Region

    Shared frmChildForm As frmOrdenesFabricacion
    Public Shared Function GetInstance() As frmOrdenesFabricacion
        If frmChildForm Is Nothing Then
            frmChildForm = New frmOrdenesFabricacion

        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Public PAFActual As clsOrdenModelo

#End Region

#Region "INICIALIZACIÓN"

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True
            Me.arrayEtiquetas = New System.Windows.Forms.Control() {Me.lblCliente, Me.lblClienteC, Me.lblClienteP, Me.lblClienteT, Me.lblCodigoOrdenC, Me.lblComision, Me.lblExpedicion, Me.lblExpedicionC, Me.lblExpedicionP, Me.lblExpedicionT, Me.lblFechaEntrada, Me.lblFechaEntradaC, Me.lblFechaEntradaP, Me.lblFechaEntradaT, Me.lblFechaFinal, Me.lblFechaPrevista, Me.lblFechaPrevistaC, Me.lblFechaPrevistaP, Me.lblFechaPrevistaT, Me.lblModelo, Me.lblModeloC, Me.lblModeloP, Me.lblModeloT, Me.lblNumeracion, Me.lblNumeroPAF, Me.lblObsercaciones, Me.lblOrden, Me.lblOrdenP, Me.lblOrdenT, Me.lblPedido, Me.lblPrecio, Me.lblRepresentante, Me.lblSerie, Me.lblSerieC, Me.lblSerieP, Me.lblSerieT, Me.lblTemporda}
            Me.arrayBotones = New System.Windows.Forms.Control() {Me.btnElegirCliente, Me.btnElegirModelo, Me.btnElegirRepresentante, Me.btnTraspasarAAlbaran}
            Me.arrayTabPages = New System.Windows.Forms.Control() {Me.tabPageAAlbaran, Me.tabPageConsumos, Me.tabPageOrden, Me.tabPageTaras, Me.tabProcesosOrden}
            Me.arrayCombos = New System.Windows.Forms.Control() {Me.cboNOMCLIENT, Me.cboNOMREPRES, Me.cboColores, Me.cboTallas, Me.cboTalleres}
            Me.arrayGrids = New System.Windows.Forms.Control() {Me.dgAnulaciones, Me.dgConsumosModelo, Me.dgOrdenes, Me.dgProcesos}
            Me.arrayTextBox = New System.Windows.Forms.Control() {Me.tx2CLIENT, Me.tx2EXPEDICIO, Me.tx2MODEL, Me.tx2NOMCLIENT, Me.tx2ORDRE, Me.tx2SERIE, Me.tx3CLIENT, Me.tx3EXPEDICIO, Me.tx3MODEL, Me.tx3NOMCLIENT, Me.tx3ORDRE, Me.tx3SERIE, Me.tx4CLIENT, Me.tx4EXPEDICIO, Me.tx4MODEL, Me.tx4NOMCLIENT, Me.tx4ORDRE, Me.tx4SERIE, Me.txtAAlbaran, Me.txtALBARA, Me.txtCLIENT, Me.txtCOMIS, Me.txtEXPEDICIO, Me.txtMODEL, Me.txtOBSERV, Me.txtPREU, Me.txtREPRES, Me.txtSERIE, Me.txtTALLA01, Me.txtTALLA02, Me.txtTALLA03, Me.txtTALLA04, Me.txtTALLA05, Me.txtTALLA06, Me.txtTALLA07, Me.txtTALLA08, Me.txtTALLA09, Me.txtTALLA10, Me.txtTEMPORADA}
            Me.arrayDateTime = New System.Windows.Forms.Control() {Me.dtpAAlbaran, Me.dt2ENTRADA, Me.dt2PREVISTA, Me.dt3ENTRADA, Me.dt3PREVISTA, Me.dt4ENTRADA, Me.dt4PREVISTA, Me.dtpENTRADA, Me.dtpFINAL, Me.dtpPREVISTA, Me.dtpAAlbaran}
            Me.arrayCheckBox = New System.Windows.Forms.Control() {Me.chkALB}
            PAFActual = New clsOrdenModelo(ds.Tables("cordre"), empresaPorDefecto, BindingContext, "O")
            HacerBindings()
            HacerBindingsSeleccion()
            IniciarDGDetalle()
            IniciarDGFeines()
            IniciarDGAnulaciones()

            'PAFActual.tabla.AcceptChanges()

            PonerModificables(soloLectura)
            MoverActual()
            dtpAAlbaran.Value = Date.Today
            PSBTIPO(PAFActual.centro) : cargando = False
            PonerHandlersErroresParaGrids()
            lstOrdenes.AllowRowSizing = C1.Win.C1List.RowSizingEnum.None
            dgOrdenes.ColumnFooters = True
            dgOrdenes.FooterStyle.Font = New System.Drawing.Font("Tahoma", 12, FontStyle.Bold, GraphicsUnit.Pixel)
            dgOrdenes.FooterStyle.BackColor = Color.GreenYellow

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub IniciarDGDetalle()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgOrdenes)

            i = 0
            PPCol2("COLOR", dgOrdenes, rm.GetString("COLOR"), "", True, 150, False, PresentationEnum.ComboBox, False, 150, i, False, cboColores)
            i = 1
            PPCol2("TALL1", dgOrdenes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = 2
            PPCol2("TALL2", dgOrdenes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = 3
            PPCol2("TALL3", dgOrdenes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = 4
            PPCol2("TALL4", dgOrdenes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = 5
            PPCol2("TALL5", dgOrdenes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = 6
            PPCol2("TALL6", dgOrdenes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = 7
            PPCol2("TALL7", dgOrdenes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = 8
            PPCol2("TALL8", dgOrdenes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = 9
            PPCol2("TALL9", dgOrdenes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = 10
            PPCol2("TALL10", dgOrdenes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = 11
            PPCol2("QUANTITAT", dgOrdenes, rm.GetString("CANTIDAD"), "", True, 70, False, PresentationEnum.Normal, False, 70, i, False)
            i = i + 1
            PPCol2("TRASPASARLINEA", dgOrdenes, "Traspas", "", True, 50, False, PresentationEnum.Normal, True, 50, i, False)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub HacerBindings()
        Try
            HacerBindingsTodos(PAFActual.dvForm)
            AñadirBinding(cboID, PAFActual.dvForm, "ORDRE")
            AñadirBindingCombo(cboID, PAFActual.dvIdentificadores, "ORDRE", "ORDRE")
            AñadirBindingCombo(cboNOMCLIENT, PAFActual.dtClients, CCClients, CNClients) '!!!!!!!!!!!! DTCLIENTESELEGIBLES
            OcultarTodasColumnaCbo(cboNOMCLIENT)

            AñadirBindingCombo(cboNOMREPRES, PAFActual.dtRepres, CCRepres, CNRepres)

            AñadirBindingCombo(cboOperaciones, PAFActual.dtOperaciones, "DESCRI", "DESCRI")
            AñadirBindingCombo(cboDeOrden, PAFActual.dvIdentificadores.Table.Copy, "ORDRE", "ORDRE")
            AñadirBindingCombo(cboAOrden, PAFActual.dvIdentificadores.Table.Copy, "ORDRE", "ORDRE")
            AñadirBindingCombo(cboTalleres, PAFActual.dtTallers, CNTallers, CNTallers)
            dgOrdenes.SetDataBinding(PAFActual.detallePAF.dvForm, "")
            dgProcesos.SetDataBinding(PAFActual.detalleProcesos.dvForm, "")
            dgAnulaciones.SetDataBinding(PAFActual.anulaciones.dvForm, "")

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub HacerBindingsSeleccion()
        Try
            '!!!!!!
            AñadirBindingCombo(cboColores, PAFActual.dtColores, "MODCOL", "MODCOL")
            OcultarColumnaDropDown(cboColores)
            cboColores.DisplayColumns("MODCOL").Visible = True
            AutosizeColumnasTrueDBDropdown(cboColores)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "DESPLAZARSE"

    Private Sub MoverActual(Optional ByVal nuevo As Boolean = False)
        Try
            cargando = True
            dgOrdenes.Splits(0).DisplayColumns("QUANTITAT").DataColumn.FooterText = sumaTotal("TALL1", PAFActual.detallePAF.dvForm) + sumaTotal("TALL2", PAFActual.detallePAF.dvForm) + _
                    sumaTotal("TALL3", PAFActual.detallePAF.dvForm) + sumaTotal("TALL4", PAFActual.detallePAF.dvForm) + _
                    sumaTotal("TALL5", PAFActual.detallePAF.dvForm) + sumaTotal("TALL6", PAFActual.detallePAF.dvForm) + _
                    sumaTotal("TALL7", PAFActual.detallePAF.dvForm) + sumaTotal("TALL8", PAFActual.detallePAF.dvForm) + _
                    sumaTotal("TALL9", PAFActual.detallePAF.dvForm) + sumaTotal("TALL10", PAFActual.detallePAF.dvForm)
            PSBTIPO(PAFActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSiguiente.Click
        Try
            cargando = True
            PAFActual.SiguienteReg() : ActualizarOrigen()
            MoverActual()
            PSBTIPO(PAFActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAnterior.Click
        Try
            cargando = True
            PAFActual.AnteriorReg() : ActualizarOrigen()
            MoverActual()
            PSBTIPO(PAFActual.centro) : cargando = False
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUltimo.Click
        Try
            cargando = True
            PAFActual.UltimoReg() : ActualizarOrigen()
            MoverActual()
            PSBTIPO(PAFActual.centro) : cargando = False
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnPrimero.Click
        Try
            cargando = True
            PAFActual.PrimeroReg() : ActualizarOrigen()
            MoverActual()
            PSBTIPO(PAFActual.centro) : cargando = False
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ACTUALIZAR ORIGEN"

    Private Sub PonerControlesNuevo(ByVal b As Boolean)
        Try
            If Not b Then
                btnActualizar.Text = rm.GetString("CONFIRMAR")
            Else
                btnActualizar.Text = rm.GetString("ACTUALIZAR")
            End If
            cboID.AutoCompletion = Not editando
            ModiNuev(b)
            If b Then
                tabControlOrdenes.TabPages.Add(tabPageTaras)
                tabControlOrdenes.TabPages.Add(tabProcesosOrden)
                tabControlOrdenes.TabPages.Add(tabPageAAlbaran)
                tabControlOrdenes.TabPages.Add(tabPageConsumos)
            Else
                tabControlOrdenes.TabPages.Remove(tabPageConsumos)
                tabControlOrdenes.TabPages.Remove(tabProcesosOrden)
                tabControlOrdenes.TabPages.Remove(tabPageTaras)
                tabControlOrdenes.TabPages.Remove(tabPageAAlbaran)
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub ActualizarOrigen()
        Try
            If editando Then
                editando = False
                'PAFActual.tabla.AcceptChanges()
                PonerModificables(soloLectura)
            End If
            If EsRegistroNuevo Then
                editando = True : PSBTIPO(PAFActual.centro)
                dgOrdenes.Visible = True
                PonerControlesNuevo(True)
                EsRegistroNuevo = False
                PAFActual.actualizarNumeraciones()
                MoverActual()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnActualizar.Click
        Try
            cargando = True
            If PAFActual.TieneCambios Then
                Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                    Case DialogResult.Cancel
                        cargando = False
                        Exit Sub
                    Case DialogResult.No
                        PAFActual.tabla.RejectChanges()
                        PAFActual.detallePAF.tabla.RejectChanges()
                        PAFActual.detalleProcesos.tabla.RejectChanges()
                        cargando = False
                        Exit Sub
                End Select
            End If
            PAFActual.ActualizarOrigen() : ActualizarOrigen()
            PSBTIPO(PAFActual.centro) : cargando = False

        Catch ex As Exception
            If ex.ToString.Substring(0, 9) = "Duplicate" Then
                cboID.Focus()
            End If
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ESPECIFICO"

#Region "PROCESOS"

    Private Sub dgProcesos_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgProcesos.AfterColUpdate
        Try
            If PuedoModificar() Then
                cargando = True
                dgProcesos.UpdateData()
                Select Case e.Column.DataColumn.DataField
                    Case "TALLER"
                        PAFActual.detalleProcesos.TALLER = e.Column.DataColumn.Value
                    Case "CODI"
                        PAFActual.detalleProcesos.CODI = e.Column.DataColumn.Value
                    Case "NOMTALLER"
                        PAFActual.detalleProcesos.TALLER = e.Column.DataColumn.Value
                    Case "DESCRI"
                        PAFActual.detalleProcesos.CODI = cboOperaciones.Columns("CODI").Value

                End Select
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub
    Private Sub IniciarDGFeines()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgProcesos)
            i = 0
            PPCol2("LINEA", dgProcesos, rm.GetString("LINEA"), "", True, 150, False, PresentationEnum.Normal, False, 150, i, False, Nothing, False)
            i = i + 1
            PPCol2("CODI", dgProcesos, rm.GetString("CODIGOOPERACION"), "", True, 70, False, PresentationEnum.Normal, False, 70, i, False)
            i = i + 1
            PPCol2("DESCRI", dgProcesos, rm.GetString("DESCRIPCION"), "", True, 150, False, PresentationEnum.ComboBox, False, 150, i, False, cboOperaciones)
            i = i + 1
            PPCol2("TALLER", dgProcesos, rm.GetString("CODIGOTALLER"), "", True, 50, False, PresentationEnum.Normal, False, 70, i, False)
            i = i + 1
            PPCol2("NOMTALLER", dgProcesos, rm.GetString("NOMTALLER"), "", True, 200, False, PresentationEnum.ComboBox, False, 200, i, False, cboTalleres)
            i = i + 1
            PPCol2("SORTIDA", dgProcesos, rm.GetString("SALIDA"), "Short Date", True, 50, False, PresentationEnum.Normal, False, 70, i, False)
            i = i + 1
            PPCol2("PREVISTA", dgProcesos, rm.GetString("PREVISTA"), "Short Date", True, 50, False, PresentationEnum.Normal, False, 70, i, False)
            i = i + 1
            PPCol2("FINAL", dgProcesos, rm.GetString("FINAL"), "Short Date", True, 50, False, PresentationEnum.Normal, False, 70, i, False)

            'AñadirTextButton("ELEGIRTRABAJO", "", 17, ts, dgProcesos, colELEGIRTRABAJO)
            'AñadirTextButton("ELEGIRTALLER", "", 17, ts, dgProcesos, colELEGIRTALLER)
            'AñadirTextButton("IALBENTREGA", "Imprimir Albaranes", 70, ts, dgProcesos, colIMPRIMIRALBENTREGA)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboTalleres_SelChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles cboTalleres.SelChange
        Try
            If PuedoModificar() Then
                PAFActual.detalleProcesos.TALLER = cboTalleres.Columns("CODI").Value
                AñadirBindingCombo(cboTalleres, PAFActual.dtTallers, "NOM", "CODI")
            End If

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub cboOperaciones_SelChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles cboOperaciones.SelChange
        Try
            If PuedoModificar() Then
                PAFActual.detalleProcesos.CODI = cboOperaciones.Columns("CODI").Value
                AñadirBindingCombo(cboOperaciones, PAFActual.dtOperaciones, "DESCRI", "DESCRI")
            End If

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub

#End Region
#Region "TARAS"

    Private Sub IniciarDGAnulaciones()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgAnulaciones)
            i = 0
            PPCol2("LINEA", dgAnulaciones, rm.GetString("LINEA"), "", True, 150, False, PresentationEnum.Normal, False, 150, i, False, Nothing, False)
            i = 1
            PPCol2("COLOR", dgAnulaciones, rm.GetString("COLOR"), "", True, 70, False, PresentationEnum.ComboBox, False, 70, i, False, cboColores)
            i = 2
            PPCol2("TALLA", dgAnulaciones, rm.GetString("TALLA"), "", True, 70, False, PresentationEnum.ComboBox, False, 70, i, False, cboTallas)
            i = 3
            PPCol2("UNIT", dgAnulaciones, rm.GetString("UNIDADES"), "", True, 70, False, PresentationEnum.Normal, False, 70, i, False)
            i = 4
            PPCol2("MOTIU", dgAnulaciones, rm.GetString("MOTIVO"), "", True, 250, False, PresentationEnum.Normal, False, 250, i, False)


            'Los colores son los mismos que hemos cargado en detalle colores.
            'Por lo que hay que copiar los elementos del combo de detalle de colores
            'al nuevo
            'CargarColoresregistroActualAnulaciones(ts.GridColumnStyles(CType("COLOR", Object)).columncombobox)


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region
#Region "TRASPASOS"

   
    Private Sub btnTraspasarAAlbaran_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTraspasarAAlbaran.Click
        Try
            PAFActual.Traspasar(nzn(txtAAlbaran.Value, 0), dtpAAlbaran.Value)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region
    Private Sub IniciarDGConsumos()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgConsumosModelo)
            i = 0
            PPCol2("TEJIDO", dgConsumosModelo, rm.GetString("TEJIDO"), "", True, 100, False, PresentationEnum.Normal, False, 100, i, False)
            i = 1
            PPCol2("COLOR", dgConsumosModelo, rm.GetString("COLOR"), "", True, 100, False, PresentationEnum.Normal, False, 100, i, False)
            i = 2
            PPCol2("CANTIDAD", dgConsumosModelo, rm.GetString("CANTIDAD"), "", True, 100, False, PresentationEnum.Normal, False, 100, i, False)
            i = 3
            PPCol2("ACTUAL", dgConsumosModelo, rm.GetString("ACTUAL"), "", True, 100, False, PresentationEnum.Normal, False, 100, i, False)
            i = 4
            PPCol2("MINIMO", dgConsumosModelo, rm.GetString("MINIMO"), "", True, 100, False, PresentationEnum.Normal, False, 100, i, False)
            i = 5
            PPCol2("DIFERENCIA", dgConsumosModelo, rm.GetString("DIFERENCIA"), "", True, 100, False, PresentationEnum.Normal, False, 100, i, False)
            i = 6
            PPCol2("PRECIO", dgConsumosModelo, rm.GetString("PRECIO"), "", True, 100, False, PresentationEnum.Normal, False, 100, i, False)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ELECCIONES"

    Private Sub cboNombreRepresentante_SelChange(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMREPRES.SelectedValueChanged
        Try
            If PuedoModificar() Then
                cargando = True
                PAFActual.REPRES = nzn(cboNOMREPRES.WillChangeToValue, 0)
                PAFActual.COMIS = ObtenerCampoTabla(PAFActual.REPRES, "COMIS", PAFActual.dtRepres)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboSeleccion_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSeleccionCentro.SelectionChangeCommitted
        Try
            If consulta() Then
                cargando = True
                PAFActual.cambioCentro(GENERAL.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto), ultimo)
                'Aqui hay que hacer un moveractual
                cboSeleccionCentro.SelectedValue = PAFActual.centro
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
    Private Sub cboNombreCliente_SelChange(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMCLIENT.SelectedValueChanged
        Try
            If PuedoModificar() Then
                cargando = True
                PAFActual.CLIENT = nzn(cboNOMCLIENT.WillChangeToValue, 0)
                PAFActual.REPRES = ObtenerCampoTabla(PAFActual.CLIENT, "REPRES", PAFActual.dtClients)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnElegirRepresentante_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnElegirRepresentante.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmRepresentantesLista = frmRepresentantesLista.GetInstance(esEleccion)
            f.ShowDialog()
            If Not f.dr Is Nothing Then
                PAFActual.REPRES = f.dr("CODI")
                PAFActual.REPRES = f.dr("COMIS")
            End If
            Cursor = Cursors.Default
            f = Nothing
            PSBTIPO(PAFActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Function ObtenerCampoTabla(ByVal id As Integer, ByVal CAMPO As String, ByVal dt As DataTable)
        Try
            Dim ret = dt.DefaultView(dt.DefaultView.Find(id)).Item(CAMPO)
            Return nzn(ret, 0)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Sub btnElegirCliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnElegirCliente.Click
        Try
            cargando = True
            Cursor = Cursors.WaitCursor
            Dim f As frmClientesLista = frmClientesLista.GetInstance(esEleccion)
            f.ShowDialog()
            If Not f.dr Is Nothing Then
                PAFActual.CLIENT = f.dr("CODI")
                'PAFActual.REPRES = ObtenerCampoTabla(f.dr("CODI"), "REPRES", PAFActual.dtClients)
                'PAFActual.COMIS = ObtenerCampoTabla(f.dr("CODI"), "COMIS", PAFActual.dtClients)
            End If
            Cursor = Cursors.Default
            f = Nothing
            PSBTIPO(PAFActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtCodigoCliente_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCLIENT.Validated
        Try
            If PuedoModificar() Then
                cargando = True
                Cursor = Cursors.WaitCursor
                PAFActual.CLIENT = nzn(txtCLIENT.Value, 0)
                'PAFActual.REPRES = ObtenerCampoTabla(nzn(txtCLIENT.Value, 0), "REPRES", PAFActual.dtClients)
                'PAFActual.COMIS = ObtenerCampoTabla(nzn(txtCLIENT.Value, 0), "COMIS", PAFActual.dtClients)
                Cursor = Cursors.Default
                PSBTIPO(PAFActual.centro) : cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ELECCIONES COMBO"


#End Region

#Region "ORGANIZAR"

    Private Sub PrepararParaNuevoOrden()
        Try
            dgOrdenes.Visible = False
            PonerModificables(modificable)
            PonerControlesNuevo(False)
            cboID.ClearItems()

            PAFActual.NOMCLIENT = ""
            PAFActual.NOMREPRES = ""
            PAFActual.CLIENT = 0
            MoverActual(True)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerModificables(Optional ByVal b As Boolean = False)
        Try
            PonerMod(b)
            If Not b Then : cboID.DataSource = Nothing
            Else : AñadirBindingCombo(cboID, PAFActual.dvIdentificadores, "ORDRE", "ORDRE") : PAFActual.tabla.AcceptChanges() : End If

            cboID.LimitToList = b
            cboID.SuperBack = b
            If EsRegistroNuevo Then
                cboID.ReadOnly = False
            Else
                cboID.ReadOnly = editando
            End If
            ModiNuev(b)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnNuevo.Click
        Try
            EsRegistroNuevo = True
            cargando = True

            PrepararParaNuevoOrden()

            'se le pasa esto pq todavia no sabemos que hilo es el que se va a introducir
            PAFActual.NuevoRegistro()
            PSBTIPO(PAFActual.centro) : cargando = False
            'Nuevo ORDRE

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnBorrar.Click
        Try
            If MessageBox.Show(rm.GetString("BorrarActual"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                'Primero borramos el tejido de la tabla tejido
                'Despues borramos todos sus "hilos" de la tabla filcol
                editando = True
                PAFActual.borrar() : ActualizarOrigen()
                editando = False
                MoverActual()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CargaComboTallas()
        Dim dt As New DataTable("TALLA")
        Dim dr As DataRow
        Dim dc1 As New DataColumn("TALLA")
        Try
            dc1.Caption = rm.GetString("TALLA")
            dt.Columns.Add(dc1)
            With PAFActual
                If .TALLA01 <> "" Then : dr = dt.NewRow : dr("TALLA") = .TALLA01 : dt.Rows.Add(dr) : End If
                If .TALLA02 <> "" Then : dr = dt.NewRow : dr("TALLA") = .TALLA02 : dt.Rows.Add(dr) : End If
                If .TALLA03 <> "" Then : dr = dt.NewRow : dr("TALLA") = .TALLA03 : dt.Rows.Add(dr) : End If
                If .TALLA04 <> "" Then : dr = dt.NewRow : dr("TALLA") = .TALLA04 : dt.Rows.Add(dr) : End If
                If .TALLA05 <> "" Then : dr = dt.NewRow : dr("TALLA") = .TALLA05 : dt.Rows.Add(dr) : End If
                If .TALLA06 <> "" Then : dr = dt.NewRow : dr("TALLA") = .TALLA06 : dt.Rows.Add(dr) : End If
                If .TALLA07 <> "" Then : dr = dt.NewRow : dr("TALLA") = .TALLA07 : dt.Rows.Add(dr) : End If
                If .TALLA08 <> "" Then : dr = dt.NewRow : dr("TALLA") = .TALLA08 : dt.Rows.Add(dr) : End If
                If .TALLA09 <> "" Then : dr = dt.NewRow : dr("TALLA") = .TALLA09 : dt.Rows.Add(dr) : End If
                If .TALLA10 <> "" Then : dr = dt.NewRow : dr("TALLA") = .TALLA10 : dt.Rows.Add(dr) : End If
            End With
            dt.AcceptChanges()
            cboTallas.DataSource = Nothing
            AñadirBindingCombo(cboTallas, dt, "TALLA", "TALLA")

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub tabControlOrdenes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabControlOrdenes.SelectedIndexChanged
        Try
            If Not cargando Then
                'Si vamos al tabpage de trasapso
                If tabControlOrdenes.SelectedTab Is tabPageAAlbaran Then txtAAlbaran.Value = GetNumeroUltimoPAF(Albaran, PAFActual.centro) + 1

                If tabControlOrdenes.SelectedTab Is tabPageConsumos Then
                    lstOrdenes.DataSource = PAFActual.dtIdentificadores.Copy
                    OcultarMostrarColumnaCbo(lstOrdenes, "CENTRO", False)
                    AutoSizeCC(lstOrdenes)
                    lstOrdenes.Sort("ORDRE", C1.Win.C1List.SortDirEnum.DESC)
                End If

                If tabControlOrdenes.SelectedTab Is tabPageTaras Then CargaComboTallas()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Try
            PAFActual.ImprimirPAF()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles btnModificar.Click
        Try
            If Not editando Then
                cargando = True
                editando = True
                PonerModificables(modificable)
                PSBTIPO(PAFActual.centro)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Private Sub cboID_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboID.SelectedValueChanged
        Try
            If consulta() Then
                cargando = True
                PAFActual.CambiarAReg(nzn(cboID.WillChangeToValue, 0), iraregistro)
                MoverActual()
                PSBTIPO(PAFActual.centro) : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboID_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboID.KeyPress
        Try
            If editando Or EsRegistroNuevo Then : cboID.AutoCompletion = False
            Else : cboID.AutoCompletion = True : End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub btnElegirModelo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirModelo.Click
        Try
            cargando = True
            If PAFActual.CLIENT = 0 Then
                MessageBox.Show("Primer s'ha d'escollir un client", "Informació")
                cargando = False
                Exit Sub
            End If
            Cursor = Cursors.WaitCursor
            Dim f As frmModelosLista = frmModelosLista.GetInstance(esEleccion)
            f.clienteElegido = PAFActual.CLIENT
            f.centroAElegir = PAFActual.centro
            f.ShowDialog()
            If Not f.dr Is Nothing Then
                PAFActual.MODEL = f.dr("CODI")
                PAFActual.CLIENT = f.dr("CLIENT")
                PAFActual.PonerNombres()
                PAFActual.SERIE = f.dr("SERIE")
                PAFActual.TEMPORADA = f.dr("TEMPORADA")
                PAFActual.TALLA01 = general.nz(f.dr("TALLA01"), "")
                PAFActual.TALLA02 = general.nz(f.dr("TALLA02"), "")
                PAFActual.TALLA03 = general.nz(f.dr("TALLA03"), "")
                PAFActual.TALLA04 = general.nz(f.dr("TALLA04"), "")
                PAFActual.TALLA05 = general.nz(f.dr("TALLA05"), "")
                PAFActual.TALLA06 = general.nz(f.dr("TALLA06"), "")
                PAFActual.TALLA07 = general.nz(f.dr("TALLA07"), "")
                PAFActual.TALLA08 = general.nz(f.dr("TALLA08"), "")
                PAFActual.TALLA09 = general.nz(f.dr("TALLA09"), "")
                PAFActual.TALLA10 = general.nz(f.dr("TALLA10"), "")
                PAFActual.PREU = nzn(f.dr("VENDAFINAL"), 0)

                PAFActual.TratarSeleccion()
                cargando = False
                HacerBindingsSeleccion()
            End If
            cargando = True
            f.Dispose()
            f = Nothing
            cargando = False
            Cursor = Cursors.Default

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Friend Sub btnVerLista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerLista.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmListaOrdenesModelos = frmListaOrdenesModelos.GetInstance(esListado)
            f.MdiParent = Me.MdiParent
            f.Show()
            f.BringToFront()
            Cursor = Cursors.Default

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerTallasDetalleOrdenModificables()
        Try
            With PAFActual
                If .TALLA01 <> "" Then
                    dgOrdenes.Splits(0).DisplayColumns("TALL1").AllowFocus = True
                Else
                    dgOrdenes.Splits(0).DisplayColumns("TALL1").AllowFocus = False
                End If
                If .TALLA02 <> "" Then
                    dgOrdenes.Splits(0).DisplayColumns("TALL2").AllowFocus = True
                Else
                    dgOrdenes.Splits(0).DisplayColumns("TALL2").AllowFocus = False
                End If
                If .TALLA03 <> "" Then
                    dgOrdenes.Splits(0).DisplayColumns("TALL3").AllowFocus = True
                Else
                    dgOrdenes.Splits(0).DisplayColumns("TALL3").AllowFocus = False
                End If
                If .TALLA04 <> "" Then
                    dgOrdenes.Splits(0).DisplayColumns("TALL4").AllowFocus = True
                Else
                    dgOrdenes.Splits(0).DisplayColumns("TALL4").AllowFocus = False
                End If
                If .TALLA05 <> "" Then
                    dgOrdenes.Splits(0).DisplayColumns("TALL5").AllowFocus = True
                Else
                    dgOrdenes.Splits(0).DisplayColumns("TALL5").AllowFocus = False
                End If
                If .TALLA06 <> "" Then
                    dgOrdenes.Splits(0).DisplayColumns("TALL6").AllowFocus = True
                Else
                    dgOrdenes.Splits(0).DisplayColumns("TALL6").AllowFocus = False
                End If
                If .TALLA07 <> "" Then
                    dgOrdenes.Splits(0).DisplayColumns("TALL7").AllowFocus = True
                Else
                    dgOrdenes.Splits(0).DisplayColumns("TALL7").AllowFocus = False
                End If
                If .TALLA08 <> "" Then
                    dgOrdenes.Splits(0).DisplayColumns("TALL8").AllowFocus = True
                Else
                    dgOrdenes.Splits(0).DisplayColumns("TALL8").AllowFocus = False
                End If
                If .TALLA09 <> "" Then
                    dgOrdenes.Splits(0).DisplayColumns("TALL9").AllowFocus = True
                Else
                    dgOrdenes.Splits(0).DisplayColumns("TALL9").AllowFocus = False
                End If
                If .TALLA10 <> "" Then
                    dgOrdenes.Splits(0).DisplayColumns("TALL10").AllowFocus = True
                Else
                    dgOrdenes.Splits(0).DisplayColumns("TALL10").AllowFocus = False
                End If
            End With

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Private Sub dgOrdenes_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgOrdenes.GotFocus
        Try
            If PuedoModificar() Then
                cargando = True
                PonerTallasDetalleOrdenModificables()
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub dgOrdenes_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgOrdenes.RowColChange
        Try
            If PuedoModificar() Then
                cargando = True
                'PonerTallasDetalleOrdenModificables()
                dgOrdenes.Splits(0).DisplayColumns("QUANTITAT").DataColumn.FooterText = sumaTotal("TALL1", PAFActual.detallePAF.dvForm) + sumaTotal("TALL2", PAFActual.detallePAF.dvForm) + _
                    sumaTotal("TALL3", PAFActual.detallePAF.dvForm) + sumaTotal("TALL4", PAFActual.detallePAF.dvForm) + _
                    sumaTotal("TALL5", PAFActual.detallePAF.dvForm) + sumaTotal("TALL6", PAFActual.detallePAF.dvForm) + _
                    sumaTotal("TALL7", PAFActual.detallePAF.dvForm) + sumaTotal("TALL8", PAFActual.detallePAF.dvForm) + _
                    sumaTotal("TALL9", PAFActual.detallePAF.dvForm) + sumaTotal("TALL10", PAFActual.detallePAF.dvForm)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "SELECCION REGISTRO"

    Private Sub VerPAFDeCliente_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoVerPAFDeCliente.CheckedChanged
        Try
            'Hay que rellenar el combo donde podemos elegir al cliente
            'Cargar Los datos de ese cliente, podemos hacer un dataview y ya esta!
            'Dim dvClientes as New DataView(ds.Tables(tablaVentas), "CLIENT = """ & ObtenerCodigoCliente(cboSeleccionClienteParaPAF.Text) & """ ",
            'En ds.tables(tablaclientes) tengo cargados todos los PAF de venta de este tipo y PAFActual.documento, lo que tengo que hacer para añadirlos
            'es poner todos los "CLIENT" de esta tabla en el combo
            If Not cargando And consulta() Then
                cargando = True
                If rdoVerPAFDeCliente.Checked = True Then
                    cboSeleccionClienteParaPAF.Visible = True
                    lblNumeroPAF.Visible = True
                    AñadirBindingCombo(cboSeleccionClienteParaPAF, PAFActual.dtClientConPAF, CCClients, CNClients)
                    cboSeleccionClienteParaPAF.Focus()
                Else
                    lblNumeroPAF.Text = rm.GetString("NUMORDENES") & " " & PAFActual.dvIdentificadores.Count
                    cboSeleccionClienteParaPAF.Visible = False
                    lblNumeroPAF.Visible = False
                End If
                PSBTIPO(PAFActual.centro) : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub rdoVerTodasPAF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoVerTodasPAF.CheckedChanged
        Try
            If Not cargando And consulta() Then
                cargando = True
                If rdoVerTodasPAF.Checked = True Then
                    PAFActual.clienteElegido = -1
                    PAFActual.numeroRegistros = PAFActual.obtenerNumeroReg(PAFActual.tabla.TableName, "")
                    PAFActual.UltimoReg() : ActualizarOrigen()
                    AutosizeColumnasCombo(cboID)
                End If
                cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Private Sub cboSeleccionClienteParaPAF_SelChange(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSeleccionClienteParaPAF.SelectedValueChanged
        Try
            If consulta() Then
                cargando = True
                PAFActual.ElegirCliente(nzn(cboSeleccionClienteParaPAF.WillChangeToValue, 0))
                AñadirBindingCombo(cboID, PAFActual.dvIdentificadores, "ORDRE", "ORDRE")
                lblNumeroPAF.Text = rm.GetString("NUMORDENES") & " " & PAFActual.dvIdentificadores.Count
                MoverActual()
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

    Private Sub dgOrdenes_ButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgOrdenes.ButtonClick
        Dim cambios As Boolean = False
        Try
            If PuedoModificar() Then
                cargando = True

                Select Case e.Column.DataColumn.DataField
                    Case "TRASPASARLINEA"
                        Dim p As Point
                        p = ObtenerLocalizacion(dgOrdenes)
                        p.Y = p.Y + dgOrdenes.Splits(0).DisplayColumns(0).CellTop
                        PAFActual.TraspasarLinea(p)
                End Select
            End If
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub dgordenes_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgOrdenes.BeforeDelete
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
    Private Sub dgAnulaciones_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgAnulaciones.BeforeDelete
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
    Private Sub dgProcesos_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgProcesos.BeforeDelete
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

#Region "CONSUMOS"
    Private Sub dgConsumosModelo_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgConsumosModelo.BeforeDelete
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


    Private Sub btnGenerarConsumos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerarConsumos.Click
        Dim aOrdenes As New ArrayList
        Dim i As Integer
        Try
            For i = 0 To lstOrdenes.SelectedIndices.Count - 1
                aOrdenes.Add(lstOrdenes.GetItemText(lstOrdenes.SelectedIndices(i), "ORDRE"))
            Next

            PAFActual.GenerarConsumos(aOrdenes)
            dgConsumosModelo.SetDataBinding(PAFActual.dtConsumos, "")
            IniciarDGConsumos()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
#End Region

    Private Sub btnGenerarComandesCompra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerarComandesCompra.Click

        Try
            PAFActual.GenerarPedidosCompra()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub cboAOrden_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAOrden.SelectedValueChanged, cboDeOrden.SelectedValueChanged
        Try
            DirectCast(lstOrdenes.DataSource, DataTable).DefaultView.RowFilter = "ORDRE >= '" & nzn(cboDeOrden.SelectedValue, 0) & "' AND ORDRE <= '" & nzn(cboAOrden.SelectedValue, 9) & "'"

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

End Class

