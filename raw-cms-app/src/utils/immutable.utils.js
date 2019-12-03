const _addOrReplace = ({ array, element, findFn }) => {
  const arrayCopy = [...array];
  const index = arrayCopy.findIndex(findFn);

  if (index >= 0) {
    arrayCopy[index] = element;
  } else {
    arrayCopy.push(element);
  }

  return arrayCopy;
};

export const addOrReplace = _addOrReplace;
