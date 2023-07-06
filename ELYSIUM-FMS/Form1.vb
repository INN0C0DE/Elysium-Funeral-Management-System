Public Class loading_screen
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        'Dim login_form As New loading_screen
        'login_form.Show()
        admin_dashboard.Show()

        Me.Hide()
    End Sub
End Class
