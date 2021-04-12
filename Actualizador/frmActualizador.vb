Imports FtpSupport
Imports C1.C1Zip
Imports System.IO
Imports System.Xml
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports WebCamService.FtpClient

Public Class frmActulizador
    Inherits System.Windows.Forms.Form

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        Dim xmlDoc As Xml.XmlDocument = New Xml.XmlDocument
        Dim tempnode As Xml.XmlNode
        Dim pathOrigen As String
        Dim pathDestino As String

        Try

            directorio = CurDir() & "\"
            xmlDoc.Load(CurDir() & "\conf\config.xml")
            
            For Each tempnode In xmlDoc.DocumentElement.ChildNodes
                Select Case tempnode.Name
                    Case "pathOrigen"
                        pathOrigen = tempnode.InnerText
                    Case "pathDestino"
                        pathDestino = tempnode.InnerText
                End Select
            Next
            RecursiveDirectoryCopy(pathOrigen, pathDestino, True, True)
        Catch ex As Exception
            LOG(ex.ToString)
        End Try

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub
    Public directorio As String
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
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        '
        'frmActulizador
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(636, 110)
        Me.Name = "frmActulizador"
        Me.Text = "ACTUALITZADOR"

    End Sub

#End Region

#Region "VARIABLES"

    Private fcargando As frCargando
    Private mHOST As String = "81.44.253.67"

    Public Property host()
        Get
            Return mHOST
        End Get
        Set(ByVal Value)
            mHOST = Value
        End Set
    End Property

#End Region

