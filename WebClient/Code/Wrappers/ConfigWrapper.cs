using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Socializr.Code.Wrappers
{
    public class ConfigWrapper
    {
        private static ConfigWrapper instance;
        public static ConfigWrapper Instance
        {
            get
            {
                return instance ?? (instance = new ConfigWrapper());
            }
        }

        private ConfigWrapper()
        { }

        public int ResultNumber
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["ResultNumber"]);
            }
        }

        public int CommentsPerLoad
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["CommentsPerLoad"]);
            }
        }

    }
}