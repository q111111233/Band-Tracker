using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker.Objects
{
  public class Venue
  {
    private int _id;
    private string _place;

    public Venue(string Place, int Id = 0)
    {
      _id = Id;
      _place = Place;
    }

    public override bool Equals(System.Object otherVenue)
    {
        if (!(otherVenue is Venue))
        {
          return false;
        }
        else
        {
          Venue newVenue = (Venue) otherVenue;
          bool idEquality = this.GetId() == newVenue.GetId();
          bool placeEquality = this.GetPlace() == newVenue.GetPlace();
          return (idEquality && placeEquality);
        }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetPlace()
    {
      return _place;
    }
    public void SetPlace(string newPlace)
    {
      _place = newPlace;
    }
    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venuePlace = rdr.GetString(1);
        Venue newVenue = new Venue(venuePlace, venueId);
        allVenues.Add(newVenue);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allVenues;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (place) OUTPUT INSERTED.id VALUES (@VenuePlace);", conn);

      SqlParameter placeParameter = new SqlParameter();
      placeParameter.ParameterName = "@VenuePlace";
      placeParameter.Value = this.GetPlace();
      cmd.Parameters.Add(placeParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);
      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = id.ToString();
      cmd.Parameters.Add(venueIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundVenueId = 0;
      string foundVenuePlace = null;

      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0);
        foundVenuePlace = rdr.GetString(1);
      }
      Venue foundVenue = new Venue(foundVenuePlace, foundVenueId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundVenue;
    }

    public void AddBand(Band newBand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (venue_id, band_id) VALUES (@VenueId, @BandId)", conn);
      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.GetId();
      cmd.Parameters.Add(venueIdParameter);

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = newBand.GetId();
      cmd.Parameters.Add(bandIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Band> GetBands()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN bands_venues ON (venues.id = bands_venues.venue_id) JOIN bands ON (bands_venues.band_id = bands.id) WHERE venues.id = @VenueId", conn);
      SqlParameter VenueIdParam = new SqlParameter();
      VenueIdParam.ParameterName = "@VenueId";
      VenueIdParam.Value = this.GetId().ToString();

      cmd.Parameters.Add(VenueIdParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Band> bands = new List<Band>{};

      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        Band newBand = new Band(bandName, bandId);
        bands.Add(newBand);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return bands;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @VenueId; DELETE FROM bands_venues WHERE venue_id = @VenueId;", conn);
      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.GetId();

      cmd.Parameters.Add(venueIdParameter);
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

      SqlCommand cmd = new SqlCommand("UPDATE venues SET place = @NewName OUTPUT INSERTED.place WHERE id = @VenueId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);


      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.GetId();
      cmd.Parameters.Add(venueIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._place = rdr.GetString(0);
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
