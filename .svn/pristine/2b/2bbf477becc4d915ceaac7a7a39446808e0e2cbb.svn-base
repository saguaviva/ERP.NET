Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Imports C1.Win.C1TrueDBGrid


Public Class frmTraspasoLineasOrdenes
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
    '! WithEvents adds overhead: btnAceptar

    '! WithEvents adds overhead: btnCancelar
    Friend WithEvents btnCancelar As C1.Win.C1Input.C1Button
    '! Events not handled: dgTraspaso
    '! WithEvents adds overhead: dgTraspaso
    Friend WithEvents btnAceptar As C1.Win.C1Input.C1Button
    Friend WithEvents dgTraspaso As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTraspasoLineasOrdenes))
        Me.btnAceptar = New C1.Win.C1Input.C1Button()
        Me.dgTraspaso = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
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
        CType(Me.btnAceptar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dgTraspaso,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(7, 335)
        Me.sbtipo.Size = New System.Drawing.Size(123, 18)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(101, 298)
        Me.btnRecargar.Size = New System.Drawing.Size(126, 22)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(482, 298)
        Me.btnSiguiente.Size = New System.Drawing.Size(44, 22)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(56, 298)
        Me.btnAnterior.Size = New System.Drawing.Size(45, 22)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(56, 279)
        Me.btnPrimero.Size = New System.Drawing.Size(45, 24)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(482, 279)
        Me.btnUltimo.Size = New System.Drawing.Size(44, 24)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(224, 279)
        Me.btnModificar.Size = New System.Drawing.Size(137, 24)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(1246, 298)
        Me.btnTancar.Size = New System.Drawing.Size(101, 22)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(224, 298)
        Me.btnBorrar.Size = New System.Drawing.Size(137, 22)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(370, 279)
        Me.btnNuevo.Size = New System.Drawing.Size(114, 46)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(101, 279)
        Me.btnActualizar.Size = New System.Drawing.Size(126, 24)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(1246, 279)
        Me.btnVerLista.Size = New System.Drawing.Size(101, 24)
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.ItemHeight = 17
        Me.cboSeleccionCentro.Location = New System.Drawing.Point(697, 9)
        Me.cboSeleccionCentro.Size = New System.Drawing.Size(218, 25)
        '
        'btnAceptar
        '
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAceptar.Location = New System.Drawing.Point(1053, 290)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(105, 28)
        Me.btnAceptar.TabIndex = 2
        Me.btnAceptar.Text = "Acceptar"
        Me.btnAceptar.UseVisualStyleBackColor = true
        '
        'dgTraspaso
        '
        Me.dgTraspaso.AllowAddNew = true
        Me.dgTraspaso.AllowDelete = true
        Me.dgTraspaso.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgTraspaso.CaptionHeight = 18
        Me.dgTraspaso.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgTraspaso.Images.Add(CType(resources.GetObject("dgTraspaso.Images"),System.Drawing.Image))
        Me.dgTraspaso.Location = New System.Drawing.Point(6, 5)
        Me.dgTraspaso.Name = "dgTraspaso"
        Me.dgTraspaso.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgTraspaso.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgTraspaso.PreviewInfo.ZoomFactor = 75R
        Me.dgTraspaso.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen
        Me.dgTraspaso.PrintInfo.MeasurementPrinterName = Nothing
        Me.dgTraspaso.RowHeight = 15
        Me.dgTraspaso.Size = New System.Drawing.Size(1170, 281)
        Me.dgTraspaso.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgTraspaso.TabIndex = 12
        Me.dgTraspaso.Text = "C1TrueDBGrid1"
        Me.dgTraspaso.UseCompatibleTextRendering = false
        Me.dgTraspaso.PropBag = resources.GetString("dgTraspaso.PropBag")
        '
        'frmTraspasoLineasOrdenes
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 16)
        Me.ClientSize = New System.Drawing.Size(886, 308)
        Me.Controls.Add(Me.dgTraspaso)
        Me.Controls.Add(Me.btnAceptar)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmTraspasoLineasOrdenes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Traspàs linies"
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
        Me.Controls.SetChildIndex(Me.dgTraspaso, 0)
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
        CType(Me.btnAceptar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgTraspaso,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

#End Region

#Region "VARIABLES"

    Public PAF As clsOrdenModelo
    Public traspasoActual As clsTraspasosOrdenPrenda
    Public localizacion As Point

#End Region

#Region "INICIALIZACION"

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            traspasoActual = New clsTraspasosOrdenPrenda(ds.Tables("dcfactutrasp"), PAF.centro, Me.BindingContext, PAF)
            dgTraspaso.SetDataBinding(traspasoActual.dvForm, "")
            InicializarDgTraspaso()
            PonerHandlersErroresParaGrids()
            localizacion.X = localizacion.X
            Me.Location = localizacion

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub InicializarDgTraspaso()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgTraspaso)
            i = 0
            PPCol2("FRA", dgTraspaso, rm.GetString("ALBARAN"), "", _
                        True, 35, False, C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 100, i, False)
            i = i + 1
            PPCol2("TALLA01", dgTraspaso, PAF.TALLA01, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = i + 1
            PPCol2("TALLA02", dgTraspaso, PAF.TALLA02, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = i + 1
            PPCol2("TALLA03", dgTraspaso, PAF.TALLA03, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
            PPCol2("TALLA04", dgTraspaso, PAF.TALLA04, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
            PPCol2("TALLA05", dgTraspaso, PAF.TALLA05, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
            PPCol2("TALLA06", dgTraspaso, PAF.TALLA06, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
            PPCol2("TALLA07", dgTraspaso, PAF.TALLA07, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
            PPCol2("TALLA08", dgTraspaso, PAF.TALLA08, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
            PPCol2("TALLA09", dgTraspaso, PAF.TALLA09, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
                i = i + 1
            PPCol2("TALLA10", dgTraspaso, PAF.TALLA10, "", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = i + 1
            PPCol2("DATA", dgTraspaso, rm.GetString("FECHA"), "Short Date", True, 35, False, _
                    C1.Win.C1TrueDBGrid.PresentationEnum.Normal, False, 100, i, False)

            'dgTraspaso.Splits(0).DisplayColumns("DATA").ButtonText = True

            i = i + 1
            PPCol2("TRASPASADA", dgTraspaso, rm.GetString("TRASPASADA"), "", True, 35, _
                    False, C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox, False, 100, i, False)

            'En principio esto no sirve para nada pq no se trasapasa de albaranes a facturas de forma
            'parcial

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ORGANIZAR"

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            traspasoActual.ActualizarOrigen(False, True)
            Me.Close()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    '*********************************************************************************
    'Se encarga de comprobar que el documento de destiono exista sea del mismo tipo
    'y que no este traspasado a otro doc ya, devuelve 1 en caso de que no exista el documento
    'hay que crearlo, 2 si existe y no esta traspasado y 0 si existe y no esta traspasado
    '**********************************************************************************
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            Close()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub dgTraspaso_AfterColUpdate(ByVal sender As System.Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgTraspaso.AfterColUpdate
        Try
            If Not cargando Then
                cargando = True
                Select Case e.Column.DataColumn.DataField
                    Case "TRASPASADA"
                        traspasoActual.HacerCalculos()
                End Select
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "OVERRIDES"

    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

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

#End Region

    Private Sub dgTraspaso_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgTraspaso.BeforeDelete
        Try
            If MessageBox.Show(rm.GetString("BORRARLINEA"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

End Class

