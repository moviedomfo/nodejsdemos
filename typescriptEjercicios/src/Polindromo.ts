/**
 *Palabra o expresión que es igual si se lee de izquierda a derecha que de derecha a izquierda.
 * @param prahase
 */
const IsPalindrome = (prahase: string): boolean => {
  // clean all white space convert all to lowercase 
  const strA = prahase.replace(/[ ,]/g, "").toLowerCase();
  // [...strA] transform in array
  const strB = [...strA].reverse().join("");

  // If both are equals means that the word or prhase are Polindrome
  return strA === strB;
  
  //let IsPalindrome = true;
  // [...strA].forEach((item, index) => {
  //   IsPalindrome = item === strB[index];
  //   if (!IsPalindrome) {
  //     return;
  //   }
  // });
  // return IsPalindrome;

 
};

export default IsPalindrome;
