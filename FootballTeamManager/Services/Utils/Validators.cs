using System.ComponentModel.DataAnnotations;

namespace FootballTeamManager.Services.Utils
{
    public static class Validators
    {
        public static bool IsValidModel(object model)
        {
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(model, validationContext, validationResults, true);
        }
    }
}
