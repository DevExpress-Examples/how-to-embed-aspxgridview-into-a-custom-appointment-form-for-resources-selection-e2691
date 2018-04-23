Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports DevExpress.XtraScheduler
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.Web.ASPxEditors
Imports System.Collections
Imports DevExpress.Web.ASPxGridView

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Private lastInsertedAppointmentId As Integer

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

		If (Not IsPostBack) Then
			ASPxScheduler1.Start = New DateTime(2008, 7, 12)
		End If
	End Sub

	Protected Sub ASPxScheduler1_AppointmentRowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.ASPxSchedulerDataInsertingEventArgs)
		e.NewValues.Remove("ID")
	End Sub

	Protected Sub SqlDataSourceAppointments_Inserted(ByVal sender As Object, ByVal e As SqlDataSourceStatusEventArgs)
		Dim connection As SqlConnection = CType(e.Command.Connection, SqlConnection)

		Using command As New SqlCommand("SELECT IDENT_CURRENT('CarScheduling')", connection)
			lastInsertedAppointmentId = Convert.ToInt32(command.ExecuteScalar())
		End Using
	End Sub

	Protected Sub ASPxScheduler1_AppointmentRowInserted(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.ASPxSchedulerDataInsertedEventArgs)
		e.KeyFieldValue = lastInsertedAppointmentId
	End Sub

	Protected Sub ASPxScheduler1_AppointmentsInserted(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.PersistentObjectsEventArgs)
		'int count = e.Objects.Count;
		'System.Diagnostics.Debug.Assert(count == 1);

		Dim apt As Appointment = CType(e.Objects(0), Appointment)
		Dim storage As ASPxSchedulerStorage = CType(sender, ASPxSchedulerStorage)
		storage.SetAppointmentId(apt, lastInsertedAppointmentId)
	End Sub

	Protected Sub ASPxScheduler1_BeforeExecuteCallbackCommand(ByVal sender As Object, ByVal e As SchedulerCallbackCommandEventArgs)
		If e.CommandId = SchedulerCallbackCommandId.AppointmentSave Then
			e.Command = New CustomAppointmentSaveCallbackCommand(CType(sender, ASPxScheduler))
		End If
	End Sub
End Class

Public Class CustomAppointmentSaveCallbackCommand
	Inherits AppointmentFormSaveCallbackCommand
	Public Sub New(ByVal control As ASPxScheduler)
		MyBase.New(control)
	End Sub

	Protected Overrides Sub AssignControllerValues()
		MyBase.AssignControllerValues()

		AssignControllerResources()
	End Sub

	Private Sub AssignControllerResources()
		Dim ddResource As ASPxDropDownEdit = CType(Me.FindControlByID("ddResource"), ASPxDropDownEdit)
		If MyBase.Control.Storage.ResourceSharing AndAlso ddResource IsNot Nothing Then
			Dim gvMultiResource As ASPxGridView = CType(ddResource.FindControl("gvMultiResource"), ASPxGridView)
			If gvMultiResource IsNot Nothing Then
				MyBase.Controller.ResourceIds.Clear()
				Dim resources() As String = ddResource.Value.ToString().Split(New Char() { ","c }, StringSplitOptions.RemoveEmptyEntries)
				For i As Integer = 0 To resources.Length - 1
					For j As Integer = 0 To gvMultiResource.VisibleRowCount - 1
						Dim text As String = gvMultiResource.GetRowValues(j, "Text").ToString()
						If resources(i).Trim() = text.Trim() Then
							MyBase.Controller.ResourceIds.Add(gvMultiResource.GetRowValues(j, "Value"))
						End If
					Next j
				Next i
			End If
		End If
	End Sub
End Class
