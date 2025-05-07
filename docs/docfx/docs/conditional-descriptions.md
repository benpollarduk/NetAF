# Conditional Descriptions

## Overview
Normally assets are assigned a **Description** during the constructor. This is what is returned when the asset is examined.

Descriptions are usually specified as a string.

```csharp
var item = new Item("The items name", "The items description.");
```

They can also be specified as a **Description**.

```csharp
var item = new Item(new Identifier("The items name"), new Description("The items description."));
```

However, sometimes it may be desirable to have a conditional description that can change based on the state of the asset.

Conditional descriptions can be specified with **ConditionalDescription** and contain a lambda which determines which one of two strings are returned when the asset is examined.

```csharp
// the player, just for demo purposes
var player = new PlayableCharacter("Ben", "A man.");

// the description to use when the condition is true
var trueString = "A gleaming sword, owned by Ben.";

// the string to use when the condition is false
var falseString = "A gleaming sword, without an owner.";

// a lambda that determines which string is returned
Condition condition = () => player.FindItem("Sword", out _);

// the conditional description itself
var conditionalDescription = new ConditionalDescription(trueString, falseString, condition);

// create the item with the conditional description
var sword = new Item(new Identifier("Sword"), conditionalDescription);
```

For more complicated scenarios the **MultiConditionalDescription** can be used to return a description for multiple different conditions.

```csharp
// the player, just for demo purposes
var player = new PlayableCharacter("Ben", "A man.");

// the description to use when no condition is true
var fallbackString = "No condition is true.";

// the first described condition when
var firstCondition = new DescribedCondition(() => player.Name == "Ben", "Condition 1 is true.");

// the second described condition when
var secondCondition = new DescribedCondition(() => player.Name == "Dave", "Condition 2 is true.");

// the multi conditional description itself
var multiConditionalDescription = new MultiConditionalDescription(fallbackString, firstCondition, secondCondition);

// create the item with the conditional description
var passport = new Item(new Identifier("Passport"), multiConditionalDescription);
```
