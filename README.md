# Editor Documentation generator for Unity

Generate editor documentation for MonoBehaviours and ScriptableObjects from `[Tooltip]` and other field attributes.

# Output

The system outputs a relatively simple markdown file documenting all fields that are serialized by the Editor, for example:

```
# WizardsCode.Tools.Editor.DocGen.ExampleMonoBehaviour


## Public String (String)
This is a public string field with a tooltip (you are reading it now).


## Float Field (Single)
Field with a range.

Value must be between 0 and 1.5.


## Public But Undocumented String (String)
No tooltip provided.


## Private Serialized String (String)
This is a private field, but it has the SerializeField attribute. This text comes from the tooltip for the field.
```

# Use

  1. Import the DocGen code into your project
  2. `Window -> Wizards Code -> Documentation Generator`
  3. Drag an object from your project that contains one of your MonoBehaviours into 'Object Containing MonoBehavior' field
  4. [OPTIONAL] Add a REGEX to filter the files to be processed
  5. Click `Generate`
  6. Review any warnings in the Debug Log - this will tell you of any fields that are not documented yet
  7, Review the documentation generated in the directory set in the editor window (defaults to `Assets/Documentation/Generated`)

