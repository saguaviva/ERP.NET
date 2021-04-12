Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class frmTransportistasForm
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


    Friend WithEvents tabControlTalleres As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents lblFax As Label
    Friend WithEvents lblPoblacion As Label
    Friend WithEvents lblObservaciones As Label
    Friend WithEvents lblTelefonos As Label
    Friend WithEvents lblNIF As Label
    Friend WithEvents lblProvincia As Label
    Friend WithEvents lblCP As Label
    Friend WithEvents txtNIF As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCP As C1.Win.C1Input.C1TextBox
    Friend WithEvents lblDomicilio As Label
    Friend WithEvents lblCodigoTransportista As Label
    Friend WithEvents btnElegirTransportista As C1.Win.C1Input.C1Button
    Friend WithEvents lblNombreTransportista As Label
    Friend WithEvents WLabel1 As Label
    Friend WithEvents txtFAX As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtPOB As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCODI As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtEMAIL As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtOBSERV As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTEL As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtPROV As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtDOM As C1.Win.C1Input.C1TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTEL2 As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboID As C1.Win.C1List.C1Combo

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmTransportistasForm))
        Me.tabControlTalleres = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtTEL2 = New C1.Win.C1Input.C1TextBox
        Me.txtCODI = New C1.Win.C1Input.C1TextBox
        Me.WLabel1 = New System.Windows.Forms.Label
        Me.txtEMAIL = New C1.Win.C1Input.C1TextBox
        Me.btnElegirTransportista = New C1.Win.C1Input.C1Button
        Me.cboID = New C1.Win.C1List.C1Combo
        Me.lblFax = New System.Windows.Forms.Label
        Me.txtFAX = New C1.Win.C1Input.C1TextBox
        Me.lblPoblacion = New System.Windows.Forms.Label
        Me.txtPOB = New C1.Win.C1Input.C1TextBox
        Me.lblObservaciones = New System.Windows.Forms.Label
        Me.lblTelefonos = New System.Windows.Forms.Label
        Me.txtOBSERV = New C1.Win.C1Input.C1TextBox
        Me.txtTEL = New C1.Win.C1Input.C1TextBox
        Me.lblNIF = New System.Windows.Forms.Label
        Me.lblProvincia = New System.Windows.Forms.Label
        Me.lblCP = New System.Windows.Forms.Label
        Me.txtNIF = New C1.Win.C1Input.C1TextBox
        Me.txtPROV = New C1.Win.C1Input.C1TextBox
        Me.txtCP = New C1.Win.C1Input.C1TextBox
        Me.lblDomicilio = New System.Windows.Forms.Label
        Me.lblNombreTransportista = New System.Windows.Forms.Label
        Me.lblCodigoTransportista = New System.Windows.Forms.Label
        Me.txtDOM = New C1.Win.C1Input.C1TextBox
        Me.tabControlTalleres.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.txtTEL2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCODI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEMAIL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFAX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPOB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTEL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNIF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPROV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDOM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.Name = "cboSeleccionCentro"
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(618, 526)
        Me.btnVerLista.Name = "btnVerLista"
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(152, 526)
        Me.btnActualizar.Name = "btnActualizar"
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(340, 526)
        Me.btnNuevo.Name = "btnNuevo"
        '
        'arrayEtiquetas
        '

        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(240, 546)
        Me.btnBorrar.Name = "btnBorrar"
        '
        'arrayTabPages
        '
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(618, 546)
        Me.btnTancar.Name = "btnTancar"
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(240, 526)
        Me.btnModificar.Name = "btnModificar"
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(420, 526)
        Me.btnUltimo.Name = "btnUltimo"
        '
        'arrayBotones
        '

        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(120, 526)
        Me.btnPrimero.Name = "btnPrimero"
        '
        'arrayTextBox
        '

        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(120, 546)
        Me.btnAnterior.Name = "btnAnterior"
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(420, 546)
        Me.btnSiguiente.Name = "btnSiguiente"
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(152, 546)
        Me.btnRecargar.Name = "btnRecargar"
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.sbtipo.Location = New System.Drawing.Point(5, 322)
        Me.sbtipo.Name = "sbtipo"
        Me.sbtipo.Text = ""
        '
        'tabControlTalleres
        '
        Me.tabControlTalleres.Controls.Add(Me.TabPage1)
        Me.tabControlTalleres.ItemSize = New System.Drawing.Size(76, 18)
        Me.tabControlTalleres.Location = New System.Drawing.Point(12, 12)
        Me.tabControlTalleres.Name = "tabControlTalleres"
        Me.tabControlTalleres.SelectedIndex = 0
        Me.tabControlTalleres.Size = New System.Drawing.Size(864, 480)
        Me.tabControlTalleres.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.txtTEL2)
        Me.TabPage1.Controls.Add(Me.txtCODI)
        Me.TabPage1.Controls.Add(Me.WLabel1)
        Me.TabPage1.Controls.Add(Me.txtEMAIL)
        Me.TabPage1.Controls.Add(Me.btnElegirTransportista)
        Me.TabPage1.Controls.Add(Me.cboID)
        Me.TabPage1.Controls.Add(Me.lblFax)
        Me.TabPage1.Controls.Add(Me.txtFAX)
        Me.TabPage1.Controls.Add(Me.lblPoblacion)
        Me.TabPage1.Controls.Add(Me.txtPOB)
        Me.TabPage1.Controls.Add(Me.lblObservaciones)
        Me.TabPage1.Controls.Add(Me.lblTelefonos)
        Me.TabPage1.Controls.Add(Me.txtOBSERV)
        Me.TabPage1.Controls.Add(Me.txtTEL)
        Me.TabPage1.Controls.Add(Me.lblNIF)
        Me.TabPage1.Controls.Add(Me.lblProvincia)
        Me.TabPage1.Controls.Add(Me.lblCP)
        Me.TabPage1.Controls.Add(Me.txtNIF)
        Me.TabPage1.Controls.Add(Me.txtPROV)
        Me.TabPage1.Controls.Add(Me.txtCP)
        Me.TabPage1.Controls.Add(Me.lblDomicilio)
        Me.TabPage1.Controls.Add(Me.lblNombreTransportista)
        Me.TabPage1.Controls.Add(Me.lblCodigoTransportista)
        Me.TabPage1.Controls.Add(Me.txtDOM)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(856, 454)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Transportista"
        '
        'Label1
        '
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(12, 288)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.TabIndex = 215
        Me.Label1.Text = "Telèfon 2"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTEL2
        '
        Me.txtTEL2.Location = New System.Drawing.Point(112, 284)
        Me.txtTEL2.Name = "txtTEL2"
        Me.txtTEL2.Size = New System.Drawing.Size(276, 20)
        Me.txtTEL2.TabIndex = 214
        Me.txtTEL2.Tag = Nothing
        '
        'txtCODI
        '
        Me.txtCODI.BackColor = System.Drawing.Color.Azure
        Me.txtCODI.Location = New System.Drawing.Point(112, 16)
        Me.txtCODI.Name = "txtCODI"
        Me.txtCODI.Size = New System.Drawing.Size(56, 20)
        Me.txtCODI.TabIndex = 0
        Me.txtCODI.Tag = Nothing
        '
        'WLabel1
        '
        Me.WLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.WLabel1.Location = New System.Drawing.Point(12, 344)
        Me.WLabel1.Name = "WLabel1"
        Me.WLabel1.Size = New System.Drawing.Size(96, 16)
        Me.WLabel1.TabIndex = 213
        Me.WLabel1.Text = "e-mail"
        Me.WLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEMAIL
        '
        Me.txtEMAIL.Location = New System.Drawing.Point(112, 340)
        Me.txtEMAIL.Name = "txtEMAIL"
        Me.txtEMAIL.Size = New System.Drawing.Size(276, 20)
        Me.txtEMAIL.TabIndex = 9
        Me.txtEMAIL.Tag = Nothing
        '
        'btnElegirTransportista
        '
        Me.btnElegirTransportista.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirTransportista.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirTransportista.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirTransportista.Location = New System.Drawing.Point(168, 16)
        Me.btnElegirTransportista.Name = "btnElegirTransportista"
        Me.btnElegirTransportista.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirTransportista.TabIndex = 211
        Me.btnElegirTransportista.TabStop = False
        Me.btnElegirTransportista.Text = "..."
        Me.btnElegirTransportista.Visible = False
        '
        'cboID
        '
        Me.cboID.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cboID.AutoCompletion = True
        Me.cboID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboID.Caption = ""
        Me.cboID.CaptionHeight = 17
        Me.cboID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboID.ColumnCaptionHeight = 17
        Me.cboID.ColumnFooterHeight = 17
        Me.cboID.ContentHeight = 15
        Me.cboID.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboID.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboID.EditorBackColor = System.Drawing.Color.Azure
        Me.cboID.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboID.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboID.EditorHeight = 15
        Me.cboID.FlatStyle = C1.Win.C1List.FlatModeEnum.System
        Me.cboID.GapHeight = 2
        Me.cboID.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.cboID.ItemHeight = 15
        Me.cboID.Location = New System.Drawing.Point(112, 44)
        Me.cboID.MatchEntryTimeout = CType(100, Long)
        Me.cboID.MaxDropDownItems = CType(15, Short)
        Me.cboID.MaxLength = 35
        Me.cboID.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboID.Name = "cboID"
        ''Me.cbo.PartialRightColumn = False
        Me.cboID.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboID.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboID.Size = New System.Drawing.Size(276, 19)
        Me.cboID.TabIndex = 1
        Me.cboID.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:T" & _
        "rue;BackColor:Control;Border:Flat,ControlDark,1, 1, 1, 1;ForeColor:ControlText;A" & _
        "lignVert:Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:Near;}</Data></Style" & _
        "s><Splits><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeigh" & _
        "t=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" " & _
        "HorizontalScrollGroup=""1""><ClientRect>0, 0, 118, 158</ClientRect><VScrollBar><Wi" & _
        "dth>17</Width></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionS" & _
        "tyle parent=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" />" & _
        "<FooterStyle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style1" & _
        "1"" /><HeadingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""Hig" & _
        "hlightRow"" me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowS" & _
        "tyle parent=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" " & _
        "me=""Style10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" m" & _
        "e=""Normal"" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" " & _
        "me=""Normal"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""" & _
        "Footer"" /><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Ina" & _
        "ctive"" /><Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Highl" & _
        "ightRow"" /><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddR" & _
        "ow"" /><Style parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""" & _
        "Group"" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layo" & _
        "ut>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'lblFax
        '
        Me.lblFax.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFax.Location = New System.Drawing.Point(12, 316)
        Me.lblFax.Name = "lblFax"
        Me.lblFax.Size = New System.Drawing.Size(96, 16)
        Me.lblFax.TabIndex = 209
        Me.lblFax.Text = "Fax"
        Me.lblFax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFAX
        '
        Me.txtFAX.Location = New System.Drawing.Point(112, 312)
        Me.txtFAX.Name = "txtFAX"
        Me.txtFAX.Size = New System.Drawing.Size(276, 20)
        Me.txtFAX.TabIndex = 8
        Me.txtFAX.Tag = Nothing
        '
        'lblPoblacion
        '
        Me.lblPoblacion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPoblacion.Location = New System.Drawing.Point(12, 204)
        Me.lblPoblacion.Name = "lblPoblacion"
        Me.lblPoblacion.Size = New System.Drawing.Size(96, 16)
        Me.lblPoblacion.TabIndex = 208
        Me.lblPoblacion.Text = "Població"
        Me.lblPoblacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPOB
        '
        Me.txtPOB.Location = New System.Drawing.Point(112, 200)
        Me.txtPOB.Name = "txtPOB"
        Me.txtPOB.Size = New System.Drawing.Size(276, 20)
        Me.txtPOB.TabIndex = 5
        Me.txtPOB.Tag = Nothing
        '
        'lblObservaciones
        '
        Me.lblObservaciones.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblObservaciones.Location = New System.Drawing.Point(404, 20)
        Me.lblObservaciones.Name = "lblObservaciones"
        Me.lblObservaciones.Size = New System.Drawing.Size(80, 16)
        Me.lblObservaciones.TabIndex = 205
        Me.lblObservaciones.Text = "Observacions"
        '
        'lblTelefonos
        '
        Me.lblTelefonos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTelefonos.Location = New System.Drawing.Point(12, 260)
        Me.lblTelefonos.Name = "lblTelefonos"
        Me.lblTelefonos.Size = New System.Drawing.Size(96, 16)
        Me.lblTelefonos.TabIndex = 204
        Me.lblTelefonos.Text = "Telèfon 1"
        Me.lblTelefonos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtOBSERV
        '
        Me.txtOBSERV.Location = New System.Drawing.Point(404, 44)
        Me.txtOBSERV.Multiline = True
        Me.txtOBSERV.Name = "txtOBSERV"
        Me.txtOBSERV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOBSERV.Size = New System.Drawing.Size(440, 392)
        Me.txtOBSERV.TabIndex = 10
        Me.txtOBSERV.Tag = Nothing
        '
        'txtTEL
        '
        Me.txtTEL.Location = New System.Drawing.Point(112, 256)
        Me.txtTEL.Name = "txtTEL"
        Me.txtTEL.Size = New System.Drawing.Size(276, 20)
        Me.txtTEL.TabIndex = 7
        Me.txtTEL.Tag = Nothing
        '
        'lblNIF
        '
        Me.lblNIF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNIF.Location = New System.Drawing.Point(12, 232)
        Me.lblNIF.Name = "lblNIF"
        Me.lblNIF.Size = New System.Drawing.Size(96, 16)
        Me.lblNIF.TabIndex = 201
        Me.lblNIF.Text = "N.I.F."
        Me.lblNIF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblProvincia
        '
        Me.lblProvincia.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblProvincia.Location = New System.Drawing.Point(12, 176)
        Me.lblProvincia.Name = "lblProvincia"
        Me.lblProvincia.Size = New System.Drawing.Size(96, 16)
        Me.lblProvincia.TabIndex = 200
        Me.lblProvincia.Text = "Provincia"
        Me.lblProvincia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCP
        '
        Me.lblCP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCP.Location = New System.Drawing.Point(12, 148)
        Me.lblCP.Name = "lblCP"
        Me.lblCP.Size = New System.Drawing.Size(96, 16)
        Me.lblCP.TabIndex = 199
        Me.lblCP.Text = "C. Postal"
        Me.lblCP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNIF
        '
        Me.txtNIF.Location = New System.Drawing.Point(112, 228)
        Me.txtNIF.Name = "txtNIF"
        Me.txtNIF.Size = New System.Drawing.Size(276, 20)
        Me.txtNIF.TabIndex = 6
        Me.txtNIF.Tag = Nothing
        '
        'txtPROV
        '
        Me.txtPROV.Location = New System.Drawing.Point(112, 172)
        Me.txtPROV.Name = "txtPROV"
        Me.txtPROV.Size = New System.Drawing.Size(276, 20)
        Me.txtPROV.TabIndex = 4
        Me.txtPROV.Tag = Nothing
        '
        'txtCP
        '
        Me.txtCP.Location = New System.Drawing.Point(112, 144)
        Me.txtCP.MaxLength = 5
        Me.txtCP.Name = "txtCP"
        Me.txtCP.Size = New System.Drawing.Size(276, 20)
        Me.txtCP.TabIndex = 3
        Me.txtCP.Tag = Nothing
        '
        'lblDomicilio
        '
        Me.lblDomicilio.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDomicilio.Location = New System.Drawing.Point(12, 76)
        Me.lblDomicilio.Name = "lblDomicilio"
        Me.lblDomicilio.Size = New System.Drawing.Size(96, 16)
        Me.lblDomicilio.TabIndex = 195
        Me.lblDomicilio.Text = "Domicili"
        Me.lblDomicilio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNombreTransportista
        '
        Me.lblNombreTransportista.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNombreTransportista.Location = New System.Drawing.Point(12, 48)
        Me.lblNombreTransportista.Name = "lblNombreTransportista"
        Me.lblNombreTransportista.Size = New System.Drawing.Size(100, 16)
        Me.lblNombreTransportista.TabIndex = 194
        Me.lblNombreTransportista.Text = "Nom Transportista"
        Me.lblNombreTransportista.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCodigoTransportista
        '
        Me.lblCodigoTransportista.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCodigoTransportista.Location = New System.Drawing.Point(12, 20)
        Me.lblCodigoTransportista.Name = "lblCodigoTransportista"
        Me.lblCodigoTransportista.Size = New System.Drawing.Size(96, 16)
        Me.lblCodigoTransportista.TabIndex = 193
        Me.lblCodigoTransportista.Text = "Codi"
        Me.lblCodigoTransportista.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDOM
        '
        Me.txtDOM.Location = New System.Drawing.Point(112, 72)
        Me.txtDOM.Multiline = True
        Me.txtDOM.Name = "txtDOM"
        Me.txtDOM.Size = New System.Drawing.Size(276, 60)
        Me.txtDOM.TabIndex = 2
        Me.txtDOM.Tag = Nothing
        '
        'frmTransportistasForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(894, 576)
        Me.Controls.Add(Me.tabControlTalleres)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.KeyPreview = True
        Me.Name = "frmTransportistasForm"
        Me.Text = " Transportistes"
        Me.Controls.SetChildIndex(Me.cboSeleccionCentro, 0)
        Me.Controls.SetChildIndex(Me.tabControlTalleres, 0)
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
        Me.tabControlTalleres.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.txtTEL2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCODI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEMAIL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFAX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPOB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTEL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNIF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPROV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDOM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmTransportistasForm
    Public Shared Function GetInstance() As frmTransportistasForm
        If frmChildForm Is Nothing Then
            frmChildForm = New frmTransportistasForm
        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Public registroActual As clsTransportista

