﻿using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.Entity;

namespace TFC.Application.DTO.User.CreateUser
{
    public class CreateUserResponse : BaseResponse 
    {
        public UserDTO? UserDTO { get; set; } 
    }
}