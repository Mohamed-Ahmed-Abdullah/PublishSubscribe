namespace Domain.Models
{
    public class PullCriteria
    {

        /// <summary>
        /// subscriber will receive messages if this tags appear in the message, if null or empty mean get everything
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// if the title contains the string the message will be pulled, if null or empty mean get everything
        /// </summary>
        public string TitleContains { get; set; }

        /// <summary>
        /// if the content contains this string the message will be pulled, if null or empty mean get everything
        /// </summary>
        public string ContentContains { get; set; }

        ///// <summary>
        ///// if the title or the content match this regular expression
        ///// </summary>
        //public string RegularExpression { get; set; }
    }
}