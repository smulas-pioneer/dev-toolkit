import { Redux } from 'app-support';
import * as fromContext from './context';

// Store Model
export interface AppState {
  context: fromContext.State;
}

// Root Reducer
export default Redux.combineReducers<AppState>({
  context: fromContext.default,
});

export const selError = (s: AppState) => fromContext.selError(s.context);
export const selLoading = (s: AppState) => fromContext.selLoading(s.context);
