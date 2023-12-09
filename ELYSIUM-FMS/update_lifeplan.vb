﻿Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class update_lifeplan
    Private client As MongoClient
    Private database As IMongoDatabase
    Private collection As IMongoCollection(Of BsonDocument)
    Private Sub BunifuButton1_Click(sender As Object, e As EventArgs) Handles BunifuButton1.Click
        CLEARTEXT()
        Me.Close()
    End Sub
    Public Sub CLEARTEXT()
        add_id.Clear()
        add_name.Clear()
        add_birthday.Value = Date.Now
        add_number.Clear()
        add_name.Clear()
        add_email.Clear()
        add_address.Clear()
        add_package.Text = Nothing
        add_paymentplan.Text = Nothing
        package_price.Clear()
        plan_price.Clear()
        current_period.Clear()
        total_period.Clear()
        add_bankAccount.Clear()
    End Sub



    Private Sub update_lifeplan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Connect to MongoDB Atlas
        Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"
        client = New MongoClient(connectionString)
        database = client.GetDatabase("elysium-fms-database")
        collection = database.GetCollection(Of BsonDocument)("lifeplan")
    End Sub

    Private Sub updatelf_btn_Click(sender As Object, e As EventArgs) Handles updatelf_btn.Click
        Dim objectId As String = add_id.Text.Trim()

        If Not String.IsNullOrEmpty(objectId) Then
            ' Create the filter to match the ObjectId
            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))
            Dim selectedBirthday As String = add_birthday.Value.ToString("MM/dd/yyyy")
            ' Retrieve the existing document
            Dim existingDocument = collection.Find(filter).FirstOrDefault()

            If existingDocument IsNot Nothing Then
                ' Modify the document as needed
                existingDocument.Set("fullname", add_name.Text)
                existingDocument.Set("birthday", selectedBirthday)
                existingDocument.Set("number", add_number.Text)
                existingDocument.Set("email", add_email.Text)
                existingDocument.Set("address", add_address.Text)
                existingDocument.Set("package", add_package.Text)
                existingDocument.Set("payment_plan", add_paymentplan.Text)
                existingDocument.Set("package_price", package_price.Text)
                existingDocument.Set("plan_price", plan_price.Text)
                existingDocument.Set("current_period", current_period.Text)
                existingDocument.Set("total_period", total_period.Text)
                existingDocument.Set("bank", add_bankAccount.Text)
                ' Update the document in the collection
                collection.ReplaceOne(filter, existingDocument)

                MessageBox.Show("Data updated successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
                admin_dashboard.LifeplanDVGLoad()
                CLEARTEXT()
                Me.Hide()
            Else
                MessageBox.Show("Data not found!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If
        Else
            MessageBox.Show("Please select a data to update.", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub add_package_SelectedIndexChanged(sender As Object, e As EventArgs) Handles add_package.SelectedIndexChanged
        If add_package.Text = "AGAPI (TRADITIONAL)" Then
            package_price.Text = 85000.0
            plan_price.Clear()
            add_paymentplan.Text = Nothing
            total_period.Clear()
        ElseIf add_package.Text = "ELEOS (TRADITIONAL)" Then
            package_price.Text = 63750.0
            plan_price.Clear()
            total_period.Clear()
            add_paymentplan.Text = Nothing
        ElseIf add_package.Text = "THALIA (TRADITIONAL)" Then
            package_price.Text = 42500.0
            plan_price.Clear()
            total_period.Clear()
            add_paymentplan.Text = Nothing
        ElseIf add_package.Text = "SOPHRONIA (CREMATION)" Then
            package_price.Text = 123250.0
            plan_price.Clear()
            total_period.Clear()
            add_paymentplan.Text = Nothing
        ElseIf add_package.Text = "THEON (CREMATION)" Then
            package_price.Text = 80750.0
            plan_price.Clear()
            total_period.Clear()
            add_paymentplan.Text = Nothing
        ElseIf add_package.Text = "IRENE (CREMATION)" Then
            package_price.Text = 57800.0
            plan_price.Clear()
            total_period.Clear()
            add_paymentplan.Text = Nothing
        End If
    End Sub

    Private Sub add_paymentplan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles add_paymentplan.SelectedIndexChanged
        Dim TotalPrice As Double
        Dim RoundOffPrice As Double
        If add_paymentplan.Text = "ANNUAL" Then
            TotalPrice = package_price.Text / 5
            RoundOffPrice = Math.Round(TotalPrice, MidpointRounding.AwayFromZero)
            plan_price.Text = RoundOffPrice
            total_period.Text = 5
        ElseIf add_paymentplan.Text = "QUARTERLY" Then
            TotalPrice = package_price.Text / 20
            RoundOffPrice = Math.Round(TotalPrice, MidpointRounding.AwayFromZero)
            plan_price.Text = RoundOffPrice
            total_period.Text = 20
        ElseIf add_paymentplan.Text = "MONTHLY" Then
            TotalPrice = package_price.Text / 60
            RoundOffPrice = Math.Round(TotalPrice, MidpointRounding.AwayFromZero)
            plan_price.Text = RoundOffPrice
            total_period.Text = 60
        End If
    End Sub
End Class