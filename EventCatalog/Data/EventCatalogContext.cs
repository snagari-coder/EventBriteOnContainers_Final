using EventCatalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace EventCatalog.Data
{
    public class EventCatalogContext : DbContext
    {

        public EventCatalogContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<EventAudience> EventAudiences { get; set; }

        public DbSet<EventCategory> EventCategories { get; set; }

         public DbSet<EventKind> EventKinds { get; set; }

        public DbSet<EventLanguage> EventLanguages { get; set; }

        public DbSet<EventFormat> EventFormats { get; set; }

        public DbSet<EventLocation> EventLocations { get; set; }


        public DbSet<EventZipCode> EventZipCodes { get; set; }
         
        public DbSet<EventItem> EventItems { get; set; }

       // public DbSet<EventUserInfo> EventUserInfos { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventAudience>(e =>
            {
                e.ToTable("EventAudiences");
                e.Property(E => E.Id)
                .IsRequired()
                .UseHiLo("Event_Audience_hilo"); 
                e.Property(E => E.Event_AgeGroup)
                .IsRequired()
                .HasMaxLength(50);
            });

            modelBuilder.Entity<EventCategory>(e =>
            {
                e.ToTable("EventCategories");
                e.Property(E => E.Id)
                .IsRequired()
                .UseHiLo("Event_Category_hilo"); 
                e.Property(E => E.Event_Category)
                .IsRequired()
                .HasMaxLength(50);
            });

            modelBuilder.Entity<EventKind>(e =>
            {
                e.ToTable("EventKinds");
                e.Property(E => E.Id)
                .IsRequired()
                .UseHiLo("Event_Kind_hilo");
                e.Property(E => E.Event_Kind)
                .IsRequired()
                .HasMaxLength(50);
            });


            modelBuilder.Entity<EventLanguage>(e =>
            {
                e.ToTable("EventLanguages");
                e.Property(E => E.Id)
                .IsRequired()
                .UseHiLo("Event_Language_hilo");
                e.Property(E => E.Event_Language)
                .IsRequired()
                .HasMaxLength(50);
            });

            modelBuilder.Entity<EventFormat>(e =>
            {
                e.ToTable("EventFormats");
                e.Property(E => E.Id)
                .IsRequired()
                .UseHiLo("Event_Format_hilo");
                e.Property(E => E.Event_Format)
                .IsRequired()
                .HasMaxLength(50);
            });

            modelBuilder.Entity<EventLocation>(e =>
            {
                e.ToTable("EventLocations");
                e.Property(E => E.Id)
                .IsRequired()
                
                .UseHiLo("Event_Location_hilo");
                e.Property(E => E.Event_Location)
                .IsRequired()
                .HasMaxLength(50);



            });
            
         

          

            modelBuilder.Entity<EventZipCode>(e =>
            {
                e.ToTable("EventZipcodes");
                e.Property(E => E.Id)
                .IsRequired()
                .UseHiLo("Event_Zipcode_hilo");
                e.Property(E => E.Event_Zipcode)
                .IsRequired()
                .HasMaxLength(50);
            });

       

            modelBuilder.Entity<EventItem>(e =>
            {
                e.ToTable("EventItems");
                e.Property(E=>E.Id)
                .IsRequired()
                .UseHiLo("Event_Item_hilo");

                e.Property(E => E.Event_Name)
                .IsRequired()
                .HasMaxLength(200);

                e.Property(E => E.Event_Desc)
               .IsRequired()
               .HasMaxLength(500);

              

                e.Property(E => E.Event_Start_Time)
                .IsRequired();

                e.Property(E => E.Event_End_Time)
               .IsRequired();

                e.Property(E => E.Event_Organiser)
               .IsRequired();


                e.Property(E => E.Event_Price)
                .IsRequired()
                .HasColumnType("Decimal");

                e.HasOne(e => e.Event_Location)
                .WithMany()
                .HasForeignKey(e => e.Event_LocationId);

                e.HasOne(e => e.Event_Category)
                .WithMany()
                .HasForeignKey(e => e.Event_CategoryId);

                
                e.HasOne(e => e.Event_Language)
                .WithMany()
                .HasForeignKey(e => e.Event_LanguageId);

                e.HasOne(e => e.Event_Kind)
                .WithMany()
                .HasForeignKey(e => e.Event_KindId);

                e.HasOne(e => e.Event_ZipCode)
                .WithMany()
                .HasForeignKey(e => e.Event_ZipCodeId);


                e.HasOne(e => e.Event_Audience)
                .WithMany()
                .HasForeignKey(e => e.Event_AudienceId);
              
                e.HasOne(e => e.Event_Format)
                .WithMany()
                .HasForeignKey(e => e.Event_FormatId);
            }

            );


        }
    }
}
