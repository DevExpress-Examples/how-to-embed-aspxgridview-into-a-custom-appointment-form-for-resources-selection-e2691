using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DevExpress.XtraScheduler;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.Web;
using System.Collections;

public partial class _Default : System.Web.UI.Page {
    private int lastInsertedAppointmentId;

    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {
            ASPxScheduler1.Start = new DateTime(2008, 7, 12);
        }
    }

    protected void ASPxScheduler1_AppointmentRowInserting(object sender, DevExpress.Web.ASPxScheduler.ASPxSchedulerDataInsertingEventArgs e) {
        e.NewValues.Remove("ID");
    }

    protected void SqlDataSourceAppointments_Inserted(object sender, SqlDataSourceStatusEventArgs e) {
        SqlConnection connection = (SqlConnection)e.Command.Connection;

        using (SqlCommand command = new SqlCommand("SELECT IDENT_CURRENT('CarScheduling')", connection)) {
            lastInsertedAppointmentId = Convert.ToInt32(command.ExecuteScalar());
        }
    }

    protected void ASPxScheduler1_AppointmentRowInserted(object sender, DevExpress.Web.ASPxScheduler.ASPxSchedulerDataInsertedEventArgs e) {
        e.KeyFieldValue = lastInsertedAppointmentId;
    }

    protected void ASPxScheduler1_AppointmentsInserted(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e) {
        //int count = e.Objects.Count;
        //System.Diagnostics.Debug.Assert(count == 1);

        Appointment apt = (Appointment)e.Objects[0];
        ASPxSchedulerStorage storage = (ASPxSchedulerStorage)sender;
        storage.SetAppointmentId(apt, lastInsertedAppointmentId);
    }

    protected void ASPxScheduler1_BeforeExecuteCallbackCommand(object sender, SchedulerCallbackCommandEventArgs e) {
        if (e.CommandId == SchedulerCallbackCommandId.AppointmentSave)
            e.Command = new CustomAppointmentSaveCallbackCommand((ASPxScheduler)sender);
    }
}

public class CustomAppointmentSaveCallbackCommand : AppointmentFormSaveCallbackCommand {
    public CustomAppointmentSaveCallbackCommand(ASPxScheduler control)
        : base(control) {
    }

    protected override void AssignControllerValues() {
        base.AssignControllerValues();

        AssignControllerResources();
    }

    private void AssignControllerResources() {
        ASPxDropDownEdit ddResource = (ASPxDropDownEdit)this.FindControlByID("ddResource");
        if (base.Control.Storage.ResourceSharing && ddResource != null) {
            ASPxGridView gvMultiResource = (ASPxGridView)ddResource.FindControl("gvMultiResource");
            if (gvMultiResource != null) {
                base.Controller.ResourceIds.Clear();
                string[] resources = ddResource.Value.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < resources.Length; i++) {
                    for (int j = 0; j < gvMultiResource.VisibleRowCount; j++) {
                        string text = gvMultiResource.GetRowValues(j, "Text").ToString();
                        if (resources[i].Trim() == text.Trim())
                            base.Controller.ResourceIds.Add(gvMultiResource.GetRowValues(j, "Value"));
                    }
                }
            }
        }
    }
}
