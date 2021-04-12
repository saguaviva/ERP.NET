Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes



Public Class frmPagoForm2
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
    '! Events not handled: lblNombrePago
    '! WithEvents adds overhead: lblNombrePago
    '! Events not handled: lblCodigoPago
    '! WithEvents adds overhead: lblCodigoPago
    '! WithEvents adds overhead: btnPrimero

    '! WithEvents adds overhead: txtDescripcion
    '! removed Friend WithEvents lblPago As Label

    '! Events not handled: txtDias
    '! WithEvents adds overhead: txtDias
    '! Events not handled: txtNro
    '! WithEvents adds overhead: txtNro
    '! Events not handled: lblDias
    '! WithEvents adds overhead: lblDias
    '! Events not handled: lblNro
    '! WithEvents adds overhead: lblNro
    '! Events not handled: Label1
    '! WithEvents adds overhead: Label1
    '! Events not handled: txtV_1
    '! WithEvents adds overhead: txtV_1
    '! Events not handled: TabControl1
    '! WithEvents adds overhead: TabControl1
    '! Events not handled: TabPage1
    '! WithEvents adds overhead: TabPage1
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents dgFormas As C1.Win.C1TrueDBGrid.C1TrueDBGrid

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPagoForm2))
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.dgFormas = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        CType(Me.btnRecargar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSiguiente, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAnterior, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrimero, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUltimo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnModificar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnTancar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBorrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNuevo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnActualizar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnVerLista, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgFormas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        resources.ApplyResources(Me.sbtipo, "sbtipo")
        '
        'btnRecargar
        '
        resources.ApplyResources(Me.btnRecargar, "btnRecargar")
        '
        'btnSiguiente
        '
        resources.ApplyResources(Me.btnSiguiente, "btnSiguiente")
        '
        'btnAnterior
        '
        resources.ApplyResources(Me.btnAnterior, "btnAnterior")
        '
        'btnPrimero
        '
        resources.ApplyResources(Me.btnPrimero, "btnPrimero")
        '
        'btnUltimo
        '
        resources.ApplyResources(Me.btnUltimo, "btnUltimo")
        '
        'btnModificar
        '
        resources.ApplyResources(Me.btnModificar, "btnModificar")
        '
        'btnTancar
        '
        resources.ApplyResources(Me.btnTancar, "btnTancar")
        '
        'btnBorrar
        '
        resources.ApplyResources(Me.btnBorrar, "btnBorrar")
        '
        'btnNuevo
        '
        resources.ApplyResources(Me.btnNuevo, "btnNuevo")
        '
        'btnActualizar
        '
        resources.ApplyResources(Me.btnActualizar, "btnActualizar")
        '
        'btnVerLista
        '
        resources.ApplyResources(Me.btnVerLista, "btnVerLista")
        '
        'btnAceptar
        '
        resources.ApplyResources(Me.btnAceptar, "btnAceptar")
        Me.btnAceptar.Name = "btnAceptar"
        '
        'dgFormas
        '
        resources.ApplyResources(Me.dgFormas, "dgFormas")
        Me.dgFormas.Images.Add(CType(resources.GetObject("dgFormas.Images"), System.Drawing.Image))
        Me.dgFormas.Name = "dgFormas"
        Me.dgFormas.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgFormas.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgFormas.PreviewInfo.ZoomFactor = 75.0R
        Me.dgFormas.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgFormas.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgFormas.PrintInfo.PageSettings = CType(resources.GetObject("dgFormas.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgFormas.RowDivider.Color = CType(resources.GetObject("dgFormas.RowDivider.Color"), System.Drawing.Color)
        Me.dgFormas.RowDivider.Style = CType(resources.GetObject("dgFormas.RowDivider.Style"), C1.Win.C1TrueDBGrid.LineStyleEnum)
        Me.dgFormas.UseCompatibleTextRendering = False
        Me.dgFormas.PropBag = resources.GetString("dgFormas.PropBag")
        '
        'frmPagoForm2
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.dgFormas)
        Me.Controls.Add(Me.btnAceptar)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.KeyPreview = True
        Me.Name = "frmPagoForm2"
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
        Me.Controls.SetChildIndex(Me.dgFormas, 0)
        CType(Me.btnRecargar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSiguiente, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAnterior, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrimero, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUltimo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnModificar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnTancar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBorrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNuevo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnActualizar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnVerLista, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgFormas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmPagoForm2
    Public Shared Function GetInstance() As frmPagoForm2
        If frmChildForm Is Nothing Then
            frmChildForm = New frmPagoForm2
        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Public formaActual As clsForma2

#End Region

#Region "INICIALIZAR"

    Private Sub HacerBindings()
        Dim i As Integer
        Try

            dgFormas.SetDataBinding(formaActual.dvForm, "")
            OcultarColumnasDG(dgFormas)
            i = 0
            PPCol2("CODI", dgFormas, rm.GetString("CODIGO"), "", True, 55, False, _
                              C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 55, i, False)
            i = i + 1
            PPCol2("DESCRIPCIO", dgFormas, rm.GetString("DESCRIPCION"), "", True, 200, False, _
                              C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 200, i, False)
            i = i + 1
            PPCol2("Nro", dgFormas, "Nro", "", True, 55, False, _
                              C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 55, i, False)
            i = i + 1
            PPCol2("V_1", dgFormas, "V_1", "", True, 55, False, _
                              C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 55, i, False)
            i = i + 1
            PPCol2("Dies", dgFormas, "Dies", "", True, 55, False, _
                              C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 55, i, False)
            i = i + 1
            'PPCol2("ESTRANSFERENCIA", dgFormas, "Es Transferència Bancària", "", True, 55, False, _
            '                  C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox, False, 55, i, False)


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True
            tabla = tablaFormaPago
            formaActual = New clsForma2(ds.Tables(tabla), "C", BindingContext)
            HacerBindings()
            'formaActual.tabla.AcceptChanges()
            'PonerModificables(soloLectura)
            ' PSBTIPO(formaActual.centro)
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
    '            formaActual.borrar()
    '            editando = False
    '        End If

    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN()
    '    End Try
    'End Sub
    'Friend Overrides Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnActualizar.Click
    '    Try
    '        cargando = True
    '        formaActual.ActualizarOrigen()
    '        PSBTIPO(formaActual.centro) : cargando = False

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
    '        formaActual.PrimeroReg() : ActualizarOrigen()
    '        PSBTIPO(formaActual.centro) : cargando = False

    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN()
    '    End Try
    'End Sub
    'Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUltimo.Click
    '    Try
    '        cargando = True
    '        formaActual.UltimoReg() : ActualizarOrigen()
    '        PSBTIPO(formaActual.centro) : cargando = False

    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN()
    '    End Try
    'End Sub
    'Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAnterior.Click
    '    Try
    '        cargando = True
    '        formaActual.AnteriorReg() : ActualizarOrigen()
    '        PSBTIPO(formaActual.centro) : cargando = False

    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN()
    '    End Try
    'End Sub
    'Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSiguiente.Click
    '    Try
    '        cargando = True
    '        formaActual.SiguienteReg() : ActualizarOrigen()
    '        PSBTIPO(formaActual.centro) : cargando = False

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
        formaActual.ActualizarOrigen()
    End Sub
End Class
