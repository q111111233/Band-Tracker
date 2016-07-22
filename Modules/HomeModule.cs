using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using BandTracker.Objects;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/bands"] = _ => {
        List<Band> AllBands = Band.GetAll();
        return View["bands.cshtml", AllBands];
      };
      Get["/venues"] = _ => {
        List<Venue> AllVenues = Venue.GetAll();
        return View["venues.cshtml", AllVenues];
      };
      //Create a new band
      Get["/bands/new"] = _ => {
        return View["bands_form.cshtml"];
      };
      Post["/bands/new"] = _ => {
        Band newBand = new Band(Request.Form["band-name"], Request.Form["band-popularity"]);
        newBand.Save();
        return View["success.cshtml"];
      };
      Get["/venues/new"] = _ => {
        return View["venues_form.cshtml"];
      };
      Post["/venues/new"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue-name"], Request.Form["venue-popularity"]);
        newVenue.Save();
        return View["success.cshtml"];
      };
      Get["bands/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band SelectedBand = Band.Find(parameters.id);
        List<Venue> BandVenues = SelectedBand.GetVenues();
        List<Venue> AllVenues = Venue.GetAll();
        model.Add("band", SelectedBand);
        model.Add("bandVenues", BandVenues);
        model.Add("allVenues", AllVenues);
        return View["band.cshtml", model];
      };

      Get["venues/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue SelectedVenue = Venue.Find(parameters.id);
        List<Band> VenueBands = SelectedVenue.GetBands();
        List<Band> AllBands = Band.GetAll();
        model.Add("venue", SelectedVenue);
        model.Add("venueBands", VenueBands);
        model.Add("allBands", AllBands);
        return View["venue.cshtml", model];
      };

      Post["band/add_venue"] = _ => {
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        Band band = Band.Find(Request.Form["band-id"]);
        if(band.GetPopularity() < venue.GetPopularity())
        {
          return View["failure.cshtml"];
        }
        else
        {
          band.AddVenue(venue);
        }
        return View["success.cshtml"];
      };
      Post["venue/add_band"] = _ => {
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        Band band = Band.Find(Request.Form["band-id"]);
        if(band.GetPopularity() < venue.GetPopularity())
        {
          return View["failure.cshtml"];
        }
        else
        {
          venue.AddBand(band);
        }
        return View["success.cshtml"];
      };
      Delete["/delete_venues/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        SelectedVenue.Delete();
        return View["venues.cshtml", Venue.GetAll()];
      };
      Get["/edit_venues/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        return View["venue_edit.cshtml", SelectedVenue];
      };
      Patch["/edit_venues/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        SelectedVenue.Update(Request.Form["venue-name"]);
        return View["venues.cshtml", Venue.GetAll()];
      };
      Delete["/delete_bands/{id}"] = parameters => {
        Band SelectedBand = Band.Find(parameters.id);
        SelectedBand.Delete();
        return View["bands.cshtml", Band.GetAll()];
      };
      Get["/edit_bands/{id}"] = parameters => {
        Band SelectedBand = Band.Find(parameters.id);
        return View["band_edit.cshtml", SelectedBand];
      };
      Patch["/edit_bands/{id}"] = parameters => {
        Band SelectedBand = Band.Find(parameters.id);
        SelectedBand.Update(Request.Form["band-name"]);
        return View["bands.cshtml", Band.GetAll()];
      };
    }
  }
}
