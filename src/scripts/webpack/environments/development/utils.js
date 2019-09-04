class ManifestsManager {
  constructor(manifests) {
    this.manifests = manifests || [];
  }

  getManifest(projectName) {
    const manifest = this.manifests.find((m) => m.name === projectName);

    if (!manifest) {
      throw new Error(`Manifest for '${projectName}' not found!`);
    }

    return manifest;
  }

  getClientEntry(projectName) {
    const manifest = this.getManifest(projectName);

    const { clientEntry } = manifest;
    if (!clientEntry) {
      throw new Error(`Manifest for '${projectName}' hasn't contain clientEntry property or it is empty`);
    }

    return clientEntry;
  }

  getEnvConfiguration(projectName, envName) {
    const manifest = this.getManifest(projectName);

    const { env } = manifest;

    if (!env) {
      throw new Error(`Manifest for '${projectName}' hasn't contain env property`);
    }

    const currentEnv = env.find((e) => e.name === envName);

    if (!currentEnv) {
      throw new Error(`Manifest for '${projectName}' hasn't contain env item with '${envName}' name`);
    }

    return currentEnv;
  }

  getApiKey(projectName, envName) {
    const currentEnv = this.getEnvConfiguration(projectName, envName);

    const { apiKey } = currentEnv;
    if (!apiKey) {
      throw new Error(`Manifest for '${projectName}' hasn't contain apiKey in '${envName}' configuration`);
    }

    return apiKey;
  }

  getEnvUrl(projectName, envName) {
    const currentEnv = this.getEnvConfiguration(projectName, envName);

    const { url } = currentEnv;
    if (!url) {
      throw new Error(`Manifest for '${projectName}' hasn't contain url in '${envName}' configuration`);
    }

    return url;
  }
}

module.exports = {
  ManifestsManager,
};