#Region "ORGANIZAR"

    Private Sub CopiarDeFTP(ByVal path As String, ByVal pathdestino As String)
        'Dim ftp As New FtpSupport.FtpConnection("81.44.253.67:3021", "intrad", "J7V21R54")
        Try
            Dim ftp As New FtpSupport.FtpConnection("81.44.253.67", "sergio", "sergio314")

            'Dim ftp As New WebCamService.FtpClient("81.44.253.67", "sergio", "sergio314")
            ftp.Connect("81.44.253.67", "sergio", "sergio314")
            ftp.GetFile("tex.net.zip", "tex.net.zip", False, FileAttributes.Compressed)
            'ftp.ChangeDir("/ftp/T:/FTP/")
            'ftp.Download("tex.net.zip", "tex.net.zip")
            'CopiarATemp(path, ftp, pathdestino)
            ftp.Close()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub CopiarATemp(ByVal path As String, ByVal ftp As FtpSupport.FtpConnection, ByVal pathdestino As String)
        Dim cosa() As FtpSupport.Win32FindData
        Dim enu As IEnumerator
        Try
            ftp.SetCurrentDirectory(path)
            cosa = ftp.FindFiles("*.*")
            enu = cosa.GetEnumerator
            While enu.MoveNext
                With DirectCast(enu.Current, Win32FindData)
                    If Not .FileName = "." AndAlso .FileName <> ".." Then
                        If .FileAttributes = FileAttributes.Directory Then
                            If .FileName = "Editar1" Then
                                If 0 = 0 Then
                                End If
                            End If
                            If pathdestino.Substring(pathdestino.Length - 1, 1) = "\" Then
                            Else
                                pathdestino = pathdestino & "\"
                            End If
                            If path.Substring(path.Length - 1, 1) = "\" Then
                            Else
                                path = path & "\"
                            End If
                            Directory.CreateDirectory(pathdestino & .FileName)
                            CopiarATemp(path & .FileName, ftp, pathdestino & .FileName)
                        Else
                            If pathdestino.Substring(pathdestino.Length - 1, 1) = "\" Then
                            Else
                                pathdestino = pathdestino & "\"
                            End If
                            ftp.SetCurrentDirectory(path)
                            If Not .FileName = "actualizador.exe" And Not .FileName = "actualizador.pdb" Then
                                ftp.GetFile(.FileName, pathdestino & .FileName, False, .FileAttributes)
                            End If
                        End If
                    End If

                End With
            End While

        Catch ex As Exception
            LOG(ex.ToString)
        End Try

    End Sub
    Private Function ExpandFile(ByVal dstFile As String, ByVal srcFile As String) As Boolean

        ' prepare to expand file
        Dim retval As Boolean = True
        Dim srcStream As FileStream
        Dim dstStream As FileStream
        Try
            ' open the files
            srcStream = New FileStream(srcFile, FileMode.Open, FileAccess.Read)
            Dim sr As C1ZStreamReader = New C1ZStreamReader(srcStream)
            dstStream = New FileStream(dstFile, FileMode.Create, FileAccess.Write)

            ' open expander stream on compressed source


            ' copy expander stream into destination file
            StreamCopy(dstStream, sr)

        Catch ' exception? tell caller we failed

            retval = False

        Finally ' always close our streams

            If Not (srcStream Is Nothing) Then srcStream.Close()
            If Not (dstStream Is Nothing) Then dstStream.Close()
        End Try

        ' done
        ExpandFile = retval
    End Function
    Private Sub StreamCopy(ByVal dstStream As Stream, ByVal srcStream As Stream)
        Dim buffer(32768) As Byte
        Dim read As Integer
        Do
            read = srcStream.Read(buffer, 0, buffer.Length)
            If read = 0 Then Exit Do
            dstStream.Write(buffer, 0, read)
        Loop
        dstStream.Flush()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim nuevothread As New Process
        nuevothread.Start("TEX.NET.exe")
        Me.Close()
        '    Dim NavArchivos As System.Windows.Forms.OpenFileDialog
        '    Dim Fs As System.IO.FileStream
        '    Dim fsi As FileSystemInfo
        '    Dim xmlDoc As New XmlDocument
        '    Dim tempnode As XmlNode
        '    Dim pathOrigen As String
        '    Dim pathDestino As String
        '    Dim pathAplicacion As String

        '    Try
        '        'Dim thread As Threading.Thread = MostrarCargando()
        '        xmlDoc.Load("conf\config.xml")
        '        'host = txtHOST.Text
        '        For Each tempnode In xmlDoc.DocumentElement.ChildNodes
        '            Select Case tempnode.Name
        '                Case "pathOrigen"
        '                    pathOrigen = tempnode.InnerText
        '                Case "pathDestino"
        '                    pathDestino = tempnode.InnerText
        '                Case "pathAplicacion"
        '                    pathAplicacion = tempnode.InnerText
        '            End Select
        '        Next
        '        If pathDestino = "" Or pathOrigen = "" Then
        '            MessageBox.Show("config.xml Incorrecte")
        '            Me.Close()
        '        End If
        '        Directory.CreateDirectory("temp")
        '        CopiarDeFTP(pathOrigen, pathDestino)

        '        Dim m_Zip As C1ZipFile
        '        m_Zip = New C1ZipFile
        '        m_Zip.Open("tex.net.zip")
        '        Dim i As Integer
        '        For i = 0 To m_Zip.Entries.Count - 1
        '            m_Zip.Entries.Extract(i, pathDestino & "\" & m_Zip.Entries(i).FileName)
        '        Next

        '        ExpandFile(pathDestino, "tex.net.zip")
        '        Try
        '            RecursiveDirectoryCopy(pathDestino, pathAplicacion, True, True)
        '        Catch ex As Exception
        '        End Try
        '        Directory.Delete("temp", True)
        '        'thread.Abort()
        '        MessageBox.Show("ACTUALITZACIÓ COMPLETADA", "Informació", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        Dim nuevothread As New Process
        '        nuevothread.Start("tex.net.exe")

        '        Me.Close()
        '    Catch ex As Exception
        '        LOG(ex.ToString)
        '    End Try
    End Sub
    Private Function MostrarCargando() As Threading.Thread
        Try
            Dim nuevothread As New Threading.Thread(AddressOf CargandoFormulario)
            'nuevothread.Priority = Threading.ThreadPriority.BelowNormal
            nuevothread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentUICulture
            nuevothread.Start()
            Return nuevothread

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function
    Private Sub CargandoFormulario()
        Try
            fcargando = New frCargando
            fcargando.ShowDialog()
        Catch ex As Exception
            LOG(ex.ToString)

        End Try
    End Sub

    ' Usage: 
    ' Copy Recursive with Overwrite if exists. 
    ' RecursiveDirectoryCopy("C:\Data", "D:\Data", True, True) 
    ' Copy Recursive without Overwriting. 
    ' RecursiveDirectoryCopy("C:\Data", "D:\Data", True, False) 
    ' Copy this directory Only. Overwrite if exists. 
    ' RecursiveDirectoryCopy("C:\Data", "D:\Data", False, True) 
    ' Copy this directory only without overwriting. 
    ' RecursiveDirectoryCopy("C:\Data", "D:\Data", False, False) 

    ' Recursively copy all files and subdirectories from the specified source to the specified 
    ' destination. 
    Private Sub RecursiveDirectoryCopy(ByVal sourceDir As String, ByVal destDir As String, ByVal fRecursive As Boolean, ByVal overWrite As Boolean)

        Dim sDir As String
        Dim dDirInfo As IO.DirectoryInfo
        Dim sDirInfo As IO.DirectoryInfo
        Dim sFile As String
        Dim sFileInfo As IO.FileInfo
        Dim dFileInfo As IO.FileInfo
        ' Add trailing separators to the supplied paths if they don't exist. 
        If Not sourceDir.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()) Then
            sourceDir &= System.IO.Path.DirectorySeparatorChar
        End If
        If Not destDir.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()) Then
            destDir &= System.IO.Path.DirectorySeparatorChar
        End If
        'If destination directory does not exist, create it. 
        dDirInfo = New System.IO.DirectoryInfo(destDir)
        If dDirInfo.Exists = False Then dDirInfo.Create()
        dDirInfo = Nothing
        ' Recursive switch to continue drilling down into directory structure. 
        If fRecursive Then
            ' Get a list of directories from the current parent. 
            For Each sDir In System.IO.Directory.GetDirectories(sourceDir)
                sDirInfo = New System.IO.DirectoryInfo(sDir)
                dDirInfo = New System.IO.DirectoryInfo(destDir & sDirInfo.Name)
                ' Create the directory if it does not exist. 
                If dDirInfo.Exists = False Then dDirInfo.Create()
                ' Since we are in recursive mode, copy the children also 
                RecursiveDirectoryCopy(sDirInfo.FullName, dDirInfo.FullName, fRecursive, overWrite)
                sDirInfo = Nothing
                dDirInfo = Nothing
            Next
        End If
        ' Get the files from the current parent. 
        For Each sFile In System.IO.Directory.GetFiles(sourceDir)
            sFileInfo = New System.IO.FileInfo(sFile)
            dFileInfo = New System.IO.FileInfo(Replace(sFile, sourceDir, destDir))
            'If File does not exist. Copy. 
            If dFileInfo.Exists = False Then
                Try
                    sFileInfo.CopyTo(dFileInfo.FullName, overWrite)
                Catch ex As Exception
                    LOG(ex.ToString)
                End Try
            Else
                'If file exists and is the same length (size). Skip. 
                'If file exists and is of different Length (size) and overwrite = True. Copy 
                'If sFileInfo.Length <> dFileInfo.Length AndAlso overWrite Then
                'sFileInfo.CopyTo(dFileInfo.FullName, overWrite)
                ''If file exists and is of different Length (size) and overwrite = False. Skip 
                'ElseIf sFileInfo.Length <> dFileInfo.Length AndAlso Not overWrite Then
                '    Debug.WriteLine(sFileInfo.FullName & " Not copied.")
                If sFileInfo.LastWriteTime > dFileInfo.LastWriteTime AndAlso overWrite Then
                    Try
                        sFileInfo.CopyTo(dFileInfo.FullName, overWrite)
                    Catch ex As Exception
                        LOG(ex.ToString)
                    End Try
                    'If file exists and is of different Length (size) and overwrite = False. Skip 
                ElseIf sFileInfo.LastWriteTime > dFileInfo.LastWriteTime AndAlso Not overWrite Then
                    Debug.WriteLine(sFileInfo.FullName & " No copiado.")
                End If
            End If
            sFileInfo = Nothing
            dFileInfo = Nothing
        Next

    End Sub

#End Region

End Class
