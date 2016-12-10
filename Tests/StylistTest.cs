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
      Stylist firstStylist = new Stylist("Ally Berry");
      Stylist secondStylist = new Stylist("Ally Berry");

      //Assert
      Assert.Equal(firstStylist, secondStylist);
    }

    [Fact]
    public void Save_SavesStylistToDatabase_testList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Ally Berry");
      testStylist.Save();

      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      Assert.Equal(testList, result);
    }


    [Fact]
    public void Save_AssignsIdToStylistObject_testId()
    {
      //Arrange
      Stylist testStylist = new Stylist("Veronique Moore");
      testStylist.Save();

      //Act
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
