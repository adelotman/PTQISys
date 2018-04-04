Public Class SplashScreen
    Sub New
        InitializeComponent()
    End Sub

    Public Overrides Sub ProcessCommand(ByVal cmd As System.Enum, ByVal arg As Object)
        MyBase.ProcessCommand(cmd, arg)
    End Sub

    Public Enum SplashScreenCommand
        SomeCommandId
    End Enum

    Private Sub pictureEdit1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pictureEdit1.EditValueChanged

    End Sub

    Private Sub SplashScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
