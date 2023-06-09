using Lakeshore.DataInterface.UnitOfWork;
using Lakeshore.Models.DbEntities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lakeshore.Tests.Repository
{
    public abstract class BaseTest
    {
        protected readonly DbContextOptions<EpicorDataContex> DbContextOptions;
        protected readonly DbContextOptions<EpicorStagingDataContex> DbContextStagingOptions;
        protected EpicorDataContex epicorDataContex;
        protected EpicorStagingDataContex epicorStatingDataContex;
        protected Mock<ILogger> LoggerMock;

        protected BaseTest()
        {
            DbContextOptions = new DbContextOptionsBuilder<EpicorDataContex>()
                .UseInMemoryDatabase(databaseName: "EpicorData")
                .Options;

            epicorDataContex = new EpicorDataContex(DbContextOptions);
            DbContextStagingOptions = new DbContextOptionsBuilder<EpicorStagingDataContex>()
               .UseInMemoryDatabase(databaseName: "EpicorStagingData")
               .Options;

            epicorStatingDataContex = new EpicorStagingDataContex(DbContextStagingOptions);

            LoggerMock = new Mock<ILogger>();
        }
        protected void SeedDatabase()
        {
            epicorDataContex.Database.EnsureCreated();
            epicorStatingDataContex.Database.EnsureCreated();

            SeedEpicorData();
            SeedEpicorStagingData();

            epicorDataContex.SaveChanges();
            epicorStatingDataContex.SaveChanges();

            foreach (var entity in epicorDataContex.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
        }

        private void SeedEpicorData()
        {
            var orderPayments = new List<OrderPayment>
            {
                new OrderPayment(12,4,new DateTime(2023, 04, 29),12,"testcustliabilitystatus1",new DateTime()),
            };

            var vSapOrderPayments = new List<VSapOrderPayment>
            {
                new VSapOrderPayment(12,4,5,3,new DateTime(2023, 04, 29),12,"paymentType1","NEW","referenceNo1",23,"",3,1,"altransNotest","altTransNoTest2",1,"testpaymentaction",1,1),
            };

            epicorDataContex.OrderPayments.AddRange(orderPayments);
            epicorDataContex.VSapOrderPayments.AddRange(vSapOrderPayments);
        }

        private void SeedEpicorStagingData()
        {
            var ops = new List<Opo>
            {
                new Opo(234,34,"testvaluecontractId","testpo",34,new DateTime(),"testinits",23,new DateTime(),new DateTime()),
                new Opo(236,28,"testvaluecontractId1","testpo1",44,new DateTime(),"testinits1",25,new DateTime(),new DateTime()),

            };

            epicorStatingDataContex.Opos.AddRange(ops);
        }

    }
}
