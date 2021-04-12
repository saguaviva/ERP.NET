Imports MySql.Data.MySqlClient : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes


Public Class clsFuncionesLOG

    Public Shared w As IO.StreamWriter = IO.File.AppendText("errores" & Environment.UserName.Replace("\", "") & ".err")
    'Public Shared rm As New Resources.ResourceManager("Resource", GetType(frmPrincipal).Assembly)
    'ublic Shared rmImp As New Resources.ResourceManager("ResourceImp", GetType(frmPrincipal).Assembly)
    Public Shared rm As New Resources.ResourceManager("Resource", GetType(aura2k3.frmBase).Assembly)
    Public Shared rmImp As New Resources.ResourceManager("ResourceImp", GetType(aura2k3.frmBase).Assembly)
    Public general As New clsFuncionesUtiles

    

    Shared Sub LOG(ByVal logMensaje As String, Optional ByVal soloEnLOG As Boolean = False)
        Try
            w.Close()
            If logMensaje.IndexOf("ACTUALIZANDO BD: ") > -1 Then
                'w = IO.File.AppendText("actualizar.act")
                'w.Write(ControlChars.CrLf)
                'w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString())
                'w.WriteLine("  :")
                'w.WriteLine("  :{0}", logMensaje)
                'w.WriteLine("-------------------------------")
                'w.Flush()
                'w.Close()
                If modoDebug = True Then MessageBox.Show(logMensaje)
            Else
                w = IO.File.AppendText("errores" & My.User.Name.Replace("\", "") & ".err")
                w.Write(ControlChars.CrLf & "Entada de errores : ")
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString())
                w.WriteLine("  :")
                w.WriteLine("  :{0}", logMensaje)
                w.WriteLine("-------------------------------")
                w.Flush()
                w.Close()
                MessageBox.Show(logMensaje)
            End If

            'If modoDebug = True Then MessageBox.Show(logMensaje)

            'EnviarABD(logMensaje)


        Catch ex As Exception
            'MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Declare Function apiGetComputerName Lib "kernel32" Alias "GetComputerNameA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long


    Private Sub EnviarABD(ByVal log As String)
        Dim cmd As MySql.Data.MySqlClient.MySqlCommand
        Try
            cmd = New MySql.Data.MySqlClient.MySqlCommand("INSERT INTO ERRORES (FORMULARIO, FECHA, ERROR) VALUES (""" & general.NS(log) & """, """ & ConvertirAfechaMysql22(DateTime.Now) & """, """ & general.NS(System.Net.Dns.GetHostName) & """ )", cnn)
            ACN()
            cmd.ExecuteNonQuery()
            CCN()


        Catch ex As Exception
        End Try
    End Sub
    Friend Function ConvertirAfechaMysql22(ByVal fecha As Date) As String
        Try
            Return fecha.Year & "-" & fecha.Month & "-" & fecha.Day & " " & fecha.Hour & ":" & fecha.Minute & ":" & fecha.Second

        Catch ex As Exception
            clsFuncionesLOG.LOG(ex.ToString) : CCN() : Return ""
        End Try
    End Function
    Shared Sub comprobarlog()
        Dim r1 As IO.File
        Try
            If w.BaseStream.Length > 5242880 Then
                w.Close()
                r1.Delete("errores" & My.User.Name.Replace("\", "") & ".err")
                w = IO.File.AppendText("errores" & My.User.Name.Replace("\", "") & ".err")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    '    'Public Function DumpclsFuncionesLOG.LOG() As String
    '    '    ' While not at the end of the file, read and write lines.
    '    '    Dim line As String
    '    '    Dim s As String
    '    '    w = Nothing
    '    '    Dim r As New IO.StreamReader("error.err")
    '    '    line = r.ReadLine()
    '    '    While Not line Is Nothing
    '    '        Console.WriteLine(line)
    '    '        line = r.ReadLine()
    '    '        s = s & r.ReadLine
    '    '    End While
    '    '    r.Close()
    '    '    r = Nothing
    '    '    w = IO.File.AppendText("error.err")
    '    '    Return s
    '    'End Function

End Class
