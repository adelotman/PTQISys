Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting
Imports Microsoft.ReportingServices
Imports Microsoft.Reporting.WinForms
Imports WindowsDesigne.DBHRMSDataSet
Imports WindowsDesigne.DBHRMSDataSetTableAdapters

Public Class FrmRepPreview


    Private Sub FrmRepPreview_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


        '=================================================================================================================
        Dim rptDataSource As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        'Dim rptDataSource As ReportDataSource
        '"RepDataPersonal" '"RepInvDetials"
        Dim ds As New DataSet
        Dim da As New SqlClient.SqlDataAdapter

        Try
            With Me.ReportViewer1.LocalReport
                ' .ReportPath = "Reports\" & strReport & ".rdlc"
                .ReportEmbeddedResource = "WindowsDesigne." & strReport & ".rdlc"
                .DataSources.Clear()
                ReportViewer1.ProcessingMode = WinForms.ProcessingMode.Local
            End With

            Select Case strReport

                Case "Repport_Salary"
                    Dim parms(1) As Microsoft.Reporting.WinForms.ReportParameter
                    ' parms(0) = New Microsoft.Reporting.WinForms.ReportParameter("monthdate", FrmAdd_action.DateTime.Value.Month.ToString)
                    'parms(1) = New Microsoft.Reporting.WinForms.ReportParameter("yeardate", FrmAdd_action.DateTime.Value.Year.ToString)
                    ReportViewer1.LocalReport.SetParameters(parms)

            End Select
            If SqlRep <> Nothing Then
                ds = MyClss.GetRecordsByDataSet(SqlRep, "")
                rptDataSource = New ReportDataSource("DBHRMSDataSet", ds.Tables(0))
                Me.ReportViewer1.LocalReport.DataSources.Add(rptDataSource)
                Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
                Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
                Me.ReportViewer1.ZoomMode = ZoomMode.Percent
                Me.ReportViewer1.ZoomPercent = 100
                ReportViewer1.LocalReport.Refresh()
                Me.ReportViewer1.RefreshReport()
            Else
                MessageBox.Show("اسف !!! هناك خطاء اثناء الاستعلام عن التقرير الرجاء التحقق من البيانات بشكل صحيح", "حطاء في الاستعلام", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Protected Sub SubReportProcessingEventHandler(ByVal sender As Object, ByVal e As SubreportProcessingEventArgs)
        Dim BD As New BindingSource
        Dim ds1 As New DataSet
        Dim Sql = "SELECT * FROM  TbViewDetilesInvoice" ' where NoIdRec='" + NoIdRec.ToString + "'"
        ds1 = MyClss.GetRecordsByDataSet(Sql, "")
        BD.DataSource = ds1.Tables(0)
        e.DataSources.Add(New ReportDataSource("DBHRMSDataSet", BD))
    End Sub


    Sub repview()
        Dim cc As ControlCollection = Me.ReportViewer1.Parent.Controls
        'get index of previous ReportViewer so we can swap it out.
        Dim prevIndex As Integer = cc.IndexOf(Me.ReportViewer1)

        ' Remove previous ReportViewer
        cc.Remove(Me.ReportViewer1)

        'add new instance of ReportViewer.  
        ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        ' Reset report properties.

        ' ReportViewer1.Height = Unit.Parse("100%")
        ' ReportViewer1.Width = Unit.Parse("100%")
        '   ReportViewer1.CssClass = "table"
        'Add the new ReportViewer to the previous ReportViewer location.
        '  cc.AddAt(prevIndex, ReportViewer1)
        cc.Add(ReportViewer1)
        ' Clear out any previous datasources.
        Me.ReportViewer1.LocalReport.DataSources.Clear()

        'Set report mode for local processing.
        ReportViewer1.ProcessingMode = WinForms.ProcessingMode.Local

        ' Create a new report dataset.
        Dim dataSet As New DataSet()

        ' Me.ReportViewer1.LocalReport.ReportPath = Server.MapPath("ReportPeopleMailingList.rdlc")
        ReportViewer1.LocalReport.ReportEmbeddedResource = "EmployeePayrollManagementSystem.RepInvDetials.rdlc"

        ' Load dataset.
        Try
            ' retrieve dataset.
            Dim NoIdRec As Int16 = 1
            Dim Sql = "SELECT * FROM  TbViewDetilesInvoice where NoIdRec='" + NoIdRec.ToString + "'"
            dataSet = MyClss.GetRecordsByDataSet(Sql, "")
            ' dataSet = CType(Session("sessionDataSetCli"), DataSet)
        Catch
            Return
        End Try

        '' Assign report parameters.
        'Dim parms(0) As Microsoft.Reporting.WinForms.ReportParameter
        'parms(0) = New Microsoft.Reporting.WinForms.ReportParameter("title", "Clients")
        'ReportViewer1.LocalReport.SetParameters(parms)
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ' Load the dataSource.
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DBHRMDataSet", dataSet.Tables(0)))
        Me.ReportViewer1.Size = New System.Drawing.Size(700, 500)
        '  ReportViewer1.AutoSize = True
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.TabIndex = 0
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        ' Refresh the ReportViewer
        ReportViewer1.LocalReport.Refresh()
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub ReportViewer1_Load(sender As System.Object, e As System.EventArgs) Handles ReportViewer1.Load

    End Sub
End Class