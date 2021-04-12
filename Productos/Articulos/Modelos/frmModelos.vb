Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Imports C1.Win.C1TrueDBGrid


Public Class frmModelos
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
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblSerie As System.Windows.Forms.Label
    Friend WithEvents lblPack As Label
    Friend WithEvents lblMargen As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents lblPrecioVenta As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents lblTemporada As Label
    Friend WithEvents lblCliente As Label
    Friend WithEvents lblDescripcion As Label
    Friend WithEvents lblModelo As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents llbAnchoPiezaTejido As Label
    Friend WithEvents lblProveedorTejido As Label
    Friend WithEvents lblTejido As Label
    Friend WithEvents lblTinteTejido As Label
    Friend WithEvents lblEstampadorTejido As Label
    Friend WithEvents lblAcabadorTejido As Label
    Friend WithEvents lblRendimientoKGTejido As Label
    Friend WithEvents lblTallerConfeccion As Label
    Friend WithEvents lblCodigoModelo As Label
    Friend WithEvents lblClienteF As Label
    Friend WithEvents lblTemporadaFicha As Label
    Friend WithEvents lblNumeroMuestras As Label
    Friend WithEvents lblPrecioVentaModelo As Label
    Friend WithEvents lblPrecioCoste As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents tabPageModelo As System.Windows.Forms.TabPage
    Friend WithEvents tabPageForniturasModelo As System.Windows.Forms.TabPage
    Friend WithEvents tabPageEscandalloPrecio As System.Windows.Forms.TabPage
    Friend WithEvents tabPageColores As System.Windows.Forms.TabPage
    Friend WithEvents tabPageFichaModelo As System.Windows.Forms.TabPage
    Friend WithEvents tabPageStock As System.Windows.Forms.TabPage
    Friend WithEvents btnElegirCliente As C1.Win.C1Input.C1Button
    Friend WithEvents btnElegirTejido As C1.Win.C1Input.C1Button
    Friend WithEvents btnElegirTallerConfeccion As C1.Win.C1Input.C1Button
    Friend WithEvents rdoEscogerTemporada As System.Windows.Forms.RadioButton
    Friend WithEvents rdoEscogerTodasTemporadas As System.Windows.Forms.RadioButton
    Friend WithEvents btnElegirProveedor As C1.Win.C1Input.C1Button
    Friend WithEvents btnElegirTintador As C1.Win.C1Input.C1Button
    Friend WithEvents btnElegirAcabador As C1.Win.C1Input.C1Button
    Friend WithEvents btnElegirEstampador As C1.Win.C1Input.C1Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNumeroMuestras As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboSeleccionCliente As C1.Win.C1List.C1Combo
    Friend WithEvents rdoVerLosModelosTodosLosClientes As System.Windows.Forms.RadioButton
    Friend WithEvents rdoVerModelosDeCliente As System.Windows.Forms.RadioButton
    Friend WithEvents gbTejido As System.Windows.Forms.GroupBox
    Friend WithEvents cboID As C1.Win.C1List.C1Combo
    Friend WithEvents btnCanviarImagen1 As C1.Win.C1Input.C1Button
    Friend WithEvents btnCanviarImagen2 As C1.Win.C1Input.C1Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents btnEscogerModelo As C1.Win.C1Input.C1Button
    Friend WithEvents dgForni As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents dgEscandall As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents dgColores As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents dgStock As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents cboFornituras As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents tabControlModelos As System.Windows.Forms.TabControl
    Friend WithEvents cboTejido As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents dgTejidoFicha As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents dgColorFicha As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents datagrid3 As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents txtVENDA As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCOST As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboNOMACA As C1.Win.C1List.C1Combo
    Friend WithEvents cboNOMTINT As C1.Win.C1List.C1Combo
    Friend WithEvents cboNOMPROVE As C1.Win.C1List.C1Combo
    Friend WithEvents cboTEIXIT As C1.Win.C1List.C1Combo
    Friend WithEvents txtRENDIM As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtAMPLE As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTINT As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtESTAM As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtACA As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtPROVE As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtDESCRITEIXIT As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboNOMCONFEC As C1.Win.C1List.C1Combo
    Friend WithEvents txtCONFEC As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCODIMODEL As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboNOMCLIENT As C1.Win.C1List.C1Combo
    Friend WithEvents txtOBSERV As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtSERIE As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtDESCRI As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA10 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA09 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA08 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA07 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA06 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA05 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA04 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA03 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA02 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtTALLA01 As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtNPACK As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtMARGE As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx2COST As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx2VENDA As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtNFORNITURA As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtNCONFEC As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtNPLANXA As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtNREPAS As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx5NOMCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx5SERIE As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx5CLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx5CODI As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx6TEMPORADA As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx6CLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboNOMESTAM As C1.Win.C1List.C1Combo
    Friend WithEvents gbTalles As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents tx4CODI As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx4NOMCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx4SERIE As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx4CLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx3NOMCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx3SERIE As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx3CLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx3CODI As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx2NOMCLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx2SERIE As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx2CLIENT As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx2CODI As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx4VENDA As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx3COST As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx5VENDA As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx4COST As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx6VENDA As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx5COST As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx7VENDA As C1.Win.C1Input.C1TextBox
    Friend WithEvents tx6COST As C1.Win.C1Input.C1TextBox
    Friend WithEvents gbEleccionCliente As System.Windows.Forms.GroupBox
    Friend WithEvents gbConfeccion As System.Windows.Forms.GroupBox
    Friend WithEvents gbModelo As System.Windows.Forms.GroupBox
    Friend WithEvents gbsPrecio As System.Windows.Forms.GroupBox
    Friend WithEvents cboColor As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents gbEleccionTemporadas As System.Windows.Forms.GroupBox
    Friend WithEvents contextBorrar As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents btnBorrarColor As C1.Win.C1Input.C1Button
    Friend WithEvents btnAñadirColor As C1.Win.C1Input.C1Button
    Friend WithEvents txtColorAñadir As C1.Win.C1Input.C1TextBox
    Friend WithEvents txtColorQuitar As C1.Win.C1Input.C1TextBox
    Friend WithEvents cboTEMPORADA As C1.Win.C1List.C1Combo
    Friend WithEvents txtMANIPULACION As C1.Win.C1Input.C1TextBox
    Friend WithEvents dgManipulacion As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtVENDAFINAL As C1.Win.C1Input.C1TextBox
    Friend WithEvents dgEscandalloResumen As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents cboMedidasForni As C1.Win.C1TrueDBGrid.C1TrueDBDropdown
    Friend WithEvents tabPageFicha2 As System.Windows.Forms.TabPage
    Friend WithEvents datagrid5 As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents dgConsumosFichaM As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents TextBox9 As C1.Win.C1Input.C1TextBox
    Friend WithEvents imgModelo1 As System.Windows.Forms.PictureBox
    Friend WithEvents imgModelo2 As System.Windows.Forms.PictureBox
    Friend WithEvents TextBox2 As C1.Win.C1Input.C1TextBox
    Friend WithEvents TextBox1 As C1.Win.C1Input.C1TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents tx3VENDA As C1.Win.C1Input.C1TextBox
    Friend WithEvents btnBorrarImagen2 As C1.Win.C1Input.C1Button
    Friend WithEvents btnBorrarImagen1 As C1.Win.C1Input.C1Button
    Friend WithEvents btnCanviarImagen3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents imgModelo3 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmModelos))
        Me.rdoEscogerTemporada = New System.Windows.Forms.RadioButton
        Me.rdoEscogerTodasTemporadas = New System.Windows.Forms.RadioButton
        Me.gbEleccionCliente = New System.Windows.Forms.GroupBox
        Me.cboSeleccionCliente = New C1.Win.C1List.C1Combo
        Me.rdoVerLosModelosTodosLosClientes = New System.Windows.Forms.RadioButton
        Me.rdoVerModelosDeCliente = New System.Windows.Forms.RadioButton
        Me.gbEleccionTemporadas = New System.Windows.Forms.GroupBox
        Me.tabControlModelos = New System.Windows.Forms.TabControl
        Me.tabPageModelo = New System.Windows.Forms.TabPage
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.imgModelo3 = New System.Windows.Forms.PictureBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtVENDAFINAL = New C1.Win.C1Input.C1TextBox
        Me.lblPrecioCoste = New System.Windows.Forms.Label
        Me.lblPrecioVentaModelo = New System.Windows.Forms.Label
        Me.txtVENDA = New C1.Win.C1Input.C1TextBox
        Me.txtCOST = New C1.Win.C1Input.C1TextBox
        Me.gbTejido = New System.Windows.Forms.GroupBox
        Me.cboNOMACA = New C1.Win.C1List.C1Combo
        Me.cboNOMESTAM = New C1.Win.C1List.C1Combo
        Me.cboNOMTINT = New C1.Win.C1List.C1Combo
        Me.cboNOMPROVE = New C1.Win.C1List.C1Combo
        Me.cboTEIXIT = New C1.Win.C1List.C1Combo
        Me.btnElegirAcabador = New C1.Win.C1Input.C1Button
        Me.btnElegirEstampador = New C1.Win.C1Input.C1Button
        Me.btnElegirTintador = New C1.Win.C1Input.C1Button
        Me.btnElegirProveedor = New C1.Win.C1Input.C1Button
        Me.lblRendimientoKGTejido = New System.Windows.Forms.Label
        Me.txtRENDIM = New C1.Win.C1Input.C1TextBox
        Me.lblTinteTejido = New System.Windows.Forms.Label
        Me.lblEstampadorTejido = New System.Windows.Forms.Label
        Me.lblAcabadorTejido = New System.Windows.Forms.Label
        Me.btnElegirTejido = New C1.Win.C1Input.C1Button
        Me.txtAMPLE = New C1.Win.C1Input.C1TextBox
        Me.txtTINT = New C1.Win.C1Input.C1TextBox
        Me.txtESTAM = New C1.Win.C1Input.C1TextBox
        Me.txtACA = New C1.Win.C1Input.C1TextBox
        Me.txtPROVE = New C1.Win.C1Input.C1TextBox
        Me.txtDESCRITEIXIT = New C1.Win.C1Input.C1TextBox
        Me.lblTejido = New System.Windows.Forms.Label
        Me.lblProveedorTejido = New System.Windows.Forms.Label
        Me.llbAnchoPiezaTejido = New System.Windows.Forms.Label
        Me.gbConfeccion = New System.Windows.Forms.GroupBox
        Me.cboNOMCONFEC = New C1.Win.C1List.C1Combo
        Me.btnElegirTallerConfeccion = New C1.Win.C1Input.C1Button
        Me.txtCONFEC = New C1.Win.C1Input.C1TextBox
        Me.lblTallerConfeccion = New System.Windows.Forms.Label
        Me.gbModelo = New System.Windows.Forms.GroupBox
        Me.cboTEMPORADA = New C1.Win.C1List.C1Combo
        Me.btnEscogerModelo = New C1.Win.C1Input.C1Button
        Me.cboID = New C1.Win.C1List.C1Combo
        Me.txtCODIMODEL = New C1.Win.C1Input.C1TextBox
        Me.lblCodigoModelo = New System.Windows.Forms.Label
        Me.cboNOMCLIENT = New C1.Win.C1List.C1Combo
        Me.txtSERIE = New C1.Win.C1Input.C1TextBox
        Me.lblSerie = New System.Windows.Forms.Label
        Me.btnElegirCliente = New C1.Win.C1Input.C1Button
        Me.lblTemporada = New System.Windows.Forms.Label
        Me.lblCliente = New System.Windows.Forms.Label
        Me.lblDescripcion = New System.Windows.Forms.Label
        Me.txtCLIENT = New C1.Win.C1Input.C1TextBox
        Me.txtDESCRI = New C1.Win.C1Input.C1TextBox
        Me.lblModelo = New System.Windows.Forms.Label
        Me.txtOBSERV = New C1.Win.C1Input.C1TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnCanviarImagen3 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.tabPageForniturasModelo = New System.Windows.Forms.TabPage
        Me.cboMedidasForni = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.tx3NOMCLIENT = New C1.Win.C1Input.C1TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.tx3SERIE = New C1.Win.C1Input.C1TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.tx3CLIENT = New C1.Win.C1Input.C1TextBox
        Me.tx3CODI = New C1.Win.C1Input.C1TextBox
        Me.GroupBox12 = New System.Windows.Forms.GroupBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.tx4VENDA = New C1.Win.C1Input.C1TextBox
        Me.tx3COST = New C1.Win.C1Input.C1TextBox
        Me.cboFornituras = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Me.dgForni = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.tabPageEscandalloPrecio = New System.Windows.Forms.TabPage
        Me.GroupBox11 = New System.Windows.Forms.GroupBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.tx5VENDA = New C1.Win.C1Input.C1TextBox
        Me.tx4COST = New C1.Win.C1Input.C1TextBox
        Me.cboTejido = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Me.dgEscandall = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.lblPack = New System.Windows.Forms.Label
        Me.txtNPACK = New C1.Win.C1Input.C1TextBox
        Me.lblMargen = New System.Windows.Forms.Label
        Me.txtMARGE = New C1.Win.C1Input.C1TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.lblPrecioVenta = New System.Windows.Forms.Label
        Me.tx2COST = New C1.Win.C1Input.C1TextBox
        Me.tx2VENDA = New C1.Win.C1Input.C1TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtNFORNITURA = New C1.Win.C1Input.C1TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtNCONFEC = New C1.Win.C1Input.C1TextBox
        Me.txtNPLANXA = New C1.Win.C1Input.C1TextBox
        Me.txtNREPAS = New C1.Win.C1Input.C1TextBox
        Me.txtMANIPULACION = New C1.Win.C1Input.C1TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.dgManipulacion = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.tx4NOMCLIENT = New C1.Win.C1Input.C1TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.tx4SERIE = New C1.Win.C1Input.C1TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.tx4CLIENT = New C1.Win.C1Input.C1TextBox
        Me.tx4CODI = New C1.Win.C1Input.C1TextBox
        Me.tabPageColores = New System.Windows.Forms.TabPage
        Me.dgEscandalloResumen = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.contextBorrar = New System.Windows.Forms.ContextMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.txtColorQuitar = New C1.Win.C1Input.C1TextBox
        Me.btnBorrarColor = New C1.Win.C1Input.C1Button
        Me.btnAñadirColor = New C1.Win.C1Input.C1Button
        Me.cboColor = New C1.Win.C1TrueDBGrid.C1TrueDBDropdown
        Me.gbTalles = New System.Windows.Forms.GroupBox
        Me.txtTALLA10 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA09 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA08 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA07 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA06 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA05 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA04 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA03 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA02 = New C1.Win.C1Input.C1TextBox
        Me.txtTALLA01 = New C1.Win.C1Input.C1TextBox
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.tx2NOMCLIENT = New C1.Win.C1Input.C1TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.tx2SERIE = New C1.Win.C1Input.C1TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.tx2CLIENT = New C1.Win.C1Input.C1TextBox
        Me.tx2CODI = New C1.Win.C1Input.C1TextBox
        Me.GroupBox13 = New System.Windows.Forms.GroupBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.tx7VENDA = New C1.Win.C1Input.C1TextBox
        Me.tx6COST = New C1.Win.C1Input.C1TextBox
        Me.dgColores = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.txtColorAñadir = New C1.Win.C1Input.C1TextBox
        Me.tabPageStock = New System.Windows.Forms.TabPage
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.tx5NOMCLIENT = New C1.Win.C1Input.C1TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.tx5SERIE = New C1.Win.C1Input.C1TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.tx5CLIENT = New C1.Win.C1Input.C1TextBox
        Me.tx5CODI = New C1.Win.C1Input.C1TextBox
        Me.gbsPrecio = New System.Windows.Forms.GroupBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.tx6VENDA = New C1.Win.C1Input.C1TextBox
        Me.tx5COST = New C1.Win.C1Input.C1TextBox
        Me.dgStock = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.tabPageFichaModelo = New System.Windows.Forms.TabPage
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.imgModelo2 = New System.Windows.Forms.PictureBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.imgModelo1 = New System.Windows.Forms.PictureBox
        Me.btnBorrarImagen2 = New C1.Win.C1Input.C1Button
        Me.btnBorrarImagen1 = New C1.Win.C1Input.C1Button
        Me.btnCanviarImagen2 = New C1.Win.C1Input.C1Button
        Me.btnCanviarImagen1 = New C1.Win.C1Input.C1Button
        Me.datagrid3 = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.dgColorFicha = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.dgTejidoFicha = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.lblNumeroMuestras = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.tx3VENDA = New C1.Win.C1Input.C1TextBox
        Me.txtNumeroMuestras = New C1.Win.C1Input.C1TextBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.tx6TEMPORADA = New C1.Win.C1Input.C1TextBox
        Me.lblClienteF = New System.Windows.Forms.Label
        Me.lblTemporadaFicha = New System.Windows.Forms.Label
        Me.tx6CLIENT = New C1.Win.C1Input.C1TextBox
        Me.tabPageFicha2 = New System.Windows.Forms.TabPage
        Me.TextBox2 = New C1.Win.C1Input.C1TextBox
        Me.TextBox1 = New C1.Win.C1Input.C1TextBox
        Me.datagrid5 = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.dgConsumosFichaM = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.TextBox9 = New C1.Win.C1Input.C1TextBox
        Me.gbEleccionCliente.SuspendLayout()
        CType(Me.cboSeleccionCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbEleccionTemporadas.SuspendLayout()
        Me.tabControlModelos.SuspendLayout()
        Me.tabPageModelo.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.txtVENDAFINAL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVENDA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCOST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTejido.SuspendLayout()
        CType(Me.cboNOMACA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMESTAM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMTINT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMPROVE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTEIXIT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRENDIM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAMPLE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTINT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtESTAM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtACA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPROVE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDESCRITEIXIT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbConfeccion.SuspendLayout()
        CType(Me.cboNOMCONFEC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCONFEC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbModelo.SuspendLayout()
        CType(Me.cboTEMPORADA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCODIMODEL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNOMCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSERIE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDESCRI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPageForniturasModelo.SuspendLayout()
        CType(Me.cboMedidasForni, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.tx3NOMCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx3SERIE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx3CLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx3CODI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox12.SuspendLayout()
        CType(Me.tx4VENDA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx3COST, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFornituras, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgForni, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPageEscandalloPrecio.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        CType(Me.tx5VENDA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx4COST, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTejido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgEscandall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox7.SuspendLayout()
        CType(Me.txtNPACK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMARGE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx2COST, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx2VENDA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNFORNITURA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNCONFEC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNPLANXA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNREPAS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMANIPULACION, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgManipulacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.tx4NOMCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx4SERIE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx4CLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx4CODI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPageColores.SuspendLayout()
        CType(Me.dgEscandalloResumen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtColorQuitar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboColor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTalles.SuspendLayout()
        CType(Me.txtTALLA10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA09, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA08, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA07, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA06, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA05, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA04, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA03, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA02, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTALLA01, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox9.SuspendLayout()
        CType(Me.tx2NOMCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx2SERIE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx2CLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx2CODI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox13.SuspendLayout()
        CType(Me.tx7VENDA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx6COST, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgColores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtColorAñadir, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPageStock.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        CType(Me.tx5NOMCLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx5SERIE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx5CLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx5CODI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbsPrecio.SuspendLayout()
        CType(Me.tx6VENDA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx5COST, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPageFichaModelo.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.datagrid3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgColorFicha, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgTejidoFicha, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx3VENDA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNumeroMuestras, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.tx6TEMPORADA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx6CLIENT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPageFicha2.SuspendLayout()
        CType(Me.TextBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.datagrid5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgConsumosFichaM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'arrayTextBox
        '

        '
        'btnSiguiente
        '
        Me.btnSiguiente.Name = "btnSiguiente"
        '
        'btnAnterior
        '
        Me.btnAnterior.Name = "btnAnterior"
        '
        'btnPrimero
        '
        Me.btnPrimero.Name = "btnPrimero"
        '
        'btnUltimo
        '
        Me.btnUltimo.Name = "btnUltimo"
        '
        'arrayEtiquetas
        '
        '
        'btnModificar
        '
        Me.btnModificar.Name = "btnModificar"
        '
        'arrayBotones
        '
        '
        'btnTancar
        '
        Me.btnTancar.Name = "btnTancar"
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.Name = "cboSeleccionCentro"
        '
        'btnVerLista
        '
        Me.btnVerLista.Name = "btnVerLista"
        '
        'btnBorrar
        '
        Me.btnBorrar.Name = "btnBorrar"
        '
        'btnActualizar
        '
        Me.btnActualizar.Name = "btnActualizar"
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.sbtipo.Location = New System.Drawing.Point(5, 78)
        Me.sbtipo.Name = "sbtipo"
        Me.sbtipo.Text = ""
        '
        'btnNuevo
        '
        Me.btnNuevo.Name = "btnNuevo"
        '
        'rdoEscogerTemporada
        '
        Me.rdoEscogerTemporada.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoEscogerTemporada.Location = New System.Drawing.Point(136, 16)
        Me.rdoEscogerTemporada.Name = "rdoEscogerTemporada"
        Me.rdoEscogerTemporada.Size = New System.Drawing.Size(180, 15)
        Me.rdoEscogerTemporada.TabIndex = 1
        Me.rdoEscogerTemporada.Text = "Escollir Temporades a Mostrar"
        '
        'rdoEscogerTodasTemporadas
        '
        Me.rdoEscogerTodasTemporadas.Checked = True
        Me.rdoEscogerTodasTemporadas.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoEscogerTodasTemporadas.Location = New System.Drawing.Point(4, 16)
        Me.rdoEscogerTodasTemporadas.Name = "rdoEscogerTodasTemporadas"
        Me.rdoEscogerTodasTemporadas.Size = New System.Drawing.Size(132, 15)
        Me.rdoEscogerTodasTemporadas.TabIndex = 0
        Me.rdoEscogerTodasTemporadas.TabStop = True
        Me.rdoEscogerTodasTemporadas.Text = "Totes les Tempordes"
        '
        'gbEleccionCliente
        '
        Me.gbEleccionCliente.BackColor = System.Drawing.SystemColors.Control
        Me.gbEleccionCliente.Controls.Add(Me.cboSeleccionCliente)
        Me.gbEleccionCliente.Controls.Add(Me.rdoVerLosModelosTodosLosClientes)
        Me.gbEleccionCliente.Controls.Add(Me.rdoVerModelosDeCliente)
        Me.gbEleccionCliente.Location = New System.Drawing.Point(344, 4)
        Me.gbEleccionCliente.Name = "gbEleccionCliente"
        Me.gbEleccionCliente.Size = New System.Drawing.Size(472, 40)
        Me.gbEleccionCliente.TabIndex = 2
        Me.gbEleccionCliente.TabStop = False
        Me.gbEleccionCliente.Text = "El·lecció de Models"
        '
        'cboSeleccionCliente
        '
        Me.cboSeleccionCliente.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cboSeleccionCliente.AutoCompletion = True
        Me.cboSeleccionCliente.AutoSelect = True
        Me.cboSeleccionCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboSeleccionCliente.Caption = ""
        Me.cboSeleccionCliente.CaptionHeight = 17
        Me.cboSeleccionCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboSeleccionCliente.ColumnCaptionHeight = 17
        Me.cboSeleccionCliente.ColumnFooterHeight = 17
        Me.cboSeleccionCliente.ContentHeight = 15
        Me.cboSeleccionCliente.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSeleccionCliente.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboSeleccionCliente.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboSeleccionCliente.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboSeleccionCliente.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboSeleccionCliente.EditorHeight = 15
        Me.cboSeleccionCliente.GapHeight = 2
        Me.cboSeleccionCliente.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.cboSeleccionCliente.IntegralHeight = True
        Me.cboSeleccionCliente.ItemHeight = 13
        Me.cboSeleccionCliente.Location = New System.Drawing.Point(220, 16)
        Me.cboSeleccionCliente.MatchEntryTimeout = CType(100, Long)
        Me.cboSeleccionCliente.MaxDropDownItems = CType(8, Short)
        Me.cboSeleccionCliente.MaxLength = 0
        Me.cboSeleccionCliente.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboSeleccionCliente.Name = "cboSeleccionCliente"
        'Me.cbo.PartialRightColumn = False
        Me.cboSeleccionCliente.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboSeleccionCliente.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboSeleccionCliente.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboSeleccionCliente.Size = New System.Drawing.Size(244, 19)
        Me.cboSeleccionCliente.TabIndex = 2
        Me.cboSeleccionCliente.Visible = False
        Me.cboSeleccionCliente.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;" & _
        "}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlT" & _
        "ext;BackColor:Control;}Style8{}Style10{}Style11{}Style1{}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'rdoVerLosModelosTodosLosClientes
        '
        Me.rdoVerLosModelosTodosLosClientes.Checked = True
        Me.rdoVerLosModelosTodosLosClientes.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoVerLosModelosTodosLosClientes.Location = New System.Drawing.Point(8, 20)
        Me.rdoVerLosModelosTodosLosClientes.Name = "rdoVerLosModelosTodosLosClientes"
        Me.rdoVerLosModelosTodosLosClientes.Size = New System.Drawing.Size(132, 15)
        Me.rdoVerLosModelosTodosLosClientes.TabIndex = 0
        Me.rdoVerLosModelosTodosLosClientes.TabStop = True
        Me.rdoVerLosModelosTodosLosClientes.Text = "Tots els Clients"
        '
        'rdoVerModelosDeCliente
        '
        Me.rdoVerModelosDeCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdoVerModelosDeCliente.Location = New System.Drawing.Point(140, 20)
        Me.rdoVerModelosDeCliente.Name = "rdoVerModelosDeCliente"
        Me.rdoVerModelosDeCliente.Size = New System.Drawing.Size(80, 15)
        Me.rdoVerModelosDeCliente.TabIndex = 1
        Me.rdoVerModelosDeCliente.Text = "Del Client:"
        '
        'gbEleccionTemporadas
        '
        Me.gbEleccionTemporadas.BackColor = System.Drawing.SystemColors.Control
        Me.gbEleccionTemporadas.Controls.Add(Me.rdoEscogerTemporada)
        Me.gbEleccionTemporadas.Controls.Add(Me.rdoEscogerTodasTemporadas)
        Me.gbEleccionTemporadas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.gbEleccionTemporadas.Location = New System.Drawing.Point(8, 4)
        Me.gbEleccionTemporadas.Name = "gbEleccionTemporadas"
        Me.gbEleccionTemporadas.Size = New System.Drawing.Size(328, 40)
        Me.gbEleccionTemporadas.TabIndex = 5
        Me.gbEleccionTemporadas.TabStop = False
        '
        'tabControlModelos
        '
        Me.tabControlModelos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabControlModelos.Controls.Add(Me.tabPageModelo)
        Me.tabControlModelos.Controls.Add(Me.tabPageForniturasModelo)
        Me.tabControlModelos.Controls.Add(Me.tabPageEscandalloPrecio)
        Me.tabControlModelos.Controls.Add(Me.tabPageColores)
        Me.tabControlModelos.Controls.Add(Me.tabPageStock)
        Me.tabControlModelos.Controls.Add(Me.tabPageFichaModelo)
        Me.tabControlModelos.Controls.Add(Me.tabPageFicha2)
        Me.tabControlModelos.ItemSize = New System.Drawing.Size(42, 18)
        Me.tabControlModelos.Location = New System.Drawing.Point(8, 44)
        Me.tabControlModelos.Name = "tabControlModelos"
        Me.tabControlModelos.SelectedIndex = 0
        Me.tabControlModelos.Size = New System.Drawing.Size(978, 508)
        Me.tabControlModelos.TabIndex = 2
        '
        'tabPageModelo
        '
        Me.tabPageModelo.BackColor = System.Drawing.SystemColors.Control
        Me.tabPageModelo.Controls.Add(Me.Panel2)
        Me.tabPageModelo.Controls.Add(Me.GroupBox4)
        Me.tabPageModelo.Controls.Add(Me.gbTejido)
        Me.tabPageModelo.Controls.Add(Me.gbConfeccion)
        Me.tabPageModelo.Controls.Add(Me.gbModelo)
        Me.tabPageModelo.Controls.Add(Me.btnCanviarImagen3)
        Me.tabPageModelo.Controls.Add(Me.Button2)
        Me.tabPageModelo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tabPageModelo.Location = New System.Drawing.Point(4, 22)
        Me.tabPageModelo.Name = "tabPageModelo"
        Me.tabPageModelo.Size = New System.Drawing.Size(970, 482)
        Me.tabPageModelo.TabIndex = 0
        Me.tabPageModelo.Text = "Model"
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.Controls.Add(Me.imgModelo3)
        Me.Panel2.Location = New System.Drawing.Point(644, 116)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(320, 336)
        Me.Panel2.TabIndex = 304
        '
        'imgModelo3
        '
        Me.imgModelo3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.imgModelo3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.imgModelo3.Location = New System.Drawing.Point(0, 0)
        Me.imgModelo3.Name = "imgModelo3"
        Me.imgModelo3.Size = New System.Drawing.Size(320, 336)
        Me.imgModelo3.TabIndex = 301
        Me.imgModelo3.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.txtVENDAFINAL)
        Me.GroupBox4.Controls.Add(Me.lblPrecioCoste)
        Me.GroupBox4.Controls.Add(Me.lblPrecioVentaModelo)
        Me.GroupBox4.Controls.Add(Me.txtVENDA)
        Me.GroupBox4.Controls.Add(Me.txtCOST)
        Me.GroupBox4.Location = New System.Drawing.Point(700, 12)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(260, 96)
        Me.GroupBox4.TabIndex = 300
        Me.GroupBox4.TabStop = False
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label14.Location = New System.Drawing.Point(12, 72)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(132, 19)
        Me.Label14.TabIndex = 301
        Me.Label14.Text = "Preu Venda Final"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVENDAFINAL
        '
        Me.txtVENDAFINAL.BackColor = System.Drawing.Color.FromArgb(CType(230, Byte), CType(134, Byte), CType(134, Byte))
        Me.txtVENDAFINAL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVENDAFINAL.Location = New System.Drawing.Point(148, 68)
        Me.txtVENDAFINAL.Name = "txtVENDAFINAL"
        Me.txtVENDAFINAL.Size = New System.Drawing.Size(80, 22)
        Me.txtVENDAFINAL.TabIndex = 300
        Me.txtVENDAFINAL.Tag = Nothing
        '
        'lblPrecioCoste
        '
        Me.lblPrecioCoste.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblPrecioCoste.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPrecioCoste.Location = New System.Drawing.Point(12, 24)
        Me.lblPrecioCoste.Name = "lblPrecioCoste"
        Me.lblPrecioCoste.Size = New System.Drawing.Size(132, 19)
        Me.lblPrecioCoste.TabIndex = 299
        Me.lblPrecioCoste.Text = "Preu Cost"
        Me.lblPrecioCoste.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPrecioVentaModelo
        '
        Me.lblPrecioVentaModelo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblPrecioVentaModelo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPrecioVentaModelo.Location = New System.Drawing.Point(12, 44)
        Me.lblPrecioVentaModelo.Name = "lblPrecioVentaModelo"
        Me.lblPrecioVentaModelo.Size = New System.Drawing.Size(132, 19)
        Me.lblPrecioVentaModelo.TabIndex = 298
        Me.lblPrecioVentaModelo.Text = "Preu Venda"
        Me.lblPrecioVentaModelo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVENDA
        '
        Me.txtVENDA.BackColor = System.Drawing.Color.Khaki
        Me.txtVENDA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtVENDA.Location = New System.Drawing.Point(148, 44)
        Me.txtVENDA.Name = "txtVENDA"
        Me.txtVENDA.Size = New System.Drawing.Size(80, 20)
        Me.txtVENDA.TabIndex = 297
        Me.txtVENDA.Tag = Nothing
        '
        'txtCOST
        '
        Me.txtCOST.BackColor = System.Drawing.Color.Khaki
        Me.txtCOST.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCOST.Location = New System.Drawing.Point(148, 20)
        Me.txtCOST.Name = "txtCOST"
        Me.txtCOST.Size = New System.Drawing.Size(80, 20)
        Me.txtCOST.TabIndex = 296
        Me.txtCOST.Tag = Nothing
        '
        'gbTejido
        '
        Me.gbTejido.Controls.Add(Me.cboNOMACA)
        Me.gbTejido.Controls.Add(Me.cboNOMESTAM)
        Me.gbTejido.Controls.Add(Me.cboNOMTINT)
        Me.gbTejido.Controls.Add(Me.cboNOMPROVE)
        Me.gbTejido.Controls.Add(Me.cboTEIXIT)
        Me.gbTejido.Controls.Add(Me.btnElegirAcabador)
        Me.gbTejido.Controls.Add(Me.btnElegirEstampador)
        Me.gbTejido.Controls.Add(Me.btnElegirTintador)
        Me.gbTejido.Controls.Add(Me.btnElegirProveedor)
        Me.gbTejido.Controls.Add(Me.lblRendimientoKGTejido)
        Me.gbTejido.Controls.Add(Me.txtRENDIM)
        Me.gbTejido.Controls.Add(Me.lblTinteTejido)
        Me.gbTejido.Controls.Add(Me.lblEstampadorTejido)
        Me.gbTejido.Controls.Add(Me.lblAcabadorTejido)
        Me.gbTejido.Controls.Add(Me.btnElegirTejido)
        Me.gbTejido.Controls.Add(Me.txtAMPLE)
        Me.gbTejido.Controls.Add(Me.txtTINT)
        Me.gbTejido.Controls.Add(Me.txtESTAM)
        Me.gbTejido.Controls.Add(Me.txtACA)
        Me.gbTejido.Controls.Add(Me.txtPROVE)
        Me.gbTejido.Controls.Add(Me.txtDESCRITEIXIT)
        Me.gbTejido.Controls.Add(Me.lblTejido)
        Me.gbTejido.Controls.Add(Me.lblProveedorTejido)
        Me.gbTejido.Controls.Add(Me.llbAnchoPiezaTejido)
        Me.gbTejido.Location = New System.Drawing.Point(8, 240)
        Me.gbTejido.Name = "gbTejido"
        Me.gbTejido.Size = New System.Drawing.Size(628, 188)
        Me.gbTejido.TabIndex = 18
        Me.gbTejido.TabStop = False
        Me.gbTejido.Text = "Teixit"
        '
        'cboNOMACA
        '
        Me.cboNOMACA.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMACA.AutoSelect = True
        Me.cboNOMACA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMACA.Caption = ""
        Me.cboNOMACA.CaptionHeight = 17
        Me.cboNOMACA.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMACA.ColumnCaptionHeight = 17
        Me.cboNOMACA.ColumnFooterHeight = 17
        Me.cboNOMACA.ContentHeight = 15
        Me.cboNOMACA.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMACA.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMACA.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMACA.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMACA.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMACA.EditorHeight = 15
        Me.cboNOMACA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMACA.GapHeight = 2
        Me.cboNOMACA.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.cboNOMACA.IntegralHeight = True
        Me.cboNOMACA.ItemHeight = 13
        Me.cboNOMACA.Location = New System.Drawing.Point(216, 136)
        Me.cboNOMACA.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMACA.MaxDropDownItems = CType(8, Short)
        Me.cboNOMACA.MaxLength = 0
        Me.cboNOMACA.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMACA.Name = "cboNOMACA"
        'Me.cbo.PartialRightColumn = False
        Me.cboNOMACA.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboNOMACA.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMACA.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMACA.Size = New System.Drawing.Size(396, 19)
        Me.cboNOMACA.TabIndex = 10
        Me.cboNOMACA.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;" & _
        "}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlT" & _
        "ext;BackColor:Control;}Style8{}Style10{}Style11{}Style1{}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'cboNOMESTAM
        '
        Me.cboNOMESTAM.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMESTAM.AutoSelect = True
        Me.cboNOMESTAM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMESTAM.Caption = ""
        Me.cboNOMESTAM.CaptionHeight = 17
        Me.cboNOMESTAM.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMESTAM.ColumnCaptionHeight = 17
        Me.cboNOMESTAM.ColumnFooterHeight = 17
        Me.cboNOMESTAM.ContentHeight = 15
        Me.cboNOMESTAM.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMESTAM.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMESTAM.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMESTAM.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMESTAM.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMESTAM.EditorHeight = 15
        Me.cboNOMESTAM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMESTAM.GapHeight = 2
        Me.cboNOMESTAM.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Image))
        Me.cboNOMESTAM.IntegralHeight = True
        Me.cboNOMESTAM.ItemHeight = 13
        Me.cboNOMESTAM.Location = New System.Drawing.Point(216, 113)
        Me.cboNOMESTAM.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMESTAM.MaxDropDownItems = CType(8, Short)
        Me.cboNOMESTAM.MaxLength = 0
        Me.cboNOMESTAM.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMESTAM.Name = "cboNOMESTAM"
        'Me.cbo.PartialRightColumn = False
        Me.cboNOMESTAM.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboNOMESTAM.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMESTAM.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMESTAM.Size = New System.Drawing.Size(396, 19)
        Me.cboNOMESTAM.TabIndex = 8
        Me.cboNOMESTAM.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;" & _
        "}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlT" & _
        "ext;BackColor:Control;}Style8{}Style10{}Style11{}Style1{}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'cboNOMTINT
        '
        Me.cboNOMTINT.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMTINT.AutoSelect = True
        Me.cboNOMTINT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMTINT.Caption = ""
        Me.cboNOMTINT.CaptionHeight = 17
        Me.cboNOMTINT.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMTINT.ColumnCaptionHeight = 17
        Me.cboNOMTINT.ColumnFooterHeight = 17
        Me.cboNOMTINT.ContentHeight = 15
        Me.cboNOMTINT.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMTINT.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMTINT.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMTINT.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMTINT.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMTINT.EditorHeight = 15
        Me.cboNOMTINT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMTINT.GapHeight = 2
        Me.cboNOMTINT.Images.Add(CType(resources.GetObject("resource3"), System.Drawing.Image))
        Me.cboNOMTINT.IntegralHeight = True
        Me.cboNOMTINT.ItemHeight = 13
        Me.cboNOMTINT.Location = New System.Drawing.Point(216, 88)
        Me.cboNOMTINT.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMTINT.MaxDropDownItems = CType(8, Short)
        Me.cboNOMTINT.MaxLength = 0
        Me.cboNOMTINT.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMTINT.Name = "cboNOMTINT"
        'Me.cbo.PartialRightColumn = False
        Me.cboNOMTINT.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboNOMTINT.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMTINT.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMTINT.Size = New System.Drawing.Size(396, 19)
        Me.cboNOMTINT.TabIndex = 6
        Me.cboNOMTINT.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;" & _
        "}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlT" & _
        "ext;BackColor:Control;}Style8{}Style10{}Style11{}Style1{}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'cboNOMPROVE
        '
        Me.cboNOMPROVE.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMPROVE.AutoSelect = True
        Me.cboNOMPROVE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMPROVE.Caption = ""
        Me.cboNOMPROVE.CaptionHeight = 17
        Me.cboNOMPROVE.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMPROVE.ColumnCaptionHeight = 17
        Me.cboNOMPROVE.ColumnFooterHeight = 17
        Me.cboNOMPROVE.ContentHeight = 15
        Me.cboNOMPROVE.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMPROVE.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMPROVE.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMPROVE.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMPROVE.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMPROVE.EditorHeight = 15
        Me.cboNOMPROVE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMPROVE.GapHeight = 2
        Me.cboNOMPROVE.Images.Add(CType(resources.GetObject("resource4"), System.Drawing.Image))
        Me.cboNOMPROVE.IntegralHeight = True
        Me.cboNOMPROVE.ItemHeight = 13
        Me.cboNOMPROVE.Location = New System.Drawing.Point(216, 40)
        Me.cboNOMPROVE.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMPROVE.MaxDropDownItems = CType(8, Short)
        Me.cboNOMPROVE.MaxLength = 0
        Me.cboNOMPROVE.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMPROVE.Name = "cboNOMPROVE"
        'Me.cbo.PartialRightColumn = False
        Me.cboNOMPROVE.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboNOMPROVE.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMPROVE.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMPROVE.Size = New System.Drawing.Size(396, 19)
        Me.cboNOMPROVE.TabIndex = 3
        Me.cboNOMPROVE.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;" & _
        "}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlT" & _
        "ext;BackColor:Control;}Style8{}Style10{}Style11{}Style1{}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'cboTEIXIT
        '
        Me.cboTEIXIT.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cboTEIXIT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboTEIXIT.Caption = ""
        Me.cboTEIXIT.CaptionHeight = 17
        Me.cboTEIXIT.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboTEIXIT.ColumnCaptionHeight = 17
        Me.cboTEIXIT.ColumnFooterHeight = 17
        Me.cboTEIXIT.ContentHeight = 15
        Me.cboTEIXIT.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTEIXIT.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboTEIXIT.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboTEIXIT.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboTEIXIT.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTEIXIT.EditorHeight = 15
        Me.cboTEIXIT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboTEIXIT.GapHeight = 2
        Me.cboTEIXIT.Images.Add(CType(resources.GetObject("resource5"), System.Drawing.Image))
        Me.cboTEIXIT.IntegralHeight = True
        Me.cboTEIXIT.ItemHeight = 13
        Me.cboTEIXIT.Location = New System.Drawing.Point(128, 16)
        Me.cboTEIXIT.MatchEntryTimeout = CType(100, Long)
        Me.cboTEIXIT.MaxDropDownItems = CType(8, Short)
        Me.cboTEIXIT.MaxLength = 10
        Me.cboTEIXIT.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboTEIXIT.Name = "cboTEIXIT"
        'Me.cbo.PartialRightColumn = False
        Me.cboTEIXIT.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboTEIXIT.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboTEIXIT.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboTEIXIT.Size = New System.Drawing.Size(88, 19)
        Me.cboTEIXIT.TabIndex = 0
        Me.cboTEIXIT.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;" & _
        "}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlT" & _
        "ext;BackColor:Control;}Style8{}Style10{}Style11{}Style1{}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'btnElegirAcabador
        '
        Me.btnElegirAcabador.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirAcabador.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnElegirAcabador.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirAcabador.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirAcabador.Location = New System.Drawing.Point(184, 136)
        Me.btnElegirAcabador.Name = "btnElegirAcabador"
        Me.btnElegirAcabador.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirAcabador.TabIndex = 40
        Me.btnElegirAcabador.Text = "..."
        '
        'btnElegirEstampador
        '
        Me.btnElegirEstampador.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirEstampador.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnElegirEstampador.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirEstampador.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirEstampador.Location = New System.Drawing.Point(184, 112)
        Me.btnElegirEstampador.Name = "btnElegirEstampador"
        Me.btnElegirEstampador.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirEstampador.TabIndex = 39
        Me.btnElegirEstampador.Text = "..."
        '
        'btnElegirTintador
        '
        Me.btnElegirTintador.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirTintador.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnElegirTintador.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirTintador.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirTintador.Location = New System.Drawing.Point(184, 88)
        Me.btnElegirTintador.Name = "btnElegirTintador"
        Me.btnElegirTintador.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirTintador.TabIndex = 38
        Me.btnElegirTintador.Text = "..."
        '
        'btnElegirProveedor
        '
        Me.btnElegirProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnElegirProveedor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirProveedor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirProveedor.Location = New System.Drawing.Point(184, 39)
        Me.btnElegirProveedor.Name = "btnElegirProveedor"
        Me.btnElegirProveedor.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirProveedor.TabIndex = 36
        Me.btnElegirProveedor.Text = "..."
        '
        'lblRendimientoKGTejido
        '
        Me.lblRendimientoKGTejido.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblRendimientoKGTejido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRendimientoKGTejido.Location = New System.Drawing.Point(28, 160)
        Me.lblRendimientoKGTejido.Name = "lblRendimientoKGTejido"
        Me.lblRendimientoKGTejido.Size = New System.Drawing.Size(100, 20)
        Me.lblRendimientoKGTejido.TabIndex = 35
        Me.lblRendimientoKGTejido.Text = "Rend KG."
        Me.lblRendimientoKGTejido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRENDIM
        '
        Me.txtRENDIM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtRENDIM.Location = New System.Drawing.Point(128, 160)
        Me.txtRENDIM.Name = "txtRENDIM"
        Me.txtRENDIM.Size = New System.Drawing.Size(52, 20)
        Me.txtRENDIM.TabIndex = 11
        Me.txtRENDIM.Tag = Nothing
        '
        'lblTinteTejido
        '
        Me.lblTinteTejido.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblTinteTejido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTinteTejido.Location = New System.Drawing.Point(28, 88)
        Me.lblTinteTejido.Name = "lblTinteTejido"
        Me.lblTinteTejido.Size = New System.Drawing.Size(100, 20)
        Me.lblTinteTejido.TabIndex = 31
        Me.lblTinteTejido.Text = "Tint"
        Me.lblTinteTejido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEstampadorTejido
        '
        Me.lblEstampadorTejido.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblEstampadorTejido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblEstampadorTejido.Location = New System.Drawing.Point(28, 112)
        Me.lblEstampadorTejido.Name = "lblEstampadorTejido"
        Me.lblEstampadorTejido.Size = New System.Drawing.Size(100, 20)
        Me.lblEstampadorTejido.TabIndex = 32
        Me.lblEstampadorTejido.Text = "Estampador"
        Me.lblEstampadorTejido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAcabadorTejido
        '
        Me.lblAcabadorTejido.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblAcabadorTejido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAcabadorTejido.Location = New System.Drawing.Point(28, 136)
        Me.lblAcabadorTejido.Name = "lblAcabadorTejido"
        Me.lblAcabadorTejido.Size = New System.Drawing.Size(100, 20)
        Me.lblAcabadorTejido.TabIndex = 33
        Me.lblAcabadorTejido.Text = "Acabador"
        Me.lblAcabadorTejido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnElegirTejido
        '
        Me.btnElegirTejido.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirTejido.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnElegirTejido.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirTejido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirTejido.Location = New System.Drawing.Point(216, 16)
        Me.btnElegirTejido.Name = "btnElegirTejido"
        Me.btnElegirTejido.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirTejido.TabIndex = 30
        Me.btnElegirTejido.Text = "..."
        '
        'txtAMPLE
        '
        Me.txtAMPLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtAMPLE.Location = New System.Drawing.Point(128, 64)
        Me.txtAMPLE.MaxLength = 20
        Me.txtAMPLE.Name = "txtAMPLE"
        Me.txtAMPLE.Size = New System.Drawing.Size(52, 20)
        Me.txtAMPLE.TabIndex = 4
        Me.txtAMPLE.Tag = Nothing
        '
        'txtTINT
        '
        Me.txtTINT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTINT.Location = New System.Drawing.Point(128, 88)
        Me.txtTINT.Name = "txtTINT"
        Me.txtTINT.Size = New System.Drawing.Size(52, 20)
        Me.txtTINT.TabIndex = 5
        Me.txtTINT.Tag = Nothing
        '
        'txtESTAM
        '
        Me.txtESTAM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtESTAM.Location = New System.Drawing.Point(128, 112)
        Me.txtESTAM.Name = "txtESTAM"
        Me.txtESTAM.Size = New System.Drawing.Size(52, 20)
        Me.txtESTAM.TabIndex = 7
        Me.txtESTAM.Tag = Nothing
        '
        'txtACA
        '
        Me.txtACA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtACA.Location = New System.Drawing.Point(128, 136)
        Me.txtACA.Name = "txtACA"
        Me.txtACA.Size = New System.Drawing.Size(52, 20)
        Me.txtACA.TabIndex = 9
        Me.txtACA.Tag = Nothing
        '
        'txtPROVE
        '
        Me.txtPROVE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPROVE.Location = New System.Drawing.Point(128, 39)
        Me.txtPROVE.Name = "txtPROVE"
        Me.txtPROVE.Size = New System.Drawing.Size(52, 20)
        Me.txtPROVE.TabIndex = 2
        Me.txtPROVE.Tag = Nothing
        '
        'txtDESCRITEIXIT
        '
        Me.txtDESCRITEIXIT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDESCRITEIXIT.Location = New System.Drawing.Point(244, 16)
        Me.txtDESCRITEIXIT.Name = "txtDESCRITEIXIT"
        Me.txtDESCRITEIXIT.Size = New System.Drawing.Size(369, 20)
        Me.txtDESCRITEIXIT.TabIndex = 1
        Me.txtDESCRITEIXIT.Tag = Nothing
        '
        'lblTejido
        '
        Me.lblTejido.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblTejido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTejido.Location = New System.Drawing.Point(28, 16)
        Me.lblTejido.Name = "lblTejido"
        Me.lblTejido.Size = New System.Drawing.Size(100, 20)
        Me.lblTejido.TabIndex = 26
        Me.lblTejido.Text = "Teixit"
        Me.lblTejido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblProveedorTejido
        '
        Me.lblProveedorTejido.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblProveedorTejido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblProveedorTejido.Location = New System.Drawing.Point(28, 39)
        Me.lblProveedorTejido.Name = "lblProveedorTejido"
        Me.lblProveedorTejido.Size = New System.Drawing.Size(100, 20)
        Me.lblProveedorTejido.TabIndex = 28
        Me.lblProveedorTejido.Text = "Proveïdor"
        Me.lblProveedorTejido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'llbAnchoPiezaTejido
        '
        Me.llbAnchoPiezaTejido.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.llbAnchoPiezaTejido.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.llbAnchoPiezaTejido.Location = New System.Drawing.Point(28, 64)
        Me.llbAnchoPiezaTejido.Name = "llbAnchoPiezaTejido"
        Me.llbAnchoPiezaTejido.Size = New System.Drawing.Size(100, 20)
        Me.llbAnchoPiezaTejido.TabIndex = 29
        Me.llbAnchoPiezaTejido.Text = "Ample Peça"
        Me.llbAnchoPiezaTejido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gbConfeccion
        '
        Me.gbConfeccion.Controls.Add(Me.cboNOMCONFEC)
        Me.gbConfeccion.Controls.Add(Me.btnElegirTallerConfeccion)
        Me.gbConfeccion.Controls.Add(Me.txtCONFEC)
        Me.gbConfeccion.Controls.Add(Me.lblTallerConfeccion)
        Me.gbConfeccion.Location = New System.Drawing.Point(8, 432)
        Me.gbConfeccion.Name = "gbConfeccion"
        Me.gbConfeccion.Size = New System.Drawing.Size(628, 48)
        Me.gbConfeccion.TabIndex = 0
        Me.gbConfeccion.TabStop = False
        Me.gbConfeccion.Text = "Confecció"
        '
        'cboNOMCONFEC
        '
        Me.cboNOMCONFEC.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMCONFEC.AutoSelect = True
        Me.cboNOMCONFEC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMCONFEC.Caption = ""
        Me.cboNOMCONFEC.CaptionHeight = 17
        Me.cboNOMCONFEC.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMCONFEC.ColumnCaptionHeight = 17
        Me.cboNOMCONFEC.ColumnFooterHeight = 17
        Me.cboNOMCONFEC.ContentHeight = 15
        Me.cboNOMCONFEC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMCONFEC.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMCONFEC.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMCONFEC.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMCONFEC.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMCONFEC.EditorHeight = 15
        Me.cboNOMCONFEC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMCONFEC.GapHeight = 2
        Me.cboNOMCONFEC.Images.Add(CType(resources.GetObject("resource6"), System.Drawing.Image))
        Me.cboNOMCONFEC.IntegralHeight = True
        Me.cboNOMCONFEC.ItemHeight = 13
        Me.cboNOMCONFEC.Location = New System.Drawing.Point(216, 19)
        Me.cboNOMCONFEC.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMCONFEC.MaxDropDownItems = CType(8, Short)
        Me.cboNOMCONFEC.MaxLength = 0
        Me.cboNOMCONFEC.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMCONFEC.Name = "cboNOMCONFEC"
        'Me.cbo.PartialRightColumn = False
        Me.cboNOMCONFEC.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboNOMCONFEC.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMCONFEC.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMCONFEC.Size = New System.Drawing.Size(396, 19)
        Me.cboNOMCONFEC.TabIndex = 1
        Me.cboNOMCONFEC.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;" & _
        "}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlT" & _
        "ext;BackColor:Control;}Style8{}Style10{}Style11{}Style1{}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'btnElegirTallerConfeccion
        '
        Me.btnElegirTallerConfeccion.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirTallerConfeccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnElegirTallerConfeccion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirTallerConfeccion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirTallerConfeccion.Location = New System.Drawing.Point(180, 18)
        Me.btnElegirTallerConfeccion.Name = "btnElegirTallerConfeccion"
        Me.btnElegirTallerConfeccion.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirTallerConfeccion.TabIndex = 34
        Me.btnElegirTallerConfeccion.Text = "..."
        '
        'txtCONFEC
        '
        Me.txtCONFEC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCONFEC.Location = New System.Drawing.Point(128, 18)
        Me.txtCONFEC.Name = "txtCONFEC"
        Me.txtCONFEC.Size = New System.Drawing.Size(52, 20)
        Me.txtCONFEC.TabIndex = 0
        Me.txtCONFEC.Tag = Nothing
        '
        'lblTallerConfeccion
        '
        Me.lblTallerConfeccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblTallerConfeccion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTallerConfeccion.Location = New System.Drawing.Point(28, 18)
        Me.lblTallerConfeccion.Name = "lblTallerConfeccion"
        Me.lblTallerConfeccion.Size = New System.Drawing.Size(100, 20)
        Me.lblTallerConfeccion.TabIndex = 33
        Me.lblTallerConfeccion.Text = "Taller"
        Me.lblTallerConfeccion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gbModelo
        '
        Me.gbModelo.Controls.Add(Me.cboTEMPORADA)
        Me.gbModelo.Controls.Add(Me.btnEscogerModelo)
        Me.gbModelo.Controls.Add(Me.cboID)
        Me.gbModelo.Controls.Add(Me.txtCODIMODEL)
        Me.gbModelo.Controls.Add(Me.lblCodigoModelo)
        Me.gbModelo.Controls.Add(Me.cboNOMCLIENT)
        Me.gbModelo.Controls.Add(Me.txtSERIE)
        Me.gbModelo.Controls.Add(Me.lblSerie)
        Me.gbModelo.Controls.Add(Me.btnElegirCliente)
        Me.gbModelo.Controls.Add(Me.lblTemporada)
        Me.gbModelo.Controls.Add(Me.lblCliente)
        Me.gbModelo.Controls.Add(Me.lblDescripcion)
        Me.gbModelo.Controls.Add(Me.txtCLIENT)
        Me.gbModelo.Controls.Add(Me.txtDESCRI)
        Me.gbModelo.Controls.Add(Me.lblModelo)
        Me.gbModelo.Controls.Add(Me.txtOBSERV)
        Me.gbModelo.Controls.Add(Me.Label1)
        Me.gbModelo.Location = New System.Drawing.Point(8, 20)
        Me.gbModelo.Name = "gbModelo"
        Me.gbModelo.Size = New System.Drawing.Size(628, 212)
        Me.gbModelo.TabIndex = 0
        Me.gbModelo.TabStop = False
        Me.gbModelo.Text = "Model"
        '
        'cboTEMPORADA
        '
        Me.cboTEMPORADA.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cboTEMPORADA.AutoSelect = True
        Me.cboTEMPORADA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboTEMPORADA.Caption = ""
        Me.cboTEMPORADA.CaptionHeight = 17
        Me.cboTEMPORADA.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboTEMPORADA.ColumnCaptionHeight = 17
        Me.cboTEMPORADA.ColumnFooterHeight = 17
        Me.cboTEMPORADA.ContentHeight = 15
        Me.cboTEMPORADA.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTEMPORADA.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboTEMPORADA.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboTEMPORADA.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboTEMPORADA.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTEMPORADA.EditorHeight = 15
        Me.cboTEMPORADA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboTEMPORADA.GapHeight = 2
        Me.cboTEMPORADA.Images.Add(CType(resources.GetObject("resource7"), System.Drawing.Image))
        Me.cboTEMPORADA.IntegralHeight = True
        Me.cboTEMPORADA.ItemHeight = 13
        Me.cboTEMPORADA.Location = New System.Drawing.Point(504, 20)
        Me.cboTEMPORADA.MatchEntryTimeout = CType(100, Long)
        Me.cboTEMPORADA.MaxDropDownItems = CType(15, Short)
        Me.cboTEMPORADA.MaxLength = 10
        Me.cboTEMPORADA.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboTEMPORADA.Name = "cboTEMPORADA"
        'Me.cbo.PartialRightColumn = False
        Me.cboTEMPORADA.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboTEMPORADA.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboTEMPORADA.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboTEMPORADA.Size = New System.Drawing.Size(100, 19)
        Me.cboTEMPORADA.TabIndex = 32
        Me.cboTEMPORADA.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:T" & _
        "rue;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:" & _
        "Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:Near;}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'btnEscogerModelo
        '
        Me.btnEscogerModelo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEscogerModelo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnEscogerModelo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEscogerModelo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnEscogerModelo.Location = New System.Drawing.Point(248, 20)
        Me.btnEscogerModelo.Name = "btnEscogerModelo"
        Me.btnEscogerModelo.Size = New System.Drawing.Size(24, 20)
        Me.btnEscogerModelo.TabIndex = 31
        Me.btnEscogerModelo.Text = "..."
        Me.btnEscogerModelo.Visible = False
        '
        'cboID
        '
        Me.cboID.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cboID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboID.Caption = ""
        Me.cboID.CaptionHeight = 17
        Me.cboID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboID.ColumnCaptionHeight = 17
        Me.cboID.ColumnFooterHeight = 17
        Me.cboID.ContentHeight = 15
        Me.cboID.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboID.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboID.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboID.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboID.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboID.EditorHeight = 15
        Me.cboID.GapHeight = 2
        Me.cboID.Images.Add(CType(resources.GetObject("resource8"), System.Drawing.Image))
        Me.cboID.IntegralHeight = True
        Me.cboID.ItemHeight = 13
        Me.cboID.Location = New System.Drawing.Point(128, 20)
        Me.cboID.MatchEntryTimeout = CType(100, Long)
        Me.cboID.MaxDropDownItems = CType(15, Short)
        Me.cboID.MaxLength = 10
        Me.cboID.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboID.Name = "cboID"
        ''Me.cbo.PartialRightColumn = False
        Me.cboID.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboID.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboID.Size = New System.Drawing.Size(121, 19)
        Me.cboID.TabIndex = 0
        Me.cboID.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;" & _
        "}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlT" & _
        "ext;BackColor:Control;}Style8{}Style10{}Style11{}Style1{}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'txtCODIMODEL
        '
        Me.txtCODIMODEL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCODIMODEL.Location = New System.Drawing.Point(128, 48)
        Me.txtCODIMODEL.Name = "txtCODIMODEL"
        Me.txtCODIMODEL.Size = New System.Drawing.Size(96, 20)
        Me.txtCODIMODEL.TabIndex = 1
        Me.txtCODIMODEL.Tag = Nothing
        '
        'lblCodigoModelo
        '
        Me.lblCodigoModelo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblCodigoModelo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCodigoModelo.Location = New System.Drawing.Point(28, 48)
        Me.lblCodigoModelo.Name = "lblCodigoModelo"
        Me.lblCodigoModelo.Size = New System.Drawing.Size(100, 20)
        Me.lblCodigoModelo.TabIndex = 29
        Me.lblCodigoModelo.Text = "Codi Model"
        Me.lblCodigoModelo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboNOMCLIENT
        '
        Me.cboNOMCLIENT.AddItemSeparator = Microsoft.VisualBasic.ChrW(59)
        Me.cboNOMCLIENT.AutoSelect = True
        Me.cboNOMCLIENT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboNOMCLIENT.Caption = ""
        Me.cboNOMCLIENT.CaptionHeight = 17
        Me.cboNOMCLIENT.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboNOMCLIENT.ColumnCaptionHeight = 17
        Me.cboNOMCLIENT.ColumnFooterHeight = 17
        Me.cboNOMCLIENT.ContentHeight = 15
        Me.cboNOMCLIENT.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMCLIENT.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboNOMCLIENT.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboNOMCLIENT.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMCLIENT.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNOMCLIENT.EditorHeight = 15
        Me.cboNOMCLIENT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboNOMCLIENT.GapHeight = 2
        Me.cboNOMCLIENT.Images.Add(CType(resources.GetObject("resource9"), System.Drawing.Image))
        Me.cboNOMCLIENT.IntegralHeight = True
        Me.cboNOMCLIENT.ItemHeight = 13
        Me.cboNOMCLIENT.Location = New System.Drawing.Point(220, 180)
        Me.cboNOMCLIENT.MatchEntryTimeout = CType(100, Long)
        Me.cboNOMCLIENT.MaxDropDownItems = CType(8, Short)
        Me.cboNOMCLIENT.MaxLength = 0
        Me.cboNOMCLIENT.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboNOMCLIENT.Name = "cboNOMCLIENT"
        'Me.cbo.PartialRightColumn = False
        Me.cboNOMCLIENT.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboNOMCLIENT.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.cboNOMCLIENT.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboNOMCLIENT.Size = New System.Drawing.Size(396, 19)
        Me.cboNOMCLIENT.TabIndex = 5
        Me.cboNOMCLIENT.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1List.Design.ContextWrapper""><Da" & _
        "ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" & _
        "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" & _
        "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" & _
        "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft" & _
        " Sans Serif, 8.25pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackC" & _
        "olor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;" & _
        "}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlT" & _
        "ext;BackColor:Control;}Style8{}Style10{}Style11{}Style1{}</Data></Styles><Splits" & _
        "><C1.Win.C1List.ListBoxView AllowColSelect=""False"" Name="""" CaptionHeight=""17"" Co" & _
        "lumnCaptionHeight=""17"" ColumnFooterHeight=""17"" VerticalScrollGroup=""1"" Horizonta" & _
        "lScrollGroup=""1""><ClientRect>0, 0, 116, 156</ClientRect><VScrollBar><Width>17</W" & _
        "idth></VScrollBar><HScrollBar><Height>17</Height></HScrollBar><CaptionStyle pare" & _
        "nt=""Style2"" me=""Style9"" /><EvenRowStyle parent=""EvenRow"" me=""Style7"" /><FooterSt" & _
        "yle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style11"" /><Hea" & _
        "dingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow" & _
        """ me=""Style6"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle pare" & _
        "nt=""OddRow"" me=""Style8"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style" & _
        "10"" /><SelectedStyle parent=""Selected"" me=""Style5"" /><Style parent="""" me=""Normal" & _
        """ /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent="""" me=""Norma" & _
        "l"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /" & _
        "><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" />" & _
        "<Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""HighlightRow"" " & _
        "/><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><St" & _
        "yle parent=""Heading"" me=""RecordSelector"" /><Style parent=""Caption"" me=""Group"" />" & _
        "</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" & _
        "ed</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>"
        '
        'txtSERIE
        '
        Me.txtSERIE.BackColor = System.Drawing.Color.MediumAquamarine
        Me.txtSERIE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtSERIE.Location = New System.Drawing.Point(504, 48)
        Me.txtSERIE.MaxLength = 10
        Me.txtSERIE.Name = "txtSERIE"
        Me.txtSERIE.TabIndex = 2
        Me.txtSERIE.Tag = Nothing
        '
        'lblSerie
        '
        Me.lblSerie.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblSerie.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblSerie.Location = New System.Drawing.Point(420, 48)
        Me.lblSerie.Name = "lblSerie"
        Me.lblSerie.Size = New System.Drawing.Size(80, 20)
        Me.lblSerie.TabIndex = 22
        Me.lblSerie.Text = "Sèrie"
        Me.lblSerie.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnElegirCliente
        '
        Me.btnElegirCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnElegirCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnElegirCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElegirCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnElegirCliente.Location = New System.Drawing.Point(188, 180)
        Me.btnElegirCliente.Name = "btnElegirCliente"
        Me.btnElegirCliente.Size = New System.Drawing.Size(24, 20)
        Me.btnElegirCliente.TabIndex = 21
        Me.btnElegirCliente.Text = "..."
        '
        'lblTemporada
        '
        Me.lblTemporada.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblTemporada.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTemporada.Location = New System.Drawing.Point(420, 20)
        Me.lblTemporada.Name = "lblTemporada"
        Me.lblTemporada.Size = New System.Drawing.Size(80, 20)
        Me.lblTemporada.TabIndex = 20
        Me.lblTemporada.Text = "Temporada"
        Me.lblTemporada.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCliente
        '
        Me.lblCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblCliente.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCliente.Location = New System.Drawing.Point(36, 180)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(100, 20)
        Me.lblCliente.TabIndex = 19
        Me.lblCliente.Text = "Client"
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDescripcion
        '
        Me.lblDescripcion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblDescripcion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDescripcion.Location = New System.Drawing.Point(28, 80)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(100, 20)
        Me.lblDescripcion.TabIndex = 18
        Me.lblDescripcion.Text = "Descripció"
        Me.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCLIENT
        '
        Me.txtCLIENT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCLIENT.Location = New System.Drawing.Point(136, 180)
        Me.txtCLIENT.Name = "txtCLIENT"
        Me.txtCLIENT.Size = New System.Drawing.Size(52, 20)
        Me.txtCLIENT.TabIndex = 4
        Me.txtCLIENT.Tag = Nothing
        '
        'txtDESCRI
        '
        Me.txtDESCRI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDESCRI.Location = New System.Drawing.Point(128, 80)
        Me.txtDESCRI.MaxLength = 50
        Me.txtDESCRI.Multiline = True
        Me.txtDESCRI.Name = "txtDESCRI"
        Me.txtDESCRI.Size = New System.Drawing.Size(480, 20)
        Me.txtDESCRI.TabIndex = 3
        Me.txtDESCRI.Tag = Nothing
        '
        'lblModelo
        '
        Me.lblModelo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblModelo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblModelo.Location = New System.Drawing.Point(28, 20)
        Me.lblModelo.Name = "lblModelo"
        Me.lblModelo.Size = New System.Drawing.Size(100, 20)
        Me.lblModelo.TabIndex = 17
        Me.lblModelo.Text = "Model"
        Me.lblModelo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOBSERV
        '
        Me.txtOBSERV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtOBSERV.Location = New System.Drawing.Point(132, 108)
        Me.txtOBSERV.Multiline = True
        Me.txtOBSERV.Name = "txtOBSERV"
        Me.txtOBSERV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOBSERV.Size = New System.Drawing.Size(476, 68)
        Me.txtOBSERV.TabIndex = 7
        Me.txtOBSERV.Tag = Nothing
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(76, 108)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 15)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Notes"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCanviarImagen3
        '
        Me.btnCanviarImagen3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCanviarImagen3.Location = New System.Drawing.Point(772, 456)
        Me.btnCanviarImagen3.Name = "btnCanviarImagen3"
        Me.btnCanviarImagen3.Size = New System.Drawing.Size(108, 23)
        Me.btnCanviarImagen3.TabIndex = 302
        Me.btnCanviarImagen3.Text = "Canviar Imatge"
        '
        'Button2
        '
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(888, 456)
        Me.Button2.Name = "Button2"
        Me.Button2.TabIndex = 303
        Me.Button2.Text = "Esborrar"
        '
        'tabPageForniturasModelo
        '
        Me.tabPageForniturasModelo.BackColor = System.Drawing.SystemColors.Control
        Me.tabPageForniturasModelo.Controls.Add(Me.cboMedidasForni)
        Me.tabPageForniturasModelo.Controls.Add(Me.GroupBox3)
        Me.tabPageForniturasModelo.Controls.Add(Me.GroupBox12)
        Me.tabPageForniturasModelo.Controls.Add(Me.cboFornituras)
        Me.tabPageForniturasModelo.Controls.Add(Me.dgForni)
        Me.tabPageForniturasModelo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tabPageForniturasModelo.Location = New System.Drawing.Point(4, 22)
        Me.tabPageForniturasModelo.Name = "tabPageForniturasModelo"
        Me.tabPageForniturasModelo.Size = New System.Drawing.Size(970, 482)
        Me.tabPageForniturasModelo.TabIndex = 1
        Me.tabPageForniturasModelo.Text = "Fornitures Per Model"
        '
        'cboMedidasForni
        '
        Me.cboMedidasForni.AllowColMove = False
        Me.cboMedidasForni.AllowColSelect = True
        Me.cboMedidasForni.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboMedidasForni.AlternatingRows = True
        Me.cboMedidasForni.CaptionHeight = 17
        Me.cboMedidasForni.ColumnCaptionHeight = 17
        Me.cboMedidasForni.ColumnFooterHeight = 17
        Me.cboMedidasForni.ExtendRightColumn = True
        Me.cboMedidasForni.FetchRowStyles = True
        Me.cboMedidasForni.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.cboMedidasForni.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboMedidasForni.Images.Add(CType(resources.GetObject("resource10"), System.Drawing.Image))
        Me.cboMedidasForni.Location = New System.Drawing.Point(136, 128)
        Me.cboMedidasForni.Name = "cboMedidasForni"
        Me.cboMedidasForni.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboMedidasForni.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.cboMedidasForni.RowHeight = 15
        Me.cboMedidasForni.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboMedidasForni.ScrollTips = False
        Me.cboMedidasForni.Size = New System.Drawing.Size(76, 148)
        Me.cboMedidasForni.TabIndex = 304
        Me.cboMedidasForni.Text = "C1TrueDBDropdown1"
        Me.cboMedidasForni.ValueTranslate = True
        Me.cboMedidasForni.Visible = False
        Me.cboMedidasForni.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:HighlightTe" & _
        "xt;BackColor:Highlight;}Style9{}OddRow{}RecordSelector{AlignImage:Center;}Headin" & _
        "g{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;Bac" & _
        "kColor:Control;}Style8{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style" & _
        "1{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.DropdownView AllowColMove=""False" & _
        """ Name="""" AlternatingRowStyle=""True"" CaptionHeight=""17"" ColumnCaptionHeight=""17""" & _
        " ColumnFooterHeight=""17"" ExtendRightColumn=""True"" FetchRowStyles=""True"" MarqueeS" & _
        "tyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=""17"" RecordSelec" & _
        "tors=""False"" VerticalScrollGroup=""1"" HorizontalScrollGroup=""1""><CaptionStyle par" & _
        "ent=""Style2"" me=""Style10"" /><EditorStyle parent=""Editor"" me=""Style5"" /><EvenRowS" & _
        "tyle parent=""EvenRow"" me=""Style8"" /><FilterBarStyle parent=""FilterBar"" me=""Style" & _
        "13"" /><FooterStyle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""" & _
        "Style12"" /><HeadingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle paren" & _
        "t=""HighlightRow"" me=""Style7"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><O" & _
        "ddRowStyle parent=""OddRow"" me=""Style9"" /><RecordSelectorStyle parent=""RecordSele" & _
        "ctor"" me=""Style11"" /><SelectedStyle parent=""Selected"" me=""Style6"" /><Style paren" & _
        "t=""Normal"" me=""Style1"" /><ClientRect>0, 0, 72, 144</ClientRect><BorderSide>0</Bo" & _
        "rderSide></C1.Win.C1TrueDBGrid.DropdownView></Splits><NamedStyles><Style parent=" & _
        """"" me=""Normal"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" m" & _
        "e=""Footer"" /><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""" & _
        "Inactive"" /><Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Ed" & _
        "itor"" /><Style parent=""Normal"" me=""HighlightRow"" /><Style parent=""Normal"" me=""Ev" & _
        "enRow"" /><Style parent=""Normal"" me=""OddRow"" /><Style parent=""Heading"" me=""Record" & _
        "Selector"" /><Style parent=""Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""" & _
        "Group"" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layo" & _
        "ut>None</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 72," & _
        " 144</ClientArea></Blob>"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.tx3NOMCLIENT)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.tx3SERIE)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.tx3CLIENT)
        Me.GroupBox3.Controls.Add(Me.tx3CODI)
        Me.GroupBox3.Location = New System.Drawing.Point(24, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(412, 96)
        Me.GroupBox3.TabIndex = 303
        Me.GroupBox3.TabStop = False
        '
        'tx3NOMCLIENT
        '
        Me.tx3NOMCLIENT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx3NOMCLIENT.Location = New System.Drawing.Point(120, 64)
        Me.tx3NOMCLIENT.Name = "tx3NOMCLIENT"
        Me.tx3NOMCLIENT.ReadOnly = True
        Me.tx3NOMCLIENT.Size = New System.Drawing.Size(276, 20)
        Me.tx3NOMCLIENT.TabIndex = 31
        Me.tx3NOMCLIENT.Tag = Nothing
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(16, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 20)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Model"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx3SERIE
        '
        Me.tx3SERIE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx3SERIE.Location = New System.Drawing.Point(68, 40)
        Me.tx3SERIE.Name = "tx3SERIE"
        Me.tx3SERIE.ReadOnly = True
        Me.tx3SERIE.Size = New System.Drawing.Size(56, 20)
        Me.tx3SERIE.TabIndex = 37
        Me.tx3SERIE.Tag = Nothing
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(16, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 16)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "Sèrie"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(16, 68)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 16)
        Me.Label7.TabIndex = 35
        Me.Label7.Text = "Client"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx3CLIENT
        '
        Me.tx3CLIENT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx3CLIENT.Location = New System.Drawing.Point(68, 64)
        Me.tx3CLIENT.Name = "tx3CLIENT"
        Me.tx3CLIENT.ReadOnly = True
        Me.tx3CLIENT.Size = New System.Drawing.Size(40, 20)
        Me.tx3CLIENT.TabIndex = 33
        Me.tx3CLIENT.Tag = Nothing
        '
        'tx3CODI
        '
        Me.tx3CODI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx3CODI.Location = New System.Drawing.Point(68, 16)
        Me.tx3CODI.Name = "tx3CODI"
        Me.tx3CODI.ReadOnly = True
        Me.tx3CODI.Size = New System.Drawing.Size(96, 20)
        Me.tx3CODI.TabIndex = 32
        Me.tx3CODI.Tag = Nothing
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.Label30)
        Me.GroupBox12.Controls.Add(Me.Label31)
        Me.GroupBox12.Controls.Add(Me.tx4VENDA)
        Me.GroupBox12.Controls.Add(Me.tx3COST)
        Me.GroupBox12.Location = New System.Drawing.Point(760, 12)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(200, 96)
        Me.GroupBox12.TabIndex = 302
        Me.GroupBox12.TabStop = False
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label30.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label30.Location = New System.Drawing.Point(12, 24)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(100, 19)
        Me.Label30.TabIndex = 299
        Me.Label30.Text = "Preu Cost"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label31.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label31.Location = New System.Drawing.Point(12, 56)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(100, 19)
        Me.Label31.TabIndex = 298
        Me.Label31.Text = "Preu Venda"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tx4VENDA
        '
        Me.tx4VENDA.BackColor = System.Drawing.Color.Khaki
        Me.tx4VENDA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx4VENDA.Location = New System.Drawing.Point(116, 52)
        Me.tx4VENDA.Name = "tx4VENDA"
        Me.tx4VENDA.Size = New System.Drawing.Size(80, 20)
        Me.tx4VENDA.TabIndex = 297
        Me.tx4VENDA.Tag = Nothing
        '
        'tx3COST
        '
        Me.tx3COST.BackColor = System.Drawing.Color.Khaki
        Me.tx3COST.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx3COST.Location = New System.Drawing.Point(116, 20)
        Me.tx3COST.Name = "tx3COST"
        Me.tx3COST.Size = New System.Drawing.Size(80, 20)
        Me.tx3COST.TabIndex = 296
        Me.tx3COST.Tag = Nothing
        '
        'cboFornituras
        '
        Me.cboFornituras.AllowColMove = False
        Me.cboFornituras.AllowColSelect = True
        Me.cboFornituras.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboFornituras.AlternatingRows = True
        Me.cboFornituras.CaptionHeight = 17
        Me.cboFornituras.ColumnCaptionHeight = 17
        Me.cboFornituras.ColumnFooterHeight = 17
        Me.cboFornituras.ExtendRightColumn = True
        Me.cboFornituras.FetchRowStyles = True
        Me.cboFornituras.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.cboFornituras.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboFornituras.Images.Add(CType(resources.GetObject("resource11"), System.Drawing.Image))
        Me.cboFornituras.Location = New System.Drawing.Point(40, 128)
        Me.cboFornituras.Name = "cboFornituras"
        Me.cboFornituras.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboFornituras.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.cboFornituras.RowHeight = 15
        Me.cboFornituras.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboFornituras.ScrollTips = False
        Me.cboFornituras.Size = New System.Drawing.Size(76, 148)
        Me.cboFornituras.TabIndex = 33
        Me.cboFornituras.Text = "C1TrueDBDropdown1"
        Me.cboFornituras.ValueTranslate = True
        Me.cboFornituras.Visible = False
        Me.cboFornituras.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:HighlightTe" & _
        "xt;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Headin" & _
        "g{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;Al" & _
        "ignVert:Center;}Style8{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style" & _
        "9{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.DropdownView AllowColMove=""False" & _
        """ Name="""" AlternatingRowStyle=""True"" CaptionHeight=""17"" ColumnCaptionHeight=""17""" & _
        " ColumnFooterHeight=""17"" ExtendRightColumn=""True"" FetchRowStyles=""True"" MarqueeS" & _
        "tyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=""17"" RecordSelec" & _
        "tors=""False"" VerticalScrollGroup=""1"" HorizontalScrollGroup=""1""><CaptionStyle par" & _
        "ent=""Style2"" me=""Style10"" /><EditorStyle parent=""Editor"" me=""Style5"" /><EvenRowS" & _
        "tyle parent=""EvenRow"" me=""Style8"" /><FilterBarStyle parent=""FilterBar"" me=""Style" & _
        "13"" /><FooterStyle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""" & _
        "Style12"" /><HeadingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle paren" & _
        "t=""HighlightRow"" me=""Style7"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><O" & _
        "ddRowStyle parent=""OddRow"" me=""Style9"" /><RecordSelectorStyle parent=""RecordSele" & _
        "ctor"" me=""Style11"" /><SelectedStyle parent=""Selected"" me=""Style6"" /><Style paren" & _
        "t=""Normal"" me=""Style1"" /><ClientRect>0, 0, 72, 144</ClientRect><BorderSide>0</Bo" & _
        "rderSide></C1.Win.C1TrueDBGrid.DropdownView></Splits><NamedStyles><Style parent=" & _
        """"" me=""Normal"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" m" & _
        "e=""Footer"" /><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""" & _
        "Inactive"" /><Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Ed" & _
        "itor"" /><Style parent=""Normal"" me=""HighlightRow"" /><Style parent=""Normal"" me=""Ev" & _
        "enRow"" /><Style parent=""Normal"" me=""OddRow"" /><Style parent=""Heading"" me=""Record" & _
        "Selector"" /><Style parent=""Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""" & _
        "Group"" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layo" & _
        "ut>None</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 72," & _
        " 144</ClientArea></Blob>"
        '
        'dgForni
        '
        Me.dgForni.AllowAddNew = True
        Me.dgForni.AllowColSelect = False
        Me.dgForni.AllowDelete = True
        Me.dgForni.AllowSort = False
        Me.dgForni.CaptionHeight = 17
        Me.dgForni.ExtendRightColumn = True
        Me.dgForni.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.dgForni.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.dgForni.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgForni.Images.Add(CType(resources.GetObject("resource12"), System.Drawing.Image))
        Me.dgForni.Location = New System.Drawing.Point(24, 112)
        Me.dgForni.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.dgForni.Name = "dgForni"
        Me.dgForni.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgForni.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgForni.PreviewInfo.ZoomFactor = 75
        Me.dgForni.RecordSelectorWidth = 17
        Me.dgForni.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgForni.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.dgForni.RowHeight = 15
        Me.dgForni.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.dgForni.Size = New System.Drawing.Size(652, 252)
        Me.dgForni.SpringMode = True
        Me.dgForni.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgForni.TabIndex = 32
        Me.dgForni.Text = "C1TrueDBGrid1"
        Me.dgForni.WrapCellPointer = True
        Me.dgForni.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Style9{}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:Hig" & _
        "hlightText;BackColor:Highlight;}Style14{}OddRow{}RecordSelector{AlignImage:Cente" & _
        "r;}Style15{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeCol" & _
        "or:ControlText;BackColor:Control;}Style8{}Style10{AlignHorz:Near;}Style11{}Style" & _
        "12{}Style13{}Style1{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Allo" & _
        "wColSelect=""False"" Name="""" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFoo" & _
        "terHeight=""17"" ExtendRightColumn=""True"" MarqueeStyle=""DottedCellBorder"" RecordSe" & _
        "lectorWidth=""17"" DefRecSelWidth=""17"" VerticalScrollGroup=""1"" HorizontalScrollGro" & _
        "up=""1"" SpringMode=""True""><CaptionStyle parent=""Style2"" me=""Style10"" /><EditorSty" & _
        "le parent=""Editor"" me=""Style5"" /><EvenRowStyle parent=""EvenRow"" me=""Style8"" /><F" & _
        "ilterBarStyle parent=""FilterBar"" me=""Style13"" /><FooterStyle parent=""Footer"" me=" & _
        """Style3"" /><GroupStyle parent=""Group"" me=""Style12"" /><HeadingStyle parent=""Headi" & _
        "ng"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow"" me=""Style7"" /><Inacti" & _
        "veStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle parent=""OddRow"" me=""Style9""" & _
        " /><RecordSelectorStyle parent=""RecordSelector"" me=""Style11"" /><SelectedStyle pa" & _
        "rent=""Selected"" me=""Style6"" /><Style parent=""Normal"" me=""Style1"" /><ClientRect>0" & _
        ", 0, 648, 248</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeV" & _
        "iew></Splits><NamedStyles><Style parent="""" me=""Normal"" /><Style parent=""Normal"" " & _
        "me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /><Style parent=""Heading"" me=" & _
        """Caption"" /><Style parent=""Heading"" me=""Inactive"" /><Style parent=""Normal"" me=""S" & _
        "elected"" /><Style parent=""Normal"" me=""Editor"" /><Style parent=""Normal"" me=""Highl" & _
        "ightRow"" /><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddR" & _
        "ow"" /><Style parent=""Heading"" me=""RecordSelector"" /><Style parent=""Normal"" me=""F" & _
        "ilterBar"" /><Style parent=""Caption"" me=""Group"" /></NamedStyles><vertSplits>1</ve" & _
        "rtSplits><horzSplits>1</horzSplits><Layout>None</Layout><DefaultRecSelWidth>17</" & _
        "DefaultRecSelWidth><ClientArea>0, 0, 648, 248</ClientArea><PrintPageHeaderStyle " & _
        "parent="""" me=""Style14"" /><PrintPageFooterStyle parent="""" me=""Style15"" /></Blob>"
        '
        'tabPageEscandalloPrecio
        '
        Me.tabPageEscandalloPrecio.BackColor = System.Drawing.SystemColors.Control
        Me.tabPageEscandalloPrecio.Controls.Add(Me.GroupBox11)
        Me.tabPageEscandalloPrecio.Controls.Add(Me.cboTejido)
        Me.tabPageEscandalloPrecio.Controls.Add(Me.dgEscandall)
        Me.tabPageEscandalloPrecio.Controls.Add(Me.GroupBox7)
        Me.tabPageEscandalloPrecio.Controls.Add(Me.GroupBox2)
        Me.tabPageEscandalloPrecio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tabPageEscandalloPrecio.Location = New System.Drawing.Point(4, 22)
        Me.tabPageEscandalloPrecio.Name = "tabPageEscandalloPrecio"
        Me.tabPageEscandalloPrecio.Size = New System.Drawing.Size(970, 482)
        Me.tabPageEscandalloPrecio.TabIndex = 2
        Me.tabPageEscandalloPrecio.Text = "Escandall de Preu"
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.Label28)
        Me.GroupBox11.Controls.Add(Me.Label29)
        Me.GroupBox11.Controls.Add(Me.tx5VENDA)
        Me.GroupBox11.Controls.Add(Me.tx4COST)
        Me.GroupBox11.Location = New System.Drawing.Point(760, 12)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(200, 96)
        Me.GroupBox11.TabIndex = 301
        Me.GroupBox11.TabStop = False
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label28.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label28.Location = New System.Drawing.Point(12, 24)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(100, 19)
        Me.Label28.TabIndex = 299
        Me.Label28.Text = "Preu Cost"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label29.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label29.Location = New System.Drawing.Point(12, 56)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(100, 19)
        Me.Label29.TabIndex = 298
        Me.Label29.Text = "Preu Venda"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx5VENDA
        '
        Me.tx5VENDA.BackColor = System.Drawing.Color.Khaki
        Me.tx5VENDA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx5VENDA.Location = New System.Drawing.Point(116, 52)
        Me.tx5VENDA.Name = "tx5VENDA"
        Me.tx5VENDA.Size = New System.Drawing.Size(80, 20)
        Me.tx5VENDA.TabIndex = 297
        Me.tx5VENDA.Tag = Nothing
        '
        'tx4COST
        '
        Me.tx4COST.BackColor = System.Drawing.Color.Khaki
        Me.tx4COST.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx4COST.Location = New System.Drawing.Point(116, 20)
        Me.tx4COST.Name = "tx4COST"
        Me.tx4COST.Size = New System.Drawing.Size(80, 20)
        Me.tx4COST.TabIndex = 296
        Me.tx4COST.Tag = Nothing
        '
        'cboTejido
        '
        Me.cboTejido.AllowColMove = False
        Me.cboTejido.AllowColSelect = True
        Me.cboTejido.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboTejido.AlternatingRows = True
        Me.cboTejido.CaptionHeight = 17
        Me.cboTejido.ColumnCaptionHeight = 17
        Me.cboTejido.ColumnFooterHeight = 17
        Me.cboTejido.ExtendRightColumn = True
        Me.cboTejido.FetchRowStyles = True
        Me.cboTejido.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.cboTejido.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cboTejido.Images.Add(CType(resources.GetObject("resource13"), System.Drawing.Image))
        Me.cboTejido.Location = New System.Drawing.Point(44, 128)
        Me.cboTejido.Name = "cboTejido"
        Me.cboTejido.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboTejido.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.cboTejido.RowHeight = 15
        Me.cboTejido.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboTejido.ScrollTips = False
        Me.cboTejido.Size = New System.Drawing.Size(64, 116)
        Me.cboTejido.TabIndex = 78
        Me.cboTejido.Text = "C1TrueDBDropdown1"
        Me.cboTejido.ValueTranslate = True
        Me.cboTejido.Visible = False
        Me.cboTejido.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:HighlightTe" & _
        "xt;BackColor:Highlight;}Style9{}OddRow{}RecordSelector{AlignImage:Center;}Headin" & _
        "g{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;Bac" & _
        "kColor:Control;}Style8{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style" & _
        "1{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.DropdownView AllowColMove=""False" & _
        """ Name="""" AlternatingRowStyle=""True"" CaptionHeight=""17"" ColumnCaptionHeight=""17""" & _
        " ColumnFooterHeight=""17"" ExtendRightColumn=""True"" FetchRowStyles=""True"" MarqueeS" & _
        "tyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=""17"" RecordSelec" & _
        "tors=""False"" VerticalScrollGroup=""1"" HorizontalScrollGroup=""1""><CaptionStyle par" & _
        "ent=""Style2"" me=""Style10"" /><EditorStyle parent=""Editor"" me=""Style5"" /><EvenRowS" & _
        "tyle parent=""EvenRow"" me=""Style8"" /><FilterBarStyle parent=""FilterBar"" me=""Style" & _
        "13"" /><FooterStyle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""" & _
        "Style12"" /><HeadingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle paren" & _
        "t=""HighlightRow"" me=""Style7"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><O" & _
        "ddRowStyle parent=""OddRow"" me=""Style9"" /><RecordSelectorStyle parent=""RecordSele" & _
        "ctor"" me=""Style11"" /><SelectedStyle parent=""Selected"" me=""Style6"" /><Style paren" & _
        "t=""Normal"" me=""Style1"" /><ClientRect>0, 0, 60, 112</ClientRect><BorderSide>0</Bo" & _
        "rderSide></C1.Win.C1TrueDBGrid.DropdownView></Splits><NamedStyles><Style parent=" & _
        """"" me=""Normal"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" m" & _
        "e=""Footer"" /><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""" & _
        "Inactive"" /><Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Ed" & _
        "itor"" /><Style parent=""Normal"" me=""HighlightRow"" /><Style parent=""Normal"" me=""Ev" & _
        "enRow"" /><Style parent=""Normal"" me=""OddRow"" /><Style parent=""Heading"" me=""Record" & _
        "Selector"" /><Style parent=""Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""" & _
        "Group"" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layo" & _
        "ut>None</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 60," & _
        " 112</ClientArea></Blob>"
        '
        'dgEscandall
        '
        Me.dgEscandall.AllowAddNew = True
        Me.dgEscandall.AllowDelete = True
        Me.dgEscandall.AllowSort = False
        Me.dgEscandall.CaptionHeight = 17
        Me.dgEscandall.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.dgEscandall.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.dgEscandall.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgEscandall.Images.Add(CType(resources.GetObject("resource14"), System.Drawing.Image))
        Me.dgEscandall.Location = New System.Drawing.Point(24, 112)
        Me.dgEscandall.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.dgEscandall.Name = "dgEscandall"
        Me.dgEscandall.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgEscandall.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgEscandall.PreviewInfo.ZoomFactor = 75
        Me.dgEscandall.RecordSelectorWidth = 17
        Me.dgEscandall.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgEscandall.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.dgEscandall.RowHeight = 15
        Me.dgEscandall.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.dgEscandall.Size = New System.Drawing.Size(879, 160)
        Me.dgEscandall.SpringMode = True
        Me.dgEscandall.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgEscandall.TabIndex = 77
        Me.dgEscandall.Text = "C1TrueDBGrid1"
        Me.dgEscandall.WrapCellPointer = True
        Me.dgEscandall.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Style9{}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:Hig" & _
        "hlightText;BackColor:Highlight;}Style14{}OddRow{}RecordSelector{AlignImage:Cente" & _
        "r;}Style15{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeCol" & _
        "or:ControlText;BackColor:Control;}Style8{}Style10{AlignHorz:Near;}Style11{}Style" & _
        "12{}Style13{}Style1{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name" & _
        "="""" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeight=""17"" MarqueeS" & _
        "tyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=""17"" VerticalScr" & _
        "ollGroup=""1"" HorizontalScrollGroup=""1"" SpringMode=""True""><CaptionStyle parent=""S" & _
        "tyle2"" me=""Style10"" /><EditorStyle parent=""Editor"" me=""Style5"" /><EvenRowStyle p" & _
        "arent=""EvenRow"" me=""Style8"" /><FilterBarStyle parent=""FilterBar"" me=""Style13"" />" & _
        "<FooterStyle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style1" & _
        "2"" /><HeadingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""Hig" & _
        "hlightRow"" me=""Style7"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowS" & _
        "tyle parent=""OddRow"" me=""Style9"" /><RecordSelectorStyle parent=""RecordSelector"" " & _
        "me=""Style11"" /><SelectedStyle parent=""Selected"" me=""Style6"" /><Style parent=""Nor" & _
        "mal"" me=""Style1"" /><ClientRect>0, 0, 875, 156</ClientRect><BorderSide>0</BorderS" & _
        "ide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent="""" me=""N" & _
        "ormal"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Foote" & _
        "r"" /><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive" & _
        """ /><Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Editor"" />" & _
        "<Style parent=""Normal"" me=""HighlightRow"" /><Style parent=""Normal"" me=""EvenRow"" /" & _
        "><Style parent=""Normal"" me=""OddRow"" /><Style parent=""Heading"" me=""RecordSelector" & _
        """ /><Style parent=""Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""Group"" /" & _
        "></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>None<" & _
        "/Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 875, 156</C" & _
        "lientArea><PrintPageHeaderStyle parent="""" me=""Style14"" /><PrintPageFooterStyle p" & _
        "arent="""" me=""Style15"" /></Blob>"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.lblPack)
        Me.GroupBox7.Controls.Add(Me.txtNPACK)
        Me.GroupBox7.Controls.Add(Me.lblMargen)
        Me.GroupBox7.Controls.Add(Me.txtMARGE)
        Me.GroupBox7.Controls.Add(Me.Label20)
        Me.GroupBox7.Controls.Add(Me.lblPrecioVenta)
        Me.GroupBox7.Controls.Add(Me.tx2COST)
        Me.GroupBox7.Controls.Add(Me.tx2VENDA)
        Me.GroupBox7.Controls.Add(Me.Label16)
        Me.GroupBox7.Controls.Add(Me.txtNFORNITURA)
        Me.GroupBox7.Controls.Add(Me.Label8)
        Me.GroupBox7.Controls.Add(Me.Label9)
        Me.GroupBox7.Controls.Add(Me.Label10)
        Me.GroupBox7.Controls.Add(Me.txtNCONFEC)
        Me.GroupBox7.Controls.Add(Me.txtNPLANXA)
        Me.GroupBox7.Controls.Add(Me.txtNREPAS)
        Me.GroupBox7.Controls.Add(Me.txtMANIPULACION)
        Me.GroupBox7.Controls.Add(Me.Label13)
        Me.GroupBox7.Controls.Add(Me.dgManipulacion)
        Me.GroupBox7.Location = New System.Drawing.Point(24, 276)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(880, 200)
        Me.GroupBox7.TabIndex = 76
        Me.GroupBox7.TabStop = False
        '
        'lblPack
        '
        Me.lblPack.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblPack.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPack.Location = New System.Drawing.Point(492, 76)
        Me.lblPack.Name = "lblPack"
        Me.lblPack.Size = New System.Drawing.Size(112, 20)
        Me.lblPack.TabIndex = 109
        Me.lblPack.Text = "Pack"
        Me.lblPack.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNPACK
        '
        Me.txtNPACK.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtNPACK.Location = New System.Drawing.Point(604, 76)
        Me.txtNPACK.Name = "txtNPACK"
        Me.txtNPACK.Size = New System.Drawing.Size(72, 20)
        Me.txtNPACK.TabIndex = 14
        Me.txtNPACK.Tag = Nothing
        '
        'lblMargen
        '
        Me.lblMargen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblMargen.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblMargen.Location = New System.Drawing.Point(692, 144)
        Me.lblMargen.Name = "lblMargen"
        Me.lblMargen.Size = New System.Drawing.Size(95, 20)
        Me.lblMargen.TabIndex = 104
        Me.lblMargen.Text = "% de Marge"
        Me.lblMargen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMARGE
        '
        Me.txtMARGE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMARGE.Location = New System.Drawing.Point(792, 144)
        Me.txtMARGE.Name = "txtMARGE"
        Me.txtMARGE.Size = New System.Drawing.Size(72, 20)
        Me.txtMARGE.TabIndex = 17
        Me.txtMARGE.Tag = Nothing
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label20.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label20.Location = New System.Drawing.Point(492, 144)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(108, 20)
        Me.Label20.TabIndex = 101
        Me.Label20.Text = "Cost prenda"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPrecioVenta
        '
        Me.lblPrecioVenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblPrecioVenta.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPrecioVenta.Location = New System.Drawing.Point(692, 168)
        Me.lblPrecioVenta.Name = "lblPrecioVenta"
        Me.lblPrecioVenta.Size = New System.Drawing.Size(95, 20)
        Me.lblPrecioVenta.TabIndex = 102
        Me.lblPrecioVenta.Text = "Preu Venda"
        Me.lblPrecioVenta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx2COST
        '
        Me.tx2COST.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx2COST.Location = New System.Drawing.Point(604, 144)
        Me.tx2COST.Name = "tx2COST"
        Me.tx2COST.Size = New System.Drawing.Size(72, 20)
        Me.tx2COST.TabIndex = 16
        Me.tx2COST.Tag = Nothing
        '
        'tx2VENDA
        '
        Me.tx2VENDA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx2VENDA.Location = New System.Drawing.Point(792, 168)
        Me.tx2VENDA.Name = "tx2VENDA"
        Me.tx2VENDA.Size = New System.Drawing.Size(72, 20)
        Me.tx2VENDA.TabIndex = 18
        Me.tx2VENDA.Tag = Nothing
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label16.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label16.Location = New System.Drawing.Point(492, 116)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(112, 20)
        Me.Label16.TabIndex = 98
        Me.Label16.Text = "Fornitures"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNFORNITURA
        '
        Me.txtNFORNITURA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtNFORNITURA.Location = New System.Drawing.Point(604, 116)
        Me.txtNFORNITURA.Name = "txtNFORNITURA"
        Me.txtNFORNITURA.Size = New System.Drawing.Size(72, 20)
        Me.txtNFORNITURA.TabIndex = 15
        Me.txtNFORNITURA.Tag = Nothing
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(492, 36)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(112, 20)
        Me.Label8.TabIndex = 84
        Me.Label8.Text = "Planxa"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label9.Location = New System.Drawing.Point(492, 56)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(112, 20)
        Me.Label9.TabIndex = 85
        Me.Label9.Text = "Tall"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label10.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label10.Location = New System.Drawing.Point(492, 96)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(112, 20)
        Me.Label10.TabIndex = 86
        Me.Label10.Text = "Total manipulació"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNCONFEC
        '
        Me.txtNCONFEC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtNCONFEC.Location = New System.Drawing.Point(604, 16)
        Me.txtNCONFEC.Name = "txtNCONFEC"
        Me.txtNCONFEC.Size = New System.Drawing.Size(72, 20)
        Me.txtNCONFEC.TabIndex = 2
        Me.txtNCONFEC.Tag = Nothing
        '
        'txtNPLANXA
        '
        Me.txtNPLANXA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtNPLANXA.Location = New System.Drawing.Point(604, 36)
        Me.txtNPLANXA.Name = "txtNPLANXA"
        Me.txtNPLANXA.Size = New System.Drawing.Size(72, 20)
        Me.txtNPLANXA.TabIndex = 3
        Me.txtNPLANXA.Tag = Nothing
        '
        'txtNREPAS
        '
        Me.txtNREPAS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtNREPAS.Location = New System.Drawing.Point(604, 56)
        Me.txtNREPAS.Name = "txtNREPAS"
        Me.txtNREPAS.Size = New System.Drawing.Size(72, 20)
        Me.txtNREPAS.TabIndex = 4
        Me.txtNREPAS.Tag = Nothing
        '
        'txtMANIPULACION
        '
        Me.txtMANIPULACION.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtMANIPULACION.Location = New System.Drawing.Point(604, 96)
        Me.txtMANIPULACION.Name = "txtMANIPULACION"
        Me.txtMANIPULACION.Size = New System.Drawing.Size(72, 20)
        Me.txtMANIPULACION.TabIndex = 13
        Me.txtMANIPULACION.Tag = Nothing
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label13.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label13.Location = New System.Drawing.Point(492, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(112, 20)
        Me.Label13.TabIndex = 83
        Me.Label13.Text = "Confecció"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgManipulacion
        '
        Me.dgManipulacion.AllowAddNew = True
        Me.dgManipulacion.AllowColSelect = False
        Me.dgManipulacion.AllowDelete = True
        Me.dgManipulacion.AllowSort = False
        Me.dgManipulacion.Caption = "MANIPULACIÓ"
        Me.dgManipulacion.CaptionHeight = 17
        Me.dgManipulacion.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.dgManipulacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.dgManipulacion.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgManipulacion.Images.Add(CType(resources.GetObject("resource15"), System.Drawing.Image))
        Me.dgManipulacion.Location = New System.Drawing.Point(8, 12)
        Me.dgManipulacion.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.dgManipulacion.Name = "dgManipulacion"
        Me.dgManipulacion.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgManipulacion.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgManipulacion.PreviewInfo.ZoomFactor = 75
        Me.dgManipulacion.RecordSelectorWidth = 17
        Me.dgManipulacion.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgManipulacion.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.dgManipulacion.RowHeight = 15
        Me.dgManipulacion.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.dgManipulacion.Size = New System.Drawing.Size(476, 176)
        Me.dgManipulacion.SpringMode = True
        Me.dgManipulacion.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgManipulacion.TabIndex = 303
        Me.dgManipulacion.Text = "C1TrueDBGrid1"
        Me.dgManipulacion.WrapCellPointer = True
        Me.dgManipulacion.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Style1{}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:Hig" & _
        "hlightText;BackColor:Highlight;}Style12{}OddRow{}RecordSelector{AlignImage:Cente" & _
        "r;}Style13{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeCo" & _
        "lor:ControlText;AlignVert:Center;}Style8{}Style10{AlignHorz:Near;}Style11{}Style" & _
        "14{}Style15{}Style9{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Allo" & _
        "wColSelect=""False"" Name="""" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFoo" & _
        "terHeight=""17"" MarqueeStyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSe" & _
        "lWidth=""17"" VerticalScrollGroup=""1"" HorizontalScrollGroup=""1"" SpringMode=""True"">" & _
        "<CaptionStyle parent=""Style2"" me=""Style10"" /><EditorStyle parent=""Editor"" me=""St" & _
        "yle5"" /><EvenRowStyle parent=""EvenRow"" me=""Style8"" /><FilterBarStyle parent=""Fil" & _
        "terBar"" me=""Style13"" /><FooterStyle parent=""Footer"" me=""Style3"" /><GroupStyle pa" & _
        "rent=""Group"" me=""Style12"" /><HeadingStyle parent=""Heading"" me=""Style2"" /><HighLi" & _
        "ghtRowStyle parent=""HighlightRow"" me=""Style7"" /><InactiveStyle parent=""Inactive""" & _
        " me=""Style4"" /><OddRowStyle parent=""OddRow"" me=""Style9"" /><RecordSelectorStyle p" & _
        "arent=""RecordSelector"" me=""Style11"" /><SelectedStyle parent=""Selected"" me=""Style" & _
        "6"" /><Style parent=""Normal"" me=""Style1"" /><ClientRect>0, 17, 472, 155</ClientRec" & _
        "t><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyle" & _
        "s><Style parent="""" me=""Normal"" /><Style parent=""Normal"" me=""Heading"" /><Style pa" & _
        "rent=""Heading"" me=""Footer"" /><Style parent=""Heading"" me=""Caption"" /><Style paren" & _
        "t=""Heading"" me=""Inactive"" /><Style parent=""Normal"" me=""Selected"" /><Style parent" & _
        "=""Normal"" me=""Editor"" /><Style parent=""Normal"" me=""HighlightRow"" /><Style parent" & _
        "=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><Style parent=""Hea" & _
        "ding"" me=""RecordSelector"" /><Style parent=""Normal"" me=""FilterBar"" /><Style paren" & _
        "t=""Caption"" me=""Group"" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</" & _
        "horzSplits><Layout>None</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><Clie" & _
        "ntArea>0, 0, 472, 172</ClientArea><PrintPageHeaderStyle parent="""" me=""Style14"" /" & _
        "><PrintPageFooterStyle parent="""" me=""Style15"" /></Blob>"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.tx4NOMCLIENT)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.tx4SERIE)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.tx4CLIENT)
        Me.GroupBox2.Controls.Add(Me.tx4CODI)
        Me.GroupBox2.Location = New System.Drawing.Point(24, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(412, 96)
        Me.GroupBox2.TabIndex = 302
        Me.GroupBox2.TabStop = False
        '
        'tx4NOMCLIENT
        '
        Me.tx4NOMCLIENT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx4NOMCLIENT.Location = New System.Drawing.Point(120, 64)
        Me.tx4NOMCLIENT.Name = "tx4NOMCLIENT"
        Me.tx4NOMCLIENT.ReadOnly = True
        Me.tx4NOMCLIENT.Size = New System.Drawing.Size(276, 20)
        Me.tx4NOMCLIENT.TabIndex = 31
        Me.tx4NOMCLIENT.Tag = Nothing
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(16, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 20)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "Model"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx4SERIE
        '
        Me.tx4SERIE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx4SERIE.Location = New System.Drawing.Point(68, 40)
        Me.tx4SERIE.Name = "tx4SERIE"
        Me.tx4SERIE.ReadOnly = True
        Me.tx4SERIE.Size = New System.Drawing.Size(56, 20)
        Me.tx4SERIE.TabIndex = 37
        Me.tx4SERIE.Tag = Nothing
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(16, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 16)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Sèrie"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(16, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 16)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "Client"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx4CLIENT
        '
        Me.tx4CLIENT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx4CLIENT.Location = New System.Drawing.Point(68, 64)
        Me.tx4CLIENT.Name = "tx4CLIENT"
        Me.tx4CLIENT.ReadOnly = True
        Me.tx4CLIENT.Size = New System.Drawing.Size(40, 20)
        Me.tx4CLIENT.TabIndex = 33
        Me.tx4CLIENT.Tag = Nothing
        '
        'tx4CODI
        '
        Me.tx4CODI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx4CODI.Location = New System.Drawing.Point(68, 16)
        Me.tx4CODI.Name = "tx4CODI"
        Me.tx4CODI.ReadOnly = True
        Me.tx4CODI.Size = New System.Drawing.Size(96, 20)
        Me.tx4CODI.TabIndex = 32
        Me.tx4CODI.Tag = Nothing
        '
        'tabPageColores
        '
        Me.tabPageColores.BackColor = System.Drawing.SystemColors.Control
        Me.tabPageColores.Controls.Add(Me.dgEscandalloResumen)
        Me.tabPageColores.Controls.Add(Me.txtColorQuitar)
        Me.tabPageColores.Controls.Add(Me.btnBorrarColor)
        Me.tabPageColores.Controls.Add(Me.btnAñadirColor)
        Me.tabPageColores.Controls.Add(Me.cboColor)
        Me.tabPageColores.Controls.Add(Me.gbTalles)
        Me.tabPageColores.Controls.Add(Me.GroupBox9)
        Me.tabPageColores.Controls.Add(Me.GroupBox13)
        Me.tabPageColores.Controls.Add(Me.dgColores)
        Me.tabPageColores.Controls.Add(Me.txtColorAñadir)
        Me.tabPageColores.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tabPageColores.Location = New System.Drawing.Point(4, 22)
        Me.tabPageColores.Name = "tabPageColores"
        Me.tabPageColores.Size = New System.Drawing.Size(970, 482)
        Me.tabPageColores.TabIndex = 4
        Me.tabPageColores.Text = "Colors - Talles"
        '
        'dgEscandalloResumen
        '
        Me.dgEscandalloResumen.AllowColMove = False
        Me.dgEscandalloResumen.AllowColSelect = False
        Me.dgEscandalloResumen.AllowFilter = False
        Me.dgEscandalloResumen.AllowRowSelect = False
        Me.dgEscandalloResumen.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgEscandalloResumen.AllowSort = False
        Me.dgEscandalloResumen.AllowUpdate = False
        Me.dgEscandalloResumen.AllowUpdateOnBlur = False
        Me.dgEscandalloResumen.AlternatingRows = True
        Me.dgEscandalloResumen.CaptionHeight = 17
        Me.dgEscandalloResumen.ContextMenu = Me.contextBorrar
        Me.dgEscandalloResumen.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.dgEscandalloResumen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.dgEscandalloResumen.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgEscandalloResumen.Images.Add(CType(resources.GetObject("resource16"), System.Drawing.Image))
        Me.dgEscandalloResumen.Location = New System.Drawing.Point(616, 112)
        Me.dgEscandalloResumen.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.dgEscandalloResumen.Name = "dgEscandalloResumen"
        Me.dgEscandalloResumen.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgEscandalloResumen.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgEscandalloResumen.PreviewInfo.ZoomFactor = 75
        Me.dgEscandalloResumen.RecordSelectorWidth = 17
        Me.dgEscandalloResumen.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgEscandalloResumen.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.dgEscandalloResumen.RowHeight = 15
        Me.dgEscandalloResumen.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.dgEscandalloResumen.Size = New System.Drawing.Size(248, 360)
        Me.dgEscandalloResumen.SpringMode = True
        Me.dgEscandalloResumen.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgEscandalloResumen.TabIndex = 308
        Me.dgEscandalloResumen.Text = "C1TrueDBGrid2"
        Me.dgEscandalloResumen.WrapCellPointer = True
        Me.dgEscandalloResumen.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Style1{}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:Hig" & _
        "hlightText;BackColor:Highlight;}Style12{}OddRow{}RecordSelector{AlignImage:Cente" & _
        "r;}Style13{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeCo" & _
        "lor:ControlText;AlignVert:Center;}Style8{}Style10{AlignHorz:Near;}Style11{}Style" & _
        "14{}Style15{}Style9{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Allo" & _
        "wColMove=""False"" AllowColSelect=""False"" AllowRowSelect=""False"" Name="""" AllowRowS" & _
        "izing=""None"" AlternatingRowStyle=""True"" CaptionHeight=""17"" ColumnCaptionHeight=""" & _
        "17"" ColumnFooterHeight=""17"" MarqueeStyle=""DottedCellBorder"" RecordSelectorWidth=" & _
        """17"" DefRecSelWidth=""17"" VerticalScrollGroup=""1"" HorizontalScrollGroup=""1"" Sprin" & _
        "gMode=""True""><CaptionStyle parent=""Style2"" me=""Style10"" /><EditorStyle parent=""E" & _
        "ditor"" me=""Style5"" /><EvenRowStyle parent=""EvenRow"" me=""Style8"" /><FilterBarStyl" & _
        "e parent=""FilterBar"" me=""Style13"" /><FooterStyle parent=""Footer"" me=""Style3"" /><" & _
        "GroupStyle parent=""Group"" me=""Style12"" /><HeadingStyle parent=""Heading"" me=""Styl" & _
        "e2"" /><HighLightRowStyle parent=""HighlightRow"" me=""Style7"" /><InactiveStyle pare" & _
        "nt=""Inactive"" me=""Style4"" /><OddRowStyle parent=""OddRow"" me=""Style9"" /><RecordSe" & _
        "lectorStyle parent=""RecordSelector"" me=""Style11"" /><SelectedStyle parent=""Select" & _
        "ed"" me=""Style6"" /><Style parent=""Normal"" me=""Style1"" /><ClientRect>0, 0, 244, 35" & _
        "6</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits" & _
        "><NamedStyles><Style parent="""" me=""Normal"" /><Style parent=""Normal"" me=""Heading""" & _
        " /><Style parent=""Heading"" me=""Footer"" /><Style parent=""Heading"" me=""Caption"" />" & _
        "<Style parent=""Heading"" me=""Inactive"" /><Style parent=""Normal"" me=""Selected"" /><" & _
        "Style parent=""Normal"" me=""Editor"" /><Style parent=""Normal"" me=""HighlightRow"" /><" & _
        "Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><Style" & _
        " parent=""Heading"" me=""RecordSelector"" /><Style parent=""Normal"" me=""FilterBar"" />" & _
        "<Style parent=""Caption"" me=""Group"" /></NamedStyles><vertSplits>1</vertSplits><ho" & _
        "rzSplits>1</horzSplits><Layout>None</Layout><DefaultRecSelWidth>17</DefaultRecSe" & _
        "lWidth><ClientArea>0, 0, 244, 356</ClientArea><PrintPageHeaderStyle parent="""" me" & _
        "=""Style14"" /><PrintPageFooterStyle parent="""" me=""Style15"" /></Blob>"
        '
        'contextBorrar
        '
        Me.contextBorrar.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        Me.contextBorrar.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Text = "Esborrar linies seleccionades"
        '
        'txtColorQuitar
        '
        Me.txtColorQuitar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtColorQuitar.Location = New System.Drawing.Point(888, 192)
        Me.txtColorQuitar.MaxLength = 10
        Me.txtColorQuitar.Name = "txtColorQuitar"
        Me.txtColorQuitar.Size = New System.Drawing.Size(76, 20)
        Me.txtColorQuitar.TabIndex = 307
        Me.txtColorQuitar.Tag = Nothing
        '
        'btnBorrarColor
        '
        Me.btnBorrarColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBorrarColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnBorrarColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBorrarColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnBorrarColor.Location = New System.Drawing.Point(888, 212)
        Me.btnBorrarColor.Name = "btnBorrarColor"
        Me.btnBorrarColor.Size = New System.Drawing.Size(76, 44)
        Me.btnBorrarColor.TabIndex = 306
        Me.btnBorrarColor.Text = "Treure un Color"
        '
        'btnAñadirColor
        '
        Me.btnAñadirColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAñadirColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnAñadirColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAñadirColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAñadirColor.Location = New System.Drawing.Point(888, 140)
        Me.btnAñadirColor.Name = "btnAñadirColor"
        Me.btnAñadirColor.Size = New System.Drawing.Size(76, 44)
        Me.btnAñadirColor.TabIndex = 305
        Me.btnAñadirColor.Text = "Afegir Color"
        '
        'cboColor
        '
        Me.cboColor.AllowColMove = True
        Me.cboColor.AllowColSelect = True
        Me.cboColor.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
        Me.cboColor.AlternatingRows = True
        Me.cboColor.CaptionHeight = 17
        Me.cboColor.ColumnCaptionHeight = 17
        Me.cboColor.ColumnFooterHeight = 17
        Me.cboColor.FetchRowStyles = False
        Me.cboColor.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.cboColor.Images.Add(CType(resources.GetObject("resource17"), System.Drawing.Image))
        Me.cboColor.Location = New System.Drawing.Point(44, 128)
        Me.cboColor.Name = "cboColor"
        Me.cboColor.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.cboColor.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.cboColor.RowHeight = 15
        Me.cboColor.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboColor.ScrollTips = False
        Me.cboColor.Size = New System.Drawing.Size(60, 188)
        Me.cboColor.TabIndex = 304
        Me.cboColor.TabStop = False
        Me.cboColor.Text = "C1TrueDBDropdown1"
        Me.cboColor.ValueTranslate = True
        Me.cboColor.Visible = False
        Me.cboColor.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:HighlightTe" & _
        "xt;BackColor:Highlight;}Style9{}OddRow{}RecordSelector{AlignImage:Center;}Headin" & _
        "g{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;Bac" & _
        "kColor:Control;}Style8{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style" & _
        "1{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.DropdownView Name="""" Alternating" & _
        "RowStyle=""True"" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeight=""" & _
        "17"" MarqueeStyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=""17""" & _
        " RecordSelectors=""False"" VerticalScrollGroup=""1"" HorizontalScrollGroup=""1""><Capt" & _
        "ionStyle parent=""Style2"" me=""Style10"" /><EditorStyle parent=""Editor"" me=""Style5""" & _
        " /><EvenRowStyle parent=""EvenRow"" me=""Style8"" /><FilterBarStyle parent=""FilterBa" & _
        "r"" me=""Style13"" /><FooterStyle parent=""Footer"" me=""Style3"" /><GroupStyle parent=" & _
        """Group"" me=""Style12"" /><HeadingStyle parent=""Heading"" me=""Style2"" /><HighLightRo" & _
        "wStyle parent=""HighlightRow"" me=""Style7"" /><InactiveStyle parent=""Inactive"" me=""" & _
        "Style4"" /><OddRowStyle parent=""OddRow"" me=""Style9"" /><RecordSelectorStyle parent" & _
        "=""RecordSelector"" me=""Style11"" /><SelectedStyle parent=""Selected"" me=""Style6"" />" & _
        "<Style parent=""Normal"" me=""Style1"" /><ClientRect>0, 0, 56, 184</ClientRect><Bord" & _
        "erSide>0</BorderSide></C1.Win.C1TrueDBGrid.DropdownView></Splits><NamedStyles><S" & _
        "tyle parent="""" me=""Normal"" /><Style parent=""Normal"" me=""Heading"" /><Style parent" & _
        "=""Heading"" me=""Footer"" /><Style parent=""Heading"" me=""Caption"" /><Style parent=""H" & _
        "eading"" me=""Inactive"" /><Style parent=""Normal"" me=""Selected"" /><Style parent=""No" & _
        "rmal"" me=""Editor"" /><Style parent=""Normal"" me=""HighlightRow"" /><Style parent=""No" & _
        "rmal"" me=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><Style parent=""Heading" & _
        """ me=""RecordSelector"" /><Style parent=""Normal"" me=""FilterBar"" /><Style parent=""C" & _
        "aption"" me=""Group"" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horz" & _
        "Splits><Layout>None</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientAr" & _
        "ea>0, 0, 56, 184</ClientArea></Blob>"
        '
        'gbTalles
        '
        Me.gbTalles.Controls.Add(Me.txtTALLA10)
        Me.gbTalles.Controls.Add(Me.txtTALLA09)
        Me.gbTalles.Controls.Add(Me.txtTALLA08)
        Me.gbTalles.Controls.Add(Me.txtTALLA07)
        Me.gbTalles.Controls.Add(Me.txtTALLA06)
        Me.gbTalles.Controls.Add(Me.txtTALLA05)
        Me.gbTalles.Controls.Add(Me.txtTALLA04)
        Me.gbTalles.Controls.Add(Me.txtTALLA03)
        Me.gbTalles.Controls.Add(Me.txtTALLA02)
        Me.gbTalles.Controls.Add(Me.txtTALLA01)
        Me.gbTalles.Location = New System.Drawing.Point(440, 12)
        Me.gbTalles.Name = "gbTalles"
        Me.gbTalles.Size = New System.Drawing.Size(260, 96)
        Me.gbTalles.TabIndex = 78
        Me.gbTalles.TabStop = False
        Me.gbTalles.Text = "Talles"
        '
        'txtTALLA10
        '
        Me.txtTALLA10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTALLA10.Location = New System.Drawing.Point(208, 60)
        Me.txtTALLA10.MaxLength = 3
        Me.txtTALLA10.Name = "txtTALLA10"
        Me.txtTALLA10.Size = New System.Drawing.Size(40, 20)
        Me.txtTALLA10.TabIndex = 9
        Me.txtTALLA10.Tag = Nothing
        '
        'txtTALLA09
        '
        Me.txtTALLA09.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTALLA09.Location = New System.Drawing.Point(160, 60)
        Me.txtTALLA09.MaxLength = 3
        Me.txtTALLA09.Name = "txtTALLA09"
        Me.txtTALLA09.Size = New System.Drawing.Size(40, 20)
        Me.txtTALLA09.TabIndex = 8
        Me.txtTALLA09.Tag = Nothing
        '
        'txtTALLA08
        '
        Me.txtTALLA08.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTALLA08.Location = New System.Drawing.Point(112, 60)
        Me.txtTALLA08.MaxLength = 3
        Me.txtTALLA08.Name = "txtTALLA08"
        Me.txtTALLA08.Size = New System.Drawing.Size(40, 20)
        Me.txtTALLA08.TabIndex = 7
        Me.txtTALLA08.Tag = Nothing
        '
        'txtTALLA07
        '
        Me.txtTALLA07.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTALLA07.Location = New System.Drawing.Point(64, 60)
        Me.txtTALLA07.MaxLength = 3
        Me.txtTALLA07.Name = "txtTALLA07"
        Me.txtTALLA07.Size = New System.Drawing.Size(40, 20)
        Me.txtTALLA07.TabIndex = 6
        Me.txtTALLA07.Tag = Nothing
        '
        'txtTALLA06
        '
        Me.txtTALLA06.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTALLA06.Location = New System.Drawing.Point(16, 60)
        Me.txtTALLA06.MaxLength = 3
        Me.txtTALLA06.Name = "txtTALLA06"
        Me.txtTALLA06.Size = New System.Drawing.Size(40, 20)
        Me.txtTALLA06.TabIndex = 5
        Me.txtTALLA06.Tag = Nothing
        '
        'txtTALLA05
        '
        Me.txtTALLA05.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTALLA05.Location = New System.Drawing.Point(208, 32)
        Me.txtTALLA05.MaxLength = 3
        Me.txtTALLA05.Name = "txtTALLA05"
        Me.txtTALLA05.Size = New System.Drawing.Size(40, 20)
        Me.txtTALLA05.TabIndex = 4
        Me.txtTALLA05.Tag = Nothing
        '
        'txtTALLA04
        '
        Me.txtTALLA04.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTALLA04.Location = New System.Drawing.Point(160, 32)
        Me.txtTALLA04.MaxLength = 3
        Me.txtTALLA04.Name = "txtTALLA04"
        Me.txtTALLA04.Size = New System.Drawing.Size(40, 20)
        Me.txtTALLA04.TabIndex = 3
        Me.txtTALLA04.Tag = Nothing
        '
        'txtTALLA03
        '
        Me.txtTALLA03.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTALLA03.Location = New System.Drawing.Point(112, 32)
        Me.txtTALLA03.MaxLength = 3
        Me.txtTALLA03.Name = "txtTALLA03"
        Me.txtTALLA03.Size = New System.Drawing.Size(40, 20)
        Me.txtTALLA03.TabIndex = 2
        Me.txtTALLA03.Tag = Nothing
        '
        'txtTALLA02
        '
        Me.txtTALLA02.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTALLA02.Location = New System.Drawing.Point(64, 32)
        Me.txtTALLA02.MaxLength = 3
        Me.txtTALLA02.Name = "txtTALLA02"
        Me.txtTALLA02.Size = New System.Drawing.Size(40, 20)
        Me.txtTALLA02.TabIndex = 1
        Me.txtTALLA02.Tag = Nothing
        '
        'txtTALLA01
        '
        Me.txtTALLA01.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtTALLA01.Location = New System.Drawing.Point(16, 32)
        Me.txtTALLA01.MaxLength = 3
        Me.txtTALLA01.Name = "txtTALLA01"
        Me.txtTALLA01.Size = New System.Drawing.Size(40, 20)
        Me.txtTALLA01.TabIndex = 0
        Me.txtTALLA01.Tag = Nothing
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.tx2NOMCLIENT)
        Me.GroupBox9.Controls.Add(Me.Label11)
        Me.GroupBox9.Controls.Add(Me.tx2SERIE)
        Me.GroupBox9.Controls.Add(Me.Label12)
        Me.GroupBox9.Controls.Add(Me.Label21)
        Me.GroupBox9.Controls.Add(Me.tx2CLIENT)
        Me.GroupBox9.Controls.Add(Me.tx2CODI)
        Me.GroupBox9.Location = New System.Drawing.Point(24, 12)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(412, 96)
        Me.GroupBox9.TabIndex = 303
        Me.GroupBox9.TabStop = False
        '
        'tx2NOMCLIENT
        '
        Me.tx2NOMCLIENT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx2NOMCLIENT.Location = New System.Drawing.Point(120, 64)
        Me.tx2NOMCLIENT.Name = "tx2NOMCLIENT"
        Me.tx2NOMCLIENT.ReadOnly = True
        Me.tx2NOMCLIENT.Size = New System.Drawing.Size(276, 20)
        Me.tx2NOMCLIENT.TabIndex = 31
        Me.tx2NOMCLIENT.Tag = Nothing
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label11.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label11.Location = New System.Drawing.Point(16, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(52, 20)
        Me.Label11.TabIndex = 34
        Me.Label11.Text = "Model"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx2SERIE
        '
        Me.tx2SERIE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx2SERIE.Location = New System.Drawing.Point(68, 40)
        Me.tx2SERIE.Name = "tx2SERIE"
        Me.tx2SERIE.ReadOnly = True
        Me.tx2SERIE.Size = New System.Drawing.Size(56, 20)
        Me.tx2SERIE.TabIndex = 37
        Me.tx2SERIE.Tag = Nothing
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label12.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label12.Location = New System.Drawing.Point(16, 44)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(52, 16)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "Sèrie"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label21.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label21.Location = New System.Drawing.Point(16, 68)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(48, 16)
        Me.Label21.TabIndex = 35
        Me.Label21.Text = "Client"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx2CLIENT
        '
        Me.tx2CLIENT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx2CLIENT.Location = New System.Drawing.Point(68, 64)
        Me.tx2CLIENT.Name = "tx2CLIENT"
        Me.tx2CLIENT.ReadOnly = True
        Me.tx2CLIENT.Size = New System.Drawing.Size(40, 20)
        Me.tx2CLIENT.TabIndex = 33
        Me.tx2CLIENT.Tag = Nothing
        '
        'tx2CODI
        '
        Me.tx2CODI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx2CODI.Location = New System.Drawing.Point(68, 16)
        Me.tx2CODI.Name = "tx2CODI"
        Me.tx2CODI.ReadOnly = True
        Me.tx2CODI.Size = New System.Drawing.Size(96, 20)
        Me.tx2CODI.TabIndex = 32
        Me.tx2CODI.Tag = Nothing
        '
        'GroupBox13
        '
        Me.GroupBox13.Controls.Add(Me.Label32)
        Me.GroupBox13.Controls.Add(Me.Label33)
        Me.GroupBox13.Controls.Add(Me.tx7VENDA)
        Me.GroupBox13.Controls.Add(Me.tx6COST)
        Me.GroupBox13.Location = New System.Drawing.Point(760, 12)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(200, 96)
        Me.GroupBox13.TabIndex = 302
        Me.GroupBox13.TabStop = False
        '
        'Label32
        '
        Me.Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label32.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label32.Location = New System.Drawing.Point(12, 24)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(100, 19)
        Me.Label32.TabIndex = 299
        Me.Label32.Text = "Preu Cost"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label33.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label33.Location = New System.Drawing.Point(12, 56)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(100, 19)
        Me.Label33.TabIndex = 298
        Me.Label33.Text = "Preu Venda"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx7VENDA
        '
        Me.tx7VENDA.BackColor = System.Drawing.Color.Khaki
        Me.tx7VENDA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx7VENDA.Location = New System.Drawing.Point(116, 52)
        Me.tx7VENDA.Name = "tx7VENDA"
        Me.tx7VENDA.Size = New System.Drawing.Size(80, 20)
        Me.tx7VENDA.TabIndex = 297
        Me.tx7VENDA.Tag = Nothing
        '
        'tx6COST
        '
        Me.tx6COST.BackColor = System.Drawing.Color.Khaki
        Me.tx6COST.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx6COST.Location = New System.Drawing.Point(116, 20)
        Me.tx6COST.Name = "tx6COST"
        Me.tx6COST.Size = New System.Drawing.Size(80, 20)
        Me.tx6COST.TabIndex = 296
        Me.tx6COST.Tag = Nothing
        '
        'dgColores
        '
        Me.dgColores.AllowColMove = False
        Me.dgColores.AllowColSelect = False
        Me.dgColores.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgColores.AllowSort = False
        Me.dgColores.AllowUpdateOnBlur = False
        Me.dgColores.AlternatingRows = True
        Me.dgColores.CaptionHeight = 17
        Me.dgColores.ContextMenu = Me.contextBorrar
        Me.dgColores.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.dgColores.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.dgColores.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgColores.Images.Add(CType(resources.GetObject("resource18"), System.Drawing.Image))
        Me.dgColores.Location = New System.Drawing.Point(24, 112)
        Me.dgColores.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.dgColores.Name = "dgColores"
        Me.dgColores.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgColores.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgColores.PreviewInfo.ZoomFactor = 75
        Me.dgColores.RecordSelectorWidth = 17
        Me.dgColores.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgColores.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.dgColores.RowHeight = 15
        Me.dgColores.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.dgColores.Size = New System.Drawing.Size(584, 360)
        Me.dgColores.SpringMode = True
        Me.dgColores.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgColores.TabIndex = 79
        Me.dgColores.Text = "C1TrueDBGrid2"
        Me.dgColores.WrapCellPointer = True
        Me.dgColores.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Style9{}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:Hig" & _
        "hlightText;BackColor:Highlight;}Style14{}OddRow{}RecordSelector{AlignImage:Cente" & _
        "r;}Style15{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeCol" & _
        "or:ControlText;BackColor:Control;}Style8{}Style10{AlignHorz:Near;}Style11{}Style" & _
        "12{}Style13{}Style1{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Allo" & _
        "wColMove=""False"" AllowColSelect=""False"" Name="""" AllowRowSizing=""None"" Alternatin" & _
        "gRowStyle=""True"" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeight=" & _
        """17"" MarqueeStyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=""17" & _
        """ VerticalScrollGroup=""1"" HorizontalScrollGroup=""1"" SpringMode=""True""><CaptionSt" & _
        "yle parent=""Style2"" me=""Style10"" /><EditorStyle parent=""Editor"" me=""Style5"" /><E" & _
        "venRowStyle parent=""EvenRow"" me=""Style8"" /><FilterBarStyle parent=""FilterBar"" me" & _
        "=""Style13"" /><FooterStyle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Grou" & _
        "p"" me=""Style12"" /><HeadingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyl" & _
        "e parent=""HighlightRow"" me=""Style7"" /><InactiveStyle parent=""Inactive"" me=""Style" & _
        "4"" /><OddRowStyle parent=""OddRow"" me=""Style9"" /><RecordSelectorStyle parent=""Rec" & _
        "ordSelector"" me=""Style11"" /><SelectedStyle parent=""Selected"" me=""Style6"" /><Styl" & _
        "e parent=""Normal"" me=""Style1"" /><ClientRect>0, 0, 580, 356</ClientRect><BorderSi" & _
        "de>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style pa" & _
        "rent="""" me=""Normal"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Headi" & _
        "ng"" me=""Footer"" /><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading""" & _
        " me=""Inactive"" /><Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" m" & _
        "e=""Editor"" /><Style parent=""Normal"" me=""HighlightRow"" /><Style parent=""Normal"" m" & _
        "e=""EvenRow"" /><Style parent=""Normal"" me=""OddRow"" /><Style parent=""Heading"" me=""R" & _
        "ecordSelector"" /><Style parent=""Normal"" me=""FilterBar"" /><Style parent=""Caption""" & _
        " me=""Group"" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits>" & _
        "<Layout>None</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0" & _
        ", 580, 356</ClientArea><PrintPageHeaderStyle parent="""" me=""Style14"" /><PrintPage" & _
        "FooterStyle parent="""" me=""Style15"" /></Blob>"
        '
        'txtColorAñadir
        '
        Me.txtColorAñadir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtColorAñadir.Location = New System.Drawing.Point(888, 120)
        Me.txtColorAñadir.MaxLength = 10
        Me.txtColorAñadir.Name = "txtColorAñadir"
        Me.txtColorAñadir.Size = New System.Drawing.Size(76, 20)
        Me.txtColorAñadir.TabIndex = 10
        Me.txtColorAñadir.Tag = Nothing
        '
        'tabPageStock
        '
        Me.tabPageStock.BackColor = System.Drawing.SystemColors.Control
        Me.tabPageStock.Controls.Add(Me.GroupBox8)
        Me.tabPageStock.Controls.Add(Me.gbsPrecio)
        Me.tabPageStock.Controls.Add(Me.dgStock)
        Me.tabPageStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tabPageStock.Location = New System.Drawing.Point(4, 22)
        Me.tabPageStock.Name = "tabPageStock"
        Me.tabPageStock.Size = New System.Drawing.Size(970, 482)
        Me.tabPageStock.TabIndex = 5
        Me.tabPageStock.Text = "Stock"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.tx5NOMCLIENT)
        Me.GroupBox8.Controls.Add(Me.Label22)
        Me.GroupBox8.Controls.Add(Me.tx5SERIE)
        Me.GroupBox8.Controls.Add(Me.Label24)
        Me.GroupBox8.Controls.Add(Me.Label25)
        Me.GroupBox8.Controls.Add(Me.tx5CLIENT)
        Me.GroupBox8.Controls.Add(Me.tx5CODI)
        Me.GroupBox8.Location = New System.Drawing.Point(24, 12)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(412, 96)
        Me.GroupBox8.TabIndex = 77
        Me.GroupBox8.TabStop = False
        '
        'tx5NOMCLIENT
        '
        Me.tx5NOMCLIENT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx5NOMCLIENT.Location = New System.Drawing.Point(120, 64)
        Me.tx5NOMCLIENT.Name = "tx5NOMCLIENT"
        Me.tx5NOMCLIENT.ReadOnly = True
        Me.tx5NOMCLIENT.Size = New System.Drawing.Size(276, 20)
        Me.tx5NOMCLIENT.TabIndex = 31
        Me.tx5NOMCLIENT.Tag = Nothing
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label22.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label22.Location = New System.Drawing.Point(16, 16)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(52, 20)
        Me.Label22.TabIndex = 34
        Me.Label22.Text = "Model"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx5SERIE
        '
        Me.tx5SERIE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx5SERIE.Location = New System.Drawing.Point(68, 40)
        Me.tx5SERIE.Name = "tx5SERIE"
        Me.tx5SERIE.ReadOnly = True
        Me.tx5SERIE.Size = New System.Drawing.Size(56, 20)
        Me.tx5SERIE.TabIndex = 37
        Me.tx5SERIE.Tag = Nothing
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label24.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label24.Location = New System.Drawing.Point(16, 44)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(52, 16)
        Me.Label24.TabIndex = 36
        Me.Label24.Text = "Sèrie"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label25.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label25.Location = New System.Drawing.Point(16, 68)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(48, 16)
        Me.Label25.TabIndex = 35
        Me.Label25.Text = "Client"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx5CLIENT
        '
        Me.tx5CLIENT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx5CLIENT.Location = New System.Drawing.Point(68, 64)
        Me.tx5CLIENT.Name = "tx5CLIENT"
        Me.tx5CLIENT.ReadOnly = True
        Me.tx5CLIENT.Size = New System.Drawing.Size(40, 20)
        Me.tx5CLIENT.TabIndex = 33
        Me.tx5CLIENT.Tag = Nothing
        '
        'tx5CODI
        '
        Me.tx5CODI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx5CODI.Location = New System.Drawing.Point(68, 16)
        Me.tx5CODI.Name = "tx5CODI"
        Me.tx5CODI.ReadOnly = True
        Me.tx5CODI.Size = New System.Drawing.Size(96, 20)
        Me.tx5CODI.TabIndex = 32
        Me.tx5CODI.Tag = Nothing
        '
        'gbsPrecio
        '
        Me.gbsPrecio.Controls.Add(Me.Label26)
        Me.gbsPrecio.Controls.Add(Me.Label27)
        Me.gbsPrecio.Controls.Add(Me.tx6VENDA)
        Me.gbsPrecio.Controls.Add(Me.tx5COST)
        Me.gbsPrecio.Location = New System.Drawing.Point(760, 12)
        Me.gbsPrecio.Name = "gbsPrecio"
        Me.gbsPrecio.Size = New System.Drawing.Size(200, 96)
        Me.gbsPrecio.TabIndex = 301
        Me.gbsPrecio.TabStop = False
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label26.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label26.Location = New System.Drawing.Point(12, 24)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(100, 19)
        Me.Label26.TabIndex = 299
        Me.Label26.Text = "Preu Cost"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label27.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label27.Location = New System.Drawing.Point(12, 56)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(100, 19)
        Me.Label27.TabIndex = 298
        Me.Label27.Text = "Preu Venda"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tx6VENDA
        '
        Me.tx6VENDA.BackColor = System.Drawing.Color.Khaki
        Me.tx6VENDA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx6VENDA.Location = New System.Drawing.Point(116, 52)
        Me.tx6VENDA.Name = "tx6VENDA"
        Me.tx6VENDA.Size = New System.Drawing.Size(80, 20)
        Me.tx6VENDA.TabIndex = 297
        Me.tx6VENDA.Tag = Nothing
        '
        'tx5COST
        '
        Me.tx5COST.BackColor = System.Drawing.Color.Khaki
        Me.tx5COST.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx5COST.Location = New System.Drawing.Point(116, 20)
        Me.tx5COST.Name = "tx5COST"
        Me.tx5COST.Size = New System.Drawing.Size(80, 20)
        Me.tx5COST.TabIndex = 296
        Me.tx5COST.Tag = Nothing
        '
        'dgStock
        '
        Me.dgStock.AllowAddNew = True
        Me.dgStock.AllowDelete = True
        Me.dgStock.CaptionHeight = 17
        Me.dgStock.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.dgStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.dgStock.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgStock.Images.Add(CType(resources.GetObject("resource19"), System.Drawing.Image))
        Me.dgStock.Location = New System.Drawing.Point(24, 112)
        Me.dgStock.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.dgStock.Name = "dgStock"
        Me.dgStock.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgStock.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgStock.PreviewInfo.ZoomFactor = 75
        Me.dgStock.RecordSelectorWidth = 17
        Me.dgStock.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgStock.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.dgStock.RowHeight = 15
        Me.dgStock.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.dgStock.Size = New System.Drawing.Size(892, 360)
        Me.dgStock.SpringMode = True
        Me.dgStock.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgStock.TabIndex = 78
        Me.dgStock.Text = "C1TrueDBGrid1"
        Me.dgStock.WrapCellPointer = True
        Me.dgStock.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Style1{}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:Hig" & _
        "hlightText;BackColor:Highlight;}Style14{}OddRow{}RecordSelector{AlignImage:Cente" & _
        "r;}Style15{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeCo" & _
        "lor:ControlText;AlignVert:Center;}Style8{}Style10{AlignHorz:Near;}Style11{}Style" & _
        "12{}Style13{}Style9{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name" & _
        "="""" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeight=""17"" MarqueeS" & _
        "tyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=""17"" VerticalScr" & _
        "ollGroup=""1"" HorizontalScrollGroup=""1"" SpringMode=""True""><CaptionStyle parent=""S" & _
        "tyle2"" me=""Style10"" /><EditorStyle parent=""Editor"" me=""Style5"" /><EvenRowStyle p" & _
        "arent=""EvenRow"" me=""Style8"" /><FilterBarStyle parent=""FilterBar"" me=""Style13"" />" & _
        "<FooterStyle parent=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style1" & _
        "2"" /><HeadingStyle parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""Hig" & _
        "hlightRow"" me=""Style7"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowS" & _
        "tyle parent=""OddRow"" me=""Style9"" /><RecordSelectorStyle parent=""RecordSelector"" " & _
        "me=""Style11"" /><SelectedStyle parent=""Selected"" me=""Style6"" /><Style parent=""Nor" & _
        "mal"" me=""Style1"" /><ClientRect>0, 0, 888, 356</ClientRect><BorderSide>0</BorderS" & _
        "ide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent="""" me=""N" & _
        "ormal"" /><Style parent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Foote" & _
        "r"" /><Style parent=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive" & _
        """ /><Style parent=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Editor"" />" & _
        "<Style parent=""Normal"" me=""HighlightRow"" /><Style parent=""Normal"" me=""EvenRow"" /" & _
        "><Style parent=""Normal"" me=""OddRow"" /><Style parent=""Heading"" me=""RecordSelector" & _
        """ /><Style parent=""Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""Group"" /" & _
        "></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>None<" & _
        "/Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 888, 356</C" & _
        "lientArea><PrintPageHeaderStyle parent="""" me=""Style14"" /><PrintPageFooterStyle p" & _
        "arent="""" me=""Style15"" /></Blob>"
        '
        'tabPageFichaModelo
        '
        Me.tabPageFichaModelo.BackColor = System.Drawing.SystemColors.Control
        Me.tabPageFichaModelo.Controls.Add(Me.Panel1)
        Me.tabPageFichaModelo.Controls.Add(Me.datagrid3)
        Me.tabPageFichaModelo.Controls.Add(Me.dgColorFicha)
        Me.tabPageFichaModelo.Controls.Add(Me.dgTejidoFicha)
        Me.tabPageFichaModelo.Controls.Add(Me.lblNumeroMuestras)
        Me.tabPageFichaModelo.Controls.Add(Me.Label23)
        Me.tabPageFichaModelo.Controls.Add(Me.tx3VENDA)
        Me.tabPageFichaModelo.Controls.Add(Me.txtNumeroMuestras)
        Me.tabPageFichaModelo.Controls.Add(Me.GroupBox5)
        Me.tabPageFichaModelo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tabPageFichaModelo.Location = New System.Drawing.Point(4, 22)
        Me.tabPageFichaModelo.Name = "tabPageFichaModelo"
        Me.tabPageFichaModelo.Size = New System.Drawing.Size(970, 482)
        Me.tabPageFichaModelo.TabIndex = 3
        Me.tabPageFichaModelo.Text = "Fitxa Model"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.btnBorrarImagen2)
        Me.Panel1.Controls.Add(Me.btnBorrarImagen1)
        Me.Panel1.Controls.Add(Me.btnCanviarImagen2)
        Me.Panel1.Controls.Add(Me.btnCanviarImagen1)
        Me.Panel1.Location = New System.Drawing.Point(576, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(368, 476)
        Me.Panel1.TabIndex = 309
        '
        'Panel4
        '
        Me.Panel4.AutoScroll = True
        Me.Panel4.Controls.Add(Me.imgModelo2)
        Me.Panel4.Location = New System.Drawing.Point(8, 232)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(344, 208)
        Me.Panel4.TabIndex = 313
        '
        'imgModelo2
        '
        Me.imgModelo2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.imgModelo2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.imgModelo2.Location = New System.Drawing.Point(8, 8)
        Me.imgModelo2.Name = "imgModelo2"
        Me.imgModelo2.Size = New System.Drawing.Size(1024, 768)
        Me.imgModelo2.TabIndex = 0
        Me.imgModelo2.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.AutoScroll = True
        Me.Panel3.Controls.Add(Me.imgModelo1)
        Me.Panel3.Location = New System.Drawing.Point(8, 8)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(344, 192)
        Me.Panel3.TabIndex = 312
        '
        'imgModelo1
        '
        Me.imgModelo1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.imgModelo1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.imgModelo1.Location = New System.Drawing.Point(8, 8)
        Me.imgModelo1.Name = "imgModelo1"
        Me.imgModelo1.Size = New System.Drawing.Size(1024, 768)
        Me.imgModelo1.TabIndex = 307
        Me.imgModelo1.TabStop = False
        '
        'btnBorrarImagen2
        '
        Me.btnBorrarImagen2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBorrarImagen2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnBorrarImagen2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBorrarImagen2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnBorrarImagen2.Location = New System.Drawing.Point(306, 444)
        Me.btnBorrarImagen2.Name = "btnBorrarImagen2"
        Me.btnBorrarImagen2.Size = New System.Drawing.Size(56, 21)
        Me.btnBorrarImagen2.TabIndex = 311
        Me.btnBorrarImagen2.Text = "Esborrar"
        '
        'btnBorrarImagen1
        '
        Me.btnBorrarImagen1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBorrarImagen1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnBorrarImagen1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBorrarImagen1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnBorrarImagen1.Location = New System.Drawing.Point(304, 202)
        Me.btnBorrarImagen1.Name = "btnBorrarImagen1"
        Me.btnBorrarImagen1.Size = New System.Drawing.Size(56, 21)
        Me.btnBorrarImagen1.TabIndex = 310
        Me.btnBorrarImagen1.Text = "Esborrar"
        '
        'btnCanviarImagen2
        '
        Me.btnCanviarImagen2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCanviarImagen2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnCanviarImagen2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCanviarImagen2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCanviarImagen2.Location = New System.Drawing.Point(194, 444)
        Me.btnCanviarImagen2.Name = "btnCanviarImagen2"
        Me.btnCanviarImagen2.Size = New System.Drawing.Size(112, 21)
        Me.btnCanviarImagen2.TabIndex = 308
        Me.btnCanviarImagen2.Text = "Canviar Imatge 2"
        '
        'btnCanviarImagen1
        '
        Me.btnCanviarImagen1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCanviarImagen1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnCanviarImagen1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCanviarImagen1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCanviarImagen1.Location = New System.Drawing.Point(192, 202)
        Me.btnCanviarImagen1.Name = "btnCanviarImagen1"
        Me.btnCanviarImagen1.Size = New System.Drawing.Size(112, 21)
        Me.btnCanviarImagen1.TabIndex = 1
        Me.btnCanviarImagen1.Text = "Canviar Imatge 1"
        '
        'datagrid3
        '
        Me.datagrid3.CaptionHeight = 17
        Me.datagrid3.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.datagrid3.GroupByCaption = "Drag a column header here to group by that column"
        Me.datagrid3.Images.Add(CType(resources.GetObject("resource20"), System.Drawing.Image))
        Me.datagrid3.Location = New System.Drawing.Point(12, 348)
        Me.datagrid3.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.datagrid3.Name = "datagrid3"
        Me.datagrid3.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.datagrid3.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.datagrid3.PreviewInfo.ZoomFactor = 75
        Me.datagrid3.RecordSelectorWidth = 17
        Me.datagrid3.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.datagrid3.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.datagrid3.RowHeight = 15
        Me.datagrid3.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.datagrid3.Size = New System.Drawing.Size(552, 132)
        Me.datagrid3.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.datagrid3.TabIndex = 313
        Me.datagrid3.Text = "C1TrueDBGrid1"
        Me.datagrid3.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Style1{}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:Hig" & _
        "hlightText;BackColor:Highlight;}Style12{}OddRow{}RecordSelector{AlignImage:Cente" & _
        "r;}Style13{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeCo" & _
        "lor:ControlText;AlignVert:Center;}Style8{}Style10{AlignHorz:Near;}Style11{}Style" & _
        "14{}Style15{}Style9{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name" & _
        "="""" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeight=""17"" MarqueeS" & _
        "tyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=""17"" VerticalScr" & _
        "ollGroup=""1"" HorizontalScrollGroup=""1""><CaptionStyle parent=""Style2"" me=""Style10" & _
        """ /><EditorStyle parent=""Editor"" me=""Style5"" /><EvenRowStyle parent=""EvenRow"" me" & _
        "=""Style8"" /><FilterBarStyle parent=""FilterBar"" me=""Style13"" /><FooterStyle paren" & _
        "t=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style12"" /><HeadingStyle" & _
        " parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow"" me=""Sty" & _
        "le7"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle parent=""OddRo" & _
        "w"" me=""Style9"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style11"" /><Se" & _
        "lectedStyle parent=""Selected"" me=""Style6"" /><Style parent=""Normal"" me=""Style1"" /" & _
        "><ClientRect>0, 0, 548, 128</ClientRect><BorderSide>0</BorderSide></C1.Win.C1Tru" & _
        "eDBGrid.MergeView></Splits><NamedStyles><Style parent="""" me=""Normal"" /><Style pa" & _
        "rent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /><Style parent" & _
        "=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" /><Style parent=" & _
        """Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Editor"" /><Style parent=""Nor" & _
        "mal"" me=""HighlightRow"" /><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""No" & _
        "rmal"" me=""OddRow"" /><Style parent=""Heading"" me=""RecordSelector"" /><Style parent=" & _
        """Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""Group"" /></NamedStyles><ve" & _
        "rtSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>None</Layout><DefaultRe" & _
        "cSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 548, 128</ClientArea><PrintPa" & _
        "geHeaderStyle parent="""" me=""Style14"" /><PrintPageFooterStyle parent="""" me=""Style" & _
        "15"" /></Blob>"
        '
        'dgColorFicha
        '
        Me.dgColorFicha.Caption = "COLORIDO PRENDA"
        Me.dgColorFicha.CaptionHeight = 17
        Me.dgColorFicha.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.dgColorFicha.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgColorFicha.Images.Add(CType(resources.GetObject("resource21"), System.Drawing.Image))
        Me.dgColorFicha.Location = New System.Drawing.Point(12, 160)
        Me.dgColorFicha.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.dgColorFicha.Name = "dgColorFicha"
        Me.dgColorFicha.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgColorFicha.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgColorFicha.PreviewInfo.ZoomFactor = 75
        Me.dgColorFicha.RecordSelectorWidth = 17
        Me.dgColorFicha.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgColorFicha.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.dgColorFicha.RowHeight = 15
        Me.dgColorFicha.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.dgColorFicha.Size = New System.Drawing.Size(548, 160)
        Me.dgColorFicha.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgColorFicha.TabIndex = 311
        Me.dgColorFicha.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Style9{}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:Hig" & _
        "hlightText;BackColor:Highlight;}Style14{}OddRow{}RecordSelector{AlignImage:Cente" & _
        "r;}Style15{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeCol" & _
        "or:ControlText;BackColor:Control;}Style8{}Style10{AlignHorz:Near;}Style11{}Style" & _
        "12{}Style13{}Style1{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name" & _
        "="""" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeight=""17"" MarqueeS" & _
        "tyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=""17"" VerticalScr" & _
        "ollGroup=""1"" HorizontalScrollGroup=""1""><CaptionStyle parent=""Style2"" me=""Style10" & _
        """ /><EditorStyle parent=""Editor"" me=""Style5"" /><EvenRowStyle parent=""EvenRow"" me" & _
        "=""Style8"" /><FilterBarStyle parent=""FilterBar"" me=""Style13"" /><FooterStyle paren" & _
        "t=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style12"" /><HeadingStyle" & _
        " parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow"" me=""Sty" & _
        "le7"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle parent=""OddRo" & _
        "w"" me=""Style9"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style11"" /><Se" & _
        "lectedStyle parent=""Selected"" me=""Style6"" /><Style parent=""Normal"" me=""Style1"" /" & _
        "><ClientRect>0, 17, 544, 139</ClientRect><BorderSide>0</BorderSide></C1.Win.C1Tr" & _
        "ueDBGrid.MergeView></Splits><NamedStyles><Style parent="""" me=""Normal"" /><Style p" & _
        "arent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /><Style paren" & _
        "t=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" /><Style parent" & _
        "=""Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Editor"" /><Style parent=""No" & _
        "rmal"" me=""HighlightRow"" /><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""N" & _
        "ormal"" me=""OddRow"" /><Style parent=""Heading"" me=""RecordSelector"" /><Style parent" & _
        "=""Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""Group"" /></NamedStyles><v" & _
        "ertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>None</Layout><DefaultR" & _
        "ecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 544, 156</ClientArea><PrintP" & _
        "ageHeaderStyle parent="""" me=""Style14"" /><PrintPageFooterStyle parent="""" me=""Styl" & _
        "e15"" /></Blob>"
        '
        'dgTejidoFicha
        '
        Me.dgTejidoFicha.Caption = "TEJIDO"
        Me.dgTejidoFicha.CaptionHeight = 17
        Me.dgTejidoFicha.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.dgTejidoFicha.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgTejidoFicha.Images.Add(CType(resources.GetObject("resource22"), System.Drawing.Image))
        Me.dgTejidoFicha.Location = New System.Drawing.Point(12, 60)
        Me.dgTejidoFicha.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.dgTejidoFicha.Name = "dgTejidoFicha"
        Me.dgTejidoFicha.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgTejidoFicha.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgTejidoFicha.PreviewInfo.ZoomFactor = 75
        Me.dgTejidoFicha.RecordSelectorWidth = 17
        Me.dgTejidoFicha.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgTejidoFicha.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.dgTejidoFicha.RowHeight = 15
        Me.dgTejidoFicha.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.dgTejidoFicha.Size = New System.Drawing.Size(548, 84)
        Me.dgTejidoFicha.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgTejidoFicha.TabIndex = 310
        Me.dgTejidoFicha.Text = "C1TrueDBGrid1"
        Me.dgTejidoFicha.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Style1{}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:Hig" & _
        "hlightText;BackColor:Highlight;}Style12{}OddRow{}RecordSelector{AlignImage:Cente" & _
        "r;}Style13{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeCo" & _
        "lor:ControlText;AlignVert:Center;}Style8{}Style10{AlignHorz:Near;}Style11{}Style" & _
        "14{}Style15{}Style9{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name" & _
        "="""" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeight=""17"" MarqueeS" & _
        "tyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=""17"" VerticalScr" & _
        "ollGroup=""1"" HorizontalScrollGroup=""1""><CaptionStyle parent=""Style2"" me=""Style10" & _
        """ /><EditorStyle parent=""Editor"" me=""Style5"" /><EvenRowStyle parent=""EvenRow"" me" & _
        "=""Style8"" /><FilterBarStyle parent=""FilterBar"" me=""Style13"" /><FooterStyle paren" & _
        "t=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style12"" /><HeadingStyle" & _
        " parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow"" me=""Sty" & _
        "le7"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle parent=""OddRo" & _
        "w"" me=""Style9"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style11"" /><Se" & _
        "lectedStyle parent=""Selected"" me=""Style6"" /><Style parent=""Normal"" me=""Style1"" /" & _
        "><ClientRect>0, 17, 544, 63</ClientRect><BorderSide>0</BorderSide></C1.Win.C1Tru" & _
        "eDBGrid.MergeView></Splits><NamedStyles><Style parent="""" me=""Normal"" /><Style pa" & _
        "rent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /><Style parent" & _
        "=""Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" /><Style parent=" & _
        """Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Editor"" /><Style parent=""Nor" & _
        "mal"" me=""HighlightRow"" /><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""No" & _
        "rmal"" me=""OddRow"" /><Style parent=""Heading"" me=""RecordSelector"" /><Style parent=" & _
        """Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""Group"" /></NamedStyles><ve" & _
        "rtSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>None</Layout><DefaultRe" & _
        "cSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 544, 80</ClientArea><PrintPag" & _
        "eHeaderStyle parent="""" me=""Style14"" /><PrintPageFooterStyle parent="""" me=""Style1" & _
        "5"" /></Blob>"
        '
        'lblNumeroMuestras
        '
        Me.lblNumeroMuestras.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblNumeroMuestras.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblNumeroMuestras.Location = New System.Drawing.Point(36, 328)
        Me.lblNumeroMuestras.Name = "lblNumeroMuestras"
        Me.lblNumeroMuestras.Size = New System.Drawing.Size(100, 16)
        Me.lblNumeroMuestras.TabIndex = 306
        Me.lblNumeroMuestras.Text = "MUESTRAS"
        Me.lblNumeroMuestras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(183, Byte), CType(181, Byte), CType(208, Byte))
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label23.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label23.Location = New System.Drawing.Point(380, 328)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(76, 16)
        Me.Label23.TabIndex = 300
        Me.Label23.Text = "Preu Venda"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label23.Visible = False
        '
        'tx3VENDA
        '
        Me.tx3VENDA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx3VENDA.Location = New System.Drawing.Point(456, 324)
        Me.tx3VENDA.Name = "tx3VENDA"
        Me.tx3VENDA.TabIndex = 299
        Me.tx3VENDA.Tag = Nothing
        Me.tx3VENDA.Visible = False
        '
        'txtNumeroMuestras
        '
        Me.txtNumeroMuestras.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtNumeroMuestras.Location = New System.Drawing.Point(8, 324)
        Me.txtNumeroMuestras.Name = "txtNumeroMuestras"
        Me.txtNumeroMuestras.Size = New System.Drawing.Size(24, 20)
        Me.txtNumeroMuestras.TabIndex = 79
        Me.txtNumeroMuestras.Tag = Nothing
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.tx6TEMPORADA)
        Me.GroupBox5.Controls.Add(Me.lblClienteF)
        Me.GroupBox5.Controls.Add(Me.lblTemporadaFicha)
        Me.GroupBox5.Controls.Add(Me.tx6CLIENT)
        Me.GroupBox5.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(552, 44)
        Me.GroupBox5.TabIndex = 76
        Me.GroupBox5.TabStop = False
        '
        'tx6TEMPORADA
        '
        Me.tx6TEMPORADA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx6TEMPORADA.Location = New System.Drawing.Point(416, 16)
        Me.tx6TEMPORADA.Name = "tx6TEMPORADA"
        Me.tx6TEMPORADA.ReadOnly = True
        Me.tx6TEMPORADA.Size = New System.Drawing.Size(116, 20)
        Me.tx6TEMPORADA.TabIndex = 31
        Me.tx6TEMPORADA.Tag = Nothing
        '
        'lblClienteF
        '
        Me.lblClienteF.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblClienteF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblClienteF.Location = New System.Drawing.Point(8, 20)
        Me.lblClienteF.Name = "lblClienteF"
        Me.lblClienteF.Size = New System.Drawing.Size(56, 15)
        Me.lblClienteF.TabIndex = 34
        Me.lblClienteF.Text = "Client"
        Me.lblClienteF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTemporadaFicha
        '
        Me.lblTemporadaFicha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblTemporadaFicha.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTemporadaFicha.Location = New System.Drawing.Point(348, 20)
        Me.lblTemporadaFicha.Name = "lblTemporadaFicha"
        Me.lblTemporadaFicha.Size = New System.Drawing.Size(64, 15)
        Me.lblTemporadaFicha.TabIndex = 35
        Me.lblTemporadaFicha.Text = "Temporada"
        Me.lblTemporadaFicha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tx6CLIENT
        '
        Me.tx6CLIENT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tx6CLIENT.Location = New System.Drawing.Point(76, 16)
        Me.tx6CLIENT.Name = "tx6CLIENT"
        Me.tx6CLIENT.ReadOnly = True
        Me.tx6CLIENT.Size = New System.Drawing.Size(248, 20)
        Me.tx6CLIENT.TabIndex = 32
        Me.tx6CLIENT.Tag = Nothing
        '
        'tabPageFicha2
        '
        Me.tabPageFicha2.BackColor = System.Drawing.SystemColors.Control
        Me.tabPageFicha2.Controls.Add(Me.TextBox2)
        Me.tabPageFicha2.Controls.Add(Me.TextBox1)
        Me.tabPageFicha2.Controls.Add(Me.datagrid5)
        Me.tabPageFicha2.Controls.Add(Me.dgConsumosFichaM)
        Me.tabPageFicha2.Controls.Add(Me.TextBox9)
        Me.tabPageFicha2.Location = New System.Drawing.Point(4, 22)
        Me.tabPageFicha2.Name = "tabPageFicha2"
        Me.tabPageFicha2.Size = New System.Drawing.Size(970, 482)
        Me.tabPageFicha2.TabIndex = 6
        Me.tabPageFicha2.Text = "Fitxa Model 2"
        '
        'TextBox2
        '
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.TextBox2.Location = New System.Drawing.Point(296, 8)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(108, 20)
        Me.TextBox2.TabIndex = 319
        Me.TextBox2.Tag = Nothing
        Me.TextBox2.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.TextBox1.Location = New System.Drawing.Point(40, 8)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(260, 20)
        Me.TextBox1.TabIndex = 318
        Me.TextBox1.Tag = Nothing
        Me.TextBox1.Visible = False
        '
        'datagrid5
        '
        Me.datagrid5.CaptionHeight = 17
        Me.datagrid5.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.datagrid5.GroupByCaption = "Drag a column header here to group by that column"
        Me.datagrid5.Images.Add(CType(resources.GetObject("resource23"), System.Drawing.Image))
        Me.datagrid5.Location = New System.Drawing.Point(72, 344)
        Me.datagrid5.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.datagrid5.Name = "datagrid5"
        Me.datagrid5.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.datagrid5.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.datagrid5.PreviewInfo.ZoomFactor = 75
        Me.datagrid5.RecordSelectorWidth = 17
        Me.datagrid5.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.datagrid5.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.datagrid5.RowHeight = 15
        Me.datagrid5.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.datagrid5.Size = New System.Drawing.Size(404, 76)
        Me.datagrid5.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.datagrid5.TabIndex = 317
        Me.datagrid5.Text = "C1TrueDBGrid1"
        Me.datagrid5.Visible = False
        Me.datagrid5.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Style9{}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:Hig" & _
        "hlightText;BackColor:Highlight;}Style14{}OddRow{}RecordSelector{AlignImage:Cente" & _
        "r;}Style15{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeCol" & _
        "or:ControlText;BackColor:Control;}Style8{}Style10{AlignHorz:Near;}Style11{}Style" & _
        "12{}Style13{}Style1{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name" & _
        "="""" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeight=""17"" MarqueeS" & _
        "tyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=""17"" VerticalScr" & _
        "ollGroup=""1"" HorizontalScrollGroup=""1""><CaptionStyle parent=""Style2"" me=""Style10" & _
        """ /><EditorStyle parent=""Editor"" me=""Style5"" /><EvenRowStyle parent=""EvenRow"" me" & _
        "=""Style8"" /><FilterBarStyle parent=""FilterBar"" me=""Style13"" /><FooterStyle paren" & _
        "t=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style12"" /><HeadingStyle" & _
        " parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow"" me=""Sty" & _
        "le7"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle parent=""OddRo" & _
        "w"" me=""Style9"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style11"" /><Se" & _
        "lectedStyle parent=""Selected"" me=""Style6"" /><Style parent=""Normal"" me=""Style1"" /" & _
        "><ClientRect>0, 0, 400, 72</ClientRect><BorderSide>0</BorderSide></C1.Win.C1True" & _
        "DBGrid.MergeView></Splits><NamedStyles><Style parent="""" me=""Normal"" /><Style par" & _
        "ent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /><Style parent=" & _
        """Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" /><Style parent=""" & _
        "Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Editor"" /><Style parent=""Norm" & _
        "al"" me=""HighlightRow"" /><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Nor" & _
        "mal"" me=""OddRow"" /><Style parent=""Heading"" me=""RecordSelector"" /><Style parent=""" & _
        "Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""Group"" /></NamedStyles><ver" & _
        "tSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>None</Layout><DefaultRec" & _
        "SelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 400, 72</ClientArea><PrintPage" & _
        "HeaderStyle parent="""" me=""Style14"" /><PrintPageFooterStyle parent="""" me=""Style15" & _
        """ /></Blob>"
        '
        'dgConsumosFichaM
        '
        Me.dgConsumosFichaM.CaptionHeight = 17
        Me.dgConsumosFichaM.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System
        Me.dgConsumosFichaM.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgConsumosFichaM.Images.Add(CType(resources.GetObject("resource24"), System.Drawing.Image))
        Me.dgConsumosFichaM.Location = New System.Drawing.Point(80, 248)
        Me.dgConsumosFichaM.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
        Me.dgConsumosFichaM.Name = "dgConsumosFichaM"
        Me.dgConsumosFichaM.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgConsumosFichaM.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgConsumosFichaM.PreviewInfo.ZoomFactor = 75
        Me.dgConsumosFichaM.RecordSelectorWidth = 17
        Me.dgConsumosFichaM.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgConsumosFichaM.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single
        Me.dgConsumosFichaM.RowHeight = 15
        Me.dgConsumosFichaM.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.dgConsumosFichaM.Size = New System.Drawing.Size(340, 68)
        Me.dgConsumosFichaM.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation
        Me.dgConsumosFichaM.TabIndex = 316
        Me.dgConsumosFichaM.Text = "C1TrueDBGrid1"
        Me.dgConsumosFichaM.Visible = False
        Me.dgConsumosFichaM.PropBag = "<?xml version=""1.0""?><Blob><Styles type=""C1.Win.C1TrueDBGrid.Design.ContextWrappe" & _
        "r""><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}E" & _
        "ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" & _
        "ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" & _
        "CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" & _
        "er;}Style9{}Normal{Font:Microsoft Sans Serif, 8.25pt;}HighlightRow{ForeColor:Hig" & _
        "hlightText;BackColor:Highlight;}Style14{}OddRow{}RecordSelector{AlignImage:Cente" & _
        "r;}Style15{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeCol" & _
        "or:ControlText;BackColor:Control;}Style8{}Style10{AlignHorz:Near;}Style11{}Style" & _
        "12{}Style13{}Style1{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name" & _
        "="""" CaptionHeight=""17"" ColumnCaptionHeight=""17"" ColumnFooterHeight=""17"" MarqueeS" & _
        "tyle=""DottedCellBorder"" RecordSelectorWidth=""17"" DefRecSelWidth=""17"" VerticalScr" & _
        "ollGroup=""1"" HorizontalScrollGroup=""1""><CaptionStyle parent=""Style2"" me=""Style10" & _
        """ /><EditorStyle parent=""Editor"" me=""Style5"" /><EvenRowStyle parent=""EvenRow"" me" & _
        "=""Style8"" /><FilterBarStyle parent=""FilterBar"" me=""Style13"" /><FooterStyle paren" & _
        "t=""Footer"" me=""Style3"" /><GroupStyle parent=""Group"" me=""Style12"" /><HeadingStyle" & _
        " parent=""Heading"" me=""Style2"" /><HighLightRowStyle parent=""HighlightRow"" me=""Sty" & _
        "le7"" /><InactiveStyle parent=""Inactive"" me=""Style4"" /><OddRowStyle parent=""OddRo" & _
        "w"" me=""Style9"" /><RecordSelectorStyle parent=""RecordSelector"" me=""Style11"" /><Se" & _
        "lectedStyle parent=""Selected"" me=""Style6"" /><Style parent=""Normal"" me=""Style1"" /" & _
        "><ClientRect>0, 0, 336, 64</ClientRect><BorderSide>0</BorderSide></C1.Win.C1True" & _
        "DBGrid.MergeView></Splits><NamedStyles><Style parent="""" me=""Normal"" /><Style par" & _
        "ent=""Normal"" me=""Heading"" /><Style parent=""Heading"" me=""Footer"" /><Style parent=" & _
        """Heading"" me=""Caption"" /><Style parent=""Heading"" me=""Inactive"" /><Style parent=""" & _
        "Normal"" me=""Selected"" /><Style parent=""Normal"" me=""Editor"" /><Style parent=""Norm" & _
        "al"" me=""HighlightRow"" /><Style parent=""Normal"" me=""EvenRow"" /><Style parent=""Nor" & _
        "mal"" me=""OddRow"" /><Style parent=""Heading"" me=""RecordSelector"" /><Style parent=""" & _
        "Normal"" me=""FilterBar"" /><Style parent=""Caption"" me=""Group"" /></NamedStyles><ver" & _
        "tSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>None</Layout><DefaultRec" & _
        "SelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 336, 64</ClientArea><PrintPage" & _
        "HeaderStyle parent="""" me=""Style14"" /><PrintPageFooterStyle parent="""" me=""Style15" & _
        """ /></Blob>"
        '
        'TextBox9
        '
        Me.TextBox9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.TextBox9.Location = New System.Drawing.Point(72, 320)
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(392, 20)
        Me.TextBox9.TabIndex = 315
        Me.TextBox9.Tag = Nothing
        Me.TextBox9.Visible = False
        '
        'frmModelos
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(992, 598)
        Me.Controls.Add(Me.tabControlModelos)
        Me.Controls.Add(Me.gbEleccionCliente)
        Me.Controls.Add(Me.gbEleccionTemporadas)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Name = "frmModelos"
        Me.Text = "Models"
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
        Me.Controls.SetChildIndex(Me.cboSeleccionCentro, 0)
        Me.Controls.SetChildIndex(Me.gbEleccionTemporadas, 0)
        Me.Controls.SetChildIndex(Me.gbEleccionCliente, 0)
        Me.Controls.SetChildIndex(Me.tabControlModelos, 0)
        Me.gbEleccionCliente.ResumeLayout(False)
        CType(Me.cboSeleccionCliente, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbEleccionTemporadas.ResumeLayout(False)
        Me.tabControlModelos.ResumeLayout(False)
        Me.tabPageModelo.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.txtVENDAFINAL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVENDA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCOST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTejido.ResumeLayout(False)
        CType(Me.cboNOMACA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMESTAM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMTINT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMPROVE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTEIXIT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRENDIM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAMPLE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTINT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtESTAM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtACA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPROVE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDESCRITEIXIT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbConfeccion.ResumeLayout(False)
        CType(Me.cboNOMCONFEC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCONFEC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbModelo.ResumeLayout(False)
        CType(Me.cboTEMPORADA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCODIMODEL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNOMCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSERIE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDESCRI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOBSERV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPageForniturasModelo.ResumeLayout(False)
        CType(Me.cboMedidasForni, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.tx3NOMCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx3SERIE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx3CLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx3CODI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox12.ResumeLayout(False)
        CType(Me.tx4VENDA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx3COST, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFornituras, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgForni, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPageEscandalloPrecio.ResumeLayout(False)
        Me.GroupBox11.ResumeLayout(False)
        CType(Me.tx5VENDA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx4COST, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTejido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgEscandall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox7.ResumeLayout(False)
        CType(Me.txtNPACK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMARGE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx2COST, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx2VENDA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNFORNITURA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNCONFEC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNPLANXA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNREPAS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMANIPULACION, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgManipulacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.tx4NOMCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx4SERIE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx4CLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx4CODI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPageColores.ResumeLayout(False)
        CType(Me.dgEscandalloResumen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtColorQuitar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboColor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTalles.ResumeLayout(False)
        CType(Me.txtTALLA10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA09, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA08, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA07, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA06, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA05, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA04, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA03, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA02, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTALLA01, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox9.ResumeLayout(False)
        CType(Me.tx2NOMCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx2SERIE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx2CLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx2CODI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox13.ResumeLayout(False)
        CType(Me.tx7VENDA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx6COST, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgColores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtColorAñadir, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPageStock.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        CType(Me.tx5NOMCLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx5SERIE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx5CLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx5CODI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbsPrecio.ResumeLayout(False)
        CType(Me.tx6VENDA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx5COST, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPageFichaModelo.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.datagrid3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgColorFicha, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgTejidoFicha, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx3VENDA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNumeroMuestras, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.tx6TEMPORADA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx6CLIENT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPageFicha2.ResumeLayout(False)
        CType(Me.TextBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.datagrid5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgConsumosFichaM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmModelos
    Public Shared Function GetInstance() As frmModelos
        If frmChildForm Is Nothing Then
            frmChildForm = New frmModelos

        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Public registroActual As clsModelo
    Private dtFichaTejido As New DataTable("FICHATEJIDOS")
    Private dtColores As New DataTable("MODCOL")

#End Region

#Region "INICIALIZAR"

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            ACN()
            cargando = True
            Me.arrayTextBox = New System.Windows.Forms.Control() {Me.tx2CLIENT, Me.tx2CODI, Me.tx2COST, Me.tx2NOMCLIENT, Me.tx2SERIE, Me.tx2VENDA, Me.tx3CLIENT, Me.tx3CODI, Me.tx3COST, Me.tx3NOMCLIENT, Me.tx3SERIE, Me.tx4CLIENT, Me.tx4CODI, Me.tx4COST, Me.tx4NOMCLIENT, Me.tx4SERIE, Me.tx4VENDA, Me.tx5CLIENT, Me.tx5CODI, Me.tx5COST, Me.tx5NOMCLIENT, Me.tx5SERIE, Me.tx5VENDA, Me.tx6COST, Me.tx6VENDA, Me.tx7VENDA, Me.txtACA, Me.txtAMPLE, Me.txtCLIENT, Me.txtCODIMODEL, Me.txtCONFEC, Me.txtCOST, Me.txtColorQuitar, Me.txtDESCRI, Me.txtDESCRITEIXIT, Me.txtESTAM, Me.txtMARGE, Me.txtNCONFEC, Me.txtNFORNITURA, Me.txtNPACK, Me.txtNPLANXA, Me.txtNREPAS, Me.txtMANIPULACION, Me.txtOBSERV, Me.txtPROVE, Me.txtRENDIM, Me.txtSERIE, Me.txtTALLA01, Me.txtTALLA02, Me.txtTALLA03, Me.txtTALLA04, Me.txtTALLA05, Me.txtTALLA06, Me.txtTALLA07, Me.txtTALLA08, Me.txtTALLA09, Me.txtTALLA10, Me.txtTINT, Me.txtVENDA, Me.txtVENDAFINAL}
            Me.arrayGrids = New System.Windows.Forms.Control() {Me.dgColores, Me.dgEscandall, Me.dgForni, Me.dgStock, Me.dgManipulacion}
            Me.arrayCombos = New System.Windows.Forms.Control() {Me.cboNOMACA, Me.cboNOMCLIENT, Me.cboNOMCONFEC, Me.cboNOMESTAM, Me.cboNOMPROVE, Me.cboNOMTINT, Me.cboTEIXIT, Me.cboTEMPORADA}
            Me.arrayEtiquetas = New System.Windows.Forms.Control() {Me.Label1, Me.Label10, Me.Label11, Me.Label12, Me.Label13, Me.Label16, Me.Label2, Me.Label20, Me.Label21, Me.Label22, Me.Label24, Me.Label25, Me.Label26, Me.Label27, Me.Label28, Me.Label29, Me.Label3, Me.Label30, Me.Label31, Me.Label32, Me.Label33, Me.Label4, Me.Label5, Me.Label6, Me.Label7, Me.Label8, Me.Label9, Me.lblAcabadorTejido, Me.lblCliente, Me.lblCodigoModelo, Me.lblDescripcion, Me.lblEstampadorTejido, Me.lblMargen, Me.lblModelo, Me.lblPack, Me.lblPrecioCoste, Me.lblPrecioVenta, Me.lblPrecioVentaModelo, Me.lblProveedorTejido, Me.lblRendimientoKGTejido, Me.lblSerie, Me.lblTallerConfeccion, Me.lblTejido, Me.lblTemporada, Me.lblTinteTejido, Me.llbAnchoPiezaTejido}
            Me.arrayBotones = New System.Windows.Forms.Control() {Me.btnAñadirColor, Me.btnBorrarColor, Me.btnElegirAcabador, Me.btnElegirCliente, Me.btnElegirEstampador, Me.btnElegirProveedor, Me.btnElegirTallerConfeccion, Me.btnElegirTejido, Me.btnElegirTintador, Me.btnEscogerModelo}
            '
            'btnRecargar
            '
            Me.btnRecargar.Name = "btnRecargar"
            '
            'arrayTabPages
            '
            Me.arrayTabPages = New System.Windows.Forms.Control() {Me.tabPageColores, Me.tabPageEscandalloPrecio, Me.tabPageForniturasModelo, Me.tabPageModelo, Me.tabPageStock}
            tabla = tablaModelos
            registroActual = New clsModelo(ds.Tables(tablaModelos), empresaPorDefecto, BindingContext)
            HacerBindings()
            IniciarDGs()
            'registroActual.tabla.AcceptChanges()

            PonerModificables(soloLectura)
            PonerHandlersErroresParaGrids()
            PSBTIPO(registroActual.centro) : cargando = False
            btnSiguiente.Focus()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub IniciarDGs()
        Try
            IniciarDGCartaFornituras()
            IniciarDGEscandallo()
            IniciarDGEscandalloC()
            IniciarDGColores()
            IniciarDGStock()
            IniciarDGManipulaciones()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub HacerBindings()
        Try
            HacerBindingsTodos(registroActual.dvForm)

            AñadirBinding(cboID, registroActual.dvForm, "CODI") : AutoSizeCC(cboID)
            AñadirBindingCombo(cboID, registroActual.dvIdentificadores, "CODI", "DESCRI")

            dgForni.SetDataBinding(registroActual.detalleFornituras.dvForm, "")
            dgManipulacion.SetDataBinding(registroActual.detalleManipulacion.dvForm, "")
            dgEscandall.SetDataBinding(registroActual.detalleEscandallo.dvForm, "")
            dgEscandalloResumen.SetDataBinding(registroActual.detalleEscandallo.dvForm, "")
            dgStock.SetDataBinding(registroActual.detalleStock.dvForm, "")
            dgColores.SetDataBinding(registroActual.detalleColores.dvForm, "")

            CrearBindingsATablas()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CrearBindingsATablas()
        Try
            AñadirBindingCombo(cboNOMCLIENT, registroActual.dtClients, CCClients, CNClients)

            AñadirBindingCombo(cboNOMPROVE, registroActual.dtProve, CCProve, CNProve)

            AñadirBindingCombo(cboNOMESTAM, registroActual.dtTallers.Copy, CCTallers, CNTallers)
            AñadirBindingCombo(cboNOMACA, registroActual.dtTallers.Copy, CCTallers, CNTallers)
            AñadirBindingCombo(cboNOMCONFEC, registroActual.dtTallers.Copy, CCTallers, CNTallers)
            AñadirBindingCombo(cboNOMTINT, registroActual.dtTallers.Copy, CCTallers, CNTallers)
            AñadirBindingCombo(cboTEMPORADA, registroActual.dtTemporadas, "CODI", "CODI")

            AñadirBindingCombo(cboColor, registroActual.detalleColores.dtColoresDeTejidos, "COLOR", "COLOR")

            AñadirBindingCombo(cboFornituras, registroActual.dtForni.DefaultView, CCForni, CCForni)
            AñadirBindingCombo(cboMedidasForni, registroActual.detalleFornituras.dtMedidas.DefaultView, "MEDIDA", "MEDIDA")

            AñadirBindingCombo(cboTEIXIT, registroActual.dtTeixits, CCTeixits, CCTeixits)
            AñadirBindingCombo(cboTejido, registroActual.dtTeixits.Copy.DefaultView, CCTeixits, CCTeixits)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "MODIFICAR DB"

    Friend Overrides Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnNuevo.Click
        Dim drNew As DataRow
        Try
            cargando = True
            EsRegistroNuevo = True
            PreparaParaNuevoRegistro()
            registroActual.NuevoRegistro()
            PSBTIPO(registroActual.centro)
            cargando = False

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
    Private Sub ActualizarOrigen()
        Try
            If editando Then
                editando = False
                ' registroActual.tabla.AcceptChanges()
                PonerModificables(soloLectura)
                'cboID.Text = registroActual.CODI
            End If
            If EsRegistroNuevo Then
                editando = True : PSBTIPO(registroActual.centro)
                PonerTabs()
                NuevoRegistrConfimado()
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
            If registroActual.TieneCambios Then
                Select Case MessageBox.Show(rm.GetString("QUIERECONFIRMARLOSCAMBIOS"), rm.GetString("INFORMACION"), MessageBoxButtons.YesNoCancel)
                    Case DialogResult.Cancel
                        cargando = False
                        Exit Sub
                    Case DialogResult.No
                        registroActual.tabla.RejectChanges()
                        registroActual.detalleColores.tabla.RejectChanges()
                        registroActual.detalleEscandallo.tabla.RejectChanges()
                        registroActual.detalleFornituras.tabla.RejectChanges()
                        registroActual.detalleManipulacion.tabla.RejectChanges()
                        registroActual.detalleStock.tabla.RejectChanges()
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
            If MessageBox.Show(rm.GetString("BorrarModelo"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                cargando = True
                editando = True
                registroActual.borrar() : ActualizarOrigen()
                editando = False
                cargando = False
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ESPECÍFICO"

    Private Sub IniciarDGManipulaciones()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgManipulacion)
            i = 0
            PPCol2("CODI", dgManipulacion, rm.GetString("NOMBRE"), "", True, 90, False, PresentationEnum.Normal, False, 90, i, False)
            i = 1
            PPCol2("DESCRI", dgManipulacion, rm.GetString("DESCRIPCION"), "", True, 200, True, PresentationEnum.Normal, False, 200, i, False)
            i = i + 1
            PPCol2("PREU", dgManipulacion, rm.GetString("PRECIO"), "#,##0.00", True, 60, True, PresentationEnum.Normal, False, 60, i, False)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#Region "FORNITURAS MODELO"

    Private Sub IniciarDGCartaFornituras()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgForni)
            i = 0
            PPCol2("FORNI", dgForni, rm.GetString("FORNITURA"), "", True, 90, False, PresentationEnum.ComboBox, False, 90, i, False, cboFornituras)
            i = 1
            PPCol2("DESCRI", dgForni, rm.GetString("DESCRIPCION"), "", True, 200, True, PresentationEnum.Normal, False, 200, i, False)
            i = 2
            PPCol2("MEDIDA", dgForni, rm.GetString("MEDIDA"), "", True, 70, True, PresentationEnum.Normal, False, 70, i, False, cboMedidasForni)
            i = 3
            PPCol2("UNITATS", dgForni, rm.GetString("UNIDADES"), "#,##0.00", True, 70, True, PresentationEnum.Normal, False, 70, i, False)
            i = 4
            PPCol2("PREU", dgForni, rm.GetString("PRECIO"), "#,##0.00", True, 70, True, PresentationEnum.Normal, False, 70, i, False)
            i = 5
            PPCol2("IMPORT", dgForni, rm.GetString("IMPORTE"), "#,##0.00", True, 70, True, PresentationEnum.Normal, False, 70, i, False)

            dgForni.ColumnFooters = True
            dgForni.FooterStyle.ForeColor = System.Drawing.Color.Green


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub dgForni_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgForni.RowColChange
        Try
            If PuedoModificar() Then
                'AutoSizeCC(dgForni)
                AutosizeColumnasTrueDBDropdown(cboFornituras)
                AutosizeColumnasTrueDBDropdown(cboMedidasForni)
                RecalcularTotales()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub RecalcularTotales()
        Try
            dgForni.Splits(0).DisplayColumns("IMPORT").DataColumn.FooterText = "TOTAL: " & Format(sumaTotal("IMPORT", registroActual.detalleFornituras.dvForm), "#,##0.00")
            If tabControlModelos.SelectedTab Is tabPageFichaModelo Then
                CargarImagen(registroActual.obtenerImagen("imagen1"), imgModelo1)
                CargarImagen(registroActual.obtenerImagen("imagen2"), imgModelo2)
            End If
            If tabControlModelos.SelectedTab Is tabPageModelo Then
                CargarImagen(registroActual.obtenerImagen("imagen3"), imgModelo3)
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub dgForni_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgForni.AfterColUpdate
        Try
            If PuedoModificar() Then
                cargando = True
                dgForni.UpdateData()
                Select Case e.Column.DataColumn.DataField
                    Case "PREU"
                        'registroActual.detalleFornituras.PREU = e.Column.DataColumn.Value
                    Case "CODI"
                        'registroActual.detalleFornituras.DESCR = e.Column.DataColumn.Value
                End Select
                registroActual.detalleFornituras.HacerCalculos(e.Column.DataColumn.DataField)
                cargando = False
            End If
            'With DirectCast(cboFornituras.DataSource, DataView)
            '    .Sort = "CODI"
            '    Dim idx As Integer
            '    idx = .Find(dgForni(dgForni.Row, "FORNI"))
            '    If Not idx = -1 Then
            '        dgForni(dgForni.Row, "DESCRI") = .Item(idx).Item("DESCRI")
            '        If e.Column.DataColumn.DataField = "FORNI" Then registroActual.detalleFornituras.PREU = .Item(idx).Item("PREU")
            '    Else
            '        dgForni(dgForni.Row, "DESCRI") = ""
            '    End If
            RecalcularTotales()

            'End With
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region
#Region "ESCANDALLO"

    'TODO: Hay que añadir el combo con los tejidos de momento con el metodo del boton
    Private Sub IniciarDGEscandallo()
        Dim i As Integer
        Try
            OcultarColumnasDG(dgEscandall)
            i = 0
            PPCol2("TITULO", dgEscandall, rm.GetString("PIEZA"), "", True, 100, False, PresentationEnum.Normal, False, 100, i, False)
            i = 1
            PPCol2("TEIXIT", dgEscandall, rm.GetString("TEJIDO"), "", True, 100, False, PresentationEnum.ComboBox, False, 100, i, False, cboTejido)
            i = 2
            PPCol2("CONSUM", dgEscandall, rm.GetString("CONSUMO"), "#,##0.000", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = 3
            PPCol2("PREU", dgEscandall, rm.GetString("PRECIO"), "#,##0.00", True, 50, False, PresentationEnum.Normal, False, 50, i, False)
            i = 4
            PPCol2("COST", dgEscandall, rm.GetString("IMPORTE"), "#,##0.00", True, 50, False, PresentationEnum.Normal, False, 50, i, False)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub dgEscandall_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgEscandall.AfterColUpdate
        Dim columna As String = e.Column.DataColumn.DataField
        Try
            If PuedoModificar() Then
                cargando = True
                dgEscandall.UpdateData()
                With DirectCast(cboTejido.DataSource, DataView)
                    .Sort = "CODI"
                    Dim idx As Integer
                    idx = .Find(dgEscandall(dgEscandall.Row, "TEIXIT"))
                    If Not idx = -1 Then
                        If nzn(.Item(idx).Item("PREU"), 0) <> registroActual.detalleEscandallo.PREU Then
                            If e.Column.DataColumn.DataField = "TEIXIT" Then registroActual.detalleEscandallo.PREU = nzn(.Item(idx).Item("PREU"), 0)
                        End If
                    End If
                    'Esta funcion comprueba que el titulo no haya variado (editado, añadido, borrado??) Si ha pasado esto hay
                    'que modficar los colores
                    registroActual.HacerCalculos()
                    'registroActual.detalleColores.ModificarColores()
                    AutosizeColumnasTrueDBDropdown(cboTejido)
                End With
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub
    Private Sub dgManipulacion_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgManipulacion.AfterColUpdate
        Dim columna As String = e.Column.DataColumn.DataField
        Try
            If PuedoModificar() Then
                cargando = True
                dgManipulacion.UpdateData()
                If columna = "PREU" Then registroActual.HacerCalculos()
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub
    Private Sub dgStock_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgStock.BeforeDelete
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

    Private Sub dgEscandall_BeforeDelete(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.CancelEventArgs) Handles dgEscandall.BeforeDelete
        Try
            If PuedoModificar() Then
                cargando = True
                registroActual.detalleColores.QuitarPieza(registroActual.detalleEscandallo.TITULO)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub dgEscandall_AfterInsert(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgEscandall.AfterInsert
        Try
            If PuedoModificar() Then
                cargando = True
                registroActual.detalleColores.AñadirPieza(registroActual.detalleEscandallo.TITULO)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub dgEscandall_BeforeColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs) Handles dgEscandall.BeforeColUpdate
        Try
            If PuedoModificar() Then
                cargando = True
                If e.Column.DataColumn.DataField = "TITULO" Then _
                    registroActual.detalleColores.ModificaPieza(registroActual.detalleEscandallo.TITULO, e.Column.DataColumn.Value)
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region
#Region "FICHA DEL MODELO"

    Private Sub XpButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBorrarImagen2.Click
        Try
            registroActual.BorrarImagen("imagen2")
            CargarImagen(Nothing, imgModelo2)
        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

    Private Sub XpButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBorrarImagen1.Click
        Try
            registroActual.BorrarImagen("imagen1")
            CargarImagen(Nothing, imgModelo1)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub btnCanviarImagen3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCanviarImagen3.Click
        Try
            registroActual.guardarImagen("imagen3")
            CargarImagen(registroActual.obtenerImagen("imagen3"), imgModelo3)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            registroActual.BorrarImagen("imagen3")
            CargarImagen(Nothing, imgModelo3)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub btnCanviarImagen1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCanviarImagen1.Click
        Try
            registroActual.guardarImagen("imagen1")
            CargarImagen(registroActual.obtenerImagen("imagen1"), imgModelo1)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnCanviarImagen2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCanviarImagen2.Click

        Try
            registroActual.guardarImagen("imagen2")
            CargarImagen(registroActual.obtenerImagen("imagen2"), imgModelo2)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerTejidosCabecera()
        Dim i As Integer
        Try
            dtFichaTejido.Clear()
            dtFichaTejido.Columns.Clear()
            For i = 0 To registroActual.detalleEscandallo.dvForm.Count - 1
                Dim dc As New DataColumn(registroActual.detalleEscandallo.dvForm(i).Item("TITULO"))
                dtFichaTejido.Columns.Add(dc)
            Next

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerTejidosContenido()
        Dim i As Integer
        Dim dr As DataRow
        Try
            dr = dtFichaTejido.NewRow
            For i = 0 To registroActual.detalleEscandallo.dvForm.Count - 1
                dr(registroActual.detalleEscandallo.dvForm(i).Item("TITULO")) = registroActual.detalleEscandallo.dvForm(i).Item("TEIXIT")
            Next
            dtFichaTejido.Rows.Add(dr)
            dgTejidoFicha.SetDataBinding(dtFichaTejido, "")

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerTejidos()
        Try
            PonerTejidosCabecera()
            PonerTejidosContenido()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerColores()
        Dim i As Integer
        Dim dtColoresFicha As DataTable
        Try
            dtColoresFicha = dtColores.Clone
            dtColoresFicha = dtColores.Copy
            i = 0
            PPCol2("MODCOL", dgColorFicha, rm.GetString("COLOR"), "", True, 150, _
                    False, PresentationEnum.Normal, False, 150, i, False)

            For i = 0 To registroActual.detalleEscandallo.dvForm.Count - 1
                PPCol2("TITULO", dgColorFicha, rm.GetString("TITULO"), "", True, 150, _
                        False, PresentationEnum.ComboBox, False, 150, i + 1, False)
                'AñadirComboBoxColumn(registroActual.detalleEscandallo.dvForm(i).Item("TITULO"), registroActual.detalleEscandallo.dvForm(i).Item("TITULO"), 100, ts, 5000, Nothing, Nothing, Nothing, Nothing)
                'CargarColoresTejidoACombo(registroActual.detalleEscandallo.dvForm(i).Item("TEIXIT"), ts.GridColumnStyles(registroActual.detalleEscandallo.dvForm(i).Item("TITULO")).columncombobox)
                'AñadirTextBoxColumn(registroActual.detalleescandallo.dvform(i).Item("TITULO"), registroActual.detalleescandallo.dvform(i).Item("TITULO"), 150, ts, False, "", HorizontalAlignment.Left)
            Next
            i = i + 1
            PPCol2("MUESTRA", dgColorFicha, rm.GetString("COMPLEMENTO") & "?", "", True, 150, _
                    False, PresentationEnum.Normal, False, 150, i, False)

            dtColoresFicha.Columns.Add(New DataColumn("MUESTRA"))
            dgColorFicha.SetDataBinding(dtColoresFicha, "")

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerConsumos()
        Dim dtTeixitssModeloC As New DataTable
        Dim dc As DataColumn
        Dim dr As DataRow
        Dim i As Integer
        Try
            dtTeixitssModeloC.Clear()
            dtTeixitssModeloC.Columns.Clear()
            'titulos
            For i = 0 To registroActual.detalleEscandallo.dvForm.Count - 1
                dc = New DataColumn(registroActual.detalleEscandallo.dvForm(i).Item("TITULO"))
                dtTeixitssModeloC.Columns.Add(dc)
            Next
            'cabeceras tejidos
            dr = dtTeixitssModeloC.NewRow
            For i = 0 To registroActual.detalleEscandallo.dvForm.Count - 1
                dr(i) = "tejido"
            Next
            dtTeixitssModeloC.Rows.Add(dr)

            'los tejidos
            dr = dtTeixitssModeloC.NewRow
            For i = 0 To registroActual.detalleEscandallo.dvForm.Count - 1
                dr(registroActual.detalleEscandallo.dvForm(i).Item("TITULO")) = registroActual.detalleEscandallo.dvForm(i).Item("TEIXIT")
            Next
            dtTeixitssModeloC.Rows.Add(dr)

            'cabecera anchos
            dr = dtTeixitssModeloC.NewRow
            For i = 0 To registroActual.detalleEscandallo.dvForm.Count - 1
                dr(i) = "ancho"
            Next
            dtTeixitssModeloC.Rows.Add(dr)

            'anchos
            dr = dtTeixitssModeloC.NewRow
            For i = 0 To registroActual.detalleEscandallo.dvForm.Count - 1
                dr(registroActual.detalleEscandallo.dvForm(i).Item("TITULO")) = GetCampo("TEIXITS", "AMPLE", registroActual.detalleEscandallo.dvForm(i).Item("TEIXIT"), "string")
                'dr(registroActual.detalleescandallo.dvform(i).Item("TITULO")) = registroActual.detalleescandallo.dvform(i).Item("TEIXIT")
            Next
            dtTeixitssModeloC.Rows.Add(dr)

            'cabecera consumo
            dr = dtTeixitssModeloC.NewRow
            For i = 0 To registroActual.detalleEscandallo.dvForm.Count - 1
                dr(i) = "consumo"
            Next
            dtTeixitssModeloC.Rows.Add(dr)

            'consumos
            dr = dtTeixitssModeloC.NewRow
            For i = 0 To registroActual.detalleEscandallo.dvForm.Count - 1
                dr(registroActual.detalleEscandallo.dvForm(i).Item("TITULO")) = registroActual.detalleEscandallo.dvForm(i).Item("CONSUM")
                'dr(registroActual.detalleescandallo.dvform(i).Item("TITULO")) = registroActual.detalleescandallo.dvform(i).Item("TEIXIT")
            Next
            dtTeixitssModeloC.Rows.Add(dr)
            dgConsumosFichaM.SetDataBinding(dtTeixitssModeloC, "")
            'IniciarDGConsumos()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub GenerarFichaModelo()
        Try
            PonerTejidos()
            PonerColores()
            PonerConsumos()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region
#Region "STOCK"

    Private Sub IniciarDGStock()
        Dim i As Integer
        Try
            i = 0
            OcultarColumnasDG(dgStock)

            PPCol2("COLOR", dgStock, rm.GetString("COLOR"), "", True, 70, False, PresentationEnum.ComboBox, False, 150, i, False)

            If Not PosActual(registroActual.dvForm) = -1 Then
                With registroActual
                    i = 1
                    PPCol2("TALLA01", dgStock, .TALLA01, "#,##0.00", True, 70, False, PresentationEnum.Normal, False, 70, i, False, Nothing, IIf(.TALLA01 <> "", True, False))
                    i = 2
                    PPCol2("TALLA02", dgStock, .TALLA02, "#,##0.00", True, 70, False, PresentationEnum.Normal, False, 70, i, False, Nothing, IIf(.TALLA02 <> "", True, False))
                    i = 3
                    PPCol2("TALLA03", dgStock, .TALLA03, "#,##0.00", True, 70, False, PresentationEnum.Normal, False, 70, i, False, Nothing, IIf(.TALLA03 <> "", True, False))
                    i = 4
                    PPCol2("TALLA04", dgStock, .TALLA04, "#,##0.00", True, 70, False, PresentationEnum.Normal, False, 70, i, False, Nothing, IIf(.TALLA04 <> "", True, False))
                    i = 5
                    PPCol2("TALLA05", dgStock, .TALLA05, "#,##0.00", True, 70, False, PresentationEnum.Normal, False, 70, i, False, Nothing, IIf(.TALLA05 <> "", True, False))
                    i = 6
                    PPCol2("TALLA06", dgStock, .TALLA06, "#,##0.00", True, 70, False, PresentationEnum.Normal, False, 70, i, False, Nothing, IIf(.TALLA06 <> "", True, False))
                    i = 7
                    PPCol2("TALLA07", dgStock, .TALLA07, "#,##0.00", True, 70, False, PresentationEnum.Normal, False, 70, i, False, Nothing, IIf(.TALLA07 <> "", True, False))
                    i = 8
                    PPCol2("TALLA08", dgStock, .TALLA08, "#,##0.00", True, 70, False, PresentationEnum.Normal, False, 70, i, False, Nothing, IIf(.TALLA08 <> "", True, False))
                    i = 9
                    PPCol2("TALLA09", dgStock, .TALLA09, "#,##0.00", True, 70, False, PresentationEnum.Normal, False, 70, i, False, Nothing, IIf(.TALLA09 <> "", True, False))
                    i = 10
                    PPCol2("TALLA10", dgStock, .TALLA10, "#,##0.00", True, 70, False, PresentationEnum.Normal, False, 70, i, False, Nothing, IIf(.TALLA10 <> "", True, False))
                End With
            End If
            Dim a As New ArrayList
            dgStock.Columns.Item("COLOR").ValueItems.Values.Clear()
            For i = 0 To registroActual.detalleColores.dvForm.Count - 1
                Dim v As C1.Win.C1TrueDBGrid.ValueItem
                v = New C1.Win.C1TrueDBGrid.ValueItem(registroActual.detalleColores.dvForm(i).Item("MODCOL"), "")
                If Not a.Contains(v.Value) Then
                    a.Add(v.Value)
                    dgStock.Columns.Item("COLOR").ValueItems.Values.Add(v)
                End If
            Next

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region
#Region "COLORES"

    Private Sub btnAñadirColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAñadirColor.Click
        Try
            If general.nz(txtColorAñadir.Text, "") <> "" Then
                registroActual.detalleColores.añadirColor(GENERAL.nz(txtColorAñadir.Text, ""))
            End If
            txtColorAñadir.Text = ""

        Catch ex As Exception
            LOG(ex.ToString) : CCN()
        End Try
    End Sub
    Private Sub btnBorrarColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBorrarColor.Click
        Try
            If general.nz(txtColorQuitar.Text, "") <> "" Then
                registroActual.detalleColores.QuitarColor(GENERAL.nz(txtColorQuitar.Text, ""))
            End If
            txtColorQuitar.Text = ""

        Catch ex As Exception
            LOG(ex.ToString) : CCN()
        End Try
    End Sub
    Private Sub IniciarDGColores()
        Dim i As Integer
        Try
            dgColores.SetDataBinding(registroActual.detalleColores.dvForm, "")
            OcultarColumnasDG(dgColores)

            i = 0
            PPCol2("MODCOL", dgColores, rm.GetString("COLORMODELO"), "", True, 150, False, PresentationEnum.Normal, False, 150, i, True, Nothing, False)
            dgColores.Splits(0).DisplayColumns(i).Merge = True

            i = i + 1
            PPCol2("TITULO", dgColores, rm.GetString("PIEZA"), "", True, 250, False, PresentationEnum.Normal, False, 250, i, True, Nothing, False)

            'i = i + 1
            'PPCol2("TEIXIT", dgColores, rm.GetString("TEJIDO"), "", True, 250, False, PresentationEnum.Normal, False, 250, i, True, Nothing, False)

            i = i + 1
            PPCol2("COLTITULO", dgColores, rm.GetString("COLORPIEZA"), "", True, 100, False, _
                    PresentationEnum.ComboBox, False, 100, i, False, cboColor)

            dgColores.Splits(0).AllowRowSizing = RowSizingEnum.None
            dgColores.AllowAddNew = False
            'dgColores.AllowUpdate = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub dgColores_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles dgColores.RowColChange
        Try
            If PuedoModificar() Then
                cargando = True
                With DirectCast(cboColor.DataSource, DataTable)
                    .DefaultView.RowFilter = "TEIXIT = '" & registroActual.detalleEscandallo.obtenerTejidoTitulo(registroActual.detalleColores.TITULO) & "'"
                End With
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub dgColores_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgColores.GotFocus
        Try
            If PuedoModificar() Then
                cargando = True
                With DirectCast(cboColor.DataSource, DataTable)
                    .DefaultView.RowFilter = "TEIXIT = '" & registroActual.detalleEscandallo.obtenerTejidoTitulo(registroActual.detalleColores.TITULO) & "'"
                End With
                AutoSizeCC(cboColor)
                cargando = False

            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub contextBorrar_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles contextBorrar.Popup
        Try
            If PuedoModificar() Then
                If dgColores.SelectedRows.Count > 0 Then
                    contextBorrar.MenuItems(0).Enabled = True
                Else
                    contextBorrar.MenuItems(0).Enabled = False
                End If
            Else
                contextBorrar.MenuItems(0).Enabled = False
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        Dim i As Integer
        Try
            If MessageBox.Show(rm.GetString("BORRARLINEA"), rm.GetString("ConfirmacionEliminacion"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
                For i = dgColores.SelectedRows.Count - 1 To 0 Step i - 1
                    dgColores.Bookmark = dgColores.SelectedRows(i)
                    dgColores.Delete()
                Next
                dgColores.SelectedRows.Clear()
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#End Region

    Private Sub tabControlModelos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabControlModelos.SelectedIndexChanged
        Try
            If tabControlModelos.SelectedTab Is tabPageColores Then
                registroActual.detalleColores.CargarColoresElegibles()
            End If

            If tabControlModelos.SelectedTab Is tabPageFichaModelo Then
                'Tenemos que cargar la foto que tiene el modelo
                CargarImagen(registroActual.obtenerImagen("imagen1"), imgModelo1)
                CargarImagen(registroActual.obtenerImagen("imagen2"), imgModelo2)

                'GenerarFichaModelo()
            End If

            If tabControlModelos.SelectedTab Is tabPageStock Then
                IniciarDGStock()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "IMPRESIÓN"

#End Region

#Region "COMUNES"

    Friend Sub btnVerLista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerLista.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmModelosLista = frmModelosLista.GetInstance(esListado)
            f.MdiParent = Me.MdiParent
            AddHandler f.Closed, AddressOf CType(Me.MdiParent, frmPrincipal).childCerrado
            AddHandler f.VisibleChanged, AddressOf CType(Me.MdiParent, frmPrincipal).childOcultandoMostrando
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
            cboID.AutoCompletion = b

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerModificables(Optional ByVal b As Boolean = False)
        Try
            PonerMod(b)
            If Not b Then : cboID.DataSource = Nothing
            Else : AñadirBindingCombo(cboID, registroActual.dvIdentificadores, CCModelo, CNModelo) : registroActual.tabla.AcceptChanges() : End If
            dgColores.AllowAddNew = False
            cboID.LimitToList = b
            cboID.SuperBack = b
            cboID.ReadOnly = editando
            ModiNuev(b)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub QuitarTabs()
        Try
            tabControlModelos.TabPages.Remove(tabPageEscandalloPrecio)
            tabControlModelos.TabPages.Remove(tabPageForniturasModelo)
            tabControlModelos.TabPages.Remove(tabPageColores)
            tabControlModelos.TabPages.Remove(tabPageFichaModelo)
            tabControlModelos.TabPages.Remove(tabPageStock)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerTabs()
        Try
            tabControlModelos.TabPages.Add(tabPageForniturasModelo)
            tabControlModelos.TabPages.Add(tabPageEscandalloPrecio)
            tabControlModelos.TabPages.Add(tabPageColores)
            tabControlModelos.TabPages.Add(tabPageStock)
            tabControlModelos.TabPages.Add(tabPageFichaModelo)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub NuevoRegistrConfimado()
        Try
            dgColores.Visible = True
            dgColorFicha.Visible = True
            dgConsumosFichaM.Visible = True
            dgEscandall.Visible = True
            dgForni.Visible = True
            dgStock.Visible = True
            dgTejidoFicha.Visible = True
            gbEleccionCliente.Visible = True
            gbEleccionTemporadas.Visible = True

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PreparaParaNuevoRegistro()
        Try
            dgColores.Visible = False
            dgColorFicha.Visible = False
            dgConsumosFichaM.Visible = False
            dgEscandall.Visible = False
            dgForni.Visible = False
            dgStock.Visible = False
            dgTejidoFicha.Visible = False
            gbEleccionCliente.Visible = False
            gbEleccionTemporadas.Visible = False

            QuitarTabs()
            PonerModificables(modificable)
            PonerControlesNuevo(False)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "DESPLAZARSE"

    Friend Overrides Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUltimo.Click
        Try
            cargando = True
            registroActual.UltimoReg() : AutoSizeCC(cboID) : ActualizarOrigen()
            RecalcularTotales()
            IniciarDGStock()
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Protected Friend Overrides Sub btnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnPrimero.Click
        Try
            cargando = True
            registroActual.PrimeroReg() : AutoSizeCC(cboID) : ActualizarOrigen()
            RecalcularTotales()
            IniciarDGStock()
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSiguiente.Click
        Try
            cargando = True
            registroActual.SiguienteReg() : AutoSizeCC(cboID) : ActualizarOrigen()
            RecalcularTotales()
            IniciarDGStock()
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAnterior.Click
        Try
            cargando = True
            registroActual.AnteriorReg() : AutoSizeCC(cboID) : ActualizarOrigen()
            RecalcularTotales()
            IniciarDGStock()
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "SELECCIÓN REGISTRO"

    Private Sub cboID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboID.KeyPress
        Try
            If editando Then
                cboID.AutoCompletion = False
            Else
                cboID.AutoCompletion = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub btnEscogerModelo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEscogerModelo.Click
        Try
            Dim f As frmModelosLista = frmModelosLista.GetInstance(esEleccion)
            f.ShowDialog()
            If Not f.dr Is Nothing Then CanviarModelo(f.dr("CODI"), f.dr("CLIENT"), f.dr("SERIE"), f.dr("TEMPORADA"))
            f = Nothing

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Function obtenercodigocliente(ByVal nombrecliente As String) As Integer
        Dim i As Integer
        Try
            Dim cmdSel As New MySqlCommand("SELECT codi FROM clients WHERE (NOM = """ & general.NS(nombrecliente) & """)", cnn)
            ACN()
            i = cmdSel.ExecuteScalar
            CCN()
            Return i

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    'Private Sub cboID_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboID.RowChange
    Private Sub cboID_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboID.RowChange
        Try
            If consulta() Then
                cargando = True
                cboID.DataBindings.Clear()
                registroActual.CambiarAReg(ObtenerFiladeColumnasC1(cboID.Columns, registroActual.dtIdentificadores), iraRegistroFila)
                AñadirBindingCombo(cboID, registroActual.dvIdentificadores, CCModelo, CCModelo)
                AñadirBinding(cboID, registroActual.dvForm, "CODI")
                RecalcularTotales()
                PSBTIPO(registroActual.centro) : cargando = False : CCN()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboSeleccionCliente_SelChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSeleccionCliente.SelectedValueChanged
        Try
            If consulta() Then
                cargando = True
                registroActual.ElegirCliente(nzn(cboSeleccionCliente.WillChangeToValue, 0))
                'registroActual.tabla.AcceptChanges()
                If registroActual.tabla.Rows.Count = 0 Then
                    MessageBox.Show(rm.GetString("CLIENTESINMODELOS"))
                End If
                PSBTIPO(registroActual.centro) : cargando = False
            End If
            'If consulta() Then
            '    cargando = True
            '    ElegirCliente(nzn(cboSeleccionCliente.WillChangeToValue, 0))
            '    lblNumeroMuestras.Text = rm.GetString("NUMCOMPLEMENTOS") & " " & muestraActual.dtIdentificadores.DefaultView.Count
            '    cargando = False
            'End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CanviarModelo(ByVal modeloabuscar As String, ByVal clientabuscar As Integer, ByVal serie As String, ByVal temporada As String)
        Dim idx As Integer
        Dim key(3) As Object
        Try
            cargando = True
            key(0) = temporada
            key(1) = clientabuscar
            key(2) = modeloabuscar
            key(3) = serie

            idx = registroActual.dvForm.Find(key)
            If (idx = -1) Then
                MessageBox.Show(rm.GetString("ELMODELO") & " " & cboID.Text & " " & rm.GetString("NOEXISTE"))
            Else
                BindingContext(registroActual.dvForm).Position = idx
            End If
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub rdoEscogerTemporada_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoEscogerTemporada.CheckedChanged
        Dim f As frmElegirTemporadaAMostrar
        Try
            If consulta() Then
                cargando = True
                If rdoEscogerTemporada.Checked = True Then
                    f = New frmElegirTemporadaAMostrar
                    f.ShowDialog()
                    registroActual.ElegirTemporadas(f.aTemporadas)
                End If
                PSBTIPO(registroActual.centro) : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub rdoEscogerTodasTemporadas_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoEscogerTodasTemporadas.CheckedChanged
        Try
            If consulta() Then
                cargando = True
                If rdoEscogerTodasTemporadas.Checked = True Then
                    registroActual.ElegirTemporadas(Nothing)
                End If
                PSBTIPO(registroActual.centro) : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub rdoVerLosModelosTodosLosClientes_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoVerLosModelosTodosLosClientes.CheckedChanged
        Try
            If consulta() Then
                cargando = True
                If rdoVerLosModelosTodosLosClientes.Checked = True Then
                    registroActual.ActualizarOrigen()
                    registroActual.ElegirCliente(-1)
                End If
                PSBTIPO(registroActual.centro) : cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub rdoVerModelosDeCliente_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoVerModelosDeCliente.CheckedChanged
        Try
            cargando = True
            If rdoVerModelosDeCliente.Checked = True Then
                registroActual.ActualizarOrigen()
                registroActual.CargarClientesConModelos()
                AñadirBindingCombo(cboSeleccionCliente, registroActual.dtClientesConModelos, "CODI", "NOM")
                cboSeleccionCliente.Visible = True
            Else
                cboSeleccionCliente.Visible = False
            End If
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#Region "SELECCION CENTRO"

#End Region

#End Region

#Region "ELECCIONES"
    Private Sub btnElegirEstampador_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirEstampador.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmTalleresLista = frmTalleresLista.GetInstance(esEleccion)
            f.ShowDialog()
            If Not f.dr Is Nothing Then registroActual.ESTAM = f.dr("CODI")
            Cursor = Cursors.Default
            f = Nothing
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnElegirTallerConfeccion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirTallerConfeccion.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmTalleresLista = frmTalleresLista.GetInstance(esEleccion)
            f.ShowDialog()
            If Not f.dr Is Nothing Then registroActual.CONFEC = f.dr("CODI")
            Cursor = Cursors.Default
            f = Nothing
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnElegirTejido_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirTejido.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmRepresentantesLista = frmRepresentantesLista.GetInstance(esEleccion)
            f.ShowDialog()
            If Not f.dr Is Nothing Then
                registroActual.TEIXIT = f.dr("CODI")
            End If
            Cursor = Cursors.Default
            f = Nothing

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnElegirProveedor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirProveedor.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmProveedoresLista = frmProveedoresLista.GetInstance(esEleccion)
            f.ShowDialog()
            If Not f.dr Is Nothing Then
                registroActual.PROVE = f.dr("CODI")
            End If
            Cursor = Cursors.Default
            f = Nothing
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnElegirTintador_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirTintador.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmTalleresLista = frmTalleresLista.GetInstance(esEleccion)
            f.ShowDialog()
            If Not f.dr Is Nothing Then
                registroActual.TINT = f.dr("CODI")
            End If
            Cursor = Cursors.Default
            f = Nothing
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnElegirTemporada_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New frmTransportistasLista
        Try
            f.ShowDialog()
            If Not f.dr Is Nothing Then
                registroActual.TEMPORADA = f.dr("CODI")
            End If
            f = Nothing

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnElegirAcabadorTejido_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirAcabador.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmTalleresLista = frmTalleresLista.GetInstance(esEleccion)
            f.ShowDialog()
            If Not f.dr Is Nothing Then
                registroActual.ACA = f.dr("CODI")
            End If
            Cursor = Cursors.Default
            f = Nothing

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnElegirCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnElegirCliente.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmClientesLista = frmClientesLista.GetInstance(esEleccion)
            f.ShowDialog()
            If Not f.dr Is Nothing Then
                registroActual.CLIENT = f.dr("CODI")
            End If
            Cursor = Cursors.Default
            f = Nothing
            PSBTIPO(registroActual.centro) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Private Sub cboTEMPORADA_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTEMPORADA.SelectedValueChanged
        Try
            If PuedoModificar() Then cargando = True : registroActual.TEMPORADA = general.nz(cboTEMPORADA.WillChangeToValue, "") : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboNOMTINT_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMTINT.SelectedValueChanged
        Try
            If PuedoModificar() Then cargando = True : registroActual.TINT = nzn(cboNOMTINT.WillChangeToValue, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboNOMPROVE_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMPROVE.SelectedValueChanged
        Try
            If PuedoModificar() Then cargando = True : registroActual.PROVE = nzn(cboNOMPROVE.WillChangeToValue, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboNOMACA_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMACA.SelectedValueChanged
        Try
            If PuedoModificar() Then cargando = True : registroActual.ACA = nzn(cboNOMACA.WillChangeToValue, 0) : cargando = False
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboNOMESTAM_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMESTAM.SelectedValueChanged
        Try
            If PuedoModificar() Then registroActual.ESTAM = nzn(cboNOMESTAM.WillChangeToValue, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboNOMCONFEC_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMCONFEC.SelectedValueChanged
        Try
            If PuedoModificar() Then cargando = True : registroActual.CONFEC = nzn(cboNOMCONFEC.WillChangeToValue, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboNOMCLIENT_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNOMCLIENT.SelectedValueChanged
        Try
            If PuedoModificar() Then cargando = True : registroActual.CLIENT = nzn(cboNOMCLIENT.WillChangeToValue, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub cboTEIXIT_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTEIXIT.SelectedValueChanged
        Try
            If PuedoModificar() Then cargando = True : registroActual.TEIXIT = general.nz(cboTEIXIT.WillChangeToValue, "") : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Private Sub txtCLIENT_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCLIENT.Validated
        Try
            If PuedoModificar() Then cargando = True : registroActual.CLIENT = nzn(txtCLIENT.Text, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtCodigoProveedor_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPROVE.Validated
        Try
            If PuedoModificar() Then cargando = True : registroActual.PROVE = nzn(txtPROVE.Text, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtTINT_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTINT.Validated
        Try
            If PuedoModificar() Then cargando = True : registroActual.TINT = nzn(txtTINT.Text, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtACA_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtACA.Validated
        Try
            If PuedoModificar() Then cargando = True : registroActual.ACA = nzn(txtACA.Text, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtESTAM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtESTAM.Validated
        Try
            If PuedoModificar() Then cargando = True : registroActual.ESTAM = nzn(txtESTAM.Text, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub txtCONFEC_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCONFEC.Validated
        Try
            If PuedoModificar() Then cargando = True : registroActual.CONFEC = nzn(txtCONFEC.Text, 0) : cargando = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "SELECCION CENTRO"

    Private Sub cboSeleccion_ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSeleccionCentro.SelectionChangeCommitted
        Try
            If consulta() Then
                cargando = True
                registroActual.cambioCentro(GENERAL.nz(cboSeleccionCentro.SelectedValue, empresaPorDefecto), primero)
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
            AutoSizeCC(cboID)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

    Private Sub IniciarDGEscandalloC()
        Try
            OcultarColumnasDG(dgEscandalloResumen)
            PPCol2("TITULO", dgEscandalloResumen, rm.GetString("PIEZA"), "", True, 100, False, PresentationEnum.Normal, False, 100, 0, False, Nothing, False)
            PPCol2("TEIXIT", dgEscandalloResumen, rm.GetString("TEJIDO"), "", True, 100, False, PresentationEnum.ComboBox, False, 100, 1, False, Nothing, False)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try

    End Sub

    Private Sub dgColores_BeforeUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgColores.AfterColUpdate
        Try
            If PuedoModificar() Then
                cargando = True
                dgColores.UpdateData()
                Select Case e.Column.DataColumn.DataField
                    Case "COLTITULO"
                        registroActual.detalleColores.dtColoresDeTejidos.DefaultView.Sort = "COLOR"
                        If registroActual.detalleColores.dtColoresDeTejidos.DefaultView.Find(GENERAL.nz(e.Column.DataColumn.Value, "")) < 0 Then
                            MessageBox.Show("Aquest color no existeix en el teixit seleccionat. Seleccioni un de la llista", "Informació")
                            e.Column.DataColumn.Value = ""
                            dgColores.UpdateData()
                        End If
                End Select
                cargando = False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub



    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class