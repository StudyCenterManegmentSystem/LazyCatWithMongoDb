using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Entity.Groups;

namespace Application.Dtos.GroupsDto
{
    public class UpdateGroupDto : BaseDto
    {
        [Required, StringLength(100)]
        public string GroupName { get; set; } = string.Empty;

        public string? RoomId { get; set; }

        public string? FanId { get; set; }

        public string? TeacherId { get; set; }

        public List<int>? Weekdays { get; set; }

        public DateTime Start { get; set; } 

        public DateTime End { get; set; } 

        [Required]
        public decimal Price { get; set; }

        [Required, StringLength(100)]
        public string? Duration { get; set; }

        public static implicit operator Guruh(UpdateGroupDto dto)
        {
            return new Guruh
            {
                Id = dto.Id.ToString(),
                GroupName = dto.GroupName ?? string.Empty,
                RoomId = dto.RoomId,
                FanId = dto.FanId,
                TeacherId = dto.TeacherId,
                Weekdays = dto.Weekdays ?? new List<int>(),
                Start = dto.Start.Date,
                End = dto.End.Date,
                Price = dto.Price,
                Duration = dto.Duration ?? string.Empty
            };
        }

        public static implicit operator UpdateGroupDto(Guruh dto)
        {
            return new UpdateGroupDto
            {
                Id = dto.Id.ToString(),
                GroupName = dto.GroupName ?? string.Empty, 
                RoomId = dto.RoomId,
                FanId = dto.FanId,
                TeacherId = dto.TeacherId,
                Weekdays = dto.Weekdays ?? new List<int>(), 
                Start = dto.Start,
                End = dto.End,
                Price = dto.Price,
                Duration = dto.Duration ?? string.Empty 
            };
        }
    }
}
