using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Student_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Student_Management_System.ViewModels
{
    public partial class MainWindowVM: ObservableObject
    {
        [ObservableProperty]
        string username;
        [ObservableProperty]
        string password;
   
        [RelayCommand]
        public void Login()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                bool check = context.Users.Any(User => User.Username == username && User.Password == password);

                User user = context.Users.FirstOrDefault(User => User.Username == username && User.Password == password);
                if (check) {
                    if (user.UserType == "admin")
                    {

                        var Window = new Windows.AdminWindow();
                        Window.Show();
                    }

                    else if (user.UserType == "normal")
                    {
                        var Window = new Windows.UserWindow();
                        Window.Show();
                    }
                }
                else 
                {
                    MessageBox.Show("User not Found!", "Invalid User"); 
                }
                
                
              
               
            }
        }
            
        

    }
}
