using ConsoleClient;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsClient
{
    public partial class Name : Form
    {
        private static int MessageID = 0;
        private static string UserName;
        private static MessengerClientAPI API = new MessengerClientAPI();

        public Name()
        {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            string userName = userNameTextBox.Text;
            string message = messageTextBox.Text;

            if (userName.Length > 1)
            {
                ConsoleClient.Message msg = new ConsoleClient.Message(UserName, message, DateTime.Now);
                API.SendMessage(msg);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var getMessage = new Func<Task>(async () =>
            {
                ConsoleClient.Message msg = await API.GetMessageHTTPAsync(MessageID);
                while (msg != null)
                {
                    messagesListBox.Items.Add(msg);
                    MessageID++;
                    msg = await API.GetMessageHTTPAsync(MessageID);
                }
            });
            getMessage.Invoke(); // выполнить эту f
        }
    }
}
