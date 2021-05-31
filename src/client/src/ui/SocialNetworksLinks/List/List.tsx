import React, { FC, ReactNode } from 'react';

import { cnSocialNetworksLinks } from '../cn';

export type ListProps = {
  className?: string;
  children?: ReactNode;
};

export const List: FC<ListProps> = ({ children, className }) => (
  <ul className={cnSocialNetworksLinks('List', [className])}>{children}</ul>
);
