# uTerminal: A Streamlined In-Game Console for Unity

uTerminal is a sophisticated and highly efficient in-game console specifically designed for invoking C# methods and displaying output from the Unity Debug class during game runtime.

## Usage

1. Copy the contents of [uTerminal](./uTerminal) to your Unity project Assets folder.

2. Call the initialization method in your C# code:
```csharp
uTerminal.ConsoleManager.Instance.Initialize(true);
```
3. Press F1 to open console UI Type `uterminal.help` in the console to view a list of all available commands. Utilize the up and down arrow keys to navigate through the command history, and employ the tab key for command autocompletion


## Registering Commands

There are 2 options to register commands to be used in the uTerminal.

### 1. Using the uTerminal attribute:
The uTerminal attribute can be either static (public or non-public) or non-static (public or non-public). This means it is compatible with both static and non-static methods. However, please note that, currently, it is not available for objects of type ScriptableObject.

```csharp
 [uTerminal("damage", "inflict damage on the player")]
 public void TakeDamage(int damage)
 {
    Health -= damage;
 
    if (Health <= 0)
    {
        Die();
    }
}
``` 
 
### 2. Manually adding Commands:

`AddCommand` can be used as static (public or non-public) or non-static (public or non-public). This means it supports static and non-static methods and ScriptableObject.
 
```csharp
private void Start()
{ 
    Terminal.AddCommand("damage", "player.damage", "inflict damage on the player", TakeDamage);
}

public void TakeDamage(object[] args)
{
    int damage = args[0].ToInt();

    Health -= damage;

    if (Health <= 0)
    {
        Die();
    }
}
```

To use the `AddCommand` manually, it is necessary to use the object array, but don't worry, you can convert it to int, string, float, vector2, and vector3 through an extension that is already in the project, as in the example above." 

### 3. Multiple MonoBehaviour support
Built-in support for multiple MonoBehaviours. This feature allows users to easily integrate and manage different MonoBehaviour components within a single asset.

![Alt text](https://i.imgur.com/pqqQd2k.gif)

### 3. Unity Logs

You can enable unity logs using the am code below

```csharp
uTerminalGraphics.ShowUnityLogs = false;
```
or you can enable directly in the console using

```
uterminal.logs.show
```

### 4. Chat

You can also execute your commands directly from your game chat

```csharp
void Send(string chat)
{
    //commands was not found
    if(!Terminal.ChatExecuteCommand(chat))
    {
        //send message to other server and server send to clients
    }
    else
    {
        //command found
    }
}
```


Thank you for using uTerminal :)
