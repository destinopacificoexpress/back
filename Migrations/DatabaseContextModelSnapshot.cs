﻿// <auto-generated />
using System;
using DestinopacificoExpres.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DestinopacificoExpres.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Agencias", b =>
                {
                    b.Property<int>("AgenciaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AgenciaId"));

                    b.Property<string>("Cortecias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CupoAcumulado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CupoAsignado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DireccionAgencia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAgencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImpuestoCartera")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreAgencia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelefonoAgencia")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AgenciaId");

                    b.ToTable("Agencias");
                });

            modelBuilder.Entity("FormaPago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Metodo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FormasPago");
                });

            modelBuilder.Entity("Grupos", b =>
                {
                    b.Property<int>("GrupoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GrupoId"));

                    b.Property<string>("NombreGrupo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GrupoId");

                    b.ToTable("Grupos");
                });

            modelBuilder.Entity("InfoDestino", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GrupoId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GrupoId");

                    b.ToTable("InfoDestino");
                });

            modelBuilder.Entity("Pasajero", b =>
                {
                    b.Property<int>("PasajeroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PasajeroId"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoDocumentoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PasajeroId");

                    b.ToTable("Pasajeros");
                });

            modelBuilder.Entity("TipoTiquete", b =>
                {
                    b.Property<int>("TipoTiqueteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipoTiqueteId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Incluye_Impuestos")
                        .HasColumnType("bit");

                    b.Property<string>("NombreTipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipoTiqueteId");

                    b.ToTable("TipoTiquete");
                });

            modelBuilder.Entity("Tiquete", b =>
                {
                    b.Property<int>("TiqueteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TiqueteId"));

                    b.Property<int>("AgenciaId")
                        .HasColumnType("int");

                    b.Property<int>("CantidadPasajeros")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DestinoId")
                        .HasColumnType("int");

                    b.Property<bool>("ExcesoEquipaje")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaAbordo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaExpedicion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaRetorno")
                        .HasColumnType("datetime2");

                    b.Property<int>("FormaPagoId")
                        .HasColumnType("int");

                    b.Property<int>("GrupoId")
                        .HasColumnType("int");

                    b.Property<string>("NumeroTiquete")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PasajeroId")
                        .HasColumnType("int");

                    b.Property<bool>("SoloIda")
                        .HasColumnType("bit");

                    b.Property<int>("TipoTiqueteId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalVenta")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorIndividual")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorSugerido")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("TiqueteId");

                    b.HasIndex("PasajeroId");

                    b.ToTable("Tiquetes");
                });

            modelBuilder.Entity("Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Second_Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("InfoDestino", b =>
                {
                    b.HasOne("Grupos", "Grupos")
                        .WithMany("InfoDestino")
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grupos");
                });

            modelBuilder.Entity("Tiquete", b =>
                {
                    b.HasOne("Pasajero", null)
                        .WithMany("Tiquetes")
                        .HasForeignKey("PasajeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Grupos", b =>
                {
                    b.Navigation("InfoDestino");
                });

            modelBuilder.Entity("Pasajero", b =>
                {
                    b.Navigation("Tiquetes");
                });
#pragma warning restore 612, 618
        }
    }
}
