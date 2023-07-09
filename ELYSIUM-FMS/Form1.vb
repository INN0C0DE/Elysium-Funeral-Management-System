Public Class loading_screen
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        'Dim login_form As New loading_screen
        'login_form.Show()
        login_form.Show()

        Me.Hide()
    End Sub

    Private Sub loading_screen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
