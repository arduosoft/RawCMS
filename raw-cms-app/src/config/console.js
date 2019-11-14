let _oldConsoleError = null;

const _ignoredMessages = ["TypeError: Cannot read property 'clientHeight' of undefined"];

const _tweakConsole = function() {
  _oldConsoleError = window.console.error;
  window.console.error = function() {
    const msg = arguments[0];

    if (!msg || typeof msg !== 'string') {
      _oldConsoleError(...arguments);
    }

    if (_ignoredMessages.includes(msg)) {
      return;
    }

    _oldConsoleError(...arguments);
  };
};

export const tweakConsole = _tweakConsole;
