Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones


Public Class frmEmpresas
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
    Friend WithEvents txtTEL2 As C1.Win.C1Input.C1TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblWeb As System.Windows.Forms.Label
    Friend WithEvents Button3 As C1.Win.C1Input.C1Button
    Friend WithEvents Button2 As C1.Win.C1Input.C1Button
    Friend WithEvents txtEmail2 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtEMAIL1 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtWEB As C1.Win.C1Input.C1TextBox
    Friend WithEvents lblNIF As System.Windows.Forms.Label
    Friend WithEvents lblEmails As System.Windows.Forms.Label
    Friend WithEvents lblPais As System.Windows.Forms.Label
    Friend WithEvents txtFAX As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtPOB As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtDOM As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtID As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtNIF As C1.Win.C1Input.C1TextBox
    Friend WithEvents lblCP As System.Windows.Forms.Label
    Friend WithEvents lblFax As System.Windows.Forms.Label
    Friend WithEvents lblPoblacion As System.Windows.Forms.Label
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents lblTelefonos As System.Windows.Forms.Label
    Friend WithEvents lblProvincia As System.Windows.Forms.Label
    Friend WithEvents btnElegirCliente As C1.Win.C1Input.C1Button
    Friend WithEvents lblDomicilio As System.Windows.Forms.Label
    Friend WithEvents txtPROV As C1.Win.C1Input.C1TextBox
    Friend WithEvents lblCodigoCliente As System.Windows.Forms.Label
    Friend WithEvents txtOBSERV As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCP As C1.Win.C1Input.C1TextBox
    Friend WithEvents Button1 As C1.Win.C1Input.C1Button
    Friend WithEvents txtPAIS As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTEL1 As C1.Win.C1Input.C1TextBox
    Friend WithEvents chkEmpresaPorDefecto As System.Windows.Forms.CheckBox
    Friend WithEvents pibLOGO As System.Windows.Forms.PictureBox
    Friend WithEvents btnCanviarLogo As C1.Win.C1Input.C1Button
    Friend WithEvents C1PrintDocument1 As C1.C1Preview.C1PrintDocument
    Friend WithEvents txtDESCRI As C1.Win.C1Input.C1TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtTEL2 = New C1.Win.C1Input.C1TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblWeb = New System.Windows.Forms.Label()
        Me.Button3 = New C1.Win.C1Input.C1Button()
        Me.Button2 = New C1.Win.C1Input.C1Button()
        Me.txtEmail2 = New C1.Win.C1Input.C1TextBox()
        Me.txtEMAIL1 = New C1.Win.C1Input.C1TextBox()
        Me.txtWEB = New C1.Win.C1Input.C1TextBox()
        Me.lblNIF = New System.Windows.Forms.Label()
        Me.txtPAIS = New C1.Win.C1Input.C1TextBox()
        Me.lblEmails = New System.Windows.Forms.Label()
        Me.lblPais = New System.Windows.Forms.Label()
        Me.txtFAX = New C1.Win.C1Input.C1TextBox()
        Me.txtPOB = New C1.Win.C1Input.C1TextBox()
        Me.txtDOM = New C1.Win.C1Input.C1TextBox()
        Me.txtID = New C1.Win.C1Input.C1TextBox()
        Me.txtTEL1 = New C1.Win.C1Input.C1TextBox()
        Me.txtNIF = New C1.Win.C1Input.C1TextBox()
        Me.lblCP = New System.Windows.Forms.Label()
        Me.lblFax = New System.Windows.Forms.Label()
        Me.lblPoblacion = New System.Windows.Forms.Label()
        Me.lblNombre = New System.Windows.Forms.Label()
        Me.lblTelefonos = New System.Windows.Forms.Label()
        Me.lblProvincia = New System.Windows.Forms.Label()
        Me.btnElegirCliente = New C1.Win.C1Input.C1Button()
        Me.lblDomicilio = New System.Windows.Forms.Label()
        Me.txtPROV = New C1.Win.C1Input.C1TextBox()
        Me.lblCodigoCliente = New System.Windows.Forms.Label()
        Me.txtOBSERV = New C1.Win.C1Input.C1TextBox()
        Me.txtCP = New C1.Win.C1Input.C1TextBox()
        Me.Button1 = New C1.Win.C1Input.C1Button()
        Me.chkEmpresaPorDefecto = New System.Windows.Forms.CheckBox()
        Me.pibLOGO = New System.Windows.Forms.PictureBox()
        Me.btnCanviarLogo = New C1.Win.C1Input.C1Button()
        Me.txtDESCRI = New C1.Win.C1Input.C1TextBox()
        Me.C1PrintDocument1 = New C1.C1Preview.C1PrintDocument()
        CType(Me.txtTEL2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmail2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEMAIL1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWEB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPAIS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFAX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPOB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTEL1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNIF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPROV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pibLOGO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDESCRI, System.ComponentModel.ISupportInitialize).BeginInit()
   
        Me.SuspendLayout()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.sbtipo.Location = New System.Drawing.Point(5, 1090)
        Me.sbtipo.Size = New System.Drawing.Size(88, 14)
        Me.sbtipo.Text = ""
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(60, 1077)
        Me.btnRecargar.Size = New System.Drawing.Size(90, 18)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(330, 1077)
        Me.btnSiguiente.Size = New System.Drawing.Size(32, 18)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(28, 1077)
        Me.btnAnterior.Size = New System.Drawing.Size(32, 18)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(28, 1058)
        Me.btnPrimero.Size = New System.Drawing.Size(32, 19)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(330, 1058)
        Me.btnUltimo.Size = New System.Drawing.Size(32, 19)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(150, 1058)
        Me.btnModificar.Size = New System.Drawing.Size(98, 19)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(2082, 1077)
        Me.btnTancar.Size = New System.Drawing.Size(72, 18)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(150, 1077)
        Me.btnBorrar.Size = New System.Drawing.Size(98, 18)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(248, 1058)
        Me.btnNuevo.Size = New System.Drawing.Size(82, 37)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(60, 1058)
        Me.btnActualizar.Size = New System.Drawing.Size(90, 19)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(2082, 1058)
        Me.btnVerLista.Size = New System.Drawing.Size(72, 19)
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.Location = New System.Drawing.Point(280, 7)
        '
        'txtTEL2
        '
        Me.txtTEL2.Location = New System.Drawing.Point(328, 192)
        Me.txtTEL2.MaxLength = 15
        Me.txtTEL2.Name = "txtTEL2"
        Me.txtTEL2.Size = New System.Drawing.Size(145, 20)
        Me.txtTEL2.TabIndex = 426
        Me.txtTEL2.Tag = Nothing
        '
        'Label2
        '
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(252, 192)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 20)
        Me.Label2.TabIndex = 427
        Me.Label2.Text = "Telèfon 2"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblWeb
        '
        Me.lblWeb.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblWeb.Location = New System.Drawing.Point(16, 292)
        Me.lblWeb.Name = "lblWeb"
        Me.lblWeb.Size = New System.Drawing.Size(80, 20)
        Me.lblWeb.TabIndex = 424
        Me.lblWeb.Text = "Web"
        Me.lblWeb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button3
        '
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button3.Location = New System.Drawing.Point(400, 292)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(24, 19)
        Me.Button3.TabIndex = 423
        Me.Button3.TabStop = False
        Me.Button3.Text = "..."
        '
        'Button2
        '
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button2.Location = New System.Drawing.Point(328, 240)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(24, 18)
        Me.Button2.TabIndex = 422
        Me.Button2.TabStop = False
        Me.Button2.Text = "..."
        '
        'txtEmail2
        '
        Me.txtEmail2.Location = New System.Drawing.Point(104, 264)
        Me.txtEmail2.MaxLength = 40
        Me.txtEmail2.Name = "txtEmail2"
        Me.txtEmail2.Size = New System.Drawing.Size(224, 20)
        Me.txtEmail2.TabIndex = 406
        Me.txtEmail2.Tag = Nothing
        '
        'txtEMAIL1
        '
        Me.txtEMAIL1.Location = New System.Drawing.Point(104, 240)
        Me.txtEMAIL1.MaxLength = 40
        Me.txtEMAIL1.Name = "txtEMAIL1"
        Me.txtEMAIL1.Size = New System.Drawing.Size(224, 20)
        Me.txtEMAIL1.TabIndex = 405
        Me.txtEMAIL1.Tag = Nothing
        '
        'txtWEB
        '
        Me.txtWEB.Location = New System.Drawing.Point(104, 292)
        Me.txtWEB.MaxLength = 40
        Me.txtWEB.Name = "txtWEB"
        Me.txtWEB.Size = New System.Drawing.Size(296, 20)
        Me.txtWEB.TabIndex = 407
        Me.txtWEB.Tag = Nothing
        '
        'lblNIF
        '
        Me.lblNIF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNIF.Location = New System.Drawing.Point(16, 116)
        Me.lblNIF.Name = "lblNIF"
        Me.lblNIF.Size = New System.Drawing.Size(80, 20)
        Me.lblNIF.TabIndex = 420
        Me.lblNIF.Text = "NIF"
        Me.lblNIF.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPAIS
        '
        Me.txtPAIS.Location = New System.Drawing.Point(104, 168)
        Me.txtPAIS.MaxLength = 30
        Me.txtPAIS.Name = "txtPAIS"
        Me.txtPAIS.Size = New System.Drawing.Size(145, 20)
        Me.txtPAIS.TabIndex = 400
        Me.txtPAIS.Tag = Nothing
        '
        'lblEmails
        '
        Me.lblEmails.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblEmails.Location = New System.Drawing.Point(16, 240)
        Me.lblEmails.Name = "lblEmails"
        Me.lblEmails.Size = New System.Drawing.Size(80, 20)
        Me.lblEmails.TabIndex = 419
        Me.lblEmails.Text = "Emails"
        Me.lblEmails.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPais
        '
        Me.lblPais.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPais.Location = New System.Drawing.Point(16, 168)
        Me.lblPais.Name = "lblPais"
        Me.lblPais.Size = New System.Drawing.Size(80, 20)
        Me.lblPais.TabIndex = 418
        Me.lblPais.Text = "Pais"
        Me.lblPais.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFAX
        '
        Me.txtFAX.Location = New System.Drawing.Point(104, 216)
        Me.txtFAX.MaxLength = 15
        Me.txtFAX.Name = "txtFAX"
        Me.txtFAX.Size = New System.Drawing.Size(145, 20)
        Me.txtFAX.TabIndex = 404
        Me.txtFAX.Tag = Nothing
        '
        'txtPOB
        '
        Me.txtPOB.Location = New System.Drawing.Point(328, 144)
        Me.txtPOB.MaxLength = 30
        Me.txtPOB.Name = "txtPOB"
        Me.txtPOB.Size = New System.Drawing.Size(145, 20)
        Me.txtPOB.TabIndex = 401
        Me.txtPOB.Tag = Nothing
        '
        'txtDOM
        '
        Me.txtDOM.Location = New System.Drawing.Point(104, 67)
        Me.txtDOM.Multiline = True
        Me.txtDOM.Name = "txtDOM"
        Me.txtDOM.Size = New System.Drawing.Size(348, 44)
        Me.txtDOM.TabIndex = 397
        Me.txtDOM.Tag = Nothing
        '
        'txtID
        '
        Me.txtID.BackColor = System.Drawing.Color.PeachPuff
        Me.txtID.Location = New System.Drawing.Point(104, 15)
        Me.txtID.MaxLength = 1
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(52, 20)
        Me.txtID.TabIndex = 394
        Me.txtID.Tag = Nothing
        '
        'txtTEL1
        '
        Me.txtTEL1.Location = New System.Drawing.Point(104, 192)
        Me.txtTEL1.MaxLength = 15
        Me.txtTEL1.Name = "txtTEL1"
        Me.txtTEL1.Size = New System.Drawing.Size(145, 20)
        Me.txtTEL1.TabIndex = 403
        Me.txtTEL1.Tag = Nothing
        '
        'txtNIF
        '
        Me.txtNIF.Location = New System.Drawing.Point(104, 116)
        Me.txtNIF.MaxLength = 12
        Me.txtNIF.Name = "txtNIF"
        Me.txtNIF.Size = New System.Drawing.Size(145, 20)
        Me.txtNIF.TabIndex = 402
        Me.txtNIF.Tag = Nothing
        '
        'lblCP
        '
        Me.lblCP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCP.Location = New System.Drawing.Point(252, 168)
        Me.lblCP.Name = "lblCP"
        Me.lblCP.Size = New System.Drawing.Size(75, 20)
        Me.lblCP.TabIndex = 412
        Me.lblCP.Text = "C. Postal"
        Me.lblCP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFax
        '
        Me.lblFax.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFax.Location = New System.Drawing.Point(16, 216)
        Me.lblFax.Name = "lblFax"
        Me.lblFax.Size = New System.Drawing.Size(80, 20)
        Me.lblFax.TabIndex = 417
        Me.lblFax.Text = "Fax"
        Me.lblFax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPoblacion
        '
        Me.lblPoblacion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPoblacion.Location = New System.Drawing.Point(252, 144)
        Me.lblPoblacion.Name = "lblPoblacion"
        Me.lblPoblacion.Size = New System.Drawing.Size(75, 20)
        Me.lblPoblacion.TabIndex = 416
        Me.lblPoblacion.Text = "Població"
        Me.lblPoblacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNombre
        '
        Me.lblNombre.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNombre.Location = New System.Drawing.Point(16, 41)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(84, 20)
        Me.lblNombre.TabIndex = 410
        Me.lblNombre.Text = "Nom Client"
        Me.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTelefonos
        '
        Me.lblTelefonos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTelefonos.Location = New System.Drawing.Point(16, 192)
        Me.lblTelefonos.Name = "lblTelefonos"
        Me.lblTelefonos.Size = New System.Drawing.Size(80, 20)
        Me.lblTelefonos.TabIndex = 414
        Me.lblTelefonos.Text = "Telèfon 1"
        Me.lblTelefonos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblProvincia
        '
        Me.lblProvincia.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblProvincia.Location = New System.Drawing.Point(16, 144)
        Me.lblProvincia.Name = "lblProvincia"
        Me.lblProvincia.Size = New System.Drawing.Size(80, 20)
        Me.lblProvincia.TabIndex = 413
        Me.lblProvincia.Text = "Provincia"
        Me.lblProvincia.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnElegirCliente
        '
        Me.btnElegirCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirCliente.Location = New System.Drawing.Point(160, 15)
        Me.btnElegirCliente.Name = "btnElegirCliente"
        Me.btnElegirCliente.Size = New System.Drawing.Size(24, 17)
        Me.btnElegirCliente.TabIndex = 396
        Me.btnElegirCliente.TabStop = False
        Me.btnElegirCliente.Text = "..."
        Me.btnElegirCliente.Visible = False
        '
        'lblDomicilio
        '
        Me.lblDomicilio.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDomicilio.Location = New System.Drawing.Point(16, 67)
        Me.lblDomicilio.Name = "lblDomicilio"
        Me.lblDomicilio.Size = New System.Drawing.Size(80, 20)
        Me.lblDomicilio.TabIndex = 411
        Me.lblDomicilio.Text = "Domicili"
        Me.lblDomicilio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPROV
        '
        Me.txtPROV.Location = New System.Drawing.Point(104, 144)
        Me.txtPROV.MaxLength = 30
        Me.txtPROV.Name = "txtPROV"
        Me.txtPROV.Size = New System.Drawing.Size(145, 20)
        Me.txtPROV.TabIndex = 399
        Me.txtPROV.Tag = Nothing
        '
        'lblCodigoCliente
        '
        Me.lblCodigoCliente.BackColor = System.Drawing.SystemColors.Control
        Me.lblCodigoCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCodigoCliente.Location = New System.Drawing.Point(16, 15)
        Me.lblCodigoCliente.Name = "lblCodigoCliente"
        Me.lblCodigoCliente.Size = New System.Drawing.Size(80, 20)
        Me.lblCodigoCliente.TabIndex = 409
        Me.lblCodigoCliente.Text = "Codi Empresa"
        Me.lblCodigoCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOBSERV
        '
        Me.txtOBSERV.Location = New System.Drawing.Point(480, 112)
        Me.txtOBSERV.Multiline = True
        Me.txtOBSERV.Name = "txtOBSERV"
        Me.txtOBSERV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOBSERV.Size = New System.Drawing.Size(412, 232)
        Me.txtOBSERV.TabIndex = 395
        Me.txtOBSERV.Tag = Nothing
        '
        'txtCP
        '
        Me.txtCP.Location = New System.Drawing.Point(328, 168)
        Me.txtCP.MaxLength = 5
        Me.txtCP.Name = "txtCP"
        Me.txtCP.Size = New System.Drawing.Size(145, 20)
        Me.txtCP.TabIndex = 398
        Me.txtCP.Tag = Nothing
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button1.Location = New System.Drawing.Point(328, 264)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(24, 19)
        Me.Button1.TabIndex = 421
        Me.Button1.TabStop = False
        Me.Button1.Text = "..."
        '
        'chkEmpresaPorDefecto
        '
        Me.chkEmpresaPorDefecto.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkEmpresaPorDefecto.Location = New System.Drawing.Point(296, 8)
        Me.chkEmpresaPorDefecto.Name = "chkEmpresaPorDefecto"
        Me.chkEmpresaPorDefecto.Size = New System.Drawing.Size(156, 24)
        Me.chkEmpresaPorDefecto.TabIndex = 428
        Me.chkEmpresaPorDefecto.Text = "Empresa per Defecte"
        '
        'pibLOGO
        '
        Me.pibLOGO.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.pibLOGO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pibLOGO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.pibLOGO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pibLOGO.Location = New System.Drawing.Point(480, 4)
        Me.pibLOGO.Name = "pibLOGO"
        Me.pibLOGO.Size = New System.Drawing.Size(140, 105)
        Me.pibLOGO.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pibLOGO.TabIndex = 429
        Me.pibLOGO.TabStop = False
        '
        'btnCanviarLogo
        '
        Me.btnCanviarLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCanviarLogo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCanviarLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCanviarLogo.Location = New System.Drawing.Point(620, 4)
        Me.btnCanviarLogo.Name = "btnCanviarLogo"
        Me.btnCanviarLogo.Size = New System.Drawing.Size(52, 40)
        Me.btnCanviarLogo.TabIndex = 430
        Me.btnCanviarLogo.TabStop = False
        Me.btnCanviarLogo.Text = "Escollir logo"
        '
        'txtDESCRI
        '
        Me.txtDESCRI.Location = New System.Drawing.Point(104, 41)
        Me.txtDESCRI.MaxLength = 30
        Me.txtDESCRI.Name = "txtDESCRI"
        Me.txtDESCRI.Size = New System.Drawing.Size(348, 20)
        Me.txtDESCRI.TabIndex = 431
        Me.txtDESCRI.Tag = Nothing
        '
        'C1PrintPreviewControl1
        '

        '
        'C1PrintPreviewControl1.OutlineView
        '
        'Me.C1PrintPreviewControl1.PreviewOutlineView.Dock = System.Windows.Forms.DockStyle.Fill
        'Me.C1PrintPreviewControl1.PreviewOutlineView.Location = New System.Drawing.Point(0, 0)
        'Me.C1PrintPreviewControl1.PreviewOutlineView.Name = "OutlineView"
        'Me.C1PrintPreviewControl1.PreviewOutlineView.Size = New System.Drawing.Size(165, 427)
        'Me.C1PrintPreviewControl1.PreviewOutlineView.TabIndex = 0
        ''
        ''C1PrintPreviewControl1.PreviewPane
        ''
        'Me.C1PrintPreviewControl1.PreviewPane.Document = Me.C1PrintDocument1
        'Me.C1PrintPreviewControl1.PreviewPane.IntegrateExternalTools = True
        'Me.C1PrintPreviewControl1.PreviewPane.TabIndex = 0
        ''
        ''C1PrintPreviewControl1.PreviewTextSearchPanel
        ''
        'Me.C1PrintPreviewControl1.PreviewTextSearchPanel.Dock = System.Windows.Forms.DockStyle.Right
        'Me.C1PrintPreviewControl1.PreviewTextSearchPanel.Location = New System.Drawing.Point(530, 0)
        'Me.C1PrintPreviewControl1.PreviewTextSearchPanel.MinimumSize = New System.Drawing.Size(200, 240)
        'Me.C1PrintPreviewControl1.PreviewTextSearchPanel.Name = "PreviewTextSearchPanel"
        'Me.C1PrintPreviewControl1.PreviewTextSearchPanel.Size = New System.Drawing.Size(200, 453)
        'Me.C1PrintPreviewControl1.PreviewTextSearchPanel.TabIndex = 0
        'Me.C1PrintPreviewControl1.PreviewTextSearchPanel.Visible = False
        ''
        ''C1PrintPreviewControl1.ThumbnailView
        ''
        'Me.C1PrintPreviewControl1.PreviewThumbnailView.Dock = System.Windows.Forms.DockStyle.Fill
        'Me.C1PrintPreviewControl1.PreviewThumbnailView.Location = New System.Drawing.Point(0, 0)
        'Me.C1PrintPreviewControl1.PreviewThumbnailView.Name = "ThumbnailView"
        'Me.C1PrintPreviewControl1.PreviewThumbnailView.Size = New System.Drawing.Size(165, 427)
        'Me.C1PrintPreviewControl1.PreviewThumbnailView.TabIndex = 0
        'Me.C1PrintPreviewControl1.PreviewThumbnailView.UseImageAsThumbnail = False
        
        '
        'C1PrintDocument1
        '
        Me.C1PrintDocument1.DocumentInfo.Creator = "C1Reports Engine version 2.6.20102.54119"
        Me.C1PrintDocument1.PageLayouts.Default.PageSettings = New C1.C1Preview.C1PageSettings(False, System.Drawing.Printing.PaperKind.A4, False, "25.4mm", "25.4mm", "25.4mm", "25.4mm", System.Drawing.Printing.PaperSourceKind.Upper, 0, Nothing)
        '
        'frmEmpresas
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(908, 398)
        Me.Controls.Add(Me.txtDESCRI)
        Me.Controls.Add(Me.btnCanviarLogo)
        Me.Controls.Add(Me.pibLOGO)
        Me.Controls.Add(Me.chkEmpresaPorDefecto)
        Me.Controls.Add(Me.txtTEL2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblWeb)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.txtEmail2)
        Me.Controls.Add(Me.txtEMAIL1)
        Me.Controls.Add(Me.txtWEB)
        Me.Controls.Add(Me.lblNIF)
        Me.Controls.Add(Me.txtPAIS)
        Me.Controls.Add(Me.lblEmails)
        Me.Controls.Add(Me.lblPais)
        Me.Controls.Add(Me.txtFAX)
        Me.Controls.Add(Me.txtPOB)
        Me.Controls.Add(Me.txtDOM)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.txtTEL1)
        Me.Controls.Add(Me.txtNIF)
        Me.Controls.Add(Me.lblCP)
        Me.Controls.Add(Me.lblFax)
        Me.Controls.Add(Me.lblPoblacion)
        Me.Controls.Add(Me.lblNombre)
        Me.Controls.Add(Me.lblTelefonos)
        Me.Controls.Add(Me.lblProvincia)
        Me.Controls.Add(Me.btnElegirCliente)
        Me.Controls.Add(Me.lblDomicilio)
        Me.Controls.Add(Me.txtPROV)
        Me.Controls.Add(Me.lblCodigoCliente)
        Me.Controls.Add(Me.txtOBSERV)
        Me.Controls.Add(Me.txtCP)
        Me.Controls.Add(Me.Button1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "frmEmpresas"
        Me.Text = "Empreses"
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
        Me.Controls.SetChildIndex(Me.Button1, 0)
        Me.Controls.SetChildIndex(Me.txtCP, 0)
        Me.Controls.SetChildIndex(Me.txtOBSERV, 0)
        Me.Controls.SetChildIndex(Me.lblCodigoCliente, 0)
        Me.Controls.SetChildIndex(Me.txtPROV, 0)
        Me.Controls.SetChildIndex(Me.lblDomicilio, 0)
        Me.Controls.SetChildIndex(Me.btnElegirCliente, 0)
        Me.Controls.SetChildIndex(Me.lblProvincia, 0)
        Me.Controls.SetChildIndex(Me.lblTelefonos, 0)
        Me.Controls.SetChildIndex(Me.lblNombre, 0)
        Me.Controls.SetChildIndex(Me.lblPoblacion, 0)
        Me.Controls.SetChildIndex(Me.lblFax, 0)
        Me.Controls.SetChildIndex(Me.lblCP, 0)
        Me.Controls.SetChildIndex(Me.txtNIF, 0)
        Me.Controls.SetChildIndex(Me.txtTEL1, 0)
        Me.Controls.SetChildIndex(Me.txtID, 0)
        Me.Controls.SetChildIndex(Me.txtDOM, 0)
        Me.Controls.SetChildIndex(Me.txtPOB, 0)
        Me.Controls.SetChildIndex(Me.txtFAX, 0)
        Me.Controls.SetChildIndex(Me.lblPais, 0)
        Me.Controls.SetChildIndex(Me.lblEmails, 0)
        Me.Controls.SetChildIndex(Me.txtPAIS, 0)
        Me.Controls.SetChildIndex(Me.lblNIF, 0)
        Me.Controls.SetChildIndex(Me.txtWEB, 0)
        Me.Controls.SetChildIndex(Me.txtEMAIL1, 0)
        Me.Controls.SetChildIndex(Me.txtEmail2, 0)
        Me.Controls.SetChildIndex(Me.Button2, 0)
        Me.Controls.SetChildIndex(Me.Button3, 0)
        Me.Controls.SetChildIndex(Me.lblWeb, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.txtTEL2, 0)
        Me.Controls.SetChildIndex(Me.chkEmpresaPorDefecto, 0)
        Me.Controls.SetChildIndex(Me.pibLOGO, 0)
        Me.Controls.SetChildIndex(Me.btnCanviarLogo, 0)
        Me.Controls.SetChildIndex(Me.txtDESCRI, 0)
        CType(Me.txtTEL2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmail2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEMAIL1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWEB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPAIS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFAX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPOB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTEL1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNIF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPROV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pibLOGO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDESCRI, System.ComponentModel.ISupportInitialize).EndInit()

        Me.ResumeLayout(False)

    End Sub

#End Region
    Shared frmChildForm As frmEmpresas
    Public Shared Function GetInstance() As frmEmpresas
        If frmChildForm Is Nothing Then
            frmChildForm = New frmEmpresas
        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Public empresaActual As clsEmpresa

#End Region

#Region "INICIALIZAR"

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True
            Me.arrayTextBox = New System.Windows.Forms.Control() {Me.txtCP, Me.txtDOM, Me.txtEMAIL1, Me.txtEmail2, Me.txtFAX, Me.txtNIF, Me.txtOBSERV, Me.txtPOB, Me.txtPROV, Me.txtPAIS, Me.txtTEL2, Me.txtTEL1, Me.txtWEB, Me.txtDESCRI}
            Me.arrayBotones = New System.Windows.Forms.Control() {Me.Button1, Me.Button2, Me.Button3, Me.btnCanviarLogo}
            Me.arrayPictureBox = New System.Windows.Forms.Control() {Me.pibLOGO}
            ACN()
            tabla = tablaEmpresas
            empresaActual = New clsEmpresa(ds.Tables(tablaEmpresas), empresaPorDefecto, BindingContext)
            HacerBindings()
            PonerModificables(soloLectura)
            AutoSizeControles()
            Cursor = Cursors.Default
            CCN()
            PonerHandlersErroresParaGrids()
            PSBTIPO(empresaActual.centro)
            MoverActual()
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub HacerBindings()
        Try
            HacerBindingsTodos(empresaActual.dvForm)

            txtID.DataBindings.Add(New System.Windows.Forms.Binding("Text", empresaActual.dvForm, "CODI"))

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub

#End Region

#Region "MODIFICAR DB"

    Private Sub ActualizarOrigen()
        Try
            If editando Then
                editando = False
                'empresaActual.tabla.AcceptChanges()
                PonerModificables(soloLectura)
                'PonerControlesNuevo(True)
            End If

            If EsRegistroNuevo Then
                editando = True
                PonerControlesNuevo(False)
                empresaActual.crearNumeraciones()
                PonerModificables(modificable)
                EsRegistroNuevo = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Friend Overrides Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnActualizar.Click
        Try
            cargando = True
            If empresaActual.TieneCambios Then
                Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                    Case DialogResult.Cancel
                        cargando = False
                        Exit Sub
                    Case DialogResult.No
                        empresaActual.tabla.RejectChanges()
                        cargando = False
                        Exit Sub
                End Select
            End If
            empresaActual.ActualizarOrigen() : ActualizarOrigen()
            PSBTIPO(empresaActual.centro) : cargando = False
            CCN()
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnBorrar.Click
        Try
            cargando = True
            If MessageBox.Show(rm.GetString("Borrar"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                editando = True
                empresaActual.borrar() : ActualizarOrigen()
                editando = False
            End If
            MoverActual()
            PSBTIPO(empresaActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Friend Overrides Sub btnModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not editando Then
                cargando = True
                editando = True
                PonerModificables(modificable)
                PSBTIPO(empresaActual.centro) : cargando = False
                MoverActual()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnNuevo.Click
        Try
            PonerControlesNuevo(False)
            PonerModificables(modificable)
            EsRegistroNuevo = True
            cargando = True
            empresaActual.NuevoRegistro()
            cboSeleccionCentro.SelectedValue = empresaActual.centro
            PSBTIPO(empresaActual.centro) : cargando = False
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub

#End Region

#Region "COMUNES"

    Private Sub btnVerLista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerLista.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmlistadoEmpresas = frmlistadoEmpresas.GetInstance(esListado)
            f.MdiParent = Me.MdiParent
            AddHandler f.Closed, AddressOf CType(Me.MdiParent, frmPrincipal).childCerrado
            AddHandler f.Load, AddressOf CType(Me.MdiParent, frmPrincipal).childAbierto : AddHandler f.Activated, AddressOf CType(Me.MdiParent, frmPrincipal).childOcultandoMostrando
            f.Show()
            f.BringToFront()
            Cursor = Cursors.Default

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub PonerControlesNuevo(ByVal b As Boolean)
        Try
            If Not b Then
                btnActualizar.Text = rm.GetString("CONFIRMAR")
            Else
                btnActualizar.Text = rm.GetString("ACTUALIZAR")
            End If
            If Not b Then
                'cboDESCRI.DropMode = C1.Win.C1List.DropModeEnum.Manual
            Else
                'cboDESCRI.DropMode = C1.Win.C1List.DropModeEnum.Automatic
            End If

            'cboDESCRI.AutoCompletion = Not editando
            ModiNuev(b)

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Private Sub PonerModificables(Optional ByVal b As Boolean = False)
        Try
            PonerMod(b)
            'If Not b Then : cboDESCRI.DataSource = Nothing
            'Else : AñadirBindingCombo(cboDESCRI, empresaActual.dvIdentificadores, CCClients, CNClients) : empresaActual.tabla.AcceptChanges() : End If

            'cboDESCRI.LimitToList = b
            'cboDESCRI.SuperBack = b
            txtID.ReadOnly = editando ''or esregistronuevo
            ModiNuev(b)

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Friend Sub btnRecargar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecargar.Click
        Try
            cargando = True
            Dim id As Object
            id = txtID.Text
            empresaActual.ActualizarOrigen()
            empresaActual.CambiarAReg(id, iraregistro)
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub

#End Region

#Region "DESPLAZARSE"
    Private Sub MoverActual()
        Try
            cargando = True
            If empresaPorDefecto = empresaActual.CODI Then
                chkEmpresaPorDefecto.Checked = True
            Else
                chkEmpresaPorDefecto.Checked = False
            End If
            cargando = False

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnPrimero.Click
        Try
            cargando = True
            empresaActual.PrimeroReg() : ActualizarOrigen()
            PSBTIPO(empresaActual.centro) : cargando = False
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUltimo.Click
        Try
            cargando = True
            empresaActual.UltimoReg() : ActualizarOrigen()
            PSBTIPO(empresaActual.centro) : cargando = False
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAnterior.Click
        Try
            cargando = True
            empresaActual.AnteriorReg() : ActualizarOrigen()
            PSBTIPO(empresaActual.centro) : cargando = False
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSiguiente.Click
        Try
            cargando = True
            empresaActual.SiguienteReg() : ActualizarOrigen()
            PSBTIPO(empresaActual.centro)
            cargando = False
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub

#End Region

#Region "SELECCIÓN REGISTRO"

    Private Sub txtID_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtID.KeyPress
        Try
            If consulta() Then
                cargando = True
                BindingContext(empresaActual.dvForm).SuspendBinding()
                PSBTIPO(empresaActual.centro) : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try

    End Sub
    Private Sub txtID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtID.Validated
        Try
            If consulta() Then
                Dim IDAbuscar As Char
                cargando = True
                IDAbuscar = general.nz(txtID.Text, "")
                BindingContext(empresaActual.dvForm).ResumeBinding()
                empresaActual.CambiarAReg(IDAbuscar, iraregistro)
                PSBTIPO(empresaActual.centro) : cargando = False
                MoverActual()
            Else
                If EsRegistroNuevo Or editando Then
                    If ExisteID(tabla, nzn(txtID.Text, -1), empresaActual.centro) Then 'clienteActual.dvForm.Find(nzn(txtID.Text, 0)) <> -1 Then
                        MessageBox.Show(rm.GetString("CODIGOYAEXISTENTE"), "", MessageBoxButtons.OK)
                        'empresaActual.CODI = Nothing
                        txtID.Focus()
                    End If
                    MoverActual()
                End If
            End If

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub
    'Private Sub cboDESCRI_SelChange(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If consulta() Then
    '            cargando = True
    '            empresaActual.CambiarAReg(general.ponercontrabarrasreal(nzn(cboDESCRI.WillChangeToValue, 0)), iraregistro)
    '            PSBTIPO(empresaActual.centro) : cargando = False
    '            MoverActual()
    '        End If
    '    Catch ex As Exception
    '        LOG(ex.ToString)
    '    End Try
    'End Sub
    'Private Sub cboDESCRI_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    Try
    '        If editando Or EsRegistroNuevo Then
    '            cboDESCRI.AutoCompletion = False
    '        Else
    '            cboDESCRI.AutoCompletion = True
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString)
    '    End Try
    'End Sub

#Region "SELECCION CENTRO"

    Private Sub cboSeleccion_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
                                                 Handles cboSeleccionCentro.SelectionChangeCommitted
        Try
            'If consulta() Then
            '    cargando = True
            '    empresaActual.cambioCentro(GENERAL.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto), primero)
            '    empresaActual.tabla.AcceptChanges()
            '    cboSeleccionCentro.SelectedValue = empresaActual.centro
            '    'cboDESCRI.ReBind(True)
            '    cargando = False
            'Else
            '    If EsRegistroNuevo And Not cargando Then
            '        cargando = True
            '        empresaActual.centro =general.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto)
            '        empresaActual.tabla.Clear()
            '        empresaActual.NuevoRegistro()
            '        PSBTIPO(empresaActual.centro)
            '    End If
            '    cargando = False
            'End If

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cargando = False
        End Try
    End Sub

#End Region

#End Region

#Region "ELECCIONES"

#End Region

    Private Sub GuardarEmpresaPorDefecto(ByVal empresa As Char)
        Try
            Dim xmlDoc As Xml.XmlDocument = New Xml.XmlDocument
            directorio = CurDir() & "\"
            xmlDoc.Load(CurDir() & "\conf\configuracion.xml")

            Dim tempnodeList As Xml.XmlNodeList = xmlDoc.SelectNodes("//EMPRESADEFECTO")
            Dim tempnode As Xml.XmlNode
            For Each tempnode In tempnodeList
                tempnode.InnerText = empresa
            Next
            ' 2. Save the modified XML to a file in Unicode format.
            xmlDoc.PreserveWhitespace = True
            Dim wrtr As Xml.XmlTextWriter = New Xml.XmlTextWriter(CurDir() & "\conf\configuracion.xml", System.Text.Encoding.Unicode)
            xmlDoc.WriteTo(wrtr)
            wrtr.Close()
            empresaPorDefecto = empresa

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub
    Private Sub chkEmpresaPorDefecto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEmpresaPorDefecto.CheckedChanged
        Try
            If Not cargando Then
                If chkEmpresaPorDefecto.Checked = True Then
                    GuardarEmpresaPorDefecto(empresaActual.CODI)
                End If
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

    Private Sub btnCanviarLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCanviarLogo.Click
        Dim Foto() As Byte
        Dim NavArchivos As System.Windows.Forms.OpenFileDialog
        Dim Fs As System.IO.FileStream
        Try
            NavArchivos = New System.Windows.Forms.OpenFileDialog
            NavArchivos.ShowDialog()
            Fs = New System.IO.FileStream(NavArchivos.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)
            Foto = New Byte(Fs.Length) {}
            Fs.Read(Foto, 0, Fs.Length)
            Fs.Close()
            If Not Foto Is Nothing AndAlso Not IsDBNull(Foto) Then
                Dim img() As Byte = CType(Foto, Byte())
                empresaActual.LOGO = img
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN() : Fs.Close()
        End Try
    End Sub
End Class
