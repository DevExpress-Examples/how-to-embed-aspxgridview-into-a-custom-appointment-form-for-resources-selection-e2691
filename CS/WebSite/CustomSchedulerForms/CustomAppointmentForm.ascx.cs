using System;
using System.Collections;
using System.Web.UI;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Localization;

public partial class AppointmentForm : SchedulerFormControl {
	public bool CanShowReminders {
		get {
			return ((AppointmentFormTemplateContainer)Parent).Control.Storage.EnableReminders;
		}
	}
    public bool ResourceSharing { 
        get {
            return ((AppointmentFormTemplateContainer)Parent).Control.Storage.ResourceSharing;
        } 
    }
    public IEnumerable ResourceDataSource {
        get {
            return ((AppointmentFormTemplateContainer)Parent).ResourceDataSource;
        }
    }

	protected void Page_Load(object sender, EventArgs e) {
		//PrepareChildControls();
		tbSubject.Focus();
	}
	public override void DataBind() {
		base.DataBind();

		AppointmentFormTemplateContainer container = (AppointmentFormTemplateContainer)Parent;
		Appointment apt = container.Appointment;
		edtLabel.SelectedIndex = apt.LabelId;
		edtStatus.SelectedIndex = apt.StatusId;

        PopulateResourceEditors(apt, container);

		AppointmentRecurrenceForm1.Visible = container.ShouldShowRecurrence;

		if (container.Appointment.HasReminder) {
			cbReminder.Value = container.Appointment.Reminder.TimeBeforeStart.ToString();
			chkReminder.Checked = true;
		}
		else {
			cbReminder.ClientEnabled = false;
		}

		btnOk.ClientSideEvents.Click = container.SaveHandler;
		btnCancel.ClientSideEvents.Click = container.CancelHandler;
		btnDelete.ClientSideEvents.Click = container.DeleteHandler;

		//btnDelete.Enabled = !container.IsNewAppointment;
	}

    private void PopulateResourceEditors(Appointment apt, AppointmentFormTemplateContainer container) {
        if(ResourceSharing) {
            ASPxGridView gvMultiResource = ddResource.FindControl("gvMultiResource") as ASPxGridView;
            if (gvMultiResource == null || Page.Request["__CALLBACKID"].Contains(gvMultiResource.ID))
                return;
            
            SetGridViewSelectedRows(gvMultiResource, apt.ResourceIds);
            ddResource.JSProperties.Add("cp_Caption_ResourceNone", SchedulerLocalizer.GetString(SchedulerStringId.Caption_ResourceNone));
        }
        else {
            if(!Object.Equals(apt.ResourceId, Resource.Empty.Id))
                edtResource.Value = apt.ResourceId.ToString();
            else
                edtResource.Value = SchedulerIdHelper.EmptyResourceId;
        }
    }

    //List<String> GetGridViewSeletedRowsText(ASPxGridView gridView) {
    //    List<String> result = new List<string>();
    //    for (int i = 0; i < gridView.VisibleRowCount; i++) {
    //        if(gridView.Selection.IsRowSelectedByKey(gridView.GetRowValues(i, "Value")))
    //            result.Add(gridView.GetRowValues(i, "Text").ToString());
    //    }
    //    return result;
    //}

    void SetGridViewSelectedRows(ASPxGridView gridView, IEnumerable values) {
        gridView.Selection.UnselectAll();
        foreach(object value in values)
            if (gridView.FindVisibleIndexByKeyValue(value) >= 0)
                gridView.Selection.SelectRowByKey(value);
    }

	protected override void PrepareChildControls() {
		AppointmentFormTemplateContainer container = (AppointmentFormTemplateContainer)Parent;
		ASPxScheduler control = container.Control;

		AppointmentRecurrenceForm1.EditorsInfo = new EditorsInfo(control, control.Styles.FormEditors, control.Images.FormEditors, control.Styles.Buttons);
		base.PrepareChildControls();
	}
	protected override ASPxEditBase[] GetChildEditors() {
		ASPxEditBase[] edits = new ASPxEditBase[] {
			lblSubject, tbSubject,
			lblLocation, tbLocation,
			lblLabel, edtLabel,
			lblStartDate, edtStartDate,
			lblEndDate, edtEndDate,
			lblStatus, edtStatus,
			lblAllDay, chkAllDay,
			lblResource, edtResource,
			tbDescription, cbReminder,
            ddResource
		};
		return edits;
	}
	protected override ASPxButton[] GetChildButtons() {
		ASPxButton[] buttons = new ASPxButton[] {
			btnOk, btnCancel, btnDelete
		};
		return buttons;
	}

    protected string GetResourceImageUrl(GridViewDataItemTemplateContainer container) {
        return string.Format("~/Images/{0}.jpg", container.Text);
    }
}
