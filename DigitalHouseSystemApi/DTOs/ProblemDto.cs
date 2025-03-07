﻿using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.DTOs
{
    public class ProblemDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Context { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? PhotoUrl { get; set; }

        public ProblemDto() { }

        public ProblemDto(int id,string email, string name, string context, DateTime? createdAt, string? photoUrl)
        {
            Id = id;
            Email = email;
            Name = name;
            Context = context;
            PhotoUrl = photoUrl;
            CreatedAt = createdAt;
        }
    }
}
