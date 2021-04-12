Public Class frmPedirDato
    Inherits System.Windows.Forms.Form

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

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
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents txtTextoRet As C1.Win.C1Input.C1TextBox
    Friend WithEvents btnAceptar As C1.Win.C1Input.C1Button
    Friend WithEvents lblTexto As C1.Win.C1Input.C1Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtTextoRet = New C1.Win.C1Input.C1TextBox
        Me.btnAceptar = New C1.Win.C1Input.C1Button
        Me.lblTexto = New C1.Win.C1Input.C1Label
        CType(Me.txtTextoRet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTexto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTextoRet
        '
        Me.txtTextoRet.Location = New System.Drawing.Point(8, 48)
        Me.txtTextoRet.Name = "txtTextoRet"
        Me.txtTextoRet.Size = New System.Drawing.Size(328, 20)
        Me.txtTextoRet.TabIndex = 0
        Me.txtTextoRet.Tag = Nothing
        '
        'btnAceptar
        '
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAceptar.Location = New System.Drawing.Point(264, 120)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(80, 24)
        Me.btnAceptar.TabIndex = 304
        Me.btnAceptar.Text = "Acceptar"
        Me.btnAceptar.Visible = False
        '
        'lblTexto
        '
        Me.lblTexto.Location = New System.Drawing.Point(8, 16)
        Me.lblTexto.Name = "lblTexto"
        Me.lblTexto.Size = New System.Drawing.Size(328, 23)
        Me.lblTexto.TabIndex = 305
        Me.lblTexto.Tag = Nothing
        '
        'frmPedirDato
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(352, 158)
        Me.Controls.Add(Me.lblTexto)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.txtTextoRet)
        Me.Name = "frmPedirDato"
        CType(Me.txtTextoRet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTexto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
