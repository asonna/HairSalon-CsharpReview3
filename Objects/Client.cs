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

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = this.GetId() == newClient.GetId();
        bool nameEquality = this.GetName() ==newClient.GetName();
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
       return this._number.GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public int GetNumber()
    {
      return _number;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }

    
    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientNumber = rdr.GetInt32(2);
        int clientStylistId = rdr.GetInt32(3);
        Client newClient = new Client(clientName, clientNumber, clientId, clientStylistId);
        allClients.Add(newClient);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }

      return allClients;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
