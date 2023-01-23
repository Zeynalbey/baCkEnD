using Backend_Final.Contracts.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Backend_Final.Areas.Admin.Hubs
{
    [Authorize(Roles = "admin, moderator")]
    public class AlertHub : Hub
    {
        private readonly ILogger<AlertHub> _logger;
        public AlertHub(ILogger<AlertHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation($"New conenction established {Context.ConnectionId}");

            var userClaim = Context.User!.Claims.Single(c => c.Type == CustomClaimNames.ID);
            await Groups.AddToGroupAsync(Context.ConnectionId, userClaim.Value);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation($"Connection disconnected {Context.ConnectionId}");

            var userClaim = Context.User!.Claims.Single(c => c.Type == CustomClaimNames.ID);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userClaim.Value);

             await base.OnDisconnectedAsync(exception);
        }
    }
}
