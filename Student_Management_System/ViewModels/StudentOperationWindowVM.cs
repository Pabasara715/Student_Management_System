using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Student_Management_System.Models;
using Student_Management_System.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Student_Management_System.ViewModels
{
    public partial class StudentOperationWindowVM : ObservableObject 
    {
        [ObservableProperty]
        public int id;
        [ObservableProperty]
        public string firstname;
        [ObservableProperty]
        public string lastname;
        [ObservableProperty]
        public int age;
        [ObservableProperty] 
        public string address;
               

        [ObservableProperty]
        ObservableCollection<Student> students;

        public void LoadStudents()
        {
            using (var db = new DataBaseContext())
            {
                var list = db.Students.ToList();
                Students = new ObservableCollection<Student>(list);
            }
        }
        public StudentOperationWindowVM()
        {
            LoadStudents();
        }
        public void Clear()
        {
            Id = 0;
            Firstname = null;
            Lastname = null;
            Address = null;
            Age = 0;     
            
        }

        [RelayCommand]
        public void AddStudent()
        {
            using (var db = new DataBaseContext())
            {
                int stdId = Id;
                bool cheak = db.Students.Any(Student => Student.Id == stdId);
                if (cheak)
                {
                    MessageBoxResult result3 = MessageBox.Show("Student id is already exist", "Error");
                }
                else
                {
                    if (Id != null && Firstname != null && Lastname != null && Address != null && Age != null)
                    {

                        Models.Student s = new Models.Student()
                        {
                            Id = Id,
                             FirstName = Firstname,
                         LastName = Lastname,
                         Address = Address,
                            Age = Age

                        };

                        db.Students.Add(s);
                        db.SaveChanges();
                        LoadStudents();
                        Clear();
                        MessageBoxResult result = MessageBox.Show("Student Added successfully ", "Done");

                    }
                    else
                    {
                        MessageBoxResult result2 = MessageBox.Show("Enter all data", "Error");
                    }

                }
            }
             
        }

        [RelayCommand]
        public void DeleteStudent()
        {
            try
            {
                int stdId = Id;
                if (stdId == 0)
                {
                    MessageBoxResult result1 = MessageBox.Show("Enter the Student ID of the Student");
                    return;
                }
                using (var db = new DataBaseContext())
                {
                    Models.Student s = db.Students.Find(stdId);

                    db.Students.Remove(s);
                    db.SaveChanges();
                    LoadStudents();
                    MessageBoxResult result = MessageBox.Show("Student Deleted successfully ", "Done");
                }
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show("Invalid Student ID", "Done");
            }

        }


        [RelayCommand]
        public void EditStudent()
        {
            if (Id == null||Id==0)
            {
                MessageBox.Show("Enter the Student Id to Edit", "Error");
            }
            else
            {

              if (Id != null && Firstname != null && Lastname != null && Address != null && Age != null)
              {

                    int stdId = Id;

                using (var db = new DataBaseContext())
                {

                    bool cheak = db.Students.Any(Student => Student.Id == stdId);
                    if (cheak)
                    {
                        Models.Student s = db.Students.Find(stdId);

                       
                            s.FirstName = Firstname;
                            s.LastName = Lastname;
                            s.Address = Address;
                            s.Age = Age;
                            db.SaveChanges();
                            LoadStudents();
                            MessageBox.Show("Student Successfully Edited", "Done");
                            Clear();
                       

                    }
                    else
                    {
                        MessageBox.Show("Student not found", "Error");
                    }

                }
              }
              else
              {
                    MessageBox.Show("Enter all the Data", "Error");

              }


            }



        }
        [RelayCommand]
        public void StudentModuleGrade()
        {
            var Window = new Windows.StudentModuleGrade();
            Window.Show();

        }

      



    }
}
