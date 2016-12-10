using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalon
{
  public class Stylist
  {
    private int _id; //Stylist ID
    private string _name; //Stylist Name
    // private int _number; // Stylist Number
    // private int _clientId;

    public Stylist(string name, int number, int Id = 0) //int clientId)
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

    // public override int GetHashCode()
    // {
    //    return this._number.GetHashCode();
    // }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    // public int GetNumber()
    // {
    //   return _number;
    // }

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
