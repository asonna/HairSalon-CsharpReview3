using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void GetAll_StylistsEmptyAtFirst_0()
    {
      int result = Stylist.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Equal_ReturnsTrueForSamePropertiesFromDiffInstances_true()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("Ally Berry", 1);
      Stylist secondStylist = new Stylist("Ally Berry", 1);

      //Assert
      Assert.Equal(firstStylist, secondStylist);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
