'Imports CoreLab.MySql
'Public Class clsFuncionesUtilies

'    Friend cnn As New CoreLab.MySql.MySqlConnection
'    Friend monitor As CoreLab.MySql.MySqlMonitor


'#Region "CONEXION"

'    Friend Sub ACN()
'        Try
'            If Not cnn.State = ConnectionState.Open Then : cnn.Open() : End If

'        Catch ex As Exception
'            LOG(ex.ToString)
'        End Try
'    End Sub
'    Friend Sub ccn()
'        Try

'            If Not cnn.State = ConnectionState.Closed Then : cnn.Close() : End If

'        Catch ex As Exception
'            LOG(ex.ToString)
'        End Try
'    End Sub

'#End Region

'#Region "PARAMETROS"

'    Friend Sub añadirParametroIdentificador(ByRef cmd As MySqlCommand, ByVal tipo As String, ByVal campo As String)

'        Select Case tipo
'            Case "double" : cmd.Parameters.Add(New MySqlParameter("pi" & campo, MySqlType.Double, 0, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), campo, System.Data.DataRowVersion.Original, Nothing))
'            Case "varchar" : cmd.Parameters.Add(New MySqlParameter("pi" & campo, MySqlType.VarChar, 0, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), campo, System.Data.DataRowVersion.Original, Nothing))
'            Case "int" : cmd.Parameters.Add(New MySqlParameter("pi" & campo, MySqlType.Int, 0, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), campo, System.Data.DataRowVersion.Original, Nothing))
'            Case "integer" : cmd.Parameters.Add(New MySqlParameter("pi" & campo, MySqlType.Int, 0, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), campo, System.Data.DataRowVersion.Original, Nothing))
'            Case "tinyint" : cmd.Parameters.Add(New MySqlParameter("pi" & campo, MySqlType.TinyInt, 0, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), campo, System.Data.DataRowVersion.Original, Nothing))
'            Case "date" : cmd.Parameters.Add(New MySqlParameter("pi" & campo, MySqlType.Date, 0, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), campo, System.Data.DataRowVersion.Original, Nothing))
'            Case "text" : cmd.Parameters.Add(New MySqlParameter("pi" & campo, MySqlType.Text, 0, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), campo, System.Data.DataRowVersion.Original, Nothing))
'            Case "tinyintunsigned" : cmd.Parameters.Add(New MySqlParameter("pi" & campo, MySqlType.TinyInt, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), campo, System.Data.DataRowVersion.Original, Nothing))
'            Case "blob" : cmd.Parameters.Add(New MySqlParameter("pi" & campo, MySqlType.Blob, 0, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), campo, System.Data.DataRowVersion.Original, Nothing))
'        End Select

'    End Sub
'    Friend Sub añadirParametro(ByRef cmd As MySqlCommand, ByVal tipo As String, ByVal campo As String)

'        Select Case tipo
'            Case "double" : cmd.Parameters.Add(New MySqlParameter("p" & campo, MySqlType.Double, 0, campo))
'            Case "varchar" : cmd.Parameters.Add(New MySqlParameter("p" & campo, MySqlType.VarChar, 0, campo))
'            Case "int" : cmd.Parameters.Add(New MySqlParameter("p" & campo, MySqlType.Int, 0, campo))
'            Case "integer" : cmd.Parameters.Add(New MySqlParameter("p" & campo, MySqlType.Int, 0, campo))
'            Case "tinyint" : cmd.Parameters.Add(New MySqlParameter("p" & campo, MySqlType.TinyInt, 0, campo))
'            Case "date" : cmd.Parameters.Add(New MySqlParameter("p" & campo, MySqlType.Date, 0, campo))
'            Case "text" : cmd.Parameters.Add(New MySqlParameter("p" & campo, MySqlType.Text, 0, campo))
'            Case "tinyintunsigned" : cmd.Parameters.Add(New MySqlParameter("p" & campo, MySqlType.TinyInt, 1, campo))
'            Case "blob" : cmd.Parameters.Add(New MySqlParameter("p" & campo, MySqlType.Blob, 0, campo))
'        End Select

'    End Sub

'#End Region

'#Region "ORGANIZAR"
'    Private Declare Function GetSystemMenu Lib "user32" Alias _
'            "GetSystemMenu" (ByVal hwnd As Long, ByVal bRevert As Long) _
'            As Long
'    Private Declare Function RemoveMenu Lib "user32" Alias "RemoveMenu" _
'            (ByVal hMenu As Long, ByVal nPosition As Long, _
'            ByVal wFlags As Long) As Long
'    Private Const MF_BYPOSITION = &H400&


