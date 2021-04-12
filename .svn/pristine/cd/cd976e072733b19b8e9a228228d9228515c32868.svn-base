'! Problem auto-fix by Project Analyzer 7.0.01 on 16/07/2004
'! Original file C:\aura2k3\aura2k3\frmElegirTemporadaAMostrar.vb
'! Dead class: frmElegirTemporadaAMostrar
Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Public Class frmElegirTemporadaAMostrar
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
    '! Events not handled: lstchkTemporadas
    '! WithEvents adds overhead: lstchkTemporadas
    'C1.Win.C1Input.C1Button
    '! WithEvents adds overhead: btnAceptar
    Friend WithEvents btnAceptar As C1.Win.C1Input.C1Button
    Friend WithEvents dgTemporadas As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmElegirTemporadaAMostrar))
        Me.btnAceptar = New C1.Win.C1Input.C1Button
        Me.dgTemporadas = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        CType(Me.dgTemporadas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAceptar
        '
        Me.btnAceptar.AccessibleDescription = resources.GetString("btnAceptar.AccessibleDescription")
        Me.btnAceptar.AccessibleName = resources.GetString("btnAceptar.AccessibleName")
        Me.btnAceptar.Anchor = CType(resources.GetObject("btnAceptar.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.btnAceptar.BackgroundImage = CType(resources.GetObject("btnAceptar.BackgroundImage"), System.Drawing.Image)
        Me.btnAceptar.Dock = CType(resources.GetObject("btnAceptar.Dock"), System.Windows.Forms.DockStyle)
        Me.btnAceptar.Enabled = CType(resources.GetObject("btnAceptar.Enabled"), Boolean)
        Me.btnAceptar.FlatStyle = CType(resources.GetObject("btnAceptar.FlatStyle"), System.Windows.Forms.FlatStyle)
        Me.btnAceptar.Font = CType(resources.GetObject("btnAceptar.Font"), System.Drawing.Font)
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Image)
        Me.btnAceptar.ImageAlign = CType(resources.GetObject("btnAceptar.ImageAlign"), System.Drawing.ContentAlignment)
        Me.btnAceptar.ImageIndex = CType(resources.GetObject("btnAceptar.ImageIndex"), Integer)
        Me.btnAceptar.ImeMode = CType(resources.GetObject("btnAceptar.ImeMode"), System.Windows.Forms.ImeMode)
        Me.btnAceptar.Location = CType(resources.GetObject("btnAceptar.Location"), System.Drawing.Point)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.RightToLeft = CType(resources.GetObject("btnAceptar.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.btnAceptar.Size = CType(resources.GetObject("btnAceptar.Size"), System.Drawing.Size)
        Me.btnAceptar.TabIndex = CType(resources.GetObject("btnAceptar.TabIndex"), Integer)
        Me.btnAceptar.Text = resources.GetString("btnAceptar.Text")
        Me.btnAceptar.TextAlign = CType(resources.GetObject("btnAceptar.TextAlign"), System.Drawing.ContentAlignment)
        Me.btnAceptar.Visible = CType(resources.GetObject("btnAceptar.Visible"), Boolean)
        '
        'dgTemporadas
        '
        Me.dgTemporadas.AccessibleDescription = resources.GetString("dgTemporadas.AccessibleDescription")
        Me.dgTemporadas.AccessibleName = resources.GetString("dgTemporadas.AccessibleName")
        Me.dgTemporadas.AllowAddNew = CType(resources.GetObject("dgTemporadas.AllowAddNew"), Boolean)
        Me.dgTemporadas.AllowArrows = CType(resources.GetObject("dgTemporadas.AllowArrows"), Boolean)
        Me.dgTemporadas.AllowColMove = CType(resources.GetObject("dgTemporadas.AllowColMove"), Boolean)
        Me.dgTemporadas.AllowColSelect = CType(resources.GetObject("dgTemporadas.AllowColSelect"), Boolean)
        Me.dgTemporadas.AllowDelete = CType(resources.GetObject("dgTemporadas.AllowDelete"), Boolean)
        Me.dgTemporadas.AllowDrag = CType(resources.GetObject("dgTemporadas.AllowDrag"), Boolean)
        Me.dgTemporadas.AllowFilter = CType(resources.GetObject("dgTemporadas.AllowFilter"), Boolean)
        Me.dgTemporadas.AllowHorizontalSplit = CType(resources.GetObject("dgTemporadas.AllowHorizontalSplit"), Boolean)
        Me.dgTemporadas.AllowRowSelect = CType(resources.GetObject("dgTemporadas.AllowRowSelect"), Boolean)
        Me.dgTemporadas.AllowSort = CType(resources.GetObject("dgTemporadas.AllowSort"), Boolean)
        Me.dgTemporadas.AllowUpdate = CType(resources.GetObject("dgTemporadas.AllowUpdate"), Boolean)
        Me.dgTemporadas.AllowUpdateOnBlur = CType(resources.GetObject("dgTemporadas.AllowUpdateOnBlur"), Boolean)
        Me.dgTemporadas.AllowVerticalSplit = CType(resources.GetObject("dgTemporadas.AllowVerticalSplit"), Boolean)
        Me.dgTemporadas.AlternatingRows = CType(resources.GetObject("dgTemporadas.AlternatingRows"), Boolean)
        Me.dgTemporadas.Anchor = CType(resources.GetObject("dgTemporadas.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.dgTemporadas.BackgroundImage = CType(resources.GetObject("dgTemporadas.BackgroundImage"), System.Drawing.Image)
        Me.dgTemporadas.BorderStyle = CType(resources.GetObject("dgTemporadas.BorderStyle"), System.Windows.Forms.BorderStyle)
        Me.dgTemporadas.Caption = resources.GetString("dgTemporadas.Caption")
        Me.dgTemporadas.CaptionHeight = CType(resources.GetObject("dgTemporadas.CaptionHeight"), Integer)
        Me.dgTemporadas.CellTipsDelay = CType(resources.GetObject("dgTemporadas.CellTipsDelay"), Integer)
        Me.dgTemporadas.CellTipsWidth = CType(resources.GetObject("dgTemporadas.CellTipsWidth"), Integer)
        Me.dgTemporadas.ChildGrid = CType(resources.GetObject("dgTemporadas.ChildGrid"), C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        Me.dgTemporadas.CollapseColor = CType(resources.GetObject("dgTemporadas.CollapseColor"), System.Drawing.Color)
        Me.dgTemporadas.ColumnFooters = CType(resources.GetObject("dgTemporadas.ColumnFooters"), Boolean)
        Me.dgTemporadas.ColumnHeaders = CType(resources.GetObject("dgTemporadas.ColumnHeaders"), Boolean)
        Me.dgTemporadas.DefColWidth = CType(resources.GetObject("dgTemporadas.DefColWidth"), Integer)
        Me.dgTemporadas.Dock = CType(resources.GetObject("dgTemporadas.Dock"), System.Windows.Forms.DockStyle)
        Me.dgTemporadas.EditDropDown = CType(resources.GetObject("dgTemporadas.EditDropDown"), Boolean)
        Me.dgTemporadas.EmptyRows = CType(resources.GetObject("dgTemporadas.EmptyRows"), Boolean)
        Me.dgTemporadas.Enabled = CType(resources.GetObject("dgTemporadas.Enabled"), Boolean)
        Me.dgTemporadas.ExpandColor = CType(resources.GetObject("dgTemporadas.ExpandColor"), System.Drawing.Color)
        Me.dgTemporadas.ExposeCellMode = CType(resources.GetObject("dgTemporadas.ExposeCellMode"), C1.Win.C1TrueDBGrid.ExposeCellModeEnum)
        Me.dgTemporadas.ExtendRightColumn = CType(resources.GetObject("dgTemporadas.ExtendRightColumn"), Boolean)
        Me.dgTemporadas.FetchRowStyles = CType(resources.GetObject("dgTemporadas.FetchRowStyles"), Boolean)
        Me.dgTemporadas.FilterBar = CType(resources.GetObject("dgTemporadas.FilterBar"), Boolean)
        Me.dgTemporadas.FlatStyle = CType(resources.GetObject("dgTemporadas.FlatStyle"), C1.Win.C1TrueDBGrid.FlatModeEnum)
        Me.dgTemporadas.Font = CType(resources.GetObject("dgTemporadas.Font"), System.Drawing.Font)
        Me.dgTemporadas.GroupByAreaVisible = CType(resources.GetObject("dgTemporadas.GroupByAreaVisible"), Boolean)
        Me.dgTemporadas.GroupByCaption = resources.GetString("dgTemporadas.GroupByCaption")
        Me.dgTemporadas.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.dgTemporadas.ImeMode = CType(resources.GetObject("dgTemporadas.ImeMode"), System.Windows.Forms.ImeMode)
        Me.dgTemporadas.LinesPerRow = CType(resources.GetObject("dgTemporadas.LinesPerRow"), Integer)
        Me.dgTemporadas.Location = CType(resources.GetObject("dgTemporadas.Location"), System.Drawing.Point)
        Me.dgTemporadas.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.dgTemporadas.Name = "dgTemporadas"
        Me.dgTemporadas.PictureAddnewRow = CType(resources.GetObject("dgTemporadas.PictureAddnewRow"), System.Drawing.Image)
        Me.dgTemporadas.PictureCurrentRow = CType(resources.GetObject("dgTemporadas.PictureCurrentRow"), System.Drawing.Image)
        Me.dgTemporadas.PictureFilterBar = CType(resources.GetObject("dgTemporadas.PictureFilterBar"), System.Drawing.Image)
        Me.dgTemporadas.PictureFooterRow = CType(resources.GetObject("dgTemporadas.PictureFooterRow"), System.Drawing.Image)
        Me.dgTemporadas.PictureHeaderRow = CType(resources.GetObject("dgTemporadas.PictureHeaderRow"), System.Drawing.Image)
        Me.dgTemporadas.PictureModifiedRow = CType(resources.GetObject("dgTemporadas.PictureModifiedRow"), System.Drawing.Image)
        Me.dgTemporadas.PictureStandardRow = CType(resources.GetObject("dgTemporadas.PictureStandardRow"), System.Drawing.Image)
        Me.dgTemporadas.PreviewInfo.AllowSizing = CType(resources.GetObject("dgTemporadas.PreviewInfo.AllowSizing"), Boolean)
        Me.dgTemporadas.PreviewInfo.Caption = resources.GetString("dgTemporadas.PreviewInfo.Caption")
        Me.dgTemporadas.PreviewInfo.Location = CType(resources.GetObject("dgTemporadas.PreviewInfo.Location"), System.Drawing.Point)
        Me.dgTemporadas.PreviewInfo.Size = CType(resources.GetObject("dgTemporadas.PreviewInfo.Size"), System.Drawing.Size)
        Me.dgTemporadas.PreviewInfo.ToolBars = CType(resources.GetObject("dgTemporadas.PreviewInfo.ToolBars"), Boolean)
        Me.dgTemporadas.PreviewInfo.UIStrings.Content = CType(resources.GetObject("dgTemporadas.PreviewInfo.UIStrings.Content"), String())
        Me.dgTemporadas.PreviewInfo.ZoomFactor = CType(resources.GetObject("dgTemporadas.PreviewInfo.ZoomFactor"), Double)
        Me.dgTemporadas.PrintInfo.MaxRowHeight = CType(resources.GetObject("dgTemporadas.PrintInfo.MaxRowHeight"), Integer)
        Me.dgTemporadas.PrintInfo.OwnerDrawPageFooter = CType(resources.GetObject("dgTemporadas.PrintInfo.OwnerDrawPageFooter"), Boolean)
        Me.dgTemporadas.PrintInfo.OwnerDrawPageHeader = CType(resources.GetObject("dgTemporadas.PrintInfo.OwnerDrawPageHeader"), Boolean)
        Me.dgTemporadas.PrintInfo.PageFooter = resources.GetString("dgTemporadas.PrintInfo.PageFooter")
        Me.dgTemporadas.PrintInfo.PageFooterHeight = CType(resources.GetObject("dgTemporadas.PrintInfo.PageFooterHeight"), Integer)
        Me.dgTemporadas.PrintInfo.PageHeader = resources.GetString("dgTemporadas.PrintInfo.PageHeader")
        Me.dgTemporadas.PrintInfo.PageHeaderHeight = CType(resources.GetObject("dgTemporadas.PrintInfo.PageHeaderHeight"), Integer)
        Me.dgTemporadas.PrintInfo.PrintHorizontalSplits = CType(resources.GetObject("dgTemporadas.PrintInfo.PrintHorizontalSplits"), Boolean)
        Me.dgTemporadas.PrintInfo.ProgressCaption = resources.GetString("dgTemporadas.PrintInfo.ProgressCaption")
        Me.dgTemporadas.PrintInfo.RepeatColumnFooters = CType(resources.GetObject("dgTemporadas.PrintInfo.RepeatColumnFooters"), Boolean)
        Me.dgTemporadas.PrintInfo.RepeatColumnHeaders = CType(resources.GetObject("dgTemporadas.PrintInfo.RepeatColumnHeaders"), Boolean)
        Me.dgTemporadas.PrintInfo.RepeatGridHeader = CType(resources.GetObject("dgTemporadas.PrintInfo.RepeatGridHeader"), Boolean)
        Me.dgTemporadas.PrintInfo.RepeatSplitHeaders = CType(resources.GetObject("dgTemporadas.PrintInfo.RepeatSplitHeaders"), Boolean)
        Me.dgTemporadas.PrintInfo.ShowOptionsDialog = CType(resources.GetObject("dgTemporadas.PrintInfo.ShowOptionsDialog"), Boolean)
        Me.dgTemporadas.PrintInfo.ShowProgressForm = CType(resources.GetObject("dgTemporadas.PrintInfo.ShowProgressForm"), Boolean)
        Me.dgTemporadas.PrintInfo.ShowSelection = CType(resources.GetObject("dgTemporadas.PrintInfo.ShowSelection"), Boolean)
        Me.dgTemporadas.PrintInfo.UseGridColors = CType(resources.GetObject("dgTemporadas.PrintInfo.UseGridColors"), Boolean)
        Me.dgTemporadas.RecordSelectors = CType(resources.GetObject("dgTemporadas.RecordSelectors"), Boolean)
        Me.dgTemporadas.RecordSelectorWidth = CType(resources.GetObject("dgTemporadas.RecordSelectorWidth"), Integer)
        Me.dgTemporadas.RightToLeft = CType(resources.GetObject("dgTemporadas.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.dgTemporadas.RowDivider.Color = CType(resources.GetObject("resource.Color"), System.Drawing.Color)
        Me.dgTemporadas.RowDivider.Style = CType(resources.GetObject("resource.Style"), C1.Win.C1TrueDBGrid.LineStyleEnum)
        Me.dgTemporadas.RowHeight = CType(resources.GetObject("dgTemporadas.RowHeight"), Integer)
        Me.dgTemporadas.RowSubDividerColor = CType(resources.GetObject("dgTemporadas.RowSubDividerColor"), System.Drawing.Color)
        Me.dgTemporadas.ScrollTips = CType(resources.GetObject("dgTemporadas.ScrollTips"), Boolean)
        Me.dgTemporadas.ScrollTrack = CType(resources.GetObject("dgTemporadas.ScrollTrack"), Boolean)
        Me.dgTemporadas.Size = CType(resources.GetObject("dgTemporadas.Size"), System.Drawing.Size)
        Me.dgTemporadas.SpringMode = CType(resources.GetObject("dgTemporadas.SpringMode"), Boolean)
        Me.dgTemporadas.TabAcrossSplits = CType(resources.GetObject("dgTemporadas.TabAcrossSplits"), Boolean)
        Me.dgTemporadas.TabIndex = CType(resources.GetObject("dgTemporadas.TabIndex"), Integer)
        Me.dgTemporadas.Text = resources.GetString("dgTemporadas.Text")
        Me.dgTemporadas.ViewCaptionWidth = CType(resources.GetObject("dgTemporadas.ViewCaptionWidth"), Integer)
        Me.dgTemporadas.ViewColumnWidth = CType(resources.GetObject("dgTemporadas.ViewColumnWidth"), Integer)
        Me.dgTemporadas.Visible = CType(resources.GetObject("dgTemporadas.Visible"), Boolean)
        Me.dgTemporadas.WrapCellPointer = CType(resources.GetObject("dgTemporadas.WrapCellPointer"), Boolean)
        Me.dgTemporadas.PropBag = resources.GetString("dgTemporadas.PropBag")
        '
        'frmElegirTemporadaAMostrar
        '
        Me.AccessibleDescription = resources.GetString("$this.AccessibleDescription")
        Me.AccessibleName = resources.GetString("$this.AccessibleName")
        Me.AutoScaleBaseSize = CType(resources.GetObject("$this.AutoScaleBaseSize"), System.Drawing.Size)
        Me.AutoScroll = CType(resources.GetObject("$this.AutoScroll"), Boolean)
        Me.AutoScrollMargin = CType(resources.GetObject("$this.AutoScrollMargin"), System.Drawing.Size)
        Me.AutoScrollMinSize = CType(resources.GetObject("$this.AutoScrollMinSize"), System.Drawing.Size)
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = CType(resources.GetObject("$this.ClientSize"), System.Drawing.Size)
        Me.Controls.Add(Me.dgTemporadas)
        Me.Controls.Add(Me.btnAceptar)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Enabled = CType(resources.GetObject("$this.Enabled"), Boolean)
        Me.Font = CType(resources.GetObject("$this.Font"), System.Drawing.Font)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.ImeMode = CType(resources.GetObject("$this.ImeMode"), System.Windows.Forms.ImeMode)
        Me.Location = CType(resources.GetObject("$this.Location"), System.Drawing.Point)
        Me.MaximumSize = CType(resources.GetObject("$this.MaximumSize"), System.Drawing.Size)
        Me.MinimumSize = CType(resources.GetObject("$this.MinimumSize"), System.Drawing.Size)
        Me.Name = "frmElegirTemporadaAMostrar"
        Me.RightToLeft = CType(resources.GetObject("$this.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.StartPosition = CType(resources.GetObject("$this.StartPosition"), System.Windows.Forms.FormStartPosition)
        Me.Text = resources.GetString("$this.Text")
        Me.Controls.SetChildIndex(Me.btnAceptar, 0)
        Me.Controls.SetChildIndex(Me.dgTemporadas, 0)
        CType(Me.dgTemporadas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "VARIABLES"

    Public aTemporadas As New ArrayList
    Private dvTemporadas As New DataView
    Private cmdSelect As New MySqlCommand
    Private daSinBuild As New MySqlDataAdapter

#End Region

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            CCN()
            ACN()
            ds.Tables(tablaTemporadas).Columns.Add(New DataColumn("ELEGIDO", GetType(Boolean)))
            ds.Tables(tablaTemporadas).Columns("ELEGIDO").DefaultValue = False
            IniciarDG()
            AutoSizeCC(dgTemporadas)
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub IniciarDG()

        Dim i As Integer
        Try
            cmdSelect.CommandText = "SELECT * FROM TEMPORADAS"
            daSinBuild.SelectCommand = cmdSelect
            cmdSelect.Connection = cnn
            ACN()
            daSinBuild.Fill(ds.Tables(tablaTemporadas))
            dvTemporadas = ds.Tables(tablaTemporadas).DefaultView
            dvTemporadas.Table.Columns("CENTRO").DefaultValue = "C" '!!!!!!!! 'nz(centro, empresaPorDefecto)
            dvTemporadas.Sort = "CODI"
            dgTemporadas.SetDataBinding(dvTemporadas, "")

            OcultarColumnasDG(dgTemporadas)
            i = 0
            dgTemporadas.AllowSort = False
            PPCol2("CODI", dgTemporadas, rm.GetString("TEMPORADA"), "", True, 90, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 90, i, False)
            i = i + 1
            PPCol2("DESCRI", dgTemporadas, rm.GetString("DESCRIPCION"), "", True, 200, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 200, i, False)
            i = i + 1
            PPCol2("ELEGIDO", dgTemporadas, rm.GetString("ESCOGER"), "", True, 60, _
                    False, C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox, False, 60, i, False, Nothing, True, 0, C1.Win.C1TrueDBGrid.AlignHorzEnum.Center)
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            Dim i As Integer

            For i = 0 To dvTemporadas.Count - 1
                If dgTemporadas(i, "ELEGIDO") = True Then
                    aTemporadas.Add(dgTemporadas(i, "CODI"))
                End If
            Next
            Close()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
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

End Class

