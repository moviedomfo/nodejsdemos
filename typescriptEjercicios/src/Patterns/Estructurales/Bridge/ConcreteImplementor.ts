import { IDevice } from "./Implementation";




export class Radio implements IDevice {

    private volume: number = 0;
    private chanel: number = 0;
    isEnabled(): boolean {
        return true;
    }
    getChannel(): number {
        return this.chanel;
    }
    getVolume(): number {
        return this.volume;
    }
    setChanel(chanel: number): void {
        this.chanel = chanel;

    }
    setVolume(volume: number): void {
        this.volume = volume;
    }
    // turnOn(): void {
    // 	console.log("Radio is turned on");
    // }
    // turnOff(): void {
    // 	console.log("Radio is turned off");
    // }
}

export class Tv implements IDevice {
    private volume: number = 0;
    private chanel: number = 0;
    isEnabled(): boolean {
        return true;
    }
    getChannel(): number {
        return this.chanel;
    }
    getVolume(): number {
        return this.volume;
    }
    setChanel(chanel: number): void {
        this.chanel = chanel;

    }
    setVolume(volume: number): void {
        this.volume = volume;
    }
    // turnOn(): void {
    // 	console.log("TV is turned on");
    // }
    // turnOff(): void {
    // 	console.log("TV is turned off");
    // }
}