const _optionalChain = (expression, { fallbackValue = undefined, replaceLastUndefined = true }) => {
  let val;

  try {
    val = expression();
  } catch {
    return fallbackValue;
  }

  return val === undefined && replaceLastUndefined ? fallbackValue : val;
};

export const optionalChain = _optionalChain;
