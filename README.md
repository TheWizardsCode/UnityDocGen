# Editor Documentation generator for Unity

Generate editor documentation for MonoBehaviours and ScriptableObjects from `[Tooltip]` and other field attributes.

# Use

  1. Import the DocGen code into your project
  2. `Window -> Wizards Code -> Documentation Generator`
  3. Drag an object from your project that contains one of your MonoBehaviours into 'Object Containing MonoBehavior' field
  4. [OPTIONAL] Add a REGEX to filter the files to be processed
  5. Click `Generate`
  6. Review any warnings in the Debug Log - this will tell you of any fields that are not documented yet
  7, Review the documentation generated in the directory set in the editor window (defaults to `Assets/Documentation/Generated`)

