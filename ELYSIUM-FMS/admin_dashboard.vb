Imports System.Globalization
Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class admin_dashboard
    'Timer for refresh APPT
    Private WithEvents minuteTimer As New Timer()

    Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"
    Private Sub BunifuLabel4_Click(sender As Object, e As EventArgs) Handles date_label.Click

    End Sub

    Private Sub BunifuButton1_Click(sender As Object, e As EventArgs) Handles BunifuButton1.Click
        admin_pages.SetPage("home")
    End Sub

    Private Sub BunifuButton2_Click(sender As Object, e As EventArgs) Handles BunifuButton2.Click
        admin_pages.SetPage("services")
    End Sub

    Private Sub BunifuButton3_Click(sender As Object, e As EventArgs) Handles BunifuButton3.Click
        admin_pages.SetPage("staff_account")
    End Sub

    Private Sub BunifuButton4_Click(sender As Object, e As EventArgs) Handles BunifuButton4.Click
        admin_pages.SetPage("admin_profile")
        'AdminProfileLoad()
    End Sub

    Private Sub BunifuButton5_Click(sender As Object, e As EventArgs) Handles BunifuButton5.Click
        admin_pages.SetPage("appointment")
    End Sub

    Private Sub BunifuButton6_Click(sender As Object, e As EventArgs) Handles BunifuButton6.Click
        admin_pages.SetPage("lifeplan")
    End Sub

    Private Sub BunifuButton7_Click(sender As Object, e As EventArgs) Handles BunifuButton7.Click
        admin_pages.SetPage("direct_services")
    End Sub

    Private Sub BunifuButton8_Click(sender As Object, e As EventArgs) Handles BunifuButton8.Click
        admin_pages.SetPage("inventory")
    End Sub

    Private Sub BunifuButton12_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub admin_dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        AdminProfileLoad()
        SetProfilePhoto()
        StaffDVGLoad()
        ApptDVGLoad()
        LifeplanDVGLoad()
        DisplayTotalAppointment()
        DisplayTotalLifeplan()
        DirectServicesDVGLoad()
        InventoryDVGLoad()
        EO_user.Visible = False
        EO_pass.Visible = False
        'Timer Appointment Refresher every minute
        minuteTimer.Interval = 60000
        minuteTimer.Start()
    End Sub
    Private Sub minuteTimer_Tick(sender As Object, e As EventArgs) Handles minuteTimer.Tick
        ApptDVGLoad()
        DisplayTotalAppointment()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        time_label.Text = DateTime.Now.ToString("hh:mm:ss tt") ' Display current time
        date_label.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy") ' Display current date
    End Sub

    Private Sub addstaff_btn_Click(sender As Object, e As EventArgs) Handles addstaff_btn.Click
        add_staffaccount.Show()
    End Sub

    Private Sub staff_updatebtn_Click(sender As Object, e As EventArgs) Handles staff_updatebtn.Click
        'update_staffaccount.Show()
        Try
            If staff_dgv.SelectedRows.Count > 0 Then
                With update_staffaccount

                    .update_name.Text = staff_dgv.SelectedRows.Item(0).Cells(0).Value
                    .update_username.Text = staff_dgv.SelectedRows.Item(0).Cells(1).Value
                    .update_pwd.Text = staff_dgv.SelectedRows.Item(0).Cells(2).Value
                    .update_rfid.Text = staff_dgv.SelectedRows.Item(0).Cells(3).Value
                    .update_gender.Text = staff_dgv.SelectedRows.Item(0).Cells(4).Value
                    .update_age.Text = staff_dgv.SelectedRows.Item(0).Cells(5).Value
                    .update_email.Text = staff_dgv.SelectedRows.Item(0).Cells(6).Value
                    .update_number.Text = staff_dgv.SelectedRows.Item(0).Cells(7).Value
                    .update_id.Text = staff_dgv.SelectedRows.Item(0).Cells(8).Value
                    .ShowDialog()

                End With
            Else
                MsgBox("Please select account to edit.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BunifuButton9_Click(sender As Object, e As EventArgs) Handles BunifuButton9.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to sign out?", "SIGN-OUT STATUS:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If (result = DialogResult.Yes) Then
            admin_pages.SetPage("home")
            login_form.Show()

            Me.Hide()
        Else
            'MessageBox.Show("SIGNING OUT CANCELLED!", "SIGN-OUT STATUS:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub admin_dashboard_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            Application.Exit()
        End If
    End Sub

    Private Sub lp_packages_Click(sender As Object, e As EventArgs) Handles lp_packages.Click
        lifeplan_packages.Show()
        Me.Hide()

    End Sub

    Private Sub ds_packagesBTN_Click(sender As Object, e As EventArgs) Handles ds_packagesBTN.Click
        ds_packages.Show()
        Me.Hide()
    End Sub

    Private Sub AdminProfileLoad()
        'for admin profile details display
        ' MongoDB connection string (replace <connection_string> with your actual connection string)
        Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"

        ' Create a MongoClient object with the connection string
        Dim client As MongoClient = New MongoClient(connectionString)

        ' Access the database and collection
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("admin_account")

        ' Retrieve the data from the collection
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Empty
        Dim result As List(Of BsonDocument) = collection.Find(filter).ToList()

        ' Check if any documents were found
        If result.Count > 0 Then
            ' Assuming each document has "name" and "email" fields
            Dim name As String = result(0).GetValue("fullname").ToString()
            Dim rfid As String = result(0).GetValue("rfid_no").ToString()
            Dim adminUser As String = result(0).GetValue("username").ToString()
            Dim adminPass As String = result(0).GetValue("password").ToString()
            Dim adminAge As String = result(0).GetValue("age").ToString()
            Dim adminEmail As String = result(0).GetValue("email").ToString()
            Dim adminGender As String = result(0).GetValue("gender").ToString()

            ' Update TextBox1 and TextBox2 with the retrieved values
            admin_name1.Text = name
            admin_rfid1.Text = rfid
            admin_username1.Text = adminUser
            admin_pwd1.Text = adminPass
            admin_age.Text = adminAge
            admin_email.Text = adminEmail
            admin_gender.Text = adminGender

        End If
    End Sub

    Private Sub admin_dashboard_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        AdminProfileLoad()
    End Sub

    Private Sub admin_updatebtn_Click(sender As Object, e As EventArgs) Handles admin_updatebtn.Click
        ' MongoDB connection string (replace <connection_string> with your actual connection string)
        Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"

        ' Create a MongoClient object with the connection string
        Dim client As MongoClient = New MongoClient(connectionString)

        ' Access the database and collection
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("admin_account")

        ' Retrieve the current document
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Empty
        Dim result As List(Of BsonDocument) = collection.Find(filter).ToList()

        ' Check if any documents were found
        If result.Count > 0 Then
            ' Assuming each document has "fullname", "rfid_no", "username", and "password" fields
            Dim documentId As ObjectId = result(0).GetValue("_id").AsObjectId
            Dim updatedName As String = admin_name1.Text
            Dim updatedRfid As String = admin_rfid1.Text
            Dim updatedUsername As String = admin_username1.Text
            Dim updatedPassword As String = admin_pwd1.Text
            Dim updatedEmail As String = admin_email.Text
            Dim updatedGender As String = admin_gender.Text
            Dim updatedAge As String = admin_age.Text

            ' Create an update definition with the new values
            Dim update As UpdateDefinition(Of BsonDocument) = Builders(Of BsonDocument).Update.Set(Of String)("fullname", updatedName) _
                                                                                        .Set(Of String)("rfid_no", updatedRfid) _
                                                                                        .Set(Of String)("username", updatedUsername) _
                                                                                        .Set(Of String)("password", updatedPassword) _
                                                                                        .Set(Of String)("email", updatedEmail) _
                                                                                        .Set(Of String)("gender", updatedGender) _
                                                                                        .Set(Of String)("age", updatedAge) _

            ' Update the document in the collection
            collection.UpdateOne(Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", documentId), update)

            MessageBox.Show("Data updated successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            username_label.Text = admin_name1.Text + ""
        End If
    End Sub

    Private Sub admin_gender_SelectedIndexChanged(sender As Object, e As EventArgs) Handles admin_gender.SelectedIndexChanged
        SetProfilePhoto()
    End Sub
    Private Sub SetProfilePhoto()

        If admin_gender.Text = "Male" Then
            admin_picture.Image = My.Resources.admin_male
        ElseIf admin_gender.Text = "Female" Then
            admin_picture.Image = My.Resources.admin_female
        Else
            admin_picture.Image = Nothing
        End If
    End Sub
    Public Sub StaffDVGLoad()
        '' MongoDB connection string
        'Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"

        ' Create a MongoDB client
        Dim client As MongoClient = New MongoClient(connectionString)

        ' Access the MongoDB database
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")

        ' Access the collection containing your data
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("staff_account")

        ' Fetch the data from the collection
        Dim documents As List(Of BsonDocument) = collection.Find(New BsonDocument()).ToList()

        ' Reverse the order of documents to have the most recent data on top
        documents.Reverse()

        ' Create a DataTable to hold the data
        Dim dataTable As DataTable = New DataTable()

        ' Add columns to the DataTable (replace with your own field names)

        dataTable.Columns.Add("Full Name")
        dataTable.Columns.Add("Username")
        dataTable.Columns.Add("Password")
        dataTable.Columns.Add("RFID No.")
        dataTable.Columns.Add("Gender")
        dataTable.Columns.Add("Age")
        dataTable.Columns.Add("Email")
        dataTable.Columns.Add("Number")
        dataTable.Columns.Add("ID")
        ' ...

        ' Add rows to the DataTable
        For Each document As BsonDocument In documents
            ' Create a new DataRow
            Dim row As DataRow = dataTable.NewRow()

            ' Convert ObjectId to string
            Dim objectId As ObjectId = document("_id").AsObjectId
            row("Id") = objectId.ToString()

            ' Set the values for each field (replace with your own field names)
            row("full name") = document("fullname").AsString
            row("username") = document("username").AsString
            row("password") = document("password").AsString
            row("rfid no.") = document("rfid").AsString
            row("gender") = document("gender").AsString
            row("age") = document("age").AsString
            row("email") = document("email").AsString
            row("number") = document("number").AsString
            ' ...

            ' Add the DataRow to the DataTable
            dataTable.Rows.Add(row)
        Next

        ' Bind the DataTable to the DataGridView
        staff_dgv.DataSource = dataTable
    End Sub
    Public Sub ApptDVGLoad()
        '' Create a MongoDB client
        'Dim client As MongoClient = New MongoClient(connectionString)

        ' Create a MongoDB client
        Dim client As MongoClient = New MongoClient(connectionString)

        ' Access the MongoDB database
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")

        ' Access the collection containing your data
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("appointments")

        '' Define an index for sorting by "insertionOrder" in descending order
        'Dim indexKeysDefinition = Builders(Of BsonDocument).IndexKeys.Descending("insertionOrder")
        'Dim indexModel = New CreateIndexModel(Of BsonDocument)(indexKeysDefinition)

        '' Create the index
        'collection.Indexes.CreateOne(indexModel)

        '' Fetch the data from the collection sorted by the "insertionOrder" field in descending order
        'Dim documents As List(Of BsonDocument) = collection.Find(New BsonDocument()).Sort(Builders(Of BsonDocument).Sort.Descending("insertionOrder")).ToList()



        ' Fetch the data from the collection
        Dim documents As List(Of BsonDocument) = collection.Find(New BsonDocument()).ToList()
        ' Reverse the order of documents to have the most recent data on top
        documents.Reverse()
        ' Create a DataTable to hold the data
        Dim dataTable As DataTable = New DataTable()

        ' Add columns to the DataTable (replace with your own field names)

        dataTable.Columns.Add("Full Name")
        dataTable.Columns.Add("Email")
        dataTable.Columns.Add("Number")
        dataTable.Columns.Add("Address")
        dataTable.Columns.Add("Appointment Date")
        dataTable.Columns.Add("Appointment Time")
        dataTable.Columns.Add("Appointment Status")
        dataTable.Columns.Add("ID")
        ' ...

        ' Add rows to the DataTable
        For Each document As BsonDocument In documents
            ' Create a new DataRow
            Dim row As DataRow = dataTable.NewRow()

            ' Convert ObjectId to string
            Dim objectId As ObjectId = document("_id").AsObjectId
            row("Id") = objectId.ToString()

            ' Set the values for each field (replace with your own field names)
            row("full name") = document("fullname").AsString
            row("email") = document("email").AsString
            row("number") = document("number").AsString
            row("address") = document("address").AsString
            row("appointment date") = document("apptDate").AsString
            row("appointment time") = document("apptTime").AsString
            row("appointment status") = document("apptStatus").AsString
            ' ...

            ' Add the DataRow to the DataTable
            dataTable.Rows.Add(row)
        Next
        '' Sort the DataTable by the "Appointment Date" in descending order
        'dataTable.DefaultView.Sort = "Appointment Date DESC"
        ' Bind the DataTable to the DataGridView
        appointment_dgv.DataSource = dataTable
    End Sub

    Private searchDelayTimer As Timer
    Private Sub search_staff_TextChanged(sender As Object, e As EventArgs) Handles search_staff.TextChanged
        ' Stop any previous timer
        If searchDelayTimer IsNot Nothing Then
            searchDelayTimer.Stop()
            searchDelayTimer.Dispose()
        End If

        ' Create a new timer
        searchDelayTimer = New Timer()
        searchDelayTimer.Interval = 200 ' Adjust the delay as needed (in milliseconds)
        AddHandler searchDelayTimer.Tick, AddressOf StartSearch2
        searchDelayTimer.Start()
    End Sub
    Private Sub StartSearch2(sender As Object, e As EventArgs)
        ' Stop the timer
        searchDelayTimer.Stop()

        ' Perform the search
        PerformSearch2()
    End Sub
    Private Sub PerformSearch2()
        ' Get the search keyword from the TextBox
        Dim keyword As String = search_staff.Text
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        ' Access the MongoDB database and collection
        Dim databaseName As String = "elysium-fms-database"
        Dim collectionName As String = "staff_account"
        Dim database As IMongoDatabase = client.GetDatabase(databaseName)
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

        ' Create a filter to match the search keyword
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Regex("fullname", New BsonRegularExpression(keyword, "i"))

        ' Retrieve the documents matching the filter
        Dim searchResults As List(Of BsonDocument) = collection.Find(filter).ToList()

        ' Reverse the order of documents to have the most recent data on top
        searchResults.Reverse()

        ' Convert the searchResults to a DataTable
        Dim dataTable As New DataTable()

        ' Disable auto-generating columns
        staff_dgv.AutoGenerateColumns = False

        ' Create a dictionary to map field names to custom column names
        Dim columnNames As New Dictionary(Of String, String)()

        columnNames.Add("fullname", "Full Name")
        columnNames.Add("username", "Username")
        columnNames.Add("password", "Password")
        columnNames.Add("rfid", "RFID No.")
        columnNames.Add("gender", "Gender")
        columnNames.Add("age", "Age")
        columnNames.Add("email", "Email")
        columnNames.Add("number", "Number")
        columnNames.Add("_id", "ID")
        ' Add more field name to custom column name mappings as needed

        ' Add columns to the DataTable
        For Each fieldName As String In columnNames.Keys
            If Not dataTable.Columns.Contains(columnNames(fieldName)) Then
                dataTable.Columns.Add(columnNames(fieldName), GetType(String))
            End If
        Next
        ' Add rows to the DataTable
        For Each document As BsonDocument In searchResults
            ' Create a new DataRow
            Dim row As DataRow = dataTable.NewRow()

            ' Set the values for each field (using the custom column names)
            For Each fieldName As String In columnNames.Keys
                Dim columnName As String = columnNames(fieldName)
                If dataTable.Columns.Contains(columnName) Then
                    If fieldName = "_id" Then
                        row(columnName) = document(fieldName).AsObjectId.ToString()
                    Else
                        row(columnName) = document(fieldName).AsString
                    End If
                End If
            Next

            ' Add the DataRow to the DataTable
            dataTable.Rows.Add(row)
        Next

        ' Bind the DataTable to the DataGridView
        staff_dgv.DataSource = dataTable
    End Sub

    Private Sub staff_archivebtn_Click(sender As Object, e As EventArgs) Handles staff_archivebtn.Click
        ' Check if a row is selected in the DataGridView
        If staff_dgv.SelectedRows.Count > 0 Then
            ' Get the ObjectId value from the selected row
            Dim selectedRow As DataGridViewRow = staff_dgv.SelectedRows(0)
            Dim objectId As String = selectedRow.Cells("ID").Value.ToString()

            ' Move the data from one collection to another based on the ObjectId value
            MoveData(objectId)
        End If
        StaffDVGLoad()
    End Sub
    Private Sub MoveData(objectId As String)
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("staff_account")
        ' Access the MongoDB database and collections
        Dim sourceDatabaseName As String = "elysium-fms-database"
        Dim sourceCollectionName As String = "staff_account"
        Dim targetDatabaseName As String = "elysium-fms-database"
        Dim targetCollectionName As String = "staff_account_archive"
        Dim sourceDatabase As IMongoDatabase = client.GetDatabase(sourceDatabaseName)
        Dim targetDatabase As IMongoDatabase = client.GetDatabase(targetDatabaseName)
        Dim sourceCollection As IMongoCollection(Of BsonDocument) = sourceDatabase.GetCollection(Of BsonDocument)(sourceCollectionName)
        Dim targetCollection As IMongoCollection(Of BsonDocument) = targetDatabase.GetCollection(Of BsonDocument)(targetCollectionName)

        ' Create a filter to match the ObjectId
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))

        ' Find the document in the source collection
        Dim document As BsonDocument = sourceCollection.Find(filter).FirstOrDefault()

        ' If the document exists, insert it into the target collection and delete it from the source collection
        If document IsNot Nothing Then
            ' Insert the document into the target collection
            targetCollection.InsertOne(document)

            ' Delete the document from the source collection
            sourceCollection.DeleteOne(filter)
            MessageBox.Show("Data archived successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub archived_data_Click(sender As Object, e As EventArgs) Handles archived_data.Click
        admin_authRFID.Show()
    End Sub
    Public Sub DisplayTotalAppointment()
        ' Access the MongoDB database and collection
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        Dim databaseName As String = "elysium-fms-database"
        Dim collectionName As String = "appointments"
        Dim database As IMongoDatabase = client.GetDatabase(databaseName)
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

        ' Get the total count of documents in the collection
        Dim totalCount As Long = collection.CountDocuments(FilterDefinition(Of BsonDocument).Empty)

        ' Display the count in the label
        'data_label.Text = "Total Documents: " & totalCount.ToString()
        appt_label.Text = totalCount.ToString()
        If appt_label.Text > 1 Then
            appt_label2.Text = "APPOINTMENTS"
        Else
            appt_label2.Text = "APPOINTMENT"
        End If
    End Sub

    Private Sub appt_archivebtn_Click(sender As Object, e As EventArgs) Handles appt_archivebtn.Click
        ' Check if a row is selected in the DataGridView
        If appointment_dgv.SelectedRows.Count > 0 Then
            ' Get the ObjectId value from the selected row
            Dim selectedRow As DataGridViewRow = appointment_dgv.SelectedRows(0)
            Dim objectId As String = selectedRow.Cells("ID").Value.ToString()

            ' Move the data from one collection to another based on the ObjectId value
            ArchiveAppointment(objectId)
        End If
        ApptDVGLoad()
        DisplayTotalAppointment()
    End Sub

    Private Sub appt_updatebtn_Click(sender As Object, e As EventArgs) Handles appt_updatebtn.Click
        'update_appointment.Show()
        Try
            If appointment_dgv.SelectedRows.Count > 0 Then
                With update_appointment

                    .update_name.Text = appointment_dgv.SelectedRows.Item(0).Cells(0).Value
                    .update_email.Text = appointment_dgv.SelectedRows.Item(0).Cells(1).Value
                    .update_number.Text = appointment_dgv.SelectedRows.Item(0).Cells(2).Value
                    .update_address.Text = appointment_dgv.SelectedRows.Item(0).Cells(3).Value

                    Dim aptDate As Object = appointment_dgv.SelectedRows.Item(0).Cells(4).Value
                    Dim aptdate2 As DateTime
                    If DateTime.TryParseExact(aptDate.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, aptdate2) Then
                        .update_date.Value = aptdate2
                    End If

                    Dim aptTime As Object = appointment_dgv.SelectedRows.Item(0).Cells(5).Value
                    Dim aptTime2 As DateTime
                    If DateTime.TryParseExact(aptTime.ToString(), "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, aptTime2) Then
                        .update_time.Value = aptTime2
                    End If

                    .update_status.Text = appointment_dgv.SelectedRows.Item(0).Cells(6).Value
                    .update_id.Text = appointment_dgv.SelectedRows.Item(0).Cells(7).Value
                    .ShowDialog()

                End With
            Else
                MsgBox("Please select account to edit.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            'Display the exception message in a MessageBox
            'MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Write the exception to the console
            Console.WriteLine("An error occurred: " & ex.Message)
        End Try
    End Sub
    Private Sub ArchiveAppointment(objectId As String)
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("appointments")
        ' Access the MongoDB database and collections
        Dim sourceDatabaseName As String = "elysium-fms-database"
        Dim sourceCollectionName As String = "appointments"
        Dim targetDatabaseName As String = "elysium-fms-database"
        Dim targetCollectionName As String = "appointments_archive"
        Dim sourceDatabase As IMongoDatabase = client.GetDatabase(sourceDatabaseName)
        Dim targetDatabase As IMongoDatabase = client.GetDatabase(targetDatabaseName)
        Dim sourceCollection As IMongoCollection(Of BsonDocument) = sourceDatabase.GetCollection(Of BsonDocument)(sourceCollectionName)
        Dim targetCollection As IMongoCollection(Of BsonDocument) = targetDatabase.GetCollection(Of BsonDocument)(targetCollectionName)

        ' Create a filter to match the ObjectId
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))

        ' Find the document in the source collection
        Dim document As BsonDocument = sourceCollection.Find(filter).FirstOrDefault()

        ' If the document exists, insert it into the target collection and delete it from the source collection
        If document IsNot Nothing Then
            ' Insert the document into the target collection
            targetCollection.InsertOne(document)

            ' Delete the document from the source collection
            sourceCollection.DeleteOne(filter)
            MessageBox.Show("Data archived successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub appt_archived_data_Click(sender As Object, e As EventArgs) Handles appt_archived_data.Click
        appointment_authRFID.Show()
    End Sub

    Private Sub search_appointment_TextChanged(sender As Object, e As EventArgs) Handles search_appointment.TextChanged
        ' Stop any previous timer
        If searchDelayTimer IsNot Nothing Then
            searchDelayTimer.Stop()
            searchDelayTimer.Dispose()
        End If

        ' Create a new timer
        searchDelayTimer = New Timer()
        searchDelayTimer.Interval = 200 ' Adjust the delay as needed (in milliseconds)
        AddHandler searchDelayTimer.Tick, AddressOf StartSearchAppt
        searchDelayTimer.Start()
    End Sub
    Private Sub StartSearchAppt(sender As Object, e As EventArgs)
        ' Stop the timer
        searchDelayTimer.Stop()

        ' Perform the search
        PerformSearchAppt()
    End Sub
    Private Sub PerformSearchAppt()
        ' Get the search keyword from the TextBox
        Dim keyword As String = search_appointment.Text
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        ' Access the MongoDB database and collection
        Dim databaseName As String = "elysium-fms-database"
        Dim collectionName As String = "appointments"
        Dim database As IMongoDatabase = client.GetDatabase(databaseName)
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

        ' Create a filter to match the search keyword
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Regex("fullname", New BsonRegularExpression(keyword, "i"))

        ' Retrieve the documents matching the filter
        Dim searchResults As List(Of BsonDocument) = collection.Find(filter).ToList()
        ' Reverse the result of the search filter by new to old data
        searchResults.Reverse()
        ' Convert the searchResults to a DataTable
        Dim dataTable As New DataTable()

        ' Disable auto-generating columns
        staff_dgv.AutoGenerateColumns = False

        ' Create a dictionary to map field names to custom column names
        Dim columnNames As New Dictionary(Of String, String)()

        columnNames.Add("fullname", "Full Name")
        columnNames.Add("email", "Email")
        columnNames.Add("number", "Number")
        columnNames.Add("address", "Address")
        columnNames.Add("apptDate", "Appointment Date")
        columnNames.Add("apptTime", "Appointment Time")
        columnNames.Add("apptStatus", "Appointment Status")
        columnNames.Add("_id", "ID")
        ' Add more field name to custom column name mappings as needed

        ' Add columns to the DataTable
        For Each fieldName As String In columnNames.Keys
            If Not dataTable.Columns.Contains(columnNames(fieldName)) Then
                dataTable.Columns.Add(columnNames(fieldName), GetType(String))
            End If
        Next
        ' Add rows to the DataTable
        For Each document As BsonDocument In searchResults
            ' Create a new DataRow
            Dim row As DataRow = dataTable.NewRow()

            ' Set the values for each field (using the custom column names)
            For Each fieldName As String In columnNames.Keys
                Dim columnName As String = columnNames(fieldName)
                If dataTable.Columns.Contains(columnName) Then
                    If fieldName = "_id" Then
                        row(columnName) = document(fieldName).AsObjectId.ToString()
                    Else
                        row(columnName) = document(fieldName).AsString
                    End If
                End If
            Next

            ' Add the DataRow to the DataTable
            dataTable.Rows.Add(row)
        Next

        ' Bind the DataTable to the DataGridView
        appointment_dgv.DataSource = dataTable
    End Sub

    'Life Plan
    Public Sub LifeplanDVGLoad()
        '' Create a MongoDB client
        'Dim client As MongoClient = New MongoClient(connectionString)

        ' Create a MongoDB client
        Dim client As MongoClient = New MongoClient(connectionString)

        ' Access the MongoDB database
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")

        ' Access the collection containing your data
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("lifeplan")

        ' Fetch the data from the collection
        Dim documents As List(Of BsonDocument) = collection.Find(New BsonDocument()).ToList()

        ' Reverse the order of documents to have the most recent data on top
        documents.Reverse()

        ' Create a DataTable to hold the data
        Dim dataTable As DataTable = New DataTable()

        ' Add columns to the DataTable (replace with your own field names)

        dataTable.Columns.Add("Full Name")
        dataTable.Columns.Add("Birthday")
        dataTable.Columns.Add("Number")
        dataTable.Columns.Add("Email")
        dataTable.Columns.Add("Address")
        dataTable.Columns.Add("Plan Package")
        dataTable.Columns.Add("Payment Plan")
        dataTable.Columns.Add("Plan Price")
        dataTable.Columns.Add("Contract Price Plan")
        dataTable.Columns.Add("Current Paid")
        dataTable.Columns.Add("Contract to be Paid")
        dataTable.Columns.Add("Bank Account No.")
        dataTable.Columns.Add("ID")
        ' ...

        ' Add rows to the DataTable
        For Each document As BsonDocument In documents
            ' Create a new DataRow
            Dim row As DataRow = dataTable.NewRow()

            ' Convert ObjectId to string
            Dim objectId As ObjectId = document("_id").AsObjectId
            row("Id") = objectId.ToString()

            ' Set the values for each field (replace with your own field names)
            row("full name") = document("fullname").AsString
            row("birthday") = document("birthday").AsString
            row("number") = document("number").AsString
            row("email") = document("email").AsString
            row("address") = document("address").AsString
            row("Plan Package") = document("package").AsString
            row("Payment Plan") = document("payment_plan").AsString
            row("Plan Price") = document("package_price").AsString
            row("Contract Price Plan") = document("plan_price").AsString
            row("Current Paid") = document("current_period").AsString
            row("Contract to be Paid") = document("total_period").AsString
            row("Bank Account No.") = document("bank").AsString
            ' ...

            ' Add the DataRow to the DataTable
            dataTable.Rows.Add(row)
        Next

        ' Bind the DataTable to the DataGridView
        lifeplan_dgv.DataSource = dataTable
    End Sub

    Private Sub lf_addbtn_Click(sender As Object, e As EventArgs) Handles lf_addbtn.Click
        add_lifeplan.Show()
    End Sub

    Public Sub DisplayTotalLifeplan()
        ' Access the MongoDB database and collection
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        Dim databaseName As String = "elysium-fms-database"
        Dim collectionName As String = "lifeplan"
        Dim database As IMongoDatabase = client.GetDatabase(databaseName)
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

        ' Get the total count of documents in the collection
        Dim totalCount As Long = collection.CountDocuments(FilterDefinition(Of BsonDocument).Empty)

        ' Display the count in the label
        'data_label.Text = "Total Documents: " & totalCount.ToString()
        lf_label.Text = totalCount.ToString()
        If lf_label.Text > 1 Then
            lf_label2.Text = "ACTIVE LIFE PLANS"
        Else
            lf_label2.Text = "ACTIVE LIFE PLAN"
        End If
    End Sub

    Private Sub lf_updatebtn_Click(sender As Object, e As EventArgs) Handles lf_updatebtn.Click
        'update_staffaccount.Show()
        Try
            If lifeplan_dgv.SelectedRows.Count > 0 Then
                With update_lifeplan

                    .add_name.Text = lifeplan_dgv.SelectedRows.Item(0).Cells(0).Value
                    Dim bdayCellValue As Object = lifeplan_dgv.SelectedRows.Item(0).Cells(1).Value
                    Dim bday As DateTime
                    If DateTime.TryParseExact(bdayCellValue.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, bday) Then
                        .add_birthday.Value = bday
                    End If
                    .add_number.Text = lifeplan_dgv.SelectedRows.Item(0).Cells(2).Value
                    .add_email.Text = lifeplan_dgv.SelectedRows.Item(0).Cells(3).Value
                    .add_address.Text = lifeplan_dgv.SelectedRows.Item(0).Cells(4).Value
                    .add_package.Text = lifeplan_dgv.SelectedRows.Item(0).Cells(5).Value
                    .add_paymentplan.Text = lifeplan_dgv.SelectedRows.Item(0).Cells(6).Value
                    .package_price.Text = lifeplan_dgv.SelectedRows.Item(0).Cells(7).Value
                    .plan_price.Text = lifeplan_dgv.SelectedRows.Item(0).Cells(8).Value
                    .current_period.Text = lifeplan_dgv.SelectedRows.Item(0).Cells(9).Value
                    .total_period.Text = lifeplan_dgv.SelectedRows.Item(0).Cells(10).Value
                    .add_bankAccount.Text = lifeplan_dgv.SelectedRows.Item(0).Cells(11).Value
                    .add_id.Text = lifeplan_dgv.SelectedRows.Item(0).Cells(12).Value
                    .ShowDialog()

                End With
            Else
                MsgBox("Please select lifeplan to edit.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lf_archivebtn_Click(sender As Object, e As EventArgs) Handles lf_archivebtn.Click
        ' Check if a row is selected in the DataGridView
        If lifeplan_dgv.SelectedRows.Count > 0 Then
            ' Get the ObjectId value from the selected row
            Dim selectedRow As DataGridViewRow = lifeplan_dgv.SelectedRows(0)
            Dim objectId As String = selectedRow.Cells("ID").Value.ToString()

            ' Move the data from one collection to another based on the ObjectId value
            ArchiveLifeplan(objectId)
        End If
        LifeplanDVGLoad()
        DisplayTotalLifeplan()
    End Sub
    Private Sub ArchiveLifeplan(objectId As String)
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("lifeplan")
        ' Access the MongoDB database and collections
        Dim sourceDatabaseName As String = "elysium-fms-database"
        Dim sourceCollectionName As String = "lifeplan"
        Dim targetDatabaseName As String = "elysium-fms-database"
        Dim targetCollectionName As String = "lifeplan_archive"
        Dim sourceDatabase As IMongoDatabase = client.GetDatabase(sourceDatabaseName)
        Dim targetDatabase As IMongoDatabase = client.GetDatabase(targetDatabaseName)
        Dim sourceCollection As IMongoCollection(Of BsonDocument) = sourceDatabase.GetCollection(Of BsonDocument)(sourceCollectionName)
        Dim targetCollection As IMongoCollection(Of BsonDocument) = targetDatabase.GetCollection(Of BsonDocument)(targetCollectionName)

        ' Create a filter to match the ObjectId
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))

        ' Find the document in the source collection
        Dim document As BsonDocument = sourceCollection.Find(filter).FirstOrDefault()

        ' If the document exists, insert it into the target collection and delete it from the source collection
        If document IsNot Nothing Then
            ' Insert the document into the target collection
            targetCollection.InsertOne(document)

            ' Delete the document from the source collection
            sourceCollection.DeleteOne(filter)
            MessageBox.Show("Data archived successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub lf_archived_data_Click(sender As Object, e As EventArgs) Handles lf_archived_data.Click
        lifeplan_authRFID.Show()
    End Sub

    Private Sub search_lifeplan_TextChanged(sender As Object, e As EventArgs) Handles search_lifeplan.TextChanged
        ' Stop any previous timer
        If searchDelayTimer IsNot Nothing Then
            searchDelayTimer.Stop()
            searchDelayTimer.Dispose()
        End If

        ' Create a new timer
        searchDelayTimer = New Timer()
        searchDelayTimer.Interval = 200 ' Adjust the delay as needed (in milliseconds)
        AddHandler searchDelayTimer.Tick, AddressOf StartSearchLifeplan
        searchDelayTimer.Start()
    End Sub
    Private Sub StartSearchLifeplan(sender As Object, e As EventArgs)
        ' Stop the timer
        searchDelayTimer.Stop()

        ' Perform the search
        PerformSearchLifeplan()
    End Sub
    Private Sub PerformSearchLifeplan()
        ' Get the search keyword from the TextBox
        Dim keyword As String = search_lifeplan.Text
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        ' Access the MongoDB database and collection
        Dim databaseName As String = "elysium-fms-database"
        Dim collectionName As String = "lifeplan"
        Dim database As IMongoDatabase = client.GetDatabase(databaseName)
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

        ' Create a filter to match the search keyword
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Regex("fullname", New BsonRegularExpression(keyword, "i"))

        ' Retrieve the documents matching the filter
        Dim searchResults As List(Of BsonDocument) = collection.Find(filter).ToList()

        ' Reverse the order of documents to have the most recent data on top
        searchResults.Reverse()

        ' Convert the searchResults to a DataTable
        Dim dataTable As New DataTable()

        ' Disable auto-generating columns
        staff_dgv.AutoGenerateColumns = False

        ' Create a dictionary to map field names to custom column names
        Dim columnNames As New Dictionary(Of String, String)()

        columnNames.Add("fullname", "Full Name")
        columnNames.Add("birthday", "Birthday")
        columnNames.Add("number", "Number")
        columnNames.Add("email", "Email")
        columnNames.Add("address", "Address")
        columnNames.Add("package", "Plan Package")
        columnNames.Add("payment_plan", "Payment Plan")
        columnNames.Add("package_price", "Plan Price")
        columnNames.Add("plan_price", "Contract Price Plan")
        columnNames.Add("current_period", "Current Paid")
        columnNames.Add("total_period", "Contract to be Paid")
        columnNames.Add("bank", "Bank Account No.")
        columnNames.Add("_id", "ID")
        ' Add more field name to custom column name mappings as needed

        ' Add columns to the DataTable
        For Each fieldName As String In columnNames.Keys
            If Not dataTable.Columns.Contains(columnNames(fieldName)) Then
                dataTable.Columns.Add(columnNames(fieldName), GetType(String))
            End If
        Next
        ' Add rows to the DataTable
        For Each document As BsonDocument In searchResults
            ' Create a new DataRow
            Dim row As DataRow = dataTable.NewRow()

            ' Set the values for each field (using the custom column names)
            For Each fieldName As String In columnNames.Keys
                Dim columnName As String = columnNames(fieldName)
                If dataTable.Columns.Contains(columnName) Then
                    If fieldName = "_id" Then
                        row(columnName) = document(fieldName).AsObjectId.ToString()
                    Else
                        row(columnName) = document(fieldName).AsString
                    End If
                End If
            Next

            ' Add the DataRow to the DataTable
            dataTable.Rows.Add(row)
        Next

        ' Bind the DataTable to the DataGridView
        lifeplan_dgv.DataSource = dataTable
    End Sub

    Private Sub admin_pages_SelectedIndexChanged(sender As Object, e As EventArgs) Handles admin_pages.SelectedIndexChanged

    End Sub

    Private Sub ds_add_Click(sender As Object, e As EventArgs) Handles ds_add.Click
        add_directServices.Show()
    End Sub

    <Obsolete>
    Public Sub DirectServicesDVGLoad()

        ' Create a MongoDB client
        Dim client As MongoClient = New MongoClient(connectionString)

        ' Access the MongoDB database
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")

        ' Access the collection containing your data
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("direct_services")

        ' Fetch the data from the collection
        Dim documents As List(Of BsonDocument) = collection.Find(New BsonDocument()).ToList()

        ' Reverse the order of documents to have the most recent data on top
        documents.Reverse()

        ' Create a DataTable to hold the data
        Dim dataTable As DataTable = New DataTable()

        ' Add columns to the DataTable (replace with your own field names)

        dataTable.Columns.Add("Applicant Full Name")
        dataTable.Columns.Add("Contact No.")
        dataTable.Columns.Add("Email")
        dataTable.Columns.Add("Address")
        dataTable.Columns.Add("Bank Account No.")
        dataTable.Columns.Add("Deceased Full Name")
        dataTable.Columns.Add("Date of Birth")
        dataTable.Columns.Add("Date Died")
        dataTable.Columns.Add("Cause of Death")
        dataTable.Columns.Add("Package")
        dataTable.Columns.Add("Price")
        dataTable.Columns.Add("ID")
        ' ...

        ' Add rows to the DataTable
        For Each document As BsonDocument In documents
            ' Create a new DataRow
            Dim row As DataRow = dataTable.NewRow()

            ' Convert ObjectId to string
            Dim objectId As ObjectId = document("_id").AsObjectId
            row("Id") = objectId.ToString()

            ' Set the values for each field (replace with your own field names)
            row("Applicant Full Name") = document("ds_appName").AsString
            row("Contact No.") = document("ds_contact").AsString
            row("Email") = document("ds_email").AsString
            row("Address") = document("ds_address").AsString
            row("Bank Account No.") = document("ds_bankAccount").AsString
            row("Deceased Full Name") = document("ds_deceasedName").AsString
            row("Date of Birth") = document("ds_dob").AsString
            row("Date Died") = document("ds_dod").AsString
            row("Cause of Death") = document("ds_cod").AsString
            row("Package") = document("ds_package").AsString
            row("Price") = document("ds_price").AsString
            ' ...

            ' Add the DataRow to the DataTable
            dataTable.Rows.Add(row)
        Next

        ' Bind the DataTable to the DataGridView
        ds_dgv.DataSource = dataTable
    End Sub

    Private Sub ds_update_Click(sender As Object, e As EventArgs) Handles ds_update.Click
        Try
            If ds_dgv.SelectedRows.Count > 0 Then
                With update_directServices

                    .ds_appName.Text = ds_dgv.SelectedRows.Item(0).Cells(0).Value
                    .ds_contact.Text = ds_dgv.SelectedRows.Item(0).Cells(1).Value
                    .ds_email.Text = ds_dgv.SelectedRows.Item(0).Cells(2).Value
                    .ds_address.Text = ds_dgv.SelectedRows.Item(0).Cells(3).Value
                    .ds_bankAccount.Text = ds_dgv.SelectedRows.Item(0).Cells(4).Value
                    .ds_deceasedName.Text = ds_dgv.SelectedRows.Item(0).Cells(5).Value
                    '.ds_dob.Value = ds_dgv.SelectedRows.Item(0).Cells(7).Value
                    '.ds_dod.Value = ds_dgv.SelectedRows.Item(0).Cells(8).Value
                    Dim dobCellValue As Object = ds_dgv.SelectedRows.Item(0).Cells(6).Value
                    Dim dob As DateTime
                    If DateTime.TryParseExact(dobCellValue.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dob) Then
                        .ds_dob.Value = dob
                    End If

                    Dim dodCellValue As Object = ds_dgv.SelectedRows.Item(0).Cells(7).Value
                    Dim dod As DateTime
                    If DateTime.TryParseExact(dodCellValue.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dod) Then
                        .ds_dod.Value = dod
                    End If

                    .ds_cod.Text = ds_dgv.SelectedRows.Item(0).Cells(8).Value
                    .ds_package.Text = ds_dgv.SelectedRows.Item(0).Cells(9).Value
                    .ds_price.Text = ds_dgv.SelectedRows.Item(0).Cells(10).Value
                    .ds_id.Text = ds_dgv.SelectedRows.Item(0).Cells(11).Value
                    .ShowDialog()

                End With
            Else
                MsgBox("Please select lifeplan to edit.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ds_archive_Click(sender As Object, e As EventArgs) Handles ds_archive.Click
        ' Check if a row is selected in the DataGridView
        If ds_dgv.SelectedRows.Count > 0 Then
            ' Get the ObjectId value from the selected row
            Dim selectedRow As DataGridViewRow = ds_dgv.SelectedRows(0)
            Dim objectId As String = selectedRow.Cells("ID").Value.ToString()

            ' Move the data from one collection to another based on the ObjectId value
            ArchiveDirectServices(objectId)
        End If
        DirectServicesDVGLoad()
    End Sub
    Private Sub ArchiveDirectServices(objectId As String)
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("direct_services")
        ' Access the MongoDB database and collections
        Dim sourceDatabaseName As String = "elysium-fms-database"
        Dim sourceCollectionName As String = "direct_services"
        Dim targetDatabaseName As String = "elysium-fms-database"
        Dim targetCollectionName As String = "direct_services_archived"
        Dim sourceDatabase As IMongoDatabase = client.GetDatabase(sourceDatabaseName)
        Dim targetDatabase As IMongoDatabase = client.GetDatabase(targetDatabaseName)
        Dim sourceCollection As IMongoCollection(Of BsonDocument) = sourceDatabase.GetCollection(Of BsonDocument)(sourceCollectionName)
        Dim targetCollection As IMongoCollection(Of BsonDocument) = targetDatabase.GetCollection(Of BsonDocument)(targetCollectionName)

        ' Create a filter to match the ObjectId
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))

        ' Find the document in the source collection
        Dim document As BsonDocument = sourceCollection.Find(filter).FirstOrDefault()

        ' If the document exists, insert it into the target collection and delete it from the source collection
        If document IsNot Nothing Then
            ' Insert the document into the target collection
            targetCollection.InsertOne(document)

            ' Delete the document from the source collection
            sourceCollection.DeleteOne(filter)
            MessageBox.Show("Data archived successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub ds_archived_data_Click(sender As Object, e As EventArgs) Handles ds_archived_data.Click
        ds_authRFID.Show()
    End Sub

    Private Sub search_ds_TextChanged(sender As Object, e As EventArgs) Handles search_ds.TextChanged
        ' Stop any previous timer
        If searchDelayTimer IsNot Nothing Then
            searchDelayTimer.Stop()
            searchDelayTimer.Dispose()
        End If

        ' Create a new timer
        searchDelayTimer = New Timer()
        searchDelayTimer.Interval = 200 ' Adjust the delay as needed (in milliseconds)
        AddHandler searchDelayTimer.Tick, AddressOf StartSearchDS
        searchDelayTimer.Start()
    End Sub
    Private Sub StartSearchDS(sender As Object, e As EventArgs)
        ' Stop the timer
        searchDelayTimer.Stop()

        ' Perform the search
        PerformSearchDS()
    End Sub
    Private Sub PerformSearchDS()
        ' Get the search keyword from the TextBox
        Dim keyword As String = search_ds.Text
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        ' Access the MongoDB database and collection
        Dim databaseName As String = "elysium-fms-database"
        Dim collectionName As String = "direct_services"
        Dim database As IMongoDatabase = client.GetDatabase(databaseName)
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

        ' Create a filter to match the search keyword
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Regex("ds_appName", New BsonRegularExpression(keyword, "i"))

        ' Retrieve the documents matching the filter
        Dim searchResults As List(Of BsonDocument) = collection.Find(filter).ToList()

        ' Reverse the order of documents to have the most recent data on top
        searchResults.Reverse()

        ' Convert the searchResults to a DataTable
        Dim dataTable As New DataTable()

        ' Disable auto-generating columns
        staff_dgv.AutoGenerateColumns = False

        ' Create a dictionary to map field names to custom column names
        Dim columnNames As New Dictionary(Of String, String)()

        columnNames.Add("ds_appName", "Applicant Full Name")
        columnNames.Add("ds_contact", "Contact No.")
        columnNames.Add("ds_email", "Email")
        columnNames.Add("ds_address", "Address")
        columnNames.Add("ds_bankAccount", "Bank Account No.")
        columnNames.Add("ds_deceasedName", "Deceased Full Name")
        columnNames.Add("ds_dob", "Date of Birth")
        columnNames.Add("ds_dod", "Date Died")
        columnNames.Add("ds_cod", "Cause of Death")
        columnNames.Add("ds_package", "Package")
        columnNames.Add("ds_price", "Price")
        columnNames.Add("_id", "ID")
        ' Add more field name to custom column name mappings as needed

        ' Add columns to the DataTable
        For Each fieldName As String In columnNames.Keys
            If Not dataTable.Columns.Contains(columnNames(fieldName)) Then
                dataTable.Columns.Add(columnNames(fieldName), GetType(String))
            End If
        Next
        ' Add rows to the DataTable
        For Each document As BsonDocument In searchResults
            ' Create a new DataRow
            Dim row As DataRow = dataTable.NewRow()

            ' Set the values for each field (using the custom column names)
            For Each fieldName As String In columnNames.Keys
                Dim columnName As String = columnNames(fieldName)
                If dataTable.Columns.Contains(columnName) Then
                    If fieldName = "_id" Then
                        row(columnName) = document(fieldName).AsObjectId.ToString()
                    Else
                        row(columnName) = document(fieldName).AsString
                    End If
                End If
            Next

            ' Add the DataRow to the DataTable
            dataTable.Rows.Add(row)
        Next

        ' Bind the DataTable to the DataGridView
        ds_dgv.DataSource = dataTable
    End Sub
    'Inventory
    Private Sub inv_add_Click(sender As Object, e As EventArgs) Handles inv_add.Click
        add_inventory.Show()
    End Sub
    Public Sub InventoryDVGLoad()
        ' Create a MongoDB client
        Dim client As MongoClient = New MongoClient(connectionString)

        ' Access the MongoDB database
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")

        ' Access the collection containing your data
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("inventory")

        ' Fetch the data from the collection
        Dim documents As List(Of BsonDocument) = collection.Find(New BsonDocument()).ToList()

        ' Reverse the order of documents to have the most recent data on top
        documents.Reverse()

        ' Create a DataTable to hold the data
        Dim dataTable As DataTable = New DataTable()

        ' Add columns to the DataTable (replace with your own field names)

        dataTable.Columns.Add("Product Name")
        dataTable.Columns.Add("Quantity")
        dataTable.Columns.Add("Availability")
        dataTable.Columns.Add("Date Added")
        dataTable.Columns.Add("ID")
        ' ...

        ' Add rows to the DataTable
        For Each document As BsonDocument In documents
            ' Create a new DataRow
            Dim row As DataRow = dataTable.NewRow()

            ' Convert ObjectId to string
            Dim objectId As ObjectId = document("_id").AsObjectId
            row("Id") = objectId.ToString()

            ' Set the values for each field (replace with your own field names)
            row("Product Name") = document("inv_ProductName").AsString
            row("Quantity") = document("inv_Quantity").AsString
            row("Availability") = document("inv_Availability").AsString
            row("Date Added") = document("inv_DateAdded").AsString
            ' ...

            ' Add the DataRow to the DataTable
            dataTable.Rows.Add(row)
        Next

        ' Bind the DataTable to the DataGridView
        inventory_dgv.DataSource = dataTable
    End Sub

    Private Sub inv_update_Click(sender As Object, e As EventArgs) Handles inv_update.Click
        Try
            If inventory_dgv.SelectedRows.Count > 0 Then
                With update_inventory

                    .inv_ProductName.Text = inventory_dgv.SelectedRows.Item(0).Cells(0).Value
                    .inv_Quantity.Text = inventory_dgv.SelectedRows.Item(0).Cells(1).Value
                    .inv_Availability.Text = inventory_dgv.SelectedRows.Item(0).Cells(2).Value
                    Dim dateAddedCellValue As Object = inventory_dgv.SelectedRows.Item(0).Cells(3).Value
                    Dim dateAdd As DateTime
                    If DateTime.TryParseExact(dateAddedCellValue.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dateAdd) Then
                        .inv_DateAdded.Value = dateAdd
                    End If
                    .inv_id.Text = inventory_dgv.SelectedRows.Item(0).Cells(4).Value
                    .ShowDialog()

                End With
            Else
                MsgBox("Please select lifeplan to edit.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub inv_archive_Click(sender As Object, e As EventArgs) Handles inv_archive.Click
        ' Check if a row is selected in the DataGridView
        If inventory_dgv.SelectedRows.Count > 0 Then
            ' Get the ObjectId value from the selected row
            Dim selectedRow As DataGridViewRow = inventory_dgv.SelectedRows(0)
            Dim objectId As String = selectedRow.Cells("ID").Value.ToString()

            ' Move the data from one collection to another based on the ObjectId value
            ArchiveInventory(objectId)
        End If
        InventoryDVGLoad()
    End Sub
    Private Sub ArchiveInventory(objectId As String)
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("inventory")
        ' Access the MongoDB database and collections
        Dim sourceDatabaseName As String = "elysium-fms-database"
        Dim sourceCollectionName As String = "inventory"
        Dim targetDatabaseName As String = "elysium-fms-database"
        Dim targetCollectionName As String = "inventory_archive"
        Dim sourceDatabase As IMongoDatabase = client.GetDatabase(sourceDatabaseName)
        Dim targetDatabase As IMongoDatabase = client.GetDatabase(targetDatabaseName)
        Dim sourceCollection As IMongoCollection(Of BsonDocument) = sourceDatabase.GetCollection(Of BsonDocument)(sourceCollectionName)
        Dim targetCollection As IMongoCollection(Of BsonDocument) = targetDatabase.GetCollection(Of BsonDocument)(targetCollectionName)

        ' Create a filter to match the ObjectId
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))

        ' Find the document in the source collection
        Dim document As BsonDocument = sourceCollection.Find(filter).FirstOrDefault()

        ' If the document exists, insert it into the target collection and delete it from the source collection
        If document IsNot Nothing Then
            ' Insert the document into the target collection
            targetCollection.InsertOne(document)

            ' Delete the document from the source collection
            sourceCollection.DeleteOne(filter)
            MessageBox.Show("Data archived successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub inv_archived_data_Click(sender As Object, e As EventArgs) Handles inv_archived_data.Click
        inventory_authRFID.Show()
    End Sub

    Private Sub search_inventory_TextChanged(sender As Object, e As EventArgs) Handles search_inventory.TextChanged
        ' Stop any previous timer
        If searchDelayTimer IsNot Nothing Then
            searchDelayTimer.Stop()
            searchDelayTimer.Dispose()
        End If

        ' Create a new timer
        searchDelayTimer = New Timer()
        searchDelayTimer.Interval = 200 ' Adjust the delay as needed (in milliseconds)
        AddHandler searchDelayTimer.Tick, AddressOf StartSearchINV
        searchDelayTimer.Start()
    End Sub
    Private Sub StartSearchINV(sender As Object, e As EventArgs)
        ' Stop the timer
        searchDelayTimer.Stop()

        ' Perform the search
        PerformSearchINV()
    End Sub
    Private Sub PerformSearchINV()
        ' Get the search keyword from the TextBox
        Dim keyword As String = search_inventory.Text
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        ' Access the MongoDB database and collection
        Dim databaseName As String = "elysium-fms-database"
        Dim collectionName As String = "inventory"
        Dim database As IMongoDatabase = client.GetDatabase(databaseName)
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

        ' Create a filter to match the search keyword
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Regex("inv_ProductName", New BsonRegularExpression(keyword, "i"))

        ' Retrieve the documents matching the filter
        Dim searchResults As List(Of BsonDocument) = collection.Find(filter).ToList()

        ' Reverse the order of documents to have the most recent data on top
        searchResults.Reverse()

        ' Convert the searchResults to a DataTable
        Dim dataTable As New DataTable()

        ' Disable auto-generating columns
        inventory_dgv.AutoGenerateColumns = False

        ' Create a dictionary to map field names to custom column names
        Dim columnNames As New Dictionary(Of String, String)()

        columnNames.Add("inv_ProductName", "Product Name")
        columnNames.Add("inv_Quantity", "Quantity")
        columnNames.Add("inv_Availability", "Availability")
        columnNames.Add("inv_DateAdded", "Date Added")
        columnNames.Add("_id", "ID")
        ' Add more field name to custom column name mappings as needed

        ' Add columns to the DataTable
        For Each fieldName As String In columnNames.Keys
            If Not dataTable.Columns.Contains(columnNames(fieldName)) Then
                dataTable.Columns.Add(columnNames(fieldName), GetType(String))
            End If
        Next
        ' Add rows to the DataTable
        For Each document As BsonDocument In searchResults
            ' Create a new DataRow
            Dim row As DataRow = dataTable.NewRow()

            ' Set the values for each field (using the custom column names)
            For Each fieldName As String In columnNames.Keys
                Dim columnName As String = columnNames(fieldName)
                If dataTable.Columns.Contains(columnName) Then
                    If fieldName = "_id" Then
                        row(columnName) = document(fieldName).AsObjectId.ToString()
                    Else
                        row(columnName) = document(fieldName).AsString
                    End If
                End If
            Next

            ' Add the DataRow to the DataTable
            dataTable.Rows.Add(row)
        Next

        ' Bind the DataTable to the DataGridView
        inventory_dgv.DataSource = dataTable
    End Sub

    Private Sub appt_messagebtn_Click(sender As Object, e As EventArgs) Handles appt_messagebtn.Click
        Try
            If appointment_dgv.SelectedRows.Count > 0 Then
                With notifier_appointment
                    .notifier_number.Text = appointment_dgv.SelectedRows.Item(0).Cells(2).Value
                    .notifier_message.Text = "Good day, " + appointment_dgv.SelectedRows.Item(0).Cells(0).Value + "! Your appointment request has been " + appointment_dgv.SelectedRows.Item(0).Cells(6).Value + ". Your appointment date will be on " + appointment_dgv.SelectedRows.Item(0).Cells(4).Value + ", " + appointment_dgv.SelectedRows.Item(0).Cells(5).Value + "." + Environment.NewLine + Environment.NewLine + "Thank you for choosing Elysium!"
                    .ShowDialog()
                End With
            Else
                MessageBox.Show("Please select appointment request to notify.", "MESSAGE NOTIFER:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub msg_welcome_Click(sender As Object, e As EventArgs) Handles msg_welcome.Click
        Try
            If lifeplan_dgv.SelectedRows.Count > 0 Then
                With notifier_welcomeSMS
                    .notifier_number.Text = lifeplan_dgv.SelectedRows.Item(0).Cells(2).Value
                    'Note: message characters were not fully finalized
                    .notifier_message.Text = "Good day, " + lifeplan_dgv.SelectedRows.Item(0).Cells(0).Value + "! You've successfully applied for the " + lifeplan_dgv.SelectedRows.Item(0).Cells(5).Value + " Life Plan package. Your transaction ticket is " + lifeplan_dgv.SelectedRows.Item(0).Cells(12).Value + Environment.NewLine + Environment.NewLine + "Track your plan using the Elysium App. Thank you for choosing Elysium!"
                    .ShowDialog()
                End With
            Else
                MessageBox.Show("Please select booking to notify.", "MESSAGE NOTIFER:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub msg_duedate_Click(sender As Object, e As EventArgs) Handles msg_duedate.Click
        Try
            If lifeplan_dgv.SelectedRows.Count > 0 Then
                With notifier_welcomeSMS
                    .notifier_number.Text = lifeplan_dgv.SelectedRows.Item(0).Cells(2).Value
                    'Note: message characters were not fully finalized
                    .notifier_message.Text = "Good day, " + lifeplan_dgv.SelectedRows.Item(0).Cells(0).Value + "! This message was a payment due notifier for you to pay for your plan. You can track your plan using your transaction ticket: " + lifeplan_dgv.SelectedRows.Item(0).Cells(12).Value + " in the Elysium App." + Environment.NewLine + Environment.NewLine + "Payments will reflect after 3-5 business days. Thank you for choosing Elysium!"
                    .ShowDialog()
                End With
            Else
                MessageBox.Show("Please select booking to notify.", "MESSAGE NOTIFER:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub refresh_apptBTN_Click(sender As Object, e As EventArgs) Handles refresh_apptBTN.Click
        ApptDVGLoad()
        DisplayTotalAppointment()

    End Sub
    'admin profile user and pass hiding
    Private Sub EC_user_Click(sender As Object, e As EventArgs) Handles EC_user.Click
        EC_user.Visible = False
        EO_user.Visible = True
        admin_username1.PasswordChar = ""
    End Sub

    Private Sub EO_user_Click(sender As Object, e As EventArgs) Handles EO_user.Click
        EC_user.Visible = True
        EO_user.Visible = False
        admin_username1.PasswordChar = "*"
    End Sub

    Private Sub EC_pass_Click(sender As Object, e As EventArgs) Handles EC_pass.Click
        EC_pass.Visible = False
        EO_pass.Visible = True
        admin_pwd1.PasswordChar = ""
    End Sub

    Private Sub EO_pass_Click(sender As Object, e As EventArgs) Handles EO_pass.Click
        EC_pass.Visible = True
        EO_pass.Visible = False
        admin_pwd1.PasswordChar = "*"
    End Sub
End Class