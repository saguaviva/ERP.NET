Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes


Public Class frmBancosForm
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


    Friend WithEvents lblNombreBanco As Label 'Label
    Friend WithEvents lblCodigoBanco As Label
    Friend WithEvents btnCanviarLogo As C1.Win.C1Input.C1Button 'Leadedit.UI.Windows.FlatButton
    Friend WithEvents lblCuentasCredito As Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents cboID As C1.Win.C1List.C1Combo
    Friend WithEvents txtDOM As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtDESCRIPCIO As C1.Win.C1Input.C1TextBox
    Friend WithEvents pibLOGO As System.Windows.Forms.PictureBox
    Friend WithEvents dgCuentas As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents cboTIPOCUENTA As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents cboEmpresa As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents txtNUMCUENTA As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtSWIFT As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtIBAN As C1.Win.C1Input.C1TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblIBAN As System.Windows.Forms.Label

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmBancosForm))
        Me.lblNombreBanco = New System.Windows.Forms.Label
        Me.lblCodigoBanco = New System.Windows.Forms.Label
        Me.txtDESCRIPCIO = New C1.Win.C1Input.C1TextBox
        Me.btnCanviarLogo = New C1.Win.C1Input.C1Button
        Me.lblCuentasCredito = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.lblIBAN = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtIBAN = New C1.Win.C1Input.C1TextBox
        Me.txtSWIFT = New C1.Win.C1Input.C1TextBox
        Me.txtNUMCUENTA = New C1.Win.C1Input.C1TextBox
        Me.cboEmpresa = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Me.cboTIPOCUENTA = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Me.dgCuentas = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.cboID = New C1.Win.C1List.C1Combo
        Me.pibLOGO = New System.Windows.Forms.PictureBox
        CType(Me.txtDESCRIPCIO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.txtIBAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSWIFT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNUMCUENTA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboEmpresa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTIPOCUENTA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgCuentas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(150, 24)
        Me.btnModificar.Name = "btnModificar"
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(28, 24)
        Me.btnPrimero.Name = "btnPrimero"
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(28, 44)
        Me.btnAnterior.Name = "btnAnterior"
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(614, 44)
        Me.btnTancar.Name = "btnTancar"
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(150, 44)
        Me.btnBorrar.Name = "btnBorrar"
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(248, 24)
        Me.btnNuevo.Name = "btnNuevo"
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(60, 24)
        Me.btnActualizar.Name = "btnActualizar"
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(5, 124)
        Me.sbtipo.Name = "sbtipo"
        Me.sbtipo.Text = ""
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(614, 24)
        Me.btnVerLista.Name = "btnVerLista"
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.Name = "cboSeleccionCentro"
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(330, 24)
        Me.btnUltimo.Name = "btnUltimo"
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(60, 44)
        Me.btnRecargar.Name = "btnRecargar"
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(330, 44)
        Me.btnSiguiente.Name = "btnSiguiente"
        '
        'lblNombreBanco
        '
        Me.lblNombreBanco.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNombreBanco.Location = New System.Drawing.Point(8, 36)
        Me.lblNombreBanco.Name = "lblNombreBanco"
        Me.lblNombreBanco.Size = New System.Drawing.Size(80, 20)
        Me.lblNombreBanco.TabIndex = 207
        Me.lblNombreBanco.Text = "Descripció"
        Me.lblNombreBanco.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCodigoBanco
        '
        Me.lblCodigoBanco.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCodigoBanco.Location = New System.Drawing.Point(8, 7)
        Me.lblCodigoBanco.Name = "lblCodigoBanco"
        Me.lblCodigoBanco.Size = New System.Drawing.Size(80, 20)
        Me.lblCodigoBanco.TabIndex = 206
        Me.lblCodigoBanco.Text = "Codi banc"
        Me.lblCodigoBanco.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDESCRIPCIO
        '
        Me.txtDESCRIPCIO.Location = New System.Drawing.Point(92, 36)
        Me.txtDESCRIPCIO.MaxLength = 255
        Me.txtDESCRIPCIO.Name = "txtDESCRIPCIO"
        Me.txtDESCRIPCIO.Size = New System.Drawing.Size(232, 20)
        Me.txtDESCRIPCIO.TabIndex = 1
        Me.txtDESCRIPCIO.Tag = Nothing
        '
        'btnCanviarLogo
        '
        Me.btnCanviarLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCanviarLogo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCanviarLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCanviarLogo.Location = New System.Drawing.Point(436, 68)
        Me.btnCanviarLogo.Name = "btnCanviarLogo"
        Me.btnCanviarLogo.Size = New System.Drawing.Size(52, 40)
        Me.btnCanviarLogo.TabIndex = 2
        Me.btnCanviarLogo.TabStop = False
        Me.btnCanviarLogo.Text = "Escollir logo"
        '
        'lblCuentasCredito
        '
        Me.lblCuentasCredito.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCuentasCredito.Location = New System.Drawing.Point(12, 176)
        Me.lblCuentasCredito.Name = "lblCuentasCredito"
        Me.lblCuentasCredito.Size = New System.Drawing.Size(112, 20)
        Me.lblCuentasCredito.TabIndex = 212
        Me.lblCuentasCredito.Text = "Comptes"
        Me.lblCuentasCredito.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.ItemSize = New System.Drawing.Size(42, 18)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(884, 492)
        Me.TabControl1.TabIndex = 392
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lblIBAN)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.txtIBAN)
        Me.TabPage1.Controls.Add(Me.txtSWIFT)
        Me.TabPage1.Controls.Add(Me.txtNUMCUENTA)
        Me.TabPage1.Controls.Add(Me.cboEmpresa)
        Me.TabPage1.Controls.Add(Me.cboTIPOCUENTA)
        Me.TabPage1.Controls.Add(Me.dgCuentas)
        Me.TabPage1.Controls.Add(Me.cboID)
        Me.TabPage1.Controls.Add(Me.txtDESCRIPCIO)
        Me.TabPage1.Controls.Add(Me.lblCodigoBanco)
        Me.TabPage1.Controls.Add(Me.btnCanviarLogo)
        Me.TabPage1.Controls.Add(Me.pibLOGO)
        Me.TabPage1.Controls.Add(Me.lblCuentasCredito)
        Me.TabPage1.Controls.Add(Me.lblNombreBanco)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(876, 466)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Banc"
        '
        'lblIBAN
        '
        Me.lblIBAN.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblIBAN.Location = New System.Drawing.Point(528, 124)
        Me.lblIBAN.Name = "lblIBAN"
        Me.lblIBAN.Size = New System.Drawing.Size(80, 20)
        Me.lblIBAN.TabIndex = 402
        Me.lblIBAN.Text = "IBAN"
        Me.lblIBAN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(528, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 20)
        Me.Label2.TabIndex = 401
        Me.Label2.Text = "SWIFT"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(496, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 20)
        Me.Label1.TabIndex = 400
        Me.Label1.Text = "Número Compte"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtIBAN
        '
        Me.txtIBAN.Location = New System.Drawing.Point(612, 124)
        Me.txtIBAN.MaxLength = 255
        Me.txtIBAN.Name = "txtIBAN"
        Me.txtIBAN.Size = New System.Drawing.Size(232, 20)
        Me.txtIBAN.TabIndex = 399
        Me.txtIBAN.Tag = Nothing
        '
        'txtSWIFT
        '
        Me.txtSWIFT.Location = New System.Drawing.Point(612, 96)
        Me.txtSWIFT.MaxLength = 255
        Me.txtSWIFT.Name = "txtSWIFT"
        Me.txtSWIFT.Size = New System.Drawing.Size(232, 20)
        Me.txtSWIFT.TabIndex = 398
        Me.txtSWIFT.Tag = Nothing
        '
        'txtNUMCUENTA
        '
        Me.txtNUMCUENTA.Location = New System.Drawing.Point(612, 68)
        Me.txtNUMCUENTA.MaxLength = 255
        Me.txtNUMCUENTA.Name = "txtNUMCUENTA"
        Me.txtNUMCUENTA.Size = New System.Drawing.Size(232, 20)
        Me.txtNUMCUENTA.TabIndex = 397
        Me.txtNUMCUENTA.Tag = Nothing
        '
        'cboEmpresa
        '
        Me.cboEmpresa.AllowColMove = True
        Me.cboEmpresa.AllowColSelect = True
        Me.cboEmpresa.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboEmpresa.AlternatingRows = False
        Me.cboEmpresa.CaptionHeight = 17
        Me.cboEmpresa.ColumnCaptionHeight = 17
        Me.cboEmpresa.ColumnFooterHeight = 17
        Me.cboEmpresa.FetchRowStyles = False
        Me.cboEmpresa.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.cboEmpresa.Location = New System.Drawing.Point(96, 220)
        Me.cboEmpresa.Name = "cboEmpresa"
        Me.cboEmpresa.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboEmpresa.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.cboEmpresa.RowHeight = 15
        Me.cboEmpresa.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboEmpresa.ScrollTips = False
        Me.cboEmpresa.Size = New System.Drawing.Size(60, 72)
        Me.cboEmpresa.TabIndex = 396
        Me.cboEmpresa.Text = "C1TrueDBDropdown1"
        Me.cboEmpresa.ValueTranslate = True
        Me.cboEmpresa.Visible = False
        Me.cboEmpresa.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:HighlightTe" & _
        "xt;BackColor:Highlight;}Style9{}OddRow{}RecordSelector{AlignImage:Center;}Headin" & _
        "g{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;Bac" & _
        "kColor:Control;}Style8{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style" & _
        "1{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.DropdownView Name="""" CaptionHeig" & _
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
        "Style1"" /><ClientRect>0, 0, 56, 68</ClientRect><BorderSide>0</BorderSide></C1.Wi" & _
        "n.C1TrueDBGrid.DropdownView></Splits><NamedStyles><Style parent="""" me=""Normal"" /" & _
        "><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /><St" & _
        "yle parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" /><Sty" & _
        "le parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Editor"" /><Style p" & _
        "arent=""Normal"" me=""HighlightRow"" /><Style parent=""Normal"" me=""EvenRow"" /><Style " & _
        "parent=""Normal"" me=""OddRow"" /><Style parent=""Heading"" me=""RecordSelector"" /><Sty" & _
        "le parent=""Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""Group"" /></Named" & _
        "Styles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>None</Layout>" & _
        "<DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 56, 68</ClientArea>" & _
        "</Blob>"
        '
        'cboTIPOCUENTA
        '
        Me.cboTIPOCUENTA.AllowColMove = True
        Me.cboTIPOCUENTA.AllowColSelect = True
        Me.cboTIPOCUENTA.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboTIPOCUENTA.AlternatingRows = False
        Me.cboTIPOCUENTA.CaptionHeight = 17
        Me.cboTIPOCUENTA.ColumnCaptionHeight = 17
        Me.cboTIPOCUENTA.ColumnFooterHeight = 17
        Me.cboTIPOCUENTA.FetchRowStyles = False
        Me.cboTIPOCUENTA.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.cboTIPOCUENTA.Location = New System.Drawing.Point(24, 220)
        Me.cboTIPOCUENTA.Name = "cboTIPOCUENTA"
        Me.cboTIPOCUENTA.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboTIPOCUENTA.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.cboTIPOCUENTA.RowHeight = 15
        Me.cboTIPOCUENTA.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboTIPOCUENTA.ScrollTips = False
        Me.cboTIPOCUENTA.Size = New System.Drawing.Size(60, 72)
        Me.cboTIPOCUENTA.TabIndex = 395
        Me.cboTIPOCUENTA.Text = "C1TrueDBDropdown1"
        Me.cboTIPOCUENTA.ValueTranslate = True
        Me.cboTIPOCUENTA.Visible = False
        Me.cboTIPOCUENTA.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
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
        "Style1"" /><ClientRect>0, 0, 56, 68</ClientRect><BorderSide>0</BorderSide></C1.Wi" & _
        "n.C1TrueDBGrid.DropdownView></Splits><NamedStyles><Style parent="""" me=""Normal"" /" & _
        "><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /><St" & _
        "yle parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" /><Sty" & _
        "le parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Editor"" /><Style p" & _
        "arent=""Normal"" me=""HighlightRow"" /><Style parent=""Normal"" me=""EvenRow"" /><Style " & _
        "parent=""Normal"" me=""OddRow"" /><Style parent=""Heading"" me=""RecordSelector"" /><Sty" & _
        "le parent=""Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""Group"" /></Named" & _
        "Styles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>None</Layout>" & _
        "<DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 56, 68</ClientArea>" & _
        "</Blob>"
        '
        'dgCuentas
        '
        Me.dgCuentas.AllowAddNew = True
        Me.dgCuentas.AllowDelete = True
        Me.dgCuentas.CaptionHeight = 17
        Me.dgCuentas.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgCuentas.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Image))
        Me.dgCuentas.Location = New System.Drawing.Point(8, 208)
        Me.dgCuentas.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.dgCuentas.Name = "dgCuentas"
        Me.dgCuentas.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgCuentas.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgCuentas.PreviewInfo.ZoomFactor = 75
        Me.dgCuentas.RecordSelectorWidth = 17
        Me.dgCuentas.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgCuentas.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.dgCuentas.RowHeight = 15
        Me.dgCuentas.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.dgCuentas.Size = New System.Drawing.Size(860, 212)
        Me.dgCuentas.TabIndex = 394
        Me.dgCuentas.Text = "C1TrueDBGrid2"
        Me.dgCuentas.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Style1{}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:Hig" & _
        "hlightText;BackColor:Highlight;}Style12{}OddRow{}RecordSelector{AlignImage:Cente" & _
        "r;}Style13{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeCo" & _
        "lor:ControlText;AlignVert:Center;}Style8{}Style10{AlignHorz:Near;}Style11{}Style" & _
        "14{}Style15{}Style9{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name" & _
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
        "><ClientRect>0, 0, 856, 208</ClientRect><BorderSide>0</BorderSide></C1.Win.C1Tru" & _
        "eDBGrid.MergeView></Splits><NamedStyles><Style parent="""" me=""Normal"" /><Style pa" & _
        "rent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /><Style parent" & _
        "=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" /><Style parent=" & _
        """Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Editor"" /><Style parent=""Nor" & _
        "mal"" me=""HighlightRow"" /><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""No" & _
        "rmal"" me=""OddRow"" /><Style parent=""Heading"" me=""RecordSelector"" /><Style parent=" & _
        """Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""Group"" /></NamedStyles><ve" & _
        "rtSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>None</Layout><DefaultRe" & _
        "cSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 856, 208</ClientArea><PrintPa" & _
        "geHeaderStyle parent="""" me=""Style14"" /><PrintPageFooterStyle parent="""" me=""Style" & _
        "15"" /></Blob>"
        '
        'cboID
        '
        Me.cboID.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cboID.AutoCompletion = True
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
        Me.cboID.FlatStyle = C1.Win.C1List.FlatModeEnum.System
        Me.cboID.GapHeight = 2
        Me.cboID.Images.Add(CType(resources.GetObject("resource3"), System.Drawing.Image))
        Me.cboID.ItemHeight = 15
        Me.cboID.Location = New System.Drawing.Point(92, 8)
        Me.cboID.MatchEntryTimeout = CType(100, Long)
        Me.cboID.MaxDropDownItems = CType(15, Short)
        Me.cboID.MaxLength = 32767
        Me.cboID.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboID.Name = "cboID"
        ''Me.cbo.PartialRightColumn = False
        Me.cboID.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboID.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboID.Size = New System.Drawing.Size(121, 19)
        Me.cboID.TabIndex = 392
        Me.cboID.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:T" & _
        "rue;BackColor:Control;Border:Flat,ControlDark,1, 1, 1, 1;ForeColor:ControlText;A" & _
        "lignVert:Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:Near;}</Data></Style" & _
        "s><Splits><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeigh" & _
        "t=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" " & _
        "HorizontalScrollGroup=""1""><ClientRect>0, 0, 118, 158</ClientRect><VScrollBar><Wi" & _
        "dth>17</Width></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionS" & _
        "tyle parent=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" />" & _
        "<FooterStyle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style1" & _
        "1"" /><HeadingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""Hig" & _
        "hlightRow"" me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowS" & _
        "tyle parent=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" " & _
        "me=""Style10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" m" & _
        "e=""Normal"" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" " & _
        "me=""Normal"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""" & _
        "Footer"" /><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Ina" & _
        "ctive"" /><Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Highl" & _
        "ightRow"" /><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddR" & _
        "ow"" /><Style parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""" & _
        "Group"" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layo" & _
        "ut>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'pibLOGO
        '
        Me.pibLOGO.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.pibLOGO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pibLOGO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.pibLOGO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pibLOGO.Location = New System.Drawing.Point(12, 68)
        Me.pibLOGO.Name = "pibLOGO"
        Me.pibLOGO.Size = New System.Drawing.Size(424, 96)
        Me.pibLOGO.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pibLOGO.TabIndex = 209
        Me.pibLOGO.TabStop = False
        '
        'frmBancosForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(908, 594)
        Me.Controls.Add(Me.TabControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.KeyPreview = True
        Me.Name = "frmBancosForm"
        Me.Text = " Bancs"
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
        CType(Me.txtDESCRIPCIO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.txtIBAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSWIFT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNUMCUENTA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboEmpresa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTIPOCUENTA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgCuentas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmBancosForm
    Public Shared Function GetInstance() As frmBancosForm
        If frmChildForm Is Nothing Then
            frmChildForm = New frmBancosForm
        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Public bancoActual As clsBanco
    
#End Region

#Region "INICIALIZAR"

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            Me.arrayTextBox = New System.Windows.Forms.Control() {Me.txtDESCRIPCIO, Me.txtIBAN, Me.txtSWIFT, Me.txtNUMCUENTA}
            Me.arrayPictureBox = New System.Windows.Forms.Control() {Me.pibLOGO}
            Me.arrayBotones = New System.Windows.Forms.Control() {Me.btnCanviarLogo}
            Me.arrayTabPages = New System.Windows.Forms.Control() {Me.TabPage1}
            Me.arrayGrids = New System.Windows.Forms.Control() {Me.dgCuentas}
            tabla = tablaBancos
            cargando = True
            bancoActual = New clsBanco(ds.Tables(tabla), "C", BindingContext)
            HacerBindings()
            IniciarDGs()
            PonerModificables(soloLectura)
            'bancoActual.tabla.AcceptChanges()
            PonerHandlersErroresParaGrids()
            PSBTIPO(bancoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub HacerBindings()
        Try
            HacerBindingsTodos(bancoActual.dvForm)
            AñadirBinding(cboID, bancoActual.dvForm, "CODI")
            AñadirBindingCombo(cboID, bancoActual.dvIdentificadores, CCBancs, CCBancs)
            AñadirBindingCombo(cboEmpresa, bancoActual.cuentas.dtFiliales, "CODI", "CODI")

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub IniciarDGs()
        Dim i As Integer
        Try
            dgCuentas.SetDataBinding(bancoActual.cuentas.dvForm, "")
            OcultarColumnasDG(dgCuentas)

            i = 0
            AñadirBindingCombo(cboTIPOCUENTA, bancoActual.cuentas.dtTipo, "TIPO", "NOMTIPO")
            PPCol2("TIPO", dgCuentas, rm.GetString("TIPO"), "", True, 80, False, _
                C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox, False, 80, i, False, cboTIPOCUENTA)

            i = i + 1
            PPCol2("CUENTA", dgCuentas, rm.GetString("CUENTA"), "", True, 200, False, _
               C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 200, i, False)

            i = i + 1
            PPCol2("DOM", dgCuentas, rm.GetString("DOMICILIO"), "", True, 200, False, _
                C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 200, i, False)

            i = i + 1
            PPCol2("POB", dgCuentas, rm.GetString("POBLACION"), "", True, 200, False, _
                C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 200, i, False)

            i = i + 1
            'PPCol2("PROV", dgCuentas, rm.GetString("PROVINCIA"), "", True, 200, False, _
            '    C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 200, i, False)

            i = i + 1
            PPCol2("CP", dgCuentas, rm.GetString("CP"), "", True, 150, False, _
                C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 150, i, False)

            i = i + 1
            PPCol2("CENTRO", dgCuentas, rm.GetString("EMPRESA"), "", True, 200, False, _
                C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 200, i, False, cboEmpresa)

            i = i + 1
            PPCol2("NOMCENTRO", dgCuentas, rm.GetString("EMPRESA"), "", True, 200, False, _
                C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 200, i, False, Nothing, False)

            AutoSizeCC(dgCuentas)
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
                'bancoActual.tabla.AcceptChanges()
                PonerModificables(soloLectura)
                cboID.Text = bancoActual.CODI
            End If
            If EsRegistroNuevo Then
                editando = True : PSBTIPO(bancoActual.centro)
                PonerControlesNuevo(True)
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
            If bancoActual.TieneCambios Then
                Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                    Case DialogResult.Cancel
                        cargando = False
                        Exit Sub
                    Case DialogResult.No
                        bancoActual.tabla.RejectChanges()
                        bancoActual.cuentas.tabla.RejectChanges()
                        cargando = False
                        Exit Sub
                End Select
            End If
            bancoActual.ActualizarOrigen() : ActualizarOrigen()
            PSBTIPO(bancoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnBorrar.Click
        If MessageBox.Show(rm.GetString("BorrarBanco"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
            editando = True
            bancoActual.borrar() : ActualizarOrigen()
            editando = False
        End If
    End Sub
    Friend Overrides Sub btnModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles btnModificar.Click
        Try
            If Not editando Then
                cargando = True
                editando = True
                PonerModificables(modificable)
                cboID.Text = bancoActual.CODI
                PSBTIPO(bancoActual.centro)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnNuevo.Click
        Try
            Cursor = Cursors.WaitCursor
            cargando = True
            EsRegistroNuevo = True
            PonerModificables(modificable)
            PonerControlesNuevo(False)
            PSBTIPO(bancoActual.centro)
            'cboID.ClearItems()
            btnNuevo.Visible = False
            bancoActual.NuevoRegistro()
            MoverActual(True)
            PSBTIPO(bancoActual.centro) : cargando = False
            Cursor = Cursors.Default
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ESPECÍFICO"

    Private Sub btnCanviarLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCanviarLogo.Click
        Dim Foto() As Byte
        Dim NavArchivos As System.Windows.Forms.OpenFileDialog
        Dim Fs As System.IO.FileStream
        Try
            NavArchivos = New System.Windows.Forms.OpenFileDialog
            NavArchivos.ShowDialog()
            Fs = New System.IO.FileStream(NavArchivos.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)
            Foto = New Byte(Fs.Length) {}
            Fs.Read(Foto, 0, Fs.Length)
            Fs.Close()
            If Not Foto Is Nothing AndAlso Not IsDBNull(Foto) Then
                Dim img() As Byte = CType(Foto, Byte())
                bancoActual.LOGO = img
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN() : Fs.Close()
        End Try
    End Sub

#End Region

#Region "IMPRESIÓN"

#End Region

#Region "COMUNES"

    Private Sub btnVerLista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerLista.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmBancosLista = frmBancosLista.GetInstance(esListado)
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
    Private Sub PonerControlesNuevo(ByVal b As Boolean)
        Try
            Try

                If b Then
                    btnActualizar.Text = rm.GetString("ACTUALIZAR")
                Else
                    btnActualizar.Text = rm.GetString("CONFIRMAR")
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
    Private Sub PonerModificables(Optional ByVal b As Boolean = False)
        Try
            PonerMod(b)
            If Not b Then : cboID.DataSource = Nothing
            Else : AñadirBindingCombo(cboID, bancoActual.dvIdentificadores, CCBancs, CCBancs) : bancoActual.tabla.AcceptChanges() : End If

            cboID.LimitToList = b
            cboID.SuperBack = b
            cboID.ReadOnly = editando
            ModiNuev(b)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Sub btnRecargar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecargar.Click
        Try
            cargando = True
            Dim id As Object
            id = cboID.Text
            bancoActual.ActualizarOrigen()
            bancoActual.CambiarAReg(id, iraregistro)
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
            bancoActual.PrimeroReg() : ActualizarOrigen()
            cargando = True
            MoverActual()
            PSBTIPO(bancoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUltimo.Click
        Try
            cargando = True
            bancoActual.UltimoReg() : ActualizarOrigen()
            MoverActual()
            PSBTIPO(bancoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAnterior.Click
        Try
            cargando = True
            bancoActual.AnteriorReg() : ActualizarOrigen()
            MoverActual()
            PSBTIPO(bancoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSiguiente.Click
        Try
            cargando = True
            bancoActual.SiguienteReg() : ActualizarOrigen()
            MoverActual()
            PSBTIPO(bancoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub MoverActual(Optional ByVal nuevo As Boolean = False)
        Try
            cargando = True
            PSBTIPO(bancoActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "SELECCIÓN REGISTRO"

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
    Private Sub cboID_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboID.RowChange
        Try
            If consulta() Then
                cargando = True
                bancoActual.CambiarAReg(GENERAL.nz(cboID.WillChangeToValue, ""), iraregistro)
                MoverActual()
                PSBTIPO(bancoActual.centro) : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#Region "SELECCION CENTRO"

    Private Sub cboSeleccion_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSeleccionCentro.SelectionChangeCommitted
        Try
            If consulta() Then
                cargando = True
                bancoActual.cambioCentro(GENERAL.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto), primero)
                'bancoActual.tabla.AcceptChanges()
                cboSeleccionCentro.SelectedValue = bancoActual.centro
                cargando = False
            Else
                If EsRegistroNuevo And Not cargando Then
                    cargando = True
                    bancoActual.centro = general.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto)
                    bancoActual.tabla.Clear()
                    bancoActual.NuevoRegistro()
                    PSBTIPO(bancoActual.centro)
                    cargando = False
                End If
            End If
            cargando = False
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#End Region

#Region "ELECCIONES"

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
    Private Sub dgCuentas_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCuentas.AfterUpdate
        Try
            AutoSizeCC(dgCuentas)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

End Class
