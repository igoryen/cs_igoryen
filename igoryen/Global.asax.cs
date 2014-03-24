using igoryen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;

namespace igoryen
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            System.Data.Entity.Database.SetInitializer(new igoryen.Models.Initializer());

            // to ViewModels/classes

            Mapper.CreateMap<Models.Course, ViewModels.CourseBase>();
            Mapper.CreateMap<Models.Course, ViewModels.CourseFull>();

            Mapper.CreateMap<Models.Faculty, ViewModels.FacultyBase>();
            Mapper.CreateMap<Models.Faculty, ViewModels.FacultyFull>();

            Mapper.CreateMap<Models.Message, ViewModels.MessageBase>();
            Mapper.CreateMap<Models.Message, ViewModels.MessageFull>();

            Mapper.CreateMap<Models.Student, ViewModels.StudentBase>();
            Mapper.CreateMap<Models.Student, ViewModels.StudentFull>();


            // from ViewModels/classes

            Mapper.CreateMap<ViewModels.CourseBase, Models.Course>();
            Mapper.CreateMap<ViewModels.CourseFull, Models.Course>();

            Mapper.CreateMap<ViewModels.FacultyBase, Models.Faculty>();
            Mapper.CreateMap<ViewModels.FacultyFull, Models.Faculty>();

            Mapper.CreateMap<ViewModels.MessageBase, Models.Message>();
            Mapper.CreateMap<ViewModels.MessageFull, Models.Message>();

            Mapper.CreateMap<ViewModels.StudentBase, Models.Student>();
            Mapper.CreateMap<ViewModels.StudentFull, Models.Student>();



        }
    }
}
