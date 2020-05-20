"use strict";

const parseXml = require("xml-parser");
const path = require("path");
const helpers = require("./internal");

const parseCodeFile = (node) => {
  const fileName = node.attributes.Include;

  return {
    fileName: helpers.normalizePath(fileName),
  };
};

const parseAssemblyReference = (node) => {
  const parts = node.attributes.Include.split(/\, /g);
  const hintPathNode = node.children && node.children[0];

  const result = {
    assemblyName: parts[0],
    version: null,
    culture: null,
    processorArchitecture: null,
    publicKeyToken: null,
    hintPath: null,
  };

  for (let i = 1; i < parts.length; i++) {
    const asmPartKeyValue = parts[i].split(/=/g);

    if (asmPartKeyValue.length === 2) {
      if (asmPartKeyValue[0] === "Version") {
        result.version = asmPartKeyValue[1];
      } else if (asmPartKeyValue[0] === "Culture") {
        result.culture = asmPartKeyValue[1];
      } else if (asmPartKeyValue[0] === "processorArchitecture") {
        result.processorArchitecture = asmPartKeyValue[1];
      } else if (asmPartKeyValue[0] === "PublicKeyToken") {
        result.publicKeyToken = asmPartKeyValue[1];
      }
    }
  }

  if (
    hintPathNode &&
    hintPathNode.name === "HintPath" &&
    hintPathNode.content
  ) {
    result.hintPath = helpers.normalizePath(hintPathNode.content);
  }

  return result;
};

const parsePackages = (filePath) => {
  return helpers.getFileContentsOrFail(filePath).then(parsePackagesInternal);
};

const parsePackagesSync = (filePath) => {
  const contents = helpers.getFileContentsOrFailSync(filePath);
  return parsePackagesInternal(contents);
};

const parsePackagesInternal = (contents) => {
  const xml = parseXml(contents);

  return xml.root.children.reduce((data, packageNode) => {
    if (packageNode.name === "package") {
      const parsedPackage = {
        name: packageNode.attributes.id,
        version: packageNode.attributes.version,
        targetFramework: packageNode.attributes.targetFramework,
      };

      data.push(parsedPackage);
    }

    return data;
  }, []);
};

const parseProject = (filePath, options) => {
  const providedOptions = options || {};
  return helpers.getFileContentsOrFail(filePath).then((contents) => {
    const result = parseProjectInternal(contents);

    if (!providedOptions.deepParse) {
      return result;
    } else {
      const projDir = helpers.getFileDirectory(filePath, options);
      const packagesLocation = path.join(projDir, "packages.config");

      return helpers.fileExists(packagesLocation).then((exists) => {
        if (!exists) {
          return result;
        } else {
          return parsePackages(packagesLocation).then((packages) => {
            result.packages = packages || [];
            return result;
          });
        }
      });
    }
  });
};

const parseProjectSync = (filePath, options) => {
  const providedOptions = options || {};
  const contents = helpers.getFileContentsOrFailSync(filePath);
  const result = parseProjectInternal(contents);

  if (providedOptions.deepParse) {
    const projDir = helpers.getFileDirectory(filePath, options);
    const packagesLocation = path.join(projDir, "packages.config");

    const packages =
      helpers.fileExistsSync(packagesLocation) &&
      parsePackagesSync(packagesLocation);
    result.packages = packages || [];
  }

  return result;
};

const parseProjectInternal = (contents) => {
  const xml = parseXml(contents);

  if (!xml || !xml.root) {
    throw new Error("No root element in project file");
  }

  return xml.root.children.reduce(
    (projectData, directChild) => {
      if (directChild.name === "ItemGroup") {
        const children = directChild.children;

        // TODO: Sequential dynamic mapping instead of assuming all children are same
        if (children && children.length) {
          if (children[0].name === "Reference") {
            const refs = children.map(parseAssemblyReference);
            projectData.references = projectData.references.concat(refs);
          } else if (children[0].name === "Compile") {
            const refs = children.map(parseCodeFile);
            projectData.codeFiles = projectData.codeFiles.concat(refs);
          }
        }
      }

      return projectData;
    },
    {
      references: [],
      codeFiles: [],
      packages: [],
    }
  );
};

module.exports = {
  parseProjectSync,
  parseProject,
  parsePackagesSync,
  parsePackages,
};
