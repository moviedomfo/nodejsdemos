//
// Bridge Pattern
// The Bridge Pattern is a structural design pattern that decouples an abstraction from its implementation so that the two can 
// vary independently.

import { IDevice } from "./Implementation";


/**
 * 
 * The Abstraction defines the interface for the abstraction and maintains a reference to an object of type Implementation. 
 * capa de control de alto nivel no tiene que hacer ningún trabajo real por su cuenta, sino que debe delegar el trabajo a la capa 
 * de implementación
 * Nota: no estamos hablando de las interfaces o las clases abstractas de tu lenguaje de programación. Son cosas diferentes.
 */
export abstract class RemoteControl {
    protected implementation: IDevice;

    constructor(implementation: IDevice) {
        this.implementation = implementation;
    }

    public volumeDown(): void {
        this.implementation.setVolume(this.implementation.getVolume() - 1);
    }

    public volumeUp(): void {
        this.implementation.setVolume(this.implementation.getVolume() + 1);
    }

    public channelUp(): void {
        this.implementation.setChanel(this.implementation.getChannel() + 1);
    }

    public channelDown(): void {
        this.implementation.setChanel(this.implementation.getChannel() - 1);
    }


}

