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
