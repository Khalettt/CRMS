<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Customers.aspx.cs" Inherits="Customers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1 class="m-0 text-dark">Customer Registration</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><a href="#">Home</a></li>
            <li class="breadcrumb-item active">Customers v2</li>
          </ol>
        </div>
      </div>
    </div>
  </div>

  <section class="content">
    <div class="container-fluid">
      <div class="row">
        <div class="col-md-12">
          <div class="card card-primary">
            <div class="card-header">
              <h2 class="card-title">
                <asp:Label ID="UpdateText" runat="server" Text="Add New Customer Registration"></asp:Label>
              </h2>
            </div>
            <div class="card-body">
              <div class="form-group">
                <label for="exampleInputEmail1">FullName:</label>
                <asp:TextBox ID="txtFullname" runat="server" class="form-control" placeholder="Enter fullname..."></asp:TextBox>
              </div>

              <div class="form-group">
                <label for="exampleInputEmail1">Username:</label>
                <asp:TextBox ID="txtUsername" runat="server" class="form-control" placeholder="Enter Username..."></asp:TextBox>
              </div>

              <div class="form-group">
                <label for="exampleInputEmail1">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" class="form-control" TextMode="Email" placeholder="Enter Email..."></asp:TextBox>
              </div>

              <div class="form-group">
                <label for="exampleInputEmail1">Phone Number:</label>
                <asp:TextBox ID="txtPhone" runat="server" class="form-control" placeholder="Enter Phone no..."></asp:TextBox>
              </div>

               <div class="form-group">
                <label for="exampleInputEmail1">Address:</label>
                <asp:TextBox ID="txtAddress" runat="server" class="form-control" placeholder="Enter Address..."></asp:TextBox>
              </div>

            </div>

            <div class="card-footer">
              <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" OnClick="Button1_Click" />
            </div>
          </div>
        </div>
      </div>

      <div class="row">
        <div class="col-12">
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">Table of Customers</h3>
              <div class="card-tools">
                <div class="input-group input-group-sm" style="width: 150px;">
                  <input type="text" name="table_search" class="form-control float-right" placeholder="Search">
                  <div class="input-group-append">
                    <button type="submit" class="btn btn-default"><i class="fas fa-search"></i></button>
                  </div>
                </div>
              </div>
            </div>

            <div class="card-body table-responsive p-0">
              <asp:GridView ID="dvgCustomers" Class="table table-hover text-nowrap" runat="server"
                Width="1276px"
                OnSelectedIndexChanged="dvgCustomers_SelectedIndexChanged" DataKeyNames="CustomerID"
                OnRowDeleting="dvgCustomers_RowDeleting">
                <Columns>
                  <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-primary" HeaderText="Action" SelectText="Edit" ShowSelectButton="True" />
                  <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-danger" HeaderText="Action" ShowDeleteButton="True" DeleteText="Delete" />
                </Columns>
              </asp:GridView>
            </div>

          </div>
        </div>
      </div>
    </div>
  </section>
</asp:Content>

