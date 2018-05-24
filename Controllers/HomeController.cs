using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelAgency.Models;

namespace TravelAgency.Controllers
{
    public class HomeController : Controller
    {
        TravelAgencyContext db = new TravelAgencyContext();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public ActionResult DeleteTour(int? ID = 1)
        {
            IQueryable<Tour> Tour = db.Tours;
            Tour tour = Tour.Where(p => p.ID_Tour == ID).FirstOrDefault();

            ViewBag.ID_Tour = ID;
            ViewBag.Country = tour.Resort.Country.Name;
            ViewBag.Resort = tour.Resort.Name;
            ViewBag.Hotel = tour.Hotel.Name;            
            ViewBag.Price = tour.Price;            
            return View();

        }


        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public ActionResult DeleteTour(Tour tour)
        {
            db.Tours.Remove(db.Tours.Find(tour.ID_Tour));
            db.SaveChanges();
            return RedirectToAction("FilteredBrowse");
        }


        [HttpGet]
        [Authorize(Roles = "Manager, Administrators")]
        public ActionResult AddTour()
        {
            SelectList countries = new SelectList(db.Countries, "ID_Country", "Name");
            SelectList resorts = new SelectList(db.Resorts, "ID_Resort", "Name");
            SelectList hotels = new SelectList(db.Hotels, "ID_Hotel", "Name");
            
            ViewBag.Countries = countries;
            ViewBag.Resorts = resorts;
            ViewBag.Hotels = hotels;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Manager, Administrators")]
        public ActionResult AddTour(Tour tour)
        {
            if (tour.DateStart < tour.DateEnd)
            {
                db.Tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("FilteredBrowse");
            }
            else {
                ViewBag.Message = "Дата окончания тура должна быть больше даты начала";
                SelectList countries = new SelectList(db.Countries, "ID_Country", "Name");
                SelectList resorts = new SelectList(db.Resorts, "ID_Resort", "Name");
                SelectList hotels = new SelectList(db.Hotels, "ID_Hotel", "Name");

                ViewBag.Countries = countries;
                ViewBag.Resorts = resorts;
                ViewBag.Hotels = hotels;
                return View();

            }
 
        }

        [HttpGet]
        [Authorize]
        public ActionResult AboutTour(int? ID = 1)
        {
            IQueryable<Tour> Tour = db.Tours;
            Tour tour = Tour.Where(p => p.ID_Tour == ID).FirstOrDefault();
            
            return View(tour);
        }

        [HttpGet]
        [Authorize(Roles = "Manager, Administrators")]
        public ActionResult ChangeTour(int? ID = 1)
        {
            IQueryable<Tour> Tour = db.Tours;
            Tour tour = Tour.Where(p => p.ID_Tour == ID).FirstOrDefault();
            
            ViewBag.ID_Tour = ID;
            ViewBag.Country = tour.Resort.Country.Name;
            ViewBag.Resort = tour.Resort.Name;
            ViewBag.Hotel = tour.Hotel.Name;
            ViewBag.Price = tour.Price;
            ViewBag.DateStart = tour.DateStart;
            ViewBag.DateEnd = tour.DateEnd;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Manager, Administrators")]
        public ActionResult ChangeTour(Tour tour)
        {
            db.Tours.Find(tour.ID_Tour).Price = tour.Price;            
            db.Tours.Find(tour.ID_Tour).DateEnd = tour.DateEnd;
            db.Tours.Find(tour.ID_Tour).DateStart = tour.DateStart;            
            db.SaveChanges();
            return RedirectToAction("FilteredBrowse");
        }


        [Authorize]
        public ActionResult FilteredBrowse(int? country = 1, int? resort = 1, int? stars = null, int page = 1, bool free = false)
        {
            IQueryable<Tour> Tour = db.Tours;
            
            if (country != null)
            {
                Tour = Tour.Where(p => p.Resort.Country.ID_Country == country);
            }
            if (resort != null)
            {
                Tour = Tour.Where(p => p.Resort.ID_Resort == resort);
            }
            if (stars != null)
            {
                Tour = Tour.Where(p => p.Hotel.Stars <= stars);
            }

            List<Country> countryList = db.Countries.ToList();
            var countryResort = from res in db.Resorts
                                where res.ID_Country == country
                                select res;
            List < Resort > resortList = countryResort.ToList();
            int pageSize = 5; // количество объектов на страницу


            IEnumerable<Tour> InfoPerPages = Tour.OrderBy(p => p.ID_Tour).Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Tour.Count()
            };

            FilteredTours ft = new FilteredTours
            {
                Tours = InfoPerPages.ToList(),
                 Country= new SelectList(countryList, "ID_Country", "Name", country),
                CountryResort = new SelectList(resortList, "ID_Resort", "Name", resort),
                SelectedToursCountry = country,
                SelectedResort = resort,
                SelectedStar = stars,
                PageInfo = pageInfo,
                Free = free
            };
            return View(ft);
            
        }

        public string getResorts(int? country) {
            IQueryable<Resort> countryResort = from res in db.Resorts
                                where res.ID_Country == country
                                select res;
            List<Resort> resortList = countryResort.ToList();
            SelectList resorts = new SelectList(resortList, "ID_Resort", "Name");
            
            return JsonConvert.SerializeObject(resorts);
        }

        public string getHotels(int? country) {
            var countryHotel = from res in db.Hotels
                                where res.ID_Country == country
                                select res;
            List<Hotel> hotelList = countryHotel.ToList();
            SelectList hotels = new SelectList(hotelList, "ID_Hotel", "Name");
            return JsonConvert.SerializeObject(hotels);
        }

    }
}