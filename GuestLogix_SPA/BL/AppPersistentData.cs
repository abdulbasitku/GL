using GuestLogix_SPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuestLogix_SPA.BL
{
    public class AppPersistentData
    {
        public static GLData GuestLogixData {
            get
            {
                return (GLData)HttpContext.Current.Application["GLData"];
            }
            set
            {
                if (HttpContext.Current.Application.AllKeys.Contains("GLData"))
                {
                    HttpContext.Current.Application["GLData"] = value;
                }
                else
                {
                    HttpContext.Current.Application.Add("GLData", value);
                }
            }
        }

    }
}