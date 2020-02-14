namespace Common.API
{
    /// <summary>
    /// The result returns to client from api
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// Default error message
        /// </summary>
        const string DEFAULT_ERROR_MESSAGE = "internal server error";

        /// <summary>
        /// True when error occured
        /// </summary>
        public bool Error { get; private set; }

        /// <summary>
        /// The message describes the error
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Used to set error
        /// </summary>
        /// <param name="errorMessage"></param>
        public void SetError(string errorMessage = DEFAULT_ERROR_MESSAGE)
        {
            Error = true;
            ErrorMessage = errorMessage;
        }
    }

    /// <summary>
    /// The result returns to client from api include data
    /// </summary>
    public class ApiResult<T> : ApiResult
    {
        /// <summary>
        /// The data attached to result
        /// </summary>
        public T Data { get; set; }
    }
}
