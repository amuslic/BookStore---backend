using BookStore.Domain.User;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.User
{
    //todo - make it generic, accepting T

    public interface IJsonPatchDocumentValidator
    {
        public bool ValidateJsonPatchDocument(JsonPatchDocument<UpdateUserModel> jsonPatchDocument);
    }
}
