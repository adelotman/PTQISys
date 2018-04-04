Public Class Form3
    Private sMyScanner As String = String.Empty

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Set the scan source for your scan.
        sMyScanner = TwainOperations.GetScanSource
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Scan images from source and place the file path in a list of string for later access.
        'Dim folder As String = "c:\some path\folder"
        'Dim filename As String = System.IO.Path.Combine(folder, id & ".png")
        'PictureBox1.Image = Image.FromFile(filename)
        sMyScanner = TwainOperations.GetScanSource
        Dim lstFiles As List(Of String) = TwainOperations.ScanImages(".bmp", True, sMyScanner)
        ListBox1.DataSource = lstFiles
        ListBox1.SelectedIndex = 0
        PictureBox1.Image = Image.FromFile(ListBox1.Text)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        PictureBox1.Image = Nothing
        ListBox1.DataSource = Nothing
    End Sub
End Class