using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Common
{
    public enum GeoDivisionTypes
    {
        Country = 0,
        State =1,
        City =2
    }


    public static class SystemParameterCodes
    {
        public static string ContractContext = "ContractContext";
        public static string InstagramContractContext = "InstagramContractContext";
        public static string WebsiteContractContext = "WebsiteContractContext";
    }
}
