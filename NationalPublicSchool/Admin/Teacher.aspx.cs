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
	public partial class Teacher : System.Web.UI.Page
	{
		Commonfnx fnobj = new Commonfnx();
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				GetTeacher();
			}
		}

		private void GetTeacher()
		{
			DataTable dt = fnobj.Fetch("Select ROW_NUMBER() OVER(ORDER BY (SELECT 1))as [Sr.No], teacher_id, [name],DOB, gender, mobile, email, [address],[password]  from tbl_teacher");
			GridView1.DataSource = dt;
			GridView1.DataBind();
		}

		protected void btnAdd_Click(object sender, EventArgs e)
        {
			try
			{
				if(ddlGender.SelectedValue != "0")
				{
					string email = txtEmail.Text.Trim();
					DataTable dt = fnobj.Fetch("Select * from tbl_teacher where email = '" + email + "'");
					if(dt .Rows.Count == 0)
					{
						string query = "Insert into tbl_teacher values ('" + txtName.Text.Trim() + "','" + txtDoB.Text.Trim() + "','" + 
							ddlGender.SelectedValue + "','" + txtMobile.Text.Trim() + "','" + txtEmail.Text.Trim()+ "','" + txtAddress.Text.Trim() + "','" + txtPassword.Text.Trim() + "')" ;
						fnobj.Query(query);
						lblMsg.Text = "Inserted successfully!";
						lblMsg.CssClass = "alert alert-success";
						ddlGender.SelectedIndex = 0;
						txtName.Text = string.Empty;
						txtDoB.Text = string.Empty;
						txtMobile.Text = string.Empty;
						txtEmail.Text = string.Empty;
						txtAddress.Text = string.Empty;
						txtPassword.Text = string.Empty;
						GetTeacher();
					}
					else
					{
						lblMsg.Text = "Entered <b>'" + email + "'</b> already exists!";
						lblMsg.CssClass = "alert alert-danger";
					}
				}
				else
				{
					lblMsg.Text = "Gender is required!";
					lblMsg.CssClass = "alert alert-danger";
				}
			}
			catch(Exception ex)
			{
				Response.Write("<script> alert('" + ex.Message +"'); </script>" );
				
			}
        }

		protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GridView1.PageIndex = e.NewPageIndex;
			GetTeacher();
		}

		protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			GridView1.EditIndex = -1;
			GetTeacher();
		}

		protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			try
			{
				int teacherId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
				fnobj.Query("Delete from tbl_teacher where teacher_id = '" + teacherId + "'");
				lblMsg.Text = "Teacher Deleted successfully!";
				lblMsg.CssClass = "alert alert-success";
				GridView1.EditIndex = -1;
				GetTeacher();
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
		{
			GridView1.EditIndex = e.NewEditIndex;
			GetTeacher();
		}

		protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			try
			{
				GridViewRow row = GridView1.Rows[e.RowIndex];
				int teacherId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
				
				string Name = (row.FindControl("txtName") as TextBox).Text;
				string Mobile = (row.FindControl("txtMobile") as TextBox).Text;
				string Password = (row.FindControl("txtPassword") as TextBox).Text;
				string Address = (row.FindControl("txtAddress") as TextBox).Text;
				fnobj.Query("Update tbl_teacher set name = '" + Name.Trim() + "', mobile = '" + Mobile.Trim() + "', address = '" + Address.Trim() + "', password = '" + Password.Trim() + "' where teacher_id = '" + teacherId + "'");
				lblMsg.Text = "Teacher Updated successfully!";
				lblMsg.CssClass = "alert alert-success";
				GridView1.EditIndex = -1;
				GetTeacher();
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		protected void GridView1_PageIndexChanged(object sender, EventArgs e)
		{

		}
	}
}