using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Student_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Student_Management_System.ViewModels
{
    public partial class StudentModuleGradeVM:ObservableObject 
    {
        [ObservableProperty]
        public int stdId;

        [ObservableProperty]
        public int modid;

        [ObservableProperty]
        public string firstName;

        [ObservableProperty]
        ObservableCollection<Result> studentResults;

        [ObservableProperty]
        ObservableCollection<Result> selectedModules;

        [ObservableProperty]
        ObservableCollection<int> mdidstocombo;

       

        public StudentModuleGradeVM()
        {
            using (var db = new DataBaseContext())
            {

                var list =db.Modules.Select(u=>u.ModuleID).ToList();
                Mdidstocombo=new ObservableCollection<int>(list);
            }
        }


            [RelayCommand]
        public void SelectStudent()
        {
            try
            {
                using (var db = new DataBaseContext())
                {


                    Student check = db.Students.Find(StdId);
                    if (check == null)
                    {
                        MessageBox.Show("Student Not Found","Error");
                        return;
                    }


                    LoadData(StdId);

                }
            }
            catch
            {
                MessageBox.Show("Results Not Found", "No");
            }
        }


        public void LoadData(int id)
        {
            if (StdId == 0 || StdId == null) {
                MessageBox.Show("Select the Student first by Giving the Student Id", "Error");
                return ;
            }
            else
            {
                using (var db = new DataBaseContext())
                {
                    //var list = db.Modules.ToList();
                    var grd = db.Grades.Where(Result => Result.StudentId == id).ToList();
                    StudentResults = new ObservableCollection<Result>(grd);
                }

            }
            
        }

       


        public void GpaCal()
        {
            double tot = 0;
            double tot_cred = 0;
            try
            {
                foreach (var r in studentResults)
                {
                    using (var db = new DataBaseContext())
                    {
                        Module m = db.Modules.Find(r.ModuleID);
                        int cred = m.Credit;
                        tot_cred += cred;
                        tot += cred * r.Grade;
                    }
                }

                using (var db = new DataBaseContext())
                {
                    Student s = db.Students.Find(StdId);
                    s.GPA = tot / tot_cred;
                    db.SaveChanges();
                }

                MessageBox.Show("GPA calculated", "Successful");


            }
            catch
            {
                MessageBox.Show("Unhandled Exception");
            }
        }

        


        [RelayCommand]
        public void Save()

        {

            

            try
            {

                foreach (var result in StudentResults)
                {
                    using (var db = new DataBaseContext())
                    {

                        Result checkd = db.Grades.Find(result.Id);
                        if (checkd == null)
                        {
                            MessageBox.Show("Results Not Found");
                         
                            return;
                        }
                        checkd.Grade = result.Grade;
                        db.SaveChanges();


                    }


                }

                MessageBox.Show("Grades saved.");
                GpaCal();


            }
            catch
            {
                MessageBox.Show("Unhandled Exception", "Error");
            }



        }





        [RelayCommand]
        public void Addmodule()
        {
            if (StdId == 0 || StdId == null)
            {
                MessageBox.Show("Select the Student first by Giving the Student Id", "Error");
                return;
            }
            else
            {
                try
                {
                    using (var db = new DataBaseContext())
                    {



                        Module a = db.Modules.Find(Modid);
                        if (a == null)
                        {
                            MessageBox.Show("Enter a valid Module ID", "Error");
                            return;
                        }



                    }




                    Result r = new Result()
                    {
                        StudentId = stdId,
                        ModuleID = Modid,
                        Grade = 0
                    };
                    using (var db = new DataBaseContext())
                    {
                        Result g = db.Grades.SingleOrDefault(g => g.ModuleID == Modid && g.StudentId == StdId);

                        if (g == null)
                        {
                            db.Grades.Add(r);
                            db.SaveChanges();
                            
                            LoadData(StdId);
                            MessageBoxResult result = MessageBox.Show("Module succesfully Added to student", "Done");
                        }
                        else
                        {
                            MessageBox.Show("Module Already exists", "Error");
                            return;
                        }


                    }
                }
                catch
                {
                    MessageBox.Show("Error", "No");
                }
            }

        }

        [RelayCommand]
        public void Delete()
        {

            if (StdId == 0 || StdId == null)
            {
                MessageBox.Show("Select the Student first by Giving the Student Id", "Error");
                return;
            }
            else
            {
                try
                {
                    if (Modid == 0 || Modid == null)
                    {
                        MessageBoxResult result1 = MessageBox.Show("Enter the Module ID of the Module");
                        return;

                    }

                    using (var db = new DataBaseContext())
                    {

                        Result g = db.Grades.SingleOrDefault(g => g.ModuleID == Modid && g.StudentId == StdId);

                        db.Grades.Remove(g);
                        db.SaveChanges();
                        LoadData(StdId);
                        MessageBoxResult result = MessageBox.Show("Module successfully Deleted", "Done");

                    }



                }
                catch (Exception ex)
                {
                    MessageBoxResult result = MessageBox.Show("Invalid Module ID", "Done");
                }
            }

        }



        


    }
}
