Public Class FrmChangePassword
    Dim TxtOldPassword As String

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        TxtPassWord.Clear()
        TxtNewPassWord.Clear()
        TxtConfirmPassWord.Clear()
        TxtPassWord.Focus()
        TxtPassWord.PasswordChar = "*"
    End Sub

    Private Sub FrmChangePassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Me.KeyPreview = True
        ConvertTabKeyToEnterKey(e)
        If e.KeyCode = Keys.F2 Then BtnNew_Click(sender, e)
        If e.KeyCode = Keys.F3 Then BtnSave_Click(sender, e)
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F12 Then BtnExit_Click(sender, e)

    End Sub

    Private Sub FrmChangePassword_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BtnNew_Click(sender, e)
        If UserID = 0 Or UserID = Nothing Then
            BtnSave.Enabled = False
            TxtPassWord.Enabled = False
            TxtNewPassWord.Enabled = False
            TxtConfirmPassWord.Enabled = False
            MessageBox.Show("اسف .. الرجاء الدخول من نافذة لكي يتم تسجيل بيانات الدخول الخاص بك", "خطاء في دخول النظام", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Else
            BtnSave.Enabled = True
            TxtPassWord.Enabled = True
            TxtNewPassWord.Enabled = True
            TxtConfirmPassWord.Enabled = True
        End If
        Dim Tb As New DataTable
        Sql = "Select Password From Users  WHERE UserID='" + UserID.ToString + "'"
        Tb = MyClss.GetRecords(Sql)
        If Tb.Rows.Count > 0 Then
            TxtOldPassword = Tb.Rows(0).Item(0).ToString
        End If

    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click

        If Me.TxtPassWord.Text.Trim = Nothing Then
            MessageBox.Show("اسف ..الرجاء كتابة كلمة المرور القديمة  .. ارجو التحقق", "خطاء في الادخال", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            TxtPassWord.Clear()
            TxtPassWord.Focus()
            Exit Sub
        End If

       

        


        Sql = "SELECT Password FROM Users WHERE UserName LIKE '" & USERNAME.Trim & "%' and Status='True'"
        Dim Tb As New DataTable
        Tb = MyClss.GetRecords(Sql)

        If Tb.Rows.Count > 0 Then
            TxtOldPassword = Tb.Rows(0).Item(0).ToString.Trim
            If TxtOldPassword <> TxtPassWord.Text Then
                MessageBox.Show("اسف .. كلمة المرور القديمة غير متطابقة .. ارجو التحقق", "خطاء في الادخال", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                TxtPassWord.SelectAll()
                TxtPassWord.Focus()
                Exit Sub
            End If
        Else
            MessageBox.Show("اسف ..هذا المستخدم غير مفعل .. الرجاء التحقق", "خطاء في الادخال", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If

        If Me.TxtNewPassWord.Text.Trim = Nothing Then
            MessageBox.Show("اسف ..الرجاء كتابة كلمة المرور الجديدة  .. ارجو التحقق", "خطاء في الادخال", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            TxtNewPassWord.Clear()
            TxtNewPassWord.Focus()
            Exit Sub
        End If

        If Me.TxtConfirmPassWord.Text.Trim = Nothing Then
            MessageBox.Show("اسف ..الرجاء كتابة كلمة المرور في مربع التأكيد  .. ارجو التحقق", "خطاء في الادخال", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            TxtConfirmPassWord.Clear()
            TxtConfirmPassWord.Focus()
            Exit Sub
        End If


        If Me.TxtOldPassword <> Me.TxtPassWord.Text.Trim Then
            MessageBox.Show("اسف .. كلمة المرور القديمة غير متطابقة .. ارجو التحقق", "خطاء في الادخال", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            TxtPassWord.Clear()
            TxtPassWord.Focus()
            Exit Sub
        End If


        If Me.TxtNewPassWord.Text.Trim <> Me.TxtConfirmPassWord.Text.Trim Then
            MessageBox.Show("اسف .. كلمة المرور غير متطابقة .. ارجو التحقق", "خطاء في الادخال", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            TxtConfirmPassWord.Clear()
            TxtNewPassWord.Focus()
            TxtNewPassWord.SelectAll()
            Exit Sub
        End If

        Sql = "Update Users SET Password='" + TxtNewPassWord.Text.Trim + "'  WHERE UserID='" + UserID.ToString + "'"
        MyClss.RunsInsertDeleteUpdateQry(Sql)
        If TestRunCode = True Then
            MessageBox.Show("تم حفظ البيــــانات بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Else
            MessageBox.Show("لم يتم حفظ البيــــانات ", "خطاء في عملية الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub TxtPassWord_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPassWord.LostFocus

    End Sub

    Private Sub TxtPassWord_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtPassWord.TextChanged

    End Sub

    Private Sub TxtNewPassWord_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNewPassWord.LostFocus
        If TxtConfirmPassWord.Text.Trim <> Nothing Then
            If Me.TxtNewPassWord.Text.Trim <> Me.TxtConfirmPassWord.Text.Trim Then
                MessageBox.Show("اسف .. كلمة المرور غير متطابقة .. ارجو التحقق", "خطاء في الادخال", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                TxtConfirmPassWord.Clear()
                TxtNewPassWord.Focus()
                TxtNewPassWord.SelectAll()
            End If
        End If
    End Sub

    Private Sub TxtNewPassWord_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNewPassWord.TextChanged
        'If Flag = True Then
        '    If Me.TxtNewPassWord.Text = Nothing Then
        '        Me.BtnEidt.Enabled = False
        '        Me.TxtConfirmPassWord.Enabled = False
        '    Else
        '        Me.BtnEidt.Enabled = True
        '        Me.TxtConfirmPassWord.Enabled = True
        '    End If
        'End If
        'If Flag = False Then
        '    If Me.TxtNewPassWord.Text = Nothing Then
        '        Me.TxtConfirmPassWord.Enabled = False
        '        Me.BtnSave.Enabled = False
        '    Else
        '        Me.TxtConfirmPassWord.Enabled = True
        '    End If
        'End If
    End Sub

    Private Sub TxtConfirmPassWord_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtConfirmPassWord.LostFocus
        If TxtNewPassWord.Text.Trim <> Nothing Then
            If Me.TxtNewPassWord.Text.Trim <> Me.TxtConfirmPassWord.Text.Trim Then
                MessageBox.Show("اسف .. كلمة المرور غير متطابقة .. ارجو التحقق", "خطاء في الادخال", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                TxtConfirmPassWord.Clear()
                TxtNewPassWord.Focus()
                TxtNewPassWord.SelectAll()
            End If
        End If
    End Sub

    Private Sub TxtConfirmPassWord_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtConfirmPassWord.TextChanged

    End Sub

    'Sub DisplayEndWorks(ByVal DateEnd As Date)
    '    Dim TxtEndWork1 As String

    '    TxtEndWork1 = TakremCalc(DateAndTime.Now, DateEnd)
    '    Dim DateNow As Date = Date.Now.ToShortDateString
    '    If DateEnd < DateNow Then
    '        Label46.BackColor = Color.Red
    '        Label46.Text = "صلاحيـــة القعـد منتهيـــة"
    '        TxtEndWorkDays.Text = 0
    '        TxtEndWorkMonth.Text = 0
    '        TxtEndWorkYear.Text = 0
    '        TxtEndWorkYear.BackColor = Color.Red
    '        TxtEndWorkMonth.BackColor = Color.Red
    '        TxtEndWorkDays.BackColor = Color.Red
    '    ElseIf DateEnd.Year = Date.Now.Year Then
    '        TxtEndWorkYear.Text = 0
    '        TxtEndWorkYear.BackColor = Color.Red
    '        If DateEnd.Month <= Date.Now.Month Then
    '            TxtEndWorkMonth.Text = 0
    '            TxtEndWorkMonth.BackColor = Color.Red
    '            If DateEnd.Day <= Date.Now.Day Then
    '                TxtEndWorkDays.Text = 0
    '                TxtEndWorkDays.BackColor = Color.Red
    '            Else
    '                TxtEndWorkDays.Text = TimeEndWork(2)
    '                TxtEndWorkDays.BackColor = Color.Yellow
    '            End If
    '        Else
    '            TxtEndWorkDays.Text = TimeEndWork(2)
    '            TxtEndWorkMonth.Text = TimeEndWork(1)
    '            TxtEndWorkDays.BackColor = Color.Yellow
    '            TxtEndWorkMonth.BackColor = Color.Yellow
    '            If TxtEndWorkMonth.Text = 0 Then TxtEndWorkMonth.BackColor = Color.Red
    '        End If
    '    Else

    '        TxtEndWorkDays.Text = TimeEndWork(2)
    '        TxtEndWorkMonth.Text = TimeEndWork(1)
    '        TxtEndWorkYear.Text = TimeEndWork(0)
    '        TxtEndWorkDays.BackColor = Color.Yellow
    '        TxtEndWorkMonth.BackColor = Color.Yellow
    '        TxtEndWorkYear.BackColor = Color.Yellow
    '        If TxtEndWorkYear.Text = 0 Then TxtEndWorkYear.BackColor = Color.Red
    '    End If

    '    '=======================================================================================

    '    'If DateEnd.Year <= Date.Now.Year Then
    '    '    TxtEndWorkYear.Text = 0
    '    '    TxtEndWorkYear.BackColor = Color.Red
    '    '    If DateEnd.Month <= Date.Now.Month Then
    '    '        TxtEndWorkMonth.Text = 0
    '    '        TxtEndWorkMonth.BackColor = Color.Red
    '    '        If DateEnd.Day <= Date.Now.Day Then
    '    '            TxtEndWorkDays.Text = 0
    '    '            TxtEndWorkDays.BackColor = Color.Red
    '    '        Else
    '    '            TxtEndWorkDays.Text = TimeEndWork(2)
    '    '            TxtEndWorkDays.BackColor = Color.Yellow
    '    '        End If
    '    '    Else
    '    '        TxtEndWorkDays.Text = TimeEndWork(2)
    '    '        TxtEndWorkMonth.Text = TimeEndWork(1)
    '    '        TxtEndWorkDays.BackColor = Color.Yellow
    '    '        TxtEndWorkMonth.BackColor = Color.Yellow
    '    '        If TxtEndWorkMonth.Text = 0 Then TxtEndWorkMonth.BackColor = Color.Red
    '    '    End If
    '    'Else

    '    '    TxtEndWorkDays.Text = TimeEndWork(2)
    '    '    TxtEndWorkMonth.Text = TimeEndWork(1)
    '    '    TxtEndWorkYear.Text = TimeEndWork(0)
    '    '    TxtEndWorkDays.BackColor = Color.Yellow
    '    '    TxtEndWorkMonth.BackColor = Color.Yellow
    '    '    TxtEndWorkYear.BackColor = Color.Yellow
    '    '    If TxtEndWorkYear.Text = 0 Then TxtEndWorkYear.BackColor = Color.Red
    '    'End If
    '    If TxtEndWorkDays.Text = 0 And TxtEndWorkMonth.Text = 0 And TxtEndWorkYear.Text = 0 Then
    '        Label46.BackColor = Color.Red
    '        Label46.Text = "صلاحيـــة القعـد منتهيـــة"
    '        TxtEndWorkDays.Text = 0
    '        TxtEndWorkMonth.Text = 0
    '        TxtEndWorkYear.Text = 0
    '        TxtEndWorkYear.BackColor = Color.Red
    '        TxtEndWorkMonth.BackColor = Color.Red
    '        TxtEndWorkDays.BackColor = Color.Red
    '    Else
    '        Label46.Text = "الفترة المتبقية لانتهــــــــــــاء العقد"
    '        Label46.BackColor = Color.Blue
    '    End If

    'End Sub
End Class