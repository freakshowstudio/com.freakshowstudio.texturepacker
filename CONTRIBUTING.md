# Contributing

- [Issues, bugs, feature requests, questions or problems](#issue)
- [Code Style Guide](#style)
- [Commit Message Guidelines](#commit)

## <a name="issue"></a> Issues, bugs, feature requests, questions or problems

Please use GitHub issues for contacting us regarding this package.

## <a name="style"></a> Code Style Guide

### Naming Guidelines

#### **Namespaces**

Namespaces are all **PascalCase**, multiple words concatenated together,
without hyphens ( - ) or underscores ( _ ).

**AVOID**:

```csharp
com.freakshowstudio.core.runtime
```

**PREFER**:

```csharp
FreakshowStudio.Core.Runtime
```

#### **Classes & Interfaces**

Classes and interfaces are written in **PascalCase**.
For example `RadialSlider`.

#### **Methods**

Methods are written in **PascalCase**.
For example `DoSomething()`.

#### **Fields**

All public, protected and internal non-static fields
are written in **PascalCase**.

Private non-static fields are written with a leading underscore
in _**camelCase**, including serialized fields.

```csharp
public class MyClass
{ 
    public int PublicField; 
    internal int InternalField;
    protected int ProtectedField;
    private int _privateField;

    [SerializeField]
    private int _privateSerializedField;
}
```

Static fields follow the same naming rules

```csharp
public static int PublicStatic;
private static int _privateStatic;
```

#### **Properties**

All properties are written in **PascalCase**.

```csharp
public int PageNumber
{ 
    get 
    { 
        return _pageNumber; 
    } 
    set 
    {
        _pageNumber = value; 
    }
}
```

#### **Parameters**

Parameters are written in **camelCase**.

```csharp
public void DoSomething(int someValue) {}
```

#### **Actions**

Actions are written in **PascalCase**. Avoid prefixing event fields with
**On**, reserve this for handlers.

```csharp
public event Action<int> ValueChanged;

private void Subscribe()
{
    ValueChanged += OnValueChanged;
}
```

#### **Async/Await**

When declaring async methods, always prefix them with `Async`.

```csharp
public async Task DoSomethingAsync() { }
```

#### **Misc**

Single character names are to be avoided except for temporary
looping variables, or where it makes sense to use an abbreviation for brevity.

In code, acronyms should be treated as words. For example:

**AVOID:**

```csharp
XMLHTTPRequest
String URL
FindPostByID
```

**PREFER:**

```csharp
XmlHttpRequest
String _url
FindPostById
```

### **Declarations**

#### **Access Level Modifiers**

Access level modifiers should be explicitly defined everywhere.

#### **Fields & Variables**

Prefer a single declaration per line.

**AVOID:**

```csharp
string username, twitterHandle;
```

**PREFER:**

```csharp
string username;
string twitterHandle;
```

Where possible, use **var**, except for built-in types where it is optional.

**AVOID:**

```csharp
List<int> someList = new List<int>();
```

**PREFER:**

```csharp
var someList = new List<int>();
var someInt = 42;
int otherInt = 42; // This is also OK
```

##### **Serialized Fields**

In general, serialized fields should be declared as **private**
or **internal**, and have the **SerializeField** attribute.

If public access to the variable is required, prefer a **property** instead.

**AVOID:**

```csharp
public Type Variable;
```

**PREFER:**

```csharp
[SerializeField]
private Type _variable;

[field: SerializeField]
public Type OtherVariable { get; private set; }
```


#### **Classes**

Exactly one class per source file, although inner classes are
encouraged where scoping is appropriate.

The default access level for classes should be **internal**.
Only use **public** when the class is intended to be used outside the assembly.

Classes should be declared as **sealed**, and only opened up for
extending when needed.

#### **Interfaces**

All interfaces should be prefaced with the letter **I**.

**AVOID:**

```csharp
interface RadialSlider {}
```

**PREFER:**

```csharp
interface IRadialSlider {}
```

### **Spacing**

#### **Indentation**

Indentation should be done using **spaces** — never tabs.

#### **Blocks**

Indentation for blocks uses **4 spaces** for optimal readability:

**AVOID:**

```csharp
for (int i = 0; i < 10; i++)
{ 
  Debug.Log("index=" + i);
}
```

**PREFER:**

```csharp
for (int i = 0; i < 10; i++)
{ 
    Debug.Log("index=" + i);
}
```

#### **Line Wraps**

Indentation for line wraps should also use **4 spaces**

**AVOID:**

```csharp
CoolUiWidget widget = 
        someIncrediblyLongExpression(that, reallyWouldNotFit, on, aSingle, line);
```

**PREFER:**

```csharp
CoolUiWidget widget = 
    someIncrediblyLongExpression(that, reallyWouldNotFit, on, aSingle, line);
```

Methods with a long parameter list should be split with one
parameter per line, except in cases where the grouping of
variables makes sense

```csharp
var result = LongFunction(
    with,
    some,
    parameters,
    relatedA, relatedB);

// ...

int LongFunction(
    int with,
    int some,
    int parameters,
    int relatedA, int relatedB)
{}
```

Methods that use fluent syntax, or other call chains, should be
separated with a line break **before** the period symbol

```csharp
var result = TheObject
    .DoSomething()
    .Where(x => x.isSomething)
    .Finally();
```

#### **Line Length**

Lines must be no longer than **80** characters long, this is a hard limit,
the only exception is links that will not fit.

#### **Vertical Spacing**

There should be exactly one blank line between methods to aid in visual
clarity and organization. Whitespace within methods should separate
functionality, but having too many sections in a method often means
you should refactor into several methods.

One blank line should be used to sort imports into logical groups.

There should be two blank lines between imports and the start of the code,
and two blank lines separating regions defined in the code.

### **Brace Style**

All braces get their own line.

**AVOID:**

```csharp
class MyClass { 
    void DoSomething() { 
        if (someTest) { 
            // ... 
        } else { 
            // ... 
        } 
    }
}
```

**PREFER:**

```csharp
class MyClass
{ 
    void DoSomething() 
    { 
        if (someTest) 
        { 
            // ... 
        } 
        else 
        { 
            // ... 
        } 
    }
}
```

Conditional statements should as a general rule be enclosed with braces,
irrespective of the number of lines required, but in some cases it can
be acceptable to use just a single line.

**AVOID:**

```csharp
if (someTest) doSomething();
```

**PREFER:**

```csharp
if (someTest)
{ 
    DoSomething();
}

// Keep it on one line if it improves readability of the code in general
if (condition) continue; 
```

### **Ternary Operators**

Ternary operators can be inline for short expressions
but should be placed on separate lines for longer expressions.

When splitting a ternary over multiple lines, the `?` and `:`
symbols should be placed on their own line.

**AVOID:**

```csharp
var a = b
    ? c
    : d;

var theResultOfTheTernary = someLongCondition ? anExpressionThatIsLong : anotherExpressionThatIsReallyLong;
```

**PREFER:**

```csharp
var a = b ? c : d;

var theResultOfTheTernary = someLongCondition 
    ? anExpressionThatIsLong 
    : anotherExpressionThatIsReallyLong;
```

### **Switch Statements**

Switch-statements come with `default` case by default. If the `default`
case is never reached, it should be removed.

**AVOID:**

```csharp
switch (variable)
{ 
    case 1: 
        break; 
    case 2: 
        break; 
    default: 
        break;
}
```

**PREFER:**

```csharp
switch (variable)
{ 
    case 1: 
        break; 
    case 2: 
        break;
}
```

### **Attributes**

Attributes should be placed on their own line and should be placed above
the declaration. Prefer one attribute per line.

**AVOID:**

```csharp
[SerializeField] private Type _variable;

[SerializeField, Range(0, 10), Tooltip("The value of this variable")]
private int _otherVariable;
```

**PREFER:**

```csharp
[SerializeField] 
private Type _variable;

[SerializeField] 
[Range(0, 10)]
[Tooltip("The value of this variable")]
private int _otherVariable;
```

### **Tests**

Tests should be placed in a separate assembly and should be placed in a
separate folder. There should be one assembly/folder for **Editor** tests,
and one for **Runtime** tests.

To make internals of a class visible for a test assembly,
specify the `InternalsVisibleTo` attribute in `AssemblyInfo.cs`.

Classes that contain tests should be prefixed with the word **Test**.
Tests should be logically grouped into classes.

Each test method should follow the naming ``What_How_ExpectedResult``.
This way it is clear what we are testing and what we expect
the result to be.

**AVOID:**

```csharp
[Test]
public void ConstructThing() {}

[Test]
public void ConstructThingBadly() {}
```

**PREFER:**

```csharp
[Test]
public void Thing_Construct_ShouldSucceed() {}

[Test]
public void Thing_ConstructError_ShouldThrow() {}
```

### **Organization**

For a single class, all inspector variables should be grouped together. All other variables
should be grouped together separately, as well as properties. Unity callback methods,
public methods and private methods should also be grouped together, as well as
implementations of interfaces. Use `#region` where it makes the code clearer.

Code should be logically organized into separate assemblies, using assembly definition
files in Unity. Each assembly should have an `AssemblyInfo.cs` file describing it, and
a `csc.rsp` file with compiler flags (typically `-nullable` and `-warnaserror`).

Assemblies are organized under the top folders `Editor`, `Runtime` and `Tests`.
Additionally, shaders are organized under the `Shaders` folder.

Use the `internal` access modifier in
conjunction with `InternalsVisibleTo` to make it easier for the editor code to access
the runtime code, and for the tests to access the runtime and editor code.

```
 FreakshowStudio/
 ├─ Editor/
 |  ├─ Core/
 │  |  ├─ AssemblyInfo.cs
 │  |  ├─ csc.rsp
 │  |  ├─ FreakshowStudio.Editor.Core.asmdef
 ├─ Runtime/
 |  ├─ Core/
 │  |  ├─ AssemblyInfo.cs
 │  |  ├─ csc.rsp
 │  |  ├─ FreakshowStudio.Runtime.Core.asmdef
 ├─ Tests/
 │  ├─ Editor/
 |  |  ├─ Core/
 │  │  |  ├─ AssemblyInfo.cs
 │  │  |  ├─ csc.rsp
 │  │  |  ├─ FreakshowStudio.Tests.Editor.Core.asmdef
 │  ├─ Runtime/
 |  |  ├─ Core/
 │  │  |  ├─ AssemblyInfo.cs
 │  │  |  ├─ csc.rsp
 │  │  |  ├─ FreakshowStudio.Tests.Runtime.Core.asmdef
```

### **Language features**

All code should use the nullable feature.

### **Language**

Use US English spelling.

**AVOID:**

```csharp
string colour = "red";
```

**PREFER:**

```csharp
string color = "red";
```

The exception here is `MonoBehaviour` as that's what the class is actually called.

## <a name="commit"></a> Commit Message Guidelines

Each commit message consists of a **header**, a **body**, and a **footer**.


```
<header>
<BLANK LINE>
<body>
<BLANK LINE>
<footer>
```

The `header` is mandatory and must conform to the
[Commit Message Header](#commit-header) format.

The `body` is optional.
When the body is present, it must be at least 20 characters long and must
conform to the [Commit Message Body](#commit-body) format.

The `footer` is optional. The [Commit Message Footer](#commit-footer)
format describes what the footer is used for and the structure it must have.

## <a name="commit-header"></a>Commit Message Header

```
<type>(<scope>): <short summary>
  │       │             │
  │       │             └─⫸ Summary in present tense. Not capitalized. No period at the end.
  │       │
  │       └─⫸ Commit Scope
  │
  └─⫸ Commit Type: build|ci|docs|feat|fix|perf|refactor|test
```

The `<type>` and `<summary>` fields are mandatory, the `(<scope>)` field is optional.


### Type

Must be one of the following:

| Type         | Description                                                   |
|--------------|---------------------------------------------------------------|
| **build**    | Changes that affect the build system or external dependencies |
| **ci**       | Changes to our CI configuration files and scripts             |
| **docs**     | Documentation only changes                                    |
| **feat**     | A new feature                                                 |
| **fix**      | A bug fix                                                     |
| **perf**     | A code change that improves performance                       |
| **refactor** | A code change that neither fixes a bug nor adds a feature     |
| **test**     | Adding missing tests or correcting existing tests             |

### Summary

Use the summary field to provide a succinct description of the change:

* use the imperative, present tense: "change" not "changed" nor "changes"
* don't capitalize the first letter
* no dot (.) at the end

## <a name="commit-body"></a>Commit Message Body

Just as in the summary, use the imperative, present tense:
"fix" not "fixed" nor "fixes."

Explain the motivation for the change in the commit message body.
This commit message should explain _why_ you are making the change.
You can include a comparison of the previous behavior with the new
behavior to illustrate the impact of the change.


## <a name="commit-footer"></a>Commit Message Footer

The footer can contain information about breaking changes and
deprecations and is also the place to reference issues and other
PRs that this commit closes or is related to.

For example:

```
BREAKING CHANGE: <breaking change summary>
<BLANK LINE>
<breaking change description + migration instructions>
<BLANK LINE>
<BLANK LINE>
Fixes #<issue number>
```

or

```
DEPRECATED: <what is deprecated>
<BLANK LINE>
<deprecation description + recommended update path>
<BLANK LINE>
<BLANK LINE>
Closes #<pr number>
```

Breaking Change section should start with the phrase `BREAKING CHANGE: `
followed by a *brief* summary of the breaking change, a blank line, and a
detailed description of the breaking change that also includes
migration instructions.

Similarly, a Deprecation section should start with `DEPRECATED: `
followed by a short description of what is deprecated, a blank line
and a detailed description of the deprecation that also mentions the
recommended update path.

## Revert commits

If the commit reverts a previous commit, it should begin with `revert: `,
followed by the header of the reverted commit.

The content of the commit message body should contain:

- information about the SHA of the commit being reverted in the following format: `This reverts commit <SHA>`,
- a clear description of the reason for reverting the commit message.
