Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Imports C1.Win.C1TrueDBGrid


Public Class frmTraspasoLineaPAF
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
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents btnCancelar As C1.Win.C1Input.C1Button
    Friend WithEvents btnAceptar As C1.Win.C1Input.C1Button
    Friend WithEvents dgTraspaso As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents cboTalleres As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmTraspasoLineaPAF))
        Me.btnAceptar = New C1.Win.C1Input.C1Button
        Me.dgTraspaso = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.cboTalleres = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        CType(Me.dgTraspaso, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTalleres, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(5, 278)
        Me.sbtipo.Name = "sbtipo"
        Me.sbtipo.Size = New System.Drawing.Size(88, 15)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(72, 248)
        Me.btnRecargar.Name = "btnRecargar"
        Me.btnRecargar.Size = New System.Drawing.Size(90, 18)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(344, 248)
        Me.btnSiguiente.Name = "btnSiguiente"
        Me.btnSiguiente.Size = New System.Drawing.Size(32, 18)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(40, 248)
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.Size = New System.Drawing.Size(32, 18)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(40, 233)
        Me.btnPrimero.Name = "btnPrimero"
        Me.btnPrimero.Size = New System.Drawing.Size(32, 19)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(344, 233)
        Me.btnUltimo.Name = "btnUltimo"
        Me.btnUltimo.Size = New System.Drawing.Size(32, 19)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(160, 233)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New System.Drawing.Size(98, 19)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(628, 248)
        Me.btnTancar.Name = "btnTancar"
        Me.btnTancar.Size = New System.Drawing.Size(72, 18)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(160, 248)
        Me.btnBorrar.Name = "btnBorrar"
        Me.btnBorrar.Size = New System.Drawing.Size(98, 18)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(264, 233)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(82, 37)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(72, 233)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(90, 19)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(628, 233)
        Me.btnVerLista.Name = "btnVerLista"
        Me.btnVerLista.Size = New System.Drawing.Size(72, 19)
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.Location = New System.Drawing.Point(280, 7)
        Me.cboSeleccionCentro.Name = "cboSeleccionCentro"
        '
        'btnAceptar
        '
        Me.btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAceptar.Location = New System.Drawing.Point(712, 256)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 22)
        Me.btnAceptar.TabIndex = 2
        Me.btnAceptar.Text = "Acceptar"
        '
        'dgTraspaso
        '
        Me.dgTraspaso.AllowAddNew = True
        Me.dgTraspaso.AllowDelete = True
        Me.dgTraspaso.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgTraspaso.CaptionHeight = 18
        Me.dgTraspaso.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.dgTraspaso.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgTraspaso.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.dgTraspaso.Location = New System.Drawing.Point(16, 15)
        Me.dgTraspaso.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.dgTraspaso.Name = "dgTraspaso"
        Me.dgTraspaso.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgTraspaso.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgTraspaso.PreviewInfo.ZoomFactor = 75
        Me.dgTraspaso.RecordSelectorWidth = 17
        Me.dgTraspaso.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgTraspaso.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.dgTraspaso.RowHeight = 15
        Me.dgTraspaso.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.dgTraspaso.Size = New System.Drawing.Size(776, 230)
        Me.dgTraspaso.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgTraspaso.TabIndex = 12
        Me.dgTraspaso.Text = "C1TrueDBGrid1"
        Me.dgTraspaso.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Style9{}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:Hig" & _
        "hlightText;BackColor:Highlight;}Style14{}OddRow{}RecordSelector{AlignImage:Cente" & _
        "r;}Style15{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeCo" & _
        "lor:ControlText;AlignVert:Center;}Style8{}Style10{AlignHorz:Near;}Style11{}Style" & _
        "12{}Style13{}Style1{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBar" & _
        "Height=""16"" Name="""" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeig" & _
        "ht=""17"" MarqueeStyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=" & _
        """17"" VerticalScrollGroup=""1"" HorizontalScrollGroup=""1""><CaptionStyle parent=""Sty" & _
        "le2"" me=""Style10"" /><EditorStyle parent=""Editor"" me=""Style5"" /><EvenRowStyle par" & _
        "ent=""EvenRow"" me=""Style8"" /><FilterBarStyle parent=""FilterBar"" me=""Style13"" /><F" & _
        "ooterStyle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style12""" & _
        " /><HeadingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""Highl" & _
        "ightRow"" me=""Style7"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowSty" & _
        "le parent=""OddRow"" me=""Style9"" /><RecordSelectorStyle parent=""RecordSelector"" me" & _
        "=""Style11"" /><SelectedStyle parent=""Selected"" me=""Style6"" /><Style parent=""Norma" & _
        "l"" me=""Style1"" /><ClientRect>0, 0, 772, 226</ClientRect><BorderSide>0</BorderSid" & _
        "e></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent="""" me=""Nor" & _
        "mal"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer""" & _
        " /><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" " & _
        "/><Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Editor"" /><S" & _
        "tyle parent=""Normal"" me=""HighlightRow"" /><Style parent=""Normal"" me=""EvenRow"" /><" & _
        "Style parent=""Normal"" me=""OddRow"" /><Style parent=""Heading"" me=""RecordSelector"" " & _
        "/><Style parent=""Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""Group"" /><" & _
        "/NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>None</L" & _
        "ayout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 772, 226</Cli" & _
        "entArea><PrintPageHeaderStyle parent="""" me=""Style14"" /><PrintPageFooterStyle par" & _
        "ent="""" me=""Style15"" /></Blob>"
        '
        'cboTalleres
        '
        Me.cboTalleres.AllowColMove = True
        Me.cboTalleres.AllowColSelect = True
        Me.cboTalleres.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboTalleres.AlternatingRows = True
        Me.cboTalleres.CaptionHeight = 17
        Me.cboTalleres.ColumnCaptionHeight = 17
        Me.cboTalleres.ColumnFooterHeight = 17
        Me.cboTalleres.FetchRowStyles = False
        Me.cboTalleres.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.cboTalleres.Location = New System.Drawing.Point(36, 28)
        Me.cboTalleres.Name = "cboTalleres"
        Me.cboTalleres.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboTalleres.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.cboTalleres.RowHeight = 15
        Me.cboTalleres.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboTalleres.ScrollTips = False
        Me.cboTalleres.Size = New System.Drawing.Size(60, 188)
        Me.cboTalleres.TabIndex = 233
        Me.cboTalleres.TabStop = False
        Me.cboTalleres.Text = "C1TrueDBDropdown1"
        Me.cboTalleres.ValueTranslate = True
        Me.cboTalleres.Visible = False
        Me.cboTalleres.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:HighlightTe" & _
        "xt;BackColor:Highlight;}Style9{}OddRow{}RecordSelector{AlignImage:Center;}Headin" & _
        "g{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;Bac" & _
        "kColor:Control;}Style8{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style" & _
        "1{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.DropdownView Name="""" Alternating" & _
        "RowStyle=""True"" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeight=""" & _
        "17"" MarqueeStyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=""17""" & _
        " RecordSelectors=""False"" VerticalScrollGroup=""1"" HorizontalScrollGroup=""1""><Capt" & _
        "ionStyle parent=""Style2"" me=""Style10"" /><EditorStyle parent=""Editor"" me=""Style5""" & _
        " /><EvenRowStyle parent=""EvenRow"" me=""Style8"" /><FilterBarStyle parent=""FilterBa" & _
        "r"" me=""Style13"" /><FooterStyle parent=""Footer"" me=""Style3"" /><GroupStyle parent=" & _
        """Group"" me=""Style12"" /><HeadingStyle parent=""Heading"" me=""Style2"" /><HighLightRo" & _
        "wStyle parent=""HighlightRow"" me=""Style7"" /><InactiveStyle parent=""Inactive"" me=""" & _
        "Style4"" /><OddRowStyle parent=""OddRow"" me=""Style9"" /><RecordSelectorStyle parent" & _
        "=""RecordSelector"" me=""Style11"" /><SelectedStyle parent=""Selected"" me=""Style6"" />" & _
        "<Style parent=""Normal"" me=""Style1"" /><ClientRect>0, 0, 56, 184</ClientRect><Bord" & _
        "erSide>0</BorderSide></C1.Win.C1TrueDBGrid.DropdownView></Splits><NamedStyles><S" & _
        "tyle parent="""" me=""Normal"" /><Style parent=""Normal"" me=""Heading"" /><Style parent" & _
        "=""Heading"" me=""Footer"" /><Style parent=""Heading"" me=""Caption"" /><Style parent=""H" & _
        "eading"" me=""Inactive"" /><Style parent=""Normal"" me=""Selected"" /><Style parent=""No" & _
        "rmal"" me=""Editor"" /><Style parent=""Normal"" me=""HighlightRow"" /><Style parent=""No" & _
        "rmal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><Style parent=""Heading" & _
        """ me=""RecordSelector"" /><Style parent=""Normal"" me=""FilterBar"" /><Style parent=""C" & _
        "aption"" me=""Group"" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horz" & _
        "Splits><Layout>None</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientAr" & _
        "ea>0, 0, 56, 184</ClientArea></Blob>"
        '
        'frmTraspasoLineaPAF
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(208, Byte), CType(222, Byte), CType(225, Byte))
        Me.ClientSize = New System.Drawing.Size(800, 290)
        Me.Controls.Add(Me.cboTalleres)
        Me.Controls.Add(Me.dgTraspaso)
        Me.Controls.Add(Me.btnAceptar)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Name = "frmTraspasoLineaPAF"
        Me.Text = "Traspàs linies"
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
        Me.Controls.SetChildIndex(Me.btnAceptar, 0)
        Me.Controls.SetChildIndex(Me.dgTraspaso, 0)
        Me.Controls.SetChildIndex(Me.cboTalleres, 0)
        CType(Me.dgTraspaso, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTalleres, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "VARIABLES"

    Public PAF As clsPAFVenta
    Public traspasoActual As clsTraspasosVenta
    Public OCV As Char = "V"
    Public localizacion As Point

#End Region

#Region "INICIALIZACION"

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            traspasoActual = New clsTraspasosVenta(ds.Tables("dcfactutrasp"), PAF.centro, Me.BindingContext, PAF, OCV)

            dgTraspaso.SetDataBinding(traspasoActual.dvForm, "")
            InicializarDgTraspaso()
            PonerHandlersErroresParaGrids()
            localizacion.X = localizacion.X
            Me.Location = localizacion

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub InicializarDgTraspaso()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgTraspaso)
            i = 0
            Select Case PAF.DOCUMENT
                Case Pedido
                    Select Case OCV
                        Case OrdenFabComplementos
                            PPCol2("FRA", dgTraspaso, rm.GetString("ORDEN"), "", _
                                True, 35, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 100, i, False)

                        Case Else
                            PPCol2("FRA", dgTraspaso, rm.GetString("ALBARAN"), "", _
                                True, 35, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 100, i, False)
                    End Select
                Case Albaran
                    PPCol2("FRA", dgTraspaso, rm.GetString("AFACTURA"), "", True, 35, _
                            False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 100, i, False)
            End Select
            If OCV = OrdenFabComplementos Then
                AñadirBindingCombo(cboTalleres, traspasoActual.dtTalleres, CNTallers, CCTallers)
                i = i + 1
                PPCol2("TALLER", dgTraspaso, rm.GetString("CODIGOTALLER"), "", True, 50, False, PresentationEnum.Normal, False, 70, i, False)
                i = i + 1
                PPCol2("NOMTALLER", dgTraspaso, rm.GetString("NOMTALLER"), "", True, 200, False, PresentationEnum.ComboBox, False, 200, i, False, cboTalleres)
            End If
            i = i + 1
            If PAF.TIPUS <> Prenda Then
                PPCol2("QUANTITAT", dgTraspaso, rm.GetString("CANTIDAD"), "", True, 35, False, PresentationEnum.Normal, False, 100, i, False)
            Else
                PPCol2("TALLA01", dgTraspaso, PAF.lineasVenta.TTALLA01, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
                PPCol2("TALLA02", dgTraspaso, PAF.lineasVenta.TTALLA02, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
                PPCol2("TALLA03", dgTraspaso, PAF.lineasVenta.TTALLA03, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
                PPCol2("TALLA04", dgTraspaso, PAF.lineasVenta.TTALLA04, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
                PPCol2("TALLA05", dgTraspaso, PAF.lineasVenta.TTALLA05, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
                PPCol2("TALLA06", dgTraspaso, PAF.lineasVenta.TTALLA06, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
                PPCol2("TALLA07", dgTraspaso, PAF.lineasVenta.TTALLA07, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
                PPCol2("TALLA08", dgTraspaso, PAF.lineasVenta.TTALLA08, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
                PPCol2("TALLA09", dgTraspaso, PAF.lineasVenta.TTALLA09, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
                PPCol2("TALLA10", dgTraspaso, PAF.lineasVenta.TTALLA10, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            End If

            i = i + 1
            PPCol2("DATA", dgTraspaso, rm.GetString("FECHA"), "Short Date", True, 35, False, _
                    C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 100, i, False)

            'dgTraspaso.Splits(0).DisplayColumns("DATA").ButtonText = True

            i = i + 1
            PPCol2("TRASPASADA", dgTraspaso, rm.GetString("TRASPASADA"), "", True, 35, _
                    False, C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox, False, 100, i, False)

            'En principio esto no sirve para nada pq no se trasapasa de albaranes a facturas de forma
            'parcial

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ORGANIZAR"

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            'traspasoActual.ActualizarOrigen(False, True)
            Me.Close()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    '*********************************************************************************
    'Se encarga de comprobar que el documento de destiono exista sea del mismo tipo
    'y que no este traspasado a otro doc ya, devuelve 1 en caso de que no exista el documento
    'hay que crearlo, 2 si existe y no esta traspasado y 0 si existe y no esta traspasado
    '**********************************************************************************
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            Close()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboTalleres_SelChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles cboTalleres.SelChange
        Try
            If PuedoModificar() Then
                traspasoActual.TALLER = cboTalleres.Columns("CODI").Value
                AñadirBindingCombo(cboTalleres, traspasoActual.dtTalleres, "NOM", "CODI")
            End If

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub dgTraspaso_AfterColUpdate(ByVal sender As System.Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgTraspaso.AfterColUpdate
        Try
            If Not cargando Then
                cargando = True
                Select Case e.Column.DataColumn.DataField
                    Case "TALLER"
                        traspasoActual.TALLER = e.Column.DataColumn.Value
                        dgTraspaso.Update()
                        dgTraspaso.Refresh()
                    Case "NOMTALLER"
                        traspasoActual.TALLER = e.Column.DataColumn.Value
                        dgTraspaso.Update()
                        dgTraspaso.Refresh()
                    Case "TRASPASADA"
                        dgTraspaso.Update()
                        dgTraspaso.Refresh()
                        traspasoActual.HacerCalculos(traspasoActual.DATA)
                End Select

                cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub dgTraspaso_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgTraspaso.BeforeDelete
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

#End Region

#Region "OVERRIDES"

    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
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

#End Region

    Private Sub frmTraspasoLineaPAF_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        traspasoActual.guardarDV()
        traspasoActual.ActualizarOrigen(False, True)
    End Sub
End Class

