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
    public void Equal_ReturnsTrueForSamePropertiesFromDiffInstances_2ndInstanceEquate1st()
    {
      //Arrange, Act
      Client firstClient = new Client("Ally Berry", 1);
      Client secondClient = new Client("Ally Berry", 1);

      //Assert
      Assert.Equal(firstClient, secondClient);
    }

    [Fact]
    public void Save_SavesClientToDatabase_clientList()
    {
      //Arrange
      Client testClient = new Client("Ally Berry", 1);
      testClient.Save();

      //Act
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Save_AssignsIdToClientObject_clientId()
    {
      //Arrange
      Client testClient = new Client("Veronique Moore", 2);
      testClient.Save();

      //Act
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      //Assert
      Assert.Equal(testId, result);
    }


    [Fact]
    public void Find_FindsClientInDatabase_specifiedClient()
    {
      //Arrange
      Client testClient = new Client("Veronique Moore", 2);
      testClient.Save();

      //Act
      Client foundClient = Client.Find(testClient.GetId());

      //Assert
      Assert.Equal(testClient, foundClient);
    }

    [Fact]
    public void update_ChangeClientStylistId_ClientwithNewStylistId()
    {
      //Arrange
    Client updateClient = new Client("Ally Berry", 3);
    updateClient.Save();
    int newStylistId = 5;
    string newName = "Vero Moore";

    //Act
    updateClient.Update(newName, newStylistId);

    Client result = Client.GetAll()[0];
    int comparingId = result.GetId();

    Client testClient = new Client(newName, newStylistId, comparingId);

    //Assert
    Assert.Equal(testClient, result);
    }


    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
