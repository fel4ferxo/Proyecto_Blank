using Microsoft.AspNet.SignalR;

namespace Web.Sockets
{
    [Microsoft.AspNet.SignalR.Hubs.HubName("NotificacionesHub")]
    public class NotificacionesHub : Hub
    {
        public void reload()
        {
            Clients.All.reloadTable();
        }
        
    }
}