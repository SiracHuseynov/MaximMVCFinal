using Maxim.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxim.Data.DAL
{
	public class AppDbContext : IdentityDbContext
	{
        public AppDbContext(DbContextOptions option) : base(option)
        {
            
        }

        public DbSet<Service> Services { get; set; }

        public DbSet<AppUser> Userss { get; set; }

    }
}
