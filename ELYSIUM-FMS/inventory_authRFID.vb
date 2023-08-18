Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class inventory_authRFID
    Private connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"
    Private Sub inventory_authRFID_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub rfid_admintb_KeyDown(sender As Object, e As KeyEventArgs) Handles rfid_admintb.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevent the Enter key from adding a new line to the TextBox

            Dim cardNumber As String = rfid_admintb.Text.Trim()

            If cardNumber.Length > 0 Then
                If VerifyCardNumber(cardNumber) Then
                    MessageBox.Show("Admin authentication success!", "ELYSIUM FMS | Admin Authentication:", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    archived_form.Show()
                    archived_form.archive_pages.SetPage("inventory")
                    Me.Close()
                Else
                    MessageBox.Show("Admin authentication failed! Call your admin for authentication.", "ELYSIUM FMS | Admin Authentication:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    rfid_admintb.Clear()
                    rfid_admintb.Focus()
                End If
            End If
        End If
    End Sub
    Private Function VerifyCardNumber(cardNumber As String) As Boolean
        Dim client As New MongoClient(connectionString)
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("admin_account")

        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of String)("rfid_no", cardNumber)
        Dim count As Long = collection.CountDocuments(filter)

        ' Debug output:
        Console.WriteLine("Card number: " + cardNumber)
        Console.WriteLine("Count: " + count.ToString())

        Return count > 0
    End Function
End Class