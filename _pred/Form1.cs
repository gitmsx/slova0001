using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iword001
{
    public partial class Form1 : Form
    {


        int kolichestvo_slov = 0;
        int vneseno_slov = 0;


        int[] mas_int = new int[75];


        string podskazka;

        Color Color1_sets1 = Color.Black;
        Color Color2_sets1 = Color.White;

        Color Color1_sets2 = Color.Yellow;
        Color Color2_sets2 = Color.Black;


        Color Color1_sets3 = Color.Red;
        Color Color2_sets3 = Color.LightBlue;

        Color Color1_sets4 = Color.LightGray;
        Color Color2_sets4 = Color.White;




        //   int[] mas_otv_flag = new int[75];


        string[] massiv_input = new String[75];
        string[] massiv_etalon = new String[75];
        string[] massiv__ouput = new String[75];
        Label[] massiv__ouput_link = new  Label[75];



        int count_all_word = 0;  // Current number of entered words.
        int count_inpuut_word = 0;  // Current number of entered words.



        public Form1()
        {
            InitializeComponent();

        }

        void zagruzka_frazy(string input_fra2)
        {
            String input_fra3 = System.Text.RegularExpressions.Regex.Replace(input_fra2, "[ ]+", " ");
            String pattern = " ";
            massiv_input = System.Text.RegularExpressions.Regex.Split(input_fra3, pattern);
            massiv_etalon = System.Text.RegularExpressions.Regex.Split(input_fra3, pattern);
            kolichestvo_slov = massiv_input.Length;


            var r = new Random();  // Change the position of words randomly.
            for (int i = massiv_input.Length-1 ; i > 1; i--)
            {
                int j = r.Next(i)+1;
                var t = massiv_input[i];
                massiv_input[i] = massiv_input[j];
                massiv_input[j] = t;
            }


        }

        Label vnesti_v_spisok_vibora(int i)
        {
            Label l = new Label();
            l.Name = "" + i.ToString();
            l.Text = massiv_input[i];
            l.ForeColor = Color1_sets1;
            l.BackColor = Color2_sets1;
            l.Font = new Font("Serif", 24, FontStyle.Bold);

            int dlinatmp = massiv_input[i].Length;


            l.Width = 20 + 20 * dlinatmp;
            l.Height = 50;
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.Margin = new Padding(5);
            return l;
        }


        Label vnesti_v_spisok_otv(int i)
        {
            Label l = new Label();
            l.Name = "" + i.ToString();
            l.Text = massiv__ouput[i];
            l.ForeColor = Color1_sets2;
            l.BackColor = Color2_sets2;
            l.Font = new Font("Serif", 18, FontStyle.Bold);
            
            int dlinatmp = massiv__ouput[i].Length;
            l.Width = 20 + 20 * dlinatmp;

            
            l.Height = 50;
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.Margin = new Padding(3);
            return l;
        }




        private void otvet_refresh()
        {
            flowLayoutPanel2.Controls.Clear();
            for (int i1 = 1; i1 <= vneseno_slov; i1++)
            {
                Label ls = vnesti_v_spisok_otv(i1);
                flowLayoutPanel2.Controls.Add(ls);

                ls.Click += new EventHandler(this.Click_otmena_polzovatela);
            }

        }






        void Click_otmena_polzovatela(object sender, EventArgs ev1)
        {

            Label cucerrentLabel1 = (Label)sender;

            int NumInArr = int.Parse(cucerrentLabel1.Name);


            massiv__ouput_link[NumInArr].ForeColor = Color1_sets1;
            massiv__ouput_link[NumInArr].BackColor = Color2_sets1;
            mas_int[int.Parse(massiv__ouput_link[NumInArr].Name)] = 0;

            for (int i1 = 1; i1 <= vneseno_slov; i1++)
            {

                if (NumInArr < i1)
                {
                    massiv__ouput[i1 - 1] = massiv__ouput[i1];
                    massiv__ouput_link[i1 - 1] = massiv__ouput_link[i1];

                }
                



            }
            
            massiv__ouput[vneseno_slov] = "";
            vneseno_slov--;


            // vibor 





            otvet_refresh();
        }

        private void urok_resart()
        {
            MessageBox.Show("Congratulations ! /n you win !");
            init2();
        }


        private void init2()
        {

            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel1.Controls.Clear();
            ///////////////////////////// что это ???????
            ///////////////////////////// что это ???????
            ///////////////////////////// что это ???????
            ///////////////////////////// что это ???????
            ///
            for (int i1 = 0; i1 < mas_int.Length; i1++)
            {
                mas_int[i1] = 0;
                //   mas_otv_flag[i1] = 0;
            }
            vneseno_slov = 0;
            kolichestvo_slov = 0;



            String inputfr = load_from_txt();

            label1.Text = inputfr;

            zagruzka_frazy(" " + inputfr);

            start001();


            ;
        }


        void Click_vibor_polzovatela(object sender, EventArgs ev1)
        {

            Label cucerrentLabel1 = (Label)sender;

            int NumInArr = int.Parse(cucerrentLabel1.Name);
            if (mas_int[NumInArr] == 0)
            {
                count_inpuut_word++;
                cucerrentLabel1.ForeColor = Color1_sets4;
                cucerrentLabel1.BackColor = Color2_sets4;
                mas_int[NumInArr] = 1;

                vneseno_slov++;
                massiv__ouput[vneseno_slov] = cucerrentLabel1.Text;
                massiv__ouput_link[vneseno_slov] = cucerrentLabel1;

                otvet_refresh();



                if (vneseno_slov == kolichestvo_slov-1)
                {

                
                   bool foundanswer = check_answer();

                    if (foundanswer)
                    {
                        urok_resart(); 
                    }
                 }
                
            }
            else
            {
                //cucerrentLabel1.ForeColor = Color.DarkBlue;
                //cucerrentLabel1.BackColor = Color.Aqua;
                //mas_int[NumInArr] = 0;
                ////  flowLayoutPanel2refresh();
                //;
            }



        }


        bool check_answer()
        {

            bool identical = true;

            for (int i1 = 1; i1 < kolichestvo_slov; i1++)

                if ( massiv_etalon[i1] != massiv__ouput[i1] )
                {
                    identical = false;
                    break;
                }
            return identical;

        }



        private void start001()
        {
            for (int i1 = 1; i1 < kolichestvo_slov; i1++)
            {
                Label ls = vnesti_v_spisok_vibora(i1);
                flowLayoutPanel1.Controls.Add(ls);

                ls.Click += new EventHandler(this.Click_vibor_polzovatela);
            }
        }
        


        private void Form1_Load(object sender, EventArgs e)
        {
            init2();


        }

        private string load_from_txt()
        {
          //  string[] lines = System.IO.File.ReadAllLines(@"C:\GIT\vadim2\_tmp\text\pro_eng.txt");
          ///  string[] line_s_rus = System.IO.File.ReadAllLines(@"C:\GIT\vadim2\_tmp\text\pro_rus.txt", Encoding.GetEncoding(1251));
            string[] lines = System.IO.File.ReadAllLines(@"Texteng.txt");
            string[] line_s_rus = System.IO.File.ReadAllLines(@"Textrus3.txt", Encoding.GetEncoding(1251));
            string[] line_s_rus2 = System.IO.File.ReadAllLines(@"Textrus2.txt", Encoding.GetEncoding(1251));
            string[] line_s_eng2 = System.IO.File.ReadAllLines(@"Texteng2.txt", Encoding.GetEncoding(1251));
            var rand001 = new Random();
            int jrand = rand001.Next(lines.Length);

            label2.Text = line_s_rus[jrand];
            label4.Text = line_s_eng2[jrand];
            label3.Text = Convert.ToString(jrand)+"  "+ line_s_rus2[jrand];

            return lines[jrand];


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            init2();
        }
    }
}
