const handlers: Array<(config: any) => void> = [];

// Add Listner
export const listenConfiguration = <T>(cb: (config: T) => void) => {
    handlers.push(cb);
};

export const loadConfiguration = (configFile: string) => {
    return new Promise((res, rej) => {
        fetch(configFile).then(k => k.json()).then(config => {
            handlers.forEach(p => p(config));
            res(config);
        }).catch(err => {
            // Will use empty object for configuration
            // Default values will be used.
            handlers.forEach(p => p({}));
            res({});
        });
    });
};