import { Tv } from './ConcreteImplementor';
import { Radio } from './ConcreteImplementor';
import { AdvancedRemoteControl } from './RefinedAbstraction';
export { pattern_bridge_client };
// Cliente.ts
//Querés evitar que una clase tenga una jerarquía muy rígida y compleja, y en su lugar, querés que la jerarquía de clases de implementación 
// sea independiente de la jerarquía de clases de abstracción. Querés que ambas jerarquías evolucionen independientemente una de la otra.
function pattern_bridge_client() {
    const implementationA = new Tv();
    const refinedAbstractionA = new AdvancedRemoteControl(implementationA);
    console.log('Cliente: Usando :' + refinedAbstractionA.getTypeName());
    refinedAbstractionA.mute();

    const implementationB = new Radio();
    const refinedAbstractionB = new AdvancedRemoteControl(implementationB);
    console.log('Cliente: Usando :' + refinedAbstractionB.getTypeName());
    refinedAbstractionB.mute();
}

export default pattern_bridge_client;