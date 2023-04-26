using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Api.Models.Abstractions;
using TaskManager.Api.Models.Data;
using TaskManager.Command.Models;

namespace TaskManager.Api.Models.Services
{
    public class UsersService :AbstractionService,ICommandService<UserModel>
    {
        private readonly ApplicationContext _db;

        public UsersService(ApplicationContext db)
        {
            _db = db;
        }
        public (string,string) GetUserLoginPassFromBasicAuth(HttpRequest httpRequest)
        {
            string authHeader = httpRequest.Headers.Authorization;
            if (authHeader is null)
                return (null,null);
            if (authHeader.StartsWith("Basic"))
            {
                authHeader = authHeader.Replace("Basic","");
            }
            var encoding = Encoding.GetEncoding("iso-8859-1");
                string[] encodingPassArray = encoding.GetString(Convert.FromBase64String(authHeader)).Split(':');
                string userName = encodingPassArray[0];
                string userPass = encodingPassArray[1];
                return (userName, userPass);
        }
        public User GetUser(string  login,string pass) =>
            _db.Users.FirstOrDefault(u=>u.Email ==login && u.Password == pass);
        public User GetUser(string login) =>
            _db.Users.FirstOrDefault(u => u.Email == login);

        public ClaimsIdentity GetClaimsIdentity(string name,string password)
        {
            User currentUser = GetUser(name, password);
            if (currentUser is null)
                return null;
            currentUser.LastLoginData = DateTime.Now;
            _db.Users.Update(currentUser);
            _db.SaveChanges();

            var claims = new List<Claim> 
            {
                new(ClaimsIdentity.DefaultNameClaimType,currentUser.Email),
                new(ClaimsIdentity.DefaultRoleClaimType, currentUser.Status.ToString()),
            };
            ClaimsIdentity claimsIdentity = new(claims,"Token",ClaimsIdentity.DefaultNameClaimType,ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;  
        }

        public bool Create(UserModel model)
        {
            return DoAction(() =>
            {
                User newUser = new(model);
                _db.Users.Add(newUser);
                _db.SaveChanges();
            });
        }

        public bool Update(int index, UserModel model)
        {
            User user = _db.Users.FirstOrDefault(x => x.Id == index);
            if (user is null)
                return false;
           return DoAction(() =>
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Password = model.Password;
                user.Email = model.Email;
                user.Phone = model.Phone;
                user.LastLoginData= model.LastLoginData;
                user.Photo = model.Photo;
                user.Status = model.Status;
                _db.Users.Update(user);
                _db.SaveChanges();
            });
        }

        public bool Delete(int index)
        {
            User user = _db.Users.FirstOrDefault(x => x.Id == index);
            if (user is null)
                return false;
            return DoAction(() =>
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            });
        }

        public bool CreateMultipleUsers(List<UserModel> userModels)
        {
            return DoAction(() =>
            {
                User[] newUsers = userModels.Select(u => new User(u)).ToArray();
                _db.Users.AddRange(newUsers);
                _db.SaveChangesAsync();
            });
        }
        
    }
}
