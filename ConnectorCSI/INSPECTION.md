# CSI Connector Review

## Build Errors
 - None reported

## Build Warnings
- ```CS0162 Unreachable code detected```
  -
- ```CS0219 The variable $VARIABLE is assigned by its value is never used```
  - Appears in Convert2DProperty.cs and cPlugin.cs
  - ```Convert2DProperty.cs``` matProp, thickness, color, notes, GUID are not assigned to fields in CSIProperty2D, so they can't be returned
  - ```cPlugin.cs``` 
- ```CS0436 The type 'ConnectorBindingsCSI' in 'C:\...\ConnectorBindingsCSI.ClientOperations.cs' conflicts with the imported type 'ConnectorBindingsCSI' in 'DriverCSharp'... Using the type defined in 'C:\...\ConnectorBindingsCSI.ClientOperations.cs'```
  -
- ```CS0472 The result of the expression is always 'true' since a value of type '$VALUE' is never equal to 'null' of type '$VALUE```
  - 