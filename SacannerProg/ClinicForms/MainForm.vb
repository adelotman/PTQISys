Public Class MainForm

    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ' If MessageBox.Show("هل انت متاكد من انهاء المنظومة ؟", "ايقاف تشغيل المنظومة", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then Exit Sub
        End
    End Sub

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BarButtonItem1_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        FrmTypeDrugs.ShowDialog()
    End Sub

    Private Sub BarButtonItem2_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        FrmPatient.ShowDialog()
    End Sub

    Private Sub BarButtonItem4_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        FrmRepFinds.ShowDialog()
    End Sub

    Private Sub BarButtonItem3_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        FrmChangePassword.ShowDialog()
    End Sub

    Private Sub RibbonControl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RibbonControl.Click

    End Sub

    Private Sub ال_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles ال.ItemClick
        FrmScannFile.ShowDialog()
    End Sub
End Class