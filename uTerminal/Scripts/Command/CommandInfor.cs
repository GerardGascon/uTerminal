namespace uTerminal
{
    public class CommandInfor
    {
        public readonly string path;
        public readonly string name;
        public readonly string description;

        public CommandInfor(string path, string name, string description)
        {
            this.path = path; 
            this.name = name;
            this.description = description;
        }
    }
}