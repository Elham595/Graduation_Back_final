using graduation_project.Data;
using graduation_project.Models;

namespace graduation_project.Service
{
    public class MessageService : IMessageService
    {
        private readonly FashionDesignContext _fashionDbContext;
        public MessageService(FashionDesignContext fashionDesignContext)
        {
            _fashionDbContext = fashionDesignContext;
        }

        public async Task SaveMessageAsync(MessageChat message)
        {
            try
            {
                await _fashionDbContext.MessageChats.AddAsync(message);
                await _fashionDbContext.SaveChangesAsync();
            }catch(Exception ex)
            {

            }
        }

        public async  Task<IQueryable<MessageChat>> GetMessagesAsync(string username)
        {
            var data = await Task.FromResult(_fashionDbContext.MessageChats.Where(m => m.SendFrom == username || m.SendTo == username));
            Console.WriteLine(data);
            return data;
        }
    }
}
