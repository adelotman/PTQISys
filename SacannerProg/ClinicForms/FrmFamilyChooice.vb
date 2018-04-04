Public Class FrmFamilyChooice

    Private Sub BtnOk_Click(sender As System.Object, e As System.EventArgs) Handles BtnOk.Click
        If DGViewEmp.RowCount = 0 Then Exit Sub

        If MessageBox.Show("   هل ترغب في حفظ بيانات الاسرة ؟", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        TestRunCode = False
        Dim N As Int16 = 0
        For i As Int16 = 0 To DGViewEmp.RowCount - 1

            If DGViewEmp.Rows(i).Cells("ChkFamily").Value = True Then
                Dim Fname, Related As String
                Fname = DGViewEmp.Rows(i).Cells("Fname").Value.ToString
                Related = DGViewEmp.Rows(i).Cells("Related").Value.ToString
                Sql = "Select Count(*) From TbPatients Where PatientName=N'" + Fname.ToString.Trim + "' And Relate=N'" + Related.ToString.Trim + "' And  NoEmp='" + NoEmpl.ToString + "'"
                N = MyClss.ExecuteScalar(Sql)

                If N = 0 Then
                    Sql = "INSERT INTO TbPatients(PatientName, NoEmp, IsActive,Relate)"
                    Sql += "VALUES (N'" + Fname.ToString + "','" + NoEmpl.ToString + "','True',N'" + Related + "')"
                    MyClss.RunsInsertDeleteUpdateQry(Sql)
                    '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
                    MyClss.UserMovement(UserID, "2", Fname, Me.Text)
                    'Else
                    'Sql = "UPDATE TbPatients SET [PatientName] = N'" + Fname + "', [IsActive] = '" + ChkIsActive.Checked.ToString + "', [NoEmp] = '" + NoEmp.ToString + "',[Relate]='" + CmbRelEmp.Text + "' WHERE ([NoPatient] ='" + DGVItems.CurrentRow.Cells("NoPatient").Value.ToString + "')"
                End If

            End If
        Next
        If TestRunCode = True Then
            MessageBox.Show("تم حفظ بيانات افراد الاسرة بنجاح", "نجاح الحفظ", MessageBoxButtons.OK)
        End If
        'If N = 0 Then
        '    If MessageBox.Show(" أسف لم يتم اختيار الزوج او الزوجة من الجدول .. هل ترغب في الاستمرار", "خطاء في الاختيار", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) = Windows.Forms.DialogResult.No Then
        '        Exit Sub
        '    Else
        '        Me.Close()
        '    End If

        'Else
        '    Me.Close()
        'End If
    End Sub

    Private Sub FrmFamilyChooice_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        ConvertTabKeyToEnterKey(e)
        If e.Control And e.KeyCode.ToString = "S" Then
            FrmFindEmp.ShowDialog()
        End If

        If e.KeyCode = Keys.F2 And BtnOk.Visible = True Then BtnOk_Click(sender, e)
        If e.KeyCode = Keys.F3 And BtnPrint.Visible = True Then BtnPrint_Click(sender, e)
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F12 Then BtnExit_Click(sender, e)
    End Sub

    Private Sub FrmFamilyChooice_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If NoEmpl = Nothing Or NoEmpl = 0 Then Me.Close()

        '  TxtEmpName.Text = FrmOrderEmpTrying.TxtFullName.Text


        Dim DSFamliy As New DataSet
        DSFamliy = MyClss.GetRecordsByDataSet("Select ID,Fname,birthDate,Sex,Related,State,Notes from FamilyFolder where Emp_no='" & NoEmpl.ToString & "'", "")
        TxtNoF.Text = DSFamliy.Tables(0).Rows.Count
        TxtFamilyF.Text = "عدد افراد الاسرة"
        TxtSonsF.Text = "عدد الابناء"
        Sql = "SELECT     COUNT(*) AS Counts    FROM FamilyFolder WHERE     Related <> N'الزوجة' AND Emp_no ='" & NoEmpl.ToString & "'"
        TxtSons.Text = MyClss.ExecuteScalar(Sql) ' TxtNoF.Text
        DGViewEmp.Columns.Clear()
        DGViewEmp.DataSource = DSFamliy.Tables(0)
        'انشاء عمود جديد من مربع الاختيار Chkbox
        Dim NewColumn1 As New DataGridViewCheckBoxColumn
        NewColumn1.Name = "ChkFamily"

        'ادخال العمود الجديد للجريد الرئيسية
        '  DGViewEmp.Columns.Clear()

        BtnOk.Visible = True
        BtnPrint.Visible = False

        'If myFamilyTable.Rows.Count > 0 Then
        '    With myFamilyTable
        '        For i As Int16 = 0 To .Rows.Count - 1
        '            For j As Int16 = 0 To DGViewEmp.Rows.Count - 1
        '                If myFamilyTable.Rows(i).Item(0) = DGViewEmp.Rows(j).Cells("ID").Value Then
        '                    DGViewEmp.Rows(j).Cells("ChkFamily").Value = True
        '                    Exit For
        '                End If

        '            Next
        '        Next
        '    End With
        'End If

        DGViewEmp.Columns.Insert(0, NewColumn1)
        'DGViewEmp.Columns(0).ReadOnly = True
        TxtFamilyF.Text = "عدد افراد الاسر ة"

        With DGViewEmp
            .RowsDefaultCellStyle.BackColor = Color.Bisque
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Beige
            .Columns(0).HeaderText = "اختيار"
            .Columns(1).HeaderText = "ر.م"
            .Columns(2).HeaderText = "اســم العائلــة"
            .Columns(3).HeaderText = "تاريخ الميلاد"
            .Columns(4).HeaderText = "النوع"
            .Columns(5).HeaderText = "صلة القرابة"
            .Columns(6).HeaderText = "الحالة الاجتماعية"
            .Columns(7).HeaderText = "ملاحظـــــات"
            .Columns(1).Width = 100
            .Columns(2).Width = 200
            .Columns(3).Width = 110
            .Columns(4).Width = 100
            .Columns(5).Width = 100
            .Columns(6).Width = 100
            .Columns(7).Width = 140

            .Columns(1).Visible = False
            .Columns(2).ReadOnly = True
            .Columns(3).ReadOnly = True
            .Columns(4).ReadOnly = True
            .Columns(5).ReadOnly = True
            .Columns(6).ReadOnly = True
            .Columns(7).ReadOnly = True


        End With


        Dim newDate As DateTime = DateAdd(DateInterval.WeekOfYear, 7, Date.Now)


    End Sub

    Private Sub BtnExit_Click(sender As System.Object, e As System.EventArgs) Handles BtnExit.Click
        ' FrmOrderEmpTrying.BtnDisplayFamliy.Enabled = False
        Me.Close()
    End Sub

    Private Sub BtnPrint_Click(sender As System.Object, e As System.EventArgs) Handles BtnPrint.Click

    End Sub
End Class