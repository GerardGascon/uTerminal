namespace uTerminal.Samples
{
    public static class SayHello
    {
        [uCommand("hello", "say hello world exemple")]
        public static void Say()
        {
            Console.Log("Hello, world!");
        }
    }
}
