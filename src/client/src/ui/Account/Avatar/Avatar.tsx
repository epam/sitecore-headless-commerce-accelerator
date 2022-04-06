//    Copyright 2022 EPAM Systems, Inc.
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

import React, { useCallback, useEffect, useRef, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import {
  addAccountImage as AddAccountImage,
  AddImage,
  removeAccountImage as RemoveAccountImage,
  RemoveImage,
  updateStatus as UpdateStatus,
} from 'services/account';
import { Button, Input } from 'components';
import { LoadingStatus } from 'models';

import { Dropdown } from '../Dropdown/Dropdown';

import { AvatarProps } from './models';

import { cnAvatar } from './cn';
import './Avatar.scss';

const AVATAR_CONTENT = {
  SIZE_ERROR_MESSAGE: 'Please upload image no more than 1Mb',
  TYPE_ERROR_MESSAGE: 'Please upload image of type .jpg, .png',
};

const AVATAR_DROPDOWN_CONTENT = {
  DELETE: 'Delete',
};

const IMAGE_EXTENSIONS = {
  JPG: 'jpg',
  PNG: 'png',
};

const MAX_IMAGE_SIZE = 1048576;

export const Avatar = (props: AvatarProps) => {
  const dispatch = useDispatch();
  const addAccountImage = useSelector(AddAccountImage);
  const removeAccountImage = useSelector(RemoveAccountImage);
  const updateStatus = useSelector(UpdateStatus);
  const isLoading = updateStatus === LoadingStatus.Loading;

  const { imageUrl, isAccountDetails } = props;

  const [image, setImage] = useState(null);
  const [isImageSizeValid, setIsImageSizeValid] = useState(true);
  const [isImageTypeValid, setIsImageTypeValid] = useState(true);
  const [isDropdownVisible, setIsDropdownVisible] = useState(false);

  const dropdownWrapperRef = useRef(null);

  const isImageValid = (image: File) => {
    setIsImageSizeValid(true);
    setIsImageTypeValid(true);

    if (image && image.size > MAX_IMAGE_SIZE) {
      setIsImageSizeValid(false);
      return false;
    }

    if (image && image.name) {
      const imageExtension = image.name.split('.').pop().toLowerCase();

      if (imageExtension !== IMAGE_EXTENSIONS.JPG && imageExtension !== IMAGE_EXTENSIONS.PNG) {
        setIsImageTypeValid(false);
        return false;
      }
    }

    return true;
  };

  const handleOnClick = (event: any) => {
    event.target.value = '';
  };

  const handleOnChange = (event: any) => {
    const newImage = event.target?.files?.[0];

    const isNewImageValid = isImageValid(newImage);

    if (newImage && isNewImageValid) {
      dispatch(AddImage(newImage));
    }
  };

  const handleOnButtonDeleteClick = () => {
    dispatch(RemoveImage());
    setIsDropdownVisible(false);
  };

  const handleOutsideClick = useCallback(
    (e: MouseEvent) => {
      if (dropdownWrapperRef.current && !dropdownWrapperRef.current.contains(e.target)) {
        setIsDropdownVisible(false);
      }
    },
    [dropdownWrapperRef],
  );

  const handleOnButtonClick = () => {
    setIsDropdownVisible(!isDropdownVisible);
  };

  useEffect(() => {
    if (isLoading) {
      setIsImageSizeValid(true);
      setIsImageTypeValid(true);
    }

    if (addAccountImage && addAccountImage.imageUrl) {
      setImage(addAccountImage.imageUrl);
    }

    if (removeAccountImage && removeAccountImage.status === LoadingStatus.Loaded) {
      setImage(null);
    }
  }, [isLoading, addAccountImage, removeAccountImage, imageUrl]);

  useEffect(() => {
    window.addEventListener('click', handleOutsideClick, false);

    return () => {
      window.removeEventListener('click', handleOutsideClick, false);
    };
  }, []);

  const isImageDeleted = removeAccountImage.status === LoadingStatus.Loaded;
  const isImageAdding = addAccountImage.status === LoadingStatus.Loading;
  const isImageAdded = addAccountImage.status === LoadingStatus.Loaded;
  const isImage = !!((imageUrl && !isImageAdding && !isImageAdded && !isImageDeleted) || image);

  const getImageSrc = () => {
    if (image || (isImageDeleted && imageUrl)) {
      return image;
    }
    if (imageUrl && !isImageAdding && !isImageAdded) {
      return imageUrl;
    }
    return image;
  };

  const dropdownItems = [
    {
      text: AVATAR_DROPDOWN_CONTENT.DELETE,
      handler: handleOnButtonDeleteClick,
    },
  ];

  return (
    <div className={cnAvatar({ image: isImage, isAccountDetails: isAccountDetails })}>
      {isImage ? (
        <img src={getImageSrc()} className={cnAvatar('Image')} />
      ) : (
        <svg width="126" height="126" viewBox="0 0 126 126" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path
            d="M62.9999 34.2563C51.5811 34.2563 42.1311 43.7063 42.1311 55.1251C42.1311 66.5438 51.5811 75.9938 62.9999 75.9938C74.4186 75.9938 83.8686 66.5438 83.8686 55.1251C83.8686 43.3126 74.4186 34.2563 62.9999 34.2563ZM62.9999 71.6626C53.5499 71.6626 46.0686 64.1813 46.0686 54.7313C46.0686 45.2813 53.5499 37.8001 62.9999 37.8001C72.4499 37.8001 79.9311 45.2813 79.9311 54.7313C79.9311 64.1813 72.4499 71.6626 62.9999 71.6626Z"
            fill="#767676"
          />
          <path
            d="M25.6162 95.4038L25.5989 95.3779L25.5786 95.3543C18.1708 86.777 13.4939 75.4726 13.4939 62.9999C13.4939 35.7135 35.7138 13.4937 63.0001 13.4937C90.2865 13.4937 112.506 35.7135 112.506 62.9999C112.506 75.4786 108.22 86.7719 100.438 95.336L97.6943 98.0799C88.7245 106.658 76.6413 112.112 63.0001 112.112C49.3479 112.112 37.2699 107.044 28.31 98.0839L28.2749 98.0488L28.2337 98.0214C27.1074 97.2705 26.3671 96.5301 25.6162 95.4038ZM30.3858 95.6965L29.9686 96.0568L30.3678 96.437C38.723 104.394 50.263 109.569 63.0001 109.569C75.7373 109.569 87.2773 104.394 95.6325 96.437L96.0317 96.0568L95.6145 95.6965C86.8685 88.1432 75.3358 83.3687 63.0001 83.3687C50.6645 83.3687 39.1318 88.1432 30.3858 95.6965ZM97.3209 93.695L97.701 94.0276L98.0298 93.6441C105.189 85.2917 109.569 74.547 109.569 62.9999C109.569 37.13 88.87 16.4312 63.0001 16.4312C37.1303 16.4312 16.4314 37.13 16.4314 62.9999C16.4314 74.9365 20.8087 85.6867 27.9785 93.6531L28.3154 94.0275L28.6873 93.6879C37.6532 85.5017 50.1335 80.4312 63.0001 80.4312C75.8611 80.4312 87.9529 85.4979 97.3209 93.695Z"
            fill="#767676"
            stroke="#767676"
          />
        </svg>
      )}
      {isAccountDetails && (
        <div
          className={cnAvatar('UploadImageButton', {
            image: isImage,
            isAccountDetails: isAccountDetails,
          })}
        >
          <label htmlFor="upload" className={cnAvatar('UploadImageButtonLabel')}>
            <svg width="18" height="18" viewBox="0 0 18 18" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path
                d="M15.2972 5.10112H12.8166L11.3743 3.6555C11.3726 3.65381 11.3709 3.65268 11.3698 3.651L11.367 3.64875C11.1504 3.43443 10.8529 3.30225 10.5238 3.30225H7.52513C7.17638 3.30225 6.86588 3.453 6.6465 3.68981L6.64538 3.68643L5.23294 5.10168H2.70169C2.03907 5.10168 1.50244 5.63887 1.50244 6.3015V13.4987C1.50244 14.1613 2.03963 14.6985 2.70169 14.6985H15.2966C15.9592 14.6985 16.4964 14.1613 16.4964 13.4987V6.3015C16.4964 5.63887 15.9592 5.10168 15.2966 5.10168L15.2972 5.10112ZM15.8974 13.4981C15.8974 13.8283 15.6279 14.0977 15.2972 14.0977H2.70225C2.3715 14.0977 2.10263 13.8283 2.10263 13.4981V6.30093C2.10263 5.97018 2.37207 5.70131 2.70225 5.70131H5.48213L6.38438 4.79737L6.399 4.84125L7.0875 4.0965C7.20394 3.9705 7.35975 3.90131 7.52569 3.90131H10.5244C10.683 3.90131 10.8326 3.96262 10.9502 4.07906L12.5679 5.70075H15.2972C15.6279 5.70075 15.8974 5.96962 15.8974 6.30037V13.4976V13.4981Z"
                fill="white"
              />
              <path
                d="M9.00001 5.70166C6.84676 5.70166 5.10132 7.44653 5.10132 9.59978C5.10132 11.753 6.84676 13.499 9.00001 13.499C11.1533 13.499 12.8987 11.753 12.8987 9.59978C12.8987 7.44653 11.1533 5.70166 9.00001 5.70166ZM9.00001 12.8988C7.18088 12.8988 5.70151 11.4189 5.70151 9.59978C5.70151 7.78066 7.18144 6.30185 9.00001 6.30185C10.8186 6.30185 12.2985 7.78122 12.2985 9.59978C12.2985 11.4189 10.8186 12.8988 9.00001 12.8988Z"
                fill="white"
              />
            </svg>
          </label>
          <Input
            type="file"
            name="upload"
            id="upload"
            accept=".png, .jpg"
            className={cnAvatar('UploadImageButtonInput')}
            onChange={handleOnChange}
            onClick={handleOnClick}
            error={!isImageSizeValid || !isImageTypeValid}
            helperText={
              (!isImageSizeValid && AVATAR_CONTENT.SIZE_ERROR_MESSAGE) ||
              (!isImageTypeValid && AVATAR_CONTENT.TYPE_ERROR_MESSAGE)
            }
          />
        </div>
      )}
      {isAccountDetails && isImage && (
        <div ref={dropdownWrapperRef}>
          <Button className={cnAvatar('RemoveImageButton')} rounded buttonTheme="clear" onClick={handleOnButtonClick} />
          <Dropdown isDropdownVisible={isDropdownVisible} dropdownItems={dropdownItems} />
        </div>
      )}
    </div>
  );
};
