﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    internal interface IData
    {
        List<Users> GetUsers();
        int SaveUser(Users users);
        int UpdateUser(Users users);
        int DeleteUser(int id);
        Users GetUsersByID(string id);
    }
}
