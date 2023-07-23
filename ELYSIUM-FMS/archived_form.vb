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

        ' Convert the searchResults to a DataTable
        Dim dataTable As New DataTable()

        ' Disable auto-generating columns
        staff_dgv.AutoGenerateColumns = False

        ' Create a dictionary to map field names to custom column names
        Dim columnNames As New Dictionary(Of String, String)()
        columnNames.Add("_id", "ID")
        columnNames.Add("fullname", "Full Name")
        columnNames.Add("username", "Username")
        columnNames.Add("password", "Password")
        columnNames.Add("rfid", "RFID No.")
        columnNames.Add("gender", "Gender")
        columnNames.Add("age", "Age")
        columnNames.Add("email", "Email")
        columnNames.Add("number", "Number")
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

        ' Create a DataTable to hold the data
        Dim dataTable As DataTable = New DataTable()

        ' Add columns to the DataTable (replace with your own field names)
        dataTable.Columns.Add("ID")
        dataTable.Columns.Add("Full Name")
        dataTable.Columns.Add("Username")
        dataTable.Columns.Add("Password")
        dataTable.Columns.Add("RFID No.")
        dataTable.Columns.Add("Gender")
        dataTable.Columns.Add("Age")
        dataTable.Columns.Add("Email")
        dataTable.Columns.Add("Number")
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

        ' Create a DataTable to hold the data
        Dim dataTable As DataTable = New DataTable()

        ' Add columns to the DataTable (replace with your own field names)
        dataTable.Columns.Add("ID")
        dataTable.Columns.Add("Full Name")
        dataTable.Columns.Add("Email")
        dataTable.Columns.Add("Number")
        dataTable.Columns.Add("Address")
        dataTable.Columns.Add("Appointment Date")
        dataTable.Columns.Add("Appointment Time")
        dataTable.Columns.Add("Appointment Status")
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

        ' Convert the searchResults to a DataTable
        Dim dataTable As New DataTable()

        ' Disable auto-generating columns
        staff_dgv.AutoGenerateColumns = False

        ' Create a dictionary to map field names to custom column names
        Dim columnNames As New Dictionary(Of String, String)()
        columnNames.Add("_id", "ID")
        columnNames.Add("fullname", "Full Name")
        columnNames.Add("email", "Email")
        columnNames.Add("number", "Number")
        columnNames.Add("address", "Address")
        columnNames.Add("apptDate", "Appointment Date")
        columnNames.Add("apptTime", "Appointment Time")
        columnNames.Add("apptStatus", "Appointment Status")
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
End Class