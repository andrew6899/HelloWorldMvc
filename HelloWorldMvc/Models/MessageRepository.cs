using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldMvc.Models
{
    public class MessageRepository : IMessageRepository
    {

        private readonly AppDbContext _appDbContext;
        public MessageRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Message> AllMessages {
            get
            {
                return _appDbContext.Messages;
            }
        }
        public Message GetMessageById(int MessageId)
        {
            Message greeting = new Message();
            foreach (var msg in _appDbContext.Messages)
            {
                if (msg.GreetingMessageId.Equals(MessageId)) {
                    greeting = msg;
                }
            }
            return greeting;
        }
        public void ModifyMessages(Message msg)
        {
            //Create a new database entry if the message isn't empty
            if(!msg.GreetingMessage.Equals(string.Empty))
            {
                //special case. Message with an ID of -1 with the text PURGE will delete
                //all messages except the first
                if (msg.GreetingMessageId.Equals(-1) && msg.GreetingMessage.Equals("PURGE")) //purge logic
                {
                    foreach (var m in _appDbContext.Messages)
                    {
                        if (!m.GreetingMessageId.Equals(1)) _appDbContext.Remove(m);
                    }
                    _appDbContext.SaveChanges();
                }
                else
                {
                    _appDbContext.Add(msg);
                    _appDbContext.SaveChanges();
                }
            }
        }
    }
}
