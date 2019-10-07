class RawCms {
  plugins = {};
  utils = {};

  vuexStore;
  env = {};

  eventBus = new Vue();

  loadComponentTpl = path => {
    return axios.get(path).then(x => {
      return x.data;
    });
  };
}

const _rawCms = new RawCms();

window.RawCMS = _rawCms;
export const RawCMS = _rawCms;
export default _rawCms;
