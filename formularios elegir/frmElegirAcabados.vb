Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Public Class frmElegirAcabados
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
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents btnAceptar As C1.Win.C1Input.C1Button
    Friend WithEvents C1Report1 As C1.C1Report.C1Report
    Friend WithEvents C1PreviewPane1 As C1.Win.C1Preview.C1PreviewPane
    Friend WithEvents lstAcabados As C1.Win.C1List.C1List
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmElegirAcabados))
        Me.btnAceptar = New C1.Win.C1Input.C1Button()
        Me.lstAcabados = New C1.Win.C1List.C1List()
        Me.C1Report1 = New C1.C1Report.C1Report()
        Me.C1PreviewPane1 = New C1.Win.C1Preview.C1PreviewPane()
        CType(Me.lstAcabados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1PreviewPane1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
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
        'btnAceptar
        '
        resources.ApplyResources(Me.btnAceptar, "btnAceptar")
        Me.btnAceptar.Name = "btnAceptar"
        '
        'lstAcabados
        '
        Me.lstAcabados.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        resources.ApplyResources(Me.lstAcabados, "lstAcabados")
        Me.lstAcabados.CaptionHeight = 17
        Me.lstAcabados.ColumnCaptionHeight = 17
        Me.lstAcabados.ColumnFooterHeight = 17
        Me.lstAcabados.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark
        Me.lstAcabados.Images.Add(CType(resources.GetObject("lstAcabados.Images"), System.Drawing.Image))
        Me.lstAcabados.ItemHeight = 15
        Me.lstAcabados.MatchEntryTimeout = CType(2000, Long)
        Me.lstAcabados.Name = "lstAcabados"
        Me.lstAcabados.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.lstAcabados.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.lstAcabados.PropBag = resources.GetString("lstAcabados.PropBag")
        '
        'C1Report1
        '
        Me.C1Report1.ReportDefinition = resources.GetString("C1Report1.ReportDefinition")
        Me.C1Report1.ReportName = "C1Report1"
        '
        'C1PreviewPane1
        '
        resources.ApplyResources(Me.C1PreviewPane1, "C1PreviewPane1")
        Me.C1PreviewPane1.Name = "C1PreviewPane1"
        '
        'frmElegirAcabados
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.C1PreviewPane1)
        Me.Controls.Add(Me.lstAcabados)
        Me.Controls.Add(Me.btnAceptar)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "frmElegirAcabados"
        Me.Controls.SetChildIndex(Me.cboSeleccionCentro, 0)
        Me.Controls.SetChildIndex(Me.btnAceptar, 0)
        Me.Controls.SetChildIndex(Me.lstAcabados, 0)
        Me.Controls.SetChildIndex(Me.C1PreviewPane1, 0)
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
        CType(Me.lstAcabados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1PreviewPane1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "VARIABLES"

    Friend codigo As String
    Friend nombre As String
    Friend aAcabados As New ArrayList
    Friend codigoProveedor As String = -1
    Friend precioCoste As String
    Friend dt As New DataTable

    Friend preu As Double
    Friend client As Integer
    Friend temporada As String
    Friend serie As String
    Friend preumodel As Double
    Friend mostrarPrecioModelo As Boolean = False
    Friend centro As Char = "C"
    Friend tejido As String

#End Region

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'CargarFormularioElegir(tablaAcabados.ToLower, "CODI", "CODI")

            lstAcabados.AllowRowSizing = C1.Win.C1List.RowSizingEnum.None
            lstAcabados.AllowVerticalSplit = False
            lstAcabados.AllowHorizontalSplit = False
            CargaAcabados(tablaAcabados.ToLower)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CargaAcabados(ByVal tabla As String)
        Dim cmdSelectReader As New MySqlCommand
        Dim dcCODIGO As New DataColumn
        Dim dcPROVEEDOR As New DataColumn
        Dim dcCODIGOPROVEEDOR As New DataColumn
        Dim dcK As New DataColumn
        Dim dcM As New DataColumn
        Dim DATAREADER As MySqlDataReader
        Dim dr As DataRow
        Dim i As Integer
        Dim strSQL As String
        Try
            dt.TableName = tabla
            'dg.AllowRowSelect = True
            cmdSelectReader.Connection = cnn


            strSQL = "SELECT CODI, PROVE, PREUK, PREUM FROM " & tablaAcabados & " " & _
                    " WHERE PROVE = " & client & " "

            cmdSelectReader.CommandText = strSQL


            dcCODIGO.ColumnName = "CODI"
            dcCODIGOPROVEEDOR.ColumnName = tablaProveedores
            dcK.ColumnName = "PREUK"
            dcM.ColumnName = "PREUM"

            dt.Columns.Add(dcCODIGO)
            dt.Columns.Add(dcCODIGOPROVEEDOR)
            dt.Columns.Add(dcK)
            dt.Columns.Add(dcM)
            CCN()
            ACN()
            DATAREADER = cmdSelectReader.ExecuteReader()
            lstAcabados.DataMode = C1.Win.C1List.DataModeEnum.AddItem
            While DATAREADER.Read

                If DATAREADER.FieldCount = 0 Then
                    MessageBox.Show("No hi ha cap del acabat de : " & codigoProveedor)
                End If

                'dr.Item("CODI") = DATAREADER.Item("CODI")
                lstAcabados.AddItem(DATAREADER.Item("CODI"))
                'dr.Item(tablaProveedores) = DATAREADER.Item(tablaProveedores)
                'dr.Item("PREUK") = DATAREADER.Item("PREUK")
                'dr.Item("PREUM") = DATAREADER.Item("PREUM")

            End While
            DATAREADER.Close()
            cmdSelectReader.CommandText = "SELECT ACABAT FROM ACABATSTEIXITS WHERE TEIXIT = """ & tejido & """ AND PROVE = " & client & " "
            DATAREADER = cmdSelectReader.ExecuteReader()
            While DATAREADER.Read
                For i = 0 To lstAcabados.ListCount - 1
                    If lstAcabados.Columns(0).Value = DATAREADER("ACABAT") Then
                        lstAcabados.SetSelected(i, True)
                    End If
                Next
            End While
            DATAREADER.Close()
            CCN()
            lstAcabados.SelectionMode = C1.Win.C1List.SelectionModeEnum.CheckBox
            lstAcabados.Splits(0).DisplayColumns(0).AutoSize()
            lstAcabados.Columns(0).Caption = rm.GetString("ACABADOR")
            lstAcabados.Splits(0).AllowVerticalSizing = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim i As Integer
        For i = 0 To lstAcabados.SelectedIndices.Count - 1
            ' if aAcabados.Contains(
            ' aAcabados.Add(lstAcabados.DataSource.rows(lstAcabados.SelectedIndices(i)).item(0)) ' .lstAcabados.SelectedIndices(i)))
        Next
        Me.Close()
    End Sub
    Private Sub lstAcabados_SelChange(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles lstAcabados.SelChange
        Try
            aAcabados.Add(lstAcabados.WillChangeToText)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#Region "DESPLAZAMIENTO"

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


End Class
