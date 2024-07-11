<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="EmployeeAttendance.aspx.cs" Inherits="NationalPublicSchool.Admin.EmployeeAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('../images/bgsh1.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed">
        <div class="container p-md-4 p-sm-4">
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
            <h3 class="text-center">Teacher Attendance</h3>


            <div class="row mb-3 mr-lg-5 mt-md-5">
                <div class="col-md-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558C9" Text="Add Class" OnClick="btnAdd_Click" />
                </div>
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-6">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" DataKeyNames="class_id" AutoGenerateColumns="False" EmptyDataText="No Record to display" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowPaging="true" PageSize="4">
                        <Columns>

                            <asp:TemplateField HeaderText="Class">

                                <ItemTemplate>
                                    <div class="form-check-inline">
                                        <asp:RadioButton ID="RadioButton1" runat="server" Text="Present" Checked="true" GroupName="attendance" CssClass="form-check-input" />
                                    </div>
                                    <div class="form-check-inline">
                                        <asp:RadioButton ID="RadioButton2" runat="server" Text="Absent"  GroupName="attendance" CssClass="form-check-input"/>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"/>
                            </asp:TemplateField>
                            <asp:CommandField CausesValidation="False" HeaderText="Operation" ShowEditButton="True"/>
                        </Columns>
                        <HeaderStyle BackColor="#5558c9" ForeColor="White" />
                    </asp:GridView>
                </div>
            </div>

            <div class="row mb-3 mr-lg-5 mt-md-5">
                <div class="col-md-6 col-lg-4 col-xl-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnMarkAttendance" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558C9" Text="Mark Attendance" OnClick="btnMarkAttendance_Click" />
                </div>
            </div>

        </div>
    </div>

</asp:Content>
