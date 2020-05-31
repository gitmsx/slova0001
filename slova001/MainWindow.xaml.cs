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

namespace slova001
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

    private void butt002_Click(object sender, RoutedEventArgs e)
    {




            for (int i = 0; i < 34; i++)
            {
                Button newBtn = new Button();
                newBtn.Content = "Button  ==-" + i.ToString();
                newBtn.Name = "Button" + i.ToString();
                newBtn.Height = 23;
                WrapPanel002.Children.Add(newBtn);
                
            }

       


        }

    
    }
}
