Imports Microsoft.Office.Core
Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones
Imports Excel = Microsoft.Office.Interop.Excel

Namespace ExcelDirecto

    Public Class ExcelDirecto
        Public xlApp As Object
        Public ProcId As Integer

#Region "VARIABLES"

        Public WithEvents m_Excel As Excel.Application
        Public WithEvents objLibroExcel As Excel.Workbook
        Public WithEvents objHojaExcel As Excel.Worksheet
        Public tamHoja As Integer
        Public tamDetalle As Integer
        Public tamDetalleNoPrimera As Integer
        Public iniDetalle As Integer
        Public iniDetalleNoPrimera As Integer
        Public WithEvents boton1 As Microsoft.Office.Core.CommandBarButton

#End Region

#Region "ESTRUCTURAS"

        Public Structure TamMargenes

            Public margen_izq As Double
            Public margen_der As Double
            Public margen_sup As Double
            Public margen_inf As Double

        End Structure
        Public Structure Fuente

            Public nombre As String
            Public tamaño As Double
            Public color As Double
            Public colorIndex As Integer
            Public negrita As Boolean
            Public cursiva As Boolean

        End Structure
        Public Enum Orientacion
            Vertical = Excel.XlPageOrientation.xlPortrait
            Horizontal = Excel.XlPageOrientation.xlLandscape
        End Enum
        Public Enum Alineacion
            Derecha = Excel.XlHAlign.xlHAlignRight
            Izquierda = Excel.XlHAlign.xlHAlignLeft
            Centro = Excel.XlHAlign.xlHAlignCenter
            Justificado = Excel.XlHAlign.xlHAlignJustify
            Superior = Excel.XlVAlign.xlVAlignTop
            Inferior = Excel.XlVAlign.xlVAlignBottom
        End Enum
        Public Enum OrientacionTexto
            Downward = Excel.XlOrientation.xlDownward
            Horizontal = Excel.XlOrientation.xlHorizontal
            Upward = Excel.XlOrientation.xlUpward
            Vertical = Excel.XlOrientation.xlVertical
        End Enum

#End Region

