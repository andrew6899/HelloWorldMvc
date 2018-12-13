using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldMvc.Models;

namespace HelloWorldMvc.Models
{
    public class MockMessageRepository : IMessageRepository
    {
        //private readonly AppDbContext _appDbContext;
        //public MockMessageRepository(AppDbContext appDbContext)
        //{
        //    _appDbContext = appDbContext;
        //}
        public IEnumerable<Message> AllMessages
        {
            get; set;
        }
        public List<Message> AllMockMessages
        {
            get
            {
                return new List<Message>
                {
                    new Message{GreetingMessageId=1, GreetingMessage="Hello World!"},
                    new Message{GreetingMessageId=2, GreetingMessage="Greetings"},
                    new Message{GreetingMessageId=3, GreetingMessage="Guten Tag"},
                    new Message{GreetingMessageId=4, GreetingMessage="Hiya"},
                    new Message{GreetingMessageId=5, GreetingMessage="Hello"},
                    new Message{GreetingMessageId=6, GreetingMessage="Aloha"},
                    new Message{GreetingMessageId=7, GreetingMessage="Welcome!"},
                    new Message{GreetingMessageId=8, GreetingMessage="Hey!"},
                    new Message{GreetingMessageId=9, GreetingMessage="Howdy"},
                    new Message{GreetingMessageId=10, GreetingMessage="Goodbye"}
                };
            }
        }
        public Message GetMessageById(int MessageId)
        {
            Message greeting = new Message();
            foreach (var msg in AllMockMessages)
            {
                if (msg.GreetingMessageId.Equals(MessageId))
                {
                    greeting = msg;
                }
            }
            return greeting;
        }

        public void ModifyMessages(Message msg)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<Message> ModifyMockMessages(Message msg)
        {
            List<Message> modifiedMockMessages = AllMockMessages;
            //Create a new List entry if the message isn't empty
            if (!msg.GreetingMessage.Equals(string.Empty))
            {
                //special case. Message with an ID of -1 with the text PURGE will delete
                //all messages except the first
                if (msg.GreetingMessageId.Equals(-1) && msg.GreetingMessage.Equals("PURGE")) //purge logic
                {
                    //Logic is modified here since we are using a list for
                    //this test instead of the appDbContext
                    modifiedMockMessages.RemoveAll(x => !x.GreetingMessageId.Equals(1));
                    return modifiedMockMessages;
                }
                //if more than 9 messages, don't add the new message.
                //This logic is normally in the controller
                if (modifiedMockMessages.Count() > 9)
                {
                    return modifiedMockMessages;
                }
                else
                {
                    modifiedMockMessages.Add(msg);
                }
            }
            return modifiedMockMessages;
        }
    }
}
