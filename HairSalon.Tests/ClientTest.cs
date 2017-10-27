using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{

    [TestClass]
    public class ClientTests : IDisposable
    {
        public ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=isaac_niamatali_test;";
        }

        public void Dispose()
        {
          Client.DeleteAll();
          Stylist.DeleteAll();
        }

        [TestMethod]
        public void SameNameisTrue()
        {
          //Arrange, Act
          Client firstClient = new Client("jeff bae-zos", 1, "3pm");
          Client secondClient = new Client("jeff bae-zos", 1, "3pm");

          //Assert
          Assert.AreEqual(firstClient, secondClient);
        }

        [TestMethod]
        public void SavesClientToList()
        {
          //Arrange
          Client testClient = new Client("jeff bae-zos", 1, "3pm");
          testClient.Save();

          //Act
          List<Client> result = Client.GetAll();
          List<Client> testList = new List<Client>{testClient};

          //Assert
          CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_DatabaseAssignsIdToObject_Id()
        {
          //Arrange
          Client testClient = new Client("lisa su", 1, "3pm");
          testClient.Save();

          //Act
          Client savedClient = Client.GetAll()[0];

          int result = savedClient.GetId();
          int testId = testClient.GetId();

          //Assert
          Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void FindClient()
        {
          //Arrange
          Client testClient = new Client("jensen huaaaaaang", 1, "3pm");
          testClient.Save();

          //Act
          Client foundClient = Client.Find(testClient.GetId());

          //Assert
          Assert.AreEqual(testClient, foundClient);
        }


    }
}
