<%@ Control Language="vb" AutoEventWireup="true" Inherits="AppointmentForm" CodeFile="CustomAppointmentForm.ascx.vb" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.3, Version=9.3.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v9.3, Version=9.3.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v9.3, Version=9.3.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v9.3, Version=9.3.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<table class="dxscAppointmentForm" cellpadding="0" cellspacing="0" style="width: 100%; height: 230px;">
	<tr>
		<td class="dxscDoubleCell" colspan="2">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
						<dxe:ASPxLabel ID="lblSubject" runat="server" AssociatedControlID="tbSubject" Text="Subject:">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxTextBox ClientInstanceName="_dx" ID="tbSubject" runat="server" Width="100%" Text='<%#(CType(Container, AppointmentFormTemplateContainer)).Appointment.Subject%>' />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr> 
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
						<dxe:ASPxLabel ID="lblLocation" runat="server" AssociatedControlID="tbLocation" Text="Location:">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxTextBox ClientInstanceName="_dx" ID="tbLocation" runat="server" Width="100%" Text='<%#(CType(Container, AppointmentFormTemplateContainer)).Appointment.Location%>' />
					</td>
				</tr>
			</table>
		</td>
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell" style="padding-left: 25px;">
						<dxe:ASPxLabel ID="lblLabel" runat="server" AssociatedControlID="edtLabel" Text="Label:">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtLabel" runat="server" Width="100%" DataSource='<%#(CType(Container, AppointmentFormTemplateContainer)).LabelDataSource%>' />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
						<dxe:ASPxLabel ID="lblStartDate" runat="server" AssociatedControlID="edtStartDate" Text="Start time:" Wrap="false">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxDateEdit ClientInstanceName="_dx" ID="edtStartDate" runat="server" Width="100%" Date='<%#(CType(Container, AppointmentFormTemplateContainer)).Start%>' EditFormat="DateTime" DateOnError="Undo" AllowNull="false" />
					</td>
				</tr>
			</table>
		</td>
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell" style="padding-left: 25px;">
						<dxe:ASPxLabel runat="server" ID="lblEndDate" Text="End time:" Wrap="false" AssociatedControlID="edtEndDate"/>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxDateEdit id="edtEndDate" runat="server" Date='<%#(CType(Container, AppointmentFormTemplateContainer)).End%>'
							EditFormat="DateTime" Width="100%" DateOnError="Undo" AllowNull="false">
						</dxe:ASPxDateEdit>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
						<dxe:ASPxLabel ID="lblStatus" runat="server" AssociatedControlID="edtStatus" Text="Show time as:" Wrap="false">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtStatus" runat="server" Width="100%" DataSource='<%#(CType(Container, AppointmentFormTemplateContainer)).StatusDataSource%>' />
					</td>
				</tr>
			</table>
		</td>
		<td class="dxscSingleCell" style="padding-left: 22px;">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td style="width: 20px; height: 20px;">
						<dxe:ASPxCheckBox ClientInstanceName="_dx" ID="chkAllDay" runat="server" Checked='<%#(CType(Container, AppointmentFormTemplateContainer)).Appointment.AllDay%>'>
						</dxe:ASPxCheckBox>
					</td>
					<td style="padding-left: 2px;">
						<dxe:ASPxLabel ID="lblAllDay" runat="server" Text="All day event" AssociatedControlID="chkAllDay" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
<%
   If CanShowReminders Then
%>
		<td class="dxscSingleCell">
<%
   Else
%>
		<td class="dxscDoubleCell" colspan="2">
<%
   End If
%>
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
						<dxe:ASPxLabel ID="lblResource" runat="server" AssociatedControlID="edtResource" Text="Resource:">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
<%
   If ResourceSharing Then
%>
						<dxe:ASPxDropDownEdit id="ddResource" runat="server" Width="100%" ClientInstanceName="ddResource" Enabled='<%#(CType(Container, AppointmentFormTemplateContainer)).CanEditResource%>' AllowUserInput="false">
							<DropDownWindowTemplate>

								<dx:ASPxGridView ID="gvMultiResource" runat="server" Width="100%" DataSource='<%#ResourceDataSource%>' KeyFieldName="Value" Border-BorderWidth="0">
									<SettingsBehavior AllowMultiSelection="True" />
									<SettingsPager Mode="ShowPager" PageSize="5" />

									<Columns>
										<dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" />
										<dx:GridViewDataColumn FieldName="Text" Caption="Name" VisibleIndex="1" />
										<dx:GridViewDataColumn FieldName="Value" Caption="Image" VisibleIndex="2">
											<DataItemTemplate>
												<dxe:ASPxImage ID="ASPxImage1" runat="server" Width="100px" Height="50px"
													ImageUrl='<%#GetResourceImageUrl(Container)%>'  />
											</DataItemTemplate>
										</dx:GridViewDataColumn>
									</Columns>

									<ClientSideEvents SelectionChanged="function(s, e) {
										s.GetSelectedFieldValues('Text', OnGridSelectionComplete);
										e.processOnServer = false;
									}" 
									Init="function(s, e) {
										ddResource.SetValue( ddResource.cp_Caption_ResourceNone);
									}" />
								</dx:ASPxGridView>

							</DropDownWindowTemplate>
						</dxe:ASPxDropDownEdit>                        
<%
   Else
%>
						<dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtResource" runat="server" Width="100%" DataSource='<%#ResourceDataSource%> ' Enabled='<%#(CType(Container, AppointmentFormTemplateContainer)).CanEditResource%>' />