#End Region

#Region "INICIALIZAR"

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            cargando = True
            Me.arrayTextBox = New System.Windows.Forms.Control() {Me.txtCP, Me.txtDOM, Me.txtEMAIL, Me.txtFAX, Me.txtNIF, Me.txtOBSERV, Me.txtPOB, Me.txtPROV, Me.txtTEL, Me.txtTEL2}
            Me.arrayBotones = New System.Windows.Forms.Control() {Me.btnElegirTransportista}
            Me.arrayTabPages = New System.Windows.Forms.Control() {Me.TabPage1}
            Me.arrayEtiquetas = New System.Windows.Forms.Control() {Me.WLabel1, Me.lblCP, Me.lblCodigoTransportista, Me.lblDomicilio, Me.lblFax, Me.lblNIF, Me.lblNombreTransportista, Me.lblObservaciones, Me.lblPoblacion, Me.lblProvincia, Me.lblTelefonos}
            
            tabla = tablaTransportistas
            registroActual = New clsTransportista(ds.Tables(tabla), "C", BindingContext)

            HacerBindings()
            'registroActual.tabla.AcceptChanges()
            PonerModificables(soloLectura)
            PonerHandlersErroresParaGrids()
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub HacerBindings()
        Try
            HacerBindingsTodos(registroActual.dvForm)
            AñadirBinding(txtCODI, registroActual.dvForm, "CODI")
            AñadirBinding(cboID, registroActual.dvForm, "NOM")
            AñadirBindingCombo(cboID, registroActual.dvIdentificadores, CCTrans, CNTrans)
            OcultarMostrarColumnaCbo(cboID, "CENTRO", False)



        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "MODIFICAR DB"

    Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnNuevo.Click
        Try
            cargando = True
            PonerModificables(modificable)
            EsRegistroNuevo = True
            PonerControlesNuevo(False)
            'cboID.ClearItems()
            registroActual.NuevoRegistro()
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Private Sub ActualizarOrigen()
        Try
            If editando Then
                editando = False
                'registroActual.tabla.AcceptChanges()
                PonerModificables(soloLectura)
            End If
            If EsRegistroNuevo Then
                editando = True : PSBTIPO(registroActual.centro)
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
            If registroActual.TieneCambios Then
                Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                    Case DialogResult.Cancel
                        cargando = False
                        Exit Sub
                    Case DialogResult.No
                        registroActual.tabla.RejectChanges()
                        cargando = False
                        Exit Sub
                End Select
            End If
            registroActual.ActualizarOrigen() : ActualizarOrigen()
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnBorrar.Click
        Try
            If MessageBox.Show(rm.GetString("BorrarTransportista"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                editando = True
                registroActual.borrar() : ActualizarOrigen()
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
                PSBTIPO(registroActual.centro)
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
            Dim f As frmTransportistasLista = frmTransportistasLista.GetInstance(esListado)
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
    Private Sub PonerModificables(Optional ByVal b As Boolean = False)
        Try
            PonerMod(b)
            If Not b Then : cboID.DataSource = Nothing
            Else : AñadirBindingCombo(cboID, registroActual.dvIdentificadores, CCTrans, CNTrans) : registroActual.tabla.AcceptChanges() : End If

            cboID.LimitToList = b
            cboID.SuperBack = b
            txtCODI.ReadOnly = editando Or EsRegistroNuevo
            ModiNuev(b)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerControlesNuevo(ByVal b As Boolean)
        Try
            If b Then
                btnActualizar.Text = rm.GetString("ACTUALIZAR")
            Else
                btnActualizar.Text = rm.GetString("CONFIRMAR")
            End If
            ModiNuev(b)
            cboID.AutoCompletion = Not editando

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Sub btnRecargar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecargar.Click
        Try
            cargando = True
            Dim id As Object
            id = nzn(txtCODI.Text, 0)
            registroActual.ActualizarOrigen()
            registroActual.CambiarAReg(id, iraregistro)
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
            registroActual.PrimeroReg() : ActualizarOrigen()
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUltimo.Click
        Try
            cargando = True
            registroActual.UltimoReg() : ActualizarOrigen()
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAnterior.Click
        Try
            cargando = True
            registroActual.AnteriorReg() : ActualizarOrigen()
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSiguiente.Click
        Try
            cargando = True
            registroActual.SiguienteReg() : ActualizarOrigen()
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "SELECCIÓN REGISTRO"

    Private Sub btnElegirTransportista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirTransportista.Click
        Try
            If PuedoModificar() Then
                cargando = True
                Cursor = Cursors.WaitCursor
                Dim f As frmTransportistasLista = frmTransportistasLista.GetInstance(esEleccion)
                f.ShowDialog()
                Cursor = Cursors.Default
                If Not f.dr Is Nothing Then registroActual.CambiarAReg(f.dr("CODI"), iraregistro)

                f = Nothing
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboID_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboID.RowChange
        Dim SQL As String
        Try
            If consulta() Then
                cargando = True
                registroActual.CambiarAReg(general.ponercontrabarrasreal(cboID.WillChangeToValue), iraregistro)
                PSBTIPO(registroActual.centro) : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtCODI_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCODI.Validated
        If Not cargando Then
            cargando = True
            ValidarCambio()
            cargando = False
        End If
    End Sub
    Private Sub txtID_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCODI.KeyDown
        Try
            If consulta() Then
                cargando = True
                If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Tab Then
                    ValidarCambio()
                Else
                    BindingContext(registroActual.dvForm).SuspendBinding()
                    PSBTIPO(registroActual.centro)
                End If
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub ValidarCambio()
        Try
            If Not EsRegistroNuevo AndAlso Not editando Then
                Dim IDAbuscar As Integer
                cargando = True
                IDAbuscar = nzn(txtCODI.Text, 0)
                BindingContext(registroActual.dvForm).ResumeBinding()
                registroActual.CambiarAReg(IDAbuscar, iraregistro)
                PSBTIPO(registroActual.centro) : cargando = False
            Else
                If EsRegistroNuevo Then
                    If ExisteID(tabla, nzn(txtCODI.Text, -1), registroActual.centro) Then 'clienteActual.dvForm.Find(nzn(txtID.Text, 0)) <> -1 Then
                        MessageBox.Show(rm.GetString("CODIGOYAEXISTENTE"), "", MessageBoxButtons.OK)
                        txtCODI.Focus()
                        BindingContext(registroActual.dvForm).ResumeBinding()
                    End If
                End If
            End If
        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub cboID_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboID.KeyPress
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

#Region "SELECCION CENTRO"
    Private Sub cboSeleccion_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSeleccionCentro.SelectionChangeCommitted
        Try
            If consulta() Then
                cargando = True
                registroActual.cambioCentro(GENERAL.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto), primero)
                'registroActual.tabla.AcceptChanges()
                cboSeleccionCentro.SelectedValue = registroActual.centro
                cargando = False

            Else
                If EsRegistroNuevo And Not cargando Then
                    cargando = True
                    registroActual.centro = general.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto)
                    registroActual.tabla.Clear()
                    registroActual.NuevoRegistro()
                    PSBTIPO(registroActual.centro)
                    cargando = False
                End If
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#End Region

#Region "ELECCIONES"

#End Region

    Private Sub frm_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        GrabaParametro(registroActual.CODI, "transportista")
    End Sub

End Class

