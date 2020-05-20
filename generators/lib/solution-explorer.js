var traverse = require('traverse');
const tabdownParser = require('tabdown-kfatehi');
const vsParser = require('./vs-parse/index');

class SolutionExplorer {
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

  findNode(source, keyword) {
    for (var i = 0; i < source.length; i++) {
      if (source[i].value === keyword || source[i].value.includes(keyword)) {
        return source[i];
      } else if (source[i].children) {
        var result = this.findNode(source[i].children, keyword);
        if (result) return result;
      }
    }
    return null;
  }

  insert(node, ...values) {
    if (node) {
      const index = this.solution.children.indexOf(node);
      values.forEach((value, i) => {
        this.solution.children.splice(index + i + 1, 0, { value: value });
      });
    }
    return this.solution;
  }

  addProject(project) {
    const { projectTypeId, name, relativePath, id } = project;

    const projectBeginBlock = `Project("{${projectTypeId.toUpperCase()}}") = "${name}", "${relativePath}", "{${id.toUpperCase()}}"`;
    const projectEndBlock = `EndProject`;

    const node = this.findNode(this.solution.children, 'MinimumVisualStudioVersion');
    return this.insert(node, projectBeginBlock, projectEndBlock);
  }

  addProjects(projects) {
    projects.forEach((project) => {
      this.solution = this.addProject(project);
    });
    return this.solution;
  }

  addNestedProject(project) {
    const { parent, id } = project;

    const nestedProjectsLine = `{${id.toUpperCase()}} = {${parent.id.toUpperCase()}}`;
    const node = this.findNode(this.solution.children, 'GlobalSection(NestedProjects)');
    if (node) {
      node.children = node.children || [];
      node.children.push({ value: nestedProjectsLine });
    }

    return this.solution;
  }

  addNestedProjects(projects) {
    projects.forEach((project) => {
      if (project.parent) {
        this.solution = this.addNestedProject(project);
      }
    });
    return this.solution;
  }

  addProjectConfigurationPlatform(project, configuration) {
    const activeConfigLine = `{${project.id.toUpperCase()}}.${configuration}.ActiveCfg = ${configuration}`;
    const build0ConfigLine = `{${project.id.toUpperCase()}}.${configuration}.Build.0 = ${configuration}`;

    const node = this.findNode(this.solution.children, 'GlobalSection(ProjectConfigurationPlatforms)');
    if (node) {
      node.children = node.children || [];

      node.children.push({ value: activeConfigLine });
      node.children.push({ value: build0ConfigLine });
    }

    return this.solution;
  }

  addProjectsConfigurationPlatforms(projects, settings) {
    projects.forEach((project) =>
      settings.buildConfiguration.forEach(
        (configuration) => (this.solution = this.addProjectConfigurationPlatform(project, configuration)),
      ),
    );
    return this.solution;
  }

  addParent(project) {
    var nestedProject = this.solution.nestedProjects.find((_) => _.projectId == project.id);
    if (nestedProject) {
      project.parent = this.solution.projects.find((project) => project.id === nestedProject.parentProjectId);
    }
    return project;
  }

  getProjects(helixLayerNames) {
    this.solution = vsParser.parseSolutionSync(this.content);

    return helixLayerNames
      .map((helixLayerName) => {
        var projects = this.solution.projects.map((project) => this.addParent(project));
        projects = projects.filter((project) => project.name.includes('.' + helixLayerName + '.'));
        return projects.concat([...new Set(projects.map((project) => project.parent))]);
      })
      .reduce((a, b) => a.concat(b));
  }

  addLayers(projects, settings) {
    this.solution = tabdownParser.parse(this.content, {
      linebreaks: true,
      indent: '\t',
    });

    this.solution = this.addProjects(projects);
    this.solution = this.addNestedProjects(projects);
    this.solution = this.addProjectsConfigurationPlatforms(
      projects.filter((project) => project.projectTypeId == settings.codeProject),
      settings,
    );

    return this.solution;
  }
}

module.exports = SolutionExplorer;
