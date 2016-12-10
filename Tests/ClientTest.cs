using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void GetAll_ClientsEmptyAtFirst_0()
    {
      int result = Client.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Equal_ReturnsTrueForSamePropertiesFromDiffInstances_true()
    {
      //Arrange, Act
      Client firstClient = new Client("Ally Berry", 1);
      Client secondClient = new Client("Ally Berry", 1);

      //Assert
      Assert.Equal(firstClient, secondClient);
    }


    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
