using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace UnitTest
{
    public partial class Form1 : Form
    {

        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        

        String[] commands = new String[] { "stop livingroom", "bedroom light on", "bedroom light off", "open garage","close garage",
            "mode one", "mode five", "mode seven", "open door", "close door" , "stop the music", "music one", "music seven", "music five", "how is the weather outside"};

        String[] data_send = new string[] {"livingroom light off", "bedroom light on", "bedroom light off", "open garage", "close garage"
        , "study light", "evening light", "relaxing light", "open the door", "close the door"};

        Random rnd = new Random();

        int successfull_units;

        public Form1()
        {
            InitializeComponent();
            successfull_units = 0;
            synthesizer.Volume = 100;
            synthesizer.Rate = 1;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int index_command;
            int repeats = 0;
            while (repeats<15)
            {
                index_command = rnd.Next(0, 10);
                synthesizer.Speak(commands[index_command]);
                System.Threading.Thread.Sleep(1000);
                String text_from_file = System.IO.File.ReadAllText(@"D:\Smart house\voice-control-c\UnitTest\UnitTest\text.txt");
                label2.Text = text_from_file;
                if (data_send[index_command].Equals(text_from_file))
                {
                    successfull_units++;
                }
                repeats++;
                number_tested.Text = repeats.ToString() + "/15";
                good_tests.Text = successfull_units.ToString();
                System.Threading.Thread.Sleep(1000);
                this.Refresh();
            }
        }
    }
}
