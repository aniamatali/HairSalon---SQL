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
      public void Save_SavesCategoryToDatabase_CategoryList()
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


    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
