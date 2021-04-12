Module abrirCosas

    Private Declare Function GetDesktopWindow Lib "user32" () As Long

    Private Declare Function ShellExecute Lib "shell32" _
        Alias "ShellExecuteA" (ByVal hwnd As Long, ByVal lpOperation As String, _
        ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, _
        ByVal nShowCmd As Long) As Long

    Friend Const SW_SHOWNORMAL As Long = 1
    Friend Const SW_SHOWMAXIMIZED As Long = 3
    Friend Const SW_SHOWDEFAULT As Long = 10
    Friend Const SE_ERR_NOASSOC As Long = 31

    Public Sub RunShellExecute(ByVal sTopic As String, ByVal sFile As Object, _
    ByVal sParams As Object, ByVal sDirectory As Object, ByVal nShowCmd As Long)

        Dim hWndDesk As Long
        Dim success As Long

        'the desktop will be the
        'default for error messages
        hWndDesk = GetDesktopWindow()

        'execute the passed operation
        success = ShellExecute(hWndDesk, sTopic, sFile, sParams, sDirectory, nShowCmd)

        'This is optional. Uncomment the three lines
        'below to have the "Open With.." dialog appear
        'when the ShellExecute API call fails
        If success = SE_ERR_NOASSOC Then
            Call Shell("rundll32.exe shell32.dll,OpenAs_RunDLL " & sFile, vbNormalFocus)
        End If

    End Sub
    Private Sub Command2_Click(ByVal Index As Integer)
        Dim sTopic As String
        Dim sFile As String
        Dim sParams As Object
        Dim sDirectory As Object
        Select Case Index
            Case 0 'send email to address using default email app
                sTopic = "Open"
                sFile = "mailto:me@123.com"
                sParams = 0&
                sDirectory = 0&

            Case 1 'open default browser to specified site
                sTopic = "Open"
                sFile = "http://vbnet.mvps.org/"
                sParams = 0&
                sDirectory = 0&

            Case 2 'open specified browser to specified site
                sTopic = "Open"
                sFile = "C:\Program Files\Internet Explorer\iexplore.exe"
                sParams = "http://vbnet.mvps.org/"
                sDirectory = 0&

            Case Else
        End Select

        Call RunShellExecute(sTopic, sFile, sParams, sDirectory, SW_SHOWNORMAL)

    End Sub

End Module
