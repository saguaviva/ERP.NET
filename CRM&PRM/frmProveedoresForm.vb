Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class frmProveedoresForm
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
    Friend WithEvents tpProveedor As System.Windows.Forms.TabPage
    Friend WithEvents comboIVA As C1.Win.C1List.C1Combo
    Friend WithEvents btnElegirProveedor As C1.Win.C1Input.C1Button
    Friend WithEvents btnRecarrgar As C1.Win.C1Input.C1Button
    Friend WithEvents txtNIF As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCP As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtFAX As C1.Win.C1Input.C1TextBox
    Friend WithEvents lblIvaHilo As Label
    Friend WithEvents lblNotes As Label
    Friend WithEvents lblCuentaContable As Label
    Friend WithEvents btnElegirFormaPago As C1.Win.C1Input.C1Button
    Friend WithEvents lblFormaDePago As Label
    Friend WithEvents lblOficina As Label
    Friend WithEvents lblNumCompte As Label
    Friend WithEvents lblDiasPago As Label
    Friend WithEvents lblContacte As Label
    Friend WithEvents lblTelefonos As Label
    Friend WithEvents lblBanc As Label
    Friend WithEvents lblNIF As Label
    Friend WithEvents lblProvincia As Label
    Friend WithEvents lblCP As Label
    Friend WithEvents lblDomicilio As Label
    Friend WithEvents lblNombre As Label
    Friend WithEvents lblCodigoProveedor As Label
    Friend WithEvents chkTraspaso As System.Windows.Forms.CheckBox
    Friend WithEvents lblFax As Label
    Friend WithEvents btnElegirBanco As C1.Win.C1Input.C1Button
    Friend WithEvents lblPoblacion As Label
    Friend WithEvents lblPais As Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cboID As C1.Win.C1List.C1Combo
    Friend WithEvents lblEmail1 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTEL2 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtEMAIL2 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtDOM As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtEMAIL1 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtPAIS As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTEL As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtPROV As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtPOB As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtBANC As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtFORMA As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCONTACTE As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtSUBCTA As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtDIA2 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtDIA1 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtDIA3 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtDC As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCTA As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCOFI As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboNOMBANC As C1.Win.C1List.C1Combo
    Friend WithEvents cboNOMFORMA As C1.Win.C1List.C1Combo
    Friend WithEvents txtNOTES As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCODI As C1.Win.C1Input.C1TextBox

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProveedoresForm))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tpProveedor = New System.Windows.Forms.TabPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtTEL2 = New C1.Win.C1Input.C1TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtEMAIL2 = New C1.Win.C1Input.C1TextBox
        Me.lblEmail1 = New System.Windows.Forms.Label
        Me.txtEMAIL1 = New C1.Win.C1Input.C1TextBox
        Me.cboID = New C1.Win.C1List.C1Combo
        Me.txtCODI = New C1.Win.C1Input.C1TextBox
        Me.lblPais = New System.Windows.Forms.Label
        Me.lblTelefonos = New System.Windows.Forms.Label
        Me.lblContacte = New System.Windows.Forms.Label
        Me.btnElegirProveedor = New C1.Win.C1Input.C1Button
        Me.lblNIF = New System.Windows.Forms.Label
        Me.lblProvincia = New System.Windows.Forms.Label
        Me.lblCP = New System.Windows.Forms.Label
        Me.lblDomicilio = New System.Windows.Forms.Label
        Me.lblCodigoProveedor = New System.Windows.Forms.Label
        Me.lblNombre = New System.Windows.Forms.Label
        Me.txtPAIS = New C1.Win.C1Input.C1TextBox
        Me.lblFax = New System.Windows.Forms.Label
        Me.txtCONTACTE = New C1.Win.C1Input.C1TextBox
        Me.lblPoblacion = New System.Windows.Forms.Label
        Me.txtNIF = New C1.Win.C1Input.C1TextBox
        Me.txtTEL = New C1.Win.C1Input.C1TextBox
        Me.txtPROV = New C1.Win.C1Input.C1TextBox
        Me.txtCP = New C1.Win.C1Input.C1TextBox
        Me.txtDOM = New C1.Win.C1Input.C1TextBox
        Me.txtFAX = New C1.Win.C1Input.C1TextBox
        Me.txtPOB = New C1.Win.C1Input.C1TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblCuentaContable = New System.Windows.Forms.Label
        Me.btnElegirFormaPago = New C1.Win.C1Input.C1Button
        Me.lblFormaDePago = New System.Windows.Forms.Label
        Me.lblDiasPago = New System.Windows.Forms.Label
        Me.txtDIA2 = New C1.Win.C1Input.C1TextBox
        Me.txtSUBCTA = New C1.Win.C1Input.C1TextBox
        Me.txtDIA1 = New C1.Win.C1Input.C1TextBox
        Me.txtFORMA = New C1.Win.C1Input.C1TextBox
        Me.txtDIA3 = New C1.Win.C1Input.C1TextBox
        Me.cboNOMFORMA = New C1.Win.C1List.C1Combo
        Me.cboNOMBANC = New C1.Win.C1List.C1Combo
        Me.txtBANC = New C1.Win.C1Input.C1TextBox
        Me.btnElegirBanco = New C1.Win.C1Input.C1Button
        Me.lblOficina = New System.Windows.Forms.Label
        Me.lblNumCompte = New System.Windows.Forms.Label
        Me.lblBanc = New System.Windows.Forms.Label
        Me.txtDC = New C1.Win.C1Input.C1TextBox
        Me.txtCOFI = New C1.Win.C1Input.C1TextBox
        Me.txtCTA = New C1.Win.C1Input.C1TextBox
        Me.comboIVA = New C1.Win.C1List.C1Combo
        Me.txtNOTES = New C1.Win.C1Input.C1TextBox
        Me.lblIvaHilo = New System.Windows.Forms.Label
        Me.lblNotes = New System.Windows.Forms.Label
        Me.chkTraspaso = New System.Windows.Forms.CheckBox
        Me.TabControl1.SuspendLayout()
        Me.tpProveedor.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtTEL2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEMAIL2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEMAIL1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCODI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPAIS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCONTACTE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNIF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTEL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPROV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFAX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPOB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtDIA2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSUBCTA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDIA1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFORMA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDIA3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMFORMA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMBANC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBANC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCOFI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCTA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNOTES, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(5, 144)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        '
        'btnVerLista
        '
        '
        'cboSeleccionCentro
        '
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tpProveedor)
        Me.TabControl1.ItemSize = New System.Drawing.Size(58, 18)
        Me.TabControl1.Location = New System.Drawing.Point(8, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(970, 519)
        Me.TabControl1.TabIndex = 0
        '
        'tpProveedor
        '
        Me.tpProveedor.Controls.Add(Me.GroupBox2)
        Me.tpProveedor.Controls.Add(Me.GroupBox1)
        Me.tpProveedor.Controls.Add(Me.comboIVA)
        Me.tpProveedor.Controls.Add(Me.txtNOTES)
        Me.tpProveedor.Controls.Add(Me.lblIvaHilo)
        Me.tpProveedor.Controls.Add(Me.lblNotes)
        Me.tpProveedor.Controls.Add(Me.chkTraspaso)
        Me.tpProveedor.Location = New System.Drawing.Point(4, 22)
        Me.tpProveedor.Name = "tpProveedor"
        Me.tpProveedor.Size = New System.Drawing.Size(962, 493)
        Me.tpProveedor.TabIndex = 0
        Me.tpProveedor.Text = "Proveïdor"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtTEL2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtEMAIL2)
        Me.GroupBox2.Controls.Add(Me.lblEmail1)
        Me.GroupBox2.Controls.Add(Me.txtEMAIL1)
        Me.GroupBox2.Controls.Add(Me.cboID)
        Me.GroupBox2.Controls.Add(Me.txtCODI)
        Me.GroupBox2.Controls.Add(Me.lblPais)
        Me.GroupBox2.Controls.Add(Me.lblTelefonos)
        Me.GroupBox2.Controls.Add(Me.lblContacte)
        Me.GroupBox2.Controls.Add(Me.btnElegirProveedor)
        Me.GroupBox2.Controls.Add(Me.lblNIF)
        Me.GroupBox2.Controls.Add(Me.lblProvincia)
        Me.GroupBox2.Controls.Add(Me.lblCP)
        Me.GroupBox2.Controls.Add(Me.lblDomicilio)
        Me.GroupBox2.Controls.Add(Me.lblCodigoProveedor)
        Me.GroupBox2.Controls.Add(Me.lblNombre)
        Me.GroupBox2.Controls.Add(Me.txtPAIS)
        Me.GroupBox2.Controls.Add(Me.lblFax)
        Me.GroupBox2.Controls.Add(Me.txtCONTACTE)
        Me.GroupBox2.Controls.Add(Me.lblPoblacion)
        Me.GroupBox2.Controls.Add(Me.txtNIF)
        Me.GroupBox2.Controls.Add(Me.txtTEL)
        Me.GroupBox2.Controls.Add(Me.txtPROV)
        Me.GroupBox2.Controls.Add(Me.txtCP)
        Me.GroupBox2.Controls.Add(Me.txtDOM)
        Me.GroupBox2.Controls.Add(Me.txtFAX)
        Me.GroupBox2.Controls.Add(Me.txtPOB)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox2.Location = New System.Drawing.Point(8, 20)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(460, 288)
        Me.GroupBox2.TabIndex = 384
        Me.GroupBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(4, 208)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 324
        Me.Label2.Text = "Telèfon 2"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTEL2
        '
        Me.txtTEL2.Location = New System.Drawing.Point(88, 204)
        Me.txtTEL2.MaxLength = 15
        Me.txtTEL2.Name = "txtTEL2"
        Me.txtTEL2.Size = New System.Drawing.Size(140, 20)
        Me.txtTEL2.TabIndex = 323
        Me.txtTEL2.Tag = Nothing
        '
        'Label1
        '
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(232, 232)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 16)
        Me.Label1.TabIndex = 322
        Me.Label1.Text = "email 2"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEMAIL2
        '
        Me.txtEMAIL2.Location = New System.Drawing.Point(300, 228)
        Me.txtEMAIL2.MaxLength = 40
        Me.txtEMAIL2.Name = "txtEMAIL2"
        Me.txtEMAIL2.Size = New System.Drawing.Size(140, 20)
        Me.txtEMAIL2.TabIndex = 321
        Me.txtEMAIL2.Tag = Nothing
        '
        'lblEmail1
        '
        Me.lblEmail1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblEmail1.Location = New System.Drawing.Point(4, 232)
        Me.lblEmail1.Name = "lblEmail1"
        Me.lblEmail1.Size = New System.Drawing.Size(78, 16)
        Me.lblEmail1.TabIndex = 320
        Me.lblEmail1.Text = "email"
        Me.lblEmail1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEMAIL1
        '
        Me.txtEMAIL1.Location = New System.Drawing.Point(88, 228)
        Me.txtEMAIL1.MaxLength = 40
        Me.txtEMAIL1.Name = "txtEMAIL1"
        Me.txtEMAIL1.Size = New System.Drawing.Size(140, 20)
        Me.txtEMAIL1.TabIndex = 319
        Me.txtEMAIL1.Tag = Nothing
        '
        'cboID
        '
        Me.cboID.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboID.AutoCompletion = True
        Me.cboID.Caption = ""
        Me.cboID.CaptionHeight = 17
        Me.cboID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboID.ColumnCaptionHeight = 17
        Me.cboID.ColumnFooterHeight = 17
        Me.cboID.ContentHeight = 15
        Me.cboID.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboID.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboID.EditorBackColor = System.Drawing.Color.Khaki
        Me.cboID.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboID.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboID.EditorHeight = 15
        Me.cboID.Images.Add(CType(resources.GetObject("cboID.Images"), System.Drawing.Image))
        Me.cboID.ItemHeight = 15
        Me.cboID.Location = New System.Drawing.Point(88, 52)
        Me.cboID.MatchEntryTimeout = CType(100, Long)
        Me.cboID.MaxDropDownItems = CType(15, Short)
        Me.cboID.MaxLength = 35
        Me.cboID.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboID.Name = "cboID"
        Me.cboID.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboID.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboID.Size = New System.Drawing.Size(260, 21)
        Me.cboID.TabIndex = 318
        Me.cboID.Text = "ComboBoxEx1"
        Me.cboID.PropBag = resources.GetString("cboID.PropBag")
        '
        'txtCODI
        '
        Me.txtCODI.BackColor = System.Drawing.Color.Khaki
        Me.txtCODI.Location = New System.Drawing.Point(88, 24)
        Me.txtCODI.MaxLength = 35
        Me.txtCODI.Name = "txtCODI"
        Me.txtCODI.Size = New System.Drawing.Size(48, 20)
        Me.txtCODI.TabIndex = 0
        Me.txtCODI.Tag = Nothing
        '
        'lblPais
        '
        Me.lblPais.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPais.Location = New System.Drawing.Point(240, 160)
        Me.lblPais.Name = "lblPais"
        Me.lblPais.Size = New System.Drawing.Size(44, 16)
        Me.lblPais.TabIndex = 317
        Me.lblPais.Text = "Pais"
        Me.lblPais.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTelefonos
        '
        Me.lblTelefonos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTelefonos.Location = New System.Drawing.Point(4, 184)
        Me.lblTelefonos.Name = "lblTelefonos"
        Me.lblTelefonos.Size = New System.Drawing.Size(72, 16)
        Me.lblTelefonos.TabIndex = 289
        Me.lblTelefonos.Text = "Telèfon 1"
        Me.lblTelefonos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContacte
        '
        Me.lblContacte.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblContacte.Location = New System.Drawing.Point(4, 256)
        Me.lblContacte.Name = "lblContacte"
        Me.lblContacte.Size = New System.Drawing.Size(64, 16)
        Me.lblContacte.TabIndex = 290
        Me.lblContacte.Text = "Contacte"
        Me.lblContacte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnElegirProveedor
        '
        Me.btnElegirProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirProveedor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirProveedor.Location = New System.Drawing.Point(140, 24)
        Me.btnElegirProveedor.Name = "btnElegirProveedor"
        Me.btnElegirProveedor.Size = New System.Drawing.Size(28, 20)
        Me.btnElegirProveedor.TabIndex = 314
        Me.btnElegirProveedor.Text = "..."
        Me.btnElegirProveedor.Visible = False
        '
        'lblNIF
        '
        Me.lblNIF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNIF.Location = New System.Drawing.Point(4, 160)
        Me.lblNIF.Name = "lblNIF"
        Me.lblNIF.Size = New System.Drawing.Size(72, 16)
        Me.lblNIF.TabIndex = 288
        Me.lblNIF.Text = "N.I.F."
        Me.lblNIF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblProvincia
        '
        Me.lblProvincia.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblProvincia.Location = New System.Drawing.Point(4, 136)
        Me.lblProvincia.Name = "lblProvincia"
        Me.lblProvincia.Size = New System.Drawing.Size(72, 16)
        Me.lblProvincia.TabIndex = 287
        Me.lblProvincia.Text = "Provincia"
        Me.lblProvincia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCP
        '
        Me.lblCP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCP.Location = New System.Drawing.Point(4, 112)
        Me.lblCP.Name = "lblCP"
        Me.lblCP.Size = New System.Drawing.Size(72, 16)
        Me.lblCP.TabIndex = 286
        Me.lblCP.Text = "C. Postal"
        Me.lblCP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDomicilio
        '
        Me.lblDomicilio.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDomicilio.Location = New System.Drawing.Point(4, 88)
        Me.lblDomicilio.Name = "lblDomicilio"
        Me.lblDomicilio.Size = New System.Drawing.Size(72, 16)
        Me.lblDomicilio.TabIndex = 285
        Me.lblDomicilio.Text = "Domicili"
        Me.lblDomicilio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCodigoProveedor
        '
        Me.lblCodigoProveedor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCodigoProveedor.Location = New System.Drawing.Point(4, 28)
        Me.lblCodigoProveedor.Name = "lblCodigoProveedor"
        Me.lblCodigoProveedor.Size = New System.Drawing.Size(80, 16)
        Me.lblCodigoProveedor.TabIndex = 283
        Me.lblCodigoProveedor.Text = "Codi Proveïdor"
        Me.lblCodigoProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNombre
        '
        Me.lblNombre.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNombre.Location = New System.Drawing.Point(4, 56)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(80, 16)
        Me.lblNombre.TabIndex = 284
        Me.lblNombre.Text = "Nom Proveidor"
        Me.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPAIS
        '
        Me.txtPAIS.Location = New System.Drawing.Point(300, 156)
        Me.txtPAIS.MaxLength = 30
        Me.txtPAIS.Name = "txtPAIS"
        Me.txtPAIS.Size = New System.Drawing.Size(140, 20)
        Me.txtPAIS.TabIndex = 7
        Me.txtPAIS.Tag = Nothing
        '
        'lblFax
        '
        Me.lblFax.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFax.Location = New System.Drawing.Point(240, 184)
        Me.lblFax.Name = "lblFax"
        Me.lblFax.Size = New System.Drawing.Size(36, 16)
        Me.lblFax.TabIndex = 293
        Me.lblFax.Text = "Fax"
        Me.lblFax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCONTACTE
        '
        Me.txtCONTACTE.Location = New System.Drawing.Point(88, 252)
        Me.txtCONTACTE.MaxLength = 40
        Me.txtCONTACTE.Name = "txtCONTACTE"
        Me.txtCONTACTE.Size = New System.Drawing.Size(272, 20)
        Me.txtCONTACTE.TabIndex = 10
        Me.txtCONTACTE.Tag = Nothing
        '
        'lblPoblacion
        '
        Me.lblPoblacion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPoblacion.Location = New System.Drawing.Point(240, 136)
        Me.lblPoblacion.Name = "lblPoblacion"
        Me.lblPoblacion.Size = New System.Drawing.Size(56, 16)
        Me.lblPoblacion.TabIndex = 292
        Me.lblPoblacion.Text = "Població"
        Me.lblPoblacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNIF
        '
        Me.txtNIF.Location = New System.Drawing.Point(88, 156)
        Me.txtNIF.MaxLength = 12
        Me.txtNIF.Name = "txtNIF"
        Me.txtNIF.Size = New System.Drawing.Size(140, 20)
        Me.txtNIF.TabIndex = 6
        Me.txtNIF.Tag = Nothing
        '
        'txtTEL
        '
        Me.txtTEL.Location = New System.Drawing.Point(88, 180)
        Me.txtTEL.MaxLength = 15
        Me.txtTEL.Name = "txtTEL"
        Me.txtTEL.Size = New System.Drawing.Size(140, 20)
        Me.txtTEL.TabIndex = 8
        Me.txtTEL.Tag = Nothing
        '
        'txtPROV
        '
        Me.txtPROV.Location = New System.Drawing.Point(88, 132)
        Me.txtPROV.MaxLength = 35
        Me.txtPROV.Name = "txtPROV"
        Me.txtPROV.Size = New System.Drawing.Size(140, 20)
        Me.txtPROV.TabIndex = 4
        Me.txtPROV.Tag = Nothing
        '
        'txtCP
        '
        Me.txtCP.Location = New System.Drawing.Point(88, 108)
        Me.txtCP.MaxLength = 5
        Me.txtCP.Name = "txtCP"
        Me.txtCP.Size = New System.Drawing.Size(56, 20)
        Me.txtCP.TabIndex = 3
        Me.txtCP.Tag = Nothing
        '
        'txtDOM
        '
        Me.txtDOM.Location = New System.Drawing.Point(88, 84)
        Me.txtDOM.MaxLength = 35
        Me.txtDOM.Multiline = True
        Me.txtDOM.Name = "txtDOM"
        Me.txtDOM.Size = New System.Drawing.Size(356, 20)
        Me.txtDOM.TabIndex = 2
        Me.txtDOM.Tag = Nothing
        '
        'txtFAX
        '
        Me.txtFAX.Location = New System.Drawing.Point(300, 180)
        Me.txtFAX.MaxLength = 15
        Me.txtFAX.Name = "txtFAX"
        Me.txtFAX.Size = New System.Drawing.Size(140, 20)
        Me.txtFAX.TabIndex = 9
        Me.txtFAX.Tag = Nothing
        '
        'txtPOB
        '
        Me.txtPOB.Location = New System.Drawing.Point(300, 132)
        Me.txtPOB.MaxLength = 35
        Me.txtPOB.Name = "txtPOB"
        Me.txtPOB.Size = New System.Drawing.Size(140, 20)
        Me.txtPOB.TabIndex = 5
        Me.txtPOB.Tag = Nothing
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.lblCuentaContable)
        Me.GroupBox1.Controls.Add(Me.btnElegirFormaPago)
        Me.GroupBox1.Controls.Add(Me.lblFormaDePago)
        Me.GroupBox1.Controls.Add(Me.lblDiasPago)
        Me.GroupBox1.Controls.Add(Me.txtDIA2)
        Me.GroupBox1.Controls.Add(Me.txtSUBCTA)
        Me.GroupBox1.Controls.Add(Me.txtDIA1)
        Me.GroupBox1.Controls.Add(Me.txtFORMA)
        Me.GroupBox1.Controls.Add(Me.txtDIA3)
        Me.GroupBox1.Controls.Add(Me.cboNOMFORMA)
        Me.GroupBox1.Controls.Add(Me.cboNOMBANC)
        Me.GroupBox1.Controls.Add(Me.txtBANC)
        Me.GroupBox1.Controls.Add(Me.btnElegirBanco)
        Me.GroupBox1.Controls.Add(Me.lblOficina)
        Me.GroupBox1.Controls.Add(Me.lblNumCompte)
        Me.GroupBox1.Controls.Add(Me.lblBanc)
        Me.GroupBox1.Controls.Add(Me.txtDC)
        Me.GroupBox1.Controls.Add(Me.txtCOFI)
        Me.GroupBox1.Controls.Add(Me.txtCTA)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox1.Location = New System.Drawing.Point(8, 312)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(460, 160)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'lblCuentaContable
        '
        Me.lblCuentaContable.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCuentaContable.Location = New System.Drawing.Point(4, 128)
        Me.lblCuentaContable.Name = "lblCuentaContable"
        Me.lblCuentaContable.Size = New System.Drawing.Size(84, 16)
        Me.lblCuentaContable.TabIndex = 307
        Me.lblCuentaContable.Text = "Cta. Contable"
        Me.lblCuentaContable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnElegirFormaPago
        '
        Me.btnElegirFormaPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirFormaPago.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirFormaPago.Location = New System.Drawing.Point(140, 76)
        Me.btnElegirFormaPago.Name = "btnElegirFormaPago"
        Me.btnElegirFormaPago.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirFormaPago.TabIndex = 305
        Me.btnElegirFormaPago.Text = "..."
        '
        'lblFormaDePago
        '
        Me.lblFormaDePago.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFormaDePago.Location = New System.Drawing.Point(4, 80)
        Me.lblFormaDePago.Name = "lblFormaDePago"
        Me.lblFormaDePago.Size = New System.Drawing.Size(92, 16)
        Me.lblFormaDePago.TabIndex = 304
        Me.lblFormaDePago.Text = "Forma pagament"
        Me.lblFormaDePago.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDiasPago
        '
        Me.lblDiasPago.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDiasPago.Location = New System.Drawing.Point(4, 104)
        Me.lblDiasPago.Name = "lblDiasPago"
        Me.lblDiasPago.Size = New System.Drawing.Size(88, 16)
        Me.lblDiasPago.TabIndex = 296
        Me.lblDiasPago.Text = "Dies Pagament"
        Me.lblDiasPago.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDIA2
        '
        Me.txtDIA2.Location = New System.Drawing.Point(132, 100)
        Me.txtDIA2.Name = "txtDIA2"
        Me.txtDIA2.Size = New System.Drawing.Size(28, 20)
        Me.txtDIA2.TabIndex = 7
        Me.txtDIA2.Tag = Nothing
        '
        'txtSUBCTA
        '
        Me.txtSUBCTA.Location = New System.Drawing.Point(96, 124)
        Me.txtSUBCTA.MaxLength = 10
        Me.txtSUBCTA.Name = "txtSUBCTA"
        Me.txtSUBCTA.Size = New System.Drawing.Size(100, 20)
        Me.txtSUBCTA.TabIndex = 8
        Me.txtSUBCTA.Tag = Nothing
        '
        'txtDIA1
        '
        Me.txtDIA1.Location = New System.Drawing.Point(96, 100)
        Me.txtDIA1.Name = "txtDIA1"
        Me.txtDIA1.Size = New System.Drawing.Size(28, 20)
        Me.txtDIA1.TabIndex = 5
        Me.txtDIA1.Tag = Nothing
        '
        'txtFORMA
        '
        Me.txtFORMA.Location = New System.Drawing.Point(96, 76)
        Me.txtFORMA.MaxLength = 8
        Me.txtFORMA.Name = "txtFORMA"
        Me.txtFORMA.Size = New System.Drawing.Size(44, 20)
        Me.txtFORMA.TabIndex = 3
        Me.txtFORMA.Tag = Nothing
        '
        'txtDIA3
        '
        Me.txtDIA3.Location = New System.Drawing.Point(168, 100)
        Me.txtDIA3.Name = "txtDIA3"
        Me.txtDIA3.Size = New System.Drawing.Size(28, 20)
        Me.txtDIA3.TabIndex = 20
        Me.txtDIA3.Tag = Nothing
        '
        'cboNOMFORMA
        '
        Me.cboNOMFORMA.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMFORMA.AutoSelect = True
        Me.cboNOMFORMA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMFORMA.Caption = ""
        Me.cboNOMFORMA.CaptionHeight = 17
        Me.cboNOMFORMA.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMFORMA.ColumnCaptionHeight = 17
        Me.cboNOMFORMA.ColumnFooterHeight = 17
        Me.cboNOMFORMA.ContentHeight = 15
        Me.cboNOMFORMA.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMFORMA.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMFORMA.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMFORMA.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMFORMA.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMFORMA.EditorHeight = 15
        Me.cboNOMFORMA.Images.Add(CType(resources.GetObject("cboNOMFORMA.Images"), System.Drawing.Image))
        Me.cboNOMFORMA.IntegralHeight = True
        Me.cboNOMFORMA.ItemHeight = 13
        Me.cboNOMFORMA.Location = New System.Drawing.Point(172, 76)
        Me.cboNOMFORMA.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMFORMA.MaxDropDownItems = CType(8, Short)
        Me.cboNOMFORMA.MaxLength = 255
        Me.cboNOMFORMA.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMFORMA.Name = "cboNOMFORMA"
        Me.cboNOMFORMA.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboNOMFORMA.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMFORMA.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMFORMA.Size = New System.Drawing.Size(280, 19)
        Me.cboNOMFORMA.TabIndex = 4
        Me.cboNOMFORMA.PropBag = resources.GetString("cboNOMFORMA.PropBag")
        '
        'cboNOMBANC
        '
        Me.cboNOMBANC.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMBANC.AutoSelect = True
        Me.cboNOMBANC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMBANC.Caption = ""
        Me.cboNOMBANC.CaptionHeight = 17
        Me.cboNOMBANC.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMBANC.ColumnCaptionHeight = 17
        Me.cboNOMBANC.ColumnFooterHeight = 17
        Me.cboNOMBANC.ContentHeight = 15
        Me.cboNOMBANC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMBANC.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMBANC.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMBANC.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMBANC.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMBANC.EditorHeight = 15
        Me.cboNOMBANC.Images.Add(CType(resources.GetObject("cboNOMBANC.Images"), System.Drawing.Image))
        Me.cboNOMBANC.IntegralHeight = True
        Me.cboNOMBANC.ItemHeight = 13
        Me.cboNOMBANC.Location = New System.Drawing.Point(172, 20)
        Me.cboNOMBANC.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMBANC.MaxDropDownItems = CType(8, Short)
        Me.cboNOMBANC.MaxLength = 255
        Me.cboNOMBANC.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMBANC.Name = "cboNOMBANC"
        Me.cboNOMBANC.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboNOMBANC.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMBANC.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMBANC.Size = New System.Drawing.Size(280, 19)
        Me.cboNOMBANC.TabIndex = 0
        Me.cboNOMBANC.PropBag = resources.GetString("cboNOMBANC.PropBag")
        '
        'txtBANC
        '
        Me.txtBANC.Location = New System.Drawing.Point(96, 20)
        Me.txtBANC.MaxLength = 4
        Me.txtBANC.Name = "txtBANC"
        Me.txtBANC.Size = New System.Drawing.Size(44, 20)
        Me.txtBANC.TabIndex = 11
        Me.txtBANC.Tag = Nothing
        '
        'btnElegirBanco
        '
        Me.btnElegirBanco.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirBanco.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirBanco.Location = New System.Drawing.Point(140, 20)
        Me.btnElegirBanco.Name = "btnElegirBanco"
        Me.btnElegirBanco.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirBanco.TabIndex = 295
        Me.btnElegirBanco.Text = "..."
        '
        'lblOficina
        '
        Me.lblOficina.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblOficina.Location = New System.Drawing.Point(4, 52)
        Me.lblOficina.Name = "lblOficina"
        Me.lblOficina.Size = New System.Drawing.Size(84, 16)
        Me.lblOficina.TabIndex = 302
        Me.lblOficina.Text = "Oficina"
        Me.lblOficina.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNumCompte
        '
        Me.lblNumCompte.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNumCompte.Location = New System.Drawing.Point(168, 52)
        Me.lblNumCompte.Name = "lblNumCompte"
        Me.lblNumCompte.Size = New System.Drawing.Size(95, 16)
        Me.lblNumCompte.TabIndex = 301
        Me.lblNumCompte.Text = "Núm. Compte"
        Me.lblNumCompte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBanc
        '
        Me.lblBanc.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblBanc.Location = New System.Drawing.Point(4, 24)
        Me.lblBanc.Name = "lblBanc"
        Me.lblBanc.Size = New System.Drawing.Size(84, 16)
        Me.lblBanc.TabIndex = 291
        Me.lblBanc.Text = "Banc"
        Me.lblBanc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDC
        '
        Me.txtDC.Location = New System.Drawing.Point(272, 48)
        Me.txtDC.Name = "txtDC"
        Me.txtDC.Size = New System.Drawing.Size(32, 20)
        Me.txtDC.TabIndex = 14
        Me.txtDC.Tag = Nothing
        '
        'txtCOFI
        '
        Me.txtCOFI.Location = New System.Drawing.Point(96, 48)
        Me.txtCOFI.MaxLength = 4
        Me.txtCOFI.Name = "txtCOFI"
        Me.txtCOFI.Size = New System.Drawing.Size(40, 20)
        Me.txtCOFI.TabIndex = 1
        Me.txtCOFI.Tag = Nothing
        '
        'txtCTA
        '
        Me.txtCTA.Location = New System.Drawing.Point(324, 48)
        Me.txtCTA.Name = "txtCTA"
        Me.txtCTA.Size = New System.Drawing.Size(124, 20)
        Me.txtCTA.TabIndex = 2
        Me.txtCTA.Tag = Nothing
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
        Me.comboIVA.ContentHeight = 15
        Me.comboIVA.Cursor = System.Windows.Forms.Cursors.Default
        Me.comboIVA.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.comboIVA.EditorBackColor = System.Drawing.SystemColors.Window
        Me.comboIVA.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.comboIVA.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.comboIVA.EditorHeight = 15
        Me.comboIVA.Images.Add(CType(resources.GetObject("comboIVA.Images"), System.Drawing.Image))
        Me.comboIVA.IntegralHeight = True
        Me.comboIVA.ItemHeight = 13
        Me.comboIVA.Location = New System.Drawing.Point(512, 320)
        Me.comboIVA.MatchEntryTimeout = CType(100, Long)
        Me.comboIVA.MaxDropDownItems = CType(8, Short)
        Me.comboIVA.MaxLength = 0
        Me.comboIVA.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.comboIVA.Name = "comboIVA"
        Me.comboIVA.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.comboIVA.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.comboIVA.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.comboIVA.Size = New System.Drawing.Size(148, 19)
        Me.comboIVA.TabIndex = 2
        Me.comboIVA.PropBag = resources.GetString("comboIVA.PropBag")
        '
        'txtNOTES
        '
        Me.txtNOTES.Location = New System.Drawing.Point(476, 28)
        Me.txtNOTES.Multiline = True
        Me.txtNOTES.Name = "txtNOTES"
        Me.txtNOTES.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNOTES.Size = New System.Drawing.Size(480, 280)
        Me.txtNOTES.TabIndex = 0
        Me.txtNOTES.Tag = Nothing
        '
        'lblIvaHilo
        '
        Me.lblIvaHilo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblIvaHilo.Location = New System.Drawing.Point(480, 324)
        Me.lblIvaHilo.Name = "lblIvaHilo"
        Me.lblIvaHilo.Size = New System.Drawing.Size(24, 16)
        Me.lblIvaHilo.TabIndex = 311
        Me.lblIvaHilo.Text = "IVA"
        Me.lblIvaHilo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNotes
        '
        Me.lblNotes.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNotes.Location = New System.Drawing.Point(480, 8)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(100, 16)
        Me.lblNotes.TabIndex = 310
        Me.lblNotes.Text = "Notes"
        Me.lblNotes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkTraspaso
        '
        Me.chkTraspaso.BackColor = System.Drawing.Color.Transparent
        Me.chkTraspaso.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkTraspaso.Location = New System.Drawing.Point(672, 324)
        Me.chkTraspaso.Name = "chkTraspaso"
        Me.chkTraspaso.Size = New System.Drawing.Size(64, 16)
        Me.chkTraspaso.TabIndex = 273
        Me.chkTraspaso.Text = "Traspas"
        Me.chkTraspaso.UseVisualStyleBackColor = False
        Me.chkTraspaso.Visible = False
        '
        'frmProveedoresForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(984, 594)
        Me.Controls.Add(Me.TabControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.KeyPreview = True
        Me.Name = "frmProveedoresForm"
        Me.Text = " Proveïdors"
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
        Me.TabControl1.ResumeLayout(False)
        Me.tpProveedor.ResumeLayout(False)
        Me.tpProveedor.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtTEL2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEMAIL2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEMAIL1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCODI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPAIS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCONTACTE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNIF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTEL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPROV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFAX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPOB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtDIA2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSUBCTA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDIA1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFORMA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDIA3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMFORMA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMBANC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBANC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCOFI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCTA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comboIVA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNOTES, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmProveedoresForm
    Public Shared Function GetInstance() As frmProveedoresForm
        If frmChildForm Is Nothing Then
            frmChildForm = New frmProveedoresForm
        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Public registroActual As clsProveedor

#End Region

#Region "INICIALIZAR"

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True
            Me.arrayCombos = New System.Windows.Forms.Control() {Me.cboNOMBANC, Me.cboNOMFORMA, Me.comboIVA}
            Me.arrayBotones = New System.Windows.Forms.Control() {Me.btnElegirBanco, Me.btnElegirFormaPago, Me.btnElegirProveedor}
            Me.arrayTextBox = New System.Windows.Forms.Control() {Me.txtBANC, Me.txtCOFI, Me.txtCONTACTE, Me.txtCP, Me.txtCTA, Me.txtDC, Me.txtDIA1, Me.txtDIA2, Me.txtDIA3, Me.txtDOM, Me.txtEMAIL1, Me.txtEMAIL2, Me.txtFAX, Me.txtFORMA, Me.txtNIF, Me.txtNOTES, Me.txtPAIS, Me.txtPOB, Me.txtPROV, Me.txtSUBCTA, Me.txtTEL, Me.txtTEL2}
            Me.arrayEtiquetas = New System.Windows.Forms.Control() {Me.Label1, Me.Label2, Me.lblBanc, Me.lblCP, Me.lblCodigoProveedor, Me.lblContacte, Me.lblCuentaContable, Me.lblDiasPago, Me.lblDomicilio, Me.lblEmail1, Me.lblFax, Me.lblFormaDePago, Me.lblIvaHilo, Me.lblNIF, Me.lblNombre, Me.lblNotes, Me.lblNumCompte, Me.lblOficina, Me.lblPais, Me.lblPoblacion, Me.lblProvincia, Me.lblTelefonos}
            ACN()
            tabla = tablaProveedores
            registroActual = New clsProveedor(ds.Tables(tablaProveedores), "C", BindingContext) 'nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto), BindingContext)
            HacerBindings()
            AutoSizeControles()
            PonerModificables(soloLectura)
            PonerHandlersErroresParaGrids()
            PSBTIPO(registroActual.centro) : CCN() : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub HacerBindings()
        Try
            HacerBindingsTodos(registroActual.dvForm)

            AñadirBinding(txtCODI, registroActual.dvForm, "CODI")
            cboID.DataBindings.Add(New Binding("Text", registroActual.dvForm, "NOM"))
            AñadirBindingCombo(cboID, registroActual.dvIdentificadores, CCProve, CNProve)
            OcultarMostrarColumnaCbo(cboID, "CENTRO", False)

            CrearBindingIVA(comboIVA, registroActual.dvForm, True)

            AñadirBindingCombo(cboNOMBANC, registroActual.dtBancs, CCBancs, CNBancs)
            AñadirBindingCombo(cboNOMFORMA, registroActual.dtForpag, CCForpag, CNForpag)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub

#End Region

#Region "MODIFICAR DB"

    Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnNuevo.Click
        Try
            cargando = True
            PonerModificables(modificable)
            EsRegistroNuevo = True
            PonerControlesNuevo(False)
            registroActual.NuevoRegistro()
            cboSeleccionCentro.SelectedValue = registroActual.centro
            PSBTIPO(registroActual.centro) : cargando = False

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
                editando = True : PSBTIPO(registroActual.centro)
                PonerControlesNuevo(True)
                EsRegistroNuevo = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Friend Overrides Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnActualizar.Click
        Try
            cargando = True
            If registroActual.TieneCambios Then
                Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                    Case DialogResult.Cancel
                        cargando = False
                        Exit Sub
                    Case DialogResult.No
                        registroActual.tabla.RejectChanges()
                        cargando = False
                        Exit Sub
                End Select
            End If
            registroActual.ActualizarOrigen()
            ActualizarOrigen()

            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnBorrar.Click
        Try
            If MessageBox.Show(rm.GetString("BorrarProveedor"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                editando = True
                registroActual.borrar() : ActualizarOrigen()
                editando = False
            End If
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
                PSBTIPO(registroActual.centro)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ESPECÍFICO"

#End Region

#Region "IMPRESIÓN"

#End Region

#Region "COMUNES"

    Private Sub btnVerLista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerLista.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmProveedoresLista = frmProveedoresLista.GetInstance(esListado)
            f.MdiParent = Me.MdiParent
            AddHandler f.Closed, AddressOf CType(Me.MdiParent, frmPrincipal).childCerrado
            AddHandler f.Load, AddressOf CType(Me.MdiParent, frmPrincipal).childAbierto : AddHandler f.Activated, AddressOf CType(Me.MdiParent, frmPrincipal).childOcultandoMostrando
            f.Show()
            f.BringToFront()
            Cursor = Cursors.Default

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerModificables(Optional ByVal b As Boolean = False)
        Try
            PonerMod(b)
            If Not b Then : cboID.DataSource = Nothing
            Else : AñadirBindingCombo(cboID, registroActual.dvIdentificadores, CCProve, CNProve) : registroActual.tabla.AcceptChanges() : End If
            cboID.LimitToList = b
            cboID.SuperBack = b
            txtCODI.ReadOnly = editando Or EsRegistroNuevo
            ModiNuev(b)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerControlesNuevo(ByVal b As Boolean)
        Try
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
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Sub btnRecargar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecargar.Click
        Try
            cargando = True
            Dim id As Object
            id = txtCODI.Text
            registroActual.ActualizarOrigen()
            registroActual.CambiarAReg(id, iraregistro)
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub

#End Region

#Region "DESPLAZARSE"

    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnPrimero.Click
        Try
            cargando = True
            registroActual.PrimeroReg() : ActualizarOrigen()
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUltimo.Click
        Try
            cargando = True
            registroActual.UltimoReg() : ActualizarOrigen()
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAnterior.Click
        Try
            cargando = True
            registroActual.AnteriorReg() : ActualizarOrigen()
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSiguiente.Click
        Try
            cargando = True
            registroActual.SiguienteReg() : ActualizarOrigen()
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "SELECCIÓN REGISTRO"

    Private Sub btnElegirProveedor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirProveedor.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmProveedoresLista = frmProveedoresLista.GetInstance(esEleccion)
            f.ShowDialog()
            Cursor = Cursors.Default
            If Not f.dr Is Nothing Then registroActual.CambiarAReg(f.dr("CODI"), iraregistro)
            f = Nothing

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboID_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboID.RowChange
        Try
            If consulta() Then
                cargando = True
                registroActual.CambiarAReg(general.ponercontrabarrasreal(cboID.WillChangeToValue), iraregistro)
                PSBTIPO(registroActual.centro) : cargando = False
            End If

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
    Private Sub txtCODI_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCODI.Validated
        If Not cargando Then
            cargando = True
            ValidarCambio()
            cargando = False
        End If
    End Sub
    Private Sub txtID_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCODI.KeyDown
        Try
            If consulta() Then
                cargando = True
                If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Tab Then
                    ValidarCambio()
                Else
                    BindingContext(registroActual.dvForm).SuspendBinding()
                    PSBTIPO(registroActual.centro)
                End If
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub ValidarCambio()
        Try
            If Not EsRegistroNuevo AndAlso Not editando Then
                Dim IDAbuscar As Integer
                cargando = True
                IDAbuscar = nzn(txtCODI.Text, 0)
                BindingContext(registroActual.dvForm).ResumeBinding()
                registroActual.CambiarAReg(IDAbuscar, iraregistro)
                PSBTIPO(registroActual.centro) : cargando = False
            Else
                If EsRegistroNuevo Then
                    'BindingContext(clienteActual.dvForm).SuspendBinding()
                    If ExisteID(tabla, nzn(txtCODI.Text, -1), registroActual.centro) Then 'clienteActual.dvForm.Find(nzn(txtID.Text, 0)) <> -1 Then
                        MessageBox.Show(rm.GetString("CODIGOYAEXISTENTE"), "", MessageBoxButtons.OK)
                        txtCODI.Focus()
                        BindingContext(registroActual.dvForm).ResumeBinding()
                    End If
                End If
            End If
        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#Region "SELECCION CENTRO"

    Private Sub cboSeleccion_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSeleccionCentro.SelectionChangeCommitted
        Try

            If consulta() Then
                cargando = True
                registroActual.cambioCentro(GENERAL.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto), primero)
                cboSeleccionCentro.SelectedValue = registroActual.centro
                cargando = False
            Else
                If EsRegistroNuevo And Not cargando Then
                    cargando = True
                    registroActual.centro = general.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto)
                    registroActual.tabla.Clear()
                    registroActual.NuevoRegistro()
                    PSBTIPO(registroActual.centro)
                    cargando = False
                End If
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#End Region

#Region "ELECCIONES"

    Private Sub btnElegirBanco_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirBanco.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmBancosLista = frmBancosLista.GetInstance(esEleccion)
            f.ShowDialog()
            Cursor = Cursors.Default
            If Not f.dr Is Nothing Then registroActual.BANC = f.dr("CODI")
            f = Nothing

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnElegirFormaPago_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirFormaPago.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmPagoLista = frmPagoLista.GetInstance(esEleccion)
            f.ShowDialog()
            Cursor = Cursors.Default
            If Not f.dr Is Nothing Then registroActual.FORMA = f.dr("CODI")

            f = Nothing
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboNombreBanco_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMBANC.SelectedValueChanged
        Try
            If PuedoModificar() Then cargando = True : registroActual.BANC = general.nz(cboNOMBANC.WillChangeToValue, "") : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboFormaDePago_selectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMFORMA.SelectedValueChanged
        Try
            If PuedoModificar() Then cargando = True : registroActual.FORMA = general.nz(cboNOMFORMA.WillChangeToValue, "") : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub comboIVA_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles comboIVA.SelectedValueChanged
        Try
            If PuedoModificar() Then cargando = True : registroActual.IVA = comboIVA.SelectedValue : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtCodigoBanco_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBANC.Validated
        Try
            If PuedoModificar() Then cargando = True : registroActual.BANC = general.nz(txtBANC.Text, "") : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtCodigoFormaPago_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFORMA.Validated
        Try
            If PuedoModificar() Then cargando = True : registroActual.FORMA = general.nz(txtFORMA.Text, "") : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

    Private Sub frm_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        GrabaParametro(registroActual.CODI, "proveedor")
    End Sub

End Class
