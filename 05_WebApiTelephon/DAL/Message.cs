using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public int PhoneId { get; set; }
        [ForeignKey("PhoneId")]
        [JsonIgnore]
        public Phone Phone { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}