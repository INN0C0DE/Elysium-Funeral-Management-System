Imports MongoDB.Bson
Imports MongoDB.Driver
Imports MongoDB.Driver.Linq
Public Class add_lifeplan
    Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
    Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
    Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("lifeplan")
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

    Private Sub addlf_btn_Click(sender As Object, e As EventArgs) Handles addlf_btn.Click
        If add_name.Text = "" Or add_birthday.Text = "" Or add_number.Text = "" Or add_email.Text = "" Or add_package.Text = "" Or package_price.Text = "" Or add_paymentplan.Text = "" Or plan_price.Text = "" Or current_period.Text = "" Or total_period.Text = "" Or add_bankAccount.Text = "" Then
            MsgBox("Please fill up all forms needed.", vbInformation, "You forgot to fill something!")
        Else
            Dim selectedBirthday As String = add_birthday.Value.ToString("MM/dd/yyyy")
            Dim document As BsonDocument = New BsonDocument()
            document.Add("fullname", add_name.Text)
            document.Add("birthday", selectedBirthday)
            document.Add("number", add_number.Text)
            document.Add("email", add_email.Text)
            document.Add("address", add_address.Text)
            document.Add("package", add_package.Text)
            document.Add("payment_plan", add_paymentplan.Text)
            document.Add("package_price", package_price.Text)
            document.Add("plan_price", plan_price.Text)
            document.Add("current_period", current_period.Text)
            document.Add("total_period", total_period.Text)
            document.Add("bank", add_bankAccount.Text)
            document.Add("plan_added", add_planAdded.Text)
            collection.InsertOne(document)
            MessageBox.Show("Data added successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Hide()
            admin_dashboard.LifeplanDVGLoad()
            admin_dashboard.DisplayTotalLifeplan()
            CLEARTEXT()

            'BindDataToGridView()
            'DisplayTotalDocumentsCount()
        End If
    End Sub

    Private Sub BunifuButton1_Click(sender As Object, e As EventArgs) Handles BunifuButton1.Click
        Me.Close()
        CLEARTEXT()
    End Sub
    Public Sub CLEARTEXT()
        add_name.Clear()
        add_birthday.Value = Date.Now
        add_number.Clear()
        add_name.Clear()
        add_email.Clear()
        add_package.Text = Nothing
        add_paymentplan.Text = Nothing
        package_price.Clear()
        plan_price.Clear()
        current_period.Clear()
        total_period.Clear()
        add_bankAccount.Clear()
        add_planAdded.Value = Date.Now
    End Sub

    Private Sub add_lifeplan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        add_planAdded.Value = Date.Now
    End Sub
End Class