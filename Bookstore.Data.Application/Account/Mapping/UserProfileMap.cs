using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Bookstore.Data.Application.Account.Entities;

namespace Bookstore.Data.Application.Account.Mapping
{
    public class UserProfileMap : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileMap()
        {
            //key  
            HasKey(t => t.ID);

            //fields  
            Property(t => t.FirstName);
            Property(t => t.LastName);
            Property(t => t.Address).HasMaxLength(100).HasColumnType("nvarchar");
            Property(t => t.AddedDate);
            Property(t => t.ModifiedDate);
            Property(t => t.IP);

            //table  
            ToTable("UserProfiles");

            //relationship  
            HasRequired(t => t.User).WithRequiredDependent(u => u.UserProfile);
        }
    }
}
