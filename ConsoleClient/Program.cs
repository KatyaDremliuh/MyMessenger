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
            MessageID = 1;
            Console.WriteLine("Enter Ur name: ");
            UserName = Console.ReadLine();

            Console.WriteLine("Enter Ur text msg: ");
            string messageText = string.Empty; // считываем смс
            // вызываем GetNewMessage, пока пользователь не введёт exit
            while (messageText != "exit")
            {
                GetNewMessage();

                messageText = Console.ReadLine();

                if (messageText != null && messageText.Length > 1)
                {
                    Message sendMsg = new Message(UserName, messageText, DateTime.Now);
                    API.SendMessage(sendMsg);
                }
            }
        }
    }
}
