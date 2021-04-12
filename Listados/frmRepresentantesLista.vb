Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class frmRepresentantesLista
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
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmRepresentantesLista))
        '
        'btnTancar
        '
        Me.btnTancar.Name = "btnTancar"
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.sbtipo.Location = CType(resources.GetObject("sbtipo.Location"), System.Drawing.Point)
        Me.sbtipo.Name = "sbtipo"
        '
        'btnRecargar
        '
        Me.btnRecargar.Name = "btnRecargar"
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
        'btnModificar
        '
        Me.btnModificar.Name = "btnModificar"
        '
        'btnBorrar
        '
        Me.btnBorrar.Name = "btnBorrar"
        '
        'btnNuevo
        '
        Me.btnNuevo.Name = "btnNuevo"
        '
        'btnVerLista
        '
        Me.btnVerLista.Name = "btnVerLista"
        '
        'btnActualizar
        '
        Me.btnActualizar.Name = "btnActualizar"
        '
        'frmRepresentantesLista
        '
        Me.AccessibleDescription = resources.GetString("$this.AccessibleDescription")
        Me.AccessibleName = resources.GetString("$this.AccessibleName")
        Me.AutoScaleBaseSize = CType(resources.GetObject("$this.AutoScaleBaseSize"), System.Drawing.Size)
        Me.AutoScroll = CType(resources.GetObject("$this.AutoScroll"), Boolean)
        Me.AutoScrollMargin = CType(resources.GetObject("$this.AutoScrollMargin"), System.Drawing.Size)
        Me.AutoScrollMinSize = CType(resources.GetObject("$this.AutoScrollMinSize"), System.Drawing.Size)
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = CType(resources.GetObject("$this.ClientSize"), System.Drawing.Size)
        Me.Enabled = CType(resources.GetObject("$this.Enabled"), Boolean)
        Me.Font = CType(resources.GetObject("$this.Font"), System.Drawing.Font)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.ImeMode = CType(resources.GetObject("$this.ImeMode"), System.Windows.Forms.ImeMode)
        Me.Location = CType(resources.GetObject("$this.Location"), System.Drawing.Point)
        Me.MaximumSize = CType(resources.GetObject("$this.MaximumSize"), System.Drawing.Size)
        Me.MinimumSize = CType(resources.GetObject("$this.MinimumSize"), System.Drawing.Size)
        Me.Name = "frmRepresentantesLista"
        Me.RightToLeft = CType(resources.GetObject("$this.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.StartPosition = CType(resources.GetObject("$this.StartPosition"), System.Windows.Forms.FormStartPosition)
        Me.Text = resources.GetString("$this.Text")

    End Sub

#End Region

    Shared frmChildForm As frmRepresentantesLista
    Public Shared Function GetInstance(ByVal esEleccionLista As Short) As frmRepresentantesLista
        If frmChildForm Is Nothing Then
            frmChildForm = New frmRepresentantesLista
            frmChildForm.EsEleccionOListado = esEleccionLista
        Else
            If Not frmChildForm.EsEleccionOListado = esEleccionLista Then
                frmChildForm = New frmRepresentantesLista
                frmChildForm.EsEleccionOListado = esEleccionLista
            End If
        End If
        Return frmChildForm
    End Function

    Friend Overrides Sub HacerArraysCampoNombre()
        Try
            Dim a As New clsArrays
            If EsEleccionOListado = esListado Then
                ArrayCampos = a.ObtenerArrayCamposRepresentantes
            Else
                With ArrayCampos
                    a.aCODIGO(3) = 1
                    .Add(a.aCODIGO(0), a.aCODIGO)

                    a.aNOM(3) = 2
                    .Add(a.aNOM(0), a.aNOM)
                End With
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub CrearComandos()
        Try
            If EsEleccionOListado = esEleccion Then
                cmdSelect.CommandText = "SELECT " & tabla & ".CODI, " & tabla & ".NOM, " & _
                                            " filiales.DESCRI AS NOMCENTRO " & _
                                            " FROM " & tabla & " " & _
                                            " LEFT JOIN filiales ON (filiales.CODI = " & tabla & ".CENTRO) "
            Else
                cmdSelect.CommandText = "SELECT " & tabla & ".*, " & _
                                            " filiales.DESCRI AS NOMCENTRO " & _
                                            " FROM " & tabla & " " & _
                                            " LEFT JOIN filiales ON (filiales.CODI = " & tabla & ".CENTRO) "
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            tabla = tablaRepresentantes
            textoListado = rm.GetString("LREPRESENTANTES")
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
            Dim f As frmRepresentantesForm = frmRepresentantesForm.GetInstance()
            f.MdiParent = Me.MdiParent
            AddHandler f.Closed, AddressOf CType(Me.MdiParent, frmPrincipal).childCerrado
            AddHandler f.Load, AddressOf CType(Me.MdiParent, frmPrincipal).childAbierto : AddHandler f.Activated, AddressOf CType(Me.MdiParent, frmPrincipal).childOcultandoMostrando
            f.Show()
            f.BringToFront()
            Cursor = Cursors.Default
            Close()

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
                Dim f As frmRepresentantesForm = frmRepresentantesForm.GetInstance()
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

                f.registroActual.CambiarAReg(dg.Item(dg.Row, "CODI"), iraregistro)
                f.cargando = False
                cargando = False
                Me.Close()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub

#Region "OVERRIDES"

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
    Friend Overrides Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Friend Overrides Sub btnUltimo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

#End Region

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        dr = Nothing
        Me.Close()
        frmChildForm = Nothing
    End Sub

End Class



