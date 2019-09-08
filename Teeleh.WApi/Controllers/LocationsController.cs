using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinqToExcel;
using Teeleh.Models;
using Teeleh.Models.CustomValidation.Website;
using Teeleh.Models.Enums;
using Teeleh.Models.Helper;
using Teeleh.Models.ViewModels.Website_View_Models;

namespace Teeleh.WApi.Controllers
{
    public class LocationsController : Controller
    {
        private readonly AppDbContext db;

        public LocationsController()
        {
            db = new AppDbContext();
        }

        // GET: Locations
        public ActionResult Index()
        {
            return View();
        }


        [SessionTimeout]
        public ActionResult CreateProvince()
        {
            var viewModel = new LocationFormViewModel {Type = LocationType.PROVINCE};

            return View(viewModel);
        }

        [SessionTimeout]
        public ActionResult CreateCity()
        {
            var viewModel = new LocationFormViewModel();

            var locations = db.Locations.Where(l => l.Type == LocationType.PROVINCE).ToList();

            viewModel.Parents = locations;
            viewModel.Type = LocationType.CITY;

            return View(viewModel);
        }

        [SessionTimeout]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(LocationFormViewModel locationViewModel)
        {
            string viewToReturn = locationViewModel.Type == LocationType.CITY ? "CreateCity" : "CreateProvince";
            if (locationViewModel.Type == LocationType.CITY)
            {
                locationViewModel.Parents = db.Locations.Where(l => l.Type == LocationType.PROVINCE).ToList();
            }
            if (ModelState.IsValid)
            {
                string data = "";
                if (locationViewModel.ExcelFile != null)
                {
                    if (locationViewModel.ExcelFile.ContentType == "application/vnd.ms-excel" ||
                        locationViewModel.ExcelFile.ContentType ==
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        string fileName = locationViewModel.ExcelFile.FileName;
                        if (fileName.EndsWith(".xlsx"))
                        {
                            string targetPath = Server.MapPath("~/UploadedFiles/Locations/");
                            locationViewModel.ExcelFile.SaveAs(targetPath + fileName);
                            string pathToExcelFile = targetPath + fileName;

                            var excelFile = new ExcelQueryFactory(pathToExcelFile);
                            var locations = from a in excelFile.Worksheet<LocationExcelObject>("Sheet1") select a;
                            foreach (var location in locations)
                            {
                                var locationName = location.Name.TrimEnd(' ').TrimStart(' ');
                                var newLocation = new Location()
                                {
                                    Name = locationName,
                                    ParentId = locationViewModel.Type == LocationType.CITY
                                        ? locationViewModel.ParentId
                                        : (int?) null,
                                    Type = locationViewModel.Type
                                };
                                db.Locations.Add(newLocation);
                            }

                            db.SaveChanges();
                            ViewBag.Message = "Done!";
                            return View(viewToReturn,locationViewModel);
                        }

                        ViewBag.Message = "This file is not valid format";
                        return View(viewToReturn,locationViewModel);
                    }
                    ViewBag.Message = "Only Excel file format is allowed";
                    return View(viewToReturn,locationViewModel);
                }
            }

            ViewBag.Message = "Oops... something went wrong!";
            return View(viewToReturn,locationViewModel);
        }
    }
}