const _RawCMS = window.RawCMS || {};

_RawCMS.plugins = _RawCMS.plugins || {};

_RawCMS.loadComponentTpl = path => {
  return axios({
    url: path,
    method: 'get',
  }).then(x => {
    return x.data;
  });
};

_RawCMS.eventBus = new Vue();

window.RawCMS = _RawCMS;
export const RawCMS = _RawCMS;
