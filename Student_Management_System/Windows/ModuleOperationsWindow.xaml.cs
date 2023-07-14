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

namespace Student_Management_System.Windows
{
    /// <summary>
    /// Interaction logic for ModuleOperationsWindow.xaml
    /// </summary>
    public partial class ModuleOperationsWindow : Window
    {
        public ModuleOperationsWindow()
        {
            DataContext = new ViewModels.ModuleOperationsWindowVM();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

        }
    }
}