'    Friend Function TipoNET2SQL(ByVal tipo As System.Type) As String
'        Try
'            Select Case tipo.FullName.ToString
'                Case "System.String" : Return "varchar"
'                Case "System.Char" : Return "varchar"
'                Case "System.Integer" : Return "integer"
'                Case "System.Int64" : Return "integer"
'                Case "System.Int32" : Return "integer"
'                Case "System.Int16" : Return "integer"
'                Case "System.Date" : Return "date"
'                Case "System.DateTime" : Return "date"
'                Case "System.Double" : Return "double"
'                Case "System.Decimal" : Return "double"
'                Case "System.Single" : Return "double"
'                Case "System.Boolean" : Return "tinyintunsigned"
'                Case "System.Byte" : Return "tinyintunsigned"
'                Case "System.Byte[]" : Return "blob"
'            End Select
'            MessageBox.Show("TIPO " & tipo.FullName.ToString)

'        Catch ex As Exception
'            LOG(ex.ToString) : ccn()
'        End Try
'    End Function
'    Friend Function esPrimaryKey(ByVal cons As System.Data.UniqueConstraint, ByVal columna As String) As Boolean
'        Dim e As IEnumerator
'        Try
'            e = cons.Columns.GetEnumerator
'            While e.MoveNext
'                If String.Compare(columna, e.Current.Caption, True) = 0 Then
'                    Return True
'                End If
'            End While

'            Return False

'        Catch ex As Exception
'            LOG(ex.ToString) : ccn()
'        End Try
'    End Function
'    Friend Function NS(ByVal str As Object, Optional ByVal esNumero As Boolean = False) As String
'        Try
'            If Not IsDBNull(str) AndAlso Not str Is Nothing Then
'                str = CType(str, String)
'                str = str.Replace("""", "\""")
'                str = str.Replace(":", "::")
'                If esNumero Then
'                    str = str.Replace(",", ".")
'                End If
'                Return str
'            End If
'        Catch ex As Exception
'            LOG(ex.ToString) : ccn()
'        End Try
'    End Function
'    Friend Sub AñadirBinding(ByVal c As Object, ByVal dv As DataView, ByVal campo As String, Optional ByVal elementoBinding As String = "Text")
'        Try
'            c.databindings.add(New Binding(elementoBinding, dv, campo))
'        Catch ex As Exception
'            LOG(ex.ToString) : ccn()
'        End Try
'    End Sub
'    Friend Sub AñadirBindingCombo(ByVal c As Object, ByVal dt As Object, _
'                                    ByVal valuemember As String, _
'                                    ByVal DisplayMember As String)
'        Try
'            With c
'                .DataSource = dt
'                .DisplayMember = DisplayMember
'                .ValueMember = valuemember
'            End With
'            Try
'                OcultarMostrarColumnaCbo(c, "CENTRO", False)
'            Catch ex As Exception

'            End Try

