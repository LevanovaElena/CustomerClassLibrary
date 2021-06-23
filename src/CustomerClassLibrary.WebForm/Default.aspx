<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CustomerClassLibrary.WebForm._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="container-fluide h-100">
    <div class="row">

        <div class="form-group">
            <asp:Label runat="server" Text="First Name"></asp:Label>
            <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server"></asp:TextBox>
        </div>    
    
        <div class="form-group">
            <asp:Label runat="server" Text="Last Name"></asp:Label>
            <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label runat="server" Text="PhoneNumber"></asp:Label>
            <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" runat="server"></asp:TextBox>
        </div>    
    
        <div class="form-group">
            <asp:Label runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label runat="server" Text="Total Purchases Amount"></asp:Label>
            <asp:TextBox ID="txtTotalPurchasesAmount" CssClass="form-control" runat="server"></asp:TextBox>
        </div>    
    
        <div class="form-group">
            <asp:Label runat="server" Text="Notes"></asp:Label>
        </div>
        <div class="form-group">
            <asp:ListBox id="listBoxNotes" 
                   Rows="4"
                   Width="500px"
                   SelectionMode="Single" 
                   runat="server">

            </asp:ListBox>
        </div>
        <div class="form-group">
            <asp:Label runat="server" Text="Addresses"></asp:Label>
        </div>
        <div class="form-group">
            <asp:Table id="Table1" runat="server"
                CellPadding="10" 
                GridLines="Both"
                HorizontalAlign="Center"
                class="table">
                <asp:TableHeaderRow CssClass="">
                        <asp:TableHeaderCell id="AddressLine" Scope="Column">
                            Address Line
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell id="AddressLine2" Scope="Column">
                            Address Line2
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell id="AddressType" Scope="Column">
                                AddressType
                            </asp:TableHeaderCell>
                        <asp:TableHeaderCell id="City" Scope="Column">
                                City
                            </asp:TableHeaderCell>
                        <asp:TableHeaderCell id="PostalCode" Scope="Column">
                            PostalCode
                        </asp:TableHeaderCell>
                    <asp:TableHeaderCell id="State" Scope="Column">
                            State
                        </asp:TableHeaderCell>
                    <asp:TableHeaderCell id="Country" Scope="Column">
                            Country
                        </asp:TableHeaderCell>
                    <asp:TableHeaderCell id="idCustomer" Scope="Column"  Visible="false">
                            idCustomer
                        </asp:TableHeaderCell>
                </asp:TableHeaderRow>

    </asp:Table>
        </div>
    <asp:Button runat="server" CssClass="btn btn-primary" 
                OnClick="OnSaveClick" 
                Text="Save" />
    </div>

    <nav aria-label="Page navigation example">
  <ul class="pagination">
    <li class="page-item">
        <a class="page-link" href="#" aria-label="Previous">
            <span aria-hidden="true">&laquo;</span>
        </a>
        </li>
        <li class="page-item"><a class="page-link" href="#">1</a></li>
        <li class="page-item"><a class="page-link" href="#">2</a></li>
        <li class="page-item"><a class="page-link" href="#">3</a></li>
        <li class="page-item">
          <a class="page-link" href="#" aria-label="Next">
        <span aria-hidden="true">&raquo;</span>
      </a>
    </li>
  </ul>
</nav>
</div>
</asp:Content>
