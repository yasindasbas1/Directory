using System;
using System.Collections.Generic;
using Directory.Core;
using Directory.Data.Entities;

namespace Directory.XunitTest.Data
{
    public class TestData
    {
        public static List<Directory.Data.Entities.Contact> ContactData()
        {
            return new List<Directory.Data.Entities.Contact>()
            {
                new Directory.Data.Entities.Contact()
                {
                    Id = 100,
                    Name = "Yasin",
                    Surname = "Daşbaş",
                    Company = "Turkuvaz",
                    Deleted=false
                },
                new Directory.Data.Entities.Contact()
                {
                    Id = 200,
                    Name = "Furkan",
                    Surname = "Daşbaş",
                    Company = "Erser",
                    Deleted=false
                },

            };
        }

        public static List<ContactInformation> ContactInformationData()
        {
            return new List<ContactInformation>()
            {
                new ContactInformation()
                {
                    Id = 10,
                    ContactId = 100,
                    Location = "Kayseri",
                    Mail = "yasin.dasbas1@gmail.com",
                    Telephone = "05367132575",
                    Deleted=false
                },
                new ContactInformation()
                {
                    Id = 20,
                    ContactId = 100,
                    Location = "Sivas",
                    Mail = "yasin.dasbas1@gmail.com",
                    Telephone = "05456622",
                    Deleted=false
                },
                new ContactInformation()
                {
                    Id = 30,
                    ContactId = 100,
                    Location = "Ankara",
                    Mail = "yasin.dasbas1@gmail.com",
                    Telephone = "55852665856",
                    Deleted=false
                },

                new ContactInformation()
                {
                    Id = 40,
                    ContactId = 200,
                    Location = "Kayseri",
                    Mail = "ysndasbas@gmail.com",
                    Telephone = "05367132575",
                    Deleted=false
                },
                
                new ContactInformation()
                {
                    Id = 50,
                    ContactId = 200,
                    Location = "Sivas",
                    Mail = "yasin.dasbas1@gmail.com",
                    Telephone = "45435436543654",
                    Deleted=false
                },

            };
        }

        public static List<Directory.Data.Entities.Report> ReportData()
        {
            return new List<Directory.Data.Entities.Report>()
            {
                new Directory.Data.Entities.Report()
                {
                    Id = 1000,
                    RequestTime = DateTime.Today,
                    CompletedTime = DateTime.Today,
                    Status = ReportStatuses.Waiting,
                    Url = "d"
                },
                new Directory.Data.Entities.Report()
                {
                    Id = 2000,
                    RequestTime = DateTime.Today,
                    CompletedTime = DateTime.Today,
                    Status = ReportStatuses.Preparing,
                    Url = "d"
                },
                new Directory.Data.Entities.Report()
                {
                    Id = 3000,
                    RequestTime = DateTime.Today.AddDays(-1),
                    CompletedTime = DateTime.Today,
                    Status = ReportStatuses.Complated,
                    Url = "d"
                }

            };
        }

        public static List<ReportDetail> ReportDetailData()
        {
            return new List<ReportDetail>()
            {
                new ReportDetail()
                {
                    Id = 1,
                    ReportId = 3000,
                    Location = "Kayseri",
                },
                new ReportDetail()
                {
                    Id = 2,
                    ReportId = 3000,
                    Location = "Sivas",
                },
                new ReportDetail()
                {
                    Id = 3,
                    ReportId = 3000,
                    Location = "Ankara",
                },

            };
        }

    }
}
