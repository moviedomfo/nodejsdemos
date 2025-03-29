import { RemoteControl } from './Abstraction';
import { IDevice } from './Implementation';

/**
 * RefinedAbstraction
 * The RefinedAbstraction extends the interface defined by the Abstraction and adds new functionality.
 * This class can also override the methods defined by the Abstraction if needed.
 */
export class AdvancedRemoteControl extends RemoteControl {

    constructor(device: IDevice) {
        super(device);
    }
    override volumeDown(): void {
        console.log('Advanced toggle power functionality.');
        this.volumeDown();
    }
    mute(): void {
        console.log('Muting the device...');
        this.implementation.setVolume(0);
    }
}