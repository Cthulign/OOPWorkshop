﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using EZVet.Filters;
using EZVet.QueryProcessors;

namespace EZVet.Controllers
{
    public class ComplaintsController : ApiController
    {
        private readonly IComplaintsQueryProcessor _complaintsQueryProcessor;

        public ComplaintsController(IComplaintsQueryProcessor ComplaintsQueryProcessor)
        {
            _complaintsQueryProcessor = ComplaintsQueryProcessor;
        }

        [HttpGet]
        [Route("api/complaints/search")]
        [Authorize(Roles = Consts.Roles.Admin + "," + Consts.Roles.Owner + "," + Consts.Roles.Doctor)]
        public List<DTOs.Complaint> Search(int? customerId = null)
        {
            return _complaintsQueryProcessor.Search(customerId, null, null, null).OrderByDescending(x => x.Date).ToList();
        }

        [HttpGet]
        [Authorize(Roles = Consts.Roles.Admin + "," + Consts.Roles.Owner + "," + Consts.Roles.Doctor)]
        public DTOs.Complaint Get(int id)
        {
            return _complaintsQueryProcessor.GetComplaint(id);
        }

        [HttpPost]
        [TransactionFilter]
        [Authorize(Roles = Consts.Roles.Doctor)]
        public DTOs.Complaint Save([FromBody]DTOs.Complaint complaint)
        {
            var currPrincipal = HttpContext.Current.User as ClaimsPrincipal;
            var currIdentity = currPrincipal.Identity as BasicAuthenticationIdentity;
            int userId = currIdentity.UserId;

            
            return _complaintsQueryProcessor.Save(complaint);
        }

        //[HttpPut]
        //[TransactionFilter]
        //public DTOs.Complaint Update([FromUri]int id, [FromBody]DTOs.Complaint Complaint)
        //{
        //    return _complaintsQueryProcessor.Update(id, Complaint);
        //}
    }
}