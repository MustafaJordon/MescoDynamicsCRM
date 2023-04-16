using Forwarding.MvcApp.Models.Accounting.MasterData.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Customized;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.MasterData.BanksAccountsAndTreasuries.Generated;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers.Administration.API_Settings
{
    public class MergeDuplicateController : ApiController
    {
        //[Route("/api/MergeDuplicate/LoadAll")]
        [HttpGet, HttpPost]
        //sherif: to be used in select boxes
        public Object[] LoadAll(string pItemType, string pWhereClause)
        {
            var type = int.Parse(pItemType);
            var serialize = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            if (type == 1)
            {

                CCustomers objCCustomer = new CCustomers();
                objCCustomer.GetList(pWhereClause);
                return new Object[] { serialize.Serialize(objCCustomer.lstCVarCustomers) };

            }
            if (type == 2)
            {

                CAgents cAgents = new CAgents();
                cAgents.GetList(pWhereClause);
                return new Object[] { serialize.Serialize(cAgents.lstCVarAgents) };

            }
            if (type == 11)
            {

                CCustody cCustody = new CCustody();
                cCustody.GetList(pWhereClause);
                return new Object[] { serialize.Serialize(cCustody.lstCVarCustody) };

            }
            else if (type == 10)
            {

                CA_SubAccounts objCA_SubAccounts = new CA_SubAccounts();
                objCA_SubAccounts.GetList(pWhereClause);
                return new Object[] { serialize.Serialize(objCA_SubAccounts.lstCVarA_SubAccounts) };

            }
            else
            {
                return new Object[] { null };

            }

        }

        [HttpGet, HttpPost]
        //sherif: to be used in select boxes
        public Object[] Merge(string pItemType, string pDuplicateItemsIDs, string pDestinationItemID , string pSubAccountsIDs_ForDuplicateItems , string pSubAccountFor_DestinationItemID)
        {
            var serialize = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            CMergeDuplicate objCCustomer = new CMergeDuplicate();
            var result = false;
            var userid = WebSecurity.CurrentUserId;
            var MergeDate = DateTime.Now;
            //  objCVarCRM_Actions.ModificationDate = DateTime.Now;
            var exc = objCCustomer.MergeDuplicate(int.Parse(pItemType), pDuplicateItemsIDs, int.Parse(pDestinationItemID), userid, MergeDate ,( pSubAccountsIDs_ForDuplicateItems == null ? "-" : pSubAccountsIDs_ForDuplicateItems ), int.Parse(pSubAccountFor_DestinationItemID));


            if (exc == null)
                result = true;

            return new Object[] { serialize.Serialize(result) };
        }
        // data: { pItemType: ItemTypeNO, pDuplicateItems: DuplicateItems, pDestinationItem: $('#slDestinationItems').val() },
    }
}
