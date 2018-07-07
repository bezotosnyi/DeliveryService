namespace DeliveryService.BLL.Validator
{
    public class ValidationResult
    {
        public ValidationResult(bool success, string errorMessage)
        {
            this.Success = success;
            this.ErrorMessage = errorMessage;
        }

        public bool Success { get; }

        public string ErrorMessage { get; }
    }
}