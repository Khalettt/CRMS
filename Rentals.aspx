<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Rentals.aspx.cs" Inherits="Rentals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
  <div class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1 class="m-0 text-dark">Rental Registration</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><a href="#">Home</a></li>
            <li class="breadcrumb-item active">Rentals</li>
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
                <asp:Label ID="UpdateText" runat="server" Text="Add New Rental"></asp:Label>
              </h2>
            </div>
            <div class="card-body">
              <div class="form-group">
                <label>Customer:</label>
                <asp:DropDownList ID="ddlCustomer" runat="server" class="form-control"></asp:DropDownList>
              </div>

              <div class="form-group">
                <label>Car:</label>
                <asp:DropDownList ID="ddlCar" runat="server" class="form-control"></asp:DropDownList>
              </div>

              <div class="form-group">
                <label>User:</label>
                <asp:DropDownList ID="ddlUser" runat="server" class="form-control"></asp:DropDownList>
              </div>



              <div class="form-group">
                <label>RentDate:</label>
                <asp:TextBox ID="txtRentDate" runat="server" class="form-control" TextMode="DateTimeLocal" ></asp:TextBox>
              </div>

              <div class="form-group">
                <label>Return Date:</label>
                <asp:TextBox ID="txtReturnDate" runat="server" class="form-control" TextMode="DateTimeLocal"></asp:TextBox>
              </div>

              <div class="form-group">
                <label>Total Amount:</label>
                <asp:TextBox ID="txtTotalAmount" runat="server" class="form-control" placeholder="Enter total amount..."></asp:TextBox>
              </div>

              <div class="form-group">
                <label>Status:</label>
                <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                  <asp:ListItem Text="Ongoing" Value="Ongoing" />
                  <asp:ListItem Text="Returned" Value="Returned" />
                </asp:DropDownList>
              </div>
            </div>

            <div class="card-footer">
              <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
            </div>
          </div>
        </div>
      </div>

      <div class="row">
        <div class="col-12">
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">Table of Rentals</h3>
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
              <asp:GridView ID="dvgRentals" runat="server" Width="1276px" class="table table-hover text-nowrap"
                 DataKeyNames="RentalID"
                OnRowDeleting="gvRentals_RowDeleting"
                OnSelectedIndexChanged="gvRentals_SelectedIndexChanged">
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

