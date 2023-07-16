Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class update_staffaccount
    Private client As MongoClient
    Private database As IMongoDatabase
    Private collection As IMongoCollection(Of BsonDocument)
    Private Sub BunifuButton1_Click(sender As Object, e As EventArgs) Handles BunifuButton1.Click
        CLEARTEXT()
        Me.Close()
    End Sub
    Public Sub CLEARTEXT()
        update_id.Clear()
        update_username.Clear()
        update_pwd.Clear()
        update_rfid.Clear()
        update_name.Clear()
        update_gender.Text = Nothing
        update_email.Clear()
        update_age.Clear()
        update_number.Clear()
    End Sub

    Private Sub update_staffaccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Connect to MongoDB Atlas
        Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"
        client = New MongoClient(connectionString)
        database = client.GetDatabase("elysium-fms-database")
        collection = database.GetCollection(Of BsonDocument)("staff_account")
    End Sub

    Private Sub updatestaff_btn_Click(sender As Object, e As EventArgs) Handles updatestaff_btn.Click
        Dim objectId As String = update_id.Text.Trim()

        If Not String.IsNullOrEmpty(objectId) Then
            ' Create the filter to match the ObjectId
            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))

            ' Retrieve the existing document
            Dim existingDocument = collection.Find(filter).FirstOrDefault()

            If existingDocument IsNot Nothing Then
                ' Modify the document as needed
                existingDocument.Set("username", update_username.Text)
                existingDocument.Set("password", update_pwd.Text)
                existingDocument.Set("rfid", update_rfid.Text)
                existingDocument.Set("fullname", update_name.Text)
                existingDocument.Set("gender", update_gender.Text)
                existingDocument.Set("age", update_age.Text)
                existingDocument.Set("email", update_email.Text)
                existingDocument.Set("number", update_number.Text)

                ' Update the document in the collection
                collection.ReplaceOne(filter, existingDocument)

                MessageBox.Show("Staff updated successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
                admin_dashboard.StaffDVGLoad()
                CLEARTEXT()
                Me.Hide()
            Else
                MessageBox.Show("Data not found!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Please select an account to update.", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class