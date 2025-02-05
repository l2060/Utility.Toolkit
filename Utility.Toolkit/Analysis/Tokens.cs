

namespace Utility.Toolkit.Analysis
{
    /// <summary>
    /// token symbol
    /// </summary>
    public enum ValueType
    {
        /// <summary>
        /// token value type is string
        /// </summary>
        String,
        /// <summary>
        /// token value type is number
        /// </summary>
        Number,
        /// <summary>
        /// token value type is boolean
        /// </summary>
        Boolean,
        /// <summary>
        /// token value type is null
        /// </summary>
        Null,
    }

    /// <summary>
    /// value string boolean number null
    /// </summary>
    public class ValueToken : Token
    {
        internal ValueToken()
        {
        }

        /// <summary>
        /// get token value type
        /// </summary>
        public ValueType Type { get; protected set; }

        /// <summary>
        /// get token value
        /// </summary>
        /// <returns></returns>
        public virtual string ToValue()
        {
            return Value;
        }
    }

    /// <summary>
    /// boolean token
    /// </summary>
    public class BooleanToken : ValueToken
    {
        internal BooleanToken()
        {
            this.Type = ValueType.Boolean;
        }
    }


    /// <summary>
    /// end of file token
    /// </summary>
    public class EndOfFileToken : Token
    {
        internal EndOfFileToken()
        {
            Symbol = Symbol.EOF;
        }
    }


    /// <summary>
    /// identifier token
    /// </summary>
    public class IdentifierToken : Token
    {
        internal IdentifierToken()
        {
        }
    }

    /// <summary>
    /// keyword token
    /// </summary>
    public class KeywordToken : Token
    {
        internal KeywordToken()
        {
        }
    }

    /// <summary>
    /// null token
    /// </summary>
    public class NullToken : ValueToken
    {
        internal NullToken()
        {
            this.Type = ValueType.Null;
        }
    }

    /// <summary>
    /// number token
    /// </summary>
    public class NumberToken : ValueToken
    {
        internal NumberToken()
        {
            this.Type = ValueType.Number;
        }
    }

    /// <summary>
    /// punctuator token
    /// </summary>
    public class PunctuatorToken : Token
    {
        internal PunctuatorToken()
        {
        }
    }

    /// <summary>
    /// new line token
    /// </summary>
    public class NewLineToken : Token
    {
        internal NewLineToken()
        {

        }
    }



    /// <summary>
    /// operator token
    /// </summary>
    public class OperatorToken : PunctuatorToken
    {
        internal OperatorToken()
        {
        }
    }

    /// <summary>
    /// string token
    /// </summary>
    public class StringToken : ValueToken
    {
        internal StringToken()
        {
            this.Type = ValueType.String;
        }
        /// <inheritdoc/>

        public override string ToValue()
        {
            return $"'{this.Value.Replace("\r", "\\r").Replace("\n", "\\n")}'";
        }
    }





}
