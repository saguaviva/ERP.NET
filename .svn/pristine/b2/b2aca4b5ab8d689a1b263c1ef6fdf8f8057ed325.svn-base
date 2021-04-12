Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones


Public Class frmControlStockHilos
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
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage

    Friend WithEvents C1TrueDBGrid1 As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmControlStockHilos))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.C1TrueDBGrid1 = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.C1TrueDBGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnTancar
        '
        Me.btnTancar.Name = "btnTancar"
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = CType(resources.GetObject("sbtipo.Location"), System.Drawing.Point)
        Me.sbtipo.Name = "sbtipo"
        Me.sbtipo.Size = CType(resources.GetObject("sbtipo.Size"), System.Drawing.Size)
        Me.sbtipo.Text = resources.GetString("sbtipo.Text")
        '
        'btnRecargar
        '
        Me.btnRecargar.Name = "btnRecargar"
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Name = "btnSiguiente"
        '
        'btnAnterior
        '
        Me.btnAnterior.Name = "btnAnterior"
        '
        'btnPrimero
        '
        Me.btnPrimero.Name = "btnPrimero"
        '
        'btnUltimo
        '
        Me.btnUltimo.Name = "btnUltimo"
        '
        'btnModificar
        '
        Me.btnModificar.Name = "btnModificar"
        '
        'btnBorrar
        '
        Me.btnBorrar.Name = "btnBorrar"
        '
        'btnNuevo
        '
        Me.btnNuevo.Name = "btnNuevo"
        '
        'btnVerLista
        '
        Me.btnVerLista.Name = "btnVerLista"
        '
        'btnActualizar
        '
        Me.btnActualizar.Name = "btnActualizar"
        '
        'TabControl1
        '
        Me.TabControl1.AccessibleDescription = resources.GetString("TabControl1.AccessibleDescription")
        Me.TabControl1.AccessibleName = resources.GetString("TabControl1.AccessibleName")
        Me.TabControl1.Alignment = CType(resources.GetObject("TabControl1.Alignment"), System.Windows.Forms.TabAlignment)
        Me.TabControl1.Anchor = CType(resources.GetObject("TabControl1.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Appearance = CType(resources.GetObject("TabControl1.Appearance"), System.Windows.Forms.TabAppearance)
        Me.TabControl1.BackgroundImage = CType(resources.GetObject("TabControl1.BackgroundImage"), System.Drawing.Image)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Dock = CType(resources.GetObject("TabControl1.Dock"), System.Windows.Forms.DockStyle)
        Me.TabControl1.Enabled = CType(resources.GetObject("TabControl1.Enabled"), Boolean)
        Me.TabControl1.Font = CType(resources.GetObject("TabControl1.Font"), System.Drawing.Font)
        Me.TabControl1.ImeMode = CType(resources.GetObject("TabControl1.ImeMode"), System.Windows.Forms.ImeMode)
        Me.TabControl1.ItemSize = CType(resources.GetObject("TabControl1.ItemSize"), System.Drawing.Size)
        Me.TabControl1.Location = CType(resources.GetObject("TabControl1.Location"), System.Drawing.Point)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.Padding = CType(resources.GetObject("TabControl1.Padding"), System.Drawing.Point)
        Me.TabControl1.RightToLeft = CType(resources.GetObject("TabControl1.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.ShowToolTips = CType(resources.GetObject("TabControl1.ShowToolTips"), Boolean)
        Me.TabControl1.Size = CType(resources.GetObject("TabControl1.Size"), System.Drawing.Size)
        Me.TabControl1.TabIndex = CType(resources.GetObject("TabControl1.TabIndex"), Integer)
        Me.TabControl1.Text = resources.GetString("TabControl1.Text")
        Me.TabControl1.Visible = CType(resources.GetObject("TabControl1.Visible"), Boolean)
        '
        'TabPage1
        '
        Me.TabPage1.AccessibleDescription = resources.GetString("TabPage1.AccessibleDescription")
        Me.TabPage1.AccessibleName = resources.GetString("TabPage1.AccessibleName")
        Me.TabPage1.Anchor = CType(resources.GetObject("TabPage1.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.TabPage1.AutoScroll = CType(resources.GetObject("TabPage1.AutoScroll"), Boolean)
        Me.TabPage1.AutoScrollMargin = CType(resources.GetObject("TabPage1.AutoScrollMargin"), System.Drawing.Size)
        Me.TabPage1.AutoScrollMinSize = CType(resources.GetObject("TabPage1.AutoScrollMinSize"), System.Drawing.Size)
        Me.TabPage1.BackgroundImage = CType(resources.GetObject("TabPage1.BackgroundImage"), System.Drawing.Image)
        Me.TabPage1.Controls.Add(Me.C1TrueDBGrid1)
        Me.TabPage1.Dock = CType(resources.GetObject("TabPage1.Dock"), System.Windows.Forms.DockStyle)
        Me.TabPage1.Enabled = CType(resources.GetObject("TabPage1.Enabled"), Boolean)
        Me.TabPage1.Font = CType(resources.GetObject("TabPage1.Font"), System.Drawing.Font)
        Me.TabPage1.ImageIndex = CType(resources.GetObject("TabPage1.ImageIndex"), Integer)
        Me.TabPage1.ImeMode = CType(resources.GetObject("TabPage1.ImeMode"), System.Windows.Forms.ImeMode)
        Me.TabPage1.Location = CType(resources.GetObject("TabPage1.Location"), System.Drawing.Point)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.RightToLeft = CType(resources.GetObject("TabPage1.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.TabPage1.Size = CType(resources.GetObject("TabPage1.Size"), System.Drawing.Size)
        Me.TabPage1.TabIndex = CType(resources.GetObject("TabPage1.TabIndex"), Integer)
        Me.TabPage1.Text = resources.GetString("TabPage1.Text")
        Me.TabPage1.ToolTipText = resources.GetString("TabPage1.ToolTipText")
        Me.TabPage1.Visible = CType(resources.GetObject("TabPage1.Visible"), Boolean)
        '
        'C1TrueDBGrid1
        '
        Me.C1TrueDBGrid1.AccessibleDescription = resources.GetString("C1TrueDBGrid1.AccessibleDescription")
        Me.C1TrueDBGrid1.AccessibleName = resources.GetString("C1TrueDBGrid1.AccessibleName")
        Me.C1TrueDBGrid1.AllowAddNew = CType(resources.GetObject("C1TrueDBGrid1.AllowAddNew"), Boolean)
        Me.C1TrueDBGrid1.AllowArrows = CType(resources.GetObject("C1TrueDBGrid1.AllowArrows"), Boolean)
        Me.C1TrueDBGrid1.AllowColMove = CType(resources.GetObject("C1TrueDBGrid1.AllowColMove"), Boolean)
        Me.C1TrueDBGrid1.AllowColSelect = CType(resources.GetObject("C1TrueDBGrid1.AllowColSelect"), Boolean)
        Me.C1TrueDBGrid1.AllowDelete = CType(resources.GetObject("C1TrueDBGrid1.AllowDelete"), Boolean)
        Me.C1TrueDBGrid1.AllowDrag = CType(resources.GetObject("C1TrueDBGrid1.AllowDrag"), Boolean)
        Me.C1TrueDBGrid1.AllowFilter = CType(resources.GetObject("C1TrueDBGrid1.AllowFilter"), Boolean)
        Me.C1TrueDBGrid1.AllowHorizontalSplit = CType(resources.GetObject("C1TrueDBGrid1.AllowHorizontalSplit"), Boolean)
        Me.C1TrueDBGrid1.AllowRowSelect = CType(resources.GetObject("C1TrueDBGrid1.AllowRowSelect"), Boolean)
        Me.C1TrueDBGrid1.AllowSort = CType(resources.GetObject("C1TrueDBGrid1.AllowSort"), Boolean)
        Me.C1TrueDBGrid1.AllowUpdate = CType(resources.GetObject("C1TrueDBGrid1.AllowUpdate"), Boolean)
        Me.C1TrueDBGrid1.AllowUpdateOnBlur = CType(resources.GetObject("C1TrueDBGrid1.AllowUpdateOnBlur"), Boolean)
        Me.C1TrueDBGrid1.AllowVerticalSplit = CType(resources.GetObject("C1TrueDBGrid1.AllowVerticalSplit"), Boolean)
        Me.C1TrueDBGrid1.AlternatingRows = CType(resources.GetObject("C1TrueDBGrid1.AlternatingRows"), Boolean)
        Me.C1TrueDBGrid1.Anchor = CType(resources.GetObject("C1TrueDBGrid1.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.C1TrueDBGrid1.BackgroundImage = CType(resources.GetObject("C1TrueDBGrid1.BackgroundImage"), System.Drawing.Image)
        Me.C1TrueDBGrid1.BorderStyle = CType(resources.GetObject("C1TrueDBGrid1.BorderStyle"), System.Windows.Forms.BorderStyle)
        Me.C1TrueDBGrid1.Caption = resources.GetString("C1TrueDBGrid1.Caption")
        Me.C1TrueDBGrid1.CaptionHeight = CType(resources.GetObject("C1TrueDBGrid1.CaptionHeight"), Integer)
        Me.C1TrueDBGrid1.CellTipsDelay = CType(resources.GetObject("C1TrueDBGrid1.CellTipsDelay"), Integer)
        Me.C1TrueDBGrid1.CellTipsWidth = CType(resources.GetObject("C1TrueDBGrid1.CellTipsWidth"), Integer)
        Me.C1TrueDBGrid1.ChildGrid = CType(resources.GetObject("C1TrueDBGrid1.ChildGrid"), C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        Me.C1TrueDBGrid1.CollapseColor = CType(resources.GetObject("C1TrueDBGrid1.CollapseColor"), System.Drawing.Color)
        Me.C1TrueDBGrid1.ColumnFooters = CType(resources.GetObject("C1TrueDBGrid1.ColumnFooters"), Boolean)
        Me.C1TrueDBGrid1.ColumnHeaders = CType(resources.GetObject("C1TrueDBGrid1.ColumnHeaders"), Boolean)
        Me.C1TrueDBGrid1.DefColWidth = CType(resources.GetObject("C1TrueDBGrid1.DefColWidth"), Integer)
        Me.C1TrueDBGrid1.Dock = CType(resources.GetObject("C1TrueDBGrid1.Dock"), System.Windows.Forms.DockStyle)
        Me.C1TrueDBGrid1.EditDropDown = CType(resources.GetObject("C1TrueDBGrid1.EditDropDown"), Boolean)
        Me.C1TrueDBGrid1.EmptyRows = CType(resources.GetObject("C1TrueDBGrid1.EmptyRows"), Boolean)
        Me.C1TrueDBGrid1.Enabled = CType(resources.GetObject("C1TrueDBGrid1.Enabled"), Boolean)
        Me.C1TrueDBGrid1.ExpandColor = CType(resources.GetObject("C1TrueDBGrid1.ExpandColor"), System.Drawing.Color)
        Me.C1TrueDBGrid1.ExposeCellMode = CType(resources.GetObject("C1TrueDBGrid1.ExposeCellMode"), C1.Win.C1TrueDBGrid.ExposeCellModeEnum)
        Me.C1TrueDBGrid1.ExtendRightColumn = CType(resources.GetObject("C1TrueDBGrid1.ExtendRightColumn"), Boolean)
        Me.C1TrueDBGrid1.FetchRowStyles = CType(resources.GetObject("C1TrueDBGrid1.FetchRowStyles"), Boolean)
        Me.C1TrueDBGrid1.FilterBar = CType(resources.GetObject("C1TrueDBGrid1.FilterBar"), Boolean)
        Me.C1TrueDBGrid1.FlatStyle = CType(resources.GetObject("C1TrueDBGrid1.FlatStyle"), C1.Win.C1TrueDBGrid.FlatModeEnum)
        Me.C1TrueDBGrid1.Font = CType(resources.GetObject("C1TrueDBGrid1.Font"), System.Drawing.Font)
        Me.C1TrueDBGrid1.GroupByAreaVisible = CType(resources.GetObject("C1TrueDBGrid1.GroupByAreaVisible"), Boolean)
        Me.C1TrueDBGrid1.GroupByCaption = resources.GetString("C1TrueDBGrid1.GroupByCaption")
        Me.C1TrueDBGrid1.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.C1TrueDBGrid1.ImeMode = CType(resources.GetObject("C1TrueDBGrid1.ImeMode"), System.Windows.Forms.ImeMode)
        Me.C1TrueDBGrid1.LinesPerRow = CType(resources.GetObject("C1TrueDBGrid1.LinesPerRow"), Integer)
        Me.C1TrueDBGrid1.Location = CType(resources.GetObject("C1TrueDBGrid1.Location"), System.Drawing.Point)
        Me.C1TrueDBGrid1.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.C1TrueDBGrid1.Name = "C1TrueDBGrid1"
        Me.C1TrueDBGrid1.PictureAddnewRow = CType(resources.GetObject("C1TrueDBGrid1.PictureAddnewRow"), System.Drawing.Image)
        Me.C1TrueDBGrid1.PictureCurrentRow = CType(resources.GetObject("C1TrueDBGrid1.PictureCurrentRow"), System.Drawing.Image)
        Me.C1TrueDBGrid1.PictureFilterBar = CType(resources.GetObject("C1TrueDBGrid1.PictureFilterBar"), System.Drawing.Image)
        Me.C1TrueDBGrid1.PictureFooterRow = CType(resources.GetObject("C1TrueDBGrid1.PictureFooterRow"), System.Drawing.Image)
        Me.C1TrueDBGrid1.PictureHeaderRow = CType(resources.GetObject("C1TrueDBGrid1.PictureHeaderRow"), System.Drawing.Image)
        Me.C1TrueDBGrid1.PictureModifiedRow = CType(resources.GetObject("C1TrueDBGrid1.PictureModifiedRow"), System.Drawing.Image)
        Me.C1TrueDBGrid1.PictureStandardRow = CType(resources.GetObject("C1TrueDBGrid1.PictureStandardRow"), System.Drawing.Image)
        Me.C1TrueDBGrid1.PreviewInfo.AllowSizing = CType(resources.GetObject("C1TrueDBGrid1.PreviewInfo.AllowSizing"), Boolean)
        Me.C1TrueDBGrid1.PreviewInfo.Caption = resources.GetString("C1TrueDBGrid1.PreviewInfo.Caption")
        Me.C1TrueDBGrid1.PreviewInfo.Location = CType(resources.GetObject("C1TrueDBGrid1.PreviewInfo.Location"), System.Drawing.Point)
        Me.C1TrueDBGrid1.PreviewInfo.Size = CType(resources.GetObject("C1TrueDBGrid1.PreviewInfo.Size"), System.Drawing.Size)
        Me.C1TrueDBGrid1.PreviewInfo.ToolBars = CType(resources.GetObject("C1TrueDBGrid1.PreviewInfo.ToolBars"), Boolean)
        Me.C1TrueDBGrid1.PreviewInfo.UIStrings.Content = CType(resources.GetObject("C1TrueDBGrid1.PreviewInfo.UIStrings.Content"), String())
        Me.C1TrueDBGrid1.PreviewInfo.ZoomFactor = CType(resources.GetObject("C1TrueDBGrid1.PreviewInfo.ZoomFactor"), Double)
        Me.C1TrueDBGrid1.PrintInfo.MaxRowHeight = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.MaxRowHeight"), Integer)
        Me.C1TrueDBGrid1.PrintInfo.OwnerDrawPageFooter = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.OwnerDrawPageFooter"), Boolean)
        Me.C1TrueDBGrid1.PrintInfo.OwnerDrawPageHeader = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.OwnerDrawPageHeader"), Boolean)
        Me.C1TrueDBGrid1.PrintInfo.PageFooter = resources.GetString("C1TrueDBGrid1.PrintInfo.PageFooter")
        Me.C1TrueDBGrid1.PrintInfo.PageFooterHeight = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.PageFooterHeight"), Integer)
        Me.C1TrueDBGrid1.PrintInfo.PageHeader = resources.GetString("C1TrueDBGrid1.PrintInfo.PageHeader")
        Me.C1TrueDBGrid1.PrintInfo.PageHeaderHeight = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.PageHeaderHeight"), Integer)
        Me.C1TrueDBGrid1.PrintInfo.PrintHorizontalSplits = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.PrintHorizontalSplits"), Boolean)
        Me.C1TrueDBGrid1.PrintInfo.ProgressCaption = resources.GetString("C1TrueDBGrid1.PrintInfo.ProgressCaption")
        Me.C1TrueDBGrid1.PrintInfo.RepeatColumnFooters = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.RepeatColumnFooters"), Boolean)
        Me.C1TrueDBGrid1.PrintInfo.RepeatColumnHeaders = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.RepeatColumnHeaders"), Boolean)
        Me.C1TrueDBGrid1.PrintInfo.RepeatGridHeader = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.RepeatGridHeader"), Boolean)
        Me.C1TrueDBGrid1.PrintInfo.RepeatSplitHeaders = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.RepeatSplitHeaders"), Boolean)
        Me.C1TrueDBGrid1.PrintInfo.ShowOptionsDialog = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.ShowOptionsDialog"), Boolean)
        Me.C1TrueDBGrid1.PrintInfo.ShowProgressForm = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.ShowProgressForm"), Boolean)
        Me.C1TrueDBGrid1.PrintInfo.ShowSelection = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.ShowSelection"), Boolean)
        Me.C1TrueDBGrid1.PrintInfo.UseGridColors = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.UseGridColors"), Boolean)
        Me.C1TrueDBGrid1.RecordSelectors = CType(resources.GetObject("C1TrueDBGrid1.RecordSelectors"), Boolean)
        Me.C1TrueDBGrid1.RecordSelectorWidth = CType(resources.GetObject("C1TrueDBGrid1.RecordSelectorWidth"), Integer)
        Me.C1TrueDBGrid1.RightToLeft = CType(resources.GetObject("C1TrueDBGrid1.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.C1TrueDBGrid1.RowDivider.Color = CType(resources.GetObject("resource.Color"), System.Drawing.Color)
        Me.C1TrueDBGrid1.RowDivider.Style = CType(resources.GetObject("resource.Style"), C1.Win.C1TrueDBGrid.LineStyleEnum)
        Me.C1TrueDBGrid1.RowHeight = CType(resources.GetObject("C1TrueDBGrid1.RowHeight"), Integer)
        Me.C1TrueDBGrid1.RowSubDividerColor = CType(resources.GetObject("C1TrueDBGrid1.RowSubDividerColor"), System.Drawing.Color)
        Me.C1TrueDBGrid1.ScrollTips = CType(resources.GetObject("C1TrueDBGrid1.ScrollTips"), Boolean)
        Me.C1TrueDBGrid1.ScrollTrack = CType(resources.GetObject("C1TrueDBGrid1.ScrollTrack"), Boolean)
        Me.C1TrueDBGrid1.Size = CType(resources.GetObject("C1TrueDBGrid1.Size"), System.Drawing.Size)
        Me.C1TrueDBGrid1.SpringMode = CType(resources.GetObject("C1TrueDBGrid1.SpringMode"), Boolean)
        Me.C1TrueDBGrid1.TabAcrossSplits = CType(resources.GetObject("C1TrueDBGrid1.TabAcrossSplits"), Boolean)
        Me.C1TrueDBGrid1.TabIndex = CType(resources.GetObject("C1TrueDBGrid1.TabIndex"), Integer)
        Me.C1TrueDBGrid1.Text = resources.GetString("C1TrueDBGrid1.Text")
        Me.C1TrueDBGrid1.ViewCaptionWidth = CType(resources.GetObject("C1TrueDBGrid1.ViewCaptionWidth"), Integer)
        Me.C1TrueDBGrid1.ViewColumnWidth = CType(resources.GetObject("C1TrueDBGrid1.ViewColumnWidth"), Integer)
        Me.C1TrueDBGrid1.Visible = CType(resources.GetObject("C1TrueDBGrid1.Visible"), Boolean)
        Me.C1TrueDBGrid1.WrapCellPointer = CType(resources.GetObject("C1TrueDBGrid1.WrapCellPointer"), Boolean)
        Me.C1TrueDBGrid1.PropBag = resources.GetString("C1TrueDBGrid1.PropBag")
        '
        'frmControlStockHilos
        '
        Me.AccessibleDescription = resources.GetString("$this.AccessibleDescription")
        Me.AccessibleName = resources.GetString("$this.AccessibleName")
        Me.AutoScaleBaseSize = CType(resources.GetObject("$this.AutoScaleBaseSize"), System.Drawing.Size)
        Me.AutoScroll = CType(resources.GetObject("$this.AutoScroll"), Boolean)
        Me.AutoScrollMargin = CType(resources.GetObject("$this.AutoScrollMargin"), System.Drawing.Size)
        Me.AutoScrollMinSize = CType(resources.GetObject("$this.AutoScrollMinSize"), System.Drawing.Size)
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = CType(resources.GetObject("$this.ClientSize"), System.Drawing.Size)
        Me.Controls.Add(Me.TabControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Enabled = CType(resources.GetObject("$this.Enabled"), Boolean)
        Me.Font = CType(resources.GetObject("$this.Font"), System.Drawing.Font)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.ImeMode = CType(resources.GetObject("$this.ImeMode"), System.Windows.Forms.ImeMode)
        Me.Location = CType(resources.GetObject("$this.Location"), System.Drawing.Point)
        Me.MaximumSize = CType(resources.GetObject("$this.MaximumSize"), System.Drawing.Size)
        Me.MinimumSize = CType(resources.GetObject("$this.MinimumSize"), System.Drawing.Size)
        Me.Name = "frmControlStockHilos"
        Me.RightToLeft = CType(resources.GetObject("$this.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.StartPosition = CType(resources.GetObject("$this.StartPosition"), System.Windows.Forms.FormStartPosition)
        Me.Text = resources.GetString("$this.Text")
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
        Me.TabPage1.ResumeLayout(False)
        CType(Me.C1TrueDBGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmControlStockHilos
    Public Shared Function GetInstance() As frmControlStockHilos
        If frmChildForm Is Nothing Then
            frmChildForm = New frmControlStockHilos

        End If
        Return frmChildForm
    End Function

    Private colMETROS As Short = 6
    Private colDIF As Short = 7
    Private dt As DataTable = New DataTable("stockhilos")
    Private dv As DataView
    Private colCODIGO As Short = 0
    Private colPROVE As Short = 1
    Private colCOLOR As Short = 2
    Private colACTUAL As Short = 3
    Private colMINIMO As Short = 4
    Private colKG As Short = 5
    Private da As New MySql.Data.MySqlClient.MySqlDataAdapter
    Private cmdSelect As New MySql.Data.MySqlClient.MySqlCommand


    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try

            cmdSelect.Connection = cnn

            da.SelectCommand = cmdSelect
            CargarHilos()

            CrearTabla()
            'HacerBinding
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub CargarHilos()
        Try
            'CODI, DESCRI, PREU, PROVE, COST, OBSERV, IVA
            'TIPUS, FIL, PROVE, COLOR, ACTUALM MINIM, PREU, TINTAR, METRES, KG, OBSERV
            cmdSelect.CommandText = "SELECT fil.codi, " & _
                                        " prove.nom as NOMPROVEIDOR, " & _
                                        " filcol.color, " & _
                                        " filcol.actual, " & _
                                        " filcol.minim, " & _
                                        " filcol.kg, " & _
                                        " filcol.metres " & _
                                        " FROM fil LEFT JOIN filcol ON (filcol.fil = fil.codi) LEFT JOIN prove ON (prove.CODI = fil.prove) " & _
                                        " WHERE filcol.TIPUS = ""F"" ORDER BY fil.codi, fil.prove, filcol.color"

            da.Fill(dt)


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub C1TrueDBGrid1_FetchCellStyle(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.FetchCellStyleEventArgs) Handles C1TrueDBGrid1.FetchCellStyle
        Try
            Select Case e.Col
                Case colDIF
                    If CDbl(nzn(C1TrueDBGrid1.Columns(colDIF).CellText(e.Row), 0)) < 0 Then
                        e.CellStyle.ForeColor = Color.Red
                    End If

                Case Else
                    Debug.WriteLine("FetchCellStyle not handled: " & e.Col)

            End Select

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CrearTabla()
        ' Dim dcHILO As New DataColumn("FIL", System.Type.GetType("string"))
        ' Dim dcCOLOR As New DataColumn("COLOR", System.Type.GetType("string"))
        Dim dcDIF As New DataColumn("DIFERENCIA")
        Dim i As Integer
        Dim j As Double
        Try
            dcDIF.DataType = j.GetType


            dt.Columns.Add(dcDIF)
            dv = dt.DefaultView
            For i = 0 To dv.Count - 1
                dv(i).Item("DIFERENCIA") = nzn(dv(i).Item("ACTUAL"), 0) - nzn(dv(i).Item("MINIM"), 0)

            Next
            C1TrueDBGrid1.SetDataBinding(dv, "")
            C1TrueDBGrid1.Splits(0).DisplayColumns("DIFERENCIA").FetchStyle = True
            C1TrueDBGrid1.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.GroupBy
            For i = 0 To C1TrueDBGrid1.Splits(0).DisplayColumns.Count - 1
                C1TrueDBGrid1.Splits(0).DisplayColumns(i).AutoSize()
            Next
            C1TrueDBGrid1.GroupByAreaVisible = True


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#Region "OVERRIDES"

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTancar.Click
        Close()
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
    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

#End Region

End Class
