Public Class FrmRelatePatient
    Dim TestNewSave As Boolean, Sql As String
    Dim Tb As New DataTable
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        With Me.DGVItems
            .RowsDefaultCellStyle.BackColor = Color.Bisque
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Beige
        End With

        TestNewSave = True
        BtnSave.Enabled = True
        BtnDel.Enabled = False
        TxtPName.Clear()
        ChkIsActive.Checked = True
        TxtPName.Clear()
        TxtNo.Text = 0
        CmbRelEmp.SelectedIndex = 0
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        'التحقق من البيانات قبل الحفظ
        If TxtPName.Text = Nothing Then
            MessageBox.Show("اسف .. من فضلك قم بكتابة إسم فرد العائلة في مربع الادخال ...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtPName.Focus()
            Exit Sub
        End If


        If CmbRelEmp.Text = Nothing Then
            MessageBox.Show("اسف .. من فضلك قم باختيار صلة القرابة من مربع الادخال ...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbRelEmp.Focus()
            Exit Sub
        End If
        ''حفظ البيانات المدخلة

        'If ChkIsDefult.Checked = True Then
        '    'الغاء قيمة الحقل الافتراضي في جميع بيانات الجدول
        '    Sql = "UPDATE [dbo].[TbCurrency] SET [IsDefult] = 'False'"
        '    MyClss.RunsInsertDeleteUpdateQry(Sql)
        'End If
        Dim NoEmp As Int16 = NoEmpl

        If TestNewSave = True Then
            Sql = "INSERT INTO TbPatients(PatientName, NoEmp, IsActive,Relate)"
            Sql += "VALUES (N'" + TxtPName.Text.Trim + "','" + NoEmp.ToString + "','" + ChkIsActive.Checked.ToString + "',N'" + CmbRelEmp.Text + "')"
        Else
            Sql = "UPDATE TbPatients SET [PatientName] = N'" + TxtPName.Text + "', [IsActive] = '" + ChkIsActive.Checked.ToString + "', [NoEmp] = '" + NoEmp.ToString + "',[Relate]='" + CmbRelEmp.Text + "' WHERE ([NoPatient] ='" + DGVItems.CurrentRow.Cells("NoPatient").Value.ToString + "')"
        End If
        MyClss.RunsInsertDeleteUpdateQry(Sql)
        If TestRunCode = True Then
            If TestNewSave = True Then
                '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
                MyClss.UserMovement(UserID, "2", TxtPName.Text, Me.Text)
            Else
                MyClss.UserMovement(UserID, "3", TxtPName.Text, Me.Text)
            End If
            BtnNew_Click(sender, e)
            MessageBox.Show("تم حفظ البيــــانات بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("لم يتم حفظ البيــــانات ", "خطاء في عملية الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub

    Private Sub FrmRelatePatient_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If NoEmpl = 0 Or NoEmpl = Nothing Then Exit Sub

        Sql = "Select * from TbPatients Where NoEmp='" + NoEmpl.ToString + "'"
        Tb.Clear()
        Tb = MyClss.GetRecords(Sql)
        DGVItems.DataSource = Tb
        With DGVItems
            .Columns("NoPatient").Visible = False
            .Columns("NoEmp").Visible = False
            .Columns("PatientName").HeaderText = "اسم فرد العائلة"
            .Columns("PatientName").Width = 220
            .Columns("DateReg").HeaderText = "تاريخ التسجيل"
            .Columns("Relate").HeaderText = "صلة القرابة"
            .Columns("Relate").Width = 140
            .Columns("DateReg").Width = 130
            '  .Columns("Balance").DefaultCellStyle.Format = "N3"
            .Columns("IsActive").HeaderText = "التفعيل"
            .Columns("IsActive").Width = 100
        End With
        TxtNoItems.Text = DGVItems.RowCount

    End Sub

    Private Sub FrmRelatePatient_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
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
        Dim sql As String = "DELETE FROM TbPatients   WHERE ([NoPatient] ='" + DGVItems.CurrentRow.Cells("NoPatient").Value.ToString + "')"
        resault = MessageBox.Show("هل انت متأكد من عملية حذف البيانات ؟", "رسالة حذف ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If resault = vbYes Then
            MyClss.RunsInsertDeleteUpdateQry(sql)
            If TestRunCode = True Then
                '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
                MyClss.UserMovement(UserID, "4", TxtPName.Text, Me.Text)

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

        'Sql = "Select NoPatient,PatientName from TbPatients Where IsActive='true' And NoEmp='" + NoEmpl.ToString + "'"
        'Tb = MyClss.GetRecords(Sql)
        'FrmPatient.CmbPatName.DataSource = Tb
        'FrmPatient.CmbPatName.ValueMember = Tb.Columns(0).ColumnName
        'FrmPatient.CmbPatName.DisplayMember = Tb.Columns(1).ColumnName
        NoEmpl = -1

        ' FILLCOMBOBOXITEMS2(Sql, FrmPatient.CmbPatName)
        Me.Close()
    End Sub

    Sub DisplayData()
        With DGVItems.CurrentRow
            TxtPName.Text = .Cells("PatientName").Value
            CmbRelEmp.Text = .Cells("Relate").Value
            TxtNo.Text = .Cells("NoPatient").Value
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

    Private Sub BtnFamily_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFamily.Click
        FrmFamilyChooice.ShowDialog()
    End Sub
End Class