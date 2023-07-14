using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Student_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Windows.Controls;
using System.Windows;

namespace Student_Management_System.ViewModels
{
    public partial class AdminWindowVM : ObservableObject
 
    {
        
        [ObservableProperty] 
        public string username;
        [ObservableProperty] 
        public string password;
        [ObservableProperty] 
        public string usertype;      
        [ObservableProperty]
        ObservableCollection<User> users;
        public Action CloseAction { get; internal set; }
        [ObservableProperty]
        public ObservableCollection<string> rolltype;

        



        public void LoadUsers()
        {
            using (var db = new DataBaseContext())
            {
                var list = db.Users.ToList();
                Users = new ObservableCollection<User>(list);
            }
        }

        public void Clear()
        {
            Username = "";
            Password = "";
            Usertype = "";
            

        }

        [RelayCommand]
        public void InsertUser()
        {
            if (Username != null && Password != null && Usertype != null)
            {
                User u = new User()
                {
                    Username = Username,
                    Password = Password,
                    UserType = Usertype,


                };
                using (var db = new DataBaseContext())
                {
                    db.Users.Add(u);
                    db.SaveChanges();
                    LoadUsers();
                    MessageBoxResult result = MessageBox.Show("User Added successfully", "Done");
                    Clear();
                }
            }
            else
            {
                MessageBoxResult result2 = MessageBox.Show("All the data should be filled", "Invalid");
                Clear();

            }


        }

        public AdminWindowVM()
        {
            Rolltype = new ObservableCollection<string>();
            Rolltype.Add("admin");
            Rolltype.Add("normal");
            LoadUsers();
        }

        [RelayCommand]
        public void DeleteUser()
        {
            try
            {
                string username = Username;
                if (username == "")
                {
                    MessageBoxResult result1 = MessageBox.Show("You should enter the Username", "Error");
                    return;
                }
                using (var db = new DataBaseContext())
                {
                    User u = db.Users.Find(username);

                    db.Users.Remove(u);
                    db.SaveChanges();
                    LoadUsers();
                    MessageBoxResult result = MessageBox.Show("User Deleted successfully", "Done");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show("Invalid Username", "Error");
                Clear();
            }

        }

        [RelayCommand]
        public void EditUser()
        {
            string username = Username;
            string password = Password;
            string usertype = Usertype;
            
            if (username != null&& password != null &&usertype!=null)
            {
                using (var db = new DataBaseContext())
                {


                    bool cheak = db.Users.Any(User => User.Username == username);
                    if (cheak)
                    {
                        User u = db.Users.Find(username);

                        if (Username != null && Password != null && Usertype != null)
                        {
                            u.Username = Username;
                            u.Password = Password;
                            u.UserType = Usertype;
                            db.SaveChanges();
                            LoadUsers();
                            MessageBox.Show("Successfully edited", "Done");
                            Clear();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Data", "Error");

                        }


                    }
                    else
                    {
                        MessageBox.Show("User not found", "Error");
                    }

                }

                
            }
            else {
                MessageBoxResult result1 = MessageBox.Show("To Edit Fill All the Data", "Error");
                return;
            }
            
           

                
        }
        



    }
}
