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
using System.Speech.Synthesis;
using System.IO;
using System.Security.Cryptography;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;


namespace Voice
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        AES aesClass = new AES();
        AesManaged aes = new AesManaged();

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
            commands.Add(new string[] {"stop the music", "music one", "stop livingroom", "bedroom light on", "bedroom light off","open garage","close garage",
            "music seven", "music five","how is the weather outside", "mode one", "mode five", "mode seven", "open door", "close door"});
            GrammarBuilder gBuilder = new GrammarBuilder();
            
            gBuilder.Append(commands);
            
            Grammar grammar = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
           

            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;     // -10...10

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            String pem = File.ReadAllText(@"D:\Smart house\server\keys\public-key.pem");
            PemReader pr = new PemReader(new StringReader(pem));
            AsymmetricKeyParameter publicKey = (AsymmetricKeyParameter)pr.ReadObject();
            RSAParameters rsaParam = DotNetUtilities.ToRSAParameters((RsaKeyParameters)publicKey);

            rsa.ImportParameters(rsaParam);

            string aesKey = BitConverter.ToString(aes.Key);

            byte[] encryptedKey = rsa.Encrypt(Encoding.UTF8.GetBytes(aesKey), true);
            string key = Convert.ToBase64String(encryptedKey);

            File.WriteAllText(@"D:\Smart house\server\text\key.txt", key);
        }

        void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            aes.IV = aesClass.generateIV();
            string IV = BitConverter.ToString(aes.IV);

            File.WriteAllText(@"D:\Smart house\server\text\iv.txt", IV);

            string command = "";
            byte[] encrypted;

            switch (e.Result.Text)
            {
                case "stop livingroom":
                    command = "livingroom light off";
                    encrypted = aesClass.Encrypt(command, aes.Key, aes.IV);
                    Text1.Text = command + '\n';
                    File.WriteAllText(@"D:\Smart house\server\SpeechToText.txt", BitConverter.ToString(encrypted));
                    break;
                case "bedroom light on":
                    command = "bedroom light on";
                    encrypted = aesClass.Encrypt(command, aes.Key, aes.IV);
                    Text1.Text = command + '\n';
                    File.WriteAllText(@"D:\Smart house\server\SpeechToText.txt", BitConverter.ToString(encrypted));
                    break;
                case "bedroom light off":
                    command = "bedroom light off";
                    encrypted = aesClass.Encrypt(command, aes.Key, aes.IV);
                    Text1.Text = command + '\n';
                    File.WriteAllText(@"D:\Smart house\server\SpeechToText.txt", BitConverter.ToString(encrypted));
                    break;
                case "open garage":
                    command = "open garage";
                    encrypted = aesClass.Encrypt(command, aes.Key, aes.IV);
                    Text1.Text = command + '\n';
                    File.WriteAllText(@"D:\Smart house\server\SpeechToText.txt", BitConverter.ToString(encrypted));
                    break;
                case "close garage":
                    command = "close garage";
                    encrypted = aesClass.Encrypt(command, aes.Key, aes.IV);
                    Text1.Text = command + '\n';
                    File.WriteAllText(@"D:\Smart house\server\SpeechToText.txt", BitConverter.ToString(encrypted));
                    break;
                case "music one":
                    recEngine.RecognizeAsyncStop();
                    Text1.Text = "Chill Music On\n";
                    Process.Start("https://www.youtube.com/watch?v=0XFudmaObLI&list=RDQMOE8XiyF2cWw&start_radio=1");
                    stopmusic.Visible = true;
                    break;
                case "music five":
                    recEngine.RecognizeAsyncStop();
                    Text1.Text = "Blues Music On\n";
                    Process.Start("https://www.youtube.com/watch?v=4zAThXFOy2c&list=PLjzeyhEA84sQKuXp-rpM1dFuL2aQM_a3S");
                    stopmusic.Visible = true;
                    break;
                case "music seven":
                    recEngine.RecognizeAsyncStop();
                    Text1.Text = "Tehno Music On\n";
                    Process.Start("https://www.youtube.com/watch?v=o3WdLtpWM_c&list=PLMmqTuUsDkRINEEFXWy7Ne2897vCdxJg1");
                    stopmusic.Visible = true;
                    break;
                case "how is the weather outside":
                    recEngine.RecognizeAsyncStop();
                    string text = File.ReadAllText(@"D:\Smart house\server\date_meteo.txt");
                    Text1.Text = "The weather is: \n";
                    synthesizer.Speak(text);
                    recEngine.RecognizeAsync(RecognizeMode.Multiple);
                    break;

                case "mode seven":
                    command = "study light";
                    encrypted = aesClass.Encrypt(command, aes.Key, aes.IV);
                    Text1.Text = "study light on\n";
                    File.WriteAllText(@"D:\Smart house\server\SpeechToText.txt", BitConverter.ToString(encrypted));
                    break;
                case "mode five":
                    command = "evening light";
                    encrypted = aesClass.Encrypt(command, aes.Key, aes.IV);
                    Text1.Text = "evening light on\n";
                    File.WriteAllText(@"D:\Smart house\server\SpeechToText.txt", BitConverter.ToString(encrypted));
                    break;

                case "mode one":
                    command = "relaxing light";
                    encrypted = aesClass.Encrypt(command, aes.Key, aes.IV);
                    Text1.Text = "relaxing light on\n";
                    File.WriteAllText(@"D:\Smart house\server\SpeechToText.txt", BitConverter.ToString(encrypted));
                    break;
                case "open door":
                    command = "open the door";
                    encrypted = aesClass.Encrypt(command, aes.Key, aes.IV);
                    Text1.Text = "door opened\n";
                    File.WriteAllText(@"D:\Smart house\server\SpeechToText.txt", BitConverter.ToString(encrypted));
                    break;
                case "close door":
                    command = "close the door";
                    encrypted = aesClass.Encrypt(command, aes.Key, aes.IV);
                    Text1.Text = "door closed\n";
                    File.WriteAllText(@"D:\Smart house\server\SpeechToText.txt", BitConverter.ToString(encrypted));
                    break;
            }
            ///Should be uncommented only for testing purposes
            ///File.WriteAllText(@"D:\Smart house\voice-control-c\UnitTest\UnitTest\text.txt", command);
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
