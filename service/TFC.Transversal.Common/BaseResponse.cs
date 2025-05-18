namespace Kintech.RestCA.Transversal.Common
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; } = false;
        public string? Message { get; set; }
    }
}