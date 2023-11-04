namespace SHIBANK.Results
{
    public class Result
    {
        public Result(bool success, string information) 
        { 
            Success = success;
            Information = information;
        }

        public bool Success { get; }

        public string Information { get;}
    }
}
