<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnBuild = New System.Windows.Forms.Button
        Me.cmbConfig = New System.Windows.Forms.ComboBox
        Me.lblMsBuildVersion = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Configuration:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "MSBuild Version:"
        '
        'btnBuild
        '
        Me.btnBuild.Enabled = False
        Me.btnBuild.Location = New System.Drawing.Point(151, 60)
        Me.btnBuild.Name = "btnBuild"
        Me.btnBuild.Size = New System.Drawing.Size(75, 23)
        Me.btnBuild.TabIndex = 4
        Me.btnBuild.Text = "Build"
        Me.btnBuild.UseVisualStyleBackColor = True
        '
        'cmbConfig
        '
        Me.cmbConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbConfig.FormattingEnabled = True
        Me.cmbConfig.Location = New System.Drawing.Point(105, 33)
        Me.cmbConfig.Name = "cmbConfig"
        Me.cmbConfig.Size = New System.Drawing.Size(121, 21)
        Me.cmbConfig.TabIndex = 3
        '
        'lblMsBuildVersion
        '
        Me.lblMsBuildVersion.AutoSize = True
        Me.lblMsBuildVersion.Location = New System.Drawing.Point(105, 9)
        Me.lblMsBuildVersion.Name = "lblMsBuildVersion"
        Me.lblMsBuildVersion.Size = New System.Drawing.Size(89, 13)
        Me.lblMsBuildVersion.TabIndex = 1
        Me.lblMsBuildVersion.Text = "lblMsBuildVersion"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(236, 90)
        Me.Controls.Add(Me.lblMsBuildVersion)
        Me.Controls.Add(Me.cmbConfig)
        Me.Controls.Add(Me.btnBuild)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MSBuild Runner"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnBuild As System.Windows.Forms.Button
    Friend WithEvents cmbConfig As System.Windows.Forms.ComboBox
    Friend WithEvents lblMsBuildVersion As System.Windows.Forms.Label

End Class
