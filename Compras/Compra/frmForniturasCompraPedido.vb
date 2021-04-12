Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class frmForniturasCompraPedido
    Inherits aura2k3.frmCompraPAFPlantilla

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmForniturasCompraPedido))
        CType(Me.cboColor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPROVE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMPROVE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTOTAL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboSeleccionProveParaPAF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboKM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboArticle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNRO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCOMANDAMOSTRA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtP_DTE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBASE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBRUT1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIVA1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDTE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRE1, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'btnImprimir
        '
        Me.btnImprimir.Name = "btnImprimir"
        '
        'cboColor
        '
        Me.cboColor.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.cboColor.Name = "cboColor"
        Me.cboColor.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboColor.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        '
        'lblFecha
        '
        Me.lblFecha.Name = "lblFecha"
        '
        'lblObservaciones
        '
        Me.lblObservaciones.Name = "lblObservaciones"
        '
        'lblProveedor
        '
        Me.lblProveedor.Name = "lblProveedor"
        '
        'lblPedido
        '
        Me.lblPedido.Name = "lblPedido"
        '
        'btnElegirProveedor
        '
        Me.btnElegirProveedor.Name = "btnElegirProveedor"
        '
        'GroupBox1
        '
        Me.GroupBox1.Name = "GroupBox1"
        '
        'Label2
        '
        Me.Label2.Name = "Label2"
        '
        'txtPROVE
        '
        Me.txtPROVE.Name = "txtPROVE"
        '
        'cboNOMPROVE
        '
        Me.cboNOMPROVE.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.cboNOMPROVE.MaxDropDownItems = CType(10, Short)
        Me.cboNOMPROVE.Name = "cboNOMPROVE"
        Me.cboNOMPROVE.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboNOMPROVE.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        '
        'lblIVA
        '
        Me.lblIVA.Name = "lblIVA"
        '
        'lblOrdenFabComplemento
        '
        Me.lblOrdenFabComplemento.Name = "lblOrdenFabComplemento"
        '
        'Label1
        '
        Me.Label1.Name = "Label1"
        '
        'lblTraspasado
        '
        Me.lblTraspasado.Name = "lblTraspasado"
        '
        'lblDescuento
        '
        Me.lblDescuento.Name = "lblDescuento"
        '
        'lblBase
        '
        Me.lblBase.Name = "lblBase"
        '
        'lblTotalBruto
        '
        Me.lblTotalBruto.Name = "lblTotalBruto"
        '
        'txtTOTAL
        '
        Me.txtTOTAL.Name = "txtTOTAL"
        '
        'gbEleccionClientesPAF
        '
        Me.gbEleccionClientesPAF.Name = "gbEleccionClientesPAF"
        '
        'lblTotal
        '
        Me.lblTotal.Name = "lblTotal"
        '
        'lblNumeroPedido
        '
        Me.lblNumeroPedido.Name = "lblNumeroPedido"
        '
        'tabPagePAF
        '
        Me.tabPagePAF.Name = "tabPagePAF"
        '
        'rdoVerTodasPAF
        '
        Me.rdoVerTodasPAF.Name = "rdoVerTodasPAF"
        '
        'rdoVerPAFDeCliente
        '
        Me.rdoVerPAFDeCliente.Enabled = False
        Me.rdoVerPAFDeCliente.Name = "rdoVerPAFDeCliente"
        '
        'lblRE
        '
        Me.lblRE.Name = "lblRE"
        '
        'lblFechaEntrega
        '
        Me.lblFechaEntrega.Name = "lblFechaEntrega"
        '
        'cboSeleccionProveParaPAF
        '
        Me.cboSeleccionProveParaPAF.Enabled = False
        Me.cboSeleccionProveParaPAF.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Image))
        Me.cboSeleccionProveParaPAF.Name = "cboSeleccionProveParaPAF"
        Me.cboSeleccionProveParaPAF.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboSeleccionProveParaPAF.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        '
        'dgDetalle
        '
        Me.dgDetalle.Images.Add(CType(resources.GetObject("resource3"), System.Drawing.Image))
        Me.dgDetalle.Name = "dgDetalle"
        Me.dgDetalle.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgDetalle.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgDetalle.PreviewInfo.ZoomFactor = 75
        Me.dgDetalle.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgDetalle.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.dgDetalle.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        '
        'cboKM
        '
        Me.cboKM.Images.Add(CType(resources.GetObject("resource4"), System.Drawing.Image))
        Me.cboKM.Name = "cboKM"
        Me.cboKM.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboKM.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        '
        'cboArticle
        '
        Me.cboArticle.Images.Add(CType(resources.GetObject("resource5"), System.Drawing.Image))
        Me.cboArticle.Name = "cboArticle"
        Me.cboArticle.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboArticle.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        '
        'txtNRO
        '
        Me.txtNRO.Name = "txtNRO"
        '
        'txtCOMANDAMOSTRA
        '
        Me.txtCOMANDAMOSTRA.Name = "txtCOMANDAMOSTRA"
        '
        'lblNumeroPAF
        '
        Me.lblNumeroPAF.Name = "lblNumeroPAF"
        '
        'txtOBSERV
        '
        Me.txtOBSERV.Name = "txtOBSERV"
        '
        'lblPedidoMuestra
        '
        Me.lblPedidoMuestra.Name = "lblPedidoMuestra"
        '
        'txtP_DTE
        '
        Me.txtP_DTE.Name = "txtP_DTE"
        '
        'txtBASE1
        '
        Me.txtBASE1.Name = "txtBASE1"
        '
        'txtBRUT1
        '
        Me.txtBRUT1.Name = "txtBRUT1"
        '
        'btnCancelar
        '
        Me.btnCancelar.Name = "btnCancelar"
        '
        'txtIVA1
        '
        Me.txtIVA1.Name = "txtIVA1"
        '
        'txtDTE1
        '
        Me.txtDTE1.Name = "txtDTE1"
        '
        'txtRE1
        '
        Me.txtRE1.Name = "txtRE1"
        '
        'chkESTOC
        '
        Me.chkESTOC.Name = "chkESTOC"
        '
        'TabControlPAF
        '
        Me.TabControlPAF.Name = "TabControlPAF"
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(372, 574)
        Me.btnUltimo.Name = "btnUltimo"
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(70, 574)
        Me.btnPrimero.Name = "btnPrimero"
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(899, 594)
        Me.btnTancar.Name = "btnTancar"
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(192, 594)
        Me.btnBorrar.Name = "btnBorrar"
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(290, 574)
        Me.btnNuevo.Name = "btnNuevo"
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(102, 574)
        Me.btnActualizar.Name = "btnActualizar"
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(899, 574)
        Me.btnVerLista.Name = "btnVerLista"
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.Location = New System.Drawing.Point(830, 5)
        Me.cboSeleccionCentro.Name = "cboSeleccionCentro"
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(70, 594)
        Me.btnAnterior.Name = "btnAnterior"
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(192, 574)
        Me.btnModificar.Name = "btnModificar"
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.sbtipo.Location = New System.Drawing.Point(5, 594)
        Me.sbtipo.Name = "sbtipo"
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(102, 594)
        Me.btnRecargar.Name = "btnRecargar"
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(372, 594)
        Me.btnSiguiente.Name = "btnSiguiente"
        '
        'frmForniturasCompraPedido
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(988, 618)
        Me.Name = "frmForniturasCompraPedido"
        CType(Me.cboColor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPROVE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMPROVE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTOTAL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboSeleccionProveParaPAF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboKM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboArticle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNRO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCOMANDAMOSTRA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtP_DTE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBASE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBRUT1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIVA1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDTE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRE1, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

#End Region
    Shared frmChildForm As frmForniturasCompraPedido
    Public Shared Function GetInstance() As frmForniturasCompraPedido
        If frmChildForm Is Nothing Then
            frmChildForm = New frmForniturasCompraPedido

        End If
        Return frmChildForm
    End Function

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True
            tabla = tablaCompras
            IniciarDocumento(Fornitura, Pedido)
            PonerHandlersErroresParaGrids()
            cargando = False
            btnVerLista.Visible = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub

End Class
