using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace XPM_Server.ORM;

public partial class XpmContext : DbContext
{
    public XpmContext()
    {
    }

    public XpmContext(DbContextOptions<XpmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssignedDesigner> AssignedDesigners { get; set; }

    public virtual DbSet<AssignedInteriorDesigner> AssignedInteriorDesigners { get; set; }

    public virtual DbSet<AuthRefreshToken> AuthRefreshTokens { get; set; }

    public virtual DbSet<AuthRole> AuthRoles { get; set; }

    public virtual DbSet<AuthUser> AuthUsers { get; set; }

    public virtual DbSet<AuthUserEmailWhitelist> AuthUserEmailWhitelists { get; set; }

    public virtual DbSet<AuthUserRole> AuthUserRoles { get; set; }

    public virtual DbSet<AvailableService> AvailableServices { get; set; }

    public virtual DbSet<ChangeOrder> ChangeOrders { get; set; }

    public virtual DbSet<ChangeOrderStatus> ChangeOrderStatuses { get; set; }

    public virtual DbSet<ChangeOrderType> ChangeOrderTypes { get; set; }

    public virtual DbSet<ClientOwner> ClientOwners { get; set; }

    public virtual DbSet<ConstructionCompany> ConstructionCompanies { get; set; }

    public virtual DbSet<Consultant> Consultants { get; set; }

    public virtual DbSet<ConsultantDiscipline> ConsultantDisciplines { get; set; }

    public virtual DbSet<ConsultantEmployed> ConsultantEmployeds { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<ContractType> ContractTypes { get; set; }

    public virtual DbSet<Discipline> Disciplines { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectArchitect> ProjectArchitects { get; set; }

    public virtual DbSet<ProjectDeliveryMethod> ProjectDeliveryMethods { get; set; }

    public virtual DbSet<ProjectDesigner> ProjectDesigners { get; set; }

    public virtual DbSet<ProjectInteriorDesigner> ProjectInteriorDesigners { get; set; }

    public virtual DbSet<ProjectManager> ProjectManagers { get; set; }

    public virtual DbSet<ProjectPrincipal> ProjectPrincipals { get; set; }

    public virtual DbSet<ProjectScope> ProjectScopes { get; set; }

    public virtual DbSet<ProjectSector> ProjectSectors { get; set; }

    public virtual DbSet<ProjectSubType> ProjectSubTypes { get; set; }

    public virtual DbSet<ProjectType> ProjectTypes { get; set; }

    public virtual DbSet<ServiceProvided> ServiceProvideds { get; set; }

    public virtual DbSet<State> States { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=xpm;Username=xpm_app;Password=qu3rtY5019:");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssignedDesigner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AssignedProjectDesigner_pkey");

            entity.ToTable("AssignedDesigner", "XPM");

            entity.HasIndex(e => e.Project, "fki_Project");

            entity.HasIndex(e => e.ProjectDesigner, "fki_ProjectDesigner");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.ProjectNavigation).WithMany(p => p.AssignedDesigners)
                .HasForeignKey(d => d.Project)
                .HasConstraintName("Project");

            entity.HasOne(d => d.ProjectDesignerNavigation).WithMany(p => p.AssignedDesigners)
                .HasForeignKey(d => d.ProjectDesigner)
                .HasConstraintName("ProjectDesigner");
        });

        modelBuilder.Entity<AssignedInteriorDesigner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AssignedProjectInteriorDesigner_pkey");

            entity.ToTable("AssignedInteriorDesigner", "XPM");

