﻿using ProfileBook.Models;
using ProfileBook.Servises.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ProfileBook.Servises.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private IRepository _repository;

        public AuthenticationService(IRepository repository)
        {
            _repository = repository;
        }

        public int VerifyUser(string login, string password)
        {
            var user = GetAllUsers().Where(x => x.Login == login && x.Password == password).ToList();

            if (user == null)
            {
                return 0;
            }

            return user[0].Id;
        }

        public bool IsLogin(string login)
        {        
            var user = GetAllUsers().Where(x => x.Login == login).ToList();

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public int AddUser(UserModel user)
        {
            var result = _repository.InsertAsync(user).Result;

            return result;
        }

        private List<UserModel> GetAllUsers()
        {
            var users = _repository.GetAllAsync<UserModel>().Result;

            return users;
        }
    }
}
