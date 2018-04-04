Imports EZTwainLibrary
Public Class FrmScannFile
    Dim OFD1 As New OpenFileDialog

    Private Sub BtnPrint_Click(sender As System.Object, e As System.EventArgs) Handles BtnPrint.Click
        FrmProScanner.ShowDialog()
    End Sub

    Private Sub BtnSave_Click(sender As System.Object, e As System.EventArgs) Handles BtnSave.Click

        If FrmProScanner.OFD.FileName = "" And Information.IsNothing(Pic1.Image) = True Then ' يتحقق هل المتغير فارغ لا يوجد فيه مسار لصورة ام لا
            MsgBox("لم يتم تحديد الصورة") '  يظهر رسالة انك لم تقم بمسح صورة بالماسح الضوئي او قمت بحذف الصورة يدويا
            BtnPrint.Select() ' يضع المأشر في زر مسح
            Exit Sub
        End If

        If TxtNoFile.Text = Nothing Then
            MessageBox.Show("اسف .. من فضلك قم بكتابة كود الملف في مربع الادخال ...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtNoFile.Focus()
            Exit Sub
        End If
        'Dim insertinto As New OleDb.OleDbCommand("insert into PicT (Pic) values (?)", con) ' يقوم بإجراء أمر الإضافة لقاعدة البيانات
        ' لان الذي نريد إضافتها صورة يجب تحويلها بايت إي 0 و 1
        '    Dim p() As Byte = My.Computer.FileSystem.ReadAllBytes(FrmProScanner.OFD.FileName) ' يقوم بأخذ الصورة من الهاردسك وتحويلها لبايت
        'insertinto.Parameters.AddWithValue("@PicD", OleDb.OleDbType.Binary).Value = p ' الان يضيفها في البارومتر الخاص بالصورة

        Dim n As Int16 = MyClss.ExecuteScalar("SELECT Count(*) From TbScannerFiles Where NoCode='" + TxtNoFile.Text.Trim + "'")
        'TbInvoiceDetalis
        If n > 0 Then
            MessageBox.Show("أسف لا يمكن حفظ البيانات  ..  كود الملف موجود مسبقا .. الرجاء التحقق !!!  ", "فشل عملية الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
            Exit Sub
        End If

        TestRunCode = False
        Sql = "INSERT INTO TbScannerFiles ( NoCode, Notes, ScanImg)"
        Sql += " VALUES (@NoCode, @Notes, @ScanImg) "
        'Sql += " VALUES (N'" + TxtNoFile.Text + "',N'" + TxtNotes.Text + "','" + stream.GetBuffer().ToString + ") "
        FrmProScanner.OFD.FileName = "" ' يفرغ المتغير

        'IDSc, NoCode, Notes, ScanImg, DateReg from TbScannerFiles
        Dim CMD1 As SqlClient.SqlCommand = New SqlClient.SqlCommand
        Try
            With CMD1
                .CommandType = CommandType.Text
                .Connection = Cn
                .Parameters.Add("@NoCode", SqlDbType.VarChar).Value = TxtNoFile.Text
                .Parameters.Add("@Notes", SqlDbType.NVarChar).Value = TxtNotes.Text.Trim
                '    If MyPic.Image = PictureBox1.Image Then
                If Information.IsNothing(Pic1.Image) Then
                    .Parameters.Add("@ScanImg", SqlDbType.Image).Value = Convert.DBNull
                Else
                    'تعريفات حفظ الصوة في قاعدة البيانات
                    Dim MyImage As Image = Pic1.Image
                    Dim stream As New IO.MemoryStream
                    MyImage.Save(stream, Imaging.ImageFormat.Jpeg)
                    .Parameters.Add("@ScanImg", SqlDbType.Image).Value = stream.GetBuffer()
                    '       My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\Images\" + NoEmpTextBox.Text + ".Jpg", FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin)
                    '  MyPic.Image.Save(Application.StartupPath + "\Images\" + NoEmpTextBox.Text + ".Jpg", Drawing.Imaging.ImageFormat.MemoryBmp)
                End If
                .CommandText = Sql


            End With
            If Cn.State = ConnectionState.Closed Then Cn.Open()
            CMD1.ExecuteNonQuery()
            TestRunCode = True
        Catch ex As SqlClient.SqlException
            TestRunCode = False
            MsgBox(ex.ToString)
        Finally
            Cn.Close()
        End Try

        'If con.State = ConnectionState.Closed Then con.Open() ' يتأكد من ان قاعدة البيانات مفتوحه إذا كان مغلقه يفتحها
        'insertinto.ExecuteNonQuery() ' يقوم بتنفيذ أمر الإضافة حسب المعطيات التي بالأعلى
        'con.Close() ' يغلق قاعدة البيانات
        ' MyClss.RunsInsertDeleteUpdateQry(Sql)
        If TestRunCode = True Then
            'If TestNewSave = True Then
            '    '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
            '    MyClss.UserMovement(UserID, "2", TxtCardName.Text, Me.Text)
            'Else
            '    MyClss.UserMovement(UserID, "3", TxtCardName.Text, Me.Text)
            'End If
            BtnNew_Click(sender, e)
            MessageBox.Show("تم حفظ البيــــانات بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("لم يتم حفظ البيــــانات ", "خطاء في عملية الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
        '  Pic1.Image = Nothing ' تفريغ البكتشر بوكس
    End Sub

    Private Sub BtnNew_Click(sender As System.Object, e As System.EventArgs) Handles BtnNew.Click
        Pic1.Image = Nothing
        TxtNoFile.Clear()
        TxtNotes.Clear()
        TxtNoFile.Focus()
       
    End Sub

    Private Sub FrmScannFile_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        BtnNew_Click(sender, e)
    End Sub

    Private Sub BtnDisplay_Click(sender As System.Object, e As System.EventArgs) Handles BtnDisplay.Click
        FrmDisplayImages.ShowDialog()
    End Sub

    Private Sub BtnDel_Click(sender As System.Object, e As System.EventArgs) Handles BtnDel.Click

    End Sub

    Private Sub BtnExit_Click(sender As System.Object, e As System.EventArgs) Handles BtnExit.Click
        Me.Dispose()
    End Sub

    Private Sub BtnGetFile_Click(sender As System.Object, e As System.EventArgs) Handles BtnGetFile.Click
        OFD1.CheckFileExists = True
        OFD1.ShowReadOnly = False
        Dim Str = "Jepeg Files (*.jpg)|*.jpg|BitMap Files (*.Bmb)|*.bmb|Acrobat PDF File|*.Pdf"
        OFD1.Filter = Str
        OFD1.FilterIndex = 1
        '  OFD1.InitialDirectory = "C:\"

        If OFD1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim OpenFileName As String = OFD1.FileName
            Str = OFD1.FileName
            Dim Ext As String = System.IO.Path.GetExtension(OFD1.FileName)
            Pic1.Image = Image.FromFile(OFD1.FileName)
            ' Pic.Visible = True
        Else
            Pic1.Image = Nothing
        End If
    End Sub
End Class
