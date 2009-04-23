Imports System.IO

Namespace My

    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            ' uncomment for debugging
            'Dim testingFile = "C:\Users\AustinWise\Documents\Visual Studio 2008\Projects\WowArmoryBankLog\WowArmoryBankLog.sln"
            'My.Forms.frmMain.BuildFilePath = testingFile
            'Return

            'make sure the file exits
            If e.CommandLine.Count = 1 Then
                Dim buildFile As String = e.CommandLine(0)
                If File.Exists(buildFile) Then
                    My.Forms.frmMain.BuildFilePath = buildFile
                    Return
                End If
            End If

            Dim name As System.Reflection.AssemblyName = Me.GetType.Assembly.GetName()
            MessageBox.Show(My.Resources.NoFilePassInError, name.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning)

            'exit if not file was passed in
            e.Cancel = True
        End Sub

    End Class

End Namespace