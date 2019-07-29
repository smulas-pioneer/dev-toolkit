import { createAction, createPromiseAction } from "redux-helper";

export const testAction = createAction<string>("TEST_ACTION");

export const testPromiseAction = createPromiseAction("TEST_PROMISE_ACTION", ()=>Promise.resolve('test'), testAction);
