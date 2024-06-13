using System;
using UniversityApp.Service.Dtos.UserDtos;

namespace UniversityApp.Service.Interfaces
{
	public interface IAuthService
	{
        string Login(UserLoginDto loginDto);
    }
}

