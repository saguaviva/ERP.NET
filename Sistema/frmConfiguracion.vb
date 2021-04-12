Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Public Class frmConfiguracion
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents NavigationBar1 As TD.Eyefinder.NavigationBar
    Friend WithEvents napPanelNavegacion As TD.Eyefinder.NavigationPane
    Friend WithEvents lstOpciones As System.Windows.Forms.ListBox
    Friend WithEvents lblHost As System.Windows.Forms.Label
    Friend WithEvents txtBD As C1.Win.C1Input.C1TextBox
    Friend WithEvents gpIdioma As System.Windows.Forms.GroupBox
    Friend WithEvents XpGroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents pnIdioma As System.Windows.Forms.Panel
    Friend WithEvents pnBD As System.Windows.Forms.Panel
    Friend WithEvents rdoCastellano As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCatalalan As System.Windows.Forms.RadioButton
    Friend WithEvents btnAceptar As C1.Win.C1Input.C1Button
    Friend WithEvents XpButton1 As C1.Win.C1Input.C1Button
    Friend WithEvents txtPuerto As C1.Win.C1Input.C1TextBox
    Friend WithEvents lnlPuerto As System.Windows.Forms.Label
    Friend WithEvents NavigationPane1 As TD.Eyefinder.NavigationPane
    Friend WithEvents lstHOSTS As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents XpGroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents C1TextBox1 As C1.Win.C1Input.C1TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtAHOST As C1.Win.C1Input.C1TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents XpButton2 As C1.Win.C1Input.C1Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtIP As C1.Win.C1Input.C1TextBox
    Friend WithEvents lblBD As System.Windows.Forms.Label
    Friend WithEvents txtBDTransferencia As C1.Win.C1Input.C1TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfiguracion))
        Me.NavigationBar1 = New TD.Eyefinder.NavigationBar()
        Me.napPanelNavegacion = New TD.Eyefinder.NavigationPane()
        Me.lstOpciones = New System.Windows.Forms.ListBox()
        Me.NavigationPane1 = New TD.Eyefinder.NavigationPane()
        Me.pnIdioma = New System.Windows.Forms.Panel()
        Me.gpIdioma = New System.Windows.Forms.GroupBox()
        Me.rdoCastellano = New System.Windows.Forms.RadioButton()
        Me.rdoCatalalan = New System.Windows.Forms.RadioButton()
        Me.lblHost = New System.Windows.Forms.Label()
        Me.txtBD = New C1.Win.C1Input.C1TextBox()
        Me.pnBD = New System.Windows.Forms.Panel()
        Me.XpGroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtIP = New C1.Win.C1Input.C1TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstHOSTS = New System.Windows.Forms.ListBox()
        Me.txtPuerto = New C1.Win.C1Input.C1TextBox()
        Me.lnlPuerto = New System.Windows.Forms.Label()
        Me.lblBD = New System.Windows.Forms.Label()
        Me.XpGroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.XpButton2 = New C1.Win.C1Input.C1Button()
        Me.txtAHOST = New C1.Win.C1Input.C1TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.C1TextBox1 = New C1.Win.C1Input.C1TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtBDTransferencia = New C1.Win.C1Input.C1TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnAceptar = New C1.Win.C1Input.C1Button()
        Me.XpButton1 = New C1.Win.C1Input.C1Button()
        Me.NavigationBar1.SuspendLayout
        Me.napPanelNavegacion.SuspendLayout
        Me.pnIdioma.SuspendLayout
        Me.gpIdioma.SuspendLayout
        CType(Me.txtBD,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnBD.SuspendLayout
        Me.XpGroupBox1.SuspendLayout
        CType(Me.txtIP,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtPuerto,System.ComponentModel.ISupportInitialize).BeginInit
        Me.XpGroupBox2.SuspendLayout
        CType(Me.XpButton2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtAHOST,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.C1TextBox1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtBDTransferencia,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnAceptar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.XpButton1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'NavigationBar1
        '
        Me.NavigationBar1.Controls.Add(Me.napPanelNavegacion)
        Me.NavigationBar1.Controls.Add(Me.NavigationPane1)
        Me.NavigationBar1.HeaderFont = New System.Drawing.Font("Tahoma", 12!, System.Drawing.FontStyle.Bold)
        Me.NavigationBar1.Location = New System.Drawing.Point(0, 0)
        Me.NavigationBar1.Name = "NavigationBar1"
        Me.NavigationBar1.PaneFont = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.NavigationBar1.SelectedPane = Me.napPanelNavegacion
        Me.NavigationBar1.ShowPanes = 2
        Me.NavigationBar1.Size = New System.Drawing.Size(125, 446)
        Me.NavigationBar1.TabIndex = 0
        '
        'napPanelNavegacion
        '
        Me.napPanelNavegacion.Controls.Add(Me.lstOpciones)
        Me.napPanelNavegacion.Location = New System.Drawing.Point(1, 31)
        Me.napPanelNavegacion.Name = "napPanelNavegacion"
        Me.napPanelNavegacion.Size = New System.Drawing.Size(123, 336)
        Me.napPanelNavegacion.TabIndex = 0
        Me.napPanelNavegacion.Text = "Configuració"
        '
        'lstOpciones
        '
        Me.lstOpciones.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lstOpciones.ItemHeight = 16
        Me.lstOpciones.Items.AddRange(New Object() {"IDIOMA", "EMPRESES", "BASE DE DADES"})
        Me.lstOpciones.Location = New System.Drawing.Point(0, 0)
        Me.lstOpciones.Name = "lstOpciones"
        Me.lstOpciones.Size = New System.Drawing.Size(125, 276)
        Me.lstOpciones.TabIndex = 0
        '
        'NavigationPane1
        '
        Me.NavigationPane1.Location = New System.Drawing.Point(1, 31)
        Me.NavigationPane1.Name = "NavigationPane1"
        Me.NavigationPane1.Size = New System.Drawing.Size(123, 336)
        Me.NavigationPane1.TabIndex = 1
        Me.NavigationPane1.Text = "Copia Seguretat"
        '
        'pnIdioma
        '
        Me.pnIdioma.Controls.Add(Me.gpIdioma)
        Me.pnIdioma.Location = New System.Drawing.Point(134, 9)
        Me.pnIdioma.Name = "pnIdioma"
        Me.pnIdioma.Size = New System.Drawing.Size(653, 443)
        Me.pnIdioma.TabIndex = 1
        '
        'gpIdioma
        '
        Me.gpIdioma.BackColor = System.Drawing.SystemColors.Control
        Me.gpIdioma.Controls.Add(Me.rdoCastellano)
        Me.gpIdioma.Controls.Add(Me.rdoCatalalan)
        Me.gpIdioma.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.gpIdioma.Location = New System.Drawing.Point(10, 14)
        Me.gpIdioma.Name = "gpIdioma"
        Me.gpIdioma.Size = New System.Drawing.Size(172, 106)
        Me.gpIdioma.TabIndex = 0
        Me.gpIdioma.TabStop = false
        Me.gpIdioma.Text = "Idioma"
        '
        'rdoCastellano
        '
        Me.rdoCastellano.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoCastellano.Location = New System.Drawing.Point(19, 60)
        Me.rdoCastellano.Name = "rdoCastellano"
        Me.rdoCastellano.Size = New System.Drawing.Size(125, 28)
        Me.rdoCastellano.TabIndex = 1
        Me.rdoCastellano.Text = "Castellà"
        '
        'rdoCatalalan
        '
        Me.rdoCatalalan.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoCatalalan.Location = New System.Drawing.Point(19, 28)
        Me.rdoCatalalan.Name = "rdoCatalalan"
        Me.rdoCatalalan.Size = New System.Drawing.Size(125, 27)
        Me.rdoCatalalan.TabIndex = 0
        Me.rdoCatalalan.Text = "Català"
        '
        'lblHost
        '
        Me.lblHost.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblHost.Location = New System.Drawing.Point(19, 23)
        Me.lblHost.Name = "lblHost"
        Me.lblHost.Size = New System.Drawing.Size(120, 23)
        Me.lblHost.TabIndex = 18
        Me.lblHost.Text = "Host"
        '
        'txtBD
        '
        Me.txtBD.Location = New System.Drawing.Point(500, 63)
        Me.txtBD.Name = "txtBD"
        Me.txtBD.Size = New System.Drawing.Size(120, 20)
        Me.txtBD.TabIndex = 14
        Me.txtBD.Tag = Nothing
        '
        'pnBD
        '
        Me.pnBD.Controls.Add(Me.XpGroupBox1)
        Me.pnBD.Controls.Add(Me.XpGroupBox2)
        Me.pnBD.Location = New System.Drawing.Point(134, 9)
        Me.pnBD.Name = "pnBD"
        Me.pnBD.Size = New System.Drawing.Size(653, 443)
        Me.pnBD.TabIndex = 19
        '
        'XpGroupBox1
        '
        Me.XpGroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.XpGroupBox1.Controls.Add(Me.Label6)
        Me.XpGroupBox1.Controls.Add(Me.txtIP)
        Me.XpGroupBox1.Controls.Add(Me.Label3)
        Me.XpGroupBox1.Controls.Add(Me.Label2)
        Me.XpGroupBox1.Controls.Add(Me.Label1)
        Me.XpGroupBox1.Controls.Add(Me.lstHOSTS)
        Me.XpGroupBox1.Controls.Add(Me.txtPuerto)
        Me.XpGroupBox1.Controls.Add(Me.lnlPuerto)
        Me.XpGroupBox1.Controls.Add(Me.txtBD)
        Me.XpGroupBox1.Controls.Add(Me.lblHost)
        Me.XpGroupBox1.Controls.Add(Me.lblBD)
        Me.XpGroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.XpGroupBox1.Location = New System.Drawing.Point(10, 9)
        Me.XpGroupBox1.Name = "XpGroupBox1"
        Me.XpGroupBox1.Size = New System.Drawing.Size(627, 124)
        Me.XpGroupBox1.TabIndex = 2
        Me.XpGroupBox1.TabStop = false
        Me.XpGroupBox1.Text = "Base de Dades"
        '
        'Label6
        '
        Me.Label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(342, 37)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(154, 23)
        Me.Label6.TabIndex = 27
        Me.Label6.Text = "IP"
        '
        'txtIP
        '
        Me.txtIP.Location = New System.Drawing.Point(500, 37)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(120, 20)
        Me.txtIP.TabIndex = 26
        Me.txtIP.Tag = Nothing
        '
        'Label3
        '
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(154, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(148, 18)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "--> FORA COESPUNT"
        '
        'Label2
        '
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(154, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(144, 19)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "--> DINS COESPUNT"
        '
        'Label1
        '
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(154, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 18)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "--> LOCAL"
        '
        'lstHOSTS
        '
        Me.lstHOSTS.ItemHeight = 16
        Me.lstHOSTS.Items.AddRange(New Object() {"127.0.0.1", "192.168.0.2", "coespunt.dyndns.info"})
        Me.lstHOSTS.Location = New System.Drawing.Point(14, 51)
        Me.lstHOSTS.Name = "lstHOSTS"
        Me.lstHOSTS.Size = New System.Drawing.Size(135, 36)
        Me.lstHOSTS.TabIndex = 21
        '
        'txtPuerto
        '
        Me.txtPuerto.Location = New System.Drawing.Point(500, 91)
        Me.txtPuerto.Name = "txtPuerto"
        Me.txtPuerto.Size = New System.Drawing.Size(120, 20)
        Me.txtPuerto.TabIndex = 19
        Me.txtPuerto.Tag = Nothing
        '
        'lnlPuerto
        '
        Me.lnlPuerto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lnlPuerto.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lnlPuerto.Location = New System.Drawing.Point(342, 91)
        Me.lnlPuerto.Name = "lnlPuerto"
        Me.lnlPuerto.Size = New System.Drawing.Size(154, 23)
        Me.lnlPuerto.TabIndex = 20
        Me.lnlPuerto.Text = "Port"
        '
        'lblBD
        '
        Me.lblBD.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblBD.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblBD.Location = New System.Drawing.Point(342, 63)
        Me.lblBD.Name = "lblBD"
        Me.lblBD.Size = New System.Drawing.Size(154, 24)
        Me.lblBD.TabIndex = 17
        Me.lblBD.Text = "Nom Base de dades"
        '
        'XpGroupBox2
        '
        Me.XpGroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.XpGroupBox2.Controls.Add(Me.Label5)
        Me.XpGroupBox2.Controls.Add(Me.XpButton2)
        Me.XpGroupBox2.Controls.Add(Me.txtAHOST)
        Me.XpGroupBox2.Controls.Add(Me.Label4)
        Me.XpGroupBox2.Controls.Add(Me.C1TextBox1)
        Me.XpGroupBox2.Controls.Add(Me.Label7)
        Me.XpGroupBox2.Controls.Add(Me.txtBDTransferencia)
        Me.XpGroupBox2.Controls.Add(Me.Label9)
        Me.XpGroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.XpGroupBox2.Location = New System.Drawing.Point(10, 155)
        Me.XpGroupBox2.Name = "XpGroupBox2"
        Me.XpGroupBox2.Size = New System.Drawing.Size(628, 230)
        Me.XpGroupBox2.TabIndex = 26
        Me.XpGroupBox2.TabStop = false
        Me.XpGroupBox2.Text = "Transferir Base de Dades a un altre sistema"
        '
        'Label5
        '
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(19, 89)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(600, 56)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = resources.GetString("Label5.Text")
        '
        'XpButton2
        '
        Me.XpButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.XpButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.XpButton2.Location = New System.Drawing.Point(186, 164)
        Me.XpButton2.Name = "XpButton2"
        Me.XpButton2.Size = New System.Drawing.Size(175, 26)
        Me.XpButton2.TabIndex = 29
        Me.XpButton2.Text = "Realitzar Transferència"
        Me.XpButton2.UseVisualStyleBackColor = true
        '
        'txtAHOST
        '
        Me.txtAHOST.Location = New System.Drawing.Point(144, 55)
        Me.txtAHOST.Name = "txtAHOST"
        Me.txtAHOST.Size = New System.Drawing.Size(115, 20)
        Me.txtAHOST.TabIndex = 28
        Me.txtAHOST.Tag = Nothing
        Me.txtAHOST.Value = "192.168.0.50"
        '
        'Label4
        '
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(19, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 23)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "A Host"
        '
        'C1TextBox1
        '
        Me.C1TextBox1.Location = New System.Drawing.Point(463, 46)
        Me.C1TextBox1.Name = "C1TextBox1"
        Me.C1TextBox1.Size = New System.Drawing.Size(120, 20)
        Me.C1TextBox1.TabIndex = 19
        Me.C1TextBox1.Tag = Nothing
        '
        'Label7
        '
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(338, 46)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(120, 23)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Puerto"
        '
        'txtBDTransferencia
        '
        Me.txtBDTransferencia.Location = New System.Drawing.Point(463, 18)
        Me.txtBDTransferencia.Name = "txtBDTransferencia"
        Me.txtBDTransferencia.Size = New System.Drawing.Size(120, 20)
        Me.txtBDTransferencia.TabIndex = 14
        Me.txtBDTransferencia.Tag = Nothing
        '
        'Label9
        '
        Me.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label9.Location = New System.Drawing.Point(338, 18)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(120, 24)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Base de dades"
        '
        'btnAceptar
        '
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAceptar.Location = New System.Drawing.Point(682, 471)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(90, 26)
        Me.btnAceptar.TabIndex = 20
        Me.btnAceptar.Text = "Acceptar"
        Me.btnAceptar.UseVisualStyleBackColor = true
        '
        'XpButton1
        '
        Me.XpButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.XpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.XpButton1.Location = New System.Drawing.Point(586, 471)
        Me.XpButton1.Name = "XpButton1"
        Me.XpButton1.Size = New System.Drawing.Size(90, 26)
        Me.XpButton1.TabIndex = 21
        Me.XpButton1.Text = "Cancelar"
        Me.XpButton1.UseVisualStyleBackColor = true
        '
        'frmConfiguracion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.ClientSize = New System.Drawing.Size(656, 446)
        Me.Controls.Add(Me.XpButton1)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.NavigationBar1)
        Me.Controls.Add(Me.pnBD)
        Me.Controls.Add(Me.pnIdioma)
        Me.Name = "frmConfiguracion"
        Me.Text = "Opcions"
        Me.NavigationBar1.ResumeLayout(false)
        Me.napPanelNavegacion.ResumeLayout(false)
        Me.pnIdioma.ResumeLayout(false)
        Me.gpIdioma.ResumeLayout(false)
        CType(Me.txtBD,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnBD.ResumeLayout(false)
        Me.XpGroupBox1.ResumeLayout(false)
        CType(Me.txtIP,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtPuerto,System.ComponentModel.ISupportInitialize).EndInit
        Me.XpGroupBox2.ResumeLayout(false)
        CType(Me.XpButton2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtAHOST,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.C1TextBox1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtBDTransferencia,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnAceptar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.XpButton1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

#End Region

    Shared frmChildForm As frmConfiguracion
    Public Shared Function GetInstance() As frmConfiguracion
        If frmChildForm Is Nothing Then
            frmChildForm = New frmConfiguracion

        End If
        Return frmChildForm
    End Function
#Region "VARIABLES"

    Private lang As String

#End Region
    Private Sub frmConfiguracion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim xmlDoc As New Xml.XmlDocument
        Dim tempnode As Xml.XmlNode
        Dim lang As String
        Try
            lstOpciones.SelectedIndex = 2
            directorio = CurDir() & "\"
            xmlDoc.Load(CurDir() & "\conf\configuracion.xml")
            For Each tempnode In xmlDoc.DocumentElement.ChildNodes
                Select Case tempnode.Name
                    Case "IDIOMA"
                        Select Case tempnode.InnerText
                            Case "ca"
                                rdoCatalalan.Checked = True
                            Case "es-ES"
                                rdoCastellano.Checked = True
                        End Select
                    Case "PUERTO"
                        txtPuerto.Text = tempnode.InnerText
                    Case "BD"
                        txtBD.Text = tempnode.InnerText
                    Case "IPSERVIDOR"
                        txtIP.Text = tempnode.InnerText
                        lstHOSTS.SelectedItem = tempnode.InnerText

                End Select
            Next

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub lstOpciones_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstOpciones.SelectedIndexChanged
        Try
            Select Case lstOpciones.SelectedIndex
                Case 0
                    pnIdioma.BringToFront()
                Case 1

                Case 2
                    pnBD.BringToFront()
            End Select

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub GuardarValorANodo(ByVal valor As String, ByVal nodo As String)
        Try
            Dim xmlDoc As Xml.XmlDocument = New Xml.XmlDocument
            directorio = CurDir() & "\"
            xmlDoc.Load(CurDir() & "\conf\configuracion.xml")

            Dim tempnodeList As Xml.XmlNodeList = xmlDoc.SelectNodes("//" & nodo & "")
            Dim tempnode2 As Xml.XmlNode
            For Each tempnode2 In tempnodeList
                tempnode2.InnerText = valor
            Next

            xmlDoc.PreserveWhitespace = True
            Dim wrtr As Xml.XmlTextWriter = New Xml.XmlTextWriter(CurDir() & "\conf\configuracion.xml", System.Text.Encoding.Unicode)
            xmlDoc.WriteTo(wrtr)
            wrtr.Close()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub GuardarConfig()
        GuardarValorANodo(txtIP.Text, "IPSERVIDOR")
        GuardarValorANodo(txtBD.Text, "BD")
        GuardarValorANodo(txtPuerto.Text, "PUERTO")
        If rdoCastellano.Checked = True Then
            GuardarValorANodo("es-ES", "IDIOMA")
        End If
        If rdoCatalalan.Checked = True Then
            GuardarValorANodo("ca", "IDIOMA")
        End If
        clsFuncionesLOG.w.Close()
        Application.Exit()
        System.Diagnostics.Process.Start(Application.ExecutablePath)
    End Sub
    Private Sub XpButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XpButton1.Click
        Try
            Me.Close()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            GuardarConfig()
            Close()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

    Private Sub XpButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XpButton2.Click
        Dim cnn2 As MySqlConnection
        Dim cnn3 As MySqlConnection
        Try
            cnn2 = New MySqlConnection("User=root;Password=;Host=" & lstHOSTS.SelectedItem & " ;Database=" & txtBD.Text & ";Port=" & txtPuerto.Text)
            ''DumpIt(cnn2)
            Dim mcommand As New MySqlCommand
            cnn3 = New MySqlConnection("User=root;Password=;Host=" & txtAHOST.Text & " ;Port=" & txtPuerto.Text)
            mcommand.Connection = cnn3
            cnn3.Open()
            mcommand.CommandText = "CREATE DATABASE " & txtBDTransferencia.Text & ";"
            mcommand.ExecuteNonQuery()
            cnn3.Close()
            cnn3 = New MySqlConnection("User=root;Password=;Host=" & txtAHOST.Text & " ;Database=" & txtBDTransferencia.Text & ";Port=" & txtPuerto.Text)
            ''UnDumpIt(cnn3, True)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    'Public Sub DumpIt(ByVal myConnection As MySqlConnection)
    '    myConnection.Open()
    '    Dim mySqlDump As MySqlDump = New MySqlDump
    '    mySqlDump.Connection = myConnection

    '    myConnection.Database = txtBD.Text
    '    mySqlDump.IncludeDrop = True
    '    mySqlDump.GenerateHeader = True
    '    mySqlDump.IncludeDatabase = True
    '    mySqlDump.Backup()
    '    Dim stream As IO.StreamWriter = New IO.StreamWriter("mysqldump.dmp")
    '    stream.WriteLine(mySqlDump.DumpText)
    '    stream.Close()
    '    MessageBox.Show("Copia seguretat completada")

    '    myConnection.Close()
    'End Sub

    'Public Sub UnDumpIt(ByVal myConnection As MySqlConnection, ByVal boolSoloTransferir As Boolean)
    '    myConnection.Open()
    '    Dim mySqlDump As MySqlDump = New MySqlDump
    '    mySqlDump.Connection = myConnection
    '    myConnection.Database = txtBDTransferencia.Text

    '    Dim stream As IO.StreamReader = New IO.StreamReader("mysqldump.dmp")
    '    mySqlDump.DumpText = stream.ReadToEnd()
    '    If boolSoloTransferir Then
    '        mySqlDump.DumpText = mySqlDump.DumpText.Replace("DROP DATABASE IF EXISTS " & txtBD.Text & ";", "DROP DATABASE IF EXISTS " & txtBDTransferencia.Text & ";")
    '        mySqlDump.DumpText = mySqlDump.DumpText.Replace("CREATE DATABASE " & txtBD.Text & ";", "CREATE DATABASE " & txtBDTransferencia.Text & ";")
    '        mySqlDump.DumpText = mySqlDump.DumpText.Replace("USE " & txtBD.Text & ";", "USE " & txtBDTransferencia.Text & ";")
    '    End If
    '    stream.Close()
    '    mySqlDump.Restore()
    '    MessageBox.Show("Copia Recuperada")

    '    myConnection.Close()
    'End Sub

    Private Sub lstHOSTS_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstHOSTS.SelectedIndexChanged

    End Sub
End Class
