using Domain.Dto;
using Domain.Entity;
using Domain.Interface.Repository;
using Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> GetByLogin(UserDto userDto)
        {
            try
            {
                User user = await _repository.GetByLogin(userDto.Login);

                if (user is null)
                {
                    throw new Exception("Usuário não encontrado.");
                }

                return new UserDto(user.Login, user.Password);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDto> Save(UserDto userDto)
        {
            try
            {
                User user = await _repository.GetByLogin(userDto.Login);
                User result;
                User userConverted;

                if (user is null)
                {
                    userConverted = new User(userDto.Login, userDto.Password);

                    result = await _repository.New(userConverted);

                    if (result is null)
                    {
                        throw new Exception("Erro ao cadastrar usuário.");
                    }
                }
                else
                {
                    userConverted = new User(userDto.Login, userDto.Password, user.Id);
                    
                    result = await _repository.Update(userConverted);

                    if (result is null)
                    {
                        throw new Exception("Erro ao cadastrar usuário.");
                    }
                }

                return userDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
