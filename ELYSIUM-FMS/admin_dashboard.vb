Public Class admin_dashboard
    Private Sub BunifuLabel4_Click(sender As Object, e As EventArgs) Handles date_label.Click

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

    Private Sub BunifuButton12_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub admin_dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        time_label.Text = DateTime.Now.ToString("hh:mm:ss tt") ' Display current time
        date_label.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy") ' Display current date
    End Sub
End Class