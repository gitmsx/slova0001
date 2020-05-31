using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Synthesis;


namespace slova001
{
   
    


    public partial class MainWindow : Window
    {

 private int kolichestvo_slov = 0;
    private int vneseno_slov = 0;


        string[] massiv_input = new String[75];
        string[] massiv_etalon = new String[75];
        string[] massiv__ouput = new String[75];
        Label[] massiv__ouput_link = new Label[75];



        int count_all_word = 0;  // Current number of entered words.
        int count_inpuut_word = 0;  // Current number of entered words.






        public MainWindow()
        {
            InitializeComponent();
        }

        private void butt001_Click(object sender, RoutedEventArgs e)
        {
            Button[] btns = new Button[50];
            for (int i = 0; i < btns.Length; i++)
            {
                var btn = new Button
                {
                
              
                Content = "Button-" + i.ToString(),
                    
            };
                WrapPanel001.Children.Add(btn);
            }
        }




        void zagruzka_frazy(string input_fra2)
        {
            String input_fra3 = System.Text.RegularExpressions.Regex.Replace(input_fra2, "[ ]+", " ");
            String pattern = " ";
            massiv_input = System.Text.RegularExpressions.Regex.Split(input_fra3, pattern);
            massiv_etalon = System.Text.RegularExpressions.Regex.Split(input_fra3, pattern);
            kolichestvo_slov = massiv_input.Length;


            var r = new Random();  // Change the position of words randomly.
            for (int i = massiv_input.Length - 1; i > 1; i--)
            {
                int j = r.Next(i) + 1;
                var t = massiv_input[i];
                massiv_input[i] = massiv_input[j];
                massiv_input[j] = t;
            }


        }


        private void butt002_Click(object sender, RoutedEventArgs e)
    {


            String inputfr = load_from_txt();
            zagruzka_frazy(" " + inputfr);




            for (int i = 1; i <  massiv_input.Length; i++)
            {
                Button newBtn = new Button();
                newBtn.Content = massiv_input[i];
                newBtn.Name = "Button" + i.ToString();
                newBtn.Height = 23;
                newBtn.Click += new RoutedEventHandler(newBtn_Click);
                WrapPanel002.Children.Add(newBtn);

            }






        }
        private void newBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                MessageBox.Show(btn.Name);
            }
        }


        private string load_from_txt()
        {
            string[] lines = System.IO.File.ReadAllLines(@"T_eng.txt");
            string[] line_s_eng2 = System.IO.File.ReadAllLines(@"T_eng2.txt", Encoding.GetEncoding(1251));

            string[] line_s_rus  = System.IO.File.ReadAllLines(@"T_rus.txt", Encoding.GetEncoding(1251));
            string[] line_s_rus2 = System.IO.File.ReadAllLines(@"T_rus2.txt", Encoding.GetEncoding(1251));

            var rand001 = new Random();
            int jrand = rand001.Next(lines.Length);

            TextBox001.Text = line_s_rus[jrand];
            TextBox002.Text = line_s_eng2[jrand];
            TextBox003.Text = Convert.ToString(jrand) + "  " + line_s_rus2[jrand];

            return lines[jrand];


        }

        private void butt003_Click(object sender, RoutedEventArgs e)
        {
            load_from_txt();
        }

        private void butt004_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();


            

            synth.Rate = Convert.ToInt32(sliderspeed.Value);
            synth.Volume = Convert.ToInt32(slidervol.Value);

            // Configure the audio output.   
            synth.SetOutputToDefaultAudioDevice();

            // Speak a string.  
            synth.Speak(TextBox002.Text);


        }
 
    }
}
