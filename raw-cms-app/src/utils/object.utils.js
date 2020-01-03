const _optionalChain = (
  expression,
  { fallbackValue = undefined, replaceLastUndefined = true } = {}
) => {
  let val;

  try {
    val = expression();
  } catch {
    return fallbackValue;
  }

  return val === undefined && replaceLastUndefined ? fallbackValue : val;
};

const _deepClone = obj => {
  return JSON.parse(JSON.stringify(obj));
};

const _nameOf = expr => {
  if (!expr || typeof expr !== 'function') {
    throw new Error('`expr` should be an arrow function!');
  }

  const indexOfArrow = expr => expr.indexOf('=>');

  if (indexOfArrow < 0) {
    throw new Error('Unable to find `=>` in expr`. Have you used an arrow function?');
  }

  const segments = expr
    .toString()
    .slice(indexOfArrow)
    .trim()
    .split('.');
  return segments[segments.length - 1];
};

export const optionalChain = _optionalChain;
export const deepClone = _deepClone;
export const nameOf = _nameOf;
