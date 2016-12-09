using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalon
{
  public class Stylist
  {
    private int _idS; //Stylist ID
    private string _nameS; //Stylist Name
    private int _numberS; // Stylist Number
    private int _clientId;

    public Client(string name, int number, int Id = 0, int clientId)
    {
      _nameS = name;
      _numberS = number;
      _idS = Id;
      _clientId = clientId;
    }

    
  }
}
