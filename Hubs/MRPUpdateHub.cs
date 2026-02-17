using Microsoft.AspNetCore.SignalR;

namespace FirstProject.Api.Hubs
{
    public class MRPUpdateHub : Hub
    {
        public async Task SendMRPUpdate(int productId, decimal newMrp)
        {
            await Clients.All.SendAsync("ReceiveMRPUpdate", productId, newMrp);
        }
    }
}
