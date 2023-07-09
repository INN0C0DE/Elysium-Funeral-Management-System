Imports System.ComponentModel

Public Class login_form
    Private Sub staffpwd_hide_Click(sender As Object, e As EventArgs) Handles staffpwd_hide.Click
        staff_password.PasswordChar = ""
        staffpwd_hide.Visible = False
        staffpwd_show.Visible = True
    End Sub

    Private Sub staffpwd_show_Click(sender As Object, e As EventArgs) Handles staffpwd_show.Click
        staff_password.PasswordChar = "*"
        staffpwd_hide.Visible = True
        staffpwd_show.Visible = False
    End Sub

    Private Sub adminpwd_hide_Click(sender As Object, e As EventArgs) Handles adminpwd_hide.Click
        admin_password.PasswordChar = ""
        adminpwd_hide.Visible = False
        adminpwd_show.Visible = True
    End Sub

    Private Sub adminpwd_show_Click(sender As Object, e As EventArgs) Handles adminpwd_show.Click
        admin_password.PasswordChar = "*"
        adminpwd_hide.Visible = True
        adminpwd_show.Visible = False
    End Sub

    Private Sub staff_btn_Click(sender As Object, e As EventArgs) Handles staff_btn.Click
        Login_Pages.SetPage("Staff_Login")
        admin_username.Text = ""
        admin_password.Text = ""
        admin_username.Focus()
    End Sub

    Private Sub ADMIN_BTN_Click(sender As Object, e As EventArgs) Handles ADMIN_BTN.Click
        Login_Pages.SetPage("Admin_Login")
        staff_username.Text = ""
        staff_password.Text = ""
        staff_username.Focus()
    End Sub

    Private Sub login_form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        staffpwd_show.Visible = False
        adminpwd_show.Visible = False
        Login_Pages.SetPage("Staff_Login")
    End Sub

    Private Sub login_form_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        'If e.CloseReason = CloseReason.UserClosing Then
        '    Application.Exit()
        'End If
    End Sub

    Private Sub admin_loginbtn_Click(sender As Object, e As EventArgs) Handles admin_loginbtn.Click
        'login logic sample without db
        If admin_username.Text = "admin" And admin_password.Text = "admin" Then
            MessageBox.Show("Login Successful!", "ELYSIUM FMS | LOGIN STATUS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            admin_username.Clear()
            admin_password.Clear()
            admin_dashboard.Show()
            admin_dashboard.admin_pages.SetPage("home")
            Me.Hide()
        Else
            MessageBox.Show("Invalid Username or Password!", "ELYSIUM FMS | LOGIN STATUS:", MessageBoxButtons.OK, MessageBoxIcon.Error)
            admin_username.Clear()
            admin_password.Clear()
            admin_username.Focus()
        End If
    End Sub

    Private Sub login_form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            Application.Exit()
        End If
    End Sub
End Class