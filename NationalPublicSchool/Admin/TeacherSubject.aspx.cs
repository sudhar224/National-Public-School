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
	 
	public partial class TeacherSubject : System.Web.UI.Page
	{
		Commonfnx fnobj = new Commonfnx();
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				GetClass();
				GetTeacher();
				GetTeacherSubject();
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

		private void GetTeacher()
		{
			DataTable dt = fnobj.Fetch("Select * from tbl_teacher");
			ddlTeacher.DataSource = dt;
			ddlTeacher.DataTextField = "name";
			ddlTeacher.DataValueField = "teacher_id";
			ddlTeacher.DataBind();
			ddlTeacher.Items.Insert(0, "Select Teacher");
		}

		private void GetTeacherSubject()	
		{
			DataTable dt = fnobj.Fetch("Select Row_NUMBER() over (order by (Select 1)) as [Sr.No], ts.id, ts.class_id, c.class_name, ts.subject_id, s.subject_name, ts.teacher_id, t.name from tbl_teach_subject ts inner join tbl_class c on ts.class_id = c.class_id inner join tbl_subject s on ts.subject_id = s.subject_id inner join tbl_teacher t on ts.teacher_id = t.teacher_id");
			GridView1.DataSource = dt;
			GridView1.DataBind();
			
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				string classId = ddlClass.SelectedValue;
				string subjectId = ddlSubject.SelectedValue;
				string teacherId = ddlTeacher.SelectedValue;
				DataTable dt = fnobj.Fetch("Select * from tbl_teach_subject where class_id = '" + classId + "' and subject_id = '" + subjectId + "' or  teacher_id = '" + teacherId + "'");
				if (dt.Rows.Count == 0)
				{
					string query = "Insert into tbl_teach_subject values ('" + classId + "','" + subjectId + "','" + teacherId + "')";
					fnobj.Query(query);
					lblMsg.Text = "Inserted successfully!";
					lblMsg.CssClass = "alert alert-success";
					ddlClass.SelectedIndex = 0;
					ddlSubject.SelectedIndex = 0;
					ddlTeacher.SelectedIndex = 0;
					GetTeacherSubject();
				}
				else
				{
					lblMsg.Text = "Entered <b>Teacher Subject<b> already exists!";
					lblMsg.CssClass = "alert alert-danger";
				}
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
		{
			string classId = ddlClass.SelectedValue;
			DataTable dt = fnobj.Fetch("Select * from tbl_subject where class_id = '" + classId + "'");
			ddlSubject.DataSource = dt;
			ddlSubject.DataTextField = "subject_name";
			ddlSubject.DataValueField = "subject_id";
			ddlSubject.DataBind();
			ddlSubject.Items.Insert(0,"Select Subject");

		}

		protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GridView1.PageIndex = e.NewPageIndex;
			GetTeacherSubject();
		}

		protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			GridView1.EditIndex = -1;
			GetTeacherSubject();
		}

		protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
		{

		}

		protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			try
			{
				int teacherSubjetId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
				fnobj.Query("Delete from tbl_teach_subject where id = '" + teacherSubjetId + "'");
				lblMsg.Text = "Teacher Subject Deleted successfully!";
				lblMsg.CssClass = "alert alert-success";
				GridView1.EditIndex = -1;
				GetTeacherSubject();
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			try
			{
				GridViewRow row = GridView1.Rows[e.RowIndex];
				int teacherSubjetId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
				string classId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlClassGv")).SelectedValue;
				string subjectId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlSubjectGv")).SelectedValue;
				string teacherId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlTeacherGv")).SelectedValue;
				fnobj.Query("Update tbl_teach_subject set class_id = '" + classId + "', subject_id = '" + subjectId + "', teacher_id = '" + teacherId + "' where id = '" + teacherSubjetId + "'");
				lblMsg.Text = "Record Updated successfully!";
				lblMsg.CssClass = "alert alert-success";
				GridView1.EditIndex = -1;
				GetTeacherSubject();
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		protected void GridView1_PageIndexChanged(object sender, EventArgs e)
		{

		}

		protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
		{
			GridView1.EditIndex = e.NewEditIndex;
			GetTeacherSubject();
		}

		protected void ddlClassGv_SelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownList ddlClassSelected = (DropDownList)sender;
			GridViewRow row = (GridViewRow)ddlClassSelected.NamingContainer;
			if(row != null)
			{
				if((row.RowState & DataControlRowState.Edit) > 0)
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
			if(e.Row.RowType == DataControlRowType.DataRow)
			{
				if((e.Row.RowState & DataControlRowState.Edit) > 0)
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