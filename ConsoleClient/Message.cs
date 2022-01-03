using System;

namespace ConsoleClient
{
    public class Message
    {
        public string UserName { get; set; } // логин или никнем, под кот. отправляем смс
        public string MessageText { get; set; } // само смс
        public DateTime TimeStamp { get; set; } // время, когда было отправлено смс

        public Message(string userName, string messageText, DateTime timeStamp)
        {
            UserName = userName;
            MessageText = messageText;
            TimeStamp = timeStamp;
        }

        // ctor по умолчанию
        public Message()
        {
            UserName = "System";
            MessageText = "Server is running...";
            TimeStamp = DateTime.Now;
        }

        public override string ToString()
        {
            string output = $"{UserName} <{TimeStamp}>: {MessageText}";
            return output;
        }
    }
}
