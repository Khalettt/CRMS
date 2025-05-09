<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CarMaintenance.aspx.cs" Inherits="CarMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1 class="m-0 text-dark">Car Maintenance</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><a href="#">Home</a></li>
            <li class="breadcrumb-item active">Maintenance</li>
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
              <h2 class="card-title">Add New Maintenance</h2>
            </div>
            <div class="card-body">
              <div class="form-group">
                <label>Car:</label>
                <asp:DropDownList ID="ddlCar" class="form-control" runat="server"></asp:DropDownList>
              </div>

              <div class="form-group">
                <label>Maintenance Date:</label>
                <asp:TextBox ID="txtDate" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
              </div>

              <div class="form-group">
                <label>Description:</label>
                <asp:TextBox ID="txtDescription" runat="server" class="form-control" TextMode="MultiLine" Rows="1"></asp:TextBox>
              </div>

              <div class="form-group">
                <label>Cost:</label>
                <asp:TextBox ID="txtCost" runat="server" class="form-control" placeholder="Enter Cost..."></asp:TextBox>
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
              <h3 class="card-title">Maintenance Records</h3>
            </div>77
            <div class="card-body table-responsive p-0">
              <asp:GridView ID="dvgMaintenance"  runat="server" Class="table table-hover text-nowrap"
                Width="1276px" DataKeyNames="MaintenanceID"
                OnSelectedIndexChanged="dvgMaintenance_SelectedIndexChanged"
                OnRowDeleting="dvgMaintenance_RowDeleting">
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

