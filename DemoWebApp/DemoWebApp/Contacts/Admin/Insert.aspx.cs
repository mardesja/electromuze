using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
//using ContactManager.Models;
using DemoWebApp.Models;

namespace DemoWebApp.Contacts
{
    public partial class Insert : System.Web.UI.Page
    {
		protected DemoWebApp.Models.ApplicationDbContext _db = new DemoWebApp.Models.ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // This is the Insert method to insert the entered Contacts item
        // USAGE: <asp:FormView InsertMethod="InsertItem">
        public void InsertItem()
        {
            using (_db)
            {
                var item = new DemoWebApp.Models.Contacts();

                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    // Save changes
                    _db.Contacts.Add(item);
                    _db.SaveChanges();

                    Response.Redirect("~/Contacts/Default.aspx");
                }
            }
        }

        protected void ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Cancel", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("~/Contacts/Default.aspx");
            }
        }
    }
}
