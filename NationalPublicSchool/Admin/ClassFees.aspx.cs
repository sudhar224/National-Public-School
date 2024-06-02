﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static NationalPublicSchool.Models.CommonFn;

namespace NationalPublicSchool.Admin
{
	public partial class ClassFees : System.Web.UI.Page
	{
		Commonfnx fnobj = new Commonfnx();
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				GetClass();
				GetFees();
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
				DataTable dt = fnobj.Fetch("Select * from tbl_fees where class_id = '" + ddlClass.SelectedItem.Value + "'");
				if (dt.Rows.Count == 0)
				{
					string query = "Insert into tbl_fees values ('"+ddlClass.SelectedItem.Value +"','" + txtFeeAmounts.Text.Trim() + "')";
					fnobj.Query(query);
					lblMsg.Text = "Inserted successfully!";
					lblMsg.CssClass = "alert alert-success";
					ddlClass.SelectedIndex = 0;
					txtFeeAmounts.Text = string.Empty;
					GetFees();
				}
				else
				{
					lblMsg.Text = "Entered Fees already exists for <b>'"+ classval +"'</b>";
					lblMsg.CssClass = "alert alert-danger";
				}
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		private void GetFees()
		{
			DataTable dt = fnobj.Fetch("Select Row_NUMBER() over (order by (Select 1)) as [Sr.No], f.fees_id, f.class_id, c.class_name, f.fees_amount from tbl_fees f inner join tbl_class c on c.class_id = f.class_id");
			GridView1.DataSource = dt;
			GridView1.DataBind();
		}

		protected void GridView1_PageIndexChanged(object sender, EventArgs e)
		{

		}

		protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GridView1.PageIndex = e.NewPageIndex;
			GetFees();
		}

		protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			GridView1.EditIndex = -1;
			GetFees() ;
		}

		protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			try
			{
				int FeesId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
				fnobj.Query("Delete from tbl_fees where fees_id = '" + FeesId + "'");
				lblMsg.Text = "Fees Deleted successfully!";
				lblMsg.CssClass = "alert alert-success";
				GridView1.EditIndex = -1;
				GetFees();
			}
			catch(Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
		{
			GridView1.EditIndex = e.NewEditIndex; 
			GetFees();
		}

		protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			try
			{
				GridViewRow row = GridView1.Rows[e.RowIndex];
				int FeesId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
				string Feesamt = (row.FindControl("TextBox1") as TextBox).Text;
				fnobj.Query("Update tbl_fees set fees_amount = '" + Feesamt.Trim() + "' where fees_id = '" + FeesId + "'");
				lblMsg.Text = "Fees Updated successfully!";
				lblMsg.CssClass = "alert alert-success";
				GridView1.EditIndex = -1;
				GetFees();
			}
			catch(Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}
		
	}
}