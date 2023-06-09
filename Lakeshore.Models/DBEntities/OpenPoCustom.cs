using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lakeshore.Models.DBEntities
{
    public class OpenPoCustom
    {
        [JsonProperty("StoreTransactionNo")]
        public string StoreTransactionNo { get; set; }
        [JsonProperty("StoreNo")]
        public string StoreNo { get; set; }

        [JsonProperty("EntryDateTime")]
        public string EntryDateTime { get; set; }
        [JsonProperty("SequenceNo")]
        public string SequenceNo { get; set; }

        [JsonProperty("PaymentType")]
        public string PaymentType { get; set; }

        [JsonProperty("PaymentSubtype")]
        public string PaymentSubtype { get; set; } 
        [JsonProperty("BtCnum")]
        public string CustomerNo { get; set; }

        [JsonProperty("ReferenceNo")]
        public string ReferenceNo { get; set; }
        [JsonProperty("Amount")]
        public string Amount { get; set; }

        [JsonProperty("PaymentNote")]
        public string PaymentNote { get; set; }

        [JsonProperty("PaymentAction")]
        public string PaymentAction { get; set; }
        [JsonProperty("OfflineFlag")]
        public string IsOffline { get; set; }

        [JsonProperty("CreatedDatetime")]
        public string CreatedDatetime { get; set; } = "";
        [JsonProperty("CustLiabilityStatus")]
        public string CustLiabilityStatus { get; set; } = "";

        [JsonProperty("CustLiabilityProcessedDatetime")]
        public string CustLiabilityProcessedDatetime { get; set; } = "";

        [JsonProperty("ExpiryDate")]
        public string ExpiryDate { get; set; } = "";

    }
}