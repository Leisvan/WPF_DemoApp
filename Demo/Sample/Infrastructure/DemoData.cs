using CommonServiceLocator;
using Demo.Abstractions.Domain.Entities;
using Demo.Abstractions.Infrastructure;
using Demo.Common;
using Demo.Domain.Entities;
using Demo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Sample.Infrastructure
{
    public static class DemoData
    {
        private const string TargetEmailAddress = "leisvan@outlook.com";
        public static void AddDemoData()
        {
            var ds = ServiceLocator.Current.GetInstance<IDataStorage>();
            //AppUsers:
            var adminUser = new AppUser
            {
                Name = "Juan Ferández",
                UserName = "JuanF",
                Password = EncryptionService.Encrypt("Terminator"),
                UserType = AppUserType.Admin,
                AvatarId = 1,
            };
            var specialist = new AppUser
            {
                Name = "Felípe Álvarez",
                UserName = "Felipe",
                Password = EncryptionService.Encrypt("Tomatico"),
                UserType = AppUserType.Specialist,
                AvatarId = 3,
            };

            //Workplaces:
            var wp1 = new Workplace
            {
                Id = 10,
                CompanyName = "Mishima Zaibatsu",
                Occupation = "Estilista",
                Notes = "",
            };
            var wp2 = new Workplace
            {
                Id = 11,
                CompanyName = "Foxhound",
                Occupation = "Telecomunicador",
                Notes = "",
            };
            var wp3 = new Workplace
            {
                Id = 12,
                CompanyName = "Zoras INC",
                Occupation = "Profesor de atletismo",
                Notes = "",
            };
            var wp4 = new Workplace
            {
                Id = 13,
                CompanyName = "Umbrella Corp.",
                Occupation = "Asistente de laboratorio",
                Notes = "",
            };
            var wp6 = new Workplace
            {
                Id = 14,
                CompanyName = "Dhekelia SBA Memorial Hospital",
                Occupation = "Enfermera",
                Notes = "",
                Assigned = true,
            };

            //Requests:
            var req1 = new Request
            {
                FirstName = "Francisco José",
                LastName = "Caballero",
                Age = 32,
                HealthInsurance = "ABCD01234",
                Email = TargetEmailAddress,
                Notes = "Trabajo mucho ¯\\_(ツ)_/¯",
                OccupationalProfile = "Notario",
                State = RequestState.Pending,
                FormNumber = RandomIdGenerator.Generate(),
                RequestDateTime = GetRandomPastTime(),
            };
            var req2 = new Request
            {
                FirstName = "Enzo",
                LastName = "Santamaría",
                Age = 22,
                HealthInsurance = "ABCD01234",
                Email = TargetEmailAddress,
                OccupationalProfile = "Contador",
                State = RequestState.Pending,
                FormNumber = RandomIdGenerator.Generate(),
                RequestDateTime = GetRandomPastTime(),
            };
            var req3 = new Request
            {
                FirstName = "Juán",
                LastName = "Guitiérrez",
                Age = 68,
                HealthInsurance = "ABCD01234",
                Email = TargetEmailAddress,
                OccupationalProfile = "Albañil",
                State = RequestState.Rejected,
                FormNumber = RandomIdGenerator.Generate(),
                RequestDateTime = GetRandomPastTime(),
            };
            var req4 = new Request
            {
                FirstName = "Paulina",
                LastName = "Tejada",
                Age = 36,
                HealthInsurance = "ABCD01234",
                Email = TargetEmailAddress,
                OccupationalProfile = "Enfermera",
                State = RequestState.Accepted,
                FormNumber = RandomIdGenerator.Generate(),
                RequestDateTime = GetRandomPastTime(),
                WorkplaceId = wp6.Id,
            };
            var req6 = new Request
            {
                FirstName = "Samara",
                LastName = "Cañadas",
                Age = 28,
                HealthInsurance = "ABCD01234",
                Email = TargetEmailAddress,
                OccupationalProfile = "Escritora",
                State = RequestState.Pending,
                FormNumber = RandomIdGenerator.Generate(),
                RequestDateTime = DateTime.Now - TimeSpan.FromDays(50),
            };

            ds.AddMany(new[] { adminUser, specialist });
            ds.AddMany(new[] { req1, req2, req3, req4, req6 });
            ds.AddMany(new[] { wp1, wp2, wp3, wp4, wp6 });
        }

        private static Random rnd = new Random();
        private static DateTime GetRandomPastTime()
        {
            int days = rnd.Next(0, 1296000);//20d
            int time = days + rnd.Next(0, 86400);//24h
            return DateTime.Now - TimeSpan.FromSeconds(time);
        }
    }
}
