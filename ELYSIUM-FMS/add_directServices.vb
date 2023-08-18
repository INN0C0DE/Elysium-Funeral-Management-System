Imports MongoDB.Bson
Imports MongoDB.Driver
Imports MongoDB.Driver.Linq
Public Class add_directServices
    Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
    Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
    Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("direct_services")
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

    <Obsolete>
    Private Sub addds_btn_Click(sender As Object, e As EventArgs) Handles addds_btn.Click
        If ds_appName.Text = "" Or ds_contact.Text = "" Or ds_email.Text = "" Or ds_address.Text = "" Or ds_bankAccount.Text = "" Or ds_deceasedName.Text = "" Or ds_dob.Value = DateTimePicker.MinimumDateTime Or ds_dod.Value = DateTimePicker.MinimumDateTime Or ds_cod.Text = "" Or ds_package.Text = "" Or ds_price.Text = "" Then
            MsgBox("Please fill up all forms needed.", vbInformation, "You forgot to fill something!")
        Else
            Dim document As BsonDocument = New BsonDocument()
            document.Add("ds_appName", ds_appName.Text)
            document.Add("ds_contact", ds_contact.Text)
            document.Add("ds_email", ds_email.Text)
            document.Add("ds_address", ds_address.Text)
            document.Add("ds_bankAccount", ds_bankAccount.Text)
            document.Add("ds_deceasedName", ds_deceasedName.Text)
            'document.Add("ds_dob", ds_dob.Text)
            document.Add("ds_dob", ds_dob.Value)
            document.Add("ds_dod", ds_dod.Value)
            document.Add("ds_cod", ds_cod.Text)
            document.Add("ds_package", ds_package.Text)
            document.Add("ds_price", ds_price.Text)

            collection.InsertOne(document)
            MessageBox.Show("Data added successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CLEARTEXT()
            Me.Hide()
            admin_dashboard.DirectServicesDVGLoad()

        End If
    End Sub

    Private Sub add_directServices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ds_dob.MinDate = Date.Now
        'ds_dod.MinDate = Date.Now
    End Sub

    Private Sub BunifuButton1_Click(sender As Object, e As EventArgs) Handles BunifuButton1.Click
        Me.Close()
        CLEARTEXT()
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
End Class