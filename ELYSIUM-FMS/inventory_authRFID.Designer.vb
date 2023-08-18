<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class inventory_authRFID
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(inventory_authRFID))
        Dim StateProperties1 As Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties = New Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties()
        Dim StateProperties2 As Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties = New Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties()
        Dim StateProperties3 As Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties = New Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties()
        Dim StateProperties4 As Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties = New Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties()
        Me.BunifuLabel1 = New Bunifu.UI.WinForms.BunifuLabel()
        Me.rfid_admintb = New Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BunifuLabel1
        '
        Me.BunifuLabel1.AutoEllipsis = False
        Me.BunifuLabel1.CursorType = Nothing
        Me.BunifuLabel1.Font = New System.Drawing.Font("Poppins", 15.75!, System.Drawing.FontStyle.Bold)
        Me.BunifuLabel1.ForeColor = System.Drawing.Color.SaddleBrown
        Me.BunifuLabel1.Location = New System.Drawing.Point(105, 245)
        Me.BunifuLabel1.Name = "BunifuLabel1"
        Me.BunifuLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BunifuLabel1.Size = New System.Drawing.Size(190, 39)
        Me.BunifuLabel1.TabIndex = 15
        Me.BunifuLabel1.Text = "TAP RFID TO SCAN"
        Me.BunifuLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.BunifuLabel1.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.[Default]
        '
        'rfid_admintb
        '
        Me.rfid_admintb.AcceptsReturn = False
        Me.rfid_admintb.AcceptsTab = False
        Me.rfid_admintb.AnimationSpeed = 200
        Me.rfid_admintb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.rfid_admintb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.rfid_admintb.BackColor = System.Drawing.Color.Transparent
        Me.rfid_admintb.BackgroundImage = CType(resources.GetObject("rfid_admintb.BackgroundImage"), System.Drawing.Image)
        Me.rfid_admintb.BorderColorActive = System.Drawing.Color.DodgerBlue
        Me.rfid_admintb.BorderColorDisabled = System.Drawing.Color.FromArgb(CType(CType(161, Byte), Integer), CType(CType(161, Byte), Integer), CType(CType(161, Byte), Integer))
        Me.rfid_admintb.BorderColorHover = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rfid_admintb.BorderColorIdle = System.Drawing.Color.Silver
        Me.rfid_admintb.BorderRadius = 1
        Me.rfid_admintb.BorderThickness = 1
        Me.rfid_admintb.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.rfid_admintb.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.rfid_admintb.DefaultFont = New System.Drawing.Font("Segoe UI Semibold", 9.75!)
        Me.rfid_admintb.DefaultText = ""
        Me.rfid_admintb.FillColor = System.Drawing.Color.White
        Me.rfid_admintb.HideSelection = True
        Me.rfid_admintb.IconLeft = Nothing
        Me.rfid_admintb.IconLeftCursor = System.Windows.Forms.Cursors.IBeam
        Me.rfid_admintb.IconPadding = 10
        Me.rfid_admintb.IconRight = Nothing
        Me.rfid_admintb.IconRightCursor = System.Windows.Forms.Cursors.IBeam
        Me.rfid_admintb.Lines = New String(-1) {}
        Me.rfid_admintb.Location = New System.Drawing.Point(462, 21)
        Me.rfid_admintb.MaxLength = 32767
        Me.rfid_admintb.MinimumSize = New System.Drawing.Size(100, 35)
        Me.rfid_admintb.Modified = False
        Me.rfid_admintb.Multiline = False
        Me.rfid_admintb.Name = "rfid_admintb"
        StateProperties1.BorderColor = System.Drawing.Color.DodgerBlue
        StateProperties1.FillColor = System.Drawing.Color.Empty
        StateProperties1.ForeColor = System.Drawing.Color.Empty
        StateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty
        Me.rfid_admintb.OnActiveState = StateProperties1
        StateProperties2.BorderColor = System.Drawing.Color.Empty
        StateProperties2.FillColor = System.Drawing.Color.White
        StateProperties2.ForeColor = System.Drawing.Color.Empty
        StateProperties2.PlaceholderForeColor = System.Drawing.Color.Silver
        Me.rfid_admintb.OnDisabledState = StateProperties2
        StateProperties3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer))
        StateProperties3.FillColor = System.Drawing.Color.Empty
        StateProperties3.ForeColor = System.Drawing.Color.Empty
        StateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty
        Me.rfid_admintb.OnHoverState = StateProperties3
        StateProperties4.BorderColor = System.Drawing.Color.Silver
        StateProperties4.FillColor = System.Drawing.Color.White
        StateProperties4.ForeColor = System.Drawing.Color.Empty
        StateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty
        Me.rfid_admintb.OnIdleState = StateProperties4
        Me.rfid_admintb.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.rfid_admintb.PlaceholderForeColor = System.Drawing.Color.Silver
        Me.rfid_admintb.PlaceholderText = ""
        Me.rfid_admintb.ReadOnly = False
        Me.rfid_admintb.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.rfid_admintb.SelectedText = ""
        Me.rfid_admintb.SelectionLength = 0
        Me.rfid_admintb.SelectionStart = 0
        Me.rfid_admintb.ShortcutsEnabled = True
        Me.rfid_admintb.Size = New System.Drawing.Size(200, 35)
        Me.rfid_admintb.Style = Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox._Style.Bunifu
        Me.rfid_admintb.TabIndex = 14
        Me.rfid_admintb.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.rfid_admintb.TextMarginBottom = 0
        Me.rfid_admintb.TextMarginLeft = 5
        Me.rfid_admintb.TextMarginTop = 0
        Me.rfid_admintb.TextPlaceholder = ""
        Me.rfid_admintb.UseSystemPasswordChar = False
        Me.rfid_admintb.WordWrap = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ELYSIUM_FMS.My.Resources.Resources.rfid_scan
        Me.PictureBox1.Location = New System.Drawing.Point(-56, -30)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(525, 300)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'inventory_authRFID
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Wheat
        Me.ClientSize = New System.Drawing.Size(401, 276)
        Me.Controls.Add(Me.BunifuLabel1)
        Me.Controls.Add(Me.rfid_admintb)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "inventory_authRFID"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ELYSIUM FMS | Admin Authentication"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BunifuLabel1 As Bunifu.UI.WinForms.BunifuLabel
    Friend WithEvents rfid_admintb As Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox
    Friend WithEvents PictureBox1 As PictureBox
End Class