'        Catch ex As Exception
'            LOG(ex.ToString) : ccn()
'        End Try
'    End Sub
'    'Le pasamos el nombre de la lista y nos devuelve el nombre del campo
'    Friend Function ponercontrabarrasreal(ByRef valorcampo As String) As Object
'        Try
'            If valorcampo Is Nothing Then
'                Return Nothing
'            End If
'            valorcampo = nz(valorcampo.Replace("""", "\"""), "")
'            Return valorcampo
'        Catch ex As Exception
'            LOG(ex.ToString) : ccn()
'        End Try
'    End Function
'    Function nzn(ByVal str As Object, ByVal defecto As Object) As Double
'        Try
'            If str Is Nothing OrElse IsDBNull(str) Then
'                Return defecto
'            Else
'                If Not IsNumeric(str) Then
'                    Return defecto
'                Else
'                    Return str
'                End If
'            End If
'        Catch ex As Exception
'            LOG(ex.ToString)
'        End Try
'    End Function
'    Function nz(ByVal str As Object, ByVal defecto As Object, Optional ByVal numerico As Boolean = False) As Object
'        Try
'            If numerico = True Then
'                If str Is Nothing OrElse IsDBNull(str) OrElse str = "" Then
'                    Return defecto
'                Else : Return str
'                End If
'            Else
'                If str Is Nothing OrElse IsDBNull(str) Then
'                    Return defecto
'                Else : Return str
'                End If
'            End If
'        Catch ex As Exception
'            LOG(ex.ToString)
'        End Try
'    End Function
'    Friend Function OBN(ByVal codi As Object, ByVal dt As DataTable, Optional ByVal nombre As String = "NOM", Optional ByVal IDSORT As String = "CODI") As String
'        Try
'            Dim dv As New DataView
'            dv = dt.DefaultView
'            dv.Sort = IDSORT
'            Dim i As Integer = dv.Find(codi)
'            If i > -1 Then
'                Return nzN(dv(i).Item(nombre), 0)
'            Else
'                Return Nothing
'            End If
'        Catch ex As Exception
'            LOG(ex.ToString) : ccn()
'        End Try
'    End Function
'    Function esCampoDeNombre(ByVal campo As String) As Boolean
'        Try
'            Select Case campo
'                Case "NOMACABADOR" : Return True
'                Case "NOMTEIXIDOR" : Return True
'                Case "NOMPROVE" : Return True
'                Case "NOMCLIENT" : Return True
'                Case "NOMESTAMPADOR" : Return True
'                Case "NOMREPRES" : Return True
'                Case "NOMTRANS" : Return True
'                Case "NOMTALLER" : Return True
'                Case "NOMTEJEDOR" : Return True
'                Case "NOMCONFEC" : Return True
'                Case "NOMTINT" : Return True
'                Case "NOMACA" : Return True
'                Case "NOMESTAM" : Return True
'                Case "NOMTINT" : Return True
'                Case "NOMMAQUI" : Return True
'                Case "NOMBANC" : Return True
'                Case "NOMIVA" : Return True
'                Case "NOMFORMA" : Return True
'                Case "NOMFILIAL" : Return True
'            End Select
'            Return False

'        Catch ex As Exception
'            LOG(ex.ToString) : ccn()
'        End Try
'    End Function
'    Friend Function ConvertirAfechaMysql(ByVal fecha As Date) As String
'        Try
'            Return fecha.Year & "-" & fecha.Month & "-" & fecha.Day
'        Catch ex As Exception
'            LOG(ex.ToString) : ccn()
'        End Try
'    End Function
'    Function GetCampo(ByVal tabla As String, ByVal campo As String, ByVal id1 As Object, ByVal tipoid1 As String, Optional ByVal id2 As String = "", Optional ByVal id3 As String = "", Optional ByVal id4 As String = "") As Object

'        Try
'            'Ahora la funcion solo vale si el identificador es codi
'            Dim cmdSel As New MySqlCommand

'            'cn2 = cnn.ConnectionString
'            cmdSel.Connection = cnn
'            If Not IsDBNull(id1) Then
'                Select Case tipoid1
'                    Case "double" : id1 = CType(id1, System.Double)
'                    Case "string" : id1 = CType(id1, System.String)
'                    Case "integer" : id1 = CType(id1, System.Int32)
'                    Case "text" : id1 = CType(id1, System.String)
'                End Select
'                cmdSel.CommandText = "SELECT " & campo & " FROM " & tabla & " WHERE (CODI = """ & id1 & """)"
'                'ccn()
'                ACN()
'                GetCampo = cmdSel.ExecuteScalar
'                ccn()
'            Else
'                Return ""
'            End If
'        Catch ex As Exception
'            LOG(ex.ToString) : ccn()
'        End Try
'    End Function
'    Friend Function sumaTotal(ByVal campo As String, ByVal dv As DataView) As Double
'        Dim i As Integer
'        Dim suma As Double
'        Try
'            For i = 0 To dv.Count - 1
'                suma = suma + nzn(dv(i).Item(campo), 0)
'            Next
'            Return nzn(suma, 0)
'        Catch ex As Exception
'            LOG(ex.ToString)
'            ccn()
'        End Try
'    End Function


'#End Region

'#Region "CARGAR"

