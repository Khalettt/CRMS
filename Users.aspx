<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
  <div class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1 class="m-0 text-dark">User Registration</h1>
        </div>
        <!-- /.col -->
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><a href="#">Home</a></li>
            <li class="breadcrumb-item active">Users v2</li>
          </ol>
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
    </div>
    <!-- /.container-fluid -->
  </div>


  <section class="content">
    <div class="container-fluid">
      <div class="row">
        <!-- left column -->
        <div class="col-md-12">
          <!-- general form elements -->
          <div class="card card-primary">
            <div class="card-header">
              <h2 class="card-title">
                <asp:Label ID="UpdateText" runat="server" Text="Add New User Registration"></asp:Label></h2>
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
                <label for="exampleInputEmail1">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" class="form-control" TextMode="Password" placeholder="Enter password..."></asp:TextBox>
              </div>
              <div class="form-group">
                <label for="exampleInputEmail1">confrim Password:</label>
                <asp:TextBox ID="txtConfirm" runat="server" class="form-control" TextMode="Password" placeholder="Enter confirm password..."></asp:TextBox>
              </div>
              <div class="input-group">
                <div class="custom-file">
                  <asp:FileUpload ID="FuUsersPhoto" class="custom-file-input" runat="server" />
                  <label class="custom-file-label" for="exampleInputFile">Choose file</label>
                </div>
              </div>


            </div>
            <!-- /.card-body -->

            <div class="card-footer">
              <asp:Button ID="btn" class="btn btn-primary" runat="server" Text="Save" OnClick="Button1_Click" />
            </div>
          </div>
          <!-- /.card -->

          <!-- Form Element sizes -->

        </div>
        <!-- /.row -->
      </div>



      <div class="row">
        <div class="col-12">
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">Table of Users</h3>
              <div class="card-tools">
                <div class="input-group input-group-sm" style="width: 150px;">
                  <input type="text" name="table_search" class="form-control float-right" placeholder="Search">

                  <div class="input-group-append">
                    <button type="submit" class="btn btn-default"><i class="fas fa-search"></i></button>
                  </div>
                </div>
              </div>
            </div>
            <!-- /.card-header -->
            <div class="card-body table-responsive p-0">
              <asp:GridView ID="dvgUsers" runat="server" Class="table table-hover text-nowrap"
                Width="1276px"
                OnSelectedIndexChanged="dvgUsers_SelectedIndexChanged" DataKeyNames="Userid"
                OnRowDeleting="dvgUsers_RowDeleting">
                <Columns>
                  <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-primary" HeaderText="Action" SelectText="Edit" ShowSelectButton="True" />
                  <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-danger" HeaderText="Action" ShowDeleteButton="True" DeleteText="Delete" />
                </Columns>

              </asp:GridView>
            </div>
            <!-- /.card-body -->
          </div>
          <!-- /.card -->
        </div>
      </div>
    </div>
    <!-- /.container-fluid -->
  </section>


</asp:Content>

