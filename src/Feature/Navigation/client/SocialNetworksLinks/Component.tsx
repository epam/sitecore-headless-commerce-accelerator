import * as Jss from 'Foundation/ReactJss/client';
import * as React from 'react';

import { Link, Text } from '@sitecore-jss/sitecore-jss-react';

import { SocialLinksProps, SocialLinksState } from './models';
import './styles.scss';

export default class SocialNetworksLinks extends Jss.SafePureComponent<SocialLinksProps, SocialLinksState> {
    protected safeRender() {
      const { datasource } = this.props.fields.data;

      return (
        <div>
          <Text tag="h2" field={datasource.sectionTitle.jss} className="social-title" />
          <ul className="social-list">
            {datasource.links && datasource.links.items && datasource.links.items.map((link, index) => {
                const { uri } = link;
                return (
                  <li key={index} className="social-item">
                    {this.props.isPageEditingMode
                      ? <Link field={uri.jss} className="social-link"/>
                      : <Link field={uri.jss} className="social-link"><i className={uri.jss.value.class}/></Link>
                    }
                  </li>
                );
            })}
          </ul>
        </div>
      );
    }
  }