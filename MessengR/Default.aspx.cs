using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace MessengR
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var allUsers = Membership.GetAllUsers();
            users.DataSource = from MembershipUser u in allUsers
                               let online = Chat.IsOnline(u.UserName)
                               select new
                               {
                                   Name = u.UserName,
                                   Online = online
                               };
            users.DataBind();
        }
    }
}