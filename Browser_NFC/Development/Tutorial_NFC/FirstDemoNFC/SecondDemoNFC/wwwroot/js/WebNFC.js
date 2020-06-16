class WebNFC {
    constructor(scan) {
        this.scan = scan;
    }

    async read(read) {
        if ('NDEFReader' in window) {
            const reader = new NDEFReader();
            const abortController = new AbortController();
            const permissionStatus = await navigator.permissions.query({ name: "nfc" });

            // Abort NFC Scanning Trigger
            abortController.signal.onabort = signal => {
                if ('stopped' in this.scan) {
                    // On NFC Scanning Stopped
                    this.scan.stopped(signal);
                }
            };

            await reader.scan({ signal: abortController.signal }).then(() => {
                reader.onerror = () => {
                    if ('error' in read) {
                        // On NFC Read Error
                        read.error();
                    }
                };
                reader.onreading = tag => {
                    if ('success' in read) {
                        // On NFC Read Success
                        read.success(tag);
                    }
                };
            }).catch(error => {
                if ('error' in this.scan) {
                    // On NFC Scan Error
                    this.scan.error(error);
                }
            });

            if (permissionStatus.state !== 'granted') {
                if ('denied' in this.scan) {
                    // NFC Perrmission Denied By User
                    this.scan.denied();
                }
            } else {
                if ('granted' in this.scan) {
                    // NFC Perrmission Granted By User
                    this.scan.granted();
                }
            }

            if ('stop' in this.scan) {
                // Stop NFC Scanning
                this.scan.stop(abortController);
            }

        } else {
            if ('unsupported' in this.scan) {
                // NFC Read Not Support in Browser (Only Chrome 81 to <= 83 Supported)
                this.scan.unsupported();
            }
        }
    }

    async write(action) {
        if ('NDEFWriter' in window) {
            // Write Tag
        } else {
            // NFC Write Not Support in Browser (Only Chrome 81 to <= 83 Supported)
        }
    }
}

Object.defineProperty(NDEFRecord.prototype, 'textRecordToString', {
    value: function textRecordToString() {
        if (this.recordType === 'text') {
            const decoder = new TextDecoder(this.encoding);
            return decoder.decode(this.data);
        }
    },
    writable: true,
    configurable: true
});

var scan = {
    error: function (error) {
        // NFC Scan Error Event
    },
    stop: function (abortController) {
        // NFC Stop Scan Event
        // Stop NFC scanning by executing abortController.abort()
    },
    stopped: function (signal) {
        // NFC Scanning Stopped Event
    },
    denied: function () {
        // NFC Is Denied Event
        // Remediation: Go to Browser Settings > Advanced > Site Settings > NFC Devices > Select "${location.host}" > Select NFC Devices > Allow
    },
    granted: function () {
        // NFC Is Granted Event
    },
    unsupported: function () {
        // NFC Not Supported In Browser Event
    }
}

var read = {
    success: function (tag) {
        var serialNumber = tag.serialNumber;
        var message = tag.message;
        for (const record of message.records) {
            switch (record.recordType) {
                case "empty":
                    break;
                case "text":
                    var recordText = record.textRecordToString();
                    console.log(recordText);
                    break;
                case "url":
                    break;
                case "smart-poster":
                    break;
                case "absolute-url":
                    break;
                case "mime":
                    break;
                case "unknown":
                default:
                    break;
            }
        }
    },
    error: function () {
        console.log('read failed');
    }
}

new WebNFC(scan).read(read);