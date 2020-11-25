using Northwind.Store.Data;
using Northwind.Store.Model;
using Northwind.Store.Notification;
using System;
using System.Threading.Tasks;

namespace Northwind.Store.Business
{
    public class OrderB
    {
        private readonly BaseRepository<Order, int> _oR = null;
        public OrderB(BaseRepository<Order, int> oR)
        {
            _oR = oR;
        }

        public async Task Create(Order o, Notifications nm = null)
        {
            // Validaciones y acciones sobre otras tablas
            if (nm != null)
            {
                if (o.OrderDetails.Count > 0)
                {
                    Message m = new Message()
                    {
                        Title = "La orden no contiene detalle."
                    };
                    nm.Add(m);
                }

                if (o.State != ModelState.Added)
                {
                    Message m = new Message()
                    {
                        Title = "La order no viene en el correcto estado"
                    };
                    nm.Add(m);
                }

                if (nm.Count == 0)
                {
                    await _oR.Save(o, nm);
                }
            }
            else
            {
                await _oR.Save(o, nm);
            }
        }
    }
}
