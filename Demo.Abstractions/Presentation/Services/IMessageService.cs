using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Abstractions.Presentation.Services
{
    public interface IMessageService
    {
        void ConfigureMessages();
        Task ShowNotificationAsync(string message, MessageType type, string actionContent, Action action);
        Task<bool> ShowMessageAsync(string title, string message);
    }

    public static class MessageServiceExtensions
    {
        public static async Task ShowNotificationAsync(this IMessageService service, string message, MessageType type)
        {
            await service.ShowNotificationAsync(message, type, null, null);
        }
    }

}
