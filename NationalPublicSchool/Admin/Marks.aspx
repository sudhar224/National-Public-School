﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="Marks.aspx.cs" Inherits="NationalPublicSchool.Admin.Marks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('../images/bgsh1.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed">
        <div class="container p-md-4 p-sm-4">
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
            <h3 class="text-center">Add Marks</h3>


            <div class="row mb-3 mr-lg-5 mt-md-5">
                <div class="col-md-6">
                    <label for="ddlClass">Class</label>
                    <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Class is required!!" ControlToValidate="ddlClass" Display="Dynamic" ForeColor="Red" InitialValue="Select Class" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <label for="ddlSubject">Subject</label>
                    <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Subject is required!!" ControlToValidate="ddlSubject" Display="Dynamic" ForeColor="Red" InitialValue="Select Subject" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>

                <div class="col-md-12 mt-2">
                    <label for="txtRoll">Student Roll Number</label>
                    <asp:TextBox ID="txtRoll" runat="server" CssClass="form-control" placeholder="Enter  Roll Number" required></asp:TextBox>
                </div>

                <div class="col-md-6 mt-2">
                    <label for="txtStudMarks">Total Marks(Student Marks)</label>
                    <asp:TextBox ID="txtStudMarks" runat="server" CssClass="form-control" placeholder="Enter Total Markst" TextMode="Number" required></asp:TextBox>
                </div>

                <div class="col-md-6 mt-2">
                    <label for="txtOutOfMarks">Out Of Marks</label>
                    <asp:TextBox ID="txtOutofMarks" runat="server" CssClass="form-control" placeholder="Enter Out Of Marks" TextMode="Number" required></asp:TextBox>
                </div>
            </div>

            <div class="row mb-3 mr-lg-5 mt-md-5">
                <div class="col-md-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558C9" Text="Add Marks" OnClick="btnAdd_Click" />

                </div>
            </div>
            <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No record to display" AutoGenerateColumns="False" AllowPaging="True" PageSize="4" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="exam_id" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleted="GridView1_RowDeleted" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Sr.No" HeaderText="Sr,No" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Class">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlClassGv" runat="server" CssClass="form-control" DataSourceID="SqlDataSource1" DataTextField="class_name" DataValueField="class_id" SelectedValue='<%# Eval("class_id") %>' OnSelectedIndexChanged="ddlClassGv_SelectedIndexChanged">
                                        <asp:ListItem>Select Class</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NationalSchoolConnectionString %>" SelectCommand="SELECT * FROM [tbl_class]"></asp:SqlDataSource>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("class_name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Subject">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlSubjectGv" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("subject_name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Roll Number">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRollNoGv" runat="server" CssClass="form-control" Text='<%# Eval("rollno") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("rollno") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total Marks">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtStudMarrksGv" runat="server" CssClass="form-control" Text='<%# Eval("total_marks") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("total_marks") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Out Of Marks">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtOutOfMarksGv" runat="server" CssClass="form-control" Text='<%# Eval("out_ofmarks") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("out_ofmarks") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:CommandField CausesValidation="false" HeaderText="Operation" ShowEditButton="True" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                        </Columns>
                        <HeaderStyle BackColor="#5558c9" ForeColor="White" />
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
