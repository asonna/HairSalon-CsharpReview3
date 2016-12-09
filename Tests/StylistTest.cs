using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient; // Check this name later on both tabs

namespace HairSalon
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    
  }
}
