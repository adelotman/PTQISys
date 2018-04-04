Imports System.IO
Imports System.Drawing.Imaging
Public Class FrmDisplayImages

    Private Sub FrmDisplayImages_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If DGVDataPics.RowCount = 0 Then Exit Sub
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            'Dim ms As New MemoryStream
            'Dim img As Bitmap
            'img = DGVDataPics.CurrentRow.Cells("ScanImg").Value
            'img.Save(ms, ImageFormat.Jpeg)
            'Pic1.Image = Image.FromStream(ms)
            'BtnDel.Enabled = True
            DisplayData()
        End If
    End Sub

    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Dim ds As New DataSet
        'ds.Clear()
        'Dim da As New OleDb.OleDbDataAdapter("select * from PicT ", con)
        'da.Fill(ds, "PicT")
        BtnNew_Click(sender, e)

    End Sub

    Sub DisplayData()
        Dim DS1 As New DataSet
        Dim TxtNCode As String
        TxtNCode = DGVDataPics.CurrentRow.Cells("IDSc").Value.ToString
        ' DGVDataPics.Rows(e.RowIndex).Selected = True

        Dim str As String = "SELECT ScanImg  FROM TbScannerFiles WHERE (IDSc)='" & TxtNCode.ToString & "'"
        If Cn.State = ConnectionState.Closed Then Cn.Open()
        Dim ADP As New SqlClient.SqlDataAdapter(str, Cn)
        DS1.Clear()
        ADP.Fill(DS1, "TBL")
        ADP.Dispose()
        If DS1.Tables(0).Rows.Count = 0 Then Exit Sub
        If Information.IsDBNull(DS1.Tables(0).Rows(0).Item("ScanImg")) = False Then
            Dim ReadPic() As Byte = CType(DS1.Tables(0).Rows(0).Item("ScanImg"), Byte())
            Dim stream As New IO.MemoryStream(ReadPic)
            Dim MyImage As New Bitmap(stream)
            Pic1.Visible = True
            Pic1.Image = MyImage
        Else
            Pic1.Image = Nothing
            '  Pic1.Visible = False
        End If
        Cn.Close()

        ' Pic1.Image = Image.FromStream(ms)
        BtnDel.Enabled = True
    End Sub

    Private Sub DGVDataPics_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVDataPics.CellClick
        If DGVDataPics.RowCount = 0 Then Exit Sub
        DisplayData()
    End Sub


    Private Sub DGVDataPics_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVDataPics.CellContentClick

    End Sub

    Private Sub BtnNew_Click(sender As System.Object, e As System.EventArgs) Handles BtnNew.Click
        With Me.DGVDataPics
            .RowsDefaultCellStyle.BackColor = Color.Bisque
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Beige
        End With
        Sql = "select     IDSc, NoCode, Notes, ScanImg, DateReg from TbScannerFiles"
        Dim Tb As New DataTable
        Tb = MyClss.GetRecords(Sql)

        Pic1.Image = Nothing ' يجب تفريغه لتمكن من إعادة ملئه
        DGVDataPics.DataSource = Nothing ' يجب تفريغه لتمكن من إعادة ملئه

        DGVDataPics.DataSource = Tb
        DisplayGrid()

        ' Pic1.DataBindings.Add("image", Tb, "TbScannerFiles.ScanImg", True) ' ربط الصورة في حقل بوكتشر بوكس
        TxtFind.Clear()
        BtnDel.Enabled = False
        '  DataGridView1.Columns(1).Visible = False ' إخفاء عمود الصورة من داتا قريد
    End Sub

    Sub DisplayGrid()
        '  DataGridView1.DataMember = "PicT"
        With DGVDataPics
            .Columns("IDSc").Visible = False
            .Columns("NoCode").Width = 130
            .Columns("NoCode").HeaderText = "رقم القيد"
            .Columns("Notes").HeaderText = "ملاحظات"
            .Columns("ScanImg").HeaderText = "الصورة"
            .Columns("DateReg").HeaderText = "تاريخ التسجيل"
            .Columns("Notes").Width = 170
            '.Columns("IsActive").Width = 110
            TxtNoItems.Text = .Rows.Count
        End With
    End Sub
    Private Sub BtnSave_Click(sender As System.Object, e As System.EventArgs) Handles BtnSave.Click

    End Sub

    Private Sub BtnFind_Click(sender As System.Object, e As System.EventArgs) Handles BtnFind.Click

        Sql = "select  IDSc, NoCode, Notes, ScanImg, DateReg from TbScannerFiles"
        If TxtFind.Text <> Nothing Then
            Sql += " Where (NoCode) like N'%" & Trim(Me.TxtFind.Text) & "%' or (Notes) like '%" & Trim(Me.TxtFind.Text) & "%'"
        End If

        Dim Tb As New DataTable
        Tb = MyClss.GetRecords(Sql)
        DGVDataPics.DataSource = Tb
        DisplayGrid()
    End Sub

    Private Sub BtnPrint_Click(sender As System.Object, e As System.EventArgs) Handles BtnPrint.Click

    End Sub

    Private Sub BtnDel_Click(sender As System.Object, e As System.EventArgs) Handles BtnDel.Click
        'Dim n As Int32 = MyClss.ExecuteScalar("SELECT Count(*) From TbDataCllinets Where IDSc='" + DGVDataPics.CurrentRow.Cells("IDSc").Value.ToString + "'")
        ''TbInvoiceDetalis
        'If n > 0 Then
        '    MessageBox.Show("أسف لا يمكن حذف البيانات  .. هناك بيانات في جدول اخر مرتبطة بها  ", "فشل عملية الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        '    Exit Sub
        'End If


        Dim resault As Integer
        Dim sql As String = "DELETE FROM TbScannerFiles   WHERE ([IDSc] ='" + DGVDataPics.CurrentRow.Cells("IDSc").Value.ToString + "')"
        resault = MessageBox.Show("هل انت متأكد من عملية حذف البيانات ؟", "رسالة حذف ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If resault = vbYes Then
            MyClss.RunsInsertDeleteUpdateQry(sql)
            If TestRunCode = True Then
                '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
                MyClss.UserMovement(UserID, "4", DGVDataPics.CurrentRow.Cells("NoCode").Value.ToString, Me.Text)

                BtnNew_Click(sender, e)
                MessageBox.Show("لقد تم حذف البيانات ", "نجاح عملية الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
            Else
                MessageBox.Show("لم يتم حذف البيــــانات ", "خطاء في عملية الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
        Else
            MessageBox.Show("تم ايقاف عملية الحذف", "حذف سجل", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        End If
    End Sub

    Private Sub BtnExit_Click(sender As System.Object, e As System.EventArgs) Handles BtnExit.Click
        Me.Dispose()
    End Sub

    Private Sub TxtFind_Click(sender As System.Object, e As System.EventArgs) Handles TxtFind.Click

    End Sub

    Private Sub TxtFind_TextChanged(sender As Object, e As System.EventArgs) Handles TxtFind.TextChanged
        BtnFind_Click(sender, e)
    End Sub
End Class