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
				DataTable dt = fnobj.Fetch("Select * from tbl_exam where class_id = '" + classId + "' and subject_id = '" + subjectId + "' and  rollno = '" + rollNo + "'");
				if (dt.Rows.Count == 0)
				{
					string query = "Insert into tbl_exam values ('" + classId + "','" + subjectId + "','" + rollNo + "','" + studMarks + "','" + outOfMarks +"')";
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

		}

		protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{

		}

		protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
		{

		}

		protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
		{

		}

		protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{

		}

		protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
		{

		}

		protected void ddlClassGv_SelectedIndexChanged(object sender, EventArgs e)
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
			ddlSubject.Items.Insert(0, "Select Subject");
		}
	}
}