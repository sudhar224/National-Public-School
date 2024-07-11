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
	public partial class MarkDetails : System.Web.UI.Page
	{
		Commonfnx fnobj = new Commonfnx();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				GetClass();
				GetMarks();
			}
		}

		private void GetMarks()
		{
			DataTable dt = fnobj.Fetch("select ROW_NUMBER() over(order by(select 1)) as [Sr.No],e.exam_id, e.class_id, c.class_name, e.subject_id, s.subject_name ,e.rollno, e.total_marks, e.out_ofmarks from tbl_exam e inner join tbl_class c on c.class_id = e.class_id inner join tbl_subject s on s.subject_id = e.subject_id");
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
				string rollNo = txtRoll.Text.Trim();
				DataTable dt = fnobj.Fetch("select ROW_NUMBER() over(order by(select 1)) as [Sr.No],e.exam_id, e.class_id, c.class_name, e.subject_id, s.subject_name ,e.rollno, e.total_marks, e.out_ofmarks from tbl_exam e inner join tbl_class c on c.class_id = e.class_id inner join tbl_subject s on s.subject_id = e.subject_id where e.class_id = '" + classId +"' and e.rollno = '"+ rollNo + "'");
				GridView1.DataSource = dt;
				GridView1.DataBind();
			}
			catch (Exception ex) 
			{
				throw;
			}
        }

		protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GridView1.PageIndex = e.NewPageIndex;

		}
	}
}