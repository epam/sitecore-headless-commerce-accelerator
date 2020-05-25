var traverse = require('traverse');
var jsonpath = require('jsonpath');
const tabdownParser = require('tabdown-kfatehi');
const vsParser = require('./vs-parse/index');

class SolutionFileExplorer {
  constructor(content) {
    this.content = content || {};
    this.solution = {};
  }

  toString() {
    return traverse(this.solution)
      .reduce(function (acc, x) {
        var hasValue = x && x.value;
        var indent = (this.level - 2) / 2;
        return hasValue && acc.push({ indent, value: x.value.trim() }), acc;
      }, [])
      .map((value) => `${'\t'.repeat(value.indent)}${value.value}`)
      .join('\r\n');
  }

  findNode(keyword) { 
    const result = jsonpath.query(this.solution, `$..children[?(@.value == "${keyword}" || @.value.match(/^.*${keyword}/g))]`);
    if(result && result.length) {
      return result[0];
    }
  }

  insert(node, ...values) {
    if (node) {
      const index = this.solution.children.indexOf(node);
      this.solution.children.splice(index + 1, 0, ...values);
    }
  }

  getProjectNodes(projects) {
    return projects
    .map(project => [
      ({ value: `Project("{${project.projectTypeId.toUpperCase()}}") = "${project.name}", "${project.relativePath}", "{${project.id.toUpperCase()}}"`}), 
      ({ value: `EndProject`})
    ])
    .reduce((a, b) => a.concat(b));
  }

  addNestedProject(node, project) {
    node.children = node.children || [];
    const { children, id } = project;

    const nestedProjectsLines = children.map(child => {
      if (child.children && child.children.length) {
        this.addNestedProject(node, child);
      }
      return ({ value: `{${child.id.toUpperCase()}} = {${id.toUpperCase()}}` });
    });

    node.children.push(...nestedProjectsLines);
  }

  addProjectConfigurationPlatform(node, projects, configurations) {
    if (node) {
      node.children = node.children || [];

      projects.forEach(project => 
        configurations.forEach(configuration => 
            node.children.push(
              ({ value: `{${project.id.toUpperCase()}}.${configuration}.ActiveCfg = ${configuration}` }), 
              ({ value: `{${project.id.toUpperCase()}}.${configuration}.Build.0 = ${configuration}` }))
      ));
    }
  }

  addChildren(project) {
    var children = jsonpath.query(this.solution, `$..nestedProjects[?(@.parentProjectId == "${project.id}")]`)
    project.children = project.children || [];

    children.forEach(child => {
      var a = jsonpath.query(this.solution, `$..projects[?(@.id == "${child.projectId}")]`)[0];
      project.children.push(a);
    });
    return project;
  }

  getProjects(projectNames) {
    this.solution = vsParser.parseSolutionSync(this.content);
    this.solution.projects.forEach((project) => {
        this.addChildren(project)
      }
    );

    return projectNames.map(name => jsonpath.query(this.solution, `$..projects[?(@.name == "${name}")]`)[0]);
  }

  addProjects(projects, settings) {
    this.solution = tabdownParser.parse(this.content, {
      linebreaks: true,
      indent: '\t',
    });

    projects.forEach((project) => {
      const children = jsonpath.query(project, `$..children`).reduce((a, b) => a.concat(b));
      const projectNodes = this.getProjectNodes(children);
      this.insert(this.findNode('MinimumVisualStudioVersion'), ...projectNodes);

      this.addNestedProject(this.findNode('NestedProjects'), project);

      const codeProjects = jsonpath.query(project, `$..children[?(@.projectTypeId == "${settings.codeProject}")]`)
      this.addProjectConfigurationPlatform(this.findNode('ProjectConfigurationPlatforms'), codeProjects, settings.buildConfiguration);
    });

    return this.solution;
  }
}

module.exports = SolutionFileExplorer;
