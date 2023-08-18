Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class update_inventory
    Private client As MongoClient
    Private database As IMongoDatabase
    Private collection As IMongoCollection(Of BsonDocument)
    Private Sub BunifuButton1_Click(sender As Object, e As EventArgs) Handles BunifuButton1.Click
        Me.Close()
        CLEARTEXT()
    End Sub
    Public Sub CLEARTEXT()
        inv_ProductName.Clear()
        inv_Quantity.Clear()
        inv_Availability.Text = Nothing
        inv_DateAdded.Value = Date.Now
        inv_id.Clear()
    End Sub
    Private Sub update_inventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Connect to MongoDB Atlas
        Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"
        client = New MongoClient(connectionString)
        database = client.GetDatabase("elysium-fms-database")
        collection = database.GetCollection(Of BsonDocument)("inventory")
    End Sub
    Private Sub updateInv_btn_Click(sender As Object, e As EventArgs) Handles updateInv_btn.Click
        Dim objectId As String = inv_id.Text.Trim()
        Dim selectedDate As String = inv_DateAdded.Value.ToString("MM/dd/yyyy")
        If Not String.IsNullOrEmpty(objectId) Then
            ' Create the filter to match the ObjectId
            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))

            ' Retrieve the existing document
            Dim existingDocument = collection.Find(filter).FirstOrDefault()

            If existingDocument IsNot Nothing Then
                ' Modify the document as needed
                existingDocument.Set("inv_ProductName", inv_ProductName.Text)
                existingDocument.Set("inv_Quantity", inv_Quantity.Text)
                existingDocument.Set("inv_Availability", inv_Availability.Text)
                existingDocument.Set("inv_DateAdded", selectedDate)

                ' Update the document in the collection
                collection.ReplaceOne(filter, existingDocument)

                MessageBox.Show("Data updated successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
                admin_dashboard.InventoryDVGLoad()
                CLEARTEXT()
                Me.Hide()
            Else
                MessageBox.Show("Data not found!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Please select a data to update.", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class