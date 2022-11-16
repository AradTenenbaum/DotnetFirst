using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI;

    public class SuperHeroContext : DbContext
    {
        public SuperHeroContext (DbContextOptions<SuperHeroContext> options)
            : base(options)
        {

        }

        public DbSet<SuperHeroAPI.SuperHero> SuperHeros { get; set; }
    }
