using MintosParser.StatementTypes;

namespace MintosParser.OutputStatementTypes {
    public class OutputStatementFactory {
        public static IOutputStatementType? Create(IStatementType statement) {
            Type? type = Type.GetType($"MintosParser.OutputStatementTypes.{statement.OutputType}OutputStatementType");
            if(type != null) {
                try {
                    return (IOutputStatementType?)Activator.CreateInstance(type);
                }
                catch(Exception err) {
                    Console.WriteLine("Output Statement for " + statement.OutputType + " cannot be created! Skipping ...", err);
                }
            }

            return null;
        }
    }
}