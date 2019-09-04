//    Copyright 2019 EPAM Systems, Inc.
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

import * as React from 'react';

import * as JSS from 'Foundation/ReactJss/client';

import { GlobalFooterProps, GlobalFooterState } from './models';
import './styles.scss';

export default class GlobalFooterControl extends JSS.SafePureComponent<GlobalFooterProps, GlobalFooterState> {
    protected safeRender() {
        return (
            <footer id="footer-main" className="footer-main">
                <div className="footer-wrap rainbow-bar rainbow-bar-3">
                    <div className="row footer-columns">
                        <div className="col-md-9">
                            <div className="row footer-links-row">
                                <div className="col-xs-4 footer-links-col">
                                    <ul className="footer-links-list">
                                        <li className="footer-list-item"><a href="" title="" className="footer-link footer-link-primary">Gift Cards</a></li>
                                        <li className="footer-list-item"><a href="" title="" className="footer-link footer-link-primary">Find a Store</a></li>
                                        <li className="footer-list-item"><a href="" title="" className="footer-link footer-link-primary">Sign Up for Email</a></li>
                                        <li className="footer-list-item"><a href="" title="" className="footer-link footer-link-primary">Join Wooli</a></li>
                                    </ul>
                                </div>
                                <div className="col-xs-4 footer-links-col">
                                    <ul className="footer-links-list">
                                        <li className="footer-list-item"><a href="" title="" className="footer-link footer-link-primary">Get Help</a></li>
                                        <li className="footer-list-item"><a href="" title="" className="footer-link footer-link-secondary">Order Status</a></li>
                                        <li className="footer-list-item"><a href="" title="" className="footer-link footer-link-secondary">Shipping and Review</a></li>
                                        <li className="footer-list-item"><a href="" title="" className="footer-link footer-link-secondary">Returns</a></li>
                                        <li className="footer-list-item"><a href="" title="" className="footer-link footer-link-secondary">Payment Options</a></li>
                                        <li className="footer-list-item"><a href="" title="" className="footer-link footer-link-secondary">Contact US</a></li>
                                    </ul>
                                </div>
                                <div className="col-xs-4 footer-links-col">
                                    <ul className="footer-links-list">
                                        <li className="footer-list-item"><a href="" title="" className="footer-link footer-link-primary">News</a></li>
                                        <li className="footer-list-item"><a href="" title="" className="footer-link footer-link-primary">About Wooli</a></li>
                                        <li className="footer-list-item"><a href="" title="" className="footer-link footer-link-secondary">Careers</a></li>
                                        <li className="footer-list-item"><a href="" title="" className="footer-link footer-link-secondary">Investors</a></li>
                                        <li className="footer-list-item"><a href="" title="" className="footer-link footer-link-secondary">Supply Chain</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div className="col-md-3">
                            <h2 className="social-title">Social</h2>
                            <ul className="social-list">
                                <li className="social-item"><a href="twitter.com" target="_blank" title="" className="social-link"><i className="fa fa-twitter"/></a></li>
                                <li className="social-item"><a href="fb.com" target="_blank" title="" className="social-link"><i className="fa fa-facebook"/></a></li>
                                <li className="social-item"><a href="youtube.com" target="_blank" title="" className="social-link"><i className="fa fa-youtube"/></a></li>
                                <li className="social-item"><a href="instagram.com" target="_blank" title="" className="social-link"><i className="fa fa-instagram"/></a></li>
                            </ul>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-xs-12">
                            <p className="footer-copyright">© 2018 wooli™ All Rights Reserved Terms and Conditions</p>
                        </div>
                    </div>
                </div>
            </footer>
        );
    }
}
