Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class frmModelosLista
    Inherits frmPlantillaLista

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.sbtipo.Location = New System.Drawing.Point(5, 591)
        Me.sbtipo.Name = "sbtipo"
        Me.sbtipo.Size = New System.Drawing.Size(88, 15)
        '
        'btnRecargar
        '
        Me.btnRecargar.Location = New System.Drawing.Point(60, 112)
        Me.btnRecargar.Name = "btnRecargar"
        Me.btnRecargar.Size = New System.Drawing.Size(88, 19)
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(308, 131)
        Me.btnSiguiente.Name = "btnSiguiente"
        Me.btnSiguiente.Size = New System.Drawing.Size(32, 18)
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(28, 131)
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.Size = New System.Drawing.Size(32, 18)
        '
        'btnPrimero
        '
        Me.btnPrimero.Location = New System.Drawing.Point(28, 112)
        Me.btnPrimero.Name = "btnPrimero"
        Me.btnPrimero.Size = New System.Drawing.Size(32, 19)
        '
        'btnUltimo
        '
        Me.btnUltimo.Location = New System.Drawing.Point(308, 112)
        Me.btnUltimo.Name = "btnUltimo"
        Me.btnUltimo.Size = New System.Drawing.Size(32, 19)
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(148, 131)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New System.Drawing.Size(80, 18)
        '
        'btnTancar
        '
        Me.btnTancar.Location = New System.Drawing.Point(594, 131)
        Me.btnTancar.Name = "btnTancar"
        Me.btnTancar.Size = New System.Drawing.Size(72, 18)
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(228, 131)
        Me.btnBorrar.Name = "btnBorrar"
        Me.btnBorrar.Size = New System.Drawing.Size(80, 18)
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(148, 112)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(161, 19)
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(60, 131)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(88, 18)
        '
        'btnVerLista
        '
        Me.btnVerLista.Location = New System.Drawing.Point(594, 112)
        Me.btnVerLista.Name = "btnVerLista"
        Me.btnVerLista.Size = New System.Drawing.Size(72, 19)
        '
        'cboSeleccionCentro
        '
        Me.cboSeleccionCentro.Location = New System.Drawing.Point(280, 6)
        Me.cboSeleccionCentro.Name = "cboSeleccionCentro"
        '
        'frmModelosLista
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(922, 571)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmModelosLista"
        Me.Text = "Llistat Models"

    End Sub

#End Region

    Shared frmChildForm As frmModelosLista
    Public Shared Function GetInstance(ByVal esEleccionLista As Short) As frmModelosLista
        If frmChildForm Is Nothing Then
            frmChildForm = New frmModelosLista
            frmChildForm.EsEleccionOListado = esEleccionLista
        Else
            If Not frmChildForm.EsEleccionOListado = esEleccionLista Then
                frmChildForm = New frmModelosLista
                frmChildForm.EsEleccionOListado = esEleccionLista
            End If
        End If
        Return frmChildForm
    End Function

#Region "VARIABLES"

    Private dtClients As New DataTable
    Private dtTalleres As New DataTable
    Public clienteElegido As Integer = -1
    Public modeloElegido As String = ""
    Public temporadaElegida As String = ""
    Public serieElegida As String = ""
    Public centroAElegir As String = ""


