namespace Utility.Toolkit.Analysis
{
    /// <summary>
    /// 
    /// </summary>
    public class Symbol
    {
        /// <summary>
        /// end of file
        /// </summary>
        public static readonly Symbol EOF = new Symbol("END OF FILE", SymbolTypes.Operator);
        /// <summary>
        /// get symbol name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// get symbol type
        /// </summary>
        internal SymbolTypes Type { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public Symbol(string name, SymbolTypes type)
        {
            Name = name;
            Type = type;
        }
        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Name}:{Type}";
        }
    }
}
