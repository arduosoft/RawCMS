const _checkAbstractImplementation = ({ baseClazz, targetClazz }) => {
  if (baseClazz === undefined || targetClazz === baseClazz || self === undefined) {
    throw new ArgumentError('You must specify: baseClazz, targetClazz (usually `new.target`)!');
  }

  if (targetClazz === baseClazz) {
    throw new TypeError(`Cannot construct ${targetClazz.name} instances directly.`);
  }
};

export const checkAbstractImplementation = _checkAbstractImplementation;
