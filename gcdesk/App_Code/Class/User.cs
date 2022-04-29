using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for Pessoa
/// Class of User 
/// </summary>
public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int TypeAccess { get; set; }
    public int Status { get; set; }
    public User()
    {
    //
    // TODO: Add constructor logic here
    //
    }
}