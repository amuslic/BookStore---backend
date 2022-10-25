using BookStore.Domain.User;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;

namespace BookStore.Application.User
{
    public class JsonPatchDocumentValidator : IJsonPatchDocumentValidator
    {
        private readonly List<string> RequiredFieldsNames = new() { "firstName", "lastName", "dateOfBirth" };
       
        public bool ValidateJsonPatchDocument(JsonPatchDocument<UpdateUserModel> jsonPatchDocument)
        {
            var validOperations = jsonPatchDocument.Operations
               .Where(o => o.OperationType != Microsoft.AspNetCore.JsonPatch.Operations.OperationType.Invalid);

            if(!validOperations.Any())
            {
                return false;
            }

            var deleteOperations = jsonPatchDocument.Operations
                .Where(o => o.OperationType == Microsoft.AspNetCore.JsonPatch.Operations.OperationType.Remove)
                .Select(o=>o.path[1..]);

            return !RequiredFieldsNames.Intersect(deleteOperations)
                          .Any();
        }
    }
}
