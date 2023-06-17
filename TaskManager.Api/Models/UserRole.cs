﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Api.Models.Abstracted;

namespace TaskManager.Api.Models
{
    [Table("user_role")]
    public class UserRole: Model
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public UserRole()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        {
        }

        [Column("name")]
        public string Name { get; set; }

        [Column("project_id")]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public List<User>? Users { get; set; } = new();
    }
}
