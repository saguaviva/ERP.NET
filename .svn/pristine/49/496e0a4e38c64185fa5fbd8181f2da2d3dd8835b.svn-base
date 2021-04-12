Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Class frmTraspasarModeloAAlbaran
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
        frmChildForm = Nothing
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.Name = "cboSeleccionCentro"
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(572, 572)
        Me.btnVerLista.Name = "btnVerLista"
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(60, 592)
        Me.btnActualizar.Name = "btnActualizar"
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(148, 572)
        Me.btnNuevo.Name = "btnNuevo"
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(228, 592)
        Me.btnBorrar.Name = "btnBorrar"
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(572, 592)
        Me.btnTancar.Name = "btnTancar"
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(148, 592)
        Me.btnModificar.Name = "btnModificar"
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(308, 572)
        Me.btnUltimo.Name = "btnUltimo"
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(28, 572)
        Me.btnPrimero.Name = "btnPrimero"
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(28, 592)
        Me.btnAnterior.Name = "btnAnterior"
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(308, 592)
        Me.btnSiguiente.Name = "btnSiguiente"
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(60, 572)
        Me.btnRecargar.Name = "btnRecargar"
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(5, 616)
        Me.sbtipo.Name = "sbtipo"
        Me.sbtipo.Text = ""
        '
        'frmTraspasarModeloAAlbaran
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(520, 278)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Enabled = False
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Name = "frmTraspasarModeloAAlbaran"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual

    End Sub

#End Region

    Shared frmChildForm As frmTraspasarModeloAAlbaran
    Public Shared Function GetInstance() As frmTraspasarModeloAAlbaran
        If frmChildForm Is Nothing Then
            frmChildForm = New frmTraspasarModeloAAlbaran

        End If
        Return frmChildForm
    End Function

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Cursor = Cursors.WaitCursor
            PonerHandlersErroresParaGrids()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub

    Friend Overrides Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As System.EventArgs)

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

    Friend Overrides Sub btnUltimo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
End Class

