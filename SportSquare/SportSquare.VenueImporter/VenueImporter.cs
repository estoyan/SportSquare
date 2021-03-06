﻿using System.Collections.Generic;
using System.Xml;
using SportSquare.Models;
using System.Linq;

namespace SportSquare.VenueImporter
{
    public class VenueImporter
    {   
        //private const string FILE_PATH = "\\venueList.xml";
        private const string FILE_PATH = "\\SportSquare.VenueImporter\\venueList.xml";

        public IList<Venue> Venues { get; private set; }
        public IList<VenueType> VenueTypes { get; private set; }
        public VenueImporter()
        {
            this.Venues =  new List<Venue>();
            this.VenueTypes =  new List<VenueType>();
        }

        public IList<Venue> ParseVenues()
        {
            var directory = "D:\\GitHub\\Team works\\SportSquare\\SportSquare";
            //var directory = "C:\\Academy\\WebForms\\SportSquareV2\\SportSquare\\SportSquare";

            using (var reader = XmlReader.Create(directory+FILE_PATH))
            {
                double latitude = 0.0;
                double longitude = 0.0;
                string image = "";
                string name = "";
                string city = "";
                string address = "";
                string phone = "";
                string webAddress = "";
                string[] venueType = new string[1];
                bool isInsideDiv = false;
                bool isInsideInfo = false;
                while (reader.Read())
                {
                    if (reader.AttributeCount > 0 && reader.GetAttribute(0) == "sp-club clearfix")
                    {
                        latitude = double.Parse(reader.GetAttribute(1));
                        longitude = double.Parse(reader.GetAttribute(2));
                    }
                    if (reader.AttributeCount > 0 && reader.GetAttribute(0) == "image col-xs-4")
                    {
                        isInsideDiv = true;
                    }
                    if (reader.AttributeCount > 0 && reader.GetAttribute(0) == "info")
                    {
                        isInsideInfo = true;
                    }

                    if (isInsideDiv && reader.Name == "img")
                    {
                        image = reader.GetAttribute(0);
                        isInsideDiv = false;
                    }
                    if (isInsideInfo && reader.Name == "h4" && reader.NodeType == XmlNodeType.Element)
                    {
                        reader.Read();
                        name = reader.Value;
                    }
                    if (isInsideInfo && reader.Name == "p" && reader.NodeType == XmlNodeType.Element && reader.GetAttribute(0) == "city")
                    {
                        reader.Read();
                        city = reader.Value;
                    }
                    if (isInsideInfo && reader.Name == "p" && reader.NodeType == XmlNodeType.Element && reader.GetAttribute(0) == "address")
                    {
                        reader.Read();
                        address = reader.Value;
                    }

                    if (isInsideInfo && reader.AttributeCount > 0 && reader.GetAttribute(0) == "phone")
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            reader.Read();

                        }
                        phone = reader.Value;
                    }
                    if (isInsideInfo && reader.AttributeCount > 0 && reader.GetAttribute(0) == "www")
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            reader.Read();

                        }
                        webAddress = reader.Value;
                    }
                    if (isInsideInfo && reader.AttributeCount > 0 && reader.GetAttribute(0) == "type")
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            reader.Read();

                        }
                        venueType = reader.Value.Split(',');
                        isInsideInfo = false;
                    }
                    if (reader.AttributeCount > 0 && reader.GetAttribute(0) == "benefits")
                    {
                        var venue = new Venue(latitude, longitude, image, name, phone, webAddress, address, city);
                        foreach (var type in venueType)
                        {
                            if(this.VenueTypes.FirstOrDefault(x=>x.Name==type)==null)
                            {
                                var newVenueType = new VenueType();
                                newVenueType.Name = type;
                                this.VenueTypes.Add(newVenueType);
                                venue.VenueTypes.Add(newVenueType);
                            }
                        }
                        this.Venues.Add(venue);
                    }
                }
            }
            return Venues;
        }
    }
}
