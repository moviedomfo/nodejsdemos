let texto = "Hola Mundo";
function revertir(a){
    let invertido = "";
    n = a.length-1;
    while (n > -1 ){  
      invertido = invertido + a[n];
      n --;
    }
    console.log(invertido);
}

revertir(texto);