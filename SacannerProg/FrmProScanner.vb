Imports EZTwainLibrary
Public Class FrmProScanner
    Public OFD As New OpenFileDialog ' تعريف متغير من نوع فتح ملف
    Private Sub BtnStart_Click(sender As System.Object, e As System.EventArgs) Handles BtnStart.Click
        '  EZTwain.SelectTwainsource(0) ' امر تحديد الاسكنر
        Dim x = EZTwain.SelectTwainsource(0)
        If x = 0 Then Exit Sub
        Timer1.Start()
    End Sub

    Private Sub BtnPaush_Click(sender As System.Object, e As System.EventArgs) Handles BtnPaush.Click
        Timer1.Stop()
        TxtState.Text = "ايقاف مؤقت"
    End Sub

    Private Sub BtnCancel_Click(sender As System.Object, e As System.EventArgs) Handles BtnCancel.Click
        Timer1.Stop()
        TxtState.Text = "..."
        ProgBar.Value = 0
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        'ProgBar.Value = 0
        ProgBar.Increment(1)
        With ProgBar
            If .Value = .Maximum Then

            End If

            Select Case .Value
                Case Is = 0
                    TxtState.Text = "..."
                Case Is = 4
                    TxtState.Text = "بدء المسح"
                Case Is = 7
                    TxtState.Text = "جاري عملية المسح"
                    If My.Computer.FileSystem.FileExists(Application.StartupPath & "MyImage.Bmp") = True Then ' يتحقق إذا كان الصورة موجوده في الهارديسك بنفس الاسم ام لا 
                        My.Computer.FileSystem.DeleteFile(Application.StartupPath & "MyImage.Bmp") ' في حال وجودها سيتم حذفها حتى يتمكن من حفظ الصورة الجديده
                    End If
                    EZTwain.AcquireToFileName(Me.Handle, Application.StartupPath & "MyImage.Bmp") ' يقوم باستخراج الصورة من الماسح الضوئي وحفظها في الهاردسك
                    FrmScannFile.Pic1.ImageLocation = Application.StartupPath + "MyImage.Bmp" ' لجلب الصورة في البكتشر بوكس وعرضها
                    OFD.FileName = Application.StartupPath + "MyImage.Bmp" ' لجلب موقع الصوره ووضعها في متغير
                Case Is = 90
                    TxtState.Text = "انتهاء عملية المسح"
                Case Is = 0
                    TxtState.Text = "..."
                    Timer1.Stop()
            End Select
        End With
    End Sub

    Private Sub FrmProScanner_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Timer1.Stop()
    End Sub
End Class