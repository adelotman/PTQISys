Imports EZTwainLibrary

Public Class Form1
    Dim OFD As New OpenFileDialog ' تعريف متغير من نوع فتح ملف

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        EZTwain.SelectTwainsource(0) ' امر تحديد الاسكنر

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "MyImage.Bmp") = True Then ' يتحقق إذا كان الصورة موجوده في الهارديسك بنفس الاسم ام لا 
            My.Computer.FileSystem.DeleteFile(Application.StartupPath & "MyImage.Bmp") ' في حال وجودها سيتم حذفها حتى يتمكن من حفظ الصورة الجديده
        End If
        EZTwain.AcquireToFileName(Me.Handle, Application.StartupPath & "MyImage.Bmp") ' يقوم باستخراج الصورة من الماسح الضوئي وحفظها في الهاردسك
        PictureBox1.ImageLocation = Application.StartupPath + "MyImage.Bmp" ' لجلب الصورة في البكتشر بوكس وعرضها
        OFD.FileName = Application.StartupPath + "MyImage.Bmp" ' لجلب موقع الصوره ووضعها في متغير

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If OFD.FileName = "" Then ' يتحقق هل المتغير فارغ لا يوجد فيه مسار لصورة ام لا
            MsgBox("لم يتم تحديد صورة") '  يظهر رسالة انك لم تقم بمسح صورة بالماسح الضوئي او قمت بحذف الصورة يدويا
            Button1.Select() ' يضع المأشر في زر مسح
            Exit Sub
        End If

        Dim insertinto As New OleDb.OleDbCommand("insert into PicT (Pic) values (?)", con) ' يقوم بإجراء أمر الإضافة لقاعدة البيانات
        ' لان الذي نريد إضافتها صورة يجب تحويلها بايت إي 0 و 1
        Dim p() As Byte = My.Computer.FileSystem.ReadAllBytes(OFD.FileName) ' يقوم بأخذ الصورة من الهاردسك وتحويلها لبايت
        insertinto.Parameters.AddWithValue("@PicD", OleDb.OleDbType.Binary).Value = p ' الان يضيفها في البارومتر الخاص بالصورة
        OFD.FileName = "" ' يفرغ المتغير

        If con.State = ConnectionState.Closed Then con.Open() ' يتأكد من ان قاعدة البيانات مفتوحه إذا كان مغلقه يفتحها
        insertinto.ExecuteNonQuery() ' يقوم بتنفيذ أمر الإضافة حسب المعطيات التي بالأعلى
        con.Close() ' يغلق قاعدة البيانات
        MsgBox("تم الإضافة بنجاح", MsgBoxStyle.Information, "نجاح") ' رسالة تشير بنجاح العملية

        PictureBox1.Image = Nothing ' تفريغ البكتشر بوكس

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        FrmDisplayImages.ShowDialog()

    End Sub

   
End Class
