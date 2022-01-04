using System;

namespace ConsoleClient
{
    class Program
    {
        private static int MessageID;
        private static string UserName;
        private static MessengerClientAPI API = new MessengerClientAPI();

        private static void GetNewMessage() // f получает все смс и выводит их на экран
        {
            Message msg = API.GetMessage(MessageID);

            while (msg != null)
            {
                Console.WriteLine(msg);
                MessageID++;
                msg = API.GetMessage(MessageID);
            }
        }

        static void Main(string[] args)
        {
            //Message msg = new Message("RR", "Hi!", DateTime.UtcNow);
            //string output = JsonConvert.SerializeObject(msg);
            //Console.WriteLine(output); // {"UserName":"RR","MessageText":"Hi!","TimeStamp":"2022-01-03T20:43:26.5833758Z"}

            //Message deserializedMsg = JsonConvert.DeserializeObject<Message>(output);
            //Console.WriteLine(deserializedMsg); // RR <03.01.2022 20:43:26>: Hi!

            MessageID = 1;
            Console.WriteLine("Enter Ur name: ");
            //UserName = "RR";
            UserName = Console.ReadLine();

            string messageText = string.Empty; // считываем смс
            // вызываем GetNewMessage, пока пользователь не введёт exit
            while (messageText!="exit")
            {
                GetNewMessage();

                messageText = Console.ReadLine();

                if (messageText.Length>1)
                {
                    Message sendMsg = new Message(UserName, messageText, DateTime.Now);
                    API.SendMessage(sendMsg);
                }
            }
        }
    }
}
