Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes



Public Class frmMaquinasForm
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

    Friend WithEvents lblNombreIVA As Label
    Friend WithEvents lblCodigoIVA As Label
    Friend WithEvents txtDescripcion As C1.Win.C1Input.C1TextBox
    Friend WithEvents lblPrecio As Label
    Friend WithEvents txtPrecio As C1.Win.C1Input.C1TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents cboID As C1.Win.C1List.C1Combo

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMaquinasForm))
        Me.lblNombreIVA = New System.Windows.Forms.Label()
        Me.lblCodigoIVA = New System.Windows.Forms.Label()
        Me.txtDescripcion = New C1.Win.C1Input.C1TextBox()
        Me.txtPrecio = New C1.Win.C1Input.C1TextBox()
        Me.lblPrecio = New System.Windows.Forms.Label()
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
        CType(Me.txtPrecio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.sbtipo.Location = New System.Drawing.Point(13, 193)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(44, 167)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(326, 167)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(12, 167)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(12, 144)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(326, 144)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(144, 144)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(567, 167)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(144, 167)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(244, 144)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(44, 144)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(567, 144)
        '
        'lblNombreIVA
        '
        Me.lblNombreIVA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNombreIVA.Location = New System.Drawing.Point(12, 36)
        Me.lblNombreIVA.Name = "lblNombreIVA"
        Me.lblNombreIVA.Size = New System.Drawing.Size(64, 14)
        Me.lblNombreIVA.TabIndex = 207
        Me.lblNombreIVA.Text = "Descripció"
        Me.lblNombreIVA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCodigoIVA
        '
        Me.lblCodigoIVA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCodigoIVA.Location = New System.Drawing.Point(12, 12)
        Me.lblCodigoIVA.Name = "lblCodigoIVA"
        Me.lblCodigoIVA.Size = New System.Drawing.Size(80, 15)
        Me.lblCodigoIVA.TabIndex = 206
        Me.lblCodigoIVA.Text = "Codi Màquina"
        Me.lblCodigoIVA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescripcion.Location = New System.Drawing.Point(92, 36)
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Size = New System.Drawing.Size(368, 18)
        Me.txtDescripcion.TabIndex = 1
        Me.txtDescripcion.Tag = Nothing
        '
        'txtPrecio
        '
        Me.txtPrecio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrecio.Location = New System.Drawing.Point(92, 60)
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.Size = New System.Drawing.Size(48, 18)
        Me.txtPrecio.TabIndex = 2
        Me.txtPrecio.Tag = Nothing
        '
        'lblPrecio
        '
        Me.lblPrecio.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPrecio.Location = New System.Drawing.Point(12, 60)
        Me.lblPrecio.Name = "lblPrecio"
        Me.lblPrecio.Size = New System.Drawing.Size(40, 15)
        Me.lblPrecio.TabIndex = 211
        Me.lblPrecio.Text = "Preu"
        Me.lblPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.ItemSize = New System.Drawing.Size(53, 18)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(635, 120)
        Me.TabControl1.TabIndex = 212
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.cboID)
        Me.TabPage1.Controls.Add(Me.txtPrecio)
        Me.TabPage1.Controls.Add(Me.lblNombreIVA)
        Me.TabPage1.Controls.Add(Me.txtDescripcion)
        Me.TabPage1.Controls.Add(Me.lblPrecio)
        Me.TabPage1.Controls.Add(Me.lblCodigoIVA)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(627, 94)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Màquina"
        '
        'cboID
        '
        Me.cboID.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboID.AutoCompletion = True
        Me.cboID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboID.Caption = ""
        Me.cboID.CaptionHeight = 17
        Me.cboID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboID.ColumnCaptionHeight = 17
        Me.cboID.ColumnFooterHeight = 17
        Me.cboID.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboID.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboID.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboID.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboID.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboID.Images.Add(CType(resources.GetObject("cboID.Images"), System.Drawing.Image))
        Me.cboID.ItemHeight = 15
        Me.cboID.Location = New System.Drawing.Point(92, 12)
        Me.cboID.MatchEntryTimeout = CType(100, Long)
        Me.cboID.MaxDropDownItems = CType(15, Short)
        Me.cboID.MaxLength = 32767
        Me.cboID.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboID.Name = "cboID"
        Me.cboID.PropBag = resources.GetString("cboID.PropBag")
        Me.cboID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboID.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboID.Size = New System.Drawing.Size(124, 19)
        Me.cboID.TabIndex = 394
        '
        'frmMaquinasForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(657, 215)
        Me.Controls.Add(Me.TabControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "frmMaquinasForm"
        Me.Text = " Màquines"
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
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
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
        CType(Me.txtPrecio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmMaquinasForm
    Public Shared Function GetInstance() As frmMaquinasForm
        If frmChildForm Is Nothing Then
            frmChildForm = New frmMaquinasForm

        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Public maquinaActual As clsMaquina

#End Region

#Region "INICIALIZAR"

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True

            Me.arrayTextBox = New System.Windows.Forms.Control() {Me.txtDescripcion, Me.txtPrecio}
            tabla = tablaMaquinas
            maquinaActual = New clsMaquina(ds.Tables(tabla), "C", BindingContext)
            HacerBindings()
            'maquinaActual.tabla.AcceptChanges()
            PonerModificables(soloLectura)
            PSBTIPO(maquinaActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub HacerBindings()
        Try
            AñadirBinding(cboID, maquinaActual.dvForm, "CODI")
            AñadirBinding(txtDescripcion, maquinaActual.dvForm, "DESCRI")
            AñadirBinding(txtPrecio, maquinaActual.dvForm, "PREU")
            AñadirBindingCombo(cboID, maquinaActual.dvIdentificadores, CCMaqui, CCMaqui)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "MODIFICAR DB"

    Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnBorrar.Click
        If MessageBox.Show(rm.GetString("BorrarMaquina"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
            editando = True
            maquinaActual.borrar() : ActualizarOrigen()
            editando = False
        End If
    End Sub
    Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnNuevo.Click
        Try
            cargando = True
            EsRegistroNuevo = True
            PonerModificables(modificable)
            PonerControlesNuevo(False)
            PSBTIPO(maquinaActual.centro)
            'cboID.ClearItems()
            maquinaActual.NuevoRegistro()
            PSBTIPO(maquinaActual.centro) : cargando = False
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub ActualizarOrigen()
        Try
            If editando Then
                editando = False
                PonerModificables(soloLectura)
                cboID.Text = maquinaActual.CODI
            End If
            If EsRegistroNuevo Then
                editando = True : PSBTIPO(maquinaActual.centro)
                PonerControlesNuevo(True)
                EsRegistroNuevo = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnActualizar.Click
        Try
            cargando = True
            If maquinaActual.TieneCambios Then
                Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                    Case DialogResult.Cancel
                        cargando = False
                        Exit Sub
                    Case DialogResult.No
                        maquinaActual.tabla.RejectChanges()
                        cargando = False
                        Exit Sub
                End Select
            End If
            maquinaActual.ActualizarOrigen() : ActualizarOrigen()
            PSBTIPO(maquinaActual.centro) : cargando = False

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
                PSBTIPO(maquinaActual.centro)
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

    Private Sub PonerModificables(Optional ByVal b As Boolean = False)
        Try
            PonerMod(b)
            If Not b Then : cboID.DataSource = Nothing
            Else : AñadirBindingCombo(cboID, maquinaActual.dvIdentificadores, CCMaqui, CCMaqui) : maquinaActual.tabla.AcceptChanges() : End If

            cboID.LimitToList = b
            cboID.SuperBack = b
            cboID.ReadOnly = editando ''or esregistronuevo
            ModiNuev(b)

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
    Private Sub btnVerLista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerLista.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmMaquinasLista = frmMaquinasLista.GetInstance(esListado)
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

    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnPrimero.Click
        Try
            cargando = True
            maquinaActual.PrimeroReg() : ActualizarOrigen()
            PSBTIPO(maquinaActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUltimo.Click
        Try
            cargando = True
            maquinaActual.UltimoReg() : ActualizarOrigen()
            PSBTIPO(maquinaActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAnterior.Click
        Try
            cargando = True
            maquinaActual.AnteriorReg() : ActualizarOrigen()
            PSBTIPO(maquinaActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSiguiente.Click
        Try
            cargando = True
            maquinaActual.SiguienteReg() : ActualizarOrigen()
            PSBTIPO(maquinaActual.centro) : cargando = False

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
                maquinaActual.CambiarAReg(general.nz(cboID.WillChangeToValue, ""), iraregistro)
                PSBTIPO(maquinaActual.centro) : cargando = False
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



#End Region

#Region "ELECCIONES"

#End Region

End Class