            entity.HasIndex(e => e.InteriorDesigner, "fki_O");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.InteriorDesignerNavigation).WithMany(p => p.AssignedInteriorDesigners)
                .HasForeignKey(d => d.InteriorDesigner)
                .HasConstraintName("ProjectInteriorDesigner");

            entity.HasOne(d => d.ProjectNavigation).WithMany(p => p.AssignedInteriorDesigners)
                .HasForeignKey(d => d.Project)
                .HasConstraintName("Project");
        });

        modelBuilder.Entity<AuthRefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Auth.RefreshToken_pkey");

            entity.ToTable("Auth.RefreshToken", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.TokenHash).HasMaxLength(1000);
            entity.Property(e => e.TokenSalt).HasMaxLength(50);

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.AuthRefreshTokens)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User");
        });

        modelBuilder.Entity<AuthRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserRole_pkey");

            entity.ToTable("Auth.Role", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Description).HasMaxLength(1024);
            entity.Property(e => e.Named).HasMaxLength(64);
        });

        modelBuilder.Entity<AuthUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("Auth.User", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.EmailAddress).HasMaxLength(128);
            entity.Property(e => e.FirstName)
                .HasMaxLength(64)
                .HasColumnName("First Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(64)
                .HasColumnName("Last Name");
            entity.Property(e => e.Password).HasMaxLength(256);
            entity.Property(e => e.PasswordSalt).HasMaxLength(256);
        });

        modelBuilder.Entity<AuthUserEmailWhitelist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Auth.UserEmailWhitelist_pkey");

            entity.ToTable("Auth.UserEmailWhitelist", "XPM");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EmailAddress).HasMaxLength(128);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AuthUserEmailWhitelists)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CreatedBy");
        });

        modelBuilder.Entity<AuthUserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserRole_pkey1");

            entity.ToTable("Auth.UserRole", "XPM");

            entity.HasIndex(e => e.AssignedBy, "fki_AssignedBy");

            entity.HasIndex(e => e.Role, "fki_Role");

            entity.HasIndex(e => e.User, "fki_User");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.AssignedByNavigation).WithMany(p => p.AuthUserRoleAssignedByNavigations)
                .HasForeignKey(d => d.AssignedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AssignedBy");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.AuthUserRoles)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Role");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.AuthUserRoleUserNavigations)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User");
        });

        modelBuilder.Entity<AvailableService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ServicesAvailable_pkey");

            entity.ToTable("AvailableService", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<ChangeOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ChangeOrder_pkey");

            entity.ToTable("ChangeOrder", "XPM");

            entity.HasIndex(e => e.Status, "fki_Status");

            entity.HasIndex(e => e.Type, "fki_Type");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.Number).HasMaxLength(256);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ChangeOrderCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("CreatedBy");

            entity.HasOne(d => d.LastUpdatedByNavigation).WithMany(p => p.ChangeOrderLastUpdatedByNavigations)
                .HasForeignKey(d => d.LastUpdatedBy)
                .HasConstraintName("LastUpdatedBy");

            entity.HasOne(d => d.ProjectNavigation).WithMany(p => p.ChangeOrders)
                .HasForeignKey(d => d.Project)
                .HasConstraintName("Project");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.ChangeOrders)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("Status");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.ChangeOrders)
                .HasForeignKey(d => d.Type)
                .HasConstraintName("Type");
        });

        modelBuilder.Entity<ChangeOrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ChangeOrderStatus_pkey");

            entity.ToTable("ChangeOrderStatus", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<ChangeOrderType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ChangeOrderType_pkey");

            entity.ToTable("ChangeOrderType", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<ClientOwner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ClientOwner_pkey");

            entity.ToTable("ClientOwner", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<ConstructionCompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConstructionCompany_pkey");

            entity.ToTable("ConstructionCompany", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<Consultant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Consultant_pkey");

            entity.ToTable("Consultant", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<ConsultantDiscipline>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConsultantDiscipline_pkey");

            entity.ToTable("ConsultantDiscipline", "XPM");

            entity.HasIndex(e => e.Consultant, "fki_Consultant");

            entity.HasIndex(e => e.Discipline, "fki_Discipline");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.ConsultantNavigation).WithMany(p => p.ConsultantDisciplines)
                .HasForeignKey(d => d.Consultant)
                .HasConstraintName("Consultant");

            entity.HasOne(d => d.DisciplineNavigation).WithMany(p => p.ConsultantDisciplines)
                .HasForeignKey(d => d.Discipline)
                .HasConstraintName("Discipline");
        });

        modelBuilder.Entity<ConsultantEmployed>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConsultantEmployed_pkey");

            entity.ToTable("ConsultantEmployed", "XPM");

            entity.HasIndex(e => e.ConsultantDiscipline, "fki_ConsultantDiscipline");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.ContactEmail).HasMaxLength(40);
            entity.Property(e => e.ContactName).HasMaxLength(40);
            entity.Property(e => e.ContactPhone).HasMaxLength(40);

            entity.HasOne(d => d.ConsultantDisciplineNavigation).WithMany(p => p.ConsultantEmployeds)
                .HasForeignKey(d => d.ConsultantDiscipline)
                .HasConstraintName("ConsultantDiscipline");

            entity.HasOne(d => d.ProjectNavigation).WithMany(p => p.ConsultantEmployeds)
                .HasForeignKey(d => d.Project)
                .HasConstraintName("Project");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Contact_pkey");

            entity.ToTable("Contact", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.EmailAddress).HasMaxLength(256);
            entity.Property(e => e.FaxNumber).HasMaxLength(32);
            entity.Property(e => e.Named).HasMaxLength(256);
            entity.Property(e => e.PhoneNumber).HasMaxLength(32);
            entity.Property(e => e.Title).HasMaxLength(256);
        });

        modelBuilder.Entity<ContractType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("XPM_ContractType_pkey");

            entity.ToTable("ContractType", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<Discipline>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Discipline_pkey");

            entity.ToTable("Discipline", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Project_pkey");

            entity.ToTable("Project", "XPM", tb => tb.HasComment("Main project table."));

            entity.HasIndex(e => e.ClientOwner, "fki_ClientOwner");

            entity.HasIndex(e => e.ConstructionCompany, "fki_ConstructionCompany");

            entity.HasIndex(e => e.Contact, "fki_Contact");

            entity.HasIndex(e => e.ContractType, "fki_ContractType");

            entity.HasIndex(e => e.CreatedBy, "fki_CreatedBy");

            entity.HasIndex(e => e.LastUpdatedBy, "fki_LastUpdatedBy");

            entity.HasIndex(e => e.ProjectArchitect, "fki_ProjectArchitect");

            entity.HasIndex(e => e.ProjectDeliveryMethod, "fki_ProjectDeliveryMethod");

            entity.HasIndex(e => e.ProjectManager, "fki_ProjectManager");

            entity.HasIndex(e => e.ProjectPrincipal, "fki_ProjectPrincipal");

            entity.HasIndex(e => e.ProjectScope, "fki_ProjectScope");

            entity.HasIndex(e => e.ProjectSector, "fki_ProjectSector");

            entity.HasIndex(e => e.ProjectSubType, "fki_ProjectSubtype");

            entity.HasIndex(e => e.ProjectType, "fki_ProjectType");

            entity.HasIndex(e => e.State, "fki_State");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.AddressLine1).HasColumnType("character varying");
            entity.Property(e => e.AddressLine2).HasColumnType("character varying");
            entity.Property(e => e.City).HasColumnType("character varying");
            entity.Property(e => e.FinalCost).HasColumnType("money");
            entity.Property(e => e.FinalFeesPaid).HasColumnType("money");
            entity.Property(e => e.InitialCost).HasColumnType("money");
            entity.Property(e => e.Named).HasMaxLength(256);
            entity.Property(e => e.Notes).HasColumnType("character varying");
            entity.Property(e => e.Number).HasMaxLength(40);
            entity.Property(e => e.ServiceFee).HasColumnType("money");
            entity.Property(e => e.Zip).HasColumnType("character varying");

            entity.HasOne(d => d.ClientOwnerNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ClientOwner)
                .HasConstraintName("ClientOwner");

            entity.HasOne(d => d.ConstructionCompanyNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ConstructionCompany)
                .HasConstraintName("ConstructionCompany");

            entity.HasOne(d => d.ContactNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.Contact)
                .HasConstraintName("Contact");

            entity.HasOne(d => d.ContractTypeNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ContractType)
                .HasConstraintName("ContractType");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProjectCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("CreatedBy");

            entity.HasOne(d => d.LastUpdatedByNavigation).WithMany(p => p.ProjectLastUpdatedByNavigations)
                .HasForeignKey(d => d.LastUpdatedBy)
                .HasConstraintName("LastUpdatedBy");

            entity.HasOne(d => d.ProjectArchitectNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectArchitect)
                .HasConstraintName("ProjectArchitect");

            entity.HasOne(d => d.ProjectDeliveryMethodNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectDeliveryMethod)
                .HasConstraintName("ProjectDeliveryMethod");

            entity.HasOne(d => d.ProjectManagerNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectManager)
                .HasConstraintName("ProjectManager");

            entity.HasOne(d => d.ProjectPrincipalNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectPrincipal)
                .HasConstraintName("ProjectPrincipal");

            entity.HasOne(d => d.ProjectScopeNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectScope)
                .HasConstraintName("ProjectScope");

            entity.HasOne(d => d.ProjectSectorNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectSector)
                .HasConstraintName("ProjectSector");

            entity.HasOne(d => d.ProjectSubTypeNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectSubType)
                .HasConstraintName("ProjectSubtype");

            entity.HasOne(d => d.ProjectTypeNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectType)
                .HasConstraintName("ProjectType");

            entity.HasOne(d => d.StateNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.State)
                .HasConstraintName("State");
        });

        modelBuilder.Entity<ProjectArchitect>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProjectArchitect_pkey");

            entity.ToTable("ProjectArchitect", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<ProjectDeliveryMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProjectDeliveryMethod_pkey");

            entity.ToTable("ProjectDeliveryMethod", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<ProjectDesigner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProjectDesigner_pkey");

            entity.ToTable("ProjectDesigner", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<ProjectInteriorDesigner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProjectInteriorDesigner_pkey");

            entity.ToTable("ProjectInteriorDesigner", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<ProjectManager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProjectManager_pkey");

            entity.ToTable("ProjectManager", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<ProjectPrincipal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProjectPrincipal_pkey");

            entity.ToTable("ProjectPrincipal", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<ProjectScope>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("XPM_ProjectScope_pkey");

            entity.ToTable("ProjectScope", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<ProjectSector>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("XPM_ProjectSector_pkey");

            entity.ToTable("ProjectSector", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);
        });

        modelBuilder.Entity<ProjectSubType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("XPM_ProjectSubType_pkey");

            entity.ToTable("ProjectSubType", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);

            entity.HasOne(d => d.ProjectTypeNavigation).WithMany(p => p.ProjectSubTypes)
                .HasForeignKey(d => d.ProjectType)
                .HasConstraintName("ProjectType");
        });

        modelBuilder.Entity<ProjectType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("XPM_ProjectType_pkey");

            entity.ToTable("ProjectType", "XPM");

            entity.HasIndex(e => e.ProjectSector, "fki_{");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Named).HasMaxLength(256);

            entity.HasOne(d => d.ProjectSectorNavigation).WithMany(p => p.ProjectTypes)
                .HasForeignKey(d => d.ProjectSector)
                .HasConstraintName("ProjectSector");
        });

        modelBuilder.Entity<ServiceProvided>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ServiceProvided_pkey1");

            entity.ToTable("ServiceProvided", "XPM");

            entity.HasIndex(e => e.AvailableService, "fki_AvailableService");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.AvailableServiceNavigation).WithMany(p => p.ServiceProvideds)
                .HasForeignKey(d => d.AvailableService)
                .HasConstraintName("AvailableService");

            entity.HasOne(d => d.ProjectNavigation).WithMany(p => p.ServiceProvideds)
                .HasForeignKey(d => d.Project)
                .HasConstraintName("Project");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("State_pkey");

            entity.ToTable("State", "XPM");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Abbreviated).HasMaxLength(2);
            entity.Property(e => e.Named)
                .HasMaxLength(32)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
