using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XainportWeb.Models;

namespace XainportWeb.Data
{
    public class XainportWebContext : DbContext
    {
        public XainportWebContext (DbContextOptions<XainportWebContext> options)
            : base(options)
        {
        }

        public DbSet<XainportWeb.Models.Covid19AttestationModel> Covid19AttestationModel { get; set; }
    }
}
