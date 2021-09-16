namespace carportal.Models
{
    public class ServiceResponse<T>
    {

        public T Data { get; set; }

        public bool isSuccess { get; set; } = true;

        public string message { get; set; } = null;
        
    }
}