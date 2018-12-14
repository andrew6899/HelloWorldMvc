using System;
using System.Collections.Generic;
using System.Linq;
using HelloWorldMvc.Models;
using Xunit;

namespace UnitTest
{
    public class MessageRepoTests
    {

        private readonly AppDbContext _appDbContext;
        [Fact]
        public void GetHelloWorld() //First message must always be "Hello World!"
        {
            MockMessageRepository repo = new MockMessageRepository();
            string greetingMessage = repo.GetMessageById(1).GreetingMessage;
            Assert.Equal("Hello World!", greetingMessage);
        }

        [Fact]
        public void MessageCountAfterPurge() //There should be one remaining message after a purge
        {
            MockMessageRepository repo = new MockMessageRepository();
            Message purgeMessage = new Message{ GreetingMessageId=-1, GreetingMessage="PURGE" };
            IEnumerable<Message> result = repo.ModifyMockMessages(purgeMessage);
            Assert.Single(result);
        }


        [Fact]
        public void InsertEleventhMessage() //Inserting an 11th message should not be possible
        {
            MockMessageRepository repo = new MockMessageRepository();
            Message eleventhMessage = new Message { GreetingMessageId = 11, GreetingMessage = "I'm the eleventh!" };
            IEnumerable<Message> result = repo.ModifyMockMessages(eleventhMessage);
            Assert.Equal(10, result.Count());
        }
    }
}
