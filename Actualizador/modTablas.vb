Module modTablas

    Public w As IO.StreamWriter = IO.File.AppendText("errorActualizador.err")
    Public Sub LOG(ByVal logMensaje As String)
        w.Close()
        w = IO.File.AppendText("errores.err")
        w.Write(ControlChars.CrLf & "Entada de errores : ")
        w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString())
        w.WriteLine("  :")
        w.WriteLine("  :{0}", logMensaje)
        w.WriteLine("-------------------------------")
        ' Update the underlying file.
        w.Flush()
        w.Close()
    End Sub

End Module
