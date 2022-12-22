using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace formatif.Models
{
    public class SalleRobotDb : DbContext
    {
        public DbSet<Eleve> Eleves { get; set; }
        public DbSet<Enseignant> Enseignants { get; set; }

        public DbSet<Autorisation> Autorisations { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Message> Messages { get; set; }


        public SalleRobotDb()
        {
            Database.SetInitializer( new MigrateDatabaseToLatestVersion<formatif.Models.SalleRobotDb,formatif.Migrations.Configuration>());
        }

        

    }
}