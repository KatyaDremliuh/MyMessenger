using ConsoleClient;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPCoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Messenger : ControllerBase
    {
        private static readonly List<MessageController> ListOfMessages = new List<MessageController>();
        // GET api/<Messenger>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string outPutString = "Not found"; // по умолчанию

            // а если сообщение валидно, вернем номер смс и его текст 
            if (id < ListOfMessages.Count && id >= 0)
            {
                outPutString = JsonConvert.SerializeObject(ListOfMessages[id]);
            }

            Console.WriteLine($"Запрошено сообщение № {id} : {outPutString}");

            return outPutString;
        }

        // POST api/<Messenger>
        [HttpPost]
        public IActionResult SendMessage([FromBody] MessageController msg)
        {
            if (msg == null)
            {
                return BadRequest();
            }

            ListOfMessages.Add(msg);

            Console.WriteLine($"Всего сообщений: {ListOfMessages.Count}. Посланное сообщение:{msg}");
            return new OkResult();
        }
    }
}
