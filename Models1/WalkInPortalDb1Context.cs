using System;
using System.Collections.Generic; 
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Backend.Models1
{
    public partial class WalkInPortalDb1Context : DbContext
    {
        public WalkInPortalDb1Context()
        {
        }

        public WalkInPortalDb1Context(DbContextOptions<WalkInPortalDb1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AllJobRolesOfUser> AllJobRolesOfUsers { get; set; }

        public virtual DbSet<College> Colleges { get; set; }

        public virtual DbSet<DriveApplied> DriveApplieds { get; set; }

        public virtual DbSet<DriveAvailableTime> DriveAvailableTimes { get; set; }

        public virtual DbSet<EdiucationInfo> EdiucationInfos { get; set; }

        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

        public virtual DbSet<JobRole> JobRoles { get; set; }

        public virtual DbSet<PersonalInfo> PersonalInfos { get; set; }

        public virtual DbSet<Prerequisite> Prerequisites { get; set; }

        public virtual DbSet<ProfessionalQualificationInfo> ProfessionalQualificationInfos { get; set; }

        public virtual DbSet<Qualification> Qualifications { get; set; }

        public virtual DbSet<Round> Rounds { get; set; }

        public virtual DbSet<Slot> Slots { get; set; }

        public virtual DbSet<Stream> Streams { get; set; }

        public virtual DbSet<TechnologyTable> TechnologyTables { get; set; }

        public virtual DbSet<UserReg> UserRegs { get; set; }

        public virtual DbSet<WalkInDrife> WalkInDrives { get; set; }

        public virtual DbSet<professional_qualification_info_has_technology_familier_table> professional_qualification_info_has_technology_familier_table {  get; set; }

        public virtual DbSet<professional_qualification_info_has_technology_expert_table> professional_qualification_info_has_technology_expert_table { get; set; }

        public virtual DbSet<user_reg_has_all_job> user_reg_has_all_job { get; set; }

        public virtual DbSet<walk_in_drives_has_job_roles> walk_in_drives_has_job_roles { get; set; }

        public virtual DbSet<drive_applied_has_job_roles> drive_applied_has_job_roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseMySql("server=localhost;database=walk_in_portal_db1;user=root;password=Tejas@2003", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<walk_in_drives_has_job_roles>().HasKey(m => new { m.Drive_id, m.Role_id });
            modelBuilder.Entity<professional_qualification_info_has_technology_familier_table>().HasKey(m => new { m.Id, m.Tech_id });
            modelBuilder.Entity<professional_qualification_info_has_technology_expert_table>().HasKey(m => new { m.Id, m.Tech_id });

            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<AllJobRolesOfUser>(entity =>
            {
                entity.HasKey(e => e.RoleId).HasName("PRIMARY");

                entity
                    .ToTable("all_job_roles_of_user")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.RoleId).HasColumnName("Role_id");
                entity.Property(e => e.Name).HasMaxLength(45);
            });

            modelBuilder.Entity<College>(entity =>
            {
                entity.HasKey(e => e.Collegeid).HasName("PRIMARY");

                entity
                    .ToTable("college")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");
                  
                entity.Property(e => e.CollegeLocaton)
                    .HasMaxLength(45)
                    .HasColumnName("College_locaton");
                entity.Property(e => e.Name).HasMaxLength(45);
            });

            modelBuilder.Entity<DriveApplied>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.DriveId, e.SlotsId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity
                    .ToTable("drive_applied")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.HasIndex(e => e.Id, "fk_Drive_applied_User_reg1_idx");

                entity.HasIndex(e => new { e.SlotsId, e.DriveId }, "fk_Drive_applied_drive_available_time1_idx");

                entity.Property(e => e.DriveId).HasColumnName("Drive_id");
                entity.Property(e => e.SlotsId).HasColumnName("Slots_id");
                entity.Property(e => e.DtCreated).HasColumnName("dt_created");
                entity.Property(e => e.DtUpdated).HasColumnName("dt_updated");
                entity.Property(e => e.Resume).HasMaxLength(45);

                entity.HasOne(d => d.IdNavigation).WithMany(p => p.DriveApplieds)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Drive_applied_User_reg1");

                entity.HasOne(d => d.DriveAvailableTime).WithMany(p => p.DriveApplieds)
                    .HasForeignKey(d => new { d.SlotsId, d.DriveId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Drive_applied_drive_available_time1");

                entity.HasMany(d => d.Roles).WithMany(p => p.DriveApplieds)
                    .UsingEntity<Dictionary<string, object>>(
                        "DriveAppliedHasJobRole",
                        r => r.HasOne<JobRole>().WithMany()
                            .HasForeignKey("RoleId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk_Drive_applied_has_Job_roles_Job_roles1"),
                        l => l.HasOne<DriveApplied>().WithMany()
                            .HasForeignKey("Id", "DriveId", "SlotsId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk_Drive_applied_has_Job_roles_Drive_applied1")
                        //j =>
                        //{
                        //    j.HasKey("Id", "DriveId", "SlotsId", "RoleId")
                        //        .HasName("PRIMARY")
                        //        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });
                        //    j
                        //        .ToTable("drive_applied_has_job_roles")
                        //        .HasCharSet("utf8mb3")
                        //        .UseCollation("utf8mb3_general_ci");
                        //    j.HasIndex(new[] { "Id", "DriveId", "SlotsId" }, "fk_Drive_applied_has_Job_roles_Drive_applied1_idx");
                        //    j.HasIndex(new[] { "RoleId" }, "fk_Drive_applied_has_Job_roles_Job_roles1_idx");
                        //    j.IndexerProperty<int>("DriveId").HasColumnName("Drive_id");
                        //    j.IndexerProperty<int>("SlotsId").HasColumnName("Slots_id");
                        //    j.IndexerProperty<int>("RoleId").HasColumnName("Role_id");
                        //}
                        );
            });

            modelBuilder.Entity<DriveAvailableTime>(entity =>
            {
                entity.HasKey(e => new { e.SlotsId, e.WalkInDrivesDriveId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity
                    .ToTable("drive_available_time")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.HasIndex(e => e.SlotsId, "fk_drive_available_time_Slots1_idx");

                entity.HasIndex(e => e.WalkInDrivesDriveId, "fk_drive_available_time_Walk_in_drives1_idx");

                entity.Property(e => e.SlotsId).HasColumnName("Slots_id");
                entity.Property(e => e.WalkInDrivesDriveId).HasColumnName("Walk_in_drives_Drive_id");

                entity.HasOne(d => d.Slots).WithMany(p => p.DriveAvailableTimes)
                    .HasForeignKey(d => d.SlotsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_drive_available_time_Slots1");

                entity.HasOne(d => d.WalkInDrivesDrive).WithMany(p => p.DriveAvailableTimes)
                    .HasForeignKey(d => d.WalkInDrivesDriveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_drive_available_time_Walk_in_drives1");
            });

            modelBuilder.Entity<EdiucationInfo>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Collegeid, e.Streamid, e.QualificationId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity
                    .ToTable("ediucation_info")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.HasIndex(e => e.Collegeid, "fk_Ediucation_info_College1_idx");

                entity.HasIndex(e => e.QualificationId, "fk_Ediucation_info_Qualification1_idx");

                entity.HasIndex(e => e.Streamid, "fk_Ediucation_info_Stream1_idx");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.QualificationId).HasColumnName("Qualification_id");
                entity.Property(e => e.PassingYear).HasColumnName("Passing_year");

                entity.HasOne(d => d.College).WithMany(p => p.EdiucationInfos)
                    .HasForeignKey(d => d.Collegeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Ediucation_info_College1");

                entity.HasOne(d => d.IdNavigation).WithMany(p => p.EdiucationInfos)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Ediucation_info_User_reg1");

                entity.HasOne(d => d.Qualification).WithMany(p => p.EdiucationInfos)
                    .HasForeignKey(d => d.QualificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Ediucation_info_Qualification1");

                entity.HasOne(d => d.Stream).WithMany(p => p.EdiucationInfos)
                    .HasForeignKey(d => d.Streamid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Ediucation_info_Stream1");
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);
                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<JobRole>(entity =>
            {
                entity.HasKey(e => e.RoleId).HasName("PRIMARY");

                entity
                    .ToTable("job_roles")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.RoleId).HasColumnName("Role_id");
                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.Name).HasMaxLength(10000);
                entity.Property(e => e.Requirement).HasColumnType("text");
            });

            modelBuilder.Entity<PersonalInfo>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity
                    .ToTable("personal_info")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.DisplayPicture)
                    .HasMaxLength(45)
                    .HasColumnName("Display_picture");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(45)
                    .HasColumnName("FIrst_name");
                entity.Property(e => e.LastName)
                    .HasMaxLength(45)
                    .HasColumnName("Last_name");
                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(45)
                    .HasColumnName("Phone_no");
                entity.Property(e => e.PortfolioUrl)
                    .HasMaxLength(45)
                    .HasColumnName("Portfolio_url");
                entity.Property(e => e.RefferedEmployeeName)
                    .HasMaxLength(45)
                    .HasColumnName("Reffered_employee_name");
                entity.Property(e => e.Resume).HasMaxLength(45);
                entity.Property(e => e.SendJobUpdate).HasColumnName("Send_job_update");

                entity.HasOne(d => d.IdNavigation).WithOne(p => p.PersonalInfo)
                    .HasForeignKey<PersonalInfo>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Personal_info_User_reg1");
            });

            modelBuilder.Entity<Prerequisite>(entity =>
            {
                entity.HasKey(e => e.PreId).HasName("PRIMARY");

                entity
                    .ToTable("prerequisite")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.PreId)
                    .ValueGeneratedNever()
                    .HasColumnName("pre_id");
                entity.Property(e => e.Generalinstruction).HasColumnType("text");
                entity.Property(e => e.Instruction).HasColumnType("text");
                entity.Property(e => e.Minrequirement).HasColumnType("text");
            });

            modelBuilder.Entity<ProfessionalQualificationInfo>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity
                    .ToTable("professional_qualification_info")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.ApplicantType)
                    .HasMaxLength(45)
                    .HasColumnName("Applicant_type");
                entity.Property(e => e.AppliedPrevious12Months).HasColumnName("Applied_previous_12_months");
                entity.Property(e => e.CurrentCtc).HasColumnName("Current_CTC");
                entity.Property(e => e.ExpectedCtc).HasColumnName("Expected_CTC");
                entity.Property(e => e.NoticePeriod).HasColumnName("Notice_Period");
                entity.Property(e => e.NoticePeriodDuration)
                    .HasMaxLength(45)
                    .HasColumnName("Notice_period_duration");
                entity.Property(e => e.RoleForApplied)
                    .HasMaxLength(45)
                    .HasColumnName("Role_for_applied");
                entity.Property(e => e.YearsOfExperience).HasColumnName("Years_of_Experience");

                entity.HasOne(d => d.IdNavigation).WithOne(p => p.ProfessionalQualificationInfo)
                    .HasForeignKey<ProfessionalQualificationInfo>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Professional_qualification_info_User_reg");

                entity.HasMany(d => d.Teches).WithMany(p => p.Ids)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProfessionalQualificationInfoHasTechnologyExpertTable",
                        r => r.HasOne<TechnologyTable>().WithMany()
                            .HasForeignKey("TechId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk_Professional_qualification_info_has_Technology_table_Techn1"),
                        l => l.HasOne<ProfessionalQualificationInfo>().WithMany()
                            .HasForeignKey("Id")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk_Professional_qualification_info_has_Technology_table_Profe1")
                        //j =>
                        //{
                        //    j.HasKey("Id", "TechId")
                        //        .HasName("PRIMARY")
                        //        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        //    j
                        //        .ToTable("professional_qualification_info_has_technology_expert_table")
                        //        .HasCharSet("utf8mb3")
                        //        .UseCollation("utf8mb3_general_ci");
                        //    j.HasIndex(new[] { "Id" }, "fk_Professional_qualification_info_has_Technology_table_Pro_idx");
                        //    j.HasIndex(new[] { "TechId" }, "fk_Professional_qualification_info_has_Technology_table_Tec_idx");
                        //    j.IndexerProperty<int>("TechId").HasColumnName("Tech_id");
                        //}
                        );

                entity.HasMany(d => d.TechesNavigation).WithMany(p => p.IdsNavigation)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProfessionalQualificationInfoHasTechnologyFamilierTable",
                        r => r.HasOne<TechnologyTable>().WithMany()
                            .HasForeignKey("TechId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk_Professional_qualification_info_has_Technology_table1_Tech1"),
                        l => l.HasOne<ProfessionalQualificationInfo>().WithMany()
                            .HasForeignKey("Id")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk_Professional_qualification_info_has_Technology_table1_Prof1")
                        //j =>
                        //{
                        //    j.HasKey("Id", "TechId")
                        //        .HasName("PRIMARY")
                        //        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        //    j
                        //        .ToTable("professional_qualification_info_has_technology_familier_table")
                        //        .HasCharSet("utf8mb3")
                        //        .UseCollation("utf8mb3_general_ci");
                        //    j.HasIndex(new[] { "Id" }, "fk_Professional_qualification_info_has_Technology_table1_Pr_idx");
                        //    j.HasIndex(new[] { "TechId" }, "fk_Professional_qualification_info_has_Technology_table1_Te_idx");
                        //    j.IndexerProperty<int>("TechId").HasColumnName("Tech_id");
                        //}
                        );
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.HasKey(e => e.QualificationId).HasName("PRIMARY");

                entity
                    .ToTable("qualification")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.QualificationId).HasColumnName("Qualification_id");
                entity.Property(e => e.Name).HasMaxLength(45);
            });

            modelBuilder.Entity<Round>(entity =>
            {
                entity.HasKey(e => new { e.RoundId, e.DriveId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity
                    .ToTable("rounds")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.HasIndex(e => e.DriveId, "fk_Rounds_Walk_in_drives1_idx");

                entity.Property(e => e.RoundId).HasColumnName("Round_id");
                entity.Property(e => e.DriveId).HasColumnName("Drive_id");
                entity.Property(e => e.ExtraInfo)
                    .HasMaxLength(1000)
                    .HasColumnName("extra_info");
                entity.Property(e => e.Time).HasMaxLength(45);
                entity.Property(e => e.Type).HasMaxLength(100);

                entity.HasOne(d => d.Drive).WithMany(p => p.Rounds)
                    .HasForeignKey(d => d.DriveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Rounds_Walk_in_drives1");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity
                    .ToTable("slots")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.SlotName)
                    .HasMaxLength(45)
                    .HasColumnName("Slot_name");
            });

            modelBuilder.Entity<Stream>(entity =>
            {
                entity.HasKey(e => e.Streamid).HasName("PRIMARY");

                entity
                    .ToTable("stream")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.Name).HasMaxLength(45);
            });

            modelBuilder.Entity<TechnologyTable>(entity =>
            {
                entity.HasKey(e => e.TechId).HasName("PRIMARY");

                entity
                    .ToTable("technology_table")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.TechId).HasColumnName("Tech_id");
                entity.Property(e => e.TechName)
                    .HasMaxLength(45)
                    .HasColumnName("Tech_name");
            });

            modelBuilder.Entity<UserReg>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity
                    .ToTable("user_reg")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.Email).HasMaxLength(45);
                entity.Property(e => e.Password).HasMaxLength(45);

                entity.HasMany(d => d.Roles).WithMany(p => p.Ids)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRegHasAllJob",
                        r => r.HasOne<AllJobRolesOfUser>().WithMany()
                            .HasForeignKey("RoleId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk_User_reg_has_All_Job_roles_of_user_All_Job_roles_of_user1"),
                        l => l.HasOne<UserReg>().WithMany()
                            .HasForeignKey("Id")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk_User_reg_has_All_Job_roles_of_user_User_reg1")
                        //j =>
                        //{
                        //    j.HasKey("Id", "RoleId")
                        //        .HasName("PRIMARY")
                        //        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        //    j
                        //        .ToTable("user_reg_has_all_job")
                        //        .HasCharSet("utf8mb3")
                        //        .UseCollation("utf8mb3_general_ci");
                        //    j.HasIndex(new[] { "RoleId" }, "fk_User_reg_has_All_Job_roles_of_user_All_Job_roles_of_user_idx");
                        //    j.HasIndex(new[] { "Id" }, "fk_User_reg_has_All_Job_roles_of_user_User_reg1_idx");
                        //    j.IndexerProperty<int>("RoleId").HasColumnName("Role_id");
                        //}
                        );
            });

            modelBuilder.Entity<WalkInDrife>(entity =>
            {
                entity.HasKey(e => e.DriveId).HasName("PRIMARY");

                entity
                    .ToTable("walk_in_drives")
                    .HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.DriveId).HasColumnName("Drive_id");
                entity.Property(e => e.DriveEndDate)
                    .HasMaxLength(45)
                    .HasColumnName("Drive_end_date");
                entity.Property(e => e.DriveName)
                    .HasMaxLength(45)
                    .HasColumnName("Drive_name");
                entity.Property(e => e.DriveStartDate)
                    .HasMaxLength(45)
                    .HasColumnName("Drive_start_date");
                entity.Property(e => e.DtCreated).HasColumnName("dt_created");
                entity.Property(e => e.DtUpdated).HasColumnName("dt_updated");
                entity.Property(e => e.ExtraInfo)
                    .HasMaxLength(45)
                    .HasColumnName("Extra_info");
                entity.Property(e => e.Location).HasMaxLength(45);

                entity.HasMany(d => d.Roles).WithMany(p => p.Drives)
                    .UsingEntity<Dictionary<string, object>>(
                        "WalkInDrivesHasJobRole",
                        r => r.HasOne<JobRole>().WithMany()
                            .HasForeignKey("RoleId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk_Walk_in_drives_has_Job_roles_Job_roles1"),
                        l => l.HasOne<WalkInDrife>().WithMany()
                            .HasForeignKey("DriveId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk_Walk_in_drives_has_Job_roles_Walk_in_drives1")
                        //j =>
                        //{
                        //    j.HasKey("DriveId", "RoleId")
                        //        .HasName("PRIMARY")
                        //        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        //    j
                        //        .ToTable("walk_in_drives_has_job_roles")
                        //        .HasCharSet("utf8mb3")
                        //        .UseCollation("utf8mb3_general_ci");
                        //    j.HasIndex(new[] { "RoleId" }, "fk_Walk_in_drives_has_Job_roles_Job_roles1_idx");
                        //    j.HasIndex(new[] { "DriveId" }, "fk_Walk_in_drives_has_Job_roles_Walk_in_drives1_idx");
                        //    j.IndexerProperty<int>("DriveId").HasColumnName("Drive_id");
                        //    j.IndexerProperty<int>("RoleId").HasColumnName("Role_id");
                        //}
                        );
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}