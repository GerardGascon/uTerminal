/*
 * SayHello Class
 * 
 * This script defines a static class with a single command to demonstrate the usage of uTerminal.
 * The command "hello" is associated with the method "Say," which logs "Hello, world!" to the uTerminal console.
 *  
 */

namespace uTerminal.Samples
{
    public static class SayHello
    {
        // Command attribute marks the method as a uTerminal command with a specific name and description
        [uCommand("hello", "say hello world example")]
        public static void Say()
        {
            // Log a greeting message to the uTerminal console
            uTerminalDebug.Log("Hello, world!");
        }
    }
}