import * as fromContext from './context';
import { combineReducers } from 'redux';

// Store Model
export interface AppState {
  context: fromContext.State;
}

// Root Reducer
export default combineReducers<AppState>({
  context: fromContext.default,
});

export const selError = (s: AppState) => fromContext.selError(s.context);
export const selLoading = (s: AppState) => fromContext.selLoading(s.context);
