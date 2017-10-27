using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
        public StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=isaac_niamatali_test;";
        }

       [TestMethod]
       public void StylistsShouldBe0()
       {
         //Arrange, Act
         int result = Stylist.GetAll().Count;

         //Assert
         Assert.AreEqual(0, result);
       }

       [TestMethod]
      public void StylistNamesMatch()
      {
        //Arrange, Act
        Stylist firstStylist = new Stylist("billy gates");
        Stylist secondStylist = new Stylist("billy gates");

        //Assert
        Assert.AreEqual(firstStylist, secondStylist);
      }

      [TestMethod]
      public void Saves()
      {
        //Arrange
        Stylist testStylist = new Stylist("billy gates");
        testStylist.Save();

        //Act
        List<Stylist> result = Stylist.GetAll();
        List<Stylist> testList = new List<Stylist>{testStylist};

        //Assert
        CollectionAssert.AreEqual(testList, result);
      }

      [TestMethod]
    public void FindStylistsinDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("jeffrey bezos");
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      //Assert
      Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void GetAllClients()
    {
      Stylist testStylist = new Stylist("steven jobbers");
      testStylist.Save();

      Client firstClient = new Client("david chappelle", testStylist.GetId(), "3pm");
      firstClient.Save();
      Client secondClient = new Client("barbara ballters", testStylist.GetId(), "3pm");
      secondClient.Save();


      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClients();

      CollectionAssert.AreEqual(testClientList, resultClientList);
    }


    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
