import { get } from 'lodash';

export const getRenderingSitecoreProps = (obj: object) => get(obj, 'rendering');
