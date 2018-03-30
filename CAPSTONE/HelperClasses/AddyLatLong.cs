using CAPSTONE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace CAPSTONE.HelperClasses
{
    public class AddyLatLong
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<string> GetLatLng(string model)
        {
            string requestUri = "https://maps.googleapis.com/maps/api/geocode/xml?key=AIzaSyC4ALH2fGv4h1UC3bN0y8QgmOJCEhop7K8&address=" + Uri.EscapeDataString(model);
            List<string> latlng = new List<string>();
            WebRequest request = WebRequest.Create(requestUri);
            WebResponse response = request.GetResponse();
            XDocument xdoc = XDocument.Load(response.GetResponseStream());

            XElement result = xdoc.Element("GeocodeResponse").Element("result");
            XElement locationElement = result.Element("geometry").Element("location");
            XElement lat = locationElement.Element("lat");
            XElement lng = locationElement.Element("lng");
            string newLat = lat.Value.ToString();
            string newLng = lng.Value.ToString();
            latlng.Add(newLat);
            latlng.Add(newLng);
            return latlng;
        }
    }
}