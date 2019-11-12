using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagementSolution
{
    public partial class _Default : Page
    {
        InventoryAdapter adapter = new InventoryAdapter();
        DataTable table = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<string> categories = adapter.GetCategories();
                ddl_categories.DataSource = categories;
                ddl_categories.DataBind();

                table = adapter.GetInventory();
                Session["InventoryTable"] = table;
                gv_inventory.DataSource = table;
                gv_inventory.DataBind();
            }
        }

        protected void gv_inventory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text == "0")
                    e.Row.Cells[1].BackColor = System.Drawing.Color.OrangeRed;
            }
        }

        public SortDirection dir
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }
            set
            {
                ViewState["dirState"] = value;
            }
        }

        protected void gv_inventory_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (dir == SortDirection.Ascending)
            {
                dir = SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                dir = SortDirection.Ascending;
                sortingDirection = "Asc";
            }

            DataTable table = Session["InventoryTable"] as DataTable;
            if (table != null)
            {
                table.DefaultView.Sort = e.SortExpression + " " + sortingDirection;
                gv_inventory.DataSource = Session["InventoryTable"];
                gv_inventory.DataBind();
            }
        }

        protected void gv_inventory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_inventory.DataSource = Session["InventoryTable"];
            gv_inventory.PageIndex = e.NewPageIndex;
            gv_inventory.DataBind();
        }

        protected void tbx_search_TextChanged(object sender, EventArgs e)
        {
            string filter = tbx_search.Text;

            table = adapter.FilterInventory(filter);
            Session["InventoryTable"] = table;
            gv_inventory.DataSource = table;
            gv_inventory.DataBind();
            tbx_search.Focus();
        }

        protected void ddl_categories_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = ddl_categories.SelectedValue;
            table = adapter.FilterByCategory(category);
            Session["InventoryTable"] = table;
            gv_inventory.DataSource = table;
            gv_inventory.DataBind();
        }
    }
}