<%
   End If
%>
					</td>

				</tr>
			</table>
		</td>
<%
   If CanShowReminders Then
%>
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell" style="padding-left: 22px;">
						<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
							<tr>
								<td style="width: 20px; height: 20px;">
									<dxe:ASPxCheckBox ClientInstanceName="_dx" ID="chkReminder" runat="server"> 
										<ClientSideEvents CheckedChanged="function(s, e) { OnChkReminderCheckedChanged(s, e); }" />
									</dxe:ASPxCheckBox>
								</td>
								<td style="padding-left: 2px;">
									<dxe:ASPxLabel ID="lblReminder" runat="server" Text="Reminder" AssociatedControlID="chkReminder" />
								</td>
							</tr>
						</table>
					</td>
					<td class="dxscControlCell" style="padding-left: 3px">
						<dxe:ASPxComboBox  ID="cbReminder" ClientInstanceName="_dxAppointmentForm_cbReminder" runat="server" Width="100%" DataSource='<%#(CType(Container, AppointmentFormTemplateContainer)).ReminderDataSource%>' />
					</td>
				</tr>
			</table>
		</td>
<%
   End If
%>
	</tr>
	<tr>
		<td class="dxscDoubleCell" colspan="2" style="height: 90px;">
			<dxe:ASPxMemo ClientInstanceName="_dx" ID="tbDescription" runat="server" Width="100%" Rows="6" Text='<%#(CType(Container, AppointmentFormTemplateContainer)).Appointment.Description%>' />
		</td>
	</tr>
</table>

<dxsc:AppointmentRecurrenceForm ID="AppointmentRecurrenceForm1" runat="server"
	IsRecurring='<%#(CType(Container, AppointmentFormTemplateContainer)).Appointment.IsRecurring%>' 
	DayNumber='<%#(CType(Container, AppointmentFormTemplateContainer)).RecurrenceDayNumber%>' 
	End='<%#(CType(Container, AppointmentFormTemplateContainer)).RecurrenceEnd%>' 
	Month='<%#(CType(Container, AppointmentFormTemplateContainer)).RecurrenceMonth%>' 
	OccurrenceCount='<%#(CType(Container, AppointmentFormTemplateContainer)).RecurrenceOccurrenceCount%>' 
	Periodicity='<%#(CType(Container, AppointmentFormTemplateContainer)).RecurrencePeriodicity%>' 
	RecurrenceRange='<%#(CType(Container, AppointmentFormTemplateContainer)).RecurrenceRange%>' 
	Start='<%#(CType(Container, AppointmentFormTemplateContainer)).RecurrenceStart%>' 
	WeekDays='<%#(CType(Container, AppointmentFormTemplateContainer)).RecurrenceWeekDays%>' 
	WeekOfMonth='<%#(CType(Container, AppointmentFormTemplateContainer)).RecurrenceWeekOfMonth%>' 
	RecurrenceType='<%#(CType(Container, AppointmentFormTemplateContainer)).RecurrenceType%>'
	IsFormRecreated='<%#(CType(Container, AppointmentFormTemplateContainer)).IsFormRecreated%>' >
</dxsc:AppointmentRecurrenceForm>

<table cellpadding="0" cellspacing="0" style="width: 100%; height: 35px;">
	<tr>
		<td style="width: 100%; height: 100%;" align="center">
			<table style="height: 100%;">
				<tr>
					<td>
						<dxe:ASPxButton runat="server" ClientInstanceName="_dx" ID="btnOk" Text="OK" UseSubmitBehavior="false" AutoPostBack="false" 
							EnableViewState="false" Width="91px"/>
					</td>
					<td>
						<dxe:ASPxButton runat="server" ClientInstanceName="_dx" ID="btnCancel" Text="Cancel" UseSubmitBehavior="false" AutoPostBack="false" EnableViewState="false" 
							Width="91px" CausesValidation="False" />
					</td>
					<td>
						<dxe:ASPxButton runat="server" ClientInstanceName="_dx" ID="btnDelete" Text="Delete" UseSubmitBehavior="false"
							AutoPostBack="false" EnableViewState="false" Width="91px"
							Enabled='<%#(CType(Container, AppointmentFormTemplateContainer)).CanDeleteAppointment%>'
							CausesValidation="False" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<table cellpadding="0" cellspacing="0" style="width: 100%;">
	<tr>
		<td style="width: 100%;" align="left">
			<dxsc:ASPxSchedulerStatusInfo runat="server" ID="schedulerStatusInfo" Priority="1" MasterControlId='<%#(CType(Container, DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)).ControlId%>' />
		</td>
	</tr>
</table>
<script id="dxss_ASPxSchedulerAppoinmentForm" type="text/javascript">
	function OnChkReminderCheckedChanged(s, e) {
		var isReminderEnabled = s.GetValue();
		if (isReminderEnabled)
			_dxAppointmentForm_cbReminder.SetSelectedIndex(3);
		else
			_dxAppointmentForm_cbReminder.SetSelectedIndex(-1);

		_dxAppointmentForm_cbReminder.SetEnabled(isReminderEnabled);

	}

	function OnGridSelectionComplete(values) {
		var resourceNames = new Array();
		var count = values.length;

		if (count > 0) {
			for(var i = 0; i < count; i++)
				_aspxArrayPush(resourceNames, values[i]);
		}
		else
			_aspxArrayPush(resourceNames, ddResource.cp_Caption_ResourceNone);

		ddResource.SetValue(resourceNames.join(', '));
	}

</script>