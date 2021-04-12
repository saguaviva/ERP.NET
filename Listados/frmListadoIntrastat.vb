Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Imports Microsoft.Office.Interop

Public Class frmListadoIntrastat

    Shared frmChildForm As frmListadoIntrastat
    Private xlsDir As ExcelDirecto.ExcelDirecto
    Private fcargando As frCargando

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        InitForm()
    End Sub

    Private Sub InitForm()

        Dim años As New DataTable
        Dim meses As New DataTable

        meses.Columns.Add("mes", GetType(String))

        Dim dr As DataRow = meses.NewRow
        dr("mes") = rm.GetString("ENERO")
        meses.Rows.Add(dr)
        dr = meses.NewRow
        dr("mes") = rm.GetString("FEBRERO")
        meses.Rows.Add(dr)
        dr = meses.NewRow
        dr("mes") = rm.GetString("MARZO")
        meses.Rows.Add(dr)
        dr = meses.NewRow
        dr("mes") = rm.GetString("ABRIL")
        meses.Rows.Add(dr)
        dr = meses.NewRow
        dr("mes") = rm.GetString("MAYO")
        meses.Rows.Add(dr)
        dr = meses.NewRow
        dr("mes") = rm.GetString("JUNIO")
        meses.Rows.Add(dr)
        dr = meses.NewRow
        dr("mes") = rm.GetString("JULIO")
        meses.Rows.Add(dr)
        dr = meses.NewRow
        dr("mes") = rm.GetString("AGOSTO")
        meses.Rows.Add(dr)
        dr = meses.NewRow
        dr("mes") = rm.GetString("SEPTIEMBRE")
        meses.Rows.Add(dr)
        dr = meses.NewRow
        dr("mes") = rm.GetString("OCTUBRE")
        meses.Rows.Add(dr)
        dr = meses.NewRow
        dr("mes") = rm.GetString("NOVIEMBRE")
        meses.Rows.Add(dr)
        dr = meses.NewRow
        dr("mes") = rm.GetString("DICIEMBRE")
        meses.Rows.Add(dr)

        cmbMeses.ItemsDataSource = meses
        cmbMeses.ItemsDisplayMember = "mes"
        cmbMeses.ItemsValueMember = "mes"
        cmbMeses.SelectedIndex = DateTime.Now.Month - 1

              Me.cmbAños.Culture =  3082
        Using cmd As New MySqlCommand("SELECT DISTINCT YEAR(`DATA`) as year FROM factur order by YEAR(`DATA`) desc;")
            Using sda As New MySqlDataAdapter()
                cmd.Connection = cnn
                sda.SelectCommand = cmd
                sda.Fill(años)
                'cmbAños.DataSource = años
                'cmbAños.ItemsDataSource = años
                'cmbAños.ItemsDisplayMember = "año"
                'cmbAños.ItemsValueMember = "año"

                ''cmbAños.DisplayMember = "año"
                ''cmbAños.ValueMember = "año"
                For each dr2 As DataRow In años.Rows
                            cmbAños.Items.Add(dr2("year"))
                Next

                cmbAños.SelectedIndex = 0
            End Using

        End Using

        


       

    End Sub

    Friend Shared Function GetInstance(esEleccionLista As Short) As Form
        frmChildForm = New frmListadoIntrastat
        Return frmChildForm
    End Function

    Private Sub frmListadoIntrastat_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        frmChildForm = Nothing
    End Sub

    Friend Sub CargandoFormulario()
        Try
            fcargando = New frCargando
            fcargando.Show()
            fcargando.Refresh()
            fcargando.BringToFront()

        Catch ex As Exception
            fcargando.Close() : xlsDir.CerrarExcel() : Throw ex ': LOG(ex.ToString)  : xlsDir.CerrarExcel() : CCN() : cnn.Close()
        End Try
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            xlsDir = New ExcelDirecto.ExcelDirecto(False, 0, 0, 0, 0, 0)

            CargandoFormulario()

            xlsDir.AñadirHoja("Factures", "Calibri")
            fcargando.lblstatus.Text = "Generant document pas 1/2"
            PonerCabecera()
            PonerLineas()
            xlsDir.objHojaExcel.Columns.EntireColumn.AutoFit()

            fcargando.lblstatus.Text = "Generant document pas 2/2"
            xlsDir.objLibroExcel.Sheets.Add()
            xlsDir.objHojaExcel = xlsDir.objLibroExcel.Worksheets(1)
            xlsDir.AñadirHoja("Agrupacio", "Calibri")
            PonerAgrupacion()
            PonerNcCodes()
            CambiarDiseño()
            Hayclientes()

            xlsDir.objHojaExcel.Columns.EntireColumn.AutoFit()

            fcargando.lblstatus.Text = "Obrint Excel..."
            xlsDir.LiberarExcel(num:="Intrastat-" & cmbMeses.SelectedIndex + 1 & "-" & cmbAños.Text)
            fcargando.Close()
        Catch ex As Exception
            fcargando.Close() : xlsDir.CerrarExcel() : Throw ex
        End Try
    End Sub

    Private Sub Hayclientes()
        Dim strsql As String
        Dim dtClientes As New DataTable
        Dim i As Integer

        strsql = "SELECT DISTINCT LEFT (clients.NIF, 2) as pais FROM clients WHERE "
        'strsql = strsql & " LEFT (clients.NIF, 2) IN ('AT','BE','BG','CY','CZ','DE','DK','EE','EL','FI','FR','GB','HR','HU','IE','IT','LT','LU','LV','MT','NL','PL','PT','RO','SE','SI','SK') "
         strsql = strsql & " LEFT (clients.NIF, 2) IN ('AT','BE','BG','CY','CZ','DE','DK','EE','EL','FI','FR','HR','HU','IE','IT','LT','LU','LV','MT','NL','PL','PT','RO','SE','SI','SK') "

        Using cmd As New MySqlCommand(strsql)
            Using sda As New MySqlDataAdapter()
                cmd.Connection = cnn
                sda.SelectCommand = cmd
                sda.Fill(dtClientes)
            End Using
        End Using

        For i = 3 To 30
            xlsDir.objHojaExcel.Cells(i, 1).Value = "no"
        Next

        For i = 0 To dtClientes.Rows.Count - 1
            Select Case dtClientes.Rows(i)("pais")
                Case "AT" : xlsDir.objHojaExcel.Cells(3, 1).Value = "si"
                Case "BE" : xlsDir.objHojaExcel.Cells(4, 1).Value = "si"
                Case "BG" : xlsDir.objHojaExcel.Cells(5, 1).Value = "si"
                Case "CY" : xlsDir.objHojaExcel.Cells(6, 1).Value = "si"
                Case "CZ" : xlsDir.objHojaExcel.Cells(7, 1).Value = "si"
                Case "DE" : xlsDir.objHojaExcel.Cells(8, 1).Value = "si"
                Case "DK" : xlsDir.objHojaExcel.Cells(9, 1).Value = "si"
                Case "EE" : xlsDir.objHojaExcel.Cells(10, 1).Value = "si"
                Case "EL" : xlsDir.objHojaExcel.Cells(11, 1).Value = "si"
                Case "ES" : xlsDir.objHojaExcel.Cells(12, 1).Value = "si"
                Case "FI" : xlsDir.objHojaExcel.Cells(13, 1).Value = "si"
                Case "FR" : xlsDir.objHojaExcel.Cells(14, 1).Value = "si"
                'Case "GB" : xlsDir.objHojaExcel.Cells(15, 1).Value = "si"
                Case "HR" : xlsDir.objHojaExcel.Cells(16, 1).Value = "si"
                Case "HU" : xlsDir.objHojaExcel.Cells(17, 1).Value = "si"
                Case "IE" : xlsDir.objHojaExcel.Cells(18, 1).Value = "si"
                Case "IT" : xlsDir.objHojaExcel.Cells(19, 1).Value = "si"
                Case "LT" : xlsDir.objHojaExcel.Cells(20, 1).Value = "si"
                Case "LU" : xlsDir.objHojaExcel.Cells(21, 1).Value = "si"
                Case "LV" : xlsDir.objHojaExcel.Cells(22, 1).Value = "si"
                Case "MT" : xlsDir.objHojaExcel.Cells(23, 1).Value = "si"
                Case "NL" : xlsDir.objHojaExcel.Cells(24, 1).Value = "si"
                Case "PL" : xlsDir.objHojaExcel.Cells(25, 1).Value = "si"
                Case "PT" : xlsDir.objHojaExcel.Cells(26, 1).Value = "si"
                Case "RO" : xlsDir.objHojaExcel.Cells(27, 1).Value = "si"
                Case "SE" : xlsDir.objHojaExcel.Cells(28, 1).Value = "si"
                Case "SI" : xlsDir.objHojaExcel.Cells(29, 1).Value = "si"
                Case "SK" : xlsDir.objHojaExcel.Cells(30, 1).Value = "si"
            End Select
        Next

    End Sub

    Private Sub CambiarDiseño()
        Dim i As Integer
        ' Poner bordes
        For i = 3 To 31
            With xlsDir.objHojaExcel.Range("C" & i, "Q" & i).Borders(Excel.XlBordersIndex.xlEdgeTop)
                .LineStyle = Excel.XlLineStyle.xlContinuous
                .Weight = Excel.XlBorderWeight.xlThin
                .Color = RGB(0, 0, 0)
            End With
        Next
        'Poner bordes verdes
        With xlsDir.objHojaExcel.Range("J2", "J31").Borders(Excel.XlBordersIndex.xlEdgeLeft)
            .LineStyle = Excel.XlLineStyle.xlContinuous
            .Weight = Excel.XlBorderWeight.xlMedium
            .Color = RGB(0, 176, 80)
        End With
        With xlsDir.objHojaExcel.Range("J2", "J31").Borders(Excel.XlBordersIndex.xlEdgeRight)
            .LineStyle = Excel.XlLineStyle.xlContinuous
            .Weight = Excel.XlBorderWeight.xlMedium
            .Color = RGB(0, 176, 80)
        End With
        With xlsDir.objHojaExcel.Range("Q2", "Q31").Borders(Excel.XlBordersIndex.xlEdgeLeft)
            .LineStyle = Excel.XlLineStyle.xlContinuous
            .Weight = Excel.XlBorderWeight.xlMedium
            .Color = RGB(0, 176, 80)
        End With
        With xlsDir.objHojaExcel.Range("Q2", "Q31").Borders(Excel.XlBordersIndex.xlEdgeRight)
            .LineStyle = Excel.XlLineStyle.xlContinuous
            .Weight = Excel.XlBorderWeight.xlMedium
            .Color = RGB(0, 176, 80)
        End With
        'Poner colores
        For i = 4 To 30 Step 2
            xlsDir.PonerColor(i, 4, i, 17, RGB(235, 241, 222))
        Next

    End Sub

    Private Sub PonerNcCodes()
        Dim dtNcCodes As New DataTable
        Dim strsql As String
        Dim i As Integer

        strsql = " SELECT (Sum(dfactu.PESO * dfactu.UNITATS) ) / 1000 as kg, SUM(dfactu.`IMPORT`) - SUM(IFNULL(factur.DTE1,0)) As importe, 	dfactu.NCCODE, Left(clients.NIF, 2) as pais"
        strsql = strsql & " FROM factur INNER Join dfactu ON factur.FRA = dfactu.FRA And factur.DOCUMENT = dfactu.DOCUMENT INNER Join clients ON factur.CLIENT = clients.CODI "
        strsql = strsql & " WHERE factur.DOCUMENT = 'F' And dfactu.DESCRI Not Like 'TRANSIT AND COURIER CHARGES' And YEAR (factur.`DATA`) = 2017 And MONTH (factur.`DATA`) = 3 And LEFT (clients.NIF, 2) "
        'strsql = strsql & " IN ('AT','BE','BG','CY','CZ','DE','DK','EE','EL','FI','FR','GB','HR','HU','IE','IT','LT','LU','LV','MT','NL','PL','PT','RO','SE','SI','SK') "
        strsql = strsql & " IN ('AT','BE','BG','CY','CZ','DE','DK','EE','EL','FI','FR','HR','HU','IE','IT','LT','LU','LV','MT','NL','PL','PT','RO','SE','SI','SK') "
        strsql = strsql & " group BY Left(clients.NIF, 2),dfactu.NCCODE "

        Using cmd As New MySqlCommand(strsql)
            Using sda As New MySqlDataAdapter()
                cmd.Connection = cnn
                sda.SelectCommand = cmd
                sda.Fill(dtNcCodes)
            End Using
        End Using
        Dim lin As Integer = 3
        For i = 0 To dtNcCodes.Rows.Count - 1
            Dim linia, columna1, columna2 As Integer
            Select Case dtNcCodes.Rows(i)("pais")
                Case "AT" : linia = lin
                Case "BE" : lin = lin + 1 : linia = lin
                Case "BG" : lin = lin + 1 : linia = lin
                Case "CY" : lin = lin + 1 : linia = lin
                Case "CZ" : lin = lin + 1 : linia = lin
                Case "DE" : lin = lin + 1 : linia = lin
                Case "DK" : lin = lin + 1 : linia = lin
                Case "EE" : lin = lin + 1 : linia = lin
                Case "EL" : lin = lin + 1 : linia = lin
                Case "ES" : lin = lin + 1 : linia = lin
                Case "FI" : lin = lin + 1 : linia = lin
                Case "FR" : lin = lin + 1 : linia = lin
                'Case "GB" : lin = lin + 1 : linia = lin
                Case "HR" : lin = lin + 1 : linia = lin
                Case "HU" : lin = lin + 1 : linia = lin
                Case "IE" : lin = lin + 1 : linia = lin
                Case "IT" : lin = lin + 1 : linia = lin
                Case "LT" : lin = lin + 1 : linia = lin
                Case "LU" : lin = lin + 1 : linia = lin
                Case "LV" : lin = lin + 1 : linia = lin
                Case "MT" : lin = lin + 1 : linia = lin
                Case "NL" : lin = lin + 1 : linia = lin
                Case "PL" : lin = lin + 1 : linia = lin
                Case "PT" : lin = lin + 1 : linia = lin
                Case "RO" : lin = lin + 1 : linia = lin
                Case "SE" : lin = lin + 1 : linia = lin
                Case "SI" : lin = lin + 1 : linia = lin
                Case "SK" : lin = lin + 1 : linia = lin
            End Select
            If Not IsDBNull(dtNcCodes.Rows(i)("NCCODE")) Then
                Select Case dtNcCodes.Rows(i)("NCCODE")
                    Case "6002 40 00"
                        columna1 = 4 : columna2 = 11
                    Case "6002 90 00"
                        columna1 = 5 : columna2 = 12
                    Case "6004 10 00"
                        columna1 = 6 : columna2 = 13
                    Case "6004 90 00"
                        columna1 = 7 : columna2 = 14
                    Case "6117 80 10"
                        columna1 = 8 : columna2 = 15
                    Case "5205 23 00"
                        columna1 = 9 : columna2 = 16
                End Select

                xlsDir.objHojaExcel.Cells(linia, columna1).Value = nzn(dtNcCodes.Rows(i)("kg"), 0)
                xlsDir.objHojaExcel.Cells(linia, columna2).Value = nzn(dtNcCodes.Rows(i)("importe"), 0)

            End If
        Next

        xlsDir.objHojaExcel.Range("D3", "Q31").NumberFormat = "#,##0.00" ' formato numero

        'totales
        For i = 4 To 17
            xlsDir.objHojaExcel.Cells(31, i).FormulaR1C1 = "=SUM(R[-28]C:R[-1]C)"
        Next
        For i = 3 To 31
            xlsDir.objHojaExcel.Cells(i, 10).FormulaR1C1 = "=SUM(RC[-6]:RC[-1])"
            xlsDir.objHojaExcel.Cells(i, 17).FormulaR1C1 = "=SUM(RC[-6]:RC[-1])"
        Next
    End Sub

    Private Sub PonerAgrupacion()
        Dim fuente As ExcelDirecto.ExcelDirecto.Fuente

        fuente.nombre = "Calibri"
        fuente.tamaño = 12
        xlsDir.PonerFuenteARango(2, 1, 2, 30, fuente)

        fuente.negrita = True
        xlsDir.PonerFuenteARango(1, 1, 1, 30, fuente) ' Titulos en negrita
        xlsDir.PonerFuenteARango(2, 2, 2, 3, fuente)
        xlsDir.PonerFuenteARango(2, 10, 2, 10, fuente)
        xlsDir.PonerFuenteARango(2, 17, 2, 17, fuente)
        xlsDir.PonerFuenteARango(2, 3, 30, 3, fuente)

        fuente.negrita = False
        fuente.color = RGB(255, 0, 0)
        xlsDir.PonerFuenteARango(2, 9, 2, 9, fuente)
        xlsDir.PonerFuenteARango(2, 16, 2, 16, fuente)
        Dim pos As Integer = 1
        With xlsDir.objHojaExcel
            ' linea 1
            .Cells(pos, 4).Value = "Codis arancelaris:"
            .Cells(pos, 10).Value = "TOTAL"
            .Cells(pos, 17).Value = "TOTAL"
            ' linea 2
            pos = pos + 1
            .Cells(pos, 1).Value = "Clients"
            .Cells(pos, 2).Value = "Ordre alfabètic"
            .Cells(pos, 3).Value = "sigles"
            .Cells(pos, 4).Value = "6002 40 00"
            .Cells(pos, 5).Value = "6002 90 00"
            .Cells(pos, 6).Value = "6004 10 00"
            .Cells(pos, 7).Value = "6004 90 00"
            .Cells(pos, 8).Value = "6117 80 10"
            .Cells(pos, 9).Value = "5205 23 00"
            .Cells(pos, 10).Value = "PESOS/Kgrs"
            .Cells(pos, 11).Value = "6002 40 00"
            .Cells(pos, 12).Value = "6002 90 00"
            .Cells(pos, 13).Value = "6004 10 00"
            .Cells(pos, 14).Value = "6004 90 00"
            .Cells(pos, 15).Value = "6117 80 10"
            .Cells(pos, 16).Value = "5205 23 00"
            .Cells(pos, 17).Value = "FACTURES€"
            ' paises
            pos = pos + 1
            .Cells(pos, 2).Value = "Austria (1995)"
            .Cells(pos, 3).Value = "AT"
            pos = pos + 1
            .Cells(pos, 2).Value = "Bélgica (1952)"
            .Cells(pos, 3).Value = "BE"
            pos = pos + 1
            .Cells(pos, 2).Value = "Bulgaria (2007)"
            .Cells(pos, 3).Value = "BG"
            pos = pos + 1
            .Cells(pos, 2).Value = "Chipre (2004)"
            .Cells(pos, 3).Value = "CY"
            pos = pos + 1
            .Cells(pos, 2).Value = "República Checa (2004)"
            .Cells(pos, 3).Value = "CZ"
            pos = pos + 1
            .Cells(pos, 2).Value = "Alemania (1952)"
            .Cells(pos, 3).Value = "DE"
            pos = pos + 1
            .Cells(pos, 2).Value = "Dinamarca (1973)"
            .Cells(pos, 3).Value = "DK"
            pos = pos + 1
            .Cells(pos, 2).Value = "Estonia (2004)"
            .Cells(pos, 3).Value = "EE"
            pos = pos + 1
            .Cells(pos, 2).Value = "Grecia (1981)"
            .Cells(pos, 3).Value = "EL"
            pos = pos + 1
            .Cells(pos, 2).Value = "España (1986)"
            .Cells(pos, 3).Value = "ES"
            .Cells(pos, 1).Font.Strikethrough = True
            .Cells(pos, 2).Font.Strikethrough = True
            .Cells(pos, 3).Font.Strikethrough = True
            pos = pos + 1
            .Cells(pos, 2).Value = "Finlandia (1995)"
            .Cells(pos, 3).Value = "FI"
            pos = pos + 1
            .Cells(pos, 2).Value = "Francia (1952)"
            .Cells(pos, 3).Value = "FR"
            'pos = pos + 1
            '.Cells(pos, 2).Value = "Reino Unido (1973)"
            '.Cells(pos, 3).Value = "GB"
            pos = pos + 1
            .Cells(pos, 2).Value = "Croacia (2013)"
            .Cells(pos, 3).Value = "HR"
            pos = pos + 1
            .Cells(pos, 2).Value = "Hungría (2004)"
            .Cells(pos, 3).Value = "HU"
            pos = pos + 1
            .Cells(pos, 2).Value = "Irlanda (1973)"
            .Cells(pos, 3).Value = "IE"
            pos = pos + 1
            .Cells(pos, 2).Value = "Italia (1952)"
            .Cells(pos, 3).Value = "IT"
            pos = pos + 1
            .Cells(pos, 2).Value = "Lituania (2004)"
            .Cells(pos, 3).Value = "LT"
            pos = pos + 1
            .Cells(pos, 2).Value = "Luxemburgo (1952)"
            .Cells(pos, 3).Value = "LU"
            pos = pos + 1
            .Cells(pos, 2).Value = "Letonia (2004)"
            .Cells(pos, 3).Value = "LV"
            pos = pos + 1
            .Cells(pos, 2).Value = "Malta (2004)"
            .Cells(pos, 3).Value = "MT"
            pos = pos + 1
            .Cells(pos, 2).Value = "Países Bajos (1952)"
            .Cells(pos, 3).Value = "NL"
            pos = pos + 1
            .Cells(pos, 2).Value = "Polonia (2004)"
            .Cells(pos, 3).Value = "PL"
            pos = pos + 1
            .Cells(pos, 2).Value = "Portugal (1986)"
            .Cells(pos, 3).Value = "PT"
            pos = pos + 1
            .Cells(pos, 2).Value = "Rumanía (2007)"
            .Cells(pos, 3).Value = "RO"
            pos = pos + 1
            .Cells(pos, 2).Value = "Suecia (1995)"
            .Cells(pos, 3).Value = "SE"
            pos = pos + 1
            .Cells(pos, 2).Value = "Eslovenia (2004)"
            .Cells(pos, 3).Value = "SI"
            pos = pos + 1
            .Cells(pos, 2).Value = "Eslovaquia (2004)"
            .Cells(pos, 3).Value = "SK"
            

        End With
    End Sub

    Private Sub PonerLineas()
        Dim dtLineas As New DataTable
        Dim strsql As String
        Dim facturaAnt As Double = -1, importeTransporte As Double = 0, kgTotal As Double = 0, importe As Double = 0
        Dim linia As Double = 2, i As Integer

        strsql = "SELECT factur.FRA, clients.NOM, dfactu.DESCRI, dfactu.COMPOSICIO, dfactu.TALLA,dfactu.UNITATS, dfactu.PESO, dfactu.`IMPORT`, dfactu.NCCODE, clients.NIF,LEFT(clients.NIF,2),factur.DTE1, clients.DTOFORMA "
        strsql = strsql & " FROM factur INNER JOIN dfactu ON factur.FRA = dfactu.FRA AND factur.DOCUMENT = dfactu.DOCUMENT INNER JOIN clients ON factur.CLIENT = clients.CODI "
        strsql = strsql & " WHERE factur.DOCUMENT = 'F' AND YEAR(factur.`DATA`) = " & cmbAños.Text & " AND MONTH(factur.`DATA`) = " & cmbMeses.SelectedIndex + 1 & " "
        'strsql = strsql & " AND LEFT(clients.NIF,2) IN ('AT','BE','BG','CY','CZ','DE','DK','EE','EL','FI','FR','GB','HR','HU','IE','IT','LT','LU','LV','MT','NL','PL','PT','RO','SE','SI','SK') "
        strsql = strsql & "  AND LEFT(clients.NIF,2) IN ('AT','BE','BG','CY','CZ','DE','DK','EE','EL','FI','FR','HR','HU','IE','IT','LT','LU','LV','MT','NL','PL','PT','RO','SE','SI','SK') "
        strsql = strsql & " ORDER BY factur.FRA, dfactu.nlinea"

        Using cmd As New MySqlCommand(strsql)
            Using sda As New MySqlDataAdapter()
                cmd.Connection = cnn
                sda.SelectCommand = cmd
                sda.Fill(dtLineas)
            End Using
        End Using

        'xlsDir.objHojaExcel.Cells("I:2", "I:" & dtLineas.Rows.Count).Style.Numberformat.Format = "0.00"

        For i = 0 To dtLineas.Rows.Count - 1
            'TRANSIT AND COURIER CHARGES
            With xlsDir.objHojaExcel
                If facturaAnt <> dtLineas.Rows(i)("FRA") Then
                    .Cells(linia, 1).Value = dtLineas.Rows(i)("FRA")
                    facturaAnt = dtLineas.Rows(i)("FRA")
                End If
                .Cells(linia, 2).Value = dtLineas.Rows(i)("NOM")
                .Cells(linia, 3).Value = dtLineas.Rows(i)("DESCRI")
                .Cells(linia, 4).Value = dtLineas.Rows(i)("COMPOSICIO")
                .Cells(linia, 5).Value = dtLineas.Rows(i)("TALLA")
                .Cells(linia, 6).Value = dtLineas.Rows(i)("UNITATS")
                .Cells(linia, 7).Value = nzn(dtLineas.Rows(i)("PESO"), 0)
                .Cells(linia, 8).Value = Math.Round((CDbl(nzn(dtLineas.Rows(i)("PESO"), 0)) * CDbl(dtLineas.Rows(i)("UNITATS"))) / 1000, 2)

                .Cells(linia, 10).Value = dtLineas.Rows(i)("NCCODE")
                .Cells(linia, 11).Value = dtLineas.Rows(i)("NIF")
                .Cells(linia, 12).Value = nzn(dtLineas.Rows(i)("DTE1"), 0).ToString("0.00")
                .Cells(linia, 16).Value = dtLineas.Rows(i)("NIF").ToString().Substring(0, 2)

                kgTotal = kgTotal + Math.Round((CDbl(nzn(dtLineas.Rows(i)("PESO"), 0)) * CDbl(dtLineas.Rows(i)("UNITATS"))) / 1000, 2)

                If dtLineas.Rows(i)("DESCRI") = "TRANSIT AND COURIER CHARGES" Then
                    xlsDir.PonerColor(linia, 3, linia, 9, RGB(255, 255, 0))
                    importeTransporte = importeTransporte + CDbl(dtLineas.Rows(i)("IMPORT"))
                    .Cells(linia, 9).Value = CDbl(dtLineas.Rows(i)("IMPORT")) 'IIf(CDbl(dtLineas.Rows(i)("IMPORT")) < 0, "'", "") & CDbl(dtLineas.Rows(i)("IMPORT"))
                Else
                    If nzn(dtLineas.Rows(i)("DTE1"), 0) <> 0 Then
                        .Cells(linia, 9).Value = CDbl(dtLineas.Rows(i)("IMPORT")) * ((100 - nzn(dtLineas.Rows(i)("DTOFORMA"), 0)) / 100) 'IIf((CDbl(dtLineas.Rows(i)("IMPORT")) * ((100 - nzn(dtLineas.Rows(i)("DTOFORMA"), 0)) / 100)) < 0, "'", "") & CDbl(dtLineas.Rows(i)("IMPORT")) * ((100 - nzn(dtLineas.Rows(i)("DTOFORMA"), 0)) / 100) '.ToString("0.00") '-nzn(dtLineas.Rows(i)("DTE1"), 0)
                        xlsDir.PonerColor(linia, 12, linia, 12, RGB(146, 208, 80))
                        xlsDir.PonerColor(linia, 9, linia, 9, RGB(146, 208, 80))
                        importe = importe + CDbl(CDbl(dtLineas.Rows(i)("IMPORT")) * ((100 - nzn(dtLineas.Rows(i)("DTOFORMA"), 0)) / 100)).ToString("0.00") '- nzn(dtLineas.Rows(i)("DTE1"), 0)
                        .Cells(linia, 12).Value = (CDbl(dtLineas.Rows(i)("IMPORT")) - CDbl(dtLineas.Rows(i)("IMPORT")) * ((100 - nzn(dtLineas.Rows(i)("DTOFORMA"), 0)) / 100)).ToString("0.00")
                    Else
                        importe = importe + CDbl(dtLineas.Rows(i)("IMPORT"))
                        .Cells(linia, 9).Value = CDbl(dtLineas.Rows(i)("IMPORT")) 'IIf(CDbl(dtLineas.Rows(i)("IMPORT")) < 0, "'", "") & CDbl(dtLineas.Rows(i)("IMPORT"))
                    End If
                End If
                .Cells(linia, 9).NumberFormat = "#,##0.00"
            End With
            linia = linia + 1
        Next

        xlsDir.objHojaExcel.Cells(1, 14).Value = importeTransporte
        xlsDir.objHojaExcel.Cells(6, 13).Value = kgTotal
        xlsDir.objHojaExcel.Cells(6, 14).Value = importe + importeTransporte
        xlsDir.objHojaExcel.Cells(8, 14).Value = importe
        xlsDir.objHojaExcel.Cells(1, 14).NumberFormat = "#,##0.00"
        xlsDir.objHojaExcel.Cells(6, 13).NumberFormat = "#,##0.00"
        xlsDir.objHojaExcel.Cells(6, 14).NumberFormat = "#,##0.00"
        xlsDir.objHojaExcel.Cells(8, 14).NumberFormat = "#,##0.00"

        Dim fuente As ExcelDirecto.ExcelDirecto.Fuente

        fuente.nombre = "Calibri"
        fuente.tamaño = 10
        xlsDir.PonerFuenteARango(2, 1, dtLineas.Rows.Count + 1, 17, fuente) ' formatear todas las lineas

        fuente.negrita = True
        xlsDir.PonerFuenteARango(4, 13, 5, 14, fuente) ' sumas, importe ..

        fuente.negrita = False
        fuente.color = RGB(255, 0, 0)
        xlsDir.PonerFuenteARango(2, 12, dtLineas.Rows.Count + 1, 12, fuente) ' descuento en rojo

        fuente.tamaño = 12
        fuente.negrita = True
        fuente.color = RGB(255, 0, 0)
        xlsDir.PonerFuenteARango(1, 12, 1, 14, fuente) ' titulo de descuento y transporte

        xlsDir.PonerColor(2, 16, dtLineas.Rows.Count, 16, RGB(255, 255, 0))

    End Sub

    Private Sub PonerCabecera()
        Dim fuente As ExcelDirecto.ExcelDirecto.Fuente

        fuente.nombre = "Calibri"
        fuente.tamaño = 12
        fuente.negrita = True
        xlsDir.PonerFuenteARango(1, 1, 1, 14, fuente) ' Titulos en negrita

        With xlsDir.objHojaExcel
            .Cells(1, 1).Value = "Factura"
            .Cells(1, 2).Value = "Client"
            .Cells(1, 3).Value = "Descripció"
            .Cells(1, 4).Value = "Composició"
            .Cells(1, 5).Value = "Mides"
            .Cells(1, 6).Value = "Quantitat"
            .Cells(1, 7).Value = "Pes unitari"
            .Cells(1, 8).Value = "Pes total/Kgrs"
            .Cells(1, 9).Value = "Import"
            .Cells(1, 10).Value = "NC CODE"
            .Cells(1, 11).Value = "VAT"
            .Cells(1, 12).Value = "Dte?"
            .Cells(1, 13).Value = "Transport"
            .Cells(4, 13).Value = "SUMES:"
            .Cells(5, 13).Value = "pes total/kgrs"
            .Cells(5, 14).Value = "import"
            .Cells(8, 13).Value = "Import venda"
        End With

    End Sub
End Class