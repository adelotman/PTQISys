Public Class FrmLogin
    Public M, T As Int16
    Dim DsTest As New DataSet
    Private Sub BtnLogUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLogUser.Click


        BtnLogUser.Enabled = False

        Cursor = Cursors.WaitCursor
        '«· √ﬂœ „‰ √‰ «·„” Œœ„ ﬁœ «Œ «— «”„ „” Œœ„ „‰ «·ﬁ«∆„…
        If Me.CmbUserName.Text = "" Then

            MsgBox(" „‰ ›÷·ﬂ .. «Œ — «”„ «·„” Œœ„ „‰ «·ﬁ«∆„… ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "«ﬂ » «”„ »—‰«„Ãﬂ")
            BtnLogUser.Enabled = True
            Cursor = Cursors.Default
            Exit Sub

        End If
        If Me.TxtPassword.Text = "" Then

            MsgBox(" „‰ ›÷·ﬂ .. «ﬂ » ﬂ·„… «·„—Ê— ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "«ﬂ » «”„ »—‰«„Ãﬂ")
            BtnLogUser.Enabled = True
            Me.TxtPassword.Focus()
            Cursor = Cursors.Default
            Exit Sub

        End If
        '«” œ⁄«¡ «·« ’«· «·⁄«„ »ﬁÊ«⁄œ »Ì«‰«  «·”Ì—›—

        Try
            '„⁄—›… Õ«·… «·« ’«· «·⁄«„ ›≈–« ﬂ«‰ €Ì— „› ÊÕ

            ' If GSQLConnection.State <> ConnectionState.Open Then
            '⁄‰œÂ« «” œ⁄Ì ≈Ã—«¡ «·« ’«· «·⁄«„ „‰ „œÌÊ· «·« ’«·
            ' GeneralConnection()

            'End If
            If Cn.State = ConnectionState.Closed Then Cn.Open()
            Dim GSQLCmd As SqlClient.SqlCommand = Cn.CreateCommand
            'If GSQLConnection.State = ConnectionState.Open Then
            If Cn.State = ConnectionState.Open Then
                GDR = Nothing

                '  GSQLCmd.CommandType = CommandType.Text
                ' GSQLCmd.CommandText = "SELECT * FROM Users WHERE UserName LIKE '" & Me.CmbUserName.Text & "%' and Status='True'"
                '  GDR = GSQLCmd.ExecuteReader
                ' GDR.Read()




                Dim SqlStr As String = "SELECT * FROM Users WHERE UserName LIKE '" & Me.CmbUserName.Text & "%' and Status='True'"

                DsTest = MyClss.GetRecordsByDataSet(SqlStr, "")
                If DsTest.Tables(0).Rows.Count > 0 Then
                    USERNAME = Me.CmbUserName.Text
                    UserID = DsTest.Tables(0).Rows(0).Item("UserID")

                    Dim IsLogin As Boolean = DsTest.Tables(0).Rows(0).Item("IsLogin")
                    If IsLogin = True Then
                        If MessageBox.Show("√”› ·„  ﬁ„ » ”ÃÌ· «·Œ—ÊÃ ›Ì «·„—… «·”«»ﬁ… ... Â·  —€» ›Ì  ”ÃÌ· «·Œ—ÊÃ ·  „ﬂ‰ „‰ œŒÊ· «·Ï «·„‰ŸÊ„… ø", " ”ÃÌ· Œ—ÊÃ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then Exit Sub
                        SqlStr = "UPDATE Users SET [IsLogin] ='False' WHERE UserID='" + UserID.ToString + "'"
                        MyClss.RunsInsertDeleteUpdateQry(SqlStr)
                        If TestRunCode = False Then
                            MessageBox.Show("√”› ·„ Ì „ﬂ‰ «·‰Ÿ«„ „‰  ”ÃÌ· «·Œ—ÊÃ .. «·—Ã«¡ «·„Õ«Ê·… „—… «Œ—Ï", "Œÿ«¡ ›Ì «·Õ›Ÿ", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                            Exit Sub
                        End If
                        '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
                        MyClss.UserMovement(UserID, "8", USERNAME + "  ”ÃÌ· Œ—ÊÃ «·„” Œœ„", Me.Text)
                    End If

                    '›Ì Õ«·… „”«Ê«… ﬂ·„… «·„—Ê— «·„œŒ·… ·ﬂ·„… «·„—Ê— «·„ÊÃÊœ… ›Ì ﬁ«⁄œ… «·»Ì«‰« 
                    If DsTest.Tables(0).Rows(0).Item("Password") = Me.TxtPassword.Text Then
                        MsgBox(" «‰  «·¬‰ „ ’· »ﬁ«⁄œ… «·»Ì«‰«  »‰Ã«Õ ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "«ﬂ » «”„ »—‰«„Ãﬂ")
                        Timer1.Enabled = False
                        '1 New - 2 Insert - 3  Edit  - 4 Del - 5 Print - 6 Find - 7 Login - 8 Logout
                        MyClss.UserMovement(UserID, "7", USERNAME + " œŒÊ· «·„” Œœ„", Me.Text)


                        '  CloseOpenBtnMenue(1)
                        ' CloseOpenBtnMenue(2)
                        '  FrmMainMenue.Show()
                        Dim LST As New List(Of String)
                        Dim Test As Boolean = False
                        For Each Frm As Form In My.Application.OpenForms
                            '   LST.Add(Frm.Name)
                            If Frm.Name = "FrmMain" Then
                                Test = True
                            End If
                        Next
                        If Test = False Then MainForm.Show() 'FormTest.Show()
                        MainForm.Show()
                        TxtPassword.Clear()
                        Me.Hide()
                        'End If

                        '≈€·ﬁ ﬂ«∆‰ ﬁ«—∆ «·»Ì«‰« 
                        '    GDR.Close()

                        '-------------------------------------------------------

                        '-------------------------------------------------------
                    ElseIf DsTest.Tables(0).Rows(0).Item("Password") <> Me.TxtPassword.Text Then
                        'GSQLConnection.Close()
                        Cn.Close()
                        MsgBox(" „‰ ›÷·ﬂ .. «ﬂ » ﬂ·„… «·„—Ê— «·’ÕÌÕ… ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "«ﬂ » «”„ »—‰«„Ãﬂ")
                        Me.TxtPassword.Focus()
                        Me.TxtPassword.SelectAll()
                        ' GDR.Close()

                    End If
                End If
            End If
        Catch ex As SqlClient.SqlException
            'GSQLConnection.Close()
            MsgBox(ex)
            Cn.Close()
        Finally

            BtnLogUser.Enabled = True
            Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub UsersIDs()
        '==========================================================================|
        '«·≈Ã—«¡ «·Œ«’ »Ã·» ﬂ· √”„«¡ «·„” Œœ„Ì‰ ÊÊ÷⁄Â„ ›Ì ﬁ«∆„… «·„” Œœ„Ì‰
        ' –·ﬂ „‰ ﬁÊ«⁄œ »Ì«‰«  «·Œ«œ„ 
        '==========================================================================|

        '«” œ⁄«¡ «·« ’«· «·⁄«„ »ﬁÊ«⁄œ »Ì«‰«  «·”Ì—›—


        Try
            Call ConData()
            Me.Cursor = Cursors.WaitCursor
            '„⁄—›… Õ«·… «·« ’«· «·⁄«„ ›≈–« ﬂ«‰ €Ì— „› ÊÕ

            'If GSQLConnection.State <> ConnectionState.Open Then
            '⁄‰œÂ« «” œ⁄Ì ≈Ã—«¡ «·« ’«· «·⁄«„ „‰ „œÌÊ· «·« ’«·
            'GeneralConnection()
            'End If

            Dim GSQLCmd As SqlClient.SqlCommand = Cn.CreateCommand

            'If GSQLConnection.State = ConnectionState.Open Then
            If Cn.State = ConnectionState.Open Then
                GDR = Nothing

                GSQLCmd.CommandType = CommandType.Text
                GSQLCmd.CommandText = "SELECT * FROM Users where Status='True'"
                GDR = GSQLCmd.ExecuteReader
                CmbUserName.Items.Clear()
                Do While GDR.Read()
                    CmbUserName.Items.Add(GDR.GetSqlString(1))
                    ' Application.DoEvents()
                Loop
                GDR.Close()
                'GSQLConnection.Close()
                Cn.Close()
                Me.Cursor = Cursors.Default

            End If

        Catch

        Finally
            Me.Cursor = Cursors.Default
            Cn.Close()
            '   GSQLConnection.Close()
        End Try
    End Sub

    Private Sub FrmLogin_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'M = 60
        'T = 0
        'Timer1.Enabled = True
        'ProgBar.Value = 0
        UsersIDs()
    End Sub

    Private Sub FrmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Call UsersIDs()
        M = 60
        T = 0
        Timer1.Enabled = True
        ProgBar.Value = 0
        'Dim cscreen As clsScreenRes = New clsScreenRes()
        'Dim sep() As String
        'Try
        '    beginRes = cscreen.CurrentResolution
        '    If String.Compare(beginRes, "1024x768") <> 0 Then
        '        sep = Strings.Split(beginRes, "x")
        '        beginW = CInt(sep(0))
        '        beginH = CInt(sep(1))
        '        '  cscreen.ChangeResolution(1024, 768)
        '    End If
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        '  Me.Close()
        'Dim cscreen As clsScreenRes = New clsScreenRes()
        'Try
        '    cscreen.ChangeResolution(beginW, beginH)
        'Catch ex As Exception
        'End Try
        System.Diagnostics.Process.GetCurrentProcess().Kill()
        System.Environment.Exit(0)
    End Sub

    Private Sub CmbUserName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbUserName.SelectedIndexChanged
        Me.TxtPassword.Focus()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        M -= 1
        Me.LbTimer.Text = M 'Val(Me.LbTimer.Text) - M
        Select Case M
            Case 1 To 10
                Me.Label6.Text = "À‹Ê«‰‹Ì"
                Me.LbTimer.ForeColor = Color.Red
                Pic3.Left = 393
                Pic3.Top = 155
                Pic3.Visible = True
                Pic2.Visible = False
                ProgBar.BackColor = Color.Red
            Case 11 To 30
                Me.LbTimer.ForeColor = Color.YellowGreen
                Pic2.Left = 393
                Pic2.Top = 155
                Pic2.Visible = True
                Pic1.Visible = False
                ProgBar.BackColor = Color.YellowGreen
        End Select
        T += 1
        If T <= 60 Then ProgBar.Value = T
        If Me.LbTimer.Text = "0" Then
            MessageBox.Show("«”› .. ·ﬁœ «‰ Â  «·„œ… «·„”„ÊÕ‹‹… ·œŒÊ· «·„” Œœ„ ..”Ì „ «‰Â«¡ «·‰Ÿ«„", "«‰ Â«¡ «·„œ… ", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Timer1.Enabled = False

            System.Diagnostics.Process.GetCurrentProcess().Kill()
            'Dim cscreen As clsScreenRes = New clsScreenRes()
            'Try
            '    cscreen.ChangeResolution(beginW, beginH)
            'Catch ex As Exception
            'End Try
            ' System.Environment.Exit(0)
            Application.Exit()
            Me.Close()
        End If
    End Sub
End Class