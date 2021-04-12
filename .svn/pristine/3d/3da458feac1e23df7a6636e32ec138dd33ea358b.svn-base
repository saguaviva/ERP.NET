Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes



Public Class frmRemesas
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

    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblRemsor As System.Windows.Forms.Label
    Friend WithEvents lblOficina As System.Windows.Forms.Label
    Friend WithEvents lblCPYPoblacion As System.Windows.Forms.Label
    'Leadedit.UI.Windows.FlatButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnElegirBanco As C1.Win.C1Input.C1Button
    Friend WithEvents btnElegirCliente As C1.Win.C1Input.C1Button 'Leadedit.UI.Windows.FlatButton
    Friend WithEvents btnVerEfectosSinRemetir As C1.Win.C1Input.C1Button 'Leadedit.UI.Windows.FlatButton
    Friend WithEvents btnVerTodosLosEfectos As C1.Win.C1Input.C1Button 'Leadedit.UI.Windows.FlatButton
    Friend WithEvents btnVerEfectosRemetidos As C1.Win.C1Input.C1Button 'Leadedit.UI.Windows.FlatButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtREMESOR As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtDOMREMESOR As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtOFICINA As C1.Win.C1Input.C1TextBox
    Friend WithEvents tabCRemes As System.Windows.Forms.TabControl
    Friend WithEvents tabRemes As System.Windows.Forms.TabPage
    Friend WithEvents dtpFECHA As C1.Win.C1Input.C1DateEdit
    Friend WithEvents tabVENCIMREMES As System.Windows.Forms.TabPage
    Friend WithEvents dgRemesas As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents btnAñadirARemesa As C1.Win.C1Input.C1Button
    Friend WithEvents dtpFinal As C1.Win.C1Input.C1DateEdit
    Friend WithEvents dtpInicio As C1.Win.C1Input.C1DateEdit
   
    Friend WithEvents cboNOMBANC As C1.Win.C1List.C1Combo
    Friend WithEvents lblIDREMESA As System.Windows.Forms.Label
    Friend WithEvents WButton1 As C1.Win.C1Input.C1Button
    Friend WithEvents XpGroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtLINEA As C1.Win.C1Input.C1TextBox
    Friend WithEvents lblLinea As System.Windows.Forms.Label
    Friend WithEvents dtpFecha2 As C1.Win.C1Input.C1DateEdit
    Friend WithEvents dtpFecha1 As C1.Win.C1Input.C1DateEdit
    Friend WithEvents XpGroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cboCODI As C1.Win.C1List.C1Combo
    Friend WithEvents XpGroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoCOBRO As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDESCUENTO As System.Windows.Forms.RadioButton
    Friend WithEvents txtBANC As C1.Win.C1Input.C1TextBox
    Friend WithEvents dgVenci As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents txtPROVREMESOR As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtPOBREMESOR As C1.Win.C1Input.C1TextBox
    Friend WithEvents dgCuentas As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents cboCuentas As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents cboDeLosClientes As C1.Win.C1List.C1Combo
    Friend WithEvents tabImpresion As System.Windows.Forms.TabPage
    Friend WithEvents txtCPREMESOR As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtNOMREMESOR As C1.Win.C1Input.C1TextBox
    Friend WithEvents XpGroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ppRemesa As C1.Win.C1Preview.C1PrintPreviewControl
    Friend WithEvents XpGroupBox5 As System.Windows.Forms.GroupBox


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRemesas))
        Dim Style1 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style2 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style3 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style4 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style5 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style6 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style7 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style8 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Me.lblRemsor = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblOficina = New System.Windows.Forms.Label()
        Me.lblCPYPoblacion = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnElegirBanco = New C1.Win.C1Input.C1Button()
        Me.btnElegirCliente = New C1.Win.C1Input.C1Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tabCRemes = New System.Windows.Forms.TabControl()
        Me.tabRemes = New System.Windows.Forms.TabPage()
        Me.XpGroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtNOMREMESOR = New C1.Win.C1Input.C1TextBox()
        Me.txtLINEA = New C1.Win.C1Input.C1TextBox()
        Me.lblLinea = New System.Windows.Forms.Label()
        Me.lblIDREMESA = New System.Windows.Forms.Label()
        Me.cboCODI = New C1.Win.C1List.C1Combo()
        Me.txtREMESOR = New C1.Win.C1Input.C1TextBox()
        Me.cboNOMBANC = New C1.Win.C1List.C1Combo()
        Me.txtDOMREMESOR = New C1.Win.C1Input.C1TextBox()
        Me.txtBANC = New C1.Win.C1Input.C1TextBox()
        Me.txtPROVREMESOR = New C1.Win.C1Input.C1TextBox()
        Me.txtPOBREMESOR = New C1.Win.C1Input.C1TextBox()
        Me.txtCPREMESOR = New C1.Win.C1Input.C1TextBox()
        Me.dtpFECHA = New C1.Win.C1Input.C1DateEdit()
        Me.txtOFICINA = New C1.Win.C1Input.C1TextBox()
        Me.cboCuentas = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.dgCuentas = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.XpGroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rdoDESCUENTO = New System.Windows.Forms.RadioButton()
        Me.rdoCOBRO = New System.Windows.Forms.RadioButton()
        Me.tabVENCIMREMES = New System.Windows.Forms.TabPage()
        Me.XpGroupBox3 = New System.Windows.Forms.GroupBox()
        Me.XpGroupBox5 = New System.Windows.Forms.GroupBox()
        Me.dtpInicio = New C1.Win.C1Input.C1DateEdit()
        Me.dtpFinal = New C1.Win.C1Input.C1DateEdit()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboDeLosClientes = New C1.Win.C1List.C1Combo()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.btnAñadirARemesa = New C1.Win.C1Input.C1Button()
        Me.XpGroupBox2 = New System.Windows.Forms.GroupBox()
        Me.WButton1 = New C1.Win.C1Input.C1Button()
        Me.dtpFecha1 = New C1.Win.C1Input.C1DateEdit()
        Me.dtpFecha2 = New C1.Win.C1Input.C1DateEdit()
        Me.dgVenci = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.dgRemesas = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.tabImpresion = New System.Windows.Forms.TabPage()
        Me.ppRemesa = New C1.Win.C1Preview.C1PrintPreviewControl()
        Me.tabCRemes.SuspendLayout()
        Me.tabRemes.SuspendLayout()
        Me.XpGroupBox1.SuspendLayout()
        CType(Me.txtNOMREMESOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLINEA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCODI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREMESOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMBANC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDOMREMESOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBANC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPROVREMESOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPOBREMESOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPREMESOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFECHA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOFICINA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCuentas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgCuentas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XpGroupBox4.SuspendLayout()
        Me.tabVENCIMREMES.SuspendLayout()
        Me.XpGroupBox3.SuspendLayout()
        Me.XpGroupBox5.SuspendLayout()
        CType(Me.dtpInicio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFinal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDeLosClientes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XpGroupBox2.SuspendLayout()
        CType(Me.dtpFecha1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFecha2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgVenci, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgRemesas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabImpresion.SuspendLayout()
        CType(Me.ppRemesa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ppRemesa.SuspendLayout()
        Me.SuspendLayout()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.sbtipo.Location = New System.Drawing.Point(28, 0)
        Me.sbtipo.Size = New System.Drawing.Size(88, 15)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(60, 41)
        Me.btnRecargar.Size = New System.Drawing.Size(90, 18)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(330, 46)
        Me.btnSiguiente.Size = New System.Drawing.Size(32, 19)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(28, 46)
        Me.btnAnterior.Size = New System.Drawing.Size(32, 19)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(28, 56)
        Me.btnPrimero.Size = New System.Drawing.Size(32, 18)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(330, 63)
        Me.btnUltimo.Size = New System.Drawing.Size(32, 19)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(148, 63)
        Me.btnModificar.Size = New System.Drawing.Size(98, 19)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(514, 46)
        Me.btnTancar.Size = New System.Drawing.Size(72, 19)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(150, 45)
        Me.btnBorrar.Size = New System.Drawing.Size(98, 18)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(248, 45)
        Me.btnNuevo.Size = New System.Drawing.Size(82, 37)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(60, 63)
        Me.btnActualizar.Size = New System.Drawing.Size(90, 19)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(536, 59)
        Me.btnVerLista.Size = New System.Drawing.Size(72, 19)
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.Location = New System.Drawing.Point(280, 7)
        '
        'lblRemsor
        '
        Me.lblRemsor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRemsor.Location = New System.Drawing.Point(8, 76)
        Me.lblRemsor.Name = "lblRemsor"
        Me.lblRemsor.Size = New System.Drawing.Size(70, 22)
        Me.lblRemsor.TabIndex = 2
        Me.lblRemsor.Text = "Per:"
        Me.lblRemsor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(8, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 22)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Domicili"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblOficina
        '
        Me.lblOficina.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblOficina.Location = New System.Drawing.Point(8, 164)
        Me.lblOficina.Name = "lblOficina"
        Me.lblOficina.Size = New System.Drawing.Size(70, 22)
        Me.lblOficina.TabIndex = 5
        Me.lblOficina.Text = "de l'oficina"
        Me.lblOficina.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCPYPoblacion
        '
        Me.lblCPYPoblacion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCPYPoblacion.Location = New System.Drawing.Point(8, 124)
        Me.lblCPYPoblacion.Name = "lblCPYPoblacion"
        Me.lblCPYPoblacion.Size = New System.Drawing.Size(72, 22)
        Me.lblCPYPoblacion.TabIndex = 15
        Me.lblCPYPoblacion.Text = "CP i població"
        Me.lblCPYPoblacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(8, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 22)
        Me.Label1.TabIndex = 296
        Me.Label1.Text = "Banc"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnElegirBanco
        '
        Me.btnElegirBanco.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirBanco.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirBanco.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirBanco.Location = New System.Drawing.Point(124, 52)
        Me.btnElegirBanco.Name = "btnElegirBanco"
        Me.btnElegirBanco.Size = New System.Drawing.Size(24, 18)
        Me.btnElegirBanco.TabIndex = 299
        Me.btnElegirBanco.TabStop = False
        Me.btnElegirBanco.Text = "..."
        '
        'btnElegirCliente
        '
        Me.btnElegirCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirCliente.Location = New System.Drawing.Point(124, 76)
        Me.btnElegirCliente.Name = "btnElegirCliente"
        Me.btnElegirCliente.Size = New System.Drawing.Size(24, 19)
        Me.btnElegirCliente.TabIndex = 301
        Me.btnElegirCliente.TabStop = False
        Me.btnElegirCliente.Text = "..."
        Me.btnElegirCliente.Visible = False
        '
        'Label3
        '
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(264, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 19)
        Me.Label3.TabIndex = 313
        Me.Label3.Text = "Data"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tabCRemes
        '
        Me.tabCRemes.Controls.Add(Me.tabRemes)
        Me.tabCRemes.Controls.Add(Me.tabVENCIMREMES)
        Me.tabCRemes.Controls.Add(Me.tabImpresion)
        Me.tabCRemes.ItemSize = New System.Drawing.Size(50, 18)
        Me.tabCRemes.Location = New System.Drawing.Point(4, 7)
        Me.tabCRemes.Name = "tabCRemes"
        Me.tabCRemes.SelectedIndex = 0
        Me.tabCRemes.Size = New System.Drawing.Size(976, 513)
        Me.tabCRemes.TabIndex = 314
        '
        'tabRemes
        '
        Me.tabRemes.Controls.Add(Me.XpGroupBox1)
        Me.tabRemes.Controls.Add(Me.cboCuentas)
        Me.tabRemes.Controls.Add(Me.dgCuentas)
        Me.tabRemes.Controls.Add(Me.XpGroupBox4)
        Me.tabRemes.Location = New System.Drawing.Point(4, 22)
        Me.tabRemes.Name = "tabRemes"
        Me.tabRemes.Size = New System.Drawing.Size(968, 487)
        Me.tabRemes.TabIndex = 0
        Me.tabRemes.Text = "Remesa"
        '
        'XpGroupBox1
        '
        Me.XpGroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.XpGroupBox1.Controls.Add(Me.txtNOMREMESOR)
        Me.XpGroupBox1.Controls.Add(Me.txtLINEA)
        Me.XpGroupBox1.Controls.Add(Me.lblLinea)
        Me.XpGroupBox1.Controls.Add(Me.lblIDREMESA)
        Me.XpGroupBox1.Controls.Add(Me.cboCODI)
        Me.XpGroupBox1.Controls.Add(Me.Label1)
        Me.XpGroupBox1.Controls.Add(Me.txtREMESOR)
        Me.XpGroupBox1.Controls.Add(Me.cboNOMBANC)
        Me.XpGroupBox1.Controls.Add(Me.btnElegirBanco)
        Me.XpGroupBox1.Controls.Add(Me.btnElegirCliente)
        Me.XpGroupBox1.Controls.Add(Me.txtDOMREMESOR)
        Me.XpGroupBox1.Controls.Add(Me.txtBANC)
        Me.XpGroupBox1.Controls.Add(Me.lblRemsor)
        Me.XpGroupBox1.Controls.Add(Me.Label2)
        Me.XpGroupBox1.Controls.Add(Me.lblCPYPoblacion)
        Me.XpGroupBox1.Controls.Add(Me.txtPROVREMESOR)
        Me.XpGroupBox1.Controls.Add(Me.txtPOBREMESOR)
        Me.XpGroupBox1.Controls.Add(Me.txtCPREMESOR)
        Me.XpGroupBox1.Controls.Add(Me.Label3)
        Me.XpGroupBox1.Controls.Add(Me.dtpFECHA)
        Me.XpGroupBox1.Controls.Add(Me.txtOFICINA)
        Me.XpGroupBox1.Controls.Add(Me.lblOficina)
        Me.XpGroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.XpGroupBox1.Location = New System.Drawing.Point(12, 20)
        Me.XpGroupBox1.Name = "XpGroupBox1"
        Me.XpGroupBox1.Size = New System.Drawing.Size(472, 304)
        Me.XpGroupBox1.TabIndex = 334
        Me.XpGroupBox1.TabStop = False
        Me.XpGroupBox1.Text = "Remesa"
        '
        'txtNOMREMESOR
        '
        Me.txtNOMREMESOR.Location = New System.Drawing.Point(80, 76)
        Me.txtNOMREMESOR.Name = "txtNOMREMESOR"
        Me.txtNOMREMESOR.Size = New System.Drawing.Size(380, 20)
        Me.txtNOMREMESOR.TabIndex = 336
        Me.txtNOMREMESOR.Tag = Nothing
        '
        'txtLINEA
        '
        Me.txtLINEA.Location = New System.Drawing.Point(80, 196)
        Me.txtLINEA.Multiline = True
        Me.txtLINEA.Name = "txtLINEA"
        Me.txtLINEA.Size = New System.Drawing.Size(340, 89)
        Me.txtLINEA.TabIndex = 335
        Me.txtLINEA.Tag = Nothing
        '
        'lblLinea
        '
        Me.lblLinea.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblLinea.Location = New System.Drawing.Point(8, 196)
        Me.lblLinea.Name = "lblLinea"
        Me.lblLinea.Size = New System.Drawing.Size(70, 22)
        Me.lblLinea.TabIndex = 334
        Me.lblLinea.Text = "Linia"
        Me.lblLinea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIDREMESA
        '
        Me.lblIDREMESA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblIDREMESA.Location = New System.Drawing.Point(8, 24)
        Me.lblIDREMESA.Name = "lblIDREMESA"
        Me.lblIDREMESA.Size = New System.Drawing.Size(72, 22)
        Me.lblIDREMESA.TabIndex = 332
        Me.lblIDREMESA.Text = "Codi"
        Me.lblIDREMESA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboCODI
        '
        Me.cboCODI.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboCODI.Caption = ""
        Me.cboCODI.CaptionHeight = 17
        Me.cboCODI.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboCODI.ColumnCaptionHeight = 17
        Me.cboCODI.ColumnFooterHeight = 17
        Me.cboCODI.ContentHeight = 16
        Me.cboCODI.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboCODI.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboCODI.EditorFont = New System.Drawing.Font("Tahoma", 8.25!)
        Me.cboCODI.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCODI.EditorHeight = 16
        Me.cboCODI.Images.Add(CType(resources.GetObject("cboCODI.Images"), System.Drawing.Image))
        Me.cboCODI.ItemHeight = 15
        Me.cboCODI.Location = New System.Drawing.Point(80, 24)
        Me.cboCODI.MatchEntryTimeout = CType(2000, Long)
        Me.cboCODI.MaxDropDownItems = CType(5, Short)
        Me.cboCODI.MaxLength = 32767
        Me.cboCODI.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboCODI.Name = "cboCODI"
        Me.cboCODI.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboCODI.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboCODI.Size = New System.Drawing.Size(96, 22)
        Me.cboCODI.TabIndex = 331
        Me.cboCODI.Text = "C1Combo1"
        Me.cboCODI.PropBag = resources.GetString("cboCODI.PropBag")
        '
        'txtREMESOR
        '
        Me.txtREMESOR.Location = New System.Drawing.Point(80, 76)
        Me.txtREMESOR.Name = "txtREMESOR"
        Me.txtREMESOR.Size = New System.Drawing.Size(40, 20)
        Me.txtREMESOR.TabIndex = 319
        Me.txtREMESOR.Tag = Nothing
        Me.txtREMESOR.Visible = False
        '
        'cboNOMBANC
        '
        Me.cboNOMBANC.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMBANC.Caption = ""
        Me.cboNOMBANC.CaptionHeight = 17
        Me.cboNOMBANC.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMBANC.ColumnCaptionHeight = 17
        Me.cboNOMBANC.ColumnFooterHeight = 17
        Me.cboNOMBANC.ContentHeight = 16
        Me.cboNOMBANC.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMBANC.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMBANC.EditorFont = New System.Drawing.Font("Tahoma", 8.25!)
        Me.cboNOMBANC.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMBANC.EditorHeight = 16
        Me.cboNOMBANC.Images.Add(CType(resources.GetObject("cboNOMBANC.Images"), System.Drawing.Image))
        Me.cboNOMBANC.ItemHeight = 15
        Me.cboNOMBANC.Location = New System.Drawing.Point(156, 52)
        Me.cboNOMBANC.MatchEntryTimeout = CType(2000, Long)
        Me.cboNOMBANC.MaxDropDownItems = CType(5, Short)
        Me.cboNOMBANC.MaxLength = 32767
        Me.cboNOMBANC.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMBANC.Name = "cboNOMBANC"
        Me.cboNOMBANC.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMBANC.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMBANC.Size = New System.Drawing.Size(308, 22)
        Me.cboNOMBANC.TabIndex = 330
        Me.cboNOMBANC.Text = "C1Combo1"
        Me.cboNOMBANC.PropBag = resources.GetString("cboNOMBANC.PropBag")
        '
        'txtDOMREMESOR
        '
        Me.txtDOMREMESOR.Location = New System.Drawing.Point(80, 100)
        Me.txtDOMREMESOR.Name = "txtDOMREMESOR"
        Me.txtDOMREMESOR.Size = New System.Drawing.Size(380, 20)
        Me.txtDOMREMESOR.TabIndex = 322
        Me.txtDOMREMESOR.Tag = Nothing
        '
        'txtBANC
        '
        Me.txtBANC.Location = New System.Drawing.Point(80, 52)
        Me.txtBANC.Name = "txtBANC"
        Me.txtBANC.Size = New System.Drawing.Size(40, 20)
        Me.txtBANC.TabIndex = 315
        Me.txtBANC.Tag = Nothing
        '
        'txtPROVREMESOR
        '
        Me.txtPROVREMESOR.Location = New System.Drawing.Point(344, 124)
        Me.txtPROVREMESOR.Name = "txtPROVREMESOR"
        Me.txtPROVREMESOR.Size = New System.Drawing.Size(116, 20)
        Me.txtPROVREMESOR.TabIndex = 329
        Me.txtPROVREMESOR.Tag = Nothing
        '
        'txtPOBREMESOR
        '
        Me.txtPOBREMESOR.Location = New System.Drawing.Point(140, 124)
        Me.txtPOBREMESOR.Name = "txtPOBREMESOR"
        Me.txtPOBREMESOR.Size = New System.Drawing.Size(200, 20)
        Me.txtPOBREMESOR.TabIndex = 328
        Me.txtPOBREMESOR.Tag = Nothing
        '
        'txtCPREMESOR
        '
        Me.txtCPREMESOR.Location = New System.Drawing.Point(80, 124)
        Me.txtCPREMESOR.Name = "txtCPREMESOR"
        Me.txtCPREMESOR.Size = New System.Drawing.Size(56, 20)
        Me.txtCPREMESOR.TabIndex = 327
        Me.txtCPREMESOR.Tag = Nothing
        '
        'dtpFECHA
        '
        '
        '
        '
        Me.dtpFECHA.Calendar.DayNameLength = 1
        Me.dtpFECHA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpFECHA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpFECHA.Location = New System.Drawing.Point(348, 26)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(116, 20)
        Me.dtpFECHA.TabIndex = 325
        Me.dtpFECHA.Tag = Nothing
        '
        'txtOFICINA
        '
        Me.txtOFICINA.Location = New System.Drawing.Point(80, 164)
        Me.txtOFICINA.Name = "txtOFICINA"
        Me.txtOFICINA.Size = New System.Drawing.Size(252, 20)
        Me.txtOFICINA.TabIndex = 316
        Me.txtOFICINA.Tag = Nothing
        '
        'cboCuentas
        '
        Me.cboCuentas.AllowColMove = True
        Me.cboCuentas.AllowColSelect = True
        Me.cboCuentas.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboCuentas.AlternatingRows = False
        Me.cboCuentas.CaptionStyle = Style1
        Me.cboCuentas.ColumnCaptionHeight = 17
        Me.cboCuentas.ColumnFooterHeight = 17
        Me.cboCuentas.EvenRowStyle = Style2
        Me.cboCuentas.FetchRowStyles = False
        Me.cboCuentas.FooterStyle = Style3
        Me.cboCuentas.HeadingStyle = Style4
        Me.cboCuentas.HighLightRowStyle = Style5
        Me.cboCuentas.Images.Add(CType(resources.GetObject("cboCuentas.Images"), System.Drawing.Image))
        Me.cboCuentas.Location = New System.Drawing.Point(504, 140)
        Me.cboCuentas.Name = "cboCuentas"
        Me.cboCuentas.OddRowStyle = Style6
        Me.cboCuentas.RecordSelectorStyle = Style7
        Me.cboCuentas.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboCuentas.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboCuentas.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboCuentas.ScrollTips = False
        Me.cboCuentas.Size = New System.Drawing.Size(84, 104)
        Me.cboCuentas.Style = Style8
        Me.cboCuentas.TabIndex = 340
        Me.cboCuentas.Text = "C1TrueDBDropdown1"
        Me.cboCuentas.ValueTranslate = True
        Me.cboCuentas.Visible = False
        Me.cboCuentas.PropBag = resources.GetString("cboCuentas.PropBag")
        '
        'dgCuentas
        '
        Me.dgCuentas.AllowAddNew = True
        Me.dgCuentas.AllowDelete = True
        Me.dgCuentas.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgCuentas.Images.Add(CType(resources.GetObject("dgCuentas.Images"), System.Drawing.Image))
        Me.dgCuentas.Location = New System.Drawing.Point(488, 108)
        Me.dgCuentas.Name = "dgCuentas"
        Me.dgCuentas.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgCuentas.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgCuentas.PreviewInfo.ZoomFactor = 75.0R
        Me.dgCuentas.PrintInfo.PageSettings = CType(resources.GetObject("dgCuentas.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgCuentas.Size = New System.Drawing.Size(452, 210)
        Me.dgCuentas.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgCuentas.TabIndex = 339
        Me.dgCuentas.PropBag = resources.GetString("dgCuentas.PropBag")
        '
        'XpGroupBox4
        '
        Me.XpGroupBox4.BackColor = System.Drawing.SystemColors.Control
        Me.XpGroupBox4.Controls.Add(Me.rdoDESCUENTO)
        Me.XpGroupBox4.Controls.Add(Me.rdoCOBRO)
        Me.XpGroupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.XpGroupBox4.Location = New System.Drawing.Point(488, 19)
        Me.XpGroupBox4.Name = "XpGroupBox4"
        Me.XpGroupBox4.Size = New System.Drawing.Size(268, 81)
        Me.XpGroupBox4.TabIndex = 338
        Me.XpGroupBox4.TabStop = False
        Me.XpGroupBox4.Text = "Tipus"
        '
        'rdoDESCUENTO
        '
        Me.rdoDESCUENTO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoDESCUENTO.Location = New System.Drawing.Point(144, 37)
        Me.rdoDESCUENTO.Name = "rdoDESCUENTO"
        Me.rdoDESCUENTO.Size = New System.Drawing.Size(104, 22)
        Me.rdoDESCUENTO.TabIndex = 1
        Me.rdoDESCUENTO.Text = "Descompte"
        '
        'rdoCOBRO
        '
        Me.rdoCOBRO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoCOBRO.Location = New System.Drawing.Point(28, 37)
        Me.rdoCOBRO.Name = "rdoCOBRO"
        Me.rdoCOBRO.Size = New System.Drawing.Size(104, 22)
        Me.rdoCOBRO.TabIndex = 0
        Me.rdoCOBRO.Text = "Cobrament"
        '
        'tabVENCIMREMES
        '
        Me.tabVENCIMREMES.Controls.Add(Me.XpGroupBox3)
        Me.tabVENCIMREMES.Controls.Add(Me.dgRemesas)
        Me.tabVENCIMREMES.Location = New System.Drawing.Point(4, 22)
        Me.tabVENCIMREMES.Name = "tabVENCIMREMES"
        Me.tabVENCIMREMES.Size = New System.Drawing.Size(968, 487)
        Me.tabVENCIMREMES.TabIndex = 1
        Me.tabVENCIMREMES.Text = "Detall"
        '
        'XpGroupBox3
        '
        Me.XpGroupBox3.BackColor = System.Drawing.SystemColors.Control
        Me.XpGroupBox3.Controls.Add(Me.XpGroupBox5)
        Me.XpGroupBox3.Controls.Add(Me.XpGroupBox2)
        Me.XpGroupBox3.Controls.Add(Me.dgVenci)
        Me.XpGroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.XpGroupBox3.Location = New System.Drawing.Point(20, 8)
        Me.XpGroupBox3.Name = "XpGroupBox3"
        Me.XpGroupBox3.Size = New System.Drawing.Size(928, 249)
        Me.XpGroupBox3.TabIndex = 338
        Me.XpGroupBox3.TabStop = False
        Me.XpGroupBox3.Text = "Carregar efectes"
        '
        'XpGroupBox5
        '
        Me.XpGroupBox5.BackColor = System.Drawing.SystemColors.Control
        Me.XpGroupBox5.Controls.Add(Me.dtpInicio)
        Me.XpGroupBox5.Controls.Add(Me.dtpFinal)
        Me.XpGroupBox5.Controls.Add(Me.Label4)
        Me.XpGroupBox5.Controls.Add(Me.cboDeLosClientes)
        Me.XpGroupBox5.Controls.Add(Me.CheckBox1)
        Me.XpGroupBox5.Controls.Add(Me.btnAñadirARemesa)
        Me.XpGroupBox5.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.XpGroupBox5.Location = New System.Drawing.Point(8, 16)
        Me.XpGroupBox5.Name = "XpGroupBox5"
        Me.XpGroupBox5.Size = New System.Drawing.Size(596, 76)
        Me.XpGroupBox5.TabIndex = 341
        Me.XpGroupBox5.TabStop = False
        '
        'dtpInicio
        '
        '
        '
        '
        Me.dtpInicio.Calendar.DayNameLength = 1
        Me.dtpInicio.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpInicio.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpInicio.Location = New System.Drawing.Point(8, 20)
        Me.dtpInicio.Name = "dtpInicio"
        Me.dtpInicio.Size = New System.Drawing.Size(114, 20)
        Me.dtpInicio.TabIndex = 334
        Me.dtpInicio.Tag = Nothing
        Me.dtpInicio.Value = New Date(2005, 5, 3, 0, 0, 0, 0)
        '
        'dtpFinal
        '
        '
        '
        '
        Me.dtpFinal.Calendar.DayNameLength = 1
        Me.dtpFinal.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpFinal.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpFinal.Location = New System.Drawing.Point(8, 44)
        Me.dtpFinal.Name = "dtpFinal"
        Me.dtpFinal.Size = New System.Drawing.Size(114, 20)
        Me.dtpFinal.TabIndex = 335
        Me.dtpFinal.Tag = Nothing
        Me.dtpFinal.Value = New Date(2005, 5, 3, 0, 0, 0, 0)
        '
        'Label4
        '
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(224, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(336, 22)
        Me.Label4.TabIndex = 338
        Me.Label4.Text = "Efectes del client:"
        '
        'cboDeLosClientes
        '
        Me.cboDeLosClientes.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboDeLosClientes.AutoCompletion = True
        Me.cboDeLosClientes.Caption = ""
        Me.cboDeLosClientes.CaptionHeight = 17
        Me.cboDeLosClientes.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboDeLosClientes.ColumnCaptionHeight = 17
        Me.cboDeLosClientes.ColumnFooterHeight = 17
        Me.cboDeLosClientes.ContentHeight = 16
        Me.cboDeLosClientes.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboDeLosClientes.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboDeLosClientes.EditorFont = New System.Drawing.Font("Tahoma", 8.25!)
        Me.cboDeLosClientes.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDeLosClientes.EditorHeight = 16
        Me.cboDeLosClientes.Images.Add(CType(resources.GetObject("cboDeLosClientes.Images"), System.Drawing.Image))
        Me.cboDeLosClientes.ItemHeight = 15
        Me.cboDeLosClientes.Location = New System.Drawing.Point(224, 40)
        Me.cboDeLosClientes.MatchEntryTimeout = CType(2000, Long)
        Me.cboDeLosClientes.MaxDropDownItems = CType(5, Short)
        Me.cboDeLosClientes.MaxLength = 32767
        Me.cboDeLosClientes.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboDeLosClientes.Name = "cboDeLosClientes"
        Me.cboDeLosClientes.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboDeLosClientes.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboDeLosClientes.Size = New System.Drawing.Size(336, 22)
        Me.cboDeLosClientes.TabIndex = 337
        Me.cboDeLosClientes.Text = "Escolleix un client"
        Me.cboDeLosClientes.PropBag = resources.GetString("cboDeLosClientes.PropBag")
        '
        'CheckBox1
        '
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.CheckBox1.Location = New System.Drawing.Point(428, 16)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(132, 23)
        Me.CheckBox1.TabIndex = 339
        Me.CheckBox1.Text = "De tots els clients"
        '
        'btnAñadirARemesa
        '
        Me.btnAñadirARemesa.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAñadirARemesa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAñadirARemesa.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAñadirARemesa.Location = New System.Drawing.Point(124, 16)
        Me.btnAñadirARemesa.Name = "btnAñadirARemesa"
        Me.btnAñadirARemesa.Size = New System.Drawing.Size(92, 49)
        Me.btnAñadirARemesa.TabIndex = 331
        Me.btnAñadirARemesa.TabStop = False
        Me.btnAñadirARemesa.Text = "Carregar efectes entre aquestes dates"
        '
        'XpGroupBox2
        '
        Me.XpGroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.XpGroupBox2.Controls.Add(Me.WButton1)
        Me.XpGroupBox2.Controls.Add(Me.dtpFecha1)
        Me.XpGroupBox2.Controls.Add(Me.dtpFecha2)
        Me.XpGroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.XpGroupBox2.Location = New System.Drawing.Point(620, 16)
        Me.XpGroupBox2.Name = "XpGroupBox2"
        Me.XpGroupBox2.Size = New System.Drawing.Size(280, 80)
        Me.XpGroupBox2.TabIndex = 340
        Me.XpGroupBox2.TabStop = False
        '
        'WButton1
        '
        Me.WButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.WButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.WButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.WButton1.Location = New System.Drawing.Point(128, 24)
        Me.WButton1.Name = "WButton1"
        Me.WButton1.Size = New System.Drawing.Size(112, 48)
        Me.WButton1.TabIndex = 336
        Me.WButton1.TabStop = False
        Me.WButton1.Text = "Afegir efectes no remesos entre aquestes dates"
        '
        'dtpFecha1
        '
        '
        '
        '
        Me.dtpFecha1.Calendar.DayNameLength = 1
        Me.dtpFecha1.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpFecha1.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpFecha1.Location = New System.Drawing.Point(8, 24)
        Me.dtpFecha1.Name = "dtpFecha1"
        Me.dtpFecha1.Size = New System.Drawing.Size(114, 20)
        Me.dtpFecha1.TabIndex = 337
        Me.dtpFecha1.Tag = Nothing
        Me.dtpFecha1.Value = New Date(2005, 5, 3, 0, 0, 0, 0)
        '
        'dtpFecha2
        '
        '
        '
        '
        Me.dtpFecha2.Calendar.DayNameLength = 1
        Me.dtpFecha2.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpFecha2.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpFecha2.Location = New System.Drawing.Point(8, 48)
        Me.dtpFecha2.Name = "dtpFecha2"
        Me.dtpFecha2.Size = New System.Drawing.Size(114, 20)
        Me.dtpFecha2.TabIndex = 338
        Me.dtpFecha2.Tag = Nothing
        Me.dtpFecha2.Value = New Date(2005, 5, 3, 0, 0, 0, 0)
        '
        'dgVenci
        '
        Me.dgVenci.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgVenci.Images.Add(CType(resources.GetObject("dgVenci.Images"), System.Drawing.Image))
        Me.dgVenci.Location = New System.Drawing.Point(104, 100)
        Me.dgVenci.Name = "dgVenci"
        Me.dgVenci.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgVenci.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgVenci.PreviewInfo.ZoomFactor = 75.0R
        Me.dgVenci.PrintInfo.PageSettings = CType(resources.GetObject("dgVenci.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgVenci.Size = New System.Drawing.Size(720, 137)
        Me.dgVenci.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgVenci.TabIndex = 336
        Me.dgVenci.Text = "C1TrueDBGrid1"
        Me.dgVenci.PropBag = resources.GetString("dgVenci.PropBag")
        '
        'dgRemesas
        '
        Me.dgRemesas.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgRemesas.Images.Add(CType(resources.GetObject("dgRemesas.Images"), System.Drawing.Image))
        Me.dgRemesas.Location = New System.Drawing.Point(20, 260)
        Me.dgRemesas.Name = "dgRemesas"
        Me.dgRemesas.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgRemesas.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgRemesas.PreviewInfo.ZoomFactor = 75.0R
        Me.dgRemesas.PrintInfo.PageSettings = CType(resources.GetObject("dgRemesas.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgRemesas.Size = New System.Drawing.Size(924, 219)
        Me.dgRemesas.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgRemesas.TabIndex = 332
        Me.dgRemesas.Text = "C1TrueDBGrid1"
        Me.dgRemesas.PropBag = resources.GetString("dgRemesas.PropBag")
        '
        'tabImpresion
        '
        Me.tabImpresion.Controls.Add(Me.ppRemesa)
        Me.tabImpresion.Location = New System.Drawing.Point(4, 22)
        Me.tabImpresion.Name = "tabImpresion"
        Me.tabImpresion.Size = New System.Drawing.Size(968, 487)
        Me.tabImpresion.TabIndex = 2
        Me.tabImpresion.Text = "Impresió"
        '
        'ppRemesa
        '
        Me.ppRemesa.Location = New System.Drawing.Point(84, 12)
        Me.ppRemesa.Name = "ppRemesa"
        '
        'ppRemesa.OutlineView
        '
        Me.ppRemesa.PreviewOutlineView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ppRemesa.PreviewOutlineView.Location = New System.Drawing.Point(0, 0)
        Me.ppRemesa.PreviewOutlineView.Name = "OutlineView"
        Me.ppRemesa.PreviewOutlineView.Size = New System.Drawing.Size(165, 427)
        Me.ppRemesa.PreviewOutlineView.TabIndex = 0
        '
        'ppRemesa.PreviewPane
        '
        Me.ppRemesa.PreviewPane.IntegrateExternalTools = True
        Me.ppRemesa.PreviewPane.TabIndex = 0
        '
        'ppRemesa.PreviewTextSearchPanel
        '
        Me.ppRemesa.PreviewTextSearchPanel.Dock = System.Windows.Forms.DockStyle.Right
        Me.ppRemesa.PreviewTextSearchPanel.Location = New System.Drawing.Point(530, 0)
        Me.ppRemesa.PreviewTextSearchPanel.MinimumSize = New System.Drawing.Size(200, 240)
        Me.ppRemesa.PreviewTextSearchPanel.Name = "PreviewTextSearchPanel"
        Me.ppRemesa.PreviewTextSearchPanel.Size = New System.Drawing.Size(200, 453)
        Me.ppRemesa.PreviewTextSearchPanel.TabIndex = 0
        Me.ppRemesa.PreviewTextSearchPanel.Visible = False
        '
        'ppRemesa.ThumbnailView
        '
        Me.ppRemesa.PreviewThumbnailView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ppRemesa.PreviewThumbnailView.Location = New System.Drawing.Point(0, 0)
        Me.ppRemesa.PreviewThumbnailView.Name = "ThumbnailView"
        Me.ppRemesa.PreviewThumbnailView.Size = New System.Drawing.Size(165, 351)
        Me.ppRemesa.PreviewThumbnailView.TabIndex = 0
        Me.ppRemesa.PreviewThumbnailView.UseImageAsThumbnail = False
        Me.ppRemesa.Size = New System.Drawing.Size(730, 424)
        Me.ppRemesa.TabIndex = 0
        Me.ppRemesa.Text = "C1PrintPreviewControl1"
        '
        'frmRemesas
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(992, 570)
        Me.Controls.Add(Me.tabCRemes)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "frmRemesas"
        Me.Text = "Remeses"
        Me.Controls.SetChildIndex(Me.btnActualizar, 0)
        Me.Controls.SetChildIndex(Me.btnModificar, 0)
        Me.Controls.SetChildIndex(Me.btnUltimo, 0)
        Me.Controls.SetChildIndex(Me.btnBorrar, 0)
        Me.Controls.SetChildIndex(Me.btnNuevo, 0)
        Me.Controls.SetChildIndex(Me.btnRecargar, 0)
        Me.Controls.SetChildIndex(Me.cboSeleccionCentro, 0)
        Me.Controls.SetChildIndex(Me.tabCRemes, 0)
        Me.Controls.SetChildIndex(Me.sbtipo, 0)
        Me.Controls.SetChildIndex(Me.btnTancar, 0)
        Me.Controls.SetChildIndex(Me.btnAnterior, 0)
        Me.Controls.SetChildIndex(Me.btnSiguiente, 0)
        Me.Controls.SetChildIndex(Me.btnVerLista, 0)
        Me.Controls.SetChildIndex(Me.btnPrimero, 0)
        Me.tabCRemes.ResumeLayout(False)
        Me.tabRemes.ResumeLayout(False)
        Me.XpGroupBox1.ResumeLayout(False)
        Me.XpGroupBox1.PerformLayout()
        CType(Me.txtNOMREMESOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLINEA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCODI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREMESOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMBANC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDOMREMESOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBANC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPROVREMESOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPOBREMESOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPREMESOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFECHA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOFICINA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCuentas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgCuentas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XpGroupBox4.ResumeLayout(False)
        Me.tabVENCIMREMES.ResumeLayout(False)
        Me.XpGroupBox3.ResumeLayout(False)
        Me.XpGroupBox5.ResumeLayout(False)
        Me.XpGroupBox5.PerformLayout()
        CType(Me.dtpInicio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFinal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDeLosClientes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XpGroupBox2.ResumeLayout(False)
        CType(Me.dtpFecha1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFecha2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgVenci, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgRemesas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabImpresion.ResumeLayout(False)
        CType(Me.ppRemesa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ppRemesa.ResumeLayout(False)
        Me.ppRemesa.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmRemesas
    Public Shared Function GetInstance() As frmRemesas
        If frmChildForm Is Nothing Then
            frmChildForm = New frmRemesas

        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Private remesaActual As clsRemesa
    Private ponercomonoremes As Boolean = False

#End Region

#Region "INICIALIZAR"

    Private Sub HacerBindings()
        Try
            HacerBindingsTodos(remesaActual.dvForm)
            AñadirBinding(cboCODI, remesaActual.dvForm, "CODI")

            AñadirBindingCombo(cboCODI, remesaActual.dvIdentificadores, "CODI", "CODI")
            AñadirBindingCombo(cboNOMBANC, remesaActual.dtBancs, CCBancs, CNBancs)
            AñadirBindingCombo(cboDeLosClientes, remesaActual.dtClients, "CODI", "NOM")

            dgRemesas.SetDataBinding(remesaActual.lineasRemesa.dvForm, "")
            dgCuentas.SetDataBinding(remesaActual.cuentasRemesa.dvForm, "")

            'AñadirBinding(txt, dvRemesas, "POBREMESOR")

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True

            Me.arrayCombos = New System.Windows.Forms.Control() {Me.cboNOMBANC}
            Me.arrayTabPages = New System.Windows.Forms.Control() {Me.tabRemes}
            Me.arrayBotones = New System.Windows.Forms.Control() {Me.WButton1, Me.btnAñadirARemesa, Me.btnElegirBanco, Me.btnElegirCliente}
            Me.arrayGrids = New System.Windows.Forms.Control() {Me.dgCuentas, Me.dgRemesas, Me.dgVenci}
            Me.arrayTextBox = New System.Windows.Forms.Control() {Me.txtBANC, Me.txtCPREMESOR, Me.txtDOMREMESOR, Me.txtLINEA, Me.txtOFICINA, Me.txtPOBREMESOR, Me.txtPROVREMESOR, Me.txtREMESOR, Me.txtNOMREMESOR}
            Me.arrayDateTime = New System.Windows.Forms.Control() {Me.dtpFECHA, Me.dtpFecha1, Me.dtpFecha2, Me.dtpFinal, Me.dtpInicio}
            Me.arrayRadios = New System.Windows.Forms.Control() {Me.rdoCOBRO, Me.rdoDESCUENTO}
            remesaActual = New clsRemesa(ds.Tables("REMESAS"), empresaPorDefecto, Me.BindingContext)
            HacerBindings()
            IniciarDgRemesas()
            IniciarDGCuentas()
            PonerModificables(soloLectura)
            MoverActual()

            PSBTIPO(remesaActual.centro) : cargando = False
            PonerHandlersErroresParaGrids()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub

#End Region

#Region "MODIFICAR DB"

    Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnBorrar.Click
        Try
            If MessageBox.Show(rm.GetString("BorrarRemesa"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                cargando = True
                editando = True
                remesaActual.borrar() : ActualizarOrigen()
                PSBTIPO(remesaActual.centro) : cargando = False
                editando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnNuevo.Click
        Try
            If consulta() Then
                cargando = True
                EsRegistroNuevo = True
                'QuitarTabPages()
                PonerModificables(modificable)
                PonerControlesNuevo(True)
                remesaActual.NuevoRegistro()

                'se le pasa esto pq todavia no sabemos que hilo es el que se va a introducir
                MoverActual(True)
                PSBTIPO(remesaActual.centro) : cargando = False
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
                PSBTIPO(remesaActual.centro)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub ActualizarOrigen()
        Try
            If editando Then
                editando = False
                'remesaActual.tabla.AcceptChanges()
                PonerModificables(soloLectura)
            End If
            If EsRegistroNuevo Then
                editando = True
                PonerControlesNuevo(False)
                PonerModificables(modificable)
                'PonerTabPages()
                EsRegistroNuevo = False
                MoverActual()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnActualizar.Click
        Try
            cargando = True
            If remesaActual.TieneCambios Then
                Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                    Case DialogResult.Cancel
                        cargando = False
                        Exit Sub
                    Case DialogResult.No
                        remesaActual.tabla.RejectChanges()
                        cargando = False
                        Exit Sub
                End Select
            End If

            remesaActual.ActualizarOrigen() : ActualizarOrigen()
            If Not desplazando Then PSBTIPO(remesaActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ESPECÍFICO"

    Private Sub IniciarDGCuentas()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgCuentas)
            i = 0
            AñadirBindingCombo(cboCuentas, remesaActual.dtCuentas, "CUENTA", "CUENTA")
            OcultarMostrarColumnaCbo(cboCuentas, "TIPO", False)
            PPCol2("CUENTA", dgCuentas, rm.GetString("CUENTA"), "", True, 200, _
                        False, C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox, False, 200, i, False, cboCuentas)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub IniciarDGVencim()
        Dim i As Integer
        Try
            dgVenci.SetDataBinding(remesaActual.dvVencimientos, "")
            OcultarColumnasDG(dgVenci)
            i = 0

            PPCol2("FRA", dgVenci, rm.GetString("NUMEFECTO"), "", True, 80, _
                        False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 80, i, False)
            i = i + 1
            PPCol2("NOMCLIENT", dgVenci, rm.GetString("NOMCLIENT"), "", True, 125, _
                        False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 125, i, False)
            i = i + 1
            PPCol2("PLAZA", dgVenci, rm.GetString("PLAZA"), "", True, 125, _
                        False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 125, i, False)
            i = i + 1
            PPCol2("VENCIM", dgVenci, rm.GetString("VENCIMIENTO"), "Short Date", True, 125, _
                        False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 125, i, False)
            i = i + 1
            PPCol2("REMES", dgVenci, rm.GetString("REMESADO"), "", True, 125, _
                        False, C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox, False, 125, i, False)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub IniciarDgRemesas()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgRemesas)
            i = 0

            PPCol2("NEFECTO", dgRemesas, rm.GetString("NUMEFECTO"), "", True, 125, _
                        False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 125, i, False)
            i = i + 1
            'AñadirTextButton("ELEGIREFECTO", "", 17, ts, dgRemesas, colElegirEfecto)

            PPCol2("NOMCLIENT", dgRemesas, rm.GetString("NOMCLIENT"), "", True, 250, _
                False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 250, i, False)

            PPCol2("PLAZA", dgRemesas, rm.GetString("PLAZA"), "", True, 250, _
                False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 250, i, False)

            PPCol2("IMPORT", dgRemesas, rm.GetString("IMPORTE"), "", True, 100, _
                False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 100, i, False)

            PPCol2("VENCIM", dgRemesas, "Venciment", "Short Date", True, 100, _
                False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 100, i, False)

            'AñadirCheckBoxColumn("REMES", "Ya remes", 50, ts, True, colRemes, dgRemesas)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Private Sub dgCuentas_BeforeColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs) Handles dgVenci.BeforeColUpdate
        Try
            If Not cargando Then
                cargando = True
                Select Case e.Column.DataColumn.DataField
                    Case "REMES"
                        remesaActual.Remetir()
                        btnRecargar_Click(Nothing, Nothing)
                End Select
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub dgRemesas_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgRemesas.BeforeDelete
        Try
            If MessageBox.Show(rm.GetString("CONFELIMLINREMESA"), "", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                remesaActual.lineasRemesa.borrarLineaActual()
            Else
                e.Cancel = True
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Try
            If CheckBox1.Checked = False Then
                cboDeLosClientes.Visible = True
            Else
                cboDeLosClientes.Visible = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub tabCRemes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabCRemes.SelectedIndexChanged
        Try
            If tabCRemes.SelectedTab Is tabImpresion Then
                ppRemesa.Document = remesaActual.imprimirRemesa
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub PonerFotoBanco(ByVal cod As String)
        Dim obj As Object
        Try
            Dim cmdSel As New MySqlCommand("SELECT LOGO FROM BANCS WHERE CODI = """ & cod & """ ", cnn)

            ACN()
            obj = cmdSel.ExecuteScalar
            CCN()
            If Not IsDBNull(obj) Then
                If Not obj Is Nothing Then
                    ' e.Value is the original value

                    Dim img() As Byte = CType(obj, Byte())
                    ' Performs the conversion
                    Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream
                    Dim offset As Integer = 0 ' 78
                    ms.Write(img, offset, img.Length - offset)
                    Dim bmp As Bitmap = New Bitmap(ms)
                    ms.Close()
                    ' pibLOGO.Image = bmp
                Else
                    'pibLOGO.Image = Nothing
                End If
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Function ComprobarFechas(ByVal d1 As Date, ByVal d2 As Date) As Object
        '! Changed ComprobarFechas to Object
        Try
            If d1.CompareTo(d2) <= 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Sub btnAñadirARemesa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAñadirARemesa.Click
        Try
            If CheckBox1.Checked = True Then
                remesaActual.CargarVencimientos(GENERAL.nz(dtpInicio.Value, ""), general.nz(dtpFinal.Value, ""))
            Else
                remesaActual.CargarVencimientos(GENERAL.nz(dtpInicio.Value, ""), general.nz(dtpFinal.Value, ""), nzn(cboDeLosClientes.SelectedValue, 0))
            End If
            dgVenci.SetDataBinding(remesaActual.dvVencimientos, "")
            IniciarDGVencim()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub WButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WButton1.Click
        Try
            If ComprobarFechas(dtpInicio.Value, dtpFinal.Value) Then
                'messagebox.Show(rm.GetString("QUIERESDESCARGAR")
                If CheckBox1.Checked = True Then
                    remesaActual.RemetirIntervalo(GENERAL.nz(dtpFecha1.Value, ""), general.nz(dtpFecha2.Value, ""))
                Else
                    remesaActual.RemetirIntervalo(GENERAL.nz(dtpFecha1.Value, ""), general.nz(dtpFecha2.Value, ""), nzn(cboDeLosClientes.SelectedValue, 0))
                End If
                'Else
                'MessageBox.Show("Interval de dates incorrecte")
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub txtBANC_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBANC.Validated
        Try
            If PuedoModificar() Then
                cargando = True
                remesaActual.BANC = general.nz(txtBANC.Text, "")
                MoverActual()
                'PonerFotoBanco(remesaActual.BANC)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub rdoCOBRO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoCOBRO.CheckedChanged
        Try
            If PuedoModificar() Then
                cargando = True
                If rdoCOBRO.Checked = True Then
                    remesaActual.COBRO = True
                    remesaActual.DESCUENTO = False
                End If
                If remesaActual.DESCUENTO = True Then
                    remesaActual.dtCuentas.DefaultView.RowFilter = "TIPO = 'C'"
                    remesaActual.cuentasRemesa.dvForm.Table.Clear()
                Else
                    remesaActual.dtCuentas.DefaultView.RowFilter = "TIPO = 'D'"
                    remesaActual.cuentasRemesa.dvForm.Table.Clear()
                End If
                PSBTIPO(remesaActual.centro) : cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub rdoDESCUENTO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoDESCUENTO.CheckedChanged
        Try
            If PuedoModificar() Then
                cargando = True
                If rdoDESCUENTO.Checked = True Then
                    remesaActual.COBRO = False
                    remesaActual.DESCUENTO = True
                End If
                If remesaActual.DESCUENTO = True Then
                    remesaActual.dtCuentas.DefaultView.RowFilter = "TIPO = 'C'"
                    remesaActual.cuentasRemesa.dvForm.Table.Clear()
                Else
                    remesaActual.dtCuentas.DefaultView.RowFilter = "TIPO = 'D'"
                    remesaActual.cuentasRemesa.dvForm.Table.Clear()
                End If
                PSBTIPO(remesaActual.centro) : cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub

#End Region

#Region "IMPRESIÓN"

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'remesaActual.ImprimirRemesa()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
            CCN()

        End Try

    End Sub

#End Region

#Region "COMUNES"

    Private Sub PonerModificables(Optional ByVal b As Boolean = False)
        Try
            PonerMod(b)
            If Not b Then : cboCODI.DataSource = Nothing
            Else : AñadirBindingCombo(cboCODI, remesaActual.dvIdentificadores, "CODI", "CODI") : remesaActual.tabla.AcceptChanges() : End If

            cboCODI.LimitToList = b
            cboCODI.SuperBack = b
            cboCODI.ReadOnly = editando Or EsRegistroNuevo

            txtCPREMESOR.ReadOnly = True
            txtDOMREMESOR.ReadOnly = True
            txtPOBREMESOR.ReadOnly = True
            txtPROVREMESOR.ReadOnly = True
            txtREMESOR.ReadOnly = True

            ModiNuev(b)

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

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Sub btnRecargar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecargar.Click
        Try
            cargando = True
            Dim id As Object
            id = cboCODI.Text
            remesaActual.ActualizarOrigen()
            remesaActual.CambiarAReg(id, iraregistro)
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
            cargando = True
            remesaActual.MoverActual()
            'dgCuentas.Rebind(True)
            'dgCuentas.SetDataBinding(remesaActual.cuentasRemesa.dvForm, "")
            PSBTIPO(remesaActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSiguiente.Click
        Try
            cargando = True
            remesaActual.SiguienteReg() ': ActualizarOrigen()
            MoverActual()
            PSBTIPO(remesaActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUltimo.Click
        Try
            cargando = True
            remesaActual.UltimoReg() : ActualizarOrigen()
            MoverActual()
            PSBTIPO(remesaActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAnterior.Click
        Try
            cargando = True
            remesaActual.AnteriorReg() : ActualizarOrigen()
            MoverActual()
            PSBTIPO(remesaActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnPrimero.Click
        Try
            cargando = True
            remesaActual.PrimeroReg() : ActualizarOrigen()
            MoverActual()
            PSBTIPO(remesaActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "SELECCIÓN REGISTRO"

#Region "SELECCION CENTRO"

    Private Sub cboSeleccion_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSeleccionCentro.SelectionChangeCommitted
        Try
            If consulta() Then
                cargando = True
                remesaActual.cambioCentro(GENERAL.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto), primero)
                'remesaActual.tabla.AcceptChanges()
                cboSeleccionCentro.SelectedValue = remesaActual.centro
                MoverActual()
                cargando = False
            Else
                If EsRegistroNuevo And Not cargando Then
                    cargando = True
                    remesaActual.centro = general.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto)
                    remesaActual.tabla.Clear()
                    remesaActual.NuevoRegistro()
                    PSBTIPO(remesaActual.centro)
                    MoverActual()
                    cargando = False
                End If
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

    Private Sub cboCODI_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboCODI.KeyPress
        Try
            If editando Or EsRegistroNuevo Then : cboCODI.AutoCompletion = False
            Else : cboCODI.AutoCompletion = True : End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub cboCODI_ItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCODI.SelectedValueChanged
        Try
            If consulta() Then
                cargando = True
                remesaActual.CambiarAReg(nzn(cboCODI.WillChangeToValue, 0), iraregistro)
                MoverActual()
                PSBTIPO(remesaActual.centro) : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ELECCIONES"

    Private Sub btnElegirBanco_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirBanco.Click
        Try
            'RellenarListaDescuento()
            'RellenarListaCobro()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboNombreBanco_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMBANC.SelectedValueChanged
        Try
            If PuedoModificar() Then
                cargando = True
                remesaActual.BANC = general.nz(cboNOMBANC.WillChangeToValue, "")
                MoverActual()
                'PonerFotoBanco(remesaActual.BANC)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub txtREMESOR_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtREMESOR.Validated
        Try
            If PuedoModificar() Then cargando = True : remesaActual.REMESOR = nzn(txtREMESOR.Text, 0) : btnRecargar_Click(Nothing, Nothing) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub

#End Region

#Region "ORGANIZAR"

    Private Sub dgCuentas_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCuentas.AfterUpdate
        Try
            AutoSizeCC(dgCuentas)
            remesaActual.OFICINA = cboCuentas.Columns("DOM").Value & " " & cboCuentas.Columns("POB").Value & " " & cboCuentas.Columns("CP").Value

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#End Region

    Private Sub dgCuentas_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgCuentas.BeforeDelete
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
    Private Sub dgVenci_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgVenci.BeforeDelete
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