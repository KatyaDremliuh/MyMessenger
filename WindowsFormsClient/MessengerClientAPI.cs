using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class MessengerClientAPI
    {
        private static readonly HttpClient client = new HttpClient();

        public void TestNewtonsoftJson()
        {
            Message msg = new Message("RR", "Hi!", DateTime.UtcNow);
            string output = JsonConvert.SerializeObject(msg);
            Console.WriteLine(output); // {"UserName":"RR","MessageText":"Hi!","TimeStamp":"2022-01-03T20:43:26.5833758Z"}

            Message deserializedMsg = JsonConvert.DeserializeObject<Message>(output);
            Console.WriteLine(deserializedMsg); // RR <03.01.2022 20:43:26>: Hi!

            // как сохранить в файл
            string path = @"d:\temp\ser.txt";
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
            {
                sw.WriteLine(output);
            }
        }

        // функция, кот. получает смс
        public Message GetMessage(int messageId)
        {
            WebRequest request = WebRequest.Create("http://localhost:5000/api/Messenger/" + messageId);
            request.Method = "Get";

            WebResponse response = request.GetResponse(); // записать ответ
            string status = ((HttpWebResponse)response).StatusDescription;

            // прочитать статус и то, что внутри в сообщении
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            // если статус ОК и id не "Not found"
            if (status.ToLower() == "ok" && responseFromServer != "Not found")
            {
                Message deserializedMsg = JsonConvert.DeserializeObject<Message>(responseFromServer);
                return deserializedMsg;
            }

            return null;
        }

        public async Task<Message> GetMessageHTTPAsync(int messageId)
        {
            var responseString = await client.GetStringAsync("http://localhost:5000/api/Messenger/");

            if (responseString != null)
            {
                Message deserializedMsg = JsonConvert.DeserializeObject<Message>(responseString);
                return deserializedMsg;
            }

            return null;
        }

        // функция, кот. отправляет смс
        public bool SendMessage(Message msg)
        {
            // создать запрос типа "POST", указать путь
            WebRequest request = WebRequest.Create("http://localhost:5000/api/Messenger");
            request.Method = "POST";

            string postData = JsonConvert.SerializeObject(msg); // записать сообщение
            byte[] byteArray = Encoding.UTF8.GetBytes(postData); // перевести в байты
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            // записать всё это добро в request
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            // отловить ответ
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();

            // прочитать ответ
            StreamReader reader = new StreamReader(dataStream);
            string responceFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            return true;
        }
    }
}
