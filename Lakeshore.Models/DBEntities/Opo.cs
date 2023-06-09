using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lakeshore.Models.DbEntities
{
    [Table("opo")]
    [Index("ProcessedDatetime", "RecordId", Name = "IX_opo_processed_datetime")]
    public partial class Opo
    {
        [Key]
        [Column("record_id")]
        public long RecordId { get; set; }
        [Column("customer_no", TypeName = "decimal(18, 0)")]
        public decimal CustomerNo { get; set; }
        [Column("value_contract_id")]
        [StringLength(50)]
        [Unicode(false)]
        public string ValueContractId { get; set; } = "0";
        [Column("po")]
        [StringLength(50)]
        [Unicode(false)]
        public string Po { get; set; } = null!;
        [Column("ordr", TypeName = "decimal(18, 0)")]
        public decimal Ordr { get; set; } = 0;
        [Column("entry_datetime", TypeName = "datetime")]
        public DateTime EntryDatetime { get; set; }
        [Column("inits")]
        [StringLength(3)]
        [Unicode(false)]
        public string Inits { get; set; } = "POS";
        [Column("amount", TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        [Column("added_datetime", TypeName = "datetime")]
        public DateTime AddedDatetime { get; set; }
        [Column("processed_datetime", TypeName = "datetime")]
        public DateTime ProcessedDatetime { get; set; } = new DateTime(1900, 1, 1);

        public Opo()
        {

        }

        public Opo(long recordId, decimal customerNo, string valueContractId, string po,decimal ordr, DateTime entryDatetime, string inits, decimal amount, DateTime addedDatetime, DateTime processedDatetime)
        {
            RecordId = recordId;
            CustomerNo = customerNo;
            ValueContractId = valueContractId;
            Po = po;
            Ordr = ordr;
            EntryDatetime = entryDatetime;
            Inits = inits;
            Amount = amount;
            AddedDatetime = addedDatetime;
            ProcessedDatetime = processedDatetime;
        }

    }
}
