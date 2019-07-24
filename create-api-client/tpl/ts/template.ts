
class BaseClientProxy {
  public static defaultHeaders: { [key: string]: string };
  protected transformOptions(options_) {
    // add creds
    options_.credentials = 'include';
    options_.mode = 'cors';
    if (BaseClientProxy.defaultHeaders) {
      options_.headers = { ...options_.headers, ...BaseClientProxy.defaultHeaders };
    }
    return Promise.resolve(options_);
  }

}
