﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TFC.Domain.Model.Enum;

namespace TFC.Domain.Model.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

        [MaxLength(9)]
        public string? Dni { get; set; }

        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Surname { get; set; }

        [MaxLength(255)]
        public string? Email { get; set; }

        [JsonIgnore]
        public string? FriendCode { get; set; }
        public byte[]? Password { get; set; }
        public Role Role { get; set; } 
        public DateTime InscriptionDate { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Routine> Routines { get; set; } = new List<Routine>();
    }
}