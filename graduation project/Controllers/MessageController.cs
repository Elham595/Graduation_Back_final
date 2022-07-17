using graduation_project.Models;
using graduation_project.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly HashSet<UserConnection> _connections;

        public MessageController(IMessageService messageService,HashSet<UserConnection> connections)
        {
            _messageService = messageService;
            _connections = connections;
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAll()
        {
            ResetConnections();
            return await Task.FromResult(Ok());
        }

        protected void ResetConnections()
        {
            _connections.Clear();
        }

    }
}
