<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Payments.aspx.cs" Inherits="Payments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <div class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1 class="m-0 text-dark">Payments</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><a href="#">Home</a></li>
            <li class="breadcrumb-item active">Payments</li>
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
              <h2 class="card-title">Add New Payment</h2>
            </div>
            <div class="card-body">
              <div class="form-group">
                <label>Rental ID:</label>
                <asp:DropDownList ID="ddlReantal"  class="form-control" runat="server" AutoPostBack="true" ></asp:DropDownList>
              </div>

               <div class="form-group">
                <label>PaymentDate:</label>
                <asp:TextBox ID="txtPaymentate" runat="server" class="form-control" TextMode="Date" ></asp:TextBox>
              </div>

              <div class="form-group">
                <label>Amount Paid:</label>
                <asp:TextBox ID="txtAmount" runat="server" class="form-control" placeholder="Enter Amount..."></asp:TextBox>
              </div>

              <div class="form-group">
                <label>Payment Method:</label>
                <asp:DropDownList ID="ddlMethod" runat="server" class="form-control">
                  <asp:ListItem Text="Select Method" Value="" />
                  <asp:ListItem Text="Cash" Value="Cash" />
                  <asp:ListItem Text="Card" Value="Card" />
                  <asp:ListItem Text="Mobile" Value="Mobile" />
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
              <h3 class="card-title">Payments Table</h3>
            </div>
            <div class="card-body table-responsive p-0">
              <asp:GridView ID="dvgPayments"  runat="server" Class="table table-hover text-nowrap"
                Width="1276px" DataKeyNames="PaymentId"
                  OnSelectedIndexChanged="dvgPayments_SelectedIndexChanged" 
                  OnRowDeleting="dvgPayments_RowDeleting">

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

