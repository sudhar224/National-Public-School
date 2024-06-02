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
	public partial class Subject : System.Web.UI.Page
	{
		Commonfnx fnobj = new Commonfnx();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				GetClass();
				GetSubject();
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

		protected void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				string classval = ddlClass.SelectedItem.Text;
				DataTable dt = fnobj.Fetch("Select * from tbl_subject where class_id = '" + ddlClass.SelectedItem.Value + "' and subject_name = '" + txtSubject.Text.Trim() + "'");
				if (dt.Rows.Count == 0)
				{
					string query = "Insert into tbl_subject values ('" + ddlClass.SelectedItem.Value + "','" + txtSubject.Text.Trim() + "')";
					fnobj.Query(query);
					lblMsg.Text = "Inserted successfully!";
					lblMsg.CssClass = "alert alert-success";
					ddlClass.SelectedIndex = 0;
					txtSubject.Text = string.Empty;
					GetSubject();
				}
				else
				{
					lblMsg.Text = "Entered Subject already exists for <b>'" + classval + "'</b>";
					lblMsg.CssClass = "alert alert-danger";
				}
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		private void GetSubject()
		{
			DataTable dt = fnobj.Fetch("Select Row_NUMBER() over (order by (Select 1)) as [Sr.No], s.subject_id, s.class_id, c.class_name, s.subject_name from tbl_subject s inner join tbl_class c on c.class_id = s.class_id");
			GridView1.DataSource = dt;
			GridView1.DataBind();
		}

		protected void GridView1_PageIndexChanged(object sender, EventArgs e)
		{

		}

		protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GridView1.PageIndex = e.NewPageIndex;
			GetSubject();
		}

		protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			GridView1.EditIndex = -1;
			GetSubject();
		}

		protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
		{
			GridView1.EditIndex = e.NewEditIndex;
			GetSubject();
		}

		protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			try
			{
				GridViewRow row = GridView1.Rows[e.RowIndex];
				int subId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
				string classId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("DropDownList1")).SelectedValue;
				string subjName = (row.FindControl("TextBox1") as TextBox).Text;
				fnobj.Query("Update tbl_subject set class_id = '" + classId + "', subject_name = '"+subjName+"' where subject_id = '" + subId + "'");
				lblMsg.Text = "Subject Updated successfully!";
				lblMsg.CssClass = "alert alert-success";
				GridView1.EditIndex = -1;
				GetSubject();
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}
	}
}