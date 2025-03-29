import { RemoteControl } from './Abstraction';
import { IDevice } from './Implementation';

export class AdvancedRemoteControl extends RemoteControl {
    constructor(device: IDevice) {
        super(device);
    }

    mute(): void {
        console.log('Muting the device...');
        this.implementation.setVolume(0);
    }
}