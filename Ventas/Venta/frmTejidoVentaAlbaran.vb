Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class frmTejidoVentaAlbaran
    Inherits aura2k3.frmVentaPAFPlantilla

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTejidoVentaAlbaran))
        Me.tabPageRecibos.SuspendLayout
        Me.tabPageConsumo.SuspendLayout
        Me.gbVencim.SuspendLayout
        Me.GroupBox3.SuspendLayout
        CType(Me.dgConsumos,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txCFRA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txCNOMCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtP_DTE,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtCOMIS,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtREPRES,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dtpDATA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboSeleccionClienteParaPAF,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnElegirCliente,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnElegirRepresentate,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboNOMCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboFRA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboNOMREPRES,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboDeFacuraRecibo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboAFacturaRecibo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dtpAFechaRecibo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dtpDeFechaRecibo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnGenerarRecibo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dtcDATA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtALBCLI,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboDOM,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txcCLIENT,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gb.SuspendLayout
        Me.gbEleccionClientesPAF.SuspendLayout
        Me.GroupBox4.SuspendLayout
        Me.tabControlPAFVentas.SuspendLayout
        CType(Me.txtOBSERV,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtBASE1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtIVA1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtRE1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtDTE1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dgLineasPAFVenta,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboMuestra,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboMuestra2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboKM,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboTipo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboTalla,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboColor,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txvTOTAL,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtBRUT1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dgImpresion,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dgImpresion2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnEliminarFilasSeleccionadas,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboBANCO,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboImpresion,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtOBSERVOCULTA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtTOTAL,System.ComponentModel.ISupportInitialize).BeginInit
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
        'tabPageTraspasoPAF
        '
        Me.tabPageTraspasoPAF.Size = New System.Drawing.Size(994, 539)
        '
        'tabPageVencimientos
        '
        Me.tabPageVencimientos.Size = New System.Drawing.Size(994, 550)
        '
        'tabPageImpresionPAF
        '
        Me.tabPageImpresionPAF.Size = New System.Drawing.Size(994, 539)
        '
        'tabPageRecibos
        '
        Me.tabPageRecibos.Size = New System.Drawing.Size(994, 539)
        '
        'tabPagePAF
        '
        Me.tabPagePAF.Size = New System.Drawing.Size(994, 539)
        '
        'tabPageConsumo
        '
        Me.tabPageConsumo.Size = New System.Drawing.Size(994, 539)
        '
        'gbVencim
        '
        Me.gbVencim.Size = New System.Drawing.Size(461, 544)
        '
        'dgConsumos
        '
        Me.dgConsumos.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgConsumos.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgConsumos.PreviewInfo.ZoomFactor = 75R
        Me.dgConsumos.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgConsumos.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgConsumos.Size = New System.Drawing.Size(1876, 292)
        Me.dgConsumos.PropBag = resources.GetString("dgConsumos.PropBag")
        '
        'dtpDATA
        '
        '
        '
        '
        Me.dtpDATA.Calendar.DayNameLength = 1
        Me.dtpDATA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        '
        'cboSeleccionClienteParaPAF
        '
        Me.cboSeleccionClienteParaPAF.Size = New System.Drawing.Size(317, 22)
        '
        'cboNOMCLIENT
        '
        Me.cboNOMCLIENT.Size = New System.Drawing.Size(322, 22)
        '
        'cboFRA
        '
        Me.cboFRA.Size = New System.Drawing.Size(137, 22)
        '
        'cboNOMREPRES
        '
        Me.cboNOMREPRES.Size = New System.Drawing.Size(315, 22)
        '
        'cboDeFacuraRecibo
        '
        Me.cboDeFacuraRecibo.Size = New System.Drawing.Size(156, 22)
        '
        'cboAFacturaRecibo
        '
        Me.cboAFacturaRecibo.Size = New System.Drawing.Size(156, 22)
        '
        'dtpAFechaRecibo
        '
        '
        '
        '
        Me.dtpAFechaRecibo.Calendar.DayNameLength = 1
        Me.dtpAFechaRecibo.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        '
        'dtpDeFechaRecibo
        '
        '
        '
        '
        Me.dtpDeFechaRecibo.Calendar.DayNameLength = 1
        Me.dtpDeFechaRecibo.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        '
        'btnGenerarRecibo
        '
        Me.btnGenerarRecibo.Location = New System.Drawing.Point(259, 312)
        '
        'dtcDATA
        '
        '
        '
        '
        Me.dtcDATA.Calendar.DayNameLength = 1
        Me.dtcDATA.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        '
        'cboDOM
        '
        Me.cboDOM.Size = New System.Drawing.Size(315, 22)
        '
        'GroupBox4
        '
        Me.GroupBox4.Size = New System.Drawing.Size(380, 292)
        '
        'tabControlPAFVentas
        '
        Me.tabControlPAFVentas.Size = New System.Drawing.Size(1002, 565)
        '
        'dgLineasPAFVenta
        '
        Me.dgLineasPAFVenta.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgLineasPAFVenta.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgLineasPAFVenta.PreviewInfo.ZoomFactor = 75R
        Me.dgLineasPAFVenta.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgLineasPAFVenta.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgLineasPAFVenta.Size = New System.Drawing.Size(988, 248)
        Me.dgLineasPAFVenta.PropBag = resources.GetString("dgLineasPAFVenta.PropBag")
        '
        'cboMuestra
        '
        Me.cboMuestra.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboMuestra.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        '
        'cboMuestra2
        '
        Me.cboMuestra2.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboMuestra2.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        '
        'cboKM
        '
        Me.cboKM.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboKM.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        '
        'cboTipo
        '
        Me.cboTipo.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboTipo.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        '
        'cboTalla
        '
        Me.cboTalla.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboTalla.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        '
        'cboColor
        '
        Me.cboColor.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboColor.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        '
        'txvTOTAL
        '
        Me.txvTOTAL.Location = New System.Drawing.Point(180, 453)
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(282, 451)
        '
        'txtBRUT1
        '
        Me.txtBRUT1.DisplayFormat.CustomFormat = "#,##0.00"
        Me.txtBRUT1.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtBRUT1.DisplayFormat.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None
        '
        'dgImpresion
        '
        Me.dgImpresion.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgImpresion.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgImpresion.PreviewInfo.ZoomFactor = 75R
        Me.dgImpresion.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgImpresion.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgImpresion.Size = New System.Drawing.Size(475, 250)
        Me.dgImpresion.PropBag = resources.GetString("dgImpresion.PropBag")
        '
        'dgImpresion2
        '
        Me.dgImpresion2.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgImpresion2.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgImpresion2.PreviewInfo.ZoomFactor = 75R
        Me.dgImpresion2.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgImpresion2.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgImpresion2.Size = New System.Drawing.Size(475, 250)
        Me.dgImpresion2.PropBag = resources.GetString("dgImpresion2.PropBag")
        '
        'cboBANCO
        '
        Me.cboBANCO.Size = New System.Drawing.Size(308, 22)
        '
        'cboImpresion
        '
        Me.cboImpresion.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboImpresion.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.[Single]
        '
        'txtTOTAL
        '
        Me.txtTOTAL.DisplayFormat.CustomFormat = "#,##0.00"
        Me.txtTOTAL.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.txtTOTAL.DisplayFormat.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.sbtipo.Location = New System.Drawing.Point(5, 739)
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(115, 717)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(510, 717)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(70, 717)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(70, 689)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(510, 689)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(255, 689)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(887, 717)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(255, 717)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(395, 689)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(115, 689)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(887, 689)
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.Location = New System.Drawing.Point(796, 5)
        Me.cboSeleccionCentro.Size = New System.Drawing.Size(158, 25)
        '
        'frmTejidoVentaAlbaran
        '
        Me.ClientSize = New System.Drawing.Size(992, 731)
        Me.Name = "frmTejidoVentaAlbaran"
        Me.tabPageRecibos.ResumeLayout(false)
        Me.tabPageConsumo.ResumeLayout(false)
        Me.gbVencim.ResumeLayout(false)
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        CType(Me.dgConsumos,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txCFRA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txCNOMCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtP_DTE,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtCOMIS,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtREPRES,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtpDATA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboSeleccionClienteParaPAF,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnElegirCliente,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnElegirRepresentate,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboNOMCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboFRA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboNOMREPRES,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboDeFacuraRecibo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboAFacturaRecibo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtpAFechaRecibo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtpDeFechaRecibo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnGenerarRecibo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtcDATA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtALBCLI,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboDOM,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txcCLIENT,System.ComponentModel.ISupportInitialize).EndInit
        Me.gb.ResumeLayout(false)
        Me.gbEleccionClientesPAF.ResumeLayout(false)
        Me.gbEleccionClientesPAF.PerformLayout
        Me.GroupBox4.ResumeLayout(false)
        Me.GroupBox4.PerformLayout
        Me.tabControlPAFVentas.ResumeLayout(false)
        CType(Me.txtOBSERV,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtBASE1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtIVA1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtRE1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtDTE1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgLineasPAFVenta,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboMuestra,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboMuestra2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboKM,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboTipo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboTalla,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboColor,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txvTOTAL,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtBRUT1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgImpresion,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgImpresion2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnEliminarFilasSeleccionadas,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboBANCO,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboImpresion,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtOBSERVOCULTA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtTOTAL,System.ComponentModel.ISupportInitialize).EndInit
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

    Shared frmChildForm As frmTejidoVentaAlbaran
    Public Shared Function GetInstance() As frmTejidoVentaAlbaran
        If frmChildForm Is Nothing Then
            frmChildForm = New frmTejidoVentaAlbaran

        End If
        Return frmChildForm
    End Function

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True
            tabla = tablaVentas
            IniciarDocumento(Albaran)

            tabControlPAFVentas.SelectedIndex = 0
            cargando = False
            PonerHandlersErroresParaGrids()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
End Class
