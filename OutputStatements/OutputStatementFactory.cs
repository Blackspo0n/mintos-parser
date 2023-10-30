using System.Xml.Schema;
using MintosParser.StatementTypes;
using NLog;

namespace MintosParser.OutputStatementTypes {
    public class OutputStatementFactory {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static IOutputStatementType? Create(IStatementType statement) {

            Type? type = Type.GetType($"MintosParser.OutputStatementTypes.{statement.outputType}OutputStatementType");
            if(type != null){
                try {
                return (IOutputStatementType?)Activator.CreateInstance(type);
            }
            catch(Exception err) {
                logger.Info("Output Statement for " + statement.outputType + " does not exists! Skipping ...", err);
            }
            }
            return null;
        }

    }
}