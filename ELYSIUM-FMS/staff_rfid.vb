Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class staff_rfid
    Private connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"
    Private Sub staff_rfid_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub rfid_stafftb_KeyDown(sender As Object, e As KeyEventArgs) Handles rfid_stafftb.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevent the Enter key from adding a new line to the TextBox

            Dim cardNumber As String = rfid_stafftb.Text.Trim()

            If cardNumber.Length > 0 Then
                If VerifyCardNumber(cardNumber) Then
                    MessageBox.Show("RFID card valid, logged in successfully", "ELYSIUM FMS | LOGIN STATUS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    admin_dashboard.Show()
                    admin_dashboard.admin_pages.SetPage("home")
                    admin_dashboard.BunifuButton3.Visible = False
                    admin_dashboard.BunifuButton4.Visible = False
                    admin_dashboard.username_label.Text = GetAdminFullName(cardNumber) + "" ' Set the admin name label in admin_dashboard form
                    login_form.Hide()
                    Me.Close()
                Else
                    MessageBox.Show("RFID card not valid, logged in failed!", "ELYSIUM FMS | LOGIN STATUS:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    rfid_stafftb.Clear()
                    rfid_stafftb.Focus()
                End If
            End If
        End If
    End Sub
    Private Function VerifyCardNumber(cardNumber As String) As Boolean
        Dim client As New MongoClient(connectionString)
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("staff_account")

        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of String)("rfid", cardNumber)
        Dim count As Long = collection.CountDocuments(filter)

        ' Debug output:
        Console.WriteLine("Card number: " + cardNumber)
        Console.WriteLine("Count: " + count.ToString())

        Return count > 0
    End Function

    Private Function GetAdminFullName(cardNumber As String) As String
        Dim client As New MongoClient(connectionString)
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("staff_account")

        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of String)("rfid", cardNumber)
        Dim projection As ProjectionDefinition(Of BsonDocument) = Builders(Of BsonDocument).Projection.Include("fullname")

        Dim adminDocument As BsonDocument = collection.Find(filter).Project(projection).FirstOrDefault()

        If adminDocument IsNot Nothing Then
            Return adminDocument("fullname").AsString
        Else
            Return String.Empty
        End If
    End Function
End Class