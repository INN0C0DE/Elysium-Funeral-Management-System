Imports System.ComponentModel
Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class login_form
    'Private mongoClient As MongoClient
    'Private database As IMongoDatabase
    'Private collection As IMongoCollection(Of BsonDocument)

    Private mongoClientAdmin As MongoClient
    Private databaseAdmin As IMongoDatabase
    Private collectionAdmin As IMongoCollection(Of BsonDocument)

    Private mongoClientStaff As MongoClient
    Private databaseStaff As IMongoDatabase
    Private collectionStaff As IMongoCollection(Of BsonDocument)
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

        '' Initialize the MongoDB client (admin)
        'Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"
        'mongoClient = New MongoClient(connectionString)
        '' Initialize the MongoDB client (staff)
        'Dim connectionString2 As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"
        'mongoClient = New MongoClient(connectionString)

        '' Set the database and collection names for admin 
        'Dim dbName As String = "elysium-fms-database"
        'Dim collectionName As String = "admin_account"
        'database = mongoClient.GetDatabase(dbName)
        'collection = database.GetCollection(Of BsonDocument)(collectionName)
        '' Set the database and collection names for staff
        'Dim dbName2 As String = "elysium-fms-database"
        'Dim collectionName2 As String = "staff_account"
        'database = mongoClient.GetDatabase(dbName2)
        'collection = database.GetCollection(Of BsonDocument)(collectionName2)

        ' Initialize the MongoDB client for admin
        Dim adminConnectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"
        mongoClientAdmin = New MongoClient(adminConnectionString)
        Dim dbNameAdmin As String = "elysium-fms-database"
        Dim collectionNameAdmin As String = "admin_account"
        databaseAdmin = mongoClientAdmin.GetDatabase(dbNameAdmin)
        collectionAdmin = databaseAdmin.GetCollection(Of BsonDocument)(collectionNameAdmin)

        ' Initialize the MongoDB client for staff
        Dim staffConnectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"
        mongoClientStaff = New MongoClient(staffConnectionString)
        Dim dbNameStaff As String = "elysium-fms-database"
        Dim collectionNameStaff As String = "staff_account"
        databaseStaff = mongoClientStaff.GetDatabase(dbNameStaff)
        collectionStaff = databaseStaff.GetCollection(Of BsonDocument)(collectionNameStaff)
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
        Dim document = collectionAdmin.Find(filter).FirstOrDefault()

        If document IsNot Nothing Then
            Dim loggedInUser As String = document("fullname").ToString()
            'Dim adminFullName As String = document("fullname").ToString()
            admin_dashboard.username_label.Text = loggedInUser + ""
            admin_dashboard.admin_name1.Text = loggedInUser
            admin_dashboard.role_label.Text = "Funeral Administrator"
            ' Login successful
            MessageBox.Show("Login Successful!", "ELYSIUM FMS | LOGIN STATUS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            admin_username.Clear()
            admin_password.Clear()
            admin_dashboard.Show()
            admin_dashboard.admin_pages.SetPage("home")
            admin_dashboard.BunifuButton3.Visible = True
            admin_dashboard.BunifuButton4.Visible = True
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

    Private Sub staff_logintbn_Click(sender As Object, e As EventArgs) Handles staff_logintbn.Click
        Dim username2 As String = staff_username.Text
        Dim password2 As String = staff_password.Text

        ' Build the query to find the document with the given username and password
        Dim filter = Builders(Of BsonDocument).Filter.And(
            Builders(Of BsonDocument).Filter.Eq(Of String)("username", username2),
            Builders(Of BsonDocument).Filter.Eq(Of String)("password", password2)
        )

        ' Find the document in the collection
        Dim document = collectionStaff.Find(filter).FirstOrDefault()

        If document IsNot Nothing Then
            Dim loggedInUser As String = document("fullname").ToString()
            'Dim adminFullName As String = document("fullname").ToString()
            admin_dashboard.username_label.Text = loggedInUser + ""
            admin_dashboard.role_label.Text = "Funeral Staff"
            'admin_dashboard.admin_name1.Text = loggedInUser
            ' Login successful
            MessageBox.Show("Login Successful!", "ELYSIUM FMS | LOGIN STATUS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            staff_username.Clear()
            staff_password.Clear()
            admin_dashboard.Show()
            admin_dashboard.admin_pages.SetPage("home")
            admin_dashboard.BunifuButton3.Visible = False
            admin_dashboard.BunifuButton4.Visible = False
            Me.Hide()
        Else
            ' Login failed
            MessageBox.Show("Invalid Username or Password!", "ELYSIUM FMS | LOGIN STATUS:", MessageBoxButtons.OK, MessageBoxIcon.Error)
            staff_username.Clear()
            staff_password.Clear()
            staff_username.Focus()
        End If
    End Sub

    Private Sub rfidbtn_Click(sender As Object, e As EventArgs) Handles rfidbtn.Click
        staff_rfid.Show()
    End Sub

    Private Sub rfid_btn_Click(sender As Object, e As EventArgs) Handles rfid_btn.Click
        admin_rfid.Show()
    End Sub

    Private Sub gsm_info_Click(sender As Object, e As EventArgs) Handles gsm_info.Click
        MessageBox.Show("Please make sure that your GSM Module is in COM Port Number 4. See details in Device Manager > Ports (COM & LPT) > USB SERIAL CH340 (COM4). ", "ELYSIUM FMS | NOTICE:", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub gsm_info2_Click(sender As Object, e As EventArgs) Handles gsm_info2.Click
        MessageBox.Show("Please make sure that your GSM Module is in COM Port Number 4. See details in Device Manager > Ports (COM & LPT) > USB SERIAL CH340 (COM4). ", "ELYSIUM FMS | NOTICE:", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class