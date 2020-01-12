const _toFirstUpperCase = function(str) {
  if (typeof str !== 'string') {
    throw new Error('You should pass a string!');
  }

  if (!str) {
    return str;
  }

  return str.substr(0, 1).toUpperCase() + str.substr(1);
};

export const toFirstUpperCase = _toFirstUpperCase;
