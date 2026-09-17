using Microsoft.VisualBasic.ApplicationServices;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace GUIChartBot
{
    public partial class Form1 : Form
    {
        private string userName = "";
        private string favouriteTopic = "";
        private string lastTopic = "";
        private Random random = new Random();

        private Dictionary<string, List<string>> KeywordResponse;
        private List<string> worriedWords;
        public Form1()
        {
            InitializeComponent();
            InitializeData();
        }
        private void InitializeData()
        {
            KeywordResponse = new Dictionary<string, List<string>>
            {
                { "hello", new List<string> { "Hello! How can I assist you today?", "Hi there! What would you like to talk about?" } },
                { "password", new List<string> { "Always use strong, unique passwords with at least 12 characters.","Consider using a password manager to store credentials securely."  } },
                { "email", new List<string> { "Be cautious of phishing emails and avoid clicking on suspicious links.","Use two-factor authentication for added security." } },
                { "social media", new List<string> { "Be mindful of the information you share online.","Regularly review your privacy settings on social media platforms." } },
                { "cybersecurity", new List<string> { "Keep your software and operating system up to date.","Use antivirus software and firewalls to protect your devices." } },
                { "privacy", new List<string> { "Be aware of the data you share online and with apps.","Regularly review app permissions and privacy settings." } },
                { "phishing", new List<string> { "Be cautious of phishing attempts and avoid clicking on suspicious links.","Verify the sender's email address before providing any sensitive information." }    },
                { "safe browsing", new List<string> { "Use secure and trusted websites.","Avoid clicking on suspicious links or downloading unknown files." } }
            };
            worriedWords = new List<string> { "worried", "anxious", "nervous", "concerned" };
        }
        public void Response(string userInput)
        {
            string response = "I'm not sure how to respond to that. Can you please rephrase?";
            foreach (var keyword in KeywordResponse.Keys)
            {
                if (userInput.ToLower().Contains(keyword))
                {
                    var responses = KeywordResponse[keyword];
                    response = responses[random.Next(responses.Count)];
                    Console.Write(response);
                    Thread.Sleep(15);
                    break;
                }
            }
            foreach (var worriedWord in worriedWords)
            {
                if (userInput.ToLower().Contains(worriedWord))
                {
                    response = "It's okay to feel worried sometimes. Remember to take deep breaths and focus on positive thoughts.";
                    Console.Write(response);
                    Thread.Sleep(15);
                    break;
                }
            }
          
            textBox1.AppendText("Chatbot: " + response + Environment.NewLine);
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            Display_Logo();
            PlayGreeting();
        }

        public void PlayGreeting()
        {
            SoundPlayer player = new SoundPlayer("C:\\Users\\Student\\source\\repos\\GUIchartBot\\Resourse\\chartbot.wav");
            player.Play();
        }

        public void Display_Logo()
        {
            pictureBox1.Image = Image.FromFile("C:\\Users\\Student\\source\\repos\\GUIchartBot\\Resourse\\logo.png");
        }

        public void button1_Click(object sender, EventArgs e)
        {
            PlayGreeting();
            textBox1.AppendText("Bot: Hello"+ Environment.NewLine);
        }

        public void button2_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("Bot: " + textBox2.Text + Environment.NewLine);    
            Response(textBox2.Text);
            textBox2.Clear();
        }

        public void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
