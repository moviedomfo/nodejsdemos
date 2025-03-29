import { Tv } from './ConcreteImplementor';
import { Radio } from './ConcreteImplementor';
import { AdvancedRemoteControl } from './RefinedAbstraction';

// Cliente.ts

function main() {
    const implementationA = new Tv();
    const refinedAbstractionA = new AdvancedRemoteControl(implementationA);
    console.log('Cliente: Usando RefinedAbstraction con ConcreteImplementationA:');
    refinedAbstractionA.mute();
    
    const implementationB = new Radio();
    const refinedAbstractionB = new AdvancedRemoteControl(implementationB);
    console.log('Cliente: Usando RefinedAbstraction con ConcreteImplementationB:');
    refinedAbstractionB.mute();
}

main();