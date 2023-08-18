Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class update_directServices
    Private client As MongoClient
    Private database As IMongoDatabase
    Private collection As IMongoCollection(Of BsonDocument)
    Private Sub updateds_btn_Click(sender As Object, e As EventArgs) Handles updateds_btn.Click
        Dim objectId As String = ds_id.Text.Trim()

        If Not String.IsNullOrEmpty(objectId) Then
            ' Create the filter to match the ObjectId
            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))

            ' Retrieve the existing document
            Dim existingDocument = Collection.Find(filter).FirstOrDefault()

            If existingDocument IsNot Nothing Then
                ' Modify the document as needed
                existingDocument.Set("ds_appName", ds_appName.Text)
                existingDocument.Set("ds_contact", ds_contact.Text)
                existingDocument.Set("ds_email", ds_email.Text)
                existingDocument.Set("ds_address", ds_address.Text)
                existingDocument.Set("ds_address", ds_address.Text)
                existingDocument.Set("ds_bankAccount", ds_bankAccount.Text)
                existingDocument.Set("ds_deceasedName", ds_deceasedName.Text)
                existingDocument.Set("ds_dob", ds_dob.Text)
                existingDocument.Set("ds_dod", ds_dod.Text)
                existingDocument.Set("ds_cod", ds_cod.Text)
                existingDocument.Set("ds_package", ds_package.Text)
                existingDocument.Set("ds_price", ds_price.Text)
                ' Update the document in the collection
                collection.ReplaceOne(filter, existingDocument)

                MessageBox.Show("Data updated successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
                admin_dashboard.DirectServicesDVGLoad()
                CLEARTEXT()
                Me.Hide()
            Else
                MessageBox.Show("Data not found!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Please select a data to update.", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub update_directServices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Connect to MongoDB Atlas
        Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"
        client = New MongoClient(connectionString)
        database = client.GetDatabase("elysium-fms-database")
        collection = database.GetCollection(Of BsonDocument)("direct_services")
    End Sub

    Private Sub BunifuButton1_Click(sender As Object, e As EventArgs) Handles BunifuButton1.Click
        CLEARTEXT()
        Me.Close()
    End Sub
    Public Sub CLEARTEXT()
        ds_appName.Clear()
        ds_contact.Clear()
        ds_email.Clear()
        ds_address.Clear()
        ds_bankAccount.Clear()
        ds_deceasedName.Clear()
        ds_dob.Clear()
        ds_dod.Clear()
        ds_cod.Clear()
        ds_package.Text = Nothing
        ds_price.Clear()
    End Sub
End Class