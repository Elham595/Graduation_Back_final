using graduation_project.Models;

namespace graduation_project.Service
{
    public interface IMessageService
    {
        Task<IQueryable<MessageChat>> GetMessagesAsync(string username);

        Task SaveMessageAsync(MessageChat message);
    }
}