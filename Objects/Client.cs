using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace HairSalon.Objects
{
  public class Client
  {
    private string _name; //Client Name
    private int _id; //Client ID
    private int _stylistId;

    public Client(string name, int stylistId, int Id = 0)
    {
      _id = Id;
      _name = name;
      // _number = number;
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

    public void SetId(int id)
    {
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string name)
    {
      _name = name;
    }
    //
    // public void SetStylistId(int stylistId)
    // {
    //   _stylistId = stylistId;
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
      Client foundClient = new Client(foundClientName, foundClientStylistId, foundClientId);

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
        Client newClient = new Client(clientName, clientStylistId, clientId);
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

    public void Update(string newName, int newStylistId)
    {
     SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @Clientname, stylist_id = @newStylistId WHERE id = @ClientId;", conn);

     SqlParameter clientNameParameter = new SqlParameter();
     clientNameParameter.ParameterName = "@ClientName";
     clientNameParameter.Value = newName;
     cmd.Parameters.Add(clientNameParameter);

     SqlParameter newStylistIdParameter = new SqlParameter();
     newStylistIdParameter.ParameterName = "@newStylistId";
     newStylistIdParameter.Value = newStylistId.ToString();
     cmd.Parameters.Add(newStylistIdParameter);

     SqlParameter clientIdParameter = new SqlParameter();
     clientIdParameter.ParameterName = "@ClientId";
     clientIdParameter.Value = this.GetId().ToString();
     cmd.Parameters.Add(clientIdParameter);

     cmd.ExecuteNonQuery();

    //  SqlDataReader rdr = cmd.ExecuteReader();
     //
    //  while(rdr.Read())
    //  {
    //   //  this._id = rdr.GetInt32(0);
    //   //  this._name = rdr.GetString(1);
    //    this._stylistId = rdr.GetInt32(0);
    //  }
     //
    //  if (rdr != null)
    //  {
    //    rdr.Close();
    //  }

     if (conn != null)
     {
       conn.Close();
     }
    }

    public void Delete()
    {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @ClientId;", conn);
        SqlParameter idParameter = new SqlParameter();
        idParameter.ParameterName = "@ClientId";
        idParameter.Value = this.GetId().ToString();
        cmd.Parameters.Add(idParameter);

        cmd.ExecuteNonQuery();
        if(conn != null)
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
