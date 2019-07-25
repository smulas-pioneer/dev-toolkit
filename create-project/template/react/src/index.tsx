import React from 'react';
import ReactDOM from 'react-dom';
import { Provider, configureStore, loadConfiguration, Route, HashRouter } from 'app-support';
import { RouteComponentProps } from 'react-router';
import { App } from './components/App';
import 'semantic-ui-css/semantic.min.css';
import * as reducers from './reducers';
//import * as serviceWorker from './serviceWorker';

const store = configureStore(reducers.default, true);

loadConfiguration('config.json').then((cfg: any) => {
    ReactDOM.render(
        <Provider store={store}>
            <HashRouter basename={cfg.APPNAME || ''}>
                <Route path={`/`} component={(p: RouteComponentProps<any>) => <App />} />
            </HashRouter>
        </Provider>,
        document.getElementById('root')
    );
});

//serviceWorker.unregister();
