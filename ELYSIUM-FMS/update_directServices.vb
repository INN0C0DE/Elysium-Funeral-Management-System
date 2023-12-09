Imports MongoDB.Bson
Imports MongoDB.Driver
Imports System.Globalization
Public Class update_directServices
    Private client As MongoClient
    Private database As IMongoDatabase
    Private collection As IMongoCollection(Of BsonDocument)


    Private Sub updateds_btn_Click(sender As Object, e As EventArgs) Handles updateds_btn.Click
        Dim objectId As String = ds_id.Text.Trim()
        Dim selectedDOB As String = ds_dob.Value.ToString("MM/dd/yyyy")
        Dim selectedDOD As String = ds_dod.Value.ToString("MM/dd/yyyy")
        If Not String.IsNullOrEmpty(objectId) Then
            ' Create the filter to match the ObjectId
            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))



            ' Retrieve the existing document
            Dim existingDocument = collection.Find(filter).FirstOrDefault()

            If existingDocument IsNot Nothing Then
                ' Modify the document as needed
                existingDocument.Set("ds_appName", ds_appName.Text)
                existingDocument.Set("ds_contact", ds_contact.Text)
                existingDocument.Set("ds_email", ds_email.Text)
                existingDocument.Set("ds_address", ds_address.Text)
                existingDocument.Set("ds_address", ds_address.Text)
                existingDocument.Set("ds_bankAccount", ds_bankAccount.Text)
                existingDocument.Set("ds_deceasedName", ds_deceasedName.Text)
                existingDocument.Set("ds_dob", selectedDOB)
                existingDocument.Set("ds_dod", selectedDOD)
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
        ds_dob.Value = Date.Now
        ds_dod.Value = Date.Now
        ds_cod.Clear()
        ds_package.Text = Nothing
        ds_price.Clear()
    End Sub

    Private Sub ds_package_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ds_package.SelectedIndexChanged
        If ds_package.Text = "AGAPI (TRADITIONAL)" Then
            ds_price.Text = 100000.0

        ElseIf ds_package.Text = "ELEOS (TRADITIONAL)" Then
            ds_price.Text = 75000.0

        ElseIf ds_package.Text = "THALIA (TRADITIONAL)" Then
            ds_price.Text = 50000.0

        ElseIf ds_package.Text = "SOPHRONIA (CREMATION)" Then
            ds_price.Text = 145000.0

        ElseIf ds_package.Text = "THEON (CREMATION)" Then
            ds_price.Text = 95000.0

        ElseIf ds_package.Text = "IRENE (CREMATION)" Then
            ds_price.Text = 68000.0

        End If
    End Sub
End Class