<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        TextBox1 = New TextBox()
        TextBox2 = New TextBox()
        Button1 = New Button()
        Label1 = New Label()
        Label2 = New Label()
        DataGridView1 = New DataGridView()
        Label3 = New Label()
        TextBox3 = New TextBox()
        Label4 = New Label()
        RadioButton1 = New RadioButton()
        RadioButton2 = New RadioButton()
        ComboBox1 = New ComboBox()
        Label5 = New Label()
        Button2 = New Button()
        DataGridView2 = New DataGridView()
        Label6 = New Label()
        TextBox4 = New TextBox()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridView2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(217, 39)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(304, 31)
        TextBox1.TabIndex = 0
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(217, 311)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(304, 31)
        TextBox2.TabIndex = 1
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(577, 39)
        Button1.Name = "Button1"
        Button1.Size = New Size(373, 49)
        Button1.TabIndex = 2
        Button1.Text = "BELI"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(60, 39)
        Label1.Name = "Label1"
        Label1.Size = New Size(110, 25)
        Label1.TabIndex = 3
        Label1.Text = "ID Minuman"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(71, 311)
        Label2.Name = "Label2"
        Label2.Size = New Size(99, 25)
        Label2.TabIndex = 4
        Label2.Text = "Jumlah Beli"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(30, 502)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 62
        DataGridView1.Size = New Size(920, 225)
        DataGridView1.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(38, 91)
        Label3.Name = "Label3"
        Label3.Size = New Size(132, 25)
        Label3.TabIndex = 6
        Label3.Text = "Merk Minuman"
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(217, 91)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(304, 31)
        TextBox3.TabIndex = 7
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(30, 142)
        Label4.Name = "Label4"
        Label4.Size = New Size(140, 25)
        Label4.TabIndex = 8
        Label4.Text = "Varian Minuman"
        ' 
        ' RadioButton1
        ' 
        RadioButton1.AutoSize = True
        RadioButton1.Location = New Point(217, 142)
        RadioButton1.Name = "RadioButton1"
        RadioButton1.Size = New Size(79, 29)
        RadioButton1.TabIndex = 9
        RadioButton1.TabStop = True
        RadioButton1.Text = "Botol"
        RadioButton1.UseVisualStyleBackColor = True
        ' 
        ' RadioButton2
        ' 
        RadioButton2.AutoSize = True
        RadioButton2.Location = New Point(359, 142)
        RadioButton2.Name = "RadioButton2"
        RadioButton2.Size = New Size(90, 29)
        RadioButton2.TabIndex = 10
        RadioButton2.TabStop = True
        RadioButton2.Text = "Kaleng"
        RadioButton2.UseVisualStyleBackColor = True
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"Kecil", "Sedang", "Besar"})
        ComboBox1.Location = New Point(217, 199)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(304, 33)
        ComboBox1.TabIndex = 11
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(22, 199)
        Label5.Name = "Label5"
        Label5.Size = New Size(148, 25)
        Label5.TabIndex = 12
        Label5.Text = "Ukuran Minuman"
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(577, 104)
        Button2.Name = "Button2"
        Button2.Size = New Size(373, 41)
        Button2.TabIndex = 13
        Button2.Text = "RESET"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' DataGridView2
        ' 
        DataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView2.Location = New Point(217, 253)
        DataGridView2.Name = "DataGridView2"
        DataGridView2.RowHeadersWidth = 62
        DataGridView2.Size = New Size(304, 36)
        DataGridView2.TabIndex = 14
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(110, 253)
        Label6.Name = "Label6"
        Label6.Size = New Size(60, 25)
        Label6.TabIndex = 15
        Label6.Text = "Harga"
        ' 
        ' TextBox4
        ' 
        TextBox4.Font = New Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        TextBox4.Location = New Point(577, 163)
        TextBox4.Multiline = True
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(373, 46)
        TextBox4.TabIndex = 16
        TextBox4.TextAlign = HorizontalAlignment.Center
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(987, 751)
        Controls.Add(TextBox4)
        Controls.Add(Label6)
        Controls.Add(DataGridView2)
        Controls.Add(Button2)
        Controls.Add(Label5)
        Controls.Add(ComboBox1)
        Controls.Add(RadioButton2)
        Controls.Add(RadioButton1)
        Controls.Add(Label4)
        Controls.Add(TextBox3)
        Controls.Add(Label3)
        Controls.Add(DataGridView1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(Button1)
        Controls.Add(TextBox2)
        Controls.Add(TextBox1)
        Name = "Form1"
        Text = "Form1"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridView2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBox4 As TextBox

End Class
