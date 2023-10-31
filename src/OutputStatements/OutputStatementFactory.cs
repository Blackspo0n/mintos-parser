using System.Xml.Schema;
using MintosParser.StatementTypes;

namespace MintosParser.OutputStatementTypes {
    public class OutputStatementFactory {
        public static IOutputStatementType? Create(IStatementType statement) {
            Type? type = Type.GetType($"MintosParser.OutputStatementTypes.{statement.outputType}OutputStatementType");
            if(type != null) {
                try {
                    return (IOutputStatementType?)Activator.CreateInstance(type);
                }
                catch(Exception err) {
                    Console.WriteLine("Output Statement for " + statement.outputType + " cannot be created! Skipping ...", err);
                }
            }

            return null;
        }
    }
}