#End Region

    Friend Sub PonerNombres()
        Dim i As Integer
        Try
            For i = 0 To dv.Count - 1
                dv(i).Item("NOMCLIENT") = general.OBN(dv(i).Item("CLIENT"), dtClients)
            Next
            For i = 0 To dv.Count - 1
                dv(i).Item("NOMCONFEC") = general.OBN(dv(i).Item("CONFEC"), dtTalleres)
            Next

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Friend Overrides Sub HacerArraysCampoNombre()
        Dim a As New clsArrays
        Try
            With ArrayCampos

                a.aDescri(3) = 1
                .Add("CODI", a.aCODIGO)

                a.aDescri(3) = 2
                .Add("DESCRI", a.aDescri)

                a.aTEMPORADA(3) = 3
                .Add("TEMPORADA", a.aTEMPORADA)

                a.aSerie(3) = 4
                .Add("SERIE", a.aSerie)

                a.aNOMCLIENT(3) = 5
                .Add("NOMCLIENT", a.aNOMCLIENT)

                If EsEleccionOListado = esListado Then
                    a.aNOMTEIXIDOR(3) = 6
                    .Add("NOMTEIXIDOR", a.aNOMTEIXIDOR)

                    Dim aPreu(3) As String
                    aPreu(0) = "VENDA"
                    aPreu(1) = "Preu Venda"
                    aPreu(2) = "double"
                    aPreu(3) = 7
                    .Add("VENDA", aPreu)

                    Dim aVENDAFINAL(3) As String
                    aVENDAFINAL(0) = "VENDAFINAL"
                    aVENDAFINAL(1) = "Preu Venda Final"
                    aVENDAFINAL(2) = "double"
                    aVENDAFINAL(3) = 8
                    .Add("VENDAFINAL", aVENDAFINAL)

                    Dim aCost(3) As String
                    aCost(0) = "COST"
                    aCost(1) = rm.GetString("COSTE")
                    aCost(2) = "double"
                    aCost(3) = 9
                    .Add("COST", aCost)
                End If

            End With

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub CrearComandos()
        Try
            If EsEleccionOListado = esEleccion Then
                cmdSelect.CommandText = "SELECT " & tabla & ".*, " & _
                                     " FILIALES.DESCRI AS NOMCENTRO " & _
                                     " FROM " & tabla & " " & _
                                     " LEFT JOIN FILIALES ON (FILIALES.CODI = " & tabla & ".CENTRO)" & _
                                     " WHERE " & _
                                     " models.CENTRO = """ & centroAElegir & """ " & _
                                     " " & IIf(clienteElegido <> -1, " AND MODELS.CLIENT = """ & clienteElegido & """ ", "") & " " & _
                                     " ORDER BY models.TEMPORADA, models.SERIE, models.CLIENT, models.CODI "
            Else
                cmdSelect.CommandText = "SELECT " & tabla & ".*, " & _
                                     " FILIALES.DESCRI AS NOMCENTRO " & _
                                     " FROM " & tabla & " " & _
                                     " LEFT JOIN FILIALES ON (FILIALES.CODI = " & tabla & ".CENTRO)" & _
                                     " ORDER BY models.TEMPORADA, models.SERIE, models.CLIENT, models.CODI"
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            tabla = tablaModelos
            textoListado = rm.GetString("LMODELOS")
            IniciarListado()
            CargaTabla(tablaTalleres, CCTallers, CNTallers, dtTalleres)
            CargaTabla(tablaClientes, CCClients, CNClients, dtClients)
            PonerNombres()
            AutoSizeCC(dg)
            dv.Sort = "CODI, CLIENT, TEMPORADA, SERIE"

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
    Friend Overrides Sub btnFormulario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmModelos = frmModelos.GetInstance()
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
    Friend Overrides Sub dg_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Cursor = Cursors.WaitCursor
            If EsEleccionOListado = esEleccion Then
                dr = dg.Item(dg.Row).Row
                Me.Close()
                frmChildForm = Nothing
            Else
                Dim f As frmModelos = frmModelos.GetInstance()
                f.MdiParent = Me.MdiParent
                AddHandler f.Closed, AddressOf CType(Me.MdiParent, frmPrincipal).childCerrado
                AddHandler f.Load, AddressOf CType(Me.MdiParent, frmPrincipal).childAbierto : AddHandler f.Activated, AddressOf CType(Me.MdiParent, frmPrincipal).childOcultandoMostrando
                Me.Visible = False
                f.Show()
                f.BringToFront()

                cargando = True
                f.cargando = True
                If f.registroActual.centro <> dg.Item(dg.Row, "CENTRO") Then
                    f.registroActual.centro = dg.Item(dg.Row, "CENTRO")
                    f.registroActual.cambioCentro(GENERAL.nz(dg.Item(dg.Row, "CENTRO"), empresaPorDefecto), primero)
                    f.cboSeleccionCentro.SelectedValue = f.registroActual.centro
                    'f.proveActual.tabla.AcceptChanges()
                End If
                f.registroActual.CambiarAReg(dg.Item(dg.Row).Row, iraRegistroFila)
                cargando = False
                Cursor = Cursors.Default
                f.cargando = False
                Me.Close()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        dr = Nothing
        Me.Close()
        frmChildForm = Nothing
    End Sub

End Class