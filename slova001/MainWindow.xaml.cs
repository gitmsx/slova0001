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
using System.Drawing;



namespace slova001
{

    public partial class MainWindow : Window
    {



        SolidColorBrush color102 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0d47a1"));
        SolidColorBrush color101 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#82b3c9"));
        SolidColorBrush color103 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#b4004e"));
        SolidColorBrush color104 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#00701a"));
        SolidColorBrush color105 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));
        SolidColorBrush color106 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003c8f"));
        SolidColorBrush color107 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#1565c0"));
        SolidColorBrush color108 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#b3e5fc"));
        




        private string but_ch = "but_cho";
        private string but_ans = "but_ans";


        private int kolichestvo_slov = -2;
        private int vneseno_slov = 0;

        string[] massiv_input = new String[75];
        int[] massiv_activ_buttons = new int[75];

        //string[] massiv_etalon = new String[75];
        //string[] massiv__ouput = new String[75];
        //Label[] massiv__ouput_link = new Label[75];


        //Brush Brush_set_l101 = Brushes.DarkGray;
        //Brush Brush_set_l102 = Brushes.SteelBlue;
        //Brush Brush_set_l103 = Brushes.DarkSlateGray;


        //int count_all_word = 0;  // Current number of entered words.
        //int count_inpuut_word = 0;  // Current number of entered words.






        public MainWindow()
        {
            InitializeComponent();
            new_uroks();
        }

        private void butt001_Click(object sender, RoutedEventArgs e)
        {

        }




        void zagruzka_frazy(string input_fra2)
        {
            String input_fra3 = System.Text.RegularExpressions.Regex.Replace(input_fra2, "[ ]+", " ");
            String pattern = " ";
            massiv_input = System.Text.RegularExpressions.Regex.Split(input_fra3, pattern);
        //    massiv_etalon = System.Text.RegularExpressions.Regex.Split(input_fra3, pattern);
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




        private void new_message_in_words_in_panel_choice(object sender, RoutedEventArgs e)
        {
            new_uroks();
        }




        private void new_uroks()
        {
            WrapPanel001.Children.Clear();
            WrapPanel002.Children.Clear();

            String inputfr = load_from_txt();
            zagruzka_frazy(" " + inputfr);


            for (int i = 1; i < massiv_input.Length; i++)
            {

                massiv_activ_buttons[i] = 1;
                Button newBtn = new Button();
                newBtn.Content = "  " + massiv_input[i] + " ";
                newBtn.Name = but_ch + i.ToString();
                newBtn.FontSize = slider_size.Value;

                newBtn.Width = newBtn.Width + 7;
                newBtn.Height = newBtn.Height + 3;
                newBtn.Background = color101;  // new urok
                newBtn.Foreground = color105;  // new urok
                newBtn.BorderBrush = color106;  // new urok
                

                newBtn.Margin = new Thickness(5, 2, 0, 0);
                newBtn.BorderThickness = new Thickness(1, 1, 1, 3);


                newBtn.Click += new RoutedEventHandler(click_wrap2);
                WrapPanel002.Children.Add(newBtn);

            }
        }


        private void add_word_in_answer(int position)
        {
            Button newBtn = new Button();
            newBtn.Content = massiv_input[position];
            newBtn.Name = but_ans + position.ToString();
            newBtn.FontSize = slider_size.Value;

            newBtn.Width = newBtn.Width + 7;
            newBtn.Height = newBtn.Height + 3;
            newBtn.Background = color108;  // wrap 1 add


            newBtn.BorderBrush = color107;  // 


            newBtn.Margin = new Thickness(5, 2, 0, 0);
            newBtn.BorderThickness = new Thickness(1, 1, 1, 3);



            newBtn.Click += new RoutedEventHandler(click_wrap1);
            WrapPanel001.Children.Add(newBtn);

            int kol_vnes = 0;

            for (int di3 = 1; di3 < massiv_input.Length; di3++)
            {
                kol_vnes += massiv_activ_buttons[di3];
            }

            if (kol_vnes == 0)
            {
                bool foundanswer = check_answer();
                if (foundanswer)
                {
                    MessageBox.Show("You win!!!");
                    new_uroks();
                }
            }

        }


        private void click_wrap1(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {


                string but_cho_str = btn.Name;
                string but_cho_str2 = but_cho_str.Replace(but_ans, "");

                int numb_but_pressed = Convert.ToInt32(but_cho_str2);
                massiv_activ_buttons[numb_but_pressed] = 1;
                WrapPanel001.Children.Remove(btn);
                foreach (Button txt12 in WrapPanel002.Children)
                {
                    if (txt12.Name == but_ch + but_cho_str2)
                    {
                        txt12.Background = color101;// wrap 2 101 vozvrat   = ok   proyavka 
                        txt12.Foreground = color105;  //  105
                        txt12.BorderBrush = color106;
                    }
                    else
                        continue;
                }


            }
        }

        private void click_wrap2(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                //MessageBox.Show(btn.Name);

                string but_cho_str = btn.Name;
                string but_cho_str2 = but_cho_str.Replace(but_ch, "");
                int numb_but_pressed = Convert.ToInt32(but_cho_str2);


                if (massiv_activ_buttons[numb_but_pressed] == 1)
                {
                    massiv_activ_buttons[numb_but_pressed] = 0;
                    btn.Background  = color102;             //  1-> 2 101 skryvau 
                    btn.Foreground  = color102;
                    btn.BorderBrush = color102;
                    add_word_in_answer(numb_but_pressed);
                }
            }
        }


        private string load_from_txt()
        {
            string[] lines = System.IO.File.ReadAllLines(@"T_eng2.txt", Encoding.GetEncoding(1251));


            string[] line_s_rus = System.IO.File.ReadAllLines(@"T_rus.txt", Encoding.GetEncoding(1251));
            string[] line_s_rus2 = System.IO.File.ReadAllLines(@"T_rus2.txt", Encoding.GetEncoding(1251));

            var rand001 = new Random();
            int jrand = rand001.Next(lines.Length);

            TextBox001.Text = line_s_rus[jrand];
            TextBox002.Text = lines[jrand];
            TextBox003.Text = Convert.ToString(jrand) + "  " + line_s_rus2[jrand];
            return lines[jrand];
        }




        bool check_answer()
        {

            bool identical = true;
            string prov_str = "";


            foreach (Button txt12 in WrapPanel001.Children)
            {
                prov_str += txt12.Content + " ";
            }
            if (prov_str != TextBox002.Text + " ")
            {
                identical = false;

            }
            return identical;

        }



        private void butt003_Click(object sender, RoutedEventArgs e)
        {
            load_from_txt();
        }


        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            foreach (Button txt12 in WrapPanel001.Children)
            {


                txt12.FontSize = slider_size.Value;
                txt12.Width = txt12.Width + 7;
                txt12.Height = txt12.Height + 3;
            }
        }
        private void Slider_ValueChanged2(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            foreach (Button txt12 in WrapPanel002.Children)
            {


                txt12.FontSize = slider_size2.Value;
                txt12.Width = txt12.Width + 7;
                txt12.Height = txt12.Height + 3;
            }
        }


        private void butt004_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Rate = Convert.ToInt32(sliderspeed.Value);
            synth.Volume = Convert.ToInt32(slidervol.Value);

            // Configure the audio output.   
            synth.SetOutputToDefaultAudioDevice();

            // Speak a string.  
            synth.SpeakAsync(TextBox002.Text);


        }

    }
}
