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

import { applyMiddleware, compose, createStore, DeepPartial, Middleware, Store } from 'redux';
import createSagaMiddleware, { END, SagaMiddleware } from 'redux-saga';

import { routerMiddleware } from 'connected-react-router';
import { createBrowserHistory, createMemoryHistory, History } from 'history';

import customRouterMiddleware from 'Foundation/ReactJss/client/SitecoreContext/routerMiddleware';

import { AppState } from '../models';

import makeRootReducer from './reducer';
import sagas from './sagas';

export default class AppStore {
  public get instance() {
    return this.storeInstance;
  }

  public get history() {
    return this.routerHistory;
  }
  private storeInstance: Store;
  private sagaMiddleware: SagaMiddleware<{}>;

  private routerHistory: History;
  private routerMiddleware: Middleware;

  public constructor(preloadedState: DeepPartial<AppState>, ssr: boolean = false) {
    // init the sagaMiddleware in order to run it later
    this.sagaMiddleware = createSagaMiddleware();

    this.routerHistory = ssr ? createMemoryHistory() : createBrowserHistory();
    this.routerMiddleware = routerMiddleware(this.routerHistory);

    const middlewares = [this.sagaMiddleware, this.routerMiddleware, customRouterMiddleware];

    const composeEnhancers = this.getComposeEnhancer();

    const enhancer = composeEnhancers(applyMiddleware(...middlewares));
    const store = createStore(makeRootReducer(this.routerHistory), preloadedState, enhancer);

    if (!ssr) {
      this.runSaga();
    }

    this.storeInstance = store;
  }

  public close() {
    if (this.storeInstance) {
      this.storeInstance.dispatch(END);
    }
  }

  private runSaga() {
    if (this.sagaMiddleware) {
      this.sagaMiddleware.run(sagas);
    }
  }

  private getComposeEnhancer() {
    // return compose without REDUX DEV TOOLS when running production bundle
    if (process.env.NODE_ENV === 'production') {
      return compose;
    }

    return typeof window === 'object' && window['__REDUX_DEVTOOLS_EXTENSION_COMPOSE__']
      ? window['__REDUX_DEVTOOLS_EXTENSION_COMPOSE__']
      : compose;
  }
}
