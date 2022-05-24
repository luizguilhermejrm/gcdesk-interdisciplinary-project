using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for Login
/// Class of Login
/// </summary>
public class Ticket
{
    private int _ticketID;
    private string _description;
    private string _localization;
    private string _typeTicket;
    private string _status;
    private string _openTime;
    private string _closeTime;
    private string _grade;
    private int _userID;
    private int _analistyID;

    public int TicketId
    {
        get
        {
            return _ticketID;
        }

        set
        {

            _ticketID = value;
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }

        set
        {

            _description = value;
        }
    }

    public string Localization
    {
        get
        {
            return _localization;
        }

        set
        {

            _localization = value;
        }
    }

    public string TypeTicket
    {
        get
        {
            return _typeTicket;
        }

        set
        {

            _typeTicket = value;
        }
    }

    public string Status
    {
        get
        {
            return _status;
        }

        set
        {

            _status = value;
        }
    }

    public string OpenTime
    {
        get
        {
            return _openTime;
        }

        set
        {

            _openTime = value;
        }
    }

    public string CloseTime
    {
        get
        {
            return _closeTime;
        }

        set
        {

            _closeTime = value;
        }
    }

    public string Grade
    {
        get
        {
            return _grade;
        }

        set
        {

            _grade = value;
        }
    }

    public int UserId
    {
        get
        {
            return _userID;
        }

        set
        {

            _userID = value;
        }
    }

    public int AnalistyId
    {
        get
        {
            return _analistyID;
        }

        set
        {

            _analistyID = value;
        }
    }

}