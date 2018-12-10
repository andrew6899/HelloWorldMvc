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
        //AppDbContext context = new AppDbContext(DbContextOptions < AppDbContext > options) : base(options)
        //AppDbContext context = applicationBuilder.ApplicationServices.GetRequiredService<AppDbContext>();
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
            string json = string.Empty;
            Message msg = new Message();
            msg = _messageRepository.GetMessageById(messageId);
            return Json(msg);
        }

        [HttpPost]
        public IActionResult ModifyMessages([FromBody] Message m)
        {
            int messageCount = _messageRepository.AllMessages.Count();
            var status = string.Empty;
            //If there are more than nine messages, don't add an additional message.
            if (messageCount > 9)
            {
                status = ($"Database full, could not add message.");
                if (m.GreetingMessageId.Equals(-1) && m.GreetingMessage.Equals("PURGE"))
                {
                    _messageRepository.ModifyMessages(m);
                    status = "Purge Complete. Only one message remains";
                }
                return Json(status);
            }
            //Add received message to the database
            if (m.GreetingMessage != null)
            {
                if (m.GreetingMessageId.Equals(-1) && m.GreetingMessage.Equals("PURGE"))
                {
                    _messageRepository.ModifyMessages(m);
                    status = "Purge Complete. Only one message remains";
                }
                else
                {
                    m.GreetingMessageId = messageCount + 1;
                    _messageRepository.ModifyMessages(m);
                    status = ($"Added {m.GreetingMessage} to the repository.");
                }
            }
            return Json(status);
        }
    }
}
