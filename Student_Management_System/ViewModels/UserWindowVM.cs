using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Student_Management_System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.ViewModels 
{
    public partial class UserWindowVM : ObservableObject
    {
        [RelayCommand]
        public void StudentOperation()
        {
            var Window = new StudentOperationWindow();
            Window.Show();

        }

        [RelayCommand]
        public void ModuleOperation()
        {
            var Window = new ModuleOperationsWindow();
            Window.Show();

        }


    }
}

    

