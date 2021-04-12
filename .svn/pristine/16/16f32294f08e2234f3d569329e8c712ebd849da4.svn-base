Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes



Public Class frmIVAForm
    Inherits aura2k3.frmBase

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent() : Dim tom As SMcMaster.TabOrderManager = New SMcMaster.TabOrderManager(Me) : tom.SetTabOrder(SMcMaster.TabOrderManager.TabScheme.AcrossFirst)
        'arrayTextBox.AddRange(New System.Windows.Forms.Control() {Me.txtDescripcion, Me.txtIVA, Me.txtRE})
        '        arrayTextBox.AddRange(New System.Windows.Forms.Control() {Me.txtDescripcion, Me.txtIVA, Me.txtRE})
        'arrayTextBox = New ERP.clsArrayControles(Of System.Windows.Forms.Control)("txtDescripcion", Me)
        arrayTextBox = New TextBox() {txtDescripcion, txtIVA, txtRE}
        'arrayTextBox.AsignarControles(New System.Windows.Forms.Control() {Me.txtDescripcion, Me.txtIVA, Me.txtRE})
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


    Friend WithEvents lblNombreIVA As Label


    Friend WithEvents lblCodigoIVA As Label


    Friend WithEvents txtDescripcion As C1.Win.C1Input.C1TextBox

    Friend WithEvents txtRE As C1.Win.C1Input.C1TextBox

    Friend WithEvents txtIVA As C1.Win.C1Input.C1TextBox


    Friend WithEvents lblRE As Label


    Friend WithEvents lblIVA As Label


    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl


    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage



    Friend WithEvents cboID As C1.Win.C1List.C1Combo

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIVAForm))
        Me.lblNombreIVA = New System.Windows.Forms.Label()
        Me.lblCodigoIVA = New System.Windows.Forms.Label()
        Me.txtDescripcion = New C1.Win.C1Input.C1TextBox()
        Me.txtRE = New C1.Win.C1Input.C1TextBox()
        Me.txtIVA = New C1.Win.C1Input.C1TextBox()
        Me.lblRE = New System.Windows.Forms.Label()
        Me.lblIVA = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.cboID = New C1.Win.C1List.C1Combo()
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
        CType(Me.txtDescripcion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(5, 136)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(60, 92)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(308, 112)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(28, 112)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(28, 92)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(308, 92)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(148, 112)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(548, 112)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(228, 112)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(148, 92)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(60, 112)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(548, 92)
        '
        'lblNombreIVA
        '
        Me.lblNombreIVA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNombreIVA.Location = New System.Drawing.Point(8, 32)
        Me.lblNombreIVA.Name = "lblNombreIVA"
        Me.lblNombreIVA.Size = New System.Drawing.Size(64, 16)
        Me.lblNombreIVA.TabIndex = 207
        Me.lblNombreIVA.Text = "Descripció"
        '
        'lblCodigoIVA
        '
        Me.lblCodigoIVA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCodigoIVA.Location = New System.Drawing.Point(8, 8)
        Me.lblCodigoIVA.Name = "lblCodigoIVA"
        Me.lblCodigoIVA.Size = New System.Drawing.Size(56, 16)
        Me.lblCodigoIVA.TabIndex = 206
        Me.lblCodigoIVA.Text = "Codi IVA"
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Location = New System.Drawing.Point(72, 32)
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Size = New System.Drawing.Size(364, 18)
        Me.txtDescripcion.TabIndex = 1
        Me.txtDescripcion.Tag = Nothing
        '
        'txtRE
        '
        Me.txtRE.Location = New System.Drawing.Point(164, 56)
        Me.txtRE.Name = "txtRE"
        Me.txtRE.Size = New System.Drawing.Size(56, 18)
        Me.txtRE.TabIndex = 3
        Me.txtRE.Tag = Nothing
        '
        'txtIVA
        '
        Me.txtIVA.Location = New System.Drawing.Point(72, 56)
        Me.txtIVA.Name = "txtIVA"
        Me.txtIVA.Size = New System.Drawing.Size(48, 18)
        Me.txtIVA.TabIndex = 2
        Me.txtIVA.Tag = Nothing
        '
        'lblRE
        '
        Me.lblRE.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRE.Location = New System.Drawing.Point(132, 56)
        Me.lblRE.Name = "lblRE"
        Me.lblRE.Size = New System.Drawing.Size(32, 16)
        Me.lblRE.TabIndex = 210
        Me.lblRE.Text = "%RE"
        '
        'lblIVA
        '
        Me.lblIVA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblIVA.Location = New System.Drawing.Point(28, 60)
        Me.lblIVA.Name = "lblIVA"
        Me.lblIVA.Size = New System.Drawing.Size(40, 12)
        Me.lblIVA.TabIndex = 211
        Me.lblIVA.Text = "%IVA"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.ItemSize = New System.Drawing.Size(42, 18)
        Me.TabControl1.Location = New System.Drawing.Point(4, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(492, 124)
        Me.TabControl1.TabIndex = 212
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.cboID)
        Me.TabPage1.Controls.Add(Me.lblRE)
        Me.TabPage1.Controls.Add(Me.txtIVA)
        Me.TabPage1.Controls.Add(Me.lblIVA)
        Me.TabPage1.Controls.Add(Me.lblNombreIVA)
        Me.TabPage1.Controls.Add(Me.lblCodigoIVA)
        Me.TabPage1.Controls.Add(Me.txtDescripcion)
        Me.TabPage1.Controls.Add(Me.txtRE)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(484, 98)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "IVA"
        '
        'cboID
        '
        Me.cboID.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboID.Caption = ""
        Me.cboID.CaptionHeight = 17
        Me.cboID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboID.ColumnCaptionHeight = 17
        Me.cboID.ColumnFooterHeight = 17
        Me.cboID.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboID.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboID.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboID.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboID.Images.Add(CType(resources.GetObject("cboID.Images"), System.Drawing.Image))
        Me.cboID.ItemHeight = 15
        Me.cboID.Location = New System.Drawing.Point(72, 4)
        Me.cboID.MatchEntryTimeout = CType(2000, Long)
        Me.cboID.MaxDropDownItems = CType(15, Short)
        Me.cboID.MaxLength = 10
        Me.cboID.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboID.Name = "cboID"
        Me.cboID.PropBag = resources.GetString("cboID.PropBag")
        Me.cboID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboID.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboID.Size = New System.Drawing.Size(121, 21)
        Me.cboID.TabIndex = 212
        Me.cboID.Text = "C1Combo1"
        '
        'frmIVAForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(500, 198)
        Me.Controls.Add(Me.TabControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.KeyPreview = True
        Me.Name = "frmIVAForm"
        Me.Text = " IVA"
        Me.Controls.SetChildIndex(Me.cboSeleccionCentro, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
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
        CType(Me.txtDescripcion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIVA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmIVAForm
    Public Shared Function GetInstance() As frmIVAForm
        If frmChildForm Is Nothing Then
            frmChildForm = New frmIVAForm
        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Public IVAactual As clsIVA

#End Region

#Region "INICIALIZAR"

    Private Sub HacerBindings()
        Try
            AñadirBinding(cboID, IVAactual.dvForm, "CODI")
            AñadirBinding(txtDescripcion, IVAactual.dvForm, "DESCRIPCIO")
            AñadirBinding(txtIVA, IVAactual.dvForm, "IVA")
            AñadirBinding(txtRE, IVAactual.dvForm, "RE")
            AñadirBindingCombo(cboID, IVAactual.dvIdentificadores, CCIVA, CCIVA)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True
            tabla = tablaIVA
            'formulario = True

            IVAactual = New clsIVA(ds.Tables(tabla), "C", BindingContext)
            HacerBindings()
            PonerModificables(soloLectura)

            PSBTIPO(IVAactual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub

#End Region

#Region "MODIFICAR DB"

    Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnBorrar.Click
        If MessageBox.Show(rm.GetString("BorrarIVA"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
            editando = True
            IVAactual.borrar() : ActualizarOrigen()
            editando = False
        End If
    End Sub
    Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnNuevo.Click
        Try
            cargando = True
            EsRegistroNuevo = True
            PonerModificables(modificable)
            PonerControlesNuevo(False)
            'cboID.ClearItems()
            PSBTIPO(IVAactual.centro)
            btnNuevo.Visible = False
            IVAactual.NuevoRegistro()
            PSBTIPO(IVAactual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub ActualizarOrigen()
        Try
            If editando Then
                editando = False
                PonerModificables(soloLectura)
                cboID.Text = IVAactual.CODI
            End If
            If EsRegistroNuevo Then
                editando = True : PSBTIPO(IVAactual.centro)
                PonerControlesNuevo(True)
                EsRegistroNuevo = False
                'MoverActual()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnActualizar.Click
        Try
            cargando = True
            IVAactual.guardarDV()

            Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                Case DialogResult.Cancel
                    cargando = False
                    Exit Sub
                Case DialogResult.No
                    IVAactual.tabla.RejectChanges()
                    cargando = False
                    Exit Sub
            End Select
            IVAactual.ActualizarOrigen() : ActualizarOrigen()
            PSBTIPO(IVAactual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles btnModificar.Click
        Try
            If Not editando Then
                cargando = True
                editando = True
                PonerModificables(modificable)
                cboID.Text = IVAactual.CODI
                PSBTIPO(IVAactual.centro)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ESPECÍFICO"

#End Region

#Region "IMPRESIÓN"

#End Region

#Region "COMUNES"

    Private Sub btnVerLista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerLista.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmIVALista = frmIVALista.GetInstance(esListado)
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
    Private Sub PonerControlesNuevo(ByVal b As Boolean)
        Try
            If Not b Then
                btnActualizar.Text = rm.GetString("CONFIRMAR")
            Else
                btnActualizar.Text = rm.GetString("ACTUALIZAR")
            End If
            ModiNuev(b)
            cboID.AutoCompletion = Not editando

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerModificables(Optional ByVal b As Boolean = False)
        Try
            PonerMod(b)
            If Not b Then : cboID.DataSource = Nothing
            Else : AñadirBindingCombo(cboID, IVAactual.dvIdentificadores, CCIVA, CCIVA) : IVAactual.tabla.AcceptChanges() : End If

            cboID.LimitToList = b
            cboID.SuperBack = b
            cboID.ReadOnly = editando ''or esregistronuevo
            ModiNuev(b)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Sub btnRecargar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecargar.Click
        Try
            cargando = True
            Dim id As Object
            id = cboID.Text
            IVAactual.ActualizarOrigen()
            IVAactual.CambiarAReg(id, iraregistro)
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "DESPLAZARSE"

    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnPrimero.Click
        Try
            cargando = True
            IVAactual.PrimeroReg() : ActualizarOrigen()
            PSBTIPO(IVAactual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUltimo.Click
        Try
            cargando = True
            IVAactual.UltimoReg() : ActualizarOrigen()
            PSBTIPO(IVAactual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAnterior.Click
        Try
            cargando = True
            IVAactual.AnteriorReg() : ActualizarOrigen()
            PSBTIPO(IVAactual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSiguiente.Click
        Try
            cargando = True
            IVAactual.SiguienteReg() : ActualizarOrigen()
            PSBTIPO(IVAactual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "SELECCIÓN REGISTRO"

    Private Sub cboID_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboID.RowChange
        Try
            If consulta() Then
                cargando = True
                IVAactual.CambiarAReg(general.nz(cboID.WillChangeToValue, ""), iraregistro)
                PSBTIPO(IVAactual.centro) : cargando = False

            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboID.KeyPress
        Try
            If editando Or EsRegistroNuevo Then
                cboID.AutoCompletion = False
            Else
                cboID.AutoCompletion = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub btnModificar_Click_1(sender As Object, e As EventArgs) Handles btnModificar.Click

    End Sub

#Region "SELECCION CENTRO"

#End Region

#End Region

#Region "ELECCIONES"

#End Region

End Class
