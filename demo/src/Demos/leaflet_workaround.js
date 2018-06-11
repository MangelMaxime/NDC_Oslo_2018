// Workaround leaflet limitation when used with Webpack
// See: https://github.com/PaulLeCam/react-leaflet/issues/255#issuecomment-261904061
import L from 'leaflet';

// stupid hack so that leaflet's images work after going through webpack
import marker from './../../node_modules/leaflet/dist/images/marker-icon.png';
import marker2x from './../../node_modules/leaflet/dist/images/marker-icon-2x.png';
import markerShadow from './../../node_modules/leaflet/dist/images/marker-shadow.png';

delete L.Icon.Default.prototype._getIconUrl;

L.Icon.Default.mergeOptions({
    iconRetinaUrl: marker2x,
    iconUrl: marker,
    shadowUrl: markerShadow
});
