Public Class FrmCashier
    Dim TestNewSave As Boolean, Sql As String
    Dim Tb As New DataTable
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        With Me.DGVItems
            .RowsDefaultCellStyle.BackColor = Color.Bisque
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Beige
        End With

        Sql = "Select * from TbCashier"
        Tb.Clear()
        Tb = MyClss.GetRecords(Sql)
        DGVItems.DataSource = Tb
        With DGVItems
            .Columns("NoCashier").Visible = False
            '   .Columns(1).Visible = False
            .Columns("CashierName").HeaderText = "وصف نوع العملة"
            .Columns("CashierName").Width = 250
            .Columns("NoEmp").HeaderText = "كود الموظف"
            .Columns("NoEmp").Width = 150
            .Columns("Balance").HeaderText = "الرصيد"
            .Columns("Balance").Width = 170
            .Columns("Balance").DefaultCellStyle.Format = "N3"
            '.Columns(4).HeaderText = "أفتراضي"
            '.Columns(4).Width = 100
            .Columns("IsActive").HeaderText = "التفعيل"
            .Columns("IsActive").Width = 100
        End With
        TxtNoItems.Text = DGVItems.RowCount
        TestNewSave = True
        BtnSave.Enabled = True
        BtnDel.Enabled = False
        TxtCashierName.Clear()
        'ChkIsDefult.Checked = False
        ChkIsActive.Checked = True
        TxtCashierName.Clear()
        TxtBalance.Text = 0
        TxtNo.Text = 0
        ChkEmp.Checked = False
        CmbEmp.SelectedIndex = -1
        CmbEmp.Enabled = ChkEmp.Checked
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        'التحقق من البيانات قبل الحفظ
        If TxtCashierName.Text = Nothing Then
            MessageBox.Show("اسف .. من فضلك قم بكتابة إسم الخزينة في مربع الادخال ...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtCashierName.Focus()
            Exit Sub
        End If


        'If Me.TxtValue.Text = 0 Then
        '    MessageBox.Show("اسف .. من فضلك قم بكتابة سعر العملة  في مربع الادخال ...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    TxtValue.Focus()
        '    Exit Sub
        'End If
        ''حفظ البيانات المدخلة

        'If ChkIsDefult.Checked = True Then
        '    'الغاء قيمة الحقل الافتراضي في جميع بيانات الجدول
        '    Sql = "UPDATE [dbo].[TbCurrency] SET [IsDefult] = 'False'"
        '    MyClss.RunsInsertDeleteUpdateQry(Sql)
        'End If
        Dim NoEmp As Int16 = 0
        If ChkEmp.Checked And CmbEmp.Items.Count > 0 And CmbEmp.SelectedItem <> Nothing Then NoEmp = CmbEmp.SelectedValue

        If TestNewSave = True Then
            Sql = "INSERT INTO TbCashier(CashierName, NoEmp, IsActive)"
            Sql += "VALUES (N'" + TxtCashierName.Text.Trim + "','" + NoEmp.ToString + "','" + ChkIsActive.Checked.ToString + "')"
        Else
            Sql = "UPDATE TbCashier SET [CashierName] = N'" + TxtCashierName.Text + "', [IsActive] = '" + ChkIsActive.Checked.ToString + "', [NoEmp] = '" + NoEmp.ToString + "' WHERE ([NoCashier] ='" + DGVItems.CurrentRow.Cells("NoCashier").Value.ToString + "')"
        End If
        MyClss.RunsInsertDeleteUpdateQry(Sql)
        If TestRunCode = True Then
            If TestNewSave = True Then
                '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
                MyClss.UserMovement(UserID, "2", TxtCashierName.Text, Me.Text)
            Else
                MyClss.UserMovement(UserID, "3", TxtCashierName.Text, Me.Text)
            End If
            BtnNew_Click(sender, e)
            MessageBox.Show("تم حفظ البيــــانات بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("لم يتم حفظ البيــــانات ", "خطاء في عملية الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
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
        Dim sql As String = "DELETE FROM TbCashier   WHERE ([NoCashier] ='" + DGVItems.CurrentRow.Cells("NoCashier").Value.ToString + "')"
        resault = MessageBox.Show("هل انت متأكد من عملية حذف البيانات ؟", "رسالة حذف ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If resault = vbYes Then
            MyClss.RunsInsertDeleteUpdateQry(sql)
            If TestRunCode = True Then
                '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
                MyClss.UserMovement(UserID, "4", TxtCashierName.Text, Me.Text)

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
        Me.Close()
    End Sub

    Private Sub DGVItems_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVItems.CellClick
        If DGVItems.RowCount = 0 Then Exit Sub
        With DGVItems.CurrentRow
            TxtCashierName.Text = .Cells("CashierName").Value
            TxtBalance.Text = .Cells("Balance").Value
            TxtNo.Text = .Cells("NoCashier").Value
            ChkIsActive.Checked = .Cells("IsActive").Value

        End With
        BtnDel.Enabled = True
        TestNewSave = False
        'TxtCashierName.Focus()
    End Sub

    Private Sub DGVItems_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVItems.CellContentClick

    End Sub

    Private Sub DGVItems_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGVItems.KeyUp
        If DGVItems.RowCount = 0 Then Exit Sub
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            With DGVItems.CurrentRow
                TxtCashierName.Text = .Cells("CashierName").Value
                TxtBalance.Text = .Cells("Balance").Value
                TxtNo.Text = .Cells("NoCashier").Value
                ChkIsActive.Checked = .Cells("IsActive").Value
                If .Cells("NoEmp").Value > 0 Then
                    ChkEmp.Checked = True
                    CmbEmp.SelectedValue = .Cells("NoEmp").Value
                Else
                    ChkEmp.Checked = False
                    CmbEmp.SelectedIndex = -1
                End If

            End With
            BtnDel.Enabled = True
            TestNewSave = False
        End If
        '   TxtCashierName.Focus()
    End Sub

    Private Sub ChkEmp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkEmp.CheckedChanged
        CmbEmp.Enabled = ChkEmp.Checked
    End Sub
End Class