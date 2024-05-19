using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoginDemoServer.Models;

public partial class LoginDemoDbContext : DbContext
{
    public Models.Users GetUSerFromDB(string email)
    {
        Models.Users user = this.Users.Where(u => u.Email == email).FirstOrDefault();
        return user;
    }
    public Users UserLogin(string email, string password)
    {
        return Users.AsNoTracking().Where(uu => uu.Email == email && uu.Password == password).FirstOrDefault();
    }
}

