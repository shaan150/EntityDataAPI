using Bogus;
using EntityDataAPI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityDataAPI.Data
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Entities.Any())
            {
                return; // DB has been seeded
            }

            var entities = GenerateEntities(context, 10);
            context.Entities.AddRange(entities);
            context.SaveChanges();
        }

        private static List<Entity> GenerateEntities(AppDbContext context, int numberOfEntities)
        {
            var entities = new List<Entity>();

            var entityFaker = new Faker<Entity>()
                .RuleFor(e => e.EntityID, f => Guid.NewGuid().ToString())
                .RuleFor(e => e.Deceased, f => f.Random.Bool())
                .RuleFor(e => e.Gender, f => f.PickRandom<string>(new string[] { "Male", "Female", "Non-binary", null }));

            for (int i = 0; i < numberOfEntities; i++)
            {
                var entity = entityFaker.Generate();
                entity.Dates = GenerateDates(context, entity, 3);
                entity.Names = GenerateNames(context, entity, 3);
                entity.Addresses = GenerateAddresses(context, entity, 3);
                entities.Add(entity);
            }

            return entities;
        }

        private static List<Date> GenerateDates(AppDbContext context, Entity entity, int numberOfDates)
        {
            var dateFaker = new Faker<Date>()
                .RuleFor(d => d.DateID, f => Guid.NewGuid().ToString())
                .RuleFor(d => d.DateType, f => f.PickRandom(new string[] { "Birth", "Death", "Anniversary", null }))
                .RuleFor(d => d.DateTime, f => f.Date.Past(50))
                .RuleFor(d => d.EntityId, f => entity.EntityID)
                .RuleFor(d => d.Entity, f => entity);

            var dates = dateFaker.Generate(numberOfDates);
            context.Dates.AddRange(dates);
            return dates;
        }

        private static List<Name> GenerateNames(AppDbContext context, Entity entity, int numberOfNames)
        {
            var nameFaker = new Faker<Name>()
                .RuleFor(n => n.NameID, f => Guid.NewGuid().ToString())
                .RuleFor(n => n.FirstName, f => f.Name.FirstName())
                .RuleFor(n => n.MiddleName, f => f.Name.FirstName())
                .RuleFor(n => n.Surname, f => f.Name.LastName())
                .RuleFor(n => n.EntityID, f => entity.EntityID)
                .RuleFor(n => n.Entity, f => entity);

            var names = nameFaker.Generate(numberOfNames);
            context.Names.AddRange(names);
            return names;
        }

        private static List<Address> GenerateAddresses(AppDbContext context, Entity entity, int numberOfAddresses)
        {
            var addressFaker = new Faker<Address>()
                .RuleFor(a => a.AddressID, f => Guid.NewGuid().ToString())
                .RuleFor(a => a.AddressLine, f => f.Address.StreetAddress())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.Country, f => f.Address.Country())
                .RuleFor(a => a.EntityID, f => entity.EntityID)
                .RuleFor(a => a.Entity, f => entity);

            List<Address> addresses = addressFaker.Generate(numberOfAddresses);
            context.Addresses.AddRange(addresses);
            return addresses;
        }
    }
}
