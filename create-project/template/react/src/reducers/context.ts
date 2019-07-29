import { onStart, onEnd, onError } from "../utils/configureMiddlewares";


export interface State {
  error: string|undefined;
  loading: boolean;
}

const defaultState: State = {
  loading: false,
  error: undefined
};

// Root Reducer
export default (state: State = defaultState, action: any): State => {
  if (onStart.matchAction(action)) {
    return { ...state, loading: true };
  } else if (onEnd.matchAction(action)) {
    return { ...state, loading: false };
  } else if (onError.matchAction(action)) {
    return { ...state, loading: false };
  }
  return state;
};

// Selectors
export const selLoading = (s: State) => s.loading;
export const selError = (s: State) => s.error;
