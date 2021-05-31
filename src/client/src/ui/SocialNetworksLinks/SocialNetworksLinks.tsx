import React, { FC, memo } from 'react';
import { GraphQLRenderingWithParams } from 'Foundation/ReactJss';

import { get, omit } from 'lodash';
import { Link, Text } from '@sitecore-jss/sitecore-jss-react';

import { getRenderingSitecoreProps } from 'Foundation/utils';
import {
  SocialNetworksLinksParamsRenderingParams,
  SocialNetworksLinksDataSource,
} from 'services/socialNetworkLink/models/generated';

import { Item } from './Item';
import { List } from './List';

import { cnSocialNetworksLinks } from './cn';
import './SocialNetworksLinks.scss';

export type SocialNetworksLinksProps = GraphQLRenderingWithParams<
  SocialNetworksLinksDataSource,
  SocialNetworksLinksParamsRenderingParams
>;

export const SocialNetworksLinks: FC<SocialNetworksLinksProps> = memo((props) => {
  const rendering = getRenderingSitecoreProps(props);
  const linkList = get(rendering, ['fields', 'data', 'datasource', 'links', 'items'], []);
  const title = get(rendering, ['fields', 'data', 'datasource', 'sectionTitle', 'jss']);

  const params = get(rendering, ['params']);
  const { titleTag, titleClass, containerClass, showTitle } = params;

  return (
    <div className={cnSocialNetworksLinks(null, [containerClass])}>
      {showTitle && <Text className={cnSocialNetworksLinks('Title', [titleClass])} tag={titleTag} field={title} />}
      <List>
        {linkList.map((item: any) => {
          const itemUriValues = item.uri.jss.value;

          return (
            <Item key={item.id}>
              <Link field={omit(itemUriValues, ['class'])}>
                <i className={cnSocialNetworksLinks('Icon', [itemUriValues.class])} />
              </Link>
            </Item>
          );
        })}
      </List>
    </div>
  );
});
