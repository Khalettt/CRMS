<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Cars.aspx.cs" Inherits="Cars" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1 class="m-0 text-dark">Car Registration</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><a href="#">Home</a></li>
            <li class="breadcrumb-item active">Cars v2</li>
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
                <asp:Label ID="UpdateText" runat="server" Text="Add New Car"></asp:Label>
              </h2>
            </div>
            <div class="card-body">
              <div class="form-group">
                <label>Make:</label>
                <asp:TextBox ID="txtMake" runat="server" class="form-control" placeholder="Enter car make..."></asp:TextBox>
              </div>

              <div class="form-group">
                <label>Model:</label>
                <asp:TextBox ID="txtModel" runat="server" class="form-control" placeholder="Enter car model..."></asp:TextBox>
              </div>

              <div class="form-group">
                <label>Year:</label>
                <asp:TextBox ID="txtYear" runat="server" class="form-control" placeholder="Enter year..."></asp:TextBox>
              </div>

              <div class="form-group">
                <label>Registration Number:</label>
                <asp:TextBox ID="txtRegistrationNumber" runat="server" class="form-control" placeholder="Enter registration number..."></asp:TextBox>
              </div>

              <div class="form-group">
                <label>Status:</label>
                <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                  <asp:ListItem Text="available" Value="available" />
                  <asp:ListItem Text="rented" Value="rented" />
                  <asp:ListItem Text="maintenance" Value="maintenance" />
                </asp:DropDownList>
              </div>

              <div class="form-group">
                <label>Rental Price Per Day:</label>
                <asp:TextBox ID="txtRentalPrice" runat="server" class="form-control" placeholder="Enter rental price..."></asp:TextBox>
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
              <h3 class="card-title">Table of Cars</h3>
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
              <asp:GridView ID="dvgCars" Class="table table-hover text-nowrap" runat="server"
                Width="1276px"
                OnSelectedIndexChanged="dvgCars_SelectedIndexChanged" DataKeyNames="car_id"
                OnRowDeleting="dvgCars_RowDeleting">
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

