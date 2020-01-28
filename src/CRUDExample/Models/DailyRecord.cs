using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDExample.Models
{
    /// <summary>
    /// 每日記錄
    /// </summary>
    public class DailyRecord
    {
        /// <summary>
        /// 流水序號
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Today;
        /// <summary>
        /// 狀態旗標
        /// </summary>
        [NotMapped]
        public StatusFlags Status { get; set; }
        /// <summary>
        /// 旗標文字
        /// </summary>
        [MaxLength(8)]
        [Column("Status")]
        [Required]
        public string StatusText
        {
            get => Status.ToString();
            set
            {
                StatusFlags tryParseValue;
                if (!Enum.TryParse<StatusFlags>(value, out tryParseValue))
                    throw new ArgumentException($"Invalid StatusText: {value}");
                Status = tryParseValue;
            }
        }
        /// <summary>
        /// 異常或警示事件摘要
        /// </summary>
        [Required]
        public string EventSummary { get; set; }
        /// <summary>
        /// 備註說明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 彙整人員姓名
        /// </summary>
        [MaxLength(8)]
        [Required]
        public string User { get; set; }

    }

    /// <summary>
    /// 狀態旗標
    /// </summary>
    public enum StatusFlags
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal,
        /// <summary>
        /// 警示
        /// </summary>
        Warn,
        /// <summary>
        /// 異常
        /// </summary>
        Error
    }
}
