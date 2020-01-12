const _randomString = (
  length,
  { alphabet = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ' } = {}
) => {
  const arr = [];
  for (let i = 0; i < length; i++) {
    arr.push(alphabet[Math.floor(Math.random() * alphabet.length)]);
  }

  return arr.join('');
};

export const randomString = _randomString;
