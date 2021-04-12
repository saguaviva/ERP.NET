Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Imports C1.Win.C1TrueDBGrid



Public Class frmAlbaranesModelos
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
    Friend WithEvents lblSerie As Label
    Friend WithEvents lblModelo As Label
    Friend WithEvents lblOrden As Label
    Friend WithEvents btnImprimir As C1.Win.C1Input.C1Button
    Friend WithEvents tabPageOrden As System.Windows.Forms.TabPage
    Friend WithEvents lblFecha As Label
    Friend WithEvents cboID As C1.Win.C1List.C1Combo
    Friend WithEvents lblAlbaran As Label
    Friend WithEvents tabPageAFactura As System.Windows.Forms.TabPage
    Friend WithEvents dgAlbaranes As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cboColores As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents dtpDATA As C1.Win.C1Input.C1DateEdit
    Friend WithEvents txtREPRES As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboNOMREPRES As C1.Win.C1List.C1Combo
    Friend WithEvents cboNOMCLIENT As C1.Win.C1List.C1Combo
    Friend WithEvents txtCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtPREU As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCOMIS As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtOBSERV As C1.Win.C1Input.C1TextBox
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
    Friend WithEvents txtORDRE As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtMODEL As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtSERIE As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTEMPORADA As C1.Win.C1Input.C1TextBox
    Friend WithEvents lblTemporada As System.Windows.Forms.Label
    Friend WithEvents btnEsogerOrden As C1.Win.C1Input.C1Button
    Friend WithEvents gbEleccionClientesPAF As System.Windows.Forms.GroupBox
    Friend WithEvents lblNumeroPAF As System.Windows.Forms.Label
    Friend WithEvents cboSeleccionClienteParaPAF As C1.Win.C1List.C1Combo
    Friend WithEvents rdoVerTodasPAF As System.Windows.Forms.RadioButton
    Friend WithEvents rdoVerPAFDeCliente As System.Windows.Forms.RadioButton
    Friend WithEvents gbCentro As System.Windows.Forms.GroupBox
    Friend WithEvents lblNumeracion As System.Windows.Forms.Label
    Friend WithEvents gbTraspasarIntervalo As System.Windows.Forms.GroupBox
    Friend WithEvents date2 As C1.Win.C1Input.C1DateEdit
    Friend WithEvents date1 As C1.Win.C1Input.C1DateEdit
    Friend WithEvents txtAPAF As C1.Win.C1Input.C1TextBox
    Friend WithEvents cmbPAF1 As C1.Win.C1List.C1Combo
    Friend WithEvents lblAPAF As System.Windows.Forms.Label
    Friend WithEvents lblConFecha As System.Windows.Forms.Label
    Friend WithEvents cmbPAF2 As C1.Win.C1List.C1Combo
    Friend WithEvents btnTraspasarAPAF As C1.Win.C1Input.C1Button
    Friend WithEvents cmbCliente2 As C1.Win.C1List.C1Combo
    Friend WithEvents cmbCliente1 As C1.Win.C1List.C1Combo
    Friend WithEvents lblDeFecha As System.Windows.Forms.Label
    Friend WithEvents lblHastaCliente As System.Windows.Forms.Label
    Friend WithEvents lblHastaFecha As System.Windows.Forms.Label
    Friend WithEvents lblHastaPAF As System.Windows.Forms.Label
    Friend WithEvents lblDePAF As System.Windows.Forms.Label
    Friend WithEvents lblDeCliente As System.Windows.Forms.Label
    Friend WithEvents dtpAFecha As C1.Win.C1Input.C1DateEdit
    Friend WithEvents chkTRASPAS As System.Windows.Forms.CheckBox
    Friend WithEvents chkESTOC As System.Windows.Forms.CheckBox
    Friend WithEvents txtTALLA09 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA08 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA07 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA06 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA05 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA04 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA03 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA02 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA01 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA10 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtBRUT1 As C1.Win.C1Input.C1TextBox


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmAlbaranesModelos))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tabPageOrden = New System.Windows.Forms.TabPage
        Me.txtBRUT1 = New C1.Win.C1Input.C1TextBox
        Me.txtIVA1 = New C1.Win.C1Input.C1TextBox
        Me.txtRE1 = New C1.Win.C1Input.C1TextBox
        Me.txtBASE1 = New C1.Win.C1Input.C1TextBox
        Me.txtDTE1 = New C1.Win.C1Input.C1TextBox
        Me.txtTOTAL = New C1.Win.C1Input.C1TextBox
        Me.lblRE = New System.Windows.Forms.Label
        Me.lblTotal = New System.Windows.Forms.Label
        Me.lblIVA = New System.Windows.Forms.Label
        Me.lblDescuento = New System.Windows.Forms.Label
        Me.lblBase = New System.Windows.Forms.Label
        Me.lblTotalBruto = New System.Windows.Forms.Label
        Me.cboColores = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnEsogerOrden = New C1.Win.C1Input.C1Button
        Me.lblTemporada = New System.Windows.Forms.Label
        Me.txtTEMPORADA = New C1.Win.C1Input.C1TextBox
        Me.txtSERIE = New C1.Win.C1Input.C1TextBox
        Me.txtMODEL = New C1.Win.C1Input.C1TextBox
        Me.txtORDRE = New C1.Win.C1Input.C1TextBox
        Me.dtpDATA = New C1.Win.C1Input.C1DateEdit
        Me.txtREPRES = New C1.Win.C1Input.C1TextBox
        Me.cboNOMREPRES = New C1.Win.C1List.C1Combo
        Me.cboNOMCLIENT = New C1.Win.C1List.C1Combo
        Me.btnElegirCliente = New C1.Win.C1Input.C1Button
        Me.lblAlbaran = New System.Windows.Forms.Label
        Me.lblCliente = New System.Windows.Forms.Label
        Me.txtCLIENT = New C1.Win.C1Input.C1TextBox
        Me.lblSerie = New System.Windows.Forms.Label
        Me.lblFecha = New System.Windows.Forms.Label
        Me.btnElegirRepresentante = New C1.Win.C1Input.C1Button
        Me.lblRepresentante = New System.Windows.Forms.Label
        Me.txtPREU = New C1.Win.C1Input.C1TextBox
        Me.txtCOMIS = New C1.Win.C1Input.C1TextBox
        Me.lblComision = New System.Windows.Forms.Label
        Me.lblPrecio = New System.Windows.Forms.Label
        Me.lblOrden = New System.Windows.Forms.Label
        Me.cboID = New C1.Win.C1List.C1Combo
        Me.lblModelo = New System.Windows.Forms.Label
        Me.txtTALLA10 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA09 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA08 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA07 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA06 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA05 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA04 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA03 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA02 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA01 = New C1.Win.C1Input.C1TextBox
        Me.dgAlbaranes = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.chkTRASPAS = New System.Windows.Forms.CheckBox
        Me.chkESTOC = New System.Windows.Forms.CheckBox
        Me.txtOBSERV = New C1.Win.C1Input.C1TextBox
        Me.tabPageAFactura = New System.Windows.Forms.TabPage
        Me.gbTraspasarIntervalo = New System.Windows.Forms.GroupBox
        Me.dtpAFecha = New C1.Win.C1Input.C1DateEdit
        Me.date2 = New C1.Win.C1Input.C1DateEdit
        Me.date1 = New C1.Win.C1Input.C1DateEdit
        Me.txtAPAF = New C1.Win.C1Input.C1TextBox
        Me.cmbPAF1 = New C1.Win.C1List.C1Combo
        Me.lblAPAF = New System.Windows.Forms.Label
        Me.lblConFecha = New System.Windows.Forms.Label
        Me.cmbPAF2 = New C1.Win.C1List.C1Combo
        Me.btnTraspasarAPAF = New C1.Win.C1Input.C1Button
        Me.cmbCliente2 = New C1.Win.C1List.C1Combo
        Me.cmbCliente1 = New C1.Win.C1List.C1Combo
        Me.lblDeFecha = New System.Windows.Forms.Label
        Me.lblHastaCliente = New System.Windows.Forms.Label
        Me.lblHastaFecha = New System.Windows.Forms.Label
        Me.lblHastaPAF = New System.Windows.Forms.Label
        Me.lblDePAF = New System.Windows.Forms.Label
        Me.lblDeCliente = New System.Windows.Forms.Label
        Me.btnImprimir = New C1.Win.C1Input.C1Button
        Me.gbEleccionClientesPAF = New System.Windows.Forms.GroupBox
        Me.lblNumeroPAF = New System.Windows.Forms.Label
        Me.cboSeleccionClienteParaPAF = New C1.Win.C1List.C1Combo
        Me.rdoVerTodasPAF = New System.Windows.Forms.RadioButton
        Me.rdoVerPAFDeCliente = New System.Windows.Forms.RadioButton
        Me.gbCentro = New System.Windows.Forms.GroupBox
        Me.lblNumeracion = New System.Windows.Forms.Label
        Me.TabControl1.SuspendLayout()
        Me.tabPageOrden.SuspendLayout()
        CType(Me.txtBRUT1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIVA1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBASE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDTE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTOTAL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboColores, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtTEMPORADA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSERIE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMODEL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtORDRE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDATA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREPRES, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMREPRES, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPREU, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCOMIS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA09, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA08, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA07, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA06, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA05, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA04, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA03, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA02, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA01, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgAlbaranes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPageAFactura.SuspendLayout()
        Me.gbTraspasarIntervalo.SuspendLayout()
        CType(Me.dtpAFecha, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.date2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.date1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAPAF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPAF1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPAF2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCliente2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCliente1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbEleccionClientesPAF.SuspendLayout()
        CType(Me.cboSeleccionClienteParaPAF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbCentro.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnUltimo
        '
        Me.btnUltimo.Name = "btnUltimo"
        '
        'btnPrimero
        '
        Me.btnPrimero.Name = "btnPrimero"
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.Name = "cboSeleccionCentro"
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(5, 144)
        Me.sbtipo.Name = "sbtipo"
        Me.sbtipo.Text = ""
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(390, 44)
        Me.btnVerLista.Name = "btnVerLista"
        '
        'btnActualizar
        '
        Me.btnActualizar.Name = "btnActualizar"
        '
        'btnAnterior
        '
        Me.btnAnterior.Name = "btnAnterior"
        '
        'btnRecargar
        '
        Me.btnRecargar.Name = "btnRecargar"
        '
        'btnNuevo
        '
        Me.btnNuevo.Name = "btnNuevo"
        '
        'btnBorrar
        '
        Me.btnBorrar.Name = "btnBorrar"
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(390, 64)
        Me.btnTancar.Name = "btnTancar"
        '
        'btnModificar
        '
        Me.btnModificar.Name = "btnModificar"
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Name = "btnSiguiente"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tabPageOrden)
        Me.TabControl1.Controls.Add(Me.tabPageAFactura)
        Me.TabControl1.ItemSize = New System.Drawing.Size(42, 18)
        Me.TabControl1.Location = New System.Drawing.Point(8, 56)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(972, 497)
        Me.TabControl1.TabIndex = 0
        '
        'tabPageOrden
        '
        Me.tabPageOrden.BackColor = System.Drawing.Color.FromArgb(CType(176, Byte), CType(206, Byte), CType(172, Byte))
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
        Me.tabPageOrden.Controls.Add(Me.cboColores)
        Me.tabPageOrden.Controls.Add(Me.GroupBox2)
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
        Me.tabPageOrden.Controls.Add(Me.dgAlbaranes)
        Me.tabPageOrden.Controls.Add(Me.chkTRASPAS)
        Me.tabPageOrden.Controls.Add(Me.chkESTOC)
        Me.tabPageOrden.Controls.Add(Me.txtOBSERV)
        Me.tabPageOrden.Location = New System.Drawing.Point(4, 22)
        Me.tabPageOrden.Name = "tabPageOrden"
        Me.tabPageOrden.Size = New System.Drawing.Size(964, 471)
        Me.tabPageOrden.TabIndex = 0
        Me.tabPageOrden.Text = "Albarà"
        '
        'txtBRUT1
        '
        Me.txtBRUT1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBRUT1.CustomFormat = "#,##0.00"
        Me.txtBRUT1.DataType = GetType(System.Double)
        Me.txtBRUT1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtBRUT1.Location = New System.Drawing.Point(676, 438)
        Me.txtBRUT1.Name = "txtBRUT1"
        Me.txtBRUT1.Size = New System.Drawing.Size(70, 20)
        Me.txtBRUT1.TabIndex = 293
        Me.txtBRUT1.Tag = Nothing
        '
        'txtIVA1
        '
        Me.txtIVA1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtIVA1.CustomFormat = "#,##0.00"
        Me.txtIVA1.DataType = GetType(System.Double)
        Me.txtIVA1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtIVA1.Location = New System.Drawing.Point(524, 438)
        Me.txtIVA1.Name = "txtIVA1"
        Me.txtIVA1.Size = New System.Drawing.Size(70, 20)
        Me.txtIVA1.TabIndex = 292
        Me.txtIVA1.Tag = Nothing
        '
        'txtRE1
        '
        Me.txtRE1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRE1.CustomFormat = "#,##0.00"
        Me.txtRE1.DataType = GetType(System.Double)
        Me.txtRE1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtRE1.Location = New System.Drawing.Point(404, 438)
        Me.txtRE1.Name = "txtRE1"
        Me.txtRE1.Size = New System.Drawing.Size(70, 20)
        Me.txtRE1.TabIndex = 291
        Me.txtRE1.Tag = Nothing
        '
        'txtBASE1
        '
        Me.txtBASE1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBASE1.CustomFormat = "#,##0.00"
        Me.txtBASE1.DataType = GetType(System.Double)
        Me.txtBASE1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtBASE1.Location = New System.Drawing.Point(292, 438)
        Me.txtBASE1.Name = "txtBASE1"
        Me.txtBASE1.Size = New System.Drawing.Size(70, 20)
        Me.txtBASE1.TabIndex = 290
        Me.txtBASE1.Tag = Nothing
        '
        'txtDTE1
        '
        Me.txtDTE1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDTE1.CustomFormat = "#,##0.00"
        Me.txtDTE1.DataType = GetType(System.Double)
        Me.txtDTE1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtDTE1.Location = New System.Drawing.Point(148, 438)
        Me.txtDTE1.Name = "txtDTE1"
        Me.txtDTE1.Size = New System.Drawing.Size(70, 20)
        Me.txtDTE1.TabIndex = 289
        Me.txtDTE1.Tag = Nothing
        '
        'txtTOTAL
        '
        Me.txtTOTAL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTOTAL.CustomFormat = "#,##0.00"
        Me.txtTOTAL.DataType = GetType(System.Double)
        Me.txtTOTAL.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtTOTAL.Location = New System.Drawing.Point(808, 438)
        Me.txtTOTAL.Name = "txtTOTAL"
        Me.txtTOTAL.Size = New System.Drawing.Size(70, 20)
        Me.txtTOTAL.TabIndex = 288
        Me.txtTOTAL.Tag = Nothing
        '
        'lblRE
        '
        Me.lblRE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRE.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRE.Location = New System.Drawing.Point(372, 438)
        Me.lblRE.Name = "lblRE"
        Me.lblRE.Size = New System.Drawing.Size(30, 20)
        Me.lblRE.TabIndex = 287
        Me.lblRE.Text = "RE"
        Me.lblRE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotal.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTotal.Location = New System.Drawing.Point(764, 438)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(44, 20)
        Me.lblTotal.TabIndex = 286
        Me.lblTotal.Text = "TOTAL"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIVA
        '
        Me.lblIVA.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblIVA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblIVA.Location = New System.Drawing.Point(492, 438)
        Me.lblIVA.Name = "lblIVA"
        Me.lblIVA.Size = New System.Drawing.Size(30, 20)
        Me.lblIVA.TabIndex = 285
        Me.lblIVA.Text = "IVA"
        Me.lblIVA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDescuento
        '
        Me.lblDescuento.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDescuento.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDescuento.Location = New System.Drawing.Point(112, 438)
        Me.lblDescuento.Name = "lblDescuento"
        Me.lblDescuento.Size = New System.Drawing.Size(36, 20)
        Me.lblDescuento.TabIndex = 284
        Me.lblDescuento.Text = "Dte.:"
        Me.lblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBase
        '
        Me.lblBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBase.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblBase.Location = New System.Drawing.Point(228, 438)
        Me.lblBase.Name = "lblBase"
        Me.lblBase.Size = New System.Drawing.Size(66, 20)
        Me.lblBase.TabIndex = 283
        Me.lblBase.Text = "Base"
        Me.lblBase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalBruto
        '
        Me.lblTotalBruto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalBruto.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTotalBruto.Location = New System.Drawing.Point(612, 438)
        Me.lblTotalBruto.Name = "lblTotalBruto"
        Me.lblTotalBruto.Size = New System.Drawing.Size(66, 20)
        Me.lblTotalBruto.TabIndex = 282
        Me.lblTotalBruto.Text = "Total Brut"
        Me.lblTotalBruto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboColores
        '
        Me.cboColores.AllowColMove = True
        Me.cboColores.AllowColSelect = True
        Me.cboColores.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboColores.AlternatingRows = False
        Me.cboColores.CaptionHeight = 17
        Me.cboColores.ColumnCaptionHeight = 17
        Me.cboColores.ColumnFooterHeight = 17
        Me.cboColores.FetchRowStyles = False
        Me.cboColores.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.cboColores.Location = New System.Drawing.Point(108, 240)
        Me.cboColores.Name = "cboColores"
        Me.cboColores.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboColores.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.cboColores.RowHeight = 15
        Me.cboColores.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboColores.ScrollTips = False
        Me.cboColores.Size = New System.Drawing.Size(62, 100)
        Me.cboColores.TabIndex = 280
        Me.cboColores.Text = "C1TrueDBDropdown1"
        Me.cboColores.ValueTranslate = True
        Me.cboColores.Visible = False
        Me.cboColores.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:HighlightTe" & _
        "xt;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Headin" & _
        "g{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;Al" & _
        "ignVert:Center;}Style8{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style" & _
        "9{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.DropdownView Name="""" CaptionHeig" & _
        "ht=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeight=""17"" MarqueeStyle=""DottedCel" & _
        "lBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=""17"" RecordSelectors=""False"" Ve" & _
        "rticalScrollGroup=""1"" HorizontalScrollGroup=""1""><CaptionStyle parent=""Style2"" me" & _
        "=""Style10"" /><EditorStyle parent=""Editor"" me=""Style5"" /><EvenRowStyle parent=""Ev" & _
        "enRow"" me=""Style8"" /><FilterBarStyle parent=""FilterBar"" me=""Style13"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style12"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style7"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style9"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "11"" /><SelectedStyle parent=""Selected"" me=""Style6"" /><Style parent=""Normal"" me=""" & _
        "Style1"" /><ClientRect>0, 0, 58, 96</ClientRect><BorderSide>0</BorderSide></C1.Wi" & _
        "n.C1TrueDBGrid.DropdownView></Splits><NamedStyles><Style parent="""" me=""Normal"" /" & _
        "><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /><St" & _
        "yle parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" /><Sty" & _
        "le parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Editor"" /><Style p" & _
        "arent=""Normal"" me=""HighlightRow"" /><Style parent=""Normal"" me=""EvenRow"" /><Style " & _
        "parent=""Normal"" me=""OddRow"" /><Style parent=""Heading"" me=""RecordSelector"" /><Sty" & _
        "le parent=""Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""Group"" /></Named" & _
        "Styles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>None</Layout>" & _
        "<DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 58, 96</ClientArea>" & _
        "</Blob>"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnEsogerOrden)
        Me.GroupBox2.Controls.Add(Me.lblTemporada)
        Me.GroupBox2.Controls.Add(Me.txtTEMPORADA)
        Me.GroupBox2.Controls.Add(Me.txtSERIE)
        Me.GroupBox2.Controls.Add(Me.txtMODEL)
        Me.GroupBox2.Controls.Add(Me.txtORDRE)
        Me.GroupBox2.Controls.Add(Me.dtpDATA)
        Me.GroupBox2.Controls.Add(Me.txtREPRES)
        Me.GroupBox2.Controls.Add(Me.cboNOMREPRES)
        Me.GroupBox2.Controls.Add(Me.cboNOMCLIENT)
        Me.GroupBox2.Controls.Add(Me.btnElegirCliente)
        Me.GroupBox2.Controls.Add(Me.lblAlbaran)
        Me.GroupBox2.Controls.Add(Me.lblCliente)
        Me.GroupBox2.Controls.Add(Me.txtCLIENT)
        Me.GroupBox2.Controls.Add(Me.lblSerie)
        Me.GroupBox2.Controls.Add(Me.lblFecha)
        Me.GroupBox2.Controls.Add(Me.btnElegirRepresentante)
        Me.GroupBox2.Controls.Add(Me.lblRepresentante)
        Me.GroupBox2.Controls.Add(Me.txtPREU)
        Me.GroupBox2.Controls.Add(Me.txtCOMIS)
        Me.GroupBox2.Controls.Add(Me.lblComision)
        Me.GroupBox2.Controls.Add(Me.lblPrecio)
        Me.GroupBox2.Controls.Add(Me.lblOrden)
        Me.GroupBox2.Controls.Add(Me.cboID)
        Me.GroupBox2.Controls.Add(Me.lblModelo)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(524, 196)
        Me.GroupBox2.TabIndex = 279
        Me.GroupBox2.TabStop = False
        '
        'btnEsogerOrden
        '
        Me.btnEsogerOrden.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEsogerOrden.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEsogerOrden.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnEsogerOrden.Location = New System.Drawing.Point(100, 36)
        Me.btnEsogerOrden.Name = "btnEsogerOrden"
        Me.btnEsogerOrden.Size = New System.Drawing.Size(124, 22)
        Me.btnEsogerOrden.TabIndex = 383
        Me.btnEsogerOrden.Text = "Escollir Ordre"
        '
        'lblTemporada
        '
        Me.lblTemporada.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTemporada.Location = New System.Drawing.Point(224, 84)
        Me.lblTemporada.Name = "lblTemporada"
        Me.lblTemporada.Size = New System.Drawing.Size(83, 20)
        Me.lblTemporada.TabIndex = 382
        Me.lblTemporada.Text = "Temporada"
        Me.lblTemporada.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTEMPORADA
        '
        Me.txtTEMPORADA.Location = New System.Drawing.Point(312, 88)
        Me.txtTEMPORADA.Name = "txtTEMPORADA"
        Me.txtTEMPORADA.Size = New System.Drawing.Size(120, 20)
        Me.txtTEMPORADA.TabIndex = 381
        Me.txtTEMPORADA.Tag = Nothing
        '
        'txtSERIE
        '
        Me.txtSERIE.Location = New System.Drawing.Point(100, 84)
        Me.txtSERIE.Name = "txtSERIE"
        Me.txtSERIE.Size = New System.Drawing.Size(120, 20)
        Me.txtSERIE.TabIndex = 380
        Me.txtSERIE.Tag = Nothing
        '
        'txtMODEL
        '
        Me.txtMODEL.Location = New System.Drawing.Point(312, 60)
        Me.txtMODEL.Name = "txtMODEL"
        Me.txtMODEL.Size = New System.Drawing.Size(120, 20)
        Me.txtMODEL.TabIndex = 379
        Me.txtMODEL.Tag = Nothing
        '
        'txtORDRE
        '
        Me.txtORDRE.Location = New System.Drawing.Point(100, 60)
        Me.txtORDRE.Name = "txtORDRE"
        Me.txtORDRE.Size = New System.Drawing.Size(120, 20)
        Me.txtORDRE.TabIndex = 378
        Me.txtORDRE.Tag = Nothing
        '
        'dtpDATA
        '
        '
        'dtpDATA.Calendar
        '
        Me.dtpDATA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        'Me.dtpDATA.Calendar.UIStrings.Content = New String() {"&Clear:&Netejar", "&Today:&Avui"}
        Me.dtpDATA.DisableOnNoData = False
        Me.dtpDATA.EmptyAsNull = True
        Me.dtpDATA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpDATA.Location = New System.Drawing.Point(384, 16)
        Me.dtpDATA.Name = "dtpDATA"
        Me.dtpDATA.Size = New System.Drawing.Size(116, 20)
        Me.dtpDATA.TabIndex = 377
        Me.dtpDATA.Tag = Nothing
        '
        'txtREPRES
        '
        Me.txtREPRES.Location = New System.Drawing.Point(100, 136)
        Me.txtREPRES.Name = "txtREPRES"
        Me.txtREPRES.Size = New System.Drawing.Size(48, 20)
        Me.txtREPRES.TabIndex = 8
        Me.txtREPRES.Tag = Nothing
        '
        'cboNOMREPRES
        '
        Me.cboNOMREPRES.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
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
        Me.cboNOMREPRES.GapHeight = 2
        Me.cboNOMREPRES.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.cboNOMREPRES.IntegralHeight = True
        Me.cboNOMREPRES.ItemHeight = 13
        Me.cboNOMREPRES.Location = New System.Drawing.Point(176, 136)
        Me.cboNOMREPRES.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMREPRES.MaxDropDownItems = CType(8, Short)
        Me.cboNOMREPRES.MaxLength = 255
        Me.cboNOMREPRES.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMREPRES.Name = "cboNOMREPRES"
        'Me.cbo.PartialRightColumn = False
        Me.cboNOMREPRES.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboNOMREPRES.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMREPRES.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMREPRES.Size = New System.Drawing.Size(320, 19)
        Me.cboNOMREPRES.TabIndex = 9
        Me.cboNOMREPRES.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:T" & _
        "rue;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:" & _
        "Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:Near;}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'cboNOMCLIENT
        '
        Me.cboNOMCLIENT.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
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
        Me.cboNOMCLIENT.GapHeight = 2
        Me.cboNOMCLIENT.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Image))
        Me.cboNOMCLIENT.IntegralHeight = True
        Me.cboNOMCLIENT.ItemHeight = 13
        Me.cboNOMCLIENT.Location = New System.Drawing.Point(176, 112)
        Me.cboNOMCLIENT.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMCLIENT.MaxDropDownItems = CType(8, Short)
        Me.cboNOMCLIENT.MaxLength = 0
        Me.cboNOMCLIENT.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMCLIENT.Name = "cboNOMCLIENT"
        'Me.cbo.PartialRightColumn = False
        Me.cboNOMCLIENT.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboNOMCLIENT.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMCLIENT.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMCLIENT.Size = New System.Drawing.Size(320, 19)
        Me.cboNOMCLIENT.TabIndex = 4
        Me.cboNOMCLIENT.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:T" & _
        "rue;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:" & _
        "Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:Near;}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'btnElegirCliente
        '
        Me.btnElegirCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirCliente.Location = New System.Drawing.Point(148, 112)
        Me.btnElegirCliente.Name = "btnElegirCliente"
        Me.btnElegirCliente.Size = New System.Drawing.Size(26, 20)
        Me.btnElegirCliente.TabIndex = 3
        Me.btnElegirCliente.Text = "..."
        '
        'lblAlbaran
        '
        Me.lblAlbaran.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAlbaran.Location = New System.Drawing.Point(12, 16)
        Me.lblAlbaran.Name = "lblAlbaran"
        Me.lblAlbaran.Size = New System.Drawing.Size(83, 20)
        Me.lblAlbaran.TabIndex = 239
        Me.lblAlbaran.Text = "Albarà"
        Me.lblAlbaran.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCliente
        '
        Me.lblCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCliente.Location = New System.Drawing.Point(12, 112)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(83, 20)
        Me.lblCliente.TabIndex = 47
        Me.lblCliente.Text = "Client"
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCLIENT
        '
        Me.txtCLIENT.Location = New System.Drawing.Point(100, 112)
        Me.txtCLIENT.Name = "txtCLIENT"
        Me.txtCLIENT.Size = New System.Drawing.Size(48, 20)
        Me.txtCLIENT.TabIndex = 2
        Me.txtCLIENT.Tag = Nothing
        '
        'lblSerie
        '
        Me.lblSerie.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblSerie.Location = New System.Drawing.Point(12, 84)
        Me.lblSerie.Name = "lblSerie"
        Me.lblSerie.Size = New System.Drawing.Size(83, 20)
        Me.lblSerie.TabIndex = 43
        Me.lblSerie.Text = "Serie"
        Me.lblSerie.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFecha
        '
        Me.lblFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFecha.Location = New System.Drawing.Point(304, 16)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(76, 20)
        Me.lblFecha.TabIndex = 36
        Me.lblFecha.Text = "Data"
        Me.lblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnElegirRepresentante
        '
        Me.btnElegirRepresentante.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirRepresentante.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirRepresentante.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirRepresentante.Location = New System.Drawing.Point(148, 136)
        Me.btnElegirRepresentante.Name = "btnElegirRepresentante"
        Me.btnElegirRepresentante.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirRepresentante.TabIndex = 52
        Me.btnElegirRepresentante.Text = "..."
        '
        'lblRepresentante
        '
        Me.lblRepresentante.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRepresentante.Location = New System.Drawing.Point(12, 136)
        Me.lblRepresentante.Name = "lblRepresentante"
        Me.lblRepresentante.Size = New System.Drawing.Size(83, 20)
        Me.lblRepresentante.TabIndex = 51
        Me.lblRepresentante.Text = "Representant"
        Me.lblRepresentante.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPREU
        '
        Me.txtPREU.CustomFormat = "#,##0.00"
        Me.txtPREU.DataType = GetType(System.Double)
        Me.txtPREU.EditFormat.CustomFormat = "#,##0.00"
        Me.txtPREU.EditFormat.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None
        Me.txtPREU.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtPREU.Location = New System.Drawing.Point(444, 164)
        Me.txtPREU.Name = "txtPREU"
        Me.txtPREU.Size = New System.Drawing.Size(48, 20)
        Me.txtPREU.TabIndex = 10
        Me.txtPREU.Tag = Nothing
        '
        'txtCOMIS
        '
        Me.txtCOMIS.Location = New System.Drawing.Point(100, 164)
        Me.txtCOMIS.Name = "txtCOMIS"
        Me.txtCOMIS.Size = New System.Drawing.Size(48, 20)
        Me.txtCOMIS.TabIndex = 11
        Me.txtCOMIS.Tag = Nothing
        '
        'lblComision
        '
        Me.lblComision.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblComision.Location = New System.Drawing.Point(12, 164)
        Me.lblComision.Name = "lblComision"
        Me.lblComision.Size = New System.Drawing.Size(83, 20)
        Me.lblComision.TabIndex = 57
        Me.lblComision.Text = "% Comissió"
        Me.lblComision.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPrecio
        '
        Me.lblPrecio.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPrecio.Location = New System.Drawing.Point(356, 164)
        Me.lblPrecio.Name = "lblPrecio"
        Me.lblPrecio.Size = New System.Drawing.Size(85, 20)
        Me.lblPrecio.TabIndex = 55
        Me.lblPrecio.Text = "Preu"
        Me.lblPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblOrden
        '
        Me.lblOrden.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblOrden.Location = New System.Drawing.Point(12, 60)
        Me.lblOrden.Name = "lblOrden"
        Me.lblOrden.Size = New System.Drawing.Size(83, 20)
        Me.lblOrden.TabIndex = 38
        Me.lblOrden.Text = "Ordre"
        Me.lblOrden.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboID
        '
        Me.cboID.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cboID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboID.Caption = ""
        Me.cboID.CaptionHeight = 17
        Me.cboID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboID.ColumnCaptionHeight = 17
        Me.cboID.ColumnFooterHeight = 17
        Me.cboID.ContentHeight = 15
        Me.cboID.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboID.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboID.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboID.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboID.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboID.EditorHeight = 15
        Me.cboID.GapHeight = 2
        Me.cboID.Images.Add(CType(resources.GetObject("resource3"), System.Drawing.Image))
        Me.cboID.IntegralHeight = True
        Me.cboID.ItemHeight = 13
        Me.cboID.Location = New System.Drawing.Point(100, 16)
        Me.cboID.MatchEntryTimeout = CType(100, Long)
        Me.cboID.MaxDropDownItems = CType(15, Short)
        Me.cboID.MaxLength = 0
        Me.cboID.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboID.Name = "cboID"
        ''Me.cbo.PartialRightColumn = False
        Me.cboID.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboID.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboID.Size = New System.Drawing.Size(120, 19)
        Me.cboID.TabIndex = 0
        Me.cboID.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:T" & _
        "rue;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:" & _
        "Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:Near;}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'lblModelo
        '
        Me.lblModelo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblModelo.Location = New System.Drawing.Point(224, 60)
        Me.lblModelo.Name = "lblModelo"
        Me.lblModelo.Size = New System.Drawing.Size(83, 20)
        Me.lblModelo.TabIndex = 41
        Me.lblModelo.Text = "Model"
        Me.lblModelo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTALLA10
        '
        Me.txtTALLA10.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA10.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA10.Location = New System.Drawing.Point(706, 208)
        Me.txtTALLA10.MaxLength = 3
        Me.txtTALLA10.Name = "txtTALLA10"
        Me.txtTALLA10.Size = New System.Drawing.Size(50, 18)
        Me.txtTALLA10.TabIndex = 276
        Me.txtTALLA10.Tag = Nothing
        '
        'txtTALLA09
        '
        Me.txtTALLA09.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA09.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA09.Location = New System.Drawing.Point(654, 208)
        Me.txtTALLA09.MaxLength = 3
        Me.txtTALLA09.Name = "txtTALLA09"
        Me.txtTALLA09.Size = New System.Drawing.Size(48, 18)
        Me.txtTALLA09.TabIndex = 275
        Me.txtTALLA09.Tag = Nothing
        '
        'txtTALLA08
        '
        Me.txtTALLA08.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA08.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA08.Location = New System.Drawing.Point(602, 208)
        Me.txtTALLA08.MaxLength = 3
        Me.txtTALLA08.Name = "txtTALLA08"
        Me.txtTALLA08.Size = New System.Drawing.Size(48, 18)
        Me.txtTALLA08.TabIndex = 274
        Me.txtTALLA08.Tag = Nothing
        '
        'txtTALLA07
        '
        Me.txtTALLA07.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA07.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA07.Location = New System.Drawing.Point(552, 208)
        Me.txtTALLA07.MaxLength = 3
        Me.txtTALLA07.Name = "txtTALLA07"
        Me.txtTALLA07.Size = New System.Drawing.Size(46, 18)
        Me.txtTALLA07.TabIndex = 273
        Me.txtTALLA07.Tag = Nothing
        '
        'txtTALLA06
        '
        Me.txtTALLA06.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA06.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA06.Location = New System.Drawing.Point(502, 208)
        Me.txtTALLA06.MaxLength = 3
        Me.txtTALLA06.Name = "txtTALLA06"
        Me.txtTALLA06.Size = New System.Drawing.Size(46, 18)
        Me.txtTALLA06.TabIndex = 272
        Me.txtTALLA06.Tag = Nothing
        '
        'txtTALLA05
        '
        Me.txtTALLA05.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA05.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA05.Location = New System.Drawing.Point(450, 208)
        Me.txtTALLA05.MaxLength = 3
        Me.txtTALLA05.Name = "txtTALLA05"
        Me.txtTALLA05.Size = New System.Drawing.Size(48, 18)
        Me.txtTALLA05.TabIndex = 271
        Me.txtTALLA05.Tag = Nothing
        '
        'txtTALLA04
        '
        Me.txtTALLA04.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA04.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA04.Location = New System.Drawing.Point(400, 208)
        Me.txtTALLA04.MaxLength = 3
        Me.txtTALLA04.Name = "txtTALLA04"
        Me.txtTALLA04.Size = New System.Drawing.Size(46, 18)
        Me.txtTALLA04.TabIndex = 270
        Me.txtTALLA04.Tag = Nothing
        '
        'txtTALLA03
        '
        Me.txtTALLA03.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA03.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA03.Location = New System.Drawing.Point(348, 208)
        Me.txtTALLA03.MaxLength = 3
        Me.txtTALLA03.Name = "txtTALLA03"
        Me.txtTALLA03.Size = New System.Drawing.Size(50, 18)
        Me.txtTALLA03.TabIndex = 269
        Me.txtTALLA03.Tag = Nothing
        '
        'txtTALLA02
        '
        Me.txtTALLA02.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA02.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA02.Location = New System.Drawing.Point(298, 208)
        Me.txtTALLA02.MaxLength = 3
        Me.txtTALLA02.Name = "txtTALLA02"
        Me.txtTALLA02.Size = New System.Drawing.Size(48, 18)
        Me.txtTALLA02.TabIndex = 268
        Me.txtTALLA02.Tag = Nothing
        '
        'txtTALLA01
        '
        Me.txtTALLA01.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTALLA01.Font = New System.Drawing.Font("Tahoma", 6.5!)
        Me.txtTALLA01.Location = New System.Drawing.Point(246, 208)
        Me.txtTALLA01.MaxLength = 3
        Me.txtTALLA01.Name = "txtTALLA01"
        Me.txtTALLA01.Size = New System.Drawing.Size(48, 18)
        Me.txtTALLA01.TabIndex = 267
        Me.txtTALLA01.Tag = Nothing
        '
        'dgAlbaranes
        '
        Me.dgAlbaranes.AllowAddNew = True
        Me.dgAlbaranes.AllowDelete = True
        Me.dgAlbaranes.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dgAlbaranes.CaptionHeight = 17
        Me.dgAlbaranes.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.dgAlbaranes.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgAlbaranes.Images.Add(CType(resources.GetObject("resource4"), System.Drawing.Image))
        Me.dgAlbaranes.Location = New System.Drawing.Point(76, 208)
        Me.dgAlbaranes.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.dgAlbaranes.Name = "dgAlbaranes"
        Me.dgAlbaranes.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgAlbaranes.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgAlbaranes.PreviewInfo.ZoomFactor = 75
        Me.dgAlbaranes.RecordSelectorWidth = 17
        Me.dgAlbaranes.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgAlbaranes.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.dgAlbaranes.RowHeight = 15
        Me.dgAlbaranes.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.dgAlbaranes.Size = New System.Drawing.Size(805, 204)
        Me.dgAlbaranes.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgAlbaranes.TabIndex = 277
        Me.dgAlbaranes.Text = "C1TrueDBGrid1"
        Me.dgAlbaranes.WrapCellPointer = True
        Me.dgAlbaranes.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Style9{}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:Hig" & _
        "hlightText;BackColor:Highlight;}Style14{}OddRow{}RecordSelector{AlignImage:Cente" & _
        "r;}Style15{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeCol" & _
        "or:ControlText;BackColor:Control;}Style8{}Style10{AlignHorz:Near;}Style11{}Style" & _
        "12{}Style13{}Style1{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name" & _
        "="""" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeight=""17"" MarqueeS" & _
        "tyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=""17"" VerticalScr" & _
        "ollGroup=""1"" HorizontalScrollGroup=""1""><CaptionStyle parent=""Style2"" me=""Style10" & _
        """ /><EditorStyle parent=""Editor"" me=""Style5"" /><EvenRowStyle parent=""EvenRow"" me" & _
        "=""Style8"" /><FilterBarStyle parent=""FilterBar"" me=""Style13"" /><FooterStyle paren" & _
        "t=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style12"" /><HeadingStyle" & _
        " parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow"" me=""Sty" & _
        "le7"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle parent=""OddRo" & _
        "w"" me=""Style9"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style11"" /><Se" & _
        "lectedStyle parent=""Selected"" me=""Style6"" /><Style parent=""Normal"" me=""Style1"" /" & _
        "><ClientRect>0, 0, 801, 200</ClientRect><BorderSide>0</BorderSide></C1.Win.C1Tru" & _
        "eDBGrid.MergeView></Splits><NamedStyles><Style parent="""" me=""Normal"" /><Style pa" & _
        "rent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /><Style parent" & _
        "=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" /><Style parent=" & _
        """Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Editor"" /><Style parent=""Nor" & _
        "mal"" me=""HighlightRow"" /><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""No" & _
        "rmal"" me=""OddRow"" /><Style parent=""Heading"" me=""RecordSelector"" /><Style parent=" & _
        """Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""Group"" /></NamedStyles><ve" & _
        "rtSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>None</Layout><DefaultRe" & _
        "cSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 801, 200</ClientArea><PrintPa" & _
        "geHeaderStyle parent="""" me=""Style14"" /><PrintPageFooterStyle parent="""" me=""Style" & _
        "15"" /></Blob>"
        '
        'chkTRASPAS
        '
        Me.chkTRASPAS.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkTRASPAS.BackColor = System.Drawing.Color.Transparent
        Me.chkTRASPAS.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkTRASPAS.Location = New System.Drawing.Point(656, 4)
        Me.chkTRASPAS.Name = "chkTRASPAS"
        Me.chkTRASPAS.TabIndex = 25
        Me.chkTRASPAS.Text = "Traspasat"
        '
        'chkESTOC
        '
        Me.chkESTOC.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkESTOC.BackColor = System.Drawing.Color.Transparent
        Me.chkESTOC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkESTOC.Location = New System.Drawing.Point(544, 4)
        Me.chkESTOC.Name = "chkESTOC"
        Me.chkESTOC.TabIndex = 24
        Me.chkESTOC.Text = "Restat a l'Stock"
        '
        'txtOBSERV
        '
        Me.txtOBSERV.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtOBSERV.Location = New System.Drawing.Point(544, 36)
        Me.txtOBSERV.Multiline = True
        Me.txtOBSERV.Name = "txtOBSERV"
        Me.txtOBSERV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOBSERV.Size = New System.Drawing.Size(388, 168)
        Me.txtOBSERV.TabIndex = 12
        Me.txtOBSERV.Tag = Nothing
        '
        'tabPageAFactura
        '
        Me.tabPageAFactura.BackColor = System.Drawing.Color.FromArgb(CType(176, Byte), CType(206, Byte), CType(172, Byte))
        Me.tabPageAFactura.Controls.Add(Me.gbTraspasarIntervalo)
        Me.tabPageAFactura.Location = New System.Drawing.Point(4, 22)
        Me.tabPageAFactura.Name = "tabPageAFactura"
        Me.tabPageAFactura.Size = New System.Drawing.Size(964, 471)
        Me.tabPageAFactura.TabIndex = 3
        Me.tabPageAFactura.Text = "Traspàs a Factura"
        '
        'gbTraspasarIntervalo
        '
        Me.gbTraspasarIntervalo.BackColor = System.Drawing.Color.FromArgb(CType(176, Byte), CType(206, Byte), CType(172, Byte))
        Me.gbTraspasarIntervalo.Controls.Add(Me.dtpAFecha)
        Me.gbTraspasarIntervalo.Controls.Add(Me.date2)
        Me.gbTraspasarIntervalo.Controls.Add(Me.date1)
        Me.gbTraspasarIntervalo.Controls.Add(Me.txtAPAF)
        Me.gbTraspasarIntervalo.Controls.Add(Me.cmbPAF1)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblAPAF)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblConFecha)
        Me.gbTraspasarIntervalo.Controls.Add(Me.cmbPAF2)
        Me.gbTraspasarIntervalo.Controls.Add(Me.btnTraspasarAPAF)
        Me.gbTraspasarIntervalo.Controls.Add(Me.cmbCliente2)
        Me.gbTraspasarIntervalo.Controls.Add(Me.cmbCliente1)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblDeFecha)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblHastaCliente)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblHastaFecha)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblHastaPAF)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblDePAF)
        Me.gbTraspasarIntervalo.Controls.Add(Me.lblDeCliente)
        Me.gbTraspasarIntervalo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.gbTraspasarIntervalo.Location = New System.Drawing.Point(16, 16)
        Me.gbTraspasarIntervalo.Name = "gbTraspasarIntervalo"
        Me.gbTraspasarIntervalo.Size = New System.Drawing.Size(492, 264)
        Me.gbTraspasarIntervalo.TabIndex = 26
        Me.gbTraspasarIntervalo.TabStop = False
        '
        'dtpAFecha
        '
        '
        'dtpAFecha.Calendar
        '
        Me.dtpAFecha.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpAFecha.DisableOnNoData = False
        Me.dtpAFecha.EmptyAsNull = True
        Me.dtpAFecha.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpAFecha.Location = New System.Drawing.Point(356, 136)
        Me.dtpAFecha.Name = "dtpAFecha"
        Me.dtpAFecha.Size = New System.Drawing.Size(108, 20)
        Me.dtpAFecha.TabIndex = 384
        Me.dtpAFecha.Tag = Nothing
        '
        'date2
        '
        '
        'date2.Calendar
        '
        Me.date2.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.date2.DisableOnNoData = False
        Me.date2.EmptyAsNull = True
        Me.date2.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.date2.Location = New System.Drawing.Point(356, 56)
        Me.date2.Name = "date2"
        Me.date2.Size = New System.Drawing.Size(124, 20)
        Me.date2.TabIndex = 383
        Me.date2.Tag = Nothing
        '
        'date1
        '
        '
        'date1.Calendar
        '
        Me.date1.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.date1.DisableOnNoData = False
        Me.date1.EmptyAsNull = True
        Me.date1.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.date1.Location = New System.Drawing.Point(116, 56)
        Me.date1.Name = "date1"
        Me.date1.Size = New System.Drawing.Size(124, 20)
        Me.date1.TabIndex = 382
        Me.date1.Tag = Nothing
        '
        'txtAPAF
        '
        Me.txtAPAF.Location = New System.Drawing.Point(144, 136)
        Me.txtAPAF.Name = "txtAPAF"
        Me.txtAPAF.TabIndex = 268
        Me.txtAPAF.Tag = Nothing
        '
        'cmbPAF1
        '
        Me.cmbPAF1.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cmbPAF1.AutoSelect = True
        Me.cmbPAF1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPAF1.Caption = ""
        Me.cmbPAF1.CaptionHeight = 17
        Me.cmbPAF1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbPAF1.ColumnCaptionHeight = 17
        Me.cmbPAF1.ColumnFooterHeight = 17
        Me.cmbPAF1.ContentHeight = 15
        Me.cmbPAF1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPAF1.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cmbPAF1.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbPAF1.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cmbPAF1.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmbPAF1.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPAF1.EditorHeight = 15
        Me.cmbPAF1.GapHeight = 2
        Me.cmbPAF1.Images.Add(CType(resources.GetObject("resource5"), System.Drawing.Image))
        Me.cmbPAF1.IntegralHeight = True
        Me.cmbPAF1.ItemHeight = 13
        Me.cmbPAF1.Location = New System.Drawing.Point(116, 32)
        Me.cmbPAF1.MatchEntryTimeout = CType(100, Long)
        Me.cmbPAF1.MaxDropDownItems = CType(8, Short)
        Me.cmbPAF1.MaxLength = 0
        Me.cmbPAF1.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbPAF1.Name = "cmbPAF1"
        'Me.cmbPAF1.PartialRightColumn = False
        Me.cmbPAF1.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cmbPAF1.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cmbPAF1.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbPAF1.Size = New System.Drawing.Size(124, 19)
        Me.cmbPAF1.TabIndex = 0
        Me.cmbPAF1.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:T" & _
        "rue;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:" & _
        "Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:Near;}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'lblAPAF
        '
        Me.lblAPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAPAF.Location = New System.Drawing.Point(8, 136)
        Me.lblAPAF.Name = "lblAPAF"
        Me.lblAPAF.Size = New System.Drawing.Size(136, 17)
        Me.lblAPAF.TabIndex = 16
        Me.lblAPAF.Text = "Traspassar a Factura"
        Me.lblAPAF.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblConFecha
        '
        Me.lblConFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblConFecha.Location = New System.Drawing.Point(268, 136)
        Me.lblConFecha.Name = "lblConFecha"
        Me.lblConFecha.Size = New System.Drawing.Size(84, 17)
        Me.lblConFecha.TabIndex = 18
        Me.lblConFecha.Text = "Amb Data"
        Me.lblConFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbPAF2
        '
        Me.cmbPAF2.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cmbPAF2.AutoSelect = True
        Me.cmbPAF2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPAF2.Caption = ""
        Me.cmbPAF2.CaptionHeight = 17
        Me.cmbPAF2.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbPAF2.ColumnCaptionHeight = 17
        Me.cmbPAF2.ColumnFooterHeight = 17
        Me.cmbPAF2.ContentHeight = 15
        Me.cmbPAF2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPAF2.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cmbPAF2.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbPAF2.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cmbPAF2.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmbPAF2.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPAF2.EditorHeight = 15
        Me.cmbPAF2.GapHeight = 2
        Me.cmbPAF2.Images.Add(CType(resources.GetObject("resource6"), System.Drawing.Image))
        Me.cmbPAF2.IntegralHeight = True
        Me.cmbPAF2.ItemHeight = 13
        Me.cmbPAF2.Location = New System.Drawing.Point(356, 32)
        Me.cmbPAF2.MatchEntryTimeout = CType(100, Long)
        Me.cmbPAF2.MaxDropDownItems = CType(8, Short)
        Me.cmbPAF2.MaxLength = 0
        Me.cmbPAF2.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbPAF2.Name = "cmbPAF2"
        'Me.cmbPAF2.PartialRightColumn = False
        Me.cmbPAF2.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cmbPAF2.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cmbPAF2.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbPAF2.Size = New System.Drawing.Size(124, 19)
        Me.cmbPAF2.TabIndex = 1
        Me.cmbPAF2.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:T" & _
        "rue;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:" & _
        "Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:Near;}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'btnTraspasarAPAF
        '
        Me.btnTraspasarAPAF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTraspasarAPAF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTraspasarAPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnTraspasarAPAF.Location = New System.Drawing.Point(352, 224)
        Me.btnTraspasarAPAF.Name = "btnTraspasarAPAF"
        Me.btnTraspasarAPAF.Size = New System.Drawing.Size(128, 32)
        Me.btnTraspasarAPAF.TabIndex = 9
        Me.btnTraspasarAPAF.TabStop = False
        Me.btnTraspasarAPAF.Text = "Fer Traspàs a Factura"
        '
        'cmbCliente2
        '
        Me.cmbCliente2.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cmbCliente2.AutoSelect = True
        Me.cmbCliente2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbCliente2.Caption = ""
        Me.cmbCliente2.CaptionHeight = 17
        Me.cmbCliente2.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbCliente2.ColumnCaptionHeight = 17
        Me.cmbCliente2.ColumnFooterHeight = 17
        Me.cmbCliente2.ContentHeight = 15
        Me.cmbCliente2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbCliente2.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cmbCliente2.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbCliente2.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cmbCliente2.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmbCliente2.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbCliente2.EditorHeight = 15
        Me.cmbCliente2.GapHeight = 2
        Me.cmbCliente2.Images.Add(CType(resources.GetObject("resource7"), System.Drawing.Image))
        Me.cmbCliente2.IntegralHeight = True
        Me.cmbCliente2.ItemHeight = 13
        Me.cmbCliente2.Location = New System.Drawing.Point(356, 84)
        Me.cmbCliente2.MatchEntryTimeout = CType(100, Long)
        Me.cmbCliente2.MaxDropDownItems = CType(8, Short)
        Me.cmbCliente2.MaxLength = 0
        Me.cmbCliente2.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbCliente2.Name = "cmbCliente2"
        'Me.cmbCliente2.PartialRightColumn = False
        Me.cmbCliente2.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cmbCliente2.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cmbCliente2.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbCliente2.Size = New System.Drawing.Size(124, 19)
        Me.cmbCliente2.TabIndex = 5
        Me.cmbCliente2.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:T" & _
        "rue;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:" & _
        "Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:Near;}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'cmbCliente1
        '
        Me.cmbCliente1.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cmbCliente1.AutoSelect = True
        Me.cmbCliente1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbCliente1.Caption = ""
        Me.cmbCliente1.CaptionHeight = 17
        Me.cmbCliente1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbCliente1.ColumnCaptionHeight = 17
        Me.cmbCliente1.ColumnFooterHeight = 17
        Me.cmbCliente1.ContentHeight = 15
        Me.cmbCliente1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbCliente1.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cmbCliente1.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbCliente1.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cmbCliente1.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmbCliente1.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbCliente1.EditorHeight = 15
        Me.cmbCliente1.GapHeight = 2
        Me.cmbCliente1.Images.Add(CType(resources.GetObject("resource8"), System.Drawing.Image))
        Me.cmbCliente1.IntegralHeight = True
        Me.cmbCliente1.ItemHeight = 13
        Me.cmbCliente1.Location = New System.Drawing.Point(116, 84)
        Me.cmbCliente1.MatchEntryTimeout = CType(100, Long)
        Me.cmbCliente1.MaxDropDownItems = CType(8, Short)
        Me.cmbCliente1.MaxLength = 0
        Me.cmbCliente1.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbCliente1.Name = "cmbCliente1"
        'Me.cmbCliente1.PartialRightColumn = False
        Me.cmbCliente1.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cmbCliente1.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cmbCliente1.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbCliente1.Size = New System.Drawing.Size(124, 19)
        Me.cmbCliente1.TabIndex = 4
        Me.cmbCliente1.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:T" & _
        "rue;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:" & _
        "Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:Near;}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'lblDeFecha
        '
        Me.lblDeFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDeFecha.Location = New System.Drawing.Point(12, 60)
        Me.lblDeFecha.Name = "lblDeFecha"
        Me.lblDeFecha.Size = New System.Drawing.Size(100, 16)
        Me.lblDeFecha.TabIndex = 13
        Me.lblDeFecha.Text = "De la Data"
        Me.lblDeFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHastaCliente
        '
        Me.lblHastaCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblHastaCliente.Location = New System.Drawing.Point(252, 84)
        Me.lblHastaCliente.Name = "lblHastaCliente"
        Me.lblHastaCliente.Size = New System.Drawing.Size(100, 19)
        Me.lblHastaCliente.TabIndex = 12
        Me.lblHastaCliente.Text = "Al Client"
        Me.lblHastaCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHastaFecha
        '
        Me.lblHastaFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblHastaFecha.Location = New System.Drawing.Point(252, 60)
        Me.lblHastaFecha.Name = "lblHastaFecha"
        Me.lblHastaFecha.Size = New System.Drawing.Size(100, 16)
        Me.lblHastaFecha.TabIndex = 11
        Me.lblHastaFecha.Text = "A la Data"
        Me.lblHastaFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHastaPAF
        '
        Me.lblHastaPAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblHastaPAF.Location = New System.Drawing.Point(252, 32)
        Me.lblHastaPAF.Name = "lblHastaPAF"
        Me.lblHastaPAF.Size = New System.Drawing.Size(100, 19)
        Me.lblHastaPAF.TabIndex = 10
        Me.lblHastaPAF.Text = "A l'Albarà"
        Me.lblHastaPAF.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDePAF
        '
        Me.lblDePAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDePAF.Location = New System.Drawing.Point(12, 32)
        Me.lblDePAF.Name = "lblDePAF"
        Me.lblDePAF.Size = New System.Drawing.Size(100, 19)
        Me.lblDePAF.TabIndex = 9
        Me.lblDePAF.Text = "De l'Albarà"
        Me.lblDePAF.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDeCliente
        '
        Me.lblDeCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDeCliente.Location = New System.Drawing.Point(12, 84)
        Me.lblDeCliente.Name = "lblDeCliente"
        Me.lblDeCliente.Size = New System.Drawing.Size(100, 19)
        Me.lblDeCliente.TabIndex = 8
        Me.lblDeCliente.Text = "Del Client"
        Me.lblDeCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnImprimir
        '
        Me.btnImprimir.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimir.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnImprimir.Location = New System.Drawing.Point(808, 572)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(80, 22)
        Me.btnImprimir.TabIndex = 22
        Me.btnImprimir.Text = "Imprimir"
        '
        'gbEleccionClientesPAF
        '
        Me.gbEleccionClientesPAF.BackColor = System.Drawing.Color.FromArgb(CType(133, Byte), CType(180, Byte), CType(126, Byte))
        Me.gbEleccionClientesPAF.Controls.Add(Me.lblNumeroPAF)
        Me.gbEleccionClientesPAF.Controls.Add(Me.cboSeleccionClienteParaPAF)
        Me.gbEleccionClientesPAF.Controls.Add(Me.rdoVerTodasPAF)
        Me.gbEleccionClientesPAF.Controls.Add(Me.rdoVerPAFDeCliente)
        Me.gbEleccionClientesPAF.Location = New System.Drawing.Point(8, 0)
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
        Me.cboSeleccionClienteParaPAF.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
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
        Me.cboSeleccionClienteParaPAF.GapHeight = 2
        Me.cboSeleccionClienteParaPAF.Images.Add(CType(resources.GetObject("resource9"), System.Drawing.Image))
        Me.cboSeleccionClienteParaPAF.IntegralHeight = True
        Me.cboSeleccionClienteParaPAF.ItemHeight = 13
        Me.cboSeleccionClienteParaPAF.LimitToList = True
        Me.cboSeleccionClienteParaPAF.Location = New System.Drawing.Point(232, 22)
        Me.cboSeleccionClienteParaPAF.MatchEntryTimeout = CType(100, Long)
        Me.cboSeleccionClienteParaPAF.MaxDropDownItems = CType(8, Short)
        Me.cboSeleccionClienteParaPAF.MaxLength = 255
        Me.cboSeleccionClienteParaPAF.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboSeleccionClienteParaPAF.Name = "cboSeleccionClienteParaPAF"
        'Me.cbo.PartialRightColumn = False
        Me.cboSeleccionClienteParaPAF.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboSeleccionClienteParaPAF.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboSeleccionClienteParaPAF.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboSeleccionClienteParaPAF.Size = New System.Drawing.Size(316, 19)
        Me.cboSeleccionClienteParaPAF.TabIndex = 2
        Me.cboSeleccionClienteParaPAF.Visible = False
        Me.cboSeleccionClienteParaPAF.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:T" & _
        "rue;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:" & _
        "Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:Near;}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
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
        Me.gbCentro.BackColor = System.Drawing.Color.FromArgb(CType(133, Byte), CType(180, Byte), CType(126, Byte))
        Me.gbCentro.Controls.Add(Me.lblNumeracion)
        Me.gbCentro.Location = New System.Drawing.Point(712, 0)
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
        'frmAlbaranesModelos
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(176, Byte), CType(206, Byte), CType(172, Byte))
        Me.CancelButton = Nothing
        Me.ClientSize = New System.Drawing.Size(992, 618)
        Me.Controls.Add(Me.gbEleccionClientesPAF)
        Me.Controls.Add(Me.gbCentro)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnImprimir)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "frmAlbaranesModelos"
        Me.Text = "Albarans de Models"
        Me.Controls.SetChildIndex(Me.cboSeleccionCentro, 0)
        Me.Controls.SetChildIndex(Me.btnActualizar, 0)
        Me.Controls.SetChildIndex(Me.btnNuevo, 0)
        Me.Controls.SetChildIndex(Me.btnBorrar, 0)
        Me.Controls.SetChildIndex(Me.btnTancar, 0)
        Me.Controls.SetChildIndex(Me.btnUltimo, 0)
        Me.Controls.SetChildIndex(Me.btnPrimero, 0)
        Me.Controls.SetChildIndex(Me.btnAnterior, 0)
        Me.Controls.SetChildIndex(Me.btnSiguiente, 0)
        Me.Controls.SetChildIndex(Me.btnModificar, 0)
        Me.Controls.SetChildIndex(Me.btnImprimir, 0)
        Me.Controls.SetChildIndex(Me.sbtipo, 0)
        Me.Controls.SetChildIndex(Me.btnRecargar, 0)
        Me.Controls.SetChildIndex(Me.btnVerLista, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.gbCentro, 0)
        Me.Controls.SetChildIndex(Me.gbEleccionClientesPAF, 0)
        Me.TabControl1.ResumeLayout(False)
        Me.tabPageOrden.ResumeLayout(False)
        CType(Me.txtBRUT1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIVA1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBASE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDTE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTOTAL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboColores, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.txtTEMPORADA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSERIE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMODEL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtORDRE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDATA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREPRES, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMREPRES, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPREU, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCOMIS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA09, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA08, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA07, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA06, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA05, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA04, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA03, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA02, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA01, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgAlbaranes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPageAFactura.ResumeLayout(False)
        Me.gbTraspasarIntervalo.ResumeLayout(False)
        CType(Me.dtpAFecha, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.date2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.date1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAPAF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPAF1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPAF2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCliente2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCliente1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbEleccionClientesPAF.ResumeLayout(False)
        CType(Me.cboSeleccionClienteParaPAF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbCentro.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmAlbaranesModelos
    Public Shared Function GetInstance() As frmAlbaranesModelos
        If frmChildForm Is Nothing Then
            frmChildForm = New frmAlbaranesModelos

        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Public PAFActual As clsPAFVenta

#End Region

#Region "INICIALIZAR"

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Cursor = Cursors.WaitCursor
            Me.arrayTextBox = New System.Windows.Forms.Control() {Me.txtBASE1, Me.txtBRUT1, Me.txtCLIENT, Me.txtCOMIS, Me.txtDTE1, Me.txtIVA1, Me.txtMODEL, Me.txtOBSERV, Me.txtORDRE, Me.txtPREU, Me.txtRE1, Me.txtREPRES, Me.txtSERIE, Me.txtTALLA01, Me.txtTALLA10, Me.txtTALLA02, Me.txtTALLA03, Me.txtTALLA04, Me.txtTALLA05, Me.txtTALLA06, Me.txtTALLA07, Me.txtTALLA08, Me.txtTALLA09, Me.txtTEMPORADA, Me.txtTOTAL, Me.txtORDRE, Me.txtMODEL, Me.txtSERIE}
            Me.arrayBotones = New System.Windows.Forms.Control() {Me.btnElegirCliente, Me.btnElegirRepresentante, Me.btnEsogerOrden}
            Me.arrayEtiquetas = New System.Windows.Forms.Control() {Me.lblAlbaran, Me.lblCliente, Me.lblComision, Me.lblFecha, Me.lblModelo, Me.lblOrden, Me.lblPrecio, Me.lblRepresentante, Me.lblSerie}
            Me.arrayTabPages = New System.Windows.Forms.Control() {Me.tabPageAFactura, Me.tabPageOrden}
            Me.arrayCombos = New System.Windows.Forms.Control() {Me.cboNOMCLIENT, Me.cboNOMREPRES}
            Me.arrayGrids = New System.Windows.Forms.Control() {Me.dgAlbaranes}
            Me.arrayCheckBox = New System.Windows.Forms.Control() {Me.chkESTOC, Me.chkTRASPAS}
            Me.arrayDateTime = New System.Windows.Forms.Control() {Me.dtpDATA}
            cargando = True
            ACN()
            'PAFActual = New clsPAFVentaAlbaranModelo(ds.Tables(tablaVentas), empresaPorDefecto, BindingContext, Prenda)
            PAFActual = New clsPAFVenta(ds.Tables(tablaVentas), empresaPorDefecto, BindingContext, Albaran, Prenda)
            HacerBindings()
            IniciarDGDetalle()
            'PonerSiNoTraspasado()
            PonerModificables(soloLectura)
            PSBTIPO(PAFActual.centro) : cargando = False
            'btnUltimo_Click(btnUltimo, Nothing)
            ccn()
            
            PonerHandlersErroresParaGrids()
            dtpAFecha.Value = Date.Today
            Dim p As Point
            p.X = gbCentro.Location.X + lblNumeracion.Location.X + lblNumeracion.Size.Width
            p.Y = lblNumeroPAF.Location.Y
            cboSeleccionCentro.Location = p
            dgAlbaranes.ColumnFooters = True
            dgAlbaranes.FooterStyle.Font = New System.Drawing.Font("Tahoma", 12, FontStyle.Bold, GraphicsUnit.Pixel)
            dgAlbaranes.FooterStyle.BackColor = Color.GreenYellow

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub IniciarDGDetalle()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgAlbaranes)
            i = 0
            PPCol2("COLOR", dgAlbaranes, rm.GetString("COLOR"), "", True, 150, False, C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox, False, 150, i, False, cboColores)
            HacerBindingsSeleccion()
            i = i + 1
            PPCol2("TALLA01", dgAlbaranes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = i + 1
            PPCol2("TALLA02", dgAlbaranes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = i + 1
            PPCol2("TALLA03", dgAlbaranes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = i + 1
            PPCol2("TALLA04", dgAlbaranes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = i + 1
            PPCol2("TALLA05", dgAlbaranes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = i + 1
            PPCol2("TALLA06", dgAlbaranes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = i + 1
            PPCol2("TALLA07", dgAlbaranes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = i + 1
            PPCol2("TALLA08", dgAlbaranes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = i + 1
            PPCol2("TALLA09", dgAlbaranes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = i + 1
            PPCol2("TALLA10", dgAlbaranes, "", "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = i + 1
            PPCol2("UNITATS", dgAlbaranes, rm.GetString("UNIDADES"), "#,##0.00", True, 45, False, PresentationEnum.Normal, False, 45, i, False, Nothing, False, 0, AlignHorzEnum.Far)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Sub
    Private Sub HacerBindingsSeleccion()
        Try
            AñadirBindingCombo(cboColores, PAFActual.dtColoresPrendas, "MODCOL", "MODCOL")
            OcultarColumnaDropDown(cboColores)
            cboColores.DisplayColumns("MODCOL").Visible = True
            AutosizeColumnasTrueDBDropdown(cboColores)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub HacerBindings()
        Try
            'Bindings CALBA
            HacerBindingsTodos(PAFActual.dvForm)

            AñadirBinding(cboID, PAFActual.dvForm, "FRA")
            AñadirBindingCombo(cboID, PAFActual.dvIdentificadores, "FRA", "FRA")

            'CargaTabla(tablaModelos, "CODI", "CODI", PAFActual.dtModels, True)

            AñadirBindingCombo(cboNOMCLIENT, PAFActual.dtclients, CCClients, CNClients)
            OcultarTodasColumnaCbo(cboNOMCLIENT)
            AñadirBindingCombo(cboNOMREPRES, PAFActual.dtRepres, CCRepres, CNRepres)

            'Bindings ALBA (el detalle)
            dgAlbaranes.SetDataBinding(PAFActual.lineasVenta.dvForm, "")

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "DESPLAZARSE"

    Private Sub MoverActual(Optional ByVal nuevo As Integer = 1)
        Try
            cargando = True
            cmbCliente1.Text = PAFActual.CLIENT
            cmbCliente2.Text = PAFActual.CLIENT
            cmbPAF1.Text = PAFActual.FRA
            cmbPAF2.Text = PAFActual.FRA
            date1.Value = PAFActual.DATA
            date2.Value = PAFActual.DATA
            dtpAFecha.Value = Now.Date

            dgAlbaranes.Splits(0).DisplayColumns("UNITATS").DataColumn.FooterText = sumaTotal("TALLA01", PAFActual.lineasVenta.dvForm) + _
                    sumaTotal("TALLA02", PAFActual.lineasVenta.dvForm) + _
                    sumaTotal("TALLA03", PAFActual.lineasVenta.dvForm) + sumaTotal("TALLA04", PAFActual.lineasVenta.dvForm) + _
                    sumaTotal("TALLA05", PAFActual.lineasVenta.dvForm) + sumaTotal("TALLA06", PAFActual.lineasVenta.dvForm) + _
                    sumaTotal("TALLA07", PAFActual.lineasVenta.dvForm) + sumaTotal("TALLA08", PAFActual.lineasVenta.dvForm) + _
                    sumaTotal("TALLA09", PAFActual.lineasVenta.dvForm) + sumaTotal("TALLA10", PAFActual.lineasVenta.dvForm)

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
            If b Then : btnActualizar.Text = rm.GetString("ACTUALIZAR")
            Else : btnActualizar.Text = rm.GetString("CONFIRMAR") : End If
            ModiNuev(b)
            cboID.AutoCompletion = Not editando

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
                editando = True : PSBTIPO(PAFActual.centro)
                dgAlbaranes.Visible = True
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
            If EsRegistroNuevo Then
                If PAFActual.ALBCLI = "" Then
                    If MessageBox.Show(rm.GetString("FALTAPEDIDOCLIENTE"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNo) = DialogResult.No Then
                        cargando = False
                        Exit Sub
                    End If
                End If
            End If

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
                End Select
            End If
            PAFActual.ActualizarOrigen() : ActualizarOrigen()
            PSBTIPO(PAFActual.centro)
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
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
    Private Sub cboSeleccion_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSeleccionCentro.SelectionChangeCommitted
        Try
            If consulta() Then
                cargando = True
                PAFActual.cambioCentro(GENERAL.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto), ultimo)
                MoverActual()
                'Aqui hay que hacer un moveractual
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
    Private Sub txtCodigoCliente_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCLIENT.Validated
        Try
            If PuedoModificar() Then cargando = True : PAFActual.CLIENT = nzn(cboNOMCLIENT.WillChangeToValue, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtCodigoRepresentante_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtREPRES.Validated
        Try
            If PuedoModificar() Then cargando = True : PAFActual.REPRES = nzn(cboNOMREPRES.Text, 0) : cargando = False

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
    Private Sub cboNombreRepresentante_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMREPRES.SelectedValueChanged
        Try
            If PuedoModificar() Then
                cargando = True
                PAFActual.REPRES = nzn(cboNOMREPRES.WillChangeToValue, 0)
                PAFActual.PonerDatosRepre()
                PSBTIPO(PAFActual.centro) : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "TRASPASOS"

    Private Sub btnTraspasarAPAF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTraspasarAPAF.Click
        Try
            If Not cargando Then
                cargando = True
                PAFActual.Traspasar(nzn(txtAPAF.Text, 0), dtpAFecha.Value, cmbPAF1.Text, cmbPAF2.Text, _
                                    date1.Value, date2.Value, nzn(cmbCliente1.Text, 0), nzn(cmbCliente2.Text, 0), False)
                PSBTIPO(PAFActual.centro) : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#End Region

#Region "ORGANIZAR"

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
    Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnNuevo.Click

        Try

            If PAFActual.mClienteSel <> -1 Then CambioEleccionTodosPAF()
            cboSeleccionClienteParaPAF.Visible = False
            rdoVerPAFDeCliente.Checked = False
            rdoVerTodasPAF.Checked = True

            ActualizarOrigen()
            PonerModificables(modificable)
            PonerControlesNuevo(False)
            dgAlbaranes.Visible = False
            cargando = True
            EsRegistroNuevo = True
            PAFActual.NuevoRegistro()
            PSBTIPO(PAFActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnBorrar.Click
        Try
            If MessageBox.Show(rm.GetString("BorrarActual"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                cargando = True
                editando = True
                PAFActual.borrar() : ActualizarOrigen()

                AñadirBindingCombo(cboID, PAFActual.dvIdentificadores, "FRA", "FRA")
                PAFActual.tabla.AcceptChanges()

                MoverActual()
                editando = False
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Function obtenerCantidad(ByVal linea As Integer) As Double
        Try
            Return PAFActual.lineasVenta.dvForm(linea).Item("QUANTITAT")

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function obtenerImporte(ByVal linea As Integer) As Double
        Try
            Return PAFActual.lineasVenta.dvForm(linea).Item("QUANTITAT") * PAFActual.PREU
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Sub chkRestadoStock_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkESTOC.CheckedChanged
        Try
            If chkESTOC.Checked = True Then
                RestarStock()
            Else
                SumarStock()
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Function obtenerfechaPedido(ByVal ordre As Integer) As OleDb.OleDbType
        Try
            '!!!!!!!!!!
            Return PAFActual.DATA.ToOADate

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function obtenerDatosCliente(ByVal cliente As String) As String
        Dim cmdSel As New MySqlCommand
        Dim str As String
        Dim dareader As MySqlDataReader
        Try

            ' If cboDireccionEntrega.Text = "" Then
            str = "SELECT NOM, DOM, CP, PROV, POB, NIF FROM " & tablaClientes & " WHERE CODI = """ & cliente & """ "
            ' Else
            'Hay que coger los datos de ADRES
            'str = "SELECT clients.NOM, ADRES.DOM, ADRES.CP, ADRES.PROV, ADRES.POB, clients.NIF FROM " & tablaClientes & " LEFT JOIN ADRES ON (clients.CODI = ADRES.CLIENT)"
            'End If
            cmdSel.CommandText = str
            cmdSel.Connection = cnn
            ACN()
            dareader = cmdSel.ExecuteReader

            While dareader.Read
                With dareader
                    ' If cboDireccionEntrega.Text = "" Then
                    '  str = .Item("NOM") & vbCrLf & .Item("DOM") & vbCrLf & .Item("CP") & " " & .Item("POB") & vbCrLf & .Item("PROV") & vbCrLf & vbCrLf & .Item("NIF")
                    '  Else
                    str = .Item("NOM") & vbCrLf & .Item("DOM") & vbCrLf & .Item("CP") & " " & .Item("POB") & vbCrLf & .Item("PROV") & vbCrLf & vbCrLf & .Item("NIF")
                    ' End If

                End With
            End While
            dareader.Close()
            CCN()
            Return str

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN() : dareader.Close()
        End Try
    End Function
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
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerModificables(Optional ByVal b As Boolean = False)
        Try
            PonerMod(b)
            If Not b Then : cboID.DataSource = Nothing
            Else : AñadirBindingCombo(cboID, PAFActual.dvIdentificadores, "FRA", "FRA") : PAFActual.tabla.AcceptChanges() : End If

            cboID.LimitToList = b
            cboID.SuperBack = b
            cboID.ReadOnly = editando Or EsRegistroNuevo
            ModiNuev(b)

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
    Private Sub dgAlbaranes_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgAlbaranes.RowColChange
        Try
            'MessageBox.Show(PAFActual.DTCOL.Rows.Count)
            'MessageBox.Show(cboColores.DataSource.rows.count)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub dgAlbaranes_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgAlbaranes.AfterColUpdate
        Dim columna As String = e.Column.DataColumn.DataField
        Try
            If PuedoModificar() Then
                cargando = True
                dgAlbaranes.UpdateData()
                PAFActual.lineasVenta.HacerCalculosLineasPAF(columna)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Private Sub btnEsogerOrden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEsogerOrden.Click
        Try
            cargando = True
            Cursor = Cursors.WaitCursor
            Dim f As frmListaOrdenesModelos = frmListaOrdenesModelos.GetInstance(esEleccion)
            f.ShowDialog()
            Cursor = Cursors.Default
            If Not f.dr Is Nothing Then
                PAFActual.MODEL = general.nz(f.dr("MODEL"), "")
                PAFActual.ORDRE = f.dr("ORDRE")
                PAFActual.CLIENT = f.dr("CLIENT")
                PAFActual.SERIE = general.nz(f.dr("SERIE"), "")
                PAFActual.TEMPORADA = general.nz(f.dr("TEMPORADA"), "")
                PAFActual.PREU = f.dr("PREU")
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

                PAFActual.PonerNombres()
                PAFActual.TratarSeleccion()
                HacerBindingsSeleccion()
            End If
            f = Nothing
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub

#End Region

#Region "STOCK"

    Private Sub RestartockTallas(ByVal fila As Integer)
        Dim cmdUpd As New MySqlCommand
        Dim SQL As String = "UPDATE MODSTK SET "
        Try
            If Not PAFActual.TALLA01 = "" Then

                SQL = SQL & " TALLA01 = TALLA01 - """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL1"), True) & ""","
            End If
            If Not PAFActual.TALLA02 = "" Then

                SQL = SQL & " TALLA02 = TALLA02 -  """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL2"), True) & ""","
            End If
            If Not PAFActual.TALLA01 = "" Then

                SQL = SQL & " TALLA03 = TALLA03 - """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL3"), True) & ""","
            End If
            If Not PAFActual.TALLA01 = "" Then

                SQL = SQL & " TALLA04 = TALLA04 - """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL4"), True) & ""","
            End If
            If Not PAFActual.TALLA01 = "" Then

                SQL = SQL & " TALLA05 = TALLA05 -  """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL5"), True) & ""","
            End If
            If Not PAFActual.TALLA02 = "" Then

                SQL = SQL & " TALLA06 = TALLA06 -  """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL6"), True) & ""","
            End If
            If Not PAFActual.TALLA03 = "" Then

                SQL = SQL & " TALLA07 = TALLA07 -  """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL7"), True) & ""","
            End If
            If Not PAFActual.TALLA01 = "" Then

                SQL = SQL & " TALLA08 = TALLA08 -  """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL8"), True) & ""","
            End If
            If Not PAFActual.TALLA01 = "" Then

                SQL = SQL & " TALLA09 = TALLA09 -  """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL9"), True) & ""","
            End If
            If Not PAFActual.TALLA01 = "" Then

                SQL = SQL & " TALLA10 = TALLA10 -  """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL10"), True) & ""","
            End If
            SQL = SQL.Substring(0, SQL.Length - 1)
            '!!!!!!!!!!!! falta la temporada

            SQL = SQL & " WHERE MODEL = """ & general.NS(PAFActual.MODEL) & """ AND CLIENT = """ & PAFActual.CLIENT & """AND SERIE = """ & general.NS(PAFActual.SERIE) & """ AND COLOR = """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("COLOR")) & """"
            cmdUpd.Connection = cnn
            cmdUpd.CommandText = SQL

            ACN()
            cmdUpd.ExecuteNonQuery()
            CCN()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub SumarstockTallas(ByVal fila As Integer)
        Dim cmdUpd As New MySqlCommand
        Dim SQL As String = "UPDATE MODSTK SET "
        Try
            If Not PAFActual.TALLA01 = "" Then

                SQL = SQL & " TALLA01 = TALLA01 + """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL1"), True) & ""","
            End If
            If Not PAFActual.TALLA02 = "" Then

                SQL = SQL & " TALLA02 = TALLA02 +  """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL2"), True) & ""","
            End If
            If Not PAFActual.TALLA01 = "" Then

                SQL = SQL & " TALLA03 = TALLA03 + """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL3"), True) & ""","
            End If
            If Not PAFActual.TALLA01 = "" Then

                SQL = SQL & " TALLA04 = TALLA04 + """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL4"), True) & ""","
            End If
            If Not PAFActual.TALLA01 = "" Then

                SQL = SQL & " TALLA05 = TALLA05 +  """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL5"), True) & ""","
            End If
            If Not PAFActual.TALLA02 = "" Then

                SQL = SQL & " TALLA06 = TALLA06 +  """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL6"), True) & ""","
            End If
            If Not PAFActual.TALLA03 = "" Then

                SQL = SQL & " TALLA07 = TALLA07 +  """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL7"), True) & ""","
            End If
            If Not PAFActual.TALLA01 = "" Then

                SQL = SQL & " TALLA08 = TALLA08 +  """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL8"), True) & ""","
            End If
            If Not PAFActual.TALLA01 = "" Then

                SQL = SQL & " TALLA09 = TALLA09 +  """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL9"), True) & ""","
            End If
            If Not PAFActual.TALLA01 = "" Then

                SQL = SQL & " TALLA10 = TALLA10 +  """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("TALL10"), True) & ""","
            End If
            SQL = SQL.Substring(0, SQL.Length - 1)
            '!!!!!!!!!!!! falta la temporada

            SQL = SQL & " WHERE MODEL = """ & general.NS(PAFActual.MODEL) & """ AND CLIENT = """ & PAFActual.CLIENT & """ AND SERIE = """ & general.NS(PAFActual.SERIE) & """ AND COLOR = """ & general.NS(PAFActual.lineasVenta.dvForm(fila).Item("COLOR")) & """"
            cmdUpd.Connection = cnn
            cmdUpd.CommandText = SQL

            ACN()
            cmdUpd.ExecuteNonQuery()
            CCN()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub SumarStock()
        '! Dead variable: j
        Dim i, j As Integer
        Try
            'Por cada talla(no nula) y color hay que restar sumar al stock
            For i = 0 To PAFActual.lineasVenta.dvForm.Count - 1
                SumarstockTallas(i)
            Next

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub RestarStock()
        '! Dead variable: j
        Dim i, j As Integer
        Try
            'Por cada talla(no nula) y color hay que restar sumar al stock
            For i = 0 To PAFActual.lineasVenta.dvForm.Count - 1
                RestartockTallas(i)
            Next

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

    Private Sub cboID_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboID.SelectedValueChanged
        Try
            If consulta() Then
                cargando = True
                PAFActual.CambiarAReg(nzn(cboID.WillChangeToValue, 0), iraregistro)
                MoverActual()
                PSBTIPO(PAFActual.centro) : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub

#Region "SELECCION REGISTRO"

    Private Sub CambioEleccionTodosPAF()
        Try
            PAFActual.ElegirCliente(-1)
            PAFActual.numeroRegistros = PAFActual.obtenerNumeroReg(PAFActual.tabla.TableName, "")
            PAFActual.tabla.AcceptChanges()

            AutoSizeCC(cboID)
            lblNumeroPAF.Text = rm.GetString("NUMALBARANES") & " " & PAFActual.dvIdentificadores.Count

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub VerPAFDeCliente_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoVerPAFDeCliente.CheckedChanged
        Try
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
                    CambioEleccionTodosPAF()
                    'PAFActual.IDCliente = -1
                    'PAFActual.numeroRegistros = PAFActual.obtenerNumeroReg(PAFActual.tabla.TableName, "")
                    ''PAFActual.UltimoReg() : ActualizarOrigen()
                    ''CBOOR.Text = PAFActual.FRA
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
                AñadirBindingCombo(cboID, PAFActual.dvIdentificadores, "FRA", "FRA")
                PAFActual.tabla.AcceptChanges()
                lblNumeroPAF.Text = rm.GetString("NUMALBARANES") & " " & PAFActual.dvIdentificadores.Count

                MoverActual()
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

    Private Sub dgAlbaranes_AfterDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgAlbaranes.AfterDelete
        Try
            PAFActual.lineasVenta.HacerCalculosLineasPAF("PREU")

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub dgAlbaranes_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgAlbaranes.BeforeDelete
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
#Region "COMUNES"

    Friend Sub btnRecargar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecargar.Click
        Try
            cargando = True
            Dim id As Object
            id = cboID.SelectedValue
            PAFActual.ActualizarOrigen()
            PAFActual.CambiarAReg(id, iraregistro)
            MoverActual()
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub


#End Region

End Class


'La función comprueba si el albarán al cual se va a traspasar es correcto, es decir,
'no ha sido traspasado a una factura. Si el albaran no existe también devuelve cierto
'!Private Function ObtenerTara(ByVal talla As String) As Double
'!         'Esta funcion devuelve las taras
'!         Dim i As Double = 0
'!         Dim tara As Double = 0
'!         Try
'!             For i = 0 To dvAnulaciones.Count - 1
'!                 If dvAnulaciones(i).Item("TALLA") = talla Then
'!                     tara = tara + dvAnulaciones(i).Item("UNIT")
'!                 End If
'!             Next
'! 
'!             Return tara
'! 
'!         Catch ex As Exception
'!             LOG(ex.ToString) : cargando = false : ccn()
'!         End Try
'!     End Function
