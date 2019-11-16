using Microsoft.EntityFrameworkCore;
using System;

namespace DAL2
{
    public class Class1 : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"workstation id=TestForStudents.mssql.somee.com;packet size=4096;user id=annushka_SQLLogin_1;pwd=96qgb37h6y;data source=TestForStudents.mssql.somee.com;persist security info=False;initial catalog=TestForStudents");
        }
    }
}
