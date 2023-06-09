using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lakeshore.Models.DbEntities
{
    [Table("order_payment")]
    //[Index("CustLiabilityStatus", "CustLiabilityProcessedDatetime", "StoreTransactionNo", "StoreNo", "EntryDatetime", "SequenceNo", Name = "IX_order_payment_cust_liability")]
    //[Index("PaymentType", "PaymentSubtype", "StoreTransactionNo", "StoreNo", "EntryDatetime", "SequenceNo", Name = "IX_order_payment_type_x")]
    public partial class OrderPayment
    {
        [Key]
        [Column("store_transaction_no", TypeName = "decimal(18, 0)")]
        public decimal StoreTransactionNo { get; set; }
        [Key]
        [Column("store_no")]
        public int StoreNo { get; set; }
        [Key]
        [Column("entry_datetime", TypeName = "datetime")]
        public DateTime EntryDatetime { get; set; }
        [Key]
        [Column("sequence_no", TypeName = "decimal(18, 0)")]
        public decimal SequenceNo { get; set; }
        /*
        [Column("payment_type")]
        [StringLength(50)]
        [Unicode(false)]
        public string PaymentType { get; set; } = null!;
        [Column("payment_subtype")]
        [StringLength(255)]
        [Unicode(false)]
        public string? PaymentSubtype { get; set; }
        [Column("reference_no")]
        [StringLength(256)]
        [Unicode(false)]
        public string ReferenceNo { get; set; } = null!;
        [Column("amount", TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        [Column("payment_note")]
        [StringLength(256)]
        [Unicode(false)]
        public string? PaymentNote { get; set; }
        [Column("payment_action")]
        [StringLength(256)]
        [Unicode(false)]
        public string? PaymentAction { get; set; }
        [Column("offline_flag")]
        [StringLength(1)]
        [Unicode(false)]
        public string? OfflineFlag { get; set; }
        [Column("created_datetime", TypeName = "datetime")]
        public DateTime CreatedDatetime { get; set; }
        */
        [Column("cust_liability_status")]
        [StringLength(1)]
        [Unicode(false)]
        public string CustLiabilityStatus { get; set; } = null!;
        [Column("cust_liability_processed_datetime", TypeName = "datetime")]
        public DateTime CustLiabilityProcessedDatetime { get; set; }
        //[Column("expiry_date")]
        //[StringLength(4)]
        //[Unicode(false)]
        //public string? ExpiryDate { get; set; }

        public OrderPayment()
        { }

        public OrderPayment(decimal storeTransactionNo, int storeNo, DateTime entryDatetime, decimal sequenceNo, string custLiabilityStatus, DateTime custLiabilityProcessedDatetime)
        { 
            StoreTransactionNo = storeTransactionNo;
            StoreNo = storeNo;
            EntryDatetime = entryDatetime;
            SequenceNo = sequenceNo;
            CustLiabilityStatus = custLiabilityStatus;
            CustLiabilityProcessedDatetime = custLiabilityProcessedDatetime;
        }
    }
}
