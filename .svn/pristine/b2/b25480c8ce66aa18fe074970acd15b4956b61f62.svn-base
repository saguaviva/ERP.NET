Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes



Public Class frmIncoterms
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
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents dgIncoterms2 As C1.Win.C1TrueDBGrid.C1TrueDBGrid

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmIncoterms))
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.dgIncoterms2 = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        CType(Me.dgIncoterms2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnTancar
        '
        Me.btnTancar.Location = CType(resources.GetObject("btnTancar.Location"), System.Drawing.Point)
        Me.btnTancar.Name = "btnTancar"
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = CType(resources.GetObject("btnBorrar.Location"), System.Drawing.Point)
        Me.btnBorrar.Name = "btnBorrar"
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = CType(resources.GetObject("btnNuevo.Location"), System.Drawing.Point)
        Me.btnNuevo.Name = "btnNuevo"
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = CType(resources.GetObject("btnUltimo.Location"), System.Drawing.Point)
        Me.btnUltimo.Name = "btnUltimo"
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = CType(resources.GetObject("btnActualizar.Location"), System.Drawing.Point)
        Me.btnActualizar.Name = "btnActualizar"
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = CType(resources.GetObject("btnPrimero.Location"), System.Drawing.Point)
        Me.btnPrimero.Name = "btnPrimero"
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = CType(resources.GetObject("btnAnterior.Location"), System.Drawing.Point)
        Me.btnAnterior.Name = "btnAnterior"
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = CType(resources.GetObject("btnSiguiente.Location"), System.Drawing.Point)
        Me.btnSiguiente.Name = "btnSiguiente"
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = CType(resources.GetObject("btnRecargar.Location"), System.Drawing.Point)
        Me.btnRecargar.Name = "btnRecargar"
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = CType(resources.GetObject("btnVerLista.Location"), System.Drawing.Point)
        Me.btnVerLista.Name = "btnVerLista"
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.Name = "cboSeleccionCentro"
        '
        'btnModificar
        '
        Me.btnModificar.Location = CType(resources.GetObject("btnModificar.Location"), System.Drawing.Point)
        Me.btnModificar.Name = "btnModificar"
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = CType(resources.GetObject("sbtipo.Location"), System.Drawing.Point)
        Me.sbtipo.Name = "sbtipo"
        Me.sbtipo.Text = resources.GetString("sbtipo.Text")
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
        'dgIncoterms2
        '
        Me.dgIncoterms2.AccessibleDescription = resources.GetString("dgIncoterms2.AccessibleDescription")
        Me.dgIncoterms2.AccessibleName = resources.GetString("dgIncoterms2.AccessibleName")
        Me.dgIncoterms2.AllowAddNew = CType(resources.GetObject("dgIncoterms2.AllowAddNew"), Boolean)
        Me.dgIncoterms2.AllowArrows = CType(resources.GetObject("dgIncoterms2.AllowArrows"), Boolean)
        Me.dgIncoterms2.AllowColMove = CType(resources.GetObject("dgIncoterms2.AllowColMove"), Boolean)
        Me.dgIncoterms2.AllowColSelect = CType(resources.GetObject("dgIncoterms2.AllowColSelect"), Boolean)
        Me.dgIncoterms2.AllowDelete = CType(resources.GetObject("dgIncoterms2.AllowDelete"), Boolean)
        Me.dgIncoterms2.AllowDrag = CType(resources.GetObject("dgIncoterms2.AllowDrag"), Boolean)
        Me.dgIncoterms2.AllowFilter = CType(resources.GetObject("dgIncoterms2.AllowFilter"), Boolean)
        Me.dgIncoterms2.AllowHorizontalSplit = CType(resources.GetObject("dgIncoterms2.AllowHorizontalSplit"), Boolean)
        Me.dgIncoterms2.AllowRowSelect = CType(resources.GetObject("dgIncoterms2.AllowRowSelect"), Boolean)
        Me.dgIncoterms2.AllowSort = CType(resources.GetObject("dgIncoterms2.AllowSort"), Boolean)
        Me.dgIncoterms2.AllowUpdate = CType(resources.GetObject("dgIncoterms2.AllowUpdate"), Boolean)
        Me.dgIncoterms2.AllowUpdateOnBlur = CType(resources.GetObject("dgIncoterms2.AllowUpdateOnBlur"), Boolean)
        Me.dgIncoterms2.AllowVerticalSplit = CType(resources.GetObject("dgIncoterms2.AllowVerticalSplit"), Boolean)
        Me.dgIncoterms2.AlternatingRows = CType(resources.GetObject("dgIncoterms2.AlternatingRows"), Boolean)
        Me.dgIncoterms2.Anchor = CType(resources.GetObject("dgIncoterms2.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.dgIncoterms2.BackgroundImage = CType(resources.GetObject("dgIncoterms2.BackgroundImage"), System.Drawing.Image)
        Me.dgIncoterms2.BorderStyle = CType(resources.GetObject("dgIncoterms2.BorderStyle"), System.Windows.Forms.BorderStyle)
        Me.dgIncoterms2.Caption = resources.GetString("dgIncoterms2.Caption")
        Me.dgIncoterms2.CaptionHeight = CType(resources.GetObject("dgIncoterms2.CaptionHeight"), Integer)
        Me.dgIncoterms2.CellTipsDelay = CType(resources.GetObject("dgIncoterms2.CellTipsDelay"), Integer)
        Me.dgIncoterms2.CellTipsWidth = CType(resources.GetObject("dgIncoterms2.CellTipsWidth"), Integer)
        Me.dgIncoterms2.ChildGrid = CType(resources.GetObject("dgIncoterms2.ChildGrid"), C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        Me.dgIncoterms2.CollapseColor = CType(resources.GetObject("dgIncoterms2.CollapseColor"), System.Drawing.Color)
        Me.dgIncoterms2.ColumnFooters = CType(resources.GetObject("dgIncoterms2.ColumnFooters"), Boolean)
        Me.dgIncoterms2.ColumnHeaders = CType(resources.GetObject("dgIncoterms2.ColumnHeaders"), Boolean)
        Me.dgIncoterms2.DefColWidth = CType(resources.GetObject("dgIncoterms2.DefColWidth"), Integer)
        Me.dgIncoterms2.Dock = CType(resources.GetObject("dgIncoterms2.Dock"), System.Windows.Forms.DockStyle)
        Me.dgIncoterms2.EditDropDown = CType(resources.GetObject("dgIncoterms2.EditDropDown"), Boolean)
        Me.dgIncoterms2.EmptyRows = CType(resources.GetObject("dgIncoterms2.EmptyRows"), Boolean)
        Me.dgIncoterms2.Enabled = CType(resources.GetObject("dgIncoterms2.Enabled"), Boolean)
        Me.dgIncoterms2.ExpandColor = CType(resources.GetObject("dgIncoterms2.ExpandColor"), System.Drawing.Color)
        Me.dgIncoterms2.ExposeCellMode = CType(resources.GetObject("dgIncoterms2.ExposeCellMode"), C1.Win.C1TrueDBGrid.ExposeCellModeEnum)
        Me.dgIncoterms2.ExtendRightColumn = CType(resources.GetObject("dgIncoterms2.ExtendRightColumn"), Boolean)
        Me.dgIncoterms2.FetchRowStyles = CType(resources.GetObject("dgIncoterms2.FetchRowStyles"), Boolean)
        Me.dgIncoterms2.FilterBar = CType(resources.GetObject("dgIncoterms2.FilterBar"), Boolean)
        Me.dgIncoterms2.FlatStyle = CType(resources.GetObject("dgIncoterms2.FlatStyle"), C1.Win.C1TrueDBGrid.FlatModeEnum)
        Me.dgIncoterms2.Font = CType(resources.GetObject("dgIncoterms2.Font"), System.Drawing.Font)
        Me.dgIncoterms2.GroupByAreaVisible = CType(resources.GetObject("dgIncoterms2.GroupByAreaVisible"), Boolean)
        Me.dgIncoterms2.GroupByCaption = resources.GetString("dgIncoterms2.GroupByCaption")
        Me.dgIncoterms2.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.dgIncoterms2.ImeMode = CType(resources.GetObject("dgIncoterms2.ImeMode"), System.Windows.Forms.ImeMode)
        Me.dgIncoterms2.LinesPerRow = CType(resources.GetObject("dgIncoterms2.LinesPerRow"), Integer)
        Me.dgIncoterms2.Location = CType(resources.GetObject("dgIncoterms2.Location"), System.Drawing.Point)
        Me.dgIncoterms2.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.dgIncoterms2.Name = "dgIncoterms2"
        Me.dgIncoterms2.PictureAddnewRow = CType(resources.GetObject("dgIncoterms2.PictureAddnewRow"), System.Drawing.Image)
        Me.dgIncoterms2.PictureCurrentRow = CType(resources.GetObject("dgIncoterms2.PictureCurrentRow"), System.Drawing.Image)
        Me.dgIncoterms2.PictureFilterBar = CType(resources.GetObject("dgIncoterms2.PictureFilterBar"), System.Drawing.Image)
        Me.dgIncoterms2.PictureFooterRow = CType(resources.GetObject("dgIncoterms2.PictureFooterRow"), System.Drawing.Image)
        Me.dgIncoterms2.PictureHeaderRow = CType(resources.GetObject("dgIncoterms2.PictureHeaderRow"), System.Drawing.Image)
        Me.dgIncoterms2.PictureModifiedRow = CType(resources.GetObject("dgIncoterms2.PictureModifiedRow"), System.Drawing.Image)
        Me.dgIncoterms2.PictureStandardRow = CType(resources.GetObject("dgIncoterms2.PictureStandardRow"), System.Drawing.Image)
        Me.dgIncoterms2.PreviewInfo.AllowSizing = CType(resources.GetObject("dgIncoterms2.PreviewInfo.AllowSizing"), Boolean)
        Me.dgIncoterms2.PreviewInfo.Caption = resources.GetString("dgIncoterms2.PreviewInfo.Caption")
        Me.dgIncoterms2.PreviewInfo.Location = CType(resources.GetObject("dgIncoterms2.PreviewInfo.Location"), System.Drawing.Point)
        Me.dgIncoterms2.PreviewInfo.Size = CType(resources.GetObject("dgIncoterms2.PreviewInfo.Size"), System.Drawing.Size)
        Me.dgIncoterms2.PreviewInfo.ToolBars = CType(resources.GetObject("dgIncoterms2.PreviewInfo.ToolBars"), Boolean)
        Me.dgIncoterms2.PreviewInfo.UIStrings.Content = CType(resources.GetObject("dgIncoterms2.PreviewInfo.UIStrings.Content"), String())
        Me.dgIncoterms2.PreviewInfo.ZoomFactor = CType(resources.GetObject("dgIncoterms2.PreviewInfo.ZoomFactor"), Double)
        Me.dgIncoterms2.PrintInfo.MaxRowHeight = CType(resources.GetObject("dgIncoterms2.PrintInfo.MaxRowHeight"), Integer)
        Me.dgIncoterms2.PrintInfo.OwnerDrawPageFooter = CType(resources.GetObject("dgIncoterms2.PrintInfo.OwnerDrawPageFooter"), Boolean)
        Me.dgIncoterms2.PrintInfo.OwnerDrawPageHeader = CType(resources.GetObject("dgIncoterms2.PrintInfo.OwnerDrawPageHeader"), Boolean)
        Me.dgIncoterms2.PrintInfo.PageFooter = resources.GetString("dgIncoterms2.PrintInfo.PageFooter")
        Me.dgIncoterms2.PrintInfo.PageFooterHeight = CType(resources.GetObject("dgIncoterms2.PrintInfo.PageFooterHeight"), Integer)
        Me.dgIncoterms2.PrintInfo.PageHeader = resources.GetString("dgIncoterms2.PrintInfo.PageHeader")
        Me.dgIncoterms2.PrintInfo.PageHeaderHeight = CType(resources.GetObject("dgIncoterms2.PrintInfo.PageHeaderHeight"), Integer)
        Me.dgIncoterms2.PrintInfo.PrintHorizontalSplits = CType(resources.GetObject("dgIncoterms2.PrintInfo.PrintHorizontalSplits"), Boolean)
        Me.dgIncoterms2.PrintInfo.ProgressCaption = resources.GetString("dgIncoterms2.PrintInfo.ProgressCaption")
        Me.dgIncoterms2.PrintInfo.RepeatColumnFooters = CType(resources.GetObject("dgIncoterms2.PrintInfo.RepeatColumnFooters"), Boolean)
        Me.dgIncoterms2.PrintInfo.RepeatColumnHeaders = CType(resources.GetObject("dgIncoterms2.PrintInfo.RepeatColumnHeaders"), Boolean)
        Me.dgIncoterms2.PrintInfo.RepeatGridHeader = CType(resources.GetObject("dgIncoterms2.PrintInfo.RepeatGridHeader"), Boolean)
        Me.dgIncoterms2.PrintInfo.RepeatSplitHeaders = CType(resources.GetObject("dgIncoterms2.PrintInfo.RepeatSplitHeaders"), Boolean)
        Me.dgIncoterms2.PrintInfo.ShowOptionsDialog = CType(resources.GetObject("dgIncoterms2.PrintInfo.ShowOptionsDialog"), Boolean)
        Me.dgIncoterms2.PrintInfo.ShowProgressForm = CType(resources.GetObject("dgIncoterms2.PrintInfo.ShowProgressForm"), Boolean)
        Me.dgIncoterms2.PrintInfo.ShowSelection = CType(resources.GetObject("dgIncoterms2.PrintInfo.ShowSelection"), Boolean)
        Me.dgIncoterms2.PrintInfo.UseGridColors = CType(resources.GetObject("dgIncoterms2.PrintInfo.UseGridColors"), Boolean)
        Me.dgIncoterms2.RecordSelectors = CType(resources.GetObject("dgIncoterms2.RecordSelectors"), Boolean)
        Me.dgIncoterms2.RecordSelectorWidth = CType(resources.GetObject("dgIncoterms2.RecordSelectorWidth"), Integer)
        Me.dgIncoterms2.RightToLeft = CType(resources.GetObject("dgIncoterms2.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.dgIncoterms2.RowDivider.Color = CType(resources.GetObject("resource.Color"), System.Drawing.Color)
        Me.dgIncoterms2.RowDivider.Style = CType(resources.GetObject("resource.Style"), C1.Win.C1TrueDBGrid.LineStyleEnum)
        Me.dgIncoterms2.RowHeight = CType(resources.GetObject("dgIncoterms2.RowHeight"), Integer)
        Me.dgIncoterms2.RowSubDividerColor = CType(resources.GetObject("dgIncoterms2.RowSubDividerColor"), System.Drawing.Color)
        Me.dgIncoterms2.ScrollTips = CType(resources.GetObject("dgIncoterms2.ScrollTips"), Boolean)
        Me.dgIncoterms2.ScrollTrack = CType(resources.GetObject("dgIncoterms2.ScrollTrack"), Boolean)
        Me.dgIncoterms2.Size = CType(resources.GetObject("dgIncoterms2.Size"), System.Drawing.Size)
        Me.dgIncoterms2.SpringMode = CType(resources.GetObject("dgIncoterms2.SpringMode"), Boolean)
        Me.dgIncoterms2.TabAcrossSplits = CType(resources.GetObject("dgIncoterms2.TabAcrossSplits"), Boolean)
        Me.dgIncoterms2.TabIndex = CType(resources.GetObject("dgIncoterms2.TabIndex"), Integer)
        Me.dgIncoterms2.Text = resources.GetString("dgIncoterms2.Text")
        Me.dgIncoterms2.ViewCaptionWidth = CType(resources.GetObject("dgIncoterms2.ViewCaptionWidth"), Integer)
        Me.dgIncoterms2.ViewColumnWidth = CType(resources.GetObject("dgIncoterms2.ViewColumnWidth"), Integer)
        Me.dgIncoterms2.Visible = CType(resources.GetObject("dgIncoterms2.Visible"), Boolean)
        Me.dgIncoterms2.WrapCellPointer = CType(resources.GetObject("dgIncoterms2.WrapCellPointer"), Boolean)
        Me.dgIncoterms2.PropBag = resources.GetString("dgIncoterms2.PropBag")
        '
        'frmIncoterms
        '
        Me.AccessibleDescription = resources.GetString("$this.AccessibleDescription")
        Me.AccessibleName = resources.GetString("$this.AccessibleName")
        Me.AutoScaleBaseSize = CType(resources.GetObject("$this.AutoScaleBaseSize"), System.Drawing.Size)
        Me.AutoScroll = CType(resources.GetObject("$this.AutoScroll"), Boolean)
        Me.AutoScrollMargin = CType(resources.GetObject("$this.AutoScrollMargin"), System.Drawing.Size)
        Me.AutoScrollMinSize = CType(resources.GetObject("$this.AutoScrollMinSize"), System.Drawing.Size)
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = CType(resources.GetObject("$this.ClientSize"), System.Drawing.Size)
        Me.Controls.Add(Me.dgIncoterms2)
        Me.Controls.Add(Me.btnAceptar)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Enabled = CType(resources.GetObject("$this.Enabled"), Boolean)
        Me.Font = CType(resources.GetObject("$this.Font"), System.Drawing.Font)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.ImeMode = CType(resources.GetObject("$this.ImeMode"), System.Windows.Forms.ImeMode)
        Me.KeyPreview = True
        Me.Location = CType(resources.GetObject("$this.Location"), System.Drawing.Point)
        Me.MaximumSize = CType(resources.GetObject("$this.MaximumSize"), System.Drawing.Size)
        Me.MinimumSize = CType(resources.GetObject("$this.MinimumSize"), System.Drawing.Size)
        Me.Name = "frmIncoterms"
        Me.RightToLeft = CType(resources.GetObject("$this.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.StartPosition = CType(resources.GetObject("$this.StartPosition"), System.Windows.Forms.FormStartPosition)
        Me.Text = resources.GetString("$this.Text")
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
        Me.Controls.SetChildIndex(Me.dgIncoterms2, 0)
        CType(Me.dgIncoterms2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmIncoterms
    Public Shared Function GetInstance() As frmIncoterms
        If frmChildForm Is Nothing Then
            frmChildForm = New frmIncoterms
        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Public incotermActual As clsIncoterm

#End Region

#Region "INICIALIZAR"

    Private Sub HacerBindings()
        Dim i As Integer
        Try

            dgIncoterms2.SetDataBinding(incotermActual.dvForm, "")
            OcultarColumnasDG(dgIncoterms2)
            i = 0
            PPCol2("NOMBRE", dgIncoterms2, rm.GetString("NOMBRE"), "", True, 200, False, _
                  C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 200, i, True)
            i = i + 1
            PPCol2("DESCRI", dgIncoterms2, rm.GetString("DESCRIPCION"), "", True, 200, False, _
                                          C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 200, i, True)


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True
            tabla = "incoterm"
            incotermActual = New clsIncoterm(ds.Tables(tabla), "C", BindingContext)
            HacerBindings()
            'incotermActual.tabla.AcceptChanges()
            'PonerModificables(soloLectura)
            ' PSBTIPO(incotermActual.centro)
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub

#End Region

#Region "MODIFICAR DB"

    'Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnBorrar.Click
    '    Try
    '        If MessageBox.Show(rm.GetString("BorrarFormaPago"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
    '            editando = True
    '            incotermActual.borrar()
    '            editando = False
    '        End If

    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN()
    '    End Try
    'End Sub
    'Friend Overrides Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnActualizar.Click
    '    Try
    '        cargando = True
    '        incotermActual.ActualizarOrigen()
    '        PSBTIPO(incotermActual.centro) : cargando = False

    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN()
    '    End Try
    'End Sub

#End Region

#Region "ESPECÍFICO"

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        MakeGradient()
    End Sub
    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        MakeGradient()
    End Sub
    Private Sub MakeGradient()
        Dim objBrush As New Drawing2D.LinearGradientBrush(Me.DisplayRectangle, Color.Blue, Color.AliceBlue, Drawing2D.LinearGradientMode.Vertical)
        Dim objGraphics As Graphics = Me.CreateGraphics()
        objGraphics.FillRectangle(objBrush, Me.DisplayRectangle)
        objBrush.Dispose()
        objGraphics.Dispose()
    End Sub

#End Region

#Region "IMPRESIÓN"

#End Region

#Region "COMUNES"

    Private Sub btnVerLista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerLista.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmPagoLista = frmPagoLista.GetInstance(esListado)
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


#End Region

#Region "DESPLAZARSE"

    'Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)  'Handles btnPrimero.Click
    '    Try
    '        cargando = True
    '        incotermActual.PrimeroReg() : ActualizarOrigen()
    '        PSBTIPO(incotermActual.centro) : cargando = False

    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN()
    '    End Try
    'End Sub
    'Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUltimo.Click
    '    Try
    '        cargando = True
    '        incotermActual.UltimoReg() : ActualizarOrigen()
    '        PSBTIPO(incotermActual.centro) : cargando = False

    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN()
    '    End Try
    'End Sub
    'Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAnterior.Click
    '    Try
    '        cargando = True
    '        incotermActual.AnteriorReg() : ActualizarOrigen()
    '        PSBTIPO(incotermActual.centro) : cargando = False

    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN()
    '    End Try
    'End Sub
    'Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSiguiente.Click
    '    Try
    '        cargando = True
    '        incotermActual.SiguienteReg() : ActualizarOrigen()
    '        PSBTIPO(incotermActual.centro) : cargando = False

    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN()
    '    End Try
    'End Sub

#End Region

#Region "SELECCIÓN REGISTRO"

#End Region

#Region "ELECCIONES"

#End Region

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        incotermActual.ActualizarOrigen()
    End Sub
End Class
