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

###### ConfigAccessor
```csharp
* ConfigAccessor.LoadConfig<T>(path, filename);
* ConfigAccessor.LoadConfig<T>(filename); //default path: Configurations\
```
Example
```cssharp
var config = ConfigAccessor.LoadConfig<Config>(@"..\..\App_Data", "Config.config");
```


## Commom

###### Object Extenstion
ToInt
* string.ToInt(defualt = 0)
```cssharp
var value = "26".ToInt(); // 26
var value2 = "a".ToInt(); // 0
var value3 = "a".ToInt(-1); // -1
```
