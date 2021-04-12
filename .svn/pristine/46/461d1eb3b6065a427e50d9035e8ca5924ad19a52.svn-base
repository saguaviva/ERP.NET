Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes



Public Class frmEstFacturacionArticulos
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
        frmChildForm = Nothing
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents dgEstadisticas As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents btnGenerarEstadísitcas As C1.Win.C1Input.C1Button

    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkHilos As System.Windows.Forms.CheckBox
    Friend WithEvents chkTejidos As System.Windows.Forms.CheckBox
    Friend WithEvents chkMuestras As System.Windows.Forms.CheckBox
    Friend WithEvents chkModelos As System.Windows.Forms.CheckBox
    Friend WithEvents tabEstaditicas As System.Windows.Forms.TabPage
    Friend WithEvents cboEmpresas As C1.Win.C1List.C1Combo
    Friend WithEvents dtpFecha1 As C1.Win.C1Input.C1DateEdit
    Friend WithEvents dtpFecha2 As C1.Win.C1Input.C1DateEdit
    Friend WithEvents chkAgruparFacturas As System.Windows.Forms.CheckBox
    Friend WithEvents gbEmpresa As System.Windows.Forms.GroupBox
    Friend WithEvents gbPersona As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents tabMejores As System.Windows.Forms.TabPage
    Friend WithEvents chkAgruparPorCliente As System.Windows.Forms.CheckBox
    Friend WithEvents chkAgruparPorArticulo As System.Windows.Forms.CheckBox
    Friend WithEvents gbAgrupacion As System.Windows.Forms.GroupBox
    Friend WithEvents chkAgruparPorTipo As System.Windows.Forms.CheckBox
    Friend WithEvents btnExportarExcel As C1.Win.C1Input.C1Button
    Friend WithEvents chkPorMeses As System.Windows.Forms.CheckBox
    Friend WithEvents chkPerAnys As System.Windows.Forms.CheckBox
    Friend WithEvents rdoFacturas As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAlbaranes As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPedidos As System.Windows.Forms.RadioButton
    Friend WithEvents rdoClients As System.Windows.Forms.RadioButton
    Friend WithEvents rdoProveedores As System.Windows.Forms.RadioButton
    Friend WithEvents chkFornituras As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmEstFacturacionArticulos))
        Me.dgEstadisticas = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.btnGenerarEstadísitcas = New C1.Win.C1Input.C1Button
        Me.dtpFecha1 = New C1.Win.C1Input.C1DateEdit
        Me.dtpFecha2 = New C1.Win.C1Input.C1DateEdit
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tabEstaditicas = New System.Windows.Forms.TabPage
        Me.tabMejores = New System.Windows.Forms.TabPage
        Me.gbAgrupacion = New System.Windows.Forms.GroupBox
        Me.chkPerAnys = New System.Windows.Forms.CheckBox
        Me.chkPorMeses = New System.Windows.Forms.CheckBox
        Me.chkAgruparPorTipo = New System.Windows.Forms.CheckBox
        Me.chkAgruparFacturas = New System.Windows.Forms.CheckBox
        Me.chkAgruparPorCliente = New System.Windows.Forms.CheckBox
        Me.chkAgruparPorArticulo = New System.Windows.Forms.CheckBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnExportarExcel = New C1.Win.C1Input.C1Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.chkModelos = New System.Windows.Forms.CheckBox
        Me.chkMuestras = New System.Windows.Forms.CheckBox
        Me.chkTejidos = New System.Windows.Forms.CheckBox
        Me.chkHilos = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.rdoFacturas = New System.Windows.Forms.RadioButton
        Me.rdoAlbaranes = New System.Windows.Forms.RadioButton
        Me.rdoPedidos = New System.Windows.Forms.RadioButton
        Me.gbPersona = New System.Windows.Forms.GroupBox
        Me.rdoClients = New System.Windows.Forms.RadioButton
        Me.rdoProveedores = New System.Windows.Forms.RadioButton
        Me.gbEmpresa = New System.Windows.Forms.GroupBox
        Me.cboEmpresas = New C1.Win.C1List.C1Combo
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkFornituras = New System.Windows.Forms.CheckBox
        CType(Me.dgEstadisticas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFecha1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFecha2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.tabEstaditicas.SuspendLayout()
        Me.gbAgrupacion.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.gbPersona.SuspendLayout()
        Me.gbEmpresa.SuspendLayout()
        CType(Me.cboEmpresas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(5, 557)
        Me.sbtipo.Name = "sbtipo"
        Me.sbtipo.Size = New System.Drawing.Size(88, 15)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(60, 516)
        Me.btnRecargar.Name = "btnRecargar"
        Me.btnRecargar.Size = New System.Drawing.Size(90, 19)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(308, 535)
        Me.btnSiguiente.Name = "btnSiguiente"
        Me.btnSiguiente.Size = New System.Drawing.Size(32, 18)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(28, 535)
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.Size = New System.Drawing.Size(32, 18)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(28, 516)
        Me.btnPrimero.Name = "btnPrimero"
        Me.btnPrimero.Size = New System.Drawing.Size(32, 19)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(308, 516)
        Me.btnUltimo.Name = "btnUltimo"
        Me.btnUltimo.Size = New System.Drawing.Size(32, 19)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(148, 535)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New System.Drawing.Size(98, 18)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(912, 535)
        Me.btnTancar.Name = "btnTancar"
        Me.btnTancar.Size = New System.Drawing.Size(72, 18)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(228, 535)
        Me.btnBorrar.Name = "btnBorrar"
        Me.btnBorrar.Size = New System.Drawing.Size(98, 18)
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.Location = New System.Drawing.Point(280, 7)
        Me.cboSeleccionCentro.Name = "cboSeleccionCentro"
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(148, 516)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(82, 37)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(912, 516)
        Me.btnVerLista.Name = "btnVerLista"
        Me.btnVerLista.Size = New System.Drawing.Size(72, 19)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(60, 535)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(90, 18)
        '
        'dgEstadisticas
        '
        Me.dgEstadisticas.CaptionHeight = 17
        Me.dgEstadisticas.FilterBar = True
        Me.dgEstadisticas.GroupByCaption = "Desplaça aquí una capçalera de columna per agrupar per aquesta columna"
        Me.dgEstadisticas.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.dgEstadisticas.Location = New System.Drawing.Point(4, 8)
        Me.dgEstadisticas.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.dgEstadisticas.Name = "dgEstadisticas"
        Me.dgEstadisticas.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgEstadisticas.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgEstadisticas.PreviewInfo.ZoomFactor = 75
        Me.dgEstadisticas.RecordSelectorWidth = 17
        Me.dgEstadisticas.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgEstadisticas.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.dgEstadisticas.RowHeight = 15
        Me.dgEstadisticas.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.dgEstadisticas.Size = New System.Drawing.Size(948, 312)
        Me.dgEstadisticas.TabIndex = 12
        Me.dgEstadisticas.Text = "C1TrueDBGrid1"
        Me.dgEstadisticas.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Style1{}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:Hig" & _
        "hlightText;BackColor:Highlight;}Style14{}OddRow{}RecordSelector{AlignImage:Cente" & _
        "r;}Style15{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeCo" & _
        "lor:ControlText;AlignVert:Center;}Style8{}Style10{AlignHorz:Near;}Style11{}Style" & _
        "12{}Style13{}Style9{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBar" & _
        "Height=""16"" Name="""" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeig" & _
        "ht=""17"" FilterBar=""True"" MarqueeStyle=""DottedCellBorder"" RecordSelectorWidth=""17" & _
        """ DefRecSelWidth=""17"" VerticalScrollGroup=""1"" HorizontalScrollGroup=""1""><Caption" & _
        "Style parent=""Style2"" me=""Style10"" /><EditorStyle parent=""Editor"" me=""Style5"" />" & _
        "<EvenRowStyle parent=""EvenRow"" me=""Style8"" /><FilterBarStyle parent=""FilterBar"" " & _
        "me=""Style13"" /><FooterStyle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Gr" & _
        "oup"" me=""Style12"" /><HeadingStyle parent=""Heading"" me=""Style2"" /><HighLightRowSt" & _
        "yle parent=""HighlightRow"" me=""Style7"" /><InactiveStyle parent=""Inactive"" me=""Sty" & _
        "le4"" /><OddRowStyle parent=""OddRow"" me=""Style9"" /><RecordSelectorStyle parent=""R" & _
        "ecordSelector"" me=""Style11"" /><SelectedStyle parent=""Selected"" me=""Style6"" /><St" & _
        "yle parent=""Normal"" me=""Style1"" /><ClientRect>0, 0, 944, 308</ClientRect><Border" & _
        "Side>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style " & _
        "parent="""" me=""Normal"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Hea" & _
        "ding"" me=""Footer"" /><Style parent=""Heading"" me=""Caption"" /><Style parent=""Headin" & _
        "g"" me=""Inactive"" /><Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal""" & _
        " me=""Editor"" /><Style parent=""Normal"" me=""HighlightRow"" /><Style parent=""Normal""" & _
        " me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><Style parent=""Heading"" me=" & _
        """RecordSelector"" /><Style parent=""Normal"" me=""FilterBar"" /><Style parent=""Captio" & _
        "n"" me=""Group"" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplit" & _
        "s><Layout>None</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0," & _
        " 0, 944, 308</ClientArea><PrintPageHeaderStyle parent="""" me=""Style14"" /><PrintPa" & _
        "geFooterStyle parent="""" me=""Style15"" /></Blob>"
        '
        'btnGenerarEstadísitcas
        '
        Me.btnGenerarEstadísitcas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerarEstadísitcas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerarEstadísitcas.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnGenerarEstadísitcas.Location = New System.Drawing.Point(8, 8)
        Me.btnGenerarEstadísitcas.Name = "btnGenerarEstadísitcas"
        Me.btnGenerarEstadísitcas.Size = New System.Drawing.Size(112, 28)
        Me.btnGenerarEstadísitcas.TabIndex = 17
        Me.btnGenerarEstadísitcas.Text = "Generar Estadísticas"
        '
        'dtpFecha1
        '
        '
        'dtpFecha1.Calendar
        '
        Me.dtpFecha1.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpFecha1.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpFecha1.Location = New System.Drawing.Point(120, 84)
        Me.dtpFecha1.Name = "dtpFecha1"
        Me.dtpFecha1.Size = New System.Drawing.Size(124, 20)
        Me.dtpFecha1.TabIndex = 19
        Me.dtpFecha1.Tag = Nothing
        '
        'dtpFecha2
        '
        '
        'dtpFecha2.Calendar
        '
        Me.dtpFecha2.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dtpFecha2.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpFecha2.Location = New System.Drawing.Point(120, 112)
        Me.dtpFecha2.Name = "dtpFecha2"
        Me.dtpFecha2.Size = New System.Drawing.Size(124, 20)
        Me.dtpFecha2.TabIndex = 20
        Me.dtpFecha2.Tag = Nothing
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabEstaditicas)
        Me.TabControl1.Controls.Add(Me.tabMejores)
        Me.TabControl1.ItemSize = New System.Drawing.Size(60, 18)
        Me.TabControl1.Location = New System.Drawing.Point(4, 156)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(980, 352)
        Me.TabControl1.TabIndex = 27
        '
        'tabEstaditicas
        '
        Me.tabEstaditicas.BackColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        Me.tabEstaditicas.Controls.Add(Me.dgEstadisticas)
        Me.tabEstaditicas.Location = New System.Drawing.Point(4, 22)
        Me.tabEstaditicas.Name = "tabEstaditicas"
        Me.tabEstaditicas.Size = New System.Drawing.Size(972, 326)
        Me.tabEstaditicas.TabIndex = 0
        Me.tabEstaditicas.Text = "Estadistiques"
        '
        'tabMejores
        '
        Me.tabMejores.BackColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        Me.tabMejores.Location = New System.Drawing.Point(4, 22)
        Me.tabMejores.Name = "tabMejores"
        Me.tabMejores.Size = New System.Drawing.Size(972, 342)
        Me.tabMejores.TabIndex = 1
        Me.tabMejores.Text = "Gràfics"
        '
        'gbAgrupacion
        '
        Me.gbAgrupacion.Controls.Add(Me.chkPerAnys)
        Me.gbAgrupacion.Controls.Add(Me.chkPorMeses)
        Me.gbAgrupacion.Controls.Add(Me.chkAgruparPorTipo)
        Me.gbAgrupacion.Controls.Add(Me.chkAgruparFacturas)
        Me.gbAgrupacion.Controls.Add(Me.chkAgruparPorCliente)
        Me.gbAgrupacion.Controls.Add(Me.chkAgruparPorArticulo)
        Me.gbAgrupacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.gbAgrupacion.Location = New System.Drawing.Point(692, 4)
        Me.gbAgrupacion.Name = "gbAgrupacion"
        Me.gbAgrupacion.Size = New System.Drawing.Size(284, 148)
        Me.gbAgrupacion.TabIndex = 41
        Me.gbAgrupacion.TabStop = False
        Me.gbAgrupacion.Text = "Agrupacions"
        '
        'chkPerAnys
        '
        Me.chkPerAnys.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkPerAnys.Location = New System.Drawing.Point(144, 76)
        Me.chkPerAnys.Name = "chkPerAnys"
        Me.chkPerAnys.Size = New System.Drawing.Size(132, 24)
        Me.chkPerAnys.TabIndex = 43
        Me.chkPerAnys.Text = "Per Anys"
        '
        'chkPorMeses
        '
        Me.chkPorMeses.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkPorMeses.Location = New System.Drawing.Point(8, 76)
        Me.chkPorMeses.Name = "chkPorMeses"
        Me.chkPorMeses.Size = New System.Drawing.Size(128, 24)
        Me.chkPorMeses.TabIndex = 42
        Me.chkPorMeses.Text = "Per Mesos"
        '
        'chkAgruparPorTipo
        '
        Me.chkAgruparPorTipo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkAgruparPorTipo.Location = New System.Drawing.Point(8, 48)
        Me.chkAgruparPorTipo.Name = "chkAgruparPorTipo"
        Me.chkAgruparPorTipo.Size = New System.Drawing.Size(128, 24)
        Me.chkAgruparPorTipo.TabIndex = 41
        Me.chkAgruparPorTipo.Text = "Per Sector de Negoci"
        '
        'chkAgruparFacturas
        '
        Me.chkAgruparFacturas.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkAgruparFacturas.Location = New System.Drawing.Point(8, 20)
        Me.chkAgruparFacturas.Name = "chkAgruparFacturas"
        Me.chkAgruparFacturas.Size = New System.Drawing.Size(128, 24)
        Me.chkAgruparFacturas.TabIndex = 33
        Me.chkAgruparFacturas.Text = "Per Documents"
        '
        'chkAgruparPorCliente
        '
        Me.chkAgruparPorCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkAgruparPorCliente.Location = New System.Drawing.Point(144, 48)
        Me.chkAgruparPorCliente.Name = "chkAgruparPorCliente"
        Me.chkAgruparPorCliente.Size = New System.Drawing.Size(136, 24)
        Me.chkAgruparPorCliente.TabIndex = 39
        Me.chkAgruparPorCliente.Text = "Per Client / Proveïdor"
        '
        'chkAgruparPorArticulo
        '
        Me.chkAgruparPorArticulo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkAgruparPorArticulo.Location = New System.Drawing.Point(144, 20)
        Me.chkAgruparPorArticulo.Name = "chkAgruparPorArticulo"
        Me.chkAgruparPorArticulo.Size = New System.Drawing.Size(136, 24)
        Me.chkAgruparPorArticulo.TabIndex = 40
        Me.chkAgruparPorArticulo.Text = "Per article"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DarkGreen
        Me.Panel1.Controls.Add(Me.btnExportarExcel)
        Me.Panel1.Controls.Add(Me.btnGenerarEstadísitcas)
        Me.Panel1.Location = New System.Drawing.Point(676, 520)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(248, 44)
        Me.Panel1.TabIndex = 38
        '
        'btnExportarExcel
        '
        Me.btnExportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportarExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExportarExcel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExportarExcel.Location = New System.Drawing.Point(128, 8)
        Me.btnExportarExcel.Name = "btnExportarExcel"
        Me.btnExportarExcel.Size = New System.Drawing.Size(112, 28)
        Me.btnExportarExcel.TabIndex = 18
        Me.btnExportarExcel.Text = "Exportar Excel"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkFornituras)
        Me.GroupBox3.Controls.Add(Me.chkModelos)
        Me.GroupBox3.Controls.Add(Me.chkMuestras)
        Me.GroupBox3.Controls.Add(Me.chkTejidos)
        Me.GroupBox3.Controls.Add(Me.chkHilos)
        Me.GroupBox3.Location = New System.Drawing.Point(560, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(128, 148)
        Me.GroupBox3.TabIndex = 37
        Me.GroupBox3.TabStop = False
        '
        'chkModelos
        '
        Me.chkModelos.BackColor = System.Drawing.Color.Transparent
        Me.chkModelos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkModelos.Location = New System.Drawing.Point(8, 92)
        Me.chkModelos.Name = "chkModelos"
        Me.chkModelos.Size = New System.Drawing.Size(104, 22)
        Me.chkModelos.TabIndex = 3
        Me.chkModelos.Text = "Models"
        '
        'chkMuestras
        '
        Me.chkMuestras.BackColor = System.Drawing.Color.Transparent
        Me.chkMuestras.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkMuestras.Location = New System.Drawing.Point(8, 68)
        Me.chkMuestras.Name = "chkMuestras"
        Me.chkMuestras.Size = New System.Drawing.Size(104, 22)
        Me.chkMuestras.TabIndex = 2
        Me.chkMuestras.Text = "Mostres"
        '
        'chkTejidos
        '
        Me.chkTejidos.BackColor = System.Drawing.Color.Transparent
        Me.chkTejidos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkTejidos.Location = New System.Drawing.Point(8, 44)
        Me.chkTejidos.Name = "chkTejidos"
        Me.chkTejidos.Size = New System.Drawing.Size(104, 22)
        Me.chkTejidos.TabIndex = 1
        Me.chkTejidos.Text = "Teixits"
        '
        'chkHilos
        '
        Me.chkHilos.BackColor = System.Drawing.Color.Transparent
        Me.chkHilos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkHilos.Location = New System.Drawing.Point(8, 20)
        Me.chkHilos.Name = "chkHilos"
        Me.chkHilos.Size = New System.Drawing.Size(104, 22)
        Me.chkHilos.TabIndex = 0
        Me.chkHilos.Text = "Fils"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rdoFacturas)
        Me.GroupBox2.Controls.Add(Me.rdoAlbaranes)
        Me.GroupBox2.Controls.Add(Me.rdoPedidos)
        Me.GroupBox2.Location = New System.Drawing.Point(396, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(160, 148)
        Me.GroupBox2.TabIndex = 36
        Me.GroupBox2.TabStop = False
        '
        'rdoFacturas
        '
        Me.rdoFacturas.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoFacturas.Location = New System.Drawing.Point(8, 84)
        Me.rdoFacturas.Name = "rdoFacturas"
        Me.rdoFacturas.Size = New System.Drawing.Size(132, 24)
        Me.rdoFacturas.TabIndex = 4
        Me.rdoFacturas.Text = "Factures"
        '
        'rdoAlbaranes
        '
        Me.rdoAlbaranes.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoAlbaranes.Location = New System.Drawing.Point(8, 52)
        Me.rdoAlbaranes.Name = "rdoAlbaranes"
        Me.rdoAlbaranes.Size = New System.Drawing.Size(132, 24)
        Me.rdoAlbaranes.TabIndex = 3
        Me.rdoAlbaranes.Text = "Albarans"
        '
        'rdoPedidos
        '
        Me.rdoPedidos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoPedidos.Location = New System.Drawing.Point(8, 20)
        Me.rdoPedidos.Name = "rdoPedidos"
        Me.rdoPedidos.Size = New System.Drawing.Size(132, 24)
        Me.rdoPedidos.TabIndex = 2
        Me.rdoPedidos.Text = "Comandes"
        '
        'gbPersona
        '
        Me.gbPersona.Controls.Add(Me.rdoClients)
        Me.gbPersona.Controls.Add(Me.rdoProveedores)
        Me.gbPersona.Location = New System.Drawing.Point(256, 4)
        Me.gbPersona.Name = "gbPersona"
        Me.gbPersona.Size = New System.Drawing.Size(132, 148)
        Me.gbPersona.TabIndex = 35
        Me.gbPersona.TabStop = False
        Me.gbPersona.Text = "Clients/Proveïdors"
        '
        'rdoClients
        '
        Me.rdoClients.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoClients.Location = New System.Drawing.Point(8, 60)
        Me.rdoClients.Name = "rdoClients"
        Me.rdoClients.Size = New System.Drawing.Size(108, 24)
        Me.rdoClients.TabIndex = 1
        Me.rdoClients.Text = "Clients"
        '
        'rdoProveedores
        '
        Me.rdoProveedores.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoProveedores.Location = New System.Drawing.Point(8, 28)
        Me.rdoProveedores.Name = "rdoProveedores"
        Me.rdoProveedores.Size = New System.Drawing.Size(108, 24)
        Me.rdoProveedores.TabIndex = 0
        Me.rdoProveedores.Text = "Proveïdors"
        '
        'gbEmpresa
        '
        Me.gbEmpresa.Controls.Add(Me.cboEmpresas)
        Me.gbEmpresa.Location = New System.Drawing.Point(12, 8)
        Me.gbEmpresa.Name = "gbEmpresa"
        Me.gbEmpresa.Size = New System.Drawing.Size(236, 64)
        Me.gbEmpresa.TabIndex = 34
        Me.gbEmpresa.TabStop = False
        Me.gbEmpresa.Text = "Empresa"
        '
        'cboEmpresas
        '
        Me.cboEmpresas.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cboEmpresas.Caption = ""
        Me.cboEmpresas.CaptionHeight = 17
        Me.cboEmpresas.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboEmpresas.ColumnCaptionHeight = 17
        Me.cboEmpresas.ColumnFooterHeight = 17
        Me.cboEmpresas.ContentHeight = 15
        Me.cboEmpresas.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboEmpresas.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboEmpresas.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboEmpresas.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboEmpresas.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboEmpresas.EditorHeight = 15
        Me.cboEmpresas.GapHeight = 2
        Me.cboEmpresas.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.cboEmpresas.ItemHeight = 15
        Me.cboEmpresas.Location = New System.Drawing.Point(8, 24)
        Me.cboEmpresas.MatchEntryTimeout = CType(100, Long)
        Me.cboEmpresas.MaxDropDownItems = CType(5, Short)
        Me.cboEmpresas.MaxLength = 32767
        Me.cboEmpresas.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboEmpresas.Name = "cboEmpresas"
        'Me.cbo.PartialRightColumn = False
        Me.cboEmpresas.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboEmpresas.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboEmpresas.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboEmpresas.Size = New System.Drawing.Size(224, 21)
        Me.cboEmpresas.TabIndex = 14
        Me.cboEmpresas.Text = "C1Combo1"
        Me.cboEmpresas.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 8" & _
        ".25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight" & _
        ";}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:" & _
        "True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:" & _
        "Control;}Style8{}Style10{}Style11{}Style1{}</Data></Styles><Splits><C1.Win.C1Lis" & _
        "t.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" ColumnCaptionHei" & _
        "ght=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" HorizontalScrollGroup=""" & _
        "1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</Width></VScroll" & _
        "Bar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle parent=""Style2"" me" & _
        "=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterStyle parent=""Fo" & _
        "oter"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><HeadingStyle pare" & _
        "nt=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow"" me=""Style6"" " & _
        "/><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle parent=""OddRow"" me" & _
        "=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style10"" /><Selecte" & _
        "dStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal"" /></C1.Win.C" & _
        "1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Normal"" /><Style pa" & _
        "rent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /><Style parent" & _
        "=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" /><Style parent=" & _
        """Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" /><Style paren" & _
        "t=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><Style parent=""He" & _
        "ading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" /></NamedStyles>" & _
        "<vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><De" & _
        "faultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'Label2
        '
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(32, 112)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 20)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "a la data"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(32, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 20)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "De la data"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkFornituras
        '
        Me.chkFornituras.BackColor = System.Drawing.Color.Transparent
        Me.chkFornituras.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkFornituras.Location = New System.Drawing.Point(7, 116)
        Me.chkFornituras.Name = "chkFornituras"
        Me.chkFornituras.Size = New System.Drawing.Size(104, 22)
        Me.chkFornituras.TabIndex = 4
        Me.chkFornituras.Text = "Fornitures"
        '
        'frmEstFacturacionArticulos
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        Me.CancelButton = Nothing
        Me.ClientSize = New System.Drawing.Size(992, 570)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.gbPersona)
        Me.Controls.Add(Me.gbEmpresa)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpFecha1)
        Me.Controls.Add(Me.dtpFecha2)
        Me.Controls.Add(Me.gbAgrupacion)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Panel1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Name = "frmEstFacturacionArticulos"
        Me.Text = "Estadístiques Facturació Clients"
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox3, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.gbAgrupacion, 0)
        Me.Controls.SetChildIndex(Me.dtpFecha2, 0)
        Me.Controls.SetChildIndex(Me.dtpFecha1, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.gbEmpresa, 0)
        Me.Controls.SetChildIndex(Me.gbPersona, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
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
        CType(Me.dgEstadisticas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFecha1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFecha2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.tabEstaditicas.ResumeLayout(False)
        Me.gbAgrupacion.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.gbPersona.ResumeLayout(False)
        Me.gbEmpresa.ResumeLayout(False)
        CType(Me.cboEmpresas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmEstFacturacionArticulos

    Public Shared Function GetInstance() As frmEstFacturacionArticulos
        If frmChildForm Is Nothing Then
            frmChildForm = New frmEstFacturacionArticulos

        End If
        Return frmChildForm
    End Function

    Private clsEst As clsEstadisticas

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            AddHandler rdoAlbaranes.CheckedChanged, AddressOf cambioDeAlgunRadio
            AddHandler rdoClients.CheckedChanged, AddressOf cambioDeAlgunRadio
            AddHandler rdoFacturas.CheckedChanged, AddressOf cambioDeAlgunRadio
            AddHandler rdoPedidos.CheckedChanged, AddressOf cambioDeAlgunRadio
            AddHandler rdoProveedores.CheckedChanged, AddressOf cambioDeAlgunRadio

            clsEst = New clsEstadisticas
            HacerBindings()
            dgEstadisticas.FooterStyle.Font = New System.Drawing.Font("Tahoma", 12, FontStyle.Bold, GraphicsUnit.Pixel)
            cboEmpresas.SelectedValue = empresaPorDefecto
            rdoFacturas.Checked = True
            dtpFecha1.Value = Date.Now.AddMonths(-2)
            dtpFecha2.Value = Date.Now
            dgEstadisticas.AllowFilter = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Private Sub HacerBindings()
        Try
            dgEstadisticas.SetDataBinding(clsEst.dvEstadisticas, "")
            dgEstadisticas.ColumnFooters = True
            AñadirBindingCombo(cboEmpresas, dtFiliales, "CODI", "DESCRI")

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

    Private Sub GenerarArraysEst()
        Try
            clsEst.aDocumentos.Clear()
            clsEst.aEmpresas.Clear()
            clsEst.aTiposDeArticulos.Clear()

            'Documentos
            If rdoPedidos.Checked = True Then
                clsEst.aDocumentos.Add(Pedido)
            End If
            If rdoAlbaranes.Checked = True Then
                clsEst.aDocumentos.Add(Albaran)
            End If
            If rdoFacturas.Checked = True Then
                clsEst.aDocumentos.Add(Factura)
            End If

            'Empresas
            clsEst.aEmpresas.Add(cboEmpresas.SelectedValue)

            'Fechas
            clsEst.DesdeFecha = dtpFecha1.Value
            clsEst.AFecha = dtpFecha2.Value

            If chkHilos.Checked = True Then clsEst.aTiposDeArticulos.Add(Hilos)
            If chkTejidos.Checked = True Then clsEst.aTiposDeArticulos.Add(Tejido)
            If chkMuestras.Checked = True Then clsEst.aTiposDeArticulos.Add(Muestra)
            If chkModelos.Checked = True Then clsEst.aTiposDeArticulos.Add(Prenda)

            clsEst.agruparFactures = chkAgruparFacturas.Checked
            clsEst.agruparArticulos = chkAgruparPorArticulo.Checked
            clsEst.agruparClientes = chkAgruparPorCliente.Checked
            clsEst.agruparTipo = chkAgruparPorTipo.Checked
            clsEst.agruparMeses = chkPorMeses.Checked
            clsEst.agruparAños = chkPerAnys.Checked

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#Region "OVERRIDES"

    Friend Overrides Sub btnActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Friend Overrides Sub btnAnterior_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Friend Overrides Sub btnBorrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Friend Overrides Sub btnModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Friend Overrides Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Friend Overrides Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Friend Overrides Sub btnUltimo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

#End Region

    Private Sub IniciarDG()
        Dim i As Integer
        Try
            dgEstadisticas.SetDataBinding(clsEst.dvEstadisticas, "")
            OcultarColumnasDG(dgEstadisticas)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try

        Try : i = i + 1
            PPCol2("FRA", dgEstadisticas, rm.GetString("NUMERO"), "", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, True, Nothing, True)
        Catch ex As Exception : i = i - 1 : LOG(ex.ToString) : End Try

        Try : i = i + 1
            PPCol2("DATA", dgEstadisticas, rm.GetString("FECHA"), "Short Date", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, True, Nothing, True)
        Catch ex As Exception : i = i - 1 : LOG(ex.ToString) : End Try

        Try : i = i + 1
            PPCol2("MES", dgEstadisticas, rm.GetString("MES"), "", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, True, Nothing, True)
        Catch ex As Exception : i = i - 1 : LOG(ex.ToString) : End Try

        Try : i = i + 1
            PPCol2("ANY", dgEstadisticas, rm.GetString("AÑO"), "", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, True, Nothing, True)
        Catch ex As Exception : i = i - 1 : LOG(ex.ToString) : End Try

        Try : i = i + 1
            PPCol2("NOMCLIENT", dgEstadisticas, rm.GetString("NOMCLIENT"), "", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, True, Nothing, True)
        Catch ex As Exception : i = i - 1 : LOG(ex.ToString) : End Try

        Try : i = i + 1
            PPCol2("ARTICLE", dgEstadisticas, rm.GetString("ARTICULO"), "", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, True, Nothing, True)
        Catch ex As Exception : i = i - 1 : LOG(ex.ToString) : End Try

        Try : i = i + 1
            PPCol2("UNITATS", dgEstadisticas, rm.GetString("UNIDADES"), "", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, True, Nothing, True)
        Catch ex As Exception : i = i - 1 : LOG(ex.ToString) : End Try

        Try : i = i + 1
            PPCol2("TIPUS", dgEstadisticas, rm.GetString("TIPO"), "", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, True, Nothing, True)
        Catch ex As Exception : i = i - 1 : LOG(ex.ToString) : End Try


        Try : i = i + 1
            PPCol2("IMPORTIVA", dgEstadisticas, rm.GetString("IMPORTIVA"), "#,##0.00", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, True, Nothing, True)
        Catch ex As Exception : i = i - 1 : LOG(ex.ToString) : End Try

        Try : i = i + 1
            PPCol2("IMPORTSINIVA", dgEstadisticas, rm.GetString("TOTAL"), "#,##0.00", True, 50, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 50, i, True, Nothing, True)
        Catch ex As Exception : i = i - 1 : LOG(ex.ToString) : End Try

    End Sub

    Private Sub btnGenerarEstadísitcas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerarEstadísitcas.Click
        Try
            GenerarArraysEst()
            If rdoClients.Checked = True Then
                clsEst.GenerarEst()
            Else
                If rdoProveedores.Checked = True Then
                    clsEst.GenerarEstProve()
                Else
                    Exit Sub
                End If
            End If

            IniciarDG()

            dgEstadisticas.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.GroupBy
            dgEstadisticas.FooterStyle.BackColor = Color.GreenYellow

            RecalcularTotales()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

    Private Sub RecalcularTotales()
        Try
            Try
                dgEstadisticas.Splits(0).DisplayColumns("IMPORTIVA").DataColumn.FooterText = Format(sumaTotal("IMPORTIVA", clsEst.dvEstadisticas), "#,##0.00")
            Catch ex As Exception : End Try
            Try
                dgEstadisticas.Splits(0).DisplayColumns("IMPORTSINIVA").DataColumn.FooterText = Format(sumaTotal("IMPORTSINIVA", clsEst.dvEstadisticas), "#,##0.00")
            Catch ex As Exception : End Try

            AutoSizeCC(dgEstadisticas)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

    Private Sub dgEstadisticas_Filter(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.FilterEventArgs) Handles dgEstadisticas.Filter
        Try
            clsEst.dvEstadisticas.RowFilter = e.Condition
            RecalcularTotales()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

    Private Sub btnExportarExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportarExcel.Click
        Dim sDBPath As String
        Dim NavArchivos As System.Windows.Forms.SaveFileDialog
        Try

            NavArchivos = New System.Windows.Forms.SaveFileDialog
            NavArchivos.Filter = "Arxius pdf (*.xls) | *.xls"
            NavArchivos.InitialDirectory = "c:\"
            NavArchivos.ShowDialog()
            sDBPath = NavArchivos.FileName
            dgEstadisticas.ExportToExcel(sDBPath, True)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Try
            'If TabControl1.SelectedTab Is tabEstaditicas Then
            '    gb()
            '    gbAgrupacion.Visible = True
            'Else

            'End If
            'Select Case TabControl1.SelectedIndex
            '    Case tabEstaditicas.i1
            '    Case TAB()
            'End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cambioDeAlgunRadio(ByVal sender As System.Object, ByVal e As System.EventArgs)
        chkAgruparFacturas.Visible = True
        chkAgruparPorArticulo.Visible = True
        chkAgruparPorCliente.Visible = True
        chkAgruparPorTipo.Visible = True
        chkHilos.Visible = True
        chkModelos.Visible = True
        chkMuestras.Visible = True
        chkPerAnys.Visible = True
        chkPorMeses.Visible = True
        chkTejidos.Visible = True
        rdoAlbaranes.Visible = True
        rdoFacturas.Visible = True
        rdoPedidos.Visible = True

        Select Case DirectCast(sender, RadioButton).Name
            Case "rdoPedidos"

            Case "rdoFacturas"
            Case "rdoAlbaranes"
            Case "rdoClients"
                chkHilos.Visible = False
                chkFornituras.Visible = False
            Case "rdoProveedores"
                rdoFacturas.Visible = False
                chkModelos.Visible = False

        End Select
    End Sub

End Class

