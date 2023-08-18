Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class update_appointment
    Private client As MongoClient
    Private database As IMongoDatabase
    Private collection As IMongoCollection(Of BsonDocument)
    Private Sub BunifuButton1_Click(sender As Object, e As EventArgs) Handles BunifuButton1.Click
        CLEARTEXT()
        Me.Close()
    End Sub
    Public Sub CLEARTEXT()
        update_id.Clear()
        update_name.Clear()
        update_status.Text = Nothing
        update_email.Clear()
        update_number.Clear()
        update_date.Clear()
        update_time.Clear()
    End Sub

    Private Sub updateAppt_btn_Click(sender As Object, e As EventArgs) Handles updateAppt_btn.Click
        Dim objectId As String = update_id.Text.Trim()

        If Not String.IsNullOrEmpty(objectId) Then
            ' Create the filter to match the ObjectId
            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))

            ' Retrieve the existing document
            Dim existingDocument = Collection.Find(filter).FirstOrDefault()

            If existingDocument IsNot Nothing Then
                ' Modify the document as needed
                existingDocument.Set("fullname", update_name.Text)
                existingDocument.Set("email", update_email.Text)
                existingDocument.Set("number", update_number.Text)
                existingDocument.Set("address", update_address.Text)
                existingDocument.Set("apptDate", update_date.Text)
                existingDocument.Set("apptTime", update_time.Text)
                existingDocument.Set("apptStatus", update_status.Text)
                ' Update the document in the collection
                collection.ReplaceOne(filter, existingDocument)

                MessageBox.Show("Appointment updated successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
                admin_dashboard.ApptDVGLoad()
                CLEARTEXT()
                Me.Hide()
            Else
                MessageBox.Show("Data not found!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Please select a data to update.", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub update_appointment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Connect to MongoDB Atlas
        Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"
        client = New MongoClient(connectionString)
        database = client.GetDatabase("elysium-fms-database")
        collection = database.GetCollection(Of BsonDocument)("appointments")
    End Sub
End Class