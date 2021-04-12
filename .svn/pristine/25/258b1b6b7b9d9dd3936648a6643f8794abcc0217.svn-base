Imports Microsoft.Office.Interop
Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Imports C1.Win.C1TrueDBGrid
Imports System.Drawing.Printing
Imports C1.Win.C1PrintPreview

Public Class frmTejidos
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


    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents lblTejedor As Label
    Friend WithEvents lblEstampador As Label
    Friend WithEvents lblAcabador As Label
    Friend WithEvents btnElegirAcabador As C1.Win.C1Input.C1Button
    Friend WithEvents btnElegirEstampador As C1.Win.C1Input.C1Button
    Friend WithEvents btnElegirTejidor As C1.Win.C1Input.C1Button
    Friend WithEvents lblCodigoTejidoEscandallo As Label
    Friend WithEvents lblTotalMateria As Label
    Friend WithEvents lblStockCru As Label
    Friend WithEvents lblStockDisposat As Label
    Friend WithEvents lblStockCruM As Label
    Friend WithEvents lblStockDisipM As Label
    Friend WithEvents lblStockCruK As Label
    Friend WithEvents lblStockDisposatK As Label

    Private WithEvents lblIVA As Label
    Private WithEvents lblRendimiento As Label
    Private WithEvents lblPreciokg As Label
    Private WithEvents lblTantoPorCientoMargen As Label
    Private WithEvents lblPrecioMetro As Label
    Private WithEvents lblAncho2 As Label
    Private WithEvents lblNumeroHilo As Label
    Private WithEvents lblGramaje As Label
    Private WithEvents lblNombre As Label
    Private WithEvents lblCodigoTejido As Label
    Private WithEvents gb2 As System.Windows.Forms.GroupBox

    Private WithEvents XpGroupBox1 As System.Windows.Forms.GroupBox 'Dacris.System.Windows.Forms.GroupBox
    Private WithEvents lblPrecioTejidoParaModelos As Label
    Friend WithEvents btnElegirTejido As C1.Win.C1Input.C1Button


    Friend WithEvents tpTejido As System.Windows.Forms.TabPage
    Friend WithEvents tpEscandalloTejido As System.Windows.Forms.TabPage
    Friend WithEvents lblNumeroTejidos As C1.Win.C1Input.C1Label
    Friend WithEvents cboID As C1.Win.C1List.C1Combo
    Friend WithEvents dgCartaColores As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents dgComposcionTejido As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Private WithEvents txtPREUPERMODEL As C1.Win.C1Input.C1TextBox
    Private WithEvents comboIVA As C1.Win.C1List.C1Combo
    Private WithEvents txtNRO As C1.Win.C1Input.C1TextBox
    Private WithEvents txtAMPLENOU As C1.Win.C1Input.C1TextBox
    Private WithEvents txtPREUM As C1.Win.C1Input.C1TextBox
    Private WithEvents txtMAQUI As C1.Win.C1Input.C1TextBox
    Private WithEvents txtDESCRI As C1.Win.C1Input.C1TextBox
    Private WithEvents txtRENDIMENT As C1.Win.C1Input.C1TextBox
    Private WithEvents txtMARGE As C1.Win.C1Input.C1TextBox
    Private WithEvents txtGRAMA As C1.Win.C1Input.C1TextBox
    Private WithEvents txtPREUK As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboHilos As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents txtTEIXIDOR As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtACABADOR As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtESTAMPADOR As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtMATERIA As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtPACA As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtPESTAM As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtPTEIXIR As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCRU As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx2CODI As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx2DESCRI As C1.Win.C1Input.C1TextBox
    Private WithEvents txtOBSERV As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtSTDISPK As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtSTDISPM As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboNOMMAQUI As C1.Win.C1List.C1Combo
    Friend WithEvents cboNOMTEIXIDOR As C1.Win.C1List.C1Combo
    Friend WithEvents cboNOMESTAMPADOR As C1.Win.C1List.C1Combo
    Friend WithEvents cboNOMACABADOR As C1.Win.C1List.C1Combo
    Friend WithEvents tabImpresioFitxa As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnGenerarEtiquetas As C1.Win.C1Input.C1Button
    Friend WithEvents txtNumEtiquetas As C1.Win.C1Input.C1TextBox
    Friend WithEvents gbImpresionEtiquetas As System.Windows.Forms.GroupBox
    Friend WithEvents gbFichaTecnica As System.Windows.Forms.GroupBox
    Friend WithEvents txtACABAT As C1.Win.C1Input.C1TextBox
    Friend WithEvents btnLimpiarAcabados As C1.Win.C1Input.C1Button
    Friend WithEvents dgAcabados As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents cboAcabados As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents txtSTCRUK As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtSTCRUM As C1.Win.C1Input.C1TextBox
    Friend WithEvents lblMAQUI As System.Windows.Forms.Label
    Friend WithEvents btnElegirMaquina As C1.Win.C1Input.C1Button
    Friend WithEvents tabPageValoracion As System.Windows.Forms.TabPage
    Friend WithEvents dgValoracion As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents btnExportarPDF As C1.Win.C1Input.C1Button
    Friend WithEvents ppv As C1.Win.C1Preview.C1PrintPreviewControl
    Friend WithEvents ppFitxa As C1.Win.C1Preview.C1PrintPreviewControl
    Friend WithEvents C1Combo1 As C1.Win.C1List.C1Combo
    Friend WithEvents chkTubular As CheckBox
    Private WithEvents txtAMPLE As C1.Win.C1Input.C1TextBox
    Private WithEvents lblAncho As Label
    Private WithEvents txtComposicio As C1.Win.C1Input.C1TextBox
    Private WithEvents lblComposicio As Label
    Private WithEvents txtNCCODE As C1.Win.C1Input.C1TextBox
    Private WithEvents lblNCCODE As Label
    Private WithEvents lblMLineal As Label
    Private WithEvents txtMLINEAL As C1.Win.C1Input.C1TextBox
    Friend WithEvents btnExportarExcel As C1.Win.C1Input.C1Button


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTejidos))
        Dim Style1 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style2 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style3 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style4 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style5 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style6 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tpTejido = New System.Windows.Forms.TabPage()
        Me.C1Combo1 = New C1.Win.C1List.C1Combo()
        Me.dgCartaColores = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.lblNumeroTejidos = New C1.Win.C1Input.C1Label()
        Me.XpGroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblMLineal = New System.Windows.Forms.Label()
        Me.txtMLINEAL = New C1.Win.C1Input.C1TextBox()
        Me.txtNCCODE = New C1.Win.C1Input.C1TextBox()
        Me.lblNCCODE = New System.Windows.Forms.Label()
        Me.txtComposicio = New C1.Win.C1Input.C1TextBox()
        Me.lblComposicio = New System.Windows.Forms.Label()
        Me.txtAMPLE = New C1.Win.C1Input.C1TextBox()
        Me.lblAncho = New System.Windows.Forms.Label()
        Me.chkTubular = New System.Windows.Forms.CheckBox()
        Me.btnElegirMaquina = New C1.Win.C1Input.C1Button()
        Me.lblMAQUI = New System.Windows.Forms.Label()
        Me.cboID = New C1.Win.C1List.C1Combo()
        Me.cboNOMMAQUI = New C1.Win.C1List.C1Combo()
        Me.btnElegirTejido = New C1.Win.C1Input.C1Button()
        Me.lblPrecioTejidoParaModelos = New System.Windows.Forms.Label()
        Me.txtPREUPERMODEL = New C1.Win.C1Input.C1TextBox()
        Me.comboIVA = New C1.Win.C1List.C1Combo()
        Me.lblNumeroHilo = New System.Windows.Forms.Label()
        Me.txtNRO = New C1.Win.C1Input.C1TextBox()
        Me.txtAMPLENOU = New C1.Win.C1Input.C1TextBox()
        Me.lblNombre = New System.Windows.Forms.Label()
        Me.txtPREUM = New C1.Win.C1Input.C1TextBox()
        Me.txtMAQUI = New C1.Win.C1Input.C1TextBox()
        Me.lblCodigoTejido = New System.Windows.Forms.Label()
        Me.txtDESCRI = New C1.Win.C1Input.C1TextBox()
        Me.lblAncho2 = New System.Windows.Forms.Label()
        Me.lblIVA = New System.Windows.Forms.Label()
        Me.lblRendimiento = New System.Windows.Forms.Label()
        Me.txtRENDIMENT = New C1.Win.C1Input.C1TextBox()
        Me.lblGramaje = New System.Windows.Forms.Label()
        Me.lblPrecioMetro = New System.Windows.Forms.Label()
        Me.txtMARGE = New C1.Win.C1Input.C1TextBox()
        Me.lblPreciokg = New System.Windows.Forms.Label()
        Me.lblTantoPorCientoMargen = New System.Windows.Forms.Label()
        Me.txtGRAMA = New C1.Win.C1Input.C1TextBox()
        Me.txtPREUK = New C1.Win.C1Input.C1TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtOBSERV = New C1.Win.C1Input.C1TextBox()
        Me.gb2 = New System.Windows.Forms.GroupBox()
        Me.txtSTCRUK = New C1.Win.C1Input.C1TextBox()
        Me.txtSTCRUM = New C1.Win.C1Input.C1TextBox()
        Me.lblStockDisposatK = New System.Windows.Forms.Label()
        Me.txtSTDISPK = New C1.Win.C1Input.C1TextBox()
        Me.lblStockCruK = New System.Windows.Forms.Label()
        Me.lblStockDisipM = New System.Windows.Forms.Label()
        Me.lblStockCruM = New System.Windows.Forms.Label()
        Me.txtSTDISPM = New C1.Win.C1Input.C1TextBox()
        Me.lblStockDisposat = New System.Windows.Forms.Label()
        Me.lblStockCru = New System.Windows.Forms.Label()
        Me.tpEscandalloTejido = New System.Windows.Forms.TabPage()
        Me.cboAcabados = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.dgAcabados = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.btnLimpiarAcabados = New C1.Win.C1Input.C1Button()
        Me.txtACABAT = New C1.Win.C1Input.C1TextBox()
        Me.cboHilos = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.cboNOMACABADOR = New C1.Win.C1List.C1Combo()
        Me.dgComposcionTejido = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.cboNOMESTAMPADOR = New C1.Win.C1List.C1Combo()
        Me.cboNOMTEIXIDOR = New C1.Win.C1List.C1Combo()
        Me.txtCRU = New C1.Win.C1Input.C1TextBox()
        Me.txtPACA = New C1.Win.C1Input.C1TextBox()
        Me.txtPESTAM = New C1.Win.C1Input.C1TextBox()
        Me.txtPTEIXIR = New C1.Win.C1Input.C1TextBox()
        Me.lblTotalMateria = New System.Windows.Forms.Label()
        Me.txtMATERIA = New C1.Win.C1Input.C1TextBox()
        Me.btnElegirAcabador = New C1.Win.C1Input.C1Button()
        Me.btnElegirEstampador = New C1.Win.C1Input.C1Button()
        Me.btnElegirTejidor = New C1.Win.C1Input.C1Button()
        Me.lblAcabador = New System.Windows.Forms.Label()
        Me.lblEstampador = New System.Windows.Forms.Label()
        Me.lblTejedor = New System.Windows.Forms.Label()
        Me.txtACABADOR = New C1.Win.C1Input.C1TextBox()
        Me.txtESTAMPADOR = New C1.Win.C1Input.C1TextBox()
        Me.txtTEIXIDOR = New C1.Win.C1Input.C1TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.tx2CODI = New C1.Win.C1Input.C1TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCodigoTejidoEscandallo = New System.Windows.Forms.Label()
        Me.tx2DESCRI = New C1.Win.C1Input.C1TextBox()
        Me.tabImpresioFitxa = New System.Windows.Forms.TabPage()
        Me.gbImpresionEtiquetas = New System.Windows.Forms.GroupBox()
        Me.ppv = New C1.Win.C1Preview.C1PrintPreviewControl()
        Me.btnGenerarEtiquetas = New C1.Win.C1Input.C1Button()
        Me.txtNumEtiquetas = New C1.Win.C1Input.C1TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbFichaTecnica = New System.Windows.Forms.GroupBox()
        Me.ppFitxa = New C1.Win.C1Preview.C1PrintPreviewControl()
        Me.tabPageValoracion = New System.Windows.Forms.TabPage()
        Me.btnExportarExcel = New C1.Win.C1Input.C1Button()
        Me.btnExportarPDF = New C1.Win.C1Input.C1Button()
        Me.dgValoracion = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        CType(Me.btnRecargar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSiguiente, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAnterior, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrimero, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUltimo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnModificar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnTancar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBorrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNuevo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnActualizar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnVerLista, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.tpTejido.SuspendLayout()
        CType(Me.C1Combo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgCartaColores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNumeroTejidos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XpGroupBox1.SuspendLayout()
        CType(Me.txtMLINEAL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNCCODE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComposicio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAMPLE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnElegirMaquina, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMMAQUI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnElegirTejido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPREUPERMODEL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNRO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAMPLENOU, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPREUM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMAQUI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDESCRI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRENDIMENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMARGE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGRAMA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPREUK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gb2.SuspendLayout()
        CType(Me.txtSTCRUK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSTCRUM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSTDISPK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSTDISPM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpEscandalloTejido.SuspendLayout()
        CType(Me.cboAcabados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgAcabados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnLimpiarAcabados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtACABAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboHilos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMACABADOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgComposcionTejido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMESTAMPADOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMTEIXIDOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCRU, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPACA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPESTAM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPTEIXIR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMATERIA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnElegirAcabador, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnElegirEstampador, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnElegirTejidor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtACABADOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtESTAMPADOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTEIXIDOR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.tx2CODI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx2DESCRI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabImpresioFitxa.SuspendLayout()
        Me.gbImpresionEtiquetas.SuspendLayout()
        CType(Me.ppv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ppv.PreviewPane, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ppv.SuspendLayout()
        CType(Me.btnGenerarEtiquetas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNumEtiquetas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbFichaTecnica.SuspendLayout()
        CType(Me.ppFitxa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ppFitxa.PreviewPane, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ppFitxa.SuspendLayout()
        Me.tabPageValoracion.SuspendLayout()
        CType(Me.btnExportarExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExportarPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgValoracion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(5, 210)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(35, 121)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(317, 121)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(3, 121)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(3, 98)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(317, 98)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(135, 98)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(632, 186)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(135, 121)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(235, 98)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(35, 98)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(632, 166)
        '
        'cboSeleccionCentro
        '
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tpTejido)
        Me.TabControl1.Controls.Add(Me.tpEscandalloTejido)
        Me.TabControl1.Controls.Add(Me.tabImpresioFitxa)
        Me.TabControl1.Controls.Add(Me.tabPageValoracion)
        Me.TabControl1.ItemSize = New System.Drawing.Size(42, 18)
        Me.TabControl1.Location = New System.Drawing.Point(8, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(860, 630)
        Me.TabControl1.TabIndex = 0
        '
        'tpTejido
        '
        Me.tpTejido.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(196, Byte), Integer))
        Me.tpTejido.Controls.Add(Me.C1Combo1)
        Me.tpTejido.Controls.Add(Me.dgCartaColores)
        Me.tpTejido.Controls.Add(Me.lblNumeroTejidos)
        Me.tpTejido.Controls.Add(Me.XpGroupBox1)
        Me.tpTejido.Controls.Add(Me.Label2)
        Me.tpTejido.Controls.Add(Me.txtOBSERV)
        Me.tpTejido.Controls.Add(Me.gb2)
        Me.tpTejido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.tpTejido.Location = New System.Drawing.Point(4, 22)
        Me.tpTejido.Name = "tpTejido"
        Me.tpTejido.Size = New System.Drawing.Size(852, 604)
        Me.tpTejido.TabIndex = 0
        Me.tpTejido.Text = "Teixit"
        '
        'C1Combo1
        '
        Me.C1Combo1.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.C1Combo1.Caption = ""
        Me.C1Combo1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.C1Combo1.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.C1Combo1.EditorBackColor = System.Drawing.SystemColors.Window
        Me.C1Combo1.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.C1Combo1.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.C1Combo1.Images.Add(CType(resources.GetObject("C1Combo1.Images"), System.Drawing.Image))
        Me.C1Combo1.Location = New System.Drawing.Point(620, 324)
        Me.C1Combo1.MatchEntryTimeout = CType(2000, Long)
        Me.C1Combo1.MaxDropDownItems = CType(5, Short)
        Me.C1Combo1.MaxLength = 32767
        Me.C1Combo1.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.C1Combo1.Name = "C1Combo1"
        Me.C1Combo1.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.C1Combo1.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.C1Combo1.Size = New System.Drawing.Size(121, 21)
        Me.C1Combo1.TabIndex = 286
        Me.C1Combo1.Text = "C1Combo1"
        Me.C1Combo1.Visible = False
        Me.C1Combo1.PropBag = resources.GetString("C1Combo1.PropBag")
        '
        'dgCartaColores
        '
        Me.dgCartaColores.AllowAddNew = True
        Me.dgCartaColores.AllowDelete = True
        Me.dgCartaColores.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgCartaColores.Caption = "COLORS PER TIPUS DE TEIXIT"
        Me.dgCartaColores.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgCartaColores.Images.Add(CType(resources.GetObject("dgCartaColores.Images"), System.Drawing.Image))
        Me.dgCartaColores.Location = New System.Drawing.Point(4, 358)
        Me.dgCartaColores.Name = "dgCartaColores"
        Me.dgCartaColores.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgCartaColores.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgCartaColores.PreviewInfo.ZoomFactor = 75.0R
        Me.dgCartaColores.PrintInfo.PageSettings = CType(resources.GetObject("dgCartaColores.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgCartaColores.Size = New System.Drawing.Size(844, 240)
        Me.dgCartaColores.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation
        Me.dgCartaColores.TabIndex = 285
        Me.dgCartaColores.Text = "C1TrueDBGrid2"
        Me.dgCartaColores.UseCompatibleTextRendering = False
        Me.dgCartaColores.WrapCellPointer = True
        Me.dgCartaColores.PropBag = resources.GetString("dgCartaColores.PropBag")
        '
        'lblNumeroTejidos
        '
        Me.lblNumeroTejidos.AutoSize = True
        Me.lblNumeroTejidos.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblNumeroTejidos.ForeColor = System.Drawing.Color.Red
        Me.lblNumeroTejidos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNumeroTejidos.Location = New System.Drawing.Point(700, 4)
        Me.lblNumeroTejidos.Name = "lblNumeroTejidos"
        Me.lblNumeroTejidos.Size = New System.Drawing.Size(0, 13)
        Me.lblNumeroTejidos.TabIndex = 284
        Me.lblNumeroTejidos.Tag = Nothing
        Me.lblNumeroTejidos.TextDetached = True
        '
        'XpGroupBox1
        '
        Me.XpGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(196, Byte), Integer))
        Me.XpGroupBox1.Controls.Add(Me.lblMLineal)
        Me.XpGroupBox1.Controls.Add(Me.txtMLINEAL)
        Me.XpGroupBox1.Controls.Add(Me.txtNCCODE)
        Me.XpGroupBox1.Controls.Add(Me.lblNCCODE)
        Me.XpGroupBox1.Controls.Add(Me.txtComposicio)
        Me.XpGroupBox1.Controls.Add(Me.lblComposicio)
        Me.XpGroupBox1.Controls.Add(Me.txtAMPLE)
        Me.XpGroupBox1.Controls.Add(Me.lblAncho)
        Me.XpGroupBox1.Controls.Add(Me.chkTubular)
        Me.XpGroupBox1.Controls.Add(Me.btnElegirMaquina)
        Me.XpGroupBox1.Controls.Add(Me.lblMAQUI)
        Me.XpGroupBox1.Controls.Add(Me.cboID)
        Me.XpGroupBox1.Controls.Add(Me.cboNOMMAQUI)
        Me.XpGroupBox1.Controls.Add(Me.btnElegirTejido)
        Me.XpGroupBox1.Controls.Add(Me.lblPrecioTejidoParaModelos)
        Me.XpGroupBox1.Controls.Add(Me.txtPREUPERMODEL)
        Me.XpGroupBox1.Controls.Add(Me.comboIVA)
        Me.XpGroupBox1.Controls.Add(Me.lblNumeroHilo)
        Me.XpGroupBox1.Controls.Add(Me.txtNRO)
        Me.XpGroupBox1.Controls.Add(Me.txtAMPLENOU)
        Me.XpGroupBox1.Controls.Add(Me.lblNombre)
        Me.XpGroupBox1.Controls.Add(Me.txtPREUM)
        Me.XpGroupBox1.Controls.Add(Me.txtMAQUI)
        Me.XpGroupBox1.Controls.Add(Me.lblCodigoTejido)
        Me.XpGroupBox1.Controls.Add(Me.txtDESCRI)
        Me.XpGroupBox1.Controls.Add(Me.lblAncho2)
        Me.XpGroupBox1.Controls.Add(Me.lblIVA)
        Me.XpGroupBox1.Controls.Add(Me.lblRendimiento)
        Me.XpGroupBox1.Controls.Add(Me.txtRENDIMENT)
        Me.XpGroupBox1.Controls.Add(Me.lblGramaje)
        Me.XpGroupBox1.Controls.Add(Me.lblPrecioMetro)
        Me.XpGroupBox1.Controls.Add(Me.txtMARGE)
        Me.XpGroupBox1.Controls.Add(Me.lblPreciokg)
        Me.XpGroupBox1.Controls.Add(Me.lblTantoPorCientoMargen)
        Me.XpGroupBox1.Controls.Add(Me.txtGRAMA)
        Me.XpGroupBox1.Controls.Add(Me.txtPREUK)
        Me.XpGroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.XpGroupBox1.Location = New System.Drawing.Point(4, 7)
        Me.XpGroupBox1.Name = "XpGroupBox1"
        Me.XpGroupBox1.Size = New System.Drawing.Size(492, 275)
        Me.XpGroupBox1.TabIndex = 281
        Me.XpGroupBox1.TabStop = False
        '
        'lblMLineal
        '
        Me.lblMLineal.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblMLineal.Location = New System.Drawing.Point(290, 64)
        Me.lblMLineal.Name = "lblMLineal"
        Me.lblMLineal.Size = New System.Drawing.Size(88, 15)
        Me.lblMLineal.TabIndex = 295
        Me.lblMLineal.Text = "Metre Lineal"
        Me.lblMLineal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMLINEAL
        '
        Me.txtMLINEAL.Enabled = False
        Me.txtMLINEAL.Location = New System.Drawing.Point(384, 63)
        Me.txtMLINEAL.Name = "txtMLINEAL"
        Me.txtMLINEAL.Size = New System.Drawing.Size(92, 18)
        Me.txtMLINEAL.TabIndex = 294
        Me.txtMLINEAL.Tag = Nothing
        '
        'txtNCCODE
        '
        Me.txtNCCODE.Location = New System.Drawing.Point(100, 107)
        Me.txtNCCODE.MaxLength = 20
        Me.txtNCCODE.Name = "txtNCCODE"
        Me.txtNCCODE.Size = New System.Drawing.Size(119, 18)
        Me.txtNCCODE.TabIndex = 292
        Me.txtNCCODE.Tag = Nothing
        '
        'lblNCCODE
        '
        Me.lblNCCODE.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNCCODE.Location = New System.Drawing.Point(12, 110)
        Me.lblNCCODE.Name = "lblNCCODE"
        Me.lblNCCODE.Size = New System.Drawing.Size(82, 15)
        Me.lblNCCODE.TabIndex = 293
        Me.lblNCCODE.Text = "NC CODE"
        Me.lblNCCODE.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComposicio
        '
        Me.txtComposicio.Location = New System.Drawing.Point(301, 107)
        Me.txtComposicio.MaxLength = 20
        Me.txtComposicio.Name = "txtComposicio"
        Me.txtComposicio.Size = New System.Drawing.Size(175, 18)
        Me.txtComposicio.TabIndex = 290
        Me.txtComposicio.Tag = Nothing
        '
        'lblComposicio
        '
        Me.lblComposicio.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblComposicio.Location = New System.Drawing.Point(213, 110)
        Me.lblComposicio.Name = "lblComposicio"
        Me.lblComposicio.Size = New System.Drawing.Size(82, 15)
        Me.lblComposicio.TabIndex = 291
        Me.lblComposicio.Text = "Composició"
        Me.lblComposicio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAMPLE
        '
        Me.txtAMPLE.Location = New System.Drawing.Point(100, 133)
        Me.txtAMPLE.MaxLength = 20
        Me.txtAMPLE.Name = "txtAMPLE"
        Me.txtAMPLE.Size = New System.Drawing.Size(236, 18)
        Me.txtAMPLE.TabIndex = 288
        Me.txtAMPLE.Tag = Nothing
        '
        'lblAncho
        '
        Me.lblAncho.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAncho.Location = New System.Drawing.Point(12, 136)
        Me.lblAncho.Name = "lblAncho"
        Me.lblAncho.Size = New System.Drawing.Size(84, 15)
        Me.lblAncho.TabIndex = 289
        Me.lblAncho.Text = "Ample"
        Me.lblAncho.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkTubular
        '
        Me.chkTubular.AutoSize = True
        Me.chkTubular.Location = New System.Drawing.Point(353, 157)
        Me.chkTubular.Name = "chkTubular"
        Me.chkTubular.Size = New System.Drawing.Size(62, 17)
        Me.chkTubular.TabIndex = 287
        Me.chkTubular.Text = "Tubular"
        Me.chkTubular.UseVisualStyleBackColor = True
        '
        'btnElegirMaquina
        '
        Me.btnElegirMaquina.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirMaquina.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirMaquina.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirMaquina.Location = New System.Drawing.Point(144, 199)
        Me.btnElegirMaquina.Name = "btnElegirMaquina"
        Me.btnElegirMaquina.Size = New System.Drawing.Size(22, 20)
        Me.btnElegirMaquina.TabIndex = 286
        Me.btnElegirMaquina.Text = "..."
        Me.btnElegirMaquina.UseVisualStyleBackColor = True
        Me.btnElegirMaquina.Visible = False
        '
        'lblMAQUI
        '
        Me.lblMAQUI.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblMAQUI.Location = New System.Drawing.Point(12, 203)
        Me.lblMAQUI.Name = "lblMAQUI"
        Me.lblMAQUI.Size = New System.Drawing.Size(84, 16)
        Me.lblMAQUI.TabIndex = 285
        Me.lblMAQUI.Text = "Màquina"
        Me.lblMAQUI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboID
        '
        Me.cboID.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboID.AutoCompletion = True
        Me.cboID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboID.Caption = ""
        Me.cboID.CaptionHeight = 17
        Me.cboID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboID.ColumnCaptionHeight = 17
        Me.cboID.ColumnFooterHeight = 17
        Me.cboID.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboID.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboID.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboID.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboID.Images.Add(CType(resources.GetObject("cboID.Images"), System.Drawing.Image))
        Me.cboID.ItemHeight = 15
        Me.cboID.Location = New System.Drawing.Point(100, 16)
        Me.cboID.MatchEntryTimeout = CType(2000, Long)
        Me.cboID.MaxDropDownItems = CType(15, Short)
        Me.cboID.MaxLength = 10
        Me.cboID.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboID.Name = "cboID"
        Me.cboID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboID.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboID.Size = New System.Drawing.Size(148, 19)
        Me.cboID.TabIndex = 0
        Me.cboID.PropBag = resources.GetString("cboID.PropBag")
        '
        'cboNOMMAQUI
        '
        Me.cboNOMMAQUI.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMMAQUI.AutoSelect = True
        Me.cboNOMMAQUI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMMAQUI.Caption = ""
        Me.cboNOMMAQUI.CaptionHeight = 17
        Me.cboNOMMAQUI.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMMAQUI.ColumnCaptionHeight = 17
        Me.cboNOMMAQUI.ColumnFooterHeight = 17
        Me.cboNOMMAQUI.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMMAQUI.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMMAQUI.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMMAQUI.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMMAQUI.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMMAQUI.Images.Add(CType(resources.GetObject("cboNOMMAQUI.Images"), System.Drawing.Image))
        Me.cboNOMMAQUI.IntegralHeight = True
        Me.cboNOMMAQUI.ItemHeight = 13
        Me.cboNOMMAQUI.Location = New System.Drawing.Point(168, 199)
        Me.cboNOMMAQUI.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMMAQUI.MaxDropDownItems = CType(8, Short)
        Me.cboNOMMAQUI.MaxLength = 0
        Me.cboNOMMAQUI.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMMAQUI.Name = "cboNOMMAQUI"
        Me.cboNOMMAQUI.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMMAQUI.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMMAQUI.Size = New System.Drawing.Size(316, 19)
        Me.cboNOMMAQUI.TabIndex = 11
        Me.cboNOMMAQUI.PropBag = resources.GetString("cboNOMMAQUI.PropBag")
        '
        'btnElegirTejido
        '
        Me.btnElegirTejido.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirTejido.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirTejido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirTejido.Location = New System.Drawing.Point(248, 16)
        Me.btnElegirTejido.Name = "btnElegirTejido"
        Me.btnElegirTejido.Size = New System.Drawing.Size(32, 20)
        Me.btnElegirTejido.TabIndex = 281
        Me.btnElegirTejido.Text = "..."
        Me.btnElegirTejido.UseVisualStyleBackColor = True
        Me.btnElegirTejido.Visible = False
        '
        'lblPrecioTejidoParaModelos
        '
        Me.lblPrecioTejidoParaModelos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPrecioTejidoParaModelos.Location = New System.Drawing.Point(340, 227)
        Me.lblPrecioTejidoParaModelos.Name = "lblPrecioTejidoParaModelos"
        Me.lblPrecioTejidoParaModelos.Size = New System.Drawing.Size(72, 16)
        Me.lblPrecioTejidoParaModelos.TabIndex = 280
        Me.lblPrecioTejidoParaModelos.Text = "Preu Model"
        Me.lblPrecioTejidoParaModelos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPREUPERMODEL
        '
        Me.txtPREUPERMODEL.Location = New System.Drawing.Point(412, 223)
        Me.txtPREUPERMODEL.Name = "txtPREUPERMODEL"
        Me.txtPREUPERMODEL.Size = New System.Drawing.Size(60, 18)
        Me.txtPREUPERMODEL.TabIndex = 14
        Me.txtPREUPERMODEL.Tag = Nothing
        '
        'comboIVA
        '
        Me.comboIVA.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.comboIVA.AutoSelect = True
        Me.comboIVA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.comboIVA.Caption = ""
        Me.comboIVA.CaptionHeight = 17
        Me.comboIVA.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.comboIVA.ColumnCaptionHeight = 17
        Me.comboIVA.ColumnFooterHeight = 17
        Me.comboIVA.Cursor = System.Windows.Forms.Cursors.Default
        Me.comboIVA.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.comboIVA.EditorBackColor = System.Drawing.SystemColors.Window
        Me.comboIVA.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.comboIVA.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.comboIVA.Images.Add(CType(resources.GetObject("comboIVA.Images"), System.Drawing.Image))
        Me.comboIVA.IntegralHeight = True
        Me.comboIVA.ItemHeight = 13
        Me.comboIVA.Location = New System.Drawing.Point(100, 247)
        Me.comboIVA.MatchEntryTimeout = CType(100, Long)
        Me.comboIVA.MaxDropDownItems = CType(8, Short)
        Me.comboIVA.MaxLength = 0
        Me.comboIVA.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.comboIVA.Name = "comboIVA"
        Me.comboIVA.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.comboIVA.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.comboIVA.Size = New System.Drawing.Size(104, 19)
        Me.comboIVA.TabIndex = 15
        Me.comboIVA.PropBag = resources.GetString("comboIVA.PropBag")
        '
        'lblNumeroHilo
        '
        Me.lblNumeroHilo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNumeroHilo.Location = New System.Drawing.Point(12, 89)
        Me.lblNumeroHilo.Name = "lblNumeroHilo"
        Me.lblNumeroHilo.Size = New System.Drawing.Size(84, 15)
        Me.lblNumeroHilo.TabIndex = 270
        Me.lblNumeroHilo.Text = "Nro de fil"
        Me.lblNumeroHilo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNRO
        '
        Me.txtNRO.Location = New System.Drawing.Point(100, 85)
        Me.txtNRO.MaxLength = 50
        Me.txtNRO.Name = "txtNRO"
        Me.txtNRO.Size = New System.Drawing.Size(236, 18)
        Me.txtNRO.TabIndex = 5
        Me.txtNRO.Tag = Nothing
        '
        'txtAMPLENOU
        '
        Me.txtAMPLENOU.Location = New System.Drawing.Point(100, 155)
        Me.txtAMPLENOU.MaxLength = 20
        Me.txtAMPLENOU.Name = "txtAMPLENOU"
        Me.txtAMPLENOU.Size = New System.Drawing.Size(236, 18)
        Me.txtAMPLENOU.TabIndex = 7
        Me.txtAMPLENOU.Tag = Nothing
        '
        'lblNombre
        '
        Me.lblNombre.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNombre.Location = New System.Drawing.Point(12, 44)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(84, 13)
        Me.lblNombre.TabIndex = 268
        Me.lblNombre.Text = "Descripció"
        Me.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPREUM
        '
        Me.txtPREUM.Location = New System.Drawing.Point(100, 223)
        Me.txtPREUM.Name = "txtPREUM"
        Me.txtPREUM.Size = New System.Drawing.Size(68, 18)
        Me.txtPREUM.TabIndex = 12
        Me.txtPREUM.Tag = Nothing
        '
        'txtMAQUI
        '
        Me.txtMAQUI.Location = New System.Drawing.Point(100, 199)
        Me.txtMAQUI.Name = "txtMAQUI"
        Me.txtMAQUI.Size = New System.Drawing.Size(40, 18)
        Me.txtMAQUI.TabIndex = 10
        Me.txtMAQUI.Tag = Nothing
        '
        'lblCodigoTejido
        '
        Me.lblCodigoTejido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCodigoTejido.Location = New System.Drawing.Point(12, 20)
        Me.lblCodigoTejido.Name = "lblCodigoTejido"
        Me.lblCodigoTejido.Size = New System.Drawing.Size(84, 16)
        Me.lblCodigoTejido.TabIndex = 267
        Me.lblCodigoTejido.Text = "Codi Teixit"
        Me.lblCodigoTejido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDESCRI
        '
        Me.txtDESCRI.Location = New System.Drawing.Point(100, 40)
        Me.txtDESCRI.Name = "txtDESCRI"
        Me.txtDESCRI.Size = New System.Drawing.Size(376, 18)
        Me.txtDESCRI.TabIndex = 2
        Me.txtDESCRI.Tag = Nothing
        '
        'lblAncho2
        '
        Me.lblAncho2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAncho2.Location = New System.Drawing.Point(12, 158)
        Me.lblAncho2.Name = "lblAncho2"
        Me.lblAncho2.Size = New System.Drawing.Size(84, 15)
        Me.lblAncho2.TabIndex = 271
        Me.lblAncho2.Text = "Ample nou"
        Me.lblAncho2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIVA
        '
        Me.lblIVA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblIVA.Location = New System.Drawing.Point(40, 251)
        Me.lblIVA.Name = "lblIVA"
        Me.lblIVA.Size = New System.Drawing.Size(56, 14)
        Me.lblIVA.TabIndex = 276
        Me.lblIVA.Text = "IVA"
        Me.lblIVA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRendimiento
        '
        Me.lblRendimiento.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRendimiento.Location = New System.Drawing.Point(12, 181)
        Me.lblRendimiento.Name = "lblRendimiento"
        Me.lblRendimiento.Size = New System.Drawing.Size(84, 15)
        Me.lblRendimiento.TabIndex = 275
        Me.lblRendimiento.Text = "Rendiment"
        Me.lblRendimiento.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRENDIMENT
        '
        Me.txtRENDIMENT.Location = New System.Drawing.Point(100, 177)
        Me.txtRENDIMENT.Name = "txtRENDIMENT"
        Me.txtRENDIMENT.Size = New System.Drawing.Size(68, 18)
        Me.txtRENDIMENT.TabIndex = 8
        Me.txtRENDIMENT.Tag = Nothing
        '
        'lblGramaje
        '
        Me.lblGramaje.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblGramaje.Location = New System.Drawing.Point(12, 67)
        Me.lblGramaje.Name = "lblGramaje"
        Me.lblGramaje.Size = New System.Drawing.Size(88, 15)
        Me.lblGramaje.TabIndex = 269
        Me.lblGramaje.Text = "Gramatge gr/m2"
        Me.lblGramaje.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPrecioMetro
        '
        Me.lblPrecioMetro.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPrecioMetro.Location = New System.Drawing.Point(12, 228)
        Me.lblPrecioMetro.Name = "lblPrecioMetro"
        Me.lblPrecioMetro.Size = New System.Drawing.Size(84, 15)
        Me.lblPrecioMetro.TabIndex = 272
        Me.lblPrecioMetro.Text = "Preu Metre"
        Me.lblPrecioMetro.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMARGE
        '
        Me.txtMARGE.Location = New System.Drawing.Point(260, 177)
        Me.txtMARGE.Name = "txtMARGE"
        Me.txtMARGE.Size = New System.Drawing.Size(76, 18)
        Me.txtMARGE.TabIndex = 9
        Me.txtMARGE.Tag = Nothing
        '
        'lblPreciokg
        '
        Me.lblPreciokg.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPreciokg.Location = New System.Drawing.Point(172, 227)
        Me.lblPreciokg.Name = "lblPreciokg"
        Me.lblPreciokg.Size = New System.Drawing.Size(72, 16)
        Me.lblPreciokg.TabIndex = 10
        Me.lblPreciokg.Text = "Preu quilo"
        Me.lblPreciokg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTantoPorCientoMargen
        '
        Me.lblTantoPorCientoMargen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTantoPorCientoMargen.Location = New System.Drawing.Point(172, 181)
        Me.lblTantoPorCientoMargen.Name = "lblTantoPorCientoMargen"
        Me.lblTantoPorCientoMargen.Size = New System.Drawing.Size(80, 15)
        Me.lblTantoPorCientoMargen.TabIndex = 273
        Me.lblTantoPorCientoMargen.Text = "% de Marge"
        Me.lblTantoPorCientoMargen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtGRAMA
        '
        Me.txtGRAMA.Location = New System.Drawing.Point(100, 62)
        Me.txtGRAMA.Name = "txtGRAMA"
        Me.txtGRAMA.Size = New System.Drawing.Size(68, 18)
        Me.txtGRAMA.TabIndex = 3
        Me.txtGRAMA.Tag = Nothing
        '
        'txtPREUK
        '
        Me.txtPREUK.Location = New System.Drawing.Point(244, 223)
        Me.txtPREUK.Name = "txtPREUK"
        Me.txtPREUK.Size = New System.Drawing.Size(60, 18)
        Me.txtPREUK.TabIndex = 13
        Me.txtPREUK.Tag = Nothing
        '
        'Label2
        '
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(512, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(144, 17)
        Me.Label2.TabIndex = 256
        Me.Label2.Text = "Notas"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtOBSERV
        '
        Me.txtOBSERV.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOBSERV.Location = New System.Drawing.Point(508, 24)
        Me.txtOBSERV.Multiline = True
        Me.txtOBSERV.Name = "txtOBSERV"
        Me.txtOBSERV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOBSERV.Size = New System.Drawing.Size(332, 258)
        Me.txtOBSERV.TabIndex = 0
        Me.txtOBSERV.Tag = Nothing
        '
        'gb2
        '
        Me.gb2.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(196, Byte), Integer))
        Me.gb2.Controls.Add(Me.txtSTCRUK)
        Me.gb2.Controls.Add(Me.txtSTCRUM)
        Me.gb2.Controls.Add(Me.lblStockDisposatK)
        Me.gb2.Controls.Add(Me.txtSTDISPK)
        Me.gb2.Controls.Add(Me.lblStockCruK)
        Me.gb2.Controls.Add(Me.lblStockDisipM)
        Me.gb2.Controls.Add(Me.lblStockCruM)
        Me.gb2.Controls.Add(Me.txtSTDISPM)
        Me.gb2.Controls.Add(Me.lblStockDisposat)
        Me.gb2.Controls.Add(Me.lblStockCru)
        Me.gb2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.gb2.Location = New System.Drawing.Point(4, 288)
        Me.gb2.Name = "gb2"
        Me.gb2.Size = New System.Drawing.Size(492, 63)
        Me.gb2.TabIndex = 283
        Me.gb2.TabStop = False
        '
        'txtSTCRUK
        '
        Me.txtSTCRUK.BackColor = System.Drawing.Color.LightSalmon
        Me.txtSTCRUK.Location = New System.Drawing.Point(144, 20)
        Me.txtSTCRUK.Name = "txtSTCRUK"
        Me.txtSTCRUK.Size = New System.Drawing.Size(96, 18)
        Me.txtSTCRUK.TabIndex = 293
        Me.txtSTCRUK.Tag = Nothing
        '
        'txtSTCRUM
        '
        Me.txtSTCRUM.BackColor = System.Drawing.Color.PaleTurquoise
        Me.txtSTCRUM.Location = New System.Drawing.Point(144, 40)
        Me.txtSTCRUM.Name = "txtSTCRUM"
        Me.txtSTCRUM.Size = New System.Drawing.Size(96, 18)
        Me.txtSTCRUM.TabIndex = 292
        Me.txtSTCRUM.Tag = Nothing
        '
        'lblStockDisposatK
        '
        Me.lblStockDisposatK.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblStockDisposatK.Location = New System.Drawing.Point(336, 16)
        Me.lblStockDisposatK.Name = "lblStockDisposatK"
        Me.lblStockDisposatK.Size = New System.Drawing.Size(44, 20)
        Me.lblStockDisposatK.TabIndex = 291
        Me.lblStockDisposatK.Text = "KG"
        Me.lblStockDisposatK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSTDISPK
        '
        Me.txtSTDISPK.BackColor = System.Drawing.Color.LightSalmon
        Me.txtSTDISPK.Location = New System.Drawing.Point(384, 16)
        Me.txtSTDISPK.Name = "txtSTDISPK"
        Me.txtSTDISPK.Size = New System.Drawing.Size(96, 18)
        Me.txtSTDISPK.TabIndex = 3
        Me.txtSTDISPK.Tag = Nothing
        '
        'lblStockCruK
        '
        Me.lblStockCruK.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblStockCruK.Location = New System.Drawing.Point(96, 20)
        Me.lblStockCruK.Name = "lblStockCruK"
        Me.lblStockCruK.Size = New System.Drawing.Size(40, 11)
        Me.lblStockCruK.TabIndex = 289
        Me.lblStockCruK.Text = "KG"
        Me.lblStockCruK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblStockDisipM
        '
        Me.lblStockDisipM.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblStockDisipM.Location = New System.Drawing.Point(336, 40)
        Me.lblStockDisipM.Name = "lblStockDisipM"
        Me.lblStockDisipM.Size = New System.Drawing.Size(44, 16)
        Me.lblStockDisipM.TabIndex = 287
        Me.lblStockDisipM.Text = "Metres"
        Me.lblStockDisipM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblStockCruM
        '
        Me.lblStockCruM.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblStockCruM.Location = New System.Drawing.Point(96, 40)
        Me.lblStockCruM.Name = "lblStockCruM"
        Me.lblStockCruM.Size = New System.Drawing.Size(44, 16)
        Me.lblStockCruM.TabIndex = 286
        Me.lblStockCruM.Text = "Metres"
        Me.lblStockCruM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSTDISPM
        '
        Me.txtSTDISPM.BackColor = System.Drawing.Color.PaleTurquoise
        Me.txtSTDISPM.Location = New System.Drawing.Point(384, 36)
        Me.txtSTDISPM.Name = "txtSTDISPM"
        Me.txtSTDISPM.Size = New System.Drawing.Size(96, 18)
        Me.txtSTDISPM.TabIndex = 2
        Me.txtSTDISPM.Tag = Nothing
        '
        'lblStockDisposat
        '
        Me.lblStockDisposat.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblStockDisposat.Location = New System.Drawing.Point(248, 28)
        Me.lblStockDisposat.Name = "lblStockDisposat"
        Me.lblStockDisposat.Size = New System.Drawing.Size(84, 16)
        Me.lblStockDisposat.TabIndex = 285
        Me.lblStockDisposat.Text = "Stock Disposat"
        Me.lblStockDisposat.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblStockCru
        '
        Me.lblStockCru.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblStockCru.Location = New System.Drawing.Point(8, 28)
        Me.lblStockCru.Name = "lblStockCru"
        Me.lblStockCru.Size = New System.Drawing.Size(84, 16)
        Me.lblStockCru.TabIndex = 284
        Me.lblStockCru.Text = "Stock Cru"
        Me.lblStockCru.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tpEscandalloTejido
        '
        Me.tpEscandalloTejido.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(196, Byte), Integer))
        Me.tpEscandalloTejido.Controls.Add(Me.cboAcabados)
        Me.tpEscandalloTejido.Controls.Add(Me.dgAcabados)
        Me.tpEscandalloTejido.Controls.Add(Me.btnLimpiarAcabados)
        Me.tpEscandalloTejido.Controls.Add(Me.txtACABAT)
        Me.tpEscandalloTejido.Controls.Add(Me.cboHilos)
        Me.tpEscandalloTejido.Controls.Add(Me.cboNOMACABADOR)
        Me.tpEscandalloTejido.Controls.Add(Me.dgComposcionTejido)
        Me.tpEscandalloTejido.Controls.Add(Me.cboNOMESTAMPADOR)
        Me.tpEscandalloTejido.Controls.Add(Me.cboNOMTEIXIDOR)
        Me.tpEscandalloTejido.Controls.Add(Me.txtCRU)
        Me.tpEscandalloTejido.Controls.Add(Me.txtPACA)
        Me.tpEscandalloTejido.Controls.Add(Me.txtPESTAM)
        Me.tpEscandalloTejido.Controls.Add(Me.txtPTEIXIR)
        Me.tpEscandalloTejido.Controls.Add(Me.lblTotalMateria)
        Me.tpEscandalloTejido.Controls.Add(Me.txtMATERIA)
        Me.tpEscandalloTejido.Controls.Add(Me.btnElegirAcabador)
        Me.tpEscandalloTejido.Controls.Add(Me.btnElegirEstampador)
        Me.tpEscandalloTejido.Controls.Add(Me.btnElegirTejidor)
        Me.tpEscandalloTejido.Controls.Add(Me.lblAcabador)
        Me.tpEscandalloTejido.Controls.Add(Me.lblEstampador)
        Me.tpEscandalloTejido.Controls.Add(Me.lblTejedor)
        Me.tpEscandalloTejido.Controls.Add(Me.txtACABADOR)
        Me.tpEscandalloTejido.Controls.Add(Me.txtESTAMPADOR)
        Me.tpEscandalloTejido.Controls.Add(Me.txtTEIXIDOR)
        Me.tpEscandalloTejido.Controls.Add(Me.GroupBox1)
        Me.tpEscandalloTejido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.tpEscandalloTejido.Location = New System.Drawing.Point(4, 22)
        Me.tpEscandalloTejido.Name = "tpEscandalloTejido"
        Me.tpEscandalloTejido.Size = New System.Drawing.Size(852, 604)
        Me.tpEscandalloTejido.TabIndex = 1
        Me.tpEscandalloTejido.Text = "Escandall Teixit"
        '
        'cboAcabados
        '
        Me.cboAcabados.AllowColMove = True
        Me.cboAcabados.AllowColSelect = True
        Me.cboAcabados.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboAcabados.AlternatingRows = False
        Me.cboAcabados.CaptionStyle = Style1
        Me.cboAcabados.ColumnCaptionHeight = 17
        Me.cboAcabados.ColumnFooterHeight = 17
        Me.cboAcabados.EvenRowStyle = Style2
        Me.cboAcabados.ExtendRightColumn = True
        Me.cboAcabados.FetchRowStyles = False
        Me.cboAcabados.FooterStyle = Style3
        Me.cboAcabados.HeadingStyle = Style4
        Me.cboAcabados.HighLightRowStyle = Style5
        Me.cboAcabados.Images.Add(CType(resources.GetObject("cboAcabados.Images"), System.Drawing.Image))
        Me.cboAcabados.Location = New System.Drawing.Point(76, 328)
        Me.cboAcabados.Name = "cboAcabados"
        Me.cboAcabados.OddRowStyle = Style6
        Me.cboAcabados.RecordSelectorStyle = Style7
        Me.cboAcabados.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboAcabados.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboAcabados.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboAcabados.ScrollTips = False
        Me.cboAcabados.Size = New System.Drawing.Size(150, 156)
        Me.cboAcabados.Style = Style8
        Me.cboAcabados.TabIndex = 287
        Me.cboAcabados.Text = "C1TrueDBDropdown1"
        Me.cboAcabados.UseCompatibleTextRendering = False
        Me.cboAcabados.ValueTranslate = True
        Me.cboAcabados.Visible = False
        Me.cboAcabados.PropBag = resources.GetString("cboAcabados.PropBag")
        '
        'dgAcabados
        '
        Me.dgAcabados.AllowAddNew = True
        Me.dgAcabados.AllowDelete = True
        Me.dgAcabados.AllowSort = False
        Me.dgAcabados.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgAcabados.Caption = "Acabats"
        Me.dgAcabados.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgAcabados.Images.Add(CType(resources.GetObject("dgAcabados.Images"), System.Drawing.Image))
        Me.dgAcabados.Location = New System.Drawing.Point(12, 300)
        Me.dgAcabados.Name = "dgAcabados"
        Me.dgAcabados.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgAcabados.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgAcabados.PreviewInfo.ZoomFactor = 75.0R
        Me.dgAcabados.PrintInfo.PageSettings = CType(resources.GetObject("dgAcabados.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgAcabados.Size = New System.Drawing.Size(700, 176)
        Me.dgAcabados.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation
        Me.dgAcabados.TabIndex = 286
        Me.dgAcabados.Text = "Acabats"
        Me.dgAcabados.UseCompatibleTextRendering = False
        Me.dgAcabados.WrapCellPointer = True
        Me.dgAcabados.PropBag = resources.GetString("dgAcabados.PropBag")
        '
        'btnLimpiarAcabados
        '
        Me.btnLimpiarAcabados.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLimpiarAcabados.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLimpiarAcabados.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLimpiarAcabados.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnLimpiarAcabados.Location = New System.Drawing.Point(720, 448)
        Me.btnLimpiarAcabados.Name = "btnLimpiarAcabados"
        Me.btnLimpiarAcabados.Size = New System.Drawing.Size(100, 28)
        Me.btnLimpiarAcabados.TabIndex = 269
        Me.btnLimpiarAcabados.Text = "Netejar Acabats"
        Me.btnLimpiarAcabados.UseVisualStyleBackColor = True
        '
        'txtACABAT
        '
        Me.txtACABAT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtACABAT.Location = New System.Drawing.Point(12, 484)
        Me.txtACABAT.Multiline = True
        Me.txtACABAT.Name = "txtACABAT"
        Me.txtACABAT.Size = New System.Drawing.Size(700, 40)
        Me.txtACABAT.TabIndex = 267
        Me.txtACABAT.Tag = Nothing
        '
        'cboHilos
        '
        Me.cboHilos.AllowColMove = True
        Me.cboHilos.AllowColSelect = True
        Me.cboHilos.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboHilos.AlternatingRows = False
        Me.cboHilos.CaptionStyle = Style9
        Me.cboHilos.ColumnCaptionHeight = 17
        Me.cboHilos.ColumnFooterHeight = 17
        Me.cboHilos.EvenRowStyle = Style10
        Me.cboHilos.FetchRowStyles = False
        Me.cboHilos.FooterStyle = Style11
        Me.cboHilos.HeadingStyle = Style12
        Me.cboHilos.HighLightRowStyle = Style13
        Me.cboHilos.Images.Add(CType(resources.GetObject("cboHilos.Images"), System.Drawing.Image))
        Me.cboHilos.Location = New System.Drawing.Point(24, 76)
        Me.cboHilos.Name = "cboHilos"
        Me.cboHilos.OddRowStyle = Style14
        Me.cboHilos.RecordSelectorStyle = Style15
        Me.cboHilos.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboHilos.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboHilos.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboHilos.ScrollTips = False
        Me.cboHilos.Size = New System.Drawing.Size(68, 156)
        Me.cboHilos.Style = Style16
        Me.cboHilos.TabIndex = 266
        Me.cboHilos.Text = "C1TrueDBDropdown1"
        Me.cboHilos.UseCompatibleTextRendering = False
        Me.cboHilos.ValueTranslate = True
        Me.cboHilos.Visible = False
        Me.cboHilos.PropBag = resources.GetString("cboHilos.PropBag")
        '
        'cboNOMACABADOR
        '
        Me.cboNOMACABADOR.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMACABADOR.AutoSelect = True
        Me.cboNOMACABADOR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMACABADOR.Caption = ""
        Me.cboNOMACABADOR.CaptionHeight = 17
        Me.cboNOMACABADOR.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMACABADOR.ColumnCaptionHeight = 17
        Me.cboNOMACABADOR.ColumnFooterHeight = 17
        Me.cboNOMACABADOR.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMACABADOR.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMACABADOR.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMACABADOR.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMACABADOR.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMACABADOR.Images.Add(CType(resources.GetObject("cboNOMACABADOR.Images"), System.Drawing.Image))
        Me.cboNOMACABADOR.IntegralHeight = True
        Me.cboNOMACABADOR.ItemHeight = 13
        Me.cboNOMACABADOR.Location = New System.Drawing.Point(424, 276)
        Me.cboNOMACABADOR.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMACABADOR.MaxDropDownItems = CType(8, Short)
        Me.cboNOMACABADOR.MaxLength = 0
        Me.cboNOMACABADOR.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMACABADOR.Name = "cboNOMACABADOR"
        Me.cboNOMACABADOR.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMACABADOR.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMACABADOR.Size = New System.Drawing.Size(288, 19)
        Me.cboNOMACABADOR.TabIndex = 264
        Me.cboNOMACABADOR.PropBag = resources.GetString("cboNOMACABADOR.PropBag")
        '
        'dgComposcionTejido
        '
        Me.dgComposcionTejido.AllowAddNew = True
        Me.dgComposcionTejido.AllowDelete = True
        Me.dgComposcionTejido.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgComposcionTejido.Caption = "COMPOSICIÓ TEIXIT"
        Me.dgComposcionTejido.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgComposcionTejido.Images.Add(CType(resources.GetObject("dgComposcionTejido.Images"), System.Drawing.Image))
        Me.dgComposcionTejido.Location = New System.Drawing.Point(16, 52)
        Me.dgComposcionTejido.Name = "dgComposcionTejido"
        Me.dgComposcionTejido.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgComposcionTejido.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgComposcionTejido.PreviewInfo.ZoomFactor = 75.0R
        Me.dgComposcionTejido.PrintInfo.PageSettings = CType(resources.GetObject("dgComposcionTejido.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgComposcionTejido.Size = New System.Drawing.Size(812, 144)
        Me.dgComposcionTejido.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation
        Me.dgComposcionTejido.TabIndex = 263
        Me.dgComposcionTejido.Text = "C1TrueDBGrid1"
        Me.dgComposcionTejido.UseCompatibleTextRendering = False
        Me.dgComposcionTejido.WrapCellPointer = True
        Me.dgComposcionTejido.PropBag = resources.GetString("dgComposcionTejido.PropBag")
        '
        'cboNOMESTAMPADOR
        '
        Me.cboNOMESTAMPADOR.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMESTAMPADOR.AutoSelect = True
        Me.cboNOMESTAMPADOR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMESTAMPADOR.Caption = ""
        Me.cboNOMESTAMPADOR.CaptionHeight = 17
        Me.cboNOMESTAMPADOR.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMESTAMPADOR.ColumnCaptionHeight = 17
        Me.cboNOMESTAMPADOR.ColumnFooterHeight = 17
        Me.cboNOMESTAMPADOR.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMESTAMPADOR.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMESTAMPADOR.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMESTAMPADOR.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMESTAMPADOR.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMESTAMPADOR.Images.Add(CType(resources.GetObject("cboNOMESTAMPADOR.Images"), System.Drawing.Image))
        Me.cboNOMESTAMPADOR.IntegralHeight = True
        Me.cboNOMESTAMPADOR.ItemHeight = 13
        Me.cboNOMESTAMPADOR.Location = New System.Drawing.Point(424, 252)
        Me.cboNOMESTAMPADOR.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMESTAMPADOR.MaxDropDownItems = CType(8, Short)
        Me.cboNOMESTAMPADOR.MaxLength = 0
        Me.cboNOMESTAMPADOR.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMESTAMPADOR.Name = "cboNOMESTAMPADOR"
        Me.cboNOMESTAMPADOR.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMESTAMPADOR.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMESTAMPADOR.Size = New System.Drawing.Size(288, 19)
        Me.cboNOMESTAMPADOR.TabIndex = 4
        Me.cboNOMESTAMPADOR.PropBag = resources.GetString("cboNOMESTAMPADOR.PropBag")
        '
        'cboNOMTEIXIDOR
        '
        Me.cboNOMTEIXIDOR.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMTEIXIDOR.AutoSelect = True
        Me.cboNOMTEIXIDOR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMTEIXIDOR.Caption = ""
        Me.cboNOMTEIXIDOR.CaptionHeight = 17
        Me.cboNOMTEIXIDOR.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMTEIXIDOR.ColumnCaptionHeight = 17
        Me.cboNOMTEIXIDOR.ColumnFooterHeight = 17
        Me.cboNOMTEIXIDOR.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMTEIXIDOR.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMTEIXIDOR.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMTEIXIDOR.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMTEIXIDOR.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMTEIXIDOR.Images.Add(CType(resources.GetObject("cboNOMTEIXIDOR.Images"), System.Drawing.Image))
        Me.cboNOMTEIXIDOR.IntegralHeight = True
        Me.cboNOMTEIXIDOR.ItemHeight = 13
        Me.cboNOMTEIXIDOR.Location = New System.Drawing.Point(424, 228)
        Me.cboNOMTEIXIDOR.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMTEIXIDOR.MaxDropDownItems = CType(8, Short)
        Me.cboNOMTEIXIDOR.MaxLength = 0
        Me.cboNOMTEIXIDOR.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMTEIXIDOR.Name = "cboNOMTEIXIDOR"
        Me.cboNOMTEIXIDOR.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMTEIXIDOR.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMTEIXIDOR.Size = New System.Drawing.Size(288, 19)
        Me.cboNOMTEIXIDOR.TabIndex = 2
        Me.cboNOMTEIXIDOR.PropBag = resources.GetString("cboNOMTEIXIDOR.PropBag")
        '
        'txtCRU
        '
        Me.txtCRU.Location = New System.Drawing.Point(720, 508)
        Me.txtCRU.Name = "txtCRU"
        Me.txtCRU.Size = New System.Drawing.Size(112, 18)
        Me.txtCRU.TabIndex = 12
        Me.txtCRU.Tag = Nothing
        '
        'txtPACA
        '
        Me.txtPACA.Location = New System.Drawing.Point(716, 276)
        Me.txtPACA.Name = "txtPACA"
        Me.txtPACA.Size = New System.Drawing.Size(112, 18)
        Me.txtPACA.TabIndex = 10
        Me.txtPACA.Tag = Nothing
        '
        'txtPESTAM
        '
        Me.txtPESTAM.Location = New System.Drawing.Point(716, 252)
        Me.txtPESTAM.Name = "txtPESTAM"
        Me.txtPESTAM.Size = New System.Drawing.Size(112, 18)
        Me.txtPESTAM.TabIndex = 9
        Me.txtPESTAM.Tag = Nothing
        '
        'txtPTEIXIR
        '
        Me.txtPTEIXIR.Location = New System.Drawing.Point(716, 228)
        Me.txtPTEIXIR.Name = "txtPTEIXIR"
        Me.txtPTEIXIR.Size = New System.Drawing.Size(112, 18)
        Me.txtPTEIXIR.TabIndex = 8
        Me.txtPTEIXIR.Tag = Nothing
        '
        'lblTotalMateria
        '
        Me.lblTotalMateria.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTotalMateria.Location = New System.Drawing.Point(612, 204)
        Me.lblTotalMateria.Name = "lblTotalMateria"
        Me.lblTotalMateria.Size = New System.Drawing.Size(96, 16)
        Me.lblTotalMateria.TabIndex = 261
        Me.lblTotalMateria.Text = "Total Materia"
        Me.lblTotalMateria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMATERIA
        '
        Me.txtMATERIA.Location = New System.Drawing.Point(716, 200)
        Me.txtMATERIA.Name = "txtMATERIA"
        Me.txtMATERIA.Size = New System.Drawing.Size(112, 18)
        Me.txtMATERIA.TabIndex = 7
        Me.txtMATERIA.Tag = Nothing
        '
        'btnElegirAcabador
        '
        Me.btnElegirAcabador.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirAcabador.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirAcabador.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirAcabador.Location = New System.Drawing.Point(392, 276)
        Me.btnElegirAcabador.Name = "btnElegirAcabador"
        Me.btnElegirAcabador.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirAcabador.TabIndex = 8
        Me.btnElegirAcabador.Text = "..."
        Me.btnElegirAcabador.UseVisualStyleBackColor = True
        '
        'btnElegirEstampador
        '
        Me.btnElegirEstampador.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirEstampador.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirEstampador.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirEstampador.Location = New System.Drawing.Point(392, 252)
        Me.btnElegirEstampador.Name = "btnElegirEstampador"
        Me.btnElegirEstampador.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirEstampador.TabIndex = 5
        Me.btnElegirEstampador.Text = "..."
        Me.btnElegirEstampador.UseVisualStyleBackColor = True
        '
        'btnElegirTejidor
        '
        Me.btnElegirTejidor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirTejidor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirTejidor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirTejidor.Location = New System.Drawing.Point(392, 228)
        Me.btnElegirTejidor.Name = "btnElegirTejidor"
        Me.btnElegirTejidor.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirTejidor.TabIndex = 2
        Me.btnElegirTejidor.Text = "..."
        Me.btnElegirTejidor.UseVisualStyleBackColor = True
        '
        'lblAcabador
        '
        Me.lblAcabador.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAcabador.Location = New System.Drawing.Point(236, 280)
        Me.lblAcabador.Name = "lblAcabador"
        Me.lblAcabador.Size = New System.Drawing.Size(96, 16)
        Me.lblAcabador.TabIndex = 252
        Me.lblAcabador.Text = "Acabador"
        Me.lblAcabador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblEstampador
        '
        Me.lblEstampador.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblEstampador.Location = New System.Drawing.Point(236, 256)
        Me.lblEstampador.Name = "lblEstampador"
        Me.lblEstampador.Size = New System.Drawing.Size(96, 16)
        Me.lblEstampador.TabIndex = 251
        Me.lblEstampador.Text = "Estampador"
        Me.lblEstampador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTejedor
        '
        Me.lblTejedor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTejedor.Location = New System.Drawing.Point(236, 232)
        Me.lblTejedor.Name = "lblTejedor"
        Me.lblTejedor.Size = New System.Drawing.Size(96, 16)
        Me.lblTejedor.TabIndex = 250
        Me.lblTejedor.Text = "Teixidor"
        Me.lblTejedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtACABADOR
        '
        Me.txtACABADOR.Location = New System.Drawing.Point(336, 276)
        Me.txtACABADOR.Name = "txtACABADOR"
        Me.txtACABADOR.Size = New System.Drawing.Size(56, 18)
        Me.txtACABADOR.TabIndex = 5
        Me.txtACABADOR.Tag = Nothing
        '
        'txtESTAMPADOR
        '
        Me.txtESTAMPADOR.Location = New System.Drawing.Point(336, 252)
        Me.txtESTAMPADOR.Name = "txtESTAMPADOR"
        Me.txtESTAMPADOR.NullText = """"""
        Me.txtESTAMPADOR.Size = New System.Drawing.Size(56, 18)
        Me.txtESTAMPADOR.TabIndex = 3
        Me.txtESTAMPADOR.Tag = Nothing
        '
        'txtTEIXIDOR
        '
        Me.txtTEIXIDOR.Location = New System.Drawing.Point(336, 228)
        Me.txtTEIXIDOR.Name = "txtTEIXIDOR"
        Me.txtTEIXIDOR.Size = New System.Drawing.Size(56, 18)
        Me.txtTEIXIDOR.TabIndex = 1
        Me.txtTEIXIDOR.Tag = Nothing
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.tx2CODI)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblCodigoTejidoEscandallo)
        Me.GroupBox1.Controls.Add(Me.tx2DESCRI)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox1.Location = New System.Drawing.Point(16, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(608, 41)
        Me.GroupBox1.TabIndex = 243
        Me.GroupBox1.TabStop = False
        '
        'tx2CODI
        '
        Me.tx2CODI.Location = New System.Drawing.Point(92, 16)
        Me.tx2CODI.Name = "tx2CODI"
        Me.tx2CODI.Size = New System.Drawing.Size(100, 18)
        Me.tx2CODI.TabIndex = 245
        Me.tx2CODI.Tag = Nothing
        '
        'Label3
        '
        Me.Label3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Enabled = False
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(240, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 16)
        Me.Label3.TabIndex = 244
        Me.Label3.Text = "Descripció"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCodigoTejidoEscandallo
        '
        Me.lblCodigoTejidoEscandallo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCodigoTejidoEscandallo.Enabled = False
        Me.lblCodigoTejidoEscandallo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCodigoTejidoEscandallo.Location = New System.Drawing.Point(16, 19)
        Me.lblCodigoTejidoEscandallo.Name = "lblCodigoTejidoEscandallo"
        Me.lblCodigoTejidoEscandallo.Size = New System.Drawing.Size(72, 16)
        Me.lblCodigoTejidoEscandallo.TabIndex = 243
        Me.lblCodigoTejidoEscandallo.Text = "Codi Teixit"
        Me.lblCodigoTejidoEscandallo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tx2DESCRI
        '
        Me.tx2DESCRI.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tx2DESCRI.Location = New System.Drawing.Point(328, 16)
        Me.tx2DESCRI.Name = "tx2DESCRI"
        Me.tx2DESCRI.Size = New System.Drawing.Size(264, 18)
        Me.tx2DESCRI.TabIndex = 0
        Me.tx2DESCRI.Tag = Nothing
        '
        'tabImpresioFitxa
        '
        Me.tabImpresioFitxa.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(196, Byte), Integer))
        Me.tabImpresioFitxa.Controls.Add(Me.gbImpresionEtiquetas)
        Me.tabImpresioFitxa.Controls.Add(Me.gbFichaTecnica)
        Me.tabImpresioFitxa.Location = New System.Drawing.Point(4, 22)
        Me.tabImpresioFitxa.Name = "tabImpresioFitxa"
        Me.tabImpresioFitxa.Size = New System.Drawing.Size(852, 604)
        Me.tabImpresioFitxa.TabIndex = 2
        Me.tabImpresioFitxa.Text = "Impresió Fitxa - Etiquetes"
        '
        'gbImpresionEtiquetas
        '
        Me.gbImpresionEtiquetas.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbImpresionEtiquetas.Controls.Add(Me.ppv)
        Me.gbImpresionEtiquetas.Controls.Add(Me.btnGenerarEtiquetas)
        Me.gbImpresionEtiquetas.Controls.Add(Me.txtNumEtiquetas)
        Me.gbImpresionEtiquetas.Controls.Add(Me.Label1)
        Me.gbImpresionEtiquetas.Location = New System.Drawing.Point(3, 11)
        Me.gbImpresionEtiquetas.Name = "gbImpresionEtiquetas"
        Me.gbImpresionEtiquetas.Size = New System.Drawing.Size(253, 495)
        Me.gbImpresionEtiquetas.TabIndex = 246
        Me.gbImpresionEtiquetas.TabStop = False
        Me.gbImpresionEtiquetas.Text = "Impresió Etiquetes"
        '
        'ppv
        '
        Me.ppv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ppv.AvailablePreviewActions = CType(((((C1.Win.C1Preview.C1PreviewActionFlags.FileOpen Or C1.Win.C1Preview.C1PreviewActionFlags.FileSave) _
            Or C1.Win.C1Preview.C1PreviewActionFlags.PageSetup) _
            Or C1.Win.C1Preview.C1PreviewActionFlags.Print) _
            Or C1.Win.C1Preview.C1PreviewActionFlags.Reflow), C1.Win.C1Preview.C1PreviewActionFlags)
        Me.ppv.Location = New System.Drawing.Point(6, 85)
        Me.ppv.Name = "ppv"
        Me.ppv.NavigationPanelVisible = False
        '
        'ppv.PreviewPane
        '
        Me.ppv.PreviewPane.IntegrateExternalTools = True
        Me.ppv.PreviewPane.ShowToolTips = False
        Me.ppv.PreviewPane.TabIndex = 0
        Me.ppv.Size = New System.Drawing.Size(241, 387)
        Me.ppv.StatusBarVisible = False
        Me.ppv.TabIndex = 248
        Me.ppv.Text = "C1PrintPreviewControl1"
        '
        '
        '
        Me.ppv.ToolBars.File.Open.Image = CType(resources.GetObject("ppv.ToolBars.File.Open.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.File.Open.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.File.Open.Name = "btnFileOpen"
        Me.ppv.ToolBars.File.Open.Size = New System.Drawing.Size(32, 22)
        Me.ppv.ToolBars.File.Open.Tag = "C1PreviewActionEnum.FileOpen"
        Me.ppv.ToolBars.File.Open.ToolTipText = "Open File"
        '
        '
        '
        Me.ppv.ToolBars.File.PageSetup.Image = CType(resources.GetObject("ppv.ToolBars.File.PageSetup.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.File.PageSetup.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.File.PageSetup.Name = "btnPageSetup"
        Me.ppv.ToolBars.File.PageSetup.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.File.PageSetup.Tag = "C1PreviewActionEnum.PageSetup"
        Me.ppv.ToolBars.File.PageSetup.ToolTipText = "Page Setup"
        '
        '
        '
        Me.ppv.ToolBars.File.Parameters.Image = CType(resources.GetObject("ppv.ToolBars.File.Parameters.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.File.Parameters.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.File.Parameters.Name = "btnParameters"
        Me.ppv.ToolBars.File.Parameters.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.File.Parameters.Tag = "C1PreviewActionEnum.Parameters"
        Me.ppv.ToolBars.File.Parameters.ToolTipText = "Report Parameters"
        Me.ppv.ToolBars.File.Parameters.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.File.Print.Image = CType(resources.GetObject("ppv.ToolBars.File.Print.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.File.Print.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.File.Print.Name = "btnPrint"
        Me.ppv.ToolBars.File.Print.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.File.Print.Tag = "C1PreviewActionEnum.Print"
        Me.ppv.ToolBars.File.Print.ToolTipText = "Print"
        '
        '
        '
        Me.ppv.ToolBars.File.PrintLayout.Image = CType(resources.GetObject("ppv.ToolBars.File.PrintLayout.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.File.PrintLayout.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.File.PrintLayout.Name = "btnPrintLayout"
        Me.ppv.ToolBars.File.PrintLayout.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.File.PrintLayout.Tag = "C1PreviewActionEnum.PrintLayout"
        Me.ppv.ToolBars.File.PrintLayout.ToolTipText = "Print Layout"
        Me.ppv.ToolBars.File.PrintLayout.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.File.Reflow.Image = CType(resources.GetObject("ppv.ToolBars.File.Reflow.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.File.Reflow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.File.Reflow.Name = "btnReflow"
        Me.ppv.ToolBars.File.Reflow.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.File.Reflow.Tag = "C1PreviewActionEnum.Reflow"
        Me.ppv.ToolBars.File.Reflow.ToolTipText = "Reflow"
        '
        '
        '
        Me.ppv.ToolBars.File.Save.Image = CType(resources.GetObject("ppv.ToolBars.File.Save.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.File.Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.File.Save.Name = "btnFileSave"
        Me.ppv.ToolBars.File.Save.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.File.Save.Tag = "C1PreviewActionEnum.FileSave"
        Me.ppv.ToolBars.File.Save.ToolTipText = "Save File"
        '
        '
        '
        Me.ppv.ToolBars.File.Stop.Image = CType(resources.GetObject("ppv.ToolBars.File.Stop.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.File.Stop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.File.Stop.Name = "btnStop"
        Me.ppv.ToolBars.File.Stop.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.File.Stop.Tag = "C1PreviewActionEnum.Stop"
        Me.ppv.ToolBars.File.Stop.ToolTipText = "Stop"
        Me.ppv.ToolBars.File.Stop.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Navigation.GoFirst.Image = CType(resources.GetObject("ppv.ToolBars.Navigation.GoFirst.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Navigation.GoFirst.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Navigation.GoFirst.Name = "btnGoFirst"
        Me.ppv.ToolBars.Navigation.GoFirst.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.Navigation.GoFirst.Tag = "C1PreviewActionEnum.GoFirst"
        Me.ppv.ToolBars.Navigation.GoFirst.ToolTipText = "Go To First Page"
        Me.ppv.ToolBars.Navigation.GoFirst.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Navigation.GoLast.Image = CType(resources.GetObject("ppv.ToolBars.Navigation.GoLast.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Navigation.GoLast.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Navigation.GoLast.Name = "btnGoLast"
        Me.ppv.ToolBars.Navigation.GoLast.Size = New System.Drawing.Size(23, 20)
        Me.ppv.ToolBars.Navigation.GoLast.Tag = "C1PreviewActionEnum.GoLast"
        Me.ppv.ToolBars.Navigation.GoLast.ToolTipText = "Go To Last Page"
        Me.ppv.ToolBars.Navigation.GoLast.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Navigation.GoNext.Image = CType(resources.GetObject("ppv.ToolBars.Navigation.GoNext.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Navigation.GoNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Navigation.GoNext.Name = "btnGoNext"
        Me.ppv.ToolBars.Navigation.GoNext.Size = New System.Drawing.Size(23, 20)
        Me.ppv.ToolBars.Navigation.GoNext.Tag = "C1PreviewActionEnum.GoNext"
        Me.ppv.ToolBars.Navigation.GoNext.ToolTipText = "Go To Next Page"
        Me.ppv.ToolBars.Navigation.GoNext.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Navigation.GoPrev.Image = CType(resources.GetObject("ppv.ToolBars.Navigation.GoPrev.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Navigation.GoPrev.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Navigation.GoPrev.Name = "btnGoPrev"
        Me.ppv.ToolBars.Navigation.GoPrev.Size = New System.Drawing.Size(23, 20)
        Me.ppv.ToolBars.Navigation.GoPrev.Tag = "C1PreviewActionEnum.GoPrev"
        Me.ppv.ToolBars.Navigation.GoPrev.ToolTipText = "Go To Previous Page"
        Me.ppv.ToolBars.Navigation.GoPrev.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Navigation.HistoryNext.Image = CType(resources.GetObject("ppv.ToolBars.Navigation.HistoryNext.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Navigation.HistoryNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Navigation.HistoryNext.Name = "btnHistoryNext"
        Me.ppv.ToolBars.Navigation.HistoryNext.Size = New System.Drawing.Size(32, 20)
        Me.ppv.ToolBars.Navigation.HistoryNext.Tag = "C1PreviewActionEnum.HistoryNext"
        Me.ppv.ToolBars.Navigation.HistoryNext.ToolTipText = "Next View"
        Me.ppv.ToolBars.Navigation.HistoryNext.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Navigation.HistoryPrev.Image = CType(resources.GetObject("ppv.ToolBars.Navigation.HistoryPrev.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Navigation.HistoryPrev.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Navigation.HistoryPrev.Name = "btnHistoryPrev"
        Me.ppv.ToolBars.Navigation.HistoryPrev.Size = New System.Drawing.Size(32, 20)
        Me.ppv.ToolBars.Navigation.HistoryPrev.Tag = "C1PreviewActionEnum.HistoryPrev"
        Me.ppv.ToolBars.Navigation.HistoryPrev.ToolTipText = "Previous View"
        Me.ppv.ToolBars.Navigation.HistoryPrev.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Navigation.LblOfPages.Name = "lblOfPages"
        Me.ppv.ToolBars.Navigation.LblOfPages.Size = New System.Drawing.Size(27, 15)
        Me.ppv.ToolBars.Navigation.LblOfPages.Tag = "C1PreviewActionEnum.GoPageCount"
        Me.ppv.ToolBars.Navigation.LblOfPages.Text = "of 0"
        Me.ppv.ToolBars.Navigation.LblOfPages.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Navigation.LblPage.Name = "lblPage"
        Me.ppv.ToolBars.Navigation.LblPage.Size = New System.Drawing.Size(33, 15)
        Me.ppv.ToolBars.Navigation.LblPage.Tag = "C1PreviewActionEnum.GoPageLabel"
        Me.ppv.ToolBars.Navigation.LblPage.Text = "Page"
        Me.ppv.ToolBars.Navigation.LblPage.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Navigation.NavigationPane.Image = CType(resources.GetObject("ppv.ToolBars.Navigation.NavigationPane.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Navigation.NavigationPane.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Navigation.NavigationPane.Name = "btnNavigationPane"
        Me.ppv.ToolBars.Navigation.NavigationPane.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.Navigation.NavigationPane.Tag = "C1PreviewActionEnum.NavigationPane"
        Me.ppv.ToolBars.Navigation.NavigationPane.ToolTipText = "Navigation Pane"
        Me.ppv.ToolBars.Navigation.NavigationPane.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Navigation.PageNo.Name = "txtPageNo"
        Me.ppv.ToolBars.Navigation.PageNo.Size = New System.Drawing.Size(34, 23)
        Me.ppv.ToolBars.Navigation.PageNo.Tag = "C1PreviewActionEnum.GoPageNumber"
        Me.ppv.ToolBars.Navigation.PageNo.Text = "1"
        Me.ppv.ToolBars.Navigation.PageNo.Visible = False
        Me.ppv.ToolBars.Navigation.ToolTipPageNo = Nothing
        '
        '
        '
        Me.ppv.ToolBars.Page.Continuous.Image = CType(resources.GetObject("ppv.ToolBars.Page.Continuous.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Page.Continuous.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Page.Continuous.Name = "btnPageContinuous"
        Me.ppv.ToolBars.Page.Continuous.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.Page.Continuous.Tag = "C1PreviewActionEnum.PageContinuous"
        Me.ppv.ToolBars.Page.Continuous.ToolTipText = "Continuous View"
        Me.ppv.ToolBars.Page.Continuous.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Page.Facing.Image = CType(resources.GetObject("ppv.ToolBars.Page.Facing.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Page.Facing.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Page.Facing.Name = "btnPageFacing"
        Me.ppv.ToolBars.Page.Facing.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.Page.Facing.Tag = "C1PreviewActionEnum.PageFacing"
        Me.ppv.ToolBars.Page.Facing.ToolTipText = "Pages Facing View"
        Me.ppv.ToolBars.Page.Facing.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Page.FacingContinuous.Image = CType(resources.GetObject("ppv.ToolBars.Page.FacingContinuous.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Page.FacingContinuous.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Page.FacingContinuous.Name = "btnPageFacingContinuous"
        Me.ppv.ToolBars.Page.FacingContinuous.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.Page.FacingContinuous.Tag = "C1PreviewActionEnum.PageFacingContinuous"
        Me.ppv.ToolBars.Page.FacingContinuous.ToolTipText = "Pages Facing Continuous View"
        Me.ppv.ToolBars.Page.FacingContinuous.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Page.Single.Image = CType(resources.GetObject("ppv.ToolBars.Page.Single.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Page.Single.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Page.Single.Name = "btnPageSingle"
        Me.ppv.ToolBars.Page.Single.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.Page.Single.Tag = "C1PreviewActionEnum.PageSingle"
        Me.ppv.ToolBars.Page.Single.ToolTipText = "Single Page View"
        Me.ppv.ToolBars.Page.Single.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Search.CloseSearch.Image = CType(resources.GetObject("ppv.ToolBars.Search.CloseSearch.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Search.CloseSearch.Name = "btnCloseSearch"
        Me.ppv.ToolBars.Search.CloseSearch.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.Search.CloseSearch.Tag = "C1PreviewActionEnum.CloseSearch"
        Me.ppv.ToolBars.Search.CloseSearch.ToolTipText = "Close"
        Me.ppv.ToolBars.Search.CloseSearch.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Search.MatchCase.Name = "btnMatchCase"
        Me.ppv.ToolBars.Search.MatchCase.Size = New System.Drawing.Size(73, 22)
        Me.ppv.ToolBars.Search.MatchCase.Tag = "C1PreviewActionEnum.MatchCase"
        Me.ppv.ToolBars.Search.MatchCase.Text = "Match Case"
        Me.ppv.ToolBars.Search.MatchCase.ToolTipText = "Search with case sensitivity"
        Me.ppv.ToolBars.Search.MatchCase.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Search.MatchWholeWord.Name = "btnMatchWholeWord"
        Me.ppv.ToolBars.Search.MatchWholeWord.Size = New System.Drawing.Size(77, 22)
        Me.ppv.ToolBars.Search.MatchWholeWord.Tag = "C1PreviewActionEnum.MatchWholeWord"
        Me.ppv.ToolBars.Search.MatchWholeWord.Text = "Whole Word"
        Me.ppv.ToolBars.Search.MatchWholeWord.ToolTipText = "Match whole word only"
        Me.ppv.ToolBars.Search.MatchWholeWord.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Search.SearchLabel.Name = "lblSearch"
        Me.ppv.ToolBars.Search.SearchLabel.Size = New System.Drawing.Size(33, 22)
        Me.ppv.ToolBars.Search.SearchLabel.Tag = "C1PreviewActionEnum.SearchLabel"
        Me.ppv.ToolBars.Search.SearchLabel.Text = "Find:"
        Me.ppv.ToolBars.Search.SearchLabel.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Search.SearchNext.Image = CType(resources.GetObject("ppv.ToolBars.Search.SearchNext.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Search.SearchNext.Name = "btnSearchNext"
        Me.ppv.ToolBars.Search.SearchNext.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.Search.SearchNext.Tag = "C1PreviewActionEnum.SearchNext"
        Me.ppv.ToolBars.Search.SearchNext.ToolTipText = "Search Next"
        Me.ppv.ToolBars.Search.SearchNext.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Search.SearchPrevious.Image = CType(resources.GetObject("ppv.ToolBars.Search.SearchPrevious.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Search.SearchPrevious.Name = "btnSearchPrevious"
        Me.ppv.ToolBars.Search.SearchPrevious.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.Search.SearchPrevious.Tag = "C1PreviewActionEnum.SearchPrevious"
        Me.ppv.ToolBars.Search.SearchPrevious.ToolTipText = "Search Previous"
        Me.ppv.ToolBars.Search.SearchPrevious.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Search.SearchText.Name = "txtSearchText"
        Me.ppv.ToolBars.Search.SearchText.Size = New System.Drawing.Size(200, 25)
        Me.ppv.ToolBars.Search.SearchText.Tag = "C1PreviewActionEnum.SearchText"
        Me.ppv.ToolBars.Search.SearchText.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Text.Find.Image = CType(resources.GetObject("ppv.ToolBars.Text.Find.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Text.Find.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Text.Find.Name = "btnFind"
        Me.ppv.ToolBars.Text.Find.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.Text.Find.Tag = "C1PreviewActionEnum.Find"
        Me.ppv.ToolBars.Text.Find.ToolTipText = "Find Text"
        Me.ppv.ToolBars.Text.Find.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Text.Hand.Image = CType(resources.GetObject("ppv.ToolBars.Text.Hand.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Text.Hand.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Text.Hand.Name = "btnHandTool"
        Me.ppv.ToolBars.Text.Hand.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.Text.Hand.Tag = "C1PreviewActionEnum.HandTool"
        Me.ppv.ToolBars.Text.Hand.ToolTipText = "Hand Tool"
        Me.ppv.ToolBars.Text.Hand.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Text.SelectText.Image = CType(resources.GetObject("ppv.ToolBars.Text.SelectText.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Text.SelectText.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Text.SelectText.Name = "btnSelectTextTool"
        Me.ppv.ToolBars.Text.SelectText.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.Text.SelectText.Tag = "C1PreviewActionEnum.SelectTextTool"
        Me.ppv.ToolBars.Text.SelectText.ToolTipText = "Text Select Tool"
        Me.ppv.ToolBars.Text.SelectText.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Zoom.DropZoomFactor.Name = "dropZoomFactor"
        Me.ppv.ToolBars.Zoom.DropZoomFactor.Size = New System.Drawing.Size(13, 22)
        Me.ppv.ToolBars.Zoom.DropZoomFactor.Tag = "C1PreviewActionEnum.ZoomFactor"
        Me.ppv.ToolBars.Zoom.DropZoomFactor.Visible = False
        Me.ppv.ToolBars.Zoom.ToolTipZoomFactor = Nothing
        '
        '
        '
        Me.ppv.ToolBars.Zoom.ZoomFactor.Name = "txtZoomFactor"
        Me.ppv.ToolBars.Zoom.ZoomFactor.Size = New System.Drawing.Size(34, 25)
        Me.ppv.ToolBars.Zoom.ZoomFactor.Tag = "C1PreviewActionEnum.ZoomFactor"
        Me.ppv.ToolBars.Zoom.ZoomFactor.Text = "100%"
        Me.ppv.ToolBars.Zoom.ZoomFactor.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Zoom.ZoomIn.Image = CType(resources.GetObject("ppv.ToolBars.Zoom.ZoomIn.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Zoom.ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Zoom.ZoomIn.Name = "btnZoomIn"
        Me.ppv.ToolBars.Zoom.ZoomIn.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.Zoom.ZoomIn.Tag = "C1PreviewActionEnum.ZoomIn"
        Me.ppv.ToolBars.Zoom.ZoomIn.ToolTipText = "Zoom In"
        Me.ppv.ToolBars.Zoom.ZoomIn.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Zoom.ZoomInTool.Image = CType(resources.GetObject("ppv.ToolBars.Zoom.ZoomInTool.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Zoom.ZoomInTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Zoom.ZoomInTool.Name = "itemZoomInTool"
        Me.ppv.ToolBars.Zoom.ZoomInTool.Size = New System.Drawing.Size(207, 22)
        Me.ppv.ToolBars.Zoom.ZoomInTool.Tag = "C1PreviewActionEnum.ZoomInTool"
        Me.ppv.ToolBars.Zoom.ZoomInTool.Text = "Herramienta para acercar"
        Me.ppv.ToolBars.Zoom.ZoomInTool.ToolTipText = "Herramienta para acercar"
        '
        '
        '
        Me.ppv.ToolBars.Zoom.ZoomOut.Image = CType(resources.GetObject("ppv.ToolBars.Zoom.ZoomOut.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Zoom.ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Zoom.ZoomOut.Name = "btnZoomOut"
        Me.ppv.ToolBars.Zoom.ZoomOut.Size = New System.Drawing.Size(23, 22)
        Me.ppv.ToolBars.Zoom.ZoomOut.Tag = "C1PreviewActionEnum.ZoomOut"
        Me.ppv.ToolBars.Zoom.ZoomOut.ToolTipText = "Zoom Out"
        Me.ppv.ToolBars.Zoom.ZoomOut.Visible = False
        '
        '
        '
        Me.ppv.ToolBars.Zoom.ZoomOutTool.Image = CType(resources.GetObject("ppv.ToolBars.Zoom.ZoomOutTool.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Zoom.ZoomOutTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Zoom.ZoomOutTool.Name = "itemZoomOutTool"
        Me.ppv.ToolBars.Zoom.ZoomOutTool.Size = New System.Drawing.Size(207, 22)
        Me.ppv.ToolBars.Zoom.ZoomOutTool.Tag = "C1PreviewActionEnum.ZoomOutTool"
        Me.ppv.ToolBars.Zoom.ZoomOutTool.Text = "Herramienta para alejar"
        Me.ppv.ToolBars.Zoom.ZoomOutTool.ToolTipText = "Herramienta para alejar"
        '
        '
        '
        Me.ppv.ToolBars.Zoom.ZoomTool.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ppv.ToolBars.Zoom.ZoomInTool, Me.ppv.ToolBars.Zoom.ZoomOutTool})
        Me.ppv.ToolBars.Zoom.ZoomTool.Image = CType(resources.GetObject("ppv.ToolBars.Zoom.ZoomTool.Image"), System.Drawing.Image)
        Me.ppv.ToolBars.Zoom.ZoomTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppv.ToolBars.Zoom.ZoomTool.Name = "btnZoomTool"
        Me.ppv.ToolBars.Zoom.ZoomTool.Size = New System.Drawing.Size(32, 22)
        Me.ppv.ToolBars.Zoom.ZoomTool.Tag = "C1PreviewActionEnum.ZoomInTool"
        Me.ppv.ToolBars.Zoom.ZoomTool.ToolTipText = "Zoom In Tool"
        Me.ppv.ToolBars.Zoom.ZoomTool.Visible = False
        '
        'btnGenerarEtiquetas
        '
        Me.btnGenerarEtiquetas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerarEtiquetas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerarEtiquetas.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnGenerarEtiquetas.Location = New System.Drawing.Point(15, 38)
        Me.btnGenerarEtiquetas.Name = "btnGenerarEtiquetas"
        Me.btnGenerarEtiquetas.Size = New System.Drawing.Size(168, 41)
        Me.btnGenerarEtiquetas.TabIndex = 244
        Me.btnGenerarEtiquetas.Text = "Generar Etiquetes"
        Me.btnGenerarEtiquetas.UseVisualStyleBackColor = True
        '
        'txtNumEtiquetas
        '
        Me.txtNumEtiquetas.Location = New System.Drawing.Point(115, 16)
        Me.txtNumEtiquetas.Name = "txtNumEtiquetas"
        Me.txtNumEtiquetas.Size = New System.Drawing.Size(68, 18)
        Me.txtNumEtiquetas.TabIndex = 243
        Me.txtNumEtiquetas.Tag = Nothing
        Me.txtNumEtiquetas.Value = "1"
        '
        'Label1
        '
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(9, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 20)
        Me.Label1.TabIndex = 245
        Me.Label1.Text = "Numero Etiquetes"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gbFichaTecnica
        '
        Me.gbFichaTecnica.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbFichaTecnica.Controls.Add(Me.ppFitxa)
        Me.gbFichaTecnica.Location = New System.Drawing.Point(267, 11)
        Me.gbFichaTecnica.Name = "gbFichaTecnica"
        Me.gbFichaTecnica.Size = New System.Drawing.Size(582, 524)
        Me.gbFichaTecnica.TabIndex = 247
        Me.gbFichaTecnica.TabStop = False
        Me.gbFichaTecnica.Text = "Fitxa Tècnica"
        '
        'ppFitxa
        '
        Me.ppFitxa.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ppFitxa.Location = New System.Drawing.Point(6, 24)
        Me.ppFitxa.Name = "ppFitxa"
        '
        'ppFitxa.PreviewPane
        '
        Me.ppFitxa.PreviewPane.IntegrateExternalTools = True
        Me.ppFitxa.PreviewPane.TabIndex = 0
        Me.ppFitxa.Size = New System.Drawing.Size(558, 500)
        Me.ppFitxa.TabIndex = 0
        Me.ppFitxa.Text = "C1PrintPreviewControl1"
        '
        '
        '
        Me.ppFitxa.ToolBars.File.Open.Image = CType(resources.GetObject("ppFitxa.ToolBars.File.Open.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.File.Open.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.File.Open.Name = "btnFileOpen"
        Me.ppFitxa.ToolBars.File.Open.Size = New System.Drawing.Size(32, 22)
        Me.ppFitxa.ToolBars.File.Open.Tag = "C1PreviewActionEnum.FileOpen"
        Me.ppFitxa.ToolBars.File.Open.ToolTipText = "Open File"
        '
        '
        '
        Me.ppFitxa.ToolBars.File.PageSetup.Image = CType(resources.GetObject("ppFitxa.ToolBars.File.PageSetup.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.File.PageSetup.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.File.PageSetup.Name = "btnPageSetup"
        Me.ppFitxa.ToolBars.File.PageSetup.Size = New System.Drawing.Size(23, 22)
        Me.ppFitxa.ToolBars.File.PageSetup.Tag = "C1PreviewActionEnum.PageSetup"
        Me.ppFitxa.ToolBars.File.PageSetup.ToolTipText = "Page Setup"
        '
        '
        '
        Me.ppFitxa.ToolBars.File.Print.Image = CType(resources.GetObject("ppFitxa.ToolBars.File.Print.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.File.Print.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.File.Print.Name = "btnPrint"
        Me.ppFitxa.ToolBars.File.Print.Size = New System.Drawing.Size(23, 22)
        Me.ppFitxa.ToolBars.File.Print.Tag = "C1PreviewActionEnum.Print"
        Me.ppFitxa.ToolBars.File.Print.ToolTipText = "Print"
        '
        '
        '
        Me.ppFitxa.ToolBars.File.Reflow.Image = CType(resources.GetObject("ppFitxa.ToolBars.File.Reflow.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.File.Reflow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.File.Reflow.Name = "btnReflow"
        Me.ppFitxa.ToolBars.File.Reflow.Size = New System.Drawing.Size(23, 22)
        Me.ppFitxa.ToolBars.File.Reflow.Tag = "C1PreviewActionEnum.Reflow"
        Me.ppFitxa.ToolBars.File.Reflow.ToolTipText = "Reflow"
        '
        '
        '
        Me.ppFitxa.ToolBars.File.Save.Image = CType(resources.GetObject("ppFitxa.ToolBars.File.Save.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.File.Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.File.Save.Name = "btnFileSave"
        Me.ppFitxa.ToolBars.File.Save.Size = New System.Drawing.Size(23, 22)
        Me.ppFitxa.ToolBars.File.Save.Tag = "C1PreviewActionEnum.FileSave"
        Me.ppFitxa.ToolBars.File.Save.ToolTipText = "Save File"
        '
        '
        '
        Me.ppFitxa.ToolBars.Navigation.GoFirst.Image = CType(resources.GetObject("ppFitxa.ToolBars.Navigation.GoFirst.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Navigation.GoFirst.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Navigation.GoFirst.Name = "btnGoFirst"
        Me.ppFitxa.ToolBars.Navigation.GoFirst.Size = New System.Drawing.Size(23, 22)
        Me.ppFitxa.ToolBars.Navigation.GoFirst.Tag = "C1PreviewActionEnum.GoFirst"
        Me.ppFitxa.ToolBars.Navigation.GoFirst.ToolTipText = "Go To First Page"
        '
        '
        '
        Me.ppFitxa.ToolBars.Navigation.GoLast.Image = CType(resources.GetObject("ppFitxa.ToolBars.Navigation.GoLast.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Navigation.GoLast.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Navigation.GoLast.Name = "btnGoLast"
        Me.ppFitxa.ToolBars.Navigation.GoLast.Size = New System.Drawing.Size(23, 20)
        Me.ppFitxa.ToolBars.Navigation.GoLast.Tag = "C1PreviewActionEnum.GoLast"
        Me.ppFitxa.ToolBars.Navigation.GoLast.ToolTipText = "Go To Last Page"
        '
        '
        '
        Me.ppFitxa.ToolBars.Navigation.GoNext.Image = CType(resources.GetObject("ppFitxa.ToolBars.Navigation.GoNext.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Navigation.GoNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Navigation.GoNext.Name = "btnGoNext"
        Me.ppFitxa.ToolBars.Navigation.GoNext.Size = New System.Drawing.Size(23, 20)
        Me.ppFitxa.ToolBars.Navigation.GoNext.Tag = "C1PreviewActionEnum.GoNext"
        Me.ppFitxa.ToolBars.Navigation.GoNext.ToolTipText = "Go To Next Page"
        '
        '
        '
        Me.ppFitxa.ToolBars.Navigation.GoPrev.Image = CType(resources.GetObject("ppFitxa.ToolBars.Navigation.GoPrev.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Navigation.GoPrev.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Navigation.GoPrev.Name = "btnGoPrev"
        Me.ppFitxa.ToolBars.Navigation.GoPrev.Size = New System.Drawing.Size(23, 20)
        Me.ppFitxa.ToolBars.Navigation.GoPrev.Tag = "C1PreviewActionEnum.GoPrev"
        Me.ppFitxa.ToolBars.Navigation.GoPrev.ToolTipText = "Go To Previous Page"
        '
        '
        '
        Me.ppFitxa.ToolBars.Navigation.HistoryNext.Image = CType(resources.GetObject("ppFitxa.ToolBars.Navigation.HistoryNext.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Navigation.HistoryNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Navigation.HistoryNext.Name = "btnHistoryNext"
        Me.ppFitxa.ToolBars.Navigation.HistoryNext.Size = New System.Drawing.Size(32, 20)
        Me.ppFitxa.ToolBars.Navigation.HistoryNext.Tag = "C1PreviewActionEnum.HistoryNext"
        Me.ppFitxa.ToolBars.Navigation.HistoryNext.ToolTipText = "Next View"
        '
        '
        '
        Me.ppFitxa.ToolBars.Navigation.HistoryPrev.Image = CType(resources.GetObject("ppFitxa.ToolBars.Navigation.HistoryPrev.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Navigation.HistoryPrev.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Navigation.HistoryPrev.Name = "btnHistoryPrev"
        Me.ppFitxa.ToolBars.Navigation.HistoryPrev.Size = New System.Drawing.Size(32, 20)
        Me.ppFitxa.ToolBars.Navigation.HistoryPrev.Tag = "C1PreviewActionEnum.HistoryPrev"
        Me.ppFitxa.ToolBars.Navigation.HistoryPrev.ToolTipText = "Previous View"
        '
        '
        '
        Me.ppFitxa.ToolBars.Navigation.LblOfPages.Name = "lblOfPages"
        Me.ppFitxa.ToolBars.Navigation.LblOfPages.Size = New System.Drawing.Size(27, 15)
        Me.ppFitxa.ToolBars.Navigation.LblOfPages.Tag = "C1PreviewActionEnum.GoPageCount"
        Me.ppFitxa.ToolBars.Navigation.LblOfPages.Text = "of 0"
        '
        '
        '
        Me.ppFitxa.ToolBars.Navigation.LblPage.Name = "lblPage"
        Me.ppFitxa.ToolBars.Navigation.LblPage.Size = New System.Drawing.Size(33, 15)
        Me.ppFitxa.ToolBars.Navigation.LblPage.Tag = "C1PreviewActionEnum.GoPageLabel"
        Me.ppFitxa.ToolBars.Navigation.LblPage.Text = "Page"
        Me.ppFitxa.ToolBars.Navigation.ToolTipPageNo = Nothing
        '
        '
        '
        Me.ppFitxa.ToolBars.Page.Continuous.Image = CType(resources.GetObject("ppFitxa.ToolBars.Page.Continuous.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Page.Continuous.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Page.Continuous.Name = "btnPageContinuous"
        Me.ppFitxa.ToolBars.Page.Continuous.Size = New System.Drawing.Size(23, 22)
        Me.ppFitxa.ToolBars.Page.Continuous.Tag = "C1PreviewActionEnum.PageContinuous"
        Me.ppFitxa.ToolBars.Page.Continuous.ToolTipText = "Continuous View"
        '
        '
        '
        Me.ppFitxa.ToolBars.Page.Facing.Image = CType(resources.GetObject("ppFitxa.ToolBars.Page.Facing.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Page.Facing.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Page.Facing.Name = "btnPageFacing"
        Me.ppFitxa.ToolBars.Page.Facing.Size = New System.Drawing.Size(23, 22)
        Me.ppFitxa.ToolBars.Page.Facing.Tag = "C1PreviewActionEnum.PageFacing"
        Me.ppFitxa.ToolBars.Page.Facing.ToolTipText = "Pages Facing View"
        '
        '
        '
        Me.ppFitxa.ToolBars.Page.FacingContinuous.Image = CType(resources.GetObject("ppFitxa.ToolBars.Page.FacingContinuous.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Page.FacingContinuous.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Page.FacingContinuous.Name = "btnPageFacingContinuous"
        Me.ppFitxa.ToolBars.Page.FacingContinuous.Size = New System.Drawing.Size(23, 22)
        Me.ppFitxa.ToolBars.Page.FacingContinuous.Tag = "C1PreviewActionEnum.PageFacingContinuous"
        Me.ppFitxa.ToolBars.Page.FacingContinuous.ToolTipText = "Pages Facing Continuous View"
        '
        '
        '
        Me.ppFitxa.ToolBars.Page.Single.Image = CType(resources.GetObject("ppFitxa.ToolBars.Page.Single.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Page.Single.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Page.Single.Name = "btnPageSingle"
        Me.ppFitxa.ToolBars.Page.Single.Size = New System.Drawing.Size(23, 22)
        Me.ppFitxa.ToolBars.Page.Single.Tag = "C1PreviewActionEnum.PageSingle"
        Me.ppFitxa.ToolBars.Page.Single.ToolTipText = "Single Page View"
        '
        '
        '
        Me.ppFitxa.ToolBars.Text.Find.Image = CType(resources.GetObject("ppFitxa.ToolBars.Text.Find.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Text.Find.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Text.Find.Name = "btnFind"
        Me.ppFitxa.ToolBars.Text.Find.Size = New System.Drawing.Size(23, 20)
        Me.ppFitxa.ToolBars.Text.Find.Tag = "C1PreviewActionEnum.Find"
        Me.ppFitxa.ToolBars.Text.Find.ToolTipText = "Find Text"
        '
        '
        '
        Me.ppFitxa.ToolBars.Text.Hand.Image = CType(resources.GetObject("ppFitxa.ToolBars.Text.Hand.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Text.Hand.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Text.Hand.Name = "btnHandTool"
        Me.ppFitxa.ToolBars.Text.Hand.Size = New System.Drawing.Size(23, 22)
        Me.ppFitxa.ToolBars.Text.Hand.Tag = "C1PreviewActionEnum.HandTool"
        Me.ppFitxa.ToolBars.Text.Hand.ToolTipText = "Hand Tool"
        '
        '
        '
        Me.ppFitxa.ToolBars.Text.SelectText.Image = CType(resources.GetObject("ppFitxa.ToolBars.Text.SelectText.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Text.SelectText.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Text.SelectText.Name = "btnSelectTextTool"
        Me.ppFitxa.ToolBars.Text.SelectText.Size = New System.Drawing.Size(23, 20)
        Me.ppFitxa.ToolBars.Text.SelectText.Tag = "C1PreviewActionEnum.SelectTextTool"
        Me.ppFitxa.ToolBars.Text.SelectText.ToolTipText = "Text Select Tool"
        Me.ppFitxa.ToolBars.Zoom.ToolTipZoomFactor = Nothing
        '
        '
        '
        Me.ppFitxa.ToolBars.Zoom.ZoomIn.Image = CType(resources.GetObject("ppFitxa.ToolBars.Zoom.ZoomIn.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Zoom.ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Zoom.ZoomIn.Name = "btnZoomIn"
        Me.ppFitxa.ToolBars.Zoom.ZoomIn.Size = New System.Drawing.Size(23, 22)
        Me.ppFitxa.ToolBars.Zoom.ZoomIn.Tag = "C1PreviewActionEnum.ZoomIn"
        Me.ppFitxa.ToolBars.Zoom.ZoomIn.ToolTipText = "Zoom In"
        '
        '
        '
        Me.ppFitxa.ToolBars.Zoom.ZoomOut.Image = CType(resources.GetObject("ppFitxa.ToolBars.Zoom.ZoomOut.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Zoom.ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Zoom.ZoomOut.Name = "btnZoomOut"
        Me.ppFitxa.ToolBars.Zoom.ZoomOut.Size = New System.Drawing.Size(23, 22)
        Me.ppFitxa.ToolBars.Zoom.ZoomOut.Tag = "C1PreviewActionEnum.ZoomOut"
        Me.ppFitxa.ToolBars.Zoom.ZoomOut.ToolTipText = "Zoom Out"
        '
        '
        '
        Me.ppFitxa.ToolBars.Zoom.ZoomTool.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ppFitxa.ToolBars.Zoom.ZoomInTool, Me.ppFitxa.ToolBars.Zoom.ZoomOutTool})
        Me.ppFitxa.ToolBars.Zoom.ZoomTool.Image = CType(resources.GetObject("ppFitxa.ToolBars.Zoom.ZoomTool.Image"), System.Drawing.Image)
        Me.ppFitxa.ToolBars.Zoom.ZoomTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppFitxa.ToolBars.Zoom.ZoomTool.Name = "btnZoomTool"
        Me.ppFitxa.ToolBars.Zoom.ZoomTool.Size = New System.Drawing.Size(32, 22)
        Me.ppFitxa.ToolBars.Zoom.ZoomTool.Tag = "C1PreviewActionEnum.ZoomInTool"
        Me.ppFitxa.ToolBars.Zoom.ZoomTool.ToolTipText = "Zoom In Tool"
        '
        'tabPageValoracion
        '
        Me.tabPageValoracion.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(196, Byte), Integer))
        Me.tabPageValoracion.Controls.Add(Me.btnExportarExcel)
        Me.tabPageValoracion.Controls.Add(Me.btnExportarPDF)
        Me.tabPageValoracion.Controls.Add(Me.dgValoracion)
        Me.tabPageValoracion.Location = New System.Drawing.Point(4, 22)
        Me.tabPageValoracion.Name = "tabPageValoracion"
        Me.tabPageValoracion.Size = New System.Drawing.Size(852, 604)
        Me.tabPageValoracion.TabIndex = 3
        Me.tabPageValoracion.Text = "Valoració Stock"
        '
        'btnExportarExcel
        '
        Me.btnExportarExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportarExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExportarExcel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExportarExcel.Location = New System.Drawing.Point(620, 486)
        Me.btnExportarExcel.Name = "btnExportarExcel"
        Me.btnExportarExcel.Size = New System.Drawing.Size(100, 28)
        Me.btnExportarExcel.TabIndex = 271
        Me.btnExportarExcel.Text = "Exportar a Excel"
        Me.btnExportarExcel.UseVisualStyleBackColor = True
        '
        'btnExportarPDF
        '
        Me.btnExportarPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExportarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportarPDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExportarPDF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExportarPDF.Location = New System.Drawing.Point(740, 486)
        Me.btnExportarPDF.Name = "btnExportarPDF"
        Me.btnExportarPDF.Size = New System.Drawing.Size(100, 28)
        Me.btnExportarPDF.TabIndex = 270
        Me.btnExportarPDF.Text = "Exportar a PDF"
        Me.btnExportarPDF.UseVisualStyleBackColor = True
        '
        'dgValoracion
        '
        Me.dgValoracion.AllowUpdate = False
        Me.dgValoracion.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgValoracion.Caption = "Llistat Valorat"
        Me.dgValoracion.ExtendRightColumn = True
        Me.dgValoracion.FilterBar = True
        Me.dgValoracion.GroupByCaption = "Desplaça aquí una capçalera de columna per agrupar per aquesta columna"
        Me.dgValoracion.Images.Add(CType(resources.GetObject("dgValoracion.Images"), System.Drawing.Image))
        Me.dgValoracion.Location = New System.Drawing.Point(16, 16)
        Me.dgValoracion.Name = "dgValoracion"
        Me.dgValoracion.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgValoracion.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgValoracion.PreviewInfo.ZoomFactor = 75.0R
        Me.dgValoracion.PrintInfo.PageSettings = CType(resources.GetObject("dgValoracion.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgValoracion.RowDivider.Color = System.Drawing.Color.Black
        Me.dgValoracion.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.dgValoracion.Size = New System.Drawing.Size(824, 464)
        Me.dgValoracion.SpringMode = True
        Me.dgValoracion.TabIndex = 0
        Me.dgValoracion.UseCompatibleTextRendering = False
        Me.dgValoracion.WrapCellPointer = True
        Me.dgValoracion.PropBag = resources.GetString("dgValoracion.PropBag")
        '
        'frmTejidos
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(196, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(876, 696)
        Me.Controls.Add(Me.TabControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.KeyPreview = True
        Me.Name = "frmTejidos"
        Me.Text = "Teixits"
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
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        CType(Me.btnRecargar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSiguiente, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAnterior, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrimero, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUltimo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnModificar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnTancar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBorrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNuevo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnActualizar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnVerLista, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.tpTejido.ResumeLayout(False)
        Me.tpTejido.PerformLayout()
        CType(Me.C1Combo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgCartaColores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNumeroTejidos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XpGroupBox1.ResumeLayout(False)
        Me.XpGroupBox1.PerformLayout()
        CType(Me.txtMLINEAL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNCCODE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComposicio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAMPLE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnElegirMaquina, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMMAQUI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnElegirTejido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPREUPERMODEL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comboIVA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNRO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAMPLENOU, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPREUM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMAQUI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDESCRI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRENDIMENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMARGE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGRAMA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPREUK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gb2.ResumeLayout(False)
        CType(Me.txtSTCRUK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSTCRUM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSTDISPK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSTDISPM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpEscandalloTejido.ResumeLayout(False)
        Me.tpEscandalloTejido.PerformLayout()
        CType(Me.cboAcabados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgAcabados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnLimpiarAcabados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtACABAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboHilos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMACABADOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgComposcionTejido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMESTAMPADOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMTEIXIDOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCRU, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPACA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPESTAM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPTEIXIR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMATERIA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnElegirAcabador, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnElegirEstampador, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnElegirTejidor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtACABADOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtESTAMPADOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTEIXIDOR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.tx2CODI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx2DESCRI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabImpresioFitxa.ResumeLayout(False)
        Me.gbImpresionEtiquetas.ResumeLayout(False)
        CType(Me.ppv.PreviewPane, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ppv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ppv.ResumeLayout(False)
        Me.ppv.PerformLayout()
        CType(Me.btnGenerarEtiquetas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNumEtiquetas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbFichaTecnica.ResumeLayout(False)
        CType(Me.ppFitxa.PreviewPane, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ppFitxa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ppFitxa.ResumeLayout(False)
        Me.ppFitxa.PerformLayout()
        Me.tabPageValoracion.ResumeLayout(False)
        CType(Me.btnExportarExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExportarPDF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgValoracion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmTejidos
    Public Shared Function GetInstance() As frmTejidos
        If frmChildForm Is Nothing Then
            frmChildForm = New frmTejidos

        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Public tejidoActual As clsTejido

#End Region

#Region "INICIALIZAR"

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True
            Me.arrayGrids = New System.Windows.Forms.Control() {Me.dgCartaColores, Me.dgComposcionTejido, Me.dgAcabados}
            Me.arrayTextBox = New System.Windows.Forms.Control() {Me.txtAMPLENOU, txtAMPLE, Me.txtACABADOR, Me.txtESTAMPADOR, Me.txtMAQUI, Me.txtTEIXIDOR, Me.tx2CODI, Me.txtDESCRI, Me.tx2DESCRI, Me.txtGRAMA, Me.txtPACA, Me.txtPESTAM, Me.txtPTEIXIR, Me.txtCRU, Me.txtMATERIA, Me.txtOBSERV, Me.txtNRO, Me.txtPREUK, Me.txtPREUM, Me.txtPREUPERMODEL, Me.txtRENDIMENT, Me.txtSTCRUK, Me.txtSTCRUM, Me.txtSTDISPK, Me.txtSTDISPM, Me.txtMARGE, Me.txtACABAT, Me.txtComposicio, Me.txtNCCODE, txtMLINEAL}
            Me.arrayCombos = New System.Windows.Forms.Control() {Me.comboIVA, Me.cboNOMACABADOR, Me.cboNOMESTAMPADOR, Me.cboNOMMAQUI, Me.cboNOMTEIXIDOR, cboID}
            Me.arrayTabPages = New System.Windows.Forms.Control() {Me.tpEscandalloTejido, Me.tpTejido}
            Me.arrayCheckBox = New System.Windows.Forms.Control() {Me.chkTubular}
            Me.arrayEtiquetas = New System.Windows.Forms.Control() {Me.Label2, Me.Label3, lblAncho, Me.lblAcabador, Me.lblAncho2, Me.lblCodigoTejido, Me.lblCodigoTejidoEscandallo, Me.lblEstampador, Me.lblGramaje, Me.lblIVA, Me.lblMAQUI, Me.lblNombre, Me.lblNumeroHilo, Me.lblPrecioMetro, Me.lblPrecioTejidoParaModelos, Me.lblPreciokg, Me.lblRendimiento, Me.lblStockCru, Me.lblStockCruK, Me.lblStockCruM, Me.lblStockDisipM, Me.lblStockDisposat, Me.lblStockDisposatK, Me.lblTantoPorCientoMargen, Me.lblTejedor, Me.lblTotalMateria}
            Me.arrayBotones = New System.Windows.Forms.Control() {Me.btnElegirAcabador, Me.btnElegirEstampador, Me.btnElegirMaquina, Me.btnElegirTejido, Me.btnElegirTejidor, Me.btnLimpiarAcabados}

            ACN()

            tabla = tablaTejidos
            tejidoActual = New clsTejido(ds.Tables(tabla), empresaPorDefecto, BindingContext)
            HacerBindings()
            IniciarDgColores()
            IniciarDgAcabados()
            InicializarDgComposicionTejido()
            PonerModificables(soloLectura)
            CCN()
            PonerHandlersErroresParaGrids()
            MoverActual()
            btnSiguiente.Focus()
            PSBTIPO(tejidoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub HacerBindings()
        Try
            HacerBindingsTodos(tejidoActual.dvForm)
            AñadirBinding(cboID, tejidoActual.dvForm, "CODI")
            AñadirBindingCombo(cboID, tejidoActual.dvIdentificadores, CCTeixits, CCTeixits)

            CrearBindingIVA(comboIVA, tejidoActual.dvForm, True)

            Dim dtTallers2 As DataTable = tejidoActual.dtTallers.Copy
            Dim dtTallers3 As DataTable = tejidoActual.dtTallers.Copy

            AñadirBindingCombo(cboNOMTEIXIDOR, tejidoActual.dtTallers, CCTallers, CNTallers)
            AñadirBindingCombo(cboNOMESTAMPADOR, dtTallers2, CCTallers, CNTallers)
            AñadirBindingCombo(cboNOMACABADOR, dtTallers3, CCTallers, CNTallers)
            AñadirBindingCombo(cboNOMMAQUI, tejidoActual.dtMaqui, CCMaqui, CNMaqui)

            dgCartaColores.SetDataBinding(tejidoActual.cartaColores.dvForm, "")
            dgAcabados.SetDataBinding(tejidoActual.acabados.dvForm, "")

            dgComposcionTejido.SetDataBinding(tejidoActual.composicion.dvForm, "")
            AñadirBindingCombo(cboHilos, tejidoActual.dvHilos, CCFil, CCFil)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "MODIFICAR DB"

    Private Sub ActualizarOrigen()
        Try
            If editando Then
                editando = False
                lblNumeroTejidos.Text = rm.GetString("NUMTEJIDOS") & " " & tejidoActual.dvIdentificadores.Count
                'tejidoActual.tabla.AcceptChanges()
                PonerModificables(soloLectura)
            End If
            If EsRegistroNuevo Then
                editando = True : PSBTIPO(tejidoActual.centro)
                PonerControlesNuevo(True)
                dgCartaColores.Visible = True
                dgComposcionTejido.Visible = True
                QuitarPonerTabPages(poner)
                EsRegistroNuevo = False
                MoverActual()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            cargando = True
            If tejidoActual.TieneCambios Then
                Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                    Case DialogResult.Cancel
                        cargando = False
                        Exit Sub
                    Case DialogResult.No
                        tejidoActual.tabla.RejectChanges()
                        tejidoActual.cartaColores.tabla.RejectChanges()
                        tejidoActual.acabados.tabla.RejectChanges()
                        cargando = False
                        Exit Sub
                End Select
            End If
            'If tejidoActual.dvForm(0)("CODI") = "" Then
            '    tejidoActual.dvForm(0)("CODI") = cboID.Text
            'End If

            tejidoActual.ActualizarOrigen() : ActualizarOrigen()
            PSBTIPO(tejidoActual.centro) : cargando = False
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnBorrar.Click
        Try
            If MessageBox.Show(rm.GetString("BorrarTejido"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                cargando = True
                editando = True
                tejidoActual.borrar() : ActualizarOrigen()
                editando = False
                MoverActual()
                PSBTIPO(tejidoActual.centro)
                lblNumeroTejidos.Text = rm.GetString("NUMTEJIDOS") & " " & tejidoActual.dvIdentificadores.Count
                'tejidoActual.tabla.AcceptChanges()
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnNuevo.Click
        Try
            cargando = True
            editando = True
            EsRegistroNuevo = True
            QuitarPonerTabPages(quitar)
            PonerModificables(modificable)
            PSBTIPO(tejidoActual.centro)
            cboID.ReadOnly = False
            tejidoActual.NuevoRegistro()
            cargando = False

            'JP 25/06/2018: esto ha dejado de funcionar por arte de magia, con lo de arriba funciona
            'cargando = True
            'EsRegistroNuevo = True
            'dgCartaColores.Visible = False
            'dgComposcionTejido.Visible = False

            'QuitarPonerTabPages(quitar)
            'PonerControlesNuevo(False)

            'tejidoActual.NuevoRegistro()
            'PonerModificables(modificable)
            'MoverActual(True)
            'cboID.AutoCompletion = False
            'PSBTIPO(tejidoActual.centro)
            'cargando = False

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
                PSBTIPO(tejidoActual.centro)
                cboID.Text = tejidoActual.CODI
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub QuitarPonerTabPages(ByVal queHacer As Short)
        Try
            If queHacer = poner Then
                TabControl1.TabPages.Add(tpEscandalloTejido)
                TabControl1.TabPages.Add(tabImpresioFitxa)
            Else
                TabControl1.TabPages.Remove(tpEscandalloTejido)
                TabControl1.TabPages.Remove(tabImpresioFitxa)
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub


#End Region

#Region "ESPECÍFICO"

#Region "ACABADOS"

    Private Sub dgAcabados_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgAcabados.AfterColUpdate
        Dim columna As String = e.Column.DataColumn.DataField
        Try
            If PuedoModificar() Then
                tejidoActual.acabados.PonerAcabadosEnTejido()
                tejidoActual.HacerCalculos(True)
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub dgAcabados_AfterDelete(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgAcabados.AfterDelete
        Try
            tejidoActual.acabados.PonerAcabadosEnTejido()
            tejidoActual.HacerCalculos(True)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#End Region

#Region "COMPOSICIONES"

    Private Sub InicializarDgComposicionTejido()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgComposcionTejido)
            i = 0

            PPCol2("COMP", dgComposcionTejido, rm.GetString("HILO"), "",
                    True, 125, False, PresentationEnum.ComboBox, False, 125, i, False, cboHilos)

            i = i + 1

            PPCol2("ELEGIRCOMP", dgComposcionTejido, "", "",
                    True, 20, False, PresentationEnum.Normal, True, 20, i, False)
            i = i + 1

            PPCol2("PER", dgComposcionTejido, "%", "#,##0.00",
                    True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = i + 1

            PPCol2("PROVE", dgComposcionTejido, "", "",
                    True, 50, False, PresentationEnum.Normal, False, 50, i, False, Nothing, False)
            i = i + 1

            PPCol2("NOMPROVE", dgComposcionTejido, rm.GetString("PROVEEDOR"), "",
                    True, 240, False, PresentationEnum.Normal, False, 240, i, False, , Nothing, False)
            i = i + 1

            PPCol2("PREU", dgComposcionTejido, rm.GetString("PRECIO"), "#,##0.00",
                    True, 70, False, PresentationEnum.Normal, False, 70, i, False)
            i = i + 1

            PPCol2("IMPORT", dgComposcionTejido, rm.GetString("IMPORTE"), "#,##0.00",
                    True, 70, False, PresentationEnum.Normal, False, 70, i, False)
            i = i + 1

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Private Sub dgComposcionTejido_ButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgComposcionTejido.ButtonClick
        Try
            If PuedoModificar() Then
                cargando = True
                Select Case e.Column.DataColumn.DataField
                    Case "ELEGIRCOMP"
                        dgComposcionTejido.EditActive = True
                        Cursor = Cursors.WaitCursor
                        Dim f As frmHilosLista = frmHilosLista.GetInstance(esEleccion, tejidoActual.centro)
                        f.ShowDialog()

                        Cursor = Cursors.Default
                        If Not f.dr Is Nothing Then
                            tejidoActual.composicion.PROVE = f.dr("PROVE")
                            tejidoActual.composicion.NOMPROVE = f.dr("NOMPROVE")
                            tejidoActual.composicion.COMP = f.dr("CODI")
                            tejidoActual.composicion.PREU = f.dr("COST")
                        End If
                        f = Nothing
                End Select
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub dgComposcionTejido_BeforeColUpdate(ByVal sender As System.Object, ByVal e As C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs) Handles dgComposcionTejido.BeforeColUpdate
        Try
            If PuedoModificar() Then
                cargando = True
                If e.OldValue <> e.Column.DataColumn.Value AndAlso tejidoActual.composicion.esColParaActualizarPrecio(e.Column.DataColumn.DataField) Then
                    tejidoActual.composicion.hayCambios = True
                Else
                    tejidoActual.composicion.hayCambios = False
                End If
                cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub
    Private Sub dgComposcionTejido_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgComposcionTejido.RowColChange
        Try
            If PuedoModificar() Then
                cargando = True

                'Miramos si hay un cambio de columna
                If e.LastRow <> dgComposcionTejido.Row Then
                    'tejidoActual.composicion.HacerCalculosLineas(True, dgComposcionTejido.Splits(0).DisplayColumns(e.LastCol).DataColumn.DataField)
                Else
                    'tejidoActual.composicion.HacerCalculosLineas(False, dgComposcionTejido.Splits(0).DisplayColumns(e.LastCol).DataColumn.DataField)
                End If

                '            'Asociamos las tablas adecuadas y ajustamos el tamaño
                '            With PAFActual.lineasVenta

                '                If .TIPUS = "T" Then
                '                    dgLineasPAFVenta.Splits(0).DisplayColumns("TALLA").AllowFocus = False
                '                    dgLineasPAFVenta.Splits(0).DisplayColumns("KM").AllowFocus = True
                '                    AñadirBindingCombo(cboMuestra, PAFActual.lineasVenta.dtTeixits.DefaultView, "", "CODI")
                '                    'cboMuestra.DataSource = PAFActual.lineasVenta.dtTeixits
                '                    'cboMuestra.DataMember = "CODI"
                '                    'AutosizeColumnasTrueDBDropdown(cboMuestra)
                '                End If

                '                If .TIPUS = "M" Then
                '                    dgLineasPAFVenta.Splits(0).DisplayColumns("TALLA").AllowFocus = True
                '                    dgLineasPAFVenta.Splits(0).DisplayColumns("KM").AllowFocus = False
                '                    AñadirBindingCombo(cboMuestra2, PAFActual.lineasVenta.dvMuestras, "", "CODI")
                '                    'cboMuestra2.DataSource = PAFActual.lineasVenta.dvMuestras
                '                    'cboMuestra2.DataMember = "CODI"
                '                    'AutosizeColumnasTrueDBDropdown(cboMuestra2)
                '                    cboMuestra2.ReBind(True)

                '                End If

                '            End With

                '            AutosizeColumnasTrueDBDropdown(cboColor)
                '            AutosizeColumnasTrueDBDropdown(cboKM)
                '            AutosizeColumnasTrueDBDropdown(cboTalla)
                '            AutoSizeCC(dgLineasPAFVenta)
                cargando = False

            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub
    Private Sub cboHilos_SelChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles cboHilos.SelChange
        Try
            If PuedoModificar() Then
                cargando = True
                'Debug.WriteLine("QUE ASCO")
                'ObtenerFiladeColumnasC1(cboHilos.Columns, tejidoActual.dtHilos)
                tejidoActual.composicion.COMP = cboHilos.Columns("CODI").Value
                tejidoActual.composicion.PROVE = nzn(cboHilos.Columns("PROVE").Value, 0)
                tejidoActual.composicion.PREU = nzn(cboHilos.Columns("COST").Value, 0)
                tejidoActual.HacerCalculos(True)
                cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub

#End Region
#Region "COLORES"

    Private Sub IniciarDgColores()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgCartaColores)
            i = 0

            PPCol2("COLOR", dgCartaColores, rm.GetString("COLOR"), "",
                    True, 150, False, PresentationEnum.Normal, False, 125, i, False)
            i = i + 1

            PPCol2("ACTUAL", dgCartaColores, rm.GetString("ACTUAL"), "#,##0.00",
                    True, 70, False, PresentationEnum.Normal, False, 70, i, False)
            i = i + 1

            PPCol2("MINIM", dgCartaColores, rm.GetString("MINIMO"), "#,##0.00",
                    True, 70, False, PresentationEnum.Normal, False, 70, i, False)
            i = i + 1

            PPCol2("TINTAR", dgCartaColores, rm.GetString("TINTAR"), "#,##0.00",
                    True, 70, False, PresentationEnum.Normal, False, 70, i, False)
            i = i + 1

            PPCol2("PREU", dgCartaColores, rm.GetString("PRECIO"), "#,##0.00",
                    True, 70, False, PresentationEnum.Normal, False, 70, i, False)
            i = i + 1

            PPCol2("METRES", dgCartaColores, "€/Mts.", "#,##0.00",
                    True, 70, False, PresentationEnum.Normal, False, 70, i, False)
            i = i + 1

            PPCol2("KG", dgCartaColores, "€/KG", "#,##0.00",
                    True, 70, False, PresentationEnum.Normal, False, 70, i, False)
            i = i + 1

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Private Sub IniciarDgAcabados()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgAcabados)
            i = 0
            PPCol2("ACABAT", dgAcabados, rm.GetString("ACABADO"), "",
                    True, 150, False, PresentationEnum.Normal, False, 125, i, False, cboAcabados)
            AñadirBindingCombo(cboAcabados, tejidoActual.acabados.dvTodosLosAcabados, "CODI", "CODI")
            OcultarTodasColumnaCbo(cboAcabados)

            i = i + 1
            PPCol2("ORDEN", dgAcabados, rm.GetString("ORDEN"), "##0",
                    True, 70, False, PresentationEnum.Normal, False, 70, i, False)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region
#Region "DISPOSICIONES"

    Private Sub txtStockCruM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSTCRUM.Validated
        Try
            If PuedoModificar() Then
                cargando = True
                With tejidoActual
                    .STCRUK = roundnum(.STCRUM / .RENDIMENT, 2)
                End With
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtStockCruK_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSTCRUK.Validated
        Try
            If PuedoModificar() Then
                cargando = True
                With tejidoActual
                    .STCRUM = roundnum(.STCRUK * .RENDIMENT, 2)
                End With
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtStockDisposatM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSTDISPM.Validated
        Try
            If PuedoModificar() Then
                cargando = True
                With tejidoActual
                    .STDISPK = roundnum(.STDISPM / .RENDIMENT, 2)
                End With
                cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtStockDisposatK_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSTDISPK.Validated
        Try
            If PuedoModificar() Then
                cargando = True
                With tejidoActual
                    .STDISPM = roundnum(.STDISPK * .RENDIMENT, 2)
                End With
                cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#End Region

#Region "IMPRESIÓN"

    Private Function obtenerComposicion() As String
        Dim i As Integer
        Dim s As String
        Try

            For i = 0 To tejidoActual.composicion.dvForm.Count - 1

                s = s & " " & tejidoActual.composicion.dvForm(i).Item("PER") & "% " & tejidoActual.composicion.dvForm(i).Item("COMP")
            Next
            Return s
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    'Private Sub btnImprimirEtiquetas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim cnTemp As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=access\temp.mdb;User Id=admin;Password=;")


    '    Dim strSQL As String
    '    Dim cmdIns As New OleDb.OleDbCommand

    '    Dim accessDir As New AccessDirecto(tabla)
    '    Dim strComposicion As String
    '    Dim i As Integer = 0
    '    Try
    '        If txtNumeroEtiquetas.Text = 0 Then
    '            MessageBox.Show("Número de etiquetes incorrecte", "Error", MessageBoxButtons.OK)
    '        Else

    '            accessDir.BorrarDatosTablaAccess("tETIQUETASTEJIDO")

    '            cmdIns.Connection = cnTemp
    '            strComposicion = obtenerComposicion()
    '            'MessageBox.Show(strCliewnte)
    '            'En los pedidos de venta el proveedor es el cliente por una futura unificacion de las dos funciones
    '            strSQL = "INSERT INTO tETIQUETASTEJIDO ( CODIGO, COMPOSICION, ANCHO, RENDIMIENTO, PESO,  CONDICIONES, PRECIOKG,  PRECIOMETRO, PRECIOUNIDAD) VALUES("
    '            strSQL = strSQL & " """ & general.NS(cboID.Text) & """, """ & general.NS(strComposicion) & """ , """ & general.NS(tejidoActual.AMPLE) & """, """ & general.NS(tejidoActual.RENDIMENT) & """, """ & general.NS(tejidoActual.GRAMA) & """, """" , """ & general.NS(tejidoActual.PREUK) & """, """ & general.NS(tejidoActual.PREUM) & """, """")"
    '            cmdIns.CommandText = strSQL
    '            cnTemp.Open()

    '            For i = 0 To CType(txtNumeroEtiquetas.Text, Integer) - 1
    '                cmdIns.ExecuteNonQuery()
    '            Next
    '            cnTemp.Close()
    '            accessDir.CargarInformeAccess("iETIQUETASTEJIDO", "Etiquetes Teixit")

    '        End If
    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN()
    '    End Try
    'End Sub
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Try
            If TabControl1.SelectedTab Is tabImpresioFitxa Then
                ppFitxa.Document = tejidoActual.imprimirFicha
            End If
            If TabControl1.SelectedTab Is tabPageValoracion Then
                'Rellena la tabla tejidoActual.dtValaroracionStock
                tejidoActual.GenerarValoracionStock()
                'Carga el tejidoActual.dtValaroracionStock en el grid
                IniciarDgValoracion()
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#End Region

#Region "COMUNES"

    Friend Sub btnRecargar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecargar.Click
        Try
            cargando = True
            Dim id As Object
            id = cboID.Text
            tejidoActual.ActualizarOrigen()
            tejidoActual.CambiarAReg(id, iraregistro)
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub btnVerLista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerLista.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmTejidosLista = frmTejidosLista.GetInstance(esListado, tejidoActual.centro)
            f.MdiParent = Me.MdiParent
            AddHandler f.Closed, AddressOf CType(Me.MdiParent, frmPrincipal).childCerrado
            AddHandler f.Load, AddressOf CType(Me.MdiParent, frmPrincipal).childAbierto : AddHandler f.Activated, AddressOf CType(Me.MdiParent, frmPrincipal).childOcultandoMostrando
            f.Show()
            f.BringToFront()
            PSBTIPO(tejidoActual.centro) : cargando = False
            Cursor = Cursors.Default

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerControlesNuevo(ByVal b As Boolean)
        Try
            If Not b Then
                btnActualizar.Text = rm.GetString("CONFIRMAR")
            Else
                btnActualizar.Text = rm.GetString("ACTUALIZAR")
            End If
            ModiNuev(b)
            cboID.AutoCompletion = Not editando

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerModificables(Optional ByVal b As Boolean = False)
        Try
            PonerMod(b)
            If Not b Then : cboID.DataSource = Nothing
            Else : AñadirBindingCombo(cboID, tejidoActual.dvIdentificadores, CCTeixits, CCTeixits) : tejidoActual.tabla.AcceptChanges() : End If
            cboID.LimitToList = b
            cboID.SuperBack = b
            cboID.ReadOnly = editando
            ModiNuev(b)
            tejidoActual.tabla.AcceptChanges()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "DESPLAZARSE"

    Private Sub MoverActual(Optional ByVal nuevo As Boolean = False)
        Try
            cargando = True
            AutoSizeCC(dgAcabados)
            AutoSizeCC(cboAcabados)
            PSBTIPO(tejidoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnPrimero.Click
        Try
            cargando = True
            tejidoActual.PrimeroReg() : ActualizarOrigen()
            MoverActual()
            PSBTIPO(tejidoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUltimo.Click
        Try
            cargando = True
            tejidoActual.UltimoReg() : ActualizarOrigen()
            MoverActual()
            PSBTIPO(tejidoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAnterior.Click
        Try
            cargando = True
            tejidoActual.AnteriorReg() : ActualizarOrigen()
            MoverActual()
            PSBTIPO(tejidoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSiguiente.Click
        Try
            cargando = True
            tejidoActual.SiguienteReg() : ActualizarOrigen()
            MoverActual()
            PSBTIPO(tejidoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "SELECCIÓN REGISTRO"

    Private Sub cboID_ItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboID.RowChange
        Try
            If consulta() Then
                cargando = True
                tejidoActual.pasarDeTodo = True
                tejidoActual.CambiarAReg(general.nz(cboID.WillChangeToValue, ""), iraregistro)
                MoverActual()
                PSBTIPO(tejidoActual.centro) : tejidoActual.pasarDeTodo = False : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnElegirTejido_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirTejido.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmTejidosLista = frmTejidosLista.GetInstance(esEleccion, tejidoActual.centro)
            f.ShowDialog()
            Cursor = Cursors.Default
            If Not f.dr Is Nothing Then tejidoActual.CambiarAReg(general.nz(f.dr("CODI"), ""), iraregistro)

            MoverActual()
            f = Nothing
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboID.KeyPress
        Try
            If editando Or EsRegistroNuevo Then
                cboID.AutoCompletion = False
            Else
                cboID.AutoCompletion = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub cboSeleccion_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSeleccionCentro.SelectionChangeCommitted
        Try
            If consulta() Then
                cargando = True
                tejidoActual.cambioCentro(general.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto), primero)
                lblNumeroTejidos.Text = rm.GetString("NUMTEJIDOS") & " " & tejidoActual.dvIdentificadores.Count
                cboSeleccionCentro.SelectedValue = tejidoActual.centro
                AutoSizeCC(cboID)
                'tejidoActual.tabla.AcceptChanges()
                cargando = False
            Else
                If EsRegistroNuevo And Not cargando Then
                    cargando = True
                    tejidoActual.centro = general.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto)
                    tejidoActual.tabla.Clear()
                    tejidoActual.NuevoRegistro()
                    PSBTIPO(tejidoActual.centro)
                    cargando = False
                End If
            End If
            tejidoActual.GenerarValoracionStock()
            IniciarDgValoracion()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ELECCIONES"

    Private Sub txtCodigoMaquina_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMAQUI.Validated
        Try
            If PuedoModificar() Then
                tejidoActual.MAQUI = nzn(txtMAQUI.Text, 0)
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtCodigoTejedor_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTEIXIDOR.Validated
        Try
            If PuedoModificar() Then cargando = True : tejidoActual.TEIXIDOR = nzn(txtTEIXIDOR.Text, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtCodigoAcabador_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtACABADOR.Validated

        Try
            If PuedoModificar() Then
                cargando = True
                tejidoActual.ACABADOR = nzn(txtACABADOR.Text, 0)
                tejidoActual.acabados.CargarTodosLosAcabados()
                tejidoActual.acabados.CambioDetalle(tejidoActual.centro, tejidoActual)
                'RellenarListaAcabados(tejidoActual.ACABADOR)
                cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtCodigoEstampador_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtESTAMPADOR.Validated
        Try
            If PuedoModificar() Then cargando = True : tejidoActual.ESTAMPADOR = nzn(txtESTAMPADOR.Text, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtRendimiento_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRENDIMENT.Validated
        Try
            If PuedoModificar() Then cargando = True : tejidoActual.RENDIMENT = nzn(txtRENDIMENT.Text, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Private Sub cboNombreMaquina_selectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMMAQUI.SelectedValueChanged
        Try
            If PuedoModificar() Then cargando = True : tejidoActual.MAQUI = nzn(cboNOMMAQUI.WillChangeToValue, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboNombreTejedor_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMTEIXIDOR.SelectedValueChanged
        Try
            If PuedoModificar() Then cargando = True : tejidoActual.TEIXIDOR = nzn(cboNOMTEIXIDOR.WillChangeToValue, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboNombreEstampador_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMESTAMPADOR.SelectedValueChanged
        Try
            If PuedoModificar() Then cargando = True : tejidoActual.ESTAMPADOR = nzn(cboNOMESTAMPADOR.WillChangeToValue, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboNombreAcabador_ItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMACABADOR.SelectedValueChanged
        Try
            If PuedoModificar() Then
                cargando = True
                tejidoActual.ACABADOR = nzn(cboNOMACABADOR.WillChangeToValue, 0)
                tejidoActual.acabados.CargarTodosLosAcabados()
                cargando = False
            End If

            If PuedoModificar() Then
                cargando = True
                tejidoActual.ACABADOR = nzn(txtACABADOR.Text, 0)
                tejidoActual.acabados.CargarTodosLosAcabados()
                tejidoActual.acabados.CambioDetalle(tejidoActual.centro, tejidoActual)
                'RellenarListaAcabados(tejidoActual.ACABADOR)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Private Sub btnElegirMaquina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirMaquina.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmMaquinasLista = frmMaquinasLista.GetInstance(esEleccion)
            f.ShowDialog()
            Cursor = Cursors.Default
            If Not f.dr Is Nothing Then tejidoActual.MAQUI = f.dr("CODI")
            f = Nothing

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnElegirTejidor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirTejidor.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmTalleresLista = frmTalleresLista.GetInstance(esEleccion)
            f.ShowDialog()
            If Not f.dr Is Nothing Then tejidoActual.TEIXIDOR = f.dr("CODI")
            Cursor = Cursors.Default
            f = Nothing
            PSBTIPO(tejidoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnElegirEstampador_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirEstampador.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmTalleresLista = frmTalleresLista.GetInstance(esEleccion)
            f.ShowDialog()
            If Not f.dr Is Nothing Then tejidoActual.ESTAMPADOR = f.dr("CODI")
            Cursor = Cursors.Default
            f = Nothing
            PSBTIPO(tejidoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnElegirAcabador_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirAcabador.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmTalleresLista = frmTalleresLista.GetInstance(esEleccion)
            f.ShowDialog()
            If Not f.dr Is Nothing Then
                If PuedoModificar() Then tejidoActual.acabados.borraracabados()
                tejidoActual.ACABADOR = f.dr("CODI")
            End If
            Cursor = Cursors.Default
            f = Nothing
            PSBTIPO(tejidoActual.centro) : cargando = False

            If PuedoModificar() Then
                cargando = True
                tejidoActual.ACABADOR = nzn(txtACABADOR.Text, 0)
                tejidoActual.acabados.CargarTodosLosAcabados()
                tejidoActual.acabados.CambioDetalle(tejidoActual.centro, tejidoActual)
                'RellenarListaAcabados(tejidoActual.ACABADOR)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

    Private Sub btnGenerarEtiquetas_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerarEtiquetas.Click
        Dim i As Integer = 0
        Try
            ppv.Document = tejidoActual.imprimirEtiquetas(nzn(txtNumEtiquetas.Value, 0))
            Dim pd As PrintDialog = New PrintDialog()
            pd.PrinterSettings = ppv.Document.PrinterSettings
            If pd.ShowDialog() = DialogResult.OK Then
                ppv.Document.Print()
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub dgCartaColores_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgCartaColores.BeforeDelete
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
    Private Sub dgComposcionTejido_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgComposcionTejido.BeforeDelete
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
    Private Sub frm_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        GrabaParametro(tejidoActual.CODI, "tejido")
    End Sub
    Private Sub btnLimpiarAcabados_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiarAcabados.Click
        Try
            'lstOperaciones.SelectedIndices.Clear()
            'ActualizarOrigenAcabados()
            tejidoActual.acabados.borraracabados()
            tejidoActual.ACABAT = ""
            tejidoActual.PACA = 0
            'tejidoActual.acabados.CambioDetalle(tejidoActual.centro, tejidoActual)
        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub dgAcabados_Error(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ErrorEventArgs) Handles dgAcabados.Error
        Try
            If e.Exception.ToString.IndexOf("System.Data.ConstraintException: Column") <> -1 Then
                MessageBox.Show("No pots seleccionar dos acabats iguals a la llista")
                e.[Continue] = False
                e.Handled = True
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub IniciarDgValoracion()
        Dim i As Integer
        Try
            dgValoracion.SetDataBinding(tejidoActual.dtValoracion.DefaultView, "")
            dgValoracion.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.GroupBy
            'Desplaça aquí una capçalera de columna per agrupar per aquesta columna
            OcultarColumnasDG(dgValoracion)
            i = 0
            'CODIGOTEJEDOR, NOMBRETEJEDOR, SUM(SUMASTOCK) AS STOCKTOTAL ,SUM(VALORACION)  
            PPCol2("CODIGOTEJIDO", dgValoracion, rm.GetString("CODIGO"), "",
                    True, 0, False, PresentationEnum.Normal, False, 1, i, False)
            i = i + 1
            PPCol2("DESCRI", dgValoracion, rm.GetString("DESCRIPCION"), "",
                    True, 0, False, PresentationEnum.Normal, False, 1, i, False)
            i = i + 1
            PPCol2("CODIGOTEJEDOR", dgValoracion, rm.GetString("CODIGOTEJEDOR"), "",
                    True, 0, False, PresentationEnum.Normal, False, 1, i, False)
            i = i + 1
            PPCol2("NOMBRETEJEDOR", dgValoracion, rm.GetString("NOMBRE"), "",
                    True, 0, False, PresentationEnum.Normal, False, 1, i, False)
            i = i + 1
            '#,##0.00
            PPCol2("STOCKTOTAL", dgValoracion, "Stock Total Acabat", "#,##0.00",
                    True, 0, False, PresentationEnum.Normal, False, 1, i, False)
            i = i + 1
            PPCol2("VALORACION", dgValoracion, rm.GetString("VALORACION"), "",
                    True, 0, False, PresentationEnum.Normal, False, 1, i, False)
            i = i + 1
            PPCol2("STOCKTOTALCRUDO", dgValoracion, "Stock Total CRU", "#,##0.00",
                True, 0, False, PresentationEnum.Normal, False, 1, i, False)
            i = i + 1
            PPCol2("VALORSTOCKCRUDO", dgValoracion, "Valoració Stock CRU", "#,##0.00",
                True, 0, False, PresentationEnum.Normal, False, 1, i, False)
            i = i + 1
            AutoSizeCC(dgValoracion)
            dgValoracion.Refresh()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Private Sub btnExportarPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportarPDF.Click
        Dim sDBPath As String
        Dim NavArchivos As System.Windows.Forms.SaveFileDialog
        Try

            NavArchivos = New System.Windows.Forms.SaveFileDialog
            NavArchivos.Filter = "Arxius pdf (*.pdf) | *.pdf"
            NavArchivos.InitialDirectory = "c:\"
            NavArchivos.ShowDialog()
            sDBPath = NavArchivos.FileName
            dgValoracion.ExportToPDF(sDBPath)


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnExportarExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportarExcel.Click
        Dim sDBPath As String
        Dim NavArchivos As System.Windows.Forms.SaveFileDialog
        Try

            'NavArchivos = New System.Windows.Forms.SaveFileDialog
            'NavArchivos.Filter = "Arxius pdf (*.xls) | *.xls"
            'NavArchivos.InitialDirectory = "c:\"
            ''NavArchivos.ShowDialog()
            'sDBPath = NavArchivos.FileName
            'dgValoracion.ExportToExcel(sDBPath)
            CrearExcel(dgValoracion, "Llistat Valorat", False)
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Sub CrearExcel(ByVal dg As C1.Win.C1TrueDBGrid.C1TrueDBGrid,
                                     ByVal titulo As String,
                                     ByVal vistaprevia As Boolean)
        Dim m_Excel As Excel.Application
        Dim i, j, z As Integer
        '' Creamos un objeto WorkBook 
        Dim objLibroExcel As Excel.Workbook
        '' Creamos un objeto WorkSheet
        Dim objHojaExcel As Excel.Worksheet
        Try

            m_Excel = New Excel.Application
            m_Excel.Visible = False

            '' Creamos una instancia del Workbooks de excel
            '' Creamos una instancia de la primera hoja de trabajo de excel
            objLibroExcel = m_Excel.Workbooks.Add()
            objHojaExcel = objLibroExcel.Worksheets(1)
            m_Excel.Application.ScreenUpdating = False
            m_Excel.Calculation = Excel.XlCalculation.xlCalculationManual
            objHojaExcel.Name = titulo
            objHojaExcel.Visible = Excel.XlSheetVisibility.xlSheetVisible
            '' Hacemos esta hoja la visible en pantalla 
            '' (como seleccionamos la primera esto no es necesario
            '' si seleccionamos una diferente a la primera si lo
            '' necesitariamos).
            objHojaExcel.Activate()
            objHojaExcel.Range("A1:D1").Merge()
            objHojaExcel.Range("A1:D1").Value = titulo
            objHojaExcel.Range("A1:D1").Font.Bold = True
            objHojaExcel.Range("A1:D1").Font.Size = 15

            z = 0
            For i = 0 To dg.Splits(0).DisplayColumns.Count - 1

                If dg.Splits(0).DisplayColumns(i).Visible = True Then
                    objHojaExcel.Cells(3, z + 1).Font.Bold = True
                    objHojaExcel.Cells(3, z + 1).Value = dg.Splits(0).DisplayColumns(i).DataColumn.Caption
                    z = z + 1
                End If
            Next

            For i = 0 To dg.Splits(0).Rows.Count - 1
                Application.DoEvents()
                z = 0
                For j = 0 To dg.Splits(0).DisplayColumns.Count - 1
                    If dg.Splits(0).DisplayColumns(j).Visible = True Then
                        objHojaExcel.Cells(i + 5, z + 1) = dg.Item(i, dg.Splits(0).DisplayColumns(j).Name)
                        z = z + 1
                    End If

                Next
            Next

            objHojaExcel.Cells.EntireColumn.AutoFit()
            '' Selecionado todo el rango especificado
            If vistaprevia Then
                objLibroExcel.PrintPreview()
            Else

            End If

            objHojaExcel = Nothing
            objLibroExcel = Nothing
            m_Excel.Calculation = Excel.XlCalculation.xlCalculationAutomatic
            m_Excel.Visible = True
            m_Excel.ScreenUpdating = True

            ' xls1.Save("prueba1.xls")
            ' abrirExcel()
        Catch ex1 As Exception
            MessageBox.Show(ex1.ToString)
        End Try

    End Sub

    Private Sub txtAMPLENOU_Validated(sender As Object, e As EventArgs) Handles txtAMPLENOU.Validated
        tejidoActual.MetroLineal(nzn(txtAMPLENOU.Text, 0), nzn(txtGRAMA.Text, 0))
    End Sub

    Private Sub txtGRAMA_Validated(sender As Object, e As EventArgs) Handles txtGRAMA.Validated
        tejidoActual.MetroLineal(nzn(txtAMPLENOU.Text, 0), nzn(txtGRAMA.Text, 0))
    End Sub
End Class

