Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes

Public Class frmElegirPAFP
    Inherits aura2k3.frmBase

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

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
    Friend WithEvents lstPAFP As System.Windows.Forms.ListBox
    Friend WithEvents lstCompraVenta As System.Windows.Forms.ListBox
    Friend WithEvents btnAbrirListado As C1.Win.C1Input.C1Button
    Friend WithEvents btnCancelar As C1.Win.C1Input.C1Button
    Friend WithEvents lblEmpresa As System.Windows.Forms.Label
    Friend WithEvents lblDocumento As System.Windows.Forms.Label
    Friend WithEvents lblTipo As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmElegirPAFP))
        Me.lstPAFP = New System.Windows.Forms.ListBox
        Me.lstCompraVenta = New System.Windows.Forms.ListBox
        Me.btnAbrirListado = New C1.Win.C1Input.C1Button
        Me.btnCancelar = New C1.Win.C1Input.C1Button
        Me.lblEmpresa = New System.Windows.Forms.Label
        Me.lblDocumento = New System.Windows.Forms.Label
        Me.lblTipo = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = CType(resources.GetObject("btnAnterior.Location"), System.Drawing.Point)
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.Size = CType(resources.GetObject("btnAnterior.Size"), System.Drawing.Size)
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
        Me.btnRecargar.Size = CType(resources.GetObject("btnRecargar.Size"), System.Drawing.Size)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = CType(resources.GetObject("btnUltimo.Location"), System.Drawing.Point)
        Me.btnUltimo.Name = "btnUltimo"
        '
        'sbtipo
        '
        Me.sbtipo.Location = CType(resources.GetObject("sbtipo.Location"), System.Drawing.Point)
        Me.sbtipo.Name = "sbtipo"
        Me.sbtipo.Size = CType(resources.GetObject("sbtipo.Size"), System.Drawing.Size)
        '
        'btnModificar
        '
        Me.btnModificar.Location = CType(resources.GetObject("btnModificar.Location"), System.Drawing.Point)
        Me.btnModificar.Name = "btnModificar"
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = CType(resources.GetObject("btnBorrar.Location"), System.Drawing.Point)
        Me.btnBorrar.Name = "btnBorrar"
        Me.btnBorrar.Size = CType(resources.GetObject("btnBorrar.Size"), System.Drawing.Size)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = CType(resources.GetObject("btnNuevo.Location"), System.Drawing.Point)
        Me.btnNuevo.Name = "btnNuevo"
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = CType(resources.GetObject("btnActualizar.Location"), System.Drawing.Point)
        Me.btnActualizar.Name = "btnActualizar"
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
        'btnTancar
        '
        Me.btnTancar.Location = CType(resources.GetObject("btnTancar.Location"), System.Drawing.Point)
        Me.btnTancar.Name = "btnTancar"
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = CType(resources.GetObject("btnPrimero.Location"), System.Drawing.Point)
        Me.btnPrimero.Name = "btnPrimero"
        '
        'lstPAFP
        '
        Me.lstPAFP.AccessibleDescription = resources.GetString("lstPAFP.AccessibleDescription")
        Me.lstPAFP.AccessibleName = resources.GetString("lstPAFP.AccessibleName")
        Me.lstPAFP.Anchor = CType(resources.GetObject("lstPAFP.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.lstPAFP.BackgroundImage = CType(resources.GetObject("lstPAFP.BackgroundImage"), System.Drawing.Image)
        Me.lstPAFP.ColumnWidth = CType(resources.GetObject("lstPAFP.ColumnWidth"), Integer)
        Me.lstPAFP.Dock = CType(resources.GetObject("lstPAFP.Dock"), System.Windows.Forms.DockStyle)
        Me.lstPAFP.Enabled = CType(resources.GetObject("lstPAFP.Enabled"), Boolean)
        Me.lstPAFP.Font = CType(resources.GetObject("lstPAFP.Font"), System.Drawing.Font)
        Me.lstPAFP.HorizontalExtent = CType(resources.GetObject("lstPAFP.HorizontalExtent"), Integer)
        Me.lstPAFP.HorizontalScrollbar = CType(resources.GetObject("lstPAFP.HorizontalScrollbar"), Boolean)
        Me.lstPAFP.ImeMode = CType(resources.GetObject("lstPAFP.ImeMode"), System.Windows.Forms.ImeMode)
        Me.lstPAFP.IntegralHeight = CType(resources.GetObject("lstPAFP.IntegralHeight"), Boolean)
        Me.lstPAFP.ItemHeight = CType(resources.GetObject("lstPAFP.ItemHeight"), Integer)
        Me.lstPAFP.Items.AddRange(New Object() {resources.GetString("lstPAFP.Items"), resources.GetString("lstPAFP.Items1"), resources.GetString("lstPAFP.Items2"), resources.GetString("lstPAFP.Items3")})
        Me.lstPAFP.Location = CType(resources.GetObject("lstPAFP.Location"), System.Drawing.Point)
        Me.lstPAFP.Name = "lstPAFP"
        Me.lstPAFP.RightToLeft = CType(resources.GetObject("lstPAFP.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.lstPAFP.ScrollAlwaysVisible = CType(resources.GetObject("lstPAFP.ScrollAlwaysVisible"), Boolean)
        Me.lstPAFP.Size = CType(resources.GetObject("lstPAFP.Size"), System.Drawing.Size)
        Me.lstPAFP.TabIndex = CType(resources.GetObject("lstPAFP.TabIndex"), Integer)
        Me.lstPAFP.Visible = CType(resources.GetObject("lstPAFP.Visible"), Boolean)
        '
        'lstCompraVenta
        '
        Me.lstCompraVenta.AccessibleDescription = resources.GetString("lstCompraVenta.AccessibleDescription")
        Me.lstCompraVenta.AccessibleName = resources.GetString("lstCompraVenta.AccessibleName")
        Me.lstCompraVenta.Anchor = CType(resources.GetObject("lstCompraVenta.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.lstCompraVenta.BackgroundImage = CType(resources.GetObject("lstCompraVenta.BackgroundImage"), System.Drawing.Image)
        Me.lstCompraVenta.ColumnWidth = CType(resources.GetObject("lstCompraVenta.ColumnWidth"), Integer)
        Me.lstCompraVenta.Dock = CType(resources.GetObject("lstCompraVenta.Dock"), System.Windows.Forms.DockStyle)
        Me.lstCompraVenta.Enabled = CType(resources.GetObject("lstCompraVenta.Enabled"), Boolean)
        Me.lstCompraVenta.Font = CType(resources.GetObject("lstCompraVenta.Font"), System.Drawing.Font)
        Me.lstCompraVenta.HorizontalExtent = CType(resources.GetObject("lstCompraVenta.HorizontalExtent"), Integer)
        Me.lstCompraVenta.HorizontalScrollbar = CType(resources.GetObject("lstCompraVenta.HorizontalScrollbar"), Boolean)
        Me.lstCompraVenta.ImeMode = CType(resources.GetObject("lstCompraVenta.ImeMode"), System.Windows.Forms.ImeMode)
        Me.lstCompraVenta.IntegralHeight = CType(resources.GetObject("lstCompraVenta.IntegralHeight"), Boolean)
        Me.lstCompraVenta.ItemHeight = CType(resources.GetObject("lstCompraVenta.ItemHeight"), Integer)
        Me.lstCompraVenta.Items.AddRange(New Object() {resources.GetString("lstCompraVenta.Items"), resources.GetString("lstCompraVenta.Items1"), resources.GetString("lstCompraVenta.Items2"), resources.GetString("lstCompraVenta.Items3")})
        Me.lstCompraVenta.Location = CType(resources.GetObject("lstCompraVenta.Location"), System.Drawing.Point)
        Me.lstCompraVenta.Name = "lstCompraVenta"
        Me.lstCompraVenta.RightToLeft = CType(resources.GetObject("lstCompraVenta.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.lstCompraVenta.ScrollAlwaysVisible = CType(resources.GetObject("lstCompraVenta.ScrollAlwaysVisible"), Boolean)
        Me.lstCompraVenta.Size = CType(resources.GetObject("lstCompraVenta.Size"), System.Drawing.Size)
        Me.lstCompraVenta.TabIndex = CType(resources.GetObject("lstCompraVenta.TabIndex"), Integer)
        Me.lstCompraVenta.Visible = CType(resources.GetObject("lstCompraVenta.Visible"), Boolean)
        '
        'btnAbrirListado
        '
        Me.btnAbrirListado.AccessibleDescription = resources.GetString("btnAbrirListado.AccessibleDescription")
        Me.btnAbrirListado.AccessibleName = resources.GetString("btnAbrirListado.AccessibleName")
        Me.btnAbrirListado.Anchor = CType(resources.GetObject("btnAbrirListado.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.btnAbrirListado.BackgroundImage = CType(resources.GetObject("btnAbrirListado.BackgroundImage"), System.Drawing.Image)
        Me.btnAbrirListado.Dock = CType(resources.GetObject("btnAbrirListado.Dock"), System.Windows.Forms.DockStyle)
        Me.btnAbrirListado.Enabled = CType(resources.GetObject("btnAbrirListado.Enabled"), Boolean)
        Me.btnAbrirListado.FlatStyle = CType(resources.GetObject("btnAbrirListado.FlatStyle"), System.Windows.Forms.FlatStyle)
        Me.btnAbrirListado.Font = CType(resources.GetObject("btnAbrirListado.Font"), System.Drawing.Font)
        Me.btnAbrirListado.Image = CType(resources.GetObject("btnAbrirListado.Image"), System.Drawing.Image)
        Me.btnAbrirListado.ImageAlign = CType(resources.GetObject("btnAbrirListado.ImageAlign"), System.Drawing.ContentAlignment)
        Me.btnAbrirListado.ImageIndex = CType(resources.GetObject("btnAbrirListado.ImageIndex"), Integer)
        Me.btnAbrirListado.ImeMode = CType(resources.GetObject("btnAbrirListado.ImeMode"), System.Windows.Forms.ImeMode)
        Me.btnAbrirListado.Location = CType(resources.GetObject("btnAbrirListado.Location"), System.Drawing.Point)
        Me.btnAbrirListado.Name = "btnAbrirListado"
        Me.btnAbrirListado.RightToLeft = CType(resources.GetObject("btnAbrirListado.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.btnAbrirListado.Size = CType(resources.GetObject("btnAbrirListado.Size"), System.Drawing.Size)
        Me.btnAbrirListado.TabIndex = CType(resources.GetObject("btnAbrirListado.TabIndex"), Integer)
        Me.btnAbrirListado.Text = resources.GetString("btnAbrirListado.Text")
        Me.btnAbrirListado.TextAlign = CType(resources.GetObject("btnAbrirListado.TextAlign"), System.Drawing.ContentAlignment)
        Me.btnAbrirListado.Visible = CType(resources.GetObject("btnAbrirListado.Visible"), Boolean)
        '
        'btnCancelar
        '
        Me.btnCancelar.AccessibleDescription = resources.GetString("btnCancelar.AccessibleDescription")
        Me.btnCancelar.AccessibleName = resources.GetString("btnCancelar.AccessibleName")
        Me.btnCancelar.Anchor = CType(resources.GetObject("btnCancelar.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.BackgroundImage = CType(resources.GetObject("btnCancelar.BackgroundImage"), System.Drawing.Image)
        Me.btnCancelar.Dock = CType(resources.GetObject("btnCancelar.Dock"), System.Windows.Forms.DockStyle)
        Me.btnCancelar.Enabled = CType(resources.GetObject("btnCancelar.Enabled"), Boolean)
        Me.btnCancelar.FlatStyle = CType(resources.GetObject("btnCancelar.FlatStyle"), System.Windows.Forms.FlatStyle)
        Me.btnCancelar.Font = CType(resources.GetObject("btnCancelar.Font"), System.Drawing.Font)
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.ImageAlign = CType(resources.GetObject("btnCancelar.ImageAlign"), System.Drawing.ContentAlignment)
        Me.btnCancelar.ImageIndex = CType(resources.GetObject("btnCancelar.ImageIndex"), Integer)
        Me.btnCancelar.ImeMode = CType(resources.GetObject("btnCancelar.ImeMode"), System.Windows.Forms.ImeMode)
        Me.btnCancelar.Location = CType(resources.GetObject("btnCancelar.Location"), System.Drawing.Point)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.RightToLeft = CType(resources.GetObject("btnCancelar.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.btnCancelar.Size = CType(resources.GetObject("btnCancelar.Size"), System.Drawing.Size)
        Me.btnCancelar.TabIndex = CType(resources.GetObject("btnCancelar.TabIndex"), Integer)
        Me.btnCancelar.Text = resources.GetString("btnCancelar.Text")
        Me.btnCancelar.TextAlign = CType(resources.GetObject("btnCancelar.TextAlign"), System.Drawing.ContentAlignment)
        Me.btnCancelar.Visible = CType(resources.GetObject("btnCancelar.Visible"), Boolean)
        '
        'lblEmpresa
        '
        Me.lblEmpresa.AccessibleDescription = resources.GetString("lblEmpresa.AccessibleDescription")
        Me.lblEmpresa.AccessibleName = resources.GetString("lblEmpresa.AccessibleName")
        Me.lblEmpresa.Anchor = CType(resources.GetObject("lblEmpresa.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.lblEmpresa.AutoSize = CType(resources.GetObject("lblEmpresa.AutoSize"), Boolean)
        Me.lblEmpresa.Dock = CType(resources.GetObject("lblEmpresa.Dock"), System.Windows.Forms.DockStyle)
        Me.lblEmpresa.Enabled = CType(resources.GetObject("lblEmpresa.Enabled"), Boolean)
        Me.lblEmpresa.Font = CType(resources.GetObject("lblEmpresa.Font"), System.Drawing.Font)
        Me.lblEmpresa.Image = CType(resources.GetObject("lblEmpresa.Image"), System.Drawing.Image)
        Me.lblEmpresa.ImageAlign = CType(resources.GetObject("lblEmpresa.ImageAlign"), System.Drawing.ContentAlignment)
        Me.lblEmpresa.ImageIndex = CType(resources.GetObject("lblEmpresa.ImageIndex"), Integer)
        Me.lblEmpresa.ImeMode = CType(resources.GetObject("lblEmpresa.ImeMode"), System.Windows.Forms.ImeMode)
        Me.lblEmpresa.Location = CType(resources.GetObject("lblEmpresa.Location"), System.Drawing.Point)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.RightToLeft = CType(resources.GetObject("lblEmpresa.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.lblEmpresa.Size = CType(resources.GetObject("lblEmpresa.Size"), System.Drawing.Size)
        Me.lblEmpresa.TabIndex = CType(resources.GetObject("lblEmpresa.TabIndex"), Integer)
        Me.lblEmpresa.Text = resources.GetString("lblEmpresa.Text")
        Me.lblEmpresa.TextAlign = CType(resources.GetObject("lblEmpresa.TextAlign"), System.Drawing.ContentAlignment)
        Me.lblEmpresa.Visible = CType(resources.GetObject("lblEmpresa.Visible"), Boolean)
        '
        'lblDocumento
        '
        Me.lblDocumento.AccessibleDescription = resources.GetString("lblDocumento.AccessibleDescription")
        Me.lblDocumento.AccessibleName = resources.GetString("lblDocumento.AccessibleName")
        Me.lblDocumento.Anchor = CType(resources.GetObject("lblDocumento.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.lblDocumento.AutoSize = CType(resources.GetObject("lblDocumento.AutoSize"), Boolean)
        Me.lblDocumento.Dock = CType(resources.GetObject("lblDocumento.Dock"), System.Windows.Forms.DockStyle)
        Me.lblDocumento.Enabled = CType(resources.GetObject("lblDocumento.Enabled"), Boolean)
        Me.lblDocumento.Font = CType(resources.GetObject("lblDocumento.Font"), System.Drawing.Font)
        Me.lblDocumento.Image = CType(resources.GetObject("lblDocumento.Image"), System.Drawing.Image)
        Me.lblDocumento.ImageAlign = CType(resources.GetObject("lblDocumento.ImageAlign"), System.Drawing.ContentAlignment)
        Me.lblDocumento.ImageIndex = CType(resources.GetObject("lblDocumento.ImageIndex"), Integer)
        Me.lblDocumento.ImeMode = CType(resources.GetObject("lblDocumento.ImeMode"), System.Windows.Forms.ImeMode)
        Me.lblDocumento.Location = CType(resources.GetObject("lblDocumento.Location"), System.Drawing.Point)
        Me.lblDocumento.Name = "lblDocumento"
        Me.lblDocumento.RightToLeft = CType(resources.GetObject("lblDocumento.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.lblDocumento.Size = CType(resources.GetObject("lblDocumento.Size"), System.Drawing.Size)
        Me.lblDocumento.TabIndex = CType(resources.GetObject("lblDocumento.TabIndex"), Integer)
        Me.lblDocumento.Text = resources.GetString("lblDocumento.Text")
        Me.lblDocumento.TextAlign = CType(resources.GetObject("lblDocumento.TextAlign"), System.Drawing.ContentAlignment)
        Me.lblDocumento.Visible = CType(resources.GetObject("lblDocumento.Visible"), Boolean)
        '
        'lblTipo
        '
        Me.lblTipo.AccessibleDescription = resources.GetString("lblTipo.AccessibleDescription")
        Me.lblTipo.AccessibleName = resources.GetString("lblTipo.AccessibleName")
        Me.lblTipo.Anchor = CType(resources.GetObject("lblTipo.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.lblTipo.AutoSize = CType(resources.GetObject("lblTipo.AutoSize"), Boolean)
        Me.lblTipo.Dock = CType(resources.GetObject("lblTipo.Dock"), System.Windows.Forms.DockStyle)
        Me.lblTipo.Enabled = CType(resources.GetObject("lblTipo.Enabled"), Boolean)
        Me.lblTipo.Font = CType(resources.GetObject("lblTipo.Font"), System.Drawing.Font)
        Me.lblTipo.Image = CType(resources.GetObject("lblTipo.Image"), System.Drawing.Image)
        Me.lblTipo.ImageAlign = CType(resources.GetObject("lblTipo.ImageAlign"), System.Drawing.ContentAlignment)
        Me.lblTipo.ImageIndex = CType(resources.GetObject("lblTipo.ImageIndex"), Integer)
        Me.lblTipo.ImeMode = CType(resources.GetObject("lblTipo.ImeMode"), System.Windows.Forms.ImeMode)
        Me.lblTipo.Location = CType(resources.GetObject("lblTipo.Location"), System.Drawing.Point)
        Me.lblTipo.Name = "lblTipo"
        Me.lblTipo.RightToLeft = CType(resources.GetObject("lblTipo.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.lblTipo.Size = CType(resources.GetObject("lblTipo.Size"), System.Drawing.Size)
        Me.lblTipo.TabIndex = CType(resources.GetObject("lblTipo.TabIndex"), Integer)
        Me.lblTipo.Text = resources.GetString("lblTipo.Text")
        Me.lblTipo.TextAlign = CType(resources.GetObject("lblTipo.TextAlign"), System.Drawing.ContentAlignment)
        Me.lblTipo.Visible = CType(resources.GetObject("lblTipo.Visible"), Boolean)
        '
        'frmElegirPAFP
        '
        Me.AccessibleDescription = resources.GetString("$this.AccessibleDescription")
        Me.AccessibleName = resources.GetString("$this.AccessibleName")
        Me.AutoScaleBaseSize = CType(resources.GetObject("$this.AutoScaleBaseSize"), System.Drawing.Size)
        Me.AutoScroll = CType(resources.GetObject("$this.AutoScroll"), Boolean)
        Me.AutoScrollMargin = CType(resources.GetObject("$this.AutoScrollMargin"), System.Drawing.Size)
        Me.AutoScrollMinSize = CType(resources.GetObject("$this.AutoScrollMinSize"), System.Drawing.Size)
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.CancelButton = Nothing
        Me.ClientSize = CType(resources.GetObject("$this.ClientSize"), System.Drawing.Size)
        Me.Controls.Add(Me.lblTipo)
        Me.Controls.Add(Me.lblDocumento)
        Me.Controls.Add(Me.lblEmpresa)
        Me.Controls.Add(Me.lstCompraVenta)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAbrirListado)
        Me.Controls.Add(Me.lstPAFP)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Enabled = CType(resources.GetObject("$this.Enabled"), Boolean)
        Me.Font = CType(resources.GetObject("$this.Font"), System.Drawing.Font)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.ImeMode = CType(resources.GetObject("$this.ImeMode"), System.Windows.Forms.ImeMode)
        Me.Location = CType(resources.GetObject("$this.Location"), System.Drawing.Point)
        Me.MaximumSize = CType(resources.GetObject("$this.MaximumSize"), System.Drawing.Size)
        Me.MinimumSize = CType(resources.GetObject("$this.MinimumSize"), System.Drawing.Size)
        Me.Name = "frmElegirPAFP"
        Me.RightToLeft = CType(resources.GetObject("$this.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.StartPosition = CType(resources.GetObject("$this.StartPosition"), System.Windows.Forms.FormStartPosition)
        Me.Text = resources.GetString("$this.Text")
        Me.Controls.SetChildIndex(Me.lstPAFP, 0)
        Me.Controls.SetChildIndex(Me.btnAbrirListado, 0)
        Me.Controls.SetChildIndex(Me.btnCancelar, 0)
        Me.Controls.SetChildIndex(Me.lstCompraVenta, 0)
        Me.Controls.SetChildIndex(Me.lblEmpresa, 0)
        Me.Controls.SetChildIndex(Me.lblDocumento, 0)
        Me.Controls.SetChildIndex(Me.lblTipo, 0)
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
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmElegirPAFP
    Public Shared Function GetInstance() As frmElegirPAFP
        If frmChildForm Is Nothing Then
            frmChildForm = New frmElegirPAFP
        End If
        Return frmChildForm
    End Function

    Private filial As String = empresaPorDefecto

    Private Sub ColocarCBOCENTRO()
        Dim p As Point

        p.X = lstCompraVenta.Left
        p.Y = lstCompraVenta.Top - 40

        cboSeleccionCentro.Visible = True
        cboSeleccionCentro.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom) Or (System.Windows.Forms.AnchorStyles.Right)), System.Windows.Forms.AnchorStyles)
        cboSeleccionCentro.Enabled = True
        cboSeleccionCentro.Show()
        cboSeleccionCentro.BringToFront()
        cboSeleccionCentro.Location = p
    End Sub
    Private Sub cboSeleccion_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSeleccionCentro.SelectionChangeCommitted
        Try
            cargando = True
            If cboSeleccionCentro.SelectedValue <> "ZZ" Then
                filial = cboSeleccionCentro.SelectedValue
                lstCompraVenta.Enabled = True
            End If
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnAbrirListado_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbrirListado.Click
        Dim f As frmPAFLista
        Try

            '"COMPRA", "VENTA", "ORDRES FABRICACIÓ MOSTRES", "ORDRES FABRICACIÓ MODELS"})
            If lstPAFP.Enabled = True AndAlso lstPAFP.SelectedIndices.Count <> 0 Then
                Cursor = Cursors.WaitCursor
                Select Case lstCompraVenta.SelectedIndex
                    Case 0
                        'Este es caso de las compras
                        Select Case lstPAFP.SelectedIndex
                            Case 0
                                'Pedido compra
                                f = frmPAFLista.GetInstance("T", Pedido, filial, tablaCompras)
                            Case 1
                                f = frmPAFLista.GetInstance("M", Albaran, filial, tablaCompras)
                            Case 2
                                'Facturas compra no existe
                                'f = frmPAFLista.GetInstance("M", Factura, filial, tablaCompras)
                            Case 3
                                'Facturas proforma compra no existe
                                'f = frmPAFLista.GetInstance("T", Proforma, filial, tablaCompras)
                        End Select
                    Case 1
                        'VENTAS
                        Select Case lstPAFP.SelectedIndex
                            Case 0
                                f = frmPAFLista.GetInstance("T", Pedido, filial, tablaVentas)
                            Case 1
                                f = frmPAFLista.GetInstance("T", Albaran, filial, tablaVentas)
                            Case 2
                                f = frmPAFLista.GetInstance("T", Factura, filial, tablaVentas)
                            Case 3
                                f = frmPAFLista.GetInstance("T", Proforma, filial, tablaVentas)
                        End Select
                    Case 2
                        'Ordenes fab muestras
                        f = frmPAFLista.GetInstance(OrdenFabComplementos, Pedido, filial, tablaCompras)
                    Case 3
                        'Ordenes fab modelos
                        f = frmPAFLista.GetInstance(OrdenModelos, Pedido, filial, "ORDRE")

                End Select
                f.MdiParent = Me.MdiParent
                f.Show()
                f.BringToFront()
                Cursor = Cursors.Default
            End If

            f.MdiParent = Me.MdiParent
            AddHandler f.Closed, AddressOf CType(Me.MdiParent, frmPrincipal).childCerrado
            AddHandler f.Load, AddressOf CType(Me.MdiParent, frmPrincipal).childAbierto : AddHandler f.Activated, AddressOf CType(Me.MdiParent, frmPrincipal).childOcultandoMostrando
            Me.Visible = False
            f.cargando = True
            f.Show()
            f.BringToFront()
            Cursor = Cursors.Default
            cargando = True

            f.cargando = False
            cargando = False
            Me.Close()
            Me.Dispose()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim dr As DataRow
            dr = dtFiliales.NewRow
            dr("DESCRI") = rm.GetString("ELEGIREMPRESA")
            dr("CODI") = "ZZ"
            dtFiliales.Rows.Add(dr)
            cboSeleccionCentro.Visible = True
            lstCompraVenta.Enabled = False
            lstPAFP.Enabled = False
            ColocarCBOCENTRO()
            cboSeleccionCentro.SelectedValue = "ZZ"

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub
    Private Sub lstCompraVenta_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCompraVenta.SelectedIndexChanged
        Try
            If lstCompraVenta.SelectedIndex = 2 OrElse lstCompraVenta.SelectedIndex = 3 Then
                lstPAFP.Enabled = False
                btnAbrirListado.Enabled = True
            End If
            If lstCompraVenta.SelectedIndex = 1 OrElse lstCompraVenta.SelectedIndex = 0 Then
                lstPAFP.SelectedItem = Nothing
                lstPAFP.Enabled = True
                btnAbrirListado.Enabled = False
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub lstPAFP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstPAFP.SelectedIndexChanged
        Try
            btnAbrirListado.Enabled = True

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

End Class
