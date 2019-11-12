<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InventoryManagementSolution._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <hr />
        <asp:Label ID="lbl_search" runat="server" Text="Filter products by name: " />
        <asp:TextBox ID="tbx_search" runat="server" AutoPostBack="true" OnTextChanged="tbx_search_TextChanged" />
        <asp:DropDownList ID="ddl_categories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_categories_SelectedIndexChanged" />
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gv_inventory" runat="server" GridLines="None" AllowPaging="True" PageSize="25" AllowSorting="True" OnSorting="gv_inventory_Sorting" OnPageIndexChanging="gv_inventory_PageIndexChanging"
                    OnRowDataBound="gv_inventory_RowDataBound" AutoGenerateColumns="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                    <Columns>
                        <asp:BoundField DataField="product_name" HeaderText="Product" />
                        <%--<asp:BoundField DataField="status" HeaderText="Status" />--%>
                        <asp:BoundField DataField="quantity" HeaderText="Inventory Count" SortExpression="quantity" />
                        <asp:BoundField DataField="brand_name" HeaderText="Brand" />
                        <asp:BoundField DataField="category_name" HeaderText="Category" />
                        <asp:BoundField DataField="list_price" HeaderText="Price" SortExpression="list_price" />
                        <asp:BoundField DataField="total_value" HeaderText="Total Value" SortExpression="total_value" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="tbx_search" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
