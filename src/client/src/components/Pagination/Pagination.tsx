//    Copyright 2020 EPAM Systems, Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

import React, { FC } from 'react';
import ReactPaginate, { ReactPaginateProps } from 'react-paginate';

import { Icon } from 'components';

import { cnPagination } from './cn';
import './Pagination.scss';

const previousLabel = <Icon icon="icon-angle-double-left" />;
const nextLabel = <Icon icon="icon-angle-double-right" />;

export const Pagination: FC<ReactPaginateProps> = ({
  pageCount,
  forcePage,
  pageRangeDisplayed,
  marginPagesDisplayed,
  onPageChange,
}) => {
  return (
    <ReactPaginate
      containerClassName={cnPagination()}
      forcePage={forcePage}
      pageCount={pageCount}
      pageRangeDisplayed={pageRangeDisplayed}
      marginPagesDisplayed={marginPagesDisplayed}
      nextLabel={nextLabel}
      previousLabel={previousLabel}
      pageClassName={cnPagination('Item')}
      previousClassName={cnPagination('Item', { previous: true })}
      nextClassName={cnPagination('Item', { next: true })}
      breakClassName={cnPagination('Item', { break: true })}
      pageLinkClassName={cnPagination('Link')}
      breakLinkClassName={cnPagination('Link')}
      previousLinkClassName={cnPagination('Link')}
      nextLinkClassName={cnPagination('Link')}
      activeClassName={cnPagination('Item', { active: true })}
      onPageChange={onPageChange}
    />
  );
};
