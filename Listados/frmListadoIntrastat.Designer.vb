<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListadoIntrastat
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmbMeses = New C1.Win.C1Input.C1ComboBox()
        Me.cmbAños = New C1.Win.C1Input.C1ComboBox()
        Me.btnExcel = New C1.Win.C1Input.C1Button()
        Me.lblMes = New System.Windows.Forms.Label()
        Me.lblAño = New System.Windows.Forms.Label()
        CType(Me.cmbMeses,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cmbAños,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.btnExcel,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'cmbMeses
        '
        Me.cmbMeses.AllowSpinLoop = false
        Me.cmbMeses.GapHeight = 0
        Me.cmbMeses.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.cmbMeses.ItemsDisplayMember = ""
        Me.cmbMeses.ItemsValueMember = ""
        Me.cmbMeses.Location = New System.Drawing.Point(16, 30)
        Me.cmbMeses.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbMeses.Name = "cmbMeses"
        Me.cmbMeses.Size = New System.Drawing.Size(267, 20)
        Me.cmbMeses.TabIndex = 0
        Me.cmbMeses.Tag = Nothing
        '
        'cmbAños
        '
        Me.cmbAños.AllowSpinLoop = false
        Me.cmbAños.GapHeight = 0
        Me.cmbAños.ImagePadding = New System.Windows.Forms.Padding(0)
        Me.cmbAños.ItemsDisplayMember = ""
        Me.cmbAños.ItemsValueMember = ""
        Me.cmbAños.Location = New System.Drawing.Point(291, 30)
        Me.cmbAños.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbAños.Name = "cmbAños"
        Me.cmbAños.Size = New System.Drawing.Size(267, 20)
        Me.cmbAños.TabIndex = 1
        Me.cmbAños.Tag = Nothing
        '
        'btnExcel
        '
        Me.btnExcel.Location = New System.Drawing.Point(231, 59)
        Me.btnExcel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(100, 28)
        Me.btnExcel.TabIndex = 2
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.UseVisualStyleBackColor = true
        '
        'lblMes
        '
        Me.lblMes.AutoSize = true
        Me.lblMes.Location = New System.Drawing.Point(17, 6)
        Me.lblMes.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMes.Name = "lblMes"
        Me.lblMes.Size = New System.Drawing.Size(34, 17)
        Me.lblMes.TabIndex = 3
        Me.lblMes.Text = "Mes"
        '
        'lblAño
        '
        Me.lblAño.AutoSize = true
        Me.lblAño.Location = New System.Drawing.Point(287, 6)
        Me.lblAño.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAño.Name = "lblAño"
        Me.lblAño.Size = New System.Drawing.Size(33, 17)
        Me.lblAño.TabIndex = 4
        Me.lblAño.Text = "Año"
        '
        'frmListadoIntrastat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8!, 16!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(569, 100)
        Me.Controls.Add(Me.lblAño)
        Me.Controls.Add(Me.lblMes)
        Me.Controls.Add(Me.btnExcel)
        Me.Controls.Add(Me.cmbAños)
        Me.Controls.Add(Me.cmbMeses)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmListadoIntrastat"
        Me.ShowIcon = false
        Me.Text = "Intrastat"
        CType(Me.cmbMeses,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cmbAños,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.btnExcel,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents cmbMeses As C1.Win.C1Input.C1ComboBox
    Friend WithEvents cmbAños As C1.Win.C1Input.C1ComboBox
    Friend WithEvents btnExcel As C1.Win.C1Input.C1Button
    Friend WithEvents lblMes As Label
    Friend WithEvents lblAño As Label
End Class
