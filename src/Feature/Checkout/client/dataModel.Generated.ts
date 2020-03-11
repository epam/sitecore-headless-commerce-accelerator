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

// tslint:disable:indent array-type

    export interface CartLineRequest {
        productId: string;
        variantId: string;
    }
    export interface AddCartLineRequest extends CartLineRequest {
        quantity: number;
    }
    export interface PromoCodeRequest {
        promoCode: string;
    }
    export interface RemoveCartLineRequest extends CartLineRequest {
    }
    export interface UpdateCartLineRequest extends CartLineRequest {
        quantity: number;
    }
