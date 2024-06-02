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
	public partial class Student : System.Web.UI.Page
	{
		Commonfnx fnobj = new Commonfnx();
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				GetClass();
				GetStudents();
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
				if (ddlGender.SelectedValue != "0")
				{
					string rollNo = txtRoll.Text.Trim();
					DataTable dt = fnobj.Fetch("Select * from tbl_student where class_id = '"+ddlClass.SelectedValue + "' and rollno = '" + rollNo + "'");
					if (dt.Rows.Count == 0)
					{
						string query = "Insert into tbl_student values ('" + txtName.Text.Trim() + "','" + txtDoB.Text.Trim() + "','" +
							ddlGender.SelectedValue + "','" + txtMobile.Text.Trim() + "','" + txtRoll.Text.Trim() + "','" + txtAddress.Text.Trim() + "','" + ddlClass.SelectedValue + "')";
						fnobj.Query(query);
						lblMsg.Text = "Inserted successfully!";
						lblMsg.CssClass = "alert alert-success";
						ddlGender.SelectedIndex = 0;
						txtName.Text = string.Empty;
						txtDoB.Text = string.Empty;
						txtMobile.Text = string.Empty;
						txtRoll.Text = string.Empty;
						txtAddress.Text = string.Empty;
						ddlClass.SelectedIndex = 0;
						GetStudents();
					}
					else
					{
						lblMsg.Text = "Entered   RollNo.<b>'" + rollNo + "'</b> already exists for selected class!";
						lblMsg.CssClass = "alert alert-danger";
					}
				}
				else
				{
					lblMsg.Text = "Gender is required!";
					lblMsg.CssClass = "alert alert-danger";
				}
			}
			catch (Exception ex)
			{
				Response.Write("<script> alert('" + ex.Message + "'); </script>");

			}
		}

		private void GetStudents()
		{
			DataTable dt = fnobj.Fetch("Select ROW_NUMBER() OVER(ORDER BY (SELECT 1))as [Sr.No], s.student_id, s.[name], s.DOB, s.gender, s.mobile, s.rollno, s.[address],c.class_id, c.class_name from tbl_student s inner join tbl_class c on c.class_id = s.class_id" );
			GridView1.DataSource = dt;
			GridView1.DataBind();
		}

		protected void GridView1_PageIndexChanged(object sender, EventArgs e)
		{

		}

		protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GridView1.PageIndex = e.NewPageIndex;
			GetStudents();
		}

		protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			GridView1.EditIndex = -1;
			GetStudents() ;
		}

		protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
		{
			GridView1.EditIndex = e.NewEditIndex; 
			GetStudents();
		}

		protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			try
			{
				GridViewRow row = GridView1.Rows[e.RowIndex];
				int studentId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

				string Name = (row.FindControl("txtName") as TextBox).Text;
				string Mobile = (row.FindControl("txtMobile") as TextBox).Text;
				string RollNo = (row.FindControl("txtRollNo") as TextBox).Text;
				string Address = (row.FindControl("txtAddress") as TextBox).Text;
				string ClassId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[4].FindControl("ddlClass")).SelectedValue;
				fnobj.Query("Update tbl_student set name = '" + Name.Trim() + "', mobile = '" + Mobile.Trim() + "', address = '" + Address.Trim() + "', rollno = '" + RollNo.Trim() + "', class_id = '" + ClassId + "' where student_id = '" + studentId + "'");
				lblMsg.Text = "Student Updated successfully!";
				lblMsg.CssClass = "alert alert-success";
				GridView1.EditIndex = -1;
				GetStudents();
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
			{
			
			  	DropDownList ddlClass = (DropDownList)e.Row.FindControl("ddlClass");
			  	DataTable dt = fnobj.Fetch("Select * from tbl_class ");
				ddlClass.DataSource = dt;
				ddlClass.DataTextField = "class_name";
				ddlClass.DataValueField = "class_id";
				ddlClass.DataBind();
				ddlClass.Items.Insert(0, "Select Class");
			  	string selectedClass = DataBinder.Eval(e.Row.DataItem,"class_name").ToString();
			    ddlClass.Items.FindByText(selectedClass).Selected = true;
			}
		}
	}
}