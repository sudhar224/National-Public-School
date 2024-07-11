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
	public partial class Marks : System.Web.UI.Page
	{
		Commonfnx fnobj = new Commonfnx();
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				GetClass();
				GetMarks();
			}
		}

		private void GetMarks()
		{
			DataTable dt = fnobj.Fetch("Select ROW_NUMBER() over(Order by(Select 1)) as [Sr.No], e.exam_id, e.class_id, c.class_name , e.subject_id, s.subject_name, e.rollno, e.total_marks, e.out_ofmarks from tbl_exam e inner join tbl_class c on e.class_id = c.class_id inner join  tbl_subject  s on e.subject_id = s.subject_id");
			GridView1.DataSource = dt;
			GridView1.DataBind();
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

		protected void btnAdd_Click(object sender, EventArgs e)
        {
			try
			{
				string classId = ddlClass.SelectedValue;
				string subjectId = ddlSubject.SelectedValue;
				string rollNo = txtRoll.Text.Trim();
				string studMarks = txtStudMarks.Text.Trim();
				string outOfMarks = txtOutofMarks.Text.Trim();
				DataTable dttbl = fnobj.Fetch("Select student_id from tbl_student where class_id = '" + classId + "' and rollno = '" + rollNo +  "'");

				if( dttbl.Rows.Count > 0)
				{
					DataTable dt = fnobj.Fetch("Select * from tbl_exam where class_id = '" + classId + "' and subject_id = '" + subjectId + "' and  rollno = '" + rollNo + "'");
					if (dt.Rows.Count == 0)
					{
						string query = "Insert into tbl_exam values ('" + classId + "','" + subjectId + "','" + rollNo + "','" + studMarks + "','" + outOfMarks + "')";
						fnobj.Query(query);
						lblMsg.Text = "Inserted successfully!";
						lblMsg.CssClass = "alert alert-success";
						ddlClass.SelectedIndex = 0;
						ddlSubject.SelectedIndex = 0;
						txtRoll.Text = string.Empty;
						txtStudMarks.Text = string.Empty;
						txtOutofMarks.Text = string.Empty;
						GetMarks();
					}
					else
					{
						lblMsg.Text = "Entered <b>Data<b> already exists!";
						lblMsg.CssClass = "alert alert-danger";
					}
				}
				else
				{
					lblMsg.Text = "Entered RollNo <b>" + rollNo + "<b> does not exist for selected class!";				
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
			GetMarks() ;
		}

		protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			GridView1.EditIndex = -1;
			GetMarks() ;
		}

		protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
		{

		}

		protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
		{
			GridView1.EditIndex = e.NewEditIndex;
			GetMarks() ;
		}

		protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			try
			{
				GridViewRow row = GridView1.Rows[e.RowIndex];
				int examId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
				string classId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlClassGv")).SelectedValue;
				string subjectId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlSubjectGv")).SelectedValue;
				string rollNo = (row.FindControl("txtRollNoGv") as TextBox).Text.Trim();
				string studMarks = (row.FindControl("txtStudMarrksGv") as TextBox).Text.Trim();
				string outOfMarks = (row.FindControl("txtOutOfMarksGv") as TextBox).Text.Trim();
				fnobj.Query("Update tbl_exam set class_id = '" + classId + "', subject_id = '" + subjectId + "', rollno = '" + rollNo + "', total_marks = '" + studMarks + "', out_ofmarks = '"+ outOfMarks +"' where exam_id = '" + examId + "'");
				lblMsg.Text = "Record Updated successfully!";
				lblMsg.CssClass = "alert alert-success";
				GridView1.EditIndex = -1;
				GetMarks();
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
		{

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
	}
}