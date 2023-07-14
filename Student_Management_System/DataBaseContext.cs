using Microsoft.EntityFrameworkCore;
using Student_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System
{
    public class DataBaseContext :DbContext
    {
        private readonly string data_path = @"C:\Users\Asus\Desktop\Group_Project\newone\Student_Management_System\Student_Management_System\DataBase\DataBase.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite($"Data source = {data_path}");

        public DbSet<Student> Students { get; set; }

        public DbSet<Module> Modules { get; set; }

        public DbSet<User> Users { get; set; }

         public DbSet<Result> Grades { get; set; }

       

    }
}
