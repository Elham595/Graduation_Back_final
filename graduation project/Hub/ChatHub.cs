using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using graduation_project.Data;
using graduation_project.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using graduation_project.Service;
using Microsoft.AspNetCore.Authorization;

namespace graduation_project
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly HashSet<UserConnection> _conectedUsers;
        private readonly IMessageService _messageService;


        public ChatHub(HashSet<UserConnection>  connectedUsers ,IMessageService messageService)
        {
            _conectedUsers = connectedUsers;
            _messageService= messageService;
        }


        public override async Task OnConnectedAsync()
        {
            _conectedUsers.Add(new UserConnection(ConnectionId, Username));
            await ConnectionChanged();
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var connection = _conectedUsers.FirstOrDefault(c => c.ConnectionId == ConnectionId && c.Username == Username);
                _conectedUsers.Remove(connection);
                await ConnectionChanged();
                await base.OnDisconnectedAsync(exception);
            }
            catch(Exception ex)
            {
            }
        }

        public async Task ConnectionChanged()
        {
            await Clients.All.SendAsync("ConnectionChanged", _conectedUsers);
        }

        [HubMethodName("SendMessage")]
        public async Task SendMessage(UserConnection connection,MessageChat message)
        {
            var client = Clients.Client(connection.ConnectionId);
            if (client is not null)
            {
                await client.SendAsync("ReciveMessage", CuttrentConnection , message);
                await _messageService.SaveMessageAsync(message);
            }
        }


        #region Properties

        protected string ConnectionId
        {
            get => Context.ConnectionId;
        }

        protected string Username
        {
            get => Context.User.Identity.Name;
        }

        protected UserConnection CuttrentConnection
        {
            get => new UserConnection(ConnectionId, Username);
        }
        #endregion
    }
}

