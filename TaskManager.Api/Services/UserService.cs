﻿using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Command.Models;
using TaskManager.Api.Services.Abstracted;

namespace TaskManager.Api.Services
{
    public class UserService: CRUDService<UserModel,User>
    {
        public UserService( ApplicationContext context) : base(context) { }
        
        public override List<UserModel> GetAll()
        {
            return _context.Users.AsNoTracking().Select(u => (UserModel)u).ToList();
        }

        public IEnumerable<Project> GetProjectsByUserId(int userId)
        {
            var projectParticipants = _context?
                .Users?
                .AsNoTracking()?
                .Include(u => u.Participants)?
                .ThenInclude(p => p.Project)?
                .FirstOrDefault(u => u.Id == userId)?
                .Participants?
                .Distinct()
                .ToArray();
            return projectParticipants.Select(p => p.Project).ToList();
        }

    }
}
