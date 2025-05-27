using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }


    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Noticia> Noticias { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<NoticiaTag> NoticiaTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.Nome).HasMaxLength(250).IsRequired();
            entity.Property(e => e.Senha).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(250).IsRequired();
        });

        modelBuilder.Entity<Noticia>(entity =>
        {
            entity.Property(e => e.Titulo).HasMaxLength(250).IsRequired();
            entity.Property(e => e.Texto).IsRequired();
            entity.HasOne(e => e.Usuario)
                  .WithMany(u => u.Noticias)
                  .HasForeignKey(e => e.UsuarioId);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.Property(e => e.Descricao).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<NoticiaTag>(entity =>
        {
            entity.HasOne(nt => nt.Noticia)
                  .WithMany(n => n.NoticiaTags)
                  .HasForeignKey(nt => nt.NoticiaId);

            entity.HasOne(nt => nt.Tag)
                  .WithMany(t => t.NoticiaTags)
                  .HasForeignKey(nt => nt.TagId);
        });
    }
}
