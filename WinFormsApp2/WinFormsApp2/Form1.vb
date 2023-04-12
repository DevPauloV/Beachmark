Public Class Form1
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        GetSystemInfo()
        GetMemoryInfo()

    End Sub


    Private Sub GetSystemInfo()
        Dim Freq As String

        TexVersion.Text = Environment.OSVersion.ToString()
        TexName.Text = Environment.MachineName
        TexProcessador.Text = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\SYSTEM\CentralProcessor\0", "ProcessorNameString", Nothing)
        TexUser.Text = Environment.UserName
        TexFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.System)
        TexCurrent.Text = Environment.CurrentDirectory
        TexIdent.Text = My.Computer.Registry.LocalMachine.OpenSubKey(
    "HARDWARE\DESCRIPTION\System\CentralProcessor\0").GetValue("Identifier")
        TexLinguage.Text = My.Computer.Info.InstalledUICulture.DisplayName

        Freq = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\SYSTEM\CentralProcessor\0", "~MHz", Nothing)
        TexSpeed.Text = Freq & " MHz"

    End Sub



    Private Sub GetMemoryInfo()
        Const un = 1024 * 1024 * 1024 'KB / MB / GB

        Dim memTotal = Val(My.Computer.Info.TotalPhysicalMemory)
        Dim memDisponivel = Val(My.Computer.Info.AvailablePhysicalMemory)

        TexTMemory.Text = Math.Round(memTotal / un, 2)
        TexAMemory.Text = Math.Round(memDisponivel / un, 2)
    End Sub



    Private PerCounter As System.Diagnostics.PerformanceCounter
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim nutzram As Double
        nutzram = (My.Computer.Info.TotalPhysicalMemory - My.Computer.Info.AvailablePhysicalMemory) / 1048576 / 1024
        Label11.Text = "RAM: " & nutzram.ToString("N") & " GB"


        Dim Prozent1 As Long
        Prozent1 = My.Computer.Info.AvailablePhysicalMemory * 100
        Dim Prozentsatz As Long
        Prozentsatz = Val(Prozent1 / My.Computer.Info.TotalPhysicalMemory)
        ProgressBar2.Value = Prozentsatz


        Dim i As Integer = Integer.Parse(Format(PerCounter.NextValue, "##0"))
        ProgressBar1.Value = i


        Label10.Text = "CPU: " & i & " %"
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PerCounter = New System.Diagnostics.PerformanceCounter
        PerCounter.CategoryName = "Processor"
        PerCounter.CounterName = "% Processor Time"
        PerCounter.InstanceName = "_Total"
    End Sub


End Class