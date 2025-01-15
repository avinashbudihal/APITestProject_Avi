namespace APITestProject_Avi.DTOs
{
    public class ResponseErrorMessage
    {
        #region Properties
        public string? type { get; set; }
        public string? title { get; set; }
        public int status { get; set; }
        public Errors? errors { get; set; }
        public string? traceId { get; set; }
        #endregion
    }
    public class Errors
    {
        #region Properties
        public List<string>? depositRequest { get; set; }
        public List<string> Amount { get; set; }
        #endregion
    }
}
