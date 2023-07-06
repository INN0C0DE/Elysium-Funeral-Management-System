Public Class admin_dashboard
    Private Sub BunifuLabel4_Click(sender As Object, e As EventArgs) Handles BunifuLabel4.Click

    End Sub

    Private Sub BunifuButton1_Click(sender As Object, e As EventArgs) Handles BunifuButton1.Click
        admin_pages.SetPage("home")
    End Sub

    Private Sub BunifuButton2_Click(sender As Object, e As EventArgs) Handles BunifuButton2.Click
        admin_pages.SetPage("services")
    End Sub

    Private Sub BunifuButton3_Click(sender As Object, e As EventArgs) Handles BunifuButton3.Click
        admin_pages.SetPage("staff_account")
    End Sub

    Private Sub BunifuButton4_Click(sender As Object, e As EventArgs) Handles BunifuButton4.Click
        admin_pages.SetPage("admin_profile")
    End Sub

    Private Sub BunifuButton5_Click(sender As Object, e As EventArgs) Handles BunifuButton5.Click
        admin_pages.SetPage("appointment")
    End Sub

    Private Sub BunifuButton6_Click(sender As Object, e As EventArgs) Handles BunifuButton6.Click
        admin_pages.SetPage("lifeplan")
    End Sub

    Private Sub BunifuButton7_Click(sender As Object, e As EventArgs) Handles BunifuButton7.Click
        admin_pages.SetPage("direct_services")
    End Sub

    Private Sub BunifuButton8_Click(sender As Object, e As EventArgs) Handles BunifuButton8.Click
        admin_pages.SetPage("inventory")
    End Sub
End Class