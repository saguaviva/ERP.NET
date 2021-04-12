Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Imports C1.Win.C1TrueDBGrid


Public Class frmMuestras
    Inherits aura2k3.frmBase

#Region " Código generado por el Diseñador de Windows Forms "


    'Form reemplaza a Dispose para limpiar la lista de componentes.


    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.

    Friend WithEvents lblDescripcion As Label
    Friend WithEvents lblIvaHilo As Label
    Friend WithEvents lblNotasHilo As Label
    Friend WithEvents lblNombreMuestra As Label
    Friend WithEvents btnElegirCliente As C1.Win.C1Input.C1Button
    Friend WithEvents lblCliente As Label
    Friend WithEvents lblComposicion As Label
    Friend WithEvents lblReferencia As Label
    Friend WithEvents lblTemporada As Label
    Friend WithEvents btnElegirMaquina As C1.Win.C1Input.C1Button
    Friend WithEvents lblMaquina As Label
    Friend WithEvents lblPrecioUnidad As Label
    Friend WithEvents lblTantoPorCientoMargen As Label
    Friend WithEvents comboIVA As C1.Win.C1List.C1Combo

    Public Sub New()

        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent() : Dim tom As SMcMaster.TabOrderManager = New SMcMaster.TabOrderManager(Me) : tom.SetTabOrder(SMcMaster.TabOrderManager.TabScheme.AcrossFirst)

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

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

    Friend WithEvents gpElegirMuestras As System.Windows.Forms.GroupBox
    Friend WithEvents rdoTodasMuestras As System.Windows.Forms.RadioButton
    Friend WithEvents cboSeleccionCliente As C1.Win.C1List.C1Combo
    Friend WithEvents cboID As C1.Win.C1List.C1Combo
    Friend WithEvents rdoVerMuestrasCliente As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnElegirMuestra As C1.Win.C1Input.C1Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents dgCartaColores As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents TabPageMuestra As System.Windows.Forms.TabPage
    Friend WithEvents lblNumeroMuestras As System.Windows.Forms.Label
    Friend WithEvents txtCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtDESCRI As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtPREU As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtREFE As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboNOMCLIENT As C1.Win.C1List.C1Combo
    Friend WithEvents cboNOMMAQUI As C1.Win.C1List.C1Combo
    Friend WithEvents txtMAQUINA As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCOMPO As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtMARGE As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtOBSERV As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTEMP As C1.Win.C1Input.C1TextBox

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMuestras))
        Me.lblNombreMuestra = New System.Windows.Forms.Label()
        Me.btnElegirCliente = New C1.Win.C1Input.C1Button()
        Me.txtCLIENT = New C1.Win.C1Input.C1TextBox()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.lblDescripcion = New System.Windows.Forms.Label()
        Me.txtDESCRI = New C1.Win.C1Input.C1TextBox()
        Me.lblPrecioUnidad = New System.Windows.Forms.Label()
        Me.txtPREU = New C1.Win.C1Input.C1TextBox()
        Me.lblTantoPorCientoMargen = New System.Windows.Forms.Label()
        Me.lblIvaHilo = New System.Windows.Forms.Label()
        Me.txtMARGE = New C1.Win.C1Input.C1TextBox()
        Me.txtOBSERV = New C1.Win.C1Input.C1TextBox()
        Me.lblNotasHilo = New System.Windows.Forms.Label()
        Me.lblComposicion = New System.Windows.Forms.Label()
        Me.txtCOMPO = New C1.Win.C1Input.C1TextBox()
        Me.lblReferencia = New System.Windows.Forms.Label()
        Me.txtREFE = New C1.Win.C1Input.C1TextBox()
        Me.lblTemporada = New System.Windows.Forms.Label()
        Me.txtTEMP = New C1.Win.C1Input.C1TextBox()
        Me.btnElegirMaquina = New C1.Win.C1Input.C1Button()
        Me.lblMaquina = New System.Windows.Forms.Label()
        Me.txtMAQUINA = New C1.Win.C1Input.C1TextBox()
        Me.comboIVA = New C1.Win.C1List.C1Combo()
        Me.rdoTodasMuestras = New System.Windows.Forms.RadioButton()
        Me.rdoVerMuestrasCliente = New System.Windows.Forms.RadioButton()
        Me.gpElegirMuestras = New System.Windows.Forms.GroupBox()
        Me.lblNumeroMuestras = New System.Windows.Forms.Label()
        Me.cboSeleccionCliente = New C1.Win.C1List.C1Combo()
        Me.cboID = New C1.Win.C1List.C1Combo()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnElegirMuestra = New C1.Win.C1Input.C1Button()
        Me.cboNOMCLIENT = New C1.Win.C1List.C1Combo()
        Me.cboNOMMAQUI = New C1.Win.C1List.C1Combo()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageMuestra = New System.Windows.Forms.TabPage()
        Me.dgCartaColores = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
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
        CType(Me.btnElegirCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDESCRI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPREU, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMARGE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCOMPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREFE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTEMP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnElegirMaquina, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMAQUINA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpElegirMuestras.SuspendLayout()
        CType(Me.cboSeleccionCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.btnElegirMuestra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMMAQUI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPageMuestra.SuspendLayout()
        CType(Me.dgCartaColores, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(5, 144)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        '
        'btnVerLista
        '
        '
        'cboSeleccionCentro
        '
        '
        'lblNombreMuestra
        '
        Me.lblNombreMuestra.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNombreMuestra.Location = New System.Drawing.Point(8, 44)
        Me.lblNombreMuestra.Name = "lblNombreMuestra"
        Me.lblNombreMuestra.Size = New System.Drawing.Size(80, 20)
        Me.lblNombreMuestra.TabIndex = 196
        Me.lblNombreMuestra.Text = "Codi"
        Me.lblNombreMuestra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnElegirCliente
        '
        Me.btnElegirCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirCliente.Location = New System.Drawing.Point(140, 21)
        Me.btnElegirCliente.Name = "btnElegirCliente"
        Me.btnElegirCliente.Size = New System.Drawing.Size(24, 19)
        Me.btnElegirCliente.TabIndex = 3
        Me.btnElegirCliente.Text = "..."
        Me.btnElegirCliente.UseVisualStyleBackColor = True
        '
        'txtCLIENT
        '
        Me.txtCLIENT.Location = New System.Drawing.Point(100, 20)
        Me.txtCLIENT.Name = "txtCLIENT"
        Me.txtCLIENT.Size = New System.Drawing.Size(40, 18)
        Me.txtCLIENT.TabIndex = 0
        Me.txtCLIENT.Tag = Nothing
        '
        'lblCliente
        '
        Me.lblCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCliente.Location = New System.Drawing.Point(8, 20)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(80, 20)
        Me.lblCliente.TabIndex = 198
        Me.lblCliente.Text = "Client"
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDescripcion
        '
        Me.lblDescripcion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDescripcion.Location = New System.Drawing.Point(8, 92)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(80, 20)
        Me.lblDescripcion.TabIndex = 202
        Me.lblDescripcion.Text = "Descripció"
        Me.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDESCRI
        '
        Me.txtDESCRI.Location = New System.Drawing.Point(100, 92)
        Me.txtDESCRI.Name = "txtDESCRI"
        Me.txtDESCRI.Size = New System.Drawing.Size(340, 18)
        Me.txtDESCRI.TabIndex = 4
        Me.txtDESCRI.Tag = Nothing
        '
        'lblPrecioUnidad
        '
        Me.lblPrecioUnidad.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPrecioUnidad.Location = New System.Drawing.Point(224, 188)
        Me.lblPrecioUnidad.Name = "lblPrecioUnidad"
        Me.lblPrecioUnidad.Size = New System.Drawing.Size(80, 20)
        Me.lblPrecioUnidad.TabIndex = 13
        Me.lblPrecioUnidad.Text = "Precio Unitat"
        Me.lblPrecioUnidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPREU
        '
        Me.txtPREU.Location = New System.Drawing.Point(312, 188)
        Me.txtPREU.Name = "txtPREU"
        Me.txtPREU.Size = New System.Drawing.Size(60, 18)
        Me.txtPREU.TabIndex = 11
        Me.txtPREU.Tag = Nothing
        '
        'lblTantoPorCientoMargen
        '
        Me.lblTantoPorCientoMargen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTantoPorCientoMargen.Location = New System.Drawing.Point(8, 188)
        Me.lblTantoPorCientoMargen.Name = "lblTantoPorCientoMargen"
        Me.lblTantoPorCientoMargen.Size = New System.Drawing.Size(80, 20)
        Me.lblTantoPorCientoMargen.TabIndex = 206
        Me.lblTantoPorCientoMargen.Text = "% Marge"
        Me.lblTantoPorCientoMargen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIvaHilo
        '
        Me.lblIvaHilo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblIvaHilo.Location = New System.Drawing.Point(8, 212)
        Me.lblIvaHilo.Name = "lblIvaHilo"
        Me.lblIvaHilo.Size = New System.Drawing.Size(80, 20)
        Me.lblIvaHilo.TabIndex = 208
        Me.lblIvaHilo.Text = "IVA"
        Me.lblIvaHilo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMARGE
        '
        Me.txtMARGE.Location = New System.Drawing.Point(100, 188)
        Me.txtMARGE.Name = "txtMARGE"
        Me.txtMARGE.Size = New System.Drawing.Size(64, 18)
        Me.txtMARGE.TabIndex = 10
        Me.txtMARGE.Tag = Nothing
        '
        'txtOBSERV
        '
        Me.txtOBSERV.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOBSERV.Location = New System.Drawing.Point(464, 32)
        Me.txtOBSERV.Multiline = True
        Me.txtOBSERV.Name = "txtOBSERV"
        Me.txtOBSERV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOBSERV.Size = New System.Drawing.Size(488, 208)
        Me.txtOBSERV.TabIndex = 0
        Me.txtOBSERV.Tag = Nothing
        '
        'lblNotasHilo
        '
        Me.lblNotasHilo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNotasHilo.Location = New System.Drawing.Point(460, 8)
        Me.lblNotasHilo.Name = "lblNotasHilo"
        Me.lblNotasHilo.Size = New System.Drawing.Size(100, 15)
        Me.lblNotasHilo.TabIndex = 212
        Me.lblNotasHilo.Text = "Notes"
        Me.lblNotasHilo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblComposicion
        '
        Me.lblComposicion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblComposicion.Location = New System.Drawing.Point(8, 116)
        Me.lblComposicion.Name = "lblComposicion"
        Me.lblComposicion.Size = New System.Drawing.Size(80, 20)
        Me.lblComposicion.TabIndex = 218
        Me.lblComposicion.Text = "Composició"
        Me.lblComposicion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCOMPO
        '
        Me.txtCOMPO.Location = New System.Drawing.Point(100, 116)
        Me.txtCOMPO.Name = "txtCOMPO"
        Me.txtCOMPO.Size = New System.Drawing.Size(178, 18)
        Me.txtCOMPO.TabIndex = 5
        Me.txtCOMPO.Tag = Nothing
        '
        'lblReferencia
        '
        Me.lblReferencia.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblReferencia.Location = New System.Drawing.Point(284, 116)
        Me.lblReferencia.Name = "lblReferencia"
        Me.lblReferencia.Size = New System.Drawing.Size(64, 20)
        Me.lblReferencia.TabIndex = 220
        Me.lblReferencia.Text = "Referència"
        Me.lblReferencia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtREFE
        '
        Me.txtREFE.Location = New System.Drawing.Point(356, 116)
        Me.txtREFE.Name = "txtREFE"
        Me.txtREFE.Size = New System.Drawing.Size(84, 18)
        Me.txtREFE.TabIndex = 6
        Me.txtREFE.Tag = Nothing
        '
        'lblTemporada
        '
        Me.lblTemporada.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTemporada.Location = New System.Drawing.Point(8, 140)
        Me.lblTemporada.Name = "lblTemporada"
        Me.lblTemporada.Size = New System.Drawing.Size(80, 20)
        Me.lblTemporada.TabIndex = 222
        Me.lblTemporada.Text = "Temporada"
        Me.lblTemporada.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTEMP
        '
        Me.txtTEMP.Location = New System.Drawing.Point(100, 140)
        Me.txtTEMP.Name = "txtTEMP"
        Me.txtTEMP.Size = New System.Drawing.Size(84, 18)
        Me.txtTEMP.TabIndex = 7
        Me.txtTEMP.Tag = Nothing
        '
        'btnElegirMaquina
        '
        Me.btnElegirMaquina.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirMaquina.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirMaquina.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirMaquina.Location = New System.Drawing.Point(140, 164)
        Me.btnElegirMaquina.Name = "btnElegirMaquina"
        Me.btnElegirMaquina.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirMaquina.TabIndex = 283
        Me.btnElegirMaquina.Text = "..."
        Me.btnElegirMaquina.UseVisualStyleBackColor = True
        '
        'lblMaquina
        '
        Me.lblMaquina.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblMaquina.Location = New System.Drawing.Point(8, 164)
        Me.lblMaquina.Name = "lblMaquina"
        Me.lblMaquina.Size = New System.Drawing.Size(80, 20)
        Me.lblMaquina.TabIndex = 282
        Me.lblMaquina.Text = "Màquina"
        Me.lblMaquina.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMAQUINA
        '
        Me.txtMAQUINA.Location = New System.Drawing.Point(100, 164)
        Me.txtMAQUINA.Name = "txtMAQUINA"
        Me.txtMAQUINA.Size = New System.Drawing.Size(40, 18)
        Me.txtMAQUINA.TabIndex = 8
        Me.txtMAQUINA.Tag = Nothing
        '
        'comboIVA
        '
        Me.comboIVA.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.comboIVA.AutoSelect = True
        Me.comboIVA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.comboIVA.Caption = ""
        Me.comboIVA.CaptionHeight = 17
        Me.comboIVA.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.comboIVA.ColumnCaptionHeight = 17
        Me.comboIVA.ColumnFooterHeight = 17
        Me.comboIVA.Cursor = System.Windows.Forms.Cursors.Default
        Me.comboIVA.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.comboIVA.EditorBackColor = System.Drawing.SystemColors.Window
        Me.comboIVA.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.comboIVA.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.comboIVA.Images.Add(CType(resources.GetObject("comboIVA.Images"), System.Drawing.Image))
        Me.comboIVA.IntegralHeight = True
        Me.comboIVA.ItemHeight = 13
        Me.comboIVA.Location = New System.Drawing.Point(100, 212)
        Me.comboIVA.MatchEntryTimeout = CType(100, Long)
        Me.comboIVA.MaxDropDownItems = CType(8, Short)
        Me.comboIVA.MaxLength = 0
        Me.comboIVA.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.comboIVA.Name = "comboIVA"
        Me.comboIVA.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.comboIVA.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.comboIVA.Size = New System.Drawing.Size(120, 19)
        Me.comboIVA.TabIndex = 12
        Me.comboIVA.PropBag = resources.GetString("comboIVA.PropBag")
        '
        'rdoTodasMuestras
        '
        Me.rdoTodasMuestras.Checked = True
        Me.rdoTodasMuestras.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoTodasMuestras.Location = New System.Drawing.Point(12, 20)
        Me.rdoTodasMuestras.Name = "rdoTodasMuestras"
        Me.rdoTodasMuestras.Size = New System.Drawing.Size(112, 16)
        Me.rdoTodasMuestras.TabIndex = 0
        Me.rdoTodasMuestras.TabStop = True
        Me.rdoTodasMuestras.Text = "Tots els clients"
        '
        'rdoVerMuestrasCliente
        '
        Me.rdoVerMuestrasCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoVerMuestrasCliente.Location = New System.Drawing.Point(132, 20)
        Me.rdoVerMuestrasCliente.Name = "rdoVerMuestrasCliente"
        Me.rdoVerMuestrasCliente.Size = New System.Drawing.Size(120, 16)
        Me.rdoVerMuestrasCliente.TabIndex = 1
        Me.rdoVerMuestrasCliente.Text = "Del client:"
        '
        'gpElegirMuestras
        '
        Me.gpElegirMuestras.BackColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.gpElegirMuestras.Controls.Add(Me.lblNumeroMuestras)
        Me.gpElegirMuestras.Controls.Add(Me.cboSeleccionCliente)
        Me.gpElegirMuestras.Controls.Add(Me.rdoVerMuestrasCliente)
        Me.gpElegirMuestras.Controls.Add(Me.rdoTodasMuestras)
        Me.gpElegirMuestras.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.gpElegirMuestras.Location = New System.Drawing.Point(8, 8)
        Me.gpElegirMuestras.Name = "gpElegirMuestras"
        Me.gpElegirMuestras.Size = New System.Drawing.Size(816, 44)
        Me.gpElegirMuestras.TabIndex = 289
        Me.gpElegirMuestras.TabStop = False
        Me.gpElegirMuestras.Text = "Selecció de Complements"
        '
        'lblNumeroMuestras
        '
        Me.lblNumeroMuestras.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNumeroMuestras.Location = New System.Drawing.Point(624, 16)
        Me.lblNumeroMuestras.Name = "lblNumeroMuestras"
        Me.lblNumeroMuestras.Size = New System.Drawing.Size(184, 16)
        Me.lblNumeroMuestras.TabIndex = 199
        Me.lblNumeroMuestras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboSeleccionCliente
        '
        Me.cboSeleccionCliente.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboSeleccionCliente.AutoCompletion = True
        Me.cboSeleccionCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboSeleccionCliente.Caption = ""
        Me.cboSeleccionCliente.CaptionHeight = 17
        Me.cboSeleccionCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboSeleccionCliente.ColumnCaptionHeight = 17
        Me.cboSeleccionCliente.ColumnFooterHeight = 17
        Me.cboSeleccionCliente.ColumnHeaders = False
        Me.cboSeleccionCliente.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSeleccionCliente.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboSeleccionCliente.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboSeleccionCliente.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboSeleccionCliente.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboSeleccionCliente.Images.Add(CType(resources.GetObject("cboSeleccionCliente.Images"), System.Drawing.Image))
        Me.cboSeleccionCliente.IntegralHeight = True
        Me.cboSeleccionCliente.ItemHeight = 13
        Me.cboSeleccionCliente.LimitToList = True
        Me.cboSeleccionCliente.Location = New System.Drawing.Point(260, 16)
        Me.cboSeleccionCliente.MatchEntryTimeout = CType(100, Long)
        Me.cboSeleccionCliente.MaxDropDownItems = CType(8, Short)
        Me.cboSeleccionCliente.MaxLength = 0
        Me.cboSeleccionCliente.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboSeleccionCliente.Name = "cboSeleccionCliente"
        Me.cboSeleccionCliente.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboSeleccionCliente.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboSeleccionCliente.Size = New System.Drawing.Size(356, 19)
        Me.cboSeleccionCliente.TabIndex = 2
        Me.cboSeleccionCliente.Visible = False
        Me.cboSeleccionCliente.PropBag = resources.GetString("cboSeleccionCliente.PropBag")
        '
        'cboID
        '
        Me.cboID.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
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
        Me.cboID.IntegralHeight = True
        Me.cboID.ItemHeight = 13
        Me.cboID.Location = New System.Drawing.Point(100, 45)
        Me.cboID.MatchEntryTimeout = CType(100, Long)
        Me.cboID.MaxDropDownItems = CType(15, Short)
        Me.cboID.MaxLength = 10
        Me.cboID.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboID.Name = "cboID"
        Me.cboID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboID.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboID.Size = New System.Drawing.Size(124, 19)
        Me.cboID.TabIndex = 2
        Me.cboID.PropBag = resources.GetString("cboID.PropBag")
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.btnElegirMuestra)
        Me.GroupBox1.Controls.Add(Me.lblCliente)
        Me.GroupBox1.Controls.Add(Me.lblTemporada)
        Me.GroupBox1.Controls.Add(Me.lblNombreMuestra)
        Me.GroupBox1.Controls.Add(Me.lblIvaHilo)
        Me.GroupBox1.Controls.Add(Me.lblComposicion)
        Me.GroupBox1.Controls.Add(Me.cboID)
        Me.GroupBox1.Controls.Add(Me.lblMaquina)
        Me.GroupBox1.Controls.Add(Me.txtCOMPO)
        Me.GroupBox1.Controls.Add(Me.btnElegirMaquina)
        Me.GroupBox1.Controls.Add(Me.txtTEMP)
        Me.GroupBox1.Controls.Add(Me.lblReferencia)
        Me.GroupBox1.Controls.Add(Me.txtMARGE)
        Me.GroupBox1.Controls.Add(Me.txtPREU)
        Me.GroupBox1.Controls.Add(Me.txtCLIENT)
        Me.GroupBox1.Controls.Add(Me.btnElegirCliente)
        Me.GroupBox1.Controls.Add(Me.txtDESCRI)
        Me.GroupBox1.Controls.Add(Me.lblDescripcion)
        Me.GroupBox1.Controls.Add(Me.lblPrecioUnidad)
        Me.GroupBox1.Controls.Add(Me.txtMAQUINA)
        Me.GroupBox1.Controls.Add(Me.lblTantoPorCientoMargen)
        Me.GroupBox1.Controls.Add(Me.comboIVA)
        Me.GroupBox1.Controls.Add(Me.txtREFE)
        Me.GroupBox1.Controls.Add(Me.cboNOMCLIENT)
        Me.GroupBox1.Controls.Add(Me.cboNOMMAQUI)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox1.Location = New System.Drawing.Point(8, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(448, 240)
        Me.GroupBox1.TabIndex = 292
        Me.GroupBox1.TabStop = False
        '
        'btnElegirMuestra
        '
        Me.btnElegirMuestra.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirMuestra.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirMuestra.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirMuestra.Location = New System.Drawing.Point(228, 44)
        Me.btnElegirMuestra.Name = "btnElegirMuestra"
        Me.btnElegirMuestra.Size = New System.Drawing.Size(120, 39)
        Me.btnElegirMuestra.TabIndex = 3
        Me.btnElegirMuestra.Text = "Escollir complement existent d'un altre client"
        Me.btnElegirMuestra.UseVisualStyleBackColor = True
        Me.btnElegirMuestra.Visible = False
        '
        'cboNOMCLIENT
        '
        Me.cboNOMCLIENT.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMCLIENT.AutoSelect = True
        Me.cboNOMCLIENT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMCLIENT.Caption = ""
        Me.cboNOMCLIENT.CaptionHeight = 17
        Me.cboNOMCLIENT.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMCLIENT.ColumnCaptionHeight = 17
        Me.cboNOMCLIENT.ColumnFooterHeight = 17
        Me.cboNOMCLIENT.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMCLIENT.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMCLIENT.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMCLIENT.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMCLIENT.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMCLIENT.Images.Add(CType(resources.GetObject("cboNOMCLIENT.Images"), System.Drawing.Image))
        Me.cboNOMCLIENT.IntegralHeight = True
        Me.cboNOMCLIENT.ItemHeight = 13
        Me.cboNOMCLIENT.Location = New System.Drawing.Point(172, 21)
        Me.cboNOMCLIENT.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMCLIENT.MaxDropDownItems = CType(8, Short)
        Me.cboNOMCLIENT.MaxLength = 0
        Me.cboNOMCLIENT.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMCLIENT.Name = "cboNOMCLIENT"
        Me.cboNOMCLIENT.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMCLIENT.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMCLIENT.RowTracking = False
        Me.cboNOMCLIENT.Size = New System.Drawing.Size(268, 19)
        Me.cboNOMCLIENT.TabIndex = 1
        Me.cboNOMCLIENT.PropBag = resources.GetString("cboNOMCLIENT.PropBag")
        '
        'cboNOMMAQUI
        '
        Me.cboNOMMAQUI.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMMAQUI.AutoSelect = True
        Me.cboNOMMAQUI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMMAQUI.Caption = ""
        Me.cboNOMMAQUI.CaptionHeight = 17
        Me.cboNOMMAQUI.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMMAQUI.ColumnCaptionHeight = 17
        Me.cboNOMMAQUI.ColumnFooterHeight = 17
        Me.cboNOMMAQUI.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMMAQUI.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMMAQUI.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMMAQUI.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMMAQUI.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMMAQUI.Images.Add(CType(resources.GetObject("cboNOMMAQUI.Images"), System.Drawing.Image))
        Me.cboNOMMAQUI.IntegralHeight = True
        Me.cboNOMMAQUI.ItemHeight = 13
        Me.cboNOMMAQUI.Location = New System.Drawing.Point(172, 165)
        Me.cboNOMMAQUI.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMMAQUI.MaxDropDownItems = CType(8, Short)
        Me.cboNOMMAQUI.MaxLength = 0
        Me.cboNOMMAQUI.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMMAQUI.Name = "cboNOMMAQUI"
        Me.cboNOMMAQUI.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMMAQUI.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMMAQUI.Size = New System.Drawing.Size(268, 19)
        Me.cboNOMMAQUI.TabIndex = 9
        Me.cboNOMMAQUI.PropBag = resources.GetString("cboNOMMAQUI.PropBag")
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageMuestra)
        Me.TabControl1.ItemSize = New System.Drawing.Size(44, 18)
        Me.TabControl1.Location = New System.Drawing.Point(8, 52)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(968, 504)
        Me.TabControl1.TabIndex = 293
        '
        'TabPageMuestra
        '
        Me.TabPageMuestra.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.TabPageMuestra.Controls.Add(Me.dgCartaColores)
        Me.TabPageMuestra.Controls.Add(Me.lblNotasHilo)
        Me.TabPageMuestra.Controls.Add(Me.txtOBSERV)
        Me.TabPageMuestra.Controls.Add(Me.GroupBox1)
        Me.TabPageMuestra.Location = New System.Drawing.Point(4, 22)
        Me.TabPageMuestra.Name = "TabPageMuestra"
        Me.TabPageMuestra.Size = New System.Drawing.Size(960, 478)
        Me.TabPageMuestra.TabIndex = 0
        Me.TabPageMuestra.Text = "Complement"
        '
        'dgCartaColores
        '
        Me.dgCartaColores.AllowAddNew = True
        Me.dgCartaColores.AllowDelete = True
        Me.dgCartaColores.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgCartaColores.Caption = "Desglòs de colors per complement i talla"
        Me.dgCartaColores.ExtendRightColumn = True
        Me.dgCartaColores.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgCartaColores.Images.Add(CType(resources.GetObject("dgCartaColores.Images"), System.Drawing.Image))
        Me.dgCartaColores.Location = New System.Drawing.Point(28, 256)
        Me.dgCartaColores.Name = "dgCartaColores"
        Me.dgCartaColores.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgCartaColores.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgCartaColores.PreviewInfo.ZoomFactor = 75.0R
        Me.dgCartaColores.PrintInfo.PageSettings = CType(resources.GetObject("dgCartaColores.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgCartaColores.Size = New System.Drawing.Size(900, 212)
        Me.dgCartaColores.SpringMode = True
        Me.dgCartaColores.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation
        Me.dgCartaColores.TabIndex = 293
        Me.dgCartaColores.Text = "C1TrueDBGrid1"
        Me.dgCartaColores.UseCompatibleTextRendering = False
        Me.dgCartaColores.WrapCellPointer = True
        Me.dgCartaColores.PropBag = resources.GetString("dgCartaColores.PropBag")
        '
        'frmMuestras
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(992, 610)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.gpElegirMuestras)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(696, 520)
        Me.Name = "frmMuestras"
        Me.Text = "Complements"
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
        Me.Controls.SetChildIndex(Me.gpElegirMuestras, 0)
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
        CType(Me.btnElegirCliente, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDESCRI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPREU, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMARGE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCOMPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREFE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTEMP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnElegirMaquina, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMAQUINA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comboIVA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpElegirMuestras.ResumeLayout(False)
        Me.gpElegirMuestras.PerformLayout()
        CType(Me.cboSeleccionCliente, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.btnElegirMuestra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMMAQUI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageMuestra.ResumeLayout(False)
        CType(Me.dgCartaColores, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmMuestras
    Public Shared Function GetInstance() As frmMuestras
        If frmChildForm Is Nothing Then
            frmChildForm = New frmMuestras
        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Public muestraActual As clsMuestra
    Public cambiandoCliente As Boolean = False
    Public cambiandoMaquina As Boolean = False

#End Region

#Region "INICIALIZAR"

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True
            Me.arrayGrids = New System.Windows.Forms.Control() {Me.dgCartaColores}
            Me.arrayBotones = New System.Windows.Forms.Control() {Me.btnElegirCliente, Me.btnElegirMaquina, Me.btnElegirMuestra}
            Me.arrayEtiquetas = New System.Windows.Forms.Control() {Me.lblCliente, Me.lblComposicion, Me.lblDescripcion, Me.lblIvaHilo, Me.lblMaquina, Me.lblNombreMuestra, Me.lblNotasHilo, Me.lblPrecioUnidad, Me.lblReferencia, Me.lblTantoPorCientoMargen, Me.lblTemporada}
            Me.arrayTabPages = New System.Windows.Forms.Control() {Me.TabPageMuestra}
            Me.arrayCombos = New System.Windows.Forms.Control() {Me.cboNOMCLIENT, Me.cboNOMMAQUI, Me.comboIVA}    '
            Me.arrayTextBox = New System.Windows.Forms.Control() {Me.txtCLIENT, Me.txtMAQUINA, Me.txtCOMPO, Me.txtDESCRI, Me.txtOBSERV, Me.txtPREU, Me.txtREFE, Me.txtMARGE, Me.txtTEMP}

            ACN()
            tabla = tablaMuestras
            muestraActual = New clsMuestra(ds.Tables(tabla), empresaPorDefecto, BindingContext)
            HacerBindings()
            IniciarCartaColores()
            'muestraActual.tabla.AcceptChanges()
            PonerModificables(soloLectura)
            lblNumeroMuestras.Text = rm.GetString("NUMCOMPLEMENTOS") & " " & muestraActual.dtIdentificadores.DefaultView.Count
            CCN()
            PonerHandlersErroresParaGrids()
            btnSiguiente.Focus()
            PSBTIPO(muestraActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub HacerBindings()
        Try
            HacerBindingsTodos(muestraActual.dvForm)
            cboID.DataBindings.Add(New Binding("Text", muestraActual.dvForm, "CODI"))
            AñadirBindingCombo(cboID, muestraActual.dvIdentificadores, CCMuestra, CCMuestra)
            'OcultarMostrarColumnaCbo(cboID, "CENTRO", False)
            'OcultarMostrarColumnaCbo(cboID, "CLIENT", False)

            CrearBindingIVA(comboIVA, muestraActual.dvForm, True)

            AñadirBindingCombo(cboNOMCLIENT, muestraActual.dtClients, CCClients, CNClients)
            AñadirBindingCombo(cboNOMMAQUI, muestraActual.dtMaqui, CCMaqui, CNMaqui)

            dgCartaColores.SetDataBinding(muestraActual.cartaColores.dvForm, "")

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "MODIFICAR DB"

    Private Sub ActualizarOrigen()
        Try
            If editando Then
                editando = False
                PonerModificables(soloLectura)
            End If
            If EsRegistroNuevo Then
                editando = True : PSBTIPO(muestraActual.centro)
                PonerControlesNuevo(True)
                dgCartaColores.Visible = True
                gpElegirMuestras.Visible = True
                EsRegistroNuevo = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnActualizar.Click
        Try
            cargando = True
            If muestraActual.TieneCambios Then
                Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                    Case DialogResult.Cancel
                        cargando = False
                        Exit Sub
                    Case DialogResult.No
                        muestraActual.tabla.RejectChanges()
                        muestraActual.cartaColores.tabla.RejectChanges()
                        cargando = False
                        Exit Sub
                End Select
            End If
            If cboID.Text = "" Then
                MessageBox.Show(rm.GetString("NOMBRECOMPLEMENTOINCORRECTO"), "Error", MessageBoxButtons.OK)
                cboID.Focus()
            Else
                cargando = True
                muestraActual.ActualizarOrigen() : ActualizarOrigen()
                PSBTIPO(muestraActual.centro) : CCN()
            End If
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnBorrar.Click
        Try
            If MessageBox.Show(rm.GetString("BORRARCOMPLEMENTO"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                editando = True
                muestraActual.borrar() : ActualizarOrigen()
                editando = False
            End If

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
                PSBTIPO(muestraActual.centro)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnNuevo.Click
        Try
            rdoTodasMuestras.Checked = True
            cargando = True
            EsRegistroNuevo = True
            dgCartaColores.Visible = False
            gpElegirMuestras.Visible = False
            PonerModificables(modificable)
            PonerControlesNuevo(False)
            cboID.ClearItems()
            muestraActual.NuevoRegistro()
            PSBTIPO(muestraActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ESPECÍFICO"

#Region "CARTA COLORES"

    Private Sub IniciarCartaColores()
        Dim i As Integer = 0
        Try
            dgCartaColores.SetDataBinding(muestraActual.cartaColores.dvForm, "")
            OcultarColumnasDG(dgCartaColores)
            i = 0
            PPCol2("COLOR", dgCartaColores, rm.GetString("COLOR"), "", True, 120, False,
                    PresentationEnum.Normal, False, 120, i, False)
            i = i + 1
            PPCol2("TALLAL", dgCartaColores, rm.GetString("TALLAL"), "", True, 50, False,
                    PresentationEnum.Normal, False, 50, i, False)
            i = i + 1
            PPCol2("TALLAH", dgCartaColores, rm.GetString("TALLAH"), "", True, 50, False,
                    PresentationEnum.Normal, False, 50, i, False)
            i = i + 1
            PPCol2("TALLA", dgCartaColores, rm.GetString("TALLA"), "", True, 120, False, _
                    PresentationEnum.Normal, False, 120, i, False)
            i = i + 1
            PPCol2("DESCRI", dgCartaColores, rm.GetString("DESCRIPCION"), "", True, 275, False,
                    PresentationEnum.Normal, False, 275, i, False)
            i = i + 1
            PPCol2("NCCODE", dgCartaColores, rm.GetString("NCCODE"), "", True, 80, False,
                    PresentationEnum.Normal, False, 80, i, False)
            i = i + 1
            PPCol2("COST", dgCartaColores, rm.GetString("COSTE"), "", True, 70, False, _
                    PresentationEnum.Normal, False, 70, i, False)
            i = i + 1
            PPCol2("VENDA", dgCartaColores, rm.GetString("PRECIOVENTA"), "", True, 70, False, _
                    PresentationEnum.Normal, False, 70, i, False)
            i = i + 1
            PPCol2("DESGLOSEMUESTRA", dgCartaColores, rm.GetString("VERDESGLOSE"), "", True, 25, False,
                    PresentationEnum.Normal, True, 25, i, False)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

    Private Sub ClickDesgloseMuestra()
        Dim f As New frmDesgloseMuestraColorMaquina
        Try
            If editando Then
                With muestraActual
                    dgCartaColores.EditActive = True
                    f.muestra = muestraActual
                    f.ShowDialog()
                    txtCLIENT.Focus()
                    dgCartaColores.Focus()
                    muestraActual.ActualizarOrigen()
                    f = Nothing
                    dgCartaColores.EditActive = False

                End With
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN() : f = Nothing
        End Try
        'MessageBox.Show(("row " + e.RowIndex.ToString() + "  col " + e.ColIndex.ToString() + " clicked."))
    End Sub
    Private Sub dgCartaColores_Click(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgCartaColores.ButtonClick

        Try
            If editando And Not cargando Then
                cargando = True
                Select Case e.Column.DataColumn.DataField
                    Case "DESGLOSEMUESTRA"
                        'muestraActual.ActualizarOrigen()
                        'dgCartaColores.EditActive = True
                        ClickDesgloseMuestra()
                End Select
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CopiarActualPonerColores(ByVal codigo As String, ByVal cliente As Integer, ByVal centro As Char)
        Dim cmdSel As MySqlCommand
        Dim SQL As String
        Dim dar As MySqlDataReader
        Dim dr As DataRow
        Try
            ACN()
            cmdSel = New MySqlCommand("SELECT * FROM TALLA WHERE MOSTRA = """ & codigo & """ AND CLIENT = " & cliente & " AND CENTRO = """ & centro & """ ", cnn)
            dar = cmdSel.ExecuteReader
            While dar.Read
                dr = muestraActual.cartaColores.tabla.NewRow
                dr("CLIENT") = muestraActual.CLIENT
                dr("MOSTRA") = dar("MOSTRA")
                dr("COLOR") = dar("COLOR")
                dr("TALLA") = dar("TALLA")
                dr("DESCRI") = dar("DESCRI")
                dr("VENDA") = dar("VENDA")
                dr("COST") = dar("COST")
                dr("CENTRO") = dar("CENTRO")
                muestraActual.cartaColores.tabla.Rows.Add(dr)
            End While
            dar.Close()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : CCN()
        End Try
    End Sub
    Private Sub CopiarMaq(ByVal codigo As String, ByVal cliente As Integer, ByVal centro As Char)
        Dim cmdSel As MySqlCommand
        Dim cmdIns As MySqlCommand
        Dim dar As MySqlDataReader
        Dim cnInsert As New MySqlConnection
        Dim sql As String
        Try
            cmdSel = New MySqlCommand("SELECT * FROM " & tablaMaq & "  WHERE (" & _
                            " MOSTRA = """ & codigo & """ AND " & _
                            " client = " & cliente & " AND " & _
                            " CENTRO = """ & centro & """)", cnn)
            ' ACN()
            dar = cmdSel.ExecuteReader

            cnInsert.ConnectionString = cnn.ConnectionString

            cmdIns = New MySqlCommand("INSERT INTO " & tablaMaq & " (" & _
                            " CLIENT, " & _
                            " MOSTRA, " & _
                            " DATA, " & _
                            " NOTAS, " & _
                            " DISCO, " & _
                            " TALLA, " & _
                            " AGULLES, " & _
                            " DAVANT, " & _
                            " DARRERA, " & _
                            " VELOSITAT, " & _
                            " TEMPS, " & _
                            " IMPORT, " & _
                            " COLOR," & _
                            " CORTES," & _
                            " CENTRO) VALUES (", cnInsert)

            sql = cmdIns.CommandText
            cnInsert.Open()
            While dar.Read
                cmdIns.CommandText = sql
                cmdIns.CommandText = cmdIns.CommandText & """" & muestraActual.dvForm(PosActual(muestraActual.dvForm)).Item("CLIENT") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("MOSTRA") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("DATA") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("NOTAS") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("DISCO") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("TALLA") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("AGULLES") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("DAVANT") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("DARRERA") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("VELOSITAT") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("TEMPS") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("IMPORT") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("COLOR") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("CORTES") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("CENTRO") & """)"

                cmdIns.ExecuteNonQuery()

            End While
            cnInsert.Close()
            dar.Close()

            'CCN()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub CopiarColores(ByVal codigo As String, ByVal cliente As Integer, ByVal centro As Char)
        Dim cmdSel As MySqlCommand
        Dim cmdIns As MySqlCommand
        Dim cnInsert As New MySqlConnection
        Dim dar As MySqlDataReader
        Dim sql As String
        Try
            cnInsert.ConnectionString = cnn.ConnectionString
            cmdSel = New MySqlCommand("SELECT * FROM " & tablaColores & " WHERE (mostra = """ & codigo & """ AND client = """ & cliente & """)", cnn)
            cmdIns = New MySqlCommand("INSERT INTO " & tablaColores & " (" & _
               " CLIENT, " & _
               " MOSTRA, " & _
               " GRADUACION, " & _
               " TALLA, " & _
               " LINEA, " & _
               " COLOR, " & _
               " FIL, " & _
               " TEIXIT, " & _
               " PROVE, " & _
               " CAPS, " & _
               " PASSADES, " & _
               " CONSUM, " & _
               " PREU, " & _
               " IMPORT, " & _
               " COLORM," & _
               " CENTRO" & _
               " ) VALUES (", cnInsert)

            sql = cmdIns.CommandText
            ACN()
            dar = cmdSel.ExecuteReader

            cnInsert.Open()
            While dar.Read
                cmdIns.CommandText = sql
                cmdIns.CommandText = cmdIns.CommandText & """" & muestraActual.dvForm(PosActual(muestraActual.dvForm)).Item("CLIENT") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("MOSTRA") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("GRADUACION") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("TALLA") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("LINEA") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("COLOR") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("FIL") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("TEIXIT") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("PROVE") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("CAPS") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("PASSADES") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("CONSUM") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("PREU") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("IMPORT") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("COLORM") & ""","
                cmdIns.CommandText = cmdIns.CommandText & """" & dar("CENTRO") & """)"
                cmdIns.ExecuteNonQuery()

            End While
            CCN()
            cnInsert.Close()
            dar.Close()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub CopiarActual(ByVal codigo As String, ByVal cliente As Integer, ByVal centro As Char)
        Dim cmdSel As MySqlCommand
        Dim SQL As String
        Dim dar As MySqlDataReader
        Dim dr As DataRow
        Try
            cmdSel = New MySqlCommand("SELECT * FROM MOSTRES WHERE CODI = """ & codigo & """ AND CLIENT = " & cliente & "", cnn)
            ACN()
            dar = cmdSel.ExecuteReader
            While dar.Read
                With muestraActual.dvForm(PosActual(muestraActual.dvForm))
                    .Item("CODI") = dar("CODI")
                    .Item("CLIENT") = muestraActual.CLIENT
                    .Item("DESCRI") = dar("DESCRI")
                    .Item("REFE") = dar("REFE")
                    .Item("TEMP") = dar("TEMP")
                    .Item("MAQUINA") = dar("MAQUINA")
                    .Item("MARGE") = dar("MARGE")
                    .Item("IVA") = dar("IVA")
                    .Item("OBSERV") = dar("OBSERV")
                    .Item("COMPO") = dar("COMPO")
                    .Item("PREU") = dar("PREU")
                    .Item("CENTRO") = dar("CENTRO")
                End With
            End While
            dar.Close()
            'CCN()
            CopiarActualPonerColores(codigo, cliente, centro)
            muestraActual.ActualizarOrigen()
            CopiarMaq(codigo, cliente, centro)
            CopiarColores(codigo, cliente, centro)
            muestraActual.ActualizarOrigen()
            CCN()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
            dar.Close()
            CCN()
        End Try
    End Sub

#End Region

#Region "IMPRESIÓN"

#End Region

#Region "COMUNES"

    Private Sub PonerControlesNuevo(ByVal b As Boolean)
        Try

            If Not b Then
                btnActualizar.Text = rm.GetString("CONFIRMAR")
            Else
                btnActualizar.Text = rm.GetString("ACTUALIZAR")
            End If
            ModiNuev(b)
            cboID.AutoCompletion = Not editando
            btnElegirMuestra.Visible = Not b
            gpElegirMuestras.Visible = b

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerModificables(Optional ByVal b As Boolean = False)
        Try
            PonerMod(b)
            If Not b Then : cboID.DataSource = Nothing
            Else : AñadirBindingCombo(cboID, muestraActual.dvIdentificadores, CCMuestra, CCMuestra) : muestraActual.tabla.AcceptChanges()
            End If
            cboID.LimitToList = b
            cboID.SuperBack = b
            cboID.ReadOnly = editando ''or esregistronuevo
            ModiNuev(b)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnVerLista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerLista.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmMuestrasLista = frmMuestrasLista.GetInstance(esListado, muestraActual.centro)
            f.MdiParent = Me.MdiParent
            AddHandler f.Closed, AddressOf CType(Me.MdiParent, frmPrincipal).childCerrado
            AddHandler f.Load, AddressOf CType(Me.MdiParent, frmPrincipal).childAbierto : AddHandler f.Activated, AddressOf CType(Me.MdiParent, frmPrincipal).childOcultandoMostrando
            f.Show()
            f.BringToFront()
            Cursor = Cursors.Default
            PSBTIPO(muestraActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnRecargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecargar.Click
        Try

            cargando = True
            cboID.DataBindings.Clear()
            Dim dr As DataRow = ObtenerFiladeColumnasC1(cboID.Columns, muestraActual.dtIdentificadores)
            If IsDBNull(dr("CLIENT")) Or IsDBNull(dr("CODI")) Or IsDBNull(dr("centro")) Or IsDBNull(dr("descri")) Or IsDBNull(dr("nomclient")) Then
                dr("CLIENT") = txtCLIENT.Text
                dr("CODI") = cboID.Text
                dr("centro") = cboSeleccionCentro.SelectedValue
                dr("descri") = txtDESCRI.Text
                dr("nomclient") = cboNOMCLIENT.Text
            End If
            muestraActual.CambiarAReg(dr, iraRegistroFila)
            cboID.DataBindings.Add(New Binding("Text", muestraActual.dvForm, "CODI"))
            AñadirBindingCombo(cboID, muestraActual.dvIdentificadores, CCMuestra, CCMuestra)
            IniciarCartaColores()

            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub

#End Region

#Region "DESPLAZARSE"

    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnPrimero.Click
        Try
            cargando = True
            muestraActual.PrimeroReg() : ActualizarOrigen()
            IniciarCartaColores()
            PSBTIPO(muestraActual.centro) : cargando = False : CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUltimo.Click
        Try
            cargando = True
            muestraActual.UltimoReg() : ActualizarOrigen()
            IniciarCartaColores()
            PSBTIPO(muestraActual.centro) : cargando = False : CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAnterior.Click
        Try
            cargando = True
            muestraActual.AnteriorReg() : ActualizarOrigen()
            IniciarCartaColores()
            PSBTIPO(muestraActual.centro) : cargando = False : CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSiguiente.Click
        Try
            cargando = True
            muestraActual.SiguienteReg() : ActualizarOrigen()
            IniciarCartaColores()
            PSBTIPO(muestraActual.centro) : cargando = False : CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "SELECCION REGISTRO"

    Public Sub ElegirCliente(ByVal cliente As Integer)
        Try
            muestraActual.ClienteSeleccionado = cliente
            IniciarCartaColores()
            'muestraActual.tabla.AcceptChanges()
            PSBTIPO(muestraActual.centro)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub
    Private Sub btnElegirMuestra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirMuestra.Click
        Dim f As frmMuestrasLista = frmMuestrasLista.GetInstance(esEleccion, muestraActual.centro)
        Try
            cargando = True
            Cursor = Cursors.WaitCursor
            f.ShowDialog()
            Cursor = Cursors.Default
            If Not f.dr Is Nothing Then CopiarActual(f.dr("CODI"), f.dr("CLIENT"), f.dr("CENTRO"))
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboSeleccionCliente_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSeleccionCliente.SelectedValueChanged
        Try
            If consulta() Then
                cargando = True
                ElegirCliente(nzn(cboSeleccionCliente.WillChangeToValue, 0))
                lblNumeroMuestras.Text = rm.GetString("NUMCOMPLEMENTOS") & " " & muestraActual.dtIdentificadores.DefaultView.Count
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub rdoVerTodasMuestras_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoTodasMuestras.CheckedChanged
        Try
            If consulta() Then
                cargando = True
                If rdoTodasMuestras.Checked = True Then
                    muestraActual.ClienteSeleccionado = -1
                    btnPrimero_Click(Nothing, Nothing)
                    'muestraActual.tabla.AcceptChanges()
                    lblNumeroMuestras.Text = rm.GetString("NUMCOMPLEMENTOS") & " " & muestraActual.dtIdentificadores.DefaultView.Count
                End If
                PSBTIPO(muestraActual.centro) : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub VerMuestrasDeCliente_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoVerMuestrasCliente.CheckedChanged
        Try
            If Not cargando And consulta() Then
                cargando = True
                If rdoVerMuestrasCliente.Checked = True Then
                    CargarClientesConMuestras()
                    muestraActual.ActualizarOrigen()
                    muestraActual.tabla.Clear()
                    'CargarClientesConHilos()
                    cboSeleccionCentro.Focus()
                    cboSeleccionCliente.Visible = True
                Else
                    cboSeleccionCliente.Visible = False
                End If
                PSBTIPO(muestraActual.centro) : cargando = False : CCN()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CargarClientesConMuestras()
        Try
            Dim cmd As New MySqlCommand("SELECT DISTINCT CLIENT as CODI, CLIENTS.NOM FROM MOSTRES INNER JOIN CLIENTS ON CLIENTS.CODI = MOSTRES.CLIENT ORDER BY NOM", cnn)
            Dim dar As MySqlDataReader
            Dim dr As DataRow
            Dim dc1 As New DataColumn("CODI")
            Dim dc2 As New DataColumn("NOM")
            ACN()
            dar = cmd.ExecuteReader
            muestraActual.dtClienteConMuestras = New DataTable
            muestraActual.dtClienteConMuestras.Columns.Clear()
            muestraActual.dtClienteConMuestras.Columns.Add(dc1)
            muestraActual.dtClienteConMuestras.Columns.Add(dc2)

            While dar.Read
                dr = muestraActual.dtClienteConMuestras.NewRow
                dr("CODI") = dar("CODI")
                dr("NOM") = dar("NOM")
                muestraActual.dtClienteConMuestras.Rows.Add(dr)
            End While
            dar.Close()
            CCN()

            AñadirBindingCombo(cboSeleccionCliente, muestraActual.dtClienteConMuestras, CCClients, CNClients)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboID_RowChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboID.RowChange
        Try
            If consulta() Then
                cargando = True
                cboID.DataBindings.Clear()
                muestraActual.CambiarAReg(ObtenerFiladeColumnasC1(cboID.Columns, muestraActual.dtIdentificadores), iraRegistroFila)
                AñadirBinding(cboID, muestraActual.dvForm, "CODI")
                AñadirBindingCombo(cboID, muestraActual.dvIdentificadores, CCMuestra, CCMuestra)
                IniciarCartaColores()
                PSBTIPO(muestraActual.centro) : cargando = False : CCN()
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

    Private Sub btnElegirCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirCliente.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmClientesLista = frmClientesLista.GetInstance(esEleccion)
            f.ShowDialog()
            If Not f.dr Is Nothing Then cambiandoCliente = True : muestraActual.CLIENT = f.dr("CODI")
            Cursor = Cursors.Default
            f = Nothing
            cambiandoCliente = False
            PSBTIPO(muestraActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnElegirMaquina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirMaquina.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmMaquinasLista = frmMaquinasLista.GetInstance(esEleccion)
            f.ShowDialog()
            Cursor = Cursors.Default
            If Not f.dr Is Nothing Then cambiandoMaquina = True : muestraActual.MAQUINA = f.dr("CODI")
            f = Nothing
            cambiandoMaquina = False
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboNombreCliente_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMCLIENT.SelectedValueChanged
        Try
            If PuedoModificar() Then
                cargando = True
                If Not cambiandoCliente Then muestraActual.CLIENT = nzn(cboNOMCLIENT.WillChangeToValue, 0)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboNombreMaquina_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMMAQUI.SelectedValueChanged
        Try
            If PuedoModificar() And Not cambiandoMaquina Then cargando = True : muestraActual.MAQUINA = nzn(cboNOMMAQUI.WillChangeToValue, 0) : cargando = False

            Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub comboIVA_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles comboIVA.SelectedValueChanged
        Try
            If PuedoModificar() Then
                cargando = True
                muestraActual.IVA = comboIVA.WillChangeToValue
                PSBTIPO(muestraActual.centro) : cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtCodigoCliente_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCLIENT.Validated
        Try
            If PuedoModificar() Then cargando = True : muestraActual.CLIENT = nzn(txtCLIENT.Text, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtCodigoMaquina_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMAQUINA.Validated
        Try
            If PuedoModificar() Then cargando = True : muestraActual.MAQUINA = nzn(txtMAQUINA.Text, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

    Private Sub txtMARGE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMARGE.Validated
        Try
            If PuedoModificar() Then
                muestraActual.cambioMargen()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub dgCartaColores_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgCartaColores.BeforeDelete
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
    Private Sub cboSeleccion_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSeleccionCentro.SelectionChangeCommitted
        Try
            If consulta() Then
                cargando = True
                muestraActual.cambioCentro(general.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto), primero)
                lblNumeroMuestras.Text = rm.GetString("NUMCOMPLEMENTOS") & " " & muestraActual.dtIdentificadores.DefaultView.Count
                cboSeleccionCentro.SelectedValue = muestraActual.centro
                AutoSizeCC(cboID)
                'tejidoActual.tabla.AcceptChanges()
                cargando = False
            Else
                If EsRegistroNuevo And Not cargando Then
                    cargando = True
                    muestraActual.centro = general.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto)
                    muestraActual.tabla.Clear()
                    muestraActual.NuevoRegistro()
                    PSBTIPO(muestraActual.centro)
                    cargando = False
                End If
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
End Class
