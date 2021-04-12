Imports MySql.Data.MySqlClient
Imports clsFuncionesUtiles
Imports clsFuncionesLOG
Imports clsFuncionesC1

Public Class DuplicarComplementos

    Shared frmDuplicarComplemetnos As DuplicarComplementos
    Dim dt As New DataTable
    Public general As New clsFuncionesUtiles

    Public Sub New()
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        InitForm()
    End Sub

    Private Sub InitForm()
        Dim strSQL As String
        Dim i As Integer = 0

        strSQL = " SELECT m.Codi as '" & rm.GetString("CODIGO") & "'"
        strSQL = strSQL & " ,m.descri as '" & rm.GetString("DESCRIPCION") & "'"
        strSQL = strSQL & " ,client as '" & rm.GetString("CLIENTE") & "'"
        strSQL = strSQL & " ,c.Nom as '" & rm.GetString("NOMCLIENT") & "'"
        strSQL = strSQL & " FROM "
        strSQL = strSQL & "	MOSTRES AS m left join clients as c on m.CLIENT = c.CODI "
        strSQL = strSQL & " WHERE "
        strSQL = strSQL & " m.CENTRO = ""M"" "
        strSQL = strSQL & " ORDER BY CLIENT, m.CODI"

        Using cmd As New MySqlCommand(strSQL)
            Using sda As New MySqlDataAdapter()
                cmd.Connection = cnn
                sda.SelectCommand = cmd
                sda.Fill(dt)
            End Using
        End Using

        cmbDuplicados.DataSource = dt
        cmbDuplicados.DisplayMember = rm.GetString("CODIGO")
        cmbDuplicados.ValueMember = rm.GetString("CODIGO")

        For i = 0 To dt.Columns.Count - 1
            cmbDuplicados.Columns(dt.Columns(i).ColumnName).Caption = dt.Columns(i).Caption
        Next
        AutosizeColumnasCombo(cmbDuplicados)

        btnDuplicar.Enabled = False

        txtDuplicar.Text = ""
        txtCliente.Text = ""
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Friend Shared Function GetInstance() As Form
        frmDuplicarComplemetnos = New DuplicarComplementos
        Return frmDuplicarComplemetnos
    End Function

    Private Sub cmbDuplicados_TextChanged(sender As Object, e As EventArgs) Handles cmbDuplicados.TextChanged
        If cmbDuplicados.SelectedIndex <> -1 Then
            txtDuplicar.Text = cmbDuplicados.SelectedValue
            txtCliente.Text = general.nz(dt.Rows(cmbDuplicados.SelectedIndex)(3), "")

            btnDuplicar.Enabled = True
        Else
            txtDuplicar.Text = ""
            txtCliente.Text = ""

            btnDuplicar.Enabled = False
        End If

    End Sub

    Private Sub btnDuplicar_Click(sender As Object, e As EventArgs) Handles btnDuplicar.Click
        Dim strSQL As String
        Dim insertSQL As String
        Dim insertSQL2 As String
        Dim duplicarMuestras As New DataTable
        Dim cmdIns As New MySqlCommand
        Dim rows() As DataRow = dt.Select("[" & rm.GetString("CODIGO") & "] = '" & txtDuplicar.Text & "' and [" & rm.GetString("CLIENTE") & "] = " & general.nz(dt.Rows(cmbDuplicados.SelectedIndex)(2), 0))

        Me.Enabled = False

        If rows.Length <> 0 Then
            MsgBox("Código duplicado")
        Else
            strSQL = "select * from mostres where codi = '" & cmbDuplicados.SelectedValue & "' and client = " & general.nz(dt.Rows(cmbDuplicados.SelectedIndex)(2), 0) & " and centro = 'M' "

            Using cmd As New MySqlCommand(strSQL)
                Using sda As New MySqlDataAdapter()
                    cmd.Connection = cnn
                    sda.SelectCommand = cmd
                    sda.Fill(duplicarMuestras)
                End Using
            End Using

            insertSQL = ""
            For Each dr As DataRow In duplicarMuestras.Rows
                insertSQL2 = ""
                For Each dc As DataColumn In duplicarMuestras.Columns
                    If dc.Caption.ToUpper = "CODI" Then
                        insertSQL = insertSQL & " insert into mostres ( codi "
                        insertSQL2 = "'" & txtDuplicar.Text & "'"
                    Else
                        insertSQL = insertSQL & "," & dc.Caption
                        Select Case dc.DataType
                            Case GetType(String)
                                insertSQL2 = insertSQL2 & ",'" & general.nz(dr(dc.Caption).ToString.Replace("'", "''"), "") & "'"
                            Case Else
                                insertSQL2 = insertSQL2 & "," & general.nz(dr(dc.Caption), "NULL").ToString
                        End Select
                    End If
                Next

                insertSQL = insertSQL & ") VAlues (" & insertSQL2 & ");" & vbLf
            Next

            Try
                cmdIns.CommandText = insertSQL
                cmdIns.Connection = cnn
                ACN()
                cmdIns.ExecuteNonQuery()
                CCN()

                '-------------------------
                '
                '-------------------------

                duplicarMuestras = New DataTable
                strSQL = "select * from talla where mostra = '" & cmbDuplicados.SelectedValue & "' and client = " & general.nz(dt.Rows(cmbDuplicados.SelectedIndex)(2), 0) & " and Centro = 'M'"
                Using cmd As New MySqlCommand(strSQL)
                    Using sda As New MySqlDataAdapter()
                        cmd.Connection = cnn
                        sda.SelectCommand = cmd
                        sda.Fill(duplicarMuestras)
                    End Using
                End Using

                insertSQL = ""
                For Each dr As DataRow In duplicarMuestras.Rows
                    insertSQL2 = ""
                    For Each dc As DataColumn In duplicarMuestras.Columns
                        If dc.Caption.ToUpper = "MOSTRA" Then
                            insertSQL = insertSQL & " insert into talla ( mostra "
                            insertSQL2 = "'" & txtDuplicar.Text & "'"
                        Else
                            insertSQL = insertSQL & "," & dc.Caption
                            Select Case dc.DataType
                                Case GetType(String)
                                    insertSQL2 = insertSQL2 & ",'" & general.nz(dr(dc.Caption).ToString, "") & "'"
                                Case Else
                                    insertSQL2 = insertSQL2 & "," & general.nz(dr(dc.Caption), "NULL").ToString.Replace(","c, "."c)
                            End Select
                        End If
                    Next

                    insertSQL = insertSQL & ") VAlues (" & insertSQL2 & ");" & vbLf
                Next

                If insertSQL <> "" Then
                    cmdIns.CommandText = insertSQL
                    cmdIns.Connection = cnn
                    ACN()
                    cmdIns.ExecuteNonQuery()
                    CCN()
                End If

                InitForm()

                MsgBox("Compenente duplicado correctamente")

            Catch ex As Exception
                MsgBox("Error al duplicar")
            End Try
        End If

        Me.Enabled = True
    End Sub

End Class