// Complejidad de password cuando resetea 
// 		+8
// 		1 mayuscula
// 		1 num
// 		1 caracter espesial +-*.$
const validarPassword = (password: string): boolean => {

    // Al menos 8 caracteres (.{8,})
    // Al menos una letra mayúscula ((?=.*[A-Z]))
    // Al menos un dígito ((?=.*\d))
    // Al menos un carácter especial entre +-*.$ ((?=.*[-+*$]))
    // No contiene espacios en blanco ((?!.*\s))
    const regex_ = /^(?=.*[A-Z])(?=.*\d)(?=.*[-+*$])(?!.*\s).{8,}$/;
    const regex = /^(?=.{8,}$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*\W).*$/;
    return regex.test(password);

}

export const ValidarPassword_Test = () => {

    // Ejemplo de uso:
    const password1 = "Password1*"; // Cumple con todas las reglas
    const password2 = "pass"; // No cumple con la longitud mínima
    const password3 = "Password"; // No cumple con los caracteres especiales
    const password4 = "password1*"; // No cumple con la mayúscula
    const password5 = "Password$"; // No cumple con el número

    const password6 = "rapi235**"; // Cumple con todas las reglas

    console.log(`${password1} --> ${validarPassword(password1)}`); // true
    console.log(`${password2} --> ${validarPassword(password2)}`); // false
    console.log(`${password3} --> ${validarPassword(password3)}`); // false
    console.log(`${password4} --> ${validarPassword(password4)}`); // false
    console.log(`${password5} --> ${validarPassword(password5)}`); // false

    console.log(`${password6} --> ${validarPassword(password6)}`); // false

}



