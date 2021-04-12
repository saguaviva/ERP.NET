Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Imports Access = Microsoft.Office.Interop.Access

Public Class frmDesgloseMuestraColorMaquina
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
    Friend WithEvents lblCodiMostra As Label
    Friend WithEvents lblCliente As Label

    Friend WithEvents lblBanc As Label
    Friend WithEvents lblVelocidad As Label
    Friend WithEvents lblAgujas As Label
    Friend WithEvents lblImporte As Label
    Friend WithEvents lblTalla As Label
    Friend WithEvents lblColor As Label
    Friend WithEvents lblDescripcionMuestra As Label

    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblDiskette As Label
    Friend WithEvents dgDesglose As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents cboHilos As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents cboColores As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents cboProveedores As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents btnAceptar As C1.Win.C1Input.C1Button
    Friend WithEvents lblDesglose As System.Windows.Forms.Label
    Friend WithEvents txtNOMMAQUI As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtMAQUINA As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtAGULLES As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtVELOSITAT As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtDISCO As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTEMPS As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtIMPORT As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtNOMCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCODI As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCOLOR As C1.Win.C1Input.C1TextBox
    Friend WithEvents dtpDATA As C1.Win.C1Input.C1DateEdit
    Friend WithEvents txtNOTAS As C1.Win.C1Input.C1TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage




    Friend WithEvents lblTiempoMaquina As System.Windows.Forms.Label
    Friend WithEvents txtCORTES As C1.Win.C1Input.C1TextBox
    Friend WithEvents lblCORTES As System.Windows.Forms.Label
    Friend WithEvents ppFitxa As C1.Win.C1Preview.C1PrintPreviewControl
    Friend WithEvents txtDESCRIP As C1.Win.C1Input.C1TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Style1 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style2 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style3 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style4 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style5 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDesgloseMuestraColorMaquina))
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
        Dim Style17 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style18 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style19 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style20 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style21 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style22 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style23 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style24 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Me.lblCodiMostra = New System.Windows.Forms.Label()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.lblDesglose = New System.Windows.Forms.Label()
        Me.lblBanc = New System.Windows.Forms.Label()
        Me.lblVelocidad = New System.Windows.Forms.Label()
        Me.lblAgujas = New System.Windows.Forms.Label()
        Me.lblImporte = New System.Windows.Forms.Label()
        Me.lblTalla = New System.Windows.Forms.Label()
        Me.lblColor = New System.Windows.Forms.Label()
        Me.lblDescripcionMuestra = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtCODI = New C1.Win.C1Input.C1TextBox()
        Me.txtNOMCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.txtCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.dtpDATA = New C1.Win.C1Input.C1DateEdit()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtCORTES = New C1.Win.C1Input.C1TextBox()
        Me.lblCORTES = New System.Windows.Forms.Label()
        Me.lblTiempoMaquina = New System.Windows.Forms.Label()
        Me.txtIMPORT = New C1.Win.C1Input.C1TextBox()
        Me.txtTEMPS = New C1.Win.C1Input.C1TextBox()
        Me.txtDISCO = New C1.Win.C1Input.C1TextBox()
        Me.txtVELOSITAT = New C1.Win.C1Input.C1TextBox()
        Me.txtAGULLES = New C1.Win.C1Input.C1TextBox()
        Me.txtMAQUINA = New C1.Win.C1Input.C1TextBox()
        Me.txtNOMMAQUI = New C1.Win.C1Input.C1TextBox()
        Me.lblDiskette = New System.Windows.Forms.Label()
        Me.cboHilos = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.dgDesglose = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.cboColores = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.cboProveedores = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.btnAceptar = New C1.Win.C1Input.C1Button()
        Me.txtDESCRIP = New C1.Win.C1Input.C1TextBox()
        Me.txtTALLA = New C1.Win.C1Input.C1TextBox()
        Me.txtCOLOR = New C1.Win.C1Input.C1TextBox()
        Me.txtNOTAS = New C1.Win.C1Input.C1TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.ppFitxa = New C1.Win.C1Preview.C1PrintPreviewControl()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtCODI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNOMCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDATA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtCORTES, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIMPORT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTEMPS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDISCO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVELOSITAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAGULLES, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMAQUINA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNOMMAQUI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboHilos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgDesglose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboColores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboProveedores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDESCRIP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCOLOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNOTAS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.ppFitxa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ppFitxa.SuspendLayout()
        Me.SuspendLayout()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(4, 201)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(60, 94)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(330, 94)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(28, 94)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(28, 74)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(330, 74)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(150, 74)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(374, 94)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(150, 94)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(248, 74)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(60, 74)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(374, 74)
        '
        'lblCodiMostra
        '
        Me.lblCodiMostra.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCodiMostra.Location = New System.Drawing.Point(16, 48)
        Me.lblCodiMostra.Name = "lblCodiMostra"
        Me.lblCodiMostra.Size = New System.Drawing.Size(70, 20)
        Me.lblCodiMostra.TabIndex = 1
        Me.lblCodiMostra.Text = "Codi Mostra"
        Me.lblCodiMostra.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCliente
        '
        Me.lblCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCliente.Location = New System.Drawing.Point(16, 20)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(70, 20)
        Me.lblCliente.TabIndex = 202
        Me.lblCliente.Text = "Client"
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDesglose
        '
        Me.lblDesglose.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDesglose.Location = New System.Drawing.Point(20, 124)
        Me.lblDesglose.Name = "lblDesglose"
        Me.lblDesglose.Size = New System.Drawing.Size(300, 16)
        Me.lblDesglose.TabIndex = 205
        Me.lblDesglose.Text = "Desglòs de FILS, per MOSTRA COLOR i TALLA"
        '
        'lblBanc
        '
        Me.lblBanc.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblBanc.Location = New System.Drawing.Point(16, 28)
        Me.lblBanc.Name = "lblBanc"
        Me.lblBanc.Size = New System.Drawing.Size(70, 20)
        Me.lblBanc.TabIndex = 209
        Me.lblBanc.Text = "Màquina"
        Me.lblBanc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblVelocidad
        '
        Me.lblVelocidad.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblVelocidad.Location = New System.Drawing.Point(16, 76)
        Me.lblVelocidad.Name = "lblVelocidad"
        Me.lblVelocidad.Size = New System.Drawing.Size(70, 20)
        Me.lblVelocidad.TabIndex = 216
        Me.lblVelocidad.Text = "Velocitat"
        Me.lblVelocidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAgujas
        '
        Me.lblAgujas.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAgujas.Location = New System.Drawing.Point(16, 52)
        Me.lblAgujas.Name = "lblAgujas"
        Me.lblAgujas.Size = New System.Drawing.Size(70, 20)
        Me.lblAgujas.TabIndex = 219
        Me.lblAgujas.Text = "Agulles"
        Me.lblAgujas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblImporte
        '
        Me.lblImporte.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblImporte.Location = New System.Drawing.Point(452, 100)
        Me.lblImporte.Name = "lblImporte"
        Me.lblImporte.Size = New System.Drawing.Size(100, 20)
        Me.lblImporte.TabIndex = 221
        Me.lblImporte.Text = "Import"
        Me.lblImporte.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTalla
        '
        Me.lblTalla.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTalla.Location = New System.Drawing.Point(140, 144)
        Me.lblTalla.Name = "lblTalla"
        Me.lblTalla.Size = New System.Drawing.Size(100, 15)
        Me.lblTalla.TabIndex = 224
        Me.lblTalla.Text = "Talla"
        '
        'lblColor
        '
        Me.lblColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblColor.Location = New System.Drawing.Point(20, 144)
        Me.lblColor.Name = "lblColor"
        Me.lblColor.Size = New System.Drawing.Size(100, 15)
        Me.lblColor.TabIndex = 227
        Me.lblColor.Text = "Color"
        '
        'lblDescripcionMuestra
        '
        Me.lblDescripcionMuestra.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDescripcionMuestra.Location = New System.Drawing.Point(264, 144)
        Me.lblDescripcionMuestra.Name = "lblDescripcionMuestra"
        Me.lblDescripcionMuestra.Size = New System.Drawing.Size(68, 15)
        Me.lblDescripcionMuestra.TabIndex = 231
        Me.lblDescripcionMuestra.Text = "Descripció"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtCODI)
        Me.GroupBox1.Controls.Add(Me.txtNOMCLIENT)
        Me.GroupBox1.Controls.Add(Me.txtCLIENT)
        Me.GroupBox1.Controls.Add(Me.lblCodiMostra)
        Me.GroupBox1.Controls.Add(Me.lblCliente)
        Me.GroupBox1.Controls.Add(Me.dtpDATA)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(456, 78)
        Me.GroupBox1.TabIndex = 236
        Me.GroupBox1.TabStop = False
        '
        'txtCODI
        '
        Me.txtCODI.Location = New System.Drawing.Point(92, 48)
        Me.txtCODI.Name = "txtCODI"
        Me.txtCODI.Size = New System.Drawing.Size(72, 20)
        Me.txtCODI.TabIndex = 235
        Me.txtCODI.Tag = Nothing
        '
        'txtNOMCLIENT
        '
        Me.txtNOMCLIENT.Location = New System.Drawing.Point(160, 20)
        Me.txtNOMCLIENT.Name = "txtNOMCLIENT"
        Me.txtNOMCLIENT.Size = New System.Drawing.Size(284, 20)
        Me.txtNOMCLIENT.TabIndex = 234
        Me.txtNOMCLIENT.Tag = Nothing
        '
        'txtCLIENT
        '
        Me.txtCLIENT.Location = New System.Drawing.Point(92, 20)
        Me.txtCLIENT.Name = "txtCLIENT"
        Me.txtCLIENT.Size = New System.Drawing.Size(60, 20)
        Me.txtCLIENT.TabIndex = 233
        Me.txtCLIENT.Tag = Nothing
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
        Me.dtpDATA.Location = New System.Drawing.Point(168, 48)
        Me.dtpDATA.Name = "dtpDATA"
        Me.dtpDATA.Size = New System.Drawing.Size(148, 20)
        Me.dtpDATA.TabIndex = 277
        Me.dtpDATA.Tag = Nothing
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.GroupBox2.Controls.Add(Me.txtCORTES)
        Me.GroupBox2.Controls.Add(Me.lblCORTES)
        Me.GroupBox2.Controls.Add(Me.lblTiempoMaquina)
        Me.GroupBox2.Controls.Add(Me.txtIMPORT)
        Me.GroupBox2.Controls.Add(Me.txtTEMPS)
        Me.GroupBox2.Controls.Add(Me.txtDISCO)
        Me.GroupBox2.Controls.Add(Me.txtVELOSITAT)
        Me.GroupBox2.Controls.Add(Me.txtAGULLES)
        Me.GroupBox2.Controls.Add(Me.txtMAQUINA)
        Me.GroupBox2.Controls.Add(Me.txtNOMMAQUI)
        Me.GroupBox2.Controls.Add(Me.lblDiskette)
        Me.GroupBox2.Controls.Add(Me.lblBanc)
        Me.GroupBox2.Controls.Add(Me.lblVelocidad)
        Me.GroupBox2.Controls.Add(Me.lblAgujas)
        Me.GroupBox2.Controls.Add(Me.lblImporte)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 424)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(668, 137)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Maquina"
        '
        'txtCORTES
        '
        Me.txtCORTES.Location = New System.Drawing.Point(288, 52)
        Me.txtCORTES.Name = "txtCORTES"
        Me.txtCORTES.Size = New System.Drawing.Size(100, 20)
        Me.txtCORTES.TabIndex = 235
        Me.txtCORTES.Tag = Nothing
        '
        'lblCORTES
        '
        Me.lblCORTES.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCORTES.Location = New System.Drawing.Point(208, 52)
        Me.lblCORTES.Name = "lblCORTES"
        Me.lblCORTES.Size = New System.Drawing.Size(70, 20)
        Me.lblCORTES.TabIndex = 234
        Me.lblCORTES.Text = "Cortes"
        Me.lblCORTES.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTiempoMaquina
        '
        Me.lblTiempoMaquina.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTiempoMaquina.Location = New System.Drawing.Point(452, 76)
        Me.lblTiempoMaquina.Name = "lblTiempoMaquina"
        Me.lblTiempoMaquina.Size = New System.Drawing.Size(100, 20)
        Me.lblTiempoMaquina.TabIndex = 233
        Me.lblTiempoMaquina.Text = "Temps màquina"
        Me.lblTiempoMaquina.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtIMPORT
        '
        Me.txtIMPORT.Location = New System.Drawing.Point(556, 100)
        Me.txtIMPORT.Name = "txtIMPORT"
        Me.txtIMPORT.Size = New System.Drawing.Size(68, 20)
        Me.txtIMPORT.TabIndex = 232
        Me.txtIMPORT.Tag = Nothing
        '
        'txtTEMPS
        '
        Me.txtTEMPS.Location = New System.Drawing.Point(556, 76)
        Me.txtTEMPS.Name = "txtTEMPS"
        Me.txtTEMPS.Size = New System.Drawing.Size(68, 20)
        Me.txtTEMPS.TabIndex = 231
        Me.txtTEMPS.Tag = Nothing
        '
        'txtDISCO
        '
        Me.txtDISCO.Location = New System.Drawing.Point(96, 100)
        Me.txtDISCO.Name = "txtDISCO"
        Me.txtDISCO.Size = New System.Drawing.Size(100, 20)
        Me.txtDISCO.TabIndex = 230
        Me.txtDISCO.Tag = Nothing
        '
        'txtVELOSITAT
        '
        Me.txtVELOSITAT.Location = New System.Drawing.Point(96, 76)
        Me.txtVELOSITAT.Name = "txtVELOSITAT"
        Me.txtVELOSITAT.Size = New System.Drawing.Size(100, 20)
        Me.txtVELOSITAT.TabIndex = 229
        Me.txtVELOSITAT.Tag = Nothing
        '
        'txtAGULLES
        '
        Me.txtAGULLES.Location = New System.Drawing.Point(96, 52)
        Me.txtAGULLES.Name = "txtAGULLES"
        Me.txtAGULLES.Size = New System.Drawing.Size(100, 20)
        Me.txtAGULLES.TabIndex = 228
        Me.txtAGULLES.Tag = Nothing
        '
        'txtMAQUINA
        '
        Me.txtMAQUINA.Location = New System.Drawing.Point(96, 28)
        Me.txtMAQUINA.Name = "txtMAQUINA"
        Me.txtMAQUINA.Size = New System.Drawing.Size(40, 20)
        Me.txtMAQUINA.TabIndex = 227
        Me.txtMAQUINA.Tag = Nothing
        '
        'txtNOMMAQUI
        '
        Me.txtNOMMAQUI.Location = New System.Drawing.Point(140, 28)
        Me.txtNOMMAQUI.Name = "txtNOMMAQUI"
        Me.txtNOMMAQUI.Size = New System.Drawing.Size(304, 20)
        Me.txtNOMMAQUI.TabIndex = 226
        Me.txtNOMMAQUI.Tag = Nothing
        '
        'lblDiskette
        '
        Me.lblDiskette.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDiskette.Location = New System.Drawing.Point(16, 100)
        Me.lblDiskette.Name = "lblDiskette"
        Me.lblDiskette.Size = New System.Drawing.Size(70, 20)
        Me.lblDiskette.TabIndex = 225
        Me.lblDiskette.Text = "DISKETTE"
        Me.lblDiskette.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboHilos
        '
        Me.cboHilos.AllowColMove = True
        Me.cboHilos.AllowColSelect = True
        Me.cboHilos.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboHilos.AlternatingRows = True
        Me.cboHilos.CaptionStyle = Style1
        Me.cboHilos.ColumnCaptionHeight = 17
        Me.cboHilos.ColumnFooterHeight = 17
        Me.cboHilos.EvenRowStyle = Style2
        Me.cboHilos.FetchRowStyles = False
        Me.cboHilos.FooterStyle = Style3
        Me.cboHilos.HeadingStyle = Style4
        Me.cboHilos.HighLightRowStyle = Style5
        Me.cboHilos.Images.Add(CType(resources.GetObject("cboHilos.Images"), System.Drawing.Image))
        Me.cboHilos.Location = New System.Drawing.Point(40, 192)
        Me.cboHilos.Name = "cboHilos"
        Me.cboHilos.OddRowStyle = Style6
        Me.cboHilos.RecordSelectorStyle = Style7
        Me.cboHilos.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboHilos.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboHilos.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboHilos.ScrollTips = False
        Me.cboHilos.Size = New System.Drawing.Size(280, 144)
        Me.cboHilos.Style = Style8
        Me.cboHilos.TabIndex = 267
        Me.cboHilos.TabStop = False
        Me.cboHilos.Text = "C1TrueDBDropdown1"
        Me.cboHilos.ValueTranslate = True
        Me.cboHilos.Visible = False
        Me.cboHilos.PropBag = resources.GetString("cboHilos.PropBag")
        '
        'dgDesglose
        '
        Me.dgDesglose.AllowAddNew = True
        Me.dgDesglose.AllowDelete = True
        Me.dgDesglose.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgDesglose.AllowSort = False
        Me.dgDesglose.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgDesglose.CaptionHeight = 17
        Me.dgDesglose.ExtendRightColumn = True
        Me.dgDesglose.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.dgDesglose.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgDesglose.Images.Add(CType(resources.GetObject("dgDesglose.Images"), System.Drawing.Image))
        Me.dgDesglose.Location = New System.Drawing.Point(0, 184)
        Me.dgDesglose.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRowRaiseCell
        Me.dgDesglose.Name = "dgDesglose"
        Me.dgDesglose.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgDesglose.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgDesglose.PreviewInfo.ZoomFactor = 75.0R
        Me.dgDesglose.PrintInfo.PageSettings = CType(resources.GetObject("dgDesglose.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgDesglose.RowHeight = 15
        Me.dgDesglose.Size = New System.Drawing.Size(968, 236)
        Me.dgDesglose.SpringMode = True
        Me.dgDesglose.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgDesglose.TabIndex = 266
        Me.dgDesglose.Text = "C1TrueDBGrid1"
        Me.dgDesglose.WrapCellPointer = True
        Me.dgDesglose.PropBag = resources.GetString("dgDesglose.PropBag")
        '
        'cboColores
        '
        Me.cboColores.AllowColMove = True
        Me.cboColores.AllowColSelect = True
        Me.cboColores.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboColores.AlternatingRows = True
        Me.cboColores.CaptionStyle = Style9
        Me.cboColores.ColumnCaptionHeight = 17
        Me.cboColores.ColumnFooterHeight = 17
        Me.cboColores.EvenRowStyle = Style10
        Me.cboColores.FetchRowStyles = False
        Me.cboColores.FooterStyle = Style11
        Me.cboColores.HeadingStyle = Style12
        Me.cboColores.HighLightRowStyle = Style13
        Me.cboColores.Images.Add(CType(resources.GetObject("cboColores.Images"), System.Drawing.Image))
        Me.cboColores.Location = New System.Drawing.Point(368, 212)
        Me.cboColores.Name = "cboColores"
        Me.cboColores.OddRowStyle = Style14
        Me.cboColores.RecordSelectorStyle = Style15
        Me.cboColores.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboColores.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboColores.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboColores.ScrollTips = False
        Me.cboColores.Size = New System.Drawing.Size(280, 144)
        Me.cboColores.Style = Style16
        Me.cboColores.TabIndex = 269
        Me.cboColores.TabStop = False
        Me.cboColores.Text = "C1TrueDBDropdown1"
        Me.cboColores.ValueTranslate = True
        Me.cboColores.Visible = False
        Me.cboColores.PropBag = resources.GetString("cboColores.PropBag")
        '
        'cboProveedores
        '
        Me.cboProveedores.AllowColMove = True
        Me.cboProveedores.AllowColSelect = True
        Me.cboProveedores.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboProveedores.AlternatingRows = True
        Me.cboProveedores.CaptionStyle = Style17
        Me.cboProveedores.ColumnCaptionHeight = 17
        Me.cboProveedores.ColumnFooterHeight = 17
        Me.cboProveedores.EvenRowStyle = Style18
        Me.cboProveedores.FetchRowStyles = False
        Me.cboProveedores.FooterStyle = Style19
        Me.cboProveedores.HeadingStyle = Style20
        Me.cboProveedores.HighLightRowStyle = Style21
        Me.cboProveedores.Images.Add(CType(resources.GetObject("cboProveedores.Images"), System.Drawing.Image))
        Me.cboProveedores.Location = New System.Drawing.Point(664, 216)
        Me.cboProveedores.Name = "cboProveedores"
        Me.cboProveedores.OddRowStyle = Style22
        Me.cboProveedores.RecordSelectorStyle = Style23
        Me.cboProveedores.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboProveedores.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboProveedores.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboProveedores.ScrollTips = False
        Me.cboProveedores.Size = New System.Drawing.Size(280, 144)
        Me.cboProveedores.Style = Style24
        Me.cboProveedores.TabIndex = 270
        Me.cboProveedores.TabStop = False
        Me.cboProveedores.Text = "C1TrueDBDropdown1"
        Me.cboProveedores.ValueTranslate = True
        Me.cboProveedores.Visible = False
        Me.cboProveedores.PropBag = resources.GetString("cboProveedores.PropBag")
        '
        'btnAceptar
        '
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAceptar.Location = New System.Drawing.Point(888, 500)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 20)
        Me.btnAceptar.TabIndex = 271
        Me.btnAceptar.Text = "Acceptar"
        '
        'txtDESCRIP
        '
        Me.txtDESCRIP.Location = New System.Drawing.Point(260, 160)
        Me.txtDESCRIP.Name = "txtDESCRIP"
        Me.txtDESCRIP.Size = New System.Drawing.Size(320, 20)
        Me.txtDESCRIP.TabIndex = 274
        Me.txtDESCRIP.Tag = Nothing
        '
        'txtTALLA
        '
        Me.txtTALLA.Location = New System.Drawing.Point(136, 160)
        Me.txtTALLA.Name = "txtTALLA"
        Me.txtTALLA.Size = New System.Drawing.Size(100, 20)
        Me.txtTALLA.TabIndex = 275
        Me.txtTALLA.Tag = Nothing
        '
        'txtCOLOR
        '
        Me.txtCOLOR.Location = New System.Drawing.Point(20, 160)
        Me.txtCOLOR.Name = "txtCOLOR"
        Me.txtCOLOR.Size = New System.Drawing.Size(100, 20)
        Me.txtCOLOR.TabIndex = 276
        Me.txtCOLOR.Tag = Nothing
        '
        'txtNOTAS
        '
        Me.txtNOTAS.Location = New System.Drawing.Point(472, 4)
        Me.txtNOTAS.Multiline = True
        Me.txtNOTAS.Name = "txtNOTAS"
        Me.txtNOTAS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNOTAS.Size = New System.Drawing.Size(488, 136)
        Me.txtNOTAS.TabIndex = 278
        Me.txtNOTAS.Tag = Nothing
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.ItemSize = New System.Drawing.Size(76, 18)
        Me.TabControl1.Location = New System.Drawing.Point(4, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(984, 588)
        Me.TabControl1.TabIndex = 279
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.dgDesglose)
        Me.TabPage1.Controls.Add(Me.txtNOTAS)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.cboHilos)
        Me.TabPage1.Controls.Add(Me.lblDesglose)
        Me.TabPage1.Controls.Add(Me.txtDESCRIP)
        Me.TabPage1.Controls.Add(Me.txtTALLA)
        Me.TabPage1.Controls.Add(Me.txtCOLOR)
        Me.TabPage1.Controls.Add(Me.lblColor)
        Me.TabPage1.Controls.Add(Me.cboProveedores)
        Me.TabPage1.Controls.Add(Me.lblDescripcionMuestra)
        Me.TabPage1.Controls.Add(Me.lblTalla)
        Me.TabPage1.Controls.Add(Me.cboColores)
        Me.TabPage1.Controls.Add(Me.btnAceptar)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(976, 562)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Fitxa Tècnica"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ppFitxa)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(976, 562)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Impresio"
        '
        'ppFitxa
        '
        Me.ppFitxa.Location = New System.Drawing.Point(20, 68)
        Me.ppFitxa.Name = "ppFitxa"
        '
        'ppFitxa.OutlineView
        '
        Me.ppFitxa.PreviewOutlineView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ppFitxa.PreviewOutlineView.Location = New System.Drawing.Point(0, 0)
        Me.ppFitxa.PreviewOutlineView.Name = "OutlineView"
        Me.ppFitxa.PreviewOutlineView.Size = New System.Drawing.Size(165, 427)
        Me.ppFitxa.PreviewOutlineView.TabIndex = 0
        '
        'ppFitxa.PreviewPane
        '
        Me.ppFitxa.PreviewPane.IntegrateExternalTools = True
        Me.ppFitxa.PreviewPane.TabIndex = 0
        '
        'ppFitxa.PreviewTextSearchPanel
        '
        Me.ppFitxa.PreviewTextSearchPanel.Dock = System.Windows.Forms.DockStyle.Right
        Me.ppFitxa.PreviewTextSearchPanel.Location = New System.Drawing.Point(530, 0)
        Me.ppFitxa.PreviewTextSearchPanel.MinimumSize = New System.Drawing.Size(200, 240)
        Me.ppFitxa.PreviewTextSearchPanel.Name = "PreviewTextSearchPanel"
        Me.ppFitxa.PreviewTextSearchPanel.Size = New System.Drawing.Size(200, 453)
        Me.ppFitxa.PreviewTextSearchPanel.TabIndex = 0
        Me.ppFitxa.PreviewTextSearchPanel.Visible = False
        '
        'ppFitxa.ThumbnailView
        '
        Me.ppFitxa.PreviewThumbnailView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ppFitxa.PreviewThumbnailView.Location = New System.Drawing.Point(0, 0)
        Me.ppFitxa.PreviewThumbnailView.Name = "ThumbnailView"
        Me.ppFitxa.PreviewThumbnailView.Size = New System.Drawing.Size(165, 403)
        Me.ppFitxa.PreviewThumbnailView.TabIndex = 0
        Me.ppFitxa.PreviewThumbnailView.UseImageAsThumbnail = False
        Me.ppFitxa.Size = New System.Drawing.Size(952, 476)
        Me.ppFitxa.TabIndex = 0
        Me.ppFitxa.Text = "C1PrintPreviewControl1"
        '
        'frmDesgloseMuestraColorMaquina
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(992, 596)
        Me.Controls.Add(Me.TabControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.KeyPreview = True
        Me.Name = "frmDesgloseMuestraColorMaquina"
        Me.Text = "Fitxa técnica mostres"
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
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.txtCODI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNOMCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDATA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.txtCORTES, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIMPORT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTEMPS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDISCO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVELOSITAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAGULLES, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMAQUINA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNOMMAQUI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboHilos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgDesglose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboColores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboProveedores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDESCRIP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCOLOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNOTAS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.ppFitxa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ppFitxa.ResumeLayout(False)
        Me.ppFitxa.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "VARIABLES"

    Public desglose As clsDesgloseMuestra
    Public maquina As clsMaquinasMuestra
    Public muestra As clsMuestra

    Private bindingManagerLineas As BindingManagerBase

#End Region

#Region "INICIALIZACION"

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            editando = True
            Me.arrayDateTime = New System.Windows.Forms.Control() {Me.dtpDATA}
            desglose = New clsDesgloseMuestra(ds.Tables("color"), muestra.centro, Me.BindingContext, Me.muestra, maquina)
            maquina = New clsMaquinasMuestra(ds.Tables(tablaMaq), muestra.centro, Me.BindingContext, Me.muestra, Me.desglose)
            desglose.m_Maquina = maquina

            'Cargamos los hilos que seleccionaremos
            HacerBindings()
            InicializarDGDesglose()
            PonerHandlersErroresParaGrids()
            dgDesglose.Splits(0).DisplayColumns("CONSUM").DataColumn.FooterText = sumaTotal("CONSUM", desglose.dvForm)
            bindingManagerLineas = BindingContext(dgDesglose.DataSource)
            txtCLIENT.ReadOnly = True
            txtCODI.ReadOnly = True
            txtCOLOR.ReadOnly = True
            txtDESCRIP.ReadOnly = True
            txtTALLA.ReadOnly = True
            txtNOMCLIENT.ReadOnly = True
            txtMAQUINA.ReadOnly = True
            txtNOMMAQUI.ReadOnly = True

        Catch ex As Exception
            LOG(ex.ToString) : cargando = false : ccn()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub HacerBindings()
        Try
            HacerBindingsTodos(muestra.dvForm)
            txtDESCRIP.Value = muestra.cartaColores.DESCRI
            txtCOLOR.Value = muestra.cartaColores.COLOR
            txtTALLA.Value = muestra.cartaColores.TALLA

            'HacerBindingsTodos(muestra.cartaColores.dvForm)
            HacerBindingsTodos(maquina.dvForm)

            dgDesglose.SetDataBinding(desglose.dvForm, "")

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ORGANIZAR"
           
    Private Sub txtTiempoMaquina_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTEMPS.Validated
        Try
            If PuedoModificar() Then : cargando = True : desglose.ValidarTiempoMaq() : cargando = False : End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTancar.Click
        Try
            Close()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub dgDesglose_ButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgDesglose.ButtonClick
        If Not cargando And editando Then
            If e.Column.DataColumn.DataField = "ELEGIRHILO" Then

                cargando = True
                Cursor = Cursors.WaitCursor
                Dim f As frmHilosLista = frmHilosLista.GetInstance(esEleccion, desglose.centro)
                f.ShowDialog()
                Cursor = Cursors.Default
                dgDesglose.EditActive = True
                If Not f.dr Is Nothing Then
                    desglose.TEIXIT = general.nz(f.dr("CODI"), "")
                    desglose.PROVE = general.nz(f.dr("PROVE"), 0)
                    desglose.PREU = nzn(f.dr("PREU"), 0)
                    desglose.CargarColores()
                    AutosizeColumnasTrueDBDropdown(cboColores)
                    AutosizeColumnasTrueDBDropdown(cboProveedores)
                End If
                f = Nothing
                cargando = False

            End If
        End If
    End Sub
    Private Sub dgDesglose_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgDesglose.AfterColUpdate
        Dim columna As String = e.Column.DataColumn.DataField
        Try
            If PuedoModificar() Then
                cargando = True
                dgDesglose.UpdateData()
                Select Case columna
                    Case "TEIXIT"
                        desglose.ponerProveedores()
                        desglose.PROVE = nzn(cboProveedores.Columns(tablaProveedores).Value, 0)
                        desglose.CargarColores()
                        AutosizeColumnasTrueDBDropdown(cboColores)
                        AutosizeColumnasTrueDBDropdown(cboProveedores)

                    Case "COLOR"
                        desglose.COLOR = general.nz(cboColores.Columns("COLOR").Value, "")
                        desglose.PREU = nzn(cboColores.Columns("COST").Value, 0)
                End Select
                cargando = False
                AutoSizeCC(dgDesglose)
                dgDesglose.Splits(0).DisplayColumns("CONSUM").DataColumn.FooterText = sumaTotal("CONSUM", desglose.dvForm)
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            desglose.ActualizarOrigen()
            maquina.ActualizarOrigen()
            Me.Close()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "INICIALIZACION"

    Private Sub InicializarDGDesglose()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgDesglose)

            i = 1
            PPCol2("TEIXIT", dgDesglose, rm.GetString("HILO") & " ", "", True, 125, False, C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox, False, 125, i, False, cboHilos)
            AñadirBindingCombo(cboHilos, desglose.dtTeixits.DefaultView, CCFil, CCFil)

            i = i + 1
            PPCol2("ELEGIRHILO", dgDesglose, "", "", True, 20, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, True, 20, i, False)

            'i = i + 1
            'PPCol2(tablaProveedores, dgDesglose, rm.GetString("PROVEEDOR"), "", True, 60, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 60, 0, False)

            i = i + 1
            PPCol2("NOMPROVE", dgDesglose, rm.GetString("NOMPROVE"), "", True, 250, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 250, i, True) ', cboProveedores)
            AñadirBindingCombo(cboProveedores, desglose.dvProveedores, "NOMPROVE", "NOMPROVE")

            i = i + 1
            PPCol2("COLOR", dgDesglose, rm.GetString("COLOR"), "", True, 125, False, C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox, False, 125, i, False, cboColores)
            AñadirBindingCombo(cboColores, desglose.dtColores, "COLOR", "COLOR")

            i = i + 1
            PPCol2("FIL", dgDesglose, "GH", "#,##0.00", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, False)

            i = i + 1
            PPCol2("CAPS", dgDesglose, rm.GetString("CAPS"), "#,##0.00", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, False)

            i = i + 1
            PPCol2("PASSADES", dgDesglose, rm.GetString("PASSADES"), "#,##0.00", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, False)

            i = i + 1
            PPCol2("GRADUACION", dgDesglose, rm.GetString("GRADUACION"), "#,##0.00", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, False)

            i = i + 1
            PPCol2("CONSUM", dgDesglose, rm.GetString("CONSUM"), "#0.000", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, False)

            i = i + 1
            PPCol2("PREU", dgDesglose, rm.GetString("PRECIO"), "#,##0.00", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, False)

            i = i + 1
            PPCol2("IMPORT", dgDesglose, rm.GetString("IMPORTE"), "#,##0.00", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, False)

            dgDesglose.Splits(0).DisplayColumns("CONSUM").FetchStyle = True
            dgDesglose.ColumnFooters = True


        Catch ex As Exception
            LOG(ex.ToString) : cargando = false : ccn()
        End Try

    End Sub

#End Region

#Region "MODIFICAR BD"

    Friend Overrides Sub btnActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Friend Overrides Sub btnBorrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Friend Overrides Sub btnModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Friend Overrides Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

#End Region

#Region "IMPRESION"
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab Is TabPage2 Then
            ppFitxa.Document = desglose.ImprimirFichaTecnica()
        End If
    End Sub
#End Region

#Region "ELECCIONES"

#End Region

#Region "DESPLAZAMIENTO"

    Friend Overrides Sub btnAnterior_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

#End Region

    Private Sub dgDesglose_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgDesglose.BeforeDelete
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

End Class
