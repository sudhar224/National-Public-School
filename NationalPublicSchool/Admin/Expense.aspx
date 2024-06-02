<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="Expense.aspx.cs" Inherits="NationalPublicSchool.Admin.Expense" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div style="background-image: url('../images/bgsh1.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed">
    <div class="container p-md-4 p-sm-4">
        <div>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <h3 class="text-center">Add Expense</h3>


        <div class="row mb-3 mr-lg-5 mt-md-5">
            <div class="col-md-6">
                <label for="ddlClass">Class</label>
                <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" ></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Class is required!!" ControlToValidate="ddlClass" Display="Dynamic" ForeColor="Red" InitialValue="Select Class" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </div>
            <div class="col-md-6">
                <label for="ddlSubject">Subject</label>
                <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Subject is required!!" ControlToValidate="ddlSubject" Display="Dynamic" ForeColor="Red" InitialValue="Select Subject" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </div>

            <div class="col-md-6 mt-2" >
                <label for="txtExpenseAmt">Charge Amount(Per Lecture)</label>
                <asp:TextBox ID="txtExpenseAmt" runat="server" CssClass="form-control" placeholder="Enter Charge Amount" TextMode="Number" required></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3 mr-lg-5 mt-md-5">
            <div class="col-md-3 col-md-offset-2 mb-3">
                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558C9" Text="Add Expense" OnClick="btnAdd_Click"  />

            </div>
        </div>
        <div class="row mb-3 mr-lg-5 ml-lg-5">
            <div class="col-md-8">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No record to display" AutoGenerateColumns="False" AllowPaging="True" PageSize="4" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="expence_id" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleted="GridView1_RowDeleted" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound">
                    <columns>
                        <asp:BoundField DataField="Sr.No" HeaderText="Sr,No" ReadOnly="True">
                            <itemstyle horizontalalign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Class">
                            <edititemtemplate>
                                <asp:DropDownList ID="ddlClassGv" runat="server" CssClass="form-control" DataSourceID="SqlDataSource1" DataTextField="class_name" DataValueField="class_id" SelectedValue='<%# Eval("class_id") %>' OnSelectedIndexChanged="ddlClassGv_SelectedIndexChanged">
                                    <asp:ListItem>Select Class</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NationalSchoolConnectionString %>" SelectCommand="SELECT * FROM [tbl_class]"></asp:SqlDataSource>
                            </edititemtemplate>
                            <itemtemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("class_name") %>'></asp:Label>
                            </itemtemplate>
                            <itemstyle horizontalalign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Subject">
                            <edititemtemplate>
                               <asp:DropDownList ID="ddlSubjectGv" runat="server" CssClass="form-control" >
                                 </asp:DropDownList>
                            </edititemtemplate>
                            <itemtemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("subject_name") %>'></asp:Label>
                            </itemtemplate>
                            <itemstyle horizontalalign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Charge Rs.(Per Lecture)">
                            <edititemtemplate>
                               <asp:TextBox ID="txtExpenseAmt" runat="server" CssClass="form-control" Text='<%# Eval("charge_amount") %>' TextMode="Number" ></asp:TextBox>
                            </edititemtemplate>
                            <itemtemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("charge_amount") %>'></asp:Label>
                            </itemtemplate>
                            <itemstyle horizontalalign="Center" />
                        </asp:TemplateField>

                        <asp:CommandField CausesValidation="false" HeaderText="Operation" ShowEditButton="True" ShowDeleteButton="true">
                            <headerstyle horizontalalign="Center" />
                            <itemstyle horizontalalign="Center" />
                        </asp:CommandField>
                    </columns>
                    <headerstyle backcolor="#5558c9" forecolor="White" />
                </asp:GridView>
            </div>
        </div>

    </div>
</div>

</asp:Content>
