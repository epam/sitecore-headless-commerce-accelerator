"use strict";
exports.toPropertyType = (name) => {
    switch (name.toLowerCase()) {
      case 'tristate':
        return 'TriState';
  
      case 'checkbox':
        return 'bool';
  
      case 'date':
      case 'datetime':
        return 'DateTime';
  
      case 'number':
        return 'float';
  
      case 'integer':
        return 'int';
  
      case 'treelist with search':
      case 'treelist':
      case 'treelistex':
      case 'treelist descriptive':
      case 'checklist':
      case 'multilist with search':
      case 'multilist':
        return 'IEnumerable<Guid>';
  
      case 'grouped droplink':
      case 'droplink':
      case 'lookup':
      case 'droptree':
      case 'reference':
      case 'tree':
        return 'Guid';
  
      case 'file':
        return 'File';
  
      case 'image':
        return 'Image';
  
      case 'general link':
      case 'general link with search':
        return 'Link';
  
      case 'password':
      case 'icon':
      case 'rich text':
      case 'html':
      case 'single-line text':
      case 'multi-line text':
      case 'frame':
      case 'text':
      case 'memo':
      case 'droplist':
      case 'grouped droplist':
      case 'valuelookup':
        return 'string';
  
      case 'attachment':
      case 'word document':
        return 'System.IO.Stream';
  
      case 'name lookup value list':
      case 'name value list':
        return 'System.Collections.Specialized.NameValueCollection';

      case 'catalog selection control':
        return 'IEnumerable<string>';

      case 'commerce int64 control':
        return 'long';
  
      default:
        return `object /* UNKNOWN TYPE: ${name} */`;
    }
  };
