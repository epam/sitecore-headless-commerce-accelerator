import React, { FC, ReactNode } from 'react';

import { cnSocialNetworksLinks } from '../cn';

export type ItemProps = {
  className?: string;
  children?: ReactNode;
};

export const Item: FC<ItemProps> = ({ children, className }) => (
  <li className={cnSocialNetworksLinks('Item', [className])}>{children}</li>
);
