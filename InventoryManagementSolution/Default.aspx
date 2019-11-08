<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InventoryManagementSolution._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <asp:GridView ID="gv_inventory" runat="server" DataKeyNames="product_id" PageSize="25">
            <Columns>
                <asp:BoundField DataField="product_name" HeaderText="Product" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
