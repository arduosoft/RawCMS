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

export function debounce(func, wait) {
  let timeout;

  return function() {
    const context = this;
    const args = arguments;
    clearTimeout(timeout);
    timeout = setTimeout(function() {
      func.apply(context, args);
    }, wait);
  };
}
