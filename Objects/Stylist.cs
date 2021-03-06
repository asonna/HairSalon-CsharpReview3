using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace HairSalon.Objects
{
  public class Stylist
  {
    private int _id; //Stylist ID
    private string _name; //Stylist Name
    // private int _number; // Stylist Number

    public Stylist(string name, int Id = 0) //int stylistId)
    {
      _name = name;
      // _number = number;
      _id = Id;
      // _clientId = clientId;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = this.GetId() == newStylist.GetId();
        bool nameEquality = this.GetName() ==newStylist.GetName();
        return (idEquality && nameEquality);
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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name) OUTPUT INSERTED.id VALUES (@StylistName);", conn);

      SqlParameter[] insertParameters = new SqlParameter[]
      {
        new SqlParameter("@StylistName", this.GetName())
        // new SqlParameter("@StylistNumber", _number),
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

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        // int stylistNumber = rdr.GetInt32(2);
        // int stylistClientId = rdr.GetInt32(3);
        Stylist newStylist = new Stylist(stylistName, stylistId);
        allStylists.Add(newStylist);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }

      return allStylists;
    }


    public static Stylist Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistId;", conn);

      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@StylistId";
      clientIdParameter.Value = id.ToString();
      cmd.Parameters.Add(clientIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundStylistId = 0;
      string foundStylistName = null;
      // int foundStylistNumber = null;

      while(rdr.Read())
      {
        foundStylistId = rdr.GetInt32(0);
        foundStylistName = rdr.GetString(1);
        // foundStylistNumber = rdr.GetInt32(3);
      }
      Stylist foundStylist = new Stylist(foundStylistName, foundStylistId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundStylist;
    }

    public List<Client> GetClients()
   {
     SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylist_id = @StylistId;", conn);
     SqlParameter stylistIdParameter = new SqlParameter("@StylistId", this.GetId());
     cmd.Parameters.Add(stylistIdParameter);
     SqlDataReader rdr = cmd.ExecuteReader();

     List<Client> stylistClients = new List<Client>{};
     while(rdr.Read())
     {
       int clientId = rdr.GetInt32(0);
       string clientName = rdr.GetString(1);
       int clientStylistId = rdr.GetInt32(2);
       Client newClient = new Client(clientName, clientStylistId, clientId);
       stylistClients.Add(newClient);
     }
     if (rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }
     return stylistClients;
   }

    public void Update(string newName)
    {
     SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("UPDATE stylists SET name = @NewName OUTPUT INSERTED.name WHERE id = @StylistId;", conn);

     SqlParameter newNameParameter = new SqlParameter();
     newNameParameter.ParameterName = "@NewName";
     newNameParameter.Value = newName;
     cmd.Parameters.Add(newNameParameter);


     SqlParameter stylistIdParameter = new SqlParameter();
     stylistIdParameter.ParameterName = "@StylistId";
     stylistIdParameter.Value = this.GetId().ToString();
     cmd.Parameters.Add(stylistIdParameter);

     cmd.ExecuteNonQuery();
    //  SqlDataReader rdr = cmd.ExecuteReader();

    //  while(rdr.Read())
    //  {
    //    this._name = rdr.GetString(0);
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

      SqlCommand cmd = new SqlCommand("DELETE FROM stylists WHERE id = @StylistId; DELETE FROM clients WHERE stylist_id = @StylistId;", conn);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();

      cmd.Parameters.Add(stylistIdParameter);
      cmd.ExecuteNonQuery();


      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
