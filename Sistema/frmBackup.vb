Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class frmBackup
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
        '! Seal for less overhead - Can declare NotInheritable Class
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
    Friend WithEvents C1List1 As C1.Win.C1List.C1List
    Friend WithEvents C1CommandDock1 As C1.Win.C1Command.C1CommandDock
    Friend WithEvents Button1 As C1.Win.C1Input.C1Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBackup))
        Me.btnAceptar = New C1.Win.C1Input.C1Button()
        Me.Button1 = New C1.Win.C1Input.C1Button()
        Me.C1List1 = New C1.Win.C1List.C1List()
        Me.C1CommandDock1 = New C1.Win.C1Command.C1CommandDock()
        CType(Me.btnRecargar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnSiguiente,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnAnterior,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnPrimero,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnUltimo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnModificar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnTancar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnBorrar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnNuevo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnActualizar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnVerLista,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnAceptar,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.Button1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.C1List1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.C1CommandDock1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'sbtipo
        '
        Me.sbtipo.FlatStyle = System.Windows.Forms.FlatStyle.System
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
        Me.btnAceptar.UseVisualStyleBackColor = true
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = true
        '
        'C1List1
        '
        Me.C1List1.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark
        Me.C1List1.Images.Add(CType(resources.GetObject("C1List1.Images"),System.Drawing.Image))
        resources.ApplyResources(Me.C1List1, "C1List1")
        Me.C1List1.MatchEntryTimeout = CType(2000,Long)
        Me.C1List1.Name = "C1List1"
        Me.C1List1.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.C1List1.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.C1List1.PreviewInfo.ZoomFactor = 75R
        Me.C1List1.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.C1List1.PropBag = resources.GetString("C1List1.PropBag")
        '
        'C1CommandDock1
        '
        Me.C1CommandDock1.Id = 1
        resources.ApplyResources(Me.C1CommandDock1, "C1CommandDock1")
        Me.C1CommandDock1.Name = "C1CommandDock1"
        '
        'frmBackup
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.C1CommandDock1)
        Me.Controls.Add(Me.C1List1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnAceptar)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.ForeColor = System.Drawing.Color.SeaGreen
        Me.Name = "frmBackup"
        Me.Controls.SetChildIndex(Me.cboSeleccionCentro, 0)
        Me.Controls.SetChildIndex(Me.btnAceptar, 0)
        Me.Controls.SetChildIndex(Me.Button1, 0)
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
        Me.Controls.SetChildIndex(Me.C1List1, 0)
        Me.Controls.SetChildIndex(Me.C1CommandDock1, 0)
        CType(Me.btnRecargar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnSiguiente,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnAnterior,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnPrimero,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnUltimo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnModificar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnTancar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnBorrar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnNuevo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnActualizar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnVerLista,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnAceptar,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Button1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.C1List1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.C1CommandDock1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

#End Region

#Region "VARIABLES"

    Dim procMysqlDump As Process
    Dim procRar As Process

#End Region

#Region "ORGANIZAR"

    Private Sub procMysqlDumpExitEvent(ByVal sender As Object, ByVal e As System.EventArgs)

        procRar = Process.Start("c:\archivos de programa\sistema\winrar\rar.exe", "a -r c:\backup.rar c:\backup\")
        procMysqlDump.EnableRaisingEvents = True
        AddHandler procMysqlDump.Exited, AddressOf procRarExitEvent

    End Sub
    Private Sub procRarExitEvent(ByVal sender As Object, ByVal e As System.EventArgs)

        MessageBox.Show("Backup completado")

    End Sub
    Private Sub IniciarForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Cursor = Cursors.WaitCursor

            'Dim myMailServer As New Quiksoft.FreeSMTP.SMTP()
            'Dim mensaje As New Quiksoft.FreeSMTP.EmailMessage()

            'mensaje.Subject = "backup"
            'mensaje.BodyParts.Add("AgASFDASASF")
            'mensaje.From.Email = "yomismo@menta.net"
            'mensaje.Recipients.Add("sergioag@menta.net")

            'myMailServer.SMTPServers.Add("smtp.menta.net")
            'myMailServer.Send(mensaje)

            'mensaje = Nothing

        Catch ex As Exception

            LOG(ex.ToString) : cargando = False : CCN() ': ccn()

        End Try
        Cursor = Cursors.Default
    End Sub
    'Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
    '    Try

    '        Dim c As New MySql.Data.MySqlClient.MySqlDump
    '        c.Connection = cnn
    '        cnn.Open()
    '        c.UseDelayedIns = True
    '        c.IncludeDrop = True
    '        c.IncludeDatabase = True
    '        c.Backup()
    '        Dim stream As IO.StreamWriter = New IO.StreamWriter("\sqls\BACKUP.SQL")
    '        stream.WriteLine(c.DumpText)
    '        stream.Close()

    '        cnn.Close()

    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN()
    '    End Try

    'End Sub
    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Try

    '        Dim c As New MySql.Data.MySqlClient.MySqlDump
    '        c.Connection = cnn
    '        cnn.Open()
    '        c.UseDelayedIns = True
    '        c.IncludeDrop = True
    '        c.IncludeDatabase = True
    '        Dim stream As IO.StreamReader = New IO.StreamReader("\sqls\BACKUP.SQL")

    '        c.DumpText = stream.ReadToEnd()
    '        c.Restore()
    '        stream.Close()
    '        cnn.Close()

    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN()
    '    End Try

    'End Sub

#End Region

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
End Class

