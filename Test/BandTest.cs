using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using BandTracker.Objects;

namespace BandTracker
{
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_EmptyAtFirst()
    {
      //Arrange, Act
      int result = Band.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualOverrideTrueForSameDescription()
    {
      //Arrange, Act
      Band firstBand = new Band("First Band", 1);
      Band secondBand = new Band("First Band", 1);

      //Assert
      Assert.Equal(firstBand, secondBand);
    }

    [Fact]
    public void Test_Save()
    {
      //Arrange
      Band testBand = new Band("First Band", 1);
      testBand.Save();

      //Act
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_SaveAssignsIdToObject()
    {
      //Arrange
      Band testBand = new Band("First Band", 1);
      testBand.Save();

      //Act
      Band savedBand = Band.GetAll()[0];

      int result = savedBand.GetId();
      int testId = testBand.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindFindsBandInDatabase()
    {
      //Arrange
      Band testBand = new Band("First Band", 1);
      testBand.Save();

      //Act
      Band result = Band.Find(testBand.GetId());

      //Assert
      Assert.Equal(testBand, result);
    }

    [Fact]
    public void Test_AddVenue_AddsVenueToBand()
    {
      //Arrange
      Band testBand = new Band("First Band", 1);
      testBand.Save();

      Venue testVenue = new Venue("Second Band", 1);
      testVenue.Save();

      //Act
      testBand.AddVenue(testVenue);

      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue>{testVenue};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetVenues_ReturnsAllBandVenues()
    {
      //Arrange
      Band testBand = new Band("First Band", 1);
      testBand.Save();

      Venue testVenue1 = new Venue("Second Band", 1);
      testVenue1.Save();

      Venue testVenue2 = new Venue("Third Band", 2);
      testVenue2.Save();

      //Act
      testBand.AddVenue(testVenue1);
      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue> {testVenue1};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Delete_DeletesBandAssociationsFromDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Second Band", 1);
      testVenue.Save();

      string testDescription = "First Band";
      Band testBand = new Band(testDescription, 1);
      testBand.Save();

      //Act
      testBand.AddVenue(testVenue);
      testBand.Delete();

      List<Band> resultVenueBands = testVenue.GetBands();
      List<Band> testVenueBands = new List<Band> {};

      //Assert
      Assert.Equal(testVenueBands, resultVenueBands);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
