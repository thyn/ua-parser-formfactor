namespace UAParser.FormFactor
{
	/// <summary>
	/// Options available for the parser
	/// </summary>
	public class ParserOptions
	{
#if REGEX_COMPILATION
        /// <summary>
        /// If true, will use compiled regular expressions for slower startup time
        /// but higher throughput
        /// </summary>
        public bool UseCompiledRegex { get; set; }
#endif
	}
}