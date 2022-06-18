using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for User
/// Class of User 
/// </summary>
public class User
{
    public int UserId { get; set; }

    public string Name { get; set; }

    public string Position { get; set; }

    public int StatusUser { get; set; }

    public int TypeAnalist { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
    public string RetypePassword { get; set; }

    public int TypeAccess { get; set; }

    public int FirstLogin { get; set; }

    public int DepartId { get; set; }

}