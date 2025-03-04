using System.ComponentModel.DataAnnotations;

namespace Financas.Test.UnitTests.Abstractions
{
    public abstract class UnitTestBase
    {
        protected static List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, validationResults, true);
            return validationResults;
        }
    }
}
