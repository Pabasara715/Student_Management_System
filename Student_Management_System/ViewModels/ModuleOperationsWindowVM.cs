using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;
using Student_Management_System.Models;

namespace Student_Management_System.ViewModels
{
    public partial class ModuleOperationsWindowVM : ObservableObject
    {
        [ObservableProperty]
        public int moduleid;
        [ObservableProperty]
        public string modulename;
        [ObservableProperty]
        public string code;       
        [ObservableProperty] 
        public int credit;
        [ObservableProperty]
        ObservableCollection<Models.Module> modules;
        

        public void LoadModules()
        {
            using (var db = new DataBaseContext())
            {
                var list = db.Modules.ToList();
                Modules = new ObservableCollection<Models.Module>(list);
            }
        }

        public ModuleOperationsWindowVM()
        {

            LoadModules();
        }

        public void Clear()
        {
            Moduleid = 0;
            Modulename = "";
            Code = "";
            Credit = 0;
          
        }

        [RelayCommand]
        public void AddModule()
        {
            using (var db = new DataBaseContext())
            {
                int moduleId = Moduleid;
                bool cheak = db.Modules.Any(Module => Module.ModuleID == moduleId);
                if (cheak) 
                {
                    MessageBoxResult result3 = MessageBox.Show("ModuleId is already taken", "Error");
                } 
                else
                {
                    if (Moduleid != null && Modulename != null && Credit != null && Code != null)
                    {
                        
                            Models.Module m = new Models.Module()
                            {
                                ModuleID = Moduleid,
                                ModuleName = Modulename,
                                ModuleCode = Code,
                                Credit = Credit

                            };
                            
                                db.Modules.Add(m);
                                db.SaveChanges();
                                LoadModules();
                                Clear();
                                MessageBoxResult result = MessageBox.Show("Module Added successfully ", "Done");
                        
                    }
                    else
                    {
                        MessageBoxResult result3 = MessageBox.Show("Enter all data", "Error");
                    }

                }
            }
                

        }

        [RelayCommand]
        public void DeleteModule()
        {
            try
            {
                int moduleId = Moduleid;
                if (moduleId == 0)
                {
                    MessageBoxResult result1 = MessageBox.Show("Enter the Module ID of the Module");
                    return;
                }
                using (var db = new DataBaseContext())
                {
                    Models.Module m = db.Modules.Find(moduleid);

                    db.Modules.Remove(m);
                    db.SaveChanges();
                    LoadModules();
                    MessageBoxResult result = MessageBox.Show("Module successfully Deleted", "Done");
                }
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show("Invalid Module ID", "Done");
            }

        }

        [RelayCommand]
        public void EditModule()
        {
            
                int moduleId = Moduleid;

            using (var db = new DataBaseContext())
            {


                bool cheak = db.Modules.Any(Module => Module.ModuleID == moduleId);
                if (cheak)
                {
                    Models.Module m = db.Modules.Find(moduleId);

                    if (Moduleid != null && Modulename != null && Credit != null && Code != null)
                    {

                        m.ModuleName = Modulename;
                        m.ModuleID = Moduleid;
                        m.ModuleCode = Code;
                        db.SaveChanges();
                        LoadModules();
                        MessageBox.Show("Successfully edited", "Done");
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Enter all the data", "Error");

                    }


                }
                else
                {
                    MessageBox.Show("Module not found", "Error");
                }

            }

        }





    }
}
