Imports MongoDB.Bson
Imports MongoDB.Driver
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
        'AdminProfileLoad()
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
        AdminProfileLoad()
        SetProfilePhoto()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        time_label.Text = DateTime.Now.ToString("hh:mm:ss tt") ' Display current time
        date_label.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy") ' Display current date
    End Sub

    Private Sub addstaff_btn_Click(sender As Object, e As EventArgs) Handles addstaff_btn.Click
        add_staffaccount.Show()
    End Sub

    Private Sub staff_updatebtn_Click(sender As Object, e As EventArgs) Handles staff_updatebtn.Click
        update_staffaccount.Show()
    End Sub

    Private Sub BunifuButton9_Click(sender As Object, e As EventArgs) Handles BunifuButton9.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to sign out?", "SIGN-OUT STATUS:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If (result = DialogResult.Yes) Then
            login_form.Show()

            Me.Hide()
        Else
            'MessageBox.Show("SIGNING OUT CANCELLED!", "SIGN-OUT STATUS:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub admin_dashboard_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            Application.Exit()
        End If
    End Sub

    Private Sub lp_packages_Click(sender As Object, e As EventArgs) Handles lp_packages.Click
        lifeplan_packages.Show()
        Me.Hide()

    End Sub

    Private Sub ds_packagesBTN_Click(sender As Object, e As EventArgs) Handles ds_packagesBTN.Click
        ds_packages.Show()
        Me.Hide()
    End Sub

    Private Sub AdminProfileLoad()
        'for admin profile details display
        ' MongoDB connection string (replace <connection_string> with your actual connection string)
        Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"

        ' Create a MongoClient object with the connection string
        Dim client As MongoClient = New MongoClient(connectionString)

        ' Access the database and collection
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("admin_account")

        ' Retrieve the data from the collection
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Empty
        Dim result As List(Of BsonDocument) = collection.Find(filter).ToList()

        ' Check if any documents were found
        If result.Count > 0 Then
            ' Assuming each document has "name" and "email" fields
            Dim name As String = result(0).GetValue("fullname").ToString()
            Dim rfid As String = result(0).GetValue("rfid_no").ToString()
            Dim adminUser As String = result(0).GetValue("username").ToString()
            Dim adminPass As String = result(0).GetValue("password").ToString()
            Dim adminAge As String = result(0).GetValue("age").ToString()
            Dim adminEmail As String = result(0).GetValue("email").ToString()
            Dim adminGender As String = result(0).GetValue("gender").ToString()

            ' Update TextBox1 and TextBox2 with the retrieved values
            admin_name1.Text = name
            admin_rfid1.Text = rfid
            admin_username1.Text = adminUser
            admin_pwd1.Text = adminPass
            admin_age.Text = adminAge
            admin_email.Text = adminEmail
            admin_gender.Text = adminGender

        End If
    End Sub

    Private Sub admin_dashboard_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        AdminProfileLoad()
    End Sub

    Private Sub admin_updatebtn_Click(sender As Object, e As EventArgs) Handles admin_updatebtn.Click
        ' MongoDB connection string (replace <connection_string> with your actual connection string)
        Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"

        ' Create a MongoClient object with the connection string
        Dim client As MongoClient = New MongoClient(connectionString)

        ' Access the database and collection
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("admin_account")

        ' Retrieve the current document
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Empty
        Dim result As List(Of BsonDocument) = collection.Find(filter).ToList()

        ' Check if any documents were found
        If result.Count > 0 Then
            ' Assuming each document has "fullname", "rfid_no", "username", and "password" fields
            Dim documentId As ObjectId = result(0).GetValue("_id").AsObjectId
            Dim updatedName As String = admin_name1.Text
            Dim updatedRfid As String = admin_rfid1.Text
            Dim updatedUsername As String = admin_username1.Text
            Dim updatedPassword As String = admin_pwd1.Text
            Dim updatedEmail As String = admin_email.Text
            Dim updatedGender As String = admin_gender.Text
            Dim updatedAge As String = admin_age.Text

            ' Create an update definition with the new values
            Dim update As UpdateDefinition(Of BsonDocument) = Builders(Of BsonDocument).Update.Set(Of String)("fullname", updatedName) _
                                                                                        .Set(Of String)("rfid_no", updatedRfid) _
                                                                                        .Set(Of String)("username", updatedUsername) _
                                                                                        .Set(Of String)("password", updatedPassword) _
                                                                                        .Set(Of String)("email", updatedEmail) _
                                                                                        .Set(Of String)("gender", updatedGender) _
                                                                                        .Set(Of String)("age", updatedAge) _

            ' Update the document in the collection
            collection.UpdateOne(Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", documentId), update)

            MessageBox.Show("Data updated successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub admin_gender_SelectedIndexChanged(sender As Object, e As EventArgs) Handles admin_gender.SelectedIndexChanged
        SetProfilePhoto()
    End Sub
    Private Sub SetProfilePhoto()

        If admin_gender.Text = "Male" Then
            admin_picture.Image = My.Resources.admin_male
        ElseIf admin_gender.Text = "Female" Then
            admin_picture.Image = My.Resources.admin_female
        Else
            admin_picture.Image = Nothing
        End If
    End Sub
End Class