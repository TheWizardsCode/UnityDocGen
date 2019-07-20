A simple linter for Unity projects. At present it simply looks for serialized fields that do not have a ToolTip and reports them in the log console as warnings.

Note this is not, nor will it ever be, a complete linter.

# Use

  1. Import the linter code into your project
  2. `Window -> Wizards Code -> Linter`
  3. Drag an object from your project that contains one of your MonoBehaviours into 'Object Containing MonoBehavior' field
  4. [OPTIONAL] Add a REGEX to filter the results reported
  5. Click `Analyze`
  6. Review any warnings in the Debug Log

