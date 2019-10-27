using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RESTfulHotel.Models;

namespace RESTfulHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : Controller
    {
        private static IHostingEnvironment _hostingEnvironment;
        private static List<RootObject> _jsonHotelsRoot = new List<RootObject>();
        
        public HotelsController(IHostingEnvironment environment) {
            _hostingEnvironment = environment;
        }
        
        // GET api/hotels
        [HttpGet]
        public ActionResult<List<RootObject>> Get()
        {
            InitHotels();
            
            if (_jsonHotelsRoot.Count == 0)
            {
                return NotFound();
            }

            return Ok(_jsonHotelsRoot);
        }
        
        // GET api/hotels/8759
        [HttpGet("{id}")]
        public ActionResult<Hotel> Get(int id)
        {
            InitHotels();

            var selectedHotel = (
                from root in _jsonHotelsRoot
                where root.hotel.hotelID == id
                select new RootObject
                {
                    hotel = root.hotel,
                    hotelRates = root.hotelRates
                }).FirstOrDefault();
            
            if (selectedHotel == null)
            {
                return NotFound();
            }

            return Ok(selectedHotel);
        }
        
        // GET api/hotels/arrival/16.03.2016
        [HttpGet("arrival/{arrival}")]
        public ActionResult<List<RootObject>> Get(string arrival)
        {
            InitHotels();
            
            arrival = FormatArrivalDate(arrival);
            
            var selectedHotels = (
                from root in _jsonHotelsRoot
                select new RootObject
                {
                    hotel = root.hotel,
                    hotelRates = (
                        from rates in root.hotelRates
                        where rates.targetDay == arrival
                        select new HotelRate
                        {
                            adults = rates.adults,
                            los = rates.los,
                            price = rates.price,
                            rateDescription = rates.rateDescription,
                            rateID = rates.rateID,
                            rateName = rates.rateName,
                            rateTags = rates.rateTags,
                            targetDay = rates.targetDay
                        }
                    ).ToList()
                }).ToList();
            
            if (selectedHotels.Count == 0)
            {
                return NotFound();
            }

            return Ok(selectedHotels);
        }
        
        // GET api/hotels/7294/arrival/15.03.2016
        [HttpGet("{id}/arrival/{arrival}")]
        public ActionResult<Hotel> Get(int id, string arrival)
        {
            InitHotels();

            // Convert the date string to the right format
            arrival = FormatArrivalDate(arrival);
            
            var selectedHotel = (
                from root in _jsonHotelsRoot
                where root.hotel.hotelID == id
                select new RootObject
                {
                    hotel = root.hotel,
                    hotelRates = (
                        from hRates in root.hotelRates
                        where hRates.targetDay == arrival
                        select new HotelRate
                        {
                            adults = hRates.adults,
                            los = hRates.los,
                            price = hRates.price,
                            rateDescription = hRates.rateDescription,
                            rateID = hRates.rateID,
                            rateName = hRates.rateName,
                            rateTags = hRates.rateTags,
                            targetDay = hRates.targetDay
                        }
                    ).ToList()
                }).FirstOrDefault();

            if (selectedHotel == null)
            {
                return NotFound();
            }

            // return the first item in the list (should be only one item)
            return Ok(selectedHotel);
        }
        
        /// <summary>
        /// Read from json file and deserialize the object
        /// </summary>
        private static void InitHotels()
        {
            if (_jsonHotelsRoot.Count != 0)
            {
                return;
            }
            
            using (var r = new StreamReader(_hostingEnvironment.ContentRootPath + "\\hotelsrates.json"))
            {
                var json = r.ReadToEnd();

                _jsonHotelsRoot = JsonConvert.DeserializeObject<List<RootObject>>(json);
            }
        }
        
        private static string FormatArrivalDate(string arrDateString)
        {
            var arrDate = Convert.ToDateTime(arrDateString);

            var formattedDate = arrDate.ToString("yyyy-MM-ddT00:00:00.000+01:00");

            return formattedDate;
        }
    }
}