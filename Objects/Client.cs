using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalon
{
  public class Client
  {
    private string _name; //Client Name
    private int _number;
    private int _id; //Client ID
    private int _stylistId;

    public Client(string name, int number, int Id = 0, int stylistId = 0)
    {
      _name = name;
      _number = number;
      _id = Id;
      _stylistId = stylistId;
    }


  }
}
