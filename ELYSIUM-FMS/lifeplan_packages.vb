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

    Private Sub agapi_Click(sender As Object, e As EventArgs) Handles agapi.Click
        If agapi_contract.Visible = False Then
            agapi_contract.Visible = True
        Else
            agapi_contract.Visible = False
        End If
    End Sub

    Private Sub eleos_Click(sender As Object, e As EventArgs) Handles eleos.Click
        If eleos_contract.Visible = False Then
            eleos_contract.Visible = True
        Else
            eleos_contract.Visible = False
        End If
    End Sub

    Private Sub thalia_Click(sender As Object, e As EventArgs) Handles thalia.Click
        If thalia_contract.Visible = False Then
            thalia_contract.Visible = True
        Else
            thalia_contract.Visible = False
        End If
    End Sub

    Private Sub sophronia_Click(sender As Object, e As EventArgs) Handles sophronia.Click
        If sophronia_contract.Visible = False Then
            sophronia_contract.Visible = True
        Else
            sophronia_contract.Visible = False
        End If
    End Sub

    Private Sub theon_Click(sender As Object, e As EventArgs) Handles theon.Click
        If theon_contract.Visible = False Then
            theon_contract.Visible = True
        Else
            theon_contract.Visible = False
        End If
    End Sub

    Private Sub irene_Click(sender As Object, e As EventArgs) Handles irene.Click
        If irene_contract.Visible = False Then
            irene_contract.Visible = True
        Else
            irene_contract.Visible = False
        End If
    End Sub
End Class