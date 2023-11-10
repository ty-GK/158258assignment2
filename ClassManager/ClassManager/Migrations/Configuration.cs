namespace ClassManager.Migrations
{
    using ClassManager.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.UI;
    using System.Xml.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ClassManager.Data.ClassManagerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ClassManager.Data.ClassManagerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var users = new List<User>
            {
                new User { UserName = "dkh", Password = "123123", ConfirmPassword = "123123"},
                new User { UserName = "tian", Password = "qweqwe", ConfirmPassword = "qweqwe"}
            };
            users.ForEach(c => context.Users.AddOrUpdate(p => p.UserName, c));
            context.SaveChanges();


            var students = new List<Student>
            {
                new Student { Name="Ethan", StudentNumber="2018023", EnrollmentYear = 2018, Gender = "male", Age = 19, TeacherId="dkh"},
                new Student { Name="Olivia", StudentNumber="2018602", EnrollmentYear = 2018, Gender = "male", Age = 18, TeacherId="dkh"},
                new Student { Name="Liam", StudentNumber="2018003", EnrollmentYear = 2018, Gender = "female", Age = 19, TeacherId="dkh"},
                new Student { Name="Emma", StudentNumber="2019064", EnrollmentYear = 2019, Gender = "female", Age = 16, TeacherId="dkh"},
                new Student { Name="Noah", StudentNumber="2019805", EnrollmentYear = 2019, Gender = "male", Age = 17, TeacherId="dkh"},
                new Student { Name="Ava", StudentNumber="2019236", EnrollmentYear = 2019, Gender = "female", Age = 17, TeacherId="dkh"},
                new Student { Name="Mason", StudentNumber="2020132", EnrollmentYear = 2020, Gender = "female", Age = 16, TeacherId="dkh"},
                new Student { Name="Sophia", StudentNumber="2020467", EnrollmentYear = 2020, Gender = "female", Age = 15, TeacherId="dkh"},
                new Student { Name="Benjamin", StudentNumber="2020520", EnrollmentYear = 2020, Gender = "male", Age = 16, TeacherId="dkh"},
                new Student { Name="Isabella", StudentNumber="2020876", EnrollmentYear = 2020, Gender = "female", Age = 15, TeacherId="dkh"},

                new Student { Name = "William", StudentNumber = "2020011", EnrollmentYear = 2020, Gender = "male", Age = 20, TeacherId = "tian" },
                new Student { Name = "Charlotte", StudentNumber = "2020012", EnrollmentYear = 2020, Gender = "female", Age = 18, TeacherId = "tian" },
                new Student { Name = "Alexander", StudentNumber = "2020013", EnrollmentYear = 2020, Gender = "male", Age = 17, TeacherId = "tian" },
                new Student { Name = "Amelia", StudentNumber = "2020014", EnrollmentYear = 2020, Gender = "female", Age = 19, TeacherId = "tian" },
                new Student { Name = "Henry", StudentNumber = "2021015", EnrollmentYear = 2021, Gender = "male", Age = 18, TeacherId = "tian" },
                new Student { Name = "Harper", StudentNumber = "2021016", EnrollmentYear = 2021, Gender = "female", Age = 17, TeacherId = "tian" },
                new Student { Name = "Lucas", StudentNumber = "2021017", EnrollmentYear = 2021, Gender = "male", Age = 20, TeacherId = "tian" },
                new Student { Name = "Mila", StudentNumber = "2021018", EnrollmentYear = 2021, Gender = "female", Age = 18, TeacherId = "tian" },
                new Student { Name = "Aiden", StudentNumber = "2021019", EnrollmentYear = 2021, Gender = "male", Age = 17, TeacherId = "tian" },
                new Student { Name = "Luna", StudentNumber = "2021020", EnrollmentYear = 2021, Gender = "female", Age = 19, TeacherId = "tian" }
            };

            students.ForEach(c => context.Students.Add(c));
            context.SaveChanges();

        }
    }
}
