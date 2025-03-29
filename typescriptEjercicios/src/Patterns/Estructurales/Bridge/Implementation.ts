

/**
 * La Implementación declara la interfaz común a todas las implementaciones concretas. 
 * Una abstracción sólo se puede comunicar con un objeto de implementación a través de los métodos que se declaren aquí.
 */
export interface IDevice {
    isEnabled(): boolean;
    getChannel(): number;
    getVolume(): number;
    setChanel(chanel: number): void;
    setVolume(volume: number): void;
}