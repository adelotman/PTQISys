Public Class FrmTypeDrugs
    Dim TestNewSave As Boolean, Sql As String
    Dim Tb As New DataTable
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        With Me.DGVItems
            .RowsDefaultCellStyle.BackColor = Color.Bisque
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Beige
        End With

        Sql = "Select * from TbMedications"
        Tb.Clear()
        Tb = MyClss.GetRecords(Sql)
        DGVItems.DataSource = Tb
        With DGVItems
            .Columns("NoMd").Visible = False
            .Columns("NoMd").Visible = False
            .Columns("NameMd").HeaderText = "اسم  الدواء"
            .Columns("NameMd").Width = 360
            .Columns("DateReg").HeaderText = "تاريخ التسجيل"
            .Columns("DateReg").Width = 130
            .Columns("IsActive").HeaderText = "التفعيل"
            .Columns("IsActive").Width = 100
        End With
        TxtNoItems.Text = DGVItems.RowCount
        TestNewSave = True
        BtnSave.Enabled = True
        BtnDel.Enabled = False
        TxtDrags.Clear()
        ChkIsActive.Checked = True
        TxtDrags.Clear()
        TxtNo.Text = 0
        '  CmbCity.SelectedIndex = 0
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        'التحقق من البيانات قبل الحفظ
        If TxtDrags.Text = Nothing Then
            MessageBox.Show("اسف .. من فضلك قم بكتابة إسم الدواء في مربع الادخال ...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtDrags.Focus()
            Exit Sub
        End If


        'If CmbCity.Text = Nothing Then
        '    MessageBox.Show("اسف .. من فضلك قم باختيار صلة القرابة من مربع الادخال ...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    CmbCity.Focus()
        '    Exit Sub
        'End If
        ''حفظ البيانات المدخلة

        'If ChkIsDefult.Checked = True Then
        '    'الغاء قيمة الحقل الافتراضي في جميع بيانات الجدول
        '    Sql = "UPDATE [dbo].[TbCurrency] SET [IsDefult] = 'False'"
        '    MyClss.RunsInsertDeleteUpdateQry(Sql)
        'End If
        Dim NoEmp As Int16 = 0

        If TestNewSave = True Then
            Sql = "INSERT INTO TbMedications(NameMd,IsActive)"
            Sql += "VALUES (N'" + TxtDrags.Text.Trim + "','" + ChkIsActive.Checked.ToString + "')"
        Else
            Sql = "UPDATE TbMedications SET [NameMd] = N'" + TxtDrags.Text + "', [IsActive] = '" + ChkIsActive.Checked.ToString + "' WHERE ([NoMd] ='" + DGVItems.CurrentRow.Cells("NoMd").Value.ToString + "')"
        End If
        MyClss.RunsInsertDeleteUpdateQry(Sql)
        If TestRunCode = True Then
            If TestNewSave = True Then
                '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
                MyClss.UserMovement(UserID, "2", TxtDrags.Text, Me.Text)
            Else
                MyClss.UserMovement(UserID, "3", TxtDrags.Text, Me.Text)
            End If
            BtnNew_Click(sender, e)
            MessageBox.Show("تم حفظ البيــــانات بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("لم يتم حفظ البيــــانات ", "خطاء في عملية الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub

    Private Sub FrmTypeDrugs_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        BtnExit_Click(sender, e)
    End Sub

    Private Sub FrmCashier_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        ConvertTabKeyToEnterKey(e)
        If e.KeyCode = Keys.F2 Then BtnNew_Click(sender, e)
        If e.KeyCode = Keys.F3 Then BtnSave_Click(sender, e)
        If e.KeyCode = Keys.F4 And BtnDel.Enabled = True Then BtnDel_Click(sender, e)
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F12 Then BtnExit_Click(sender, e)
    End Sub

    Private Sub FrmCashier_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BtnNew_Click(sender, e)
    End Sub

    Private Sub BtnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFind.Click

    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click

    End Sub

    Private Sub BtnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDel.Click
        Dim resault As Integer
        Dim sql As String = "DELETE FROM TbMedications   WHERE ([NoMd] ='" + DGVItems.CurrentRow.Cells("NoMd").Value.ToString + "')"
        resault = MessageBox.Show("هل انت متأكد من عملية حذف البيانات ؟", "رسالة حذف ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If resault = vbYes Then
            MyClss.RunsInsertDeleteUpdateQry(sql)
            If TestRunCode = True Then
                '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
                MyClss.UserMovement(UserID, "4", TxtDrags.Text, Me.Text)

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

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        ' FrmPatient.ClearDrugs()
        Me.Close()
    End Sub

    Sub DisplayData()
        With DGVItems.CurrentRow
            TxtDrags.Text = .Cells("NameMd").Value
            ' CmbCity.Text = .Cells("Relate").Value
            TxtNo.Text = .Cells("NoMd").Value
            ChkIsActive.Checked = .Cells("IsActive").Value
        End With

    End Sub
    Private Sub DGVItems_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVItems.CellClick
        If DGVItems.RowCount = 0 Then Exit Sub
        DisplayData()
        BtnDel.Enabled = True
        TestNewSave = False
        'TxtCashierName.Focus()
    End Sub

    Private Sub DGVItems_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVItems.CellContentClick

    End Sub

    Private Sub DGVItems_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGVItems.KeyUp
        If DGVItems.RowCount = 0 Then Exit Sub
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            DisplayData()
            BtnDel.Enabled = True
            TestNewSave = False
        End If
        '   TxtCashierName.Focus()
    End Sub

   
    
End Class