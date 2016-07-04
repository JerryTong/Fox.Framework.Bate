# Fox.Framework.Bate
> Happy for Coding and Work to be easy.

## Getting Started
Fox.Framework.Bate is an experimental library that uses of C#. 
You can download it and reference Fox.Framework.Bate.dll in your solution.


## DataAccessor
Write and Read. Include .xml, database ... 

###### XmlDataAccessor
```csharp
* IEnumerable<T> LoadCollection<T>(string filePath)
* IEnumerable<T> LoadCollection<T>(string filePath, string fileName)
* T LoadXml<T>(string filePath)
* T LoadXml<T>(string filePath, string fileName)
```
Example
```cssharp
var model = XmlDataAccessor.LoadXml<T>(path, filename);
var modelCollection = XmlDataAccessor.LoadCollection<T>(path, filename);
```
