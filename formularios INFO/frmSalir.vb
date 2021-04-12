Public Class frmSalir
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
    Friend WithEvents lblVolstancar As C1.Win.C1Input.C1Label
    Friend WithEvents btnNO As C1.Win.C1Input.C1Button
    Friend WithEvents btnSI As C1.Win.C1Input.C1Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnNO = New C1.Win.C1Input.C1Button()
        Me.btnSI = New C1.Win.C1Input.C1Button()
        Me.lblVolstancar = New C1.Win.C1Input.C1Label()
        CType(Me.lblVolstancar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnNO
        '
        Me.btnNO.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnNO.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnNO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnNO.Location = New System.Drawing.Point(324, 308)
        Me.btnNO.Name = "btnNO"
        Me.btnNO.Size = New System.Drawing.Size(75, 23)
        Me.btnNO.TabIndex = 1
        Me.btnNO.Text = "&NO"
        '
        'btnSI
        '
        Me.btnSI.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSI.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnSI.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSI.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSI.Location = New System.Drawing.Point(212, 308)
        Me.btnSI.Name = "btnSI"
        Me.btnSI.Size = New System.Drawing.Size(75, 23)
        Me.btnSI.TabIndex = 2
        Me.btnSI.Text = "&SI"
        '
        'lblVolstancar
        '
        Me.lblVolstancar.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblVolstancar.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblVolstancar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblVolstancar.Location = New System.Drawing.Point(98, 176)
        Me.lblVolstancar.Name = "lblVolstancar"
        Me.lblVolstancar.Size = New System.Drawing.Size(282, 88)
        Me.lblVolstancar.TabIndex = 3
        Me.lblVolstancar.Tag = Nothing
        Me.lblVolstancar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblVolstancar.Value = "Vols tancar TEX.NET?"
        '
        'frmSalir
        '
        Me.AcceptButton = Me.btnSI
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CancelButton = Me.btnNO
        Me.ClientSize = New System.Drawing.Size(450, 350)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblVolstancar)
        Me.Controls.Add(Me.btnSI)
        Me.Controls.Add(Me.btnNO)
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.Name = "frmSalir"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sortir"
        CType(Me.lblVolstancar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub XpButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSI.Click
        Me.DialogResult = DialogResult.Yes
    End Sub

    Private Sub XpButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNO.Click
        Me.DialogResult = DialogResult.Abort
    End Sub
End Class
