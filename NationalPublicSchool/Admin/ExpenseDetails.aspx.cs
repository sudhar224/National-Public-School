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
	public partial class ExpenseDetails : System.Web.UI.Page
	{
		Commonfnx fnobj = new Commonfnx();
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				GetExpenceDetails();
			}
		}

		private void GetExpenceDetails()
		{
			DataTable dt = fnobj.Fetch("Select ROW_NUMBER() over(Order by(Select 1)) as [Sr.No], e.expence_id, e.class_id, c.class_name , e.subject_id, s.subject_name, e.charge_amount from tbl_expense e inner join tbl_class c on e.class_id = c.class_id inner join  tbl_subject  s on e.subject_id = s.subject_id");
			GridView1.DataSource = dt;
			GridView1.DataBind();
		}
	}
}