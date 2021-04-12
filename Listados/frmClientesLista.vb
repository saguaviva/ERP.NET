Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class frmClientesLista
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClientesLista))
        Me.SuspendLayout()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.System
        resources.ApplyResources(Me.sbtipo, "sbtipo")
        '
        'btnRecargar
        '
        resources.ApplyResources(Me.btnRecargar, "btnRecargar")
        '
        'btnSiguiente
        '
        resources.ApplyResources(Me.btnSiguiente, "btnSiguiente")
        '
        'btnAnterior
        '
        resources.ApplyResources(Me.btnAnterior, "btnAnterior")
        '
        'btnPrimero
        '
        resources.ApplyResources(Me.btnPrimero, "btnPrimero")
        '
        'btnUltimo
        '
        resources.ApplyResources(Me.btnUltimo, "btnUltimo")
        '
        'btnModificar
        '
        resources.ApplyResources(Me.btnModificar, "btnModificar")
        '
        'btnTancar
        '
        resources.ApplyResources(Me.btnTancar, "btnTancar")
        '
        'btnBorrar
        '
        resources.ApplyResources(Me.btnBorrar, "btnBorrar")
        '
        'btnNuevo
        '
        resources.ApplyResources(Me.btnNuevo, "btnNuevo")
        '
        'btnActualizar
        '
        resources.ApplyResources(Me.btnActualizar, "btnActualizar")
        '
        'btnVerLista
        '
        resources.ApplyResources(Me.btnVerLista, "btnVerLista")
        '
        'cboSeleccionCentro
        '
        resources.ApplyResources(Me.cboSeleccionCentro, "cboSeleccionCentro")
        '
        'frmClientesLista
        '
        resources.ApplyResources(Me, "$this")
        Me.Name = "frmClientesLista"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmClientesLista
    Public Shared Function GetInstance(ByVal esEleccionLista As Short) As frmClientesLista
        If frmChildForm Is Nothing Then
            frmChildForm = New frmClientesLista
            frmChildForm.EsEleccionOListado = esEleccionLista
        Else
            If Not frmChildForm.EsEleccionOListado = esEleccionLista Then
                frmChildForm = New frmClientesLista
                frmChildForm.EsEleccionOListado = esEleccionLista
            End If
        End If
        Return frmChildForm
    End Function

    Friend Overrides Sub HacerArraysCampoNombre()
        Try
            Dim a As New clsArrays
            If EsEleccionOListado = esListado Then
                ArrayCampos = a.ObtenerArrayCamposClientes
            Else
                With ArrayCampos
                    a.aCODIGO(3) = 1
                    .Add(a.aCODIGO(0), a.aCODIGO)
                    a.aNOM(3) = 2
                    .Add(a.aNOM(0), a.aNOM)
                    a.aNOMCENTRO(3) = 3
                    .Add(a.aNOMCENTRO(0), a.aNOMCENTRO)
                End With
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Friend Overrides Sub CrearComandos()
        Try
            If EsEleccionOListado = esEleccion Then
                cmdSelect.CommandText = "SELECT clients.CODI, clients.NOM, " &
                                " filiales.DESCRI AS NOMCENTRO, " &
                                " filiales.CENTRO " &
                                " FROM " &
                                " clients LEFT JOIN paises ON (paises.CODI = clients.PAIS) " &
                                " LEFT JOIN filiales ON (filiales.CODI = clients.CENTRO) " &
                                " WHERE clients.BLOQUEADO <> 1 " &
                                " ORDER BY clients.CODI"
            Else
                cmdSelect.CommandText = "SELECT clients.*, " & _
                                " filiales.DESCRI AS NOMCENTRO, " & _
                                " filiales.CENTRO, " & _
                                " paises.NOM AS NOMPAIS, " & _
                                " bancs.DESCRIPCIO AS NOMBANC, " & _
                                " repres.NOM AS NOMREPRES, " & _
                                " forpag.DESCRIPCIO AS NOMFORMA, " & _
                                " iva.DESCRIPCIO AS NOMIVA, " & _
                                " trans.NOM AS NOMTRANS " & _
                                " FROM " & _
                                " clients LEFT JOIN paises ON (paises.CODI = clients.PAIS) " & _
                                " LEFT JOIN bancs ON (bancs.CODI = clients.BANC) " & _
                                " LEFT JOIN repres ON (repres.CODI = clients.REPRES) " & _
                                " LEFT JOIN forpag ON (forpag.CODI = clients.FORMA) " & _
                                " LEFT JOIN iva ON (iva.CODI = clients.IVA) " & _
                                " LEFT JOIN filiales ON (filiales.CODI = clients.CENTRO) " & _
                                " LEFT JOIN trans ON (trans.CODI = clients.TRANS) " & _
                                " ORDER BY clients.CODI"
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            tabla = tablaClientes
            textoListado = rm.GetString("LCLIENTES")
            IniciarListado()
            dv.Sort = "CODI"

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub

    Friend Overrides Sub btnFormulario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Cursor = Cursors.WaitCursor
            Dim f As frmClientesForm = frmClientesForm.GetInstance()
            f.MdiParent = Me.MdiParent
            AddHandler f.Closed, AddressOf CType(Me.MdiParent, frmPrincipal).childCerrado
            AddHandler f.Load, AddressOf CType(Me.MdiParent, frmPrincipal).childAbierto : AddHandler f.Activated, AddressOf CType(Me.MdiParent, frmPrincipal).childOcultandoMostrando
            f.Show()
            f.BringToFront()
            Cursor = Cursors.Default

            Me.Close()

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
                Dim f As frmClientesForm = frmClientesForm.GetInstance()
                f.MdiParent = Me.MdiParent
                AddHandler f.Closed, AddressOf CType(Me.MdiParent, frmPrincipal).childCerrado
                AddHandler f.Load, AddressOf CType(Me.MdiParent, frmPrincipal).childAbierto : AddHandler f.Activated, AddressOf CType(Me.MdiParent, frmPrincipal).childOcultandoMostrando
                Me.Visible = False
                f.cargando = True
                f.Show()
                f.BringToFront()
                Cursor = Cursors.Default
                cargando = True
                If f.registroActual.centro <> dg.Item(dg.Row, "CENTRO") Then
                    f.registroActual.centro = dg.Item(dg.Row, "CENTRO")
                    f.registroActual.cambioCentro(GENERAL.nz(dg.Item(dg.Row, "CENTRO"), empresaPorDefecto), primero)
                    f.cboSeleccionCentro.SelectedValue = f.registroActual.centro
                    'f.registroActual.tabla.AcceptChanges()
                End If
                f.registroActual.PrimeroReg()
                f.registroActual.CambiarAReg(dg.Item(dg.Row, "CODI"), iraregistro)
                f.cargando = False
                cargando = False
                Me.Close()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        dr = Nothing
        Me.Close()
        frmChildForm = Nothing
    End Sub

End Class


