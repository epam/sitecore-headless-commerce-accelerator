'use strict';

const path = require('path');
const csproj = require('./csproj');
const helpers = require('./internal');

const parseMinimumVisualStudioVersion = (lineOfText) => {
  const regex = /^MinimumVisualStudioVersion = (\d+(\.\d+){3})/;
  const result = regex.exec(lineOfText);

  return result && result[1];
};

const parseVisualStudioVersion = (lineOfText) => {
  const regex = /^VisualStudioVersion = (\d+(\.\d+){3})/;
  const result = regex.exec(lineOfText);

  return result && result[1];
};

const parseFileFormatVersion = (lineOfText) => {
  const regex = /^Microsoft Visual Studio Solution File, Format Version (\d+\.\d+)/;
  const result = regex.exec(lineOfText);

  return result && result[1];
};

const parseNestedProject = (lineOfText) => {
  const regex = /\{([A-Z0-9]{8}\-[A-Z0-9]{4}\-[A-Z0-9]{4}\-[A-Z0-9]{4}\-[A-Z0-9]{12})\} = \{([A-Z0-9]{8}\-[A-Z0-9]{4}\-[A-Z0-9]{4}\-[A-Z0-9]{4}\-[A-Z0-9]{12})\}/;
  const result = regex.exec(lineOfText);

  if (result) {
    return {
      projectId: result[1],
      parentProjectId: result[2],
    };
  }
};

const parseSolutionProject = (lineOfText) => {
  const regex = /^Project\("\{([A-Z0-9]{8}\-[A-Z0-9]{4}\-[A-Z0-9]{4}\-[A-Z0-9]{4}\-[A-Z0-9]{12})\}"\) = "([^"]+)", "([^"]+)", "\{([A-Z0-9]{8}\-[A-Z0-9]{4}\-[A-Z0-9]{4}\-[A-Z0-9]{4}\-[A-Z0-9]{12})\}"/;
  const result = regex.exec(lineOfText);

  if (result) {
    return {
      id: result[4],
      name: result[2],
      relativePath: helpers.normalizePath(result[3]),
      projectTypeId: result[1],
    };
  }
};

const parseSolution = (filePath, options) => {
  const providedOptions = options || {};
  return helpers.getFileContentsOrFail(filePath).then((contents) => {
    const returnValue = parseSolutionInternal(contents);

    if (providedOptions.deepParse) {
      const slnDir = helpers.getFileDirectory(filePath, options);

      const projectPromises = returnValue.projects.map((project) => {
        if (project && project.relativePath) {
          const projectLocation = path.join(slnDir, project.relativePath);

          return helpers.fileExists(projectLocation).then((exists) => {
            return exists ? csproj.parseProject(projectLocation, providedOptions) : null;
          });
        } else {
          return null;
        }
      });

      return Promise.all(projectPromises).then((fullProjects) => {
        for (let i = 0; i < returnValue.projects.length; i++) {
          const projectData = fullProjects[i];
          if (projectData) {
            returnValue.projects[i] = Object.assign({}, returnValue.projects[i], projectData);
          }
        }

        return returnValue;
      });
    }

    return returnValue;
  });
};

const parseSolutionSync = (filePath, options) => {
  const providedOptions = options || {};
  const contents = helpers.getFileContentsOrFailSync(filePath);
  const returnValue = parseSolutionInternal(contents);

  if (providedOptions.deepParse) {
    const slnDir = helpers.getFileDirectory(filePath, options);

    for (let i = 0; i < returnValue.projects.length; i++) {
      const project = returnValue.projects[i];

      if (project && project.relativePath) {
        const projectLocation = path.join(slnDir, project.relativePath);

        if (helpers.fileExistsSync(projectLocation)) {
          const projectData = csproj.parseProjectSync(projectLocation, providedOptions);

          if (projectData) {
            returnValue.projects[i] = Object.assign({}, project, projectData);
          }
        }
      }
    }
  }

  return returnValue;
};

const parseSolutionInternal = (contents) => {
  const lines = contents.replace(/(\r\n|\r)/g, '\n').split('\n');

  const returnValue = {
    fileFormatVersion: null,
    visualStudioVersion: null,
    minimumVisualStudioVersion: null,
    projects: [],
    nestedProjects: [],
  };

  for (let i = 0; i < lines.length; i++) {
    const solutionProject = parseSolutionProject(lines[i]);
    if (solutionProject) {
      returnValue.projects.push(solutionProject);
    }

    const nestedProject = parseNestedProject(lines[i]);
    if (nestedProject) {
      returnValue.nestedProjects.push(nestedProject);
    }

    const fileFormatVersion = parseFileFormatVersion(lines[i]);
    if (fileFormatVersion) {
      returnValue.fileFormatVersion = fileFormatVersion;
    }

    const visualStudioVersion = parseVisualStudioVersion(lines[i]);
    if (visualStudioVersion) {
      returnValue.visualStudioVersion = visualStudioVersion;
    }

    const minimumVisualStudioVersion = parseMinimumVisualStudioVersion(lines[i]);
    if (minimumVisualStudioVersion) {
      returnValue.minimumVisualStudioVersion = minimumVisualStudioVersion;
    }
  }

  return returnValue;
};

module.exports = {
  parseSolutionSync,
  parseSolution,
};
