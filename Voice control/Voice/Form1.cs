using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.IO.Ports;
using System.Speech.Synthesis;


namespace Voice
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] {"stop the music", "music one", "stop livingroom", "bedroom light on", "bedroom light off", "eleven","close eleven",
            "music seven", "music five","how is the weather outside", "mode one", "mode five", "mode seven", "nine", "close nine"});
            GrammarBuilder gBuilder = new GrammarBuilder();
            
            gBuilder.Append(commands);
            
            Grammar grammar = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
           

            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;     // -10...10
        }

        void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
           
            switch(e.Result.Text)
            {
              
                case "stop livingroom":
                    string red = ('r').ToString();
                    Text1.Text += "livingroom light off\n";
                    System.IO.File.WriteAllText(@"D:\Victor Info\Smart Home Mecatronica\Node Server\SpeechToText.txt", "livingroom light off");
                    break;
                case "bedroom light on":
                    Text1.Text += "bedroom light on\n";
                    System.IO.File.WriteAllText(@"D:\Victor Info\Smart Home Mecatronica\Node Server\SpeechToText.txt", "bedroom light on");
                    break;
                case "bedroom light off":
                    Text1.Text += "bedroom light off\n";
                    System.IO.File.WriteAllText(@"D:\Victor Info\Smart Home Mecatronica\Node Server\SpeechToText.txt", "bedroom light off");
                    break;
                case "eleven":
                    Text1.Text += "open garage\n";
                    System.IO.File.WriteAllText(@"D:\Victor Info\Smart Home Mecatronica\Node Server\SpeechToText.txt", "open garage");
                    break;
                case "close eleven":
                    Text1.Text += "close garage\n";
                    System.IO.File.WriteAllText(@"D:\Victor Info\Smart Home Mecatronica\Node Server\SpeechToText.txt", "close garage");
                    break;
                case "music one":
                    recEngine.RecognizeAsyncStop();
                    Text1.Text += "Chill Music On\n";
                    Process.Start("https://www.youtube.com/watch?v=0XFudmaObLI&list=RDQMOE8XiyF2cWw&start_radio=1");
                    stopmusic.Visible = true;
                    break;
                case "music five":
                    recEngine.RecognizeAsyncStop();
                    Text1.Text += "Blues Music On\n";
                    Process.Start("https://www.youtube.com/watch?v=4zAThXFOy2c&list=PLjzeyhEA84sQKuXp-rpM1dFuL2aQM_a3S");
                    stopmusic.Visible = true;

                    break;

                case "music seven":
                    recEngine.RecognizeAsyncStop();
                    Text1.Text += "Tehno Music On\n";
                    Process.Start("https://www.youtube.com/watch?v=o3WdLtpWM_c&list=PLMmqTuUsDkRINEEFXWy7Ne2897vCdxJg1");
                    stopmusic.Visible = true;
                    break;
                case "how is the weather outside":
                    recEngine.RecognizeAsyncStop();
                    string text = System.IO.File.ReadAllText(@"D:\Victor Info\Smart Home Mecatronica\Node Server\date_meteo.txt");
                    Text1.Text += "The weather is: \n";
                    synthesizer.Speak(text);
                    recEngine.RecognizeAsync(RecognizeMode.Multiple);
                    break;

                case "mode seven":
                    Text1.Text += "study light on\n";
                    System.IO.File.WriteAllText(@"D:\Victor Info\Smart Home Mecatronica\Node Server\SpeechToText.txt", "study light");
                    break;

                case "mode five":
                    Text1.Text += "evening light on\n";
                    System.IO.File.WriteAllText(@"D:\Victor Info\Smart Home Mecatronica\Node Server\SpeechToText.txt", "evening light");
                    break;

                case "mode one":
                    Text1.Text += "relaxing light on\n";
                    System.IO.File.WriteAllText(@"D:\Victor Info\Smart Home Mecatronica\Node Server\SpeechToText.txt", "relaxing light");
                    break;
                case "nine":
                    Text1.Text += "door opened\n";
                    System.IO.File.WriteAllText(@"D:\Victor Info\Smart Home Mecatronica\Node Server\SpeechToText.txt", "open the door");

                    break;
                case "close nine":
                    Text1.Text += "door closed  \n";
                    System.IO.File.WriteAllText(@"D:\Victor Info\Smart Home Mecatronica\Node Server\SpeechToText.txt", "close the door");

                    break;
            }
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
        }

        private void stopmusic_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            try
            {
                Text1.Text += "Music Stopped\n";
                foreach (Process proc in Process.GetProcessesByName("Firefox"))
                {
                    proc.Kill();
                }
                
                stopmusic.Visible = false;
            }
            catch (Exception ex)
            {
                Text1.Text += ex + "\n";
            }
        }
    }
}
