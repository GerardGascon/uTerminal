namespace uTerminal
{
    public class TerminalCommand
    {
        public readonly CommandInfor infor;
        public readonly Context context;

        public TerminalCommand(CommandInfor infor, Context context)
        {
            this.infor = infor;
            this.context = context;
        }
    }
}