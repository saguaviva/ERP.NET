Public Class frmErrores
    Inherits Form

    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents C1TrueDBGrid1 As C1.Win.C1TrueDBGrid.C1TrueDBGrid

    '#Region " Código generado por el Diseñador de Windows Forms "

    '    Public Sub New()
    '        MyBase.New()

    '        'El Diseñador de Windows Forms requiere esta llamada.
    '        InitializeComponent() : Dim tom As SMcMaster.TabOrderManager = New SMcMaster.TabOrderManager(Me) : tom.SetTabOrder(SMcMaster.TabOrderManager.TabScheme.AcrossFirst)

    '        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    '    End Sub

    '    'Form reemplaza a Dispose para limpiar la lista de componentes.
    '    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    '        If disposing Then
    '            If Not (components Is Nothing) Then
    '                components.Dispose()
    '            End If
    '        End If
    '       MyBase.Dispose(disposing)
    '        frmChildForm = Nothing
    '    End Sub

    '    'Requerido por el Diseñador de Windows Forms
    '    Private components As System.ComponentModel.IContainer

    '    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    '    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    '    'No lo modifique con el editor de código.
    '    Friend WithEvents cboError As C1.Win.C1List.C1Combo
    '    Friend WithEvents cboFormulari As C1.Win.C1List.C1Combo
    '    Friend WithEvents Label1 As System.Windows.Forms.Label
    '    Friend WithEvents Label2 As System.Windows.Forms.Label
    '    Friend WithEvents C1DateEdit1 As C1.Win.C1Input.C1DateEdit
    '    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    '        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmErrores))
    '        Me.cboError = New C1.Win.C1List.C1Combo
    '        Me.cboFormulari = New C1.Win.C1List.C1Combo
    '        Me.Label1 = New System.Windows.Forms.Label
    '        Me.Label2 = New System.Windows.Forms.Label
    '        Me.C1DateEdit1 = New C1.Win.C1Input.C1DateEdit
    '        CType(Me.cboError, System.ComponentModel.ISupportInitialize).BeginInit()
    '        CType(Me.cboFormulari, System.ComponentModel.ISupportInitialize).BeginInit()
    '        CType(Me.C1DateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
    '        Me.SuspendLayout()
    '        '
    '        'btnPrimero
    '        '
    '        Me.btnPrimero.Location = CType(resources.GetObject("btnPrimero.Location"), System.Drawing.Point)
    '        Me.btnPrimero.Name = "btnPrimero"
    '        Me.btnPrimero.Size = CType(resources.GetObject("btnPrimero.Size"), System.Drawing.Size)
    '        '
    '        'btnUltimo
    '        '
    '        Me.btnUltimo.Location = CType(resources.GetObject("btnUltimo.Location"), System.Drawing.Point)
    '        Me.btnUltimo.Name = "btnUltimo"
    '        Me.btnUltimo.Size = CType(resources.GetObject("btnUltimo.Size"), System.Drawing.Size)
    '        '
    '        'btnTancar
    '        '
    '        Me.btnTancar.Location = CType(resources.GetObject("btnTancar.Location"), System.Drawing.Point)
    '        Me.btnTancar.Name = "btnTancar"
    '        Me.btnTancar.Size = CType(resources.GetObject("btnTancar.Size"), System.Drawing.Size)
    '        '
    '        'btnAnterior
    '        '
    '        Me.btnAnterior.Location = CType(resources.GetObject("btnAnterior.Location"), System.Drawing.Point)
    '        Me.btnAnterior.Name = "btnAnterior"
    '        Me.btnAnterior.Size = CType(resources.GetObject("btnAnterior.Size"), System.Drawing.Size)
    '        '
    '        'btnSiguiente
    '        '
    '        Me.btnSiguiente.Location = CType(resources.GetObject("btnSiguiente.Location"), System.Drawing.Point)
    '        Me.btnSiguiente.Name = "btnSiguiente"
    '        Me.btnSiguiente.Size = CType(resources.GetObject("btnSiguiente.Size"), System.Drawing.Size)
    '        '
    '        'cboSeleccionCentro
    '        '
    '        Me.cboSeleccionCentro.Location = CType(resources.GetObject("cboSeleccionCentro.Location"), System.Drawing.Point)
    '        Me.cboSeleccionCentro.Name = "cboSeleccionCentro"
    '        '
    '        'btnVerLista
    '        '
    '        Me.btnVerLista.Location = CType(resources.GetObject("btnVerLista.Location"), System.Drawing.Point)
    '        Me.btnVerLista.Name = "btnVerLista"
    '        Me.btnVerLista.Size = CType(resources.GetObject("btnVerLista.Size"), System.Drawing.Size)
    '        '
    '        'btnRecargar
    '        '
    '        Me.btnRecargar.Location = CType(resources.GetObject("btnRecargar.Location"), System.Drawing.Point)
    '        Me.btnRecargar.Name = "btnRecargar"
    '        Me.btnRecargar.Size = CType(resources.GetObject("btnRecargar.Size"), System.Drawing.Size)
    '        '
    '        'btnActualizar
    '        '
    '        Me.btnActualizar.Location = CType(resources.GetObject("btnActualizar.Location"), System.Drawing.Point)
    '        Me.btnActualizar.Name = "btnActualizar"
    '        Me.btnActualizar.Size = CType(resources.GetObject("btnActualizar.Size"), System.Drawing.Size)
    '        '
    '        'sbtipo
    '        '
    '        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    '        Me.sbtipo.Location = CType(resources.GetObject("sbtipo.Location"), System.Drawing.Point)
    '        Me.sbtipo.Name = "sbtipo"
    '        Me.sbtipo.Size = CType(resources.GetObject("sbtipo.Size"), System.Drawing.Size)
    '        Me.sbtipo.Text = resources.GetString("sbtipo.Text")
    '        '
    '        'arrayCombos
    '        '
    '        Me.arrayCombos= NEw System.Windows.Forms.Control() {Me.cboFormulari})
    '        '
    '        'arrayEtiquetas
    '        '
    '        Me.arrayEtiquetas=New System.Windows.Forms.Control() {Me.Label1, Me.Label2})
    '        '
    '        'btnModificar
    '        '
    '        Me.btnModificar.Location = CType(resources.GetObject("btnModificar.Location"), System.Drawing.Point)
    '        Me.btnModificar.Name = "btnModificar"
    '        Me.btnModificar.Size = CType(resources.GetObject("btnModificar.Size"), System.Drawing.Size)
    '        '
    '        'btnBorrar
    '        '
    '        Me.btnBorrar.Location = CType(resources.GetObject("btnBorrar.Location"), System.Drawing.Point)
    '        Me.btnBorrar.Name = "btnBorrar"
    '        Me.btnBorrar.Size = CType(resources.GetObject("btnBorrar.Size"), System.Drawing.Size)
    '        '
    '        'btnNuevo
    '        '
    '        Me.btnNuevo.Location = CType(resources.GetObject("btnNuevo.Location"), System.Drawing.Point)
    '        Me.btnNuevo.Name = "btnNuevo"
    '        Me.btnNuevo.Size = CType(resources.GetObject("btnNuevo.Size"), System.Drawing.Size)
    '        '
    '        'cboError
    '        '
    '        Me.cboError.AccessibleDescription = resources.GetString("cboError.AccessibleDescription")
    '        Me.cboError.AccessibleName = resources.GetString("cboError.AccessibleName")
    '        Me.cboError.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
    '        Me.cboError.Anchor = CType(resources.GetObject("cboError.Anchor"), System.Windows.Forms.AnchorStyles)
    '        Me.cboError.AutoSelect = True
    '        Me.cboError.BackgroundImage = CType(resources.GetObject("cboError.BackgroundImage"), System.Drawing.Image)
    '        Me.cboError.Caption = ""
    '        Me.cboError.CaptionHeight = 17
    '        Me.cboError.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    '        Me.cboError.ColumnCaptionHeight = 17
    '        Me.cboError.ColumnFooterHeight = 17
    '        Me.cboError.ContentHeight = 16
    '        Me.cboError.DeadAreaBackColor = System.Drawing.Color.Empty
    '        Me.cboError.Dock = CType(resources.GetObject("cboError.Dock"), System.Windows.Forms.DockStyle)
    '        Me.cboError.EditorBackColor = System.Drawing.SystemColors.Window
    '        Me.cboError.EditorFont = New System.Drawing.Font("Tahoma", 8.25!)
    '        Me.cboError.EditorForeColor = System.Drawing.SystemColors.WindowText
    '        Me.cboError.EditorHeight = 16
    '        Me.cboError.Enabled = CType(resources.GetObject("cboError.Enabled"), Boolean)
    '        Me.cboError.Font = CType(resources.GetObject("cboError.Font"), System.Drawing.Font)
    '        Me.cboError.GapHeight = 2
    '        Me.cboError.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
    '        Me.cboError.ImeMode = CType(resources.GetObject("cboError.ImeMode"), System.Windows.Forms.ImeMode)
    '        Me.cboError.ItemHeight = 15
    '        Me.cboError.Location = CType(resources.GetObject("cboError.Location"), System.Drawing.Point)
    '        Me.cboError.MatchEntryTimeout = CType(2000, Long)
    '        Me.cboError.MaxDropDownItems = CType(5, Short)
    '        Me.cboError.MaxLength = 32767
    '        Me.cboError.MouseCursor = System.Windows.Forms.Cursors.Default
    '        Me.cboError.Name = "cboError"
    '        'Me.cbo.PartialRightColumn = False
    '        Me.cboError.RightToLeft = CType(resources.GetObject("cboError.RightToLeft"), System.Windows.Forms.RightToLeft)
    '        Me.cboError.RowDivider.Color = System.Drawing.Color.DarkGray
    '        Me.cboError.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
    '        Me.cboError.RowSubDividerColor = System.Drawing.Color.DarkGray
    '        Me.cboError.Size = CType(resources.GetObject("cboError.Size"), System.Drawing.Size)
    '        Me.cboError.TabIndex = CType(resources.GetObject("cboError.TabIndex"), Integer)
    '        Me.cboError.Text = resources.GetString("cboError.Text")
    '        Me.cboError.Visible = CType(resources.GetObject("cboError.Visible"), Boolean)
    '        Me.cboError.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
    '        "ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" & _
    '        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
    '        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
    '        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Wind" & _
    '        "ow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}Re" & _
    '        "cordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Raise" & _
    '        "d,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}" & _
    '        "Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowC" & _
    '        "olSelect=""False"" Name="""" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFoote" & _
    '        "rHeight=""17"" VerticalScrollGroup=""1"" HorizontalScrollGroup=""1""><ClientRect>0, 0," & _
    '        " 116, 156</ClientRect><VScrollBar><Width>17</Width></VScrollBar><HScrollBar><Hei" & _
    '        "ght>17</Height></HScrollBar><CaptionStyle parent=""Style2"" me=""Style9"" /><EvenRow" & _
    '        "Style parent=""EvenRow"" me=""Style7"" /><FooterStyle parent=""Footer"" me=""Style3"" />" & _
    '        "<GroupStyle parent=""Group"" me=""Style11"" /><HeadingStyle parent=""Heading"" me=""Sty" & _
    '        "le2"" /><HighLightRowStyle parent=""HighlightRow"" me=""Style6"" /><InactiveStyle par" & _
    '        "ent=""Inactive"" me=""Style4"" /><OddRowStyle parent=""OddRow"" me=""Style8"" /><RecordS" & _
    '        "electorStyle parent=""RecordSelector"" me=""Style10"" /><SelectedStyle parent=""Selec" & _
    '        "ted"" me=""Style5"" /><Style parent=""Normal"" me=""Style1"" /></C1.Win.C1List.ListBoxV" & _
    '        "iew></Splits><NamedStyles><Style parent="""" me=""Normal"" /><Style parent=""Normal"" " & _
    '        "me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /><Style parent=""Heading"" me=" & _
    '        """Caption"" /><Style parent=""Heading"" me=""Inactive"" /><Style parent=""Normal"" me=""S" & _
    '        "elected"" /><Style parent=""Normal"" me=""HighlightRow"" /><Style parent=""Normal"" me=" & _
    '        """EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><Style parent=""Heading"" me=""Rec" & _
    '        "ordSelector"" /><Style parent=""Caption"" me=""Group"" /></NamedStyles><vertSplits>1<" & _
    '        "/vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWid" & _
    '        "th>17</DefaultRecSelWidth></Blob>"
    '        '
    '        'cboFormulari
    '        '
    '        Me.cboFormulari.AccessibleDescription = resources.GetString("cboFormulari.AccessibleDescription")
    '        Me.cboFormulari.AccessibleName = resources.GetString("cboFormulari.AccessibleName")
    '        Me.cboFormulari.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
    '        Me.cboFormulari.Anchor = CType(resources.GetObject("cboFormulari.Anchor"), System.Windows.Forms.AnchorStyles)
    '        Me.cboFormulari.AutoSelect = True
    '        Me.cboFormulari.BackgroundImage = CType(resources.GetObject("cboFormulari.BackgroundImage"), System.Drawing.Image)
    '        Me.cboFormulari.Caption = ""
    '        Me.cboFormulari.CaptionHeight = 17
    '        Me.cboFormulari.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    '        Me.cboFormulari.ColumnCaptionHeight = 17
    '        Me.cboFormulari.ColumnFooterHeight = 17
    '        Me.cboFormulari.ContentHeight = 16
    '        Me.cboFormulari.DeadAreaBackColor = System.Drawing.Color.Empty
    '        Me.cboFormulari.Dock = CType(resources.GetObject("cboFormulari.Dock"), System.Windows.Forms.DockStyle)
    '        Me.cboFormulari.EditorBackColor = System.Drawing.SystemColors.Window
    '        Me.cboFormulari.EditorFont = New System.Drawing.Font("Tahoma", 8.25!)
    '        Me.cboFormulari.EditorForeColor = System.Drawing.SystemColors.WindowText
    '        Me.cboFormulari.EditorHeight = 16
    '        Me.cboFormulari.Enabled = CType(resources.GetObject("cboFormulari.Enabled"), Boolean)
    '        Me.cboFormulari.Font = CType(resources.GetObject("cboFormulari.Font"), System.Drawing.Font)
    '        Me.cboFormulari.GapHeight = 2
    '        Me.cboFormulari.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
    '        Me.cboFormulari.ImeMode = CType(resources.GetObject("cboFormulari.ImeMode"), System.Windows.Forms.ImeMode)
    '        Me.cboFormulari.ItemHeight = 15
    '        Me.cboFormulari.Location = CType(resources.GetObject("cboFormulari.Location"), System.Drawing.Point)
    '        Me.cboFormulari.MatchEntryTimeout = CType(2000, Long)
    '        Me.cboFormulari.MaxDropDownItems = CType(5, Short)
    '        Me.cboFormulari.MaxLength = 32767
    '        Me.cboFormulari.MouseCursor = System.Windows.Forms.Cursors.Default
    '        Me.cboFormulari.Name = "cboFormulari"
    '        'Me.cbo.PartialRightColumn = False
    '        Me.cboFormulari.RightToLeft = CType(resources.GetObject("cboFormulari.RightToLeft"), System.Windows.Forms.RightToLeft)
    '        Me.cboFormulari.RowDivider.Color = System.Drawing.Color.DarkGray
    '        Me.cboFormulari.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
    '        Me.cboFormulari.RowSubDividerColor = System.Drawing.Color.DarkGray
    '        Me.cboFormulari.Size = CType(resources.GetObject("cboFormulari.Size"), System.Drawing.Size)
    '        Me.cboFormulari.TabIndex = CType(resources.GetObject("cboFormulari.TabIndex"), Integer)
    '        Me.cboFormulari.Text = resources.GetString("cboFormulari.Text")
    '        Me.cboFormulari.Visible = CType(resources.GetObject("cboFormulari.Visible"), Boolean)
    '        Me.cboFormulari.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
    '        "ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" & _
    '        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
    '        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
    '        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Wind" & _
    '        "ow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}Re" & _
    '        "cordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Raise" & _
    '        "d,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}" & _
    '        "Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowC" & _
    '        "olSelect=""False"" Name="""" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFoote" & _
    '        "rHeight=""17"" VerticalScrollGroup=""1"" HorizontalScrollGroup=""1""><ClientRect>0, 0," & _
    '        " 116, 156</ClientRect><VScrollBar><Width>17</Width></VScrollBar><HScrollBar><Hei" & _
    '        "ght>17</Height></HScrollBar><CaptionStyle parent=""Style2"" me=""Style9"" /><EvenRow" & _
    '        "Style parent=""EvenRow"" me=""Style7"" /><FooterStyle parent=""Footer"" me=""Style3"" />" & _
    '        "<GroupStyle parent=""Group"" me=""Style11"" /><HeadingStyle parent=""Heading"" me=""Sty" & _
    '        "le2"" /><HighLightRowStyle parent=""HighlightRow"" me=""Style6"" /><InactiveStyle par" & _
    '        "ent=""Inactive"" me=""Style4"" /><OddRowStyle parent=""OddRow"" me=""Style8"" /><RecordS" & _
    '        "electorStyle parent=""RecordSelector"" me=""Style10"" /><SelectedStyle parent=""Selec" & _
    '        "ted"" me=""Style5"" /><Style parent=""Normal"" me=""Style1"" /></C1.Win.C1List.ListBoxV" & _
    '        "iew></Splits><NamedStyles><Style parent="""" me=""Normal"" /><Style parent=""Normal"" " & _
    '        "me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /><Style parent=""Heading"" me=" & _
    '        """Caption"" /><Style parent=""Heading"" me=""Inactive"" /><Style parent=""Normal"" me=""S" & _
    '        "elected"" /><Style parent=""Normal"" me=""HighlightRow"" /><Style parent=""Normal"" me=" & _
    '        """EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><Style parent=""Heading"" me=""Rec" & _
    '        "ordSelector"" /><Style parent=""Caption"" me=""Group"" /></NamedStyles><vertSplits>1<" & _
    '        "/vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWid" & _
    '        "th>17</DefaultRecSelWidth></Blob>"
    '        '
    '        'Label1
    '        '
    '        Me.Label1.AccessibleDescription = resources.GetString("Label1.AccessibleDescription")
    '        Me.Label1.AccessibleName = resources.GetString("Label1.AccessibleName")
    '        Me.Label1.Anchor = CType(resources.GetObject("Label1.Anchor"), System.Windows.Forms.AnchorStyles)
    '        Me.Label1.AutoSize = CType(resources.GetObject("Label1.AutoSize"), Boolean)
    '        Me.Label1.Dock = CType(resources.GetObject("Label1.Dock"), System.Windows.Forms.DockStyle)
    '        Me.Label1.Enabled = CType(resources.GetObject("Label1.Enabled"), Boolean)
    '        Me.Label1.Font = CType(resources.GetObject("Label1.Font"), System.Drawing.Font)
    '        Me.Label1.Image = CType(resources.GetObject("Label1.Image"), System.Drawing.Image)
    '        Me.Label1.ImageAlign = CType(resources.GetObject("Label1.ImageAlign"), System.Drawing.ContentAlignment)
    '        Me.Label1.ImageIndex = CType(resources.GetObject("Label1.ImageIndex"), Integer)
    '        Me.Label1.ImeMode = CType(resources.GetObject("Label1.ImeMode"), System.Windows.Forms.ImeMode)
    '        Me.Label1.Location = CType(resources.GetObject("Label1.Location"), System.Drawing.Point)
    '        Me.Label1.Name = "Label1"
    '        Me.Label1.RightToLeft = CType(resources.GetObject("Label1.RightToLeft"), System.Windows.Forms.RightToLeft)
    '        Me.Label1.Size = CType(resources.GetObject("Label1.Size"), System.Drawing.Size)
    '        Me.Label1.TabIndex = CType(resources.GetObject("Label1.TabIndex"), Integer)
    '        Me.Label1.Text = resources.GetString("Label1.Text")
    '        Me.Label1.TextAlign = CType(resources.GetObject("Label1.TextAlign"), System.Drawing.ContentAlignment)
    '        Me.Label1.Visible = CType(resources.GetObject("Label1.Visible"), Boolean)
    '        '
    '        'Label2
    '        '
    '        Me.Label2.AccessibleDescription = resources.GetString("Label2.AccessibleDescription")
    '        Me.Label2.AccessibleName = resources.GetString("Label2.AccessibleName")
    '        Me.Label2.Anchor = CType(resources.GetObject("Label2.Anchor"), System.Windows.Forms.AnchorStyles)
    '        Me.Label2.AutoSize = CType(resources.GetObject("Label2.AutoSize"), Boolean)
    '        Me.Label2.Dock = CType(resources.GetObject("Label2.Dock"), System.Windows.Forms.DockStyle)
    '        Me.Label2.Enabled = CType(resources.GetObject("Label2.Enabled"), Boolean)
    '        Me.Label2.Font = CType(resources.GetObject("Label2.Font"), System.Drawing.Font)
    '        Me.Label2.Image = CType(resources.GetObject("Label2.Image"), System.Drawing.Image)
    '        Me.Label2.ImageAlign = CType(resources.GetObject("Label2.ImageAlign"), System.Drawing.ContentAlignment)
    '        Me.Label2.ImageIndex = CType(resources.GetObject("Label2.ImageIndex"), Integer)
    '        Me.Label2.ImeMode = CType(resources.GetObject("Label2.ImeMode"), System.Windows.Forms.ImeMode)
    '        Me.Label2.Location = CType(resources.GetObject("Label2.Location"), System.Drawing.Point)
    '        Me.Label2.Name = "Label2"
    '        Me.Label2.RightToLeft = CType(resources.GetObject("Label2.RightToLeft"), System.Windows.Forms.RightToLeft)
    '        Me.Label2.Size = CType(resources.GetObject("Label2.Size"), System.Drawing.Size)
    '        Me.Label2.TabIndex = CType(resources.GetObject("Label2.TabIndex"), Integer)
    '        Me.Label2.Text = resources.GetString("Label2.Text")
    '        Me.Label2.TextAlign = CType(resources.GetObject("Label2.TextAlign"), System.Drawing.ContentAlignment)
    '        Me.Label2.Visible = CType(resources.GetObject("Label2.Visible"), Boolean)
    '        '
    '        'C1DateEdit1
    '        '
    '        Me.C1DateEdit1.AcceptsEscape = CType(resources.GetObject("C1DateEdit1.AcceptsEscape"), Boolean)
    '        Me.C1DateEdit1.AccessibleDescription = resources.GetString("C1DateEdit1.AccessibleDescription")
    '        Me.C1DateEdit1.AccessibleName = resources.GetString("C1DateEdit1.AccessibleName")
    '        Me.C1DateEdit1.Anchor = CType(resources.GetObject("C1DateEdit1.Anchor"), System.Windows.Forms.AnchorStyles)
    '        Me.C1DateEdit1.AutoSize = CType(resources.GetObject("C1DateEdit1.AutoSize"), Boolean)
    '        Me.C1DateEdit1.BackgroundImage = CType(resources.GetObject("C1DateEdit1.BackgroundImage"), System.Drawing.Image)
    '        Me.C1DateEdit1.BorderStyle = CType(resources.GetObject("C1DateEdit1.BorderStyle"), System.Windows.Forms.BorderStyle)
    '        '
    '        'C1DateEdit1.Calendar
    '        '
    '        Me.C1DateEdit1.Calendar.AccessibleDescription = resources.GetString("C1DateEdit1.Calendar.AccessibleDescription")
    '        Me.C1DateEdit1.Calendar.AccessibleName = resources.GetString("C1DateEdit1.Calendar.AccessibleName")
    '        Me.C1DateEdit1.Calendar.AnnuallyBoldedDates = CType(resources.GetObject("C1DateEdit1.Calendar.AnnuallyBoldedDates"), Date())
    '        Me.C1DateEdit1.Calendar.BackgroundImage = CType(resources.GetObject("C1DateEdit1.Calendar.BackgroundImage"), System.Drawing.Image)
    '        Me.C1DateEdit1.Calendar.BoldedDates = CType(resources.GetObject("C1DateEdit1.Calendar.BoldedDates"), Date())
    '        Me.C1DateEdit1.Calendar.CalendarDimensions = CType(resources.GetObject("C1DateEdit1.Calendar.CalendarDimensions"), System.Drawing.Size)
    '        Me.C1DateEdit1.Calendar.Enabled = CType(resources.GetObject("C1DateEdit1.Calendar.Enabled"), Boolean)
    '        Me.C1DateEdit1.Calendar.FirstDayOfWeek = CType(resources.GetObject("C1DateEdit1.Calendar.FirstDayOfWeek"), System.Windows.Forms.Day)
    '        Me.C1DateEdit1.Calendar.Font = CType(resources.GetObject("C1DateEdit1.Calendar.Font"), System.Drawing.Font)
    '        Me.C1DateEdit1.Calendar.ImeMode = CType(resources.GetObject("C1DateEdit1.Calendar.ImeMode"), System.Windows.Forms.ImeMode)
    '        Me.C1DateEdit1.Calendar.MonthlyBoldedDates = CType(resources.GetObject("C1DateEdit1.Calendar.MonthlyBoldedDates"), Date())
    '        Me.C1DateEdit1.Calendar.RightToLeft = CType(resources.GetObject("C1DateEdit1.Calendar.RightToLeft"), System.Windows.Forms.RightToLeft)
    '        Me.C1DateEdit1.Calendar.ShowClearButton = CType(resources.GetObject("C1DateEdit1.Calendar.ShowClearButton"), Boolean)
    '        Me.C1DateEdit1.Calendar.ShowTodayButton = CType(resources.GetObject("C1DateEdit1.Calendar.ShowTodayButton"), Boolean)
    '        Me.C1DateEdit1.Calendar.ShowWeekNumbers = CType(resources.GetObject("C1DateEdit1.Calendar.ShowWeekNumbers"), Boolean)
    '        Me.C1DateEdit1.Calendar.UIStrings.Content = CType(resources.GetObject("C1DateEdit1.Calendar.UIStrings.Content"), String())
    '        Me.C1DateEdit1.CaseSensitive = CType(resources.GetObject("C1DateEdit1.CaseSensitive"), Boolean)
    '        Me.C1DateEdit1.CopyWithLiterals = CType(resources.GetObject("C1DateEdit1.CopyWithLiterals"), Boolean)
    '        Me.C1DateEdit1.Culture = CType(resources.GetObject("C1DateEdit1.Culture"), Integer)
    '        Me.C1DateEdit1.CurrentTimeZone = CType(resources.GetObject("C1DateEdit1.CurrentTimeZone"), Boolean)
    '        Me.C1DateEdit1.CustomFormat = resources.GetString("C1DateEdit1.CustomFormat")
    '        Me.C1DateEdit1.DaylightTimeAdjustment = CType(resources.GetObject("C1DateEdit1.DaylightTimeAdjustment"), C1.Win.C1Input.DaylightTimeAdjustmentEnum)
    '        Me.C1DateEdit1.DisplayFormat.CustomFormat = resources.GetString("C1DateEdit1.DisplayFormat.CustomFormat")
    '        Me.C1DateEdit1.DisplayFormat.FormatType = CType(resources.GetObject("C1DateEdit1.DisplayFormat.FormatType"), C1.Win.C1Input.FormatTypeEnum)
    '        Me.C1DateEdit1.DisplayFormat.Inherit = CType(resources.GetObject("C1DateEdit1.DisplayFormat.Inherit"), C1.Win.C1Input.FormatInfoInheritFlags)
    '        Me.C1DateEdit1.DisplayFormat.NullText = resources.GetString("C1DateEdit1.DisplayFormat.NullText")
    '        Me.C1DateEdit1.DisplayFormat.TrimEnd = CType(resources.GetObject("C1DateEdit1.DisplayFormat.TrimEnd"), Boolean)
    '        Me.C1DateEdit1.DisplayFormat.TrimStart = CType(resources.GetObject("C1DateEdit1.DisplayFormat.TrimStart"), Boolean)
    '        Me.C1DateEdit1.Dock = CType(resources.GetObject("C1DateEdit1.Dock"), System.Windows.Forms.DockStyle)
    '        Me.C1DateEdit1.EditFormat.CustomFormat = resources.GetString("C1DateEdit1.EditFormat.CustomFormat")
    '        Me.C1DateEdit1.EditFormat.FormatType = CType(resources.GetObject("C1DateEdit1.EditFormat.FormatType"), C1.Win.C1Input.FormatTypeEnum)
    '        Me.C1DateEdit1.EditFormat.Inherit = CType(resources.GetObject("C1DateEdit1.EditFormat.Inherit"), C1.Win.C1Input.FormatInfoInheritFlags)
    '        Me.C1DateEdit1.EditFormat.NullText = resources.GetString("C1DateEdit1.EditFormat.NullText")
    '        Me.C1DateEdit1.EditFormat.TrimEnd = CType(resources.GetObject("C1DateEdit1.EditFormat.TrimEnd"), Boolean)
    '        Me.C1DateEdit1.EditFormat.TrimStart = CType(resources.GetObject("C1DateEdit1.EditFormat.TrimStart"), Boolean)
    '        Me.C1DateEdit1.EditMask = resources.GetString("C1DateEdit1.EditMask")
    '        Me.C1DateEdit1.EmptyAsNull = CType(resources.GetObject("C1DateEdit1.EmptyAsNull"), Boolean)
    '        Me.C1DateEdit1.Enabled = CType(resources.GetObject("C1DateEdit1.Enabled"), Boolean)
    '        Me.C1DateEdit1.ErrorInfo.BeepOnError = CType(resources.GetObject("C1DateEdit1.ErrorInfo.BeepOnError"), Boolean)
    '        Me.C1DateEdit1.ErrorInfo.ErrorMessage = resources.GetString("C1DateEdit1.ErrorInfo.ErrorMessage")
    '        Me.C1DateEdit1.ErrorInfo.ErrorMessageCaption = resources.GetString("C1DateEdit1.ErrorInfo.ErrorMessageCaption")
    '        Me.C1DateEdit1.ErrorInfo.ShowErrorMessage = CType(resources.GetObject("C1DateEdit1.ErrorInfo.ShowErrorMessage"), Boolean)
    '        Me.C1DateEdit1.ErrorInfo.ValueOnError = CType(resources.GetObject("C1DateEdit1.ErrorInfo.ValueOnError"), Object)
    '        Me.C1DateEdit1.Font = CType(resources.GetObject("C1DateEdit1.Font"), System.Drawing.Font)
    '        Me.C1DateEdit1.FormatType = CType(resources.GetObject("C1DateEdit1.FormatType"), C1.Win.C1Input.FormatTypeEnum)
    '        Me.C1DateEdit1.GapHeight = CType(resources.GetObject("C1DateEdit1.GapHeight"), Integer)
    '        Me.C1DateEdit1.GMTOffset = CType(resources.GetObject("C1DateEdit1.GMTOffset"), System.TimeSpan)
    '        Me.C1DateEdit1.ImeMode = CType(resources.GetObject("C1DateEdit1.ImeMode"), System.Windows.Forms.ImeMode)
    '        Me.C1DateEdit1.InitialSelection = CType(resources.GetObject("C1DateEdit1.InitialSelection"), C1.Win.C1Input.InitialSelectionEnum)
    '        Me.C1DateEdit1.Location = CType(resources.GetObject("C1DateEdit1.Location"), System.Drawing.Point)
    '        Me.C1DateEdit1.MaskInfo.AutoTabWhenFilled = CType(resources.GetObject("C1DateEdit1.MaskInfo.AutoTabWhenFilled"), Boolean)
    '        Me.C1DateEdit1.MaskInfo.CaseSensitive = CType(resources.GetObject("C1DateEdit1.MaskInfo.CaseSensitive"), Boolean)
    '        Me.C1DateEdit1.MaskInfo.CopyWithLiterals = CType(resources.GetObject("C1DateEdit1.MaskInfo.CopyWithLiterals"), Boolean)
    '        Me.C1DateEdit1.MaskInfo.EditMask = resources.GetString("C1DateEdit1.MaskInfo.EditMask")
    '        Me.C1DateEdit1.MaskInfo.EmptyAsNull = CType(resources.GetObject("C1DateEdit1.MaskInfo.EmptyAsNull"), Boolean)
    '        Me.C1DateEdit1.MaskInfo.ErrorMessage = resources.GetString("C1DateEdit1.MaskInfo.ErrorMessage")
    '        Me.C1DateEdit1.MaskInfo.Inherit = CType(resources.GetObject("C1DateEdit1.MaskInfo.Inherit"), C1.Win.C1Input.MaskInfoInheritFlags)
    '        Me.C1DateEdit1.MaskInfo.PromptChar = CType(resources.GetObject("C1DateEdit1.MaskInfo.PromptChar"), Char)
    '        Me.C1DateEdit1.MaskInfo.ShowLiterals = CType(resources.GetObject("C1DateEdit1.MaskInfo.ShowLiterals"), C1.Win.C1Input.ShowLiteralsEnum)
    '        Me.C1DateEdit1.MaskInfo.StoredEmptyChar = CType(resources.GetObject("C1DateEdit1.MaskInfo.StoredEmptyChar"), Char)
    '        Me.C1DateEdit1.MaxLength = CType(resources.GetObject("C1DateEdit1.MaxLength"), Integer)
    '        Me.C1DateEdit1.Name = "C1DateEdit1"
    '        Me.C1DateEdit1.NullText = resources.GetString("C1DateEdit1.NullText")
    '        Me.C1DateEdit1.ParseInfo.CaseSensitive = CType(resources.GetObject("C1DateEdit1.ParseInfo.CaseSensitive"), Boolean)
    '        Me.C1DateEdit1.ParseInfo.CustomFormat = resources.GetString("C1DateEdit1.ParseInfo.CustomFormat")
    '        Me.C1DateEdit1.ParseInfo.DateTimeStyle = CType(resources.GetObject("C1DateEdit1.ParseInfo.DateTimeStyle"), C1.Win.C1Input.DateTimeStyleFlags)
    '        Me.C1DateEdit1.ParseInfo.EmptyAsNull = CType(resources.GetObject("C1DateEdit1.ParseInfo.EmptyAsNull"), Boolean)
    '        Me.C1DateEdit1.ParseInfo.ErrorMessage = resources.GetString("C1DateEdit1.ParseInfo.ErrorMessage")
    '        Me.C1DateEdit1.ParseInfo.FormatType = CType(resources.GetObject("C1DateEdit1.ParseInfo.FormatType"), C1.Win.C1Input.FormatTypeEnum)
    '        Me.C1DateEdit1.ParseInfo.Inherit = CType(resources.GetObject("C1DateEdit1.ParseInfo.Inherit"), C1.Win.C1Input.ParseInfoInheritFlags)
    '        Me.C1DateEdit1.ParseInfo.NullText = resources.GetString("C1DateEdit1.ParseInfo.NullText")
    '        Me.C1DateEdit1.ParseInfo.TrimEnd = CType(resources.GetObject("C1DateEdit1.ParseInfo.TrimEnd"), Boolean)
    '        Me.C1DateEdit1.ParseInfo.TrimStart = CType(resources.GetObject("C1DateEdit1.ParseInfo.TrimStart"), Boolean)
    '        Me.C1DateEdit1.PasswordChar = CType(resources.GetObject("C1DateEdit1.PasswordChar"), Char)
    '        Me.C1DateEdit1.PostValidation.CaseSensitive = CType(resources.GetObject("C1DateEdit1.PostValidation.CaseSensitive"), Boolean)
    '        Me.C1DateEdit1.PostValidation.ErrorMessage = resources.GetString("C1DateEdit1.PostValidation.ErrorMessage")
    '        Me.C1DateEdit1.PostValidation.Inherit = CType(resources.GetObject("C1DateEdit1.PostValidation.Inherit"), C1.Win.C1Input.PostValidationInheritFlags)
    '        Me.C1DateEdit1.PostValidation.Validation = CType(resources.GetObject("C1DateEdit1.PostValidation.Validation"), C1.Win.C1Input.PostValidationTypeEnum)
    '        Me.C1DateEdit1.PostValidation.Values = CType(resources.GetObject("C1DateEdit1.PostValidation.Values"), System.Array)
    '        Me.C1DateEdit1.PostValidation.ValuesExcluded = CType(resources.GetObject("C1DateEdit1.PostValidation.ValuesExcluded"), System.Array)
    '        Me.C1DateEdit1.PreValidation.CaseSensitive = CType(resources.GetObject("C1DateEdit1.PreValidation.CaseSensitive"), Boolean)
    '        Me.C1DateEdit1.PreValidation.ErrorMessage = resources.GetString("C1DateEdit1.PreValidation.ErrorMessage")
    '        Me.C1DateEdit1.PreValidation.Inherit = CType(resources.GetObject("C1DateEdit1.PreValidation.Inherit"), C1.Win.C1Input.PreValidationInheritFlags)
    '        Me.C1DateEdit1.PreValidation.ItemSeparator = resources.GetString("C1DateEdit1.PreValidation.ItemSeparator")
    '        Me.C1DateEdit1.PreValidation.PatternString = resources.GetString("C1DateEdit1.PreValidation.PatternString")
    '        Me.C1DateEdit1.PreValidation.RegexOptions = CType(resources.GetObject("C1DateEdit1.PreValidation.RegexOptions"), C1.Win.C1Input.RegexOptionFlags)
    '        Me.C1DateEdit1.PreValidation.TrimEnd = CType(resources.GetObject("C1DateEdit1.PreValidation.TrimEnd"), Boolean)
    '        Me.C1DateEdit1.PreValidation.TrimStart = CType(resources.GetObject("C1DateEdit1.PreValidation.TrimStart"), Boolean)
    '        Me.C1DateEdit1.PreValidation.Validation = CType(resources.GetObject("C1DateEdit1.PreValidation.Validation"), C1.Win.C1Input.PreValidationTypeEnum)
    '        Me.C1DateEdit1.RightToLeft = CType(resources.GetObject("C1DateEdit1.RightToLeft"), System.Windows.Forms.RightToLeft)
    '        Me.C1DateEdit1.ShowFocusRectangle = CType(resources.GetObject("C1DateEdit1.ShowFocusRectangle"), Boolean)
    '        Me.C1DateEdit1.Size = CType(resources.GetObject("C1DateEdit1.Size"), System.Drawing.Size)
    '        Me.C1DateEdit1.TabIndex = CType(resources.GetObject("C1DateEdit1.TabIndex"), Integer)
    '        Me.C1DateEdit1.Tag = CType(resources.GetObject("C1DateEdit1.Tag"), Object)
    '        Me.C1DateEdit1.TextAlign = CType(resources.GetObject("C1DateEdit1.TextAlign"), System.Windows.Forms.HorizontalAlignment)
    '        Me.C1DateEdit1.TrimEnd = CType(resources.GetObject("C1DateEdit1.TrimEnd"), Boolean)
    '        Me.C1DateEdit1.TrimStart = CType(resources.GetObject("C1DateEdit1.TrimStart"), Boolean)
    '        Me.C1DateEdit1.UserCultureOverride = CType(resources.GetObject("C1DateEdit1.UserCultureOverride"), Boolean)
    '        Me.C1DateEdit1.Value = CType(resources.GetObject("C1DateEdit1.Value"), Object)
    '        Me.C1DateEdit1.VerticalAlign = CType(resources.GetObject("C1DateEdit1.VerticalAlign"), C1.Win.C1Input.VerticalAlignEnum)
    '        Me.C1DateEdit1.Visible = CType(resources.GetObject("C1DateEdit1.Visible"), Boolean)
    '        Me.C1DateEdit1.VisibleButtons = CType(resources.GetObject("C1DateEdit1.VisibleButtons"), C1.Win.C1Input.DropDownControlButtonFlags)
    '        '
    '        'frmErrores
    '        '
    '        Me.AccessibleDescription = resources.GetString("$this.AccessibleDescription")
    '        Me.AccessibleName = resources.GetString("$this.AccessibleName")
    '        Me.AutoScaleBaseSize = CType(resources.GetObject("$this.AutoScaleBaseSize"), System.Drawing.Size)
    '        Me.AutoScroll = CType(resources.GetObject("$this.AutoScroll"), Boolean)
    '        Me.AutoScrollMargin = CType(resources.GetObject("$this.AutoScrollMargin"), System.Drawing.Size)
    '        Me.AutoScrollMinSize = CType(resources.GetObject("$this.AutoScrollMinSize"), System.Drawing.Size)
    '        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
    '        Me.ClientSize = CType(resources.GetObject("$this.ClientSize"), System.Drawing.Size)
    '        Me.Controls.Add(Me.C1DateEdit1)
    '        Me.Controls.Add(Me.Label2)
    '        Me.Controls.Add(Me.Label1)
    '        Me.Controls.Add(Me.cboFormulari)
    '        Me.Controls.Add(Me.cboError)
    '        Me.Cursor = System.Windows.Forms.Cursors.Default
    '        Me.Enabled = CType(resources.GetObject("$this.Enabled"), Boolean)
    '        Me.Font = CType(resources.GetObject("$this.Font"), System.Drawing.Font)
    '        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    '        Me.ImeMode = CType(resources.GetObject("$this.ImeMode"), System.Windows.Forms.ImeMode)
    '        Me.Location = CType(resources.GetObject("$this.Location"), System.Drawing.Point)
    '        Me.MaximumSize = CType(resources.GetObject("$this.MaximumSize"), System.Drawing.Size)
    '        Me.MinimumSize = CType(resources.GetObject("$this.MinimumSize"), System.Drawing.Size)
    '        Me.Name = "frmErrores"
    '        Me.RightToLeft = CType(resources.GetObject("$this.RightToLeft"), System.Windows.Forms.RightToLeft)
    '        Me.StartPosition = CType(resources.GetObject("$this.StartPosition"), System.Windows.Forms.FormStartPosition)
    '        Me.Text = resources.GetString("$this.Text")
    '        Me.Controls.SetChildIndex(Me.cboSeleccionCentro, 0)
    '        Me.Controls.SetChildIndex(Me.cboError, 0)
    '        Me.Controls.SetChildIndex(Me.cboFormulari, 0)
    '        Me.Controls.SetChildIndex(Me.Label1, 0)
    '        Me.Controls.SetChildIndex(Me.sbtipo, 0)
    '        Me.Controls.SetChildIndex(Me.btnActualizar, 0)
    '        Me.Controls.SetChildIndex(Me.btnNuevo, 0)
    '        Me.Controls.SetChildIndex(Me.btnBorrar, 0)
    '        Me.Controls.SetChildIndex(Me.btnTancar, 0)
    '        Me.Controls.SetChildIndex(Me.btnUltimo, 0)
    '        Me.Controls.SetChildIndex(Me.btnPrimero, 0)
    '        Me.Controls.SetChildIndex(Me.btnAnterior, 0)
    '        Me.Controls.SetChildIndex(Me.btnSiguiente, 0)
    '        Me.Controls.SetChildIndex(Me.btnRecargar, 0)
    '        Me.Controls.SetChildIndex(Me.btnVerLista, 0)
    '        Me.Controls.SetChildIndex(Me.btnModificar, 0)
    '        Me.Controls.SetChildIndex(Me.Label2, 0)
    '        Me.Controls.SetChildIndex(Me.C1DateEdit1, 0)
    '        CType(Me.cboError, System.ComponentModel.ISupportInitialize).EndInit()
    '        CType(Me.cboFormulari, System.ComponentModel.ISupportInitialize).EndInit()
    '        CType(Me.C1DateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
    '        Me.ResumeLayout(False)

    '    End Sub

    '#End Region

    '    Shared frmChildForm As frmErrores
    '    Public Shared Function GetInstance() As frmErrores
    '        If frmChildForm Is Nothing Then
    '            frmChildForm = New frmErrores

    '        End If
    '        Return frmChildForm
    '    End Function

    '    Public errorActual As clsError

    '    Friend Overrides Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Try
    '            errorActual.SiguienteReg() : ActualizarOrigen()
    '        Catch ex As Exception
    '            LOG(ex.ToString) : cargando = false : ccn()
    '        End Try
    '    End Sub

    '    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '        Try
    '            errorActual = New clsError(ds.Tables("errores"), "C", BindingContext)

    '        Catch ex As Exception
    '            LOG(ex.ToString) : cargando = false : ccn()
    '        End Try
    '    End Sub

    '    Friend Overrides Sub btnUltimo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Try
    '            errorActual.UltimoReg() : ActualizarOrigen()
    '        Catch ex As Exception
    '            LOG(ex.ToString) : cargando = false : ccn()
    '        End Try
    '    End Sub

    '   Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Try
    '            errorActual.PrimeroReg() : ActualizarOrigen()
    '        Catch ex As Exception
    '            LOG(ex.ToString) : cargando = false : ccn()
    '        End Try
    '    End Sub

    '    Friend Overrides Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Try
    '            errorActual.NuevoRegistro()
    '        Catch ex As Exception
    '            LOG(ex.ToString) : cargando = false : ccn()
    '        End Try
    '    End Sub

    '    Friend Overrides Sub btnAnterior_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Try
    '            errorActual.AnteriorReg() : ActualizarOrigen()
    '        Catch ex As Exception
    '            LOG(ex.ToString) : cargando = false : ccn()
    '        End Try
    '    End Sub

    '    Friend Overrides Sub btnBorrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Try
    '            errorActual.borrar() : ActualizarOrigen()
    '        Catch ex As Exception
    '            LOG(ex.ToString) : cargando = false : ccn()
    '        End Try
    '    End Sub

    '    Friend Overrides Sub btnModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    End Sub
    '    Private Sub ActualizarOrigen()
    '        Try

    '        Catch ex As Exception
    '            LOG(ex.ToString) : cargando = False : CCN()
    '        End Try
    '    End Sub
    '    Friend Overrides Sub btnActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Try
    '            cargando = True
    '            If errorActual.TieneCambios Then
    '                Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
    '                    Case DialogResult.Cancel
    '                        cargando = False
    '                        Exit Sub
    '                    Case DialogResult.No
    '                        errorActual.tabla.RejectChanges()
    '                        cargando = False
    '                        Exit Sub
    '                End Select
    '            End If
    '            errorActual.ActualizarOrigen()
    '            cargando = False

    '        Catch ex As Exception
    '            LOG(ex.ToString) : cargando = False : CCN()
    '        End Try
    '    End Sub


    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmErrores))
        Me.C1TrueDBGrid1 = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        CType(Me.C1TrueDBGrid1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'C1TrueDBGrid1
        '
        resources.ApplyResources(Me.C1TrueDBGrid1, "C1TrueDBGrid1")
        Me.C1TrueDBGrid1.Images.Add(CType(resources.GetObject("C1TrueDBGrid1.Images"),System.Drawing.Image))
        Me.C1TrueDBGrid1.Name = "C1TrueDBGrid1"
        Me.C1TrueDBGrid1.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.C1TrueDBGrid1.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.C1TrueDBGrid1.PreviewInfo.ZoomFactor = 75R
        Me.C1TrueDBGrid1.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.C1TrueDBGrid1.PrintInfo.MeasurementPrinterName = Nothing
        Me.C1TrueDBGrid1.PropBag = resources.GetString("C1TrueDBGrid1.PropBag")
        Me.C1TrueDBGrid1.RowDivider.Color = CType(resources.GetObject("C1TrueDBGrid1.RowDivider.Color"),System.Drawing.Color)
        Me.C1TrueDBGrid1.RowDivider.Style = CType(resources.GetObject("C1TrueDBGrid1.RowDivider.Style"),C1.Win.C1TrueDBGrid.LineStyleEnum)
        Me.C1TrueDBGrid1.UseCompatibleTextRendering = false
        '
        'frmErrores
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.C1TrueDBGrid1)
        Me.Name = "frmErrores"
        CType(Me.C1TrueDBGrid1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Private Sub frmErrores_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       

    End Sub
End Class
