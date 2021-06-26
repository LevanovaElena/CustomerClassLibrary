<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CustomerClassLibrary.WebForm._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="container-fluide h-100 mt-3">

    <table class="table">
        <% foreach (var customer in CustomerList) { %>

            <tr>
                <td><a class="text-decoration-none text-warning" href="CustomerEdit?idCustomer=<%= customer.IdCustomer %>"><%= customer.FirstName %> <%= customer.LastName %></a></td>
                <td><%= customer.Email %></td>
                <td><%= customer.PhoneNumber %></td>
                <td><%= customer.TotalPurchasesAmount %></td>
                <td><%= customer.Notes.Count %></td>
                <td><%= customer.AddressesList.Count %></td>
                <td> 
                    <a class="btn btn-success" href="CustomerEdit?idCustomer=<%= customer.IdCustomer %>">Edit</a>
                    <a class="btn btn-danger" href="Default?idCustomerDelete=<%= customer.IdCustomer %>">Delete</a>
                </td>
            </tr>

        <% } %>
    </table>



    </div>

    <nav aria-label="Page navigation example">
      <ul class="pagination">
        <li class="page-item">
            <asp:Button ID="btnPrev" runat="server" CssClass="page-link" 
                CommandName="numberCustomer"
                CommandArgument="0"
                Text="<<"
                OnCommand="OnPrevClick"  />
        
            </li>
           <% for (int i = 0; i <= CountOfPaginationButton; i++)
               {
                   int k = i + 1;%>

           <li class="page-item">
               <a class="page-link" href="Default?pagePagination=<%= k %>"><%= k %></a>
           </li>
           <%} %>
            <li class="page-item">
              <asp:Button runat="server" CssClass="page-link"
                  ID="btnNext"
                  CommandName="numberCustomer"
                CommandArgument="0"
                Text=">>"
                OnCommand="OnNextClick" />
        </li>
      </ul>
</nav>
</div>
</asp:Content>
