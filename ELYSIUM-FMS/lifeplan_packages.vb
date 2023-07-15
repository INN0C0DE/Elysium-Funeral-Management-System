Public Class lifeplan_packages
    Private Sub BunifuLabel6_Click(sender As Object, e As EventArgs) Handles BunifuLabel6.Click

    End Sub

    Private Sub traditional_tab_Click(sender As Object, e As EventArgs) Handles traditional_tab.Click
        lf_pages.SetPage("traditional")
        traditional_tab.IdleFillColor = Color.Sienna
        cremation_tab.IdleFillColor = Color.Chocolate
    End Sub

    Private Sub cremation_tab_Click(sender As Object, e As EventArgs) Handles cremation_tab.Click
        lf_pages.SetPage("cremation")
        cremation_tab.IdleFillColor = Color.Sienna
        traditional_tab.IdleFillColor = Color.Chocolate

    End Sub

    Private Sub lifeplan_packages_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'traditional_tab.IdleFillColor = Color.Sienna
        'cremation_tab.IdleFillColor = Color.Chocolate
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        Me.Close()
        admin_dashboard.Show()
    End Sub
End Class