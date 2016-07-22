using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using BandTracker.Objects;

namespace BandTracker
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_VenuesEmptyAtFirst()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Venue firstVenue = new Venue("Household chores");
      Venue secondVenue = new Venue("Household chores");

      //Assert
      Assert.Equal(firstVenue, secondVenue);
    }

    [Fact]
    public void Test_Save_SavesVenueToDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Household chores");
      testVenue.Save();

      //Act
      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToVenueObject()
    {
      //Arrange
      Venue testVenue = new Venue("Household chores");
      testVenue.Save();

      //Act
      Venue savedVenue = Venue.GetAll()[0];

      int result = savedVenue.GetId();
      int testId = testVenue.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsVenueInDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Household chores");
      testVenue.Save();

      //Act
      Venue foundVenue = Venue.Find(testVenue.GetId());

      //Assert
      Assert.Equal(testVenue, foundVenue);
    }

    [Fact]
    public void Test_Delete_DeletesVenueFromDatabase()
    {
      //Arrange
      string name1 = "Home stuff";
      Venue testVenue1 = new Venue(name1);
      testVenue1.Save();

      string name2 = "Work stuff";
      Venue testVenue2 = new Venue(name2);
      testVenue2.Save();

      //Act
      testVenue1.Delete();
      List<Venue> resultVenues = Venue.GetAll();
      List<Venue> testVenueList = new List<Venue> {testVenue2};

      //Assert
      Assert.Equal(testVenueList, resultVenues);
    }

    [Fact]
    public void Test_AddBand_AddsBandToVenue()
    {
      //Arrange
      Venue testVenue = new Venue("Household chores");
      testVenue.Save();

      Band testBand = new Band("Mow the lawn");
      testBand.Save();

      Band testBand2 = new Band("Water the garden");
      testBand2.Save();

      //Act
      testVenue.AddBand(testBand);
      testVenue.AddBand(testBand2);

      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band>{testBand, testBand2};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetBands_ReturnsAllVenueBands()
    {
      //Arrange
      Venue testVenue = new Venue("Household chores");
      testVenue.Save();

      Band testBand1 = new Band("Mow the lawn");
      testBand1.Save();

      Band testBand2 = new Band("Buy plane ticket");
      testBand2.Save();

      //Act
      testVenue.AddBand(testBand1);
      List<Band> savedBands = testVenue.GetBands();
      List<Band> testList = new List<Band> {testBand1};

      //Assert
      Assert.Equal(testList, savedBands);
    }

    [Fact]
    public void Test_Delete_DeletesVenueAssociationsFromDatabase()
    {
      //Arrange
      Band testBand = new Band("Mow the lawn");
      testBand.Save();

      string testName = "Home stuff";
      Venue testVenue = new Venue(testName);
      testVenue.Save();

      //Act
      testVenue.AddBand(testBand);
      testVenue.Delete();

      List<Venue> resultBandVenues = testBand.GetVenues();
      List<Venue> testBandVenues = new List<Venue> {};

      //Assert
      Assert.Equal(testBandVenues, resultBandVenues);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
