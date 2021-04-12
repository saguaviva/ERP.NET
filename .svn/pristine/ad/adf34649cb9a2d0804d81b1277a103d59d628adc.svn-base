Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class frmControlStockTejidos
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
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage

    Friend WithEvents C1TrueDBGrid1 As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmControlStockTejidos))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.C1TrueDBGrid1 = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.C1TrueDBGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'TabControl1
        '
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.C1TrueDBGrid1)
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Name = "TabPage1"
        '
        'C1TrueDBGrid1
        '
        resources.ApplyResources(Me.C1TrueDBGrid1, "C1TrueDBGrid1")
        Me.C1TrueDBGrid1.Images.Add(CType(resources.GetObject("C1TrueDBGrid1.Images"), System.Drawing.Image))
        Me.C1TrueDBGrid1.Name = "C1TrueDBGrid1"
        Me.C1TrueDBGrid1.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.C1TrueDBGrid1.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.C1TrueDBGrid1.PreviewInfo.ZoomFactor = 75
        Me.C1TrueDBGrid1.PrintInfo.PageSettings = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.C1TrueDBGrid1.PropBag = resources.GetString("C1TrueDBGrid1.PropBag")
        '
        'frmControlStockTejidos
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.TabControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "frmControlStockTejidos"
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
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.C1TrueDBGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared frmChildForm As frmControlStockTejidos

    Public Shared Function GetInstance() As frmControlStockTejidos
        If frmChildForm Is Nothing Then
            frmChildForm = New frmControlStockTejidos

        End If
        Return frmChildForm
    End Function

    Private colMETROS As Short = 6
    Private colDIF As Short = 7
    Private dt As DataTable = New DataTable("stockTejidos")
    Private dv As DataView
    Private colCODIGO As Short = 0
    Private colPROVE As Short = 1
    Private colCOLOR As Short = 2
    Private colACTUAL As Short = 3
    Private colMINIMO As Short = 4
    Private colKG As Short = 5

    Private da As New MySql.Data.MySqlClient.MySqlDataAdapter
    Private cmdSelect As New MySql.Data.MySqlClient.MySqlCommand

    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Cursor = Cursors.WaitCursor

            cmdSelect.Connection = cnn

            da.SelectCommand = cmdSelect
            CargarHilos()

            CrearTabla()
            'HacerBinding
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub CargarHilos()
        Try
            'CODI, DESCRI, PREU, PROVE, COST, OBSERV, IVA
            'TIPUS, FIL, PROVE, COLOR, ACTUALM MINIM, PREU, TINTAR, METRES, KG, OBSERV
            cmdSelect.CommandText = "SELECT teixits.codi AS Codi, " & _
                                        " filcol.color, " & _
                                        " filcol.actual, " & _
                                        " filcol.minim, " & _
                                        " filiales.DESCRI AS Empresa " & _
                                        " FROM teixits " & _
                                        " LEFT JOIN filcol ON (teixits.CODI = filcol.FIL AND teixits.CENTRO = filcol.CENTRO) " & _
                                        " LEFT JOIN filiales ON (filiales.CODI = filcol.CENTRO) " & _
                                        " WHERE filcol.TIPUS = ""T"" ORDER BY teixits.codi, filcol.color"
            '" LEFT JOIN filiales ON (filiales.CODI = filcol.CENTRO) " & _


            da.Fill(dt)


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub C1TrueDBGrid1_FetchCellStyle(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.FetchCellStyleEventArgs) Handles C1TrueDBGrid1.FetchCellStyle

        Select Case e.Col
            Case colDIF
                If CDbl(C1TrueDBGrid1.Columns(colDIF).CellText(e.Row)) < 0 Then
                    e.CellStyle.ForeColor = Color.Red
                End If

            Case Else

                Debug.WriteLine("FetchCellStyle not handled: " & e.Col)

        End Select

    End Sub

    Private Sub CrearTabla()
        ' Dim dcHILO As New DataColumn("FIL", System.Type.GetType("string"))
        ' Dim dcCOLOR As New DataColumn("COLOR", System.Type.GetType("string"))
        Dim dcDIF As New DataColumn("DIFERENCIA")
        Dim i As Integer
        Dim j As Double
        Try
            dcDIF.DataType = j.GetType


            dt.Columns.Add(dcDIF)
            dv = dt.DefaultView
            For i = 0 To dv.Count - 1
                dv(i).Item("DIFERENCIA") = nzn(dv(i).Item("ACTUAL"), 0) - nzn(dv(i).Item("MINIM"), 0)

            Next
            C1TrueDBGrid1.SetDataBinding(dv, "")
            C1TrueDBGrid1.Splits(0).DisplayColumns("DIFERENCIA").FetchStyle = True
            C1TrueDBGrid1.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.GroupBy
            'Desplaça aquí una capçalera de columna per agrupar per aquesta columna
            For i = 0 To C1TrueDBGrid1.Splits(0).DisplayColumns.Count - 1
                C1TrueDBGrid1.Splits(0).DisplayColumns(i).AutoSize()

            Next
            C1TrueDBGrid1.GroupByAreaVisible = True


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTancar.Click
        Close()
    End Sub

    Friend Overrides Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

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

    Friend Overrides Sub btnUltimo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
End Class

'CODI, DESCRI, PREU, PROVE, COST, OBSERV, IVA
'TIPUS, FIL, PROVE, COLOR, ACTUALM MINIM, PREU, TINTAR, METRES, KG, OBSERV

