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
using System.Windows.Shapes;
using TheDeptBook_Assignment1.Models;

namespace TheDeptBook_Assignment1
{
    /// <summary>
    /// Interaction logic for DebtorDetailsWindow.xaml
    /// </summary>
    public partial class DebtorDetailsWindow : Window
    {
        public DebtorDetailsWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
