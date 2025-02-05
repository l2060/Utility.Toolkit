

using System;

namespace Utility.Toolkit.Analysis
{
    /// <summary>
    /// Token typed.
    /// </summary>
    public enum TokenTyped
    {
        /// <summary>
        /// The string.
        /// </summary>
        String,
        /// <summary>
        /// The number.
        /// </summary>
        Number,
        /// <summary>
        /// The identifier.
        /// </summary>
        Identifier,
        /// <summary>
        /// The keyword.
        /// </summary>
        Punctuator,
        /// <summary>
        /// The comment.
        /// </summary>
        Comment,
        /// <summary>
        /// The new line.
        /// </summary>
        NewLine,
        /// <summary>
        /// The white space.
        /// </summary>
        WhiteSpace
    }
    /// <summary>
    /// Rule test result.
    /// </summary>
    public struct RuleTestResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Utility.Toolkit.Analysis.RuleTestResult"/> success.
        /// </summary>
        public Boolean Success;

        /// <summary>
        /// Gets or sets the line count.
        /// </summary>
        public Int32 LineCount;
        /// <summary>
        /// Gets or sets the column number.
        /// </summary>
        public Int32 ColumnNumber;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public String Value;

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        public Int32 Length;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public TokenTyped Type;
    }

    /// <summary>
    /// Lexical rules.
    /// </summary>
    public interface ILexicalRules
    {

        /// <summary>
        /// Test the specified codeSpan, LineNumber and ColumnNumber.
        /// </summary>
        /// <param name="codeSpan"></param>
        /// <param name="LineNumber"></param>
        /// <param name="ColumnNumber"></param>
        /// <returns></returns>
        abstract RuleTestResult Test(in ReadOnlySpan<Char> codeSpan, in Int32 LineNumber, in Int32 ColumnNumber);

    }

}
