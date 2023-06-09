using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lakeshore.Models.DbEntities
{
    [Keyless]
    public partial class VSapOrderPayment
    {
        [Column("store_transaction_no", TypeName = "decimal(18, 0)")]
        public decimal StoreTransactionNo { get; set; }
        [Column("store_no")]
        public int StoreNo { get; set; }
        [Column("register_transaction_no", TypeName = "decimal(18, 0)")]
        public decimal? RegisterTransactionNo { get; set; }
        [Column("register_no")]
        public int RegisterNo { get; set; }
        [Column("entry_datetime", TypeName = "datetime")]
        public DateTime EntryDatetime { get; set; }
        [Column("sequence_no", TypeName = "decimal(18, 0)")]
        public decimal SequenceNo { get; set; }
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
        public string? ReferenceNo { get; set; }
        [Column("amount", TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        [Column("payment_note")]
        [StringLength(256)]
        [Unicode(false)]
        public string PaymentNote { get; set; } = null!;
        [Column("customer_no", TypeName = "decimal(18, 0)")]
        public decimal CustomerNo { get; set; }
        [Column("merch_id")]
        public int MerchId { get; set; }
        [Column("alt_trans_no")]
        [StringLength(30)]
        [Unicode(false)]
        public string AltTransNo { get; set; } = null!;
        [Column("alt_trans_no2")]
        [StringLength(18)]
        [Unicode(false)]
        public string AltTransNo2 { get; set; } = null!;
        [Column("is_void")]
        public int IsVoid { get; set; }
        [Column("payment_action")]
        [StringLength(256)]
        [Unicode(false)]
        public string PaymentAction { get; set; } = null!;
        [Column("is_offline")]
        public int IsOffline { get; set; }
        [Column("voided_trx_not_found")]
        public int VoidedTrxNotFound { get; set; }

        public VSapOrderPayment()
        { }

        public VSapOrderPayment(decimal storeTransactionNo, int storeNo, decimal? registerTransactionNo, int registerNo, DateTime entryDatetime,
            decimal sequenceNo, string paymentType, string? paymentSubtype, string? referenceNo, decimal amount,
            string paymentNote, decimal customerNo, int merchId, string altTransNo, string altTransNo2, int isVoid,
            string paymentAction, int isOffline, int voidedTrxNotFound)
        {
            StoreTransactionNo = storeTransactionNo;
            StoreNo = storeNo;
            RegisterTransactionNo = registerTransactionNo;
            RegisterNo = registerNo;
            EntryDatetime= entryDatetime;
            SequenceNo = sequenceNo;
            PaymentType = paymentType;
            PaymentSubtype = paymentSubtype;
            ReferenceNo = referenceNo;
            Amount = amount;
            PaymentNote = paymentNote;
            CustomerNo = customerNo;
            MerchId = merchId;
            AltTransNo = altTransNo;    
            AltTransNo2 = altTransNo2;
            IsVoid = isVoid;
            PaymentAction = paymentAction;
            IsOffline = isOffline;
            VoidedTrxNotFound = voidedTrxNotFound;
        }
    }
}
