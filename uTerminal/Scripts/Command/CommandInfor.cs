namespace uTerminal
{
    public class CommandInfor
    {
        public readonly string path; 
        public readonly string description;

        public CommandInfor(string path, string description)
        {
            this.path = path;  
            this.description = description;
        }
    }
}