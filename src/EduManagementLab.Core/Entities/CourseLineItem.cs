﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduManagementLab.Core.Entities
{
    public class CourseLineItem
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public List<Result> Results { get; set; } = new List<Result>();


        public class Result
        {
            public Guid Id { get; set; }
            public User? User { get; set; }
            [Required]
            public Guid UserId { get; set; }
            public CourseLineItem? CourseLineItem { get; set; }
            [Required]
            public Guid CourseLineItemId { get; set; }
            [Column(TypeName = "decimal")]
            public decimal Score { get; set; }
        }
    }
}
