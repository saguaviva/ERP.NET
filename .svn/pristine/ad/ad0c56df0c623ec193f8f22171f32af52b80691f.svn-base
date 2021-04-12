Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Imports C1.Win.C1TrueDBGrid
Imports C1.Win.C1PrintPreview

Public Class frmFacturasModelos

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

    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl 'XPTabControl.TabControlXP
    Friend WithEvents lblComision As Label
    Friend WithEvents lblPrecio As Label
    Friend WithEvents btnElegirRepresentante As C1.Win.C1Input.C1Button
    Friend WithEvents lblRepresentante As Label
    Friend WithEvents btnElegirCliente As C1.Win.C1Input.C1Button
    Friend WithEvents lblCliente As Label
    Friend WithEvents btnImprimir As C1.Win.C1Input.C1Button
    Friend WithEvents tabPageOrden As System.Windows.Forms.TabPage
    Friend WithEvents lblFecha As Label
    Friend WithEvents lblAlbaran As Label
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lblFacturaV As Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents dgFacturas As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgVencimientos As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents txtPREU As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboNOMREPRES As C1.Win.C1List.C1Combo
    Friend WithEvents txtCOMIS As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtREPRES As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtOBSERV As C1.Win.C1Input.C1TextBox
    Friend WithEvents dtpDATA As C1.Win.C1Input.C1DateEdit
    Friend WithEvents cboNOMCLIENT As C1.Win.C1List.C1Combo
    Friend WithEvents txtCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents dt2DATA As C1.Win.C1Input.C1DateEdit
    Friend WithEvents tx2NOMCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx2FRA As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx2TOTAL As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtIVA1 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtRE1 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtBASE1 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtDTE1 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTOTAL As C1.Win.C1Input.C1TextBox
    Protected WithEvents lblRE As System.Windows.Forms.Label
    Protected WithEvents lblTotal As System.Windows.Forms.Label
    Protected WithEvents lblIVA As System.Windows.Forms.Label
    Protected WithEvents lblDescuento As System.Windows.Forms.Label
    Protected WithEvents lblBase As System.Windows.Forms.Label
    Protected WithEvents lblTotalBruto As System.Windows.Forms.Label
    Friend WithEvents gbEleccionClientesPAF As System.Windows.Forms.GroupBox
    Friend WithEvents lblNumeroPAF As System.Windows.Forms.Label
    Friend WithEvents cboSeleccionClienteParaPAF As C1.Win.C1List.C1Combo
    Friend WithEvents rdoVerTodasPAF As System.Windows.Forms.RadioButton
    Friend WithEvents rdoVerPAFDeCliente As System.Windows.Forms.RadioButton
    Friend WithEvents gbCentro As System.Windows.Forms.GroupBox
    Friend WithEvents lblNumeracion As System.Windows.Forms.Label
    Friend WithEvents cboFRA As C1.Win.C1List.C1Combo
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents XpGroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblImpReciboAFecha As System.Windows.Forms.Label
    Friend WithEvents lblImpReciboDeFecha As System.Windows.Forms.Label
    Friend WithEvents dtpAFechaRecibo As C1.Win.C1Input.C1DateEdit
    Friend WithEvents dtpDeFechaRecibo As C1.Win.C1Input.C1DateEdit
    Friend WithEvents XpGroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cboAFacturaRecibo As C1.Win.C1List.C1Combo
    Friend WithEvents cboDeFacuraRecibo As C1.Win.C1List.C1Combo
    Friend WithEvents lblAFactura As System.Windows.Forms.Label
    Friend WithEvents lblDeFactura As System.Windows.Forms.Label
    Friend WithEvents btnGenerarRecibo As C1.Win.C1Input.C1Button
    Friend WithEvents btnGenerarVencim As C1.Win.C1Input.C1Button
    Friend WithEvents chkPorIntervaloFechas As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents gbImpresion As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboAPAFIMPRESION As C1.Win.C1List.C1Combo
    Friend WithEvents cboDEPAFIMPRESION As C1.Win.C1List.C1Combo
    Protected WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblFormaDePago As System.Windows.Forms.Label
    Friend WithEvents lblFORMA As System.Windows.Forms.Label
    Friend WithEvents lblDia3 As System.Windows.Forms.Label
    Friend WithEvents lblDia_3 As System.Windows.Forms.Label
    Friend WithEvents lblDia1 As System.Windows.Forms.Label
    Friend WithEvents lblDia_2 As System.Windows.Forms.Label
    Friend WithEvents lblRE2 As System.Windows.Forms.Label
    Friend WithEvents lblRE_ As System.Windows.Forms.Label
    Friend WithEvents lblIVA2 As System.Windows.Forms.Label
    Friend WithEvents lblIVA_ As System.Windows.Forms.Label
    Friend WithEvents lblDia_1 As System.Windows.Forms.Label
    Friend WithEvents lblDia2 As System.Windows.Forms.Label
    Friend WithEvents tx2CLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtBRUT1 As C1.Win.C1Input.C1TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ppfitxa2 As C1.Win.C1Preview.C1PrintPreviewControl
    Friend WithEvents txtP_DTE As C1.Win.C1Input.C1TextBox


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFacturasModelos))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabPageOrden = New System.Windows.Forms.TabPage()
        Me.txtBRUT1 = New C1.Win.C1Input.C1TextBox()
        Me.txtIVA1 = New C1.Win.C1Input.C1TextBox()
        Me.txtRE1 = New C1.Win.C1Input.C1TextBox()
        Me.txtBASE1 = New C1.Win.C1Input.C1TextBox()
        Me.txtDTE1 = New C1.Win.C1Input.C1TextBox()
        Me.txtTOTAL = New C1.Win.C1Input.C1TextBox()
        Me.lblRE = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblIVA = New System.Windows.Forms.Label()
        Me.lblDescuento = New System.Windows.Forms.Label()
        Me.lblBase = New System.Windows.Forms.Label()
        Me.lblTotalBruto = New System.Windows.Forms.Label()
        Me.txtOBSERV = New C1.Win.C1Input.C1TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtP_DTE = New C1.Win.C1Input.C1TextBox()
        Me.dtpDATA = New C1.Win.C1Input.C1DateEdit()
        Me.txtCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.lblRepresentante = New System.Windows.Forms.Label()
        Me.lblPrecio = New System.Windows.Forms.Label()
        Me.txtPREU = New C1.Win.C1Input.C1TextBox()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.cboNOMREPRES = New C1.Win.C1List.C1Combo()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.btnElegirCliente = New C1.Win.C1Input.C1Button()
        Me.cboNOMCLIENT = New C1.Win.C1List.C1Combo()
        Me.cboFRA = New C1.Win.C1List.C1Combo()
        Me.lblAlbaran = New System.Windows.Forms.Label()
        Me.lblComision = New System.Windows.Forms.Label()
        Me.txtCOMIS = New C1.Win.C1Input.C1TextBox()
        Me.txtREPRES = New C1.Win.C1Input.C1TextBox()
        Me.btnElegirRepresentante = New C1.Win.C1Input.C1Button()
        Me.dgFacturas = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.XpGroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkPorIntervaloFechas = New System.Windows.Forms.CheckBox()
        Me.lblImpReciboAFecha = New System.Windows.Forms.Label()
        Me.lblImpReciboDeFecha = New System.Windows.Forms.Label()
        Me.dtpAFechaRecibo = New C1.Win.C1Input.C1DateEdit()
        Me.dtpDeFechaRecibo = New C1.Win.C1Input.C1DateEdit()
        Me.cboAFacturaRecibo = New C1.Win.C1List.C1Combo()
        Me.cboDeFacuraRecibo = New C1.Win.C1List.C1Combo()
        Me.lblAFactura = New System.Windows.Forms.Label()
        Me.lblDeFactura = New System.Windows.Forms.Label()
        Me.XpGroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnGenerarRecibo = New C1.Win.C1Input.C1Button()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
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
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.dt2DATA = New C1.Win.C1Input.C1DateEdit()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tx2NOMCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.lblFacturaV = New System.Windows.Forms.Label()
        Me.tx2FRA = New C1.Win.C1Input.C1TextBox()
        Me.tx2CLIENT = New C1.Win.C1Input.C1TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnGenerarVencim = New C1.Win.C1Input.C1Button()
        Me.dgVencimientos = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tx2TOTAL = New C1.Win.C1Input.C1TextBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.gbImpresion = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboAPAFIMPRESION = New C1.Win.C1List.C1Combo()
        Me.cboDEPAFIMPRESION = New C1.Win.C1List.C1Combo()
        Me.btnImprimir = New C1.Win.C1Input.C1Button()
        Me.gbEleccionClientesPAF = New System.Windows.Forms.GroupBox()
        Me.lblNumeroPAF = New System.Windows.Forms.Label()
        Me.cboSeleccionClienteParaPAF = New C1.Win.C1List.C1Combo()
        Me.rdoVerTodasPAF = New System.Windows.Forms.RadioButton()
        Me.rdoVerPAFDeCliente = New System.Windows.Forms.RadioButton()
        Me.gbCentro = New System.Windows.Forms.GroupBox()
        Me.lblNumeracion = New System.Windows.Forms.Label()
        Me.ppfitxa2 = New C1.Win.C1Preview.C1PrintPreviewControl()
        Me.TabControl1.SuspendLayout()
        Me.tabPageOrden.SuspendLayout()
        CType(Me.txtBRUT1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIVA1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBASE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDTE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTOTAL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtP_DTE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDATA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPREU, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMREPRES, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFRA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCOMIS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREPRES, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgFacturas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.XpGroupBox1.SuspendLayout()
        CType(Me.dtpAFechaRecibo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDeFechaRecibo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboAFacturaRecibo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDeFacuraRecibo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        CType(Me.dt2DATA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx2NOMCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx2FRA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx2CLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.dgVencimientos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx2TOTAL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.gbImpresion.SuspendLayout()
        CType(Me.cboAPAFIMPRESION, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDEPAFIMPRESION, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbEleccionClientesPAF.SuspendLayout()
        CType(Me.cboSeleccionClienteParaPAF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbCentro.SuspendLayout()
        CType(Me.ppfitxa2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ppfitxa2.SuspendLayout()
        Me.SuspendLayout()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(5, 196)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(49, 574)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(297, 594)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(17, 594)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(17, 574)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(297, 574)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(137, 594)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(569, 594)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(217, 594)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(350, 571)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(49, 594)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(569, 574)
        '
        'cboSeleccionCentro
        '
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabPageOrden)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.ItemSize = New System.Drawing.Size(42, 18)
        Me.TabControl1.Location = New System.Drawing.Point(8, 64)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(980, 504)
        Me.TabControl1.TabIndex = 0
        '
        'tabPageOrden
        '
        Me.tabPageOrden.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.tabPageOrden.Controls.Add(Me.txtBRUT1)
        Me.tabPageOrden.Controls.Add(Me.txtIVA1)
        Me.tabPageOrden.Controls.Add(Me.txtRE1)
        Me.tabPageOrden.Controls.Add(Me.txtBASE1)
        Me.tabPageOrden.Controls.Add(Me.txtDTE1)
        Me.tabPageOrden.Controls.Add(Me.txtTOTAL)
        Me.tabPageOrden.Controls.Add(Me.lblRE)
        Me.tabPageOrden.Controls.Add(Me.lblTotal)
        Me.tabPageOrden.Controls.Add(Me.lblIVA)
        Me.tabPageOrden.Controls.Add(Me.lblDescuento)
        Me.tabPageOrden.Controls.Add(Me.lblBase)
        Me.tabPageOrden.Controls.Add(Me.lblTotalBruto)
        Me.tabPageOrden.Controls.Add(Me.txtOBSERV)
        Me.tabPageOrden.Controls.Add(Me.GroupBox1)
        Me.tabPageOrden.Controls.Add(Me.dgFacturas)
        Me.tabPageOrden.Location = New System.Drawing.Point(4, 22)
        Me.tabPageOrden.Name = "tabPageOrden"
        Me.tabPageOrden.Size = New System.Drawing.Size(972, 478)
        Me.tabPageOrden.TabIndex = 0
        Me.tabPageOrden.Text = "Factura"
        '
        'txtBRUT1
        '
        Me.txtBRUT1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBRUT1.Location = New System.Drawing.Point(756, 452)
        Me.txtBRUT1.Name = "txtBRUT1"
        Me.txtBRUT1.Size = New System.Drawing.Size(70, 20)
        Me.txtBRUT1.TabIndex = 281
        Me.txtBRUT1.Tag = Nothing
        '
        'txtIVA1
        '
        Me.txtIVA1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtIVA1.Location = New System.Drawing.Point(604, 452)
        Me.txtIVA1.Name = "txtIVA1"
        Me.txtIVA1.Size = New System.Drawing.Size(70, 20)
        Me.txtIVA1.TabIndex = 280
        Me.txtIVA1.Tag = Nothing
        '
        'txtRE1
        '
        Me.txtRE1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRE1.Location = New System.Drawing.Point(484, 452)
        Me.txtRE1.Name = "txtRE1"
        Me.txtRE1.Size = New System.Drawing.Size(70, 20)
        Me.txtRE1.TabIndex = 279
        Me.txtRE1.Tag = Nothing
        '
        'txtBASE1
        '
        Me.txtBASE1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBASE1.Location = New System.Drawing.Point(372, 452)
        Me.txtBASE1.Name = "txtBASE1"
        Me.txtBASE1.Size = New System.Drawing.Size(70, 20)
        Me.txtBASE1.TabIndex = 278
        Me.txtBASE1.Tag = Nothing
        '
        'txtDTE1
        '
        Me.txtDTE1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDTE1.Location = New System.Drawing.Point(228, 452)
        Me.txtDTE1.Name = "txtDTE1"
        Me.txtDTE1.Size = New System.Drawing.Size(70, 20)
        Me.txtDTE1.TabIndex = 277
        Me.txtDTE1.Tag = Nothing
        '
        'txtTOTAL
        '
        Me.txtTOTAL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTOTAL.Location = New System.Drawing.Point(888, 452)
        Me.txtTOTAL.Name = "txtTOTAL"
        Me.txtTOTAL.Size = New System.Drawing.Size(70, 20)
        Me.txtTOTAL.TabIndex = 276
        Me.txtTOTAL.Tag = Nothing
        '
        'lblRE
        '
        Me.lblRE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRE.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRE.Location = New System.Drawing.Point(448, 452)
        Me.lblRE.Name = "lblRE"
        Me.lblRE.Size = New System.Drawing.Size(30, 20)
        Me.lblRE.TabIndex = 275
        Me.lblRE.Text = "RE"
        Me.lblRE.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotal.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTotal.Location = New System.Drawing.Point(840, 452)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(44, 20)
        Me.lblTotal.TabIndex = 274
        Me.lblTotal.Text = "TOTAL"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIVA
        '
        Me.lblIVA.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblIVA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblIVA.Location = New System.Drawing.Point(568, 452)
        Me.lblIVA.Name = "lblIVA"
        Me.lblIVA.Size = New System.Drawing.Size(30, 20)
        Me.lblIVA.TabIndex = 273
        Me.lblIVA.Text = "IVA"
        Me.lblIVA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDescuento
        '
        Me.lblDescuento.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDescuento.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDescuento.Location = New System.Drawing.Point(188, 452)
        Me.lblDescuento.Name = "lblDescuento"
        Me.lblDescuento.Size = New System.Drawing.Size(36, 20)
        Me.lblDescuento.TabIndex = 272
        Me.lblDescuento.Text = "Dte.:"
        Me.lblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBase
        '
        Me.lblBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBase.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblBase.Location = New System.Drawing.Point(304, 452)
        Me.lblBase.Name = "lblBase"
        Me.lblBase.Size = New System.Drawing.Size(66, 20)
        Me.lblBase.TabIndex = 271
        Me.lblBase.Text = "Base"
        Me.lblBase.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalBruto
        '
        Me.lblTotalBruto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalBruto.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTotalBruto.Location = New System.Drawing.Point(688, 452)
        Me.lblTotalBruto.Name = "lblTotalBruto"
        Me.lblTotalBruto.Size = New System.Drawing.Size(66, 20)
        Me.lblTotalBruto.TabIndex = 270
        Me.lblTotalBruto.Text = "Total Brut"
        Me.lblTotalBruto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOBSERV
        '
        Me.txtOBSERV.Location = New System.Drawing.Point(560, 8)
        Me.txtOBSERV.Multiline = True
        Me.txtOBSERV.Name = "txtOBSERV"
        Me.txtOBSERV.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtOBSERV.Size = New System.Drawing.Size(398, 148)
        Me.txtOBSERV.TabIndex = 269
        Me.txtOBSERV.Tag = Nothing
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtP_DTE)
        Me.GroupBox1.Controls.Add(Me.dtpDATA)
        Me.GroupBox1.Controls.Add(Me.txtCLIENT)
        Me.GroupBox1.Controls.Add(Me.lblRepresentante)
        Me.GroupBox1.Controls.Add(Me.lblPrecio)
        Me.GroupBox1.Controls.Add(Me.txtPREU)
        Me.GroupBox1.Controls.Add(Me.lblCliente)
        Me.GroupBox1.Controls.Add(Me.cboNOMREPRES)
        Me.GroupBox1.Controls.Add(Me.lblFecha)
        Me.GroupBox1.Controls.Add(Me.btnElegirCliente)
        Me.GroupBox1.Controls.Add(Me.cboNOMCLIENT)
        Me.GroupBox1.Controls.Add(Me.cboFRA)
        Me.GroupBox1.Controls.Add(Me.lblAlbaran)
        Me.GroupBox1.Controls.Add(Me.lblComision)
        Me.GroupBox1.Controls.Add(Me.txtCOMIS)
        Me.GroupBox1.Controls.Add(Me.txtREPRES)
        Me.GroupBox1.Controls.Add(Me.btnElegirRepresentante)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(528, 140)
        Me.GroupBox1.TabIndex = 268
        Me.GroupBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(188, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 20)
        Me.Label2.TabIndex = 381
        Me.Label2.Text = "% Descompte"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtP_DTE
        '
        Me.txtP_DTE.Location = New System.Drawing.Point(282, 100)
        Me.txtP_DTE.Name = "txtP_DTE"
        Me.txtP_DTE.Size = New System.Drawing.Size(48, 20)
        Me.txtP_DTE.TabIndex = 380
        Me.txtP_DTE.Tag = Nothing
        '
        'dtpDATA
        '
        '
        '
        '
        Me.dtpDATA.Calendar.DayNameLength = 1
        Me.dtpDATA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpDATA.DisableOnNoData = False
        Me.dtpDATA.EmptyAsNull = True
        Me.dtpDATA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpDATA.Location = New System.Drawing.Point(400, 24)
        Me.dtpDATA.Name = "dtpDATA"
        Me.dtpDATA.Size = New System.Drawing.Size(108, 20)
        Me.dtpDATA.TabIndex = 379
        Me.dtpDATA.Tag = Nothing
        '
        'txtCLIENT
        '
        Me.txtCLIENT.Location = New System.Drawing.Point(110, 50)
        Me.txtCLIENT.Name = "txtCLIENT"
        Me.txtCLIENT.Size = New System.Drawing.Size(48, 20)
        Me.txtCLIENT.TabIndex = 2
        Me.txtCLIENT.Tag = Nothing
        '
        'lblRepresentante
        '
        Me.lblRepresentante.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRepresentante.Location = New System.Drawing.Point(16, 76)
        Me.lblRepresentante.Name = "lblRepresentante"
        Me.lblRepresentante.Size = New System.Drawing.Size(85, 20)
        Me.lblRepresentante.TabIndex = 51
        Me.lblRepresentante.Text = "Representant"
        Me.lblRepresentante.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPrecio
        '
        Me.lblPrecio.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPrecio.Location = New System.Drawing.Point(370, 100)
        Me.lblPrecio.Name = "lblPrecio"
        Me.lblPrecio.Size = New System.Drawing.Size(85, 20)
        Me.lblPrecio.TabIndex = 55
        Me.lblPrecio.Text = "Preu"
        Me.lblPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblPrecio.Visible = False
        '
        'txtPREU
        '
        Me.txtPREU.Location = New System.Drawing.Point(458, 100)
        Me.txtPREU.Name = "txtPREU"
        Me.txtPREU.Size = New System.Drawing.Size(48, 20)
        Me.txtPREU.TabIndex = 7
        Me.txtPREU.Tag = Nothing
        Me.txtPREU.Visible = False
        '
        'lblCliente
        '
        Me.lblCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCliente.Location = New System.Drawing.Point(16, 50)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(85, 20)
        Me.lblCliente.TabIndex = 47
        Me.lblCliente.Text = "Client"
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboNOMREPRES
        '
        Me.cboNOMREPRES.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMREPRES.AutoSelect = True
        Me.cboNOMREPRES.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMREPRES.Caption = ""
        Me.cboNOMREPRES.CaptionHeight = 17
        Me.cboNOMREPRES.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMREPRES.ColumnCaptionHeight = 17
        Me.cboNOMREPRES.ColumnFooterHeight = 17
        Me.cboNOMREPRES.ContentHeight = 15
        Me.cboNOMREPRES.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMREPRES.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMREPRES.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMREPRES.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMREPRES.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMREPRES.EditorHeight = 15
        Me.cboNOMREPRES.Images.Add(CType(resources.GetObject("cboNOMREPRES.Images"), System.Drawing.Image))
        Me.cboNOMREPRES.IntegralHeight = True
        Me.cboNOMREPRES.ItemHeight = 13
        Me.cboNOMREPRES.Location = New System.Drawing.Point(186, 76)
        Me.cboNOMREPRES.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMREPRES.MaxDropDownItems = CType(8, Short)
        Me.cboNOMREPRES.MaxLength = 255
        Me.cboNOMREPRES.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMREPRES.Name = "cboNOMREPRES"
        Me.cboNOMREPRES.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMREPRES.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMREPRES.Size = New System.Drawing.Size(292, 19)
        Me.cboNOMREPRES.TabIndex = 5
        Me.cboNOMREPRES.PropBag = resources.GetString("cboNOMREPRES.PropBag")
        '
        'lblFecha
        '
        Me.lblFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFecha.Location = New System.Drawing.Point(316, 24)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(85, 20)
        Me.lblFecha.TabIndex = 36
        Me.lblFecha.Text = "Data"
        Me.lblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnElegirCliente
        '
        Me.btnElegirCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirCliente.Location = New System.Drawing.Point(154, 50)
        Me.btnElegirCliente.Name = "btnElegirCliente"
        Me.btnElegirCliente.Size = New System.Drawing.Size(26, 20)
        Me.btnElegirCliente.TabIndex = 48
        Me.btnElegirCliente.Text = "..."
        '
        'cboNOMCLIENT
        '
        Me.cboNOMCLIENT.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMCLIENT.AutoSelect = True
        Me.cboNOMCLIENT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMCLIENT.Caption = ""
        Me.cboNOMCLIENT.CaptionHeight = 17
        Me.cboNOMCLIENT.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMCLIENT.ColumnCaptionHeight = 17
        Me.cboNOMCLIENT.ColumnFooterHeight = 17
        Me.cboNOMCLIENT.ContentHeight = 15
        Me.cboNOMCLIENT.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMCLIENT.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMCLIENT.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMCLIENT.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMCLIENT.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMCLIENT.EditorHeight = 15
        Me.cboNOMCLIENT.Images.Add(CType(resources.GetObject("cboNOMCLIENT.Images"), System.Drawing.Image))
        Me.cboNOMCLIENT.IntegralHeight = True
        Me.cboNOMCLIENT.ItemHeight = 13
        Me.cboNOMCLIENT.Location = New System.Drawing.Point(186, 50)
        Me.cboNOMCLIENT.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMCLIENT.MaxDropDownItems = CType(8, Short)
        Me.cboNOMCLIENT.MaxLength = 0
        Me.cboNOMCLIENT.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMCLIENT.Name = "cboNOMCLIENT"
        Me.cboNOMCLIENT.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMCLIENT.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMCLIENT.Size = New System.Drawing.Size(292, 19)
        Me.cboNOMCLIENT.TabIndex = 3
        Me.cboNOMCLIENT.PropBag = resources.GetString("cboNOMCLIENT.PropBag")
        '
        'cboFRA
        '
        Me.cboFRA.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboFRA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboFRA.Caption = ""
        Me.cboFRA.CaptionHeight = 17
        Me.cboFRA.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboFRA.ColumnCaptionHeight = 17
        Me.cboFRA.ColumnFooterHeight = 17
        Me.cboFRA.ContentHeight = 15
        Me.cboFRA.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboFRA.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboFRA.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboFRA.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboFRA.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboFRA.EditorHeight = 15
        Me.cboFRA.Images.Add(CType(resources.GetObject("cboFRA.Images"), System.Drawing.Image))
        Me.cboFRA.IntegralHeight = True
        Me.cboFRA.ItemHeight = 13
        Me.cboFRA.Location = New System.Drawing.Point(108, 24)
        Me.cboFRA.MatchEntryTimeout = CType(100, Long)
        Me.cboFRA.MaxDropDownItems = CType(15, Short)
        Me.cboFRA.MaxLength = 0
        Me.cboFRA.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboFRA.Name = "cboFRA"
        Me.cboFRA.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboFRA.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboFRA.Size = New System.Drawing.Size(120, 19)
        Me.cboFRA.TabIndex = 0
        Me.cboFRA.PropBag = resources.GetString("cboFRA.PropBag")
        '
        'lblAlbaran
        '
        Me.lblAlbaran.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAlbaran.Location = New System.Drawing.Point(16, 24)
        Me.lblAlbaran.Name = "lblAlbaran"
        Me.lblAlbaran.Size = New System.Drawing.Size(85, 20)
        Me.lblAlbaran.TabIndex = 239
        Me.lblAlbaran.Text = "Factura"
        Me.lblAlbaran.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblComision
        '
        Me.lblComision.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblComision.Location = New System.Drawing.Point(16, 100)
        Me.lblComision.Name = "lblComision"
        Me.lblComision.Size = New System.Drawing.Size(85, 20)
        Me.lblComision.TabIndex = 57
        Me.lblComision.Text = "% Comissió"
        Me.lblComision.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCOMIS
        '
        Me.txtCOMIS.Location = New System.Drawing.Point(110, 100)
        Me.txtCOMIS.Name = "txtCOMIS"
        Me.txtCOMIS.Size = New System.Drawing.Size(48, 20)
        Me.txtCOMIS.TabIndex = 6
        Me.txtCOMIS.Tag = Nothing
        '
        'txtREPRES
        '
        Me.txtREPRES.Location = New System.Drawing.Point(110, 76)
        Me.txtREPRES.Name = "txtREPRES"
        Me.txtREPRES.Size = New System.Drawing.Size(48, 20)
        Me.txtREPRES.TabIndex = 4
        Me.txtREPRES.Tag = Nothing
        '
        'btnElegirRepresentante
        '
        Me.btnElegirRepresentante.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirRepresentante.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirRepresentante.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirRepresentante.Location = New System.Drawing.Point(154, 76)
        Me.btnElegirRepresentante.Name = "btnElegirRepresentante"
        Me.btnElegirRepresentante.Size = New System.Drawing.Size(26, 20)
        Me.btnElegirRepresentante.TabIndex = 52
        Me.btnElegirRepresentante.Text = "..."
        '
        'dgFacturas
        '
        Me.dgFacturas.AllowAddNew = True
        Me.dgFacturas.AllowDelete = True
        Me.dgFacturas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgFacturas.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgFacturas.Images.Add(CType(resources.GetObject("dgFacturas.Images"), System.Drawing.Image))
        Me.dgFacturas.Location = New System.Drawing.Point(10, 164)
        Me.dgFacturas.Name = "dgFacturas"
        Me.dgFacturas.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgFacturas.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgFacturas.PreviewInfo.ZoomFactor = 75.0R
        Me.dgFacturas.PrintInfo.PageSettings = CType(resources.GetObject("dgFacturas.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgFacturas.Size = New System.Drawing.Size(948, 278)
        Me.dgFacturas.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation
        Me.dgFacturas.TabIndex = 267
        Me.dgFacturas.Text = "C1TrueDBGrid1"
        Me.dgFacturas.WrapCellPointer = True
        Me.dgFacturas.PropBag = resources.GetString("dgFacturas.PropBag")
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.ppfitxa2)
        Me.TabPage2.Controls.Add(Me.XpGroupBox1)
        Me.TabPage2.Controls.Add(Me.XpGroupBox2)
        Me.TabPage2.Controls.Add(Me.btnGenerarRecibo)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(972, 478)
        Me.TabPage2.TabIndex = 2
        Me.TabPage2.Text = "Rebuts"
        '
        'XpGroupBox1
        '
        Me.XpGroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.XpGroupBox1.Controls.Add(Me.chkPorIntervaloFechas)
        Me.XpGroupBox1.Controls.Add(Me.lblImpReciboAFecha)
        Me.XpGroupBox1.Controls.Add(Me.lblImpReciboDeFecha)
        Me.XpGroupBox1.Controls.Add(Me.dtpAFechaRecibo)
        Me.XpGroupBox1.Controls.Add(Me.dtpDeFechaRecibo)
        Me.XpGroupBox1.Controls.Add(Me.cboAFacturaRecibo)
        Me.XpGroupBox1.Controls.Add(Me.cboDeFacuraRecibo)
        Me.XpGroupBox1.Controls.Add(Me.lblAFactura)
        Me.XpGroupBox1.Controls.Add(Me.lblDeFactura)
        Me.XpGroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.XpGroupBox1.Location = New System.Drawing.Point(10, 8)
        Me.XpGroupBox1.Name = "XpGroupBox1"
        Me.XpGroupBox1.Size = New System.Drawing.Size(268, 192)
        Me.XpGroupBox1.TabIndex = 243
        Me.XpGroupBox1.TabStop = False
        '
        'chkPorIntervaloFechas
        '
        Me.chkPorIntervaloFechas.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkPorIntervaloFechas.Location = New System.Drawing.Point(96, 80)
        Me.chkPorIntervaloFechas.Name = "chkPorIntervaloFechas"
        Me.chkPorIntervaloFechas.Size = New System.Drawing.Size(128, 24)
        Me.chkPorIntervaloFechas.TabIndex = 11
        Me.chkPorIntervaloFechas.Text = "Per Interval de dates"
        '
        'lblImpReciboAFecha
        '
        Me.lblImpReciboAFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblImpReciboAFecha.Location = New System.Drawing.Point(24, 136)
        Me.lblImpReciboAFecha.Name = "lblImpReciboAFecha"
        Me.lblImpReciboAFecha.Size = New System.Drawing.Size(64, 20)
        Me.lblImpReciboAFecha.TabIndex = 4
        Me.lblImpReciboAFecha.Text = "A Data"
        Me.lblImpReciboAFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblImpReciboDeFecha
        '
        Me.lblImpReciboDeFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblImpReciboDeFecha.Location = New System.Drawing.Point(24, 112)
        Me.lblImpReciboDeFecha.Name = "lblImpReciboDeFecha"
        Me.lblImpReciboDeFecha.Size = New System.Drawing.Size(64, 20)
        Me.lblImpReciboDeFecha.TabIndex = 3
        Me.lblImpReciboDeFecha.Text = "De Data"
        Me.lblImpReciboDeFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpAFechaRecibo
        '
        '
        '
        '
        Me.dtpAFechaRecibo.Calendar.DayNameLength = 1
        Me.dtpAFechaRecibo.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpAFechaRecibo.Enabled = False
        Me.dtpAFechaRecibo.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpAFechaRecibo.Location = New System.Drawing.Point(96, 136)
        Me.dtpAFechaRecibo.Name = "dtpAFechaRecibo"
        Me.dtpAFechaRecibo.Size = New System.Drawing.Size(120, 20)
        Me.dtpAFechaRecibo.TabIndex = 2
        Me.dtpAFechaRecibo.Tag = Nothing
        '
        'dtpDeFechaRecibo
        '
        '
        '
        '
        Me.dtpDeFechaRecibo.Calendar.DayNameLength = 1
        Me.dtpDeFechaRecibo.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpDeFechaRecibo.Enabled = False
        Me.dtpDeFechaRecibo.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpDeFechaRecibo.Location = New System.Drawing.Point(96, 112)
        Me.dtpDeFechaRecibo.Name = "dtpDeFechaRecibo"
        Me.dtpDeFechaRecibo.Size = New System.Drawing.Size(120, 20)
        Me.dtpDeFechaRecibo.TabIndex = 1
        Me.dtpDeFechaRecibo.Tag = Nothing
        '
        'cboAFacturaRecibo
        '
        Me.cboAFacturaRecibo.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboAFacturaRecibo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboAFacturaRecibo.AutoSelect = True
        Me.cboAFacturaRecibo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboAFacturaRecibo.Caption = ""
        Me.cboAFacturaRecibo.CaptionHeight = 17
        Me.cboAFacturaRecibo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboAFacturaRecibo.ColumnCaptionHeight = 17
        Me.cboAFacturaRecibo.ColumnFooterHeight = 17
        Me.cboAFacturaRecibo.ContentHeight = 15
        Me.cboAFacturaRecibo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAFacturaRecibo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboAFacturaRecibo.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboAFacturaRecibo.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboAFacturaRecibo.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAFacturaRecibo.EditorHeight = 15
        Me.cboAFacturaRecibo.Images.Add(CType(resources.GetObject("cboAFacturaRecibo.Images"), System.Drawing.Image))
        Me.cboAFacturaRecibo.IntegralHeight = True
        Me.cboAFacturaRecibo.ItemHeight = 13
        Me.cboAFacturaRecibo.Location = New System.Drawing.Point(96, 56)
        Me.cboAFacturaRecibo.MatchEntryTimeout = CType(100, Long)
        Me.cboAFacturaRecibo.MaxDropDownItems = CType(8, Short)
        Me.cboAFacturaRecibo.MaxLength = 0
        Me.cboAFacturaRecibo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboAFacturaRecibo.Name = "cboAFacturaRecibo"
        Me.cboAFacturaRecibo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboAFacturaRecibo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboAFacturaRecibo.Size = New System.Drawing.Size(120, 19)
        Me.cboAFacturaRecibo.TabIndex = 10
        Me.cboAFacturaRecibo.PropBag = resources.GetString("cboAFacturaRecibo.PropBag")
        '
        'cboDeFacuraRecibo
        '
        Me.cboDeFacuraRecibo.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboDeFacuraRecibo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboDeFacuraRecibo.AutoSelect = True
        Me.cboDeFacuraRecibo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboDeFacuraRecibo.Caption = ""
        Me.cboDeFacuraRecibo.CaptionHeight = 17
        Me.cboDeFacuraRecibo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboDeFacuraRecibo.ColumnCaptionHeight = 17
        Me.cboDeFacuraRecibo.ColumnFooterHeight = 17
        Me.cboDeFacuraRecibo.ContentHeight = 15
        Me.cboDeFacuraRecibo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDeFacuraRecibo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboDeFacuraRecibo.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboDeFacuraRecibo.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboDeFacuraRecibo.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDeFacuraRecibo.EditorHeight = 15
        Me.cboDeFacuraRecibo.Images.Add(CType(resources.GetObject("cboDeFacuraRecibo.Images"), System.Drawing.Image))
        Me.cboDeFacuraRecibo.IntegralHeight = True
        Me.cboDeFacuraRecibo.ItemHeight = 13
        Me.cboDeFacuraRecibo.Location = New System.Drawing.Point(96, 24)
        Me.cboDeFacuraRecibo.MatchEntryTimeout = CType(100, Long)
        Me.cboDeFacuraRecibo.MaxDropDownItems = CType(8, Short)
        Me.cboDeFacuraRecibo.MaxLength = 0
        Me.cboDeFacuraRecibo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboDeFacuraRecibo.Name = "cboDeFacuraRecibo"
        Me.cboDeFacuraRecibo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboDeFacuraRecibo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboDeFacuraRecibo.Size = New System.Drawing.Size(120, 19)
        Me.cboDeFacuraRecibo.TabIndex = 9
        Me.cboDeFacuraRecibo.PropBag = resources.GetString("cboDeFacuraRecibo.PropBag")
        '
        'lblAFactura
        '
        Me.lblAFactura.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAFactura.Location = New System.Drawing.Point(16, 56)
        Me.lblAFactura.Name = "lblAFactura"
        Me.lblAFactura.Size = New System.Drawing.Size(72, 20)
        Me.lblAFactura.TabIndex = 8
        Me.lblAFactura.Text = "A Factura"
        Me.lblAFactura.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDeFactura
        '
        Me.lblDeFactura.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDeFactura.Location = New System.Drawing.Point(16, 24)
        Me.lblDeFactura.Name = "lblDeFactura"
        Me.lblDeFactura.Size = New System.Drawing.Size(72, 20)
        Me.lblDeFactura.TabIndex = 7
        Me.lblDeFactura.Text = "De Factura"
        Me.lblDeFactura.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'XpGroupBox2
        '
        Me.XpGroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.XpGroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.XpGroupBox2.Location = New System.Drawing.Point(16, 272)
        Me.XpGroupBox2.Name = "XpGroupBox2"
        Me.XpGroupBox2.Size = New System.Drawing.Size(268, 100)
        Me.XpGroupBox2.TabIndex = 244
        Me.XpGroupBox2.TabStop = False
        Me.XpGroupBox2.Visible = False
        '
        'btnGenerarRecibo
        '
        Me.btnGenerarRecibo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnGenerarRecibo.Enabled = False
        Me.btnGenerarRecibo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerarRecibo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerarRecibo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnGenerarRecibo.Location = New System.Drawing.Point(182, 232)
        Me.btnGenerarRecibo.Name = "btnGenerarRecibo"
        Me.btnGenerarRecibo.Size = New System.Drawing.Size(96, 24)
        Me.btnGenerarRecibo.TabIndex = 245
        Me.btnGenerarRecibo.TabStop = False
        Me.btnGenerarRecibo.Text = "Generar Rebuts"
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.GroupBox7)
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(972, 478)
        Me.TabPage1.TabIndex = 1
        Me.TabPage1.Text = "Venciments"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(217, Byte), Integer))
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
        Me.GroupBox3.Location = New System.Drawing.Point(32, 124)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(488, 144)
        Me.GroupBox3.TabIndex = 387
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Dades Facturació Client"
        '
        'lblFormaDePago
        '
        Me.lblFormaDePago.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblFormaDePago.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFormaDePago.Location = New System.Drawing.Point(8, 48)
        Me.lblFormaDePago.Name = "lblFormaDePago"
        Me.lblFormaDePago.Size = New System.Drawing.Size(116, 16)
        Me.lblFormaDePago.TabIndex = 287
        Me.lblFormaDePago.Text = "Forma de pagament"
        Me.lblFormaDePago.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFORMA
        '
        Me.lblFORMA.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblFORMA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFORMA.Location = New System.Drawing.Point(132, 48)
        Me.lblFORMA.Name = "lblFORMA"
        Me.lblFORMA.Size = New System.Drawing.Size(340, 16)
        Me.lblFORMA.TabIndex = 286
        Me.lblFORMA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDia3
        '
        Me.lblDia3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblDia3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDia3.Location = New System.Drawing.Point(428, 24)
        Me.lblDia3.Name = "lblDia3"
        Me.lblDia3.Size = New System.Drawing.Size(44, 16)
        Me.lblDia3.TabIndex = 285
        Me.lblDia3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDia_3
        '
        Me.lblDia_3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblDia_3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDia_3.Location = New System.Drawing.Point(388, 24)
        Me.lblDia_3.Name = "lblDia_3"
        Me.lblDia_3.Size = New System.Drawing.Size(36, 16)
        Me.lblDia_3.TabIndex = 282
        Me.lblDia_3.Text = "Dia 3"
        Me.lblDia_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDia1
        '
        Me.lblDia1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblDia1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDia1.Location = New System.Drawing.Point(252, 24)
        Me.lblDia1.Name = "lblDia1"
        Me.lblDia1.Size = New System.Drawing.Size(44, 16)
        Me.lblDia1.TabIndex = 283
        Me.lblDia1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDia_2
        '
        Me.lblDia_2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblDia_2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDia_2.Location = New System.Drawing.Point(300, 24)
        Me.lblDia_2.Name = "lblDia_2"
        Me.lblDia_2.Size = New System.Drawing.Size(36, 16)
        Me.lblDia_2.TabIndex = 281
        Me.lblDia_2.Text = "Dia 2"
        Me.lblDia_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRE2
        '
        Me.lblRE2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblRE2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRE2.Location = New System.Drawing.Point(140, 24)
        Me.lblRE2.Name = "lblRE2"
        Me.lblRE2.Size = New System.Drawing.Size(44, 16)
        Me.lblRE2.TabIndex = 279
        Me.lblRE2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRE_
        '
        Me.lblRE_.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblRE_.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRE_.Location = New System.Drawing.Point(104, 24)
        Me.lblRE_.Name = "lblRE_"
        Me.lblRE_.Size = New System.Drawing.Size(32, 16)
        Me.lblRE_.TabIndex = 278
        Me.lblRE_.Text = "RE"
        Me.lblRE_.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIVA2
        '
        Me.lblIVA2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblIVA2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblIVA2.Location = New System.Drawing.Point(44, 24)
        Me.lblIVA2.Name = "lblIVA2"
        Me.lblIVA2.Size = New System.Drawing.Size(56, 16)
        Me.lblIVA2.TabIndex = 277
        Me.lblIVA2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIVA_
        '
        Me.lblIVA_.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblIVA_.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblIVA_.Location = New System.Drawing.Point(8, 24)
        Me.lblIVA_.Name = "lblIVA_"
        Me.lblIVA_.Size = New System.Drawing.Size(32, 16)
        Me.lblIVA_.TabIndex = 276
        Me.lblIVA_.Text = "IVA"
        Me.lblIVA_.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDia_1
        '
        Me.lblDia_1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblDia_1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDia_1.Location = New System.Drawing.Point(212, 24)
        Me.lblDia_1.Name = "lblDia_1"
        Me.lblDia_1.Size = New System.Drawing.Size(36, 16)
        Me.lblDia_1.TabIndex = 280
        Me.lblDia_1.Text = "Dia 1"
        Me.lblDia_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDia2
        '
        Me.lblDia2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblDia2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDia2.Location = New System.Drawing.Point(340, 24)
        Me.lblDia2.Name = "lblDia2"
        Me.lblDia2.Size = New System.Drawing.Size(44, 16)
        Me.lblDia2.TabIndex = 284
        Me.lblDia2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.dt2DATA)
        Me.GroupBox7.Controls.Add(Me.Label5)
        Me.GroupBox7.Controls.Add(Me.Label6)
        Me.GroupBox7.Controls.Add(Me.tx2NOMCLIENT)
        Me.GroupBox7.Controls.Add(Me.lblFacturaV)
        Me.GroupBox7.Controls.Add(Me.tx2FRA)
        Me.GroupBox7.Controls.Add(Me.tx2CLIENT)
        Me.GroupBox7.Location = New System.Drawing.Point(32, 24)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(488, 93)
        Me.GroupBox7.TabIndex = 188
        Me.GroupBox7.TabStop = False
        '
        'dt2DATA
        '
        '
        '
        '
        Me.dt2DATA.Calendar.DayNameLength = 1
        Me.dt2DATA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dt2DATA.DisableOnNoData = False
        Me.dt2DATA.EmptyAsNull = True
        Me.dt2DATA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dt2DATA.Location = New System.Drawing.Point(96, 66)
        Me.dt2DATA.Name = "dt2DATA"
        Me.dt2DATA.Size = New System.Drawing.Size(108, 20)
        Me.dt2DATA.TabIndex = 380
        Me.dt2DATA.Tag = Nothing
        '
        'Label5
        '
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(12, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 20)
        Me.Label5.TabIndex = 178
        Me.Label5.Text = "Client"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(12, 66)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 20)
        Me.Label6.TabIndex = 181
        Me.Label6.Text = "Data"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx2NOMCLIENT
        '
        Me.tx2NOMCLIENT.Location = New System.Drawing.Point(152, 42)
        Me.tx2NOMCLIENT.Name = "tx2NOMCLIENT"
        Me.tx2NOMCLIENT.Size = New System.Drawing.Size(318, 20)
        Me.tx2NOMCLIENT.TabIndex = 182
        Me.tx2NOMCLIENT.Tag = Nothing
        '
        'lblFacturaV
        '
        Me.lblFacturaV.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFacturaV.Location = New System.Drawing.Point(12, 18)
        Me.lblFacturaV.Name = "lblFacturaV"
        Me.lblFacturaV.Size = New System.Drawing.Size(80, 20)
        Me.lblFacturaV.TabIndex = 180
        Me.lblFacturaV.Text = "Factura"
        Me.lblFacturaV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx2FRA
        '
        Me.tx2FRA.Location = New System.Drawing.Point(98, 18)
        Me.tx2FRA.Name = "tx2FRA"
        Me.tx2FRA.Size = New System.Drawing.Size(100, 20)
        Me.tx2FRA.TabIndex = 179
        Me.tx2FRA.Tag = Nothing
        '
        'tx2CLIENT
        '
        Me.tx2CLIENT.Location = New System.Drawing.Point(96, 40)
        Me.tx2CLIENT.Name = "tx2CLIENT"
        Me.tx2CLIENT.Size = New System.Drawing.Size(52, 20)
        Me.tx2CLIENT.TabIndex = 288
        Me.tx2CLIENT.Tag = Nothing
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnGenerarVencim)
        Me.GroupBox4.Controls.Add(Me.dgVencimientos)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.tx2TOTAL)
        Me.GroupBox4.Location = New System.Drawing.Point(548, 24)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(396, 286)
        Me.GroupBox4.TabIndex = 187
        Me.GroupBox4.TabStop = False
        '
        'btnGenerarVencim
        '
        Me.btnGenerarVencim.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerarVencim.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerarVencim.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnGenerarVencim.Location = New System.Drawing.Point(264, 256)
        Me.btnGenerarVencim.Name = "btnGenerarVencim"
        Me.btnGenerarVencim.Size = New System.Drawing.Size(104, 23)
        Me.btnGenerarVencim.TabIndex = 5
        Me.btnGenerarVencim.Text = "Generar Venciments"
        '
        'dgVencimientos
        '
        Me.dgVencimientos.AllowAddNew = True
        Me.dgVencimientos.AllowDelete = True
        Me.dgVencimientos.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgVencimientos.Images.Add(CType(resources.GetObject("dgVencimientos.Images"), System.Drawing.Image))
        Me.dgVencimientos.Location = New System.Drawing.Point(16, 56)
        Me.dgVencimientos.Name = "dgVencimientos"
        Me.dgVencimientos.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgVencimientos.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgVencimientos.PreviewInfo.ZoomFactor = 75.0R
        Me.dgVencimientos.PrintInfo.PageSettings = CType(resources.GetObject("dgVencimientos.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgVencimientos.Size = New System.Drawing.Size(352, 192)
        Me.dgVencimientos.TabIndex = 4
        Me.dgVencimientos.Text = "C1TrueDBGrid1"
        Me.dgVencimientos.PropBag = resources.GetString("dgVencimientos.PropBag")
        '
        'Label4
        '
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(168, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 18)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Import Total"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tx2TOTAL
        '
        Me.tx2TOTAL.Location = New System.Drawing.Point(272, 24)
        Me.tx2TOTAL.Name = "tx2TOTAL"
        Me.tx2TOTAL.Size = New System.Drawing.Size(96, 20)
        Me.tx2TOTAL.TabIndex = 2
        Me.tx2TOTAL.Tag = Nothing
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.TabPage3.Controls.Add(Me.gbImpresion)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(972, 478)
        Me.TabPage3.TabIndex = 3
        Me.TabPage3.Text = "Impresió"
        '
        'gbImpresion
        '
        Me.gbImpresion.Controls.Add(Me.Label3)
        Me.gbImpresion.Controls.Add(Me.Label1)
        Me.gbImpresion.Controls.Add(Me.cboAPAFIMPRESION)
        Me.gbImpresion.Controls.Add(Me.cboDEPAFIMPRESION)
        Me.gbImpresion.Controls.Add(Me.btnImprimir)
        Me.gbImpresion.Location = New System.Drawing.Point(16, 16)
        Me.gbImpresion.Name = "gbImpresion"
        Me.gbImpresion.Size = New System.Drawing.Size(248, 156)
        Me.gbImpresion.TabIndex = 16
        Me.gbImpresion.TabStop = False
        '
        'Label3
        '
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(20, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 20)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "A Document"
        '
        'Label1
        '
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(20, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 20)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "De Document"
        '
        'cboAPAFIMPRESION
        '
        Me.cboAPAFIMPRESION.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboAPAFIMPRESION.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboAPAFIMPRESION.AutoSelect = True
        Me.cboAPAFIMPRESION.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboAPAFIMPRESION.Caption = ""
        Me.cboAPAFIMPRESION.CaptionHeight = 17
        Me.cboAPAFIMPRESION.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboAPAFIMPRESION.ColumnCaptionHeight = 17
        Me.cboAPAFIMPRESION.ColumnFooterHeight = 17
        Me.cboAPAFIMPRESION.ContentHeight = 15
        Me.cboAPAFIMPRESION.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAPAFIMPRESION.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboAPAFIMPRESION.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboAPAFIMPRESION.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboAPAFIMPRESION.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAPAFIMPRESION.EditorHeight = 15
        Me.cboAPAFIMPRESION.Images.Add(CType(resources.GetObject("cboAPAFIMPRESION.Images"), System.Drawing.Image))
        Me.cboAPAFIMPRESION.IntegralHeight = True
        Me.cboAPAFIMPRESION.ItemHeight = 13
        Me.cboAPAFIMPRESION.Location = New System.Drawing.Point(112, 60)
        Me.cboAPAFIMPRESION.MatchEntryTimeout = CType(100, Long)
        Me.cboAPAFIMPRESION.MaxDropDownItems = CType(8, Short)
        Me.cboAPAFIMPRESION.MaxLength = 0
        Me.cboAPAFIMPRESION.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboAPAFIMPRESION.Name = "cboAPAFIMPRESION"
        Me.cboAPAFIMPRESION.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboAPAFIMPRESION.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboAPAFIMPRESION.Size = New System.Drawing.Size(112, 19)
        Me.cboAPAFIMPRESION.TabIndex = 18
        Me.cboAPAFIMPRESION.PropBag = resources.GetString("cboAPAFIMPRESION.PropBag")
        '
        'cboDEPAFIMPRESION
        '
        Me.cboDEPAFIMPRESION.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboDEPAFIMPRESION.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboDEPAFIMPRESION.AutoSelect = True
        Me.cboDEPAFIMPRESION.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboDEPAFIMPRESION.Caption = ""
        Me.cboDEPAFIMPRESION.CaptionHeight = 17
        Me.cboDEPAFIMPRESION.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboDEPAFIMPRESION.ColumnCaptionHeight = 17
        Me.cboDEPAFIMPRESION.ColumnFooterHeight = 17
        Me.cboDEPAFIMPRESION.ContentHeight = 15
        Me.cboDEPAFIMPRESION.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDEPAFIMPRESION.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboDEPAFIMPRESION.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboDEPAFIMPRESION.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboDEPAFIMPRESION.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDEPAFIMPRESION.EditorHeight = 15
        Me.cboDEPAFIMPRESION.Images.Add(CType(resources.GetObject("cboDEPAFIMPRESION.Images"), System.Drawing.Image))
        Me.cboDEPAFIMPRESION.IntegralHeight = True
        Me.cboDEPAFIMPRESION.ItemHeight = 13
        Me.cboDEPAFIMPRESION.Location = New System.Drawing.Point(112, 28)
        Me.cboDEPAFIMPRESION.MatchEntryTimeout = CType(100, Long)
        Me.cboDEPAFIMPRESION.MaxDropDownItems = CType(8, Short)
        Me.cboDEPAFIMPRESION.MaxLength = 0
        Me.cboDEPAFIMPRESION.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboDEPAFIMPRESION.Name = "cboDEPAFIMPRESION"
        Me.cboDEPAFIMPRESION.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboDEPAFIMPRESION.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboDEPAFIMPRESION.Size = New System.Drawing.Size(112, 19)
        Me.cboDEPAFIMPRESION.TabIndex = 17
        Me.cboDEPAFIMPRESION.PropBag = resources.GetString("cboDEPAFIMPRESION.PropBag")
        '
        'btnImprimir
        '
        Me.btnImprimir.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimir.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnImprimir.Location = New System.Drawing.Point(144, 96)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(80, 22)
        Me.btnImprimir.TabIndex = 18
        Me.btnImprimir.Text = "Imprimir"
        '
        'gbEleccionClientesPAF
        '
        Me.gbEleccionClientesPAF.BackColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.gbEleccionClientesPAF.Controls.Add(Me.lblNumeroPAF)
        Me.gbEleccionClientesPAF.Controls.Add(Me.cboSeleccionClienteParaPAF)
        Me.gbEleccionClientesPAF.Controls.Add(Me.rdoVerTodasPAF)
        Me.gbEleccionClientesPAF.Controls.Add(Me.rdoVerPAFDeCliente)
        Me.gbEleccionClientesPAF.Location = New System.Drawing.Point(8, 8)
        Me.gbEleccionClientesPAF.Name = "gbEleccionClientesPAF"
        Me.gbEleccionClientesPAF.Size = New System.Drawing.Size(704, 48)
        Me.gbEleccionClientesPAF.TabIndex = 224
        Me.gbEleccionClientesPAF.TabStop = False
        Me.gbEleccionClientesPAF.Text = "El·leccions"
        '
        'lblNumeroPAF
        '
        Me.lblNumeroPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNumeroPAF.Location = New System.Drawing.Point(556, 20)
        Me.lblNumeroPAF.Name = "lblNumeroPAF"
        Me.lblNumeroPAF.Size = New System.Drawing.Size(144, 23)
        Me.lblNumeroPAF.TabIndex = 3
        '
        'cboSeleccionClienteParaPAF
        '
        Me.cboSeleccionClienteParaPAF.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboSeleccionClienteParaPAF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboSeleccionClienteParaPAF.Caption = ""
        Me.cboSeleccionClienteParaPAF.CaptionHeight = 17
        Me.cboSeleccionClienteParaPAF.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboSeleccionClienteParaPAF.ColumnCaptionHeight = 17
        Me.cboSeleccionClienteParaPAF.ColumnFooterHeight = 17
        Me.cboSeleccionClienteParaPAF.ContentHeight = 15
        Me.cboSeleccionClienteParaPAF.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSeleccionClienteParaPAF.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboSeleccionClienteParaPAF.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboSeleccionClienteParaPAF.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboSeleccionClienteParaPAF.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboSeleccionClienteParaPAF.EditorHeight = 15
        Me.cboSeleccionClienteParaPAF.Images.Add(CType(resources.GetObject("cboSeleccionClienteParaPAF.Images"), System.Drawing.Image))
        Me.cboSeleccionClienteParaPAF.IntegralHeight = True
        Me.cboSeleccionClienteParaPAF.ItemHeight = 13
        Me.cboSeleccionClienteParaPAF.LimitToList = True
        Me.cboSeleccionClienteParaPAF.Location = New System.Drawing.Point(232, 22)
        Me.cboSeleccionClienteParaPAF.MatchEntryTimeout = CType(100, Long)
        Me.cboSeleccionClienteParaPAF.MaxDropDownItems = CType(8, Short)
        Me.cboSeleccionClienteParaPAF.MaxLength = 255
        Me.cboSeleccionClienteParaPAF.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboSeleccionClienteParaPAF.Name = "cboSeleccionClienteParaPAF"
        Me.cboSeleccionClienteParaPAF.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboSeleccionClienteParaPAF.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboSeleccionClienteParaPAF.Size = New System.Drawing.Size(316, 19)
        Me.cboSeleccionClienteParaPAF.TabIndex = 2
        Me.cboSeleccionClienteParaPAF.Visible = False
        Me.cboSeleccionClienteParaPAF.PropBag = resources.GetString("cboSeleccionClienteParaPAF.PropBag")
        '
        'rdoVerTodasPAF
        '
        Me.rdoVerTodasPAF.Checked = True
        Me.rdoVerTodasPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoVerTodasPAF.Location = New System.Drawing.Point(4, 22)
        Me.rdoVerTodasPAF.Name = "rdoVerTodasPAF"
        Me.rdoVerTodasPAF.Size = New System.Drawing.Size(132, 21)
        Me.rdoVerTodasPAF.TabIndex = 0
        Me.rdoVerTodasPAF.TabStop = True
        Me.rdoVerTodasPAF.Text = "Tots els clients"
        '
        'rdoVerPAFDeCliente
        '
        Me.rdoVerPAFDeCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoVerPAFDeCliente.Location = New System.Drawing.Point(140, 22)
        Me.rdoVerPAFDeCliente.Name = "rdoVerPAFDeCliente"
        Me.rdoVerPAFDeCliente.Size = New System.Drawing.Size(92, 21)
        Me.rdoVerPAFDeCliente.TabIndex = 1
        Me.rdoVerPAFDeCliente.Text = "Del Client"
        '
        'gbCentro
        '
        Me.gbCentro.BackColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.gbCentro.Controls.Add(Me.lblNumeracion)
        Me.gbCentro.Location = New System.Drawing.Point(720, 8)
        Me.gbCentro.Name = "gbCentro"
        Me.gbCentro.Size = New System.Drawing.Size(264, 48)
        Me.gbCentro.TabIndex = 225
        Me.gbCentro.TabStop = False
        '
        'lblNumeracion
        '
        Me.lblNumeracion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNumeracion.Location = New System.Drawing.Point(8, 24)
        Me.lblNumeracion.Name = "lblNumeracion"
        Me.lblNumeracion.Size = New System.Drawing.Size(88, 14)
        Me.lblNumeracion.TabIndex = 220
        Me.lblNumeracion.Text = "Numeració de:"
        Me.lblNumeracion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ppfitxa2
        '
        Me.ppfitxa2.Location = New System.Drawing.Point(305, 17)
        Me.ppfitxa2.Name = "ppfitxa2"
        '
        'ppfitxa2.OutlineView
        '
        Me.ppfitxa2.PreviewOutlineView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ppfitxa2.PreviewOutlineView.Location = New System.Drawing.Point(0, 0)
        Me.ppfitxa2.PreviewOutlineView.Name = "OutlineView"
        Me.ppfitxa2.PreviewOutlineView.Size = New System.Drawing.Size(165, 427)
        Me.ppfitxa2.PreviewOutlineView.TabIndex = 0
        '
        'ppfitxa2.PreviewPane
        '
        Me.ppfitxa2.PreviewPane.IntegrateExternalTools = True
        Me.ppfitxa2.PreviewPane.TabIndex = 0
        '
        'ppfitxa2.PreviewTextSearchPanel
        '
        Me.ppfitxa2.PreviewTextSearchPanel.Dock = System.Windows.Forms.DockStyle.Right
        Me.ppfitxa2.PreviewTextSearchPanel.Location = New System.Drawing.Point(530, 0)
        Me.ppfitxa2.PreviewTextSearchPanel.MinimumSize = New System.Drawing.Size(200, 240)
        Me.ppfitxa2.PreviewTextSearchPanel.Name = "PreviewTextSearchPanel"
        Me.ppfitxa2.PreviewTextSearchPanel.Size = New System.Drawing.Size(200, 453)
        Me.ppfitxa2.PreviewTextSearchPanel.TabIndex = 0
        Me.ppfitxa2.PreviewTextSearchPanel.Visible = False
        '
        'ppfitxa2.ThumbnailView
        '
        Me.ppfitxa2.PreviewThumbnailView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ppfitxa2.PreviewThumbnailView.Location = New System.Drawing.Point(0, 0)
        Me.ppfitxa2.PreviewThumbnailView.Name = "ThumbnailView"
        Me.ppfitxa2.PreviewThumbnailView.Size = New System.Drawing.Size(165, 366)
        Me.ppfitxa2.PreviewThumbnailView.TabIndex = 0
        Me.ppfitxa2.PreviewThumbnailView.UseImageAsThumbnail = False
        Me.ppfitxa2.Size = New System.Drawing.Size(632, 439)
        Me.ppfitxa2.TabIndex = 246
        Me.ppfitxa2.Text = "C1PrintPreviewControl1"
        '
        '
        '
        Me.ppfitxa2.ToolBars.File.Open.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.File.Open.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.File.Open.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.File.Open.Name = "btnFileOpen"
        Me.ppfitxa2.ToolBars.File.Open.Size = New System.Drawing.Size(32, 22)
        Me.ppfitxa2.ToolBars.File.Open.Tag = "C1PreviewActionEnum.FileOpen"
        Me.ppfitxa2.ToolBars.File.Open.ToolTipText = "Open File"
        '
        '
        '
        Me.ppfitxa2.ToolBars.File.PageSetup.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.File.PageSetup.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.File.PageSetup.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.File.PageSetup.Name = "btnPageSetup"
        Me.ppfitxa2.ToolBars.File.PageSetup.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.File.PageSetup.Tag = "C1PreviewActionEnum.PageSetup"
        Me.ppfitxa2.ToolBars.File.PageSetup.ToolTipText = "Page Setup"
        '
        '
        '
        Me.ppfitxa2.ToolBars.File.Print.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.File.Print.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.File.Print.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.File.Print.Name = "btnPrint"
        Me.ppfitxa2.ToolBars.File.Print.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.File.Print.Tag = "C1PreviewActionEnum.Print"
        Me.ppfitxa2.ToolBars.File.Print.ToolTipText = "Print"
        '
        '
        '
        Me.ppfitxa2.ToolBars.File.Reflow.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.File.Reflow.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.File.Reflow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.File.Reflow.Name = "btnReflow"
        Me.ppfitxa2.ToolBars.File.Reflow.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.File.Reflow.Tag = "C1PreviewActionEnum.Reflow"
        Me.ppfitxa2.ToolBars.File.Reflow.ToolTipText = "Reflow"
        '
        '
        '
        Me.ppfitxa2.ToolBars.File.Save.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.File.Save.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.File.Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.File.Save.Name = "btnFileSave"
        Me.ppfitxa2.ToolBars.File.Save.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.File.Save.Tag = "C1PreviewActionEnum.FileSave"
        Me.ppfitxa2.ToolBars.File.Save.ToolTipText = "Save File"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.GoFirst.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Navigation.GoFirst.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Navigation.GoFirst.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Navigation.GoFirst.Name = "btnGoFirst"
        Me.ppfitxa2.ToolBars.Navigation.GoFirst.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Navigation.GoFirst.Tag = "C1PreviewActionEnum.GoFirst"
        Me.ppfitxa2.ToolBars.Navigation.GoFirst.ToolTipText = "Go To First Page"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.GoLast.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Navigation.GoLast.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Navigation.GoLast.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Navigation.GoLast.Name = "btnGoLast"
        Me.ppfitxa2.ToolBars.Navigation.GoLast.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Navigation.GoLast.Tag = "C1PreviewActionEnum.GoLast"
        Me.ppfitxa2.ToolBars.Navigation.GoLast.ToolTipText = "Go To Last Page"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.GoNext.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Navigation.GoNext.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Navigation.GoNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Navigation.GoNext.Name = "btnGoNext"
        Me.ppfitxa2.ToolBars.Navigation.GoNext.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Navigation.GoNext.Tag = "C1PreviewActionEnum.GoNext"
        Me.ppfitxa2.ToolBars.Navigation.GoNext.ToolTipText = "Go To Next Page"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.GoPrev.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Navigation.GoPrev.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Navigation.GoPrev.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Navigation.GoPrev.Name = "btnGoPrev"
        Me.ppfitxa2.ToolBars.Navigation.GoPrev.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Navigation.GoPrev.Tag = "C1PreviewActionEnum.GoPrev"
        Me.ppfitxa2.ToolBars.Navigation.GoPrev.ToolTipText = "Go To Previous Page"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.HistoryNext.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Navigation.HistoryNext.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Navigation.HistoryNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Navigation.HistoryNext.Name = "btnHistoryNext"
        Me.ppfitxa2.ToolBars.Navigation.HistoryNext.Size = New System.Drawing.Size(32, 22)
        Me.ppfitxa2.ToolBars.Navigation.HistoryNext.Tag = "C1PreviewActionEnum.HistoryNext"
        Me.ppfitxa2.ToolBars.Navigation.HistoryNext.ToolTipText = "Next View"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.HistoryPrev.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Navigation.HistoryPrev.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Navigation.HistoryPrev.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Navigation.HistoryPrev.Name = "btnHistoryPrev"
        Me.ppfitxa2.ToolBars.Navigation.HistoryPrev.Size = New System.Drawing.Size(32, 22)
        Me.ppfitxa2.ToolBars.Navigation.HistoryPrev.Tag = "C1PreviewActionEnum.HistoryPrev"
        Me.ppfitxa2.ToolBars.Navigation.HistoryPrev.ToolTipText = "Previous View"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.LblOfPages.Name = "lblOfPages"
        Me.ppfitxa2.ToolBars.Navigation.LblOfPages.Size = New System.Drawing.Size(27, 22)
        Me.ppfitxa2.ToolBars.Navigation.LblOfPages.Tag = "C1PreviewActionEnum.GoPageCount"
        Me.ppfitxa2.ToolBars.Navigation.LblOfPages.Text = "of 0"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Navigation.LblPage.Name = "lblPage"
        Me.ppfitxa2.ToolBars.Navigation.LblPage.Size = New System.Drawing.Size(33, 22)
        Me.ppfitxa2.ToolBars.Navigation.LblPage.Tag = "C1PreviewActionEnum.GoPageLabel"
        Me.ppfitxa2.ToolBars.Navigation.LblPage.Text = "Page"
        Me.ppfitxa2.ToolBars.Navigation.ToolTipPageNo = Nothing
        '
        '
        '
        Me.ppfitxa2.ToolBars.Page.Continuous.Checked = True
        Me.ppfitxa2.ToolBars.Page.Continuous.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ppfitxa2.ToolBars.Page.Continuous.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Page.Continuous.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Page.Continuous.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Page.Continuous.Name = "btnPageContinuous"
        Me.ppfitxa2.ToolBars.Page.Continuous.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Page.Continuous.Tag = "C1PreviewActionEnum.PageContinuous"
        Me.ppfitxa2.ToolBars.Page.Continuous.ToolTipText = "Continuous View"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Page.Facing.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Page.Facing.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Page.Facing.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Page.Facing.Name = "btnPageFacing"
        Me.ppfitxa2.ToolBars.Page.Facing.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Page.Facing.Tag = "C1PreviewActionEnum.PageFacing"
        Me.ppfitxa2.ToolBars.Page.Facing.ToolTipText = "Pages Facing View"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Page.FacingContinuous.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Page.FacingContinuous.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Page.FacingContinuous.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Page.FacingContinuous.Name = "btnPageFacingContinuous"
        Me.ppfitxa2.ToolBars.Page.FacingContinuous.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Page.FacingContinuous.Tag = "C1PreviewActionEnum.PageFacingContinuous"
        Me.ppfitxa2.ToolBars.Page.FacingContinuous.ToolTipText = "Pages Facing Continuous View"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Page.Single.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Page.Single.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Page.Single.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Page.Single.Name = "btnPageSingle"
        Me.ppfitxa2.ToolBars.Page.Single.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Page.Single.Tag = "C1PreviewActionEnum.PageSingle"
        Me.ppfitxa2.ToolBars.Page.Single.ToolTipText = "Single Page View"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Text.Find.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Text.Find.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Text.Find.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Text.Find.Name = "btnFind"
        Me.ppfitxa2.ToolBars.Text.Find.Size = New System.Drawing.Size(23, 20)
        Me.ppfitxa2.ToolBars.Text.Find.Tag = "C1PreviewActionEnum.Find"
        Me.ppfitxa2.ToolBars.Text.Find.ToolTipText = "Find Text"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Text.Hand.Checked = True
        Me.ppfitxa2.ToolBars.Text.Hand.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ppfitxa2.ToolBars.Text.Hand.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Text.Hand.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Text.Hand.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Text.Hand.Name = "btnHandTool"
        Me.ppfitxa2.ToolBars.Text.Hand.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Text.Hand.Tag = "C1PreviewActionEnum.HandTool"
        Me.ppfitxa2.ToolBars.Text.Hand.ToolTipText = "Hand Tool"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Text.SelectText.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Text.SelectText.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Text.SelectText.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Text.SelectText.Name = "btnSelectTextTool"
        Me.ppfitxa2.ToolBars.Text.SelectText.Size = New System.Drawing.Size(23, 20)
        Me.ppfitxa2.ToolBars.Text.SelectText.Tag = "C1PreviewActionEnum.SelectTextTool"
        Me.ppfitxa2.ToolBars.Text.SelectText.ToolTipText = "Text Select Tool"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Zoom.DropZoomFactor.Name = "dropZoomFactor"
        Me.ppfitxa2.ToolBars.Zoom.DropZoomFactor.Size = New System.Drawing.Size(13, 22)
        Me.ppfitxa2.ToolBars.Zoom.DropZoomFactor.Tag = "C1PreviewActionEnum.ZoomFactor"
        Me.ppfitxa2.ToolBars.Zoom.ToolTipZoomFactor = Nothing
        '
        '
        '
        Me.ppfitxa2.ToolBars.Zoom.ZoomIn.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Zoom.ZoomIn.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Zoom.ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Zoom.ZoomIn.Name = "btnZoomIn"
        Me.ppfitxa2.ToolBars.Zoom.ZoomIn.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Zoom.ZoomIn.Tag = "C1PreviewActionEnum.ZoomIn"
        Me.ppfitxa2.ToolBars.Zoom.ZoomIn.ToolTipText = "Zoom In"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Zoom.ZoomOut.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Zoom.ZoomOut.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Zoom.ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Zoom.ZoomOut.Name = "btnZoomOut"
        Me.ppfitxa2.ToolBars.Zoom.ZoomOut.Size = New System.Drawing.Size(23, 22)
        Me.ppfitxa2.ToolBars.Zoom.ZoomOut.Tag = "C1PreviewActionEnum.ZoomOut"
        Me.ppfitxa2.ToolBars.Zoom.ZoomOut.ToolTipText = "Zoom Out"
        '
        '
        '
        Me.ppfitxa2.ToolBars.Zoom.ZoomTool.Image = CType(resources.GetObject("C1PrintPreviewControl1.ToolBars.Zoom.ZoomTool.Image"), System.Drawing.Image)
        Me.ppfitxa2.ToolBars.Zoom.ZoomTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ppfitxa2.ToolBars.Zoom.ZoomTool.Name = "btnZoomTool"
        Me.ppfitxa2.ToolBars.Zoom.ZoomTool.Size = New System.Drawing.Size(32, 22)
        Me.ppfitxa2.ToolBars.Zoom.ZoomTool.Tag = "C1PreviewActionEnum.ZoomInTool"
        Me.ppfitxa2.ToolBars.Zoom.ZoomTool.ToolTipText = "Zoom In Tool"
        '
        'frmFacturasModelos
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(992, 618)
        Me.Controls.Add(Me.gbEleccionClientesPAF)
        Me.Controls.Add(Me.gbCentro)
        Me.Controls.Add(Me.TabControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "frmFacturasModelos"
        Me.Text = "Factures Models"
        Me.Controls.SetChildIndex(Me.btnSiguiente, 0)
        Me.Controls.SetChildIndex(Me.btnVerLista, 0)
        Me.Controls.SetChildIndex(Me.btnModificar, 0)
        Me.Controls.SetChildIndex(Me.btnAnterior, 0)
        Me.Controls.SetChildIndex(Me.btnPrimero, 0)
        Me.Controls.SetChildIndex(Me.btnUltimo, 0)
        Me.Controls.SetChildIndex(Me.btnNuevo, 0)
        Me.Controls.SetChildIndex(Me.btnTancar, 0)
        Me.Controls.SetChildIndex(Me.btnBorrar, 0)
        Me.Controls.SetChildIndex(Me.btnActualizar, 0)
        Me.Controls.SetChildIndex(Me.btnRecargar, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.gbCentro, 0)
        Me.Controls.SetChildIndex(Me.gbEleccionClientesPAF, 0)
        Me.Controls.SetChildIndex(Me.cboSeleccionCentro, 0)
        Me.Controls.SetChildIndex(Me.sbtipo, 0)
        Me.TabControl1.ResumeLayout(False)
        Me.tabPageOrden.ResumeLayout(False)
        CType(Me.txtBRUT1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIVA1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBASE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDTE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTOTAL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtP_DTE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDATA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPREU, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMREPRES, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFRA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCOMIS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREPRES, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgFacturas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.XpGroupBox1.ResumeLayout(False)
        Me.XpGroupBox1.PerformLayout()
        CType(Me.dtpAFechaRecibo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDeFechaRecibo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboAFacturaRecibo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDeFacuraRecibo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        CType(Me.dt2DATA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx2NOMCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx2FRA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx2CLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.dgVencimientos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx2TOTAL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.gbImpresion.ResumeLayout(False)
        Me.gbImpresion.PerformLayout()
        CType(Me.cboAPAFIMPRESION, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDEPAFIMPRESION, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbEleccionClientesPAF.ResumeLayout(False)
        Me.gbEleccionClientesPAF.PerformLayout()
        CType(Me.cboSeleccionClienteParaPAF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbCentro.ResumeLayout(False)
        CType(Me.ppfitxa2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ppfitxa2.ResumeLayout(False)
        Me.ppfitxa2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmFacturasModelos
    Public Shared Function GetInstance() As frmFacturasModelos
        If frmChildForm Is Nothing Then
            frmChildForm = New frmFacturasModelos

        End If
        Return frmChildForm
    End Function


#Region "VARIABLES"

    Public PAFActual As clsPAFVenta

#End Region

#Region "INICIALIZACIÓN"

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True
            Me.arrayGrids = New System.Windows.Forms.Control() {Me.dgFacturas, Me.dgVencimientos}
            Me.arrayCombos = New System.Windows.Forms.Control() {Me.cboNOMCLIENT, Me.cboNOMREPRES, Me.cboAFacturaRecibo, Me.cboDeFacuraRecibo}
            Me.arrayBotones = New System.Windows.Forms.Control() {Me.btnElegirCliente, Me.btnElegirRepresentante, Me.btnGenerarRecibo}
            Me.arrayEtiquetas = New System.Windows.Forms.Control() {Me.Label4, Me.Label5, Me.Label6, Me.lblAFactura, Me.lblAlbaran, Me.lblBase, Me.lblCliente, Me.lblComision, Me.lblDeFactura, Me.lblDescuento, Me.lblDia1, Me.lblDia2, Me.lblDia3, Me.lblDia_1, Me.lblDia_2, Me.lblDia_3, Me.lblFORMA, Me.lblFacturaV, Me.lblFecha, Me.lblFormaDePago, Me.lblIVA, Me.lblIVA2, Me.lblIVA_, Me.lblImpReciboAFecha, Me.lblImpReciboDeFecha, Me.lblPrecio, Me.lblRE, Me.lblRE2, Me.lblRE_, Me.lblRepresentante, Me.lblTotal, Me.lblTotalBruto}
            Me.arrayTabPages = New System.Windows.Forms.Control() {Me.TabPage1, Me.tabPageOrden}
            Me.arrayCheckBox = New System.Windows.Forms.Control() {Me.chkPorIntervaloFechas}
            Me.arrayDateTime = New System.Windows.Forms.Control() {Me.dtpDATA, Me.dt2DATA, Me.dtpDeFechaRecibo, Me.dtpAFechaRecibo}
            Me.arrayTextBox = New System.Windows.Forms.Control() {Me.tx2FRA, Me.tx2NOMCLIENT, Me.tx2TOTAL, Me.txtBASE1, Me.txtBRUT1, Me.txtCLIENT, Me.txtCOMIS, Me.txtDTE1, Me.txtIVA1, Me.txtOBSERV, Me.txtPREU, Me.txtRE1, Me.txtREPRES, Me.txtTOTAL, Me.tx2CLIENT, Me.txtP_DTE}
            PAFActual = New clsPAFVenta(ds.Tables("FACTUR"), empresaPorDefecto, BindingContext, Factura, Prenda)

            HacerBindings()

            IniciarDGDetalle()
            IniciarDGVencim()

            PonerModificables(soloLectura)

            PSBTIPO(PAFActual.centro) : cargando = False
            btnUltimo_Click(btnUltimo, Nothing)
            PonerHandlersErroresParaGrids()
            Dim p As Point
            p.X = gbCentro.Location.X + lblNumeracion.Location.X + lblNumeracion.Size.Width
            p.Y = lblNumeroPAF.Location.Y
            cboSeleccionCentro.Location = p

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub IniciarDGDetalle()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgFacturas)
            i = 0
            PPCol2("ORDRE", dgFacturas, rm.GetString("ORDEN"), "", True, 10, False, PresentationEnum.Normal, _
                False, 80, i, False)
            i = i + 1
            PPCol2("MOSTRA", dgFacturas, rm.GetString("MODELO"), "", True, 10, False, PresentationEnum.Normal, _
                False, 80, i, False)
            i = i + 1
            PPCol2("SERIE", dgFacturas, rm.GetString("SERIE"), "", True, 10, False, PresentationEnum.Normal, _
                False, 80, i, False)
            i = i + 1
            PPCol2("COLOR", dgFacturas, rm.GetString("COLOR"), "", True, 10, False, PresentationEnum.Normal, _
                False, 80, i, False)
            i = i + 1
            PPCol2("ALBAR", dgFacturas, rm.GetString("ALBARAN"), "", True, 10, False, PresentationEnum.Normal, _
                   False, 80, i, False)
            i = i + 1
            PPCol2("UNITATS", dgFacturas, rm.GetString("CANTIDAD"), "", True, 10, False, PresentationEnum.Normal, _
                   False, 80, i, False, Nothing, True, 0, AlignHorzEnum.Far)
            i = i + 1
            PPCol2("PREU", dgFacturas, rm.GetString("PRECIO"), "#,##0.00", True, 10, False, PresentationEnum.Normal, _
                            False, 80, i, False, Nothing, True, 0, AlignHorzEnum.Far)
            i = i + 1
            PPCol2("IMPORT", dgFacturas, rm.GetString("IMPORTE"), "#,##0.00", True, 10, False, PresentationEnum.Normal, _
                            False, 80, i, False, Nothing, True, 0, AlignHorzEnum.Far)

            'AñadirTextBoxColumn("ORDRE", "Ordre", 150, ts, False, "", HorizontalAlignment.Left)
            ''CargarColoresregistroActual(ts.GridColumnStyles(CType("COLOR", Object)).columncombobox)
            'AñadirComboBoxColumn("MODEL", "Model", 90, ts, 10, Nothing, Nothing, Nothing, Nothing)
            'AñadirComboBoxColumn("SERIE", "Serie", 90, ts, 10, Nothing, Nothing, Nothing, Nothing)
            'AñadirComboBoxColumn("COLOR", "Color", 90, ts, 10, Nothing, Nothing, Nothing, Nothing)
            'AñadirTextBoxColumn("ALBARA", "Albarà", 70, ts, False, "", HorizontalAlignment.Left, 10)
            'AñadirTextBoxColumn("QUANT", "Quantitat", 50, ts, False, "", HorizontalAlignment.Left)
            'AñadirTextBoxColumn("PREU", "Preu", 50, ts, False, "", HorizontalAlignment.Left)
            'AñadirTextBoxColumn("IMPORT", "Import", 50, ts, False, "", HorizontalAlignment.Left)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerBindingLabels()
        Try
            AñadirBinding(lblIVA2, PAFActual.dvForm, "P_IVA1")
            AñadirBinding(lblRE2, PAFActual.dvForm, "P_RE1")
            AñadirBinding(lblDia1, PAFActual.dvForm, "DIA1")
            AñadirBinding(lblDia2, PAFActual.dvForm, "DIA2")
            AñadirBinding(lblDia3, PAFActual.dvForm, "DIA3")
            AñadirBinding(lblFORMA, PAFActual.dvForm, "NOMFORMA")

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Private Sub HacerBindings()
        Try
            HacerBindingsTodos(PAFActual.dvForm)
            PonerBindingLabels()
            AñadirBinding(cboFRA, PAFActual.dvForm, "FRA")
            AñadirBindingCombo(cboFRA, PAFActual.dvIdentificadores, "FRA", "FRA")

            AñadirBindingCombo(cboNOMCLIENT, PAFActual.dtclients, CCClients, CNClients)
            AñadirBindingCombo(cboNOMREPRES, PAFActual.dtRepres, CCRepres, CNRepres)
            dgFacturas.SetDataBinding(PAFActual.lineasVenta.dvForm, "")
            dgVencimientos.SetDataBinding(PAFActual.vencimientos.dvForm, "")
            AñadirBindingCombo(cboDeFacuraRecibo, PAFActual.dtIdentificadores.Copy.DefaultView, "FRA", "FRA")
            AñadirBindingCombo(cboAFacturaRecibo, PAFActual.dtIdentificadores.Copy.DefaultView, "FRA", "FRA")
            AñadirBindingCombo(cboDEPAFIMPRESION, PAFActual.dtIdentificadores.Copy.DefaultView, "FRA", "FRA")
            AñadirBindingCombo(cboAPAFIMPRESION, PAFActual.dtIdentificadores.Copy.DefaultView, "FRA", "FRA")

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub IniciarDGVencim()
        Dim i As Integer
        Try
            'TODO Es un calendario
            OcultarColumnasDG(dgVencimientos)
            i = 0
            PPCol2("EMISSIO", dgVencimientos, rm.GetString("EMISION"), "Short Date", True, _
                    80, False, PresentationEnum.Normal, False, 80, i, False)
            i = i + 1
            PPCol2("VENCIM", dgVencimientos, rm.GetString("VENCIMIENTO"), "Short Date", True, _
                    80, False, PresentationEnum.Normal, False, 80, i, False)
            i = i + 1
            PPCol2("IMPORT", dgVencimientos, rm.GetString("IMPORTE"), "#,##0.00", True, _
                    80, False, PresentationEnum.Normal, False, 80, i, False, Nothing, True, 0, AlignHorzEnum.Far)
            i = i + 1
            PPCol2("REMES", dgVencimientos, rm.GetString("REMESADO"), "", True, _
                    50, False, PresentationEnum.CheckBox, False, 50, i, False, Nothing, False, 0, AlignHorzEnum.Center)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
#End Region

#Region "DESPLAZARSE"

    Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSiguiente.Click
        Try
            cargando = True
            PAFActual.SiguienteReg() : ActualizarOrigen()
            PSBTIPO(PAFActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAnterior.Click
        Try
            cargando = True
            PAFActual.AnteriorReg() : ActualizarOrigen()
            PSBTIPO(PAFActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUltimo.Click
        Try
            cargando = True
            PAFActual.UltimoReg() : ActualizarOrigen()
            PSBTIPO(PAFActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnPrimero.Click
        Try
            cargando = True
            PAFActual.PrimeroReg() : ActualizarOrigen()
            PSBTIPO(PAFActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ACTUALIZAR ORIGEN"

    Private Sub PonerControlesNuevo(ByVal b As Boolean)
        Try
            Try
                If Not b Then : btnActualizar.Text = rm.GetString("CONFIRMAR")
                Else : btnActualizar.Text = rm.GetString("ACTUALIZAR") : End If
                ModiNuev(b)
                cboFRA.AutoCompletion = Not editando

            Catch ex As Exception
                LOG(ex.ToString) : cargando = False : CCN()
            End Try
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
                editando = True : PSBTIPO(PAFActual.centro)
                dgFacturas.Visible = True
                dgVencimientos.Visible = True
                PonerControlesNuevo(True)
                EsRegistroNuevo = False
                PAFActual.actualizarNumeraciones()
            End If
            MoverActual()

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
                        PAFActual.lineasVenta.tabla.RejectChanges()
                        PAFActual.vencimientos.tabla.RejectChanges()
                        cargando = False
                        Exit Sub
                End Select
            End If
            PAFActual.ActualizarOrigen() : ActualizarOrigen()
            PSBTIPO(PAFActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "VENCIMIENTOS"

    '!     Private Sub CargarVencimientos()
    '!         Try
    '!             cmdSelectVencim.Connection = cnn
    '! 
    '!             daVencim.SelectCommand = cmdSelectVencim
    '!             cmdSelectVencim.CommandText = "SELECT * FROM vencim WHERE (DOCUMENT = ""F"" AND COMVEN = ""V"") ORDER BY FRA"
    '!             cmdSelectVencim.Connection = cnn
    '!             daVencim.SelectCommand = cmdSelectVencim
    '!             LlenarDataSet("vencim", daVencim)
    '!             BuilderVencim.DataAdapter = daVencim
    '!             BuilderVencim.KeyFields = ""
    '!             daVencim.AcceptChangesDuringFill = True
    '!             daVencim.ContinueUpdateOnError = True
    '!             ds.AcceptChanges()
    '!             dvVencim.Table = ds.Tables("vencim")
    '!             dgVencimientos.DataSource = dvVencim
    '! 
    '!             dvVencim.Sort = "LIN ASC"
    '! 
    '!         Catch ex As Exception
    '!             LOG(ex.ToString) : cargando = false : ccn()
    '!         End Try
    '!     End Sub
    '!     Private Sub IniciarDGVencim()
    '!         Dim ts As New DataGridTableStyle
    '!         Try
    '! 
    '!             ts.MappingName = "vencim"
    '!             AñadirCalendarBoxColumn("EMISSIO", "Emissió", 80, ts, False, "", HorizontalAlignment.Left)
    '!             AñadirCalendarBoxColumn("VENCIM", "Venciment", 80, ts, False, "", HorizontalAlignment.Left)
    '!             AñadirTextBoxColumn("IMPORT", "Import", 70, ts, False, "", HorizontalAlignment.Left)
    '!             ts.AllowSorting = False 'Para que el nº linea no varie 
    '!             
    '!             
    '! 
    '!         Catch ex As Exception
    '!             LOG(ex.ToString) : cargando = false : ccn() :  cnn.Close()
    '!         End Try
    '!     End Sub
    'Friend Sub CanviarVencimientos(Optional ByVal nuevo As Boolean = False)
    '    Try
    '        'Parche para canviar y cargar las direcciones

    '        'Para ocultar columnas en lo que se muestra
    '        If nuevo = False Then

    '            dvVencim.RowFilter = "FRA = '" & nzn(txtCodigoPAFV.Text, 0) & "' AND FILIAL = '" & centro & "'"

    '            dvVencim.Table.Columns("TIPUS").DefaultValue = "M"
    '            dvVencim.Table.Columns("DOCUMENT").DefaultValue = "F"
    '            dvVencim.Table.Columns("FRA").DefaultValue = nzn(txtCodigoCliente.Text, 0)
    '            'dvVencim.Table.Columns("NLINEA").DefaultValue = dvLineasPAFVenta.Count
    '            dvVencim.Table.Columns("COMVEN").DefaultValue = "V"
    '            dvVencim.Table.Columns("VENCIM").DefaultValue = Date.Now.ToShortDateString
    '            dvVencim.Table.Columns("EMISSIO").DefaultValue = Date.Now.ToShortDateString
    '            dvVencim.Table.Columns("IMPORT").DefaultValue = 0
    '            dgVencimientos.Visible = True

    '        Else
    '            dgVencimientos.Visible = False
    '        End If
    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = false : ccn()
    '    End Try
    'End Sub

#End Region

#Region "ORGANIZAR"

    Private Sub CBOFRA_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboFRA.KeyPress
        Try
            If editando Then
                cboFRA.AutoCompletion = False
            Else
                cboFRA.AutoCompletion = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub cboCodigoPAF_ItemChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboFRA.Validating
        Try
            If consulta() Then
                cargando = True
                PAFActual.CambiarAReg(nzn(cboFRA.WillChangeToValue, 0), iraregistro)
                MoverActual()
                PSBTIPO(PAFActual.centro) : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Private Sub PonerModificables(Optional ByVal b As Boolean = False)
        Try

            PonerMod(b)
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
    Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnNuevo.Click
        Try
            cargando = True
            EsRegistroNuevo = True

            If PAFActual.mClienteSel <> -1 Then CambioEleccionTodosPAF()
            cboSeleccionClienteParaPAF.Visible = False
            rdoVerPAFDeCliente.Checked = False
            rdoVerTodasPAF.Checked = True

            dgFacturas.Visible = False
            dgVencimientos.Visible = False
            PonerModificables(modificable)
            PonerControlesNuevo(False)
            cboFRA.ClearItems()
            PSBTIPO(PAFActual.centro)
            'Hay que crear un CALBA nuevo

            PAFActual.NuevoRegistro()
            PAFActual.NOMCLIENT = ""
            PAFActual.NOMREPRES = ""
            'se le pasa esto pq todavia no sabemos que hilo es el que se va a introducir
            PSBTIPO(PAFActual.centro) : cargando = False
            'Nuevo ALBA

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles btnModificar.Click
        Try
            If Not editando Then
                cargando = True
                editando = True
                CambioEleccionTodosPAF()
                cboSeleccionClienteParaPAF.Visible = False
                rdoVerPAFDeCliente.Checked = False
                rdoVerTodasPAF.Checked = True

                PonerModificables(modificable)
                PSBTIPO(PAFActual.centro)
                cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnBorrar.Click
        Try
            If MessageBox.Show(rm.GetString("BorrarActual"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                editando = True
                PAFActual.borrar() : ActualizarOrigen()
                AñadirBindingCombo(cboFRA, PAFActual.dvIdentificadores, "FRA", "FRA")
                PAFActual.tabla.AcceptChanges()
                editando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub MoverActual()
        Try
            cboDeFacuraRecibo.Text = PAFActual.FRA
            cboAFacturaRecibo.Text = PAFActual.FRA
            dtpAFechaRecibo.Value = PAFActual.DATA
            dtpDeFechaRecibo.Value = PAFActual.DATA
            AutoSizeCC(dgFacturas)
            cboDEPAFIMPRESION.Text = PAFActual.FRA
            cboAPAFIMPRESION.Text = PAFActual.FRA

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub

#End Region

#Region "ELECCIONES"

    Private Sub cboNombreCliente_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMCLIENT.SelectedValueChanged
        Try
            With cboNOMCLIENT
                If PuedoModificar() Then
                    cargando = True
                    PAFActual.CLIENT = nzn(.WillChangeToValue, 0)
                    PAFActual.PonerDatosCliente()
                    PSBTIPO(PAFActual.centro) : cargando = False
                End If
            End With

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

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    'La función comprueba si el albarán al cual se va a traspasar es correcto, es decir,
    'no ha sido traspasado a una factura. Si el albaran no existe también devuelve cierto
    Private Sub txtCodigoCliente_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCLIENT.Validated
        Try
            If PuedoModificar() Then cargando = True : PAFActual.CLIENT = nzn(txtCLIENT.Text, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtCodigoRepresentante_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtREPRES.Validated
        Try
            If PuedoModificar() Then cargando = True : PAFActual.REPRES = nzn(txtREPRES.Text, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboSeleccion_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSeleccionCentro.SelectionChangeCommitted
        Try
            If consulta() Then
                cargando = True
                PAFActual.cambioCentro(general.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto), ultimo)
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

#End Region

#Region "SELECCION REGISTRO"

    Private Sub VerPAFDeCliente_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoVerPAFDeCliente.CheckedChanged
        Try
            If Not cargando And consulta() Then
                cargando = True
                If rdoVerPAFDeCliente.Checked = True Then
                    cboSeleccionClienteParaPAF.Visible = True
                    lblNumeroPAF.Visible = True
                    AñadirBindingCombo(cboSeleccionClienteParaPAF, PAFActual.dtClientConPAF, CCClients, CNClients)
                    cboSeleccionClienteParaPAF.Focus()
                    cboSeleccionClienteParaPAF.SelectedIndex = 1
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
    Private Sub CambioEleccionTodosPAF()
        Try
            PAFActual.ElegirCliente(-1)
            PAFActual.numeroRegistros = PAFActual.obtenerNumeroReg(PAFActual.tabla.TableName, "")
            PAFActual.tabla.AcceptChanges()

            AutoSizeCC(cboFRA)
            lblNumeroPAF.Text = rm.GetString("NUMFACTURAS") & " " & PAFActual.dvIdentificadores.Count

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
                    'PAFActual.IDCliente = -1
                    'PAFActual.numeroRegistros = PAFActual.obtenerNumeroReg(PAFActual.tabla.TableName, "")
                    'PAFActual.UltimoReg() : ActualizarOrigen()
                    'CBOOR.Text = PAFActual.FRA
                End If
                cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Private Sub cboSeleccionClienteParaPAF_SelChange(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboSeleccionClienteParaPAF.Validating
        Try
            If consulta() Then
                cargando = True
                PAFActual.ElegirCliente(nzn(cboSeleccionClienteParaPAF.WillChangeToValue, 0))
                AñadirBindingCombo(cboFRA, PAFActual.dvIdentificadores, "FRA", "FRA")
                PAFActual.tabla.AcceptChanges()
                lblNumeroPAF.Text = rm.GetString("NUMFACTURAS") & " " & PAFActual.dvIdentificadores.Count
                MoverActual()
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "IMPRESION"



#End Region

#Region "FACTURAS"

    Private Sub btnGenerarRecibos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerarRecibo.Click
        Try
            'ppFitxa.Document = PAFActual.ImprimirRecibos(nzn(cboDeFacuraRecibo.SelectedValue, 0), _
            '                        nzn(cboAFacturaRecibo.SelectedValue, 0), _
            '                       general.nz(dtpDeFechaRecibo.Value, Date.Today), _
            '                       general.nz(dtpAFechaRecibo.Value, Date.Today), chkPorIntervaloFechas.Checked)
            'ppFitxa.Print(True, 1, ppFitxa.PageCount)
            'btnRecargar_Click(Nothing, Nothing)

            ppfitxa2.Document = PAFActual.ImprimirRecibos(nzn(cboDeFacuraRecibo.SelectedValue, 0), _
                                    nzn(cboAFacturaRecibo.SelectedValue, 0), _
                                   general.nz(dtpDeFechaRecibo.Value, Date.Today), _
                                   general.nz(dtpAFechaRecibo.Value, Date.Today), chkPorIntervaloFechas.Checked)
            ppfitxa2.Document.Print() ' .Print(True, 1, ppFitxa.PageCount)
        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#End Region

#Region "ESPECIFICO"

    Private Sub dgFacturas_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgFacturas.RowColChange
        Try
            If PuedoModificar() Then
                cargando = True
                AutoSizeCC(dgFacturas)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub dgLineasPAFVenta_AfterDelete(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgFacturas.AfterDelete
        Try
            PAFActual.lineasVenta.HacerCalculosLineasPAF("PREU")

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub dgLineasPAFVenta_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgFacturas.AfterColUpdate
        Dim columna As String = e.Column.DataColumn.DataField
        Try
            If PuedoModificar() Then
                cargando = True
                dgFacturas.UpdateData()
                PAFActual.lineasVenta.HacerCalculosLineasPAF(columna)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub

#End Region

#Region "COMUNES"

    Friend Sub btnRecargar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecargar.Click
        Try
            cargando = True
            Dim id As Object
            id = cboFRA.SelectedValue
            PAFActual.ActualizarOrigen()
            PAFActual.CambiarAReg(id, iraregistro)
            MoverActual()
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
#End Region

    Private Sub btnGenerarVencim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerarVencim.Click
        Try
            PAFActual.vencimientos.GenerarVencimientos()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub chkPorIntervaloFechas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPorIntervaloFechas.CheckedChanged
        dtpAFechaRecibo.Enabled = chkPorIntervaloFechas.Checked
        dtpDeFechaRecibo.Enabled = chkPorIntervaloFechas.Checked
    End Sub
    Private Sub dgFacturas_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgFacturas.BeforeDelete
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

    Private Sub dgVencimientos_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgVencimientos.BeforeDelete
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
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Try
            If PAFActual.TieneCambios Then
                Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                    Case DialogResult.Cancel
                        cargando = False
                        Exit Sub
                    Case DialogResult.No
                        PAFActual.tabla.RejectChanges()
                        PAFActual.lineasVenta.tabla.RejectChanges()
                        cargando = False
                        Exit Sub
                    Case DialogResult.Yes
                        PAFActual.ActualizarOrigen()
                End Select
            End If
            PAFActual.ImprimirPAF(PAFActual.FRA, PAFActual.FRA, False)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try

    End Sub
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
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
            End If
        End If
        'If TabControl1.SelectedTab Is tabImpresion Then
        '    Select Case PAFActual.centro
        '        Case "C"
        '        Case "O"
        '            Dim Report As New rptAlbaranModeloCYCO(PAFActual.FRA, PAFActual.FRA, PAFActual.CLIENT, PAFActual.CLIENT, PAFActual.TIPUS, PAFActual.DOCUMENT, PAFActual.centro)
        '            ' assigning a report to the print control
        '            PrintControl1.PrintingSystem = Report.PrintingSystem
        '            ' creating the report document
        '            Report.CreateDocument()
        '    End Select

        'End If

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
End Class

