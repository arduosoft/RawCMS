const _transparentize = function(color, opacity) {
  const alpha = opacity === undefined ? 0.5 : 1 - opacity;
  return Color(color)
    .alpha(alpha)
    .rgbString();
};

const _colorize = function(isOpaque, isHover, value) {
  const v = value;
  const c = v < -50 ? '#D60000' : v < 0 ? '#F46300' : v < 50 ? '#0358B6' : '#44DE28';

  const opacity = isHover ? 1 - Math.abs(v / 150) - 0.2 : 1 - Math.abs(v / 150);

  return isOpaque ? c : _transparentize(c, opacity);
};

export const colorize = _colorize;
export const transparentize = _transparentize;
