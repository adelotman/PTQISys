Public Class FrmPatient
    Dim TestNewSave, TestNewSave2 As Boolean, Sql As String
    Dim Tb As New DataTable
    Dim TbPatInfo As New DataTable
    Dim FindTest As Boolean = False

    Private Sub FrmPatient_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If FindTest = True Then Exit Sub

        ClearDrugs()

        If NoEmpl = -1 And TxtNoEmp.Text <> Nothing Then
            Sql = "Select NoPatient,PatientName from TbPatients Where IsActive='true' And NoEmp='" + TxtNoEmp.Text.ToString + "'"
            Tb = MyClss.GetRecords(Sql)
            CmbPatName.DataSource = Tb
            CmbPatName.ValueMember = Tb.Columns(0).ColumnName
            CmbPatName.DisplayMember = Tb.Columns(1).ColumnName
        End If

        If NoEmpl > 0 Then
            TxtNoEmp.Text = NoEmpl
            TxtName.Text = NameEmpl
            '  BackgroundWorker1.RunWorkerAsync()
            Sql = "Select NoPatient,PatientName,Relate from TbPatients Where IsActive='true' And NoEmp='" + NoEmpl.ToString + "'"
            TbPatInfo = MyClss.GetRecords(Sql)
            If TbPatInfo.Rows.Count > 0 Then
                CmbPatName.DataSource = TbPatInfo
                CmbPatName.ValueMember = TbPatInfo.Columns(0).ColumnName
                CmbPatName.DisplayMember = TbPatInfo.Columns(1).ColumnName
            End If
            LastInfoPat(TxtNoEmp.Text)
        Else
            '  TxtNoEmp.Text = Nothing
            ' TxtName.Text = Nothing
            'CmbPatName.DataSource = Nothing
            'CmbPatName.Text = Nothing
        End If
    End Sub

    Private Sub FrmPatient_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        ConvertTabKeyToEnterKey(e)
        If e.KeyCode = Keys.F2 Then BtnNew_Click(sender, e)
        If e.KeyCode = Keys.F3 Then BtnSave_Click(sender, e)
        If e.KeyCode = Keys.F4 And BtnDel.Enabled = True Then BtnDel_Click(sender, e)
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F12 Then BtnExit_Click(sender, e)
    End Sub

    Private Sub FrmPatient_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BtnNew_Click(sender, e)
        Control.CheckForIllegalCrossThreadCalls = False
    End Sub
    Sub LastInfoPat(ByVal NoEmp As Integer)
        Sql = "Select NoPr,PatientName,Descrtion,DateReg from TbViewInfoPatients Where Emp_no='" + NoEmp.ToString + "'"
        Tb = MyClss.GetRecords(Sql)
        TxtInfo.Text = Nothing

        If Tb.Rows.Count > 0 Then
            With Tb.Rows(Tb.Rows.Count - 1)
                TxtInfo.Text = " :رقم الوصفة " + .Item("NoPr").ToString + "-" + " : إسم المريض " + .Item("PatientName").ToString + "-" + "  : تاريخ الوصفة " + .Item("DateReg").ToString + "-" + " : وصف العلاج " + .Item("Descrtion").ToString
            End With

        End If
        '
    End Sub
    Public Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        TestNewSave = True

        NoEmpl = 0
        NameEmpl = Nothing
        FindTest = False
        TxtNoEmp.Text = 0
        TxtName.Text = Nothing
        DateReg.Value = DateTime.Now
        ChkIsSame.Checked = False
        TablelPanel1.Enabled = False
        TxtRelated.Text = Nothing
        Sql = "Select Max(NoPr) From TbPrescriptions "
        TxtNoD.Text = MyClss.ExecuteScalar(Sql) + 1

        CmbPatName.DataSource = Nothing
        '  CmbRelated.SelectedIndex = -1
        TxtDescrption.Clear()
        TxtInfo.Clear()
        TxtFind.Clear()
        '  TablelPanel1.Enabled = False
        ClearDrugs()
        CmbPatName.Focus()

    End Sub

    Public Sub ClearDrugs()
        With Me.DGVItems
            .RowsDefaultCellStyle.BackColor = Color.Bisque
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Beige
        End With

        TxtNoDrug.Clear()
        NumQDrug.Value = 1
        BtnDelete.Enabled = False
        DGVItems.DataSource = Nothing
        Sql = "Select NoMd,NameMd from TbMedications Where IsActive='true'"
        Dim TbD As New DataTable
        TbD = MyClss.GetRecords(Sql)
        If TbD.Rows.Count > 0 Then
            CmbTypeDrugs.DataSource = TbD
            CmbTypeDrugs.ValueMember = TbD.Columns(0).ColumnName
            CmbTypeDrugs.DisplayMember = TbD.Columns(1).ColumnName

        End If

        'FILLCOMBOBOXITEMS2(Sql, CmbTypeDrugs)
        Sql = "Select NameMd from TbMedications Where IsActive='true'"
        'AutocompleteSql(Sql, CmbTypeDrugs)
        TestNewSave2 = True
        Sql = "SELECT   NoEx, NoPr, NoPatien, NoMd, NameMd, Qt, DateReg, Notes FROM  TbViewMedicenPat where NoPr='" + TxtNoD.Text + "'"
        Tb.Clear()
        Tb = MyClss.GetRecords(Sql)
        DGVItems.DataSource = Tb
        With DGVItems
            .Columns("NoMd").Visible = False
            .Columns("NoPr").Visible = False
            .Columns("NoEx").Visible = False
            .Columns("NoPatien").Visible = False
            .Columns("NameMd").HeaderText = "اسم  الدواء"
            .Columns("NameMd").Width = 350
            .Columns("DateReg").HeaderText = "تاريخ التسجيل"
            .Columns("DateReg").Width = 130
            .Columns("Qt").HeaderText = "الكمية"
            .Columns("Qt").Width = 100
            .Columns("Notes").HeaderText = "ملاحظات"
            .Columns("Notes").Width = 350
        End With

        TxtNoItems.Text = DGVItems.RowCount
        CmbTypeDrugs.Focus()

    End Sub
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        'التحقق من البيانات قبل الحفظ
        If CmbPatName.Text = Nothing Then
            MessageBox.Show("اسف .. من فضلك قم باختيار إسم المريض من مربع الادخال ...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbPatName.Focus()
            Exit Sub
        End If


        'If CmbRelated.Text = Nothing Then
        '    MessageBox.Show("اسف .. من فضلك قم باختيار صلة القرابة من مربع الادخال ...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    CmbRelated.Focus()
        '    Exit Sub
        'End If

        If TxtDescrption.Text = Nothing Then
            MessageBox.Show("اسف .. من فضلك قم بكتابة  وصف العلاج  في مربع الادخال ...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtDescrption.Focus()
            Exit Sub
        End If
        ''حفظ البيانات المدخلة

        'If ChkIsDefult.Checked = True Then
        '    'الغاء قيمة الحقل الافتراضي في جميع بيانات الجدول
        '    Sql = "UPDATE [dbo].[TbCurrency] SET [IsDefult] = 'False'"
        '    MyClss.RunsInsertDeleteUpdateQry(Sql)
        'End If
        Dim NoEmp As Int16 = 0

        If TestNewSave = True Then

            Sql = "INSERT INTO TbPrescriptions(NoPatien,Descrtion)"
            Sql += "VALUES ('" + CmbPatName.SelectedValue.ToString + "',N'" + TxtDescrption.Text.Trim + "')"
        Else
            Sql = "UPDATE TbPrescriptions SET [NoPatien] = '" + CmbPatName.SelectedValue.ToString + "', [Descrtion] = N'" + TxtDescrption.Text.Trim + "' WHERE ([NoPr] ='" + TxtNoD.Text + "')"
        End If
        MyClss.RunsInsertDeleteUpdateQry(Sql)

        If TestNewSave = True Then
            Sql = "Select Max(NoPr) From TbPrescriptions "
            TxtNoD.Text = MyClss.ExecuteScalar(Sql)
        End If

        If TestRunCode = True Then
            If TestNewSave = True Then
                '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
                MyClss.UserMovement(UserID, "2", CmbPatName.Text + "---" + TxtDescrption.Text, Me.Text)
                TestNewSave = False
                TablelPanel1.Enabled = True
            Else
                MyClss.UserMovement(UserID, "3", CmbPatName.Text + "---" + TxtDescrption.Text, Me.Text)
            End If
            '    BtnNew_Click(sender, e)
            MessageBox.Show("تم حفظ البيــــانات بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("لم يتم حفظ البيــــانات ", "خطاء في عملية الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub

    Private Sub BtnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFind.Click
        If TxtFind.Text = Nothing Then Exit Sub
        Sql = "SELECT     NoPr, NoPatient, PatientName, Relate, Descrtion, DateReg, Emp_no, FullName FROM   TbViewInfoPatients"
        Sql += " Where NoPr='" + TxtFind.Text + "'"
        Tb = MyClss.GetRecords(Sql)
        FindTest = True
        If Tb.Rows.Count > 0 Then

            TxtNoD.Text = Tb.Rows(0).Item("NoPr")
            TxtNoEmp.Text = Tb.Rows(0).Item("Emp_no")
            TxtName.Text = Tb.Rows(0).Item("FullName")
            TxtRelated.Text = Tb.Rows(0).Item("Relate")
            TxtDescrption.Text = Tb.Rows(0).Item("Descrtion")
            DateReg.Value = Tb.Rows(0).Item("DateReg")
            TablelPanel1.Enabled = True

            Sql = "Select NoPatient,PatientName,Relate from TbPatients Where IsActive='true' And NoEmp='" + TxtNoEmp.Text.ToString + "'"
            TbPatInfo = MyClss.GetRecords(Sql)
            If TbPatInfo.Rows.Count > 0 Then
                CmbPatName.DataSource = TbPatInfo
                CmbPatName.ValueMember = TbPatInfo.Columns(0).ColumnName
                CmbPatName.DisplayMember = TbPatInfo.Columns(1).ColumnName
                CmbPatName.Text = Tb.Rows(0).Item("PatientName")
            End If
            TestNewSave = False

            LastInfoPat(TxtNoEmp.Text)

            ClearDrugs()

        Else
            BtnNew_Click(sender, e)
            MessageBox.Show("اسف .. لا توجد بيانات .. الرجاء التحقق من معاملات البحث", "خطاء في البحث", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TxtFind.Focus()
        End If
    End Sub

    Private Sub TxtFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFind.Click

    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        If DGVItems.RowCount = 0 Then Exit Sub
        SqlRep = "Select * From TbViewMedicenPat Where NoPr='" + TxtNoD.Text + "'"
        strReport = "RepMedicen"
        FrmRepPreview.Show()
        '  MsgBox(DVInfo.GetDriveInfo("C:"))
    End Sub

    Private Sub BtnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDel.Click
        Dim resault As Integer

        If TxtNoD.Text = 0 Or TxtNoD.Text = Nothing Then Exit Sub

        resault = MessageBox.Show("هل انت متأكد من عملية حذف البيانات ؟", "رسالة حذف ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If resault = vbYes Then
            Sql = "DELETE FROM TbMedicenPat   WHERE NoPr ='" + TxtNoD.Text.ToString + "'"
            MyClss.RunsInsertDeleteUpdateQry(Sql)

            Sql = "DELETE FROM TbPrescriptions   WHERE ([NoPr] ='" + TxtNoD.Text.ToString + "')"
            MyClss.RunsInsertDeleteUpdateQry(Sql)
            If TestRunCode = True Then
                '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
                MyClss.UserMovement(UserID, "4", CmbPatName.Text + "---" + TxtDescrption.Text, Me.Text)

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

    Private Sub BtnFindEmp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFindEmp.Click
        FindTest = False
        FrmFindEmp.ShowDialog()
    End Sub

    Private Sub ChkIsSame_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkIsSame.CheckedChanged
        If TxtNoEmp.Text = Nothing Or TxtNoEmp.Text = 0 Then Exit Sub

        If ChkIsSame.Checked = True Then
            Dim N As Int16 = 0
            Sql = "Select Count(*) From TbPatients Where PatientName like N'%" & TxtName.Text.Trim & "%' And  NoEmp='" + TxtNoEmp.Text.ToString + "'"
            N = MyClss.ExecuteScalar(Sql)

            If N = 0 Then
                Sql = "INSERT INTO TbPatients(PatientName, NoEmp, IsActive,Relate)"
                Sql += "VALUES (N'" + TxtName.Text.Trim + "','" + TxtNoEmp.Text.ToString + "','True',N'الزوج')"
                MyClss.RunsInsertDeleteUpdateQry(Sql)
                '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
                MyClss.UserMovement(UserID, "2", TxtName.Text, Me.Text)
                'Else
                'Sql = "UPDATE TbPatients SET [PatientName] = N'" + Fname + "', [IsActive] = '" + ChkIsActive.Checked.ToString + "', [NoEmp] = '" + NoEmp.ToString + "',[Relate]='" + CmbRelEmp.Text + "' WHERE ([NoPatient] ='" + DGVItems.CurrentRow.Cells("NoPatient").Value.ToString + "')"
            End If

        Else
            CmbPatName.Text = Nothing
        End If

        Sql = "Select NoPatient,PatientName,Relate from TbPatients Where IsActive='true' And NoEmp='" + TxtNoEmp.Text.ToString + "'"
        TbPatInfo = MyClss.GetRecords(Sql)
        If TbPatInfo.Rows.Count > 0 Then
            CmbPatName.DataSource = TbPatInfo
            CmbPatName.ValueMember = TbPatInfo.Columns(0).ColumnName
            CmbPatName.DisplayMember = TbPatInfo.Columns(1).ColumnName
        End If

        If ChkIsSame.Checked = True Then CmbPatName.Text = TxtName.Text
    End Sub

    Private Sub BtnPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPatient.Click
        If TxtNoEmp.Text = "0" Or TxtNoEmp.Text = Nothing Then
            MessageBox.Show("أسف .. من فضلك قم باختيار اسم الموظف من نافذة بحث عن الموظفين ", "رسالة خطاء", MessageBoxButtons.OK)
            Exit Sub
        End If
        NoEmpl = TxtNoEmp.Text
        FrmRelatePatient.ShowDialog()
    End Sub

    Private Sub BtnDrugs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDrugs.Click
        FrmTypeDrugs.ShowDialog()
    End Sub

    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        'التحقق من البيانات قبل الحفظ
        If CmbTypeDrugs.Text = Nothing Then
            MessageBox.Show("اسف .. من فضلك قم بكتابة إسم الدواء في مربع الادخال ...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbTypeDrugs.Focus()
            Exit Sub
        End If


        If NumQDrug.Value = 0 Then
            MessageBox.Show("اسف .. من فضلك قم بكتابة الكمية في مربع الادخال ...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            NumQDrug.Focus()
            Exit Sub
        End If

        'اضافة الكميات للصنف في حالة موجود في الجدول
        If TestNewSave = False And TestNewSave2 = True Then
            Sql = "Select ABS(Qt)  from TbMedicenPat where NoPr='" + TxtNoD.Text  + "' and NoMd='" + CmbTypeDrugs.SelectedValue.ToString + "'"
            Tb = MyClss.GetRecords(Sql)
            If Tb.Rows.Count = 0 Then GoTo 10

            Dim n = Tb.Rows(0).Item(0)
            If n > 0 Then
                n += NumQDrug.Value
                Sql = "UPDATE   TbMedicenPat SET    Qt ='" + n.ToString + "' where NoPr='" + TxtNoD.Text + "' and NoMd='" + CmbTypeDrugs.SelectedValue.ToString + "'"
                MyClss.RunsInsertDeleteUpdateQry(Sql)
                ClearDrugs()
                Exit Sub
            End If
        End If
        '=============================================================================================

10:     If TestNewSave2 = True Then
            Sql = "INSERT INTO TbMedicenPat(NoPr,NoPatien,NoMd,Qt)"
            Sql += "VALUES ('" + TxtNoD.Text + "','" + CmbPatName.SelectedValue.ToString + "','" + CmbTypeDrugs.SelectedValue.ToString + "','" + NumQDrug.Value.ToString + "')"
        Else
            Sql = "UPDATE TbMedicenPat SET [NoPr] = '" + TxtNoD.Text + "', [NoPatien] = '" + CmbPatName.SelectedValue.ToString + "', [NoMd] = '" + CmbTypeDrugs.SelectedValue.ToString + "', [Qt] = '" + NumQDrug.Value.ToString + "'"
            Sql += " WHERE ([NoEx] ='" + DGVItems.CurrentRow.Cells("NoMd").Value.ToString + "')"
        End If
        MyClss.RunsInsertDeleteUpdateQry(Sql)
        If TestRunCode = True Then
            Dim str As String = TxtNoD.Text + "--" + CmbPatName.Text.ToString + "--" + CmbTypeDrugs.Text.ToString + "--" + NumQDrug.Value.ToString
            If TestNewSave2 = True Then
                '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
                MyClss.UserMovement(UserID, "2", str, Me.Text)
            Else
                MyClss.UserMovement(UserID, "3", str, Me.Text)
            End If
            ClearDrugs()
            MessageBox.Show("تم حفظ البيــــانات بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("لم يتم حفظ البيــــانات ", "خطاء في عملية الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Dim resault As Integer
        Dim sql As String = "DELETE FROM TbMedicenPat   WHERE ([NoEx] ='" + DGVItems.CurrentRow.Cells("NoEx").Value.ToString + "')"
        resault = MessageBox.Show("هل انت متأكد من عملية حذف البيانات ؟", "رسالة حذف ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If resault = vbYes Then
            MyClss.RunsInsertDeleteUpdateQry(sql)
            If TestRunCode = True Then
                '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
                '   MyClss.UserMovement(UserID, "4", TxtDrags.Text, Me.Text)

                ClearDrugs()
                MessageBox.Show("لقد تم حذف البيانات ", "نجاح عملية الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
            Else
                MessageBox.Show("لم يتم حذف البيــــانات ", "خطاء في عملية الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
        Else
            MessageBox.Show("تم ايقاف عملية الحذف", "حذف سجل", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        End If
    End Sub

    Private Sub DGVItems_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVItems.CellClick
        If DGVItems.RowCount = 0 Then Exit Sub
        ', NameMd, , DateReg, Notes
        With DGVItems.CurrentRow
            CmbTypeDrugs.SelectedValue = .Cells("NoMd").Value
            NumQDrug.Value = .Cells("Qt").Value
        End With
        TestNewSave2 = False
        BtnDelete.Enabled = True
    End Sub

    Private Sub DGVItems_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVItems.CellContentClick

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Sql = "Select NoPatient,PatientName from TbPatients Where IsActive='true' And NoEmp='" + NoEmpl.ToString + "'"
        FILLCOMBOBOXITEMS2(Sql, CmbPatName)
        AutocompleteSql(Sql, CmbPatName)

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

    End Sub

    'Private Sub CmbPatName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbPatName.SelectedIndexChanged
    '    'On Error Resume Next
    '    'If NoEmpl <> 0 Or NoEmpl <> Nothing Then
    '    '    Sql = "Select Relate from TbPatients Where NoPatient='" + CmbPatName.SelectedValue.ToString + "'"
    '    '    Tb = MyClss.GetRecords(Sql)
    '    '    If Tb.Rows.Count > 0 Then CmbRelated.Text = Tb.Rows(0).Item(0).ToString
    '    '    'IsActive='true' And NoEmp='" + NoEmpl.ToString + "'"
    '    'End If
    'End Sub

    Private Sub BtnRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRep.Click
        FrmRepFinds.ShowDialog()
    End Sub

  
End Class