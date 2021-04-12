Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class frmTejidoVentaPedido
    Inherits aura2k3.frmVentaPAFPlantilla


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
        frmChildForm = Nothing
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTejidoVentaPedido))
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
        
        '
        'frmTejidoVentaPedido
        '
        Me.ClientSize = New System.Drawing.Size(992, 656)
        Me.Name = "frmTejidoVentaPedido"
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

    Shared frmChildForm As frmTejidoVentaPedido
    Public Shared Function GetInstance() As frmTejidoVentaPedido
        If frmChildForm Is Nothing Then
            frmChildForm = New frmTejidoVentaPedido
        End If
        Return frmChildForm
    End Function

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True
            tabla = tablaVentas
            IniciarDocumento(Pedido)
            cargando = False
            tabControlPAFVentas.SelectedIndex = 0
            PonerHandlersErroresParaGrids()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub

End Class