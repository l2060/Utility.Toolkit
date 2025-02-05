using System;

namespace Utility.Toolkit.Analysis.Rules
{
    /// <summary>
    /// White space rule.
    /// </summary>
    public class WhiteSpaceRule : ILexicalRules
    {
        /// <inheritdoc/>
        public RuleTestResult Test(in ReadOnlySpan<Char> codeSpan, in Int32 LineNumber, in Int32 ColumnNumber)
        {
            var result = new RuleTestResult();
            result.ColumnNumber = ColumnNumber;
            Int32 Index = 0;
            while (Char.IsWhiteSpace(codeSpan[Index]) && codeSpan[Index] != '\n')
            {
                Index++;
            }
            if (Index > 0)
            {
                result.ColumnNumber += Index;
                result.Length = Index;
                result.Value = codeSpan.Slice(0, Index).ToString();
                result.Type = TokenTyped.WhiteSpace;
                result.Success = true;
            }
            return result;
        }
    }
}
