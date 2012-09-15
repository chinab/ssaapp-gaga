using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Profile;

namespace SAP.Admin
{
    public class ProfileCommon
    {
        public String Country
        {
            get
            {
                return (String)HttpContext.Current.Profile.GetPropertyValue("Teachers");
            }
            set
            {
                HttpContext.Current.Profile.SetPropertyValue("Teachers", value);
            }
        }

        public String Gender
        {
            get
            {
                return (String)HttpContext.Current.Profile.GetPropertyValue("Gender");
            }
            set
            {
                HttpContext.Current.Profile.SetPropertyValue("Gender", value);
            }
        }

        public Int32 Age
        {
            get
            {
                return (Int32)HttpContext.Current.Profile.GetPropertyValue("Age");
            }
            set
            {
                HttpContext.Current.Profile.SetPropertyValue("Age", value);
            }
        }
    }
}