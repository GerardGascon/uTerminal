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
uTerminal.Shell.AddCommand("damage", "player.damage", "inflict damage on the player", new Action<int>(TakeDamage));
```

Thank you for using uTerminal :)
