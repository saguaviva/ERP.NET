Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Imports C1.Win.C1TrueDBGrid


Public Class frmDisposicion
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

    'System.Windows.Forms.DateTimePicker
    
    Friend WithEvents btnImprimir As C1.Win.C1Input.C1Button
    Friend WithEvents cboTejedor As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents cboTejido As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents dgDdispos As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents txtTotalPiezas As C1.Win.C1Input.C1TextBox
    Friend WithEvents lblTotalKGS As Label
    Friend WithEvents lblTotalPiezas As Label
    Friend WithEvents gb1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblFechaRecibido As Label
    Friend WithEvents lblFecha As Label
    Friend WithEvents lblDisposicion As Label
    Friend WithEvents lblCliente As Label
    Friend WithEvents btnElegirCliente As C1.Win.C1Input.C1Button
    Friend WithEvents lblAcabador As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents tabControlDisposiciones As System.Windows.Forms.TabControl
    Friend WithEvents tabPageDisposicion As System.Windows.Forms.TabPage
    Friend WithEvents lblPedido As Label
    Friend WithEvents btnElegirAcabador As C1.Win.C1Input.C1Button
    Friend WithEvents btnEscogerColorExistente As C1.Win.C1Input.C1Button
    Friend WithEvents txtColor As C1.Win.C1Input.C1TextBox
    Friend WithEvents dtpDRECEPCION As C1.Win.C1Input.C1DateEdit
    Friend WithEvents dtpFECHA As C1.Win.C1Input.C1DateEdit
    Friend WithEvents txtany As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtcolorcliente As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTotalKG As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtOBSERV As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCOMANDA As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboNOMCLIENT As C1.Win.C1List.C1Combo
    Friend WithEvents cboNOMACABADOR As C1.Win.C1List.C1Combo
    Friend WithEvents txtACABADOR As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCODICLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents chkANULADA As System.Windows.Forms.CheckBox
    Friend WithEvents chkRECIBIDO As System.Windows.Forms.CheckBox
    Friend WithEvents btnElegirAcabadorPorDefecto As C1.Win.C1Input.C1Button
    Friend WithEvents cboIDDispos As C1.Win.C1List.C1Combo

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Style1 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style2 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style3 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style4 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim Style5 As C1.Win.C1TrueDBGrid.Style = New C1.Win.C1TrueDBGrid.Style()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDisposicion))
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
        Me.btnImprimir = New C1.Win.C1Input.C1Button()
        Me.cboTejedor = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.cboTejido = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown()
        Me.tabControlDisposiciones = New System.Windows.Forms.TabControl()
        Me.tabPageDisposicion = New System.Windows.Forms.TabPage()
        Me.dgDdispos = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.txtOBSERV = New C1.Win.C1Input.C1TextBox()
        Me.txtTotalPiezas = New C1.Win.C1Input.C1TextBox()
        Me.txtTotalKG = New C1.Win.C1Input.C1TextBox()
        Me.lblTotalKGS = New System.Windows.Forms.Label()
        Me.lblTotalPiezas = New System.Windows.Forms.Label()
        Me.gb1 = New System.Windows.Forms.GroupBox()
        Me.cboIDDispos = New C1.Win.C1List.C1Combo()
        Me.btnElegirAcabadorPorDefecto = New C1.Win.C1Input.C1Button()
        Me.dtpFECHA = New C1.Win.C1Input.C1DateEdit()
        Me.dtpDRECEPCION = New C1.Win.C1Input.C1DateEdit()
        Me.txtColor = New C1.Win.C1Input.C1TextBox()
        Me.btnEscogerColorExistente = New C1.Win.C1Input.C1Button()
        Me.chkANULADA = New System.Windows.Forms.CheckBox()
        Me.btnElegirAcabador = New C1.Win.C1Input.C1Button()
        Me.lblPedido = New System.Windows.Forms.Label()
        Me.txtCOMANDA = New C1.Win.C1Input.C1TextBox()
        Me.cboNOMCLIENT = New C1.Win.C1List.C1Combo()
        Me.cboNOMACABADOR = New C1.Win.C1List.C1Combo()
        Me.chkRECIBIDO = New System.Windows.Forms.CheckBox()
        Me.lblFechaRecibido = New System.Windows.Forms.Label()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.txtCODICLIENT = New C1.Win.C1Input.C1TextBox()
        Me.txtany = New C1.Win.C1Input.C1TextBox()
        Me.lblDisposicion = New System.Windows.Forms.Label()
        Me.txtCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.btnElegirCliente = New C1.Win.C1Input.C1Button()
        Me.txtACABADOR = New C1.Win.C1Input.C1TextBox()
        Me.lblAcabador = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtcolorcliente = New C1.Win.C1Input.C1TextBox()
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
        CType(Me.btnImprimir, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTejedor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTejido, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControlDisposiciones.SuspendLayout()
        Me.tabPageDisposicion.SuspendLayout()
        CType(Me.dgDdispos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalPiezas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalKG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gb1.SuspendLayout()
        CType(Me.cboIDDispos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnElegirAcabadorPorDefecto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFECHA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDRECEPCION, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtColor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnEscogerColorExistente, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnElegirAcabador, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCOMANDA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMACABADOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCODICLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnElegirCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtACABADOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcolorcliente, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(5, 183)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(60, 56)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(330, 56)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(28, 56)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(28, 36)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(330, 36)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(150, 36)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(560, 112)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(150, 56)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(248, 36)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(60, 36)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(560, 92)
        '
        'btnImprimir
        '
        Me.btnImprimir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImprimir.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnImprimir.Location = New System.Drawing.Point(818, 575)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(72, 25)
        Me.btnImprimir.TabIndex = 9
        Me.btnImprimir.Text = "Imprimir"
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'cboTejedor
        '
        Me.cboTejedor.AllowColMove = True
        Me.cboTejedor.AllowColSelect = True
        Me.cboTejedor.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboTejedor.AlternatingRows = True
        Me.cboTejedor.CaptionStyle = Style1
        Me.cboTejedor.ColumnCaptionHeight = 17
        Me.cboTejedor.ColumnFooterHeight = 17
        Me.cboTejedor.EvenRowStyle = Style2
        Me.cboTejedor.ExtendRightColumn = True
        Me.cboTejedor.FetchRowStyles = True
        Me.cboTejedor.FooterStyle = Style3
        Me.cboTejedor.HeadingStyle = Style4
        Me.cboTejedor.HighLightRowStyle = Style5
        Me.cboTejedor.Images.Add(CType(resources.GetObject("cboTejedor.Images"), System.Drawing.Image))
        Me.cboTejedor.Location = New System.Drawing.Point(24, 168)
        Me.cboTejedor.Name = "cboTejedor"
        Me.cboTejedor.OddRowStyle = Style6
        Me.cboTejedor.RecordSelectorStyle = Style7
        Me.cboTejedor.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboTejedor.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboTejedor.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboTejedor.ScrollTips = False
        Me.cboTejedor.Size = New System.Drawing.Size(156, 156)
        Me.cboTejedor.Style = Style8
        Me.cboTejedor.TabIndex = 259
        Me.cboTejedor.TabStop = False
        Me.cboTejedor.Text = "C1TrueDBDropdown1"
        Me.cboTejedor.UseCompatibleTextRendering = False
        Me.cboTejedor.ValueTranslate = True
        Me.cboTejedor.Visible = False
        Me.cboTejedor.PropBag = resources.GetString("cboTejedor.PropBag")
        '
        'cboTejido
        '
        Me.cboTejido.AllowColMove = False
        Me.cboTejido.AllowColSelect = True
        Me.cboTejido.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.cboTejido.AlternatingRows = True
        Me.cboTejido.CaptionStyle = Style9
        Me.cboTejido.ColumnCaptionHeight = 17
        Me.cboTejido.ColumnFooterHeight = 17
        Me.cboTejido.EvenRowStyle = Style10
        Me.cboTejido.ExtendRightColumn = True
        Me.cboTejido.FetchRowStyles = True
        Me.cboTejido.FooterStyle = Style11
        Me.cboTejido.HeadingStyle = Style12
        Me.cboTejido.HighLightRowStyle = Style13
        Me.cboTejido.Images.Add(CType(resources.GetObject("cboTejido.Images"), System.Drawing.Image))
        Me.cboTejido.Location = New System.Drawing.Point(188, 168)
        Me.cboTejido.Name = "cboTejido"
        Me.cboTejido.OddRowStyle = Style14
        Me.cboTejido.RecordSelectorStyle = Style15
        Me.cboTejido.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboTejido.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        Me.cboTejido.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboTejido.ScrollTips = False
        Me.cboTejido.Size = New System.Drawing.Size(160, 156)
        Me.cboTejido.Style = Style16
        Me.cboTejido.TabIndex = 260
        Me.cboTejido.TabStop = False
        Me.cboTejido.Text = "C1TrueDBDropdown1"
        Me.cboTejido.UseCompatibleTextRendering = False
        Me.cboTejido.ValueTranslate = True
        Me.cboTejido.Visible = False
        Me.cboTejido.PropBag = resources.GetString("cboTejido.PropBag")
        '
        'tabControlDisposiciones
        '
        Me.tabControlDisposiciones.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabControlDisposiciones.Controls.Add(Me.tabPageDisposicion)
        Me.tabControlDisposiciones.ItemSize = New System.Drawing.Size(60, 18)
        Me.tabControlDisposiciones.Location = New System.Drawing.Point(4, 4)
        Me.tabControlDisposiciones.Name = "tabControlDisposiciones"
        Me.tabControlDisposiciones.SelectedIndex = 0
        Me.tabControlDisposiciones.Size = New System.Drawing.Size(980, 544)
        Me.tabControlDisposiciones.TabIndex = 261
        '
        'tabPageDisposicion
        '
        Me.tabPageDisposicion.Controls.Add(Me.cboTejedor)
        Me.tabPageDisposicion.Controls.Add(Me.cboTejido)
        Me.tabPageDisposicion.Controls.Add(Me.dgDdispos)
        Me.tabPageDisposicion.Controls.Add(Me.txtOBSERV)
        Me.tabPageDisposicion.Controls.Add(Me.txtTotalPiezas)
        Me.tabPageDisposicion.Controls.Add(Me.txtTotalKG)
        Me.tabPageDisposicion.Controls.Add(Me.lblTotalKGS)
        Me.tabPageDisposicion.Controls.Add(Me.lblTotalPiezas)
        Me.tabPageDisposicion.Controls.Add(Me.gb1)
        Me.tabPageDisposicion.Location = New System.Drawing.Point(4, 22)
        Me.tabPageDisposicion.Name = "tabPageDisposicion"
        Me.tabPageDisposicion.Size = New System.Drawing.Size(972, 518)
        Me.tabPageDisposicion.TabIndex = 0
        Me.tabPageDisposicion.Text = "Disposició"
        '
        'dgDdispos
        '
        Me.dgDdispos.AllowAddNew = True
        Me.dgDdispos.AllowDelete = True
        Me.dgDdispos.AllowSort = False
        Me.dgDdispos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgDdispos.Caption = "TEIXITS"
        Me.dgDdispos.CaptionHeight = 17
        Me.dgDdispos.ExtendRightColumn = True
        Me.dgDdispos.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgDdispos.Images.Add(CType(resources.GetObject("dgDdispos.Images"), System.Drawing.Image))
        Me.dgDdispos.LinesPerRow = 2
        Me.dgDdispos.Location = New System.Drawing.Point(8, 136)
        Me.dgDdispos.Name = "dgDdispos"
        Me.dgDdispos.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgDdispos.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgDdispos.PreviewInfo.ZoomFactor = 75.0R
        Me.dgDdispos.PrintInfo.PageSettings = CType(resources.GetObject("dgDdispos.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgDdispos.RowHeight = 30
        Me.dgDdispos.Size = New System.Drawing.Size(956, 260)
        Me.dgDdispos.SpringMode = True
        Me.dgDdispos.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation
        Me.dgDdispos.TabIndex = 267
        Me.dgDdispos.Text = "C1TrueDBGrid1"
        Me.dgDdispos.UseCompatibleTextRendering = False
        Me.dgDdispos.WrapCellPointer = True
        Me.dgDdispos.PropBag = resources.GetString("dgDdispos.PropBag")
        '
        'txtOBSERV
        '
        Me.txtOBSERV.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOBSERV.Location = New System.Drawing.Point(12, 408)
        Me.txtOBSERV.Multiline = True
        Me.txtOBSERV.Name = "txtOBSERV"
        Me.txtOBSERV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOBSERV.Size = New System.Drawing.Size(776, 96)
        Me.txtOBSERV.TabIndex = 266
        Me.txtOBSERV.Tag = Nothing
        '
        'txtTotalPiezas
        '
        Me.txtTotalPiezas.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTotalPiezas.Location = New System.Drawing.Point(904, 408)
        Me.txtTotalPiezas.Name = "txtTotalPiezas"
        Me.txtTotalPiezas.Size = New System.Drawing.Size(60, 18)
        Me.txtTotalPiezas.TabIndex = 265
        Me.txtTotalPiezas.Tag = Nothing
        Me.txtTotalPiezas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalKG
        '
        Me.txtTotalKG.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTotalKG.Location = New System.Drawing.Point(904, 436)
        Me.txtTotalKG.Name = "txtTotalKG"
        Me.txtTotalKG.Size = New System.Drawing.Size(60, 18)
        Me.txtTotalKG.TabIndex = 264
        Me.txtTotalKG.Tag = Nothing
        Me.txtTotalKG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTotalKGS
        '
        Me.lblTotalKGS.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalKGS.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTotalKGS.Location = New System.Drawing.Point(824, 440)
        Me.lblTotalKGS.Name = "lblTotalKGS"
        Me.lblTotalKGS.Size = New System.Drawing.Size(80, 18)
        Me.lblTotalKGS.TabIndex = 263
        Me.lblTotalKGS.Text = "Total kgs."
        '
        'lblTotalPiezas
        '
        Me.lblTotalPiezas.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalPiezas.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTotalPiezas.Location = New System.Drawing.Point(824, 408)
        Me.lblTotalPiezas.Name = "lblTotalPiezas"
        Me.lblTotalPiezas.Size = New System.Drawing.Size(80, 18)
        Me.lblTotalPiezas.TabIndex = 262
        Me.lblTotalPiezas.Text = "Total Peces"
        '
        'gb1
        '
        Me.gb1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gb1.Controls.Add(Me.cboIDDispos)
        Me.gb1.Controls.Add(Me.btnElegirAcabadorPorDefecto)
        Me.gb1.Controls.Add(Me.dtpFECHA)
        Me.gb1.Controls.Add(Me.dtpDRECEPCION)
        Me.gb1.Controls.Add(Me.txtColor)
        Me.gb1.Controls.Add(Me.btnEscogerColorExistente)
        Me.gb1.Controls.Add(Me.chkANULADA)
        Me.gb1.Controls.Add(Me.btnElegirAcabador)
        Me.gb1.Controls.Add(Me.lblPedido)
        Me.gb1.Controls.Add(Me.txtCOMANDA)
        Me.gb1.Controls.Add(Me.cboNOMCLIENT)
        Me.gb1.Controls.Add(Me.cboNOMACABADOR)
        Me.gb1.Controls.Add(Me.chkRECIBIDO)
        Me.gb1.Controls.Add(Me.lblFechaRecibido)
        Me.gb1.Controls.Add(Me.lblFecha)
        Me.gb1.Controls.Add(Me.txtCODICLIENT)
        Me.gb1.Controls.Add(Me.txtany)
        Me.gb1.Controls.Add(Me.lblDisposicion)
        Me.gb1.Controls.Add(Me.txtCLIENT)
        Me.gb1.Controls.Add(Me.lblCliente)
        Me.gb1.Controls.Add(Me.btnElegirCliente)
        Me.gb1.Controls.Add(Me.txtACABADOR)
        Me.gb1.Controls.Add(Me.lblAcabador)
        Me.gb1.Controls.Add(Me.Label3)
        Me.gb1.Controls.Add(Me.Label2)
        Me.gb1.Controls.Add(Me.txtcolorcliente)
        Me.gb1.Location = New System.Drawing.Point(8, 4)
        Me.gb1.Name = "gb1"
        Me.gb1.Size = New System.Drawing.Size(968, 124)
        Me.gb1.TabIndex = 261
        Me.gb1.TabStop = False
        '
        'cboIDDispos
        '
        Me.cboIDDispos.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboIDDispos.AutoCompletion = True
        Me.cboIDDispos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboIDDispos.Caption = ""
        Me.cboIDDispos.CaptionHeight = 17
        Me.cboIDDispos.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboIDDispos.ColumnCaptionHeight = 17
        Me.cboIDDispos.ColumnFooterHeight = 17
        Me.cboIDDispos.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboIDDispos.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboIDDispos.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboIDDispos.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboIDDispos.Images.Add(CType(resources.GetObject("cboIDDispos.Images"), System.Drawing.Image))
        Me.cboIDDispos.ItemHeight = 15
        Me.cboIDDispos.Location = New System.Drawing.Point(160, 17)
        Me.cboIDDispos.MatchEntryTimeout = CType(2000, Long)
        Me.cboIDDispos.MaxDropDownItems = CType(15, Short)
        Me.cboIDDispos.MaxLength = 10
        Me.cboIDDispos.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboIDDispos.Name = "cboIDDispos"
        Me.cboIDDispos.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboIDDispos.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboIDDispos.Size = New System.Drawing.Size(68, 19)
        Me.cboIDDispos.TabIndex = 281
        Me.cboIDDispos.PropBag = resources.GetString("cboIDDispos.PropBag")
        '
        'btnElegirAcabadorPorDefecto
        '
        Me.btnElegirAcabadorPorDefecto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnElegirAcabadorPorDefecto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirAcabadorPorDefecto.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirAcabadorPorDefecto.Location = New System.Drawing.Point(472, 40)
        Me.btnElegirAcabadorPorDefecto.Name = "btnElegirAcabadorPorDefecto"
        Me.btnElegirAcabadorPorDefecto.Size = New System.Drawing.Size(64, 44)
        Me.btnElegirAcabadorPorDefecto.TabIndex = 280
        Me.btnElegirAcabadorPorDefecto.Text = "Escollir Acabador Per Defecte"
        Me.btnElegirAcabadorPorDefecto.UseVisualStyleBackColor = True
        '
        'dtpFECHA
        '
        Me.dtpFECHA.AllowSpinLoop = False
        Me.dtpFECHA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        '
        '
        '
        Me.dtpFECHA.Calendar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFECHA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpFECHA.DisableOnNoData = False
        Me.dtpFECHA.EmptyAsNull = True
        Me.dtpFECHA.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpFECHA.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dtpFECHA.Location = New System.Drawing.Point(808, 40)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(148, 18)
        Me.dtpFECHA.TabIndex = 279
        Me.dtpFECHA.Tag = Nothing
        '
        'dtpDRECEPCION
        '
        Me.dtpDRECEPCION.AllowSpinLoop = False
        Me.dtpDRECEPCION.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        '
        '
        '
        Me.dtpDRECEPCION.Calendar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDRECEPCION.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpDRECEPCION.DisableOnNoData = False
        Me.dtpDRECEPCION.EmptyAsNull = True
        Me.dtpDRECEPCION.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpDRECEPCION.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.dtpDRECEPCION.Location = New System.Drawing.Point(808, 64)
        Me.dtpDRECEPCION.Name = "dtpDRECEPCION"
        Me.dtpDRECEPCION.Size = New System.Drawing.Size(148, 18)
        Me.dtpDRECEPCION.TabIndex = 278
        Me.dtpDRECEPCION.Tag = Nothing
        Me.dtpDRECEPCION.Visible = False
        '
        'txtColor
        '
        Me.txtColor.Location = New System.Drawing.Point(100, 88)
        Me.txtColor.MaxLength = 10
        Me.txtColor.Name = "txtColor"
        Me.txtColor.Size = New System.Drawing.Size(140, 18)
        Me.txtColor.TabIndex = 270
        Me.txtColor.Tag = Nothing
        '
        'btnEscogerColorExistente
        '
        Me.btnEscogerColorExistente.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEscogerColorExistente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEscogerColorExistente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnEscogerColorExistente.Location = New System.Drawing.Point(248, 88)
        Me.btnEscogerColorExistente.Name = "btnEscogerColorExistente"
        Me.btnEscogerColorExistente.Size = New System.Drawing.Size(120, 23)
        Me.btnEscogerColorExistente.TabIndex = 269
        Me.btnEscogerColorExistente.Text = "Escollir Color Existent"
        Me.btnEscogerColorExistente.UseVisualStyleBackColor = True
        '
        'chkANULADA
        '
        Me.chkANULADA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkANULADA.BackColor = System.Drawing.Color.Transparent
        Me.chkANULADA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkANULADA.Location = New System.Drawing.Point(556, 92)
        Me.chkANULADA.Name = "chkANULADA"
        Me.chkANULADA.Size = New System.Drawing.Size(140, 19)
        Me.chkANULADA.TabIndex = 268
        Me.chkANULADA.Text = "Disposició Anulada"
        Me.chkANULADA.UseVisualStyleBackColor = False
        '
        'btnElegirAcabador
        '
        Me.btnElegirAcabador.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirAcabador.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirAcabador.Location = New System.Drawing.Point(160, 64)
        Me.btnElegirAcabador.Name = "btnElegirAcabador"
        Me.btnElegirAcabador.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirAcabador.TabIndex = 267
        Me.btnElegirAcabador.Text = "..."
        Me.btnElegirAcabador.UseVisualStyleBackColor = True
        '
        'lblPedido
        '
        Me.lblPedido.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPedido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPedido.Location = New System.Drawing.Point(476, 16)
        Me.lblPedido.Name = "lblPedido"
        Me.lblPedido.Size = New System.Drawing.Size(76, 20)
        Me.lblPedido.TabIndex = 266
        Me.lblPedido.Text = "Comanda"
        Me.lblPedido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCOMANDA
        '
        Me.txtCOMANDA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCOMANDA.Location = New System.Drawing.Point(560, 16)
        Me.txtCOMANDA.Name = "txtCOMANDA"
        Me.txtCOMANDA.Size = New System.Drawing.Size(148, 18)
        Me.txtCOMANDA.TabIndex = 265
        Me.txtCOMANDA.Tag = Nothing
        '
        'cboNOMCLIENT
        '
        Me.cboNOMCLIENT.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMCLIENT.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboNOMCLIENT.AutoSelect = True
        Me.cboNOMCLIENT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMCLIENT.Caption = ""
        Me.cboNOMCLIENT.CaptionHeight = 17
        Me.cboNOMCLIENT.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMCLIENT.ColumnCaptionHeight = 17
        Me.cboNOMCLIENT.ColumnFooterHeight = 17
        Me.cboNOMCLIENT.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMCLIENT.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMCLIENT.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMCLIENT.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMCLIENT.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMCLIENT.Images.Add(CType(resources.GetObject("cboNOMCLIENT.Images"), System.Drawing.Image))
        Me.cboNOMCLIENT.IntegralHeight = True
        Me.cboNOMCLIENT.ItemHeight = 13
        Me.cboNOMCLIENT.Location = New System.Drawing.Point(188, 40)
        Me.cboNOMCLIENT.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMCLIENT.MaxDropDownItems = CType(8, Short)
        Me.cboNOMCLIENT.MaxLength = 0
        Me.cboNOMCLIENT.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMCLIENT.Name = "cboNOMCLIENT"
        Me.cboNOMCLIENT.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMCLIENT.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMCLIENT.Size = New System.Drawing.Size(280, 19)
        Me.cboNOMCLIENT.TabIndex = 262
        Me.cboNOMCLIENT.PropBag = resources.GetString("cboNOMCLIENT.PropBag")
        '
        'cboNOMACABADOR
        '
        Me.cboNOMACABADOR.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMACABADOR.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.cboNOMACABADOR.Location = New System.Drawing.Point(188, 64)
        Me.cboNOMACABADOR.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMACABADOR.MaxDropDownItems = CType(8, Short)
        Me.cboNOMACABADOR.MaxLength = 0
        Me.cboNOMACABADOR.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMACABADOR.Name = "cboNOMACABADOR"
        Me.cboNOMACABADOR.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMACABADOR.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMACABADOR.Size = New System.Drawing.Size(280, 19)
        Me.cboNOMACABADOR.TabIndex = 254
        Me.cboNOMACABADOR.PropBag = resources.GetString("cboNOMACABADOR.PropBag")
        '
        'chkRECIBIDO
        '
        Me.chkRECIBIDO.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkRECIBIDO.BackColor = System.Drawing.Color.Transparent
        Me.chkRECIBIDO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkRECIBIDO.Location = New System.Drawing.Point(556, 64)
        Me.chkRECIBIDO.Name = "chkRECIBIDO"
        Me.chkRECIBIDO.Size = New System.Drawing.Size(140, 19)
        Me.chkRECIBIDO.TabIndex = 5
        Me.chkRECIBIDO.Text = "Disposició Rebuda"
        Me.chkRECIBIDO.UseVisualStyleBackColor = False
        '
        'lblFechaRecibido
        '
        Me.lblFechaRecibido.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFechaRecibido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFechaRecibido.Location = New System.Drawing.Point(712, 64)
        Me.lblFechaRecibido.Name = "lblFechaRecibido"
        Me.lblFechaRecibido.Size = New System.Drawing.Size(96, 19)
        Me.lblFechaRecibido.TabIndex = 6
        Me.lblFechaRecibido.Text = "Data Rebut"
        Me.lblFechaRecibido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFechaRecibido.Visible = False
        '
        'lblFecha
        '
        Me.lblFecha.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFecha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFecha.Location = New System.Drawing.Point(712, 40)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(92, 20)
        Me.lblFecha.TabIndex = 4
        Me.lblFecha.Text = "Data Disposició"
        Me.lblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCODICLIENT
        '
        Me.txtCODICLIENT.Location = New System.Drawing.Point(100, 16)
        Me.txtCODICLIENT.Name = "txtCODICLIENT"
        Me.txtCODICLIENT.Size = New System.Drawing.Size(56, 18)
        Me.txtCODICLIENT.TabIndex = 0
        Me.txtCODICLIENT.Tag = Nothing
        '
        'txtany
        '
        Me.txtany.Location = New System.Drawing.Point(232, 16)
        Me.txtany.Name = "txtany"
        Me.txtany.Size = New System.Drawing.Size(36, 18)
        Me.txtany.TabIndex = 2
        Me.txtany.Tag = Nothing
        '
        'lblDisposicion
        '
        Me.lblDisposicion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDisposicion.Location = New System.Drawing.Point(8, 16)
        Me.lblDisposicion.Name = "lblDisposicion"
        Me.lblDisposicion.Size = New System.Drawing.Size(88, 20)
        Me.lblDisposicion.TabIndex = 0
        Me.lblDisposicion.Text = "Disposició Nº"
        Me.lblDisposicion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCLIENT
        '
        Me.txtCLIENT.Location = New System.Drawing.Point(100, 40)
        Me.txtCLIENT.Name = "txtCLIENT"
        Me.txtCLIENT.Size = New System.Drawing.Size(56, 18)
        Me.txtCLIENT.TabIndex = 3
        Me.txtCLIENT.Tag = Nothing
        '
        'lblCliente
        '
        Me.lblCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCliente.Location = New System.Drawing.Point(8, 40)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(88, 20)
        Me.lblCliente.TabIndex = 202
        Me.lblCliente.Text = "Client"
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnElegirCliente
        '
        Me.btnElegirCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirCliente.Location = New System.Drawing.Point(160, 39)
        Me.btnElegirCliente.Name = "btnElegirCliente"
        Me.btnElegirCliente.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirCliente.TabIndex = 201
        Me.btnElegirCliente.Text = "..."
        Me.btnElegirCliente.UseVisualStyleBackColor = True
        '
        'txtACABADOR
        '
        Me.txtACABADOR.Location = New System.Drawing.Point(100, 64)
        Me.txtACABADOR.Name = "txtACABADOR"
        Me.txtACABADOR.Size = New System.Drawing.Size(56, 18)
        Me.txtACABADOR.TabIndex = 253
        Me.txtACABADOR.Tag = Nothing
        '
        'lblAcabador
        '
        Me.lblAcabador.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAcabador.Location = New System.Drawing.Point(8, 64)
        Me.lblAcabador.Name = "lblAcabador"
        Me.lblAcabador.Size = New System.Drawing.Size(88, 20)
        Me.lblAcabador.TabIndex = 256
        Me.lblAcabador.Text = "Acabador"
        Me.lblAcabador.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(8, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 20)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Tintar a Color"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(712, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 20)
        Me.Label2.TabIndex = 262
        Me.Label2.Text = "Colorit Client"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtcolorcliente
        '
        Me.txtcolorcliente.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtcolorcliente.Location = New System.Drawing.Point(808, 16)
        Me.txtcolorcliente.Name = "txtcolorcliente"
        Me.txtcolorcliente.Size = New System.Drawing.Size(148, 18)
        Me.txtcolorcliente.TabIndex = 261
        Me.txtcolorcliente.Tag = Nothing
        '
        'frmDisposicion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(992, 610)
        Me.Controls.Add(Me.tabControlDisposiciones)
        Me.Controls.Add(Me.btnImprimir)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.KeyPreview = True
        Me.Name = "frmDisposicion"
        Me.Text = "Disposicions"
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
        Me.Controls.SetChildIndex(Me.btnImprimir, 0)
        Me.Controls.SetChildIndex(Me.tabControlDisposiciones, 0)
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
        CType(Me.btnImprimir, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTejedor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTejido, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControlDisposiciones.ResumeLayout(False)
        Me.tabPageDisposicion.ResumeLayout(False)
        CType(Me.dgDdispos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalPiezas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalKG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gb1.ResumeLayout(False)
        Me.gb1.PerformLayout()
        CType(Me.cboIDDispos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnElegirAcabadorPorDefecto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFECHA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDRECEPCION, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtColor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnEscogerColorExistente, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnElegirAcabador, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCOMANDA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMACABADOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCODICLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnElegirCliente, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtACABADOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcolorcliente, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmDisposicion
    Public Shared Function GetInstance() As frmDisposicion
        If frmChildForm Is Nothing Then
            frmChildForm = New frmDisposicion

        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Public dispoActual As clsDisposicion

#End Region

#Region "INICIALIZAR"

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Cursor = Cursors.WaitCursor
            Me.arrayEtiquetas = New System.Windows.Forms.Control() {Me.Label2, Me.Label3, Me.lblAcabador, Me.lblCliente, Me.lblDisposicion, Me.lblFecha, Me.lblFechaRecibido, Me.lblPedido, Me.lblTotalKGS, Me.lblTotalPiezas}
            Me.arrayTabPages = New System.Windows.Forms.Control() {Me.tabPageDisposicion}
            Me.arrayCombos = New System.Windows.Forms.Control() {Me.cboNOMACABADOR, cboNOMCLIENT, cboTejedor, cboTejido}
            Me.arrayGrids = New System.Windows.Forms.Control() {Me.dgDdispos}
            Me.arrayDateTime = New System.Windows.Forms.Control() {Me.dtpFECHA, Me.dtpDRECEPCION}
            Me.arrayTextBox = New System.Windows.Forms.Control() {Me.txtany, Me.txtCODICLIENT, Me.txtACABADOR, Me.txtCLIENT, Me.txtColor, Me.txtcolorcliente, Me.txtOBSERV, Me.txtCOMANDA, Me.txtTotalKG, Me.txtTotalPiezas}
            Me.arrayCheckBox = New System.Windows.Forms.Control() {Me.chkANULADA, Me.chkRECIBIDO}
            Me.arrayBotones = New System.Windows.Forms.Control() {Me.btnElegirAcabador, Me.btnElegirCliente, Me.btnEscogerColorExistente}

            cargando = True
            tabla = tablaDisposiciones
            dispoActual = New clsDisposicion(ds.Tables(tabla), empresaPorDefecto, BindingContext)
            HacerBindings()
            InicializardgDdisposes()
            PonerModificables(soloLectura)
            btnUltimo_Click(Nothing, Nothing)
            PSBTIPO(dispoActual.centro) : cargando = False

            If dispoActual.RECIBIDO = False Then dtpDRECEPCION.Visible = False
            PonerHandlersErroresParaGrids()
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub cboTejedor_SelChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles cboTejedor.SelChange
        Try
            If PuedoModificar() Then

                'dispoActual.detalleDisposicion.TEJEDOR = cboTejedor.Columns("CODI").Value
                'AñadirBindingCombo(cboTejedor, dispoActual.dtTejedores, "NOM", "CODI")
            End If

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub InicializardgDdisposes()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgDdispos)

            With dgDdispos.Columns

                i = 0
                PPCol2("TEJEDOR", dgDdispos, "Codi Teixidor", "", True, 100, True, PresentationEnum.Normal, False, 50, i, False)
                i = 1
                PPCol2("NOMTEJEDOR", dgDdispos, rm.GetString("NOMTEJEDOR"), "", True, 200, False, PresentationEnum.ComboBox, False, 200, i, False, cboTejedor)
                i = 2
                PPCol2("NALBARAN", dgDdispos, rm.GetString("ALBARAN"), "", True, 50, _
                            False, PresentationEnum.Normal, False, 50, i, True)
                i = 3
                PPCol2("TEJIDO", dgDdispos, rm.GetString("TEJIDO"), "", True, 100, _
                            False, PresentationEnum.ComboBox, False, 100, i, True, cboTejido)
                i = 4
                PPCol2("COMPOS", dgDdispos, rm.GetString("COMPOSICION"), "", True, 200, _
                            False, PresentationEnum.Normal, False, 200, i, True)
                i = 5
                PPCol2("NPIEZAS", dgDdispos, rm.GetString("NPIEZAS"), "", True, 50, _
                            False, PresentationEnum.Normal, False, 50, i, True)
                i = 6
                PPCol2("RENDIMIENTO", dgDdispos, rm.GetString("RENDIMIENTO"), "", True, 50, _
                            False, PresentationEnum.Normal, False, 50, i, True)

                i = 7
                PPCol2("GRAMAJE", dgDdispos, rm.GetString("GRAMAJE"), "", True, 50, _
                             False, PresentationEnum.Normal, False, 50, i, True)
                i = 8
                PPCol2("ANCHO", dgDdispos, rm.GetString("ANCHO"), "", True, 100, _
                            False, PresentationEnum.Normal, False, 100, i, True)
                i = 9
                PPCol2("TOTALPIEZAS", dgDdispos, rm.GetString("TOTALP"), "", True, 50, _
                            False, PresentationEnum.Normal, False, 50, i, True)
                i = 10
                PPCol2("TOTALKG", dgDdispos, rm.GetString("TOTALKG"), "", True, 50, _
                            False, PresentationEnum.Normal, False, 50, i, True)
                i = 11
                PPCol2("ACABADO", dgDdispos, rm.GetString("ACABADOS"), "", True, 250, _
                            False, PresentationEnum.ComboBox, False, 250, i, True)
                i = 12
                PPCol2("DISPUESTO", dgDdispos, rm.GetString("DISPUESTO"), "", True, _
                            45, False, PresentationEnum.CheckBox, False, 45, i, False, Nothing, True)
                i = 13
                PPCol2("SERVIDO", dgDdispos, rm.GetString("SERVIDO"), "", True, _
                            35, False, PresentationEnum.CheckBox, False, 35, i, False, Nothing, True)
                i = 14
                'PPCol2("LINEA", dgDdispos, "", "", True, _
                '            35, False, PresentationEnum.Normal, False, 35, i, False, Nothing, True)

            End With


        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try


    End Sub
    Private Sub HacerBindings()
        Try
            HacerBindingsTodos(dispoActual.dvForm)
            AñadirBinding(cboIDDispos, dispoActual.dvForm, "IDDISPOS")
            AñadirBindingCombo(cboIDDispos, dispoActual.dvIdentificadores, "CODI", "IDDISPOS")
            OcultarMostrarColumnaCbo(cboIDDispos, "CODI", False)
            AñadirBindingCombo(cboNOMACABADOR, dispoActual.dtAcabadores, CCTallers, CNTallers)
            AñadirBindingCombo(cboNOMCLIENT, dispoActual.dtClients, CCClients, CNClients)
            dgDdispos.SetDataBinding(dispoActual.detalleDisposicion.dvForm, "")
            'AñadirBindingCombo(cboTejedor, dispoActual.dtTejedores.DefaultView, "NOM", "CODI")
            AñadirBindingCombo(cboTejedor, dispoActual.dtTejedores, "NOM", "CODI")
            AñadirBindingCombo(cboTejido, dispoActual.dtTejidos.DefaultView, "CODI", "CODI")
            OcultarColumnaDropDown(cboTejido)
            OcultarMostrarColumnaCbo(cboTejido, "CODI", True)

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub

#End Region

#Region "MODIFICAR DB"

    Private Sub ActualizarOrigen()
        Try
            If editando Then
                If dispoActual.hayAlgunaSinDisponer Then
                    MessageBox.Show(rm.GetString("HAYLINEASSINDISPONER"), "", MessageBoxButtons.OK)
                Else
                    editando = False
                    PonerModificables(soloLectura)
                End If
            End If
            If EsRegistroNuevo Then
                dgDdispos.Visible = True
                editando = True : PSBTIPO(dispoActual.centro)
                PonerControlesNuevo(True)
                dispoActual.actualizarNumeraciones()
                EsRegistroNuevo = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Friend Overrides Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnActualizar.Click
        Try
            cargando = True
            If dispoActual.TieneCambios Then
                Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                    Case DialogResult.Cancel
                        cargando = False
                        Exit Sub
                    Case DialogResult.No
                        dispoActual.tabla.RejectChanges()
                        dispoActual.detalleDisposicion.tabla.RejectChanges()
                        cargando = False
                        Exit Sub
                End Select
            End If
            dispoActual.ActualizarOrigen() : ActualizarOrigen()
            PSBTIPO(dispoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnBorrar.Click
        Try
            If MessageBox.Show(rm.GetString("BorrarDisposicion"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                'Primero borramos el tejido de la tabla tejido
                'Despues borramos todos sus "tejidos"
                editando = True
                dispoActual.borrar() : ActualizarOrigen()
                editando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try

    End Sub
    Friend Overrides Sub btnModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        Try
            If Not editando Then
                cargando = True
                editando = True
                PonerModificables(modificable)
                PSBTIPO(dispoActual.centro)
                cboIDDispos.Text = dispoActual.IDDISPOS
                cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnNuevo.Click
        Try
            cargando = True
            EsRegistroNuevo = True
            dgDdispos.Visible = False

            PonerModificables(modificable)
            PonerControlesNuevo(False)
            dispoActual.NuevoRegistro()
            cboIDDispos.AutoCompletion = False
            PSBTIPO(dispoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub

#End Region

#Region "ESPECÍFICO"

    Private Sub chkAnulada_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkANULADA.CheckStateChanged
        Try
            If PuedoModificar() Then
                cargando = True
                If chkANULADA.Checked = True Then
                    chkRecibido.Checked = False
                    lblFechaRecibido.Visible = False
                End If
                cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub chkRecibido_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRECIBIDO.CheckStateChanged
        Try
            If PuedoModificar() Then
                cargando = True
                If chkRECIBIDO.Checked = True Then
                    If dispoActual.detalleDisposicion.TodasRecibidas Then
                        dtpDRECEPCION.Visible = True
                        lblFechaRecibido.Visible = True
                        chkANULADA.Checked = False
                    Else
                        MessageBox.Show(rm.GetString("DIPOSRECIBIDAS"), rm.GetString("INFO"))
                        chkRECIBIDO.Checked = False
                        lblFechaRecibido.Visible = False
                    End If
                Else
                    dtpDRECEPCION.Visible = False
                    lblFechaRecibido.Visible = False
                End If
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try

    End Sub
    Private Sub PonerKGEditable()
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub dgDdispos_ButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgDdispos.ButtonClick
        Dim f As New frmElegirAcabados
        Try
            Select Case e.Column.DataColumn.DataField
                Case "ACABADO"
                    dgDdispos.EditActive = True
                    f.client = dispoActual.ACABADOR
                    f.tejido = dispoActual.detalleDisposicion.TEJIDO
                    f.ShowDialog()
                    dispoActual.detalleDisposicion.PonerAcabadoLineaActual(f.aAcabados)
                    dgDdispos.Refresh()
            End Select

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try

    End Sub
    Private Sub dgDdispos_Error(ByVal sender As System.Object, ByVal e As C1.Win.C1TrueDBGrid.ErrorEventArgs)

        e.Handled = True

    End Sub

#Region "CAMBIO COLUMNA"

    Private Sub dgDdispos_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgDdispos.AfterColUpdate
        Dim b As Boolean
        Try

            If PuedoModificar() Then
                cargando = True
                dgDdispos.UpdateData()
                Select Case e.Column.DataColumn.DataField
                    Case "NOMTEJEDOR"
                        dispoActual.detalleDisposicion.TEJEDOR = cboTejedor.Columns("CODI").Value
                    Case "TEJEDOR"
                        dispoActual.detalleDisposicion.TEJEDOR = e.Column.DataColumn.Value
                    Case "NOMTALLER"
                        dispoActual.detalleDisposicion.TEJEDOR = e.Column.DataColumn.Value

                    Case "SERVIDO"

                        If dgDdispos(dgDdispos.Row, "DISPUESTO") = True Then
                            b = dgDdispos(dgDdispos.Row, "SERVIDO")

                            If b = True Then
                                If dispoActual.detalleDisposicion.PonerQuitarServido(True) Then
                                    dgDdispos(dgDdispos.Row, "SERVIDO") = False
                                End If
                            Else
                                If dispoActual.detalleDisposicion.PonerQuitarServido(False) Then
                                    dgDdispos(dgDdispos.Row, "SERVIDO") = True
                                End If
                            End If

                            b = dgDdispos(dgDdispos.Row, "SERVIDO")

                            If b And dgDdispos(dgDdispos.Row, "DISPUESTO") = False Then
                                dgDdispos(dgDdispos.Row, "SERVIDO") = False
                            End If

                            If dgDdispos(dgDdispos.Row, "SERVIDO") = True Then
                                dgDdispos.Splits(0).DisplayColumns("TOTALKG").AllowFocus = False
                            Else
                                dgDdispos.Splits(0).DisplayColumns("TOTALKG").AllowFocus = True
                            End If
                        Else
                            dgDdispos(dgDdispos.Row, "SERVIDO") = False
                            MessageBox.Show(rm.GetString("PRIMERODISPONER"))
                        End If

                    Case "DISPUESTO"
                        If dgDdispos(dgDdispos.Row, "DISPUESTO") = True Then
                            dgDdispos.Splits(0).DisplayColumns("TOTALKG").AllowFocus = False
                            If dispoActual.detalleDisposicion.PonerQuitarDispuesta(True) Then

                            Else
                                dgDdispos(dgDdispos.Row, "DISPUESTO") = False
                            End If
                            dgDdispos.Splits(0).DisplayColumns("TOTALKG").AllowFocus = False
                        Else
                            If dispoActual.detalleDisposicion.PonerQuitarDispuesta(False) Then
                            Else
                            End If
                            dgDdispos.Splits(0).DisplayColumns("TOTALKG").AllowFocus = True
                        End If

                    Case "TEJIDO"
                        cargando = True
                        'Debug.WriteLine("QUE ASCO")
                        dispoActual.detalleDisposicion.TEJIDO = cboTejido.Columns("CODI").Value
                        dispoActual.detalleDisposicion.COMPOS = general.nz(dispoActual.detalleDisposicion.getcomposicion(), "")
                        dispoActual.detalleDisposicion.ANCHO = general.nz(cboTejido.Columns("AMPLE").Value, "")
                        dispoActual.detalleDisposicion.DESCRIPCIO = general.nz(cboTejido.Columns("DESCRI").Value, "")
                        dispoActual.detalleDisposicion.RENDIMIENTO = nzn(cboTejido.Columns("RENDIMENT").Value, 0)
                        dispoActual.detalleDisposicion.GRAMAJE = general.nz(cboTejido.Columns("GRAMA").Value, "")
                        dispoActual.detalleDisposicion.ACABADO = general.nz(cboTejido.Columns("ACABAT").Value, "")
                        cargando = False


                        dgDdispos.EditActive = True
                        dispoActual.detalleDisposicion.TEJIDO = cboTejido.Columns("CODI").Value
                        dispoActual.detalleDisposicion.COMPOS = general.nz(dispoActual.detalleDisposicion.getcomposicion(), "")
                        dispoActual.detalleDisposicion.ANCHO = general.nz(cboTejido.Columns("AMPLE").Value, "")
                        dispoActual.detalleDisposicion.DESCRIPCIO = general.nz(cboTejido.Columns("DESCRI").Value, "")
                        dispoActual.detalleDisposicion.RENDIMIENTO = nzn(cboTejido.Columns("RENDIMENT").Value, 0)
                        dispoActual.detalleDisposicion.GRAMAJE = general.nz(cboTejido.Columns("GRAMA").Value, "")
                        dispoActual.detalleDisposicion.ACABADO = general.nz(cboTejido.Columns("ACABAT").Value, "")
                        dgDdispos.Refresh()
                End Select
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub dgDdispos_RowColChange(ByVal sender As System.Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgDdispos.RowColChange
        Try
            If PuedoModificar() Then
                cargando = True
                'If dgDdispos(dgDdispos.Row, "SERVIDO") = False And dgDdispos(dgDdispos.Row, "DISPUESTO") = False Then
                '    dgDdispos.Splits(0).DisplayColumns("TOTALKG").AllowFocus = True
                'Else
                '    If Not dgDdispos.Splits(0).DisplayColumns("TOTALKG").AllowFocus = False Then
                '        dgDdispos.Splits(0).DisplayColumns("TOTALKG").AllowFocus = False
                '        dgDdispos.Col = e.LastCol + 1
                '    End If

                'End If
                ''AutosizeColumnasTrueDBDropdown(cboTejedor)
                'AutosizeColumnasTrueDBDropdown(cboTejido)
                'AutoSizeCC(dgDdispos)
                If dispoActual.detalleDisposicion.SERVIDO = False And dispoActual.detalleDisposicion.DISPUESTO = False Then
                    dgDdispos.Splits(0).DisplayColumns("TOTALKG").AllowFocus = True
                Else
                    If Not dgDdispos.Splits(0).DisplayColumns("TOTALKG").AllowFocus = False Then
                        dgDdispos.Splits(0).DisplayColumns("TOTALKG").AllowFocus = False
                        dgDdispos.Col = e.LastCol + 1
                    End If

                End If
                'AutosizeColumnasTrueDBDropdown(cboTejedor)
                AutosizeColumnasTrueDBDropdown(cboTejido)
                AutoSizeCC(dgDdispos)
                cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString)

        End Try
    End Sub

#End Region
#Region "CAMBIO SELECCION"

    Private Sub cboTejido_SelChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles cboTejido.SelChange
        Try
            
            If PuedoModificar() Then
                'cargando = True
                ''Debug.WriteLine("QUE ASCO")
                'dispoActual.detalleDisposicion.TEJIDO = cboTejido.Columns("CODI").Value
                'dispoActual.detalleDisposicion.COMPOS = general.nz(dispoActual.detalleDisposicion.getcomposicion(), "")
                'dispoActual.detalleDisposicion.ANCHO = general.nz(cboTejido.Columns("AMPLE").Value, "")
                'dispoActual.detalleDisposicion.DESCRIPCIO = general.nz(cboTejido.Columns("DESCRI").Value, "")
                'dispoActual.detalleDisposicion.RENDIMIENTO = nzn(cboTejido.Columns("RENDIMENT").Value, 0)
                'dispoActual.detalleDisposicion.GRAMAJE = general.nz(cboTejido.Columns("GRAMA").Value, "")
                'dispoActual.detalleDisposicion.ACABADO = general.nz(cboTejido.Columns("ACABAT").Value, "")
                'cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    'Private Sub cboTejedor_SelChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles cboTejedor.SelChange
    '    Try
    '        If nzn(cboTejedor.Columns("CODI").Value, 0) = 0 Or nzn(dispoActual.ACABADOR, 0) = 0 Then
    '            MessageBox.Show(rm.GetString("ERRORELEGIRTEJEDORACABADOR"))
    '        Else
    '            dgDdispos(dgDdispos.Row, "TEJEDOR") = cboTejedor.Columns("CODI").Value
    '            dispoActual.dtTejidos.DefaultView.RowFilter = "TEIXIDOR = '" & cboTejedor.Columns("CODI").Value & "' AND ACABADOR = '" & dispoActual.ACABADOR & "'"
    '            AñadirBindingCombo(cboTejido, dispoActual.dtTejidos.DefaultView, "CODI", "CODI")
    '            AutosizeColumnasTrueDBDropdown(cboTejido)
    '        End If

    '    Catch ex As Exception
    '        LOG(ex.ToString) : CCN() : cargando = False
    '    End Try
    'End Sub

#End Region

#End Region

#Region "IMPRESIÓN"

    Private Sub VistaPreliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Try
            dispoActual.ImprimirDisposicionActual()

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try

    End Sub

#End Region

#Region "COMUNES"
    Private Sub PonerModificables(Optional ByVal b As Boolean = False)
        Try
            PonerMod(b)
            'If Not b Then : cboID.DataSource = Nothing
            'Else : AñadirBindingCombo(cboID, dispoActual.dvIdentificadores, CCDisposiciones, CCDisposiciones) : End If

            'cboID.LimitToList = b
            'cboID.SuperBack = b
            'cboID.ReadOnly = editando ''or esregistronuevo
            ModiNuev(b)

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
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
            cboIDDispos.AutoCompletion = Not editando

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub btnLlista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerLista.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmDisposicionLista = frmDisposicionLista.GetInstance(esListado)
            f.MdiParent = Me.MdiParent
            AddHandler f.Closed, AddressOf CType(Me.MdiParent, frmPrincipal).childCerrado
            AddHandler f.Load, AddressOf CType(Me.MdiParent, frmPrincipal).childAbierto : AddHandler f.Activated, AddressOf CType(Me.MdiParent, frmPrincipal).childOcultandoMostrando
            f.Show()
            f.BringToFront()
            Cursor = Cursors.Default

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try

    End Sub

#End Region

#Region "DESPLAZARSE"

    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            cargando = True
            dispoActual.PrimeroReg() : ActualizarOrigen()
            If chkANULADA.Checked = False Then
                lblFechaRecibido.Visible = False
            Else
                lblFechaRecibido.Visible = True
            End If
            cboTejido.Rebind(True)
            AutoSizeCC(dgDdispos)
            PSBTIPO(dispoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            cargando = True
            dispoActual.UltimoReg() : ActualizarOrigen()
            If chkANULADA.Checked = False Then
                lblFechaRecibido.Visible = False
            Else
                lblFechaRecibido.Visible = True
            End If
            cboTejido.Rebind(True)
            AutoSizeCC(dgDdispos)
            PSBTIPO(dispoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            cargando = True
            dispoActual.AnteriorReg() : ActualizarOrigen()
            If chkANULADA.Checked = False Then
                lblFechaRecibido.Visible = False
            Else
                lblFechaRecibido.Visible = True
            End If
            cboTejido.Rebind(True)
            AutoSizeCC(dgDdispos)
            PSBTIPO(dispoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            cargando = True
            dispoActual.SiguienteReg() : ActualizarOrigen()
            If chkANULADA.Checked = False Then
                lblFechaRecibido.Visible = False
            Else
                lblFechaRecibido.Visible = True
            End If
            cboTejido.Rebind(True)
            AutoSizeCC(dgDdispos)
            PSBTIPO(dispoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub

#End Region

#Region "SELECCIÓN REGISTRO"

    Private Sub cboID_ItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboIDDispos.RowChange
        Try
            If consulta() Then
                cargando = True
                dispoActual.CambiarAReg(GENERAL.nz(cboIDDispos.WillChangeToValue, ""), iraregistro)
                'MoverActual()
                PSBTIPO(dispoActual.centro) : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ELECCIONES"

    Private Sub btnEscogerColorExistente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEscogerColorExistente.Click
        Try
            If dispoActual.ACABADOR <> 0 Then
                Dim f As frmListadoColoresDisp = frmListadoColoresDisp.GetInstance(esEleccion)
                f.acabador = dispoActual.ACABADOR
                f.ShowDialog()
                If Not f.dr Is Nothing Then dispoActual.COLOR = f.dr("COLOR")
            Else
                MessageBox.Show(rm.GetString("ESCOGERACABADOR"))
                txtACABADOR.Focus()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub btnElegirCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirCliente.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmClientesLista = frmClientesLista.GetInstance(esEleccion)
            f.ShowDialog()
            If Not f.dr Is Nothing Then dispoActual.CLIENT = f.dr("CODI")
            Cursor = Cursors.Default
            f = Nothing
            PSBTIPO(dispoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub btnElegirAcabador_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnElegirAcabador.Click
        Try
            'Dim f As New frmElegirTaller
            '
            'f.ShowDialog()
            'dispoActual.TALLER = f.codigo

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub txtCodigoAcabador_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtACABADOR.Validated

        Try
            If PuedoModificar() Then cargando = True : dispoActual.ACABADOR = nzn(txtACABADOR.Text, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub txtCodigoCliente_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCLIENT.Validated

        Try
            If PuedoModificar() Then cargando = True : dispoActual.CLIENT = nzn(txtCLIENT.Text, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub cboNombreAcabador_ItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMACABADOR.SelectedValueChanged

        Try
            If PuedoModificar() Then cargando = True : dispoActual.ACABADOR = nzn(cboNOMACABADOR.WillChangeToValue, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub cboNombreCliente_ItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMCLIENT.SelectedValueChanged

        Try
            If PuedoModificar() Then cargando = True : dispoActual.CLIENT = nzn(cboNOMCLIENT.WillChangeToValue, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub

#End Region

    Private Sub dgDdispos_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgDdispos.BeforeDelete
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
    Private Sub XpButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirAcabadorPorDefecto.Click
        Try
            Dim f As New frmAcabadorPorDefecto(dispoActual.dtAcabadores)
            f.ShowDialog()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

End Class

'Private Sub cboColor_SelChange(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboColor.SelectedValueChanged
'    Try
'        If PuedoModificar() Then
'            cargando = True
'            dispoActual.detalleDisposicion.tabla.Clear()
'            dispoActual.COLOR = cboColor.WillChangeToText
'            dispoActual.CargarTablas()
'            Try
'                cboTejedor.ReBind(True)
'                cboTejido.ReBind(True)
'                cboTejido.DataSource = dispoActual.dtTejidos.DefaultView
'                cboTejido.DataMember = "CODI"
'                cboTejido.ListField = "CODI"
'            Catch ex As Exception

'            End Try
'            AutosizeColumnasTrueDBDropdown(cboTejedor)
'            AutosizeColumnasTrueDBDropdown(cboTejido)
'            'AutoSizeCC(dgDdispos)
'            cargando = False

'        End If

'    Catch ex As Exception
'        LOG(ex.ToString) : ccn() : cargando = false
'    End Try
'End Sub