<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProScanner
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProScanner))
        Me.BtnStart = New System.Windows.Forms.Button()
        Me.BtnPaush = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.ProgBar = New System.Windows.Forms.ProgressBar()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TxtState = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnStart
        '
        Me.BtnStart.Image = CType(resources.GetObject("BtnStart.Image"), System.Drawing.Image)
        Me.BtnStart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnStart.Location = New System.Drawing.Point(27, 51)
        Me.BtnStart.Name = "BtnStart"
        Me.BtnStart.Size = New System.Drawing.Size(91, 60)
        Me.BtnStart.TabIndex = 0
        Me.BtnStart.Text = "بدء المسح"
        Me.BtnStart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnStart.UseVisualStyleBackColor = True
        '
        'BtnPaush
        '
        Me.BtnPaush.Image = CType(resources.GetObject("BtnPaush.Image"), System.Drawing.Image)
        Me.BtnPaush.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnPaush.Location = New System.Drawing.Point(201, 51)
        Me.BtnPaush.Name = "BtnPaush"
        Me.BtnPaush.Size = New System.Drawing.Size(91, 60)
        Me.BtnPaush.TabIndex = 1
        Me.BtnPaush.Text = "ايقاف مؤقت"
        Me.BtnPaush.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnPaush.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Image = CType(resources.GetObject("BtnCancel.Image"), System.Drawing.Image)
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCancel.Location = New System.Drawing.Point(383, 51)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(91, 60)
        Me.BtnCancel.TabIndex = 2
        Me.BtnCancel.Text = "الغاء الامر"
        Me.BtnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'ProgBar
        '
        Me.ProgBar.Location = New System.Drawing.Point(5, 117)
        Me.ProgBar.Name = "ProgBar"
        Me.ProgBar.Size = New System.Drawing.Size(486, 23)
        Me.ProgBar.TabIndex = 3
        Me.ProgBar.Visible = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.TxtState})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 143)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(498, 22)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(68, 17)
        Me.ToolStripStatusLabel1.Text = "حالة الماسح :"
        '
        'TxtState
        '
        Me.TxtState.ForeColor = System.Drawing.Color.Red
        Me.TxtState.Name = "TxtState"
        Me.TxtState.Size = New System.Drawing.Size(40, 17)
        Me.TxtState.Text = "يعمل ..."
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Image = CType(resources.GetObject("Label10.Image"), System.Drawing.Image)
        Me.Label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(498, 40)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "جلب الصورة من الماسح الضوئي"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(298, 51)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(79, 30)
        Me.ListBox1.TabIndex = 43
        Me.ListBox1.Visible = False
        '
        'FrmProScanner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(498, 165)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ProgBar)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnPaush)
        Me.Controls.Add(Me.BtnStart)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmProScanner"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "جلب الصورة من الماسح الضوئي"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnStart As System.Windows.Forms.Button
    Friend WithEvents BtnPaush As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents ProgBar As System.Windows.Forms.ProgressBar
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TxtState As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
End Class
