﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Convergys.Assist.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SADataModel : DbContext
    {
        public SADataModel()
            : base("name=SADataModel")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CAEmployee> CAEmployee { get; set; }
        public DbSet<CASite> CASite { get; set; }
        public DbSet<CATeam> CATeam { get; set; }
        public DbSet<CAEmployeeView> CAEmployeeView { get; set; }
        public DbSet<CAPassword> CAPassword { get; set; }
        public DbSet<CACallbackEmpBackup> CACallbackEmpBackup { get; set; }
        public DbSet<CACallbackEvents> CACallbackEvents { get; set; }
        public DbSet<CACallbackLogHistory> CACallbackLogHistory { get; set; }
        public DbSet<CACallbackLogs> CACallbackLogs { get; set; }
        public DbSet<CACallbackReasonType> CACallbackReasonType { get; set; }
        public DbSet<CACallbackLogsSearchView> CACallbackLogsSearchView { get; set; }
        public DbSet<CAListEmpTeams> CAListEmpTeams { get; set; }
        public DbSet<CATimezone> CATimezone { get; set; }
        public DbSet<CAPreferences> CAPreferences { get; set; }
        public DbSet<CAPermission> CAPermission { get; set; }
        public DbSet<CAOfflineActivityType> CAOfflineActivityType { get; set; }
        public DbSet<CAOfflineContactType> CAOfflineContactType { get; set; }
        public DbSet<CAOfflineEvents> CAOfflineEvents { get; set; }
        public DbSet<CAOfflineLogs> CAOfflineLogs { get; set; }
        public DbSet<CAOfflineLogsLookup> CAOfflineLogsLookup { get; set; }
        public DbSet<CATeamConfig> CATeamConfig { get; set; }
    }
}