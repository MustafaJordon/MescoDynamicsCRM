using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forwarding.MvcApp.Utilities
{
    public class ApplicationMethods
    {
        public static class CompanyName
        {
            public static String GetCompanyName()
            {
                int _RowCount = 0;
                String CoName = " ";
                CDefaults ObjCo = new CDefaults();
                ObjCo.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount);
                try
                {
                    if (ObjCo.lstCVarDefaults.Count > 0)
                    {
                        return ObjCo.lstCVarDefaults[0].UnEditableCompanyName;
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }
    }
}
