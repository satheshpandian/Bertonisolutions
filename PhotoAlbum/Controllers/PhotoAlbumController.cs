﻿using PhotoAlbhum.Repository;
using PhotoAlbum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoAlbum.Controllers
{
    public class PhotoAlbumController : Controller
    {
        private IApiClient _apiClient;
        public PhotoAlbumController(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        // GET: PhotoAlbum
        public ActionResult Index()
        {
            PhotoAlbumViewModel photoAlbumViewModel = new PhotoAlbumViewModel();
            photoAlbumViewModel.Albums = _apiClient.GetAlbums();
            return View(photoAlbumViewModel);
        }

        public JsonResult GetPhotosById(int id)
        {
            return Json(_apiClient.GetPhotosByAlbumId(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCommentsById(int id)
        {
            return Json(_apiClient.GetCommentsById(id), JsonRequestBehavior.AllowGet);
        }
    }
}