#Region "ORGANIZAR"

        Public Sub PonerBordeARangoRepetidasEnFila( _
               ByVal filaInicial As Integer, _
               ByVal finaFinal As Integer, _
               ByVal colInicial As Integer, _
               ByVal colFinal As Integer, _
               ByVal aColumnas() As String, _
               ByVal tipo As Excel.XlBordersIndex, _
               ByVal estilolinea As Excel.XlLineStyle, _
               ByVal ancho As Excel.XlBorderWeight, _
               ByVal color As Integer)

            Dim col As Integer
            Dim colAnterior As Integer
            Try
                For Each col In aColumnas
                    colAnterior = col
                    Dim r As Excel.Range
                    If col <> colInicial Then
                        r = objHojaExcel.Range(objHojaExcel.Cells(filaInicial, colAnterior), objHojaExcel.Cells(finaFinal, colFinal))
                        With r.Borders(tipo)
                            .LineStyle = estilolinea
                            .Weight = ancho
                            .Color = color
                            '.ColorIndex = color
                        End With
                    End If
                Next

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub JuntarRepetidasEnFila(ByVal fila As Integer, ByVal aColumnas() As String, ByVal colInicial As Integer, ByVal colFinal As Integer)
            Try
                Dim col As Integer
                Dim colAnterior As Integer
                For Each col In aColumnas
                    With objHojaExcel
                        colAnterior = col
                        If col <> colInicial Then
                            .Range(.Cells(fila, colAnterior), .Cells(fila, col - 1)).Merge()
                        End If
                    End With
                Next

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub Juntar(ByVal row1 As Integer, ByVal col1 As Integer, ByVal row2 As Integer, ByVal col2 As Integer, Optional ByVal alineacion As Alineacion = Alineacion.Centro, Optional ByVal alineacionVert As Alineacion = Alineacion.Superior)
            Try
                If row1 < 0 OrElse row2 < 0 OrElse col1 < 0 OrElse col2 < 0 Then Exit Sub
                With objHojaExcel
                    .Range(.Cells(row1, col1), .Cells(row1, col1)).HorizontalAlignment = alineacion
                    .Range(.Cells(row1, col1), .Cells(row1, col1)).VerticalAlignment = alineacionVert
                    .Range(.Cells(row1, col1), .Cells(row2, col2)).Merge()
                End With

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub LiberarExcel(Optional ByVal fcargando As frCargando = Nothing, Optional ByVal num As String = "libro1.xls")
            Dim strFile As String
            Try
                m_Excel.ScreenUpdating = True
                m_Excel.Interactive = True
                m_Excel.Calculation = Excel.XlCalculation.xlCalculationAutomatic

                If Not fcargando Is Nothing Then fcargando.lblstatus.Text = "Guardant document Excel"
                strFile = My.Computer.FileSystem.SpecialDirectories.Desktop & "\Documentos\" & num

                If Not System.IO.Directory.Exists(My.Computer.FileSystem.SpecialDirectories.Desktop & "\Documentos\") Then
                    System.IO.Directory.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.Desktop & "\Documentos\")
                End If

                objLibroExcel.SaveAs(Filename:=strFile)
                objLibroExcel.Close()
                m_Excel.Quit()
                While (System.Runtime.InteropServices.Marshal.ReleaseComObject(m_Excel) <> 0) ' Repeat until no
                End While

                GC.Collect()
                GC.WaitForPendingFinalizers()
                Try : m_Excel = Nothing : Catch : End Try
                Try : objHojaExcel = Nothing : Catch : End Try
                Try : objLibroExcel = Nothing : Catch : End Try
                If Not fcargando Is Nothing Then fcargando.lblstatus.Text = "Obrint document Excel"
                Dim excel2 As Microsoft.Office.Interop.Excel.Application

                Dim wb As Microsoft.Office.Interop.Excel.Workbook

                excel2 = New Microsoft.Office.Interop.Excel.Application
                wb = excel2.Workbooks.Open(strFile)
                excel2.Visible = True
                wb.Activate()


            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub CerrarExcel()
            Try
                m_Excel.ScreenUpdating = True
                m_Excel.Interactive = True
                m_Excel.Quit()
                While (System.Runtime.InteropServices.Marshal.ReleaseComObject(m_Excel) <> 0) ' Repeat until no
                End While

                'm_Excel = Nothing
                'objHojaExcel = Nothing
                ' objLibroExcel = Nothing
                GC.Collect()
                GC.WaitForPendingFinalizers()

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub VerVistaPrevia()
            Try
                objLibroExcel.PrintPreview()
            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub

        Public Sub AutoSizeColumna(ByVal r As Object)
            Try
                objHojaExcel.Columns.Range(r).EntireColumn.AutoFit()

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub

        Public Sub ConfigurarPagina(ByVal orientacion As Orientacion, ByVal margenes As TamMargenes)
            Try
                With DirectCast(objLibroExcel.ActiveSheet.PageSetup, Excel.PageSetup)

                    .Orientation = orientacion
                    '.Zoom = False 'Force to use fit to page
                    '.FitToPagesWide = 1
                    '.FitToPagesTall = 1

                    'Do not print the grid lines on the printout
                    '.PrintGridlines = True


                    .LeftMargin = m_Excel.InchesToPoints(margenes.margen_izq)
                    .RightMargin = m_Excel.InchesToPoints(margenes.margen_der)
                    .TopMargin = m_Excel.InchesToPoints(margenes.margen_sup)
                    .BottomMargin = m_Excel.InchesToPoints(margenes.margen_inf)

                    'Print the first 6 rows as the header on each sheet
                    '.PrintTitleRows = m_Excel.ActiveSheet.Range("A1:A6").Address
                    '.PrintArea = "$A$1:$D25$"

                End With

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub PonerFuenteARango(ByVal r As String, ByVal fuente As Fuente)
            Try
                With objHojaExcel.Range(r).Font
                    .Name = fuente.nombre
                    .Size = fuente.tamaño
                    .Color = fuente.color
                    .Bold = fuente.negrita
                End With
            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub PonerFuenteARango(ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer, ByVal Fuente As ExcelDirecto.Fuente)
            Try
                With objHojaExcel.Range(objHojaExcel.Cells(x1, y1), objHojaExcel.Cells(x2, y2)).Font
                    .Name = Fuente.nombre
                    .Size = Fuente.tamaño
                    If Fuente.color = 0 Then
                        .ColorIndex = Fuente.colorIndex
                    Else
                        .Color = Fuente.color
                    End If
                    .Bold = Fuente.negrita
                End With
            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub PonerFuenteARango(ByVal r As Excel.Range, ByVal fuente As Fuente)
            Try
                With r.Font
                    .Name = fuente.nombre
                    .Size = fuente.tamaño
                    .Color = fuente.color
                    .Bold = fuente.negrita
                End With
            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Function Cabe(ByVal fila As Integer, ByVal hoja As Integer) As Boolean
            Try
                'Dim tam As Integer = fila * hoja
                If fila > tamHoja * hoja Then
                    Return False
                Else
                    Return True
                End If

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Function
        Public Function ObtenerHoja(ByVal celdas As Integer) As Integer
            Dim hojas As Integer
            Dim hoj As Integer
            Try
                hojas = Math.DivRem(celdas, tamHoja, hoj)
                If hojas = 0 Then
                    Return 1
                Else
                    Return hojas + 1
                End If

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Function
        Public Sub OcularFilasYColumnas(ByVal r As Excel.Range, ByVal soloColumnas As Boolean)

            Dim row1 As Long, row2 As Long, col1 As Long, col2 As Long
            Dim MaxRows As Long, MaxCols As Long
            MaxRows = m_Excel.ActiveSheet.Rows.Count
            MaxCols = m_Excel.ActiveSheet.Columns.Count

            'm_Excel.Application.ScreenUpdating = False
            m_Excel.Cells.EntireRow.Hidden = False
            m_Excel.Cells.EntireColumn.Hidden = False

            row1 = r.Range("A1").Row
            row2 = row1 + r.Rows.Count - 1

            col1 = r.Range("A1").Column
            col2 = col1 + r.Columns.Count - 1

            On Error GoTo ErrHandle
            'If soloColumnas = False Then
            If row1 <> 1 Then m_Excel.Range(m_Excel.Cells(1, 1), m_Excel.Cells(row1 - 1, 1)).EntireRow.Hidden = True
            If row2 <> MaxRows Then m_Excel.Range(m_Excel.Cells(row2 + 1, 1), m_Excel.Cells(65536, 1)).EntireRow.Hidden = True
            'End If

            'If col1 <> 1 Then m_Excel.Range(m_Excel.Cells(1, 1), m_Excel.Cells(1, col1 - 1)).EntireColumn.Hidden = True
            'If col2 <> MaxCols Then m_Excel.Range(m_Excel.Cells(1, col2 + 1), m_Excel.Cells(1, 256)).EntireColumn.Hidden = True
            'm_Excel.Application.ScreenUpdating = True
            Exit Sub
ErrHandle:
            'm_Excel.Application.ScreenUpdating = True
            MsgBox("Error." & vbCrLf & Err.Description & vbCrLf & vbCrLf & "Make sure that the rows and columns to be hidden do not contain any charts, graphic objects, or cell comments.", vbInformation, "Error")
            On Error Resume Next
            m_Excel.Cells.EntireRow.Hidden = False
            m_Excel.Cells.EntireColumn.Hidden = False
            On Error GoTo 0

        End Sub

        Public Sub AsignarMacro(ByVal lb As Excel.Shape, ByVal nombreMacro As String)

            lb.OnAction = nombreMacro

        End Sub
        Public Function AñadirBoton(ByVal x1 As Integer, ByVal y1 As Integer, _
                    ByVal x2 As Integer, ByVal y2 As Integer, ByVal largo As Integer, _
                    ByVal alto As Integer, ByVal caption As String) As Excel.Shape
            Try

                Dim lb As Excel.Shape
                With objHojaExcel
                    lb = .Shapes.AddFormControl( _
                               Excel.XlFormControl.xlButtonControl, _
                               DirectCast(.Range(.Cells(x1, y1), .Cells(x1, y1)), Excel.Range).Left, _
                               DirectCast(.Range(.Cells(x2, y2), .Cells(x2, y2)), Excel.Range).Top, largo, alto)
                    'lb.Name = caption
                    lb.TextFrame.Characters.Text = caption

                    'lb.Diagra
                End With
                Return lb
            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Function
        Public Function Prueba()
            MessageBox.Show("hola")
        End Function
        Public Sub QuitarBarras()

            Dim ie As IEnumerator
            ie = m_Excel.CommandBars.GetEnumerator
            : On Error Resume Next
            While ie.MoveNext
                : On Error Resume Next
                If ie.Current.Visible = True And ie.Current.Name <> "Worksheet Menu Bar" _
                 And ie.Current.Name <> "Standard" And ie.Current.Name <> "Formatting" Then
                    ie.Current.Visible = False
                    'Else
                    '    ie.Current.Visible = True
                End If
                : On Error Resume Next
            End While
            : On Error Resume Next


        End Sub
        Public Sub PonerBarra()

            Dim cb As Microsoft.Office.Core.CommandBar
            : On Error Resume Next
            m_Excel.CommandBars.Item("Barra").Delete()
            cb = m_Excel.CommandBars.Add("Barra", Microsoft.Office.Core.MsoBarPosition.msoBarTop)
            m_Excel.CommandBars("Barra").Visible = True
            boton1 = m_Excel.CommandBars("Barra").Controls.Add
            boton1.Caption = "Imprimir"
            boton1.DescriptionText = "Imprimir"

            cb.Visible = True
        End Sub
        Public Sub New()
            Try

            Catch ex As Exception

            End Try
        End Sub
        Public Sub New(ByVal visible As Boolean, ByVal tamHoja As Integer, _
                        ByVal tamDetalle As Integer, ByVal tamDetalleNoP As Integer, _
                        ByVal iniDet As Integer, ByVal iniDetNoP As Integer, Optional ByVal hack As Boolean = False)
            Try

                m_Excel = New Excel.Application
                'm_Excel = CType(CreateObject("Excel.Application"), Microsoft.Office.Interop.Excel.Application)
                'Codigo para cerrar totalmente el excel
                'Dim Process1() As System.Diagnostics.Process = System.Diagnostics.Process.GetProcesses()
                'm_Excel = CreateObject("Excel.Application")
                'Dim Process2() As System.Diagnostics.Process = System.Diagnostics.Process.GetProcesses()
                ''
                'Dim ct1, ct2 As Integer
                'Dim blnUnique As Boolean
                'For ct1 = 0 To Process2.GetUpperBound(0)
                '    If Process2(ct1).ProcessName = "EXCEL" Then
                '        blnUnique = True
                '        For ct2 = 0 To Process1.GetUpperBound(0)
                '            If Process1(ct2).ProcessName = "EXCEL" Then
                '                If Process2(ct1).Id = Process1(ct2).Id Then
                '                    blnUnique = False
                '                    Exit For
                '                End If
                '            End If
                '        Next
                '        If blnUnique = True Then
                '            ProcId = Process2(ct1).Id
                '            Exit For
                '        End If
                '    End If
                'Next

                m_Excel.ScreenUpdating = True
                m_Excel.Interactive = True
                If hack = False Then
                    objLibroExcel() = m_Excel.Workbooks.Add
                Else
                    objLibroExcel = m_Excel.Workbooks.Add
                End If
                m_Excel.Calculation = Excel.XlCalculation.xlCalculationManual
                'objLibroExcel = m_Excel.Workbooks.Open(curdir & "\plantilla.xls") 'm_Excel.Workbooks.Add()

                m_Excel.Visible = visible
                Me.tamHoja = tamHoja
                Me.tamDetalle = tamDetalle
                m_Excel.Workbooks(1).Colors(15) = RGB(216, 216, 216)
                Me.tamDetalleNoPrimera = tamDetalleNoP
                Me.iniDetalle = iniDet
                Me.iniDetalleNoPrimera = iniDetNoP
                QuitarBarras()
                PonerBarra()


            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Private Sub m_Excel_WorkbookBeforePrint(ByVal Wb As Excel.Workbook, ByRef Cancel As Boolean) Handles m_Excel.WorkbookBeforePrint
        End Sub
        Private Sub boton1_Click(ByVal Ctrl As Microsoft.Office.Core.CommandBarButton, ByRef CancelDefault As Boolean) Handles boton1.Click
            objLibroExcel.PrintOut()
        End Sub

#End Region

#Region "FUNCIONES HOJAS"

        Public Sub QuitarTodasLasHojas()
            Try
                m_Excel.DisplayAlerts = False
                objHojaExcel.Visible = Excel.XlSheetVisibility.xlSheetVisible
                Dim ie As IEnumerator = objLibroExcel.Worksheets.GetEnumerator
                While ie.MoveNext
                    DirectCast(ie.Current, Excel._Worksheet).Delete()
                End While
                m_Excel.DisplayAlerts = True

            Catch ex As Exception
                'LOG(ex.ToString)
            End Try
        End Sub
        Public Sub AñadirHoja(ByVal titulo As String, ByVal nombreFuente As String, Optional ByVal hack As Boolean = False)
            Try
                objHojaExcel = objLibroExcel.Worksheets(1)
                objHojaExcel.Name = Left(titulo, 31)
                objHojaExcel.Visible = Excel.XlSheetVisibility.xlSheetVisible
                objHojaExcel.Range("a1:iv65536").Font.Name = nombreFuente
                objHojaExcel.DisplayPageBreaks = True
                If hack = False Then
                    PonerAnchoRangoColumnas(1, 150, "1")
                End If

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub DuplicarHoja(ByVal titulo As String, ByVal nuevoTitulo As String, ByVal colorRGB As Integer)
            Try
                Dim i As Integer
                'm_Excel.Worksheets(m_Excel.Worksheets.Count).Copy()
                m_Excel.Sheets(1).copy(After:=m_Excel.Sheets(m_Excel.Worksheets.Count))
                m_Excel.Worksheets(m_Excel.Worksheets.Count).Name = "COPIA " & m_Excel.Worksheets.Count - 1
                'Dim rVer As Excel.Range = m_Excel.Worksheets(m_Excel.Worksheets.Count).Range(objHojaExcel.Cells(1, 1), m_Excel.Worksheets(m_Excel.Worksheets.Count).Cells(63 + 10, 60))
                'm_Excel.Worksheets(m_Excel.Worksheets.Count).OcularFilasYColumnas(rVer)

                'm_Excel.Worksheets(m_Excel.Worksheets.Count).Application.ScreenUpdating = False
                'm_Excel.Worksheets(m_Excel.Worksheets.Count).Shapes.Item("Dum").Fill.ForeColor.RGB = colorRGB
                'm_Excel.Worksheets(m_Excel.Worksheets.Count).Application.ScreenUpdating = True

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub PonerQuitarGridLines(ByVal esVisible As Boolean)
            Try
                m_Excel.ActiveWindow.DisplayGridlines = False
                Dim i As Integer
                For i = 1 To m_Excel.Windows.Count
                    m_Excel.Windows(i).DisplayGridlines = esVisible
                Next
            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub

#End Region

#Region "COLORES"

        Public Sub PonerColor(ByVal r1 As Object, ByVal r2 As Object,
                                    ByVal r3 As Object, ByVal r4 As Object, ByVal color As Object)
            Try
                Dim r As Excel.Range = objHojaExcel.Range(objHojaExcel.Cells(r1, r2), objHojaExcel.Cells(r3, r4))
                r.Interior.Color = color

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub PonerColorARango(ByVal x1 As Integer, ByVal y1 As Integer, _
                                    ByVal x2 As Integer, ByVal y2 As Integer, ByVal color As Object)
            Try
                Dim r As Excel.Range = objHojaExcel.Range(objHojaExcel.Cells(x1, y1), objHojaExcel.Cells(x2, y2))
                r.Interior.ColorIndex = color

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub

#End Region

#Region "ALINEACION"
        Public Sub AlinearRango(ByVal row1 As Integer, ByVal col1 As Integer, ByVal row2 As Integer, ByVal col2 As Integer, ByVal alineacion As Alineacion, Optional ByVal alineacionVert As Alineacion = Alineacion.Superior)
            Try
                With objHojaExcel
                    .Range(.Cells(row1, col1), .Cells(row2, col2)).HorizontalAlignment = alineacion
                    .Range(.Cells(row1, col1), .Cells(row2, col2)).VerticalAlignment = alineacionVert
                End With

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub AlinearRango(ByVal r As Object, ByVal alineacion As Alineacion)
            Try
                objHojaExcel.Range(r).HorizontalAlignment = alineacion
            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub Alinear(ByVal r1 As Object, ByVal r2 As Object, ByVal alineacion As Alineacion)
            Try
                objHojaExcel.Cells(r1, r2).HorizontalAlignment = alineacion

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub AlinearTodo(ByVal alineacion As Alineacion)
            Try

                objHojaExcel.Range("a1:iv65536").HorizontalAlignment = alineacion
            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub OrientarRango(ByVal row1 As Integer, ByVal col1 As Integer, ByVal row2 As Integer, ByVal col2 As Integer, ByVal orientacion As OrientacionTexto)
            Try
                With objHojaExcel
                    .Range(.Cells(row1, col1), .Cells(row2, col2)).Orientation = orientacion
                End With

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
#End Region

#Region "BORDES"

        Public Sub PonerHeader(ByVal txtCabeceraDerecha As String)
            Try
                objHojaExcel.PageSetup.LeftHeader = txtCabeceraDerecha

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub PonerBorde()
            Try

            Catch ex As Exception
                LOG(ex.ToString)
            End Try

        End Sub
        Public Sub PonerBordeARango(ByVal rango As Excel.Range, _
                ByVal tipo As Excel.XlBordersIndex, _
                ByVal estilolinea As Excel.XlLineStyle, _
                ByVal ancho As Excel.XlBorderWeight, _
                ByVal color As Integer)
            Try
                With rango.Borders(tipo)
                    .LineStyle = estilolinea
                    .Weight = ancho
                    '.Color = color
                    .ColorIndex = color
                End With

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub PonerBordeARango(ByVal x1 As Integer, ByVal y1 As Integer, _
                        ByVal x2 As Integer, ByVal y2 As Integer, ByVal tipo As Excel.XlBordersIndex, _
                        ByVal estilolinea As Excel.XlLineStyle, ByVal ancho As Excel.XlBorderWeight, ByVal color As Integer)
            Try
                Dim Rango As Excel.Range
                Rango = objHojaExcel.Range(objHojaExcel.Cells(x1, y1), objHojaExcel.Cells(x2, y2))
                With Rango.Borders(tipo)
                    .LineStyle = estilolinea
                    .Weight = ancho
                    '.Color = color
                    .ColorIndex = color
                End With

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        'Pone borde a un rectangulo de celdas
        Public Sub PonerBordeARangoRectangulo(ByVal x1 As Integer, ByVal y1 As Integer, _
                        ByVal x2 As Integer, ByVal y2 As Integer, ByVal estilolinea As Excel.XlLineStyle, _
                        ByVal ancho As Excel.XlBorderWeight, ByVal color As Integer)
            Try
                Dim r As Excel.Range

                r = objHojaExcel.Range(objHojaExcel.Cells(x1, y1), objHojaExcel.Cells(x2, y2))
                r.BorderAround(estilolinea, ancho, ColorIndex:=color)

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub
        Public Sub PonerBordeARangoRectangulo(ByVal rango As Excel.Range, ByVal tipo As Excel.XlBordersIndex, _
                ByVal estilolinea As Excel.XlLineStyle, ByVal ancho As Excel.XlBorderWeight, ByVal color As Integer)
            Try
                rango.BorderAround(estilolinea, ancho, ColorIndex:=color)

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub

#End Region

#Region "TEXTOS"

        Public Sub PonerTextosEnColumnas(ByVal columna As Integer, ByVal aTextos() As String)
            Dim fila As Integer
            Dim textoFila As String
            Dim anterior As String
            Try
                For Each textoFila In textoFila

                    Dim TargetCell = objHojaExcel.Range(objHojaExcel.Cells(fila, columna), objHojaExcel.Cells(fila, columna))
                    TargetCell.Value = "'" & " " & textoFila
                Next

            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub

#End Region

#Region "IMAGENES"

        Public Sub PonerImagenRepetidaEnFilas(ByVal PictureFileName As String, ByVal fila As Integer, ByVal aColumnas() As String, _
                    ByVal CenterH As Boolean, ByVal CenterV As Boolean, _
                    Optional ByVal JustificarIzq As Boolean = False, _
                    Optional ByVal width As Integer = -1, Optional ByVal heigh As Integer = -1, Optional ByVal desplazarAlaDerecha As Integer = 0)
            Dim col As Integer
            For Each col In aColumnas

                Dim TargetCell = objHojaExcel.Range(objHojaExcel.Cells(fila, col), objHojaExcel.Cells(fila, col))

                Dim p As Object, t As Double, l As Double, w As Double, h As Double
                If TypeName(m_Excel.ActiveSheet) <> "Worksheet" Then Exit Sub
                If Dir(PictureFileName) = "" Then Exit Sub
                ' import picture
                p = m_Excel.ActiveSheet.Pictures.Insert(PictureFileName)

                ' determine positions
                With TargetCell
                    t = .Top
                    l = .Left
                    If CenterH Then
                        w = .Offset(0, 1).Left - .Left
                        l = l + w / 2 - p.Width / 2
                        If l < 1 Then l = 1
                    End If
                    If CenterV Then
                        h = .Offset(1, 0).Top - .Top
                        t = t + h / 2 - p.Height / 2
                        If t < 1 Then t = 1
                    End If
                    If JustificarIzq Then
                        w = .Offset(0, 1).Left - .Left
                        l = l + w - p.Width
                        If l < 1 Then l = 1
                    End If
                End With
                ' position picture
                With p
                    .Top = t
                    .Left = l + desplazarAlaDerecha
                    If heigh <> -1 Then
                        .Width = heigh
                        .Height = width
                    End If
                End With
                p = Nothing

            Next
        End Sub
        Public Sub InsertPicture(ByVal PictureFileName As String, _
                    ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer, _
                    ByVal CenterH As Boolean, ByVal CenterV As Boolean, _
                    Optional ByVal JustificarIzq As Boolean = False, _
                    Optional ByVal width As Integer = -1, Optional ByVal heigh As Integer = -1, Optional ByVal desplazarAlaDerecha As Integer = 0)

            ' inserts a picture at the top left position of TargetCell
            ' the picture can be centered horizontally and/or vertically
            Dim TargetCell = objHojaExcel.Range(objHojaExcel.Cells(x1, y1), objHojaExcel.Cells(x2, y2))

            Dim p As Object, t As Double, l As Double, w As Double, h As Double
            If TypeName(m_Excel.ActiveSheet) <> "Worksheet" Then Exit Sub
            If Dir(PictureFileName) = "" Then Exit Sub
            ' import picture
            p = m_Excel.ActiveSheet.Pictures.Insert(PictureFileName)

            ' determine positions
            With TargetCell
                t = .Top
                l = .Left
                If CenterH Then
                    w = .Offset(0, 1).Left - .Left
                    l = l + w / 2 - p.Width / 2
                    If l < 1 Then l = 1
                End If
                If CenterV Then
                    h = .Offset(1, 0).Top - .Top
                    t = t + h / 2 - p.Height / 2
                    If t < 1 Then t = 1
                End If
                If JustificarIzq Then
                    w = .Offset(0, 1).Left - .Left
                    l = l + w - p.Width
                    If l < 1 Then l = 1
                End If
            End With
            ' position picture
            With p
                .Top = t
                .Left = l + desplazarAlaDerecha
                If heigh <> -1 Then
                    .Width = heigh
                    .Height = width
                End If
            End With
            p = Nothing
        End Sub

        Public Sub InsertPicture(ByVal PictureFileName As String, ByVal TargetCell As Excel.Range,
                       ByVal CenterH As Boolean, ByVal CenterV As Boolean, Optional ByVal JustificarIzq As Boolean = False,
                        Optional ByVal width As Double = -1, Optional ByVal height As Double = -1, Optional ByVal desplazarAlaDerecha As Integer = 0)
            ' inserts a picture at the top left position of TargetCell
            ' the picture can be centered horizontally and/or vertically
            Dim p As Object, t As Double, l As Double, w As Double, h As Double
            If TypeName(m_Excel.ActiveSheet) <> "Worksheet" Then Exit Sub
            If Dir(PictureFileName) = "" Then Exit Sub
            ' import picture
            p = m_Excel.ActiveSheet.Pictures.Insert(PictureFileName)

            ' determine positions
            With TargetCell
                t = .Top
                l = .Left
                If CenterH Then
                    w = .Offset(0, 1).Left - .Left
                    l = l + w / 2 - p.Width / 2
                    If l < 1 Then l = 1
                End If
                If CenterV Then
                    h = .Offset(1, 0).Top - .Top
                    t = t + h / 2 - p.Height / 2
                    If t < 1 Then t = 1
                End If
                If JustificarIzq Then
                    w = .Offset(0, 1).Left - .Left
                    l = l + w - p.Width
                    If l < 1 Then l = 1
                End If
            End With
            ' position picture
            With p
                .Top = t
                .Left = l + desplazarAlaDerecha
                If height <> -1 Then
                    .Width = height
                    .Height = width
                End If
            End With
            p = Nothing
        End Sub
        'With the macro below you can insert pictures and fit them to any range in a worksheet.
        Public Sub TestInsertPictureInRange()
            InsertPictureInRange("C:\FolderName\PictureFileName.gif", objHojaExcel.Range("B5:D10"))
        End Sub
        Sub InsertPictureInRange(ByVal PictureFileName As String, ByVal TargetCells As Excel.Range)
            ' inserts a picture and resizes it to fit the TargetCells range
            Dim p As Object, t As Double, l As Double, w As Double, h As Double
            If TypeName(m_Excel.ActiveSheet) <> "Worksheet" Then Exit Sub
            If Dir(PictureFileName) = "" Then Exit Sub
            ' import picture
            p = m_Excel.ActiveSheet.Pictures.Insert(PictureFileName)
            ' determine positions
            With TargetCells
                t = .Top
                l = .Left
                w = .Offset(0, .Columns.Count).Left - .Left
                h = .Offset(.Rows.Count, 0).Top - .Top
            End With
            ' position picture
            With p
                .Top = t
                .Left = l
                .Width = w
                .Height = h
            End With
            p = Nothing
        End Sub

#End Region

#Region "TAMAÑOS"

        Public Sub PonerAnchoRangoColumnas(ByVal r1 As Object, ByVal r2 As Object, ByVal tam As Integer)
            Try
                'DirectCast(objHojaExcel.Cells(r1, r2), Excel.Range).Columns().ColumnWidth = tam
                objHojaExcel.Range(objHojaExcel.Cells(r1), objHojaExcel.Cells(r2)).ColumnWidth = tam
            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub

#End Region

#Region "EVENTOS LIBRO"

        Private Sub objLibroExcel_BeforePrint(ByRef Cancel As Boolean) Handles objLibroExcel.BeforePrint
            Try
                'objLibroExcel.PrintOut(collate:=True)

                Cancel = True
            Catch ex As Exception
                LOG(ex.ToString)
            End Try
        End Sub

#End Region

#Region "FONDOS"

        Friend Sub MarcaDeAgua(ByVal texto As String, ByVal fila As Integer, ByVal columna As Integer)
            With objLibroExcel
                Dim Mud As Integer, dum As Object
                Mud = -60
                'objHojaExcel.Application.ScreenUpdating = False
                Dim Page As Integer
                .ActiveSheet.Shapes.AddTextEffect(MsoPresetTextEffect.msoTextEffect1, texto, _
                           "Arial", 32.0#, _
                           MsoTriState.msoFalse, _
                           MsoTriState.msoFalse, _
                           objHojaExcel.Range(objHojaExcel.Cells(fila, columna), objHojaExcel.Cells(fila, columna)).Left, _
                           objHojaExcel.Range(objHojaExcel.Cells(fila, columna), objHojaExcel.Cells(fila, columna)).Top).Select()

                objHojaExcel.Application.Selection.Name = "Dum"
                With objHojaExcel.Application.Selection
                    .ShapeRange.Fill.Visible = MsoTriState.msoTrue
                    .ShapeRange.Fill.Solid()
                    .ShapeRange.Fill.ForeColor.SchemeColor = 22
                    CType(.ShapeRange, Excel.ShapeRange).Fill.ForeColor.RGB = RGB(0, 255, 0)
                    .ShapeRange.Fill.Transparency = 0.5
                    .ShapeRange.Line.Visible = MsoTriState.msoFalse
                    .ShapeRange.IncrementRotation(-26.22)
                    .ShapeRange.IncrementLeft(Mud)
                    .ShapeRange.IncrementTop(200)

                    Mud = Mud + 432
                End With
            End With
            'objHojaExcel.Application.ScreenUpdating = True
            'm_Excel.Application.ScreenUpdating = True
        End Sub

#End Region

#Region "EXPORTAR DG A EXCEL"

        Friend Sub CrearExcel(ByVal dg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, _
                                        ByVal titulo As String, _
                                        ByVal vistaprevia As Boolean)
            Dim m_Excel As Excel.Application
            Dim i, j, z As Integer
            '' Creamos un objeto WorkBook 
            Dim objLibroExcel As Excel.Workbook
            '' Creamos un objeto WorkSheet
            Dim objHojaExcel As Excel.Worksheet
            Try

                m_Excel = New Excel.Application
                m_Excel.Visible = False

                '' Creamos una instancia del Workbooks de excel
                '' Creamos una instancia de la primera hoja de trabajo de excel
                objLibroExcel = m_Excel.Workbooks.Add()
                objHojaExcel = objLibroExcel.Worksheets(1)
                m_Excel.Application.ScreenUpdating = False
                m_Excel.Calculation = Excel.XlCalculation.xlCalculationManual
                objHojaExcel.Name = titulo
                objHojaExcel.Visible = Excel.XlSheetVisibility.xlSheetVisible
                '' Hacemos esta hoja la visible en pantalla 
                '' (como seleccionamos la primera esto no es necesario
                '' si seleccionamos una diferente a la primera si lo
                '' necesitariamos).
                objHojaExcel.Activate()
                objHojaExcel.Range("A1:D1").Merge()
                objHojaExcel.Range("A1:D1").Value = titulo
                objHojaExcel.Range("A1:D1").Font.Bold = True
                objHojaExcel.Range("A1:D1").Font.Size = 15

                z = 0
                For i = 0 To dg.Splits(0).DisplayColumns.Count - 1

                    If dg.Splits(0).DisplayColumns(i).Visible = True Then
                        objHojaExcel.Cells(3, z + 1).Font.Bold = True
                        objHojaExcel.Cells(3, z + 1).Value = dg.Splits(0).DisplayColumns(i).DataColumn.Caption
                        z = z + 1
                    End If
                Next

                For i = 0 To dg.Splits(0).Rows.Count - 1
                    Application.DoEvents()
                    z = 0
                    For j = 0 To dg.Splits(0).DisplayColumns.Count - 1
                        If dg.Splits(0).DisplayColumns(j).Visible = True Then
                            objHojaExcel.Cells(i + 5, z + 1) = dg.Item(i, dg.Splits(0).DisplayColumns(j).Name)
                            z = z + 1
                        End If

                    Next
                Next

                objHojaExcel.Cells.EntireColumn.AutoFit()
                '' Selecionado todo el rango especificado
                If vistaprevia Then
                    ' objLibroExcel.PrintPreview()
                Else

                End If

                objHojaExcel = Nothing
                objLibroExcel = Nothing
                m_Excel.Calculation = Excel.XlCalculation.xlCalculationAutomatic
                m_Excel.Visible = True
                m_Excel.ScreenUpdating = True
                GC.Collect()
                ' xls1.Save("prueba1.xls")
                ' abrirExcel()

            Catch ex1 As Exception
                MessageBox.Show(ex1.ToString)
            End Try

        End Sub

#End Region

    End Class

End Namespace