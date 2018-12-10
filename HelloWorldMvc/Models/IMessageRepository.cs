using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldMvc.Models
{
    public interface IMessageRepository
    {
        IEnumerable<Message> AllMessages { get; }
        Message GetMessageById(int messageId);
        void ModifyMessages(Message msg);

    }
}
