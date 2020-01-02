const _debounce = (func, wait, { context = this, immediate = false } = {}) => {
  let timeout;

  return () => {
    // FIXME
    const args = arguments;

    const later = () => {
      timeout = null;

      if (!immediate) {
        func.apply(context, args);
      }
    };

    const callNow = immediate && !timeout;
    clearTimeout(timeout);
    timeout = setTimeout(later, wait);

    if (callNow) {
      func.apply(context, args);
    }
  };
};

export function delay({ millis, value }) {
  return new Promise(function(resolve) {
    setTimeout(() => {
      resolve.bind(null, value)();
    }, millis);
  });
}

export function sleep(millis) {
  return delay({ millis, value: true });
}

export const debounce = _debounce;
