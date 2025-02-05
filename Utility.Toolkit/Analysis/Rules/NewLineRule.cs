using System;


namespace Utility.Toolkit.Analysis.Rules
{
    /// <summary>
    /// New line rule.
    /// </summary>
    public class NewLineRule : ILexicalRules
    {
        /// <inheritdoc/>
        public RuleTestResult Test(in ReadOnlySpan<Char> codeSpan, in Int32 LineNumber, in Int32 ColumnNumber)
        {
            var result = new RuleTestResult();
            if (codeSpan[0] == '\n')
            {
                result.Value = "\n";
                result.LineCount = 1;
                result.ColumnNumber = 0;
                result.Length = 1;
                result.Type = TokenTyped.NewLine;
                result.Success = true;
            }
            return result;
        }
    }
}
