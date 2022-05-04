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
    
    public int Id { get; set; }
    public int Name { get; set; }
    public int Position { get; set; }
    public int Status { get; set; }
    public int TypeAnalist { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int TypeAccess { get; set; }
    public int FirstLogin { get; set; }
    public int Depart_id { get; set; }

    public User()
    {
    //
    // TODO: Add constructor logic here
    //
    }
}