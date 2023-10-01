using System;
using System.Collections.Generic;
using domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hnba;Username=rzanc;Password=1234;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("player_pkey");

            entity.ToTable("player");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Salary)
                .HasPrecision(18, 2)
                .HasColumnName("salary");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasIndex(e => e.Name)
                .IsUnique();
            
            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("player_team_id_fkey");
        });

        modelBuilder.Entity<PlayerStat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("player_stats_pkey");

            entity.ToTable("player_stats");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Assists).HasColumnName("assists");
            entity.Property(e => e.MatchDate).HasColumnName("match_date");
            entity.Property(e => e.OpponentTeamId).HasColumnName("opponent_team_id");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.Points).HasColumnName("points");
            entity.Property(e => e.Rebounds).HasColumnName("rebounds");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.Turnover).HasColumnName("turnover");

            entity.HasOne(d => d.OpponentTeam).WithMany(p => p.PlayerStatOpponentTeams)
                .HasForeignKey(d => d.OpponentTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("player_stats_opponent_team_id_fkey");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerStats)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("player_stats_player_id_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.PlayerStatTeams)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("player_stats_team_id_fkey");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("team_pkey");

            entity.ToTable("team");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            
            
            entity.HasIndex(e => e.Name)
                .IsUnique();
        });

        modelBuilder.Entity<TeamStat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("team_stats_pkey");
            
            entity.ToTable("team_stats");
            
            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.Title).HasColumnName("title");

            
            entity.HasIndex(e => e.TeamId)
                .IsUnique();
            
            entity.HasOne(d => d.Team).WithMany()
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("team_stats_team_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