'    Friend Sub CargarTablas(ByVal aTablaOriginal As ArrayList, ByVal aCampoCODIGO As ArrayList, ByVal aCampoNOMBRE As ArrayList, ByRef aDT As ArrayList, ByVal nocerrarconexion As Boolean)
'        Dim cmdSel As New MySqlCommand
'        Dim dar As MySqlDataReader
'        Dim SQL As String
'        Dim i As Integer
'        Dim dr As DataRow
'        Try
'            cmdSel.Connection = cnn
'            ACN()
'            For i = 0 To aTablaOriginal.Count - 1
'                SQL = "SELECT DISTINCT " & aCampoCODIGO(i) & ", " & aCampoNOMBRE(i) & "  FROM " & aTablaOriginal(i) & "  ORDER BY " & aCampoNOMBRE(i) & ""
'                cmdSel.CommandText = SQL
'                dar = cmdSel.ExecuteReader
'                DirectCast(aDT(i), DataTable).Clear()
'                Try
'                    DirectCast(aDT(i), DataTable).Columns.Add(New DataColumn(aCampoCODIGO(i)))
'                    DirectCast(aDT(i), DataTable).Columns.Add(New DataColumn(aCampoNOMBRE(i)))

'                Catch ex As Exception

'                End Try


'                While dar.Read
'                    dr = CType(aDT(i), DataTable).NewRow
'                    dr(aCampoCODIGO(i)) = dar(aCampoCODIGO(i))
'                    dr(aCampoNOMBRE(i)) = dar(aCampoNOMBRE(i))
'                    CType(aDT(i), DataTable).Rows.Add(dr)
'                End While
'                dar.Close()
'            Next
'            If Not cnn.State = ConnectionState.Closed AndAlso Not nocerrarconexion Then
'                cnn.Close()
'            End If

'        Catch ex As Exception
'            LOG(ex.ToString) : ccn()
'        End Try
'    End Sub

'    'A este cargatabla le pasamos un arraylist campos y genera automaticamente los campos que necesitamos
'    Friend Sub CargaTabla(ByVal tablaOriginal As String, ByVal campos As ArrayList, ByRef dt As DataTable, ByVal nocerrar As Boolean)
'        Dim cmdSelect As New MySqlCommand
'        Dim daSelect As New MySqlDataAdapter
'        Try
'            cmdSelect.Connection = cnn

'            cmdSelect.CommandText = "SELECT DISTINCT CENTRO "
'            Dim i As Integer
'            For i = 0 To campos.Count - 1
'                cmdSelect.CommandText = cmdSelect.CommandText & "," & campos(i) & " "
'            Next
'            cmdSelect.CommandText = cmdSelect.CommandText & " FROM " & tablaOriginal & " "

'            daSelect.SelectCommand = cmdSelect
'            ACN()
'            dt.Rows.Clear()
'            daSelect.Fill(dt)
'            If Not cnn.State = ConnectionState.Closed AndAlso Not nocerrar Then
'                cnn.Close()
'            End If

'        Catch ex As Exception
'            LOG(ex.ToString)
'        End Try
'    End Sub

'    Friend Sub CargaTabla(ByVal tablaOriginal As String, ByVal campoCODIGO As String, ByVal campoNOMBRE As String, ByRef dt As DataTable, Optional ByVal nocerrar As Boolean = False, Optional ByVal centr As String = empresaPorDefecto)
'        Dim cmdSelect As New MySqlCommand
'        Dim daSelect As New MySqlDataAdapter
'        Try
'            cmdSelect.Connection = cnn
'            cmdSelect.CommandText = "SELECT DISTINCT " & campoCODIGO & ", " & _
'                        " " & campoNOMBRE & ", CENTRO  " & _
'                        " FROM " & tablaOriginal & "  " '& _

'            daSelect.SelectCommand = cmdSelect
'            ACN()
'            dt.Rows.Clear()
'            daSelect.Fill(dt)
'            If Not cnn.State = ConnectionState.Closed AndAlso Not nocerrar Then
'                cnn.Close()
'            End If
'        Catch ex As Exception
'            LOG(ex.ToString) : ccn()
'        End Try
'    End Sub
'    'Version del cargaTabla con el sql
'    Friend Sub CargaTabla(ByVal tablaOriginal As String, ByRef dt As DataTable, ByVal sql As String, Optional ByVal nocerrar As Boolean = False)
'        Dim cmdSelect As New MySqlCommand
'        Dim daSelect As New MySqlDataAdapter
'        Try
'            cmdSelect.Connection = cnn
'            cmdSelect.CommandText = sql

'            daSelect.SelectCommand = cmdSelect
'            ACN()
'            dt.Rows.Clear()
'            daSelect.Fill(dt)
'            If Not cnn.State = ConnectionState.Closed AndAlso Not nocerrar Then
'                cnn.Close()
'            End If
'        Catch ex As Exception
'            LOG(ex.ToString) : ccn()
'        End Try
'    End Sub

'#End Region

'End Class
