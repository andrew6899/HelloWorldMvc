using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelloWorldMvc.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace HelloWorldMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        public HomeController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public ViewResult Index()
        {
            return View(_messageRepository);
        }


        [HttpPost]
        public JsonResult GetMessage([FromBody]int messageId)
        {
            int count = _messageRepository.AllMessages.Count();
            //string status = string.Empty;
            Message msg = new Message();           
            //Retrieve the message
            msg = _messageRepository.GetMessageById(messageId);

            
            MessageInfo info = new MessageInfo()
            {
                Count = count,
                Status = $"Displaying message {messageId} of {count}"
            };

            //modify the status if the message wasn't found
            if (msg.GreetingMessage == null)
            {
                info.Status = $"No message with an ID of {messageId} was found";
            }

            ReturnedResponse response = new ReturnedResponse()
            {
                Info = info,
                Message = msg
            };
            //send the response object
            return Json(response);
        }

        [HttpPost]
        public IActionResult ModifyMessages([FromBody] Message m)
        {
            int messageCount = _messageRepository.AllMessages.Count();
            var status = string.Empty;
            MessageInfo json = new MessageInfo();
            json.Count = messageCount;

            //If there are more than nine messages, don't add an additional message.
            if (messageCount > 9)
            {
                json.Status = ($"Database full, could not add message.");
                if (m.GreetingMessageId.Equals(-1) && m.GreetingMessage.Equals("PURGE"))
                {
                    _messageRepository.ModifyMessages(m);
                    json.Status = "Purge Complete. Only one message remains";
                    json.Count = _messageRepository.AllMessages.Count();
                }
                return Json(json);
            }
            //Add received message to the database
            if (m.GreetingMessage != null)
            {
                if (m.GreetingMessageId.Equals(-1) && m.GreetingMessage.Equals("PURGE"))
                {
                    _messageRepository.ModifyMessages(m);
                    json.Status = "Purge Complete. Only one message remains";
                    json.Count = _messageRepository.AllMessages.Count();
                }
                else
                {
                    m.GreetingMessageId = messageCount + 1;
                    _messageRepository.ModifyMessages(m);
                    messageCount++;
                    json.Status = ($"Added {m.GreetingMessage} to the repository.");
                    json.Count = messageCount;
                }
            }
            return Json(json);
        }
    }

    //internal controller classes
    internal class MessageInfo
    {
        public int Count { get; set; }
        public string Status { get; set; }
    }

    internal class ReturnedResponse
    {
        public MessageInfo Info { get; set; }
        public Message Message { get; set; }
    }
}
