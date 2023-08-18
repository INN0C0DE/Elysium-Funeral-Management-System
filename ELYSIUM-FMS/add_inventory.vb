Imports MongoDB.Bson
Imports MongoDB.Driver
Imports MongoDB.Driver.Linq
Public Class add_inventory
    Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
    Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
    Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("inventory")
    Private Sub add_inventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub addstaff_btn_Click(sender As Object, e As EventArgs) Handles addInv_btn.Click
        If inv_ProductName.Text = "" Or inv_Quantity.Text = "" Or inv_Availability.Text = "" Or inv_DateAdded.Value = DateTimePicker.MinimumDateTime Then
            MsgBox("Please fill up all forms needed.", vbInformation, "You forgot to fill something!")
        Else
            Dim selectedDate As String = inv_DateAdded.Value.ToString("MM/dd/yyyy")

            Dim document As BsonDocument = New BsonDocument()
            document.Add("inv_ProductName", inv_ProductName.Text)
            document.Add("inv_Quantity", inv_Quantity.Text)
            document.Add("inv_Availability", inv_Availability.Text)
            document.Add("inv_DateAdded", selectedDate)
            collection.InsertOne(document)
            MessageBox.Show("Data added successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Hide()
            admin_dashboard.InventoryDVGLoad()
            CLEARTEXT()
        End If
    End Sub
    Public Sub CLEARTEXT()
        inv_ProductName.Clear()
        inv_Quantity.Clear()
        inv_Availability.Text = Nothing
        inv_DateAdded.Value = Date.Now
        inv_id.Clear()
    End Sub

    Private Sub BunifuButton1_Click(sender As Object, e As EventArgs) Handles BunifuButton1.Click
        Me.Close()
        CLEARTEXT()
    End Sub
End Class