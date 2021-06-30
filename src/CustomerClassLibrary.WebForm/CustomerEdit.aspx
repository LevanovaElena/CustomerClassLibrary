<%@ Page Title="Edit Customer" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CustomerEdit.aspx.cs" Inherits="CustomerClassLibrary.WebForm.CustomerEdit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluide h-100 mt-3">
    <div class="row">
        <asp:TextBox ID="txtIdCustomer" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
        <div class="form-group d-flex m-3">
            <asp:Label runat="server" Text="First Name" CssClass="form-label m-2" Width="180px"></asp:Label>
            <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Label runat="server" Text="" CssClass="form-label m-2 small text-danger" ID="txtFirstNameError"></asp:Label>
        </div>    
    
        <div class="form-group  d-flex m-3">

            <asp:Label runat="server" Text="Last Name" CssClass="form-label m-2" Width="180px"></asp:Label>
            <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Label runat="server" Text="" CssClass="form-label m-2 small text-danger" ID="txtLastNameError"></asp:Label>

        </div>

        <div class="form-group d-flex m-3">

            <asp:Label runat="server" Text="PhoneNumber"  CssClass="form-label m-2" Width="180px"></asp:Label>
            <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Label runat="server" Text="" CssClass="form-label m-2 small text-danger" ID="txtPhoneNumberError"></asp:Label>

        </div>    
    
        <div class="form-group d-flex m-3">
            <asp:Label runat="server" Text="Email"  CssClass="form-label m-2" Width="180px"></asp:Label>
            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Label runat="server" Text="" CssClass="form-label m-2 small text-danger" ID="txtEmailError"></asp:Label>
        </div>
        <div class="form-group d-flex m-3">
            <asp:Label runat="server" Text="Total Purchases Amount"  CssClass="form-label m-2" Width="180px"></asp:Label>
            <asp:TextBox ID="txtTotalPurchasesAmount" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Label runat="server" Text="" CssClass="form-label m-2 small text-danger" ID="txtTotalPurchasesAmountError"></asp:Label>
        </div>    
        <div class="form-group">
            <h4>Notes</h4> <asp:Label runat="server" Text="" CssClass="form-label m-2 small text-danger" ID="txtNotesError"></asp:Label>
            <asp:Repeater id="repeaterNotes" ItemType="System.string" OnItemCommand="repeaterNotes_ItemCommand" runat="server">
              <HeaderTemplate>
                 <table class="table table-light">
              </HeaderTemplate>
             
              <ItemTemplate>
                 <tr>
                    <td> <asp:TextBox ID="Notes" Text='<%# this.GetDataItem().ToString() %>' CssClass="form-control" runat="server"></asp:TextBox>
                    </td>
                     <td><asp:LinkButton runat="server" CommandName="deleteNote"
                                    CommandArgument='<%# this.GetDataItem().ToString() %>' ID="deleteNote">
                                    Detete</asp:LinkButton></td>
                 </tr>
              </ItemTemplate>

              <FooterTemplate>
                 </table>
              </FooterTemplate>
             
           </asp:Repeater>
            <asp:Button runat="server" CssClass="btn btn-primary" 
                    OnClick="OnSaveNotes" 
                    Text="New Note" />
        </div>
        <div class="m-3">
            <h4 >Addresses</h4><asp:Label runat="server" Text="" CssClass="form-label m-2 small text-danger" ID="lblAddressesError"></asp:Label>
        </div>
        <asp:Label ID="lblNumberCustomer" runat="server" Text="0" Visible="false"></asp:Label>

        <div class="form-group">
            <asp:Repeater id="Repeater" OnItemCommand="Repeater_ItemCommand" runat="server">
              <HeaderTemplate>
                 <table class="table table-light ">
                    <tr>
                       <th colspan="2">Address Line</th>
                       <th  colspan="2">Address Line2</th>
                        <th  colspan="2">AddressType</th>
                       <th  colspan="2">City</th>
                        <th  colspan="2">PostalCode</th>
                        <th colspan="2">Country</th>
                        <th colspan="2">State</th>
                        <th colspan="2"></th>
                        <th colspan="2"></th>
                        <th colspan="2"></th>
<%--                       <th>idAddress</th>
                        <th>idCustomer</th>--%>
                        
                    </tr>
              </HeaderTemplate>
             
              <ItemTemplate>
                 <tr>
                    <td colspan="2"> 
                        <asp:TextBox ID="AddressLine" Text='<%# Eval("AddressLine") %>' CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:Label runat="server" Text='<%# Eval("AddressLineError") %>' CssClass="form-label m-2 small text-danger" ID="AddressLineError"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="AddressLine2" Text='<%# Eval("AddressLine2") %>' CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:Label runat="server" Text='<%# Eval("AddressLine2Error")%>' CssClass="form-label m-2 small text-danger" ID="AddressLine2Error"></asp:Label>
                    </td>
                     <td colspan="2">  
                         <asp:TextBox ID="TypeAddress" Text='<%# Eval("TypeAddress") %>' CssClass="form-control" runat="server"></asp:TextBox>
                          <asp:Label runat="server" Text='<%# Eval("TypeAddressError") %>' CssClass="form-label m-2 small text-danger" ID="TypeAddressError"></asp:Label>
                     </td>
                    <td colspan="2">
                        <asp:TextBox ID="City" Text='<%# Eval("City") %>' CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:Label runat="server" Text='<%# Eval("CityError") %>' CssClass="form-label m-2 small text-danger" ID="CityError"></asp:Label>
                    </td>
                     <td colspan="2">
                         <asp:TextBox ID="PostalCode" Text='<%# Eval("PostalCode") %>' CssClass="form-control" runat="server"></asp:TextBox>
                         <asp:Label runat="server" Text='<%# Eval("PostalCodeError") %>' CssClass="form-label m-2 small text-danger" ID="PostalCodeError"></asp:Label>
                     </td>
                    <td colspan="2">
                        <asp:TextBox ID="Country" Text='<%# Eval("Country") %>' CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:Label runat="server" Text='<%# Eval("CountryError") %>' CssClass="form-label m-2 small text-danger" ID="CountryError"></asp:Label>
                    </td>
                     <td colspan="2">
                         <asp:TextBox ID="State" Text='<%# Eval("State") %>' CssClass="form-control" runat="server"></asp:TextBox>
                         <asp:Label runat="server" Text='<%# Eval("StateError") %>' CssClass="form-label m-2 small text-danger" ID="StateError"></asp:Label>
                     </td>
                     <td colspan="2">
                         <asp:LinkButton runat="server" CommandName="deleteAddress"
                                    CommandArgument='<%# Eval("idAddress") %>' ID="deleteAddress">
                                    Detete</asp:LinkButton>
                         <div></div>
                     </td>
                    <td colspan="2"> 
                        <asp:TextBox ID="idAddress" Text='<%# Eval("idAddress") %>' CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                        <div></div>
                    </td>
                     <td colspan="2"><asp:TextBox ID="idCustomer" Text='<%# Eval("idCustomer") %>' CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                         <div></div>
                     </td>
                 </tr>
              </ItemTemplate>

              <FooterTemplate>
                 </table>
              </FooterTemplate>
             
           </asp:Repeater>

                         <asp:Button runat="server" CssClass="btn btn-primary" 
                    OnClick="OnSaveAddress" 
                    Text="NewAddress" />
        </div>





        <div class="form-group">
                  <asp:Button runat="server" CssClass="btn btn-primary" 
                    OnClick="OnSaveClick" 
                    Text="Save" />
            </div>
        
        </div>

       
    </div>
</asp:Content>
