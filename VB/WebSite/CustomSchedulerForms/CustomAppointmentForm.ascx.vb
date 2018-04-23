Imports System
Imports System.Collections
Imports System.Web.UI
Imports DevExpress.Web
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Localization

Partial Public Class AppointmentForm
    Inherits SchedulerFormControl

    Public ReadOnly Property CanShowReminders() As Boolean
        Get
            Return CType(Parent, AppointmentFormTemplateContainer).Control.Storage.EnableReminders
        End Get
    End Property
    Public ReadOnly Property ResourceSharing() As Boolean
        Get
            Return CType(Parent, AppointmentFormTemplateContainer).Control.Storage.ResourceSharing
        End Get
    End Property
    Public ReadOnly Property ResourceDataSource() As IEnumerable
        Get
            Return CType(Parent, AppointmentFormTemplateContainer).ResourceDataSource
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        'PrepareChildControls();
        tbSubject.Focus()
    End Sub
    Public Overrides Sub DataBind()
        MyBase.DataBind()

        Dim container As AppointmentFormTemplateContainer = CType(Parent, AppointmentFormTemplateContainer)
        Dim apt As Appointment = container.Appointment
        edtLabel.SelectedIndex = CInt((apt.LabelKey))
        edtStatus.SelectedIndex = CInt((apt.StatusKey))

        PopulateResourceEditors(apt, container)

        AppointmentRecurrenceForm1.Visible = container.ShouldShowRecurrence

        If container.Appointment.HasReminder Then
            cbReminder.Value = container.Appointment.Reminder.TimeBeforeStart.ToString()
            chkReminder.Checked = True
        Else
            cbReminder.ClientEnabled = False
        End If

        btnOk.ClientSideEvents.Click = container.SaveHandler
        btnCancel.ClientSideEvents.Click = container.CancelHandler
        btnDelete.ClientSideEvents.Click = container.DeleteHandler

        'btnDelete.Enabled = !container.IsNewAppointment;
    End Sub

    Private Sub PopulateResourceEditors(ByVal apt As Appointment, ByVal container As AppointmentFormTemplateContainer)
        If ResourceSharing Then
            Dim gvMultiResource As ASPxGridView = TryCast(ddResource.FindControl("gvMultiResource"), ASPxGridView)
            If gvMultiResource Is Nothing OrElse Page.Request("__CALLBACKID").Contains(gvMultiResource.ID) Then
                Return
            End If

            SetGridViewSelectedRows(gvMultiResource, apt.ResourceIds)
            ddResource.JSProperties.Add("cp_Caption_ResourceNone", SchedulerLocalizer.GetString(SchedulerStringId.Caption_ResourceNone))
        Else
            If Not Object.Equals(apt.ResourceId, ResourceEmpty.Id) Then
                edtResource.Value = apt.ResourceId.ToString()
            Else
                edtResource.Value = SchedulerIdHelper.EmptyResourceId
            End If
        End If
    End Sub

    'List<String> GetGridViewSeletedRowsText(ASPxGridView gridView) {
    '    List<String> result = new List<string>();
    '    for (int i = 0; i < gridView.VisibleRowCount; i++) {
    '        if(gridView.Selection.IsRowSelectedByKey(gridView.GetRowValues(i, "Value")))
    '            result.Add(gridView.GetRowValues(i, "Text").ToString());
    '    }
    '    return result;
    '}

    Private Sub SetGridViewSelectedRows(ByVal gridView As ASPxGridView, ByVal values As IEnumerable)
        gridView.Selection.UnselectAll()
        For Each value As Object In values
            If gridView.FindVisibleIndexByKeyValue(value) >= 0 Then
                gridView.Selection.SelectRowByKey(value)
            End If
        Next value
    End Sub

    Protected Overrides Sub PrepareChildControls()
        Dim container As AppointmentFormTemplateContainer = CType(Parent, AppointmentFormTemplateContainer)
        Dim control As ASPxScheduler = container.Control

        AppointmentRecurrenceForm1.EditorsInfo = New EditorsInfo(control, control.Styles.FormEditors, control.Images.FormEditors, control.Styles.Buttons)
        MyBase.PrepareChildControls()
    End Sub
    Protected Overrides Function GetChildEditors() As ASPxEditBase()
        Dim edits() As ASPxEditBase = { lblSubject, tbSubject, lblLocation, tbLocation, lblLabel, edtLabel, lblStartDate, edtStartDate, lblEndDate, edtEndDate, lblStatus, edtStatus, lblAllDay, chkAllDay, lblResource, edtResource, tbDescription, cbReminder, ddResource }
        Return edits
    End Function
    Protected Overrides Function GetChildButtons() As ASPxButton()
        Dim buttons() As ASPxButton = { btnOk, btnCancel, btnDelete }
        Return buttons
    End Function

    Protected Function GetResourceImageUrl(ByVal container As GridViewDataItemTemplateContainer) As String
        Return String.Format("~/Images/{0}.jpg", container.Text)
    End Function
End Class
