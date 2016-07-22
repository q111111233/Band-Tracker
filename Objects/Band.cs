using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Globalization;

namespace BandTracker.Objects
{
  public class Band
  {
    private int _id;
    private string _name;
    private int _popularity;

    public Band(string Name, int Popularity, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _popularity = Popularity;
    }

    public override bool Equals(System.Object otherBand)
    {
        if (!(otherBand is Band))
        {
          return false;
        }
        else {
          Band newBand = (Band) otherBand;
          bool idEquality = this.GetId() == newBand.GetId();
          bool nameEquality = this.GetName() == newBand.GetName();
          return (idEquality && nameEquality);
        }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public int GetPopularity()
    {
      return _popularity;
    }
    public void SetPopularity(int newPopularity)
    {
      _popularity = newPopularity;
    }
    public static List<Band> GetAll()
    {
      List<Band> AllBands = new List<Band>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        int bandPopularity = rdr.GetInt32(2);
        Band newBand = new Band(bandName, bandPopularity, bandId);
        AllBands.Add(newBand);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllBands;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name, popularity) OUTPUT INSERTED.id VALUES (@BandName, @BandPopularity)", conn);

      SqlParameter nameParam = new SqlParameter();
      nameParam.ParameterName = "@BandName";
      TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
      nameParam.Value = textInfo.ToTitleCase(this.GetName());

      cmd.Parameters.Add(nameParam);

      SqlParameter popularityParam = new SqlParameter();
      popularityParam.ParameterName = "@BandPopularity";
      popularityParam.Value = this.GetPopularity();

      cmd.Parameters.Add(popularityParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static Band Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @BandId", conn);
      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = id.ToString();
      cmd.Parameters.Add(bandIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundBandId = 0;
      string foundBandName = null;
      int foundPopularity = 0;

      while(rdr.Read())
      {
        foundBandId = rdr.GetInt32(0);
        foundBandName = rdr.GetString(1);
        foundPopularity = rdr.GetInt32(2);
      }
      Band foundBand = new Band(foundBandName, foundPopularity, foundBandId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundBand;
    }

    public void AddVenue(Venue newVenue)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (venue_id, band_id) VALUES (@VenueId, @BandId);", conn);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = newVenue.GetId();
      cmd.Parameters.Add(venueIdParameter);

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = this.GetId();
      cmd.Parameters.Add(bandIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Venue> GetVenues()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN bands_venues ON (bands.id = bands_venues.band_id) JOIN venues ON (bands_venues.venue_id = venues.id) WHERE bands.id = @BandId", conn);
      SqlParameter BandIdParam = new SqlParameter();
      BandIdParam.ParameterName = "@BandId";
      BandIdParam.Value = this.GetId().ToString();

      cmd.Parameters.Add(BandIdParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Venue> venues = new List<Venue>{};

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venuePlace = rdr.GetString(1);
        int venuePopularity = rdr.GetInt32(2);
        Venue newVenue = new Venue(venuePlace, venuePopularity, venueId);
        venues.Add(newVenue);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return venues;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM bands WHERE id = @BandId; DELETE FROM bands_venues WHERE band_id = @BandId;", conn);
      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = this.GetId();

      cmd.Parameters.Add(bandIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE bands SET name = @NewName OUTPUT INSERTED.name WHERE id = @BandId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
      newNameParameter.Value = textInfo.ToTitleCase(newName);
      cmd.Parameters.Add(newNameParameter);


      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = this.GetId();
      cmd.Parameters.Add(bandIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
