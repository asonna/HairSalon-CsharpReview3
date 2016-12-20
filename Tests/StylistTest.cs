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
    public void Equal_ReturnsTrueForSamePropertiesFromDiffInstances_2ndInstanceEquate1st()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("Ally Berry");
      Stylist secondStylist = new Stylist("Ally Berry");

      //Assert
      Assert.Equal(firstStylist, secondStylist);
    }

    [Fact]
    public void Save_SavesStylistToDatabase_stylistList()
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
    public void Save_AssignsIdToStylistObject_stylistId()
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

    [Fact]
    public void Find_FindsStylistInDatabase_specifiedStylist()
    {
      //Arrange
      Stylist testStylist = new Stylist("Veronique Moore");
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      //Assert
      Assert.Equal(testStylist, foundStylist);
    }


    // [Fact]
    // public void GetClients_RetrievesAllClientsWithStylist_stylistClientList()
    // {
    //   Stylist testStylist = new Stylist("Vanessa Paradis");
    //   testStylist.Save();
    //
    //   Client firstClient = new Client("Veronique Moore", testStylist.GetId());
    //   firstClient.Save();
    //   Client secondClient = new Client("Diana Ross", testStylist.GetId());
    //   secondClient.Save();
    //
    //
    //   List<Client> testClientList = new List<Client> {firstClient, secondClient};
    //   List<Client> resultClientList = testStylist.GetClients();
    //
    //   Assert.Equal(testClientList, resultClientList);
    // }

    [Fact]
    public void update_ChangeStylistName_StylistwithNewName()
    {
      //Arrange
    Stylist updateStylist = new Stylist("Ally Berry");
    updateStylist.Save();
    string newName = "Veronica Moore";

    //Act
    updateStylist.Update(newName);

    Stylist result = Stylist.GetAll()[0];
    int comparingId = result.GetId();
    Stylist testStylist = new Stylist(newName, comparingId);

    //Assert
    Assert.Equal(testStylist, result);
    }

    [Fact]
    public void Delete_DeletesStylistFromDatabase()
    {
      //Arrange
      string name1 = "Ally Berry";
      Stylist testStylist1 = new Stylist(name1);
      testStylist1.Save();

      string name2 = "Veronique Moore";
      Stylist testStylist2 = new Stylist(name2);
      testStylist2.Save();

      Client testClient1 = new Client("Carole", testStylist1.GetId());
      testClient1.Save();
      Client testClient2 = new Client("Ruth", testStylist2.GetId());
      testClient2.Save();

      //Act
      testStylist1.Delete();
      List<Stylist> resultStylists = Stylist.GetAll();
      List<Stylist> testStylistList = new List<Stylist> {testStylist2};

      List<Client> resultClients = Client.GetAll();
      List<Client> testClientList = new List<Client> {testClient2};

      //Assert
      Assert.Equal(testStylistList, resultStylists);
      Assert.Equal(testClientList, resultClients);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
