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
    public partial class Delete : System.Web.UI.Page
    {
		protected DemoWebApp.Models.ApplicationDbContext _db = new DemoWebApp.Models.ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // This is the Delete methd to delete the selected Contacts item
        // USAGE: <asp:FormView DeleteMethod="DeleteItem">
        public void DeleteItem(int ContactId)
        {
            using (_db)
            {
                var item = _db.Contacts.Find(ContactId);

                if (item != null)
                {
                    _db.Contacts.Remove(item);
                    _db.SaveChanges();
                }
            }
            Response.Redirect("~/Contacts/Default.aspx");
        }

        // This is the Select methd to selects a single Contacts item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public DemoWebApp.Models.Contacts GetItem([FriendlyUrlSegmentsAttribute(0)]int? ContactId)
        {
            if (ContactId == null)
            {
                return null;
            }

            using (_db)
            {
	            return _db.Contacts.Where(m => m.ContactId == ContactId).FirstOrDefault();
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

