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
      Venue firstVenue = new Venue("First Venue", 1);
      Venue secondVenue = new Venue("First Venue", 1);

      //Assert
      Assert.Equal(firstVenue, secondVenue);
    }

    [Fact]
    public void Test_Save_SavesVenueToDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("First Venue", 1);
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
      Venue testVenue = new Venue("First Venue", 1);
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
      Venue testVenue = new Venue("First Venue", 1);
      testVenue.Save();

      //Act
      Venue foundVenue = Venue.Find(testVenue.GetId());

      //Assert
      Assert.Equal(testVenue, foundVenue);
    }

    [Fact]
    public void Test_AddBand_AddsBandToVenue()
    {
      //Arrange
      Venue testVenue = new Venue("First Venue", 1);
      testVenue.Save();

      Band testBand = new Band("Second Venue", 2);
      testBand.Save();

      Band testBand2 = new Band("Third Venue", 1);
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
      Venue testVenue = new Venue("First Venue", 1);
      testVenue.Save();

      Band testBand1 = new Band("Second Venue", 2);
      testBand1.Save();

      Band testBand2 = new Band("Third Venue", 2);
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
      Band testBand = new Band("Second Venue", 2);
      testBand.Save();

      string testName = "First Venue";
      Venue testVenue = new Venue(testName, 1);
      testVenue.Save();

      //Act
      testVenue.AddBand(testBand);
      testVenue.Delete();

      List<Venue> resultBandVenues = testBand.GetVenues();
      List<Venue> testBandVenues = new List<Venue> {};

      //Assert
      Assert.Equal(testBandVenues, resultBandVenues);
    }

    [Fact]
    public void Test_Update_UpdatesVenueInDatabase()
    {
      //Arrange
      string name = "First Venue";
      Venue testVenue = new Venue(name, 1);
      testVenue.Save();
      string newName = "Second Venue";

      //Act
      testVenue.Update(newName);

      string result = testVenue.GetPlace();

      //Assert
      Assert.Equal(newName, result);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
