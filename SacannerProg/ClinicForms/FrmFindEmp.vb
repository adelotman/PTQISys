Public Class FrmFindEmp
    Dim Sql As String
    Private Sub DGVDataEmp_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVDataEmp.CellContentClick

    End Sub

    Private Sub FrmFindEmp_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'تحميل البيانات للموظفين
        CmbTypeFind.SelectedIndex = 0
        MyDs.Clear()
        Sql = "Select Emp_no, FullName, NationalCard, ltrim(rtrim(natinality)), Mother_Name, ltrim(rtrim(Address)), MobileNo1, MobileNo2, Email, Sex, BirthDate, BirthPlace, TypeEmployment, SocialState, "
        Sql += "  BloodType, TypeEmp, ActionDate, SubMang1, MonskiaName, DepartMentName, UnitName,MaktabName, Notes,LevelEmpID from TbViewDataEmp Order By Emp_no"
        MyDs = MyClss.GetRecordsByDataSet(Sql, "")
        DGVDataEmp.DataSource = MyDs.Tables(0)
        TxtCountEmp.Text = DGVDataEmp.RowCount
        DisplayDataGrid()
        With Me.DGVDataEmp
            .RowsDefaultCellStyle.BackColor = Color.Bisque
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Beige
        End With
        'الغاء قيمة المتغيرات لرقم الموظف واسم الموظف
        NoEmpl = Nothing
        NameEmpl = Nothing
        'تحديث بيانات المريض
        FrmPatient.BtnNew_Click(sender, e)
        'FrmPatient.TxtNoEmp.Text = Nothing
        'FrmPatient.TxtName.Text = Nothing
        'FrmPatient.CmbPatName.DataSource = Nothing
        'FrmPatient.CmbPatName.Text = Nothing

    End Sub

    Private Sub BtnFind_Click(sender As System.Object, e As System.EventArgs) Handles BtnFind.Click
        'بحث عن بيانات الموظف
        Sql = "Select Emp_no, FullName, NationalCard,ltrim(rtrim(natinality)), Mother_Name, ltrim(rtrim(Address)), MobileNo1, MobileNo2, Email, Sex, BirthDate, BirthPlace, TypeEmployment, SocialState, "
        Sql += "  BloodType, TypeEmp, ActionDate, SubMang1, MonskiaName, DepartMentName, UnitName,MaktabName, Notes,LevelEmpID from TbViewDataEmp "
        If Me.TxtFind.Text = "" Then
            '  MsgBox("اسف !! الرجــــاء كتابة نص البحث المطلوب ", MsgBoxStyle.OkOnly, "رسالة خطاء")
            TxtFind.Focus()
            ' Sql = "Select * from TbViewDataEmp"
            GoTo 10
        End If
        Select Case CmbTypeFind.SelectedIndex
            Case Is = 0
                Sql += " WHERE (FullName) like N'%" & Trim(Me.TxtFind.Text) & "%' or (Emp_no) like '%" & Trim(Me.TxtFind.Text) & "%' or (StateName) like N'%" & Trim(Me.TxtFind.Text) & "%'"
            Case Is = 1
                Sql += " WHERE (Emp_no) like '%" & Trim(Me.TxtFind.Text) & "%'"
            Case Is = 2
                Sql += " WHERE (FullName) like N'%" & Trim(Me.TxtFind.Text) & "%'"
            Case Is = 3
                Sql += " WHERE (StateName) like N'%" & Trim(Me.TxtFind.Text) & "%'"
        End Select
10:     Sql += " Order by Emp_no"
        MyDs = MyClss.GetRecordsByDataSet(Sql, "")
        DGVDataEmp.DataSource = MyDs.Tables(0)
        TxtCountEmp.Text = DGVDataEmp.RowCount
        DisplayDataGrid()
    End Sub

    Sub DisplayDataGrid()
        DataViewName = MyDs.Tables(0).DefaultView
        With DGVDataEmp

            .Columns(0).HeaderText = "رقم الموظف"
            '  .Columns(0).Visible = False
            .Columns(1).HeaderText = "إسم الموظف"
            .Columns(2).HeaderText = "الرقم الوطني "
            .Columns(3).HeaderText = "الجنسية  "
            .Columns(4).HeaderText = "اسم الام  "
            .Columns(5).HeaderText = "العنوان"
            .Columns(6).HeaderText = " رقم الموبايل 1 "
            .Columns(7).HeaderText = " رقم الموبايل 2 "
            .Columns(8).HeaderText = "البريد الاكتورنني"
            .Columns(9).HeaderText = "النوع"
            .Columns(10).HeaderText = " تاريخ الميلاد "
            .Columns(11).HeaderText = "مكان الميلاد"
            .Columns(12).HeaderText = "نوع العقد"
            .Columns(13).HeaderText = "الحالة الاجتماعية"
            .Columns(14).HeaderText = " فصيلة الدم  "
            .Columns(15).HeaderText = "الوظيفة "
            .Columns(16).HeaderText = "تاريخ العلاوة "
            .Columns(17).HeaderText = "الادارة"

            .Columns(18).HeaderText = " المنسقية "
            .Columns(19).HeaderText = "القسم "
            .Columns(20).HeaderText = "الوحدة"
            .Columns(21).HeaderText = "مكتب"
            .Columns(22).HeaderText = "ملاحظات"
            .Columns(23).HeaderText = "المستوى الوظيفي"
            .Columns(0).Width = 100
            .Columns(1).Width = 200
            .Columns(2).Width = 100
            .Columns(3).Width = 100
            '.Columns(4).Width = 60
            '.Columns(5).Width = 60
            '.Columns(6).Width = 50
            '.Columns(7).Width = 60
        End With
    End Sub
    Private Sub TxtFind_Click(sender As System.Object, e As System.EventArgs) Handles TxtFind.Click

    End Sub

    Private Sub TxtFind_TextChanged(sender As Object, e As System.EventArgs) Handles TxtFind.TextChanged
        BtnFind_Click(sender, e)
    End Sub

    Private Sub CmbTypeFind_Click(sender As System.Object, e As System.EventArgs) Handles CmbTypeFind.Click

    End Sub

    Private Sub CmbTypeFind_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CmbTypeFind.SelectedIndexChanged
        TxtFind.Focus()
        TxtFind.SelectAll()
    End Sub

    Private Sub BtnExit_Click(sender As System.Object, e As System.EventArgs) Handles BtnExit.Click
        'اختيار رقم الموظف واسم الموظف من داتا قريد
        If DGVDataEmp.RowCount = 0 Then NoEmpl = Nothing : NameEmpl = Nothing : Me.Close() : Exit Sub
        NoEmpl = DGVDataEmp.CurrentRow.Cells(0).Value
        NameEmpl = DGVDataEmp.CurrentRow.Cells(1).Value
        Me.Close()
    End Sub

    Private Sub DGVDataEmp_DoubleClick(sender As Object, e As System.EventArgs) Handles DGVDataEmp.DoubleClick
        ' NoEmpl = DGVDataEmp.CurrentRow.Cells(0).Value
        ' Me.Close()
        BtnExit_Click(sender, e)
    End Sub
End Class