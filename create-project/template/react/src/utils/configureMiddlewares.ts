import { Middleware, applyMiddleware, Store, compose, createStore, Reducer ,StoreEnhancer} from 'redux';
import checkedPromise, { CheckedPromiseMiddlewareOptions, CreateAction, createAction } from 'redux-helper';
import thunk from 'redux-thunk';

/**
 * @description= On Error action creator, this action will be dispatched on every promise action error
 */
export const onError = createAction<{ messageString: string, originalError: any }>('ON_ERROR');
/**
 * @description= On Start action creator, this action will be dispatched on every promise action start
 */

export const onStart = createAction<string>('ON_START');
/**
 * @description= On End action creator, this action will be dispatched on every succesful promise action end
 */

export const onEnd = createAction<void>('ON_END');


function configureCheckedPromiseMiddleware(promiseCycleActions?: PromiseCycleActions) {
    const psa = promiseCycleActions || {
        onStart,
        onEnd,
        onError
    };

    const cpmOptions: CheckedPromiseMiddlewareOptions = {
        onStart: (msg) => psa.onStart(msg),
        onEnd: (_) => psa.onEnd(),
        onError: (err: string | { response: string } | Error | any[]) => {
            let messageString = '';
            console.group('Error');
            if (typeof (err) === 'string') {
                messageString = err;
            } else if (Array.isArray(err) && err.length > 0) {
                messageString = err[0].Message || 'Error';

            } else if (typeof (err) === 'object') { //SWAGGER EXCEPTION
                var { response } = err as { response: string };
                if (response) {
                    messageString = response;
                } else {
                    messageString = err.toString();
                }
            } else {
                messageString = JSON.stringify(err);
            }
            console.warn(err);
            console.groupEnd();
            return psa.onError({ messageString, originalError: err });
        }
    };

    return cpmOptions;
}

const configureAll = (promiseCycleActions?: PromiseCycleActions): Middleware[] => {
    const configuredCpm = checkedPromise(configureCheckedPromiseMiddleware());
    const middlewares = [configuredCpm, thunk];
    return middlewares as any;
};

export const createMiddleware = (promiseCycleActions?: PromiseCycleActions):StoreEnhancer<{dispatch:any}> => applyMiddleware(...configureAll());

export interface PromiseCycleActions {
    onStart: CreateAction<string>,
    onEnd: CreateAction<void>,
    onError: CreateAction<{ messageString: string, originalError: any }>
}

export function configureStore<IAppState>(root: Reducer<IAppState>, useDevTools?: boolean): Store<IAppState>;
export function configureStore<IAppState>(root: Reducer<IAppState>, preloadedState?: IAppState, useDevTools?: boolean): Store<IAppState>;

export function configureStore<IAppState>(root: Reducer<IAppState>, preloadedStateOrUseDevTools?: IAppState | boolean, useDevTools?: boolean): Store<IAppState> {
    let enhancers = createMiddleware();

    // Dev Tools compose if available
    const wnd = window as any;
    const devTools = wnd.devToolsExtension !== undefined ? wnd.devToolsExtension() : (f: any) => f;
    const hasDevToolsFlag = preloadedStateOrUseDevTools && typeof (preloadedStateOrUseDevTools) === "boolean";

    const addDevTools = hasDevToolsFlag || (useDevTools || false);

    if (devTools && addDevTools) {
        enhancers = compose(enhancers, devTools) as any;
    }

    // create store
    if (!hasDevToolsFlag && preloadedStateOrUseDevTools !== undefined) return createStore<IAppState,any,any,any>(root,preloadedStateOrUseDevTools as any, enhancers);

    return createStore<IAppState,any,any,any>(root, enhancers);
}
