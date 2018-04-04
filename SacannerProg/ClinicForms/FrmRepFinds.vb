Public Class FrmRepFinds
    Dim R As New FormTools.ResizeControls()
    Dim TestNewSave, TestNewSave2 As Boolean, Sql As String
    Dim Tb As New DataTable
    Dim TbPatInfo As New DataTable
    Dim FindTest As Boolean = False
    Private Sub FrmRepFinds_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.HandleCreated
        R.Container = Me    'شاشة
    End Sub

    Private Sub FrmRepFinds_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        ConvertTabKeyToEnterKey(e)
        If e.KeyCode = Keys.F2 Then BtnRef_Click(sender, e)
        If e.KeyCode = Keys.F3 Then BtnFind_Click(sender, e)
        If e.KeyCode = Keys.F4 Then BtnPrint_Click(sender, e)
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F12 Then Me.Close() ' BtnExit_Click(sender, e)

    End Sub

    Private Sub FrmRepFinds_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BtnRef_Click(sender, e)
    End Sub

    Private Sub FrmRepFinds_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        R.ResizeControls()
    End Sub

    Private Sub BtnRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRef.Click
        With Me.DGViewEmp
            .RowsDefaultCellStyle.BackColor = Color.Bisque
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Beige
        End With

        With Me.DGVItems
            .RowsDefaultCellStyle.BackColor = Color.Bisque
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Beige
        End With

        OpNoEmp.Checked = True
        ChkByDate.Checked = False
        FromDate.Value = Date.Now
        ToDate.Value = Date.Now
        FromDate.Enabled = ChkByDate.Checked
        ToDate.Enabled = ChkByDate.Checked
        ChkByRelate.Checked = False
        CmbRelEmp.Enabled = ChkByRelate.Checked
        TxtFind.Clear()
        DGViewEmp.DataSource = Nothing
        DGVItems.DataSource = Nothing
        TxtNoItems.Text = 0
    End Sub

    Private Sub BtnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFind.Click
        If TxtFind.Text = Nothing Then Exit Sub
        Sql = "SELECT     NoPr, NoPatient, PatientName, Relate, Descrtion, DateReg, Emp_no, FullName FROM   TbViewInfoPatients"
        If OpNoEmp.Checked Then Sql += " Where Emp_no='" + TxtFind.Text + "'"
        If OpNameEmp.Checked Then Sql += " Where  FullName like N'%" & TxtFind.Text.Trim & "%'"
        If OpPat.Checked Then Sql += " Where  PatientName like N'%" & TxtFind.Text.Trim & "%'"
        If OpNoMad.Checked Then Sql += " Where NoPr='" + TxtFind.Text + "'"

        If ChkByDate.Checked Then
            If FromDate.Value > ToDate.Value Then
                MessageBox.Show("أسف .. يجب أن يكون التاريخ بين فترين متناسبة !", "خطاء في التاريخ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                FromDate.Focus()
                Exit Sub
            End If
            Sql += " And DateReg >='" + Format(FromDate.Value, "yyyy-MM-dd").ToString + "' And DateReg<='" + Format(ToDate.Value, "yyyy-MM-dd").ToString + "'"
        End If

        If ChkByRelate.Checked Then
            If CmbRelEmp.Text = Nothing Then
                MessageBox.Show("أسف .. إختيار صلة القرابة من مربع الادخال !", "خطاء في صلة القرابة", MessageBoxButtons.OK, MessageBoxIcon.Error)
                CmbRelEmp.Focus()
                Exit Sub
            End If
            Sql += " And Relate like N'%" & CmbRelEmp.Text.Trim & "%'"
        End If

        Tb = MyClss.GetRecords(Sql)
        FindTest = True
        DGViewEmp.DataSource = Nothing
        DGVItems.DataSource = Nothing
        If Tb.Rows.Count > 0 Then
            DGViewEmp.DataSource = Tb
            'NoPr, , , , , DateReg, , 
            With DGViewEmp
                .Columns("NoPatient").Visible = False
                '.Columns("NoPr").Visible = False
                '.Columns("NoEx").Visible = False
                '.Columns("NoPatien").Visible = False
                .Columns("NoPr").HeaderText = "رقم الوصفة"
                .Columns("NoPatient").HeaderText = "رقم المريض  "
                .Columns("PatientName").HeaderText = "اسم  المريض"
                .Columns("Relate").HeaderText = "صلة القرابة"
                .Columns("Descrtion").HeaderText = "وصف العلاج"
                '.Columns("NameMd").Width = 250
                .Columns("DateReg").HeaderText = "تاريخ التسجيل"
                .Columns("DateReg").Width = 120
                .Columns("Emp_no").HeaderText = "رقم الموظف"
                '.Columns("Qt").Width = 100
                .Columns("FullName").HeaderText = "إسم الموظف"
                '.Columns("Notes").Width = 300

            End With

            TestNewSave = False
        Else
            'BtnNew_Click(sender, e)
            MessageBox.Show("اسف .. لا توجد بيانات .. الرجاء التحقق من معاملات البحث", "خطاء في البحث", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TxtFind.Focus()
        End If
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        If DGVItems.RowCount = 0 Or DGViewEmp.RowCount = 0 Then Exit Sub
        SqlRep = "Select * From TbViewMedicenPat Where NoPr='" + DGViewEmp.CurrentRow.Cells("NoPr").Value.ToString + "'"
        strReport = "RepMedicen"
        FrmRepPreview.Show()
    End Sub

    Private Sub TxtFind_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtFind.KeyDown
        If TxtFind.Text = Nothing Then Exit Sub
        If e.KeyCode = Keys.Enter Then BtnFind_Click(sender, e)
    End Sub

    Private Sub TxtFind_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFind.TextChanged

    End Sub

    Private Sub ChkByDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkByDate.CheckedChanged
        FromDate.Enabled = ChkByDate.Checked
        ToDate.Enabled = ChkByDate.Checked
    End Sub

    Private Sub ChkByRelate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkByRelate.CheckedChanged
        CmbRelEmp.Enabled = ChkByRelate.Checked
        CmbRelEmp.SelectedIndex = -1
        If CmbRelEmp.Enabled = True Then CmbRelEmp.SelectedIndex = 0
    End Sub
    Sub DisplayItems()
        DGVItems.DataSource = Nothing
        Dim TbItems As New DataTable
        Sql = "SELECT   NoEx, NoPr, NoPatien, NoMd, NameMd, Qt, DateReg, Notes FROM  TbViewMedicenPat where NoPr='" + DGViewEmp.CurrentRow.Cells("NoPr").Value.ToString + "'"
        TbItems.Clear()
        TbItems = MyClss.GetRecords(Sql)
        DGVItems.DataSource = TbItems
        With DGVItems
            .Columns("NoMd").Visible = False
            .Columns("NoPr").Visible = False
            .Columns("NoEx").Visible = False
            .Columns("NoPatien").Visible = False
            .Columns("NameMd").HeaderText = "اسم  الدواء"
            .Columns("NameMd").Width = 250
            .Columns("DateReg").HeaderText = "تاريخ التسجيل"
            .Columns("DateReg").Width = 120
            .Columns("Qt").HeaderText = "الكمية"
            .Columns("Qt").Width = 100
            .Columns("Notes").HeaderText = "ملاحظات"
            .Columns("Notes").Width = 300
        End With

        TxtNoItems.Text = DGVItems.RowCount

    End Sub
    Private Sub DGViewEmp_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGViewEmp.CellClick
        If DGViewEmp.Rows.Count = 0 Then Exit Sub
        DisplayItems()
    End Sub

    Private Sub DGViewEmp_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGViewEmp.CellContentClick

    End Sub

    Private Sub DGViewEmp_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGViewEmp.KeyUp
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            DisplayItems()
        End If
    End Sub

    Private Sub OpNoEmp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpNoEmp.CheckedChanged
        TxtFind.Focus()
    End Sub

    Private Sub OpNameEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpNameEmp.Click
        TxtFind.Focus()
    End Sub

    Private Sub OpNoMad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpNoMad.Click
        TxtFind.Focus()
    End Sub

    Private Sub OpPat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpPat.Click
        TxtFind.Focus()
    End Sub
End Class