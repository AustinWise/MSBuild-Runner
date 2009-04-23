Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Diagnostics
Imports System.Xml.Linq
Imports Microsoft.Win32

Public Class frmMain

    Private m_msBuildPath As String

    Private m_buildFilePath As String
    Public Property BuildFilePath() As String
        Get
            Return Me.m_buildFilePath
        End Get
        Set(ByVal value As String)
            Me.m_buildFilePath = value
            btnBuild.Enabled = True
        End Set
    End Property

    Private Sub Form1_HelpRequested(ByVal sender As Object, ByVal hlpevent As System.Windows.Forms.HelpEventArgs) Handles Me.HelpRequested
        Dim name As System.Reflection.AssemblyName = Me.GetType().Assembly.GetName()
        Dim copy As System.Reflection.AssemblyCopyrightAttribute = CType(Attribute.GetCustomAttribute(Me.GetType().Assembly, GetType(System.Reflection.AssemblyCopyrightAttribute)), System.Reflection.AssemblyCopyrightAttribute)
        MessageBox.Show(String.Format("{1} Version {2}{0}{3}", Environment.NewLine, name.Name, name.Version, copy.Copyright), name.Name, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim msbVersions = getMSBuildVersions()

        If msbVersions.Count = 0 Then
            MessageBox.Show("Somehow we were not able to find any versions.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnBuild.Enabled = False
            Return
        End If

        Dim msb = newestMSBuild(msbVersions)
        Me.lblMsBuildVersion.Text = msb.Key
        Me.m_msBuildPath = msb.Value

        cmbConfig.DataSource = getConfigurations()
    End Sub

    Private Function getConfigurations() As List(Of String)
        Dim configurations As New List(Of String)()
        If Not Me.BuildFilePath.EndsWith(".sln", StringComparison.OrdinalIgnoreCase) Then
            configurations.Add("Release")
            configurations.Add("Debug")
            Return configurations
        End If
        Dim isCurrentlyInSection As Boolean = False
        For Each line As String In File.ReadAllLines(Me.BuildFilePath)
            If line.Trim.Equals("EndGlobalSection", StringComparison.OrdinalIgnoreCase) Then
                isCurrentlyInSection = False
            End If
            If isCurrentlyInSection Then
                Dim configName As String = line.Trim.Split("|".ToCharArray())(0)
                If configurations.BinarySearch(configName, StringComparer.OrdinalIgnoreCase) < 0 Then
                    configurations.Add(configName)
                End If
            End If
            If line.Trim.Equals("GlobalSection(SolutionConfigurationPlatforms) = preSolution", StringComparison.OrdinalIgnoreCase) Then
                isCurrentlyInSection = True
            End If
        Next
        configurations.Sort(New Comparison(Of String)(AddressOf compareConfig))
        Return configurations
    End Function

    Private Function compareConfig(ByVal a As String, ByVal b As String) As Integer
        Return -a.CompareTo(b)
    End Function

    Private Function newestMSBuild(ByVal msbVersions As IDictionary(Of String, String)) As KeyValuePair(Of String, String)
        Dim newestVersion As New Version(0, 0, 0, 0)
        Dim newest As KeyValuePair(Of String, String) = Nothing

        For Each kvp In msbVersions
            Dim ver As New Version(kvp.Key)
            If ver > newestVersion Then
                newestVersion = ver
                newest = kvp
            End If
        Next

        Return newest
    End Function

    Private rVersion As New Regex("^.+\\V(?<Version>(\d|\.)+)\\.+$", RegexOptions.IgnoreCase)
    Private Function getMSBuildVersions() As Dictionary(Of String, String)
        Dim versions As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        Dim frameworkInstallRoot As String = getFrameworkInstallRoot()
        For Each d As String In Directory.GetDirectories(frameworkInstallRoot, "V*.*")
            Dim msbuild As String = Path.Combine(d, "MSBuild.exe")
            If File.Exists(msbuild) Then
                Dim m As Match = rVersion.Match(msbuild)

                versions.Add(m.Groups("Version").Value, msbuild)
            End If
        Next

        Return versions
    End Function

    Private Function getFrameworkInstallRoot() As String
        Dim hklm = Registry.LocalMachine
        Dim subKey = hklm.OpenSubKey("SOFTWARE\Microsoft\.NETFramework", False)
        Dim root As String = CStr(subKey.GetValue("InstallRoot"))
        subKey.Close()
        Return root
    End Function

    Private Sub btnBuild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuild.Click
        Me.Hide()

        Dim batchFile As String = createBuildFile(m_msBuildPath, cmbConfig.SelectedItem.ToString(), Me.m_buildFilePath)
        Dim p As Process = Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "cmd.exe"), String.Format("/C ""{0}""", batchFile))
        p.WaitForExit()
        File.Delete(batchFile)
        Me.Close()
    End Sub

    Private Function createBuildFile(ByVal msbuildPath As String, ByVal configuration As String, ByVal buildFile As String) As String
        Dim folder As String = Path.Combine(Environment.GetEnvironmentVariable("temp"), "MSBuild Runner")
        If Not Directory.Exists(folder) Then
            Directory.CreateDirectory(folder)
        End If
        Dim filePath As String = Path.Combine(folder, Date.Now.ToOADate() & ".bat")
        Dim wr As New StreamWriter(filePath, False, Encoding.ASCII)
        wr.WriteLine("@echo off")
        wr.WriteLine("cd ""{0}""", New FileInfo(buildFile).Directory.FullName)
        wr.WriteLine("{0} /t:rebuild /p:configuration={1} ""{2}""", msbuildPath, configuration, buildFile)
        wr.WriteLine("@echo.")
        wr.WriteLine("@echo.")
        wr.WriteLine("pause")
        wr.Close()
        Return filePath
    End Function
End Class
