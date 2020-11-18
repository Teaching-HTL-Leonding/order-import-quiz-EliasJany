using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;

Console.WriteLine("Hello World");

class Customer
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [Column(TypeName = "decimal(8,2)")]
    public decimal CreditLimit { get; set; }
}

class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    [Column(TypeName = "decimal(8,2)")]
    public decimal OrderValue { get; set; }
}

class QuizContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public QuizContext(DbContextOptions<QuizContext> options) : base(options) { }
}

class QuizContextFactory : IDesignTimeDbContextFactory<QuizContext>
{
    public QuizContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<QuizContext>();

        optionsBuilder
            //.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        return new QuizContext(optionsBuilder.Options);
    }
}