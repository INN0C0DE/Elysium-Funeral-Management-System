Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class archived_form
    Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
    Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
    Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("staff_account_archive")
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
        Dim collectionName As String = "staff_account_archive"
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


    Private Sub archived_form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StaffDVGLoad()
        ApptDVGLoad()
        LifeplanDVGLoad()
        DirectServicesDVGLoad()
        InventoryDVGLoad()
    End Sub
    Public Sub StaffDVGLoad()
        ' MongoDB connection string
        Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"

        ' Create a MongoDB client
        Dim client As MongoClient = New MongoClient(connectionString)

        ' Access the MongoDB database
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")

        ' Access the collection containing your data
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("staff_account_archive")

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

    Private Sub restore_staff_Click(sender As Object, e As EventArgs) Handles restore_staff.Click
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
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("staff_account_archive")
        ' Access the MongoDB database and collections
        Dim sourceDatabaseName As String = "elysium-fms-database"
        Dim sourceCollectionName As String = "staff_account_archive"
        Dim targetDatabaseName As String = "elysium-fms-database"
        Dim targetCollectionName As String = "staff_account"
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
            MessageBox.Show("Data restored successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            admin_dashboard.StaffDVGLoad()

        End If
    End Sub

    Private Sub delete_staff_Click(sender As Object, e As EventArgs) Handles delete_staff.Click
        If staff_dgv.SelectedRows.Count > 0 Then
            ' Get the ObjectId value from the selected row
            Dim selectedRow As DataGridViewRow = staff_dgv.SelectedRows(0)
            Dim objectId As String = selectedRow.Cells("Id").Value.ToString()

            ' Delete the data from MongoDB based on the ObjectId value
            DeleteData(objectId)
        End If
    End Sub
    Private Sub DeleteData(objectId As String)
        ' Access the MongoDB database and collection
        Dim databaseName As String = "elysium-fms-database"
        Dim collectionName As String = "staff_account_archive"
        Dim database As IMongoDatabase = client.GetDatabase(databaseName)
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

        ' Create a filter to match the ObjectId
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))

        ' Delete the document matching the filter
        collection.DeleteOne(filter)
        MessageBox.Show("Data deleted permanently!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
        StaffDVGLoad()

    End Sub
    Public Sub ApptDVGLoad()
        ' MongoDB connection string
        Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"

        ' Create a MongoDB client
        Dim client As MongoClient = New MongoClient(connectionString)

        ' Access the MongoDB database
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")

        ' Access the collection containing your data
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("appointments_archive")

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

        ' Bind the DataTable to the DataGridView
        appointment_dgv.DataSource = dataTable
    End Sub

    Private Sub restore_appointment_Click(sender As Object, e As EventArgs) Handles restore_appointment.Click
        ' Check if a row is selected in the DataGridView
        If appointment_dgv.SelectedRows.Count > 0 Then
            ' Get the ObjectId value from the selected row
            Dim selectedRow As DataGridViewRow = appointment_dgv.SelectedRows(0)
            Dim objectId As String = selectedRow.Cells("ID").Value.ToString()

            ' Move the data from one collection to another based on the ObjectId value
            MoveDataAppointment(objectId)
        End If
        ApptDVGLoad()
    End Sub

    Private Sub MoveDataAppointment(objectId As String)
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("appointments_archive")
        ' Access the MongoDB database and collections
        Dim sourceDatabaseName As String = "elysium-fms-database"
        Dim sourceCollectionName As String = "appointments_archive"
        Dim targetDatabaseName As String = "elysium-fms-database"
        Dim targetCollectionName As String = "appointments"
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
            MessageBox.Show("Data restored successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            admin_dashboard.ApptDVGLoad()
            admin_dashboard.DisplayTotalAppointment()

        End If
    End Sub

    Private Sub delete_apptBTN_Click(sender As Object, e As EventArgs) Handles delete_apptBTN.Click
        If appointment_dgv.SelectedRows.Count > 0 Then
            ' Get the ObjectId value from the selected row
            Dim selectedRow As DataGridViewRow = appointment_dgv.SelectedRows(0)
            Dim objectId As String = selectedRow.Cells("Id").Value.ToString()

            ' Delete the data from MongoDB based on the ObjectId value
            DeleteDataAppt(objectId)
        End If
    End Sub
    Private Sub DeleteDataAppt(objectId As String)
        ' Access the MongoDB database and collection
        Dim databaseName As String = "elysium-fms-database"
        Dim collectionName As String = "appointments_archive"
        Dim database As IMongoDatabase = client.GetDatabase(databaseName)
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

        ' Create a filter to match the ObjectId
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))

        ' Delete the document matching the filter
        collection.DeleteOne(filter)
        MessageBox.Show("Data deleted permanently!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ApptDVGLoad()

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
        Dim collectionName As String = "appointments_archive"
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

    'Life Plan Archive
    Public Sub LifeplanDVGLoad()
        '' Create a MongoDB client
        Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"

        ' Create a MongoDB client
        Dim client As MongoClient = New MongoClient(connectionString)

        ' Access the MongoDB database
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")

        ' Access the collection containing your data
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("lifeplan_archive")

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

    Private Sub restore_lifeplan_Click(sender As Object, e As EventArgs) Handles restore_lifeplan.Click
        ' Check if a row is selected in the DataGridView
        If lifeplan_dgv.SelectedRows.Count > 0 Then
            ' Get the ObjectId value from the selected row
            Dim selectedRow As DataGridViewRow = lifeplan_dgv.SelectedRows(0)
            Dim objectId As String = selectedRow.Cells("ID").Value.ToString()

            ' Move the data from one collection to another based on the ObjectId value
            MoveDataLifeplan(objectId)
        End If
        LifeplanDVGLoad()
    End Sub
    Private Sub MoveDataLifeplan(objectId As String)
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("lifeplan_archive")
        ' Access the MongoDB database and collections
        Dim sourceDatabaseName As String = "elysium-fms-database"
        Dim sourceCollectionName As String = "lifeplan_archive"
        Dim targetDatabaseName As String = "elysium-fms-database"
        Dim targetCollectionName As String = "lifeplan"
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
            MessageBox.Show("Data restored successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            admin_dashboard.LifeplanDVGLoad()
            admin_dashboard.DisplayTotalLifeplan()

        End If
    End Sub

    Private Sub delete_lifeplan_Click(sender As Object, e As EventArgs) Handles delete_lifeplan.Click
        If lifeplan_dgv.SelectedRows.Count > 0 Then
            ' Get the ObjectId value from the selected row
            Dim selectedRow As DataGridViewRow = lifeplan_dgv.SelectedRows(0)
            Dim objectId As String = selectedRow.Cells("Id").Value.ToString()

            ' Delete the data from MongoDB based on the ObjectId value
            DeleteDataLifeplan(objectId)
        End If
    End Sub
    Private Sub DeleteDataLifeplan(objectId As String)
        ' Access the MongoDB database and collection
        Dim databaseName As String = "elysium-fms-database"
        Dim collectionName As String = "lifeplan_archive"
        Dim database As IMongoDatabase = client.GetDatabase(databaseName)
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

        ' Create a filter to match the ObjectId
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))

        ' Delete the document matching the filter
        collection.DeleteOne(filter)
        MessageBox.Show("Data deleted permanently!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
        LifeplanDVGLoad()

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
        Dim collectionName As String = "lifeplan_archive"
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

    Public Sub DirectServicesDVGLoad()
        Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"
        ' Create a MongoDB client
        Dim client As MongoClient = New MongoClient(connectionString)

        ' Access the MongoDB database
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")

        ' Access the collection containing your data
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("direct_services_archived")

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

    Private Sub ds_restoreBTN_Click(sender As Object, e As EventArgs) Handles ds_restoreBTN.Click
        ' Check if a row is selected in the DataGridView
        If ds_dgv.SelectedRows.Count > 0 Then
            ' Get the ObjectId value from the selected row
            Dim selectedRow As DataGridViewRow = ds_dgv.SelectedRows(0)
            Dim objectId As String = selectedRow.Cells("ID").Value.ToString()

            ' Move the data from one collection to another based on the ObjectId value
            MoveDataDS(objectId)
        End If
        DirectServicesDVGLoad()
    End Sub
    Private Sub MoveDataDS(objectId As String)
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("direct_services_archived")
        ' Access the MongoDB database and collections
        Dim sourceDatabaseName As String = "elysium-fms-database"
        Dim sourceCollectionName As String = "direct_services_archived"
        Dim targetDatabaseName As String = "elysium-fms-database"
        Dim targetCollectionName As String = "direct_services"
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
            MessageBox.Show("Data restored successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            admin_dashboard.DirectServicesDVGLoad()

        End If
    End Sub

    Private Sub ds_deleteBTN_Click(sender As Object, e As EventArgs) Handles ds_deleteBTN.Click
        If ds_dgv.SelectedRows.Count > 0 Then
            ' Get the ObjectId value from the selected row
            Dim selectedRow As DataGridViewRow = ds_dgv.SelectedRows(0)
            Dim objectId As String = selectedRow.Cells("Id").Value.ToString()

            ' Delete the data from MongoDB based on the ObjectId value
            DeleteDataDS(objectId)
        End If
    End Sub
    Private Sub DeleteDataDS(objectId As String)
        ' Access the MongoDB database and collection
        Dim databaseName As String = "elysium-fms-database"
        Dim collectionName As String = "direct_services_archived"
        Dim database As IMongoDatabase = client.GetDatabase(databaseName)
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

        ' Create a filter to match the ObjectId
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))

        ' Delete the document matching the filter
        collection.DeleteOne(filter)
        MessageBox.Show("Data deleted permanently!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
        DirectServicesDVGLoad()

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
        Dim collectionName As String = "direct_services_archived"
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

    Private Sub inv_restore_Click(sender As Object, e As EventArgs) Handles inv_restore.Click
        ' Check if a row is selected in the DataGridView
        If inventory_dgv.SelectedRows.Count > 0 Then
            ' Get the ObjectId value from the selected row
            Dim selectedRow As DataGridViewRow = inventory_dgv.SelectedRows(0)
            Dim objectId As String = selectedRow.Cells("ID").Value.ToString()

            ' Move the data from one collection to another based on the ObjectId value
            MoveDataINV(objectId)
        End If
        InventoryDVGLoad()
    End Sub
    Private Sub MoveDataINV(objectId As String)
        Dim client As MongoClient = New MongoClient("mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("inventory_archive")
        ' Access the MongoDB database and collections
        Dim sourceDatabaseName As String = "elysium-fms-database"
        Dim sourceCollectionName As String = "inventory_archive"
        Dim targetDatabaseName As String = "elysium-fms-database"
        Dim targetCollectionName As String = "inventory"
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
            MessageBox.Show("Data restored successfully!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            admin_dashboard.InventoryDVGLoad()

        End If
    End Sub
    Public Sub InventoryDVGLoad()
        Dim connectionString As String = "mongodb+srv://trickted2:123@cluster0.bss9bgz.mongodb.net/?retryWrites=true&w=majority"
        ' Create a MongoDB client
        Dim client As MongoClient = New MongoClient(connectionString)

        ' Access the MongoDB database
        Dim database As IMongoDatabase = client.GetDatabase("elysium-fms-database")

        ' Access the collection containing your data
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("inventory_archive")

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

    Private Sub inv_delete_Click(sender As Object, e As EventArgs) Handles inv_delete.Click
        If inventory_dgv.SelectedRows.Count > 0 Then
            ' Get the ObjectId value from the selected row
            Dim selectedRow As DataGridViewRow = inventory_dgv.SelectedRows(0)
            Dim objectId As String = selectedRow.Cells("Id").Value.ToString()

            ' Delete the data from MongoDB based on the ObjectId value
            DeleteDataINV(objectId)
        End If
    End Sub
    Private Sub DeleteDataINV(objectId As String)
        ' Access the MongoDB database and collection
        Dim databaseName As String = "elysium-fms-database"
        Dim collectionName As String = "inventory_archive"
        Dim database As IMongoDatabase = client.GetDatabase(databaseName)
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

        ' Create a filter to match the ObjectId
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", New ObjectId(objectId))

        ' Delete the document matching the filter
        collection.DeleteOne(filter)
        MessageBox.Show("Data deleted permanently!", "ELYSIUM FMS:", MessageBoxButtons.OK, MessageBoxIcon.Information)
        InventoryDVGLoad()

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
        Dim collectionName As String = "inventory_archive"
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
End Class