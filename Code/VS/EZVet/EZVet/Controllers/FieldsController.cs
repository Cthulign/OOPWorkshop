﻿using System.Collections.Generic;
using System.Web.Http;
using EZVet.Filters;
using EZVet.QueryProcessors;

namespace EZVet.Controllers
{
    public class FieldsController : ApiController
    {
        private readonly IFieldsQueryProcessor _fieldsQueryProcessor;

        public FieldsController(IFieldsQueryProcessor fieldsQueryProcessor)
        {
            _fieldsQueryProcessor = fieldsQueryProcessor;
        }

        [HttpGet]
        [Authorize(Roles = Consts.Roles.Admin + "," + Consts.Roles.Owner + "," + Consts.Roles.Doctor)]
        public IEnumerable<DTOs.Field> Search(int? fieldId = null, string fieldName = null, int? type = null)
        {
            return _fieldsQueryProcessor.Search(fieldId, fieldName, type);
        }

        [HttpGet]
        [Authorize(Roles = Consts.Roles.Admin + "," + Consts.Roles.Owner + "," + Consts.Roles.Doctor)]
        public DTOs.Field Get(int id)
        {
            return _fieldsQueryProcessor.GetField(id);
        }

        [HttpPost]
        [TransactionFilter]
        [Authorize(Roles = Consts.Roles.Admin + "," + Consts.Roles.Owner)]
        public DTOs.Field Save([FromBody]DTOs.Field field)
        {
            return _fieldsQueryProcessor.Save(field);
        }

        [HttpPut]
        [TransactionFilter]
        [Authorize(Roles = Consts.Roles.Admin + "," + Consts.Roles.Owner)]
        public DTOs.Field Update([FromUri]int id, [FromBody]DTOs.Field field)
        {
            return _fieldsQueryProcessor.Update(id, field);
        }

        [HttpDelete]
        [TransactionFilter]
        [Authorize(Roles = Consts.Roles.Admin + "," + Consts.Roles.Owner)]
        public int Delete([FromUri]int id)
        {
            _fieldsQueryProcessor.Delete(id);

            return 1;
        }
    }
}
