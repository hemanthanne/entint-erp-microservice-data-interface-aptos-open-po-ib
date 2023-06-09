using Lakeshore.Common;
using Lakeshore.DataInterface.UnitOfWork;
using Lakeshore.Kafka.Client.Interfaces;
using Lakeshore.Models.DbEntities;
using Lakeshore.Models.DBEntities;
using Newtonsoft.Json;
using System.Numerics;

namespace Lakeshore.Service
{
    public class Service
    {
        private readonly Serilog.ILogger _logger;
        private readonly IKafkaProducerClient _kafkaProducerClient;
        private IUnitOfWork<EpicorDataContex> _unitOfWorkEpicorData;
        private IUnitOfWork<EpicorStagingDataContex> _unitOfWorkEpicorStaging;

        public Service(Serilog.ILogger logger
            , IKafkaProducerClient kafkaProducerClient
            , IUnitOfWork<EpicorDataContex> unitOfWorkEpicorData
            , IUnitOfWork<EpicorStagingDataContex> unitOfWorkEpicorStaging)
        {
            _logger = logger;
            _kafkaProducerClient = kafkaProducerClient;
            _unitOfWorkEpicorData = unitOfWorkEpicorData;
            _unitOfWorkEpicorStaging = unitOfWorkEpicorStaging;
        }

        public async Task ProcessRequest()
        {
            try
            {
                _logger.Information("Processing Aptos Open PO records...");
                var openPoRecords = await _unitOfWorkEpicorData.GetRepository<VSapOrderPayment>().GetListAsync();
                if (openPoRecords != null)
                {
                    _logger.Information("Total Aptos Open PO records..." + openPoRecords.Count);
                    var currentDatetime = DateTime.Now;
                    List<OrderPayment> payments = new List<OrderPayment>();
                    List<Opo> opos = new List<Opo>();
                    foreach(var openPo in openPoRecords)
                    {
                        
                        var orderPayment = GenericMapper.MapTo<VSapOrderPayment, OrderPayment>(openPo);
                        var storeTransactionNo = openPo.StoreTransactionNo;
                        var storeNo = openPo.StoreNo;
                        var entryDateTime = openPo.EntryDatetime;
                        var sequenceNo = openPo.SequenceNo;
                        var paymentType = openPo.PaymentType;
                        var paymentSubtype = openPo.PaymentSubtype;
                        var customerNo = openPo.CustomerNo;
                        var referenceNo = openPo.ReferenceNo;
                        var amount = openPo.Amount;
                        var paymentNote = openPo.PaymentNote.ToString();
                        var paymentAction = openPo.PaymentAction;
                        var offlineFlag = openPo.IsOffline;
                        //no need for CreatedDatetime, CustLiabilityStatus, CustLiabilityProcessedDatetime, ExpiryDate
                        //var createdDatetime = openPo.EntryDatetime;
                        _logger.Information("Processing store transaction number: " + storeTransactionNo + ", store number: " + storeNo + ", entry datetime: " + entryDateTime + ", sequence number: " + sequenceNo);
                        //Convert.ToDecimal(openPo.PaymentNote == "" ? "0" : openPo.PaymentNote);
                        if (openPo.PaymentSubtype == "NEW")
                        {
                            //send to APTOS Open PO Staging KAFKA topic 
                            var poData = GenericMapper.MapTo<VSapOrderPayment, OpenPoCustom>(openPo);
                            _logger.Information("Sending message to Kafka Topic...");
                            await _kafkaProducerClient.SendMessage(JsonConvert.SerializeObject(poData));
                            //write record to the opo table
                            var opo = new Opo();
                            opo.CustomerNo = customerNo;
                            opo.Po = referenceNo;
                            opo.EntryDatetime = entryDateTime;
                            opo.Amount = paymentNote == "" ? 0 : Convert.ToDecimal(paymentNote);
                            opo.AddedDatetime = currentDatetime;
                            await _unitOfWorkEpicorStaging.GetRepository<Opo>().InsertAsync(opo);
                        }
                        //else
                        //{

                        //}
                        orderPayment.CustLiabilityStatus = "C";
                        orderPayment.CustLiabilityProcessedDatetime = currentDatetime;
                        payments.Add(orderPayment);
                        _unitOfWorkEpicorData.GetRepository<OrderPayment>().Update(payments);                        
                    }
                    await _unitOfWorkEpicorData.CommitAsync();
                    _logger.Information("Update Order_Payment records successfully.");
                    await _unitOfWorkEpicorStaging.CommitAsync();
                    _logger.Information("Add Opo records successfully.");
                }
                else
                    _logger.Information("No record to process.");
            }
            catch (Exception e)
            {
                _logger.Error("Unable to process Aptos Open PO data...");
                _logger.Error("Message: " + e.Message);
            }
        }
    }
}
