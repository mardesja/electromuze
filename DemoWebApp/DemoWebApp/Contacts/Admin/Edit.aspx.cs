using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
//using ContactManager.Models;
using DemoWebApp.Models;
namespace DemoWebApp.Contacts
{
    public partial class Edit : System.Web.UI.Page
    {
		protected DemoWebApp.Models.ApplicationDbContext _db = new DemoWebApp.Models.ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // This is the Update methd to update the selected Contacts item
        // USAGE: <asp:FormView UpdateMethod="UpdateItem">
        public void UpdateItem(int  ContactId)
        {
            using (_db)
            {
                var item = _db.Contacts.Find(ContactId);

                if (item == null)
                {
                    // The item wasn't found
                    ModelState.AddModelError("", String.Format("Item with id {0} was not found", ContactId));
                    return;
                }

                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    // Save changes here
                    _db.SaveChanges();
                    Response.Redirect("~/Contacts/Default.aspx");
                }
            }
        }

        // This is the Select method to selects a single Contacts item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public DemoWebApp.Models.Contacts GetItem([FriendlyUrlSegmentsAttribute(0)]int? ContactId)
        {
            if (ContactId == null)
            {
                return null;
            }

            using (_db)
            {
                return _db.Contacts.Find(ContactId);
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
