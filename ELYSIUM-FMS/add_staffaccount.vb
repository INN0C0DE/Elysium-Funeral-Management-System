Imports MongoDB.Bson
Imports MongoDB.Driver
Imports MongoDB.Driver.Linq
Public Class add_staffaccount
    Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
    Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
    Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("staff_account")
    Private Sub BunifuButton1_Click(sender As Object, e As EventArgs) Handles BunifuButton1.Click
        Me.Close()
        CLEARTEXT()

    End Sub

    Private Sub add_staffaccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub addstaff_btn_Click(sender As Object, e As EventArgs) Handles addstaff_btn.Click
        If add_username.Text = "" Or add_pwd.Text = "" Or add_rfid.Text = "" Or add_name.Text = "" Or add_gender.Text = "" Or add_age.Text = "" Or add_email.Text = "" Or add_number.Text = "" Then
            MsgBox("Please fill up all forms needed.", vbInformation, "You forgot to fill something!")
        Else
            Dim document As BsonDocument = New BsonDocument()
            document.Add("username", add_username.Text)
            document.Add("password", add_pwd.Text)
            document.Add("rfid", add_rfid.Text)
            document.Add("fullname", add_name.Text)
            document.Add("gender", add_gender.Text)
            document.Add("age", add_age.Text)
            document.Add("email", add_email.Text)
            document.Add("number", add_number.Text)
            collection.InsertOne(document)
            MessageBox.Show("Data added successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Hide()
            admin_dashboard.StaffDVGLoad()
            CLEARTEXT()

            'BindDataToGridView()
            'DisplayTotalDocumentsCount()
        End If
    End Sub
    Public Sub CLEARTEXT()
        add_id.Clear()
        add_username.Clear()
        add_pwd.Clear()
        add_rfid.Clear()
        add_name.Clear()
        add_gender.Text = Nothing
        add_email.Clear()
        add_age.Clear()
        add_number.Clear()
    End Sub
End Class