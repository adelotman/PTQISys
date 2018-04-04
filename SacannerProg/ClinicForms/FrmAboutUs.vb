Public Class FrmAbout
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Lb1 As System.Windows.Forms.Label
    Friend WithEvents Lb2 As System.Windows.Forms.Label
    Friend WithEvents Lb3 As System.Windows.Forms.Label
    Friend WithEvents Lb4 As System.Windows.Forms.Label
    Friend WithEvents Lb5 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAbout))
        Me.Lb1 = New System.Windows.Forms.Label
        Me.Lb2 = New System.Windows.Forms.Label
        Me.Lb3 = New System.Windows.Forms.Label
        Me.Lb4 = New System.Windows.Forms.Label
        Me.Lb5 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.Button1 = New System.Windows.Forms.Button
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Lb1
        '
        Me.Lb1.AutoSize = True
        Me.Lb1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Lb1.Location = New System.Drawing.Point(320, 40)
        Me.Lb1.Name = "Lb1"
        Me.Lb1.Size = New System.Drawing.Size(129, 13)
        Me.Lb1.TabIndex = 0
        Me.Lb1.Text = "„‰ÿÊ„… «Œ Ì«— «· Œ’’"
        '
        'Lb2
        '
        Me.Lb2.AutoSize = True
        Me.Lb2.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Lb2.Location = New System.Drawing.Point(360, 72)
        Me.Lb2.Name = "Lb2"
        Me.Lb2.Size = New System.Drawing.Size(62, 13)
        Me.Lb2.TabIndex = 1
        Me.Lb2.Text = "„⁄Âœ «·‰›ÿ"
        '
        'Lb3
        '
        Me.Lb3.AutoSize = True
        Me.Lb3.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Lb3.Location = New System.Drawing.Point(296, 104)
        Me.Lb3.Name = "Lb3"
        Me.Lb3.Size = New System.Drawing.Size(163, 13)
        Me.Lb3.TabIndex = 2
        Me.Lb3.Text = "„Â‰œ”/Œ«·œ ⁄»œ«·Õ„Ìœ «·‘»·"
        '
        'Lb4
        '
        Me.Lb4.AutoSize = True
        Me.Lb4.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Lb4.Location = New System.Drawing.Point(352, 136)
        Me.Lb4.Name = "Lb4"
        Me.Lb4.Size = New System.Drawing.Size(50, 13)
        Me.Lb4.TabIndex = 3
        Me.Lb4.Text = "E-Mail : "
        '
        'Lb5
        '
        Me.Lb5.AutoSize = True
        Me.Lb5.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Lb5.Location = New System.Drawing.Point(352, 168)
        Me.Lb5.Name = "Lb5"
        Me.Lb5.Size = New System.Drawing.Size(44, 13)
        Me.Lb5.TabIndex = 4
        Me.Lb5.Text = "Mobile"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 80
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(264, 272)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Silver
        Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox2.Location = New System.Drawing.Point(256, 0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(8, 272)
        Me.PictureBox2.TabIndex = 6
        Me.PictureBox2.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.LightGray
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button1.Location = New System.Drawing.Point(160, 200)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(88, 56)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Œ—ÊÃ"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button1.UseVisualStyleBackColor = False
        '
        'FrmAbout
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(496, 261)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Lb5)
        Me.Controls.Add(Me.Lb4)
        Me.Controls.Add(Me.Lb3)
        Me.Controls.Add(Me.Lb2)
        Me.Controls.Add(Me.Lb1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAbout"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "„‰ ‰Õ‰"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Lb1.Top = Lb1.Top - 2
        If Lb1.Top < 0 Then Lb1.Top = 250
        Lb2.Top = Lb2.Top - 2
        If Lb2.Top < 0 Then Lb2.Top = 250
        Lb3.Top = Lb3.Top - 2
        If Lb3.Top < 0 Then Lb3.Top = 250
        Lb4.Top = Lb4.Top - 2
        If Lb4.Top < 0 Then Lb4.Top = 250
        Lb5.Top = Lb5.Top - 2
        If Lb5.Top < 0 Then Lb5.Top = 250

    End Sub

    
    Private Sub FrmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Lb1.Top = 248
        Lb2.Top = 278
        Lb3.Top = 308
        Lb4.Top = 338
        Lb5.Top = 368
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class
