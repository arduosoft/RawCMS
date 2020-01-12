const _getFieldSearchPayload = (value, fieldType) => {
  switch (fieldType) {
    case 'number':
    case 'int':
      return Number(value);

    case 'text':
      return { $regex: `.*${value}.*` };

    case 'bool':
    case 'regexp':
    case 'date':
    case 'list':
    case 'relation':
    case 'entities-list':
    case 'fields-list':
    default:
      return undefined;
  }
};

export const getFieldSearchPayload = _getFieldSearchPayload;
