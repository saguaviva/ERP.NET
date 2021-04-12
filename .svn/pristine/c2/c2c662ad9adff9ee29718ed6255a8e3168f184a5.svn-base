Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class frmTejidoCompraPedido
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
        Me.GroupBox1.SuspendLayout
        CType(Me.txtTOTAL,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnImprimir,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbEleccionClientesPAF.SuspendLayout
        CType(Me.btnElegirProveedor,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tabPagePAF.SuspendLayout
        CType(Me.cboSeleccionProveParaPAF,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dgDetalle,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboKM,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboColor,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboArticle,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboNOMPROVE,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtPROVE,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtOBSERV,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtNRO,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtP_DTE,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtBASE1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtBRUT1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtIVA1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtDTE1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtRE1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.TabControlPAF.SuspendLayout
        CType(Me.btnCancelar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtCOMANDAMOSTRA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboMedida,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnRecargar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnSiguiente,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnAnterior,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnPrimero,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnUltimo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnModificar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnTancar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnBorrar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnNuevo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnActualizar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnVerLista,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'cboSeleccionProveParaPAF
        '
        Me.cboSeleccionProveParaPAF.Size = New System.Drawing.Size(363, 22)
        '
        'dgDetalle
        '
        Me.dgDetalle.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgDetalle.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgDetalle.PreviewInfo.ZoomFactor = 75R
        Me.dgDetalle.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgDetalle.PrintInfo.MeasurementPrinterName = Nothing
        '
        'cboKM
        '
        Me.cboKM.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboKM.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        '
        'cboColor
        '
        Me.cboColor.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboColor.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        '
        'cboArticle
        '
        Me.cboArticle.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboArticle.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        '
        'cboNOMPROVE
        '
        Me.cboNOMPROVE.Size = New System.Drawing.Size(520, 22)
        '
        'cboMedida
        '
        Me.cboMedida.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboMedida.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.sbtipo.Location = New System.Drawing.Point(5, 708)
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(115, 703)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(510, 703)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(70, 703)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(70, 674)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(510, 674)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(255, 674)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(1040, 703)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(255, 703)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(395, 674)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(115, 674)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(1040, 674)
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.Location = New System.Drawing.Point(949, 5)
        Me.cboSeleccionCentro.Size = New System.Drawing.Size(158, 25)
        '
        'frmTejidoCompraPedido
        '
        Me.ClientSize = New System.Drawing.Size(1159, 711)
        Me.Name = "frmTejidoCompraPedido"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        CType(Me.txtTOTAL,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnImprimir,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbEleccionClientesPAF.ResumeLayout(false)
        Me.gbEleccionClientesPAF.PerformLayout
        CType(Me.btnElegirProveedor,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabPagePAF.ResumeLayout(false)
        CType(Me.cboSeleccionProveParaPAF,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgDetalle,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboKM,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboColor,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboArticle,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboNOMPROVE,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtPROVE,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtOBSERV,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtNRO,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtP_DTE,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtBASE1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtBRUT1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtIVA1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtDTE1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtRE1,System.ComponentModel.ISupportInitialize).EndInit
        Me.TabControlPAF.ResumeLayout(false)
        CType(Me.btnCancelar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtCOMANDAMOSTRA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboMedida,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnRecargar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnSiguiente,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnAnterior,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnPrimero,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnUltimo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnModificar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnTancar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnBorrar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnNuevo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnActualizar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnVerLista,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

#End Region

    Shared frmChildForm As frmTejidoCompraPedido
    Public Shared Function GetInstance() As frmTejidoCompraPedido
        If frmChildForm Is Nothing Then
            frmChildForm = New frmTejidoCompraPedido

        End If
        Return frmChildForm
    End Function

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True
            tabla = tablaCompras
            IniciarDocumento(Tejido, Pedido)
            cargando = False
            PonerHandlersErroresParaGrids()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
End Class
