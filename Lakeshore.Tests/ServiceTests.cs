using Confluent.Kafka;
using Lakeshore.DataInterface.UnitOfWork;
using Lakeshore.Kafka.Client.Interfaces;
using Lakeshore.Models.DbEntities;
using Lakeshore.Models.DBEntities;
using Lakeshore.Service;
using Lakeshore.Tests.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Lakeshore.Tests
{
    [TestFixture]
    public class ServiceTests : BaseTest
    {
        protected Mock<ILogger> LoggerMock;
        private Mock<IProducer<Null, string>> _producerMock;
        private Mock<IKafkaProducerClient> _kafkaProducerClientMock;
        private IUnitOfWork<EpicorDataContex> _unitOfWorkEpicorData;
        private IUnitOfWork<EpicorStagingDataContex> _unitOfWorkEpicorStaging;

        private Service.Service _service;

        [SetUp]
        public void SetUp()
        {
            // Arrange Database related objects
            SeedDatabase();


            LoggerMock = new Mock<ILogger>();

            _producerMock = new Mock<IProducer<Null, string>>();
            _producerMock.Setup(m => m.ProduceAsync(It.IsAny<string>(), It.IsAny<Message<Null, string>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DeliveryResult<Null, string>());

            _kafkaProducerClientMock = new Mock<IKafkaProducerClient>();
            _kafkaProducerClientMock.Setup(p => p.SendMessage(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkEpicorData = new UnitOfWork<EpicorDataContex>(epicorDataContex);

            _unitOfWorkEpicorStaging = new UnitOfWork<EpicorStagingDataContex>(epicorStatingDataContex);

            _service = new Service.Service(
                new Mock<ILogger>().Object,
               _kafkaProducerClientMock.Object,
               _unitOfWorkEpicorData,
               _unitOfWorkEpicorStaging
               );
        }

        [Test]
        public async Task ShouldProcessTheData_ThenPublish()
        {
            var message = new OpenPoCustom()
            {
                StoreTransactionNo = "12",
                StoreNo = "4",
                EntryDateTime = new DateTime(2023, 04, 29).ToString(),
                SequenceNo = "12",
                PaymentType = "paymentType1",
                PaymentSubtype = "NEW",
                CustomerNo = "3",
                ReferenceNo = "referenceNo1",
                Amount = "23",
                PaymentNote = "",
                PaymentAction = "testpaymentaction",
                IsOffline = "1",
                CreatedDatetime = "",
                CustLiabilityStatus = "",
                CustLiabilityProcessedDatetime = "",
                ExpiryDate = ""
            };

        
            var expectedOutput = JsonConvert.SerializeObject(message);
            await _service.ProcessRequest();
           
            _kafkaProducerClientMock.Verify(x => x.SendMessage(expectedOutput), Times.Once());

        }
    }
}
