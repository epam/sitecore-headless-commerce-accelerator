#addin "Cake.FileHelpers"
#addin "nuget:?package=TypeLite.Lib"
#addin "nuget:?package=Newtonsoft.Json&version=9.0.1"

using System;
using System.Linq;
using System.Reflection;
using System.Text;

using Newtonsoft.Json.Serialization;

using TypeLite;
using TypeLite.Net4;
using TypeLite.TsModels;

Func<TsGenerator> getGenerator = () => {
    var generator = new TsGenerator();

    generator.SetMemberTypeFormatter((tsProperty, memberTypeName) =>
    {
        // Handling IEnumerable<KeyPairValue<TKey, TValue>>
        if (tsProperty.PropertyType is TsCollection propertyType)
        {
            if (propertyType.ItemsType is TsClass classItemTypes)
            {
                if (classItemTypes.Name == "KeyValuePair" && classItemTypes.GenericArguments.Count == 2)
                {
                    var key = generator.GetFullyQualifiedTypeName(classItemTypes.GenericArguments[0]);
                    var value = generator.GetFullyQualifiedTypeName(classItemTypes.GenericArguments[1]);

                    if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                    {
                        return $"{{ [key: {key}]: {value} }}";
                    }
                }
            }
        }

        // Default behavior for the rest cases
        return generator.DefaultMemberTypeFormatter(tsProperty, memberTypeName);
    });

    return generator;
};

Action<FilePath, FilePath, FilePath> generateTypeScript = (assemblyName, targetFile, templateFile) =>
{
    try
    {
        var assembly = Assembly.LoadFrom(assemblyName.FullPath);

        var ts = TypeScript.Definitions(getGenerator())
            .For(assembly)
            .WithMemberFormatter(x => new CamelCasePropertyNamesContractResolver().GetResolvedPropertyName(x.Name))
            .WithIndentation(new string(' ', 2))
            .WithModuleNameFormatter(module => string.Empty)
            .WithVisibility((tsClass, typeName) => true);

        string typeScriptCode = ts.Generate();

        CopyFile(templateFile, targetFile);

        FileAppendText(targetFile, typeScriptCode);
    }
    catch(ReflectionTypeLoadException loadException)
    {
        foreach(var e in loadException.LoaderExceptions)
        {
            Error(e.Message);
        }
        throw;
	}
};