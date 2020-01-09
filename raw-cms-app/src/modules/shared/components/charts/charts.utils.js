import { vuetifyColors } from '../../../../config/vuetify.js';
import { optionalChain } from '../../../../utils/object.utils.js';

const _transparentize = function(color, opacity) {
  const alpha = opacity === undefined ? 0.5 : 1 - opacity;
  return Color(color)
    .alpha(alpha)
    .rgbString();
};

const _colorize = function(value, { range = [0, 100], lowerIsBetter = false } = {}) {
  const min = optionalChain(() => range[0], 0);
  const max = optionalChain(() => range[1], 100);
  let colors = [vuetifyColors.red.darken4, vuetifyColors.orange.base, vuetifyColors.green.base];
  if (lowerIsBetter) {
    colors = colors.reverse();
  }
  const colorMap = d3.piecewise(d3.interpolate, colors);
  const interpolationValue = (value - min) / (max - min);

  const c = colorMap(interpolationValue);
  return c;
};

export const colorize = _colorize;
export const transparentize = _transparentize;
