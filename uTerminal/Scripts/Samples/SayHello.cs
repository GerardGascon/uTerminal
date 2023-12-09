namespace uTerminal.Samples
{
    public static class SayHello
    {
        [uTerminal("hello", "say hello world exemple")]
        public static void Say()
        {
            Console.Log("Hello, world!");
        }
    }
}
