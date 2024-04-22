﻿using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Data
{


    // context for each table in the database
    // allows us to connect to the database and interact with it
    public class ItemsContext : IdentityDbContext
    {
        // this is the constructor being called in Program.cs services page
        public ItemsContext(DbContextOptions<ItemsContext> options) : base(options)
        {
        }

        public DbSet<ItemModel> Items { get; set; }
    }
}
