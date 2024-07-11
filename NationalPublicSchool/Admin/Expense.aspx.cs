using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static NationalPublicSchool.Models.CommonFn;

namespace NationalPublicSchool.Admin
{
	public partial class Expense : System.Web.UI.Page
	{
		Commonfnx fnobj = new Commonfnx();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				GetClass();
				GetExpense();
			}
		}

		private void GetClass()
		{
			DataTable dt = fnobj.Fetch("Select * from tbl_class");
			ddlClass.DataSource = dt;
			ddlClass.DataTextField = "class_name";
			ddlClass.DataValueField = "class_id";
			ddlClass.DataBind();
			ddlClass.Items.Insert(0, "Select Class");
		}

		protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
		{
			string classId = ddlClass.SelectedValue;
			DataTable dt = fnobj.Fetch("Select * from tbl_subject where class_id = '" + classId + "'");
			ddlSubject.DataSource = dt;
			ddlSubject.DataTextField = "subject_name";
			ddlSubject.DataValueField = "subject_id";
			ddlSubject.DataBind();
			ddlSubject.Items.Insert(0, "Select Subject");

		}
		private void GetExpense()
		{
			DataTable dt = fnobj.Fetch("Select ROW_NUMBER() over(Order by(Select 1)) as [Sr.No], e.expence_id, e.class_id, c.class_name , e.subject_id, s.subject_name, e.charge_amount from tbl_expense e inner join tbl_class c on e.class_id = c.class_id inner join  tbl_subject  s on e.subject_id = s.subject_id");
			GridView1.DataSource = dt;
			GridView1.DataBind();
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				string classId = ddlClass.SelectedValue;
				string subjectId = ddlSubject.SelectedValue;
				string chargeAmt = txtExpenseAmt.Text.Trim();
				DataTable dt = fnobj.Fetch("Select * from tbl_expense where class_id = '" + classId + "' and subject_id = '" + subjectId + "' or  charge_amount = '" + chargeAmt + "'");
				if (dt.Rows.Count == 0)
				{
					string query = "Insert into tbl_expense values ('" + classId + "','" + subjectId + "','" + chargeAmt + "')";
					fnobj.Query(query);
					lblMsg.Text = "Inserted successfully!";
					lblMsg.CssClass = "alert alert-success";
					ddlClass.SelectedIndex = 0;
					ddlSubject.SelectedIndex = 0;
					txtExpenseAmt.Text = string.Empty;
					GetExpense();
				}
				else
				{
					lblMsg.Text = "Entered <b>Data<b> already exists!";
					lblMsg.CssClass = "alert alert-danger";
				}
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		protected void GridView1_PageIndexChanged(object sender, EventArgs e)
		{

		}

		protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GridView1.PageIndex = e.NewPageIndex;
			GetExpense();
		}

		protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			GridView1.EditIndex = -1;
			GetExpense();
		}

		protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
		{

		}

		protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			try
			{
				int expenseId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
				fnobj.Query("Delete from tbl_expense where expence_id = '" + expenseId + "'");
				lblMsg.Text = "Expense Deleted successfully!";
				lblMsg.CssClass = "alert alert-success";
				GridView1.EditIndex = -1;
				GetExpense();
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
		{
			GridView1.EditIndex = e.NewEditIndex;
			GetExpense();
		}

		protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			try
			{
				GridViewRow row = GridView1.Rows[e.RowIndex];
				int expenseId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
				string classId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlClassGv")).SelectedValue;
				string subjectId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlSubjectGv")).SelectedValue;
				string chargeAmt = (row.FindControl("txtExpenseAmt") as TextBox).Text.Trim();
				fnobj.Query("Update tbl_expense set class_id = '" + classId + "', subject_id = '" + subjectId + "', charge_amount = '" + chargeAmt + "' where expence_id = '" + expenseId + "'");
				lblMsg.Text = "Record Updated successfully!";
				lblMsg.CssClass = "alert alert-success";
				GridView1.EditIndex = -1;
				GetExpense();
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
		protected void ddlClassGv_SelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownList ddlClassSelected = (DropDownList)sender;
			GridViewRow row = (GridViewRow)ddlClassSelected.NamingContainer;
			if (row != null)
			{
				if ((row.RowState & DataControlRowState.Edit) > 0)
				{
					DropDownList ddlSubjectGv = (DropDownList)row.FindControl("ddlSubjectGv");
					DataTable dt = fnobj.Fetch("Select * from tbl_subject where class_id = '" + ddlClassSelected.SelectedValue + "'");
					ddlSubjectGv.DataSource = dt;
					ddlSubjectGv.DataTextField = "subject_name";
					ddlSubjectGv.DataValueField = "subject_id";
					ddlSubjectGv.DataBind();
				}
			}
		}

		protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if ((e.Row.RowState & DataControlRowState.Edit) > 0)
				{
					DropDownList ddlClass = (DropDownList)e.Row.FindControl("ddlClassGv");
					DropDownList ddlSubject = (DropDownList)e.Row.FindControl("ddlSubjectGv");
					DataTable dt = fnobj.Fetch("Select * from tbl_subject where class_id = '" + ddlClass.SelectedValue + "'");
					ddlSubject.DataSource = dt;
					ddlSubject.DataTextField = "subject_name";
					ddlSubject.DataValueField = "subject_id";
					ddlSubject.DataBind();
					ddlSubject.Items.Insert(0, "Select Subject");
					string selectedSubject = DataBinder.Eval(e.Row.DataItem, "subject_name").ToString();
					ddlSubject.Items.FindByText(selectedSubject).Selected = true;
				}
			}
		}
	}
}