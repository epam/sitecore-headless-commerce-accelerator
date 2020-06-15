const isUpper = (str) => {
  return Array.from(str).every(x => x == x.toUpperCase());
}

const isLower = (str) => {
  return Array.from(str).every(x => x == x.toLowerCase());
}

const formatFirstChar = (str, formatter) => {
  let firstPart = formatter(str.substring(0, 1));
  let lastPart = str.substring(1);
  return firstPart + lastPart;
}

const toPascalCase = (str) => {
  return str
    .replace(/(?:^\w|[A-Z]|\b\w)/g, (match) => {
      return match.toUpperCase();
    })
    .replace(/[^a-zA-Z\d]+/g, '');
};

const toCamelCase = (str) => {
  let splittedPhrase = str.split(/[ -.]/);

  let result = '';
  let isFirst = true;
  splittedPhrase.forEach(function (s) {
    if (s.length <= 0)
      return;

    let modifiedValue = !isFirst
      ? formatFirstChar(s, x => x.toUpperCase())
      : (isUpper(s)
        ? s.toLowerCase()
        : formatFirstChar(s, x => x.toLowerCase()));

    result += modifiedValue;
    isFirst = false;
  });

  return result;
};

module.exports.toClass = function (name) {
  return toPascalCase(name);
}

module.exports.toInterface = function (name) {
  return toPascalCase(name);
}

module.exports.toProperty = function (name) {
  return toCamelCase(name);
}

module.exports.toPropertyType = function (typeName, id) {
  switch (typeName.toLowerCase()) {
    case 'tristate':
      return `{} /* UNKNOWN TYPE: ${typeName} */`;
    case 'checkbox':
      return 'ReactJssModule.Field<boolean>';
    case 'date':
    case 'datetime':
      return 'ReactJssModule.Field<Date>';
    case 'number':
    case 'integer':
      return 'ReactJssModule.Field<number>';
    case 'treelist with search':
    case 'treelist':
    case 'treelistex':
    case 'treelist descriptive':
    case 'checklist':
    case 'multilist with search':
    case 'multilist':
      return 'ReactJssModule.ItemList<ReactJssModule.BaseDataSourceItem>';
    case 'grouped droplink':
    case 'droplink':
    case 'lookup':
    case 'droptree':
    case 'reference':
    case 'tree':
      return 'ReactJssModule.Item<ReactJssModule.BaseDataSourceItem>';
    case 'file':
      return `{} /* UNKNOWN TYPE: ${typeName} */`;
    case 'image':
      return 'ReactJssModule.ImageField';
    case 'general link':
    case 'general link with search':
      return 'ReactJssModule.LinkField';
    case 'rich text':
      return 'ReactJssModule.RichTextField';
    case 'password':
    case 'icon':
    case 'html':
    case 'single-line text':
    case 'multi-line text':
    case 'frame':
    case 'text':
    case 'memo':
    case 'droplist':
    case 'grouped droplist':
    case 'valuelookup':
    case 'name value list':
      return 'ReactJssModule.TextField';
    case 'attachment':
    case 'word document':
    case 'name lookup value list':
      return `{} /* UNKNOWN TYPE: ${typeName} */`;
  }

  return `{} /* UNKNOWN TYPE: ${typeName} */`;
}
