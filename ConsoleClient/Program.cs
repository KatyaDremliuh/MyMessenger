using System;
using Newtonsoft.Json;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Message msg = new Message("RR", "Hi!", DateTime.UtcNow);
            string output = JsonConvert.SerializeObject(msg);
            Console.WriteLine(output); // {"UserName":"RR","MessageText":"Hi!","TimeStamp":"2022-01-03T20:43:26.5833758Z"}

            Message deserializedMsg = JsonConvert.DeserializeObject<Message>(output);
            Console.WriteLine(deserializedMsg); // RR <03.01.2022 20:43:26>: Hi!
            //Console.WriteLine(msg);
            //Console.WriteLine("Hello World!");
        }
    }
}
