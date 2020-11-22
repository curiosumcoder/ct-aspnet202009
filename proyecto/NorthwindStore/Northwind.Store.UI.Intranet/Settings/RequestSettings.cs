using Microsoft.AspNetCore.Mvc;
using Northwind.Store.Notification;
using Northwind.Store.UI.Intranet.Extensions;

namespace Northwind.Store.UI.Web.Settings
{
    public class RequestSettings
    {
        Controller controller = null;
        public RequestSettings(Controller c)
        {
            controller = c;
        }

        public Message Message
        {
            get
            {
                return controller.TempData.GetFromJson<Message>("Message"); ;
            }
            set
            {
                controller.TempData.SetAsJson("Message", value);
            }
        } 
    }
}