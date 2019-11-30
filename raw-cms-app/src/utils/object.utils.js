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

export const optionalChain = _optionalChain;
export const deepClone = _deepClone;
