using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class EfContext : DbContext
    {
        public EfContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"workstation id=TestForStudents.mssql.somee.com;packet size=4096;user id=annushka_SQLLogin_1;pwd=96qgb37h6y;data source=TestForStudents.mssql.somee.com;persist security info=False;initial catalog=TestForStudents");
        }

        public DbSet<Phone> Phones { get; set; }
    }
}
