using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for Pessoa
/// </summary>
public class Person
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int TypeAccess { get; set; }
    public int Status { get; set; }
    public Person()
    {
    //
    // TODO: Add constructor logic here
    //
    }
}