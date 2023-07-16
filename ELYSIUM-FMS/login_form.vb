Imports System.ComponentModel
Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class login_form
    Private mongoClient As MongoClient
    Private database As IMongoDatabase
    Private collection As IMongoCollection(Of BsonDocument)
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

        ' Initialize the MongoDB client
        Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"
        mongoClient = New MongoClient(connectionString)

        ' Set the database and collection names
        Dim dbName As String = "elysium-fms-database"
        Dim collectionName As String = "admin_account"
        database = mongoClient.GetDatabase(dbName)
        collection = database.GetCollection(Of BsonDocument)(collectionName)
    End Sub

    Private Sub login_form_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        'If e.CloseReason = CloseReason.UserClosing Then
        '    Application.Exit()
        'End If
    End Sub

    Private Sub admin_loginbtn_Click(sender As Object, e As EventArgs) Handles admin_loginbtn.Click
        Dim username As String = admin_username.Text
        Dim password As String = admin_password.Text

        ' Build the query to find the document with the given username and password
        Dim filter = Builders(Of BsonDocument).Filter.And(
            Builders(Of BsonDocument).Filter.Eq(Of String)("username", username),
            Builders(Of BsonDocument).Filter.Eq(Of String)("password", password)
        )

        ' Find the document in the collection
        Dim document = collection.Find(filter).FirstOrDefault()

        If document IsNot Nothing Then
            Dim loggedInUser As String = document("fullname").ToString()
            'Dim adminFullName As String = document("fullname").ToString()
            admin_dashboard.username_label.Text = loggedInUser + "!"
            admin_dashboard.admin_name1.Text = loggedInUser
            ' Login successful
            MessageBox.Show("Login Successful!", "ELYSIUM FMS | LOGIN STATUS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            admin_username.Clear()
            admin_password.Clear()
            admin_dashboard.Show()
            admin_dashboard.admin_pages.SetPage("home")
            Me.Hide()
        Else
            ' Login failed
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