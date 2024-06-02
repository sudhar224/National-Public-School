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
	public partial class AddClass : System.Web.UI.Page
	{
		Commonfnx fnobj = new Commonfnx();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				GetClass();
			}
		}

		private void GetClass()
		{
			DataTable dt = fnobj.Fetch("Select Row_Number() over(Order by (Select 1)) as [Sr.No], class_id,class_name from tbl_class");
			GridView1.DataSource = dt;
			GridView1.DataBind();
		}
        protected void btnAdd_Click(object sender, EventArgs e)
        {
			try
			{
				DataTable dt = fnobj.Fetch("Select * from tbl_class where class_name = '" + txtClass.Text.Trim() + "'");
				if(dt.Rows.Count == 0)
				{
					string query = "Insert into tbl_class values ('" + txtClass.Text.Trim() + "')";
					fnobj.Query(query);
					lblMsg.Text = "Inserted successfully!";
					lblMsg.CssClass = "alert alert-success";
					txtClass.Text = string.Empty;
					GetClass();
				}
				else
				{
					lblMsg.Text = "Entered Class already exists!";
					lblMsg.CssClass = "alert alert-danger";
				}
			}
			catch(Exception ex) 
			{
				Response.Write("<script>alert('"+ex.Message +"');</script>");
			}
        }

		protected void GridView1_PageIndexChanged(object sender, EventArgs e)
		{
		
		}

		protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GridView1.PageIndex = e.NewPageIndex;
			GetClass();
		}

		protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
		{
			GridView1.EditIndex = e.NewEditIndex;

		}

		protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			try
			{
				GridViewRow row = GridView1.Rows[e.RowIndex];
				int cId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
				string ClassName = (row.FindControl("txtClassEdit") as TextBox).Text;
				fnobj.Query("Update tbl_class set class_name = '" +  ClassName + "' where class_id = '" +cId +"'");
				lblMsg.Text = "Class Updated successfully!";
				lblMsg.CssClass = "alert alert-success";
				GridView1.EditIndex = -1;
				GetClass();
			}
			catch(Exception ex) 
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			GridView1.EditIndex = -1;
			GetClass();
		}
	}
}