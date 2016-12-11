using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalon
{
  public class Client
  {
    private string _name; //Client Name
    // private int _number;
    private int _id; //Client ID
    private int _stylistId;

    public Client(string name, int Id = 0, int stylistId = 0)
    {
      _name = name;
      // _number = number;
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
        bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());
        return (idEquality && nameEquality && stylistIdEquality);
      }
    }

    public override int GetHashCode()
    {
       return this.GetName().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string name)
    {
      _name = name;
    }

    // public int GetNumber()
    // {
    //   return _number;
    // }

    public int GetStylistId()
    {
      return _stylistId;
    }

    public void SetStylistId(int newStylistId)
    {
      _stylistId = newStylistId;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, stylist_id) OUTPUT INSERTED.id VALUES (@ClientName, @ClientStylistId);", conn);

      SqlParameter[] insertParameters = new SqlParameter[]
      {
        new SqlParameter("@ClientName", _name),
        // new SqlParameter("@ClientNumber", _number),
        new SqlParameter("@ClientStylistId", _stylistId)
      };
      cmd.Parameters.AddRange(insertParameters);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Client Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", conn);

      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = id.ToString();
      cmd.Parameters.Add(clientIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundClientId = 0;
      string foundClientName = null;
      // int foundClientNumber = null;
      int foundClientStylistId = 0;

      while(rdr.Read())
      {
        foundClientId = rdr.GetInt32(0);
        foundClientName = rdr.GetString(1);
        // foundClientNumber = rdr.GetInt32(3);
        foundClientStylistId = rdr.GetInt32(2);
      }
      Client foundClient = new Client(foundClientName, foundClientId, foundClientStylistId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundClient;
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
        // int clientNumber = rdr.GetInt32(3);
        int clientStylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, clientId, clientStylistId);
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

    public void Update(int newStylistId)
    {
     SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("UPDATE clients SET stylist_id = @newStylistId OUTPUT INSERTED.stylist_id WHERE id = @ClientId;", conn);

     SqlParameter newStylistIdParameter = new SqlParameter();
     newStylistIdParameter.ParameterName = "@newStylistId";
     newStylistIdParameter.Value = newStylistId;
     cmd.Parameters.Add(newStylistIdParameter);


     SqlParameter clientIdParameter = new SqlParameter();
     clientIdParameter.ParameterName = "@ClientId";
     clientIdParameter.Value = this.GetId();
     cmd.Parameters.Add(clientIdParameter);
     SqlDataReader rdr = cmd.ExecuteReader();

     while(rdr.Read())
     {
       this._stylistId = rdr.GetInt32(2);
     }

     if (rdr != null)
     {
       rdr.Close();
     }

     if (conn != null)
     {
       conn.Close();
     }
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
